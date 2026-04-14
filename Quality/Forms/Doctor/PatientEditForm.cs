using System;
using System.Windows.Forms;
using Quality.Services;

namespace Quality.Forms.Doctor
{
    public partial class PatientEditForm : Form
    {
        private readonly CrudService _crud = new CrudService();
        private readonly AuditService _audit = new AuditService();
        private readonly int? _patientId;

        public PatientEditForm(int? patientId = null)
        {
            _patientId = patientId;
            InitializeComponent();
            Load += PatientEditForm_Load;
        }

        private void PatientEditForm_Load(object sender, EventArgs e)
        {
            if (!_patientId.HasValue) return;
            var table = Infrastructure.MySqlDb.GetTable($"SELECT * FROM patients WHERE id = {_patientId.Value};");
            if (table.Rows.Count == 0) return;
            var row = table.Rows[0];
            txtCardNumber.Text = row["card_number"].ToString();
            txtFullName.Text = row["full_name"].ToString();
            dtBirthDate.Value = Convert.ToDateTime(row["birth_date"]);
            txtPhone.Text = row["phone"].ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCardNumber.Text) || string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                MessageBox.Show("Заполните код пациента и ФИО.", "Quality");
                return;
            }
            var id = _crud.SavePatient(_patientId, txtCardNumber.Text.Trim(), txtFullName.Text.Trim(), dtBirthDate.Value, txtPhone.Text.Trim());
            _audit.Log(_patientId.HasValue ? "UPDATE" : "INSERT", "patients", id, txtFullName.Text.Trim());
            DialogResult = DialogResult.OK;
        }
    }
}
