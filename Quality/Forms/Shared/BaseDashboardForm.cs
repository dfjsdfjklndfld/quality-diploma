using System.Drawing;
using System.Windows.Forms;
using Quality.Infrastructure;
namespace Quality.Forms.Shared
{
    public class BaseDashboardForm : Form
    {
        protected void ApplyBaseStyle()
        {
            BackColor = Color.WhiteSmoke;
            Font = new Font("Segoe UI", 10F);
            StartPosition = FormStartPosition.CenterScreen;
        }
        protected Button CreateExitButton()
        {
            var b = new Button { Text = "Выход", Width = 120, Height = 36, BackColor = Color.Gainsboro, FlatStyle = FlatStyle.Flat };
            b.Click += (s, e) => { AppSession.CurrentUser = null; Hide(); new LoginForm().Show(); };
            return b;
        }
    }
}