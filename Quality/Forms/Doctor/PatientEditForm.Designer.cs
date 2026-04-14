namespace Quality.Forms.Doctor
{
    partial class PatientEditForm
    {
        private System.ComponentModel.IContainer components = null; private System.Windows.Forms.TextBox txtFullName; private System.Windows.Forms.DateTimePicker dtBirthDate; private System.Windows.Forms.TextBox txtPhone; private System.Windows.Forms.TextBox txtCardNumber; private System.Windows.Forms.Button btnSave;
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }
        private void InitializeComponent()
        {
            this.txtCardNumber = new System.Windows.Forms.TextBox(); this.txtFullName = new System.Windows.Forms.TextBox(); this.dtBirthDate = new System.Windows.Forms.DateTimePicker(); this.txtPhone = new System.Windows.Forms.TextBox(); this.btnSave = new System.Windows.Forms.Button(); this.SuspendLayout();
            this.txtCardNumber.Location = new System.Drawing.Point(26, 22); this.txtCardNumber.Size = new System.Drawing.Size(320, 27);
            this.txtFullName.Location = new System.Drawing.Point(26, 60); this.txtFullName.Size = new System.Drawing.Size(320, 27);
            this.dtBirthDate.Location = new System.Drawing.Point(26, 98); this.dtBirthDate.Size = new System.Drawing.Size(320, 27);
            this.txtPhone.Location = new System.Drawing.Point(26, 136); this.txtPhone.Size = new System.Drawing.Size(320, 27);
            this.btnSave.BackColor = System.Drawing.Color.Gainsboro; this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnSave.Location = new System.Drawing.Point(26, 182); this.btnSave.Size = new System.Drawing.Size(320, 36); this.btnSave.Text = "Сохранить"; this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            this.ClientSize = new System.Drawing.Size(378, 245); this.Controls.Add(this.txtCardNumber); this.Controls.Add(this.txtFullName); this.Controls.Add(this.dtBirthDate); this.Controls.Add(this.txtPhone); this.Controls.Add(this.btnSave); this.Font = new System.Drawing.Font("Segoe UI", 10F); this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog; this.Name = "PatientEditForm"; this.Text = "Пациент"; this.ResumeLayout(false); this.PerformLayout();
        }
    }
}