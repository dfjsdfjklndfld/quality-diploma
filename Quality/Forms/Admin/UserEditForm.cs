using System;
using System.Data;
using System.Windows.Forms;
using Quality.Services;

namespace Quality.Forms.Admin
{
    public partial class UserEditForm : Form
    {
        private readonly CrudService _crud = new CrudService();
        private readonly AuditService _audit = new AuditService();
        private readonly LookupService _lookup = new LookupService();
        private readonly int? _userId;

        public UserEditForm(int? userId = null)
        {
            _userId = userId;
            InitializeComponent();
            Load += UserEditForm_Load;
        }

        private void UserEditForm_Load(object sender, EventArgs e)
        {
            cmbRole.Items.Clear();
            cmbRole.Items.AddRange(new object[] { "Администратор", "Врач", "Менеджер по качеству" });
            cmbRole.SelectedIndex = 0;
            BindDepartments();
            if (_userId.HasValue) LoadUser();
        }

        private void BindDepartments()
        {
            var departments = _lookup.GetDepartments();
            var row = departments.NewRow();
            row["id"] = DBNull.Value;
            row["name"] = "— Без отделения —";
            departments.Rows.InsertAt(row, 0);
            cmbDepartment.DataSource = departments;
            cmbDepartment.ValueMember = "id";
            cmbDepartment.DisplayMember = "name";
        }

        private void LoadUser()
        {
            var table = Infrastructure.MySqlDb.GetTable($"SELECT * FROM users WHERE id = {_userId.Value};");
            if (table.Rows.Count == 0) return;
            var row = table.Rows[0];
            txtLogin.Text = row["login"].ToString();
            txtFullName.Text = row["full_name"].ToString();
            cmbRole.SelectedItem = row["role_name"].ToString();
            chkIsActive.Checked = Convert.ToInt32(row["is_active"]) == 1;
            if (row["department_id"] != DBNull.Value) cmbDepartment.SelectedValue = Convert.ToInt32(row["department_id"]);
            txtPassword.Text = string.Empty;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLogin.Text) || string.IsNullOrWhiteSpace(txtFullName.Text) || (!_userId.HasValue && string.IsNullOrWhiteSpace(txtPassword.Text)))
            {
                MessageBox.Show("Заполните обязательные поля.", "Quality");
                return;
            }

            var departmentId = cmbDepartment.SelectedValue == DBNull.Value ? (int?)null : Convert.ToInt32(cmbDepartment.SelectedValue);
            var id = _crud.SaveUser(_userId, txtLogin.Text.Trim(), txtPassword.Text.Trim(), txtFullName.Text.Trim(), cmbRole.Text, departmentId, chkIsActive.Checked);
            _audit.Log(_userId.HasValue ? "UPDATE" : "INSERT", "users", id, txtLogin.Text.Trim());
            DialogResult = DialogResult.OK;
        }
    }
}
