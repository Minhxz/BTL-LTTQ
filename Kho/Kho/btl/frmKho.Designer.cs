namespace btl
{
	partial class frmKho
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
			panel1 = new Panel();
			label1 = new Label();
			groupBox1 = new GroupBox();
			btnNgungSuDung = new Button();
			btnThoat = new Button();
			btnDanhSachPhieuNhap = new Button();
			dgvPhuTung = new DataGridView();
			colMaPhuTung = new DataGridViewTextBoxColumn();
			colTenPhuTung = new DataGridViewTextBoxColumn();
			colSoLuongTon = new DataGridViewTextBoxColumn();
			colTenNCC = new DataGridViewTextBoxColumn();
			colHanSuDung = new DataGridViewTextBoxColumn();
			panel2 = new Panel();
			panel1.SuspendLayout();
			groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dgvPhuTung).BeginInit();
			panel2.SuspendLayout();
			SuspendLayout();
			// 
			// panel1
			// 
			panel1.Controls.Add(label1);
			panel1.Location = new Point(12, 12);
			panel1.Name = "panel1";
			panel1.Size = new Size(958, 58);
			panel1.TabIndex = 0;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(19, 19);
			label1.Name = "label1";
			label1.Size = new Size(35, 20);
			label1.TabIndex = 0;
			label1.Text = "Kho";
			// 
			// groupBox1
			// 
			groupBox1.Controls.Add(btnNgungSuDung);
			groupBox1.Controls.Add(btnThoat);
			groupBox1.Controls.Add(btnDanhSachPhieuNhap);
			groupBox1.Controls.Add(dgvPhuTung);
			groupBox1.Location = new Point(3, 3);
			groupBox1.Name = "groupBox1";
			groupBox1.Size = new Size(955, 549);
			groupBox1.TabIndex = 0;
			groupBox1.TabStop = false;
			groupBox1.Text = "Danh sách phụ tùng";
			// 
			// btnNgungSuDung
			// 
			btnNgungSuDung.Location = new Point(650, 494);
			btnNgungSuDung.Name = "btnNgungSuDung";
			btnNgungSuDung.RightToLeft = RightToLeft.No;
			btnNgungSuDung.Size = new Size(127, 49);
			btnNgungSuDung.TabIndex = 16;
			btnNgungSuDung.Text = "Ngưng sử dụng";
			btnNgungSuDung.UseVisualStyleBackColor = true;
			btnNgungSuDung.Click += btnNgungSuDung_Click;
			// 
			// btnThoat
			// 
			btnThoat.Location = new Point(806, 494);
			btnThoat.Name = "btnThoat";
			btnThoat.RightToLeft = RightToLeft.No;
			btnThoat.Size = new Size(127, 49);
			btnThoat.TabIndex = 15;
			btnThoat.Text = "Thoát";
			btnThoat.UseVisualStyleBackColor = true;
			btnThoat.Click += btnThoat_Click_1;
			// 
			// btnDanhSachPhieuNhap
			// 
			btnDanhSachPhieuNhap.Location = new Point(16, 494);
			btnDanhSachPhieuNhap.Name = "btnDanhSachPhieuNhap";
			btnDanhSachPhieuNhap.Size = new Size(201, 49);
			btnDanhSachPhieuNhap.TabIndex = 14;
			btnDanhSachPhieuNhap.Text = "Danh sách phiếu nhập";
			btnDanhSachPhieuNhap.UseVisualStyleBackColor = true;
			btnDanhSachPhieuNhap.Click += btnDanhSachPhieuNhap_Click;
			// 
			// dgvPhuTung
			// 
			dgvPhuTung.AllowUserToAddRows = false;
			dgvPhuTung.AllowUserToDeleteRows = false;
			dgvPhuTung.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dgvPhuTung.Columns.AddRange(new DataGridViewColumn[] { colMaPhuTung, colTenPhuTung, colSoLuongTon, colTenNCC, colHanSuDung });
			dgvPhuTung.Location = new Point(6, 26);
			dgvPhuTung.Name = "dgvPhuTung";
			dgvPhuTung.RowHeadersWidth = 51;
			dgvPhuTung.Size = new Size(943, 462);
			dgvPhuTung.TabIndex = 0;
			// 
			// colMaPhuTung
			// 
			colMaPhuTung.DataPropertyName = "MaPhuTung";
			colMaPhuTung.HeaderText = "Mã phụ tùng";
			colMaPhuTung.MinimumWidth = 6;
			colMaPhuTung.Name = "colMaPhuTung";
			colMaPhuTung.Width = 125;
			// 
			// colTenPhuTung
			// 
			colTenPhuTung.DataPropertyName = "TenPhuTung";
			colTenPhuTung.HeaderText = "Tên phụ tùng";
			colTenPhuTung.MinimumWidth = 6;
			colTenPhuTung.Name = "colTenPhuTung";
			colTenPhuTung.Width = 250;
			// 
			// colSoLuongTon
			// 
			colSoLuongTon.DataPropertyName = "SoLuongTon";
			colSoLuongTon.HeaderText = "Số lượng tồn";
			colSoLuongTon.MinimumWidth = 6;
			colSoLuongTon.Name = "colSoLuongTon";
			colSoLuongTon.Width = 125;
			// 
			// colTenNCC
			// 
			colTenNCC.DataPropertyName = "TenNCC";
			colTenNCC.HeaderText = "Nhà cung cấp";
			colTenNCC.MinimumWidth = 6;
			colTenNCC.Name = "colTenNCC";
			colTenNCC.Width = 250;
			// 
			// colHanSuDung
			// 
			colHanSuDung.DataPropertyName = "HanSuDung";
			colHanSuDung.HeaderText = "Hạn sử dụng";
			colHanSuDung.MinimumWidth = 6;
			colHanSuDung.Name = "colHanSuDung";
			colHanSuDung.Width = 178;
			// 
			// panel2
			// 
			panel2.Controls.Add(groupBox1);
			panel2.Location = new Point(12, 76);
			panel2.Name = "panel2";
			panel2.Size = new Size(958, 555);
			panel2.TabIndex = 1;
			// 
			// frmKho
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(982, 643);
			Controls.Add(panel2);
			Controls.Add(panel1);
			FormBorderStyle = FormBorderStyle.FixedSingle;
			MaximizeBox = false;
			Name = "frmKho";
			Text = "frmKho";
			Load += frmKho_Load;
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			groupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dgvPhuTung).EndInit();
			panel2.ResumeLayout(false);
			ResumeLayout(false);
		}

		#endregion

		private Panel panel1;
		private Label label1;
		private GroupBox groupBox1;
		private Button btnHuyPhuTung;
		private DataGridView dgvPhuTung;
		private Panel panel2;
		private Button btnDanhSachPhieuNhap;
		private DataGridViewTextBoxColumn colMaPhuTung;
		private DataGridViewTextBoxColumn colTenPhuTung;
		private DataGridViewTextBoxColumn colSoLuongTon;
		private DataGridViewTextBoxColumn colTenNCC;
		private DataGridViewTextBoxColumn colHanSuDung;
		private Button btnThoat;
		private Button btnNgungSuDung;
	}
}