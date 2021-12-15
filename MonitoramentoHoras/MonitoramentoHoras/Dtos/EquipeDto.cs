namespace MonitoramentoHoras.Dtos
{
    public class EquipeDto
    {
        public int pessoaLiderId { get; set; }
        public UsuarioLogadoDto pessoaLider { get; set; }
        public string nome { get; set; }
        public int setorId { get; set; }
        public SetorDto setor { get; set; }
        public int id { get; set; }
    }
}
