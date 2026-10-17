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
			groupBox2 = new GroupBox();
			dgvChiTiet = new DataGridView();
			dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
			dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
			dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
			dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
			dataGridViewTextBoxColumn5 = new DataGridViewTextBoxColumn();
			groupBox3 = new GroupBox();
			panel1 = new Panel();
			btnThoat = new Button();
			btnHuyPhieuNhap = new Button();
			btnXoaPhieuNhap = new Button();
			btnSuaPhieuNhap = new Button();
			btnThemPhieuNhap = new Button();
			colMaPhieuNhap = new DataGridViewTextBoxColumn();
			colNgayNhap = new DataGridViewTextBoxColumn();
			colTongSoLuongNhap = new DataGridViewTextBoxColumn();
			colThanhTienNhap = new DataGridViewTextBoxColumn();
			colMaNVNhap = new DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)dgvPhieuNhap).BeginInit();
			groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dgvChiTiet).BeginInit();
			groupBox3.SuspendLayout();
			panel1.SuspendLayout();
			SuspendLayout();
			// 
			// dgvPhieuNhap
			// 
			dgvPhieuNhap.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dgvPhieuNhap.Columns.AddRange(new DataGridViewColumn[] { colMaPhieuNhap, colNgayNhap, colTongSoLuongNhap, colThanhTienNhap, colMaNVNhap });
			dgvPhieuNhap.Location = new Point(6, 26);
			dgvPhieuNhap.Name = "dgvPhieuNhap";
			dgvPhieuNhap.RowHeadersWidth = 51;
			dgvPhieuNhap.Size = new Size(846, 267);
			dgvPhieuNhap.TabIndex = 0;
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
			dgvChiTiet.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dgvChiTiet.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn1, dataGridViewTextBoxColumn2, dataGridViewTextBoxColumn3, dataGridViewTextBoxColumn4, dataGridViewTextBoxColumn5 });
			dgvChiTiet.Location = new Point(6, 26);
			dgvChiTiet.Name = "dgvChiTiet";
			dgvChiTiet.RowHeadersWidth = 51;
			dgvChiTiet.Size = new Size(846, 267);
			dgvChiTiet.TabIndex = 0;
			// 
			// dataGridViewTextBoxColumn1
			// 
			dataGridViewTextBoxColumn1.HeaderText = "Mã phiếu nhập";
			dataGridViewTextBoxColumn1.MinimumWidth = 6;
			dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
			dataGridViewTextBoxColumn1.Width = 125;
			// 
			// dataGridViewTextBoxColumn2
			// 
			dataGridViewTextBoxColumn2.HeaderText = "Ngày nhập";
			dataGridViewTextBoxColumn2.MinimumWidth = 6;
			dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
			dataGridViewTextBoxColumn2.Width = 125;
			// 
			// dataGridViewTextBoxColumn3
			// 
			dataGridViewTextBoxColumn3.HeaderText = "Tổng số lượng nhập";
			dataGridViewTextBoxColumn3.MinimumWidth = 6;
			dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
			dataGridViewTextBoxColumn3.Width = 125;
			// 
			// dataGridViewTextBoxColumn4
			// 
			dataGridViewTextBoxColumn4.HeaderText = "Thành tiền";
			dataGridViewTextBoxColumn4.MinimumWidth = 6;
			dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
			dataGridViewTextBoxColumn4.Width = 125;
			// 
			// dataGridViewTextBoxColumn5
			// 
			dataGridViewTextBoxColumn5.HeaderText = "Mã NV nhập";
			dataGridViewTextBoxColumn5.MinimumWidth = 6;
			dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
			dataGridViewTextBoxColumn5.Width = 125;
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
			panel1.Controls.Add(btnThoat);
			panel1.Controls.Add(btnHuyPhieuNhap);
			panel1.Controls.Add(btnXoaPhieuNhap);
			panel1.Controls.Add(btnSuaPhieuNhap);
			panel1.Controls.Add(btnThemPhieuNhap);
			panel1.Location = new Point(12, 619);
			panel1.Name = "panel1";
			panel1.Size = new Size(858, 121);
			panel1.TabIndex = 4;
			// 
			// btnThoat
			// 
			btnThoat.Location = new Point(690, 34);
			btnThoat.Name = "btnThoat";
			btnThoat.Size = new Size(139, 53);
			btnThoat.TabIndex = 9;
			btnThoat.Text = "Thoát";
			btnThoat.UseVisualStyleBackColor = true;
			// 
			// btnHuyPhieuNhap
			// 
			btnHuyPhieuNhap.Location = new Point(522, 34);
			btnHuyPhieuNhap.Name = "btnHuyPhieuNhap";
			btnHuyPhieuNhap.Size = new Size(139, 53);
			btnHuyPhieuNhap.TabIndex = 8;
			btnHuyPhieuNhap.Text = "Hủy";
			btnHuyPhieuNhap.UseVisualStyleBackColor = true;
			// 
			// btnXoaPhieuNhap
			// 
			btnXoaPhieuNhap.Location = new Point(354, 34);
			btnXoaPhieuNhap.Name = "btnXoaPhieuNhap";
			btnXoaPhieuNhap.Size = new Size(139, 53);
			btnXoaPhieuNhap.TabIndex = 7;
			btnXoaPhieuNhap.Text = "Xóa phiếu nhập";
			btnXoaPhieuNhap.UseVisualStyleBackColor = true;
			// 
			// btnSuaPhieuNhap
			// 
			btnSuaPhieuNhap.Location = new Point(191, 34);
			btnSuaPhieuNhap.Name = "btnSuaPhieuNhap";
			btnSuaPhieuNhap.Size = new Size(139, 53);
			btnSuaPhieuNhap.TabIndex = 6;
			btnSuaPhieuNhap.Text = "Sửa phiếu nhập";
			btnSuaPhieuNhap.UseVisualStyleBackColor = true;
			// 
			// btnThemPhieuNhap
			// 
			btnThemPhieuNhap.Location = new Point(29, 34);
			btnThemPhieuNhap.Name = "btnThemPhieuNhap";
			btnThemPhieuNhap.Size = new Size(139, 53);
			btnThemPhieuNhap.TabIndex = 5;
			btnThemPhieuNhap.Text = "Thêm phiếu nhập";
			btnThemPhieuNhap.UseVisualStyleBackColor = true;
			// 
			// colMaPhieuNhap
			// 
			colMaPhieuNhap.HeaderText = "Mã phiếu nhập";
			colMaPhieuNhap.MinimumWidth = 6;
			colMaPhieuNhap.Name = "colMaPhieuNhap";
			colMaPhieuNhap.Width = 140;
			// 
			// colNgayNhap
			// 
			colNgayNhap.HeaderText = "Ngày nhập";
			colNgayNhap.MinimumWidth = 6;
			colNgayNhap.Name = "colNgayNhap";
			colNgayNhap.Width = 175;
			// 
			// colTongSoLuongNhap
			// 
			colTongSoLuongNhap.HeaderText = "Tổng số lượng nhập";
			colTongSoLuongNhap.MinimumWidth = 6;
			colTongSoLuongNhap.Name = "colTongSoLuongNhap";
			colTongSoLuongNhap.Width = 125;
			// 
			// colThanhTienNhap
			// 
			colThanhTienNhap.HeaderText = "Thành tiền";
			colThanhTienNhap.MinimumWidth = 6;
			colThanhTienNhap.Name = "colThanhTienNhap";
			colThanhTienNhap.Width = 175;
			// 
			// colMaNVNhap
			// 
			colMaNVNhap.HeaderText = "Mã NV nhập";
			colMaNVNhap.MinimumWidth = 6;
			colMaNVNhap.Name = "colMaNVNhap";
			colMaNVNhap.Width = 200;
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
		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
		private GroupBox groupBox3;
		private Panel panel1;
		private Button btnThoat;
		private Button btnHuyPhieuNhap;
		private Button btnXoaPhieuNhap;
		private Button btnSuaPhieuNhap;
		private Button btnThemPhieuNhap;
		private DataGridViewTextBoxColumn colMaPhieuNhap;
		private DataGridViewTextBoxColumn colNgayNhap;
		private DataGridViewTextBoxColumn colTongSoLuongNhap;
		private DataGridViewTextBoxColumn colThanhTienNhap;
		private DataGridViewTextBoxColumn colMaNVNhap;
	}
}