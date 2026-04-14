using System;
using System.Windows.Forms;
using Quality.Forms.Shared;
using Quality.Infrastructure;
using Quality.Services;
namespace Quality.Forms.QualityManager
{
    public partial class QualityManagerDashboardForm : BaseDashboardForm
    {
        private readonly DashboardService _service = new DashboardService();
        private readonly CrudService _crud = new CrudService();
        private readonly AuditService _audit = new AuditService();
        public QualityManagerDashboardForm() { InitializeComponent(); ApplyBaseStyle(); lblUser.Text = $"Менеджер по качеству: {AppSession.CurrentUser?.FullName}"; }
        private void QualityManagerDashboardForm_Load(object sender, EventArgs e) { RefreshData(); }
        private void RefreshData() { gridIndicators.DataSource = _service.GetSurveyAnalytics(); gridSurveys.DataSource = _service.GetSurveys(); gridActions.DataSource = _service.GetCorrectiveActions(); }
        private void btnNewAction_Click(object sender, EventArgs e) { using (var f = new CorrectiveActionEditForm()) if (f.ShowDialog() == DialogResult.OK) RefreshData(); }
        private void btnEditAction_Click(object sender, EventArgs e) { var id = GetSelectedId(); if (!id.HasValue) return; using (var f = new CorrectiveActionEditForm(id)) if (f.ShowDialog() == DialogResult.OK) RefreshData(); }
        private void btnDeleteAction_Click(object sender, EventArgs e)
        {
            var id = GetSelectedId(); if (!id.HasValue) return;
            if (MessageBox.Show("Удалить корректирующее действие?", "Quality", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            try { _crud.DeleteById("corrective_actions", id.Value); _audit.Log("DELETE", "corrective_actions", id.Value, null); RefreshData(); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Quality"); }
        }
        private int? GetSelectedId() => gridActions.CurrentRow == null ? (int?)null : Convert.ToInt32(gridActions.CurrentRow.Cells[0].Value);
    }
}
