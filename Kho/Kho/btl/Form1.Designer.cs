namespace btl
{
	using System.Drawing;
	using System.Windows.Forms;

	partial class frmNhapPhieuNhap
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
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
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnXoaNCC = new System.Windows.Forms.Button();
            this.btnThemNCC = new System.Windows.Forms.Button();
            this.txtMaNVNhap = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpNgayNhap = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.cbNhaCungCap = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMaPhieuNhap = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnHuyPhuTung = new System.Windows.Forms.Button();
            this.btnLamMoiPhuTung = new System.Windows.Forms.Button();
            this.btnXoaPhuTung = new System.Windows.Forms.Button();
            this.btnSuaPhuTung = new System.Windows.Forms.Button();
            this.btnThemPhuTung = new System.Windows.Forms.Button();
            this.txtThanhTien = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.dtpHanSuDung = new System.Windows.Forms.DateTimePicker();
            this.label9 = new System.Windows.Forms.Label();
            this.txtDonGiaNhap = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtSoLuongNhap = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtTenPhuTung = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cbMaPhuTung = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dgvPhuTung = new System.Windows.Forms.DataGridView();
            this.colMaPhieu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTenNCC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNgayNhap = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMaPhuTung = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTenPhuTung = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSoLuongNhap = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDonGiaNhap = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colThanhTien = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHanSuDung = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMaNVNhap = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnThoat = new System.Windows.Forms.Button();
            this.btnLuuPhieu = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhuTung)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnXoaNCC);
            this.groupBox1.Controls.Add(this.btnThemNCC);
            this.groupBox1.Controls.Add(this.txtMaNVNhap);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.dtpNgayNhap);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cbNhaCungCap);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtMaPhieuNhap);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(1295, 123);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin phiếu nhập";
            // 
            // btnXoaNCC
            // 
            this.btnXoaNCC.Location = new System.Drawing.Point(645, 64);
            this.btnXoaNCC.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnXoaNCC.Name = "btnXoaNCC";
            this.btnXoaNCC.Size = new System.Drawing.Size(142, 37);
            this.btnXoaNCC.TabIndex = 20;
            this.btnXoaNCC.Text = "Xóa NCC";
            this.btnXoaNCC.UseVisualStyleBackColor = true;
            // 
            // btnThemNCC
            // 
            this.btnThemNCC.Location = new System.Drawing.Point(454, 64);
            this.btnThemNCC.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnThemNCC.Name = "btnThemNCC";
            this.btnThemNCC.Size = new System.Drawing.Size(142, 37);
            this.btnThemNCC.TabIndex = 19;
            this.btnThemNCC.Text = "Thêm NCC";
            this.btnThemNCC.UseVisualStyleBackColor = true;
            // 
            // txtMaNVNhap
            // 
            this.txtMaNVNhap.Location = new System.Drawing.Point(1020, 24);
            this.txtMaNVNhap.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtMaNVNhap.Name = "txtMaNVNhap";
            this.txtMaNVNhap.Size = new System.Drawing.Size(250, 22);
            this.txtMaNVNhap.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(894, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 16);
            this.label4.TabIndex = 6;
            this.label4.Text = "Mã NV nhập:";
            // 
            // dtpNgayNhap
            // 
            this.dtpNgayNhap.Location = new System.Drawing.Point(580, 23);
            this.dtpNgayNhap.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dtpNgayNhap.Name = "dtpNgayNhap";
            this.dtpNgayNhap.Size = new System.Drawing.Size(250, 22);
            this.dtpNgayNhap.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(454, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Ngày nhập:";
            // 
            // cbNhaCungCap
            // 
            this.cbNhaCungCap.FormattingEnabled = true;
            this.cbNhaCungCap.Location = new System.Drawing.Point(149, 72);
            this.cbNhaCungCap.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbNhaCungCap.Name = "cbNhaCungCap";
            this.cbNhaCungCap.Size = new System.Drawing.Size(246, 24);
            this.cbNhaCungCap.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Nhà cung cấp:";
            // 
            // txtMaPhieuNhap
            // 
            this.txtMaPhieuNhap.Location = new System.Drawing.Point(149, 22);
            this.txtMaPhieuNhap.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtMaPhieuNhap.Name = "txtMaPhieuNhap";
            this.txtMaPhieuNhap.Size = new System.Drawing.Size(246, 22);
            this.txtMaPhieuNhap.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mã phiếu nhập:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.dgvPhuTung);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 123);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1295, 518);
            this.panel1.TabIndex = 2;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnHuyPhuTung);
            this.groupBox2.Controls.Add(this.btnLamMoiPhuTung);
            this.groupBox2.Controls.Add(this.btnXoaPhuTung);
            this.groupBox2.Controls.Add(this.btnSuaPhuTung);
            this.groupBox2.Controls.Add(this.btnThemPhuTung);
            this.groupBox2.Controls.Add(this.txtThanhTien);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.dtpHanSuDung);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.txtDonGiaNhap);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.txtSoLuongNhap);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.txtTenPhuTung);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.cbMaPhuTung);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Size = new System.Drawing.Size(1295, 198);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Thông tin phụ tùng";
            // 
            // btnHuyPhuTung
            // 
            this.btnHuyPhuTung.Location = new System.Drawing.Point(589, 132);
            this.btnHuyPhuTung.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnHuyPhuTung.Name = "btnHuyPhuTung";
            this.btnHuyPhuTung.Size = new System.Drawing.Size(141, 37);
            this.btnHuyPhuTung.TabIndex = 20;
            this.btnHuyPhuTung.Text = "Hủy";
            this.btnHuyPhuTung.UseVisualStyleBackColor = true;
            // 
            // btnLamMoiPhuTung
            // 
            this.btnLamMoiPhuTung.Location = new System.Drawing.Point(774, 132);
            this.btnLamMoiPhuTung.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnLamMoiPhuTung.Name = "btnLamMoiPhuTung";
            this.btnLamMoiPhuTung.Size = new System.Drawing.Size(141, 37);
            this.btnLamMoiPhuTung.TabIndex = 19;
            this.btnLamMoiPhuTung.Text = "Làm mới";
            this.btnLamMoiPhuTung.UseVisualStyleBackColor = true;
            // 
            // btnXoaPhuTung
            // 
            this.btnXoaPhuTung.Location = new System.Drawing.Point(397, 132);
            this.btnXoaPhuTung.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnXoaPhuTung.Name = "btnXoaPhuTung";
            this.btnXoaPhuTung.Size = new System.Drawing.Size(141, 37);
            this.btnXoaPhuTung.TabIndex = 18;
            this.btnXoaPhuTung.Text = "Xóa thông tin";
            this.btnXoaPhuTung.UseVisualStyleBackColor = true;
            // 
            // btnSuaPhuTung
            // 
            this.btnSuaPhuTung.Location = new System.Drawing.Point(205, 132);
            this.btnSuaPhuTung.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSuaPhuTung.Name = "btnSuaPhuTung";
            this.btnSuaPhuTung.Size = new System.Drawing.Size(141, 37);
            this.btnSuaPhuTung.TabIndex = 17;
            this.btnSuaPhuTung.Text = "Sửa thông tin";
            this.btnSuaPhuTung.UseVisualStyleBackColor = true;
            // 
            // btnThemPhuTung
            // 
            this.btnThemPhuTung.Location = new System.Drawing.Point(20, 132);
            this.btnThemPhuTung.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnThemPhuTung.Name = "btnThemPhuTung";
            this.btnThemPhuTung.Size = new System.Drawing.Size(141, 37);
            this.btnThemPhuTung.TabIndex = 16;
            this.btnThemPhuTung.Text = "Thêm phụ tùng";
            this.btnThemPhuTung.UseVisualStyleBackColor = true;
            // 
            // txtThanhTien
            // 
            this.txtThanhTien.Location = new System.Drawing.Point(1020, 86);
            this.txtThanhTien.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtThanhTien.Name = "txtThanhTien";
            this.txtThanhTien.Size = new System.Drawing.Size(250, 22);
            this.txtThanhTien.TabIndex = 15;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(897, 88);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(72, 16);
            this.label10.TabIndex = 14;
            this.label10.Text = "Thành tiền:";
            // 
            // dtpHanSuDung
            // 
            this.dtpHanSuDung.Location = new System.Drawing.Point(1020, 40);
            this.dtpHanSuDung.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dtpHanSuDung.Name = "dtpHanSuDung";
            this.dtpHanSuDung.Size = new System.Drawing.Size(250, 22);
            this.dtpHanSuDung.TabIndex = 9;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(894, 42);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(85, 16);
            this.label9.TabIndex = 8;
            this.label9.Text = "Hạn sử dụng:";
            // 
            // txtDonGiaNhap
            // 
            this.txtDonGiaNhap.Location = new System.Drawing.Point(580, 88);
            this.txtDonGiaNhap.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtDonGiaNhap.Name = "txtDonGiaNhap";
            this.txtDonGiaNhap.Size = new System.Drawing.Size(250, 22);
            this.txtDonGiaNhap.TabIndex = 13;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(454, 88);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(89, 16);
            this.label8.TabIndex = 12;
            this.label8.Text = "Đơn giá nhập:";
            // 
            // txtSoLuongNhap
            // 
            this.txtSoLuongNhap.Location = new System.Drawing.Point(149, 86);
            this.txtSoLuongNhap.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtSoLuongNhap.Name = "txtSoLuongNhap";
            this.txtSoLuongNhap.Size = new System.Drawing.Size(246, 22);
            this.txtSoLuongNhap.TabIndex = 11;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(18, 88);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(96, 16);
            this.label7.TabIndex = 10;
            this.label7.Text = "Số lượng nhập:";
            // 
            // txtTenPhuTung
            // 
            this.txtTenPhuTung.Location = new System.Drawing.Point(580, 39);
            this.txtTenPhuTung.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtTenPhuTung.Name = "txtTenPhuTung";
            this.txtTenPhuTung.Size = new System.Drawing.Size(250, 22);
            this.txtTenPhuTung.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(454, 41);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 16);
            this.label6.TabIndex = 8;
            this.label6.Text = "Tên phụ tùng:";
            // 
            // cbMaPhuTung
            // 
            this.cbMaPhuTung.FormattingEnabled = true;
            this.cbMaPhuTung.Location = new System.Drawing.Point(149, 39);
            this.cbMaPhuTung.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbMaPhuTung.Name = "cbMaPhuTung";
            this.cbMaPhuTung.Size = new System.Drawing.Size(246, 24);
            this.cbMaPhuTung.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 42);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 16);
            this.label5.TabIndex = 0;
            this.label5.Text = "Mã phụ tùng:";
            // 
            // dgvPhuTung
            // 
            this.dgvPhuTung.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPhuTung.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colMaPhieu,
            this.colTenNCC,
            this.colNgayNhap,
            this.colMaPhuTung,
            this.colTenPhuTung,
            this.colSoLuongNhap,
            this.colDonGiaNhap,
            this.colThanhTien,
            this.colHanSuDung,
            this.colMaNVNhap});
            this.dgvPhuTung.Location = new System.Drawing.Point(3, 203);
            this.dgvPhuTung.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvPhuTung.Name = "dgvPhuTung";
            this.dgvPhuTung.RowHeadersWidth = 51;
            this.dgvPhuTung.Size = new System.Drawing.Size(1289, 230);
            this.dgvPhuTung.TabIndex = 0;
            // 
            // colMaPhieu
            // 
            this.colMaPhieu.HeaderText = "Mã phiếu nhập";
            this.colMaPhieu.MinimumWidth = 6;
            this.colMaPhieu.Name = "colMaPhieu";
            this.colMaPhieu.Width = 125;
            // 
            // colTenNCC
            // 
            this.colTenNCC.HeaderText = "Nhà cung cấp";
            this.colTenNCC.MinimumWidth = 6;
            this.colTenNCC.Name = "colTenNCC";
            this.colTenNCC.Width = 125;
            // 
            // colNgayNhap
            // 
            this.colNgayNhap.HeaderText = "Ngày nhập";
            this.colNgayNhap.MinimumWidth = 6;
            this.colNgayNhap.Name = "colNgayNhap";
            this.colNgayNhap.Width = 125;
            // 
            // colMaPhuTung
            // 
            this.colMaPhuTung.HeaderText = "Mã phụ tùng";
            this.colMaPhuTung.MinimumWidth = 6;
            this.colMaPhuTung.Name = "colMaPhuTung";
            this.colMaPhuTung.Width = 125;
            // 
            // colTenPhuTung
            // 
            this.colTenPhuTung.HeaderText = "Tên phụ tùng";
            this.colTenPhuTung.MinimumWidth = 6;
            this.colTenPhuTung.Name = "colTenPhuTung";
            this.colTenPhuTung.Width = 125;
            // 
            // colSoLuongNhap
            // 
            this.colSoLuongNhap.HeaderText = "Số lượng nhập";
            this.colSoLuongNhap.MinimumWidth = 6;
            this.colSoLuongNhap.Name = "colSoLuongNhap";
            this.colSoLuongNhap.Width = 125;
            // 
            // colDonGiaNhap
            // 
            this.colDonGiaNhap.HeaderText = "Đơn giá nhập";
            this.colDonGiaNhap.MinimumWidth = 6;
            this.colDonGiaNhap.Name = "colDonGiaNhap";
            this.colDonGiaNhap.Width = 125;
            // 
            // colThanhTien
            // 
            this.colThanhTien.HeaderText = "Thành tiền";
            this.colThanhTien.MinimumWidth = 6;
            this.colThanhTien.Name = "colThanhTien";
            this.colThanhTien.Width = 125;
            // 
            // colHanSuDung
            // 
            this.colHanSuDung.HeaderText = "Hạn sử dụng";
            this.colHanSuDung.MinimumWidth = 6;
            this.colHanSuDung.Name = "colHanSuDung";
            this.colHanSuDung.Width = 125;
            // 
            // colMaNVNhap
            // 
            this.colMaNVNhap.HeaderText = "Mã NV nhập";
            this.colMaNVNhap.MinimumWidth = 6;
            this.colMaNVNhap.Name = "colMaNVNhap";
            this.colMaNVNhap.Width = 125;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnThoat);
            this.panel2.Controls.Add(this.btnLuuPhieu);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 561);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1295, 80);
            this.panel2.TabIndex = 3;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // btnThoat
            // 
            this.btnThoat.Location = new System.Drawing.Point(192, 19);
            this.btnThoat.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(123, 46);
            this.btnThoat.TabIndex = 3;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.UseVisualStyleBackColor = true;
            // 
            // btnLuuPhieu
            // 
            this.btnLuuPhieu.Location = new System.Drawing.Point(31, 19);
            this.btnLuuPhieu.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnLuuPhieu.Name = "btnLuuPhieu";
            this.btnLuuPhieu.Size = new System.Drawing.Size(123, 46);
            this.btnLuuPhieu.TabIndex = 1;
            this.btnLuuPhieu.Text = "Lưu phiếu";
            this.btnLuuPhieu.UseVisualStyleBackColor = true;
            // 
            // frmNhapPhieuNhap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1295, 641);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.Name = "frmNhapPhieuNhap";
            this.Text = "Form nhập phiếu";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhuTung)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private GroupBox groupBox1;
		private TextBox txtMaNVNhap;
		private Label label4;
		private DateTimePicker dtpNgayNhap;
		private Label label3;
		private ComboBox cbNhaCungCap;
		private Label label2;
		private TextBox txtMaPhieuNhap;
		private Label label1;
		private Panel panel1;
		private DataGridView dgvPhuTung;
		private Panel panel2;
		private Button btnThoat;
		private Button btnLuuPhieu;
		private Button btnXoaNCC;
		private Button btnThemNCC;
		private DataGridViewTextBoxColumn colMaPhieu;
		private DataGridViewTextBoxColumn colTenNCC;
		private DataGridViewTextBoxColumn colNgayNhap;
		private DataGridViewTextBoxColumn colMaPhuTung;
		private DataGridViewTextBoxColumn colTenPhuTung;
		private DataGridViewTextBoxColumn colSoLuongNhap;
		private DataGridViewTextBoxColumn colDonGiaNhap;
		private DataGridViewTextBoxColumn colThanhTien;
		private DataGridViewTextBoxColumn colHanSuDung;
		private DataGridViewTextBoxColumn colMaNVNhap;
		private GroupBox groupBox2;
		private Button btnXoaPhuTung;
		private Button btnSuaPhuTung;
		private Button btnThemPhuTung;
		private TextBox txtThanhTien;
		private Label label10;
		private DateTimePicker dtpHanSuDung;
		private Label label9;
		private TextBox txtDonGiaNhap;
		private Label label8;
		private TextBox txtSoLuongNhap;
		private Label label7;
		private TextBox txtTenPhuTung;
		private Label label6;
		private ComboBox cbMaPhuTung;
		private Label label5;
		private Button btnLamMoiPhuTung;
		private Button btnHuyPhuTung;
	}
}
