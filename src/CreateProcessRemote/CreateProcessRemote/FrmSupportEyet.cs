﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.DirectoryServices;
using System.IO;
using System.Linq;
using System.Management;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CreateProcessRemote
{
    public partial class FrmCreateProcessRemote : Form
    {
        public List<WorkstationDto> workstations { get; private set; }
        public CredentialDto Credential { get; private set; }
        public StringBuilder sbError { get; set; }
        public StringBuilder sbInformation { get; set; }
        public FrmCreateProcessRemote()
        {
            InitializeComponent();
            workstations = new List<WorkstationDto>();
            Credential = new CredentialDto();
            sbError = new StringBuilder();
            sbInformation = new StringBuilder();
            string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            Text += $" - Versão .:{version}";
            GetCredencial();            
        }

        private void GetCredencial()
        {
            txtDominio.Text = Properties.Settings.Default.Domain ?? "";
            txtLogin.Text = Properties.Settings.Default.Username ?? "";
            txtSenha.Text = Properties.Settings.Default.Password ?? "";
            chkSalvar.Checked = !string.IsNullOrEmpty(Properties.Settings.Default.Domain);
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtComputador.Text))
            {
                WorkstationDto workstation = AddWorkstation(txtComputador.Text);
                lstComputadores.Items.Add(workstation.Name, workstation.IsOnline);
                txtComputador.Text = string.Empty;

            }
        }

        private WorkstationDto AddWorkstation(string name)
        {
            WorkstationDto workstation = null;
            try
            {
                workstation = new WorkstationDto
                {
                    Name = name,
                    IsOnline = PingHost(name)
                };

                workstations.Add(workstation);

                return workstation;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnProcessar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarProcessar())
                {
                    Task.Run(() => { Processar(); });
                }
            }
            catch (Exception ex)
            {
                ExibeMensagem(ex.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Processar()
        {
            try
            {
                DesabilitarBotao(false);
                StatusProcesso("Aguarde processando...");
                SetaProgressBar(0, workstations.Count);
                sbError = new StringBuilder();
                sbInformation = new StringBuilder();
                Parallel.ForEach(workstations, item =>
                {
                    SetaProgressBar();
                    if (ComputadorSelecionando(item.Name))
                    {
                        Processar(item);
                    }
                });

                ExibeMensagem("Processo finalizado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (sbError.Length > 0)
                {
                    string nomeArquivo = Path.Combine(Path.GetTempPath(), Path.GetFileNameWithoutExtension(Path.GetTempFileName()) + ".txt");
                    File.WriteAllText(nomeArquivo, sbError.ToString(), Encoding.Default);
                    Task.Run(() => { Process.Start(nomeArquivo); });
                }
                Exportar();

            }
            catch (Exception ex)
            {
                ExibeMensagem(ex.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                DesabilitarBotao(true);
                SetaProgressBar(0, 0);
            }
        }

        private void Processar(WorkstationDto item)
        {
            int tentativas = 0;
            string version = string.Empty;
            try
            {
                if (item.IsOnline)
                {                       

                    item.Detalhe = CopyFile(item.Name);
                    var result = CreateRemoteProcess(item.Name, $@"""C:\{Path.GetFileName(txtCaminho.Text)}""");
                    do
                    {
                        Thread.Sleep(TimeSpan.FromSeconds(2));
                        tentativas++;
                    } while (VerificarProcesso(item.Name, result) && tentativas < 5);                                                            
                }
            }
            catch (Exception ex)
            {
                sbError.AppendLine($@"{item.Name} => Erro ao executar o arquivo {ex.ToString()}");
            }
        }

        private bool VerificarProcesso(string serverName, int processId)
        {
            try
            {
                var connection = Connect();
                if (serverName == Environment.MachineName)
                {
                    connection = null;
                }
                var scope = new ManagementScope(string.Format(@"\\{0}\root\cimv2", serverName), connection);
                scope.Connect();

                SelectQuery msQuery = new SelectQuery($"SELECT * FROM Win32_Process WHERE ProcessID ='{processId}'");
                EnumerationOptions enumOptions = new EnumerationOptions
                {
                    ReturnImmediately = true,
                    Rewindable = false
                };

                ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher(scope, msQuery, enumOptions);
                foreach (ManagementObject dr in managementObjectSearcher.Get())
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string CopyFile(string name)
        {
            try
            {
                ImpersonationHelper.Impersonate(Credential.Domain, Credential.Username, Credential.Password, delegate
                {
                    File.Copy(txtCaminho.Text, $@"\\{name}\C$\{Path.GetFileName(txtCaminho.Text)}", true);
                });
                return string.Empty;
            }
            catch (Exception ex)
            {

                sbError.AppendLine($@"{name} => Erro ao copiar o arquvivo {ex.ToString()}");
                return $@"{name} => Erro ao copiar o arquvivo {ex.Message}";
            }
        }

        private bool ComputadorSelecionando(string name)
        {
            bool result = false;
            foreach (object Item in lstComputadores.CheckedItems)
            {
                if (name == Item.ToString())
                {
                    result = true;
                    break;
                }
            }

            return result;
        }

        private bool ValidarProcessar()
        {
            if (!ValidarCredenciais())
            {
                return false;
            }
            if (string.IsNullOrEmpty(txtCaminho.Text))
            {
                ExibeMensagem("Informe o caminho do instalador", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCaminho.Focus();
                return false;
            }
            
            if (lstComputadores.CheckedItems.Count == 0)
            {
                ExibeMensagem("Selecione pelo menos um computador", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            return true;
        }

        private bool ValidarCredenciais()
        {
            if (string.IsNullOrEmpty(txtDominio.Text))
            {
                ExibeMensagem("Informe o dominio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDominio.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtLogin.Text))
            {
                ExibeMensagem("Informe o Login", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtLogin.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtSenha.Text))
            {
                ExibeMensagem("Informe a senha", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSenha.Focus();
                return false;
            }
            Credential = new CredentialDto
            {
                Domain = txtDominio.Text,
                Username = txtLogin.Text,
                Password = txtSenha.Text
            };
            SalvarCredenciais();
            return true;
        }

        private bool RemoveProgram(string serverName, string nameProgrma)
        {
            try
            {
                var connection = Connect();
                if (serverName == Environment.MachineName)
                {
                    connection = null;
                }
                var scope = new ManagementScope(string.Format(@"\\{0}\root\cimv2", serverName), connection);
                scope.Connect();

                ManagementObjectSearcher searchProcedure = BuscarProgramaInstalado(scope, nameProgrma);

                return RemoveProgram(searchProcedure);
            }
            catch (Exception ex)
            {
                sbError.AppendLine($@"{serverName} => Erro ao remover o EyeT {ex.ToString()}");
                return false;
            }
        }

        private bool VerificarProgramaInstalado(ManagementObjectSearcher managementObjectSearcher, out string version)
        {
            version = string.Empty;
            try
            {
                foreach (ManagementObject dr in managementObjectSearcher.Get())
                {
                    version = dr["version"].ToString() ?? "1.0.0.0";
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private ManagementObjectSearcher BuscarProgramaInstalado(ManagementScope scope, string nameProgrma)
        {
            try
            {
                SelectQuery msQuery = new SelectQuery($"SELECT * FROM Win32_Product WHERE Name LIKE '%{nameProgrma}%'");
                EnumerationOptions enumOptions = new EnumerationOptions
                {
                    ReturnImmediately = true,
                    Rewindable = false
                };

                ManagementObjectSearcher searchProcedure = new ManagementObjectSearcher(scope, msQuery, enumOptions);

                return searchProcedure;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool RemoveProgram(ManagementObjectSearcher searchProcedure)
        {
            try
            {
                foreach (ManagementObject dr in searchProcedure.Get())
                {
                    string version = dr["version"].ToString() ?? "1.0.0.0";
                    if (version.Substring(0, 2) == "1.")
                    {
                        var outParams = dr.InvokeMethod("Uninstall", null);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private ConnectionOptions Connect()
        {
            SalvarCredenciais();
            return new ConnectionOptions
            {
                Username = $@"{Credential.Domain}\{Credential.Username}",
                Password = Credential.Password,
                EnablePrivileges = true,
                Impersonation = ImpersonationLevel.Impersonate,
                Authentication = AuthenticationLevel.PacketPrivacy,
            };
        }

        private void SalvarCredenciais()
        {
            if (chkSalvar.Checked)
            {
                Properties.Settings.Default.Domain = txtDominio.Text;
                Properties.Settings.Default.Username = txtLogin.Text;
                Properties.Settings.Default.Password = txtSenha.Text;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.Reset();
            }
        }

        private int CreateRemoteProcess(string serverName, string processName, params string[] arguments)
        {
            try
            {
                var connection = Connect();
                if (serverName == Environment.MachineName)
                {
                    connection = null;
                }
                var scope = new ManagementScope(string.Format(@"\\{0}\root\cimv2", serverName), connection);
                scope.Connect();

                ObjectGetOptions objectGetOptions = new ObjectGetOptions();
                ManagementPath managementPath = new ManagementPath("Win32_Process");
                ManagementClass classInstance = new ManagementClass(scope, managementPath, objectGetOptions);

                ManagementBaseObject inParams = classInstance.GetMethodParameters("Create");
                if (arguments != null)
                {
                    foreach (var param in arguments)
                    {
                        processName += " " + param;
                    }
                }
                inParams["CommandLine"] = processName;

                ManagementBaseObject outParams = classInstance.InvokeMethod("Create", inParams, null);

                var returnValue = Convert.ToInt32(outParams["returnValue"]);
                return Convert.ToInt32(outParams["ProcessId"]);
            }
            catch (Exception ex)
            {
                sbError.AppendLine($@"{serverName} => Erro ao instalar o EyeT {ex.ToString()}");
                return 0;
            }
        }

        public static bool PingHost(string nameOrAddress)
        {
            bool pingable = false;
            Ping pinger = null;

            try
            {
                pinger = new Ping();
                PingReply reply = pinger.Send(nameOrAddress);
                pingable = reply.Status == IPStatus.Success;
            }
            catch (PingException)
            {
                // Discard PingExceptions and return false;
            }
            finally
            {
                if (pinger != null)
                {
                    pinger.Dispose();
                }
            }

            return pingable;
        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            if (ValidarCredenciais())
            {
                Task.Run(() => { Scan(); });
            }
        }

        private void Scan()
        {
            try
            {
                DesabilitarBotao(false);
                StatusProcesso("Aguarde escaneandos as máquinas...");
                SetaProgressBar(0, workstations.Count);
                sbError = new StringBuilder();
                Parallel.ForEach(workstations, item =>
                {
                    if (ComputadorSelecionando(item.Name))
                    {
                        var versionEyet = GetVersion(item.Name);                        
                    }
                    SetaProgressBar();
                });

                ExibeMensagem("Processo finalizado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (sbError.Length > 0)
                {
                    string nomeArquivo = Path.Combine(Path.GetTempPath(), Path.GetFileNameWithoutExtension(Path.GetTempFileName()) + ".txt");
                    File.WriteAllText(nomeArquivo, sbError.ToString(), Encoding.Default);
                    Task.Run(() => { Process.Start(nomeArquivo); });
                }
                Exportar();
            }
            catch (Exception ex)
            {
                ExibeMensagem(ex.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                DesabilitarBotao(true);
                SetaProgressBar(0, 0);
            }
        }

        private List<WorkstationDto> GetWorkstations()
        {
            try
            {
                var list = new List<WorkstationDto>();
                foreach (object Item in lstComputadores.CheckedItems)
                {
                    list.Add(new WorkstationDto
                    {
                        Name = Item.ToString(),
                        IsOnline = PingHost(Item.ToString())
                    });
                }
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string GetVersion(string name)
        {
            string result = string.Empty;
            try
            {
                if (PingHost(name))
                {
                    result = GetVersion(name, "EyeT");
                }

                return result;
            }
            catch (Exception ex)
            {
                sbError.AppendLine($@"{name} => Erro ao obter a versão do EyeT{ex.ToString()}");
                return result;
            }
        }

        private string GetVersion(string serverName, string nameProgrma)
        {
            string result = string.Empty;
            try
            {
                var connection = Connect();
                if (serverName == Environment.MachineName)
                {
                    connection = null;
                }
                var scope = new ManagementScope(string.Format(@"\\{0}\root\cimv2", serverName), connection);
                scope.Connect();

                ManagementObjectSearcher searchProcedure = BuscarProgramaInstalado(scope, nameProgrma);

                foreach (ManagementObject dr in searchProcedure.Get())
                {
                    result += $"{dr["version"].ToString() ?? "1.0.0.0"}|";
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void txtComputador_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keys.Enter == e.KeyData)
            {
                btnAdicionar_Click(null, null);
            }
        }

        private void btnPesquisa_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog fd = new OpenFileDialog())
            {
                fd.Filter = "Instalador|*.*";
                fd.InitialDirectory = @"C:\";
                fd.Title = "Selecione o arquivo";
                fd.Multiselect = false;

                if (fd.ShowDialog() == DialogResult.OK)
                {
                    txtCaminho.Text = fd.FileName;
                }
            }
        }

        private void btnImportar_Click(object sender, EventArgs e)
        {
            try
            {
                Importar();
            }
            catch (Exception ex)
            {
                ExibeMensagem(ex.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Importar()
        {
            try
            {
                using (OpenFileDialog fd = new OpenFileDialog())
                {
                    fd.Filter = "Arquivo texto|*.txt";
                    fd.InitialDirectory = @"C:\";
                    fd.Title = "Selecione o arquivo";
                    fd.Multiselect = false;

                    if (fd.ShowDialog() == DialogResult.OK)
                    {
                        string text = File.ReadAllText(fd.FileName);
                        var hosts = text.Split(Environment.NewLine.ToCharArray()).ToList();
                        Task.Run(() => { Importar(hosts); });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Importar(List<string> hosts)
        {
            try
            {
                DesabilitarBotao(false);
                StatusProcesso("Verificando máquinas online...");
                SetaProgressBar(0, hosts.Count);

                Parallel.ForEach(hosts, host =>
                {
                    if (!string.IsNullOrEmpty(host))
                    {
                        AdicionarComputador(AddWorkstation(host));
                    }
                });

                ExibeMensagem("Processo Finalizado.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                DesabilitarBotao(true);
                SetaProgressBar(0, 0);
            }
        }

        private void btnDiscovery_Click(object sender, EventArgs e)
        {
            if (ValidarCredenciais())
            {
                Task.Run(() => { Discovery(); });
            }
        }

        private void Discovery()
        {
            try
            {
                DesabilitarBotao(false);
                StatusProcesso("Aguarde pesquisando máquinas no domínio...");
                DirectoryEntry DomainEntry = new DirectoryEntry($"WinNT://{Credential.Domain}", Credential.Username, Credential.Password);
                DomainEntry.Children.SchemaFilter.Add("Computer");
                foreach (DirectoryEntry machine in DomainEntry.Children)
                {
                    workstations.Add(new WorkstationDto { Name = machine.Name });

                }

                StatusProcesso("Verificando máquinas online...");
                SetaProgressBar(0, workstations.Count);
                Parallel.ForEach(workstations, item =>
                {
                    SetaProgressBar();
                    item.IsOnline = PingHost(item.Name);
                    AdicionarComputador(item);
                });

                ExibeMensagem("Processo Finalizado.", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                DesabilitarBotao(true);
                SetaProgressBar(0, 0);
            }
        }

        private void DesabilitarBotao(bool action)
        {
            BeginInvoke(new Action(() =>
            {
                gprAcao.Enabled = action;
                gprComputadores.Enabled = action;
                gprConfiguracao.Enabled = action;                
                btnProcessar.Enabled = action;
            }));
        }

        private void AdicionarComputador(WorkstationDto item)
        {
            BeginInvoke(new Action(() =>
            {
                lstComputadores.Items.Add(item.Name, item.IsOnline);
            }));
        }

        private DialogResult ExibeMensagem(string mensagem, MessageBoxButtons button, MessageBoxIcon icon)
        {
            return (DialogResult)Invoke(new Func<DialogResult>(() => { return MessageBox.Show(this, mensagem, Text, button, icon); }));
        }

        private void StatusProcesso(string mensagem)
        {
            BeginInvoke(new Action(() =>
            {
                lblStatus.Text = mensagem;
            }));
        }

        private void SetaProgressBar(int mim, int max)
        {
            BeginInvoke(new Action(() =>
            {
                pgbStatus.Minimum = mim;
                pgbStatus.Maximum = max;
                pgbStatus.Value = 0;
            }));
        }

        private void SetaProgressBar()
        {
            BeginInvoke(new Action(() =>
            {
                pgbStatus.Increment(1);
            }));
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            Exportar();
        }

        private void Exportar()
        {
            try
            {
                string texto = "Nome;Online;Detalhe" + Environment.NewLine;
                texto += string.Join(Environment.NewLine, Array.ConvertAll(workstations.ToArray(), element => element.ToString()));
                string nomeArquivo = Path.Combine(Path.GetTempPath(), Path.GetFileNameWithoutExtension(Path.GetTempFileName()) + ".txt");
                File.WriteAllText(nomeArquivo, texto, Encoding.Default);
                Task.Run(() => { Process.Start(nomeArquivo); });
                ExibeMensagem($"Exportação Concluída {nomeArquivo}", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void chkSelecionarTodos_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i <= (lstComputadores.Items.Count - 1); i++)
            {
                if (chkSelecionarTodos.Checked)
                {
                    lstComputadores.SetItemCheckState(i, CheckState.Checked);
                }
                else
                {
                    lstComputadores.SetItemCheckState(i, CheckState.Unchecked);
                }
            }
        }

        private void btnTeste_Click(object sender, EventArgs e)
        {
            var result = CreateRemoteProcess("VDISETAP01", @"NET USE Z: ""\\GEINFCSM109\PUBLICO\SETUP""");
            result = CreateRemoteProcess("VDISETAP01", "CMD /C", $@"ipconfig > C:\teste.log");
        }
    }
}
