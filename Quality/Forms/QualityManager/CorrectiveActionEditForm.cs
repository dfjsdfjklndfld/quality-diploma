using System;
using System.Data;
using System.Windows.Forms;
using Quality.Infrastructure;
using Quality.Services;

namespace Quality.Forms.QualityManager
{
    public partial class CorrectiveActionEditForm : Form
    {
        private readonly CrudService _crud = new CrudService();
        private readonly AuditService _audit = new AuditService();
        private readonly int? _actionId;
        private readonly ComboBox cmbDepartment = new ComboBox();
        private readonly ComboBox cmbResponsible = new ComboBox();

        public CorrectiveActionEditForm(int? actionId = null)
        {
            _actionId = actionId;
            InitializeComponent();
            BuildExtraControls();
            Load += CorrectiveActionEditForm_Load;
        }

        private void BuildExtraControls()
        {
            cmbDepartment.DropDownStyle = ComboBoxStyle.DropDownList; cmbDepartment.Left = 24; cmbDepartment.Top = 255; cmbDepartment.Width = 340;
            cmbResponsible.DropDownStyle = ComboBoxStyle.DropDownList; cmbResponsible.Left = 24; cmbResponsible.Top = 291; cmbResponsible.Width = 340;
            btnSave.Top = 329; ClientSize = new System.Drawing.Size(392, 380);
            Controls.Add(cmbDepartment); Controls.Add(cmbResponsible);
        }

        private void CorrectiveActionEditForm_Load(object sender, EventArgs e)
        {
            var lookup = new LookupService();
            var deps = lookup.GetDepartments();
            var depRow = deps.NewRow(); depRow["id"] = DBNull.Value; depRow["name"] = "— Без отделения —"; deps.Rows.InsertAt(depRow, 0);
            cmbDepartment.DataSource = deps; cmbDepartment.ValueMember = "id"; cmbDepartment.DisplayMember = "name";

            var docs = lookup.GetDoctors();
            var docRow = docs.NewRow(); docRow["id"] = DBNull.Value; docRow["full_name"] = "— Не назначен —"; docs.Rows.InsertAt(docRow, 0);
            cmbResponsible.DataSource = docs; cmbResponsible.ValueMember = "id"; cmbResponsible.DisplayMember = "full_name";
            cmbStatus.SelectedIndex = 0;

            if (_actionId.HasValue) LoadAction();
        }

        private void LoadAction()
        {
            var table = MySqlDb.GetTable($"SELECT * FROM corrective_actions WHERE id = {_actionId.Value};");
            if (table.Rows.Count == 0) return;
            var row = table.Rows[0];
            txtTitle.Text = row["title"].ToString();
            txtDescription.Text = row["description"].ToString();
            dtDeadline.Value = Convert.ToDateTime(row["deadline"]);
            cmbStatus.SelectedItem = row["status"].ToString();
            if (row["department_id"] != DBNull.Value) cmbDepartment.SelectedValue = Convert.ToInt32(row["department_id"]);
            if (row["responsible_user_id"] != DBNull.Value) cmbResponsible.SelectedValue = Convert.ToInt32(row["responsible_user_id"]);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                MessageBox.Show("Введите название действия.", "Quality");
                return;
            }
            var departmentId = cmbDepartment.SelectedValue == DBNull.Value ? (int?)null : Convert.ToInt32(cmbDepartment.SelectedValue);
            var responsibleId = cmbResponsible.SelectedValue == DBNull.Value ? (int?)null : Convert.ToInt32(cmbResponsible.SelectedValue);
            var id = _crud.SaveCorrectiveAction(_actionId, txtTitle.Text.Trim(), txtDescription.Text.Trim(), departmentId, responsibleId, dtDeadline.Value, cmbStatus.Text);
            _audit.Log(_actionId.HasValue ? "UPDATE" : "INSERT", "corrective_actions", id, txtTitle.Text.Trim());
            DialogResult = DialogResult.OK;
        }
    }
}
