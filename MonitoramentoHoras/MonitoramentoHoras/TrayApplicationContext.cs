using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Timers;
using System.Windows.Forms;
using Win32_API;

namespace MonitoramentoHoras
{
    public class TrayApplicationContext : ApplicationContext
    {
        NotifyIcon notifyIcon = new NotifyIcon();
        bool pontoAtivo = false;
        private static System.Timers.Timer timer;
        private DateTime? inicioAusencia = null;
        private static readonly HttpClient client = new HttpClient();

        public TrayApplicationContext()
        {
            ToolStripMenuItem iniciarMenuItem = new ToolStripMenuItem("Iniciar ponto", null, new EventHandler(IniciarPonto));
            ToolStripMenuItem sairMenuItem = new ToolStripMenuItem("Sair", null, new EventHandler(Sair));

            using (MemoryStream ms = new MemoryStream(Properties.Resources.IconeTray))
            {
                notifyIcon.Icon = new Icon(ms);
            }
            notifyIcon.ContextMenuStrip = new ContextMenuStrip();
            notifyIcon.ContextMenuStrip.Items.Insert(0, sairMenuItem);
            notifyIcon.ContextMenuStrip.Items.Insert(1, iniciarMenuItem);
            notifyIcon.Visible = true;

            timer = new System.Timers.Timer(1000);
            timer.Elapsed += new ElapsedEventHandler(EnviarIdleTime);
            timer.Enabled = true;
        }

        void EnviarIdleTime(object sender, EventArgs e)
        {
            var idleTime = Win32.GetIdleTime();
            if (idleTime >= 300000)
            {
                if (!inicioAusencia.HasValue)
                {
                    inicioAusencia = DateTime.Now.AddMinutes(-5);
                }
            }
            else
            {
                if (inicioAusencia.HasValue)
                {
                    var values = new Dictionary<string, string>
                    {
                        { "tempoInicialOciosidade", inicioAusencia.ToString() },
                        { "tempoFinalOciosidade", DateTime.Now.ToString() },
                        { "pessoa", "0" }
                    };

                    var content = new FormUrlEncodedContent(values);

                    var response = client.PostAsync("https://backend-monitoramento-horas.herokuapp.com/api/rastreamento", content).Result;

                    var responseString = response.Content.ReadAsStringAsync();

                    inicioAusencia = null;
                }
            }
        }

        void Sair(object sender, EventArgs e)
        {
            notifyIcon.Visible = false;

            Application.Exit();
        }

        void IniciarPonto(object sender, EventArgs e)
        {
            if (!pontoAtivo)
            {
                pontoAtivo = true;
                notifyIcon.ContextMenuStrip.Items.RemoveAt(1);

                ToolStripMenuItem finalizarMenuItem = new ToolStripMenuItem("Finalizar ponto", null, new EventHandler(FinalizarPonto));

                notifyIcon.ContextMenuStrip.Items.Insert(1, finalizarMenuItem);
            }
            else
            {
                throw new Exception("O ponto já foi iniciado.");
            }
        }

        void FinalizarPonto(object sender, EventArgs e)
        {
            if (pontoAtivo)
            {
                pontoAtivo = false;
                notifyIcon.ContextMenuStrip.Items.RemoveAt(1);

                ToolStripMenuItem iniciarMenuItem = new ToolStripMenuItem("Iniciar ponto", null, new EventHandler(IniciarPonto));

                notifyIcon.ContextMenuStrip.Items.Insert(1, iniciarMenuItem);
            }
            else
            {
                throw new Exception("O ponto não foi iniciado.");
            }
        }
    }
}
