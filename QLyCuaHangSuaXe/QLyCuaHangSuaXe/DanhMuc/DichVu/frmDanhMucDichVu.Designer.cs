namespace QLyCuaHangSuaXe.DanhMuc.DichVu
{
    partial class frmDanhMucDichVu
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tlpTrang = new System.Windows.Forms.TableLayoutPanel();
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.grpThongTin = new System.Windows.Forms.GroupBox();
            this.tlpNhap = new System.Windows.Forms.TableLayoutPanel();
            this.lbltxtMaDV = new System.Windows.Forms.Label();
            this.txtMaDV = new System.Windows.Forms.TextBox();
            this.lbltxtTenDichVu = new System.Windows.Forms.Label();
            this.txtTenDichVu = new System.Windows.Forms.TextBox();
            this.lblnumGiaDichVu = new System.Windows.Forms.Label();
            this.numGiaDichVu = new System.Windows.Forms.NumericUpDown();
            this.lblnumLuongNhiemVu = new System.Windows.Forms.Label();
            this.numLuongNhiemVu = new System.Windows.Forms.NumericUpDown();
            this.lblnumPhutDuKien = new System.Windows.Forms.Label();
            this.numPhutDuKien = new System.Windows.Forms.NumericUpDown();
            this.lblcboTrangThai = new System.Windows.Forms.Label();
            this.cboTrangThai = new System.Windows.Forms.ComboBox();
            this.lbltxtMoTa = new System.Windows.Forms.Label();
            this.txtMoTa = new System.Windows.Forms.TextBox();
            this.flpThaoTac = new System.Windows.Forms.FlowLayoutPanel();
            this.btnThem = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnDinhMuc = new System.Windows.Forms.Button();
            this.btnKho = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.btnDong = new System.Windows.Forms.Button();
            this.flpTimKiem = new System.Windows.Forms.FlowLayoutPanel();
            this.lblTimKiem = new System.Windows.Forms.Label();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.lblHuongDan = new System.Windows.Forms.Label();
            this.dgvDichVu = new System.Windows.Forms.DataGridView();
            this.tlpTrang.SuspendLayout();
            this.grpThongTin.SuspendLayout();
            this.tlpNhap.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numGiaDichVu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLuongNhiemVu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPhutDuKien)).BeginInit();
            this.flpThaoTac.SuspendLayout();
            this.flpTimKiem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDichVu)).BeginInit();
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
            this.tlpTrang.Controls.Add(this.lblHuongDan, 0, 5);
            this.tlpTrang.Controls.Add(this.dgvDichVu, 0, 4);
            this.tlpTrang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpTrang.Location = new System.Drawing.Point(0, 0);
            this.tlpTrang.Name = "tlpTrang";
            this.tlpTrang.Padding = new System.Windows.Forms.Padding(20, 12, 20, 12);
            this.tlpTrang.RowCount = 6;
            this.tlpTrang.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlpTrang.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 244F));
            this.tlpTrang.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.tlpTrang.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlpTrang.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTrang.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tlpTrang.Size = new System.Drawing.Size(1180, 780);
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
            this.lblTieuDe.Size = new System.Drawing.Size(1128, 42);
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "DANH MỤC DỊCH VỤ";
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
            this.grpThongTin.Size = new System.Drawing.Size(1140, 232);
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
            this.tlpNhap.Controls.Add(this.lbltxtMaDV, 0, 0);
            this.tlpNhap.Controls.Add(this.txtMaDV, 1, 0);
            this.tlpNhap.Controls.Add(this.lbltxtTenDichVu, 2, 0);
            this.tlpNhap.Controls.Add(this.txtTenDichVu, 3, 0);
            this.tlpNhap.Controls.Add(this.lblnumGiaDichVu, 0, 1);
            this.tlpNhap.Controls.Add(this.numGiaDichVu, 1, 1);
            this.tlpNhap.Controls.Add(this.lblnumLuongNhiemVu, 2, 1);
            this.tlpNhap.Controls.Add(this.numLuongNhiemVu, 3, 1);
            this.tlpNhap.Controls.Add(this.lblnumPhutDuKien, 0, 2);
            this.tlpNhap.Controls.Add(this.numPhutDuKien, 1, 2);
            this.tlpNhap.Controls.Add(this.lblcboTrangThai, 2, 2);
            this.tlpNhap.Controls.Add(this.cboTrangThai, 3, 2);
            this.tlpNhap.Controls.Add(this.lbltxtMoTa, 0, 3);
            this.tlpNhap.Controls.Add(this.txtMoTa, 1, 3);
            this.tlpNhap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpNhap.Location = new System.Drawing.Point(8, 31);
            this.tlpNhap.Margin = new System.Windows.Forms.Padding(0);
            this.tlpNhap.Name = "tlpNhap";
            this.tlpNhap.Padding = new System.Windows.Forms.Padding(8);
            this.tlpNhap.RowCount = 4;
            this.tlpNhap.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tlpNhap.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tlpNhap.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tlpNhap.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tlpNhap.Size = new System.Drawing.Size(1124, 193);
            this.tlpNhap.TabIndex = 0;
            // 
            // lbltxtMaDV
            // 
            this.lbltxtMaDV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbltxtMaDV.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(82)))), ((int)(((byte)(99)))));
            this.lbltxtMaDV.Location = new System.Drawing.Point(12, 12);
            this.lbltxtMaDV.Margin = new System.Windows.Forms.Padding(4, 4, 8, 4);
            this.lbltxtMaDV.Name = "lbltxtMaDV";
            this.lbltxtMaDV.Size = new System.Drawing.Size(128, 38);
            this.lbltxtMaDV.TabIndex = 0;
            this.lbltxtMaDV.Text = "Mã dịch vụ";
            this.lbltxtMaDV.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtMaDV
            // 
            this.txtMaDV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMaDV.Location = new System.Drawing.Point(152, 15);
            this.txtMaDV.Margin = new System.Windows.Forms.Padding(4, 7, 16, 7);
            this.txtMaDV.Name = "txtMaDV";
            this.txtMaDV.ReadOnly = true;
            this.txtMaDV.Size = new System.Drawing.Size(394, 30);
            this.txtMaDV.TabIndex = 1;
            // 
            // lbltxtTenDichVu
            // 
            this.lbltxtTenDichVu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbltxtTenDichVu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(82)))), ((int)(((byte)(99)))));
            this.lbltxtTenDichVu.Location = new System.Drawing.Point(566, 12);
            this.lbltxtTenDichVu.Margin = new System.Windows.Forms.Padding(4, 4, 8, 4);
            this.lbltxtTenDichVu.Name = "lbltxtTenDichVu";
            this.lbltxtTenDichVu.Size = new System.Drawing.Size(128, 38);
            this.lbltxtTenDichVu.TabIndex = 2;
            this.lbltxtTenDichVu.Text = "Tên dịch vụ *";
            this.lbltxtTenDichVu.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtTenDichVu
            // 
            this.txtTenDichVu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTenDichVu.Location = new System.Drawing.Point(706, 15);
            this.txtTenDichVu.Margin = new System.Windows.Forms.Padding(4, 7, 16, 7);
            this.txtTenDichVu.Name = "txtTenDichVu";
            this.txtTenDichVu.Size = new System.Drawing.Size(394, 30);
            this.txtTenDichVu.TabIndex = 3;
            // 
            // lblnumGiaDichVu
            // 
            this.lblnumGiaDichVu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblnumGiaDichVu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(82)))), ((int)(((byte)(99)))));
            this.lblnumGiaDichVu.Location = new System.Drawing.Point(12, 58);
            this.lblnumGiaDichVu.Margin = new System.Windows.Forms.Padding(4, 4, 8, 4);
            this.lblnumGiaDichVu.Name = "lblnumGiaDichVu";
            this.lblnumGiaDichVu.Size = new System.Drawing.Size(128, 38);
            this.lblnumGiaDichVu.TabIndex = 4;
            this.lblnumGiaDichVu.Text = "Đơn giá (đ)";
            this.lblnumGiaDichVu.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // numGiaDichVu
            // 
            this.numGiaDichVu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numGiaDichVu.Location = new System.Drawing.Point(152, 61);
            this.numGiaDichVu.Margin = new System.Windows.Forms.Padding(4, 7, 16, 7);
            this.numGiaDichVu.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.numGiaDichVu.Name = "numGiaDichVu";
            this.numGiaDichVu.Size = new System.Drawing.Size(394, 30);
            this.numGiaDichVu.TabIndex = 5;
            this.numGiaDichVu.ThousandsSeparator = true;
            // 
            // lblnumLuongNhiemVu
            // 
            this.lblnumLuongNhiemVu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblnumLuongNhiemVu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(82)))), ((int)(((byte)(99)))));
            this.lblnumLuongNhiemVu.Location = new System.Drawing.Point(566, 58);
            this.lblnumLuongNhiemVu.Margin = new System.Windows.Forms.Padding(4, 4, 8, 4);
            this.lblnumLuongNhiemVu.Name = "lblnumLuongNhiemVu";
            this.lblnumLuongNhiemVu.Size = new System.Drawing.Size(128, 38);
            this.lblnumLuongNhiemVu.TabIndex = 6;
            this.lblnumLuongNhiemVu.Text = "Lương nhiệm vụ (đ)";
            this.lblnumLuongNhiemVu.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // numLuongNhiemVu
            // 
            this.numLuongNhiemVu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numLuongNhiemVu.Location = new System.Drawing.Point(706, 61);
            this.numLuongNhiemVu.Margin = new System.Windows.Forms.Padding(4, 7, 16, 7);
            this.numLuongNhiemVu.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.numLuongNhiemVu.Name = "numLuongNhiemVu";
            this.numLuongNhiemVu.Size = new System.Drawing.Size(394, 30);
            this.numLuongNhiemVu.TabIndex = 7;
            this.numLuongNhiemVu.ThousandsSeparator = true;
            // 
            // lblnumPhutDuKien
            // 
            this.lblnumPhutDuKien.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblnumPhutDuKien.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(82)))), ((int)(((byte)(99)))));
            this.lblnumPhutDuKien.Location = new System.Drawing.Point(12, 104);
            this.lblnumPhutDuKien.Margin = new System.Windows.Forms.Padding(4, 4, 8, 4);
            this.lblnumPhutDuKien.Name = "lblnumPhutDuKien";
            this.lblnumPhutDuKien.Size = new System.Drawing.Size(128, 38);
            this.lblnumPhutDuKien.TabIndex = 8;
            this.lblnumPhutDuKien.Text = "Thời gian (phút)";
            this.lblnumPhutDuKien.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // numPhutDuKien
            // 
            this.numPhutDuKien.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numPhutDuKien.Location = new System.Drawing.Point(152, 107);
            this.numPhutDuKien.Margin = new System.Windows.Forms.Padding(4, 7, 16, 7);
            this.numPhutDuKien.Maximum = new decimal(new int[] {
            1440,
            0,
            0,
            0});
            this.numPhutDuKien.Name = "numPhutDuKien";
            this.numPhutDuKien.Size = new System.Drawing.Size(394, 30);
            this.numPhutDuKien.TabIndex = 9;
            // 
            // lblcboTrangThai
            // 
            this.lblcboTrangThai.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblcboTrangThai.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(82)))), ((int)(((byte)(99)))));
            this.lblcboTrangThai.Location = new System.Drawing.Point(566, 104);
            this.lblcboTrangThai.Margin = new System.Windows.Forms.Padding(4, 4, 8, 4);
            this.lblcboTrangThai.Name = "lblcboTrangThai";
            this.lblcboTrangThai.Size = new System.Drawing.Size(128, 38);
            this.lblcboTrangThai.TabIndex = 10;
            this.lblcboTrangThai.Text = "Trạng thái";
            this.lblcboTrangThai.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboTrangThai
            // 
            this.cboTrangThai.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboTrangThai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTrangThai.Location = new System.Drawing.Point(706, 107);
            this.cboTrangThai.Margin = new System.Windows.Forms.Padding(4, 7, 16, 7);
            this.cboTrangThai.Name = "cboTrangThai";
            this.cboTrangThai.Size = new System.Drawing.Size(394, 31);
            this.cboTrangThai.TabIndex = 11;
            // 
            // lbltxtMoTa
            // 
            this.lbltxtMoTa.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbltxtMoTa.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(82)))), ((int)(((byte)(99)))));
            this.lbltxtMoTa.Location = new System.Drawing.Point(12, 150);
            this.lbltxtMoTa.Margin = new System.Windows.Forms.Padding(4, 4, 8, 4);
            this.lbltxtMoTa.Name = "lbltxtMoTa";
            this.lbltxtMoTa.Size = new System.Drawing.Size(128, 38);
            this.lbltxtMoTa.TabIndex = 12;
            this.lbltxtMoTa.Text = "Mô tả";
            this.lbltxtMoTa.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtMoTa
            // 
            this.txtMoTa.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMoTa.Location = new System.Drawing.Point(152, 153);
            this.txtMoTa.Margin = new System.Windows.Forms.Padding(4, 7, 16, 7);
            this.txtMoTa.Name = "txtMoTa";
            this.txtMoTa.Size = new System.Drawing.Size(394, 30);
            this.txtMoTa.TabIndex = 13;
            // 
            // flpThaoTac
            // 
            this.flpThaoTac.AutoScroll = true;
            this.flpThaoTac.Controls.Add(this.btnThem);
            this.flpThaoTac.Controls.Add(this.btnSua);
            this.flpThaoTac.Controls.Add(this.btnXoa);
            this.flpThaoTac.Controls.Add(this.btnDinhMuc);
            this.flpThaoTac.Controls.Add(this.btnKho);
            this.flpThaoTac.Controls.Add(this.btnLamMoi);
            this.flpThaoTac.Controls.Add(this.btnDong);
            this.flpThaoTac.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpThaoTac.Location = new System.Drawing.Point(20, 306);
            this.flpThaoTac.Margin = new System.Windows.Forms.Padding(0);
            this.flpThaoTac.Name = "flpThaoTac";
            this.flpThaoTac.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.flpThaoTac.Size = new System.Drawing.Size(1140, 52);
            this.flpThaoTac.TabIndex = 2;
            this.flpThaoTac.WrapContents = false;
            // 
            // btnThem
            // 
            this.btnThem.AutoSize = true;
            this.btnThem.BackColor = System.Drawing.Color.White;
            this.btnThem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnThem.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(207)))), ((int)(((byte)(220)))));
            this.btnThem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThem.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnThem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(85)))), ((int)(((byte)(130)))));
            this.btnThem.Location = new System.Drawing.Point(0, 8);
            this.btnThem.Margin = new System.Windows.Forms.Padding(0, 4, 10, 4);
            this.btnThem.MinimumSize = new System.Drawing.Size(108, 36);
            this.btnThem.Name = "btnThem";
            this.btnThem.Padding = new System.Windows.Forms.Padding(12, 2, 12, 2);
            this.btnThem.Size = new System.Drawing.Size(123, 37);
            this.btnThem.TabIndex = 0;
            this.btnThem.Text = "Thêm mới";
            this.btnThem.UseVisualStyleBackColor = false;
            // 
            // btnSua
            // 
            this.btnSua.AutoSize = true;
            this.btnSua.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(108)))), ((int)(((byte)(176)))));
            this.btnSua.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSua.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(108)))), ((int)(((byte)(176)))));
            this.btnSua.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSua.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnSua.ForeColor = System.Drawing.Color.White;
            this.btnSua.Location = new System.Drawing.Point(133, 8);
            this.btnSua.Margin = new System.Windows.Forms.Padding(0, 4, 10, 4);
            this.btnSua.MinimumSize = new System.Drawing.Size(108, 36);
            this.btnSua.Name = "btnSua";
            this.btnSua.Padding = new System.Windows.Forms.Padding(12, 2, 12, 2);
            this.btnSua.Size = new System.Drawing.Size(135, 37);
            this.btnSua.TabIndex = 1;
            this.btnSua.Text = "Lưu dịch vụ";
            this.btnSua.UseVisualStyleBackColor = false;
            // 
            // btnXoa
            // 
            this.btnXoa.AutoSize = true;
            this.btnXoa.BackColor = System.Drawing.Color.White;
            this.btnXoa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXoa.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(172)))), ((int)(((byte)(172)))));
            this.btnXoa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXoa.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnXoa.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(59)))), ((int)(((byte)(59)))));
            this.btnXoa.Location = new System.Drawing.Point(278, 8);
            this.btnXoa.Margin = new System.Windows.Forms.Padding(0, 4, 10, 4);
            this.btnXoa.MinimumSize = new System.Drawing.Size(108, 36);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Padding = new System.Windows.Forms.Padding(12, 2, 12, 2);
            this.btnXoa.Size = new System.Drawing.Size(185, 37);
            this.btnXoa.TabIndex = 2;
            this.btnXoa.Text = "Xóa / ngừng dùng";
            this.btnXoa.UseVisualStyleBackColor = false;
            // 
            // btnDinhMuc
            // 
            this.btnDinhMuc.AutoSize = true;
            this.btnDinhMuc.BackColor = System.Drawing.Color.White;
            this.btnDinhMuc.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDinhMuc.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(207)))), ((int)(((byte)(220)))));
            this.btnDinhMuc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDinhMuc.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnDinhMuc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(85)))), ((int)(((byte)(130)))));
            this.btnDinhMuc.Location = new System.Drawing.Point(473, 8);
            this.btnDinhMuc.Margin = new System.Windows.Forms.Padding(0, 4, 10, 4);
            this.btnDinhMuc.MinimumSize = new System.Drawing.Size(108, 36);
            this.btnDinhMuc.Name = "btnDinhMuc";
            this.btnDinhMuc.Padding = new System.Windows.Forms.Padding(12, 2, 12, 2);
            this.btnDinhMuc.Size = new System.Drawing.Size(140, 37);
            this.btnDinhMuc.TabIndex = 3;
            this.btnDinhMuc.Text = "Bộ phụ tùng";
            this.btnDinhMuc.UseVisualStyleBackColor = false;
            // 
            // btnKho
            // 
            this.btnKho.AutoSize = true;
            this.btnKho.BackColor = System.Drawing.Color.White;
            this.btnKho.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnKho.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(207)))), ((int)(((byte)(220)))));
            this.btnKho.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKho.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnKho.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(85)))), ((int)(((byte)(130)))));
            this.btnKho.Location = new System.Drawing.Point(623, 8);
            this.btnKho.Margin = new System.Windows.Forms.Padding(0, 4, 10, 4);
            this.btnKho.MinimumSize = new System.Drawing.Size(108, 36);
            this.btnKho.Name = "btnKho";
            this.btnKho.Padding = new System.Windows.Forms.Padding(12, 2, 12, 2);
            this.btnKho.Size = new System.Drawing.Size(150, 37);
            this.btnKho.TabIndex = 4;
            this.btnKho.Text = "Kho phụ tùng";
            this.btnKho.UseVisualStyleBackColor = false;
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.AutoSize = true;
            this.btnLamMoi.BackColor = System.Drawing.Color.White;
            this.btnLamMoi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLamMoi.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(207)))), ((int)(((byte)(220)))));
            this.btnLamMoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnLamMoi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(85)))), ((int)(((byte)(130)))));
            this.btnLamMoi.Location = new System.Drawing.Point(783, 8);
            this.btnLamMoi.Margin = new System.Windows.Forms.Padding(0, 4, 10, 4);
            this.btnLamMoi.MinimumSize = new System.Drawing.Size(108, 36);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Padding = new System.Windows.Forms.Padding(12, 2, 12, 2);
            this.btnLamMoi.Size = new System.Drawing.Size(120, 37);
            this.btnLamMoi.TabIndex = 5;
            this.btnLamMoi.Text = "Tải lại";
            this.btnLamMoi.UseVisualStyleBackColor = false;
            // 
            // btnDong
            // 
            this.btnDong.AutoSize = true;
            this.btnDong.BackColor = System.Drawing.Color.White;
            this.btnDong.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDong.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(207)))), ((int)(((byte)(220)))));
            this.btnDong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDong.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnDong.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(85)))), ((int)(((byte)(130)))));
            this.btnDong.Location = new System.Drawing.Point(913, 8);
            this.btnDong.Margin = new System.Windows.Forms.Padding(0, 4, 10, 4);
            this.btnDong.MinimumSize = new System.Drawing.Size(108, 36);
            this.btnDong.Name = "btnDong";
            this.btnDong.Padding = new System.Windows.Forms.Padding(12, 2, 12, 2);
            this.btnDong.Size = new System.Drawing.Size(120, 37);
            this.btnDong.TabIndex = 6;
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = false;
            // 
            // flpTimKiem
            // 
            this.flpTimKiem.AutoScroll = true;
            this.flpTimKiem.Controls.Add(this.lblTimKiem);
            this.flpTimKiem.Controls.Add(this.txtTimKiem);
            this.flpTimKiem.Controls.Add(this.btnTimKiem);
            this.flpTimKiem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpTimKiem.Location = new System.Drawing.Point(20, 358);
            this.flpTimKiem.Margin = new System.Windows.Forms.Padding(0);
            this.flpTimKiem.Name = "flpTimKiem";
            this.flpTimKiem.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.flpTimKiem.Size = new System.Drawing.Size(1140, 50);
            this.flpTimKiem.TabIndex = 3;
            this.flpTimKiem.WrapContents = false;
            // 
            // lblTimKiem
            // 
            this.lblTimKiem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(82)))), ((int)(((byte)(99)))));
            this.lblTimKiem.Location = new System.Drawing.Point(4, 8);
            this.lblTimKiem.Margin = new System.Windows.Forms.Padding(4, 4, 8, 4);
            this.lblTimKiem.Name = "lblTimKiem";
            this.lblTimKiem.Size = new System.Drawing.Size(110, 36);
            this.lblTimKiem.TabIndex = 0;
            this.lblTimKiem.Text = "Tìm dịch vụ";
            this.lblTimKiem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Location = new System.Drawing.Point(122, 12);
            this.txtTimKiem.Margin = new System.Windows.Forms.Padding(0, 8, 10, 4);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new System.Drawing.Size(320, 30);
            this.txtTimKiem.TabIndex = 1;
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
            this.btnTimKiem.Location = new System.Drawing.Point(452, 8);
            this.btnTimKiem.Margin = new System.Windows.Forms.Padding(0, 4, 10, 4);
            this.btnTimKiem.MinimumSize = new System.Drawing.Size(108, 36);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Padding = new System.Windows.Forms.Padding(12, 2, 12, 2);
            this.btnTimKiem.Size = new System.Drawing.Size(120, 37);
            this.btnTimKiem.TabIndex = 2;
            this.btnTimKiem.Text = "Tìm kiếm";
            this.btnTimKiem.UseVisualStyleBackColor = false;
            // 
            // lblHuongDan
            // 
            this.lblHuongDan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHuongDan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(82)))), ((int)(((byte)(99)))));
            this.lblHuongDan.Location = new System.Drawing.Point(24, 740);
            this.lblHuongDan.Margin = new System.Windows.Forms.Padding(4, 4, 8, 4);
            this.lblHuongDan.Name = "lblHuongDan";
            this.lblHuongDan.Size = new System.Drawing.Size(1128, 24);
            this.lblHuongDan.TabIndex = 4;
            this.lblHuongDan.Text = "Chọn một dòng trong bảng để xem hoặc cập nhật thông tin.";
            this.lblHuongDan.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dgvDichVu
            // 
            this.dgvDichVu.AllowUserToAddRows = false;
            this.dgvDichVu.AllowUserToDeleteRows = false;
            this.dgvDichVu.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDichVu.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(251)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(58)))), ((int)(((byte)(74)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDichVu.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDichVu.ColumnHeadersHeight = 40;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(234)))), ((int)(((byte)(249)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(42)))), ((int)(((byte)(56)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDichVu.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDichVu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDichVu.EnableHeadersVisualStyles = false;
            this.dgvDichVu.Location = new System.Drawing.Point(20, 414);
            this.dgvDichVu.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.dgvDichVu.MultiSelect = false;
            this.dgvDichVu.Name = "dgvDichVu";
            this.dgvDichVu.ReadOnly = true;
            this.dgvDichVu.RowHeadersVisible = false;
            this.dgvDichVu.RowHeadersWidth = 51;
            this.dgvDichVu.RowTemplate.Height = 34;
            this.dgvDichVu.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDichVu.Size = new System.Drawing.Size(1140, 322);
            this.dgvDichVu.TabIndex = 5;
            // 
            // frmDanhMucDichVu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.ClientSize = new System.Drawing.Size(1180, 780);
            this.Controls.Add(this.tlpTrang);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.MinimumSize = new System.Drawing.Size(960, 650);
            this.Name = "frmDanhMucDichVu";
            this.Text = "frmDanhMucDichVu";
            this.tlpTrang.ResumeLayout(false);
            this.grpThongTin.ResumeLayout(false);
            this.tlpNhap.ResumeLayout(false);
            this.tlpNhap.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numGiaDichVu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLuongNhiemVu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPhutDuKien)).EndInit();
            this.flpThaoTac.ResumeLayout(false);
            this.flpThaoTac.PerformLayout();
            this.flpTimKiem.ResumeLayout(false);
            this.flpTimKiem.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDichVu)).EndInit();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.TableLayoutPanel tlpTrang;
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.GroupBox grpThongTin;
        private System.Windows.Forms.TableLayoutPanel tlpNhap;
        private System.Windows.Forms.FlowLayoutPanel flpThaoTac;
        private System.Windows.Forms.FlowLayoutPanel flpTimKiem;
        private System.Windows.Forms.Label lblHuongDan;
        private System.Windows.Forms.Label lbltxtMaDV;
        private System.Windows.Forms.TextBox txtMaDV;
        private System.Windows.Forms.Label lbltxtTenDichVu;
        private System.Windows.Forms.TextBox txtTenDichVu;
        private System.Windows.Forms.Label lblnumGiaDichVu;
        private System.Windows.Forms.NumericUpDown numGiaDichVu;
        private System.Windows.Forms.Label lblnumLuongNhiemVu;
        private System.Windows.Forms.NumericUpDown numLuongNhiemVu;
        private System.Windows.Forms.Label lblnumPhutDuKien;
        private System.Windows.Forms.NumericUpDown numPhutDuKien;
        private System.Windows.Forms.Label lblcboTrangThai;
        private System.Windows.Forms.ComboBox cboTrangThai;
        private System.Windows.Forms.Label lbltxtMoTa;
        private System.Windows.Forms.TextBox txtMoTa;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnDinhMuc;
        private System.Windows.Forms.Button btnKho;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Button btnDong;
        private System.Windows.Forms.Label lblTimKiem;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.DataGridView dgvDichVu;
    }
}
