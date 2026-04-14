using System;
using System.Windows.Forms;
using Quality.Forms.Shared;
using Quality.Infrastructure;
using Quality.Services;
namespace Quality.Forms.Admin
{
    public partial class AdminDashboardForm : BaseDashboardForm
    {
        private readonly DashboardService _service = new DashboardService();
        private readonly CrudService _crud = new CrudService();
        private readonly AuditService _audit = new AuditService();
        public AdminDashboardForm() { InitializeComponent(); ApplyBaseStyle(); lblUser.Text = $"Администратор: {AppSession.CurrentUser?.FullName}"; }
        private void AdminDashboardForm_Load(object sender, EventArgs e) { RefreshData(); }
        private void RefreshData() { gridUsers.DataSource = _service.GetUsers(); gridDepartments.DataSource = _service.GetDepartments(); gridStats.DataSource = _service.GetAdminStats(); }
        private void btnAddUser_Click(object sender, EventArgs e) { using (var f = new UserEditForm()) if (f.ShowDialog() == DialogResult.OK) RefreshData(); }
        private void btnEditUser_Click(object sender, EventArgs e) { var id = GetSelectedId(gridUsers); if (!id.HasValue) return; using (var f = new UserEditForm(id)) if (f.ShowDialog() == DialogResult.OK) RefreshData(); }
        private void btnDeleteUser_Click(object sender, EventArgs e) { DeleteSelected(gridUsers, "users", "users"); }
        private void btnAddDepartment_Click(object sender, EventArgs e) { using (var f = new DepartmentEditForm()) if (f.ShowDialog() == DialogResult.OK) RefreshData(); }
        private void btnEditDepartment_Click(object sender, EventArgs e) { var id = GetSelectedId(gridDepartments); if (!id.HasValue) return; using (var f = new DepartmentEditForm(id)) if (f.ShowDialog() == DialogResult.OK) RefreshData(); }
        private void btnDeleteDepartment_Click(object sender, EventArgs e) { DeleteSelected(gridDepartments, "departments", "departments"); }
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
