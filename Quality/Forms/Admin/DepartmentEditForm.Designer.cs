namespace Quality.Forms.Admin
{
    partial class DepartmentEditForm
    {
        private System.ComponentModel.IContainer components = null; private System.Windows.Forms.TextBox txtName; private System.Windows.Forms.TextBox txtLocation; private System.Windows.Forms.TextBox txtPhone; private System.Windows.Forms.Button btnSave;
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }
        private void InitializeComponent()
        {
            this.txtName = new System.Windows.Forms.TextBox(); this.txtLocation = new System.Windows.Forms.TextBox(); this.txtPhone = new System.Windows.Forms.TextBox(); this.btnSave = new System.Windows.Forms.Button(); this.SuspendLayout();
            this.txtName.Location = new System.Drawing.Point(30, 24); this.txtName.Size = new System.Drawing.Size(300, 27);
            this.txtLocation.Location = new System.Drawing.Point(30, 65); this.txtLocation.Size = new System.Drawing.Size(300, 27);
            this.txtPhone.Location = new System.Drawing.Point(30, 106); this.txtPhone.Size = new System.Drawing.Size(300, 27);
            this.btnSave.BackColor = System.Drawing.Color.Gainsboro; this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnSave.Location = new System.Drawing.Point(30, 153); this.btnSave.Size = new System.Drawing.Size(300, 36); this.btnSave.Text = "Сохранить"; this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            this.ClientSize = new System.Drawing.Size(364, 219); this.Controls.Add(this.txtName); this.Controls.Add(this.txtLocation); this.Controls.Add(this.txtPhone); this.Controls.Add(this.btnSave); this.Font = new System.Drawing.Font("Segoe UI", 10F); this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog; this.Name = "DepartmentEditForm"; this.Text = "Отделение"; this.ResumeLayout(false); this.PerformLayout();
        }
    }
}