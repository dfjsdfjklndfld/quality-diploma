namespace Quality.Forms.Doctor
{
    partial class AppointmentEditForm
    {
        private System.ComponentModel.IContainer components = null; private System.Windows.Forms.DateTimePicker dtAppointment; private System.Windows.Forms.ComboBox cmbDepartment; private System.Windows.Forms.ComboBox cmbPatient; private System.Windows.Forms.ListBox lstServices; private System.Windows.Forms.Button btnAddService; private System.Windows.Forms.Button btnSave;
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }
        private void InitializeComponent()
        {
            this.dtAppointment = new System.Windows.Forms.DateTimePicker(); this.cmbDepartment = new System.Windows.Forms.ComboBox(); this.cmbPatient = new System.Windows.Forms.ComboBox(); this.lstServices = new System.Windows.Forms.ListBox(); this.btnAddService = new System.Windows.Forms.Button(); this.btnSave = new System.Windows.Forms.Button(); this.SuspendLayout();
            this.dtAppointment.CustomFormat = "dd.MM.yyyy HH:mm"; this.dtAppointment.Format = System.Windows.Forms.DateTimePickerFormat.Custom; this.dtAppointment.Location = new System.Drawing.Point(26, 22); this.dtAppointment.Size = new System.Drawing.Size(330, 27);
            this.cmbDepartment.Location = new System.Drawing.Point(26, 62); this.cmbDepartment.Size = new System.Drawing.Size(330, 28); this.cmbDepartment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList; this.cmbDepartment.Items.AddRange(new object[] {"Терапия", "Хирургия", "Кардиология"});
            this.cmbPatient.Location = new System.Drawing.Point(26, 102); this.cmbPatient.Size = new System.Drawing.Size(330, 28); this.cmbPatient.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.lstServices.ItemHeight = 20; this.lstServices.Location = new System.Drawing.Point(26, 142); this.lstServices.Size = new System.Drawing.Size(330, 124);
            this.btnAddService.BackColor = System.Drawing.Color.Gainsboro; this.btnAddService.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnAddService.Location = new System.Drawing.Point(26, 277); this.btnAddService.Size = new System.Drawing.Size(330, 34); this.btnAddService.Text = "+ Добавить услугу";
            this.btnSave.BackColor = System.Drawing.Color.Gainsboro; this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnSave.Location = new System.Drawing.Point(26, 320); this.btnSave.Size = new System.Drawing.Size(330, 36); this.btnSave.Text = "Сохранить"; this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            this.ClientSize = new System.Drawing.Size(387, 381); this.Controls.Add(this.dtAppointment); this.Controls.Add(this.cmbDepartment); this.Controls.Add(this.cmbPatient); this.Controls.Add(this.lstServices); this.Controls.Add(this.btnAddService); this.Controls.Add(this.btnSave); this.Font = new System.Drawing.Font("Segoe UI", 10F); this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog; this.Name = "AppointmentEditForm"; this.Text = "Приём"; this.ResumeLayout(false);
        }
    }
}