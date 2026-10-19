namespace QLyCuaHangSuaXe.DanhMuc.TiepNhan
{
    partial class frmTiepNhan
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle styleNgayNhan = new System.Windows.Forms.DataGridViewCellStyle();
            this.tlpNoiDung = new System.Windows.Forms.TableLayoutPanel();
            this.pnlTimKiem = new System.Windows.Forms.Panel();
            this.tlpTimKiem = new System.Windows.Forms.TableLayoutPanel();
            this.tlpThongTin = new System.Windows.Forms.TableLayoutPanel();
            this.tlpThaoTac = new System.Windows.Forms.TableLayoutPanel();
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.lblDuongKe = new System.Windows.Forms.Label();
            this.lblTimKiem = new System.Windows.Forms.Label();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.lblMaPhieu = new System.Windows.Forms.Label();
            this.txtMaPhieu = new System.Windows.Forms.TextBox();
            this.lblNgayNhan = new System.Windows.Forms.Label();
            this.txtNgayNhan = new System.Windows.Forms.TextBox();
            this.lblBienSoXe = new System.Windows.Forms.Label();
            this.txtBienSoXe = new System.Windows.Forms.TextBox();
            this.lblLoaiXe = new System.Windows.Forms.Label();
            this.cboLoaiXe = new System.Windows.Forms.ComboBox();
            this.lblKhachHang = new System.Windows.Forms.Label();
            this.txtKhachHang = new System.Windows.Forms.TextBox();
            this.lblDienThoai = new System.Windows.Forms.Label();
            this.txtDienThoai = new System.Windows.Forms.TextBox();
            this.lblThoPhuTrach = new System.Windows.Forms.Label();
            this.cboThoPhuTrach = new System.Windows.Forms.ComboBox();
            this.lblTrangThai = new System.Windows.Forms.Label();
            this.cboTrangThai = new System.Windows.Forms.ComboBox();
            this.lblTinhTrangXe = new System.Windows.Forms.Label();
            this.rtbTinhTrangXe = new System.Windows.Forms.RichTextBox();
            this.btnThemMoi = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnLuu = new System.Windows.Forms.Button();
            this.btnThoat = new System.Windows.Forms.Button();
            this.lblThongBao = new System.Windows.Forms.Label();
            this.lblDanhSachPhieu = new System.Windows.Forms.Label();
            this.dgvPhieu = new System.Windows.Forms.DataGridView();
            this.colMaPhieu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNgayNhan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBienSoXe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colKhachHang = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLoaiXe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTenTho = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTrangThai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tlpNoiDung.SuspendLayout();
            this.pnlTimKiem.SuspendLayout();
            this.tlpTimKiem.SuspendLayout();
            this.tlpThongTin.SuspendLayout();
            this.tlpThaoTac.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhieu)).BeginInit();
            this.SuspendLayout();
            // lblTieuDe
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "TIẾP NHẬN XE";
            // lblTimKiem
            this.lblTimKiem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTimKiem.Name = "lblTimKiem";
            this.lblTimKiem.TabIndex = 1;
            this.lblTimKiem.Text = "Tìm phiếu:";
            // txtTimKiem
            this.txtTimKiem.AutoSize = true;
            this.txtTimKiem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTimKiem.BackColor = System.Drawing.Color.White;
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.TabIndex = 2;
            this.txtTimKiem.MaxLength = 100;
            // btnTimKiem
            this.btnTimKiem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTimKiem.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(189, 197, 206);
            this.btnTimKiem.FlatAppearance.BorderSize = 1;
            this.btnTimKiem.BackColor = System.Drawing.Color.FromArgb(242, 245, 248);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.TabIndex = 3;
            this.btnTimKiem.Text = "Tìm kiếm";
            this.btnTimKiem.UseVisualStyleBackColor = false;
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            // lblDuongKe
            this.lblDuongKe.Name = "lblDuongKe";
            this.lblDuongKe.BackColor = System.Drawing.Color.FromArgb(189, 197, 206);
            // lblMaPhieu
            this.lblMaPhieu.Name = "lblMaPhieu";
            this.lblMaPhieu.TabIndex = 4;
            this.lblMaPhieu.Text = "Mã phiếu";
            // txtMaPhieu
            this.txtMaPhieu.AutoSize = true;
            this.txtMaPhieu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMaPhieu.BackColor = System.Drawing.Color.FromArgb(235, 241, 247);
            this.txtMaPhieu.ForeColor = System.Drawing.Color.FromArgb(93, 103, 113);
            this.txtMaPhieu.Name = "txtMaPhieu";
            this.txtMaPhieu.TabIndex = 5;
            this.txtMaPhieu.MaxLength = 100;
            this.txtMaPhieu.ReadOnly = true;
            this.txtMaPhieu.TabStop = false;
            // lblNgayNhan
            this.lblNgayNhan.Name = "lblNgayNhan";
            this.lblNgayNhan.TabIndex = 6;
            this.lblNgayNhan.Text = "Ngày nhận";
            // txtNgayNhan
            this.txtNgayNhan.AutoSize = true;
            this.txtNgayNhan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNgayNhan.BackColor = System.Drawing.Color.FromArgb(235, 241, 247);
            this.txtNgayNhan.ForeColor = System.Drawing.Color.FromArgb(93, 103, 113);
            this.txtNgayNhan.ReadOnly = true;
            this.txtNgayNhan.Name = "txtNgayNhan";
            this.txtNgayNhan.TabIndex = 7;
            // lblBienSoXe
            this.lblBienSoXe.Name = "lblBienSoXe";
            this.lblBienSoXe.TabIndex = 8;
            this.lblBienSoXe.Text = "Biển số xe *";
            // txtBienSoXe
            this.txtBienSoXe.AutoSize = true;
            this.txtBienSoXe.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBienSoXe.BackColor = System.Drawing.Color.White;
            this.txtBienSoXe.Name = "txtBienSoXe";
            this.txtBienSoXe.TabIndex = 9;
            this.txtBienSoXe.MaxLength = 20;
            // lblLoaiXe
            this.lblLoaiXe.Name = "lblLoaiXe";
            this.lblLoaiXe.TabIndex = 10;
            this.lblLoaiXe.Text = "Loại xe";
            // cboLoaiXe
            this.cboLoaiXe.Name = "cboLoaiXe";
            this.cboLoaiXe.TabIndex = 11;
            this.cboLoaiXe.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoaiXe.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.cboLoaiXe.BackColor = System.Drawing.Color.White;
            this.cboLoaiXe.FormattingEnabled = true;
            // lblKhachHang
            this.lblKhachHang.Name = "lblKhachHang";
            this.lblKhachHang.TabIndex = 12;
            this.lblKhachHang.Text = "Khách hàng";
            // txtKhachHang
            this.txtKhachHang.AutoSize = true;
            this.txtKhachHang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtKhachHang.BackColor = System.Drawing.Color.White;
            this.txtKhachHang.Name = "txtKhachHang";
            this.txtKhachHang.TabIndex = 13;
            this.txtKhachHang.MaxLength = 100;
            // lblDienThoai
            this.lblDienThoai.Name = "lblDienThoai";
            this.lblDienThoai.TabIndex = 14;
            this.lblDienThoai.Text = "Điện thoại";
            // txtDienThoai
            this.txtDienThoai.AutoSize = true;
            this.txtDienThoai.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDienThoai.BackColor = System.Drawing.Color.White;
            this.txtDienThoai.Name = "txtDienThoai";
            this.txtDienThoai.TabIndex = 15;
            this.txtDienThoai.MaxLength = 15;
            // lblThoPhuTrach
            this.lblThoPhuTrach.Name = "lblThoPhuTrach";
            this.lblThoPhuTrach.TabIndex = 16;
            this.lblThoPhuTrach.Text = "Thợ phụ trách";
            // cboThoPhuTrach
            this.cboThoPhuTrach.Name = "cboThoPhuTrach";
            this.cboThoPhuTrach.TabIndex = 17;
            this.cboThoPhuTrach.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboThoPhuTrach.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.cboThoPhuTrach.BackColor = System.Drawing.Color.White;
            this.cboThoPhuTrach.FormattingEnabled = true;
            // lblTrangThai
            this.lblTrangThai.Name = "lblTrangThai";
            this.lblTrangThai.TabIndex = 18;
            this.lblTrangThai.Text = "Trạng thái";
            // cboTrangThai
            this.cboTrangThai.Name = "cboTrangThai";
            this.cboTrangThai.TabIndex = 19;
            this.cboTrangThai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTrangThai.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.cboTrangThai.BackColor = System.Drawing.Color.White;
            this.cboTrangThai.FormattingEnabled = true;
            // lblTinhTrangXe
            this.lblTinhTrangXe.Name = "lblTinhTrangXe";
            this.lblTinhTrangXe.TabIndex = 20;
            this.lblTinhTrangXe.Text = "Tình trạng xe / lỗi ban đầu";
            // rtbTinhTrangXe
            this.rtbTinhTrangXe.Name = "rtbTinhTrangXe";
            this.rtbTinhTrangXe.TabIndex = 21;
            this.rtbTinhTrangXe.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtbTinhTrangXe.BackColor = System.Drawing.Color.White;
            this.rtbTinhTrangXe.DetectUrls = false;
            this.rtbTinhTrangXe.MaxLength = 1000;
            this.rtbTinhTrangXe.Text = "";
            // btnThemMoi
            this.btnThemMoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThemMoi.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(189, 197, 206);
            this.btnThemMoi.FlatAppearance.BorderSize = 1;
            this.btnThemMoi.BackColor = System.Drawing.Color.FromArgb(242, 245, 248);
            this.btnThemMoi.Name = "btnThemMoi";
            this.btnThemMoi.TabIndex = 22;
            this.btnThemMoi.Text = "Thêm mới";
            this.btnThemMoi.UseVisualStyleBackColor = false;
            this.btnThemMoi.Click += new System.EventHandler(this.btnThemMoi_Click);
            // btnSua
            this.btnSua.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSua.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(189, 197, 206);
            this.btnSua.FlatAppearance.BorderSize = 1;
            this.btnSua.BackColor = System.Drawing.Color.FromArgb(242, 245, 248);
            this.btnSua.Name = "btnSua";
            this.btnSua.TabIndex = 23;
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = false;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // btnXoa
            this.btnXoa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXoa.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(189, 197, 206);
            this.btnXoa.FlatAppearance.BorderSize = 1;
            this.btnXoa.BackColor = System.Drawing.Color.FromArgb(242, 245, 248);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.TabIndex = 24;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = false;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // btnLuu
            this.btnLuu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLuu.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(30, 108, 176);
            this.btnLuu.FlatAppearance.BorderSize = 1;
            this.btnLuu.BackColor = System.Drawing.Color.FromArgb(30, 108, 176);
            this.btnLuu.ForeColor = System.Drawing.Color.White;
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.TabIndex = 25;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.UseVisualStyleBackColor = false;
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // btnThoat
            this.btnThoat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThoat.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(189, 197, 206);
            this.btnThoat.FlatAppearance.BorderSize = 1;
            this.btnThoat.BackColor = System.Drawing.Color.FromArgb(242, 245, 248);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.TabIndex = 26;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.UseVisualStyleBackColor = false;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // lblThongBao
            this.lblThongBao.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblThongBao.AutoEllipsis = false;
            this.lblThongBao.Name = "lblThongBao";
            this.lblThongBao.TabIndex = 27;
            this.lblThongBao.Text = "";
            // lblDanhSachPhieu
            this.lblDanhSachPhieu.Name = "lblDanhSachPhieu";
            this.lblDanhSachPhieu.TabIndex = 28;
            this.lblDanhSachPhieu.Text = "Danh sách phiếu đã lưu";
            // dgvPhieu
            this.dgvPhieu.ColumnHeadersHeight = 42;
            this.dgvPhieu.GridColor = System.Drawing.Color.FromArgb(189, 197, 206);
            this.dgvPhieu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dgvPhieu.EnableHeadersVisualStyles = false;
            this.dgvPhieu.AllowUserToResizeRows = false;
            this.dgvPhieu.Name = "dgvPhieu";
            this.dgvPhieu.TabIndex = 29;
            this.lblTieuDe.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this.lblTieuDe.ForeColor = System.Drawing.Color.FromArgb(30, 108, 176);
            this.lblTieuDe.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            this.txtNgayNhan.TabStop = false;
            this.cboLoaiXe.Items.AddRange(new object[] { "Xe số", "Xe ga", "Xe côn tay" });
            this.cboTrangThai.Items.AddRange(new object[] { "Chờ khám", "Đang sửa", "Hoàn thành" });
            this.lblThongBao.ForeColor = System.Drawing.Color.FromArgb(30, 108, 176);
            this.lblDanhSachPhieu.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.dgvPhieu.AllowUserToAddRows = false;
            this.dgvPhieu.AllowUserToDeleteRows = false;
            this.dgvPhieu.AutoGenerateColumns = false;
            this.dgvPhieu.BackgroundColor = System.Drawing.Color.White;
            this.dgvPhieu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvPhieu.MultiSelect = false;
            this.dgvPhieu.ReadOnly = true;
            this.dgvPhieu.RowHeadersVisible = false;
            this.dgvPhieu.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPhieu.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.colMaPhieu, this.colNgayNhan, this.colBienSoXe, this.colKhachHang, this.colLoaiXe, this.colTenTho, this.colTrangThai });
            this.dgvPhieu.RowTemplate.Height = 40;
            this.dgvPhieu.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(235, 241, 247);
            this.dgvPhieu.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.dgvPhieu.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.dgvPhieu.ColumnHeadersDefaultCellStyle.Padding = new System.Windows.Forms.Padding(8, 0, 4, 0);
            this.dgvPhieu.ColumnHeadersDefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPhieu.DefaultCellStyle.Padding = new System.Windows.Forms.Padding(8, 0, 4, 0);
            this.dgvPhieu.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.dgvPhieu.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.dgvPhieu.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(222, 237, 250);
            this.dgvPhieu.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvPhieu.CurrentCellChanged += new System.EventHandler(this.dgvPhieu_CurrentCellChanged);
            styleNgayNhan.Format = "dd/MM HH:mm";
            // colMaPhieu
            this.colMaPhieu.DataPropertyName = "MaPhieu";
            this.colMaPhieu.HeaderText = "Mã phiếu";
            this.colMaPhieu.Name = "colMaPhieu";
            this.colMaPhieu.FillWeight = 90F;
            this.colMaPhieu.MinimumWidth = 90;
            this.colMaPhieu.ReadOnly = true;
            this.colMaPhieu.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;

            // colNgayNhan
            this.colNgayNhan.DataPropertyName = "NgayNhan";
            this.colNgayNhan.HeaderText = "Ngày nhận";
            this.colNgayNhan.Name = "colNgayNhan";
            this.colNgayNhan.FillWeight = 122F;
            this.colNgayNhan.MinimumWidth = 120;
            this.colNgayNhan.ReadOnly = true;
            this.colNgayNhan.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;

            this.colNgayNhan.DefaultCellStyle = styleNgayNhan;
            // colBienSoXe
            this.colBienSoXe.DataPropertyName = "BienSoXe";
            this.colBienSoXe.HeaderText = "Biển số xe";
            this.colBienSoXe.Name = "colBienSoXe";
            this.colBienSoXe.FillWeight = 122F;
            this.colBienSoXe.MinimumWidth = 120;
            this.colBienSoXe.ReadOnly = true;
            this.colBienSoXe.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;

            // colKhachHang
            this.colKhachHang.DataPropertyName = "KhachHang";
            this.colKhachHang.HeaderText = "Khách hàng";
            this.colKhachHang.Name = "colKhachHang";
            this.colKhachHang.FillWeight = 156F;
            this.colKhachHang.MinimumWidth = 145;
            this.colKhachHang.ReadOnly = true;
            this.colKhachHang.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;


            // colLoaiXe
            this.colLoaiXe.DataPropertyName = "LoaiXe";
            this.colLoaiXe.HeaderText = "Loại xe";
            this.colLoaiXe.Name = "colLoaiXe";
            this.colLoaiXe.FillWeight = 95F;
            this.colLoaiXe.MinimumWidth = 105;
            this.colLoaiXe.ReadOnly = true;
            this.colLoaiXe.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;

            // colTenTho
            this.colTenTho.DataPropertyName = "TenTho";
            this.colTenTho.HeaderText = "Thợ phụ trách";
            this.colTenTho.Name = "colTenTho";
            this.colTenTho.FillWeight = 176F;
            this.colTenTho.MinimumWidth = 155;
            this.colTenTho.ReadOnly = true;
            this.colTenTho.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;

            // colTrangThai
            this.colTrangThai.DataPropertyName = "TrangThai";
            this.colTrangThai.HeaderText = "Trạng thái";
            this.colTrangThai.Name = "colTrangThai";
            this.colTrangThai.FillWeight = 150F;
            this.colTrangThai.MinimumWidth = 125;
            this.colTrangThai.ReadOnly = true;
            this.colTrangThai.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // Bố cục chung: các vùng xếp theo hàng, bảng phiếu giãn theo cửa sổ.
            this.tlpNoiDung.ColumnCount = 1;
            this.tlpNoiDung.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpNoiDung.RowCount = 6;
            this.tlpNoiDung.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tlpNoiDung.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 62F));
            this.tlpNoiDung.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 244F));
            this.tlpNoiDung.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tlpNoiDung.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tlpNoiDung.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpNoiDung.Controls.Add(this.lblTieuDe, 0, 0);
            this.tlpNoiDung.Controls.Add(this.pnlTimKiem, 0, 1);
            this.tlpNoiDung.Controls.Add(this.tlpThongTin, 0, 2);
            this.tlpNoiDung.Controls.Add(this.tlpThaoTac, 0, 3);
            this.tlpNoiDung.Controls.Add(this.lblDanhSachPhieu, 0, 4);
            this.tlpNoiDung.Controls.Add(this.dgvPhieu, 0, 5);
            this.tlpNoiDung.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpNoiDung.Padding = new System.Windows.Forms.Padding(24);
            this.tlpNoiDung.Name = "tlpNoiDung";
            this.tlpNoiDung.TabIndex = 0;
            this.lblTieuDe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTieuDe.Margin = new System.Windows.Forms.Padding(0);
            // Thanh tìm kiếm
            this.pnlTimKiem.Controls.Add(this.tlpTimKiem);
            this.pnlTimKiem.Controls.Add(this.lblDuongKe);
            this.pnlTimKiem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTimKiem.Margin = new System.Windows.Forms.Padding(0, 5, 0, 14);
            this.pnlTimKiem.Name = "pnlTimKiem";
            this.tlpTimKiem.ColumnCount = 3;
            this.tlpTimKiem.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tlpTimKiem.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTimKiem.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 114F));
            this.tlpTimKiem.RowCount = 1;
            this.tlpTimKiem.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTimKiem.Controls.Add(this.lblTimKiem, 0, 0);
            this.tlpTimKiem.Controls.Add(this.txtTimKiem, 1, 0);
            this.tlpTimKiem.Controls.Add(this.btnTimKiem, 2, 0);
            this.tlpTimKiem.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpTimKiem.Height = 36;
            this.tlpTimKiem.Name = "tlpTimKiem";
            this.lblTimKiem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTimKiem.Margin = new System.Windows.Forms.Padding(0);
            this.txtTimKiem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTimKiem.Margin = new System.Windows.Forms.Padding(0, 5, 12, 0);
            this.btnTimKiem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnTimKiem.Margin = new System.Windows.Forms.Padding(0);
            this.lblDuongKe.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblDuongKe.Height = 1;
            // Bốn cột thông tin luôn rộng bằng nhau khi thay đổi kích thước form.
            this.tlpThongTin.ColumnCount = 4;
            this.tlpThongTin.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpThongTin.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpThongTin.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpThongTin.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpThongTin.RowCount = 6;
            this.tlpThongTin.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tlpThongTin.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.tlpThongTin.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tlpThongTin.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.tlpThongTin.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tlpThongTin.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpThongTin.Controls.Add(this.lblMaPhieu, 0, 0);
            this.tlpThongTin.Controls.Add(this.lblNgayNhan, 1, 0);
            this.tlpThongTin.Controls.Add(this.lblBienSoXe, 2, 0);
            this.tlpThongTin.Controls.Add(this.lblLoaiXe, 3, 0);
            this.tlpThongTin.Controls.Add(this.txtMaPhieu, 0, 1);
            this.tlpThongTin.Controls.Add(this.txtNgayNhan, 1, 1);
            this.tlpThongTin.Controls.Add(this.txtBienSoXe, 2, 1);
            this.tlpThongTin.Controls.Add(this.cboLoaiXe, 3, 1);
            this.tlpThongTin.Controls.Add(this.lblKhachHang, 0, 2);
            this.tlpThongTin.Controls.Add(this.lblDienThoai, 1, 2);
            this.tlpThongTin.Controls.Add(this.lblThoPhuTrach, 2, 2);
            this.tlpThongTin.Controls.Add(this.lblTrangThai, 3, 2);
            this.tlpThongTin.Controls.Add(this.txtKhachHang, 0, 3);
            this.tlpThongTin.Controls.Add(this.txtDienThoai, 1, 3);
            this.tlpThongTin.Controls.Add(this.cboThoPhuTrach, 2, 3);
            this.tlpThongTin.Controls.Add(this.cboTrangThai, 3, 3);
            this.tlpThongTin.Controls.Add(this.lblTinhTrangXe, 0, 4);
            this.tlpThongTin.Controls.Add(this.rtbTinhTrangXe, 0, 5);
            this.tlpThongTin.SetColumnSpan(this.lblTinhTrangXe, 4);
            this.tlpThongTin.SetColumnSpan(this.rtbTinhTrangXe, 4);
            this.tlpThongTin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpThongTin.Margin = new System.Windows.Forms.Padding(0);
            this.tlpThongTin.Name = "tlpThongTin";
            this.tlpThongTin.TabIndex = 2;
            this.lblMaPhieu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMaPhieu.Margin = new System.Windows.Forms.Padding(0);
            this.lblMaPhieu.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblNgayNhan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNgayNhan.Margin = new System.Windows.Forms.Padding(0);
            this.lblNgayNhan.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblBienSoXe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblBienSoXe.Margin = new System.Windows.Forms.Padding(0);
            this.lblBienSoXe.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblLoaiXe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLoaiXe.Margin = new System.Windows.Forms.Padding(0);
            this.lblLoaiXe.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblKhachHang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblKhachHang.Margin = new System.Windows.Forms.Padding(0);
            this.lblKhachHang.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblDienThoai.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDienThoai.Margin = new System.Windows.Forms.Padding(0);
            this.lblDienThoai.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblThoPhuTrach.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblThoPhuTrach.Margin = new System.Windows.Forms.Padding(0);
            this.lblThoPhuTrach.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTrangThai.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTrangThai.Margin = new System.Windows.Forms.Padding(0);
            this.lblTrangThai.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTinhTrangXe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTinhTrangXe.Margin = new System.Windows.Forms.Padding(0);
            this.lblTinhTrangXe.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtMaPhieu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMaPhieu.Margin = new System.Windows.Forms.Padding(0, 2, 16, 10);
            this.txtNgayNhan.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNgayNhan.Margin = new System.Windows.Forms.Padding(0, 2, 16, 10);
            this.txtBienSoXe.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBienSoXe.Margin = new System.Windows.Forms.Padding(0, 2, 16, 10);
            this.cboLoaiXe.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.cboLoaiXe.Margin = new System.Windows.Forms.Padding(0, 2, 0, 10);
            this.txtKhachHang.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.txtKhachHang.Margin = new System.Windows.Forms.Padding(0, 2, 16, 10);
            this.txtDienThoai.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDienThoai.Margin = new System.Windows.Forms.Padding(0, 2, 16, 10);
            this.cboThoPhuTrach.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.cboThoPhuTrach.Margin = new System.Windows.Forms.Padding(0, 2, 16, 10);
            this.cboTrangThai.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.cboTrangThai.Margin = new System.Windows.Forms.Padding(0, 2, 0, 10);
            this.rtbTinhTrangXe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbTinhTrangXe.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            // Nút thao tác nằm bên phải, thông báo tự xuống dòng bên trái.
            this.tlpThaoTac.ColumnCount = 6;
            this.tlpThaoTac.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpThaoTac.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 108F));
            this.tlpThaoTac.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tlpThaoTac.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tlpThaoTac.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tlpThaoTac.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tlpThaoTac.RowCount = 1;
            this.tlpThaoTac.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpThaoTac.Controls.Add(this.lblThongBao, 0, 0);
            this.tlpThaoTac.Controls.Add(this.btnThemMoi, 1, 0);
            this.tlpThaoTac.Controls.Add(this.btnSua, 2, 0);
            this.tlpThaoTac.Controls.Add(this.btnXoa, 3, 0);
            this.tlpThaoTac.Controls.Add(this.btnLuu, 4, 0);
            this.tlpThaoTac.Controls.Add(this.btnThoat, 5, 0);
            this.tlpThaoTac.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpThaoTac.Margin = new System.Windows.Forms.Padding(0);
            this.tlpThaoTac.Name = "tlpThaoTac";
            this.tlpThaoTac.TabIndex = 3;
            this.lblThongBao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblThongBao.Margin = new System.Windows.Forms.Padding(0, 7, 12, 7);
            this.lblThongBao.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnThemMoi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnThemMoi.Margin = new System.Windows.Forms.Padding(8, 12, 0, 12);
            this.btnSua.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSua.Margin = new System.Windows.Forms.Padding(8, 12, 0, 12);
            this.btnXoa.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnXoa.Margin = new System.Windows.Forms.Padding(8, 12, 0, 12);
            this.btnLuu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnLuu.Margin = new System.Windows.Forms.Padding(8, 12, 0, 12);
            this.btnThoat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnThoat.Margin = new System.Windows.Forms.Padding(8, 12, 0, 12);
            this.btnLuu.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblDanhSachPhieu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDanhSachPhieu.Margin = new System.Windows.Forms.Padding(0);
            this.lblDanhSachPhieu.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblDanhSachPhieu.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.dgvPhieu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPhieu.Margin = new System.Windows.Forms.Padding(0);
            this.dgvPhieu.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.dgvPhieu.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvPhieu.GridColor = System.Drawing.Color.FromArgb(223, 230, 237);
            this.dgvPhieu.ColumnHeadersDefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(235, 241, 247);
            this.dgvPhieu.ColumnHeadersDefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.Controls.Add(this.tlpNoiDung);
            // frmTiepNhan
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = false;
            this.BackColor = System.Drawing.Color.FromArgb(250, 252, 254);
            this.ClientSize = new System.Drawing.Size(1080, 760);
            this.MinimumSize = new System.Drawing.Size(964, 720);
            this.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmTiepNhan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Tiếp nhận xe";
            this.Load += new System.EventHandler(this.frmTiepNhan_Load);
            this.tlpNoiDung.ResumeLayout(false);
            this.pnlTimKiem.ResumeLayout(false);
            this.tlpTimKiem.ResumeLayout(false);
            this.tlpTimKiem.PerformLayout();
            this.tlpThongTin.ResumeLayout(false);
            this.tlpThongTin.PerformLayout();
            this.tlpThaoTac.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhieu)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
            this.dgvPhieu.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpNoiDung;
        private System.Windows.Forms.Panel pnlTimKiem;
        private System.Windows.Forms.TableLayoutPanel tlpTimKiem;
        private System.Windows.Forms.TableLayoutPanel tlpThongTin;
        private System.Windows.Forms.TableLayoutPanel tlpThaoTac;
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.Label lblDuongKe;
        private System.Windows.Forms.Label lblTimKiem;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.Label lblMaPhieu;
        private System.Windows.Forms.TextBox txtMaPhieu;
        private System.Windows.Forms.Label lblNgayNhan;
        private System.Windows.Forms.TextBox txtNgayNhan;
        private System.Windows.Forms.Label lblBienSoXe;
        private System.Windows.Forms.TextBox txtBienSoXe;
        private System.Windows.Forms.Label lblLoaiXe;
        private System.Windows.Forms.ComboBox cboLoaiXe;
        private System.Windows.Forms.Label lblKhachHang;
        private System.Windows.Forms.TextBox txtKhachHang;
        private System.Windows.Forms.Label lblDienThoai;
        private System.Windows.Forms.TextBox txtDienThoai;
        private System.Windows.Forms.Label lblThoPhuTrach;
        private System.Windows.Forms.ComboBox cboThoPhuTrach;
        private System.Windows.Forms.Label lblTrangThai;
        private System.Windows.Forms.ComboBox cboTrangThai;
        private System.Windows.Forms.Label lblTinhTrangXe;
        private System.Windows.Forms.RichTextBox rtbTinhTrangXe;
        private System.Windows.Forms.Button btnThemMoi;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.Label lblThongBao;
        private System.Windows.Forms.Label lblDanhSachPhieu;
        private System.Windows.Forms.DataGridView dgvPhieu;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaPhieu;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNgayNhan;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBienSoXe;
        private System.Windows.Forms.DataGridViewTextBoxColumn colKhachHang;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLoaiXe;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTenTho;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTrangThai;
    }
}
