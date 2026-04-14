using System;
using System.Data;
using System.Windows.Forms;
using Quality.Infrastructure;
using Quality.Services;

namespace Quality.Forms.Doctor
{
    public partial class SurveyForm : Form
    {
        private readonly CrudService _crud = new CrudService();
        private readonly AuditService _audit = new AuditService();
        private readonly LookupService _lookup = new LookupService();
        private int? _surveyId;

        public SurveyForm(int? surveyId = null)
        {
            _surveyId = surveyId;
            InitializeComponent();
            Load += SurveyForm_Load;
        }

        private void SurveyForm_Load(object sender, EventArgs e)
        {
            cmbPatient.DataSource = _lookup.GetCompletedAppointmentsWithoutSurvey(AppSession.CurrentUser.Id);
            cmbPatient.ValueMember = "id";
            cmbPatient.DisplayMember = "display_name";
            if (_surveyId.HasValue) LoadSurvey();
        }

        private void LoadSurvey()
        {
            var table = MySqlDb.GetTable($"SELECT * FROM patient_surveys WHERE id = {_surveyId.Value};");
            if (table.Rows.Count == 0) return;
            var row = table.Rows[0];
            cmbPatient.DataSource = MySqlDb.GetTable($"SELECT a.id, CONCAT(DATE_FORMAT(a.appointment_date, '%d.%m.%Y %H:%i'), ' | ', p.full_name) AS display_name FROM appointments a JOIN patients p ON p.id = a.patient_id WHERE a.id = {Convert.ToInt32(row["appointment_id"])};");
            cmbPatient.ValueMember = "id";
            cmbPatient.DisplayMember = "display_name";
            cmbPatient.SelectedValue = Convert.ToInt32(row["appointment_id"]);
            dtSurveyDate.Value = Convert.ToDateTime(row["survey_date"]);
            numService.Value = Convert.ToDecimal(row["service_quality_score"]);
            numAttention.Value = Convert.ToDecimal(row["doctor_attention_score"]);
            numWaiting.Value = Convert.ToDecimal(row["waiting_time_score"]);
            numClean.Value = Convert.ToDecimal(row["cleanliness_score"]);
            txtComment.Text = row["comment"].ToString();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (cmbPatient.SelectedValue == null)
            {
                MessageBox.Show("Нет доступных завершённых приёмов без оценки.", "Quality");
                return;
            }
            var id = _crud.SaveSurvey(_surveyId, Convert.ToInt32(cmbPatient.SelectedValue), dtSurveyDate.Value, (int)numService.Value, (int)numAttention.Value, (int)numWaiting.Value, (int)numClean.Value, txtComment.Text.Trim());
            _audit.Log(_surveyId.HasValue ? "UPDATE" : "INSERT", "patient_surveys", id, "Оценка качества");
            DialogResult = DialogResult.OK;
        }
    }
}
