namespace QLyCuaHangSuaXe.DanhMuc.DangNhap
{
    partial class frmQuanLyTaiKhoan
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.lblMoTa = new System.Windows.Forms.Label();
            this.tabChucNang = new System.Windows.Forms.TabControl();
            this.tabTaiKhoan = new System.Windows.Forms.TabPage();
            this.tabPhanQuyen = new System.Windows.Forms.TabPage();
            this.grpDanhSachTaiKhoan = new System.Windows.Forms.GroupBox();
            this.lblTimKiem = new System.Windows.Forms.Label();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.lblLocVaiTro = new System.Windows.Forms.Label();
            this.cboLocVaiTro = new System.Windows.Forms.ComboBox();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.dgvTaiKhoan = new System.Windows.Forms.DataGridView();
            this.grpThongTinTaiKhoan = new System.Windows.Forms.GroupBox();
            this.lblMaTaiKhoan = new System.Windows.Forms.Label();
            this.txtMaTaiKhoan = new System.Windows.Forms.TextBox();
            this.lblHoTen = new System.Windows.Forms.Label();
            this.txtHoTen = new System.Windows.Forms.TextBox();
            this.lblTenDangNhapTaiKhoan = new System.Windows.Forms.Label();
            this.txtTenDangNhapTaiKhoan = new System.Windows.Forms.TextBox();
            this.lblVaiTro = new System.Windows.Forms.Label();
            this.cboVaiTro = new System.Windows.Forms.ComboBox();
            this.lblMatKhauTaiKhoan = new System.Windows.Forms.Label();
            this.txtMatKhauTaiKhoan = new System.Windows.Forms.TextBox();
            this.lblXacNhanMatKhau = new System.Windows.Forms.Label();
            this.txtXacNhanMatKhau = new System.Windows.Forms.TextBox();
            this.chkTaiKhoanHoatDong = new System.Windows.Forms.CheckBox();
            this.lblGhiChuTaiKhoan = new System.Windows.Forms.Label();
            this.grpPhanQuyen = new System.Windows.Forms.GroupBox();
            this.lblViTri = new System.Windows.Forms.Label();
            this.lstVaiTro = new System.Windows.Forms.ListBox();
            this.lblChucNang = new System.Windows.Forms.Label();
            this.chkQuanLyTaiKhoan = new System.Windows.Forms.CheckBox();
            this.chkTiepNhanSuaChua = new System.Windows.Forms.CheckBox();
            this.chkDanhMucDichVu = new System.Windows.Forms.CheckBox();
            this.chkNhapKhoPhuTung = new System.Windows.Forms.CheckBox();
            this.chkBaoCaoThongKe = new System.Windows.Forms.CheckBox();
            this.lblGhiChuPhanQuyen = new System.Windows.Forms.Label();
            this.grpThaoTac = new System.Windows.Forms.GroupBox();
            this.btnThemMoi = new System.Windows.Forms.Button();
            this.btnLuuTaiKhoan = new System.Windows.Forms.Button();
            this.btnKhoaMoKhoa = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.btnDong = new System.Windows.Forms.Button();
            this.colMaTaiKhoan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTenDangNhap = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHoTen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colVaiTro = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTrangThai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stsTrangThai = new System.Windows.Forms.StatusStrip();
            this.tsslTrangThai = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslVaiTro = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabChucNang.SuspendLayout();
            this.tabTaiKhoan.SuspendLayout();
            this.tabPhanQuyen.SuspendLayout();
            this.grpDanhSachTaiKhoan.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTaiKhoan)).BeginInit();
            this.grpThongTinTaiKhoan.SuspendLayout();
            this.grpThaoTac.SuspendLayout();
            this.grpPhanQuyen.SuspendLayout();
            this.stsTrangThai.SuspendLayout();
            this.SuspendLayout();
            // lblTieuDe
            this.lblTieuDe.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTieuDe.ForeColor = System.Drawing.Color.FromArgb(27, 64, 96);
            this.lblTieuDe.Location = new System.Drawing.Point(20, 14);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(900, 34);
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "QUẢN LÝ TÀI KHOẢN VÀ PHÂN QUYỀN";
            // lblMoTa
            this.lblMoTa.ForeColor = System.Drawing.Color.FromArgb(98, 98, 98);
            this.lblMoTa.Location = new System.Drawing.Point(22, 52);
            this.lblMoTa.Name = "lblMoTa";
            this.lblMoTa.Size = new System.Drawing.Size(896, 20);
            this.lblMoTa.TabIndex = 1;
            this.lblMoTa.Text = "Quản lý tài khoản và quyền truy cập theo từng vị trí";
            // tabChucNang
            this.tabChucNang.Controls.Add(this.tabTaiKhoan);
            this.tabChucNang.Controls.Add(this.tabPhanQuyen);
            this.tabChucNang.Location = new System.Drawing.Point(20, 82);
            this.tabChucNang.Name = "tabChucNang";
            this.tabChucNang.SelectedIndex = 0;
            this.tabChucNang.Size = new System.Drawing.Size(900, 524);
            this.tabChucNang.TabIndex = 2;
            // tabTaiKhoan
            this.tabTaiKhoan.Controls.Add(this.grpDanhSachTaiKhoan);
            this.tabTaiKhoan.Controls.Add(this.grpThongTinTaiKhoan);
            this.tabTaiKhoan.Controls.Add(this.grpThaoTac);
            this.tabTaiKhoan.Location = new System.Drawing.Point(4, 24);
            this.tabTaiKhoan.Name = "tabTaiKhoan";
            this.tabTaiKhoan.Padding = new System.Windows.Forms.Padding(3);
            this.tabTaiKhoan.Size = new System.Drawing.Size(892, 496);
            this.tabTaiKhoan.TabIndex = 0;
            this.tabTaiKhoan.Text = "Tài khoản";
            this.tabTaiKhoan.UseVisualStyleBackColor = true;
            // tabPhanQuyen
            this.tabPhanQuyen.Controls.Add(this.grpPhanQuyen);
            this.tabPhanQuyen.Location = new System.Drawing.Point(4, 24);
            this.tabPhanQuyen.Name = "tabPhanQuyen";
            this.tabPhanQuyen.Padding = new System.Windows.Forms.Padding(3);
            this.tabPhanQuyen.Size = new System.Drawing.Size(892, 496);
            this.tabPhanQuyen.TabIndex = 1;
            this.tabPhanQuyen.Text = "Phân quyền";
            this.tabPhanQuyen.UseVisualStyleBackColor = true;
            // 
            // grpDanhSachTaiKhoan
            // 
            this.grpDanhSachTaiKhoan.Controls.Add(this.lblTimKiem);
            this.grpDanhSachTaiKhoan.Controls.Add(this.txtTimKiem);
            this.grpDanhSachTaiKhoan.Controls.Add(this.lblLocVaiTro);
            this.grpDanhSachTaiKhoan.Controls.Add(this.cboLocVaiTro);
            this.grpDanhSachTaiKhoan.Controls.Add(this.btnTimKiem);
            this.grpDanhSachTaiKhoan.Controls.Add(this.dgvTaiKhoan);
            this.grpDanhSachTaiKhoan.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpDanhSachTaiKhoan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(64)))), ((int)(((byte)(96)))));
            this.grpDanhSachTaiKhoan.Location = new System.Drawing.Point(12, 10);
            this.grpDanhSachTaiKhoan.Name = "grpDanhSachTaiKhoan";
            this.grpDanhSachTaiKhoan.Size = new System.Drawing.Size(868, 216);
            this.grpDanhSachTaiKhoan.TabIndex = 4;
            this.grpDanhSachTaiKhoan.TabStop = false;
            this.grpDanhSachTaiKhoan.Text = "Danh sách tài khoản";
            // 
            // lblTimKiem
            // 
            this.lblTimKiem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTimKiem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.lblTimKiem.Location = new System.Drawing.Point(16, 31);
            this.lblTimKiem.Name = "lblTimKiem";
            this.lblTimKiem.Size = new System.Drawing.Size(64, 20);
            this.lblTimKiem.TabIndex = 0;
            this.lblTimKiem.Text = "Tìm kiếm:";
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTimKiem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTimKiem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.txtTimKiem.Location = new System.Drawing.Point(80, 27);
            this.txtTimKiem.MaxLength = 100;
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new System.Drawing.Size(280, 23);
            this.txtTimKiem.TabIndex = 1;
            // 
            // lblLocVaiTro
            // 
            this.lblLocVaiTro.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLocVaiTro.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.lblLocVaiTro.Location = new System.Drawing.Point(376, 31);
            this.lblLocVaiTro.Name = "lblLocVaiTro";
            this.lblLocVaiTro.Size = new System.Drawing.Size(50, 20);
            this.lblLocVaiTro.TabIndex = 2;
            this.lblLocVaiTro.Text = "Vai trò:";
            // 
            // cboLocVaiTro
            // 
            this.cboLocVaiTro.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLocVaiTro.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboLocVaiTro.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.cboLocVaiTro.FormattingEnabled = true;
            this.cboLocVaiTro.Items.AddRange(new object[] {
            "Tất cả",
            "Chủ cửa hàng",
            "Thu ngân",
            "Kho",
            "Thợ"});
            this.cboLocVaiTro.Location = new System.Drawing.Point(426, 27);
            this.cboLocVaiTro.Name = "cboLocVaiTro";
            this.cboLocVaiTro.Size = new System.Drawing.Size(260, 23);
            this.cboLocVaiTro.TabIndex = 3;
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTimKiem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.btnTimKiem.Location = new System.Drawing.Point(700, 25);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(152, 28);
            this.btnTimKiem.TabIndex = 4;
            this.btnTimKiem.Text = "Tìm";
            this.btnTimKiem.UseVisualStyleBackColor = true;
            // 
            // dgvTaiKhoan
            // 
            this.dgvTaiKhoan.AllowUserToAddRows = false;
            this.dgvTaiKhoan.AllowUserToDeleteRows = false;
            this.dgvTaiKhoan.AllowUserToResizeRows = false;
            this.dgvTaiKhoan.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTaiKhoan.BackgroundColor = System.Drawing.Color.White;
            this.dgvTaiKhoan.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(234)))), ((int)(((byte)(238)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(234)))), ((int)(((byte)(238)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTaiKhoan.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvTaiKhoan.ColumnHeadersHeight = 34;
            this.dgvTaiKhoan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvTaiKhoan.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colMaTaiKhoan,
            this.colTenDangNhap,
            this.colHoTen,
            this.colVaiTro,
            this.colTrangThai});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(232)))), ((int)(((byte)(244)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvTaiKhoan.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvTaiKhoan.EnableHeadersVisualStyles = false;
            this.dgvTaiKhoan.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvTaiKhoan.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(220)))), ((int)(((byte)(224)))));
            this.dgvTaiKhoan.Location = new System.Drawing.Point(16, 66);
            this.dgvTaiKhoan.MultiSelect = false;
            this.dgvTaiKhoan.Name = "dgvTaiKhoan";
            this.dgvTaiKhoan.ReadOnly = true;
            this.dgvTaiKhoan.RowHeadersVisible = false;
            this.dgvTaiKhoan.RowHeadersWidth = 51;
            this.dgvTaiKhoan.RowTemplate.Height = 30;
            this.dgvTaiKhoan.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTaiKhoan.Size = new System.Drawing.Size(836, 134);
            this.dgvTaiKhoan.TabIndex = 5;
            // 
            // colMaTaiKhoan
            // 
            this.colMaTaiKhoan.FillWeight = 65F;
            this.colMaTaiKhoan.HeaderText = "Mã TK";
            this.colMaTaiKhoan.MinimumWidth = 65;
            this.colMaTaiKhoan.Name = "colMaTaiKhoan";
            this.colMaTaiKhoan.ReadOnly = true;
            // 
            // colTenDangNhap
            // 
            this.colTenDangNhap.FillWeight = 145F;
            this.colTenDangNhap.HeaderText = "Tên đăng nhập";
            this.colTenDangNhap.MinimumWidth = 110;
            this.colTenDangNhap.Name = "colTenDangNhap";
            this.colTenDangNhap.ReadOnly = true;
            // 
            // colHoTen
            // 
            this.colHoTen.FillWeight = 170F;
            this.colHoTen.HeaderText = "Họ tên";
            this.colHoTen.MinimumWidth = 120;
            this.colHoTen.Name = "colHoTen";
            this.colHoTen.ReadOnly = true;
            // 
            // colVaiTro
            // 
            this.colVaiTro.FillWeight = 135F;
            this.colVaiTro.HeaderText = "Vai trò";
            this.colVaiTro.MinimumWidth = 95;
            this.colVaiTro.Name = "colVaiTro";
            this.colVaiTro.ReadOnly = true;
            // 
            // colTrangThai
            // 
            this.colTrangThai.FillWeight = 130F;
            this.colTrangThai.HeaderText = "Trạng thái";
            this.colTrangThai.MinimumWidth = 95;
            this.colTrangThai.Name = "colTrangThai";
            this.colTrangThai.ReadOnly = true;
            // 
            // grpThongTinTaiKhoan
            // 
            this.grpThongTinTaiKhoan.Controls.Add(this.lblMaTaiKhoan);
            this.grpThongTinTaiKhoan.Controls.Add(this.txtMaTaiKhoan);
            this.grpThongTinTaiKhoan.Controls.Add(this.lblHoTen);
            this.grpThongTinTaiKhoan.Controls.Add(this.txtHoTen);
            this.grpThongTinTaiKhoan.Controls.Add(this.lblTenDangNhapTaiKhoan);
            this.grpThongTinTaiKhoan.Controls.Add(this.txtTenDangNhapTaiKhoan);
            this.grpThongTinTaiKhoan.Controls.Add(this.lblVaiTro);
            this.grpThongTinTaiKhoan.Controls.Add(this.cboVaiTro);
            this.grpThongTinTaiKhoan.Controls.Add(this.lblMatKhauTaiKhoan);
            this.grpThongTinTaiKhoan.Controls.Add(this.txtMatKhauTaiKhoan);
            this.grpThongTinTaiKhoan.Controls.Add(this.lblXacNhanMatKhau);
            this.grpThongTinTaiKhoan.Controls.Add(this.txtXacNhanMatKhau);
            this.grpThongTinTaiKhoan.Controls.Add(this.chkTaiKhoanHoatDong);
            this.grpThongTinTaiKhoan.Controls.Add(this.lblGhiChuTaiKhoan);
            this.grpThongTinTaiKhoan.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpThongTinTaiKhoan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(64)))), ((int)(((byte)(96)))));
            this.grpThongTinTaiKhoan.Location = new System.Drawing.Point(12, 234);
            this.grpThongTinTaiKhoan.Name = "grpThongTinTaiKhoan";
            this.grpThongTinTaiKhoan.Size = new System.Drawing.Size(868, 178);
            this.grpThongTinTaiKhoan.TabIndex = 5;
            this.grpThongTinTaiKhoan.TabStop = false;
            this.grpThongTinTaiKhoan.Text = "Thông tin tài khoản";
            // 
            // lblMaTaiKhoan
            // 
            this.lblMaTaiKhoan.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaTaiKhoan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.lblMaTaiKhoan.Location = new System.Drawing.Point(16, 31);
            this.lblMaTaiKhoan.Name = "lblMaTaiKhoan";
            this.lblMaTaiKhoan.Size = new System.Drawing.Size(116, 20);
            this.lblMaTaiKhoan.TabIndex = 0;
            this.lblMaTaiKhoan.Text = "Mã tài khoản:";
            // 
            // txtMaTaiKhoan
            // 
            this.txtMaTaiKhoan.BackColor = System.Drawing.SystemColors.Control;
            this.txtMaTaiKhoan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMaTaiKhoan.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaTaiKhoan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.txtMaTaiKhoan.Location = new System.Drawing.Point(132, 27);
            this.txtMaTaiKhoan.MaxLength = 100;
            this.txtMaTaiKhoan.Name = "txtMaTaiKhoan";
            this.txtMaTaiKhoan.ReadOnly = true;
            this.txtMaTaiKhoan.Size = new System.Drawing.Size(260, 23);
            this.txtMaTaiKhoan.TabIndex = 1;
            this.txtMaTaiKhoan.TabStop = false;
            // 
            // lblHoTen
            // 
            this.lblHoTen.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHoTen.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.lblHoTen.Location = new System.Drawing.Point(420, 31);
            this.lblHoTen.Name = "lblHoTen";
            this.lblHoTen.Size = new System.Drawing.Size(98, 20);
            this.lblHoTen.TabIndex = 2;
            this.lblHoTen.Text = "Họ tên:";
            // 
            // txtHoTen
            // 
            this.txtHoTen.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtHoTen.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHoTen.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.txtHoTen.Location = new System.Drawing.Point(518, 27);
            this.txtHoTen.MaxLength = 100;
            this.txtHoTen.Name = "txtHoTen";
            this.txtHoTen.Size = new System.Drawing.Size(334, 23);
            this.txtHoTen.TabIndex = 3;
            // 
            // lblTenDangNhapTaiKhoan
            // 
            this.lblTenDangNhapTaiKhoan.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTenDangNhapTaiKhoan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.lblTenDangNhapTaiKhoan.Location = new System.Drawing.Point(16, 67);
            this.lblTenDangNhapTaiKhoan.Name = "lblTenDangNhapTaiKhoan";
            this.lblTenDangNhapTaiKhoan.Size = new System.Drawing.Size(116, 20);
            this.lblTenDangNhapTaiKhoan.TabIndex = 4;
            this.lblTenDangNhapTaiKhoan.Text = "Tên đăng nhập:";
            // 
            // txtTenDangNhapTaiKhoan
            // 
            this.txtTenDangNhapTaiKhoan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTenDangNhapTaiKhoan.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTenDangNhapTaiKhoan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.txtTenDangNhapTaiKhoan.Location = new System.Drawing.Point(132, 63);
            this.txtTenDangNhapTaiKhoan.MaxLength = 50;
            this.txtTenDangNhapTaiKhoan.Name = "txtTenDangNhapTaiKhoan";
            this.txtTenDangNhapTaiKhoan.Size = new System.Drawing.Size(260, 23);
            this.txtTenDangNhapTaiKhoan.TabIndex = 5;
            // 
            // lblVaiTro
            // 
            this.lblVaiTro.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVaiTro.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.lblVaiTro.Location = new System.Drawing.Point(420, 67);
            this.lblVaiTro.Name = "lblVaiTro";
            this.lblVaiTro.Size = new System.Drawing.Size(98, 20);
            this.lblVaiTro.TabIndex = 6;
            this.lblVaiTro.Text = "Vai trò:";
            // 
            // cboVaiTro
            // 
            this.cboVaiTro.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboVaiTro.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboVaiTro.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.cboVaiTro.FormattingEnabled = true;
            this.cboVaiTro.Items.AddRange(new object[] {
            "Chọn vai trò",
            "Chủ cửa hàng",
            "Thu ngân",
            "Kho",
            "Thợ"});
            this.cboVaiTro.Location = new System.Drawing.Point(518, 63);
            this.cboVaiTro.Name = "cboVaiTro";
            this.cboVaiTro.Size = new System.Drawing.Size(334, 23);
            this.cboVaiTro.TabIndex = 7;
            // 
            // lblMatKhauTaiKhoan
            // 
            this.lblMatKhauTaiKhoan.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMatKhauTaiKhoan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.lblMatKhauTaiKhoan.Location = new System.Drawing.Point(16, 103);
            this.lblMatKhauTaiKhoan.Name = "lblMatKhauTaiKhoan";
            this.lblMatKhauTaiKhoan.Size = new System.Drawing.Size(116, 20);
            this.lblMatKhauTaiKhoan.TabIndex = 8;
            this.lblMatKhauTaiKhoan.Text = "Mật khẩu:";
            // 
            // txtMatKhauTaiKhoan
            // 
            this.txtMatKhauTaiKhoan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMatKhauTaiKhoan.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMatKhauTaiKhoan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.txtMatKhauTaiKhoan.Location = new System.Drawing.Point(132, 99);
            this.txtMatKhauTaiKhoan.MaxLength = 100;
            this.txtMatKhauTaiKhoan.Name = "txtMatKhauTaiKhoan";
            this.txtMatKhauTaiKhoan.Size = new System.Drawing.Size(260, 23);
            this.txtMatKhauTaiKhoan.TabIndex = 9;
            this.txtMatKhauTaiKhoan.UseSystemPasswordChar = true;
            // 
            // lblXacNhanMatKhau
            // 
            this.lblXacNhanMatKhau.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblXacNhanMatKhau.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.lblXacNhanMatKhau.Location = new System.Drawing.Point(420, 103);
            this.lblXacNhanMatKhau.Name = "lblXacNhanMatKhau";
            this.lblXacNhanMatKhau.Size = new System.Drawing.Size(98, 20);
            this.lblXacNhanMatKhau.TabIndex = 10;
            this.lblXacNhanMatKhau.Text = "Xác nhận:";
            // 
            // txtXacNhanMatKhau
            // 
            this.txtXacNhanMatKhau.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtXacNhanMatKhau.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtXacNhanMatKhau.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.txtXacNhanMatKhau.Location = new System.Drawing.Point(518, 99);
            this.txtXacNhanMatKhau.MaxLength = 100;
            this.txtXacNhanMatKhau.Name = "txtXacNhanMatKhau";
            this.txtXacNhanMatKhau.Size = new System.Drawing.Size(334, 23);
            this.txtXacNhanMatKhau.TabIndex = 11;
            this.txtXacNhanMatKhau.UseSystemPasswordChar = true;
            // 
            // chkTaiKhoanHoatDong
            // 
            this.chkTaiKhoanHoatDong.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkTaiKhoanHoatDong.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.chkTaiKhoanHoatDong.Location = new System.Drawing.Point(16, 138);
            this.chkTaiKhoanHoatDong.Name = "chkTaiKhoanHoatDong";
            this.chkTaiKhoanHoatDong.Size = new System.Drawing.Size(300, 23);
            this.chkTaiKhoanHoatDong.TabIndex = 12;
            this.chkTaiKhoanHoatDong.Text = "Tài khoản đang hoạt động";
            this.chkTaiKhoanHoatDong.UseVisualStyleBackColor = true;
            // 
            // lblGhiChuTaiKhoan
            // 
            this.lblGhiChuTaiKhoan.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGhiChuTaiKhoan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(98)))), ((int)(((byte)(98)))), ((int)(((byte)(98)))));
            this.lblGhiChuTaiKhoan.Location = new System.Drawing.Point(332, 137);
            this.lblGhiChuTaiKhoan.Name = "lblGhiChuTaiKhoan";
            this.lblGhiChuTaiKhoan.Size = new System.Drawing.Size(520, 30);
            this.lblGhiChuTaiKhoan.TabIndex = 13;
            this.lblGhiChuTaiKhoan.Text = "Chỉ Chủ cửa hàng được quản lý tài khoản và phân quyền.";
            // 
            // grpPhanQuyen
            // 
            this.grpPhanQuyen.Controls.Add(this.lblViTri);
            this.grpPhanQuyen.Controls.Add(this.lstVaiTro);
            this.grpPhanQuyen.Controls.Add(this.lblChucNang);
            this.grpPhanQuyen.Controls.Add(this.chkQuanLyTaiKhoan);
            this.grpPhanQuyen.Controls.Add(this.chkTiepNhanSuaChua);
            this.grpPhanQuyen.Controls.Add(this.chkDanhMucDichVu);
            this.grpPhanQuyen.Controls.Add(this.chkNhapKhoPhuTung);
            this.grpPhanQuyen.Controls.Add(this.chkBaoCaoThongKe);
            this.grpPhanQuyen.Controls.Add(this.lblGhiChuPhanQuyen);
            this.grpPhanQuyen.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpPhanQuyen.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(64)))), ((int)(((byte)(96)))));
            this.grpPhanQuyen.Location = new System.Drawing.Point(12, 10);
            this.grpPhanQuyen.Name = "grpPhanQuyen";
            this.grpPhanQuyen.Size = new System.Drawing.Size(868, 260);
            this.grpPhanQuyen.TabIndex = 6;
            this.grpPhanQuyen.TabStop = false;
            this.grpPhanQuyen.Text = "Phân quyền chức năng";
            // 
            // lblViTri
            // 
            this.lblViTri.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblViTri.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.lblViTri.Location = new System.Drawing.Point(16, 32);
            this.lblViTri.Name = "lblViTri";
            this.lblViTri.Size = new System.Drawing.Size(236, 20);
            this.lblViTri.TabIndex = 0;
            this.lblViTri.Text = "Vị trí:";
            // 
            // lstVaiTro
            // 
            this.lstVaiTro.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstVaiTro.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstVaiTro.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.lstVaiTro.FormattingEnabled = true;
            this.lstVaiTro.IntegralHeight = false;
            this.lstVaiTro.ItemHeight = 20;
            this.lstVaiTro.Items.AddRange(new object[] {
            "Chủ cửa hàng",
            "Thu ngân",
            "Kho",
            "Thợ"});
            this.lstVaiTro.Location = new System.Drawing.Point(16, 60);
            this.lstVaiTro.Name = "lstVaiTro";
            this.lstVaiTro.Size = new System.Drawing.Size(236, 120);
            this.lstVaiTro.TabIndex = 1;
            // 
            // lblChucNang
            // 
            this.lblChucNang.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChucNang.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.lblChucNang.Location = new System.Drawing.Point(300, 32);
            this.lblChucNang.Name = "lblChucNang";
            this.lblChucNang.Size = new System.Drawing.Size(520, 20);
            this.lblChucNang.TabIndex = 2;
            this.lblChucNang.Text = "Chức năng được hiển thị:";
            // 
            // chkQuanLyTaiKhoan
            // 
            this.chkQuanLyTaiKhoan.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkQuanLyTaiKhoan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.chkQuanLyTaiKhoan.Location = new System.Drawing.Point(300, 64);
            this.chkQuanLyTaiKhoan.Name = "chkQuanLyTaiKhoan";
            this.chkQuanLyTaiKhoan.Size = new System.Drawing.Size(520, 24);
            this.chkQuanLyTaiKhoan.TabIndex = 3;
            this.chkQuanLyTaiKhoan.Text = "Quản lý tài khoản";
            this.chkQuanLyTaiKhoan.UseVisualStyleBackColor = true;
            // 
            // chkTiepNhanSuaChua
            // 
            this.chkTiepNhanSuaChua.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkTiepNhanSuaChua.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.chkTiepNhanSuaChua.Location = new System.Drawing.Point(300, 98);
            this.chkTiepNhanSuaChua.Name = "chkTiepNhanSuaChua";
            this.chkTiepNhanSuaChua.Size = new System.Drawing.Size(520, 24);
            this.chkTiepNhanSuaChua.TabIndex = 4;
            this.chkTiepNhanSuaChua.Text = "Tiếp nhận và sửa chữa xe";
            this.chkTiepNhanSuaChua.UseVisualStyleBackColor = true;
            // 
            // chkDanhMucDichVu
            // 
            this.chkDanhMucDichVu.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDanhMucDichVu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.chkDanhMucDichVu.Location = new System.Drawing.Point(300, 132);
            this.chkDanhMucDichVu.Name = "chkDanhMucDichVu";
            this.chkDanhMucDichVu.Size = new System.Drawing.Size(520, 24);
            this.chkDanhMucDichVu.TabIndex = 5;
            this.chkDanhMucDichVu.Text = "Danh mục dịch vụ";
            this.chkDanhMucDichVu.UseVisualStyleBackColor = true;
            // 
            // chkNhapKhoPhuTung
            // 
            this.chkNhapKhoPhuTung.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkNhapKhoPhuTung.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.chkNhapKhoPhuTung.Location = new System.Drawing.Point(300, 166);
            this.chkNhapKhoPhuTung.Name = "chkNhapKhoPhuTung";
            this.chkNhapKhoPhuTung.Size = new System.Drawing.Size(520, 24);
            this.chkNhapKhoPhuTung.TabIndex = 6;
            this.chkNhapKhoPhuTung.Text = "Nhập kho phụ tùng";
            this.chkNhapKhoPhuTung.UseVisualStyleBackColor = true;
            // 
            // chkBaoCaoThongKe
            // 
            this.chkBaoCaoThongKe.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkBaoCaoThongKe.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.chkBaoCaoThongKe.Location = new System.Drawing.Point(300, 200);
            this.chkBaoCaoThongKe.Name = "chkBaoCaoThongKe";
            this.chkBaoCaoThongKe.Size = new System.Drawing.Size(520, 24);
            this.chkBaoCaoThongKe.TabIndex = 7;
            this.chkBaoCaoThongKe.Text = "Báo cáo và thống kê";
            this.chkBaoCaoThongKe.UseVisualStyleBackColor = true;
            // 
            // lblGhiChuPhanQuyen
            // 
            this.lblGhiChuPhanQuyen.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGhiChuPhanQuyen.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(98)))), ((int)(((byte)(98)))), ((int)(((byte)(98)))));
            this.lblGhiChuPhanQuyen.Location = new System.Drawing.Point(16, 204);
            this.lblGhiChuPhanQuyen.Name = "lblGhiChuPhanQuyen";
            this.lblGhiChuPhanQuyen.Size = new System.Drawing.Size(236, 38);
            this.lblGhiChuPhanQuyen.TabIndex = 8;
            this.lblGhiChuPhanQuyen.Text = "Chọn vị trí để xem quyền truy cập.";
            // 
            // grpThaoTac
            // 
            this.grpThaoTac.Controls.Add(this.btnThemMoi);
            this.grpThaoTac.Controls.Add(this.btnLuuTaiKhoan);
            this.grpThaoTac.Controls.Add(this.btnKhoaMoKhoa);
            this.grpThaoTac.Controls.Add(this.btnLamMoi);
            this.grpThaoTac.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpThaoTac.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(64)))), ((int)(((byte)(96)))));
            this.grpThaoTac.Location = new System.Drawing.Point(12, 420);
            this.grpThaoTac.Name = "grpThaoTac";
            this.grpThaoTac.Size = new System.Drawing.Size(868, 62);
            this.grpThaoTac.TabIndex = 7;
            this.grpThaoTac.TabStop = false;
            this.grpThaoTac.Text = "Thao tác tài khoản";
            // 
            // btnThemMoi
            // 
            this.btnThemMoi.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThemMoi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.btnThemMoi.Location = new System.Drawing.Point(16, 23);
            this.btnThemMoi.Name = "btnThemMoi";
            this.btnThemMoi.Size = new System.Drawing.Size(190, 28);
            this.btnThemMoi.TabIndex = 0;
            this.btnThemMoi.Text = "Thêm mới";
            this.btnThemMoi.UseVisualStyleBackColor = true;
            // 
            // btnLuuTaiKhoan
            // 
            this.btnLuuTaiKhoan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(86)))), ((int)(((byte)(133)))));
            this.btnLuuTaiKhoan.FlatAppearance.BorderSize = 0;
            this.btnLuuTaiKhoan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLuuTaiKhoan.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLuuTaiKhoan.ForeColor = System.Drawing.Color.White;
            this.btnLuuTaiKhoan.Location = new System.Drawing.Point(222, 23);
            this.btnLuuTaiKhoan.Name = "btnLuuTaiKhoan";
            this.btnLuuTaiKhoan.Size = new System.Drawing.Size(204, 28);
            this.btnLuuTaiKhoan.TabIndex = 1;
            this.btnLuuTaiKhoan.Text = "Lưu tài khoản";
            this.btnLuuTaiKhoan.UseVisualStyleBackColor = false;
            // 
            // btnKhoaMoKhoa
            // 
            this.btnKhoaMoKhoa.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKhoaMoKhoa.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.btnKhoaMoKhoa.Location = new System.Drawing.Point(442, 23);
            this.btnKhoaMoKhoa.Name = "btnKhoaMoKhoa";
            this.btnKhoaMoKhoa.Size = new System.Drawing.Size(204, 28);
            this.btnKhoaMoKhoa.TabIndex = 2;
            this.btnKhoaMoKhoa.Text = "Khóa / Mở khóa";
            this.btnKhoaMoKhoa.UseVisualStyleBackColor = true;
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLamMoi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.btnLamMoi.Location = new System.Drawing.Point(662, 23);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(190, 28);
            this.btnLamMoi.TabIndex = 3;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.UseVisualStyleBackColor = true;
            // 
            // btnDong
            // 
            this.btnDong.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDong.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.btnDong.Location = new System.Drawing.Point(800, 615);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(120, 30);
            this.btnDong.TabIndex = 4;
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = true;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // stsTrangThai
            this.stsTrangThai.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.tsslTrangThai, this.tsslVaiTro});
            this.stsTrangThai.Location = new System.Drawing.Point(0, 648);
            this.stsTrangThai.Name = "stsTrangThai";
            this.stsTrangThai.Size = new System.Drawing.Size(940, 22);
            this.stsTrangThai.SizingGrip = false;
            this.stsTrangThai.TabIndex = 4;
            // tsslTrangThai
            this.tsslTrangThai.Name = "tsslTrangThai";
            this.tsslTrangThai.Spring = true;
            this.tsslTrangThai.Text = "Sẵn sàng";
            this.tsslTrangThai.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // tsslVaiTro
            this.tsslVaiTro.Name = "tsslVaiTro";
            this.tsslVaiTro.Text = "Chủ cửa hàng  |  Thu ngân  |  Kho  |  Thợ";
            // frmQuanLyTaiKhoan
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(245, 247, 250);
            this.ClientSize = new System.Drawing.Size(940, 670);
            this.Controls.Add(this.btnDong);
            this.Controls.Add(this.tabChucNang);
            this.Controls.Add(this.lblMoTa);
            this.Controls.Add(this.lblTieuDe);
            this.Controls.Add(this.stsTrangThai);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "frmQuanLyTaiKhoan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Quản lý tài khoản và phân quyền";
            this.tabChucNang.ResumeLayout(false);
            this.tabTaiKhoan.ResumeLayout(false);
            this.tabPhanQuyen.ResumeLayout(false);
            this.grpDanhSachTaiKhoan.ResumeLayout(false);
            this.grpDanhSachTaiKhoan.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTaiKhoan)).EndInit();
            this.grpThongTinTaiKhoan.ResumeLayout(false);
            this.grpThongTinTaiKhoan.PerformLayout();
            this.grpThaoTac.ResumeLayout(false);
            this.grpPhanQuyen.ResumeLayout(false);
            this.stsTrangThai.ResumeLayout(false);
            this.stsTrangThai.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.Label lblMoTa;
        private System.Windows.Forms.TabControl tabChucNang;
        private System.Windows.Forms.TabPage tabTaiKhoan;
        private System.Windows.Forms.TabPage tabPhanQuyen;
        private System.Windows.Forms.GroupBox grpDanhSachTaiKhoan;
        private System.Windows.Forms.Label lblTimKiem;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.Label lblLocVaiTro;
        private System.Windows.Forms.ComboBox cboLocVaiTro;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.DataGridView dgvTaiKhoan;
        private System.Windows.Forms.GroupBox grpThongTinTaiKhoan;
        private System.Windows.Forms.Label lblMaTaiKhoan;
        private System.Windows.Forms.TextBox txtMaTaiKhoan;
        private System.Windows.Forms.Label lblHoTen;
        private System.Windows.Forms.TextBox txtHoTen;
        private System.Windows.Forms.Label lblTenDangNhapTaiKhoan;
        private System.Windows.Forms.TextBox txtTenDangNhapTaiKhoan;
        private System.Windows.Forms.Label lblVaiTro;
        private System.Windows.Forms.ComboBox cboVaiTro;
        private System.Windows.Forms.Label lblMatKhauTaiKhoan;
        private System.Windows.Forms.TextBox txtMatKhauTaiKhoan;
        private System.Windows.Forms.Label lblXacNhanMatKhau;
        private System.Windows.Forms.TextBox txtXacNhanMatKhau;
        private System.Windows.Forms.CheckBox chkTaiKhoanHoatDong;
        private System.Windows.Forms.Label lblGhiChuTaiKhoan;
        private System.Windows.Forms.GroupBox grpPhanQuyen;
        private System.Windows.Forms.Label lblViTri;
        private System.Windows.Forms.ListBox lstVaiTro;
        private System.Windows.Forms.Label lblChucNang;
        private System.Windows.Forms.CheckBox chkQuanLyTaiKhoan;
        private System.Windows.Forms.CheckBox chkTiepNhanSuaChua;
        private System.Windows.Forms.CheckBox chkDanhMucDichVu;
        private System.Windows.Forms.CheckBox chkNhapKhoPhuTung;
        private System.Windows.Forms.CheckBox chkBaoCaoThongKe;
        private System.Windows.Forms.Label lblGhiChuPhanQuyen;
        private System.Windows.Forms.GroupBox grpThaoTac;
        private System.Windows.Forms.Button btnThemMoi;
        private System.Windows.Forms.Button btnLuuTaiKhoan;
        private System.Windows.Forms.Button btnKhoaMoKhoa;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Button btnDong;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaTaiKhoan;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTenDangNhap;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHoTen;
        private System.Windows.Forms.DataGridViewTextBoxColumn colVaiTro;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTrangThai;
        private System.Windows.Forms.StatusStrip stsTrangThai;
        private System.Windows.Forms.ToolStripStatusLabel tsslTrangThai;
        private System.Windows.Forms.ToolStripStatusLabel tsslVaiTro;
    }
}
