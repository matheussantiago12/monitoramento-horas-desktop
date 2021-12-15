
namespace MonitoramentoHoras
{
    partial class Senha
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtNovaSenha = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.submitSenha = new MaterialSkin.Controls.MaterialRaisedButton();
            this.materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
            this.SuspendLayout();
            // 
            // txtNovaSenha
            // 
            this.txtNovaSenha.Depth = 0;
            this.txtNovaSenha.Hint = "";
            this.txtNovaSenha.Location = new System.Drawing.Point(125, 116);
            this.txtNovaSenha.MouseState = MaterialSkin.MouseState.HOVER;
            this.txtNovaSenha.Name = "txtNovaSenha";
            this.txtNovaSenha.PasswordChar = '*';
            this.txtNovaSenha.SelectedText = "";
            this.txtNovaSenha.SelectionLength = 0;
            this.txtNovaSenha.SelectionStart = 0;
            this.txtNovaSenha.Size = new System.Drawing.Size(150, 23);
            this.txtNovaSenha.TabIndex = 0;
            this.txtNovaSenha.Text = "******";
            this.txtNovaSenha.UseSystemPasswordChar = false;
            // 
            // submitSenha
            // 
            this.submitSenha.Depth = 0;
            this.submitSenha.Location = new System.Drawing.Point(106, 178);
            this.submitSenha.MouseState = MaterialSkin.MouseState.HOVER;
            this.submitSenha.Name = "submitSenha";
            this.submitSenha.Primary = true;
            this.submitSenha.Size = new System.Drawing.Size(119, 52);
            this.submitSenha.TabIndex = 1;
            this.submitSenha.Text = "Mudar senha";
            this.submitSenha.UseVisualStyleBackColor = true;
            this.submitSenha.Click += new System.EventHandler(this.submitSenha_Click);
            // 
            // materialLabel1
            // 
            this.materialLabel1.AutoSize = true;
            this.materialLabel1.Depth = 0;
            this.materialLabel1.Font = new System.Drawing.Font("Roboto", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.materialLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel1.Location = new System.Drawing.Point(34, 116);
            this.materialLabel1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel1.Name = "materialLabel1";
            this.materialLabel1.Size = new System.Drawing.Size(88, 19);
            this.materialLabel1.TabIndex = 2;
            this.materialLabel1.Text = "Nova senha";
            // 
            // Senha
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(348, 291);
            this.Controls.Add(this.materialLabel1);
            this.Controls.Add(this.submitSenha);
            this.Controls.Add(this.txtNovaSenha);
            this.Name = "Senha";
            this.Text = "Alterar Senha";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MaterialSkin.Controls.MaterialSingleLineTextField txtNovaSenha;
        private MaterialSkin.Controls.MaterialRaisedButton submitSenha;
        private MaterialSkin.Controls.MaterialLabel materialLabel1;
    }
}