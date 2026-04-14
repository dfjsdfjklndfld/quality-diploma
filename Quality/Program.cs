using System;
using System.Windows.Forms;
using Quality.Forms.Shared;
using Quality.Infrastructure;

namespace Quality
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try { DatabaseInitializer.EnsureDatabase(); }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось инициализировать базу данных MySQL.\r\n\r\n" + ex.Message, "Quality", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Application.Run(new LoginForm());
        }
    }
}