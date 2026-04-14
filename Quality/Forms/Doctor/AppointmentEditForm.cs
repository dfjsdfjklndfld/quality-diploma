using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Quality.Infrastructure;
using Quality.Services;

namespace Quality.Forms.Doctor
{
    public partial class AppointmentEditForm : Form
    {
        private readonly CrudService _crud = new CrudService();
        private readonly AuditService _audit = new AuditService();
        private readonly LookupService _lookup = new LookupService();
        private readonly int? _appointmentId;
        private DataTable _services;

        public AppointmentEditForm(int? appointmentId = null)
        {
            _appointmentId = appointmentId;
            InitializeComponent();
            Load += AppointmentEditForm_Load;
        }

        private void AppointmentEditForm_Load(object sender, EventArgs e)
        {
            cmbDepartment.DataSource = _lookup.GetDepartments();
            cmbDepartment.ValueMember = "id";
            cmbDepartment.DisplayMember = "name";

            cmbPatient.DataSource = _lookup.GetPatients();
            cmbPatient.ValueMember = "id";
            cmbPatient.DisplayMember = "display_name";

            _services = _lookup.GetServices();
            btnAddService.Click += btnAddService_Click;

            if (_appointmentId.HasValue) LoadAppointment();
        }

        private void btnAddService_Click(object sender, EventArgs e)
        {
            using (var picker = new ServicePickerForm(_services))
            {
                if (picker.ShowDialog() == DialogResult.OK && picker.SelectedServiceId.HasValue)
                {
                    foreach (DataRow row in _services.Rows)
                    {
                        if (Convert.ToInt32(row["id"]) != picker.SelectedServiceId.Value) continue;
                        lstServices.Items.Add(new ServiceItem(Convert.ToInt32(row["id"]), row["display_name"].ToString()));
                        break;
                    }
                }
            }
        }

        private void LoadAppointment()
        {
            var table = MySqlDb.GetTable($"SELECT * FROM appointments WHERE id = {_appointmentId.Value};");
            if (table.Rows.Count == 0) return;
            var row = table.Rows[0];
            dtAppointment.Value = Convert.ToDateTime(row["appointment_date"]);
            cmbDepartment.SelectedValue = Convert.ToInt32(row["department_id"]);
            cmbPatient.SelectedValue = Convert.ToInt32(row["patient_id"]);

            var serviceTable = MySqlDb.GetTable($@"SELECT ms.id, CONCAT(ms.name, ' (', FORMAT(aps.price,2), ' руб.)') AS display_name
                                                 FROM appointment_services aps
                                                 JOIN medical_services ms ON ms.id = aps.service_id
                                                 WHERE aps.appointment_id = {_appointmentId.Value};");
            lstServices.Items.Clear();
            foreach (DataRow serviceRow in serviceTable.Rows)
                lstServices.Items.Add(new ServiceItem(Convert.ToInt32(serviceRow["id"]), serviceRow["display_name"].ToString()));
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cmbPatient.SelectedValue == null || cmbDepartment.SelectedValue == null)
            {
                MessageBox.Show("Выберите пациента и отделение.", "Quality");
                return;
            }
            var serviceIds = lstServices.Items.Cast<ServiceItem>().Select(x => x.Id).ToList();
            if (serviceIds.Count == 0)
            {
                MessageBox.Show("Добавьте хотя бы одну услугу.", "Quality");
                return;
            }

            var id = _crud.SaveAppointment(_appointmentId, Convert.ToInt32(cmbPatient.SelectedValue), AppSession.CurrentUser.Id, Convert.ToInt32(cmbDepartment.SelectedValue), dtAppointment.Value, dtAppointment.Value <= DateTime.Now ? "completed" : "scheduled", null, serviceIds);
            _audit.Log(_appointmentId.HasValue ? "UPDATE" : "INSERT", "appointments", id, "Приём врача");
            DialogResult = DialogResult.OK;
        }

        private class ServiceItem
        {
            public int Id { get; }
            public string Name { get; }
            public ServiceItem(int id, string name) { Id = id; Name = name; }
            public override string ToString() => Name;
        }

        private class ServicePickerForm : Form
        {
            private readonly ComboBox _cmb = new ComboBox();
            private readonly Button _btn = new Button();
            public int? SelectedServiceId => _cmb.SelectedValue == null ? (int?)null : Convert.ToInt32(_cmb.SelectedValue);
            public ServicePickerForm(DataTable services)
            {
                Text = "Выбор услуги"; Width = 380; Height = 140; FormBorderStyle = FormBorderStyle.FixedDialog; StartPosition = FormStartPosition.CenterParent;
                _cmb.Left = 15; _cmb.Top = 15; _cmb.Width = 330; _cmb.DropDownStyle = ComboBoxStyle.DropDownList; _cmb.DataSource = services.Copy(); _cmb.ValueMember = "id"; _cmb.DisplayMember = "display_name";
                _btn.Left = 15; _btn.Top = 50; _btn.Width = 330; _btn.Height = 32; _btn.Text = "Добавить"; _btn.Click += (s, e) => DialogResult = DialogResult.OK;
                Controls.Add(_cmb); Controls.Add(_btn);
            }
        }
    }
}
