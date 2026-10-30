namespace QLyCuaHangSuaXe.DanhMuc
{
    partial class frmHoaDon
    {
        private System.ComponentModel.IContainer thanhPhan = null;
        protected override void Dispose(bool dangHuy)
        {
            if (dangHuy && thanhPhan != null) thanhPhan.Dispose();
            base.Dispose(dangHuy);
        }
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tlpTrang = new System.Windows.Forms.TableLayoutPanel();
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.tlpBoLoc = new System.Windows.Forms.TableLayoutPanel();
            this.lblTrangThaiThanhToan = new System.Windows.Forms.Label();
            this.cboTrangThaiThanhToan = new System.Windows.Forms.ComboBox();
            this.btnXemChiTiet = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.btnDong = new System.Windows.Forms.Button();
            this.dgvHoaDon = new System.Windows.Forms.DataGridView();
            this.tlpTrang.SuspendLayout();
            this.tlpBoLoc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHoaDon)).BeginInit();
            this.SuspendLayout();
            // 
            // tlpTrang
            // 
            this.tlpTrang.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(247)))), ((int)(((byte)(251)))));
            this.tlpTrang.ColumnCount = 1;
            this.tlpTrang.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTrang.Controls.Add(this.lblTieuDe, 0, 0);
            this.tlpTrang.Controls.Add(this.tlpBoLoc, 0, 1);
            this.tlpTrang.Controls.Add(this.dgvHoaDon, 0, 2);
            this.tlpTrang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpTrang.Location = new System.Drawing.Point(0, 0);
            this.tlpTrang.Margin = new System.Windows.Forms.Padding(0);
            this.tlpTrang.Name = "tlpTrang";
            this.tlpTrang.Padding = new System.Windows.Forms.Padding(16);
            this.tlpTrang.RowCount = 3;
            this.tlpTrang.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 62F));
            this.tlpTrang.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 64F));
            this.tlpTrang.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTrang.Size = new System.Drawing.Size(1180, 780);
            this.tlpTrang.TabIndex = 0;
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.AutoEllipsis = true;
            this.lblTieuDe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTieuDe.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTieuDe.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(85)))), ((int)(((byte)(130)))));
            this.lblTieuDe.Location = new System.Drawing.Point(16, 16);
            this.lblTieuDe.Margin = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(1148, 54);
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "HÓA ĐƠN VÀ THANH TOÁN";
            this.lblTieuDe.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTieuDe.Click += new System.EventHandler(this.lblTieuDe_Click);
            // 
            // tlpBoLoc
            // 
            this.tlpBoLoc.BackColor = System.Drawing.Color.White;
            this.tlpBoLoc.ColumnCount = 6;
            this.tlpBoLoc.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpBoLoc.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 210F));
            this.tlpBoLoc.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpBoLoc.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 204F));
            this.tlpBoLoc.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tlpBoLoc.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 108F));
            this.tlpBoLoc.Controls.Add(this.lblTrangThaiThanhToan, 0, 0);
            this.tlpBoLoc.Controls.Add(this.cboTrangThaiThanhToan, 1, 0);
            this.tlpBoLoc.Controls.Add(this.btnXemChiTiet, 3, 0);
            this.tlpBoLoc.Controls.Add(this.btnLamMoi, 4, 0);
            this.tlpBoLoc.Controls.Add(this.btnDong, 5, 0);
            this.tlpBoLoc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpBoLoc.Location = new System.Drawing.Point(16, 78);
            this.tlpBoLoc.Margin = new System.Windows.Forms.Padding(0, 0, 0, 12);
            this.tlpBoLoc.Name = "tlpBoLoc";
            this.tlpBoLoc.Padding = new System.Windows.Forms.Padding(12, 4, 12, 4);
            this.tlpBoLoc.RowCount = 1;
            this.tlpBoLoc.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpBoLoc.Size = new System.Drawing.Size(1148, 52);
            this.tlpBoLoc.TabIndex = 1;
            // 
            // lblTrangThaiThanhToan
            // 
            this.lblTrangThaiThanhToan.AutoEllipsis = true;
            this.lblTrangThaiThanhToan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTrangThaiThanhToan.Location = new System.Drawing.Point(15, 4);
            this.lblTrangThaiThanhToan.Name = "lblTrangThaiThanhToan";
            this.lblTrangThaiThanhToan.Size = new System.Drawing.Size(94, 44);
            this.lblTrangThaiThanhToan.TabIndex = 0;
            this.lblTrangThaiThanhToan.Text = "Trạng thái";
            this.lblTrangThaiThanhToan.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboTrangThaiThanhToan
            // 
            this.cboTrangThaiThanhToan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboTrangThaiThanhToan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTrangThaiThanhToan.Items.AddRange(new object[] {
            "Chưa thanh toán",
            "Đã thanh toán",
            "Tất cả"});
            this.cboTrangThaiThanhToan.Location = new System.Drawing.Point(112, 12);
            this.cboTrangThaiThanhToan.Margin = new System.Windows.Forms.Padding(0, 8, 12, 4);
            this.cboTrangThaiThanhToan.Name = "cboTrangThaiThanhToan";
            this.cboTrangThaiThanhToan.Size = new System.Drawing.Size(198, 31);
            this.cboTrangThaiThanhToan.TabIndex = 1;
            // 
            // btnXemChiTiet
            // 
            this.btnXemChiTiet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(85)))), ((int)(((byte)(130)))));
            this.btnXemChiTiet.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXemChiTiet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnXemChiTiet.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(207)))), ((int)(((byte)(220)))));
            this.btnXemChiTiet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXemChiTiet.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnXemChiTiet.ForeColor = System.Drawing.Color.White;
            this.btnXemChiTiet.Location = new System.Drawing.Point(704, 7);
            this.btnXemChiTiet.Margin = new System.Windows.Forms.Padding(0, 3, 10, 3);
            this.btnXemChiTiet.Name = "btnXemChiTiet";
            this.btnXemChiTiet.Size = new System.Drawing.Size(194, 38);
            this.btnXemChiTiet.TabIndex = 2;
            this.btnXemChiTiet.Text = "Thanh toán / Chi tiết";
            this.btnXemChiTiet.UseVisualStyleBackColor = false;
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.BackColor = System.Drawing.Color.White;
            this.btnLamMoi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLamMoi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnLamMoi.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(207)))), ((int)(((byte)(220)))));
            this.btnLamMoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLamMoi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(85)))), ((int)(((byte)(130)))));
            this.btnLamMoi.Location = new System.Drawing.Point(908, 7);
            this.btnLamMoi.Margin = new System.Windows.Forms.Padding(0, 3, 10, 3);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(110, 38);
            this.btnLamMoi.TabIndex = 3;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.UseVisualStyleBackColor = false;
            // 
            // btnDong
            // 
            this.btnDong.BackColor = System.Drawing.Color.White;
            this.btnDong.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDong.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(207)))), ((int)(((byte)(220)))));
            this.btnDong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDong.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnDong.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(85)))), ((int)(((byte)(130)))));
            this.btnDong.Location = new System.Drawing.Point(1028, 7);
            this.btnDong.Margin = new System.Windows.Forms.Padding(0, 3, 10, 3);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(98, 38);
            this.btnDong.TabIndex = 4;
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = false;
            // 
            // dgvHoaDon
            // 
            this.dgvHoaDon.AllowUserToAddRows = false;
            this.dgvHoaDon.AllowUserToDeleteRows = false;
            this.dgvHoaDon.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(251)))), ((int)(((byte)(254)))));
            this.dgvHoaDon.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvHoaDon.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvHoaDon.BackgroundColor = System.Drawing.Color.White;
            this.dgvHoaDon.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(240)))), ((int)(((byte)(248)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(85)))), ((int)(((byte)(130)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvHoaDon.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvHoaDon.ColumnHeadersHeight = 44;
            this.dgvHoaDon.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 10F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(235)))), ((int)(((byte)(249)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(64)))), ((int)(((byte)(92)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvHoaDon.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvHoaDon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvHoaDon.EnableHeadersVisualStyles = false;
            this.dgvHoaDon.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(230)))), ((int)(((byte)(238)))));
            this.dgvHoaDon.Location = new System.Drawing.Point(16, 142);
            this.dgvHoaDon.Margin = new System.Windows.Forms.Padding(0);
            this.dgvHoaDon.MultiSelect = false;
            this.dgvHoaDon.Name = "dgvHoaDon";
            this.dgvHoaDon.ReadOnly = true;
            this.dgvHoaDon.RowHeadersVisible = false;
            this.dgvHoaDon.RowHeadersWidth = 51;
            this.dgvHoaDon.RowTemplate.Height = 38;
            this.dgvHoaDon.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvHoaDon.Size = new System.Drawing.Size(1148, 622);
            this.dgvHoaDon.TabIndex = 2;
            // 
            // frmHoaDon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1180, 780);
            this.Controls.Add(this.tlpTrang);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.MinimumSize = new System.Drawing.Size(960, 620);
            this.Name = "frmHoaDon";
            this.Text = "Hóa đơn và thanh toán";
            this.tlpTrang.ResumeLayout(false);
            this.tlpBoLoc.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHoaDon)).EndInit();
            this.ResumeLayout(false);

        }
        private System.Windows.Forms.TableLayoutPanel tlpTrang;
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.TableLayoutPanel tlpBoLoc;
        private System.Windows.Forms.Label lblTrangThaiThanhToan;
        private System.Windows.Forms.ComboBox cboTrangThaiThanhToan;
        private System.Windows.Forms.Button btnXemChiTiet;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Button btnDong;
        private System.Windows.Forms.DataGridView dgvHoaDon;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaPhieuTiepNhan;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaHoaDon;
        private System.Windows.Forms.DataGridViewTextBoxColumn BienSo;
        private System.Windows.Forms.DataGridViewTextBoxColumn TenKhachHang;
        private System.Windows.Forms.DataGridViewTextBoxColumn NgayLap;
        private System.Windows.Forms.DataGridViewTextBoxColumn TongTien;
        private System.Windows.Forms.DataGridViewTextBoxColumn TrangThaiThanhToan;
        private System.Windows.Forms.DataGridViewTextBoxColumn NgayThanhToan;
    }
}
