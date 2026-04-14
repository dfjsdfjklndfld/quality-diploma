using System;
using System.Windows.Forms;
using Quality.Forms.Admin;
using Quality.Forms.Doctor;
using Quality.Forms.QualityManager;
using Quality.Infrastructure;
using Quality.Services;
namespace Quality.Forms.Shared
{
    public partial class LoginForm : Form
    {
        private readonly AuthService _authService = new AuthService();
        public LoginForm() { InitializeComponent(); }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            var user = _authService.Login(txtLogin.Text.Trim(), txtPassword.Text.Trim());
            if (user == null) { lblError.Text = "Неверный пароль или логин"; lblError.Visible = true; return; }
            AppSession.CurrentUser = user;
            Hide();
            Form target;
            if (user.RoleName == "Администратор")
                target = new AdminDashboardForm();
            else if (user.RoleName == "Врач")
                target = new DoctorDashboardForm();
            else
                target = new QualityManagerDashboardForm();
            target.FormClosed += (s, args) => Close();
            target.Show();
        }
    }
}