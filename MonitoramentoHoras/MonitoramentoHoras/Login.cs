using MonitoramentoHoras.Dtos;
using Newtonsoft.Json;
using System;
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
            CenterToScreen();
            InitializeComponent();
            txtUsuario.GotFocus += new EventHandler(RemoveTextTxtUsuario);
            txtUsuario.LostFocus += new EventHandler(AddTextTxtUsuario);

            txtSenha.GotFocus += new EventHandler(RemoveTextTxtSenha);
            txtSenha.LostFocus += new EventHandler(AddTexttxtSenha);
            MaximizeBox = false;
            MinimizeBox = false;
        }

        public void RemoveTextTxtUsuario(object sender, EventArgs e)
        {
            if (txtUsuario.Text == "Digite o usuário")
            {
                txtUsuario.Text = "";
            }
        }

        public void AddTextTxtUsuario(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsuario.Text))
                txtUsuario.Text = "Digite o usuário";
        }

        public void RemoveTextTxtSenha(object sender, EventArgs e)
        {
            if (txtSenha.Text == "******")
            {
                txtSenha.Text = "";
            }
        }

        public void AddTexttxtSenha(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSenha.Text))
                txtSenha.Text = "******";
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
                PopularInformacoesDoUsuario(tokenDto.Token);
                Hide();
                mensagemErro.Visible = false;

                //if (Program.UsuarioLogadoDto.mudarSenha)
                //{
                    new Senha().Show();
                     MessageBox.Show("Foi detectado que você está com uma senha auto gerada! Favor trocar sua senha.");
                //}
                //else
                //{
                //    MessageBox.Show("Logado com sucesso!");
                //}

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

                timer = new System.Timers.Timer(10000);
                timer.Elapsed += new ElapsedEventHandler(EnviarIdleTime);
                timer.Enabled = true;
            }
            else
            {
                mensagemErro.Visible = true;
            }
        }

        void PopularInformacoesDoUsuario(string token)
        {
            Program.SetToken(token);
            Program.SetTokenClient(client);
            SetMinutosOciososConfigurado();
            SetPessoaIdEUsuarioId();
        }

        void SetMinutosOciososConfigurado()
        {
            var response = client.GetAsync("https://backend-monitoramento-horas.herokuapp.com/api/configuracao/1").Result;

            string result = response.Content.ReadAsStringAsync().Result;
            ConfiguracaoDto configDto = JsonConvert.DeserializeObject<ConfiguracaoDto>(result);
            Program.SetTempoLimiteOciosidade(configDto.TempoLimiteOciosidade);
        }

        void SetPessoaIdEUsuarioId()
        {
            var response = client.GetAsync("https://backend-monitoramento-horas.herokuapp.com/api/usuario/logado").Result;

            string result = response.Content.ReadAsStringAsync().Result;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                UsuarioLogadoDto usuario = JsonConvert.DeserializeObject<UsuarioLogadoDto>(result);
                Program.SetUsuarioLogadoDto(usuario);
            }
            else
            {
                throw new Exception("Não foi possível buscar o usuário logado.");
            }
        }

        void EnviarIdleTime(object sender, EventArgs e)
        {
            if (pontoAtivo)
            {
                var idleTime = Win32.GetIdleTime();
                if (idleTime >= Program.TempoLimiteOciosidade * 60000)
                {
                    if (!inicioAusencia.HasValue)
                    {
                        inicioAusencia = DateTime.Now.AddMinutes(Program.TempoLimiteOciosidade);
                    }
                }
                else
                {
                    if (inicioAusencia.HasValue)
                    {
                        var inicioAusenciaString = inicioAusencia?.ToString("s");
                        var fimAusencia = DateTime.Now.ToString("s");

                        InserirTempoOciosoDto inserirTempoOciosoDto = new InserirTempoOciosoDto(inicioAusenciaString, fimAusencia, Program.UsuarioLogadoDto.pessoaId);

                        var json = JsonConvert.SerializeObject(inserirTempoOciosoDto);
                        var data = new StringContent(json, Encoding.UTF8, "application/json");

                        var response = client.PostAsync("https://backend-monitoramento-horas.herokuapp.com/api/rastreamento", data).Result;

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