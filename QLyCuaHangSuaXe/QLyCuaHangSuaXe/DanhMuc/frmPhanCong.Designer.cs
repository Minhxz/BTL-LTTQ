namespace QLyCuaHangSuaXe.DanhMuc
{
    partial class frmPhanCong
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
            this.flpTienDo = new System.Windows.Forms.FlowLayoutPanel();
            this.btnBatDauSua = new System.Windows.Forms.Button();
            this.btnHoanThanh = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.btnHuyPhieu = new System.Windows.Forms.Button();
            this.grbGiaoXe = new System.Windows.Forms.GroupBox();
            this.tlpGiaoXe = new System.Windows.Forms.TableLayoutPanel();
            this.lblMaPhieu = new System.Windows.Forms.Label();
            this.txtMaPhieu = new System.Windows.Forms.TextBox();
            this.lblTho = new System.Windows.Forms.Label();
            this.cboTho = new System.Windows.Forms.ComboBox();
            this.btnGiaoXe = new System.Windows.Forms.Button();
            this.dgvPhanCong = new System.Windows.Forms.DataGridView();
            this.tlpTrang.SuspendLayout();
            this.flpTienDo.SuspendLayout();
            this.grbGiaoXe.SuspendLayout();
            this.tlpGiaoXe.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhanCong)).BeginInit();
            this.SuspendLayout();
            // 
            // tlpTrang
            // 
            this.tlpTrang.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(247)))), ((int)(((byte)(251)))));
            this.tlpTrang.ColumnCount = 1;
            this.tlpTrang.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTrang.Controls.Add(this.lblTieuDe, 0, 0);
            this.tlpTrang.Controls.Add(this.flpTienDo, 0, 1);
            this.tlpTrang.Controls.Add(this.grbGiaoXe, 0, 2);
            this.tlpTrang.Controls.Add(this.dgvPhanCong, 0, 3);
            this.tlpTrang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpTrang.Location = new System.Drawing.Point(0, 0);
            this.tlpTrang.Margin = new System.Windows.Forms.Padding(0);
            this.tlpTrang.Name = "tlpTrang";
            this.tlpTrang.Padding = new System.Windows.Forms.Padding(16);
            this.tlpTrang.RowCount = 4;
            this.tlpTrang.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 62F));
            this.tlpTrang.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 64F));
            this.tlpTrang.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 86F));
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
            this.lblTieuDe.Text = "PHÂN CÔNG VÀ TIẾN ĐỘ SỬA CHỮA";
            this.lblTieuDe.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // flpTienDo
            // 
            this.flpTienDo.Controls.Add(this.btnBatDauSua);
            this.flpTienDo.Controls.Add(this.btnHoanThanh);
            this.flpTienDo.Controls.Add(this.btnLamMoi);
            this.flpTienDo.Controls.Add(this.btnHuyPhieu);
            this.flpTienDo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpTienDo.Location = new System.Drawing.Point(16, 78);
            this.flpTienDo.Margin = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.flpTienDo.Name = "flpTienDo";
            this.flpTienDo.Size = new System.Drawing.Size(1148, 56);
            this.flpTienDo.TabIndex = 1;
            this.flpTienDo.WrapContents = false;
            // 
            // btnBatDauSua
            // 
            this.btnBatDauSua.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(85)))), ((int)(((byte)(130)))));
            this.btnBatDauSua.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBatDauSua.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(207)))), ((int)(((byte)(220)))));
            this.btnBatDauSua.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBatDauSua.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnBatDauSua.ForeColor = System.Drawing.Color.White;
            this.btnBatDauSua.Location = new System.Drawing.Point(0, 6);
            this.btnBatDauSua.Margin = new System.Windows.Forms.Padding(0, 6, 12, 6);
            this.btnBatDauSua.Name = "btnBatDauSua";
            this.btnBatDauSua.Size = new System.Drawing.Size(154, 40);
            this.btnBatDauSua.TabIndex = 0;
            this.btnBatDauSua.Text = "Bắt đầu sửa";
            this.btnBatDauSua.UseVisualStyleBackColor = false;
            // 
            // btnHoanThanh
            // 
            this.btnHoanThanh.BackColor = System.Drawing.Color.White;
            this.btnHoanThanh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHoanThanh.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(207)))), ((int)(((byte)(220)))));
            this.btnHoanThanh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHoanThanh.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnHoanThanh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(85)))), ((int)(((byte)(130)))));
            this.btnHoanThanh.Location = new System.Drawing.Point(166, 6);
            this.btnHoanThanh.Margin = new System.Windows.Forms.Padding(0, 6, 12, 6);
            this.btnHoanThanh.Name = "btnHoanThanh";
            this.btnHoanThanh.Size = new System.Drawing.Size(150, 40);
            this.btnHoanThanh.TabIndex = 1;
            this.btnHoanThanh.Text = "Hoàn thành";
            this.btnHoanThanh.UseVisualStyleBackColor = false;
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.BackColor = System.Drawing.Color.White;
            this.btnLamMoi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLamMoi.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(207)))), ((int)(((byte)(220)))));
            this.btnLamMoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLamMoi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(85)))), ((int)(((byte)(130)))));
            this.btnLamMoi.Location = new System.Drawing.Point(328, 6);
            this.btnLamMoi.Margin = new System.Windows.Forms.Padding(0, 6, 12, 6);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(120, 40);
            this.btnLamMoi.TabIndex = 2;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.UseVisualStyleBackColor = false;
            // 
            // btnHuyPhieu
            // 
            this.btnHuyPhieu.BackColor = System.Drawing.Color.White;
            this.btnHuyPhieu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHuyPhieu.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(207)))), ((int)(((byte)(220)))));
            this.btnHuyPhieu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHuyPhieu.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnHuyPhieu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(85)))), ((int)(((byte)(130)))));
            this.btnHuyPhieu.Location = new System.Drawing.Point(460, 6);
            this.btnHuyPhieu.Margin = new System.Windows.Forms.Padding(0, 6, 12, 6);
            this.btnHuyPhieu.Name = "btnHuyPhieu";
            this.btnHuyPhieu.Size = new System.Drawing.Size(180, 40);
            this.btnHuyPhieu.TabIndex = 3;
            this.btnHuyPhieu.Text = "Hủy xe chưa sửa";
            this.btnHuyPhieu.UseVisualStyleBackColor = false;
            // 
            // grbGiaoXe
            // 
            this.grbGiaoXe.BackColor = System.Drawing.Color.White;
            this.grbGiaoXe.Controls.Add(this.tlpGiaoXe);
            this.grbGiaoXe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbGiaoXe.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grbGiaoXe.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(85)))), ((int)(((byte)(130)))));
            this.grbGiaoXe.Location = new System.Drawing.Point(16, 142);
            this.grbGiaoXe.Margin = new System.Windows.Forms.Padding(0, 0, 0, 12);
            this.grbGiaoXe.Name = "grbGiaoXe";
            this.grbGiaoXe.Padding = new System.Windows.Forms.Padding(12, 4, 12, 8);
            this.grbGiaoXe.Size = new System.Drawing.Size(1148, 74);
            this.grbGiaoXe.TabIndex = 2;
            this.grbGiaoXe.TabStop = false;
            this.grbGiaoXe.Text = "Giao xe cho thợ";
            // 
            // tlpGiaoXe
            // 
            this.tlpGiaoXe.ColumnCount = 5;
            this.tlpGiaoXe.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tlpGiaoXe.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tlpGiaoXe.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpGiaoXe.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpGiaoXe.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 128F));
            this.tlpGiaoXe.Controls.Add(this.lblMaPhieu, 0, 0);
            this.tlpGiaoXe.Controls.Add(this.txtMaPhieu, 1, 0);
            this.tlpGiaoXe.Controls.Add(this.lblTho, 2, 0);
            this.tlpGiaoXe.Controls.Add(this.cboTho, 3, 0);
            this.tlpGiaoXe.Controls.Add(this.btnGiaoXe, 4, 0);
            this.tlpGiaoXe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpGiaoXe.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tlpGiaoXe.Location = new System.Drawing.Point(12, 27);
            this.tlpGiaoXe.Margin = new System.Windows.Forms.Padding(0);
            this.tlpGiaoXe.Name = "tlpGiaoXe";
            this.tlpGiaoXe.RowCount = 1;
            this.tlpGiaoXe.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpGiaoXe.Size = new System.Drawing.Size(1124, 39);
            this.tlpGiaoXe.TabIndex = 0;
            // 
            // lblMaPhieu
            // 
            this.lblMaPhieu.AutoEllipsis = true;
            this.lblMaPhieu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMaPhieu.Location = new System.Drawing.Point(3, 0);
            this.lblMaPhieu.Name = "lblMaPhieu";
            this.lblMaPhieu.Size = new System.Drawing.Size(74, 39);
            this.lblMaPhieu.TabIndex = 0;
            this.lblMaPhieu.Text = "Mã phiếu";
            this.lblMaPhieu.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtMaPhieu
            // 
            this.txtMaPhieu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMaPhieu.Location = new System.Drawing.Point(80, 8);
            this.txtMaPhieu.Margin = new System.Windows.Forms.Padding(0, 8, 18, 4);
            this.txtMaPhieu.MaxLength = 10;
            this.txtMaPhieu.Name = "txtMaPhieu";
            this.txtMaPhieu.Size = new System.Drawing.Size(132, 30);
            this.txtMaPhieu.TabIndex = 1;
            // 
            // lblTho
            // 
            this.lblTho.AutoEllipsis = true;
            this.lblTho.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTho.Location = new System.Drawing.Point(233, 0);
            this.lblTho.Name = "lblTho";
            this.lblTho.Size = new System.Drawing.Size(94, 39);
            this.lblTho.TabIndex = 2;
            this.lblTho.Text = "Thợ nhận xe";
            this.lblTho.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboTho
            // 
            this.cboTho.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboTho.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTho.Location = new System.Drawing.Point(330, 8);
            this.cboTho.Margin = new System.Windows.Forms.Padding(0, 8, 18, 4);
            this.cboTho.Name = "cboTho";
            this.cboTho.Size = new System.Drawing.Size(648, 31);
            this.cboTho.TabIndex = 3;
            // 
            // btnGiaoXe
            // 
            this.btnGiaoXe.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(85)))), ((int)(((byte)(130)))));
            this.btnGiaoXe.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGiaoXe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnGiaoXe.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(207)))), ((int)(((byte)(220)))));
            this.btnGiaoXe.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGiaoXe.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnGiaoXe.ForeColor = System.Drawing.Color.White;
            this.btnGiaoXe.Location = new System.Drawing.Point(996, 2);
            this.btnGiaoXe.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.btnGiaoXe.Name = "btnGiaoXe";
            this.btnGiaoXe.Size = new System.Drawing.Size(128, 35);
            this.btnGiaoXe.TabIndex = 4;
            this.btnGiaoXe.Text = "Giao xe";
            this.btnGiaoXe.UseVisualStyleBackColor = false;
            // 
            // dgvPhanCong
            // 
            this.dgvPhanCong.AllowUserToAddRows = false;
            this.dgvPhanCong.AllowUserToDeleteRows = false;
            this.dgvPhanCong.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(251)))), ((int)(((byte)(254)))));
            this.dgvPhanCong.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvPhanCong.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPhanCong.BackgroundColor = System.Drawing.Color.White;
            this.dgvPhanCong.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(240)))), ((int)(((byte)(248)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(85)))), ((int)(((byte)(130)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPhanCong.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvPhanCong.ColumnHeadersHeight = 44;
            this.dgvPhanCong.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 10F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(235)))), ((int)(((byte)(249)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(64)))), ((int)(((byte)(92)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPhanCong.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvPhanCong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPhanCong.EnableHeadersVisualStyles = false;
            this.dgvPhanCong.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(230)))), ((int)(((byte)(238)))));
            this.dgvPhanCong.Location = new System.Drawing.Point(16, 228);
            this.dgvPhanCong.Margin = new System.Windows.Forms.Padding(0);
            this.dgvPhanCong.MultiSelect = false;
            this.dgvPhanCong.Name = "dgvPhanCong";
            this.dgvPhanCong.ReadOnly = true;
            this.dgvPhanCong.RowHeadersVisible = false;
            this.dgvPhanCong.RowHeadersWidth = 51;
            this.dgvPhanCong.RowTemplate.Height = 38;
            this.dgvPhanCong.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPhanCong.Size = new System.Drawing.Size(1148, 536);
            this.dgvPhanCong.TabIndex = 3;
            // 
            // frmPhanCong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1180, 780);
            this.Controls.Add(this.tlpTrang);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.MinimumSize = new System.Drawing.Size(960, 620);
            this.Name = "frmPhanCong";
            this.Text = "Phân công và tiến độ sửa chữa";
            this.tlpTrang.ResumeLayout(false);
            this.flpTienDo.ResumeLayout(false);
            this.grbGiaoXe.ResumeLayout(false);
            this.tlpGiaoXe.ResumeLayout(false);
            this.tlpGiaoXe.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhanCong)).EndInit();
            this.ResumeLayout(false);

        }
        private System.Windows.Forms.TableLayoutPanel tlpTrang;
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.FlowLayoutPanel flpTienDo;
        private System.Windows.Forms.Button btnBatDauSua;
        private System.Windows.Forms.Button btnHoanThanh;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Button btnHuyPhieu;
        private System.Windows.Forms.GroupBox grbGiaoXe;
        private System.Windows.Forms.TableLayoutPanel tlpGiaoXe;
        private System.Windows.Forms.Label lblMaPhieu;
        private System.Windows.Forms.TextBox txtMaPhieu;
        private System.Windows.Forms.Label lblTho;
        private System.Windows.Forms.ComboBox cboTho;
        private System.Windows.Forms.Button btnGiaoXe;
        private System.Windows.Forms.DataGridView dgvPhanCong;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaPhieuTiepNhan;
        private System.Windows.Forms.DataGridViewTextBoxColumn BienSo;
        private System.Windows.Forms.DataGridViewTextBoxColumn TenKhachHang;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaNhanVien;
        private System.Windows.Forms.DataGridViewTextBoxColumn TenNhanVien;
        private System.Windows.Forms.DataGridViewTextBoxColumn TrangThai;
        private System.Windows.Forms.DataGridViewTextBoxColumn ThoiGianBatDau;
        private System.Windows.Forms.DataGridViewTextBoxColumn ThoiGianHoanThanh;
        private System.Windows.Forms.DataGridViewTextBoxColumn NgayHen;
    }
}
