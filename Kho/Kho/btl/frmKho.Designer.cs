namespace btl
{
	using System.Drawing;
	using System.Windows.Forms;

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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnPhieuNhap = new System.Windows.Forms.Button();
            this.btnHuyPhuTung = new System.Windows.Forms.Button();
            this.btnXoaPhuTung = new System.Windows.Forms.Button();
            this.dgvPhuTung = new System.Windows.Forms.DataGridView();
            this.colMaPhuTung = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTenPhuTung = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSoLuongTon = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTenNCC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHanSuDung = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhuTung)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(12, 10);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(958, 46);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Kho";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnPhieuNhap);
            this.groupBox1.Controls.Add(this.btnHuyPhuTung);
            this.groupBox1.Controls.Add(this.btnXoaPhuTung);
            this.groupBox1.Controls.Add(this.dgvPhuTung);
            this.groupBox1.Location = new System.Drawing.Point(3, 2);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(955, 289);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Danh sách phụ tùng";
            // 
            // btnPhieuNhap
            // 
            this.btnPhieuNhap.Location = new System.Drawing.Point(16, 237);
            this.btnPhieuNhap.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnPhieuNhap.Name = "btnPhieuNhap";
            this.btnPhieuNhap.Size = new System.Drawing.Size(127, 39);
            this.btnPhieuNhap.TabIndex = 14;
            this.btnPhieuNhap.Text = "Phiếu nhập";
            this.btnPhieuNhap.UseVisualStyleBackColor = true;
            // 
            // btnHuyPhuTung
            // 
            this.btnHuyPhuTung.Location = new System.Drawing.Point(789, 237);
            this.btnHuyPhuTung.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnHuyPhuTung.Name = "btnHuyPhuTung";
            this.btnHuyPhuTung.Size = new System.Drawing.Size(127, 39);
            this.btnHuyPhuTung.TabIndex = 13;
            this.btnHuyPhuTung.Text = "Hủy";
            this.btnHuyPhuTung.UseVisualStyleBackColor = true;
            // 
            // btnXoaPhuTung
            // 
            this.btnXoaPhuTung.Location = new System.Drawing.Point(639, 237);
            this.btnXoaPhuTung.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnXoaPhuTung.Name = "btnXoaPhuTung";
            this.btnXoaPhuTung.Size = new System.Drawing.Size(127, 39);
            this.btnXoaPhuTung.TabIndex = 12;
            this.btnXoaPhuTung.Text = "Xóa";
            this.btnXoaPhuTung.UseVisualStyleBackColor = true;
            // 
            // dgvPhuTung
            // 
            this.dgvPhuTung.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPhuTung.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colMaPhuTung,
            this.colTenPhuTung,
            this.colSoLuongTon,
            this.colTenNCC,
            this.colHanSuDung});
            this.dgvPhuTung.Location = new System.Drawing.Point(6, 21);
            this.dgvPhuTung.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvPhuTung.Name = "dgvPhuTung";
            this.dgvPhuTung.RowHeadersWidth = 51;
            this.dgvPhuTung.Size = new System.Drawing.Size(943, 200);
            this.dgvPhuTung.TabIndex = 0;
            // 
            // colMaPhuTung
            // 
            this.colMaPhuTung.DataPropertyName = "MaPhuTung";
            this.colMaPhuTung.HeaderText = "Mã phụ tùng";
            this.colMaPhuTung.MinimumWidth = 6;
            this.colMaPhuTung.Name = "colMaPhuTung";
            this.colMaPhuTung.Width = 125;
            // 
            // colTenPhuTung
            // 
            this.colTenPhuTung.DataPropertyName = "TenPhuTung";
            this.colTenPhuTung.HeaderText = "Tên phụ tùng";
            this.colTenPhuTung.MinimumWidth = 6;
            this.colTenPhuTung.Name = "colTenPhuTung";
            this.colTenPhuTung.Width = 250;
            // 
            // colSoLuongTon
            // 
            this.colSoLuongTon.DataPropertyName = "SoLuongTon";
            this.colSoLuongTon.HeaderText = "Số lượng tồn";
            this.colSoLuongTon.MinimumWidth = 6;
            this.colSoLuongTon.Name = "colSoLuongTon";
            this.colSoLuongTon.Width = 125;
            // 
            // colTenNCC
            // 
            this.colTenNCC.DataPropertyName = "TenNCC";
            this.colTenNCC.HeaderText = "Nhà cung cấp";
            this.colTenNCC.MinimumWidth = 6;
            this.colTenNCC.Name = "colTenNCC";
            this.colTenNCC.Width = 250;
            // 
            // colHanSuDung
            // 
            this.colHanSuDung.DataPropertyName = "HanSuDung";
            this.colHanSuDung.HeaderText = "Hạn sử dụng";
            this.colHanSuDung.MinimumWidth = 6;
            this.colHanSuDung.Name = "colHanSuDung";
            this.colHanSuDung.Width = 178;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Location = new System.Drawing.Point(12, 61);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(958, 294);
            this.panel2.TabIndex = 1;
            // 
            // frmKho
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(982, 366);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.Name = "frmKho";
            this.Text = "frmKho";
            this.Load += new System.EventHandler(this.frmKho_Load_1);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhuTung)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

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
