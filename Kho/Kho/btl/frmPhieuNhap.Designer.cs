namespace btl
{
	partial class frmPhieuNhap
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
			dgvPhieuNhap = new DataGridView();
			colMaPhieuNhap = new DataGridViewTextBoxColumn();
			colNgayNhap = new DataGridViewTextBoxColumn();
			colTongSoLuongNhap = new DataGridViewTextBoxColumn();
			colThanhTienNhap = new DataGridViewTextBoxColumn();
			colMaNVNhap = new DataGridViewTextBoxColumn();
			groupBox2 = new GroupBox();
			dgvChiTiet = new DataGridView();
			colChiTietMaPhieuNhap = new DataGridViewTextBoxColumn();
			colChiTietTenNCC = new DataGridViewTextBoxColumn();
			colChiTietMaPhuTung = new DataGridViewTextBoxColumn();
			colChiTietTenPhuTung = new DataGridViewTextBoxColumn();
			colChiTietSoLuongNhap = new DataGridViewTextBoxColumn();
			colChiTietDonGiaNhap = new DataGridViewTextBoxColumn();
			colChiTietThanhTien = new DataGridViewTextBoxColumn();
			colChiTietHanSuDung = new DataGridViewTextBoxColumn();
			groupBox3 = new GroupBox();
			panel1 = new Panel();
			btnInPhieuNhap = new Button();
			btnThoat = new Button();
			btnXoaPhieuNhap = new Button();
			btnSuaPhieuNhap = new Button();
			btnThemPhieuNhap = new Button();
			((System.ComponentModel.ISupportInitialize)dgvPhieuNhap).BeginInit();
			groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dgvChiTiet).BeginInit();
			groupBox3.SuspendLayout();
			panel1.SuspendLayout();
			SuspendLayout();
			// 
			// dgvPhieuNhap
			// 
			dgvPhieuNhap.AllowUserToAddRows = false;
			dgvPhieuNhap.AllowUserToDeleteRows = false;
			dgvPhieuNhap.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dgvPhieuNhap.Columns.AddRange(new DataGridViewColumn[] { colMaPhieuNhap, colNgayNhap, colTongSoLuongNhap, colThanhTienNhap, colMaNVNhap });
			dgvPhieuNhap.Location = new Point(6, 26);
			dgvPhieuNhap.Name = "dgvPhieuNhap";
			dgvPhieuNhap.RowHeadersWidth = 51;
			dgvPhieuNhap.Size = new Size(846, 267);
			dgvPhieuNhap.TabIndex = 0;
			dgvPhieuNhap.CellClick += dgvPhieuNhap_CellClick;
			// 
			// colMaPhieuNhap
			// 
			colMaPhieuNhap.DataPropertyName = "MaPhieuNhap";
			colMaPhieuNhap.HeaderText = "Mã phiếu nhập";
			colMaPhieuNhap.MinimumWidth = 6;
			colMaPhieuNhap.Name = "colMaPhieuNhap";
			colMaPhieuNhap.Width = 140;
			// 
			// colNgayNhap
			// 
			colNgayNhap.DataPropertyName = "NgayNhap";
			colNgayNhap.HeaderText = "Ngày nhập";
			colNgayNhap.MinimumWidth = 6;
			colNgayNhap.Name = "colNgayNhap";
			colNgayNhap.Width = 175;
			// 
			// colTongSoLuongNhap
			// 
			colTongSoLuongNhap.DataPropertyName = "TongSoLuongNhap";
			colTongSoLuongNhap.HeaderText = "Tổng số lượng nhập";
			colTongSoLuongNhap.MinimumWidth = 6;
			colTongSoLuongNhap.Name = "colTongSoLuongNhap";
			colTongSoLuongNhap.Width = 125;
			// 
			// colThanhTienNhap
			// 
			colThanhTienNhap.DataPropertyName = "ThanhTien";
			colThanhTienNhap.HeaderText = "Thành tiền";
			colThanhTienNhap.MinimumWidth = 6;
			colThanhTienNhap.Name = "colThanhTienNhap";
			colThanhTienNhap.Width = 175;
			// 
			// colMaNVNhap
			// 
			colMaNVNhap.DataPropertyName = "NguoiNhap";
			colMaNVNhap.HeaderText = "Mã NV nhập";
			colMaNVNhap.MinimumWidth = 6;
			colMaNVNhap.Name = "colMaNVNhap";
			colMaNVNhap.Width = 200;
			// 
			// groupBox2
			// 
			groupBox2.Controls.Add(dgvPhieuNhap);
			groupBox2.Location = new Point(12, 12);
			groupBox2.Name = "groupBox2";
			groupBox2.Size = new Size(858, 296);
			groupBox2.TabIndex = 2;
			groupBox2.TabStop = false;
			groupBox2.Text = "Danh sách phiếu nhập";
			// 
			// dgvChiTiet
			// 
			dgvChiTiet.AllowUserToAddRows = false;
			dgvChiTiet.AllowUserToDeleteRows = false;
			dgvChiTiet.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dgvChiTiet.Columns.AddRange(new DataGridViewColumn[] { colChiTietMaPhieuNhap, colChiTietTenNCC, colChiTietMaPhuTung, colChiTietTenPhuTung, colChiTietSoLuongNhap, colChiTietDonGiaNhap, colChiTietThanhTien, colChiTietHanSuDung });
			dgvChiTiet.Location = new Point(6, 26);
			dgvChiTiet.Name = "dgvChiTiet";
			dgvChiTiet.RowHeadersWidth = 51;
			dgvChiTiet.Size = new Size(846, 267);
			dgvChiTiet.TabIndex = 0;
			// 
			// colChiTietMaPhieuNhap
			// 
			colChiTietMaPhieuNhap.DataPropertyName = "MaPhieuNhap";
			colChiTietMaPhieuNhap.HeaderText = "Mã phiếu nhập";
			colChiTietMaPhieuNhap.MinimumWidth = 6;
			colChiTietMaPhieuNhap.Name = "colChiTietMaPhieuNhap";
			colChiTietMaPhieuNhap.Width = 125;
			// 
			// colChiTietTenNCC
			// 
			colChiTietTenNCC.DataPropertyName = "TenNCC";
			colChiTietTenNCC.HeaderText = "Nhà cung cấp";
			colChiTietTenNCC.MinimumWidth = 6;
			colChiTietTenNCC.Name = "colChiTietTenNCC";
			colChiTietTenNCC.Width = 125;
			// 
			// colChiTietMaPhuTung
			// 
			colChiTietMaPhuTung.DataPropertyName = "MaPhuTung";
			colChiTietMaPhuTung.HeaderText = "Mã phụ tùng";
			colChiTietMaPhuTung.MinimumWidth = 6;
			colChiTietMaPhuTung.Name = "colChiTietMaPhuTung";
			colChiTietMaPhuTung.Width = 125;
			// 
			// colChiTietTenPhuTung
			// 
			colChiTietTenPhuTung.DataPropertyName = "TenPhuTung";
			colChiTietTenPhuTung.HeaderText = "Tên phụ tùng";
			colChiTietTenPhuTung.MinimumWidth = 6;
			colChiTietTenPhuTung.Name = "colChiTietTenPhuTung";
			colChiTietTenPhuTung.Width = 125;
			// 
			// colChiTietSoLuongNhap
			// 
			colChiTietSoLuongNhap.DataPropertyName = "SoLuongNhap";
			colChiTietSoLuongNhap.HeaderText = "Số lượng nhập";
			colChiTietSoLuongNhap.MinimumWidth = 6;
			colChiTietSoLuongNhap.Name = "colChiTietSoLuongNhap";
			colChiTietSoLuongNhap.Width = 125;
			// 
			// colChiTietDonGiaNhap
			// 
			colChiTietDonGiaNhap.DataPropertyName = "DonGiaNhap";
			colChiTietDonGiaNhap.HeaderText = "Đơn giá nhập";
			colChiTietDonGiaNhap.MinimumWidth = 6;
			colChiTietDonGiaNhap.Name = "colChiTietDonGiaNhap";
			colChiTietDonGiaNhap.Width = 125;
			// 
			// colChiTietThanhTien
			// 
			colChiTietThanhTien.DataPropertyName = "ThanhTien";
			colChiTietThanhTien.HeaderText = "Thành Tiền";
			colChiTietThanhTien.MinimumWidth = 6;
			colChiTietThanhTien.Name = "colChiTietThanhTien";
			colChiTietThanhTien.Width = 125;
			// 
			// colChiTietHanSuDung
			// 
			colChiTietHanSuDung.DataPropertyName = "HanSuDung";
			colChiTietHanSuDung.HeaderText = "Hạn sử dụng";
			colChiTietHanSuDung.MinimumWidth = 6;
			colChiTietHanSuDung.Name = "colChiTietHanSuDung";
			colChiTietHanSuDung.Width = 125;
			// 
			// groupBox3
			// 
			groupBox3.Controls.Add(dgvChiTiet);
			groupBox3.Location = new Point(12, 314);
			groupBox3.Name = "groupBox3";
			groupBox3.Size = new Size(858, 299);
			groupBox3.TabIndex = 3;
			groupBox3.TabStop = false;
			groupBox3.Text = "Thông tin phiếu nhập";
			// 
			// panel1
			// 
			panel1.Controls.Add(btnInPhieuNhap);
			panel1.Controls.Add(btnThoat);
			panel1.Controls.Add(btnXoaPhieuNhap);
			panel1.Controls.Add(btnSuaPhieuNhap);
			panel1.Controls.Add(btnThemPhieuNhap);
			panel1.Location = new Point(12, 619);
			panel1.Name = "panel1";
			panel1.Size = new Size(858, 121);
			panel1.TabIndex = 4;
			// 
			// btnInPhieuNhap
			// 
			btnInPhieuNhap.Location = new Point(525, 34);
			btnInPhieuNhap.Name = "btnInPhieuNhap";
			btnInPhieuNhap.Size = new Size(139, 53);
			btnInPhieuNhap.TabIndex = 11;
			btnInPhieuNhap.Text = "In phiếu nhập";
			btnInPhieuNhap.UseVisualStyleBackColor = true;
			btnInPhieuNhap.Click += btnInPhieuNhap_Click;
			// 
			// btnThoat
			// 
			btnThoat.Location = new Point(690, 34);
			btnThoat.Name = "btnThoat";
			btnThoat.Size = new Size(139, 53);
			btnThoat.TabIndex = 9;
			btnThoat.Text = "Thoát";
			btnThoat.UseVisualStyleBackColor = true;
			btnThoat.Click += btnThoat_Click;
			// 
			// btnXoaPhieuNhap
			// 
			btnXoaPhieuNhap.Location = new Point(354, 34);
			btnXoaPhieuNhap.Name = "btnXoaPhieuNhap";
			btnXoaPhieuNhap.Size = new Size(139, 53);
			btnXoaPhieuNhap.TabIndex = 7;
			btnXoaPhieuNhap.Text = "Xóa phiếu nhập";
			btnXoaPhieuNhap.UseVisualStyleBackColor = true;
			btnXoaPhieuNhap.Click += btnXoaPhieuNhap_Click;
			// 
			// btnSuaPhieuNhap
			// 
			btnSuaPhieuNhap.Location = new Point(191, 34);
			btnSuaPhieuNhap.Name = "btnSuaPhieuNhap";
			btnSuaPhieuNhap.Size = new Size(139, 53);
			btnSuaPhieuNhap.TabIndex = 6;
			btnSuaPhieuNhap.Text = "Sửa phiếu nhập";
			btnSuaPhieuNhap.UseVisualStyleBackColor = true;
			btnSuaPhieuNhap.Click += btnSuaPhieuNhap_Click;
			// 
			// btnThemPhieuNhap
			// 
			btnThemPhieuNhap.Location = new Point(29, 34);
			btnThemPhieuNhap.Name = "btnThemPhieuNhap";
			btnThemPhieuNhap.Size = new Size(139, 53);
			btnThemPhieuNhap.TabIndex = 5;
			btnThemPhieuNhap.Text = "Thêm phiếu nhập";
			btnThemPhieuNhap.UseVisualStyleBackColor = true;
			btnThemPhieuNhap.Click += btnThemPhieuNhap_Click;
			// 
			// frmPhieuNhap
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(882, 752);
			Controls.Add(panel1);
			Controls.Add(groupBox3);
			Controls.Add(groupBox2);
			FormBorderStyle = FormBorderStyle.FixedSingle;
			MaximizeBox = false;
			Name = "frmPhieuNhap";
			Text = "frmPhieuNhap";
			Load += frmPhieuNhap_Load;
			((System.ComponentModel.ISupportInitialize)dgvPhieuNhap).EndInit();
			groupBox2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dgvChiTiet).EndInit();
			groupBox3.ResumeLayout(false);
			panel1.ResumeLayout(false);
			ResumeLayout(false);
		}

		#endregion

		private DataGridView dgvPhieuNhap;
		private GroupBox groupBox2;
		private DataGridView dgvChiTiet;
		private GroupBox groupBox3;
		private Panel panel1;
		private Button btnThoat;
		private Button btnXoaPhieuNhap;
		private Button btnSuaPhieuNhap;
		private Button btnThemPhieuNhap;
		private DataGridViewTextBoxColumn colMaPhieuNhap;
		private DataGridViewTextBoxColumn colNgayNhap;
		private DataGridViewTextBoxColumn colTongSoLuongNhap;
		private DataGridViewTextBoxColumn colThanhTienNhap;
		private DataGridViewTextBoxColumn colMaNVNhap;
		private DataGridViewTextBoxColumn colChiTietMaPhieuNhap;
		private DataGridViewTextBoxColumn colChiTietTenNCC;
		private DataGridViewTextBoxColumn colChiTietMaPhuTung;
		private DataGridViewTextBoxColumn colChiTietTenPhuTung;
		private DataGridViewTextBoxColumn colChiTietSoLuongNhap;
		private DataGridViewTextBoxColumn colChiTietDonGiaNhap;
		private DataGridViewTextBoxColumn colChiTietThanhTien;
		private DataGridViewTextBoxColumn colChiTietHanSuDung;
		private Button btnInPhieuNhap;
	}
}