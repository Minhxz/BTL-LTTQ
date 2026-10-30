namespace QLyCuaHangSuaXe.DanhMuc.DichVu
{
    partial class frmDinhMuc
    {
        private System.ComponentModel.IContainer thanhPhan = null;
        protected override void Dispose(bool dangHuy)
        {
            if (dangHuy && thanhPhan != null) thanhPhan.Dispose();
            base.Dispose(dangHuy);
        }
        private void InitializeComponent()
        {
            this.tlpTrang = new System.Windows.Forms.TableLayoutPanel();
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.dgvDinhMuc = new System.Windows.Forms.DataGridView();
            this.Chon = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.MaPhuTung = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TenPhuTung = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SoLuong = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SoLuongTon = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TrangThai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.flpThaoTac = new System.Windows.Forms.FlowLayoutPanel();
            this.btnLuuDinhMuc = new System.Windows.Forms.Button();
            this.btnDong = new System.Windows.Forms.Button();
            this.tlpTrang.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDinhMuc)).BeginInit();
            this.flpThaoTac.SuspendLayout();
            this.SuspendLayout();
            this.tlpTrang.ColumnCount = 1;
            this.tlpTrang.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTrang.Controls.Add(this.lblTieuDe, 0, 0);
            this.tlpTrang.Controls.Add(this.dgvDinhMuc, 0, 1);
            this.tlpTrang.Controls.Add(this.flpThaoTac, 0, 2);
            this.tlpTrang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpTrang.Location = new System.Drawing.Point(0, 0);
            this.tlpTrang.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tlpTrang.Name = "tlpTrang";
            this.tlpTrang.Padding = new System.Windows.Forms.Padding(25, 10, 25, 15);
            this.tlpTrang.RowCount = 3;
            this.tlpTrang.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 95F));
            this.tlpTrang.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTrang.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 85F));
            this.tlpTrang.Size = new System.Drawing.Size(1325, 812);
            this.tlpTrang.TabIndex = 0;
            this.lblTieuDe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTieuDe.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTieuDe.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(85)))), ((int)(((byte)(130)))));
            this.lblTieuDe.Location = new System.Drawing.Point(29, 10);
            this.lblTieuDe.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(1267, 95);
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "BỘ PHỤ TÙNG DỊCH VỤ";
            this.lblTieuDe.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTieuDe.Click += new System.EventHandler(this.lblTieuDe_Click);
            this.dgvDinhMuc.AllowUserToAddRows = false;
            this.dgvDinhMuc.AllowUserToDeleteRows = false;
            this.dgvDinhMuc.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDinhMuc.AutoGenerateColumns = false;
            this.dgvDinhMuc.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.Chon,
                this.MaPhuTung,
                this.TenPhuTung,
                this.SoLuong,
                this.SoLuongTon,
                this.TrangThai});
            this.dgvDinhMuc.BackgroundColor = System.Drawing.Color.White;
            this.dgvDinhMuc.ColumnHeadersHeight = 44;
            this.dgvDinhMuc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDinhMuc.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvDinhMuc.Location = new System.Drawing.Point(25, 105);
            this.dgvDinhMuc.Margin = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.dgvDinhMuc.MultiSelect = false;
            this.dgvDinhMuc.Name = "dgvDinhMuc";
            this.dgvDinhMuc.RowHeadersVisible = false;
            this.dgvDinhMuc.RowHeadersWidth = 51;
            this.dgvDinhMuc.RowTemplate.Height = 38;
            this.dgvDinhMuc.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDinhMuc.Size = new System.Drawing.Size(1275, 597);
            this.dgvDinhMuc.TabIndex = 1;
            this.flpThaoTac.Controls.Add(this.btnLuuDinhMuc);
            this.flpThaoTac.Controls.Add(this.btnDong);
            this.flpThaoTac.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpThaoTac.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flpThaoTac.Location = new System.Drawing.Point(25, 712);
            this.flpThaoTac.Margin = new System.Windows.Forms.Padding(0);
            this.flpThaoTac.Name = "flpThaoTac";
            this.flpThaoTac.Padding = new System.Windows.Forms.Padding(0, 12, 0, 0);
            this.flpThaoTac.Size = new System.Drawing.Size(1275, 85);
            this.flpThaoTac.TabIndex = 2;
            this.flpThaoTac.WrapContents = false;
            this.btnLuuDinhMuc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(108)))), ((int)(((byte)(176)))));
            this.btnLuuDinhMuc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLuuDinhMuc.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLuuDinhMuc.ForeColor = System.Drawing.Color.White;
            this.btnLuuDinhMuc.Location = new System.Drawing.Point(1049, 16);
            this.btnLuuDinhMuc.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnLuuDinhMuc.Name = "btnLuuDinhMuc";
            this.btnLuuDinhMuc.Size = new System.Drawing.Size(222, 52);
            this.btnLuuDinhMuc.TabIndex = 0;
            this.btnLuuDinhMuc.Text = "Lưu định mức";
            this.btnLuuDinhMuc.UseVisualStyleBackColor = false;
            this.btnLuuDinhMuc.Click += new System.EventHandler(this.btnLuuDinhMuc_Bam);
            this.btnDong.BackColor = System.Drawing.Color.White;
            this.btnDong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDong.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(85)))), ((int)(((byte)(130)))));
            this.btnDong.Location = new System.Drawing.Point(892, 16);
            this.btnDong.Margin = new System.Windows.Forms.Padding(4, 4, 15, 4);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(138, 52);
            this.btnDong.TabIndex = 1;
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = false;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Bam);
            this.Chon.DataPropertyName = "Chon";
            this.Chon.FillWeight = 60F;
            this.Chon.HeaderText = "Sử dụng";
            this.Chon.Name = "Chon";
            this.Chon.ReadOnly = false;
            this.MaPhuTung.DataPropertyName = "MaPhuTung";
            this.MaPhuTung.FillWeight = 90F;
            this.MaPhuTung.HeaderText = "Mã phụ tùng";
            this.MaPhuTung.Name = "MaPhuTung";
            this.MaPhuTung.ReadOnly = true;
            this.TenPhuTung.DataPropertyName = "TenPhuTung";
            this.TenPhuTung.FillWeight = 190F;
            this.TenPhuTung.HeaderText = "Tên phụ tùng";
            this.TenPhuTung.Name = "TenPhuTung";
            this.TenPhuTung.ReadOnly = true;
            this.SoLuong.DataPropertyName = "SoLuong";
            this.SoLuong.FillWeight = 90F;
            this.SoLuong.HeaderText = "Số lượng";
            this.SoLuong.Name = "SoLuong";
            this.SoLuong.ReadOnly = false;
            this.SoLuongTon.DataPropertyName = "SoLuongTon";
            this.SoLuongTon.FillWeight = 90F;
            this.SoLuongTon.HeaderText = "Tồn kho";
            this.SoLuongTon.Name = "SoLuongTon";
            this.SoLuongTon.ReadOnly = true;
            this.TrangThai.DataPropertyName = "TrangThai";
            this.TrangThai.FillWeight = 150F;
            this.TrangThai.HeaderText = "Trạng thái";
            this.TrangThai.Name = "TrangThai";
            this.TrangThai.ReadOnly = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(247)))), ((int)(((byte)(251)))));
            this.ClientSize = new System.Drawing.Size(1325, 812);
            this.Controls.Add(this.tlpTrang);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MinimumSize = new System.Drawing.Size(1096, 613);
            this.Name = "frmDinhMuc";
            this.Text = "Bộ phụ tùng dịch vụ";
            this.tlpTrang.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDinhMuc)).EndInit();
            this.flpThaoTac.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        private System.Windows.Forms.TableLayoutPanel tlpTrang;
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.DataGridView dgvDinhMuc;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Chon;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaPhuTung;
        private System.Windows.Forms.DataGridViewTextBoxColumn TenPhuTung;
        private System.Windows.Forms.DataGridViewTextBoxColumn SoLuong;
        private System.Windows.Forms.DataGridViewTextBoxColumn SoLuongTon;
        private System.Windows.Forms.DataGridViewTextBoxColumn TrangThai;
        private System.Windows.Forms.FlowLayoutPanel flpThaoTac;
        private System.Windows.Forms.Button btnLuuDinhMuc;
        private System.Windows.Forms.Button btnDong;
    }
}
