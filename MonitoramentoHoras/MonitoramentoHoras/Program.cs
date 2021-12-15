using System;
using System.Windows.Forms;

namespace MonitoramentoHoras
{
    public static class Program
    {
        public static string Token { get; set; }
        public static long TempoLimiteOciosidade { get; set; }

        public static long PessoaId { get; set; }

        [STAThread]
        public static void Main()
        {
            Application.Run(new Login());
        }

        public static void SetToken(string token)
        {
            Token = token;
        }
        
        public static void SetTempoLimiteOciosidade(long tempoLimiteOciosidade)
        {
            TempoLimiteOciosidade = tempoLimiteOciosidade;
        }

        public static void SetPessoaId(long pessoaId)
        {
            PessoaId = pessoaId;
        }
    }
}
