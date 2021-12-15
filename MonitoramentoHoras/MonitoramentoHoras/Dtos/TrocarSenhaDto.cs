namespace MonitoramentoHoras.Dtos
{
    public class TrocarSenhaDto
    {
        public TrocarSenhaDto(string senha)
        {
            Senha = senha;
        }
        public string Senha { get; set; }
    }
}
