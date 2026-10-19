namespace QLyCuaHangSuaXe.DanhMuc.DangNhap
{
    partial class frmDangNhap
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.lblMoTa = new System.Windows.Forms.Label();
            this.grpDangNhap = new System.Windows.Forms.GroupBox();
            this.lblTenDangNhap = new System.Windows.Forms.Label();
            this.txtTenDangNhap = new System.Windows.Forms.TextBox();
            this.lblMatKhau = new System.Windows.Forms.Label();
            this.txtMatKhau = new System.Windows.Forms.TextBox();
            this.chkHienMatKhau = new System.Windows.Forms.CheckBox();
            this.btnDangNhap = new System.Windows.Forms.Button();
            this.btnThoat = new System.Windows.Forms.Button();
            this.btnMoDoiMatKhau = new System.Windows.Forms.Button();
            this.btnMoQuanLyTaiKhoan = new System.Windows.Forms.Button();
            this.grpDangNhap.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTieuDe.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(64)))), ((int)(((byte)(96)))));
            this.lblTieuDe.Location = new System.Drawing.Point(24, 20);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(392, 36);
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "ĐĂNG NHẬP";
            // 
            // lblMoTa
            // 
            this.lblMoTa.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(98)))), ((int)(((byte)(98)))), ((int)(((byte)(98)))));
            this.lblMoTa.Location = new System.Drawing.Point(26, 60);
            this.lblMoTa.Name = "lblMoTa";
            this.lblMoTa.Size = new System.Drawing.Size(390, 22);
            this.lblMoTa.TabIndex = 1;
            this.lblMoTa.Text = "Quản lý cửa hàng sửa xe";
            this.lblMoTa.Click += new System.EventHandler(this.lblMoTa_Click);
            // 
            // grpDangNhap
            // 
            this.grpDangNhap.Controls.Add(this.lblTenDangNhap);
            this.grpDangNhap.Controls.Add(this.txtTenDangNhap);
            this.grpDangNhap.Controls.Add(this.lblMatKhau);
            this.grpDangNhap.Controls.Add(this.txtMatKhau);
            this.grpDangNhap.Controls.Add(this.chkHienMatKhau);
            this.grpDangNhap.Controls.Add(this.btnDangNhap);
            this.grpDangNhap.Controls.Add(this.btnThoat);
            this.grpDangNhap.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.grpDangNhap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(64)))), ((int)(((byte)(96)))));
            this.grpDangNhap.Location = new System.Drawing.Point(24, 96);
            this.grpDangNhap.Name = "grpDangNhap";
            this.grpDangNhap.Size = new System.Drawing.Size(392, 238);
            this.grpDangNhap.TabIndex = 2;
            this.grpDangNhap.TabStop = false;
            this.grpDangNhap.Text = "Thông tin đăng nhập";
            // 
            // lblTenDangNhap
            // 
            this.lblTenDangNhap.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTenDangNhap.Location = new System.Drawing.Point(18, 26);
            this.lblTenDangNhap.Name = "lblTenDangNhap";
            this.lblTenDangNhap.Size = new System.Drawing.Size(356, 20);
            this.lblTenDangNhap.TabIndex = 0;
            this.lblTenDangNhap.Text = "Tên đăng nhập:";
            // 
            // txtTenDangNhap
            // 
            this.txtTenDangNhap.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtTenDangNhap.Location = new System.Drawing.Point(18, 49);
            this.txtTenDangNhap.MaxLength = 50;
            this.txtTenDangNhap.Name = "txtTenDangNhap";
            this.txtTenDangNhap.Size = new System.Drawing.Size(356, 27);
            this.txtTenDangNhap.TabIndex = 1;
            // 
            // lblMatKhau
            // 
            this.lblMatKhau.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblMatKhau.Location = new System.Drawing.Point(18, 88);
            this.lblMatKhau.Name = "lblMatKhau";
            this.lblMatKhau.Size = new System.Drawing.Size(356, 20);
            this.lblMatKhau.TabIndex = 2;
            this.lblMatKhau.Text = "Mật khẩu:";
            // 
            // txtMatKhau
            // 
            this.txtMatKhau.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtMatKhau.Location = new System.Drawing.Point(18, 111);
            this.txtMatKhau.MaxLength = 100;
            this.txtMatKhau.Name = "txtMatKhau";
            this.txtMatKhau.Size = new System.Drawing.Size(356, 27);
            this.txtMatKhau.TabIndex = 3;
            this.txtMatKhau.UseSystemPasswordChar = true;
            // 
            // chkHienMatKhau
            // 
            this.chkHienMatKhau.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkHienMatKhau.Location = new System.Drawing.Point(18, 147);
            this.chkHienMatKhau.Name = "chkHienMatKhau";
            this.chkHienMatKhau.Size = new System.Drawing.Size(200, 23);
            this.chkHienMatKhau.TabIndex = 4;
            this.chkHienMatKhau.Text = "Hiện mật khẩu";
            this.chkHienMatKhau.UseVisualStyleBackColor = true;
            this.chkHienMatKhau.CheckedChanged += new System.EventHandler(this.chkHienMatKhau_CheckedChanged);
            // 
            // btnDangNhap
            // 
            this.btnDangNhap.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(91)))), ((int)(((byte)(145)))));
            this.btnDangNhap.FlatAppearance.BorderSize = 0;
            this.btnDangNhap.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDangNhap.ForeColor = System.Drawing.Color.White;
            this.btnDangNhap.Location = new System.Drawing.Point(18, 184);
            this.btnDangNhap.Name = "btnDangNhap";
            this.btnDangNhap.Size = new System.Drawing.Size(170, 36);
            this.btnDangNhap.TabIndex = 5;
            this.btnDangNhap.Text = "Đăng nhập";
            this.btnDangNhap.UseVisualStyleBackColor = false;
            this.btnDangNhap.Click += new System.EventHandler(this.btnDangNhap_Click);
            // 
            // btnThoat
            // 
            this.btnThoat.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnThoat.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnThoat.Location = new System.Drawing.Point(204, 184);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(170, 36);
            this.btnThoat.TabIndex = 6;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.UseVisualStyleBackColor = true;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // btnMoDoiMatKhau
            // 
            this.btnMoDoiMatKhau.Location = new System.Drawing.Point(24, 354);
            this.btnMoDoiMatKhau.Name = "btnMoDoiMatKhau";
            this.btnMoDoiMatKhau.Size = new System.Drawing.Size(188, 36);
            this.btnMoDoiMatKhau.TabIndex = 3;
            this.btnMoDoiMatKhau.Text = "Đổi mật khẩu";
            this.btnMoDoiMatKhau.UseVisualStyleBackColor = true;
            this.btnMoDoiMatKhau.Click += new System.EventHandler(this.btnMoDoiMatKhau_Click);
            // 
            // btnMoQuanLyTaiKhoan
            // 
            this.btnMoQuanLyTaiKhoan.Location = new System.Drawing.Point(228, 354);
            this.btnMoQuanLyTaiKhoan.Name = "btnMoQuanLyTaiKhoan";
            this.btnMoQuanLyTaiKhoan.Size = new System.Drawing.Size(188, 36);
            this.btnMoQuanLyTaiKhoan.TabIndex = 4;
            this.btnMoQuanLyTaiKhoan.Text = "Quản lý tài khoản";
            this.btnMoQuanLyTaiKhoan.UseVisualStyleBackColor = true;
            this.btnMoQuanLyTaiKhoan.Click += new System.EventHandler(this.btnMoQuanLyTaiKhoan_Click);
            // 
            // frmDangNhap
            // 
            this.AcceptButton = this.btnDangNhap;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.CancelButton = this.btnThoat;
            this.ClientSize = new System.Drawing.Size(440, 412);
            this.Controls.Add(this.btnMoQuanLyTaiKhoan);
            this.Controls.Add(this.btnMoDoiMatKhau);
            this.Controls.Add(this.grpDangNhap);
            this.Controls.Add(this.lblMoTa);
            this.Controls.Add(this.lblTieuDe);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmDangNhap";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Đăng nhập - Quản lý cửa hàng sửa xe";
            this.grpDangNhap.ResumeLayout(false);
            this.grpDangNhap.PerformLayout();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.Label lblMoTa;
        private System.Windows.Forms.GroupBox grpDangNhap;
        private System.Windows.Forms.Label lblTenDangNhap;
        private System.Windows.Forms.TextBox txtTenDangNhap;
        private System.Windows.Forms.Label lblMatKhau;
        private System.Windows.Forms.TextBox txtMatKhau;
        private System.Windows.Forms.CheckBox chkHienMatKhau;
        private System.Windows.Forms.Button btnDangNhap;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.Button btnMoDoiMatKhau;
        private System.Windows.Forms.Button btnMoQuanLyTaiKhoan;
    }
}
