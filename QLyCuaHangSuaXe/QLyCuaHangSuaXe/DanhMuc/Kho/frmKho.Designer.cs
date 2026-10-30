namespace btl
{
    partial class frmKho
    {
        private System.ComponentModel.IContainer thanhPhan = null;
        protected override void Dispose(bool dangHuy) { if (dangHuy && thanhPhan != null) thanhPhan.Dispose(); base.Dispose(dangHuy); }
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tlpTrang = new System.Windows.Forms.TableLayoutPanel();
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.tlpTimKiem = new System.Windows.Forms.TableLayoutPanel();
            this.lblTimPhuTung = new System.Windows.Forms.Label();
            this.txtTimPhuTung = new System.Windows.Forms.TextBox();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.tabKho = new System.Windows.Forms.TabControl();
            this.tabPhuTung = new System.Windows.Forms.TabPage();
            this.tlpDanhMuc = new System.Windows.Forms.TableLayoutPanel();
            this.dgvPhuTung = new System.Windows.Forms.DataGridView();
            this.grbDieuChinh = new System.Windows.Forms.GroupBox();
            this.tlpDieuChinh = new System.Windows.Forms.TableLayoutPanel();
            this.lblMaDieuChinhTon = new System.Windows.Forms.Label();
            this.txtMaDieuChinhTon = new System.Windows.Forms.TextBox();
            this.lblMaDieuChinhGia = new System.Windows.Forms.Label();
            this.txtMaDieuChinhGia = new System.Windows.Forms.TextBox();
            this.lblSoLuongLech = new System.Windows.Forms.Label();
            this.numSoLuongLech = new System.Windows.Forms.NumericUpDown();
            this.lblGiaBanMoi = new System.Windows.Forms.Label();
            this.numGiaBanMoi = new System.Windows.Forms.NumericUpDown();
            this.lblLyDo = new System.Windows.Forms.Label();
            this.txtLyDo = new System.Windows.Forms.TextBox();
            this.flpDieuChinh = new System.Windows.Forms.FlowLayoutPanel();
            this.btnDieuChinhTon = new System.Windows.Forms.Button();
            this.btnDieuChinhGia = new System.Windows.Forms.Button();
            this.lblNguoiGhi = new System.Windows.Forms.Label();
            this.tabLichSuTon = new System.Windows.Forms.TabPage();
            this.tlpLichSuTon = new System.Windows.Forms.TableLayoutPanel();
            this.dgvLichSuTon = new System.Windows.Forms.DataGridView();
            this.lblTongTienLech = new System.Windows.Forms.Label();
            this.tabLichSuGia = new System.Windows.Forms.TabPage();
            this.dgvLichSuGia = new System.Windows.Forms.DataGridView();
            this.flpThaoTac = new System.Windows.Forms.FlowLayoutPanel();
            this.btnNhapPhuTung = new System.Windows.Forms.Button();
            this.btnDanhSachPhieuNhap = new System.Windows.Forms.Button();
            this.btnNhaCungCap = new System.Windows.Forms.Button();
            this.btnNgungSuDung = new System.Windows.Forms.Button();
            this.btnThoat = new System.Windows.Forms.Button();
            this.btnXuatExcel = new System.Windows.Forms.Button();
            this.tlpTrang.SuspendLayout();
            this.tlpTimKiem.SuspendLayout();
            this.tabKho.SuspendLayout();
            this.tabPhuTung.SuspendLayout();
            this.tlpDanhMuc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhuTung)).BeginInit();
            this.grbDieuChinh.SuspendLayout();
            this.tlpDieuChinh.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSoLuongLech)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numGiaBanMoi)).BeginInit();
            this.flpDieuChinh.SuspendLayout();
            this.tabLichSuTon.SuspendLayout();
            this.tlpLichSuTon.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLichSuTon)).BeginInit();
            this.tabLichSuGia.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLichSuGia)).BeginInit();
            this.flpThaoTac.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpTrang
            // 
            this.tlpTrang.ColumnCount = 1;
            this.tlpTrang.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTrang.Controls.Add(this.lblTieuDe, 0, 0);
            this.tlpTrang.Controls.Add(this.tlpTimKiem, 0, 1);
            this.tlpTrang.Controls.Add(this.tabKho, 0, 2);
            this.tlpTrang.Controls.Add(this.flpThaoTac, 0, 3);
            this.tlpTrang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpTrang.Location = new System.Drawing.Point(0, 0);
            this.tlpTrang.Margin = new System.Windows.Forms.Padding(0);
            this.tlpTrang.Name = "tlpTrang";
            this.tlpTrang.Padding = new System.Windows.Forms.Padding(20);
            this.tlpTrang.RowCount = 4;
            this.tlpTrang.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 72F));
            this.tlpTrang.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tlpTrang.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTrang.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 65F));
            this.tlpTrang.Size = new System.Drawing.Size(1475, 975);
            this.tlpTrang.TabIndex = 0;
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTieuDe.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTieuDe.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(85)))), ((int)(((byte)(130)))));
            this.lblTieuDe.Location = new System.Drawing.Point(24, 20);
            this.lblTieuDe.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(1427, 72);
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "KHO PHỤ TÙNG";
            this.lblTieuDe.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tlpTimKiem
            // 
            this.tlpTimKiem.ColumnCount = 3;
            this.tlpTimKiem.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 188F));
            this.tlpTimKiem.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTimKiem.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.tlpTimKiem.Controls.Add(this.lblTimPhuTung, 0, 0);
            this.tlpTimKiem.Controls.Add(this.txtTimPhuTung, 1, 0);
            this.tlpTimKiem.Controls.Add(this.btnLamMoi, 2, 0);
            this.tlpTimKiem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpTimKiem.Location = new System.Drawing.Point(20, 92);
            this.tlpTimKiem.Margin = new System.Windows.Forms.Padding(0);
            this.tlpTimKiem.Name = "tlpTimKiem";
            this.tlpTimKiem.RowCount = 1;
            this.tlpTimKiem.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTimKiem.Size = new System.Drawing.Size(1435, 60);
            this.tlpTimKiem.TabIndex = 1;
            // 
            // lblTimPhuTung
            // 
            this.lblTimPhuTung.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTimPhuTung.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(85)))), ((int)(((byte)(130)))));
            this.lblTimPhuTung.Location = new System.Drawing.Point(4, 0);
            this.lblTimPhuTung.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTimPhuTung.Name = "lblTimPhuTung";
            this.lblTimPhuTung.Size = new System.Drawing.Size(180, 60);
            this.lblTimPhuTung.TabIndex = 0;
            this.lblTimPhuTung.Text = "Tìm trong kho";
            this.lblTimPhuTung.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtTimPhuTung
            // 
            this.txtTimPhuTung.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTimPhuTung.Location = new System.Drawing.Point(193, 10);
            this.txtTimPhuTung.Margin = new System.Windows.Forms.Padding(5, 10, 15, 8);
            this.txtTimPhuTung.Name = "txtTimPhuTung";
            this.txtTimPhuTung.Size = new System.Drawing.Size(1067, 30);
            this.txtTimPhuTung.TabIndex = 1;
            this.txtTimPhuTung.TextChanged += new System.EventHandler(this.txtTim_TextChanged);
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.BackColor = System.Drawing.Color.White;
            this.btnLamMoi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnLamMoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnLamMoi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(85)))), ((int)(((byte)(130)))));
            this.btnLamMoi.Location = new System.Drawing.Point(1275, 6);
            this.btnLamMoi.Margin = new System.Windows.Forms.Padding(0, 6, 12, 6);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(148, 48);
            this.btnLamMoi.TabIndex = 2;
            this.btnLamMoi.Text = "Làm mới kho";
            this.btnLamMoi.UseVisualStyleBackColor = false;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Bam);
            // 
            // tabKho
            // 
            this.tabKho.Controls.Add(this.tabPhuTung);
            this.tabKho.Controls.Add(this.tabLichSuTon);
            this.tabKho.Controls.Add(this.tabLichSuGia);
            this.tabKho.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabKho.ItemSize = new System.Drawing.Size(220, 40);
            this.tabKho.Location = new System.Drawing.Point(20, 158);
            this.tabKho.Margin = new System.Windows.Forms.Padding(0, 6, 0, 6);
            this.tabKho.Name = "tabKho";
            this.tabKho.SelectedIndex = 0;
            this.tabKho.Size = new System.Drawing.Size(1435, 726);
            this.tabKho.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabKho.TabIndex = 2;
            // 
            // tabPhuTung
            // 
            this.tabPhuTung.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(247)))), ((int)(((byte)(251)))));
            this.tabPhuTung.Controls.Add(this.tlpDanhMuc);
            this.tabPhuTung.Location = new System.Drawing.Point(4, 44);
            this.tabPhuTung.Name = "tabPhuTung";
            this.tabPhuTung.Padding = new System.Windows.Forms.Padding(10);
            this.tabPhuTung.Size = new System.Drawing.Size(1427, 678);
            this.tabPhuTung.TabIndex = 0;
            this.tabPhuTung.Text = "Phụ tùng và điều chỉnh";
            // 
            // tlpDanhMuc
            // 
            this.tlpDanhMuc.ColumnCount = 1;
            this.tlpDanhMuc.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpDanhMuc.Controls.Add(this.dgvPhuTung, 0, 0);
            this.tlpDanhMuc.Controls.Add(this.grbDieuChinh, 0, 1);
            this.tlpDanhMuc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpDanhMuc.Location = new System.Drawing.Point(10, 10);
            this.tlpDanhMuc.Margin = new System.Windows.Forms.Padding(0);
            this.tlpDanhMuc.Name = "tlpDanhMuc";
            this.tlpDanhMuc.RowCount = 2;
            this.tlpDanhMuc.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpDanhMuc.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 252F));
            this.tlpDanhMuc.Size = new System.Drawing.Size(1407, 658);
            this.tlpDanhMuc.TabIndex = 0;
            // 
            // dgvPhuTung
            // 
            this.dgvPhuTung.AllowUserToAddRows = false;
            this.dgvPhuTung.AllowUserToDeleteRows = false;
            this.dgvPhuTung.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPhuTung.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(251)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPhuTung.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvPhuTung.ColumnHeadersHeight = 44;
            this.dgvPhuTung.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPhuTung.EnableHeadersVisualStyles = false;
            this.dgvPhuTung.Location = new System.Drawing.Point(0, 8);
            this.dgvPhuTung.Margin = new System.Windows.Forms.Padding(0, 8, 0, 5);
            this.dgvPhuTung.MultiSelect = false;
            this.dgvPhuTung.Name = "dgvPhuTung";
            this.dgvPhuTung.ReadOnly = true;
            this.dgvPhuTung.RowHeadersVisible = false;
            this.dgvPhuTung.RowHeadersWidth = 51;
            this.dgvPhuTung.RowTemplate.Height = 38;
            this.dgvPhuTung.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPhuTung.Size = new System.Drawing.Size(1407, 393);
            this.dgvPhuTung.TabIndex = 2;
            this.dgvPhuTung.CurrentCellChanged += new System.EventHandler(this.dgvPhuTung_ChonDong);
            // 
            // grbDieuChinh
            // 
            this.grbDieuChinh.Controls.Add(this.tlpDieuChinh);
            this.grbDieuChinh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbDieuChinh.Enabled = false;
            this.grbDieuChinh.Location = new System.Drawing.Point(0, 410);
            this.grbDieuChinh.Margin = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.grbDieuChinh.Name = "grbDieuChinh";
            this.grbDieuChinh.Padding = new System.Windows.Forms.Padding(12, 8, 12, 8);
            this.grbDieuChinh.Size = new System.Drawing.Size(1407, 248);
            this.grbDieuChinh.TabIndex = 3;
            this.grbDieuChinh.TabStop = false;
            this.grbDieuChinh.Text = "Điều chỉnh tồn và giá bán";
            // 
            // tlpDieuChinh
            // 
            this.tlpDieuChinh.ColumnCount = 4;
            this.tlpDieuChinh.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 170F));
            this.tlpDieuChinh.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpDieuChinh.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.tlpDieuChinh.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpDieuChinh.Controls.Add(this.lblMaDieuChinhTon, 0, 0);
            this.tlpDieuChinh.Controls.Add(this.txtMaDieuChinhTon, 1, 0);
            this.tlpDieuChinh.Controls.Add(this.lblMaDieuChinhGia, 2, 0);
            this.tlpDieuChinh.Controls.Add(this.txtMaDieuChinhGia, 3, 0);
            this.tlpDieuChinh.Controls.Add(this.lblSoLuongLech, 0, 1);
            this.tlpDieuChinh.Controls.Add(this.numSoLuongLech, 1, 1);
            this.tlpDieuChinh.Controls.Add(this.lblGiaBanMoi, 2, 1);
            this.tlpDieuChinh.Controls.Add(this.numGiaBanMoi, 3, 1);
            this.tlpDieuChinh.Controls.Add(this.lblLyDo, 0, 2);
            this.tlpDieuChinh.Controls.Add(this.txtLyDo, 1, 2);
            this.tlpDieuChinh.Controls.Add(this.flpDieuChinh, 0, 3);
            this.tlpDieuChinh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpDieuChinh.Location = new System.Drawing.Point(12, 31);
            this.tlpDieuChinh.Margin = new System.Windows.Forms.Padding(0);
            this.tlpDieuChinh.Name = "tlpDieuChinh";
            this.tlpDieuChinh.RowCount = 4;
            this.tlpDieuChinh.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.tlpDieuChinh.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.tlpDieuChinh.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 58F));
            this.tlpDieuChinh.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpDieuChinh.Size = new System.Drawing.Size(1383, 209);
            this.tlpDieuChinh.TabIndex = 0;
            // 
            // lblMaDieuChinhTon
            // 
            this.lblMaDieuChinhTon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMaDieuChinhTon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(85)))), ((int)(((byte)(130)))));
            this.lblMaDieuChinhTon.Location = new System.Drawing.Point(3, 0);
            this.lblMaDieuChinhTon.Name = "lblMaDieuChinhTon";
            this.lblMaDieuChinhTon.Size = new System.Drawing.Size(164, 44);
            this.lblMaDieuChinhTon.TabIndex = 0;
            this.lblMaDieuChinhTon.Text = "Mã điều chỉnh tồn";
            this.lblMaDieuChinhTon.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtMaDieuChinhTon
            // 
            this.txtMaDieuChinhTon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMaDieuChinhTon.Location = new System.Drawing.Point(174, 7);
            this.txtMaDieuChinhTon.Margin = new System.Windows.Forms.Padding(4, 7, 14, 7);
            this.txtMaDieuChinhTon.Name = "txtMaDieuChinhTon";
            this.txtMaDieuChinhTon.ReadOnly = true;
            this.txtMaDieuChinhTon.Size = new System.Drawing.Size(508, 30);
            this.txtMaDieuChinhTon.TabIndex = 1;
            this.txtMaDieuChinhTon.TabStop = false;
            // 
            // lblMaDieuChinhGia
            // 
            this.lblMaDieuChinhGia.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMaDieuChinhGia.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(85)))), ((int)(((byte)(130)))));
            this.lblMaDieuChinhGia.Location = new System.Drawing.Point(699, 0);
            this.lblMaDieuChinhGia.Name = "lblMaDieuChinhGia";
            this.lblMaDieuChinhGia.Size = new System.Drawing.Size(154, 44);
            this.lblMaDieuChinhGia.TabIndex = 2;
            this.lblMaDieuChinhGia.Text = "Mã điều chỉnh giá";
            this.lblMaDieuChinhGia.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtMaDieuChinhGia
            // 
            this.txtMaDieuChinhGia.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMaDieuChinhGia.Location = new System.Drawing.Point(860, 7);
            this.txtMaDieuChinhGia.Margin = new System.Windows.Forms.Padding(4, 7, 14, 7);
            this.txtMaDieuChinhGia.Name = "txtMaDieuChinhGia";
            this.txtMaDieuChinhGia.ReadOnly = true;
            this.txtMaDieuChinhGia.Size = new System.Drawing.Size(509, 30);
            this.txtMaDieuChinhGia.TabIndex = 3;
            this.txtMaDieuChinhGia.TabStop = false;
            // 
            // lblSoLuongLech
            // 
            this.lblSoLuongLech.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSoLuongLech.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(85)))), ((int)(((byte)(130)))));
            this.lblSoLuongLech.Location = new System.Drawing.Point(3, 44);
            this.lblSoLuongLech.Name = "lblSoLuongLech";
            this.lblSoLuongLech.Size = new System.Drawing.Size(164, 44);
            this.lblSoLuongLech.TabIndex = 4;
            this.lblSoLuongLech.Text = "Số lượng tăng / giảm";
            this.lblSoLuongLech.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // numSoLuongLech
            // 
            this.numSoLuongLech.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numSoLuongLech.Location = new System.Drawing.Point(174, 51);
            this.numSoLuongLech.Margin = new System.Windows.Forms.Padding(4, 7, 14, 7);
            this.numSoLuongLech.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.numSoLuongLech.Minimum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
            this.numSoLuongLech.Name = "numSoLuongLech";
            this.numSoLuongLech.Size = new System.Drawing.Size(508, 30);
            this.numSoLuongLech.TabIndex = 5;
            this.numSoLuongLech.ThousandsSeparator = true;
            // 
            // lblGiaBanMoi
            // 
            this.lblGiaBanMoi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblGiaBanMoi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(85)))), ((int)(((byte)(130)))));
            this.lblGiaBanMoi.Location = new System.Drawing.Point(699, 44);
            this.lblGiaBanMoi.Name = "lblGiaBanMoi";
            this.lblGiaBanMoi.Size = new System.Drawing.Size(154, 44);
            this.lblGiaBanMoi.TabIndex = 6;
            this.lblGiaBanMoi.Text = "Giá bán mới (đ)";
            this.lblGiaBanMoi.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // numGiaBanMoi
            // 
            this.numGiaBanMoi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numGiaBanMoi.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numGiaBanMoi.Location = new System.Drawing.Point(860, 51);
            this.numGiaBanMoi.Margin = new System.Windows.Forms.Padding(4, 7, 14, 7);
            this.numGiaBanMoi.Maximum = new decimal(new int[] {
            -1486618625,
            232830643,
            0,
            0});
            this.numGiaBanMoi.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numGiaBanMoi.Name = "numGiaBanMoi";
            this.numGiaBanMoi.Size = new System.Drawing.Size(509, 30);
            this.numGiaBanMoi.TabIndex = 7;
            this.numGiaBanMoi.ThousandsSeparator = true;
            this.numGiaBanMoi.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblLyDo
            // 
            this.lblLyDo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLyDo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(85)))), ((int)(((byte)(130)))));
            this.lblLyDo.Location = new System.Drawing.Point(3, 88);
            this.lblLyDo.Name = "lblLyDo";
            this.lblLyDo.Size = new System.Drawing.Size(164, 58);
            this.lblLyDo.TabIndex = 8;
            this.lblLyDo.Text = "Lý do *";
            this.lblLyDo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtLyDo
            // 
            this.tlpDieuChinh.SetColumnSpan(this.txtLyDo, 3);
            this.txtLyDo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLyDo.Location = new System.Drawing.Point(174, 95);
            this.txtLyDo.Margin = new System.Windows.Forms.Padding(4, 7, 14, 7);
            this.txtLyDo.MaxLength = 255;
            this.txtLyDo.Multiline = true;
            this.txtLyDo.Name = "txtLyDo";
            this.txtLyDo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLyDo.Size = new System.Drawing.Size(1195, 44);
            this.txtLyDo.TabIndex = 9;
            // 
            // flpDieuChinh
            // 
            this.flpDieuChinh.AutoScroll = true;
            this.tlpDieuChinh.SetColumnSpan(this.flpDieuChinh, 4);
            this.flpDieuChinh.Controls.Add(this.btnDieuChinhTon);
            this.flpDieuChinh.Controls.Add(this.btnDieuChinhGia);
            this.flpDieuChinh.Controls.Add(this.lblNguoiGhi);
            this.flpDieuChinh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpDieuChinh.Location = new System.Drawing.Point(0, 146);
            this.flpDieuChinh.Margin = new System.Windows.Forms.Padding(0);
            this.flpDieuChinh.Name = "flpDieuChinh";
            this.flpDieuChinh.Size = new System.Drawing.Size(1383, 63);
            this.flpDieuChinh.TabIndex = 10;
            this.flpDieuChinh.WrapContents = false;
            // 
            // btnDieuChinhTon
            // 
            this.btnDieuChinhTon.BackColor = System.Drawing.Color.White;
            this.btnDieuChinhTon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDieuChinhTon.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnDieuChinhTon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(85)))), ((int)(((byte)(130)))));
            this.btnDieuChinhTon.Location = new System.Drawing.Point(0, 6);
            this.btnDieuChinhTon.Margin = new System.Windows.Forms.Padding(0, 6, 12, 6);
            this.btnDieuChinhTon.Name = "btnDieuChinhTon";
            this.btnDieuChinhTon.Size = new System.Drawing.Size(210, 44);
            this.btnDieuChinhTon.TabIndex = 0;
            this.btnDieuChinhTon.Text = "Lưu điều chỉnh tồn";
            this.btnDieuChinhTon.UseVisualStyleBackColor = false;
            this.btnDieuChinhTon.Click += new System.EventHandler(this.btnDieuChinhTon_Bam);
            // 
            // btnDieuChinhGia
            // 
            this.btnDieuChinhGia.BackColor = System.Drawing.Color.White;
            this.btnDieuChinhGia.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDieuChinhGia.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnDieuChinhGia.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(85)))), ((int)(((byte)(130)))));
            this.btnDieuChinhGia.Location = new System.Drawing.Point(222, 6);
            this.btnDieuChinhGia.Margin = new System.Windows.Forms.Padding(0, 6, 12, 6);
            this.btnDieuChinhGia.Name = "btnDieuChinhGia";
            this.btnDieuChinhGia.Size = new System.Drawing.Size(195, 44);
            this.btnDieuChinhGia.TabIndex = 1;
            this.btnDieuChinhGia.Text = "Lưu giá bán mới";
            this.btnDieuChinhGia.UseVisualStyleBackColor = false;
            this.btnDieuChinhGia.Click += new System.EventHandler(this.btnDieuChinhGia_Bam);
            // 
            // lblNguoiGhi
            // 
            this.lblNguoiGhi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(85)))), ((int)(((byte)(130)))));
            this.lblNguoiGhi.Location = new System.Drawing.Point(437, 6);
            this.lblNguoiGhi.Margin = new System.Windows.Forms.Padding(8, 6, 0, 6);
            this.lblNguoiGhi.Name = "lblNguoiGhi";
            this.lblNguoiGhi.Size = new System.Drawing.Size(380, 44);
            this.lblNguoiGhi.TabIndex = 2;
            this.lblNguoiGhi.Text = "Người ghi:";
            this.lblNguoiGhi.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tabLichSuTon
            // 
            this.tabLichSuTon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(247)))), ((int)(((byte)(251)))));
            this.tabLichSuTon.Controls.Add(this.tlpLichSuTon);
            this.tabLichSuTon.Location = new System.Drawing.Point(4, 44);
            this.tabLichSuTon.Name = "tabLichSuTon";
            this.tabLichSuTon.Padding = new System.Windows.Forms.Padding(10);
            this.tabLichSuTon.Size = new System.Drawing.Size(192, 52);
            this.tabLichSuTon.TabIndex = 1;
            this.tabLichSuTon.Text = "Lịch sử tồn kho";
            // 
            // tlpLichSuTon
            // 
            this.tlpLichSuTon.ColumnCount = 1;
            this.tlpLichSuTon.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpLichSuTon.Controls.Add(this.dgvLichSuTon, 0, 0);
            this.tlpLichSuTon.Controls.Add(this.lblTongTienLech, 0, 1);
            this.tlpLichSuTon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpLichSuTon.Location = new System.Drawing.Point(10, 10);
            this.tlpLichSuTon.Margin = new System.Windows.Forms.Padding(0);
            this.tlpLichSuTon.Name = "tlpLichSuTon";
            this.tlpLichSuTon.RowCount = 2;
            this.tlpLichSuTon.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpLichSuTon.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlpLichSuTon.Size = new System.Drawing.Size(172, 32);
            this.tlpLichSuTon.TabIndex = 0;
            // 
            // dgvLichSuTon
            // 
            this.dgvLichSuTon.AllowUserToAddRows = false;
            this.dgvLichSuTon.AllowUserToDeleteRows = false;
            this.dgvLichSuTon.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLichSuTon.BackgroundColor = System.Drawing.Color.White;
            this.dgvLichSuTon.ColumnHeadersHeight = 44;
            this.dgvLichSuTon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLichSuTon.EnableHeadersVisualStyles = false;
            this.dgvLichSuTon.Location = new System.Drawing.Point(0, 0);
            this.dgvLichSuTon.Margin = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.dgvLichSuTon.MultiSelect = false;
            this.dgvLichSuTon.Name = "dgvLichSuTon";
            this.dgvLichSuTon.ReadOnly = true;
            this.dgvLichSuTon.RowHeadersVisible = false;
            this.dgvLichSuTon.RowHeadersWidth = 51;
            this.dgvLichSuTon.RowTemplate.Height = 38;
            this.dgvLichSuTon.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLichSuTon.Size = new System.Drawing.Size(172, 1);
            this.dgvLichSuTon.TabIndex = 0;
            // 
            // lblTongTienLech
            // 
            this.lblTongTienLech.AutoEllipsis = true;
            this.lblTongTienLech.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTongTienLech.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTongTienLech.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(85)))), ((int)(((byte)(130)))));
            this.lblTongTienLech.Location = new System.Drawing.Point(3, -18);
            this.lblTongTienLech.Name = "lblTongTienLech";
            this.lblTongTienLech.Size = new System.Drawing.Size(166, 50);
            this.lblTongTienLech.TabIndex = 1;
            this.lblTongTienLech.Text = "Thừa: 0 đ   |   Thiếu: 0 đ   |   Chênh lệch: 0 đ";
            this.lblTongTienLech.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tabLichSuGia
            // 
            this.tabLichSuGia.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(247)))), ((int)(((byte)(251)))));
            this.tabLichSuGia.Controls.Add(this.dgvLichSuGia);
            this.tabLichSuGia.Location = new System.Drawing.Point(4, 44);
            this.tabLichSuGia.Name = "tabLichSuGia";
            this.tabLichSuGia.Padding = new System.Windows.Forms.Padding(10);
            this.tabLichSuGia.Size = new System.Drawing.Size(192, 52);
            this.tabLichSuGia.TabIndex = 2;
            this.tabLichSuGia.Text = "Lịch sử giá bán";
            // 
            // dgvLichSuGia
            // 
            this.dgvLichSuGia.AllowUserToAddRows = false;
            this.dgvLichSuGia.AllowUserToDeleteRows = false;
            this.dgvLichSuGia.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLichSuGia.BackgroundColor = System.Drawing.Color.White;
            this.dgvLichSuGia.ColumnHeadersHeight = 44;
            this.dgvLichSuGia.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLichSuGia.EnableHeadersVisualStyles = false;
            this.dgvLichSuGia.Location = new System.Drawing.Point(10, 10);
            this.dgvLichSuGia.Margin = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.dgvLichSuGia.MultiSelect = false;
            this.dgvLichSuGia.Name = "dgvLichSuGia";
            this.dgvLichSuGia.ReadOnly = true;
            this.dgvLichSuGia.RowHeadersVisible = false;
            this.dgvLichSuGia.RowHeadersWidth = 51;
            this.dgvLichSuGia.RowTemplate.Height = 38;
            this.dgvLichSuGia.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLichSuGia.Size = new System.Drawing.Size(172, 32);
            this.dgvLichSuGia.TabIndex = 0;
            // 
            // flpThaoTac
            // 
            this.flpThaoTac.AutoScroll = true;
            this.flpThaoTac.Controls.Add(this.btnNhapPhuTung);
            this.flpThaoTac.Controls.Add(this.btnDanhSachPhieuNhap);
            this.flpThaoTac.Controls.Add(this.btnNhaCungCap);
            this.flpThaoTac.Controls.Add(this.btnNgungSuDung);
            this.flpThaoTac.Controls.Add(this.btnXuatExcel);
            this.flpThaoTac.Controls.Add(this.btnThoat);
            this.flpThaoTac.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpThaoTac.Location = new System.Drawing.Point(20, 890);
            this.flpThaoTac.Margin = new System.Windows.Forms.Padding(0);
            this.flpThaoTac.Name = "flpThaoTac";
            this.flpThaoTac.Size = new System.Drawing.Size(1435, 65);
            this.flpThaoTac.TabIndex = 3;
            this.flpThaoTac.WrapContents = false;
            // 
            // btnNhapPhuTung
            // 
            this.btnNhapPhuTung.BackColor = System.Drawing.Color.White;
            this.btnNhapPhuTung.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(207)))), ((int)(((byte)(220)))));
            this.btnNhapPhuTung.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNhapPhuTung.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnNhapPhuTung.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(85)))), ((int)(((byte)(130)))));
            this.btnNhapPhuTung.Location = new System.Drawing.Point(0, 8);
            this.btnNhapPhuTung.Margin = new System.Windows.Forms.Padding(0, 8, 12, 5);
            this.btnNhapPhuTung.Name = "btnNhapPhuTung";
            this.btnNhapPhuTung.Size = new System.Drawing.Size(180, 45);
            this.btnNhapPhuTung.TabIndex = 0;
            this.btnNhapPhuTung.Text = "Nhập phụ tùng";
            this.btnNhapPhuTung.UseVisualStyleBackColor = false;
            this.btnNhapPhuTung.Click += new System.EventHandler(this.btnNhapPhuTung_Bam);
            // 
            // btnDanhSachPhieuNhap
            // 
            this.btnDanhSachPhieuNhap.BackColor = System.Drawing.Color.White;
            this.btnDanhSachPhieuNhap.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(207)))), ((int)(((byte)(220)))));
            this.btnDanhSachPhieuNhap.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDanhSachPhieuNhap.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnDanhSachPhieuNhap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(85)))), ((int)(((byte)(130)))));
            this.btnDanhSachPhieuNhap.Location = new System.Drawing.Point(192, 8);
            this.btnDanhSachPhieuNhap.Margin = new System.Windows.Forms.Padding(0, 8, 12, 5);
            this.btnDanhSachPhieuNhap.Name = "btnDanhSachPhieuNhap";
            this.btnDanhSachPhieuNhap.Size = new System.Drawing.Size(180, 45);
            this.btnDanhSachPhieuNhap.TabIndex = 1;
            this.btnDanhSachPhieuNhap.Text = "Phiếu nhập kho";
            this.btnDanhSachPhieuNhap.UseVisualStyleBackColor = false;
            this.btnDanhSachPhieuNhap.Click += new System.EventHandler(this.btnDanhSachPhieuNhap_Bam);
            // 
            // btnNhaCungCap
            // 
            this.btnNhaCungCap.BackColor = System.Drawing.Color.White;
            this.btnNhaCungCap.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(207)))), ((int)(((byte)(220)))));
            this.btnNhaCungCap.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNhaCungCap.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnNhaCungCap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(85)))), ((int)(((byte)(130)))));
            this.btnNhaCungCap.Location = new System.Drawing.Point(384, 8);
            this.btnNhaCungCap.Margin = new System.Windows.Forms.Padding(0, 8, 12, 5);
            this.btnNhaCungCap.Name = "btnNhaCungCap";
            this.btnNhaCungCap.Size = new System.Drawing.Size(180, 45);
            this.btnNhaCungCap.TabIndex = 2;
            this.btnNhaCungCap.Text = "Nhà cung cấp";
            this.btnNhaCungCap.UseVisualStyleBackColor = false;
            this.btnNhaCungCap.Click += new System.EventHandler(this.btnNhaCungCap_Bam);
            // 
            // btnNgungSuDung
            // 
            this.btnNgungSuDung.BackColor = System.Drawing.Color.White;
            this.btnNgungSuDung.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(207)))), ((int)(((byte)(220)))));
            this.btnNgungSuDung.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNgungSuDung.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnNgungSuDung.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(85)))), ((int)(((byte)(130)))));
            this.btnNgungSuDung.Location = new System.Drawing.Point(576, 8);
            this.btnNgungSuDung.Margin = new System.Windows.Forms.Padding(0, 8, 12, 5);
            this.btnNgungSuDung.Name = "btnNgungSuDung";
            this.btnNgungSuDung.Size = new System.Drawing.Size(180, 45);
            this.btnNgungSuDung.TabIndex = 3;
            this.btnNgungSuDung.Text = "Ngừng / Dùng lại";
            this.btnNgungSuDung.UseVisualStyleBackColor = false;
            this.btnNgungSuDung.Click += new System.EventHandler(this.btnNgungSuDung_Bam);
            // 
            // btnThoat
            // 
            this.btnThoat.BackColor = System.Drawing.Color.White;
            this.btnThoat.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(207)))), ((int)(((byte)(220)))));
            this.btnThoat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThoat.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnThoat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(85)))), ((int)(((byte)(130)))));
            this.btnThoat.Location = new System.Drawing.Point(960, 8);
            this.btnThoat.Margin = new System.Windows.Forms.Padding(0, 8, 12, 5);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(180, 45);
            this.btnThoat.TabIndex = 4;
            this.btnThoat.Text = "Đóng kho";
            this.btnThoat.UseVisualStyleBackColor = false;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Bam_1);
            // 
            // btnXuatExcel
            // 
            this.btnXuatExcel.BackColor = System.Drawing.Color.White;
            this.btnXuatExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXuatExcel.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnXuatExcel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(85)))), ((int)(((byte)(130)))));
            this.btnXuatExcel.Location = new System.Drawing.Point(768, 6);
            this.btnXuatExcel.Margin = new System.Windows.Forms.Padding(0, 6, 12, 6);
            this.btnXuatExcel.Name = "btnXuatExcel";
            this.btnXuatExcel.Size = new System.Drawing.Size(180, 44);
            this.btnXuatExcel.TabIndex = 5;
            this.btnXuatExcel.Text = "Xuất Excel kho";
            this.btnXuatExcel.UseVisualStyleBackColor = false;
            this.btnXuatExcel.Click += new System.EventHandler(this.btnXuatExcel_Bam);
            // 
            // frmKho
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(247)))), ((int)(((byte)(251)))));
            this.ClientSize = new System.Drawing.Size(1475, 975);
            this.Controls.Add(this.tlpTrang);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(1196, 801);
            this.Name = "frmKho";
            this.Text = "Kho phụ tùng";
            this.tlpTrang.ResumeLayout(false);
            this.tlpTimKiem.ResumeLayout(false);
            this.tlpTimKiem.PerformLayout();
            this.tabKho.ResumeLayout(false);
            this.tabPhuTung.ResumeLayout(false);
            this.tlpDanhMuc.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhuTung)).EndInit();
            this.grbDieuChinh.ResumeLayout(false);
            this.tlpDieuChinh.ResumeLayout(false);
            this.tlpDieuChinh.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSoLuongLech)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numGiaBanMoi)).EndInit();
            this.flpDieuChinh.ResumeLayout(false);
            this.tabLichSuTon.ResumeLayout(false);
            this.tlpLichSuTon.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLichSuTon)).EndInit();
            this.tabLichSuGia.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLichSuGia)).EndInit();
            this.flpThaoTac.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        private System.Windows.Forms.TableLayoutPanel tlpTrang;
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.TableLayoutPanel tlpTimKiem;
        private System.Windows.Forms.Label lblTimPhuTung;
        private System.Windows.Forms.TextBox txtTimPhuTung;
        private System.Windows.Forms.DataGridView dgvPhuTung;
        private System.Windows.Forms.Button btnNhapPhuTung;
        private System.Windows.Forms.Button btnDanhSachPhieuNhap;
        private System.Windows.Forms.Button btnNhaCungCap;
        private System.Windows.Forms.Button btnNgungSuDung;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.FlowLayoutPanel flpThaoTac;
        private System.Windows.Forms.TabControl tabKho;
        private System.Windows.Forms.TabPage tabPhuTung;
        private System.Windows.Forms.TabPage tabLichSuTon;
        private System.Windows.Forms.TabPage tabLichSuGia;
        private System.Windows.Forms.TableLayoutPanel tlpDanhMuc;
        private System.Windows.Forms.GroupBox grbDieuChinh;
        private System.Windows.Forms.TableLayoutPanel tlpDieuChinh;
        private System.Windows.Forms.Label lblMaDieuChinhTon;
        private System.Windows.Forms.TextBox txtMaDieuChinhTon;
        private System.Windows.Forms.Label lblMaDieuChinhGia;
        private System.Windows.Forms.TextBox txtMaDieuChinhGia;
        private System.Windows.Forms.Label lblSoLuongLech;
        private System.Windows.Forms.NumericUpDown numSoLuongLech;
        private System.Windows.Forms.Label lblGiaBanMoi;
        private System.Windows.Forms.NumericUpDown numGiaBanMoi;
        private System.Windows.Forms.Label lblLyDo;
        private System.Windows.Forms.TextBox txtLyDo;
        private System.Windows.Forms.FlowLayoutPanel flpDieuChinh;
        private System.Windows.Forms.Button btnDieuChinhTon;
        private System.Windows.Forms.Button btnDieuChinhGia;
        private System.Windows.Forms.Label lblNguoiGhi;
        private System.Windows.Forms.TableLayoutPanel tlpLichSuTon;
        private System.Windows.Forms.DataGridView dgvLichSuTon;
        private System.Windows.Forms.Label lblTongTienLech;
        private System.Windows.Forms.DataGridView dgvLichSuGia;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Button btnXuatExcel;
    }
}
