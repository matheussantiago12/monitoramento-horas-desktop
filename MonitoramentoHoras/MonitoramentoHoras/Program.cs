using System;
using System.Windows.Forms;

namespace MonitoramentoHoras
{
    public static class Program
    {
        public static string Token { get; set; }

        [STAThread]
        public static void Main()
        {
            Application.Run(new Login());
        }

        public static void SetToken(string token)
        {
            Token = token;
        }
    }
}
