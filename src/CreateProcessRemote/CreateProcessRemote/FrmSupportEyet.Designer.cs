namespace CreateProcessRemote
{
    partial class FrmCreateProcessRemote
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gprConfiguracao = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkSalvar = new System.Windows.Forms.CheckBox();
            this.txtSenha = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtLogin = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDominio = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gprComputadores = new System.Windows.Forms.GroupBox();
            this.btnDiscovery = new System.Windows.Forms.Button();
            this.btnExportar = new System.Windows.Forms.Button();
            this.chkSelecionarTodos = new System.Windows.Forms.CheckBox();
            this.btnImportar = new System.Windows.Forms.Button();
            this.btnRemover = new System.Windows.Forms.Button();
            this.btnAdicionar = new System.Windows.Forms.Button();
            this.txtComputador = new System.Windows.Forms.TextBox();
            this.lstComputadores = new System.Windows.Forms.CheckedListBox();
            this.gprAcao = new System.Windows.Forms.GroupBox();
            this.btnPesquisa = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCaminho = new System.Windows.Forms.TextBox();
            this.btnProcessar = new System.Windows.Forms.Button();
            this.pgbStatus = new System.Windows.Forms.ProgressBar();
            this.lblStatus = new System.Windows.Forms.Label();
            this.gprConfiguracao.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.gprComputadores.SuspendLayout();
            this.gprAcao.SuspendLayout();
            this.SuspendLayout();
            // 
            // gprConfiguracao
            // 
            this.gprConfiguracao.Controls.Add(this.groupBox1);
            this.gprConfiguracao.Dock = System.Windows.Forms.DockStyle.Top;
            this.gprConfiguracao.Location = new System.Drawing.Point(0, 0);
            this.gprConfiguracao.Name = "gprConfiguracao";
            this.gprConfiguracao.Size = new System.Drawing.Size(784, 73);
            this.gprConfiguracao.TabIndex = 0;
            this.gprConfiguracao.TabStop = false;
            this.gprConfiguracao.Text = "Configuração";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkSalvar);
            this.groupBox1.Controls.Add(this.txtSenha);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtLogin);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtDominio);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 19);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(760, 47);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Autenticação";
            // 
            // chkSalvar
            // 
            this.chkSalvar.AutoSize = true;
            this.chkSalvar.Location = new System.Drawing.Point(698, 17);
            this.chkSalvar.Name = "chkSalvar";
            this.chkSalvar.Size = new System.Drawing.Size(56, 17);
            this.chkSalvar.TabIndex = 6;
            this.chkSalvar.Text = "Salvar";
            this.chkSalvar.UseVisualStyleBackColor = true;
            // 
            // txtSenha
            // 
            this.txtSenha.Location = new System.Drawing.Point(527, 15);
            this.txtSenha.Name = "txtSenha";
            this.txtSenha.PasswordChar = '*';
            this.txtSenha.Size = new System.Drawing.Size(165, 20);
            this.txtSenha.TabIndex = 5;
            this.txtSenha.Text = "Liviston456";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(474, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Senha : ";
            // 
            // txtLogin
            // 
            this.txtLogin.Location = new System.Drawing.Point(303, 15);
            this.txtLogin.Name = "txtLogin";
            this.txtLogin.Size = new System.Drawing.Size(165, 20);
            this.txtLogin.TabIndex = 3;
            this.txtLogin.Text = "0402451";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(255, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Login : ";
            // 
            // txtDominio
            // 
            this.txtDominio.Location = new System.Drawing.Point(84, 15);
            this.txtDominio.Name = "txtDominio";
            this.txtDominio.Size = new System.Drawing.Size(165, 20);
            this.txtDominio.TabIndex = 1;
            this.txtDominio.Text = "ARAUJOCORP";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Dominio : ";
            // 
            // gprComputadores
            // 
            this.gprComputadores.Controls.Add(this.btnDiscovery);
            this.gprComputadores.Controls.Add(this.btnExportar);
            this.gprComputadores.Controls.Add(this.chkSelecionarTodos);
            this.gprComputadores.Controls.Add(this.btnImportar);
            this.gprComputadores.Controls.Add(this.btnRemover);
            this.gprComputadores.Controls.Add(this.btnAdicionar);
            this.gprComputadores.Controls.Add(this.txtComputador);
            this.gprComputadores.Controls.Add(this.lstComputadores);
            this.gprComputadores.Dock = System.Windows.Forms.DockStyle.Top;
            this.gprComputadores.Location = new System.Drawing.Point(0, 73);
            this.gprComputadores.Name = "gprComputadores";
            this.gprComputadores.Size = new System.Drawing.Size(784, 280);
            this.gprComputadores.TabIndex = 1;
            this.gprComputadores.TabStop = false;
            this.gprComputadores.Text = "Computadores";
            // 
            // btnDiscovery
            // 
            this.btnDiscovery.Location = new System.Drawing.Point(585, 18);
            this.btnDiscovery.Name = "btnDiscovery";
            this.btnDiscovery.Size = new System.Drawing.Size(75, 23);
            this.btnDiscovery.TabIndex = 8;
            this.btnDiscovery.Text = "Discovery";
            this.btnDiscovery.UseVisualStyleBackColor = true;
            this.btnDiscovery.Click += new System.EventHandler(this.btnDiscovery_Click);
            // 
            // btnExportar
            // 
            this.btnExportar.Location = new System.Drawing.Point(504, 18);
            this.btnExportar.Name = "btnExportar";
            this.btnExportar.Size = new System.Drawing.Size(75, 23);
            this.btnExportar.TabIndex = 7;
            this.btnExportar.Text = "Exportar";
            this.btnExportar.UseVisualStyleBackColor = true;
            this.btnExportar.Click += new System.EventHandler(this.btnExportar_Click);
            // 
            // chkSelecionarTodos
            // 
            this.chkSelecionarTodos.AutoSize = true;
            this.chkSelecionarTodos.Location = new System.Drawing.Point(5, 44);
            this.chkSelecionarTodos.Name = "chkSelecionarTodos";
            this.chkSelecionarTodos.Size = new System.Drawing.Size(109, 17);
            this.chkSelecionarTodos.TabIndex = 6;
            this.chkSelecionarTodos.Text = "Selecionar Todos";
            this.chkSelecionarTodos.UseVisualStyleBackColor = true;
            this.chkSelecionarTodos.CheckedChanged += new System.EventHandler(this.chkSelecionarTodos_CheckedChanged);
            // 
            // btnImportar
            // 
            this.btnImportar.Location = new System.Drawing.Point(423, 18);
            this.btnImportar.Name = "btnImportar";
            this.btnImportar.Size = new System.Drawing.Size(75, 23);
            this.btnImportar.TabIndex = 5;
            this.btnImportar.Text = "Importar";
            this.btnImportar.UseVisualStyleBackColor = true;
            this.btnImportar.Click += new System.EventHandler(this.btnImportar_Click);
            // 
            // btnRemover
            // 
            this.btnRemover.Location = new System.Drawing.Point(341, 18);
            this.btnRemover.Name = "btnRemover";
            this.btnRemover.Size = new System.Drawing.Size(75, 23);
            this.btnRemover.TabIndex = 4;
            this.btnRemover.Text = "Remover";
            this.btnRemover.UseVisualStyleBackColor = true;
            // 
            // btnAdicionar
            // 
            this.btnAdicionar.Location = new System.Drawing.Point(260, 18);
            this.btnAdicionar.Name = "btnAdicionar";
            this.btnAdicionar.Size = new System.Drawing.Size(75, 23);
            this.btnAdicionar.TabIndex = 3;
            this.btnAdicionar.Text = "Adicionar";
            this.btnAdicionar.UseVisualStyleBackColor = true;
            this.btnAdicionar.Click += new System.EventHandler(this.btnAdicionar_Click);
            // 
            // txtComputador
            // 
            this.txtComputador.Location = new System.Drawing.Point(5, 19);
            this.txtComputador.Name = "txtComputador";
            this.txtComputador.Size = new System.Drawing.Size(249, 20);
            this.txtComputador.TabIndex = 2;
            this.txtComputador.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtComputador_KeyDown);
            // 
            // lstComputadores
            // 
            this.lstComputadores.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lstComputadores.FormattingEnabled = true;
            this.lstComputadores.HorizontalScrollbar = true;
            this.lstComputadores.Location = new System.Drawing.Point(3, 63);
            this.lstComputadores.MultiColumn = true;
            this.lstComputadores.Name = "lstComputadores";
            this.lstComputadores.Size = new System.Drawing.Size(778, 214);
            this.lstComputadores.TabIndex = 0;
            // 
            // gprAcao
            // 
            this.gprAcao.Controls.Add(this.btnPesquisa);
            this.gprAcao.Controls.Add(this.label5);
            this.gprAcao.Controls.Add(this.txtCaminho);
            this.gprAcao.Dock = System.Windows.Forms.DockStyle.Top;
            this.gprAcao.Location = new System.Drawing.Point(0, 353);
            this.gprAcao.Name = "gprAcao";
            this.gprAcao.Size = new System.Drawing.Size(784, 66);
            this.gprAcao.TabIndex = 2;
            this.gprAcao.TabStop = false;
            this.gprAcao.Text = "Ação";
            // 
            // btnPesquisa
            // 
            this.btnPesquisa.Location = new System.Drawing.Point(749, 39);
            this.btnPesquisa.Name = "btnPesquisa";
            this.btnPesquisa.Size = new System.Drawing.Size(29, 23);
            this.btnPesquisa.TabIndex = 8;
            this.btnPesquisa.Text = "...";
            this.btnPesquisa.UseVisualStyleBackColor = true;
            this.btnPesquisa.Click += new System.EventHandler(this.btnPesquisa_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 44);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Caminho : ";
            // 
            // txtCaminho
            // 
            this.txtCaminho.Location = new System.Drawing.Point(60, 40);
            this.txtCaminho.Name = "txtCaminho";
            this.txtCaminho.Size = new System.Drawing.Size(685, 20);
            this.txtCaminho.TabIndex = 3;
            // 
            // btnProcessar
            // 
            this.btnProcessar.Location = new System.Drawing.Point(709, 425);
            this.btnProcessar.Name = "btnProcessar";
            this.btnProcessar.Size = new System.Drawing.Size(75, 23);
            this.btnProcessar.TabIndex = 4;
            this.btnProcessar.Text = "Processar";
            this.btnProcessar.UseVisualStyleBackColor = true;
            this.btnProcessar.Click += new System.EventHandler(this.btnProcessar_Click);
            // 
            // pgbStatus
            // 
            this.pgbStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pgbStatus.Location = new System.Drawing.Point(0, 449);
            this.pgbStatus.Name = "pgbStatus";
            this.pgbStatus.Size = new System.Drawing.Size(784, 23);
            this.pgbStatus.TabIndex = 5;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(3, 433);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 13);
            this.lblStatus.TabIndex = 6;
            // 
            // FrmCreateProcessRemote
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 472);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.pgbStatus);
            this.Controls.Add(this.btnProcessar);
            this.Controls.Add(this.gprAcao);
            this.Controls.Add(this.gprComputadores);
            this.Controls.Add(this.gprConfiguracao);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FrmCreateProcessRemote";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Create Process Remote By Matheus Silas";
            this.gprConfiguracao.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gprComputadores.ResumeLayout(false);
            this.gprComputadores.PerformLayout();
            this.gprAcao.ResumeLayout(false);
            this.gprAcao.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gprConfiguracao;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtSenha;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtLogin;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDominio;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkSalvar;
        private System.Windows.Forms.GroupBox gprComputadores;
        private System.Windows.Forms.CheckBox chkSelecionarTodos;
        private System.Windows.Forms.Button btnImportar;
        private System.Windows.Forms.Button btnRemover;
        private System.Windows.Forms.Button btnAdicionar;
        private System.Windows.Forms.TextBox txtComputador;
        private System.Windows.Forms.CheckedListBox lstComputadores;
        private System.Windows.Forms.GroupBox gprAcao;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtCaminho;
        private System.Windows.Forms.Button btnProcessar;
        private System.Windows.Forms.ProgressBar pgbStatus;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnPesquisa;
        private System.Windows.Forms.Button btnDiscovery;
        private System.Windows.Forms.Button btnExportar;
    }
}

