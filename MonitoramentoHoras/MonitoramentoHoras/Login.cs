using MonitoramentoHoras.Dtos;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;

namespace MonitoramentoHoras
{
    public partial class Login : MaterialSkin.Controls.MaterialForm
    {
        public Login()
        {
            InitializeComponent();
        }

        private async void buttonLogin_Click(object sender, EventArgs e)
        {
            var url = "https://backend-monitoramento-horas.herokuapp.com/api/usuario/validar-credenciais";
            var validarCredenciasDto = new ValidarCredenciasDto(txtUsuario.Text, txtSenha.Text);

            var json = JsonConvert.SerializeObject(validarCredenciasDto);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using var client = new HttpClient();

            var response = await client.PostAsync(url, data);
            string result = response.Content.ReadAsStringAsync().Result;

            if (result.Contains("token"))
            {
                TokenDto tokenDto = JsonConvert.DeserializeObject<TokenDto>(result);
                Program.SetToken(tokenDto.Token);
                mensagemErro.Visible = false;
                Hide();
            }
            else
            {
                mensagemErro.Visible = true;
            }

        }
    }
}