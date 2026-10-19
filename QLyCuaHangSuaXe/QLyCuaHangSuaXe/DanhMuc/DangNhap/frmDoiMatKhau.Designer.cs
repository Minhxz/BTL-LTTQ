namespace QLyCuaHangSuaXe.DanhMuc.DangNhap
{
    partial class frmDoiMatKhau
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.grpDoiMatKhau = new System.Windows.Forms.GroupBox();
            this.lblMatKhauHienTai = new System.Windows.Forms.Label();
            this.txtMatKhauHienTai = new System.Windows.Forms.TextBox();
            this.lblMatKhauMoi = new System.Windows.Forms.Label();
            this.txtMatKhauMoi = new System.Windows.Forms.TextBox();
            this.lblXacNhanMatKhauMoi = new System.Windows.Forms.Label();
            this.txtXacNhanMatKhauMoi = new System.Windows.Forms.TextBox();
            this.chkHienMatKhau = new System.Windows.Forms.CheckBox();
            this.lblGhiChuMatKhau = new System.Windows.Forms.Label();
            this.btnDoiMatKhau = new System.Windows.Forms.Button();
            this.btnDong = new System.Windows.Forms.Button();
            this.grpDoiMatKhau.SuspendLayout();
            this.SuspendLayout();
            //
            // lblTieuDe
            //
            this.lblTieuDe.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTieuDe.ForeColor = System.Drawing.Color.FromArgb(27, 64, 96);
            this.lblTieuDe.Location = new System.Drawing.Point(24, 18);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(372, 34);
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "ĐỔI MẬT KHẨU";
            //
            // grpDoiMatKhau
            //
            this.grpDoiMatKhau.Controls.Add(this.lblMatKhauHienTai);
            this.grpDoiMatKhau.Controls.Add(this.txtMatKhauHienTai);
            this.grpDoiMatKhau.Controls.Add(this.lblMatKhauMoi);
            this.grpDoiMatKhau.Controls.Add(this.txtMatKhauMoi);
            this.grpDoiMatKhau.Controls.Add(this.lblXacNhanMatKhauMoi);
            this.grpDoiMatKhau.Controls.Add(this.txtXacNhanMatKhauMoi);
            this.grpDoiMatKhau.Controls.Add(this.chkHienMatKhau);
            this.grpDoiMatKhau.Controls.Add(this.lblGhiChuMatKhau);
            this.grpDoiMatKhau.ForeColor = System.Drawing.Color.FromArgb(27, 64, 96);
            this.grpDoiMatKhau.Location = new System.Drawing.Point(24, 62);
            this.grpDoiMatKhau.Name = "grpDoiMatKhau";
            this.grpDoiMatKhau.Size = new System.Drawing.Size(372, 252);
            this.grpDoiMatKhau.TabIndex = 1;
            this.grpDoiMatKhau.TabStop = false;
            this.grpDoiMatKhau.Text = "Thông tin mật khẩu";
            //
            // lblMatKhauHienTai
            //
            this.lblMatKhauHienTai.ForeColor = System.Drawing.Color.FromArgb(35, 35, 35);
            this.lblMatKhauHienTai.Location = new System.Drawing.Point(16, 24);
            this.lblMatKhauHienTai.Name = "lblMatKhauHienTai";
            this.lblMatKhauHienTai.Size = new System.Drawing.Size(340, 18);
            this.lblMatKhauHienTai.TabIndex = 0;
            this.lblMatKhauHienTai.Text = "Mật khẩu hiện tại:";
            //
            // txtMatKhauHienTai
            //
            this.txtMatKhauHienTai.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMatKhauHienTai.ForeColor = System.Drawing.Color.FromArgb(35, 35, 35);
            this.txtMatKhauHienTai.Location = new System.Drawing.Point(16, 44);
            this.txtMatKhauHienTai.MaxLength = 100;
            this.txtMatKhauHienTai.Name = "txtMatKhauHienTai";
            this.txtMatKhauHienTai.Size = new System.Drawing.Size(340, 23);
            this.txtMatKhauHienTai.TabIndex = 1;
            this.txtMatKhauHienTai.UseSystemPasswordChar = true;
            //
            // lblMatKhauMoi
            //
            this.lblMatKhauMoi.ForeColor = System.Drawing.Color.FromArgb(35, 35, 35);
            this.lblMatKhauMoi.Location = new System.Drawing.Point(16, 79);
            this.lblMatKhauMoi.Name = "lblMatKhauMoi";
            this.lblMatKhauMoi.Size = new System.Drawing.Size(340, 18);
            this.lblMatKhauMoi.TabIndex = 2;
            this.lblMatKhauMoi.Text = "Mật khẩu mới:";
            //
            // txtMatKhauMoi
            //
            this.txtMatKhauMoi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMatKhauMoi.ForeColor = System.Drawing.Color.FromArgb(35, 35, 35);
            this.txtMatKhauMoi.Location = new System.Drawing.Point(16, 99);
            this.txtMatKhauMoi.MaxLength = 100;
            this.txtMatKhauMoi.Name = "txtMatKhauMoi";
            this.txtMatKhauMoi.Size = new System.Drawing.Size(340, 23);
            this.txtMatKhauMoi.TabIndex = 3;
            this.txtMatKhauMoi.UseSystemPasswordChar = true;
            //
            // lblXacNhanMatKhauMoi
            //
            this.lblXacNhanMatKhauMoi.ForeColor = System.Drawing.Color.FromArgb(35, 35, 35);
            this.lblXacNhanMatKhauMoi.Location = new System.Drawing.Point(16, 134);
            this.lblXacNhanMatKhauMoi.Name = "lblXacNhanMatKhauMoi";
            this.lblXacNhanMatKhauMoi.Size = new System.Drawing.Size(340, 18);
            this.lblXacNhanMatKhauMoi.TabIndex = 4;
            this.lblXacNhanMatKhauMoi.Text = "Xác nhận mật khẩu:";
            //
            // txtXacNhanMatKhauMoi
            //
            this.txtXacNhanMatKhauMoi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtXacNhanMatKhauMoi.ForeColor = System.Drawing.Color.FromArgb(35, 35, 35);
            this.txtXacNhanMatKhauMoi.Location = new System.Drawing.Point(16, 154);
            this.txtXacNhanMatKhauMoi.MaxLength = 100;
            this.txtXacNhanMatKhauMoi.Name = "txtXacNhanMatKhauMoi";
            this.txtXacNhanMatKhauMoi.Size = new System.Drawing.Size(340, 23);
            this.txtXacNhanMatKhauMoi.TabIndex = 5;
            this.txtXacNhanMatKhauMoi.UseSystemPasswordChar = true;
            //
            // chkHienMatKhau
            //
            this.chkHienMatKhau.ForeColor = System.Drawing.Color.FromArgb(35, 35, 35);
            this.chkHienMatKhau.Location = new System.Drawing.Point(16, 189);
            this.chkHienMatKhau.Name = "chkHienMatKhau";
            this.chkHienMatKhau.Size = new System.Drawing.Size(340, 23);
            this.chkHienMatKhau.TabIndex = 6;
            this.chkHienMatKhau.Text = "Hiện mật khẩu";
            this.chkHienMatKhau.UseVisualStyleBackColor = true;
            this.chkHienMatKhau.CheckedChanged += new System.EventHandler(this.chkHienMatKhau_CheckedChanged);
            //
            // lblGhiChuMatKhau
            //
            this.lblGhiChuMatKhau.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGhiChuMatKhau.ForeColor = System.Drawing.Color.FromArgb(98, 98, 98);
            this.lblGhiChuMatKhau.Location = new System.Drawing.Point(16, 217);
            this.lblGhiChuMatKhau.Name = "lblGhiChuMatKhau";
            this.lblGhiChuMatKhau.Size = new System.Drawing.Size(340, 24);
            this.lblGhiChuMatKhau.TabIndex = 7;
            this.lblGhiChuMatKhau.Text = "Mật khẩu tối thiểu 8 ký tự, có chữ và số.";
            //
            // btnDoiMatKhau
            //
            this.btnDoiMatKhau.BackColor = System.Drawing.Color.FromArgb(37, 86, 133);
            this.btnDoiMatKhau.FlatAppearance.BorderSize = 0;
            this.btnDoiMatKhau.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDoiMatKhau.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDoiMatKhau.ForeColor = System.Drawing.Color.White;
            this.btnDoiMatKhau.Location = new System.Drawing.Point(24, 332);
            this.btnDoiMatKhau.Name = "btnDoiMatKhau";
            this.btnDoiMatKhau.Size = new System.Drawing.Size(178, 34);
            this.btnDoiMatKhau.TabIndex = 2;
            this.btnDoiMatKhau.Text = "Đổi mật khẩu";
            this.btnDoiMatKhau.UseVisualStyleBackColor = false;
            //
            // btnDong
            //
            this.btnDong.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnDong.ForeColor = System.Drawing.Color.FromArgb(35, 35, 35);
            this.btnDong.Location = new System.Drawing.Point(218, 332);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(178, 34);
            this.btnDong.TabIndex = 3;
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = true;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            //
            // frmDoiMatKhau
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(242, 242, 242);
            this.CancelButton = this.btnDong;
            this.ClientSize = new System.Drawing.Size(420, 400);
            this.Controls.Add(this.lblTieuDe);
            this.Controls.Add(this.grpDoiMatKhau);
            this.Controls.Add(this.btnDoiMatKhau);
            this.Controls.Add(this.btnDong);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDoiMatKhau";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Đổi mật khẩu";
            this.grpDoiMatKhau.ResumeLayout(false);
            this.grpDoiMatKhau.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.GroupBox grpDoiMatKhau;
        private System.Windows.Forms.Label lblMatKhauHienTai;
        private System.Windows.Forms.TextBox txtMatKhauHienTai;
        private System.Windows.Forms.Label lblMatKhauMoi;
        private System.Windows.Forms.TextBox txtMatKhauMoi;
        private System.Windows.Forms.Label lblXacNhanMatKhauMoi;
        private System.Windows.Forms.TextBox txtXacNhanMatKhauMoi;
        private System.Windows.Forms.CheckBox chkHienMatKhau;
        private System.Windows.Forms.Label lblGhiChuMatKhau;
        private System.Windows.Forms.Button btnDoiMatKhau;
        private System.Windows.Forms.Button btnDong;
    }
}
