namespace MonitoramentoHoras.Dtos
{
    public class UsuarioLogadoDto
    {
        public string email { get; set; }
        public string senha { get; set; }
        public int pessoaId { get; set; }
        public PessoaDto pessoa { get; set; }
        public int id { get; set; }
        public bool mudarSenha { get; set; }
    }
}
