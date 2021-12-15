using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;

namespace MonitoramentoHoras
{
    public partial class Senha : MaterialSkin.Controls.MaterialForm
    {
        private static readonly HttpClient client = new HttpClient();

        public Senha()
        {
            CenterToScreen();
            Program.SetTokenClient(client);
            InitializeComponent();
            txtNovaSenha.GotFocus += new EventHandler(RemoveTextTxtSenha);
            txtNovaSenha.LostFocus += new EventHandler(AddTextTxtSenha);
            MaximizeBox = false;
            MinimizeBox = false;
        }

        public void RemoveTextTxtSenha(object sender, EventArgs e)
        {
            if (txtNovaSenha.Text == "******")
            {
                txtNovaSenha.Text = "";
            }
        }

        public void AddTextTxtSenha(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNovaSenha.Text))
                txtNovaSenha.Text = "******";
        }

        private void submitSenha_Click(object sender, EventArgs e)
        {
            var json = JsonConvert.SerializeObject(txtNovaSenha.Text);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = client.PutAsync($"https://backend-monitoramento-horas.herokuapp.com/api/usuario/trocar-senha/{Program.UsuarioLogadoDto.id}", data).Result;
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                MessageBox.Show("Alterado com sucesso!");
                Hide();
            }
            else {
                MessageBox.Show("Erro ao alterar senha. Tente novamente mais tarde!");
            }
        }
    }
}
