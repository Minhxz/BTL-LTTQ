using System;
using System.Drawing;
using System.Windows.Forms;

namespace btl
{
	partial class frmQuanLyKho
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			lblTieuDe = new Label();
			lblMoTa = new Label();
			tabChinh = new TabControl();
			tabPhuTung = new TabPage();
			tabPhieuNhap = new TabPage();
			lblMaNVDieuChinh = new Label();
			txtMaNVDieuChinh = new TextBox();
			lblLyDoDieuChinh = new Label();
			txtLyDoDieuChinh = new TextBox();
			pnlTimDieuChinh = new Panel();
			txtTimDieuChinh = new TextBox();
			lblTimDieuChinh = new Label();
			grpDieuChinhTonKho = new GroupBox();
			lblTongTienLech = new Label();
			dgvDieuChinhTonKho = new DataGridView();
			colDieuChinhMa = new DataGridViewTextBoxColumn();
			colDieuChinhNgay = new DataGridViewTextBoxColumn();
			colDieuChinhMaPhuTung = new DataGridViewTextBoxColumn();
			colDieuChinhTenPhuTung = new DataGridViewTextBoxColumn();
			colDieuChinhSoLuongCu = new DataGridViewTextBoxColumn();
			colDieuChinhSoLuongMoi = new DataGridViewTextBoxColumn();
			colDieuChinhDonGiaNhap = new DataGridViewTextBoxColumn();
			colDieuChinhGiaTriChenhLech = new DataGridViewTextBoxColumn();
			colDieuChinhMaNhanVien = new DataGridViewTextBoxColumn();
			colDieuChinhLyDo = new DataGridViewTextBoxColumn();
			grpDieuChinhGiaBan = new GroupBox();
			dgvDieuChinhGiaBan = new DataGridView();
			colGiaBanMa = new DataGridViewTextBoxColumn();
			colGiaBanNgay = new DataGridViewTextBoxColumn();
			colGiaBanMaPhuTung = new DataGridViewTextBoxColumn();
			colGiaBanTenPhuTung = new DataGridViewTextBoxColumn();
			colGiaBanCu = new DataGridViewTextBoxColumn();
			colGiaBanMoi = new DataGridViewTextBoxColumn();
			colGiaBanMaNhanVien = new DataGridViewTextBoxColumn();
			colGiaBanLyDo = new DataGridViewTextBoxColumn();
			pnlTimPhuTung = new Panel();
			txtTimPhuTung = new TextBox();
			lblTimPhuTung = new Label();
			grpPhuTung = new GroupBox();
			lblSoLuongDieuChinh = new Label();
			nudSoLuongDieuChinh = new NumericUpDown();
			btnDieuChinhSoLuong = new Button();
			lblDonGiaBanMoi = new Label();
			txtDonGiaBanMoi = new TextBox();
			btnDieuChinhGiaBan = new Button();
			btnNgungSuDung = new Button();
			dgvPhuTung = new DataGridView();
			colMaPhuTung = new DataGridViewTextBoxColumn();
			colTenPhuTung = new DataGridViewTextBoxColumn();
			colTenNCC = new DataGridViewTextBoxColumn();
			colSoLuongTon = new DataGridViewTextBoxColumn();
			colDonGiaBan = new DataGridViewTextBoxColumn();
			colDonGiaNhap = new DataGridViewTextBoxColumn();
			colHanSuDung = new DataGridViewTextBoxColumn();
			pnlTimPhieuNhap = new Panel();
			txtTimPhieuNhap = new TextBox();
			lblTimPhieuNhap = new Label();
			grpPhieuNhap = new GroupBox();
			dgvPhieuNhap = new DataGridView();
			colMaPhieuNhap = new DataGridViewTextBoxColumn();
			colNgayNhap = new DataGridViewTextBoxColumn();
			colTongSoLuongNhap = new DataGridViewTextBoxColumn();
			colThanhTienNhap = new DataGridViewTextBoxColumn();
			colGhiChu = new DataGridViewTextBoxColumn();
			colMaNhanVienNhap = new DataGridViewTextBoxColumn();
			grpChiTietPhieuNhap = new GroupBox();
			dgvChiTietPhieuNhap = new DataGridView();
			colChiTietMaPhieuNhap = new DataGridViewTextBoxColumn();
			colChiTietMaPhuTung = new DataGridViewTextBoxColumn();
			colChiTietSoLuongNhap = new DataGridViewTextBoxColumn();
			colChiTietDonGiaNhap = new DataGridViewTextBoxColumn();
			colChiTietThanhTien = new DataGridViewTextBoxColumn();
			colChiTietHanSuDung = new DataGridViewTextBoxColumn();
			pnlChucNangPhieuNhap = new Panel();
			btnInPhieuNhap = new Button();
			btnThoat = new Button();
			btnXoaPhieuNhap = new Button();
			btnSuaPhieuNhap = new Button();
			btnThemPhieuNhap = new Button();
			btnXuatExcelPhieuNhap = new Button();
			btnXuatExcelPhuTung = new Button();
			btnThoatPhuTung = new Button();
			tabDieuChinh = new TabPage();
			grpDieuChinh = new GroupBox();
			tlpLichSu = new TableLayoutPanel();
			pnlNutPhuTung = new Panel();
			tlpPhieuNhap = new TableLayoutPanel();
			tabDieuChinh.SuspendLayout();
			grpDieuChinh.SuspendLayout();
			tlpLichSu.SuspendLayout();
			pnlNutPhuTung.SuspendLayout();
			tlpPhieuNhap.SuspendLayout();
			tabChinh.SuspendLayout();
			tabPhuTung.SuspendLayout();
			tabPhieuNhap.SuspendLayout();
			pnlTimDieuChinh.SuspendLayout();
			grpDieuChinhTonKho.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dgvDieuChinhTonKho).BeginInit();
			grpDieuChinhGiaBan.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dgvDieuChinhGiaBan).BeginInit();
			pnlTimPhuTung.SuspendLayout();
			grpPhuTung.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)nudSoLuongDieuChinh).BeginInit();
			((System.ComponentModel.ISupportInitialize)dgvPhuTung).BeginInit();
			pnlTimPhieuNhap.SuspendLayout();
			grpPhieuNhap.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dgvPhieuNhap).BeginInit();
			grpChiTietPhieuNhap.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dgvChiTietPhieuNhap).BeginInit();
			pnlChucNangPhieuNhap.SuspendLayout();
			SuspendLayout();
			//
			// pnlTimPhuTung
			//
			pnlTimPhuTung.Controls.Add(txtTimPhuTung);
			pnlTimPhuTung.Controls.Add(lblTimPhuTung);
			pnlTimPhuTung.Dock = DockStyle.Top;
			pnlTimPhuTung.Location = new Point(3, 3);
			pnlTimPhuTung.Name = "pnlTimPhuTung";
			pnlTimPhuTung.Size = new Size(1716, 50);
			pnlTimPhuTung.TabIndex = 0;
			//
			// txtTimPhuTung
			//
			txtTimPhuTung.Location = new Point(107, 12);
			txtTimPhuTung.Name = "txtTimPhuTung";
			txtTimPhuTung.Size = new Size(278, 27);
			txtTimPhuTung.TabIndex = 1;
			txtTimPhuTung.TextChanged += txtTimPhuTung_TextChanged;
			//
			// lblTimPhuTung
			//
			lblTimPhuTung.AutoSize = true;
			lblTimPhuTung.Location = new Point(55, 15);
			lblTimPhuTung.Name = "lblTimPhuTung";
			lblTimPhuTung.Size = new Size(37, 20);
			lblTimPhuTung.TabIndex = 0;
			lblTimPhuTung.Text = "Tìm:";
			//
			// grpPhuTung
			//
			grpPhuTung.Controls.Add(dgvPhuTung);
			grpPhuTung.Dock = DockStyle.Fill;
			grpPhuTung.Location = new Point(3, 53);
			grpPhuTung.Name = "grpPhuTung";
			grpPhuTung.Size = new Size(1716, 509);
			grpPhuTung.TabIndex = 1;
			grpPhuTung.TabStop = false;
			grpPhuTung.Text = "Danh sách phụ tùng";
			//
			// lblSoLuongDieuChinh
			//
			lblSoLuongDieuChinh.AutoSize = true;
			lblSoLuongDieuChinh.Location = new Point(16, 86);
			lblSoLuongDieuChinh.Name = "lblSoLuongDieuChinh";
			lblSoLuongDieuChinh.Size = new Size(145, 20);
			lblSoLuongDieuChinh.TabIndex = 17;
			lblSoLuongDieuChinh.Text = "Số lượng điều chỉnh:";
			//
			// nudSoLuongDieuChinh
			//
			nudSoLuongDieuChinh.Location = new Point(170, 83);
			nudSoLuongDieuChinh.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
			nudSoLuongDieuChinh.Minimum = new decimal(new int[] { 100000, 0, 0, int.MinValue });
			nudSoLuongDieuChinh.Name = "nudSoLuongDieuChinh";
			nudSoLuongDieuChinh.Size = new Size(100, 27);
			nudSoLuongDieuChinh.TabIndex = 18;
			//
			// btnDieuChinhSoLuong
			//
			btnDieuChinhSoLuong.Location = new Point(285, 76);
			btnDieuChinhSoLuong.Name = "btnDieuChinhSoLuong";
			btnDieuChinhSoLuong.Size = new Size(170, 44);
			btnDieuChinhSoLuong.TabIndex = 19;
			btnDieuChinhSoLuong.Text = "Điều chỉnh tồn";
			btnDieuChinhSoLuong.UseVisualStyleBackColor = true;
			btnDieuChinhSoLuong.Click += btnDieuChinhSoLuong_Click;
			//
			// lblDonGiaBanMoi
			//
			lblDonGiaBanMoi.AutoSize = true;
			lblDonGiaBanMoi.Location = new Point(500, 86);
			lblDonGiaBanMoi.Name = "lblDonGiaBanMoi";
			lblDonGiaBanMoi.Size = new Size(95, 20);
			lblDonGiaBanMoi.TabIndex = 20;
			lblDonGiaBanMoi.Text = "Giá bán mới:";
			//
			// txtDonGiaBanMoi
			//
			txtDonGiaBanMoi.Location = new Point(605, 83);
			txtDonGiaBanMoi.Name = "txtDonGiaBanMoi";
			txtDonGiaBanMoi.Size = new Size(120, 27);
			txtDonGiaBanMoi.TabIndex = 21;
			//
			// btnDieuChinhGiaBan
			//
			btnDieuChinhGiaBan.Location = new Point(740, 76);
			btnDieuChinhGiaBan.Name = "btnDieuChinhGiaBan";
			btnDieuChinhGiaBan.Size = new Size(190, 44);
			btnDieuChinhGiaBan.TabIndex = 22;
			btnDieuChinhGiaBan.Text = "Điều chỉnh giá bán";
			btnDieuChinhGiaBan.UseVisualStyleBackColor = true;
			btnDieuChinhGiaBan.Click += btnDieuChinhGiaBan_Click;
			//
			// lblMaNVDieuChinh
			//
			lblMaNVDieuChinh.AutoSize = true;
			lblMaNVDieuChinh.Location = new Point(16, 38);
			lblMaNVDieuChinh.Name = "lblMaNVDieuChinh";
			lblMaNVDieuChinh.Size = new Size(54, 20);
			lblMaNVDieuChinh.TabIndex = 23;
			lblMaNVDieuChinh.Text = "Mã NV:";
			//
			// txtMaNVDieuChinh
			//
			txtMaNVDieuChinh.Location = new Point(80, 35);
			txtMaNVDieuChinh.Name = "txtMaNVDieuChinh";
			txtMaNVDieuChinh.Size = new Size(120, 27);
			txtMaNVDieuChinh.TabIndex = 24;
			//
			// lblLyDoDieuChinh
			//
			lblLyDoDieuChinh.AutoSize = true;
			lblLyDoDieuChinh.Location = new Point(230, 38);
			lblLyDoDieuChinh.Name = "lblLyDoDieuChinh";
			lblLyDoDieuChinh.Size = new Size(49, 20);
			lblLyDoDieuChinh.TabIndex = 25;
			lblLyDoDieuChinh.Text = "Lý do:";
			//
			// txtLyDoDieuChinh
			//
			txtLyDoDieuChinh.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			txtLyDoDieuChinh.Location = new Point(290, 35);
			txtLyDoDieuChinh.Name = "txtLyDoDieuChinh";
			txtLyDoDieuChinh.Size = new Size(1410, 27);
			txtLyDoDieuChinh.TabIndex = 26;
			//
			// btnNgungSuDung
			//
			btnNgungSuDung.Location = new Point(6, 8);
			btnNgungSuDung.Name = "btnNgungSuDung";
			btnNgungSuDung.RightToLeft = RightToLeft.No;
			btnNgungSuDung.Size = new Size(160, 48);
			btnNgungSuDung.TabIndex = 16;
			btnNgungSuDung.Text = "Ngưng sử dụng";
			btnNgungSuDung.UseVisualStyleBackColor = true;
			btnNgungSuDung.Click += btnNgungSuDung_Click;
			//
			// dgvPhuTung
			//
			dgvPhuTung.AllowUserToAddRows = false;
			dgvPhuTung.AllowUserToDeleteRows = false;
			dgvPhuTung.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dgvPhuTung.Columns.AddRange(new DataGridViewColumn[] { colMaPhuTung, colTenPhuTung, colTenNCC, colSoLuongTon, colDonGiaBan, colDonGiaNhap, colHanSuDung });
			dgvPhuTung.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			dgvPhuTung.Location = new Point(6, 26);
			dgvPhuTung.Name = "dgvPhuTung";
			dgvPhuTung.RowHeadersWidth = 51;
			dgvPhuTung.Size = new Size(1704, 477);
			dgvPhuTung.TabIndex = 0;
			dgvPhuTung.CellClick += dgvPhuTung_CellClick;
			//
			// colMaPhuTung
			//
			colMaPhuTung.DataPropertyName = "MaPhuTung";
			colMaPhuTung.Frozen = true;
			colMaPhuTung.HeaderText = "Mã phụ tùng";
			colMaPhuTung.MinimumWidth = 6;
			colMaPhuTung.Name = "colMaPhuTung";
			colMaPhuTung.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
			colMaPhuTung.Width = 115;
			//
			// colTenPhuTung
			//
			colTenPhuTung.DataPropertyName = "TenPhuTung";
			colTenPhuTung.HeaderText = "Tên phụ tùng";
			colTenPhuTung.MinimumWidth = 6;
			colTenPhuTung.Name = "colTenPhuTung";
			colTenPhuTung.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			colTenPhuTung.Width = 370;
			//
			// colTenNCC
			//
			colTenNCC.DataPropertyName = "TenNCC";
			colTenNCC.HeaderText = "Nhà cung cấp";
			colTenNCC.MinimumWidth = 6;
			colTenNCC.Name = "colTenNCC";
			colTenNCC.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
			colTenNCC.Width = 400;
			//
			// colSoLuongTon
			//
			colSoLuongTon.DataPropertyName = "SoLuongTon";
			colSoLuongTon.HeaderText = "Số lượng tồn";
			colSoLuongTon.MinimumWidth = 6;
			colSoLuongTon.Name = "colSoLuongTon";
			colSoLuongTon.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
			colSoLuongTon.Width = 120;
			//
			// colDonGiaBan
			//
			colDonGiaBan.DataPropertyName = "DonGiaBan";
			colDonGiaBan.HeaderText = "Đơn giá bán";
			colDonGiaBan.MinimumWidth = 6;
			colDonGiaBan.Name = "colDonGiaBan";
			colDonGiaBan.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
			colDonGiaBan.Width = 130;
			//
			// colDonGiaNhap
			//
			colDonGiaNhap.DataPropertyName = "DonGiaNhap";
			colDonGiaNhap.HeaderText = "Đơn giá nhập";
			colDonGiaNhap.MinimumWidth = 6;
			colDonGiaNhap.Name = "colDonGiaNhap";
			colDonGiaNhap.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
			colDonGiaNhap.Width = 130;
			//
			// colHanSuDung
			//
			colHanSuDung.DataPropertyName = "HanSuDung";
			colHanSuDung.HeaderText = "Hạn sử dụng";
			colHanSuDung.MinimumWidth = 6;
			colHanSuDung.Name = "colHanSuDung";
			colHanSuDung.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
			colHanSuDung.Width = 140;
			//
			// pnlTimPhieuNhap
			//
			pnlTimPhieuNhap.Controls.Add(txtTimPhieuNhap);
			pnlTimPhieuNhap.Controls.Add(lblTimPhieuNhap);
			pnlTimPhieuNhap.Dock = DockStyle.Fill;
			pnlTimPhieuNhap.Location = new Point(3, 3);
			pnlTimPhieuNhap.Name = "pnlTimPhieuNhap";
			pnlTimPhieuNhap.Size = new Size(1710, 50);
			pnlTimPhieuNhap.TabIndex = 2;
			//
			// txtTimPhieuNhap
			//
			txtTimPhieuNhap.Location = new Point(103, 12);
			txtTimPhieuNhap.Name = "txtTimPhieuNhap";
			txtTimPhieuNhap.Size = new Size(239, 27);
			txtTimPhieuNhap.TabIndex = 1;
			txtTimPhieuNhap.TextChanged += txtTimPhieuNhap_TextChanged;
			//
			// lblTimPhieuNhap
			//
			lblTimPhieuNhap.AutoSize = true;
			lblTimPhieuNhap.Location = new Point(41, 15);
			lblTimPhieuNhap.Name = "lblTimPhieuNhap";
			lblTimPhieuNhap.Size = new Size(37, 20);
			lblTimPhieuNhap.TabIndex = 0;
			lblTimPhieuNhap.Text = "Tìm:";
			//
			// grpPhieuNhap
			//
			grpPhieuNhap.Controls.Add(dgvPhieuNhap);
			grpPhieuNhap.Dock = DockStyle.Fill;
			grpPhieuNhap.Location = new Point(3, 59);
			grpPhieuNhap.Name = "grpPhieuNhap";
			grpPhieuNhap.Size = new Size(1710, 333);
			grpPhieuNhap.TabIndex = 3;
			grpPhieuNhap.TabStop = false;
			grpPhieuNhap.Text = "Danh sách phiếu nhập";
			//
			// dgvPhieuNhap
			//
			dgvPhieuNhap.AllowUserToAddRows = false;
			dgvPhieuNhap.AllowUserToDeleteRows = false;
			dgvPhieuNhap.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dgvPhieuNhap.Columns.AddRange(new DataGridViewColumn[] { colMaPhieuNhap, colNgayNhap, colTongSoLuongNhap, colThanhTienNhap, colGhiChu, colMaNhanVienNhap });
			dgvPhieuNhap.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			dgvPhieuNhap.Location = new Point(6, 26);
			dgvPhieuNhap.Name = "dgvPhieuNhap";
			dgvPhieuNhap.RowHeadersWidth = 51;
			dgvPhieuNhap.Size = new Size(1698, 301);
			dgvPhieuNhap.TabIndex = 0;
			dgvPhieuNhap.CellClick += dgvPhieuNhap_CellClick;
			//
			// colMaPhieuNhap
			//
			colMaPhieuNhap.DataPropertyName = "MaPhieuNhap";
			colMaPhieuNhap.HeaderText = "Mã phiếu nhập";
			colMaPhieuNhap.MinimumWidth = 6;
			colMaPhieuNhap.Name = "colMaPhieuNhap";
			colMaPhieuNhap.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
			colMaPhieuNhap.Width = 130;
			//
			// colNgayNhap
			//
			colNgayNhap.DataPropertyName = "NgayNhap";
			colNgayNhap.HeaderText = "Ngày nhập";
			colNgayNhap.MinimumWidth = 6;
			colNgayNhap.Name = "colNgayNhap";
			colNgayNhap.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
			colNgayNhap.Width = 120;
			//
			// colTongSoLuongNhap
			//
			colTongSoLuongNhap.DataPropertyName = "TongSoLuongNhap";
			colTongSoLuongNhap.HeaderText = "Tổng số lượng nhập";
			colTongSoLuongNhap.MinimumWidth = 6;
			colTongSoLuongNhap.Name = "colTongSoLuongNhap";
			colTongSoLuongNhap.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
			colTongSoLuongNhap.Width = 150;
			//
			// colThanhTienNhap
			//
			colThanhTienNhap.DataPropertyName = "ThanhTien";
			colThanhTienNhap.HeaderText = "Thành tiền";
			colThanhTienNhap.MinimumWidth = 6;
			colThanhTienNhap.Name = "colThanhTienNhap";
			colThanhTienNhap.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
			colThanhTienNhap.Width = 150;
			//
			// colGhiChu
			//
			colGhiChu.DataPropertyName = "GhiChu";
			colGhiChu.HeaderText = "Ghi chú";
			colGhiChu.MinimumWidth = 6;
			colGhiChu.Name = "colGhiChu";
			colGhiChu.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			colGhiChu.Width = 400;
			//
			// colMaNhanVienNhap
			//
			colMaNhanVienNhap.DataPropertyName = "MaNhanVienNhap";
			colMaNhanVienNhap.HeaderText = "Mã NV nhập";
			colMaNhanVienNhap.MinimumWidth = 6;
			colMaNhanVienNhap.Name = "colMaNhanVienNhap";
			colMaNhanVienNhap.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
			colMaNhanVienNhap.Width = 120;
			//
			// grpChiTietPhieuNhap
			//
			grpChiTietPhieuNhap.Controls.Add(dgvChiTietPhieuNhap);
			grpChiTietPhieuNhap.Dock = DockStyle.Fill;
			grpChiTietPhieuNhap.Location = new Point(3, 398);
			grpChiTietPhieuNhap.Name = "grpChiTietPhieuNhap";
			grpChiTietPhieuNhap.Size = new Size(1710, 272);
			grpChiTietPhieuNhap.TabIndex = 4;
			grpChiTietPhieuNhap.TabStop = false;
			grpChiTietPhieuNhap.Text = "Thông tin phiếu nhập";
			//
			// dgvChiTietPhieuNhap
			//
			dgvChiTietPhieuNhap.AllowUserToAddRows = false;
			dgvChiTietPhieuNhap.AllowUserToDeleteRows = false;
			dgvChiTietPhieuNhap.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dgvChiTietPhieuNhap.Columns.AddRange(new DataGridViewColumn[] { colChiTietMaPhieuNhap, colChiTietMaPhuTung, colChiTietSoLuongNhap, colChiTietDonGiaNhap, colChiTietThanhTien, colChiTietHanSuDung });
			dgvChiTietPhieuNhap.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			dgvChiTietPhieuNhap.Location = new Point(6, 26);
			dgvChiTietPhieuNhap.Name = "dgvChiTietPhieuNhap";
			dgvChiTietPhieuNhap.RowHeadersWidth = 51;
			dgvChiTietPhieuNhap.Size = new Size(1698, 240);
			dgvChiTietPhieuNhap.TabIndex = 0;
			//
			// colChiTietMaPhieuNhap
			//
			colChiTietMaPhieuNhap.DataPropertyName = "MaPhieuNhap";
			colChiTietMaPhieuNhap.HeaderText = "Mã phiếu nhập";
			colChiTietMaPhieuNhap.MinimumWidth = 6;
			colChiTietMaPhieuNhap.Name = "colChiTietMaPhieuNhap";
			colChiTietMaPhieuNhap.Width = 130;
			//
			// colChiTietMaPhuTung
			//
			colChiTietMaPhuTung.DataPropertyName = "MaPhuTung";
			colChiTietMaPhuTung.HeaderText = "Mã phụ tùng";
			colChiTietMaPhuTung.MinimumWidth = 6;
			colChiTietMaPhuTung.Name = "colChiTietMaPhuTung";
			colChiTietMaPhuTung.Width = 130;
			//
			// colChiTietSoLuongNhap
			//
			colChiTietSoLuongNhap.DataPropertyName = "SoLuongNhap";
			colChiTietSoLuongNhap.HeaderText = "Số lượng nhập";
			colChiTietSoLuongNhap.MinimumWidth = 6;
			colChiTietSoLuongNhap.Name = "colChiTietSoLuongNhap";
			colChiTietSoLuongNhap.Width = 140;
			//
			// colChiTietDonGiaNhap
			//
			colChiTietDonGiaNhap.DataPropertyName = "DonGiaNhap";
			colChiTietDonGiaNhap.HeaderText = "Đơn giá nhập";
			colChiTietDonGiaNhap.MinimumWidth = 6;
			colChiTietDonGiaNhap.Name = "colChiTietDonGiaNhap";
			colChiTietDonGiaNhap.Width = 140;
			//
			// colChiTietThanhTien
			//
			colChiTietThanhTien.DataPropertyName = "ThanhTien";
			colChiTietThanhTien.HeaderText = "Thành Tiền";
			colChiTietThanhTien.MinimumWidth = 6;
			colChiTietThanhTien.Name = "colChiTietThanhTien";
			colChiTietThanhTien.Width = 150;
			//
			// colChiTietHanSuDung
			//
			colChiTietHanSuDung.DataPropertyName = "HanSuDung";
			colChiTietHanSuDung.HeaderText = "Hạn sử dụng";
			colChiTietHanSuDung.MinimumWidth = 6;
			colChiTietHanSuDung.Name = "colChiTietHanSuDung";
			colChiTietHanSuDung.Width = 140;
			//
			// pnlChucNangPhieuNhap
			//
			pnlChucNangPhieuNhap.Controls.Add(btnXuatExcelPhieuNhap);
			pnlChucNangPhieuNhap.Controls.Add(btnInPhieuNhap);
			pnlChucNangPhieuNhap.Controls.Add(btnThoat);
			pnlChucNangPhieuNhap.Controls.Add(btnXoaPhieuNhap);
			pnlChucNangPhieuNhap.Controls.Add(btnSuaPhieuNhap);
			pnlChucNangPhieuNhap.Controls.Add(btnThemPhieuNhap);
			pnlChucNangPhieuNhap.Dock = DockStyle.Fill;
			pnlChucNangPhieuNhap.Location = new Point(3, 676);
			pnlChucNangPhieuNhap.Name = "pnlChucNangPhieuNhap";
			pnlChucNangPhieuNhap.Size = new Size(1710, 74);
			pnlChucNangPhieuNhap.TabIndex = 5;
			//
			// btnInPhieuNhap
			//
			btnInPhieuNhap.Location = new Point(557, 10);
			btnInPhieuNhap.Name = "btnInPhieuNhap";
			btnInPhieuNhap.Size = new Size(160, 53);
			btnInPhieuNhap.TabIndex = 11;
			btnInPhieuNhap.Text = "In phiếu nhập";
			btnInPhieuNhap.UseVisualStyleBackColor = true;
			btnInPhieuNhap.Click += btnInPhieuNhap_Click;
			//
			// btnThoat
			//
			btnThoat.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			btnThoat.Location = new Point(1545, 10);
			btnThoat.Name = "btnThoat";
			btnThoat.Size = new Size(139, 53);
			btnThoat.TabIndex = 9;
			btnThoat.Text = "Thoát";
			btnThoat.UseVisualStyleBackColor = true;
			btnThoat.Click += btnThoat_Click;
			//
			// btnXoaPhieuNhap
			//
			btnXoaPhieuNhap.Location = new Point(381, 10);
			btnXoaPhieuNhap.Name = "btnXoaPhieuNhap";
			btnXoaPhieuNhap.Size = new Size(160, 53);
			btnXoaPhieuNhap.TabIndex = 7;
			btnXoaPhieuNhap.Text = "Xóa phiếu nhập";
			btnXoaPhieuNhap.UseVisualStyleBackColor = true;
			btnXoaPhieuNhap.Click += btnXoaPhieuNhap_Click;
			//
			// btnSuaPhieuNhap
			//
			btnSuaPhieuNhap.Location = new Point(205, 10);
			btnSuaPhieuNhap.Name = "btnSuaPhieuNhap";
			btnSuaPhieuNhap.Size = new Size(160, 53);
			btnSuaPhieuNhap.TabIndex = 6;
			btnSuaPhieuNhap.Text = "Sửa phiếu nhập";
			btnSuaPhieuNhap.UseVisualStyleBackColor = true;
			btnSuaPhieuNhap.Click += btnSuaPhieuNhap_Click;
			//
			// btnThemPhieuNhap
			//
			btnThemPhieuNhap.Location = new Point(29, 10);
			btnThemPhieuNhap.Name = "btnThemPhieuNhap";
			btnThemPhieuNhap.Size = new Size(160, 53);
			btnThemPhieuNhap.TabIndex = 5;
			btnThemPhieuNhap.Text = "Thêm phiếu nhập";
			btnThemPhieuNhap.UseVisualStyleBackColor = true;
			btnThemPhieuNhap.Click += btnThemPhieuNhap_Click;
			//
			// btnXuatExcelPhieuNhap
			//
			btnXuatExcelPhieuNhap.Location = new Point(733, 10);
			btnXuatExcelPhieuNhap.Name = "btnXuatExcelPhieuNhap";
			btnXuatExcelPhieuNhap.Size = new Size(160, 53);
			btnXuatExcelPhieuNhap.TabIndex = 12;
			btnXuatExcelPhieuNhap.Text = "Xuất Excel";
			btnXuatExcelPhieuNhap.UseVisualStyleBackColor = true;
			btnXuatExcelPhieuNhap.Click += btnXuatExcelPhieuNhap_Click;
			//
			// btnXuatExcelPhuTung
			//
			btnXuatExcelPhuTung.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			btnXuatExcelPhuTung.Location = new Point(1418, 8);
			btnXuatExcelPhuTung.Name = "btnXuatExcelPhuTung";
			btnXuatExcelPhuTung.Size = new Size(140, 48);
			btnXuatExcelPhuTung.TabIndex = 4;
			btnXuatExcelPhuTung.Text = "Xuất Excel";
			btnXuatExcelPhuTung.UseVisualStyleBackColor = true;
			btnXuatExcelPhuTung.Click += btnXuatExcelPhuTung_Click;
			//
			// btnThoatPhuTung
			//
			btnThoatPhuTung.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			btnThoatPhuTung.Location = new Point(1570, 8);
			btnThoatPhuTung.Name = "btnThoatPhuTung";
			btnThoatPhuTung.Size = new Size(140, 48);
			btnThoatPhuTung.TabIndex = 5;
			btnThoatPhuTung.Text = "Thoát";
			btnThoatPhuTung.UseVisualStyleBackColor = true;
			btnThoatPhuTung.Click += btnThoat_Click;
			//
			// tabChinh
			//
			tabChinh.Dock = DockStyle.Fill;
			tabChinh.Controls.Add(tabPhuTung);
			tabChinh.Controls.Add(tabDieuChinh);
			tabChinh.Controls.Add(tabPhieuNhap);
			tabChinh.Location = new Point(12, 84);
			tabChinh.MinimumSize = new Size(1400, 700);
			tabChinh.Name = "tabChinh";
			tabChinh.SelectedIndex = 0;
			tabChinh.Size = new Size(1730, 800);
			tabChinh.TabIndex = 0;
			//
			// lblTieuDe
			//
			lblTieuDe.Dock = DockStyle.Top;
			lblTieuDe.Location = new Point(12, 0);
			lblTieuDe.Name = "lblTieuDe";
			lblTieuDe.Size = new Size(1730, 50);
			lblTieuDe.TabIndex = 1;
			lblTieuDe.Text = "KHO PHỤ TÙNG";
			lblTieuDe.TextAlign = ContentAlignment.MiddleCenter;
			//
			// lblMoTa
			//
			lblMoTa.Dock = DockStyle.Top;
			lblMoTa.Location = new Point(12, 50);
			lblMoTa.Name = "lblMoTa";
			lblMoTa.Size = new Size(1730, 34);
			lblMoTa.TabIndex = 2;
			lblMoTa.Text = "Quản lý phụ tùng, lịch sử điều chỉnh tồn kho, giá bán và phiếu nhập";
			lblMoTa.TextAlign = ContentAlignment.MiddleCenter;
			//
			// tabPhuTung
			//
			tabPhuTung.Controls.Add(grpPhuTung);
			tabPhuTung.Controls.Add(grpDieuChinh);
			tabPhuTung.Controls.Add(pnlNutPhuTung);
			tabPhuTung.Controls.Add(pnlTimPhuTung);
			tabPhuTung.Location = new Point(4, 29);
			tabPhuTung.Name = "tabPhuTung";
			tabPhuTung.Padding = new Padding(3);
			tabPhuTung.Size = new Size(1722, 759);
			tabPhuTung.TabIndex = 0;
			tabPhuTung.Text = "Phụ tùng";
			tabPhuTung.UseVisualStyleBackColor = true;
			//
			// tabPhieuNhap
			//
			tabPhieuNhap.Controls.Add(tlpPhieuNhap);
			tabPhieuNhap.Location = new Point(4, 29);
			tabPhieuNhap.Name = "tabPhieuNhap";
			tabPhieuNhap.Padding = new Padding(3);
			tabPhieuNhap.Size = new Size(1722, 759);
			tabPhieuNhap.TabIndex = 2;
			tabPhieuNhap.Text = "Phiếu nhập";
			tabPhieuNhap.UseVisualStyleBackColor = true;
			//
			// pnlTimDieuChinh
			//
			pnlTimDieuChinh.Controls.Add(txtTimDieuChinh);
			pnlTimDieuChinh.Controls.Add(lblTimDieuChinh);
			pnlTimDieuChinh.Dock = DockStyle.Top;
			pnlTimDieuChinh.Location = new Point(3, 3);
			pnlTimDieuChinh.Name = "pnlTimDieuChinh";
			pnlTimDieuChinh.Size = new Size(1716, 50);
			pnlTimDieuChinh.TabIndex = 0;
			//
			// txtTimDieuChinh
			//
			txtTimDieuChinh.Location = new Point(150, 12);
			txtTimDieuChinh.Name = "txtTimDieuChinh";
			txtTimDieuChinh.Size = new Size(278, 27);
			txtTimDieuChinh.TabIndex = 1;
			txtTimDieuChinh.TextChanged += txtTimDieuChinh_TextChanged;
			//
			// lblTimDieuChinh
			//
			lblTimDieuChinh.AutoSize = true;
			lblTimDieuChinh.Location = new Point(16, 15);
			lblTimDieuChinh.Name = "lblTimDieuChinh";
			lblTimDieuChinh.Size = new Size(124, 20);
			lblTimDieuChinh.TabIndex = 0;
			lblTimDieuChinh.Text = "Tìm mã phụ tùng:";
			//
			// grpDieuChinhTonKho
			//
			grpDieuChinhTonKho.Controls.Add(lblTongTienLech);
			grpDieuChinhTonKho.Controls.Add(dgvDieuChinhTonKho);
			grpDieuChinhTonKho.Dock = DockStyle.Fill;
			grpDieuChinhTonKho.Location = new Point(3, 3);
			grpDieuChinhTonKho.Name = "grpDieuChinhTonKho";
			grpDieuChinhTonKho.Size = new Size(1710, 380);
			grpDieuChinhTonKho.TabIndex = 1;
			grpDieuChinhTonKho.TabStop = false;
			grpDieuChinhTonKho.Text = "Lịch sử điều chỉnh tồn";
			//
			// lblTongTienLech
			//
			lblTongTienLech.AutoSize = true;
			lblTongTienLech.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			lblTongTienLech.Location = new Point(6, 350);
			lblTongTienLech.Name = "lblTongTienLech";
			lblTongTienLech.Size = new Size(300, 20);
			lblTongTienLech.TabIndex = 1;
			lblTongTienLech.Text = "Tổng thừa: 0    Tổng thiếu: 0    Tổng tiền lệch: 0";
			//
			// dgvDieuChinhTonKho
			//
			dgvDieuChinhTonKho.AllowUserToAddRows = false;
			dgvDieuChinhTonKho.AllowUserToDeleteRows = false;
			dgvDieuChinhTonKho.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dgvDieuChinhTonKho.Columns.AddRange(new DataGridViewColumn[] { colDieuChinhMa, colDieuChinhNgay, colDieuChinhMaPhuTung, colDieuChinhTenPhuTung, colDieuChinhSoLuongCu, colDieuChinhSoLuongMoi, colDieuChinhDonGiaNhap, colDieuChinhGiaTriChenhLech, colDieuChinhMaNhanVien, colDieuChinhLyDo });
			dgvDieuChinhTonKho.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			dgvDieuChinhTonKho.Location = new Point(6, 26);
			dgvDieuChinhTonKho.Name = "dgvDieuChinhTonKho";
			dgvDieuChinhTonKho.RowHeadersWidth = 51;
			dgvDieuChinhTonKho.Size = new Size(1698, 318);
			dgvDieuChinhTonKho.TabIndex = 0;
			//
			// colDieuChinhMa
			//
			colDieuChinhMa.DataPropertyName = "MaDieuChinh";
			colDieuChinhMa.HeaderText = "Mã điều chỉnh";
			colDieuChinhMa.MinimumWidth = 6;
			colDieuChinhMa.Name = "colDieuChinhMa";
			colDieuChinhMa.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
			colDieuChinhMa.Width = 100;
			//
			// colDieuChinhNgay
			//
			colDieuChinhNgay.DataPropertyName = "NgayDieuChinh";
			colDieuChinhNgay.HeaderText = "Ngày";
			colDieuChinhNgay.MinimumWidth = 6;
			colDieuChinhNgay.Name = "colDieuChinhNgay";
			colDieuChinhNgay.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
			colDieuChinhNgay.Width = 120;
			//
			// colDieuChinhMaPhuTung
			//
			colDieuChinhMaPhuTung.DataPropertyName = "MaPhuTung";
			colDieuChinhMaPhuTung.HeaderText = "Mã phụ tùng";
			colDieuChinhMaPhuTung.MinimumWidth = 6;
			colDieuChinhMaPhuTung.Name = "colDieuChinhMaPhuTung";
			colDieuChinhMaPhuTung.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
			colDieuChinhMaPhuTung.Width = 95;
			//
			// colDieuChinhTenPhuTung
			//
			colDieuChinhTenPhuTung.DataPropertyName = "TenPhuTung";
			colDieuChinhTenPhuTung.HeaderText = "Tên phụ tùng";
			colDieuChinhTenPhuTung.MinimumWidth = 6;
			colDieuChinhTenPhuTung.Name = "colDieuChinhTenPhuTung";
			colDieuChinhTenPhuTung.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
			colDieuChinhTenPhuTung.Width = 320;
			//
			// colDieuChinhSoLuongCu
			//
			colDieuChinhSoLuongCu.DataPropertyName = "SoLuongCu";
			colDieuChinhSoLuongCu.HeaderText = "SL cũ";
			colDieuChinhSoLuongCu.MinimumWidth = 6;
			colDieuChinhSoLuongCu.Name = "colDieuChinhSoLuongCu";
			colDieuChinhSoLuongCu.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
			colDieuChinhSoLuongCu.Width = 65;
			//
			// colDieuChinhSoLuongMoi
			//
			colDieuChinhSoLuongMoi.DataPropertyName = "SoLuongMoi";
			colDieuChinhSoLuongMoi.HeaderText = "SL mới";
			colDieuChinhSoLuongMoi.MinimumWidth = 6;
			colDieuChinhSoLuongMoi.Name = "colDieuChinhSoLuongMoi";
			colDieuChinhSoLuongMoi.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
			colDieuChinhSoLuongMoi.Width = 70;
			//
			// colDieuChinhDonGiaNhap
			//
			colDieuChinhDonGiaNhap.DataPropertyName = "DonGiaNhap";
			colDieuChinhDonGiaNhap.HeaderText = "Đơn giá nhập";
			colDieuChinhDonGiaNhap.MinimumWidth = 6;
			colDieuChinhDonGiaNhap.Name = "colDieuChinhDonGiaNhap";
			colDieuChinhDonGiaNhap.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
			colDieuChinhDonGiaNhap.Width = 110;
			//
			// colDieuChinhGiaTriChenhLech
			//
			colDieuChinhGiaTriChenhLech.DataPropertyName = "GiaTriChenhLech";
			colDieuChinhGiaTriChenhLech.HeaderText = "Tiền lệch";
			colDieuChinhGiaTriChenhLech.MinimumWidth = 6;
			colDieuChinhGiaTriChenhLech.Name = "colDieuChinhGiaTriChenhLech";
			colDieuChinhGiaTriChenhLech.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
			colDieuChinhGiaTriChenhLech.Width = 110;
			//
			// colDieuChinhMaNhanVien
			//
			colDieuChinhMaNhanVien.DataPropertyName = "MaNhanVien";
			colDieuChinhMaNhanVien.HeaderText = "Mã NV";
			colDieuChinhMaNhanVien.MinimumWidth = 6;
			colDieuChinhMaNhanVien.Name = "colDieuChinhMaNhanVien";
			colDieuChinhMaNhanVien.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
			colDieuChinhMaNhanVien.Width = 75;
			//
			// colDieuChinhLyDo
			//
			colDieuChinhLyDo.DataPropertyName = "LyDo";
			colDieuChinhLyDo.HeaderText = "Lý do";
			colDieuChinhLyDo.MinimumWidth = 6;
			colDieuChinhLyDo.Name = "colDieuChinhLyDo";
			colDieuChinhLyDo.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			colDieuChinhLyDo.Width = 300;
			//
			// grpDieuChinhGiaBan
			//
			grpDieuChinhGiaBan.Controls.Add(dgvDieuChinhGiaBan);
			grpDieuChinhGiaBan.Dock = DockStyle.Fill;
			grpDieuChinhGiaBan.Location = new Point(3, 389);
			grpDieuChinhGiaBan.Name = "grpDieuChinhGiaBan";
			grpDieuChinhGiaBan.Size = new Size(1710, 311);
			grpDieuChinhGiaBan.TabIndex = 2;
			grpDieuChinhGiaBan.TabStop = false;
			grpDieuChinhGiaBan.Text = "Lịch sử điều chỉnh giá bán";
			//
			// dgvDieuChinhGiaBan
			//
			dgvDieuChinhGiaBan.AllowUserToAddRows = false;
			dgvDieuChinhGiaBan.AllowUserToDeleteRows = false;
			dgvDieuChinhGiaBan.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dgvDieuChinhGiaBan.Columns.AddRange(new DataGridViewColumn[] { colGiaBanMa, colGiaBanNgay, colGiaBanMaPhuTung, colGiaBanTenPhuTung, colGiaBanCu, colGiaBanMoi, colGiaBanMaNhanVien, colGiaBanLyDo });
			dgvDieuChinhGiaBan.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			dgvDieuChinhGiaBan.Location = new Point(6, 26);
			dgvDieuChinhGiaBan.Name = "dgvDieuChinhGiaBan";
			dgvDieuChinhGiaBan.RowHeadersWidth = 51;
			dgvDieuChinhGiaBan.Size = new Size(1698, 279);
			dgvDieuChinhGiaBan.TabIndex = 0;
			//
			// colGiaBanMa
			//
			colGiaBanMa.DataPropertyName = "MaDieuChinhGia";
			colGiaBanMa.HeaderText = "Mã điều chỉnh";
			colGiaBanMa.MinimumWidth = 6;
			colGiaBanMa.Name = "colGiaBanMa";
			colGiaBanMa.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
			colGiaBanMa.Width = 100;
			//
			// colGiaBanNgay
			//
			colGiaBanNgay.DataPropertyName = "NgayDieuChinh";
			colGiaBanNgay.HeaderText = "Ngày";
			colGiaBanNgay.MinimumWidth = 6;
			colGiaBanNgay.Name = "colGiaBanNgay";
			colGiaBanNgay.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
			colGiaBanNgay.Width = 120;
			//
			// colGiaBanMaPhuTung
			//
			colGiaBanMaPhuTung.DataPropertyName = "MaPhuTung";
			colGiaBanMaPhuTung.HeaderText = "Mã phụ tùng";
			colGiaBanMaPhuTung.MinimumWidth = 6;
			colGiaBanMaPhuTung.Name = "colGiaBanMaPhuTung";
			colGiaBanMaPhuTung.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
			colGiaBanMaPhuTung.Width = 95;
			//
			// colGiaBanTenPhuTung
			//
			colGiaBanTenPhuTung.DataPropertyName = "TenPhuTung";
			colGiaBanTenPhuTung.HeaderText = "Tên phụ tùng";
			colGiaBanTenPhuTung.MinimumWidth = 6;
			colGiaBanTenPhuTung.Name = "colGiaBanTenPhuTung";
			colGiaBanTenPhuTung.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
			colGiaBanTenPhuTung.Width = 320;
			//
			// colGiaBanCu
			//
			colGiaBanCu.DataPropertyName = "GiaBanCu";
			colGiaBanCu.HeaderText = "Giá bán cũ";
			colGiaBanCu.MinimumWidth = 6;
			colGiaBanCu.Name = "colGiaBanCu";
			colGiaBanCu.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
			colGiaBanCu.Width = 120;
			//
			// colGiaBanMoi
			//
			colGiaBanMoi.DataPropertyName = "GiaBanMoi";
			colGiaBanMoi.HeaderText = "Giá bán mới";
			colGiaBanMoi.MinimumWidth = 6;
			colGiaBanMoi.Name = "colGiaBanMoi";
			colGiaBanMoi.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
			colGiaBanMoi.Width = 120;
			//
			// colGiaBanMaNhanVien
			//
			colGiaBanMaNhanVien.DataPropertyName = "MaNhanVien";
			colGiaBanMaNhanVien.HeaderText = "Mã NV";
			colGiaBanMaNhanVien.MinimumWidth = 6;
			colGiaBanMaNhanVien.Name = "colGiaBanMaNhanVien";
			colGiaBanMaNhanVien.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
			colGiaBanMaNhanVien.Width = 75;
			//
			// colGiaBanLyDo
			//
			colGiaBanLyDo.DataPropertyName = "LyDo";
			colGiaBanLyDo.HeaderText = "Lý do";
			colGiaBanLyDo.MinimumWidth = 6;
			colGiaBanLyDo.Name = "colGiaBanLyDo";
			colGiaBanLyDo.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			colGiaBanLyDo.Width = 300;
			//
			// grpDieuChinh: điều chỉnh tồn và giá bán cho phụ tùng đang chọn trên lưới
			//
			grpDieuChinh.Controls.Add(lblMaNVDieuChinh);
			grpDieuChinh.Controls.Add(txtMaNVDieuChinh);
			grpDieuChinh.Controls.Add(lblLyDoDieuChinh);
			grpDieuChinh.Controls.Add(txtLyDoDieuChinh);
			grpDieuChinh.Controls.Add(lblSoLuongDieuChinh);
			grpDieuChinh.Controls.Add(nudSoLuongDieuChinh);
			grpDieuChinh.Controls.Add(btnDieuChinhSoLuong);
			grpDieuChinh.Controls.Add(lblDonGiaBanMoi);
			grpDieuChinh.Controls.Add(txtDonGiaBanMoi);
			grpDieuChinh.Controls.Add(btnDieuChinhGiaBan);
			grpDieuChinh.Dock = DockStyle.Bottom;
			grpDieuChinh.Location = new Point(3, 562);
			grpDieuChinh.Name = "grpDieuChinh";
			grpDieuChinh.Size = new Size(1716, 130);
			grpDieuChinh.TabIndex = 2;
			grpDieuChinh.TabStop = false;
			grpDieuChinh.Text = "Điều chỉnh tồn và giá bán phụ tùng đang chọn";
			//
			// tabDieuChinh: ô tìm ở trên, lịch sử điều chỉnh tồn và giá bán ở dưới
			//
			tabDieuChinh.Controls.Add(tlpLichSu);
			tabDieuChinh.Controls.Add(pnlTimDieuChinh);
			tabDieuChinh.Location = new Point(4, 29);
			tabDieuChinh.Name = "tabDieuChinh";
			tabDieuChinh.Padding = new Padding(3);
			tabDieuChinh.Size = new Size(1722, 759);
			tabDieuChinh.TabIndex = 1;
			tabDieuChinh.Text = "Điều chỉnh tồn và giá bán";
			tabDieuChinh.UseVisualStyleBackColor = true;
			//
			// tlpLichSu: lịch sử điều chỉnh tồn (trên), lịch sử điều chỉnh giá bán (dưới)
			//
			tlpLichSu.ColumnCount = 1;
			tlpLichSu.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
			tlpLichSu.Controls.Add(grpDieuChinhTonKho, 0, 0);
			tlpLichSu.Controls.Add(grpDieuChinhGiaBan, 0, 1);
			tlpLichSu.Dock = DockStyle.Fill;
			tlpLichSu.Location = new Point(3, 53);
			tlpLichSu.Margin = new Padding(0);
			tlpLichSu.Name = "tlpLichSu";
			tlpLichSu.RowCount = 2;
			tlpLichSu.RowStyles.Add(new RowStyle(SizeType.Percent, 55F));
			tlpLichSu.RowStyles.Add(new RowStyle(SizeType.Percent, 45F));
			tlpLichSu.Size = new Size(1716, 703);
			tlpLichSu.TabIndex = 1;
			//
			// pnlNutPhuTung
			//
			pnlNutPhuTung.Controls.Add(btnNgungSuDung);
			pnlNutPhuTung.Controls.Add(btnXuatExcelPhuTung);
			pnlNutPhuTung.Controls.Add(btnThoatPhuTung);
			pnlNutPhuTung.Dock = DockStyle.Bottom;
			pnlNutPhuTung.Location = new Point(3, 692);
			pnlNutPhuTung.Name = "pnlNutPhuTung";
			pnlNutPhuTung.Size = new Size(1716, 64);
			pnlNutPhuTung.TabIndex = 3;
			//
			// tlpPhieuNhap: ô tìm, danh sách phiếu nhập (trên), thông tin phiếu nhập (dưới), các nút
			//
			tlpPhieuNhap.ColumnCount = 1;
			tlpPhieuNhap.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
			tlpPhieuNhap.Controls.Add(pnlTimPhieuNhap, 0, 0);
			tlpPhieuNhap.Controls.Add(grpPhieuNhap, 0, 1);
			tlpPhieuNhap.Controls.Add(grpChiTietPhieuNhap, 0, 2);
			tlpPhieuNhap.Controls.Add(pnlChucNangPhieuNhap, 0, 3);
			tlpPhieuNhap.Dock = DockStyle.Fill;
			tlpPhieuNhap.Location = new Point(3, 3);
			tlpPhieuNhap.Name = "tlpPhieuNhap";
			tlpPhieuNhap.RowCount = 4;
			tlpPhieuNhap.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
			tlpPhieuNhap.RowStyles.Add(new RowStyle(SizeType.Percent, 55F));
			tlpPhieuNhap.RowStyles.Add(new RowStyle(SizeType.Percent, 45F));
			tlpPhieuNhap.RowStyles.Add(new RowStyle(SizeType.Absolute, 80F));
			tlpPhieuNhap.Size = new Size(1716, 753);
			tlpPhieuNhap.TabIndex = 0;
			//
			// frmQuanLyKho
			//
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			AutoScroll = true;
			ClientSize = new Size(1754, 896);
			Controls.Add(tabChinh);
			Controls.Add(lblMoTa);
			Controls.Add(lblTieuDe);
			FormBorderStyle = FormBorderStyle.FixedSingle;
			MaximizeBox = false;
			Padding = new Padding(12, 0, 12, 12);
			Name = "frmQuanLyKho";
			Text = "Quản lý kho phụ tùng";
			Load += frmQuanLyKho_Load;
			tabDieuChinh.ResumeLayout(false);
			grpDieuChinh.ResumeLayout(false);
			grpDieuChinh.PerformLayout();
			tlpLichSu.ResumeLayout(false);
			pnlNutPhuTung.ResumeLayout(false);
			tlpPhieuNhap.ResumeLayout(false);
			tabChinh.ResumeLayout(false);
			tabPhuTung.ResumeLayout(false);
			tabPhieuNhap.ResumeLayout(false);
			pnlTimDieuChinh.ResumeLayout(false);
			pnlTimDieuChinh.PerformLayout();
			grpDieuChinhTonKho.ResumeLayout(false);
			grpDieuChinhTonKho.PerformLayout();
			((System.ComponentModel.ISupportInitialize)dgvDieuChinhTonKho).EndInit();
			grpDieuChinhGiaBan.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dgvDieuChinhGiaBan).EndInit();
			pnlTimPhuTung.ResumeLayout(false);
			pnlTimPhuTung.PerformLayout();
			grpPhuTung.ResumeLayout(false);
			grpPhuTung.PerformLayout();
			((System.ComponentModel.ISupportInitialize)nudSoLuongDieuChinh).EndInit();
			((System.ComponentModel.ISupportInitialize)dgvPhuTung).EndInit();
			pnlTimPhieuNhap.ResumeLayout(false);
			pnlTimPhieuNhap.PerformLayout();
			grpPhieuNhap.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dgvPhieuNhap).EndInit();
			grpChiTietPhieuNhap.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dgvChiTietPhieuNhap).EndInit();
			pnlChucNangPhieuNhap.ResumeLayout(false);
			ResumeLayout(false);
		}

		#endregion

		private Label lblTieuDe;
		private Label lblMoTa;
		private TabControl tabChinh;
		private TabPage tabPhuTung;
		private TabPage tabPhieuNhap;
		private Label lblMaNVDieuChinh;
		private TextBox txtMaNVDieuChinh;
		private Label lblLyDoDieuChinh;
		private TextBox txtLyDoDieuChinh;
		private Panel pnlTimDieuChinh;
		private TextBox txtTimDieuChinh;
		private Label lblTimDieuChinh;
		private GroupBox grpDieuChinhTonKho;
		private Label lblTongTienLech;
		private DataGridView dgvDieuChinhTonKho;
		private DataGridViewTextBoxColumn colDieuChinhMa;
		private DataGridViewTextBoxColumn colDieuChinhNgay;
		private DataGridViewTextBoxColumn colDieuChinhMaPhuTung;
		private DataGridViewTextBoxColumn colDieuChinhTenPhuTung;
		private DataGridViewTextBoxColumn colDieuChinhSoLuongCu;
		private DataGridViewTextBoxColumn colDieuChinhSoLuongMoi;
		private DataGridViewTextBoxColumn colDieuChinhDonGiaNhap;
		private DataGridViewTextBoxColumn colDieuChinhGiaTriChenhLech;
		private DataGridViewTextBoxColumn colDieuChinhMaNhanVien;
		private DataGridViewTextBoxColumn colDieuChinhLyDo;
		private GroupBox grpDieuChinhGiaBan;
		private DataGridView dgvDieuChinhGiaBan;
		private DataGridViewTextBoxColumn colGiaBanMa;
		private DataGridViewTextBoxColumn colGiaBanNgay;
		private DataGridViewTextBoxColumn colGiaBanMaPhuTung;
		private DataGridViewTextBoxColumn colGiaBanTenPhuTung;
		private DataGridViewTextBoxColumn colGiaBanCu;
		private DataGridViewTextBoxColumn colGiaBanMoi;
		private DataGridViewTextBoxColumn colGiaBanMaNhanVien;
		private DataGridViewTextBoxColumn colGiaBanLyDo;
		private Panel pnlTimPhuTung;
		private TextBox txtTimPhuTung;
		private Label lblTimPhuTung;
		private GroupBox grpPhuTung;
		private Label lblSoLuongDieuChinh;
		private NumericUpDown nudSoLuongDieuChinh;
		private Button btnDieuChinhSoLuong;
		private Label lblDonGiaBanMoi;
		private TextBox txtDonGiaBanMoi;
		private Button btnDieuChinhGiaBan;
		private Button btnNgungSuDung;
		private DataGridView dgvPhuTung;
		private DataGridViewTextBoxColumn colMaPhuTung;
		private DataGridViewTextBoxColumn colTenPhuTung;
		private DataGridViewTextBoxColumn colTenNCC;
		private DataGridViewTextBoxColumn colSoLuongTon;
		private DataGridViewTextBoxColumn colDonGiaBan;
		private DataGridViewTextBoxColumn colDonGiaNhap;
		private DataGridViewTextBoxColumn colHanSuDung;
		private Panel pnlTimPhieuNhap;
		private TextBox txtTimPhieuNhap;
		private Label lblTimPhieuNhap;
		private GroupBox grpPhieuNhap;
		private DataGridView dgvPhieuNhap;
		private DataGridViewTextBoxColumn colMaPhieuNhap;
		private DataGridViewTextBoxColumn colNgayNhap;
		private DataGridViewTextBoxColumn colTongSoLuongNhap;
		private DataGridViewTextBoxColumn colThanhTienNhap;
		private DataGridViewTextBoxColumn colGhiChu;
		private DataGridViewTextBoxColumn colMaNhanVienNhap;
		private GroupBox grpChiTietPhieuNhap;
		private DataGridView dgvChiTietPhieuNhap;
		private DataGridViewTextBoxColumn colChiTietMaPhieuNhap;
		private DataGridViewTextBoxColumn colChiTietMaPhuTung;
		private DataGridViewTextBoxColumn colChiTietSoLuongNhap;
		private DataGridViewTextBoxColumn colChiTietDonGiaNhap;
		private DataGridViewTextBoxColumn colChiTietThanhTien;
		private DataGridViewTextBoxColumn colChiTietHanSuDung;
		private Panel pnlChucNangPhieuNhap;
		private Button btnInPhieuNhap;
		private Button btnThoat;
		private Button btnXoaPhieuNhap;
		private Button btnSuaPhieuNhap;
		private Button btnThemPhieuNhap;
		private Button btnXuatExcelPhieuNhap;
		private Button btnXuatExcelPhuTung;
		private Button btnThoatPhuTung;
		private TabPage tabDieuChinh;
		private GroupBox grpDieuChinh;
		private TableLayoutPanel tlpLichSu;
		private Panel pnlNutPhuTung;
		private TableLayoutPanel tlpPhieuNhap;
	}
}
