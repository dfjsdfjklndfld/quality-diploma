namespace Quality.Forms.QualityManager
{
    partial class CorrectiveActionEditForm
    {
        private System.ComponentModel.IContainer components = null; private System.Windows.Forms.TextBox txtTitle; private System.Windows.Forms.TextBox txtDescription; private System.Windows.Forms.DateTimePicker dtDeadline; private System.Windows.Forms.ComboBox cmbStatus; private System.Windows.Forms.Button btnSave;
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }
        private void InitializeComponent()
        {
            this.txtTitle = new System.Windows.Forms.TextBox(); this.txtDescription = new System.Windows.Forms.TextBox(); this.dtDeadline = new System.Windows.Forms.DateTimePicker(); this.cmbStatus = new System.Windows.Forms.ComboBox(); this.btnSave = new System.Windows.Forms.Button(); this.SuspendLayout();
            this.txtTitle.Location = new System.Drawing.Point(24, 22); this.txtTitle.Size = new System.Drawing.Size(340, 27);
            this.txtDescription.Location = new System.Drawing.Point(24, 60); this.txtDescription.Multiline = true; this.txtDescription.Size = new System.Drawing.Size(340, 110);
            this.dtDeadline.Location = new System.Drawing.Point(24, 181); this.dtDeadline.Size = new System.Drawing.Size(340, 27);
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList; this.cmbStatus.Items.AddRange(new object[] {"new", "in_progress", "completed"}); this.cmbStatus.Location = new System.Drawing.Point(24, 219); this.cmbStatus.Size = new System.Drawing.Size(340, 28);
            this.btnSave.BackColor = System.Drawing.Color.Gainsboro; this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnSave.Location = new System.Drawing.Point(24, 263); this.btnSave.Size = new System.Drawing.Size(340, 36); this.btnSave.Text = "Сохранить"; this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            this.ClientSize = new System.Drawing.Size(392, 321); this.Controls.Add(this.txtTitle); this.Controls.Add(this.txtDescription); this.Controls.Add(this.dtDeadline); this.Controls.Add(this.cmbStatus); this.Controls.Add(this.btnSave); this.Font = new System.Drawing.Font("Segoe UI", 10F); this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog; this.Name = "CorrectiveActionEditForm"; this.Text = "Корректирующее действие"; this.ResumeLayout(false); this.PerformLayout();
        }
    }
}