using MonitoramentoHoras.Dtos;
using System;
using System.Net.Http;
using System.Windows.Forms;

namespace MonitoramentoHoras
{
    public static class Program
    {
        public static string Token { get; set; }
        public static long TempoLimiteOciosidade { get; set; }
        public static UsuarioLogadoDto UsuarioLogadoDto { get; set; }

        [STAThread]
        public static void Main()
        {
            Application.Run(new Login());
        }

        public static void SetToken(string token)
        {
            Token = token;
        }

        public static void SetTokenClient(HttpClient client)
        {
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token);
        }

        public static void SetTempoLimiteOciosidade(long tempoLimiteOciosidade)
        {
            TempoLimiteOciosidade = tempoLimiteOciosidade;
        }

        public static void SetUsuarioLogadoDto(UsuarioLogadoDto usuarioLogadoDto)
        {
            UsuarioLogadoDto = usuarioLogadoDto;
        }
    }
}
