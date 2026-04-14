using System;
using System.Windows.Forms;
using Quality.Services;

namespace Quality.Forms.Admin
{
    public partial class DepartmentEditForm : Form
    {
        private readonly CrudService _crud = new CrudService();
        private readonly AuditService _audit = new AuditService();
        private readonly int? _departmentId;

        public DepartmentEditForm(int? departmentId = null)
        {
            _departmentId = departmentId;
            InitializeComponent();
            Load += DepartmentEditForm_Load;
        }

        private void DepartmentEditForm_Load(object sender, EventArgs e)
        {
            if (!_departmentId.HasValue) return;
            var table = Infrastructure.MySqlDb.GetTable($"SELECT * FROM departments WHERE id = {_departmentId.Value};");
            if (table.Rows.Count == 0) return;
            var row = table.Rows[0];
            txtName.Text = row["name"].ToString();
            txtLocation.Text = row["location"].ToString();
            txtPhone.Text = row["phone"].ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Введите название отделения.", "Quality");
                return;
            }
            var id = _crud.SaveDepartment(_departmentId, txtName.Text.Trim(), txtLocation.Text.Trim(), txtPhone.Text.Trim());
            _audit.Log(_departmentId.HasValue ? "UPDATE" : "INSERT", "departments", id, txtName.Text.Trim());
            DialogResult = DialogResult.OK;
        }
    }
}
