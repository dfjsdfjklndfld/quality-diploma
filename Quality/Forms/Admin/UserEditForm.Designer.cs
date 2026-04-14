namespace Quality.Forms.Admin
{
    partial class UserEditForm
    {
        private System.ComponentModel.IContainer components = null; private System.Windows.Forms.TextBox txtLogin; private System.Windows.Forms.TextBox txtPassword; private System.Windows.Forms.TextBox txtFullName; private System.Windows.Forms.ComboBox cmbRole; private System.Windows.Forms.ComboBox cmbDepartment; private System.Windows.Forms.CheckBox chkIsActive; private System.Windows.Forms.Button btnSave;
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }
        private void InitializeComponent()
        {
            this.txtLogin = new System.Windows.Forms.TextBox(); this.txtPassword = new System.Windows.Forms.TextBox(); this.txtFullName = new System.Windows.Forms.TextBox(); this.cmbRole = new System.Windows.Forms.ComboBox(); this.cmbDepartment = new System.Windows.Forms.ComboBox(); this.chkIsActive = new System.Windows.Forms.CheckBox(); this.btnSave = new System.Windows.Forms.Button(); this.SuspendLayout();
            this.txtLogin.Location = new System.Drawing.Point(30, 25); this.txtLogin.Size = new System.Drawing.Size(290, 27);
            this.txtPassword.Location = new System.Drawing.Point(30, 66); this.txtPassword.Size = new System.Drawing.Size(290, 27);
            this.txtFullName.Location = new System.Drawing.Point(30, 107); this.txtFullName.Size = new System.Drawing.Size(290, 27);
            this.cmbRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList; this.cmbRole.Location = new System.Drawing.Point(30, 148); this.cmbRole.Size = new System.Drawing.Size(290, 28);
            this.cmbDepartment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList; this.cmbDepartment.Location = new System.Drawing.Point(30, 189); this.cmbDepartment.Size = new System.Drawing.Size(290, 28);
            this.chkIsActive.AutoSize = true; this.chkIsActive.Checked = true; this.chkIsActive.CheckState = System.Windows.Forms.CheckState.Checked; this.chkIsActive.Location = new System.Drawing.Point(30, 228); this.chkIsActive.Text = "Активный пользователь";
            this.btnSave.BackColor = System.Drawing.Color.Gainsboro; this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnSave.Location = new System.Drawing.Point(30, 261); this.btnSave.Size = new System.Drawing.Size(290, 36); this.btnSave.Text = "Сохранить"; this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            this.ClientSize = new System.Drawing.Size(356, 322); this.Controls.Add(this.txtLogin); this.Controls.Add(this.txtPassword); this.Controls.Add(this.txtFullName); this.Controls.Add(this.cmbRole); this.Controls.Add(this.cmbDepartment); this.Controls.Add(this.chkIsActive); this.Controls.Add(this.btnSave); this.Font = new System.Drawing.Font("Segoe UI", 10F); this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog; this.Name = "UserEditForm"; this.Text = "Пользователь"; this.ResumeLayout(false); this.PerformLayout();
        }
    }
}