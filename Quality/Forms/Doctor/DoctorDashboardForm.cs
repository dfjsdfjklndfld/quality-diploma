using System;
using System.Windows.Forms;
using Quality.Forms.Shared;
using Quality.Infrastructure;
using Quality.Services;
namespace Quality.Forms.Doctor
{
    public partial class DoctorDashboardForm : BaseDashboardForm
    {
        private readonly DashboardService _service = new DashboardService();
        private readonly CrudService _crud = new CrudService();
        private readonly AuditService _audit = new AuditService();
        public DoctorDashboardForm() { InitializeComponent(); ApplyBaseStyle(); lblUser.Text = $"Врач: {AppSession.CurrentUser?.FullName}"; }
        private void DoctorDashboardForm_Load(object sender, EventArgs e) { RefreshData(); }
        private void RefreshData() { gridPatients.DataSource = _service.GetPatients(); gridAppointments.DataSource = _service.GetAppointmentsByDoctor(AppSession.CurrentUser.Id); gridHistory.DataSource = _service.GetAppointmentsByDoctor(AppSession.CurrentUser.Id); }
        private void btnNewPatient_Click(object sender, EventArgs e) { using (var f = new PatientEditForm()) if (f.ShowDialog() == DialogResult.OK) RefreshData(); }
        private void btnEditPatient_Click(object sender, EventArgs e) { var id = GetSelectedId(gridPatients); if (!id.HasValue) return; using (var f = new PatientEditForm(id)) if (f.ShowDialog() == DialogResult.OK) RefreshData(); }
        private void btnDeletePatient_Click(object sender, EventArgs e) { DeleteSelected(gridPatients, "patients", "patients"); }
        private void btnNewAppointment_Click(object sender, EventArgs e) { using (var f = new AppointmentEditForm()) if (f.ShowDialog() == DialogResult.OK) RefreshData(); }
        private void btnEditAppointment_Click(object sender, EventArgs e) { var id = GetSelectedId(gridAppointments); if (!id.HasValue) return; using (var f = new AppointmentEditForm(id)) if (f.ShowDialog() == DialogResult.OK) RefreshData(); }
        private void btnDeleteAppointment_Click(object sender, EventArgs e) { DeleteSelected(gridAppointments, "appointments", "appointments"); }
        private void btnSurvey_Click(object sender, EventArgs e) { using (var f = new SurveyForm()) if (f.ShowDialog() == DialogResult.OK) RefreshData(); }
        private int? GetSelectedId(DataGridView grid) => grid.CurrentRow == null ? (int?)null : Convert.ToInt32(grid.CurrentRow.Cells[0].Value);
        private void DeleteSelected(DataGridView grid, string tableName, string entityName)
        {
            var id = GetSelectedId(grid); if (!id.HasValue) return;
            if (MessageBox.Show("Удалить выбранную запись?", "Quality", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            try { _crud.DeleteById(tableName, id.Value); _audit.Log("DELETE", entityName, id.Value, null); RefreshData(); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Quality"); }
        }
    }
}
