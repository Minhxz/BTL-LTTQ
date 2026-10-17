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
			btnPhieuNhap = new Button();
			btnHuyPhuTung = new Button();
			btnXoaPhuTung = new Button();
			dgvPhuTung = new DataGridView();
			panel2 = new Panel();
			colMaPhuTung = new DataGridViewTextBoxColumn();
			colTenPhuTung = new DataGridViewTextBoxColumn();
			colSoLuongTon = new DataGridViewTextBoxColumn();
			colTenNCC = new DataGridViewTextBoxColumn();
			colHanSuDung = new DataGridViewTextBoxColumn();
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
			groupBox1.Controls.Add(btnPhieuNhap);
			groupBox1.Controls.Add(btnHuyPhuTung);
			groupBox1.Controls.Add(btnXoaPhuTung);
			groupBox1.Controls.Add(dgvPhuTung);
			groupBox1.Location = new Point(3, 3);
			groupBox1.Name = "groupBox1";
			groupBox1.Size = new Size(955, 361);
			groupBox1.TabIndex = 0;
			groupBox1.TabStop = false;
			groupBox1.Text = "Danh sách phụ tùng";
			// 
			// btnPhieuNhap
			// 
			btnPhieuNhap.Location = new Point(16, 296);
			btnPhieuNhap.Name = "btnPhieuNhap";
			btnPhieuNhap.Size = new Size(127, 49);
			btnPhieuNhap.TabIndex = 14;
			btnPhieuNhap.Text = "Phiếu nhập";
			btnPhieuNhap.UseVisualStyleBackColor = true;
			// 
			// btnHuyPhuTung
			// 
			btnHuyPhuTung.Location = new Point(789, 296);
			btnHuyPhuTung.Name = "btnHuyPhuTung";
			btnHuyPhuTung.Size = new Size(127, 49);
			btnHuyPhuTung.TabIndex = 13;
			btnHuyPhuTung.Text = "Hủy";
			btnHuyPhuTung.UseVisualStyleBackColor = true;
			// 
			// btnXoaPhuTung
			// 
			btnXoaPhuTung.Location = new Point(639, 296);
			btnXoaPhuTung.Name = "btnXoaPhuTung";
			btnXoaPhuTung.Size = new Size(127, 49);
			btnXoaPhuTung.TabIndex = 12;
			btnXoaPhuTung.Text = "Xóa";
			btnXoaPhuTung.UseVisualStyleBackColor = true;
			// 
			// dgvPhuTung
			// 
			dgvPhuTung.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dgvPhuTung.Columns.AddRange(new DataGridViewColumn[] { colMaPhuTung, colTenPhuTung, colSoLuongTon, colTenNCC, colHanSuDung });
			dgvPhuTung.Location = new Point(6, 26);
			dgvPhuTung.Name = "dgvPhuTung";
			dgvPhuTung.RowHeadersWidth = 51;
			dgvPhuTung.Size = new Size(943, 250);
			dgvPhuTung.TabIndex = 0;
			// 
			// panel2
			// 
			panel2.Controls.Add(groupBox1);
			panel2.Location = new Point(12, 76);
			panel2.Name = "panel2";
			panel2.Size = new Size(958, 367);
			panel2.TabIndex = 1;
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
			// frmKho
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(982, 457);
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
		private Button btnXoaPhuTung;
		private DataGridView dgvPhuTung;
		private Panel panel2;
		private Button btnPhieuNhap;
		private DataGridViewTextBoxColumn colMaPhuTung;
		private DataGridViewTextBoxColumn colTenPhuTung;
		private DataGridViewTextBoxColumn colSoLuongTon;
		private DataGridViewTextBoxColumn colTenNCC;
		private DataGridViewTextBoxColumn colHanSuDung;
	}
}