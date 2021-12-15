using MonitoramentoHoras.Dtos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Timers;
using System.Windows.Forms;
using Win32_API;

namespace MonitoramentoHoras
{
    public partial class Login : MaterialSkin.Controls.MaterialForm
    {
        public Login()
        {
            InitializeComponent();
        }

        NotifyIcon notifyIcon = new NotifyIcon();
        bool pontoAtivo = false;
        private static System.Timers.Timer timer;
        private DateTime? inicioAusencia = null;
        private static readonly HttpClient client = new HttpClient();

        private async void buttonLogin_Click(object sender, EventArgs e)
        {
            var url = "https://backend-monitoramento-horas.herokuapp.com/api/usuario/validar-credenciais";
            var validarCredenciasDto = new ValidarCredenciasDto(txtUsuario.Text, txtSenha.Text);

            var json = JsonConvert.SerializeObject(validarCredenciasDto);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(url, data);
            string result = response.Content.ReadAsStringAsync().Result;

            if (result.Contains("token"))
            {
                TokenDto tokenDto = JsonConvert.DeserializeObject<TokenDto>(result);
                Program.SetToken(tokenDto.Token);
                mensagemErro.Visible = false;
                Hide();
                MessageBox.Show("Logado com sucesso!");
                ToolStripMenuItem iniciarMenuItem = new ToolStripMenuItem("Iniciar ponto", null, new EventHandler(IniciarPonto));
                ToolStripMenuItem sairMenuItem = new ToolStripMenuItem("Sair", null, new EventHandler(Sair));

                using (MemoryStream ms = new MemoryStream(Properties.Resources.IconeTray))
                {
                    notifyIcon.Icon = new Icon(ms);
                }
                notifyIcon.ContextMenuStrip = new ContextMenuStrip();
                notifyIcon.ContextMenuStrip.Items.Insert(0, iniciarMenuItem);
                notifyIcon.ContextMenuStrip.Items.Insert(1, sairMenuItem);
                notifyIcon.Visible = true;

                timer = new System.Timers.Timer(1000);
                timer.Elapsed += new ElapsedEventHandler(EnviarIdleTime);
                timer.Enabled = true;
            }
            else
            {
                mensagemErro.Visible = true;
            }

        }

        void EnviarIdleTime(object sender, EventArgs e)
        {
            if (pontoAtivo)
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
                            { "pessoaId", "1" }
                        };

                        var content = new FormUrlEncodedContent(values);

                        var response = client.PostAsync("https://backend-monitoramento-horas.herokuapp.com/api/rastreamento", content).Result;

                        var responseString = response.Content.ReadAsStringAsync();

                        inicioAusencia = null;
                    }
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
                notifyIcon.ContextMenuStrip.Items.RemoveAt(0);
                MessageBox.Show("Ponto Iniciado!");

                ToolStripMenuItem finalizarMenuItem = new ToolStripMenuItem("Finalizar ponto", null, new EventHandler(FinalizarPonto));

                notifyIcon.ContextMenuStrip.Items.Insert(0, finalizarMenuItem);
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
                notifyIcon.ContextMenuStrip.Items.RemoveAt(0);
                MessageBox.Show("Ponto Finalizado!");

                ToolStripMenuItem iniciarMenuItem = new ToolStripMenuItem("Iniciar ponto", null, new EventHandler(IniciarPonto));

                notifyIcon.ContextMenuStrip.Items.Insert(0, iniciarMenuItem);
            }
            else
            {
                throw new Exception("O ponto não foi iniciado.");
            }
        }
    }
}