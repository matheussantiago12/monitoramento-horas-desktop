namespace MonitoramentoHoras.Dtos
{
    public class PessoaDto
    {
        public string nomeCompleto { get; set; }
        public string cargo { get; set; }
        public int horasTrabalhoDiario { get; set; }
        public int equipeId { get; set; }
        public EquipeDto equipe { get; set; }
        public int tipoPessoaId { get; set; }
        public TipoPessoaDto tipoPessoa { get; set; }
        public int id { get; set; }
    }
}
