namespace QLyCuaHangSuaXe.DanhMuc.TiepNhan
{
    partial class frmKiemTraTiepNhan
    {
        private System.ComponentModel.IContainer thanhPhan = null;
        protected override void Dispose(bool dangHuy)
        {
            if (dangHuy && thanhPhan != null)
                thanhPhan.Dispose();
            base.Dispose(dangHuy);
        }

        private void InitializeComponent()
        {
            this.tlpTrang = new System.Windows.Forms.TableLayoutPanel();
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.tlpChinh = new System.Windows.Forms.TableLayoutPanel();
            this.tlpTrai = new System.Windows.Forms.TableLayoutPanel();
            this.grbChon = new System.Windows.Forms.GroupBox();
            this.tlpChon = new System.Windows.Forms.TableLayoutPanel();
            this.lblDichVu = new System.Windows.Forms.Label();
            this.cboDichVu = new System.Windows.Forms.ComboBox();
            this.lblHoatDong = new System.Windows.Forms.Label();
            this.lblPhuTung = new System.Windows.Forms.Label();
            this.cboPhuTung = new System.Windows.Forms.ComboBox();
            this.lblSoLuong = new System.Windows.Forms.Label();
            this.lblTonKho = new System.Windows.Forms.Label();
            this.numSoLuong = new System.Windows.Forms.NumericUpDown();
            this.txtTonKho = new System.Windows.Forms.TextBox();
            this.lblGiaDichVu = new System.Windows.Forms.Label();
            this.txtGiaDichVu = new System.Windows.Forms.TextBox();
            this.btnThem = new System.Windows.Forms.Button();
            this.tlpPhai = new System.Windows.Forms.TableLayoutPanel();
            this.grbDanhSach = new System.Windows.Forms.GroupBox();
            this.dgvHangMuc = new System.Windows.Forms.DataGridView();
            this.tlpTong = new System.Windows.Forms.TableLayoutPanel();
            this.lblSoHangMuc = new System.Windows.Forms.Label();
            this.lblTongTien = new System.Windows.Forms.Label();
            this.grbKetQua = new System.Windows.Forms.GroupBox();
            this.lblGiaiThich = new System.Windows.Forms.Label();
            this.lblKetQua = new System.Windows.Forms.Label();
            this.tlpCuoi = new System.Windows.Forms.TableLayoutPanel();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.btnXoaDong = new System.Windows.Forms.Button();
            this.btnKiemTra = new System.Windows.Forms.Button();
            this.btnChuyen = new System.Windows.Forms.Button();
            this.tlpTrang.SuspendLayout();
            this.tlpChinh.SuspendLayout();
            this.tlpTrai.SuspendLayout();
            this.grbChon.SuspendLayout();
            this.tlpChon.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSoLuong)).BeginInit();
            this.tlpPhai.SuspendLayout();
            this.grbDanhSach.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHangMuc)).BeginInit();
            this.tlpTong.SuspendLayout();
            this.grbKetQua.SuspendLayout();
            this.tlpCuoi.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpTrang
            // 
            this.tlpTrang.ColumnCount = 1;
            this.tlpTrang.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTrang.Controls.Add(this.lblTieuDe, 0, 0);
            this.tlpTrang.Controls.Add(this.tlpChinh, 0, 1);
            this.tlpTrang.Controls.Add(this.tlpCuoi, 0, 2);
            this.tlpTrang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpTrang.Location = new System.Drawing.Point(0, 0);
            this.tlpTrang.Name = "tlpTrang";
            this.tlpTrang.Padding = new System.Windows.Forms.Padding(20, 12, 20, 12);
            this.tlpTrang.RowCount = 3;
            this.tlpTrang.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tlpTrang.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTrang.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 58F));
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
            this.lblTieuDe.Size = new System.Drawing.Size(1128, 40);
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "KIỂM TRA TRƯỚC TIẾP NHẬN";
            this.lblTieuDe.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tlpChinh
            // 
            this.tlpChinh.ColumnCount = 2;
            this.tlpChinh.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 310F));
            this.tlpChinh.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpChinh.Controls.Add(this.tlpTrai, 0, 0);
            this.tlpChinh.Controls.Add(this.tlpPhai, 1, 0);
            this.tlpChinh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpChinh.Location = new System.Drawing.Point(20, 66);
            this.tlpChinh.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.tlpChinh.Name = "tlpChinh";
            this.tlpChinh.RowCount = 1;
            this.tlpChinh.Size = new System.Drawing.Size(1140, 644);
            this.tlpChinh.TabIndex = 2;
            // 
            // tlpTrai
            // 
            this.tlpTrai.ColumnCount = 1;
            this.tlpTrai.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTrai.Controls.Add(this.grbChon, 0, 0);
            this.tlpTrai.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpTrai.Location = new System.Drawing.Point(3, 3);
            this.tlpTrai.Name = "tlpTrai";
            this.tlpTrai.RowCount = 2;
            this.tlpTrai.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 364F));
            this.tlpTrai.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTrai.Size = new System.Drawing.Size(304, 638);
            this.tlpTrai.TabIndex = 0;
            // 
            // grbChon
            // 
            this.grbChon.Controls.Add(this.tlpChon);
            this.grbChon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbChon.Location = new System.Drawing.Point(3, 3);
            this.grbChon.Name = "grbChon";
            this.grbChon.Padding = new System.Windows.Forms.Padding(10);
            this.tlpTrai.SetRowSpan(this.grbChon, 2);
            this.grbChon.Size = new System.Drawing.Size(298, 632);
            this.grbChon.TabIndex = 0;
            this.grbChon.TabStop = false;
            this.grbChon.Text = "Dịch vụ";
            // 
            // tlpChon
            // 
            this.tlpChon.ColumnCount = 2;
            this.tlpChon.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpChon.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpChon.Controls.Add(this.lblDichVu, 0, 0);
            this.tlpChon.Controls.Add(this.cboDichVu, 0, 1);
            this.tlpChon.Controls.Add(this.lblHoatDong, 0, 2);
            this.tlpChon.Controls.Add(this.lblPhuTung, 0, 3);
            this.tlpChon.Controls.Add(this.cboPhuTung, 0, 4);
            this.tlpChon.Controls.Add(this.lblSoLuong, 0, 5);
            this.tlpChon.Controls.Add(this.lblTonKho, 1, 5);
            this.tlpChon.Controls.Add(this.numSoLuong, 0, 6);
            this.tlpChon.Controls.Add(this.txtTonKho, 1, 6);
            this.tlpChon.Controls.Add(this.lblGiaDichVu, 0, 7);
            this.tlpChon.Controls.Add(this.txtGiaDichVu, 0, 8);
            this.tlpChon.Controls.Add(this.btnThem, 0, 9);
            this.tlpChon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpChon.Location = new System.Drawing.Point(10, 33);
            this.tlpChon.Name = "tlpChon";
            this.tlpChon.RowCount = 10;
            this.tlpChon.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tlpChon.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tlpChon.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tlpChon.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpChon.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tlpChon.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tlpChon.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tlpChon.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tlpChon.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tlpChon.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tlpChon.Size = new System.Drawing.Size(278, 589);
            this.tlpChon.TabIndex = 0;
            // 
            // lblDichVu
            // 
            this.tlpChon.SetColumnSpan(this.lblDichVu, 2);
            this.lblDichVu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDichVu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(82)))), ((int)(((byte)(99)))));
            this.lblDichVu.Location = new System.Drawing.Point(4, 4);
            this.lblDichVu.Margin = new System.Windows.Forms.Padding(4, 4, 8, 4);
            this.lblDichVu.Name = "lblDichVu";
            this.lblDichVu.Size = new System.Drawing.Size(266, 16);
            this.lblDichVu.TabIndex = 0;
            this.lblDichVu.Text = "Dịch vụ";
            this.lblDichVu.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboDichVu
            // 
            this.tlpChon.SetColumnSpan(this.cboDichVu, 2);
            this.cboDichVu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboDichVu.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDichVu.Location = new System.Drawing.Point(4, 28);
            this.cboDichVu.Margin = new System.Windows.Forms.Padding(4);
            this.cboDichVu.Name = "cboDichVu";
            this.cboDichVu.Size = new System.Drawing.Size(270, 31);
            this.cboDichVu.TabIndex = 1;
            // 
            // lblHoatDong
            // 
            this.tlpChon.SetColumnSpan(this.lblHoatDong, 2);
            this.lblHoatDong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHoatDong.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(82)))), ((int)(((byte)(99)))));
            this.lblHoatDong.Location = new System.Drawing.Point(4, 62);
            this.lblHoatDong.Margin = new System.Windows.Forms.Padding(4, 4, 8, 4);
            this.lblHoatDong.Name = "lblHoatDong";
            this.lblHoatDong.Size = new System.Drawing.Size(266, 16);
            this.lblHoatDong.TabIndex = 2;
            this.lblHoatDong.Text = "Đang hoạt động";
            this.lblHoatDong.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPhuTung
            // 
            this.tlpChon.SetColumnSpan(this.lblPhuTung, 2);
            this.lblPhuTung.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPhuTung.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(82)))), ((int)(((byte)(99)))));
            this.lblPhuTung.Location = new System.Drawing.Point(4, 86);
            this.lblPhuTung.Margin = new System.Windows.Forms.Padding(4, 4, 8, 4);
            this.lblPhuTung.Name = "lblPhuTung";
            this.lblPhuTung.Size = new System.Drawing.Size(266, 22);
            this.lblPhuTung.TabIndex = 3;
            this.lblPhuTung.Text = "Phụ tùng liên kết";
            this.lblPhuTung.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboPhuTung
            // 
            this.tlpChon.SetColumnSpan(this.cboPhuTung, 2);
            this.cboPhuTung.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboPhuTung.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPhuTung.Location = new System.Drawing.Point(4, 116);
            this.cboPhuTung.Margin = new System.Windows.Forms.Padding(4);
            this.cboPhuTung.Name = "cboPhuTung";
            this.cboPhuTung.Size = new System.Drawing.Size(270, 31);
            this.cboPhuTung.TabIndex = 4;
            // 
            // lblSoLuong
            // 
            this.lblSoLuong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSoLuong.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(82)))), ((int)(((byte)(99)))));
            this.lblSoLuong.Location = new System.Drawing.Point(4, 150);
            this.lblSoLuong.Margin = new System.Windows.Forms.Padding(4, 4, 8, 4);
            this.lblSoLuong.Name = "lblSoLuong";
            this.lblSoLuong.Size = new System.Drawing.Size(127, 18);
            this.lblSoLuong.TabIndex = 5;
            this.lblSoLuong.Text = "Số lượng (1–1000)";
            this.lblSoLuong.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTonKho
            // 
            this.lblTonKho.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTonKho.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(82)))), ((int)(((byte)(99)))));
            this.lblTonKho.Location = new System.Drawing.Point(143, 150);
            this.lblTonKho.Margin = new System.Windows.Forms.Padding(4, 4, 8, 4);
            this.lblTonKho.Name = "lblTonKho";
            this.lblTonKho.Size = new System.Drawing.Size(127, 18);
            this.lblTonKho.TabIndex = 6;
            this.lblTonKho.Text = "Tồn kho / đơn vị DV";
            this.lblTonKho.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // numSoLuong
            // 
            this.numSoLuong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numSoLuong.Location = new System.Drawing.Point(4, 176);
            this.numSoLuong.Margin = new System.Windows.Forms.Padding(4);
            this.numSoLuong.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numSoLuong.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numSoLuong.Name = "numSoLuong";
            this.numSoLuong.Size = new System.Drawing.Size(131, 30);
            this.numSoLuong.TabIndex = 7;
            this.numSoLuong.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // txtTonKho
            // 
            this.txtTonKho.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTonKho.Location = new System.Drawing.Point(143, 176);
            this.txtTonKho.Margin = new System.Windows.Forms.Padding(4);
            this.txtTonKho.Name = "txtTonKho";
            this.txtTonKho.ReadOnly = true;
            this.txtTonKho.Size = new System.Drawing.Size(131, 30);
            this.txtTonKho.TabIndex = 8;
            // 
            // lblGiaDichVu
            // 
            this.tlpChon.SetColumnSpan(this.lblGiaDichVu, 2);
            this.lblGiaDichVu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblGiaDichVu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(82)))), ((int)(((byte)(99)))));
            this.lblGiaDichVu.Location = new System.Drawing.Point(4, 212);
            this.lblGiaDichVu.Margin = new System.Windows.Forms.Padding(4, 4, 8, 4);
            this.lblGiaDichVu.Name = "lblGiaDichVu";
            this.lblGiaDichVu.Size = new System.Drawing.Size(266, 20);
            this.lblGiaDichVu.TabIndex = 9;
            this.lblGiaDichVu.Text = "Giá dịch vụ (đ)";
            this.lblGiaDichVu.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtGiaDichVu
            // 
            this.tlpChon.SetColumnSpan(this.txtGiaDichVu, 2);
            this.txtGiaDichVu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtGiaDichVu.Location = new System.Drawing.Point(4, 240);
            this.txtGiaDichVu.Margin = new System.Windows.Forms.Padding(4);
            this.txtGiaDichVu.Name = "txtGiaDichVu";
            this.txtGiaDichVu.ReadOnly = true;
            this.txtGiaDichVu.Size = new System.Drawing.Size(270, 30);
            this.txtGiaDichVu.TabIndex = 11;
            // 
            // btnThem
            // 
            this.btnThem.AutoSize = true;
            this.btnThem.BackColor = System.Drawing.Color.White;
            this.tlpChon.SetColumnSpan(this.btnThem, 2);
            this.btnThem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnThem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnThem.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(207)))), ((int)(((byte)(220)))));
            this.btnThem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThem.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnThem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(85)))), ((int)(((byte)(130)))));
            this.btnThem.Location = new System.Drawing.Point(0, 276);
            this.btnThem.Margin = new System.Windows.Forms.Padding(0, 4, 10, 4);
            this.btnThem.MinimumSize = new System.Drawing.Size(108, 36);
            this.btnThem.Name = "btnThem";
            this.btnThem.Padding = new System.Windows.Forms.Padding(12, 2, 12, 2);
            this.btnThem.Size = new System.Drawing.Size(268, 309);
            this.btnThem.TabIndex = 13;
            this.btnThem.Text = "+ Thêm vào danh sách";
            this.btnThem.UseVisualStyleBackColor = false;
            // 
            // tlpPhai
            // 
            this.tlpPhai.ColumnCount = 1;
            this.tlpPhai.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpPhai.Controls.Add(this.grbDanhSach, 0, 0);
            this.tlpPhai.Controls.Add(this.tlpTong, 0, 1);
            this.tlpPhai.Controls.Add(this.grbKetQua, 0, 2);
            this.tlpPhai.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpPhai.Location = new System.Drawing.Point(313, 3);
            this.tlpPhai.Name = "tlpPhai";
            this.tlpPhai.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.tlpPhai.RowCount = 3;
            this.tlpPhai.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpPhai.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tlpPhai.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 126F));
            this.tlpPhai.Size = new System.Drawing.Size(824, 638);
            this.tlpPhai.TabIndex = 1;
            // 
            // grbDanhSach
            // 
            this.grbDanhSach.Controls.Add(this.dgvHangMuc);
            this.grbDanhSach.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbDanhSach.Location = new System.Drawing.Point(13, 3);
            this.grbDanhSach.Name = "grbDanhSach";
            this.grbDanhSach.Padding = new System.Windows.Forms.Padding(10);
            this.grbDanhSach.Size = new System.Drawing.Size(808, 436);
            this.grbDanhSach.TabIndex = 0;
            this.grbDanhSach.TabStop = false;
            this.grbDanhSach.Text = "Hạng mục đã chọn";
            // 
            // dgvHangMuc
            // 
            this.dgvHangMuc.AllowUserToAddRows = false;
            this.dgvHangMuc.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvHangMuc.BackgroundColor = System.Drawing.Color.White;
            this.dgvHangMuc.ColumnHeadersHeight = 29;
            this.dgvHangMuc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvHangMuc.Location = new System.Drawing.Point(10, 33);
            this.dgvHangMuc.Name = "dgvHangMuc";
            this.dgvHangMuc.RowHeadersVisible = false;
            this.dgvHangMuc.RowHeadersWidth = 51;
            this.dgvHangMuc.Size = new System.Drawing.Size(788, 393);
            this.dgvHangMuc.TabIndex = 0;
            // 
            // tlpTong
            // 
            this.tlpTong.ColumnCount = 2;
            this.tlpTong.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpTong.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpTong.Controls.Add(this.lblSoHangMuc, 0, 0);
            this.tlpTong.Controls.Add(this.lblTongTien, 1, 0);
            this.tlpTong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpTong.Location = new System.Drawing.Point(13, 445);
            this.tlpTong.Name = "tlpTong";
            this.tlpTong.Padding = new System.Windows.Forms.Padding(8);
            this.tlpTong.RowCount = 2;
            this.tlpTong.Size = new System.Drawing.Size(808, 64);
            this.tlpTong.TabIndex = 1;
            // 
            // lblSoHangMuc
            // 
            this.lblSoHangMuc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSoHangMuc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(82)))), ((int)(((byte)(99)))));
            this.lblSoHangMuc.Location = new System.Drawing.Point(12, 12);
            this.lblSoHangMuc.Margin = new System.Windows.Forms.Padding(4, 4, 8, 4);
            this.lblSoHangMuc.Name = "lblSoHangMuc";
            this.lblSoHangMuc.Size = new System.Drawing.Size(384, 23);
            this.lblSoHangMuc.TabIndex = 0;
            this.lblSoHangMuc.Text = "0 hạng mục";
            this.lblSoHangMuc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTongTien
            // 
            this.lblTongTien.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTongTien.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTongTien.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(82)))), ((int)(((byte)(99)))));
            this.lblTongTien.Location = new System.Drawing.Point(408, 12);
            this.lblTongTien.Margin = new System.Windows.Forms.Padding(4, 4, 8, 4);
            this.lblTongTien.Name = "lblTongTien";
            this.lblTongTien.Size = new System.Drawing.Size(384, 23);
            this.lblTongTien.TabIndex = 1;
            this.lblTongTien.Text = "Dự kiến: 0 đ";
            this.lblTongTien.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // grbKetQua
            // 
            this.grbKetQua.Controls.Add(this.lblGiaiThich);
            this.grbKetQua.Controls.Add(this.lblKetQua);
            this.grbKetQua.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbKetQua.Location = new System.Drawing.Point(13, 515);
            this.grbKetQua.Name = "grbKetQua";
            this.grbKetQua.Padding = new System.Windows.Forms.Padding(12);
            this.grbKetQua.Size = new System.Drawing.Size(808, 120);
            this.grbKetQua.TabIndex = 2;
            this.grbKetQua.TabStop = false;
            this.grbKetQua.Text = "Kết quả kiểm tra";
            // 
            // lblGiaiThich
            // 
            this.lblGiaiThich.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblGiaiThich.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(82)))), ((int)(((byte)(99)))));
            this.lblGiaiThich.Location = new System.Drawing.Point(12, 65);
            this.lblGiaiThich.Margin = new System.Windows.Forms.Padding(4, 4, 8, 4);
            this.lblGiaiThich.Name = "lblGiaiThich";
            this.lblGiaiThich.Size = new System.Drawing.Size(784, 43);
            this.lblGiaiThich.TabIndex = 0;
            this.lblGiaiThich.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblKetQua
            // 
            this.lblKetQua.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblKetQua.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblKetQua.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(82)))), ((int)(((byte)(99)))));
            this.lblKetQua.Location = new System.Drawing.Point(12, 35);
            this.lblKetQua.Margin = new System.Windows.Forms.Padding(4, 4, 8, 4);
            this.lblKetQua.Name = "lblKetQua";
            this.lblKetQua.Size = new System.Drawing.Size(784, 30);
            this.lblKetQua.TabIndex = 1;
            this.lblKetQua.Text = "CHƯA KIỂM TRA";
            this.lblKetQua.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tlpCuoi
            // 
            this.tlpCuoi.ColumnCount = 5;
            this.tlpCuoi.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tlpCuoi.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tlpCuoi.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpCuoi.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tlpCuoi.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 230F));
            this.tlpCuoi.Controls.Add(this.btnLamMoi, 0, 0);
            this.tlpCuoi.Controls.Add(this.btnXoaDong, 1, 0);
            this.tlpCuoi.Controls.Add(this.btnKiemTra, 3, 0);
            this.tlpCuoi.Controls.Add(this.btnChuyen, 4, 0);
            this.tlpCuoi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpCuoi.Location = new System.Drawing.Point(23, 713);
            this.tlpCuoi.Name = "tlpCuoi";
            this.tlpCuoi.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.tlpCuoi.RowCount = 1;
            this.tlpCuoi.Size = new System.Drawing.Size(1134, 52);
            this.tlpCuoi.TabIndex = 3;
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.BackColor = System.Drawing.Color.White;
            this.btnLamMoi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLamMoi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnLamMoi.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(207)))), ((int)(((byte)(220)))));
            this.btnLamMoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnLamMoi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(85)))), ((int)(((byte)(130)))));
            this.btnLamMoi.Location = new System.Drawing.Point(0, 12);
            this.btnLamMoi.Margin = new System.Windows.Forms.Padding(0, 4, 10, 4);
            this.btnLamMoi.MinimumSize = new System.Drawing.Size(0, 36);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Padding = new System.Windows.Forms.Padding(12, 2, 12, 2);
            this.btnLamMoi.Size = new System.Drawing.Size(110, 36);
            this.btnLamMoi.TabIndex = 0;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.UseVisualStyleBackColor = false;
            // 
            // btnXoaDong
            // 
            this.btnXoaDong.BackColor = System.Drawing.Color.White;
            this.btnXoaDong.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXoaDong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnXoaDong.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(207)))), ((int)(((byte)(220)))));
            this.btnXoaDong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXoaDong.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnXoaDong.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(85)))), ((int)(((byte)(130)))));
            this.btnXoaDong.Location = new System.Drawing.Point(120, 12);
            this.btnXoaDong.Margin = new System.Windows.Forms.Padding(0, 4, 10, 4);
            this.btnXoaDong.MinimumSize = new System.Drawing.Size(0, 36);
            this.btnXoaDong.Name = "btnXoaDong";
            this.btnXoaDong.Padding = new System.Windows.Forms.Padding(12, 2, 12, 2);
            this.btnXoaDong.Size = new System.Drawing.Size(110, 36);
            this.btnXoaDong.TabIndex = 1;
            this.btnXoaDong.Text = "Xóa dòng";
            this.btnXoaDong.UseVisualStyleBackColor = false;
            // 
            // btnKiemTra
            // 
            this.btnKiemTra.BackColor = System.Drawing.Color.White;
            this.btnKiemTra.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnKiemTra.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnKiemTra.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(207)))), ((int)(((byte)(220)))));
            this.btnKiemTra.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKiemTra.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnKiemTra.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(85)))), ((int)(((byte)(130)))));
            this.btnKiemTra.Location = new System.Drawing.Point(704, 12);
            this.btnKiemTra.Margin = new System.Windows.Forms.Padding(0, 4, 10, 4);
            this.btnKiemTra.MinimumSize = new System.Drawing.Size(0, 36);
            this.btnKiemTra.Name = "btnKiemTra";
            this.btnKiemTra.Padding = new System.Windows.Forms.Padding(12, 2, 12, 2);
            this.btnKiemTra.Size = new System.Drawing.Size(190, 36);
            this.btnKiemTra.TabIndex = 2;
            this.btnKiemTra.Text = "Kiểm tra điều kiện";
            this.btnKiemTra.UseVisualStyleBackColor = false;
            // 
            // btnChuyen
            // 
            this.btnChuyen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(108)))), ((int)(((byte)(176)))));
            this.btnChuyen.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnChuyen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnChuyen.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(108)))), ((int)(((byte)(176)))));
            this.btnChuyen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChuyen.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnChuyen.ForeColor = System.Drawing.Color.White;
            this.btnChuyen.Location = new System.Drawing.Point(904, 12);
            this.btnChuyen.Margin = new System.Windows.Forms.Padding(0, 4, 10, 4);
            this.btnChuyen.MinimumSize = new System.Drawing.Size(0, 36);
            this.btnChuyen.Name = "btnChuyen";
            this.btnChuyen.Padding = new System.Windows.Forms.Padding(12, 2, 12, 2);
            this.btnChuyen.Size = new System.Drawing.Size(220, 36);
            this.btnChuyen.TabIndex = 3;
            this.btnChuyen.Text = "Chuyển sang tiếp nhận →";
            this.btnChuyen.UseVisualStyleBackColor = false;
            // 
            // frmKiemTraTiepNhan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.ClientSize = new System.Drawing.Size(1180, 780);
            this.Controls.Add(this.tlpTrang);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.MinimumSize = new System.Drawing.Size(960, 650);
            this.Name = "frmKiemTraTiepNhan";
            this.Text = "Kiểm tra trước tiếp nhận";
            this.tlpTrang.ResumeLayout(false);
            this.tlpChinh.ResumeLayout(false);
            this.tlpTrai.ResumeLayout(false);
            this.grbChon.ResumeLayout(false);
            this.tlpChon.ResumeLayout(false);
            this.tlpChon.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSoLuong)).EndInit();
            this.tlpPhai.ResumeLayout(false);
            this.grbDanhSach.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHangMuc)).EndInit();
            this.tlpTong.ResumeLayout(false);
            this.grbKetQua.ResumeLayout(false);
            this.tlpCuoi.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.TableLayoutPanel tlpTrang;
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.TableLayoutPanel tlpChinh;
        private System.Windows.Forms.TableLayoutPanel tlpTrai;
        private System.Windows.Forms.GroupBox grbChon;
        private System.Windows.Forms.TableLayoutPanel tlpChon;
        private System.Windows.Forms.Label lblDichVu;
        private System.Windows.Forms.ComboBox cboDichVu;
        private System.Windows.Forms.Label lblHoatDong;
        private System.Windows.Forms.Label lblPhuTung;
        private System.Windows.Forms.ComboBox cboPhuTung;
        private System.Windows.Forms.Label lblSoLuong;
        private System.Windows.Forms.Label lblTonKho;
        private System.Windows.Forms.NumericUpDown numSoLuong;
        private System.Windows.Forms.TextBox txtTonKho;
        private System.Windows.Forms.Label lblGiaDichVu;
        private System.Windows.Forms.TextBox txtGiaDichVu;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.TableLayoutPanel tlpPhai;
        private System.Windows.Forms.GroupBox grbDanhSach;
        private System.Windows.Forms.DataGridView dgvHangMuc;
        private System.Windows.Forms.TableLayoutPanel tlpTong;
        private System.Windows.Forms.Label lblSoHangMuc;
        private System.Windows.Forms.Label lblTongTien;
        private System.Windows.Forms.GroupBox grbKetQua;
        private System.Windows.Forms.Label lblKetQua;
        private System.Windows.Forms.Label lblGiaiThich;
        private System.Windows.Forms.TableLayoutPanel tlpCuoi;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Button btnXoaDong;
        private System.Windows.Forms.Button btnKiemTra;
        private System.Windows.Forms.Button btnChuyen;
    }
}
