namespace CreateProcessRemote
{
    public class WorkstationDto
    {
        public string Name { get; set; }        
        public bool IsOnline { get; set; }                
        public string Detalhe { get; set; }

        public override string ToString()
        {
            return $"{Name};{Online};{Detalhe}";
        }

        public string Online { get { return IsOnline ? "Sim" : "Não"; } }                
    }
}
