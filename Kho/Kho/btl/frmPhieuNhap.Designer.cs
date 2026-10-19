namespace btl
{
	using System.Drawing;
	using System.Windows.Forms;

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
            this.dgvPhieuNhap = new System.Windows.Forms.DataGridView();
            this.colMaPhieuNhap = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNgayNhap = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTongSoLuongNhap = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colThanhTienNhap = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMaNVNhap = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvChiTiet = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnThoat = new System.Windows.Forms.Button();
            this.btnHuyPhieuNhap = new System.Windows.Forms.Button();
            this.btnXoaPhieuNhap = new System.Windows.Forms.Button();
            this.btnSuaPhieuNhap = new System.Windows.Forms.Button();
            this.btnThemPhieuNhap = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhieuNhap)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvPhieuNhap
            // 
            this.dgvPhieuNhap.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPhieuNhap.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colMaPhieuNhap,
            this.colNgayNhap,
            this.colTongSoLuongNhap,
            this.colThanhTienNhap,
            this.colMaNVNhap});
            this.dgvPhieuNhap.Location = new System.Drawing.Point(6, 21);
            this.dgvPhieuNhap.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvPhieuNhap.Name = "dgvPhieuNhap";
            this.dgvPhieuNhap.RowHeadersWidth = 51;
            this.dgvPhieuNhap.Size = new System.Drawing.Size(846, 214);
            this.dgvPhieuNhap.TabIndex = 0;
            // 
            // colMaPhieuNhap
            // 
            this.colMaPhieuNhap.HeaderText = "Mã phiếu nhập";
            this.colMaPhieuNhap.MinimumWidth = 6;
            this.colMaPhieuNhap.Name = "colMaPhieuNhap";
            this.colMaPhieuNhap.Width = 140;
            // 
            // colNgayNhap
            // 
            this.colNgayNhap.HeaderText = "Ngày nhập";
            this.colNgayNhap.MinimumWidth = 6;
            this.colNgayNhap.Name = "colNgayNhap";
            this.colNgayNhap.Width = 175;
            // 
            // colTongSoLuongNhap
            // 
            this.colTongSoLuongNhap.HeaderText = "Tổng số lượng nhập";
            this.colTongSoLuongNhap.MinimumWidth = 6;
            this.colTongSoLuongNhap.Name = "colTongSoLuongNhap";
            this.colTongSoLuongNhap.Width = 125;
            // 
            // colThanhTienNhap
            // 
            this.colThanhTienNhap.HeaderText = "Thành tiền";
            this.colThanhTienNhap.MinimumWidth = 6;
            this.colThanhTienNhap.Name = "colThanhTienNhap";
            this.colThanhTienNhap.Width = 175;
            // 
            // colMaNVNhap
            // 
            this.colMaNVNhap.HeaderText = "Mã NV nhập";
            this.colMaNVNhap.MinimumWidth = 6;
            this.colMaNVNhap.Name = "colMaNVNhap";
            this.colMaNVNhap.Width = 200;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvPhieuNhap);
            this.groupBox2.Location = new System.Drawing.Point(12, 10);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Size = new System.Drawing.Size(858, 237);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Danh sách phiếu nhập";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // dgvChiTiet
            // 
            this.dgvChiTiet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvChiTiet.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5});
            this.dgvChiTiet.Location = new System.Drawing.Point(6, 21);
            this.dgvChiTiet.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvChiTiet.Name = "dgvChiTiet";
            this.dgvChiTiet.RowHeadersWidth = 51;
            this.dgvChiTiet.Size = new System.Drawing.Size(846, 214);
            this.dgvChiTiet.TabIndex = 0;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Mã phiếu nhập";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 125;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Ngày nhập";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 125;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Tổng số lượng nhập";
            this.dataGridViewTextBoxColumn3.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Width = 125;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Thành tiền";
            this.dataGridViewTextBoxColumn4.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Width = 125;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "Mã NV nhập";
            this.dataGridViewTextBoxColumn5.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.Width = 125;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dgvChiTiet);
            this.groupBox3.Location = new System.Drawing.Point(12, 251);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox3.Size = new System.Drawing.Size(858, 239);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Thông tin phiếu nhập";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnThoat);
            this.panel1.Controls.Add(this.btnHuyPhieuNhap);
            this.panel1.Controls.Add(this.btnXoaPhieuNhap);
            this.panel1.Controls.Add(this.btnSuaPhieuNhap);
            this.panel1.Controls.Add(this.btnThemPhieuNhap);
            this.panel1.Location = new System.Drawing.Point(12, 495);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(858, 97);
            this.panel1.TabIndex = 4;
            // 
            // btnThoat
            // 
            this.btnThoat.Location = new System.Drawing.Point(690, 27);
            this.btnThoat.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(139, 42);
            this.btnThoat.TabIndex = 9;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.UseVisualStyleBackColor = true;
            // 
            // btnHuyPhieuNhap
            // 
            this.btnHuyPhieuNhap.Location = new System.Drawing.Point(522, 27);
            this.btnHuyPhieuNhap.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnHuyPhieuNhap.Name = "btnHuyPhieuNhap";
            this.btnHuyPhieuNhap.Size = new System.Drawing.Size(139, 42);
            this.btnHuyPhieuNhap.TabIndex = 8;
            this.btnHuyPhieuNhap.Text = "Hủy";
            this.btnHuyPhieuNhap.UseVisualStyleBackColor = true;
            // 
            // btnXoaPhieuNhap
            // 
            this.btnXoaPhieuNhap.Location = new System.Drawing.Point(354, 27);
            this.btnXoaPhieuNhap.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnXoaPhieuNhap.Name = "btnXoaPhieuNhap";
            this.btnXoaPhieuNhap.Size = new System.Drawing.Size(139, 42);
            this.btnXoaPhieuNhap.TabIndex = 7;
            this.btnXoaPhieuNhap.Text = "Xóa phiếu nhập";
            this.btnXoaPhieuNhap.UseVisualStyleBackColor = true;
            // 
            // btnSuaPhieuNhap
            // 
            this.btnSuaPhieuNhap.Location = new System.Drawing.Point(191, 27);
            this.btnSuaPhieuNhap.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSuaPhieuNhap.Name = "btnSuaPhieuNhap";
            this.btnSuaPhieuNhap.Size = new System.Drawing.Size(139, 42);
            this.btnSuaPhieuNhap.TabIndex = 6;
            this.btnSuaPhieuNhap.Text = "Sửa phiếu nhập";
            this.btnSuaPhieuNhap.UseVisualStyleBackColor = true;
            // 
            // btnThemPhieuNhap
            // 
            this.btnThemPhieuNhap.Location = new System.Drawing.Point(29, 27);
            this.btnThemPhieuNhap.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnThemPhieuNhap.Name = "btnThemPhieuNhap";
            this.btnThemPhieuNhap.Size = new System.Drawing.Size(139, 42);
            this.btnThemPhieuNhap.TabIndex = 5;
            this.btnThemPhieuNhap.Text = "Thêm phiếu nhập";
            this.btnThemPhieuNhap.UseVisualStyleBackColor = true;
            // 
            // frmPhieuNhap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(882, 602);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.Name = "frmPhieuNhap";
            this.Text = "frmPhieuNhap";
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhieuNhap)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

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
