namespace Quality.Forms.Shared
{
    partial class LoginForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblLogin;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtLogin;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Label lblError;
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }
        private void InitializeComponent()
        {
            this.panelMain = new System.Windows.Forms.Panel(); this.lblError = new System.Windows.Forms.Label(); this.btnLogin = new System.Windows.Forms.Button(); this.txtPassword = new System.Windows.Forms.TextBox(); this.txtLogin = new System.Windows.Forms.TextBox(); this.lblPassword = new System.Windows.Forms.Label(); this.lblLogin = new System.Windows.Forms.Label(); this.lblTitle = new System.Windows.Forms.Label(); this.panelMain.SuspendLayout(); this.SuspendLayout();
            this.panelMain.BackColor = System.Drawing.Color.White; this.panelMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle; this.panelMain.Controls.Add(this.lblError); this.panelMain.Controls.Add(this.btnLogin); this.panelMain.Controls.Add(this.txtPassword); this.panelMain.Controls.Add(this.txtLogin); this.panelMain.Controls.Add(this.lblPassword); this.panelMain.Controls.Add(this.lblLogin); this.panelMain.Controls.Add(this.lblTitle); this.panelMain.Location = new System.Drawing.Point(95, 45); this.panelMain.Size = new System.Drawing.Size(390, 270);
            this.lblError.AutoSize = true; this.lblError.ForeColor = System.Drawing.Color.Firebrick; this.lblError.Location = new System.Drawing.Point(42, 191); this.lblError.Text = "Неверный пароль"; this.lblError.Visible = false;
            this.btnLogin.BackColor = System.Drawing.Color.Gainsboro; this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnLogin.Location = new System.Drawing.Point(42, 220); this.btnLogin.Size = new System.Drawing.Size(300, 35); this.btnLogin.Text = "Войти"; this.btnLogin.UseVisualStyleBackColor = false; this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            this.txtPassword.Location = new System.Drawing.Point(42, 148); this.txtPassword.PasswordChar = '*'; this.txtPassword.Size = new System.Drawing.Size(300, 27);
            this.txtLogin.Location = new System.Drawing.Point(42, 91); this.txtLogin.Size = new System.Drawing.Size(300, 27);
            this.lblPassword.AutoSize = true; this.lblPassword.Location = new System.Drawing.Point(42, 125); this.lblPassword.Text = "Пароль";
            this.lblLogin.AutoSize = true; this.lblLogin.Location = new System.Drawing.Point(42, 68); this.lblLogin.Text = "Логин";
            this.lblTitle.AutoSize = true; this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 16F, System.Drawing.FontStyle.Bold); this.lblTitle.Location = new System.Drawing.Point(36, 18); this.lblTitle.Text = "Вход в Quality";
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F); this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font; this.BackColor = System.Drawing.Color.WhiteSmoke; this.ClientSize = new System.Drawing.Size(584, 361); this.Controls.Add(this.panelMain); this.Font = new System.Drawing.Font("Segoe UI", 10F); this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog; this.MaximizeBox = false; this.Name = "LoginForm"; this.Text = "Quality - Авторизация";
            this.panelMain.ResumeLayout(false); this.panelMain.PerformLayout(); this.ResumeLayout(false);
        }
    }
}