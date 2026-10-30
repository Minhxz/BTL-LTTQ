namespace QLyCuaHangSuaXe.DanhMuc
{
 partial class frmNhanVien
 {
 private System.ComponentModel.IContainer thanhPhan = null;
 protected override void Dispose(bool dangHuy) { if (dangHuy && thanhPhan != null) thanhPhan.Dispose(); base.Dispose(dangHuy); }
 private void InitializeComponent()
 {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tlpTrang = new System.Windows.Forms.TableLayoutPanel();
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.grpThongTin = new System.Windows.Forms.GroupBox();
            this.tlpNhap = new System.Windows.Forms.TableLayoutPanel();
            this.lbltxtMaNhanVien = new System.Windows.Forms.Label();
            this.txtMaNhanVien = new System.Windows.Forms.TextBox();
            this.lbltxtHoTen = new System.Windows.Forms.Label();
            this.txtHoTen = new System.Windows.Forms.TextBox();
            this.lbltxtDienThoai = new System.Windows.Forms.Label();
            this.txtDienThoai = new System.Windows.Forms.TextBox();
            this.lblcboChucVu = new System.Windows.Forms.Label();
            this.cboChucVu = new System.Windows.Forms.ComboBox();
            this.lblnumGioiHanXe = new System.Windows.Forms.Label();
            this.numGioiHanXe = new System.Windows.Forms.NumericUpDown();
            this.lblnumLuongGio = new System.Windows.Forms.Label();
            this.numLuongGio = new System.Windows.Forms.NumericUpDown();
            this.flpThaoTac = new System.Windows.Forms.FlowLayoutPanel();
            this.btnThemMoi = new System.Windows.Forms.Button();
            this.btnLuu = new System.Windows.Forms.Button();
            this.btnNghiViec = new System.Windows.Forms.Button();
            this.btnPhanCong = new System.Windows.Forms.Button();
            this.btnBangCong = new System.Windows.Forms.Button();
            this.btnTaiLai = new System.Windows.Forms.Button();
            this.flpTimKiem = new System.Windows.Forms.FlowLayoutPanel();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.dgvNhanVien = new System.Windows.Forms.DataGridView();
            this.tabNhanSu = new System.Windows.Forms.TabControl();
            this.tabNhanVien = new System.Windows.Forms.TabPage();
            this.tabBangCong = new System.Windows.Forms.TabPage();
            this.pnlBangCong = new System.Windows.Forms.Panel();
            this.tlpTrang.SuspendLayout();
            this.grpThongTin.SuspendLayout();
            this.tlpNhap.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numGioiHanXe)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLuongGio)).BeginInit();
            this.flpThaoTac.SuspendLayout();
            this.flpTimKiem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNhanVien)).BeginInit();
            this.tabNhanSu.SuspendLayout();
            this.tabNhanVien.SuspendLayout();
            this.tabBangCong.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpTrang
            // 
            this.tlpTrang.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.tlpTrang.ColumnCount = 1;
            this.tlpTrang.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTrang.Controls.Add(this.lblTieuDe, 0, 0);
            this.tlpTrang.Controls.Add(this.grpThongTin, 0, 1);
            this.tlpTrang.Controls.Add(this.flpThaoTac, 0, 2);
            this.tlpTrang.Controls.Add(this.flpTimKiem, 0, 3);
            this.tlpTrang.Controls.Add(this.dgvNhanVien, 0, 4);
            this.tlpTrang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpTrang.Location = new System.Drawing.Point(0, 0);
            this.tlpTrang.Name = "tlpTrang";
            this.tlpTrang.Padding = new System.Windows.Forms.Padding(20, 12, 20, 12);
            this.tlpTrang.RowCount = 5;
            this.tlpTrang.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlpTrang.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 212F));
            this.tlpTrang.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.tlpTrang.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlpTrang.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTrang.Size = new System.Drawing.Size(1172, 732);
            this.tlpTrang.TabIndex = 0;
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTieuDe.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTieuDe.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(108)))), ((int)(((byte)(176)))));
            this.lblTieuDe.Location = new System.Drawing.Point(24, 16);
            this.lblTieuDe.Margin = new System.Windows.Forms.Padding(4, 4, 8, 4);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(1120, 42);
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "QUẢN LÝ NHÂN VIÊN";
            this.lblTieuDe.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // grpThongTin
            // 
            this.grpThongTin.Controls.Add(this.tlpNhap);
            this.grpThongTin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpThongTin.Location = new System.Drawing.Point(20, 66);
            this.grpThongTin.Margin = new System.Windows.Forms.Padding(0, 4, 0, 8);
            this.grpThongTin.Name = "grpThongTin";
            this.grpThongTin.Padding = new System.Windows.Forms.Padding(8);
            this.grpThongTin.Size = new System.Drawing.Size(1132, 200);
            this.grpThongTin.TabIndex = 1;
            this.grpThongTin.TabStop = false;
            this.grpThongTin.Text = "Thông tin chi tiết";
            // 
            // tlpNhap
            // 
            this.tlpNhap.ColumnCount = 4;
            this.tlpNhap.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            this.tlpNhap.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpNhap.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            this.tlpNhap.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpNhap.Controls.Add(this.lbltxtMaNhanVien, 0, 0);
            this.tlpNhap.Controls.Add(this.txtMaNhanVien, 1, 0);
            this.tlpNhap.Controls.Add(this.lbltxtHoTen, 2, 0);
            this.tlpNhap.Controls.Add(this.txtHoTen, 3, 0);
            this.tlpNhap.Controls.Add(this.lbltxtDienThoai, 0, 1);
            this.tlpNhap.Controls.Add(this.txtDienThoai, 1, 1);
            this.tlpNhap.Controls.Add(this.lblcboChucVu, 2, 1);
            this.tlpNhap.Controls.Add(this.cboChucVu, 3, 1);
            this.tlpNhap.Controls.Add(this.lblnumGioiHanXe, 2, 2);
            this.tlpNhap.Controls.Add(this.numGioiHanXe, 3, 2);
            this.tlpNhap.Controls.Add(this.lblnumLuongGio, 0, 2);
            this.tlpNhap.Controls.Add(this.numLuongGio, 1, 2);
            this.tlpNhap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpNhap.Location = new System.Drawing.Point(8, 31);
            this.tlpNhap.Margin = new System.Windows.Forms.Padding(0);
            this.tlpNhap.Name = "tlpNhap";
            this.tlpNhap.Padding = new System.Windows.Forms.Padding(8);
            this.tlpNhap.RowCount = 3;
            this.tlpNhap.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tlpNhap.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tlpNhap.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tlpNhap.Size = new System.Drawing.Size(1116, 161);
            this.tlpNhap.TabIndex = 0;
            // 
            // lbltxtMaNhanVien
            // 
            this.lbltxtMaNhanVien.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbltxtMaNhanVien.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(82)))), ((int)(((byte)(99)))));
            this.lbltxtMaNhanVien.Location = new System.Drawing.Point(12, 12);
            this.lbltxtMaNhanVien.Margin = new System.Windows.Forms.Padding(4, 4, 8, 4);
            this.lbltxtMaNhanVien.Name = "lbltxtMaNhanVien";
            this.lbltxtMaNhanVien.Size = new System.Drawing.Size(128, 38);
            this.lbltxtMaNhanVien.TabIndex = 0;
            this.lbltxtMaNhanVien.Text = "Mã nhân viên";
            this.lbltxtMaNhanVien.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtMaNhanVien
            // 
            this.txtMaNhanVien.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMaNhanVien.Location = new System.Drawing.Point(152, 15);
            this.txtMaNhanVien.Margin = new System.Windows.Forms.Padding(4, 7, 16, 7);
            this.txtMaNhanVien.Name = "txtMaNhanVien";
            this.txtMaNhanVien.ReadOnly = true;
            this.txtMaNhanVien.Size = new System.Drawing.Size(390, 30);
            this.txtMaNhanVien.TabIndex = 1;
            // 
            // lbltxtHoTen
            // 
            this.lbltxtHoTen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbltxtHoTen.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(82)))), ((int)(((byte)(99)))));
            this.lbltxtHoTen.Location = new System.Drawing.Point(562, 12);
            this.lbltxtHoTen.Margin = new System.Windows.Forms.Padding(4, 4, 8, 4);
            this.lbltxtHoTen.Name = "lbltxtHoTen";
            this.lbltxtHoTen.Size = new System.Drawing.Size(128, 38);
            this.lbltxtHoTen.TabIndex = 2;
            this.lbltxtHoTen.Text = "Họ và tên";
            this.lbltxtHoTen.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtHoTen
            // 
            this.txtHoTen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtHoTen.Location = new System.Drawing.Point(702, 15);
            this.txtHoTen.Margin = new System.Windows.Forms.Padding(4, 7, 16, 7);
            this.txtHoTen.MaxLength = 100;
            this.txtHoTen.Name = "txtHoTen";
            this.txtHoTen.Size = new System.Drawing.Size(390, 30);
            this.txtHoTen.TabIndex = 3;
            // 
            // lbltxtDienThoai
            // 
            this.lbltxtDienThoai.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbltxtDienThoai.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(82)))), ((int)(((byte)(99)))));
            this.lbltxtDienThoai.Location = new System.Drawing.Point(12, 58);
            this.lbltxtDienThoai.Margin = new System.Windows.Forms.Padding(4, 4, 8, 4);
            this.lbltxtDienThoai.Name = "lbltxtDienThoai";
            this.lbltxtDienThoai.Size = new System.Drawing.Size(128, 38);
            this.lbltxtDienThoai.TabIndex = 4;
            this.lbltxtDienThoai.Text = "Điện thoại";
            this.lbltxtDienThoai.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtDienThoai
            // 
            this.txtDienThoai.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDienThoai.Location = new System.Drawing.Point(152, 61);
            this.txtDienThoai.Margin = new System.Windows.Forms.Padding(4, 7, 16, 7);
            this.txtDienThoai.MaxLength = 10;
            this.txtDienThoai.Name = "txtDienThoai";
            this.txtDienThoai.Size = new System.Drawing.Size(390, 30);
            this.txtDienThoai.TabIndex = 5;
            // 
            // lblcboChucVu
            // 
            this.lblcboChucVu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblcboChucVu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(82)))), ((int)(((byte)(99)))));
            this.lblcboChucVu.Location = new System.Drawing.Point(562, 58);
            this.lblcboChucVu.Margin = new System.Windows.Forms.Padding(4, 4, 8, 4);
            this.lblcboChucVu.Name = "lblcboChucVu";
            this.lblcboChucVu.Size = new System.Drawing.Size(128, 38);
            this.lblcboChucVu.TabIndex = 6;
            this.lblcboChucVu.Text = "Chức vụ";
            this.lblcboChucVu.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboChucVu
            // 
            this.cboChucVu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboChucVu.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboChucVu.Location = new System.Drawing.Point(702, 61);
            this.cboChucVu.Margin = new System.Windows.Forms.Padding(4, 7, 16, 7);
            this.cboChucVu.Name = "cboChucVu";
            this.cboChucVu.Size = new System.Drawing.Size(390, 31);
            this.cboChucVu.TabIndex = 7;
            // 
            // lblnumGioiHanXe
            // 
            this.lblnumGioiHanXe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblnumGioiHanXe.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(82)))), ((int)(((byte)(99)))));
            this.lblnumGioiHanXe.Location = new System.Drawing.Point(562, 104);
            this.lblnumGioiHanXe.Margin = new System.Windows.Forms.Padding(4, 4, 8, 4);
            this.lblnumGioiHanXe.Name = "lblnumGioiHanXe";
            this.lblnumGioiHanXe.Size = new System.Drawing.Size(128, 45);
            this.lblnumGioiHanXe.TabIndex = 10;
            this.lblnumGioiHanXe.Text = "Tối đa xe nhận";
            this.lblnumGioiHanXe.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // numGioiHanXe
            // 
            this.numGioiHanXe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numGioiHanXe.Location = new System.Drawing.Point(702, 107);
            this.numGioiHanXe.Margin = new System.Windows.Forms.Padding(4, 7, 16, 7);
            this.numGioiHanXe.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numGioiHanXe.Name = "numGioiHanXe";
            this.numGioiHanXe.Size = new System.Drawing.Size(390, 30);
            this.numGioiHanXe.TabIndex = 11;
            // 
            // lblnumLuongGio
            // 
            this.lblnumLuongGio.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblnumLuongGio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(82)))), ((int)(((byte)(99)))));
            this.lblnumLuongGio.Location = new System.Drawing.Point(12, 104);
            this.lblnumLuongGio.Margin = new System.Windows.Forms.Padding(4, 4, 8, 4);
            this.lblnumLuongGio.Name = "lblnumLuongGio";
            this.lblnumLuongGio.Size = new System.Drawing.Size(128, 45);
            this.lblnumLuongGio.TabIndex = 12;
            this.lblnumLuongGio.Text = "Lương tháng (đ)";
            this.lblnumLuongGio.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // numLuongGio
            // 
            this.numLuongGio.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numLuongGio.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numLuongGio.Location = new System.Drawing.Point(152, 107);
            this.numLuongGio.Margin = new System.Windows.Forms.Padding(4, 7, 16, 7);
            this.numLuongGio.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.numLuongGio.Name = "numLuongGio";
            this.numLuongGio.Size = new System.Drawing.Size(390, 30);
            this.numLuongGio.TabIndex = 13;
            this.numLuongGio.ThousandsSeparator = true;
            // 
            // flpThaoTac
            // 
            this.flpThaoTac.AutoScroll = true;
            this.flpThaoTac.Controls.Add(this.btnThemMoi);
            this.flpThaoTac.Controls.Add(this.btnLuu);
            this.flpThaoTac.Controls.Add(this.btnNghiViec);
            this.flpThaoTac.Controls.Add(this.btnPhanCong);
            this.flpThaoTac.Controls.Add(this.btnBangCong);
            this.flpThaoTac.Controls.Add(this.btnTaiLai);
            this.flpThaoTac.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpThaoTac.Location = new System.Drawing.Point(20, 274);
            this.flpThaoTac.Margin = new System.Windows.Forms.Padding(0);
            this.flpThaoTac.Name = "flpThaoTac";
            this.flpThaoTac.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.flpThaoTac.Size = new System.Drawing.Size(1132, 52);
            this.flpThaoTac.TabIndex = 2;
            this.flpThaoTac.WrapContents = false;
            // 
            // btnThemMoi
            // 
            this.btnThemMoi.AutoSize = true;
            this.btnThemMoi.BackColor = System.Drawing.Color.White;
            this.btnThemMoi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnThemMoi.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(207)))), ((int)(((byte)(220)))));
            this.btnThemMoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThemMoi.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnThemMoi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(85)))), ((int)(((byte)(130)))));
            this.btnThemMoi.Location = new System.Drawing.Point(0, 8);
            this.btnThemMoi.Margin = new System.Windows.Forms.Padding(0, 4, 10, 4);
            this.btnThemMoi.MinimumSize = new System.Drawing.Size(108, 36);
            this.btnThemMoi.Name = "btnThemMoi";
            this.btnThemMoi.Padding = new System.Windows.Forms.Padding(12, 2, 12, 2);
            this.btnThemMoi.Size = new System.Drawing.Size(169, 37);
            this.btnThemMoi.TabIndex = 0;
            this.btnThemMoi.Text = "Thêm nhân viên";
            this.btnThemMoi.UseVisualStyleBackColor = false;
            // 
            // btnLuu
            // 
            this.btnLuu.AutoSize = true;
            this.btnLuu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(108)))), ((int)(((byte)(176)))));
            this.btnLuu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLuu.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(108)))), ((int)(((byte)(176)))));
            this.btnLuu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLuu.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnLuu.ForeColor = System.Drawing.Color.White;
            this.btnLuu.Location = new System.Drawing.Point(179, 8);
            this.btnLuu.Margin = new System.Windows.Forms.Padding(0, 4, 10, 4);
            this.btnLuu.MinimumSize = new System.Drawing.Size(108, 36);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Padding = new System.Windows.Forms.Padding(12, 2, 12, 2);
            this.btnLuu.Size = new System.Drawing.Size(155, 37);
            this.btnLuu.TabIndex = 1;
            this.btnLuu.Text = "Lưu nhân viên";
            this.btnLuu.UseVisualStyleBackColor = false;
            // 
            // btnNghiViec
            // 
            this.btnNghiViec.BackColor = System.Drawing.Color.White;
            this.btnNghiViec.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNghiViec.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(207)))), ((int)(((byte)(220)))));
            this.btnNghiViec.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNghiViec.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnNghiViec.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(85)))), ((int)(((byte)(130)))));
            this.btnNghiViec.Location = new System.Drawing.Point(344, 8);
            this.btnNghiViec.Margin = new System.Windows.Forms.Padding(0, 4, 10, 4);
            this.btnNghiViec.Name = "btnNghiViec";
            this.btnNghiViec.Size = new System.Drawing.Size(130, 37);
            this.btnNghiViec.TabIndex = 2;
            this.btnNghiViec.Text = "Nghỉ việc";
            this.btnNghiViec.UseVisualStyleBackColor = false;
            // 
            // btnPhanCong
            // 
            this.btnPhanCong.AutoSize = true;
            this.btnPhanCong.BackColor = System.Drawing.Color.White;
            this.btnPhanCong.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPhanCong.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(207)))), ((int)(((byte)(220)))));
            this.btnPhanCong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPhanCong.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnPhanCong.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(85)))), ((int)(((byte)(130)))));
            this.btnPhanCong.Location = new System.Drawing.Point(484, 8);
            this.btnPhanCong.Margin = new System.Windows.Forms.Padding(0, 4, 10, 4);
            this.btnPhanCong.MinimumSize = new System.Drawing.Size(108, 36);
            this.btnPhanCong.Name = "btnPhanCong";
            this.btnPhanCong.Padding = new System.Windows.Forms.Padding(12, 2, 12, 2);
            this.btnPhanCong.Size = new System.Drawing.Size(127, 37);
            this.btnPhanCong.TabIndex = 2;
            this.btnPhanCong.Text = "Phân công";
            this.btnPhanCong.UseVisualStyleBackColor = false;
            // 
            // btnBangCong
            // 
            this.btnBangCong.AutoSize = true;
            this.btnBangCong.BackColor = System.Drawing.Color.White;
            this.btnBangCong.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBangCong.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(207)))), ((int)(((byte)(220)))));
            this.btnBangCong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBangCong.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnBangCong.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(85)))), ((int)(((byte)(130)))));
            this.btnBangCong.Location = new System.Drawing.Point(621, 8);
            this.btnBangCong.Margin = new System.Windows.Forms.Padding(0, 4, 10, 4);
            this.btnBangCong.MinimumSize = new System.Drawing.Size(108, 36);
            this.btnBangCong.Name = "btnBangCong";
            this.btnBangCong.Padding = new System.Windows.Forms.Padding(12, 2, 12, 2);
            this.btnBangCong.Size = new System.Drawing.Size(127, 37);
            this.btnBangCong.TabIndex = 3;
            this.btnBangCong.Text = "Bảng công";
            this.btnBangCong.UseVisualStyleBackColor = false;
            // 
            // btnTaiLai
            // 
            this.btnTaiLai.AutoSize = true;
            this.btnTaiLai.BackColor = System.Drawing.Color.White;
            this.btnTaiLai.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTaiLai.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(207)))), ((int)(((byte)(220)))));
            this.btnTaiLai.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTaiLai.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnTaiLai.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(85)))), ((int)(((byte)(130)))));
            this.btnTaiLai.Location = new System.Drawing.Point(758, 8);
            this.btnTaiLai.Margin = new System.Windows.Forms.Padding(0, 4, 10, 4);
            this.btnTaiLai.MinimumSize = new System.Drawing.Size(108, 36);
            this.btnTaiLai.Name = "btnTaiLai";
            this.btnTaiLai.Padding = new System.Windows.Forms.Padding(12, 2, 12, 2);
            this.btnTaiLai.Size = new System.Drawing.Size(120, 37);
            this.btnTaiLai.TabIndex = 4;
            this.btnTaiLai.Text = "Tải lại";
            this.btnTaiLai.UseVisualStyleBackColor = false;
            // 
            // flpTimKiem
            // 
            this.flpTimKiem.AutoScroll = true;
            this.flpTimKiem.Controls.Add(this.txtTimKiem);
            this.flpTimKiem.Controls.Add(this.btnTimKiem);
            this.flpTimKiem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpTimKiem.Location = new System.Drawing.Point(20, 326);
            this.flpTimKiem.Margin = new System.Windows.Forms.Padding(0);
            this.flpTimKiem.Name = "flpTimKiem";
            this.flpTimKiem.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.flpTimKiem.Size = new System.Drawing.Size(1132, 50);
            this.flpTimKiem.TabIndex = 3;
            this.flpTimKiem.WrapContents = false;
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Location = new System.Drawing.Point(0, 12);
            this.txtTimKiem.Margin = new System.Windows.Forms.Padding(0, 8, 12, 4);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new System.Drawing.Size(360, 30);
            this.txtTimKiem.TabIndex = 0;
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.AutoSize = true;
            this.btnTimKiem.BackColor = System.Drawing.Color.White;
            this.btnTimKiem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTimKiem.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(207)))), ((int)(((byte)(220)))));
            this.btnTimKiem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTimKiem.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnTimKiem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(85)))), ((int)(((byte)(130)))));
            this.btnTimKiem.Location = new System.Drawing.Point(372, 8);
            this.btnTimKiem.Margin = new System.Windows.Forms.Padding(0, 4, 10, 4);
            this.btnTimKiem.MinimumSize = new System.Drawing.Size(108, 36);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Padding = new System.Windows.Forms.Padding(12, 2, 12, 2);
            this.btnTimKiem.Size = new System.Drawing.Size(120, 37);
            this.btnTimKiem.TabIndex = 1;
            this.btnTimKiem.Text = "Tìm kiếm";
            this.btnTimKiem.UseVisualStyleBackColor = false;
            // 
            // dgvNhanVien
            // 
            this.dgvNhanVien.AllowUserToAddRows = false;
            this.dgvNhanVien.AllowUserToDeleteRows = false;
            this.dgvNhanVien.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvNhanVien.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(251)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(58)))), ((int)(((byte)(74)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvNhanVien.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvNhanVien.ColumnHeadersHeight = 40;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(234)))), ((int)(((byte)(249)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(42)))), ((int)(((byte)(56)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvNhanVien.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvNhanVien.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvNhanVien.EnableHeadersVisualStyles = false;
            this.dgvNhanVien.Location = new System.Drawing.Point(20, 382);
            this.dgvNhanVien.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.dgvNhanVien.MultiSelect = false;
            this.dgvNhanVien.Name = "dgvNhanVien";
            this.dgvNhanVien.ReadOnly = true;
            this.dgvNhanVien.RowHeadersVisible = false;
            this.dgvNhanVien.RowHeadersWidth = 51;
            this.dgvNhanVien.RowTemplate.Height = 34;
            this.dgvNhanVien.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvNhanVien.Size = new System.Drawing.Size(1132, 338);
            this.dgvNhanVien.TabIndex = 5;
            // 
            // tabNhanSu
            // 
            this.tabNhanSu.Controls.Add(this.tabNhanVien);
            this.tabNhanSu.Controls.Add(this.tabBangCong);
            this.tabNhanSu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabNhanSu.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tabNhanSu.ItemSize = new System.Drawing.Size(210, 40);
            this.tabNhanSu.Location = new System.Drawing.Point(0, 0);
            this.tabNhanSu.Name = "tabNhanSu";
            this.tabNhanSu.SelectedIndex = 0;
            this.tabNhanSu.Size = new System.Drawing.Size(1180, 780);
            this.tabNhanSu.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabNhanSu.TabIndex = 0;
            // 
            // tabNhanVien
            // 
            this.tabNhanVien.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(247)))), ((int)(((byte)(251)))));
            this.tabNhanVien.Controls.Add(this.tlpTrang);
            this.tabNhanVien.Location = new System.Drawing.Point(4, 44);
            this.tabNhanVien.Name = "tabNhanVien";
            this.tabNhanVien.Size = new System.Drawing.Size(1172, 732);
            this.tabNhanVien.TabIndex = 0;
            this.tabNhanVien.Text = "Nhân viên";
            // 
            // tabBangCong
            // 
            this.tabBangCong.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(247)))), ((int)(((byte)(251)))));
            this.tabBangCong.Controls.Add(this.pnlBangCong);
            this.tabBangCong.Location = new System.Drawing.Point(4, 44);
            this.tabBangCong.Name = "tabBangCong";
            this.tabBangCong.Size = new System.Drawing.Size(1172, 732);
            this.tabBangCong.TabIndex = 1;
            this.tabBangCong.Text = "Bảng công và lương";
            // 
            // pnlBangCong
            // 
            this.pnlBangCong.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(247)))), ((int)(((byte)(251)))));
            this.pnlBangCong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBangCong.Location = new System.Drawing.Point(0, 0);
            this.pnlBangCong.Name = "pnlBangCong";
            this.pnlBangCong.Size = new System.Drawing.Size(1172, 732);
            this.pnlBangCong.TabIndex = 0;
            // 
            // frmNhanVien
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.ClientSize = new System.Drawing.Size(1180, 780);
            this.Controls.Add(this.tabNhanSu);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.MinimumSize = new System.Drawing.Size(960, 650);
            this.Name = "frmNhanVien";
            this.Text = "Quản lý nhân viên";
            this.tlpTrang.ResumeLayout(false);
            this.grpThongTin.ResumeLayout(false);
            this.tlpNhap.ResumeLayout(false);
            this.tlpNhap.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numGioiHanXe)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLuongGio)).EndInit();
            this.flpThaoTac.ResumeLayout(false);
            this.flpThaoTac.PerformLayout();
            this.flpTimKiem.ResumeLayout(false);
            this.flpTimKiem.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNhanVien)).EndInit();
            this.tabNhanSu.ResumeLayout(false);
            this.tabNhanVien.ResumeLayout(false);
            this.tabBangCong.ResumeLayout(false);
            this.ResumeLayout(false);

 }
 private System.Windows.Forms.TableLayoutPanel tlpTrang;
 private System.Windows.Forms.Label lblTieuDe;
 private System.Windows.Forms.GroupBox grpThongTin;
 private System.Windows.Forms.TableLayoutPanel tlpNhap;
 private System.Windows.Forms.FlowLayoutPanel flpThaoTac;
 private System.Windows.Forms.FlowLayoutPanel flpTimKiem;
 private System.Windows.Forms.Label lbltxtMaNhanVien;
 private System.Windows.Forms.TextBox txtMaNhanVien;
 private System.Windows.Forms.Label lbltxtHoTen;
 private System.Windows.Forms.TextBox txtHoTen;
 private System.Windows.Forms.Label lbltxtDienThoai;
 private System.Windows.Forms.TextBox txtDienThoai;
 private System.Windows.Forms.Label lblcboChucVu;
 private System.Windows.Forms.ComboBox cboChucVu;
 private System.Windows.Forms.Label lblnumGioiHanXe;
 private System.Windows.Forms.NumericUpDown numGioiHanXe;
 private System.Windows.Forms.Label lblnumLuongGio;
 private System.Windows.Forms.NumericUpDown numLuongGio;
 private System.Windows.Forms.Button btnThemMoi;
 private System.Windows.Forms.Button btnLuu;
 private System.Windows.Forms.Button btnPhanCong;
 private System.Windows.Forms.Button btnBangCong;
 private System.Windows.Forms.Button btnTaiLai;
 private System.Windows.Forms.Button btnNghiViec;
 private System.Windows.Forms.TabControl tabNhanSu;
 private System.Windows.Forms.TabPage tabNhanVien;
 private System.Windows.Forms.TabPage tabBangCong;
 private System.Windows.Forms.Panel pnlBangCong;
 private System.Windows.Forms.TextBox txtTimKiem;
 private System.Windows.Forms.Button btnTimKiem;
 private System.Windows.Forms.DataGridView dgvNhanVien;
 }
}
