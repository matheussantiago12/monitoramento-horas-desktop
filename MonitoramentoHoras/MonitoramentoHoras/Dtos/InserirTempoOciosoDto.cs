namespace MonitoramentoHoras.Dtos
{
    public class InserirTempoOciosoDto
    {
        public InserirTempoOciosoDto(string tempoInicialOciosidade, string tempoFinalOciosidade, long pessoaId)
        {
            TempoFinalOciosidade = tempoFinalOciosidade;
            TempoInicialOciosidade = tempoInicialOciosidade;
            PessoaId = pessoaId;
        }

        public string TempoInicialOciosidade { get; set; }
        public string TempoFinalOciosidade { get; set; }
        public long PessoaId { get; set; }
    }
}
