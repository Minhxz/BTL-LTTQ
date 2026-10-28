namespace QLyCuaHangSuaXe.DanhMuc
{
    partial class frmThanhToanQR
    {
        System.ComponentModel.IContainer thanhPhan = null;
        protected override void Dispose(bool dangHuy)
        {
            if (dangHuy)
            {
                if (picMaQR != null && picMaQR.Image != null) { picMaQR.Image.Dispose(); picMaQR.Image = null; }
                if (thanhPhan != null) thanhPhan.Dispose();
            }
            base.Dispose(dangHuy);
        }
        void InitializeComponent()
        {
            this.tlpTrang = new System.Windows.Forms.TableLayoutPanel();
            this.tlpTrang.Name = "tlpTrang";
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblHoaDon = new System.Windows.Forms.Label();
            this.lblHoaDon.Name = "lblHoaDon";
            this.lblSoTien = new System.Windows.Forms.Label();
            this.lblSoTien.Name = "lblSoTien";
            this.picMaQR = new System.Windows.Forms.PictureBox();
            this.picMaQR.Name = "picMaQR";
            this.lblTaiKhoan = new System.Windows.Forms.Label();
            this.lblTaiKhoan.Name = "lblTaiKhoan";
            this.lblNoiDung = new System.Windows.Forms.Label();
            this.lblNoiDung.Name = "lblNoiDung";
            this.flpThaoTac = new System.Windows.Forms.FlowLayoutPanel();
            this.flpThaoTac.Name = "flpThaoTac";
            this.btnXacNhan = new System.Windows.Forms.Button();
            this.btnXacNhan.Name = "btnXacNhan";
            this.btnDong = new System.Windows.Forms.Button();
            this.btnDong.Name = "btnDong";
            this.SuspendLayout();
            this.tlpTrang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpTrang.Padding = new System.Windows.Forms.Padding(24);
            this.tlpTrang.ColumnCount = 1;
            this.tlpTrang.RowCount = 7;
            this.tlpTrang.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent,100F));
            this.tlpTrang.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute,44));
            this.tlpTrang.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute,30));
            this.tlpTrang.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute,44));
            this.tlpTrang.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent,100));
            this.tlpTrang.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute,36));
            this.tlpTrang.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute,30));
            this.tlpTrang.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute,52));
            this.lblTieuDe.Text = "THANH TOÁN BẰNG QR";
            this.lblTieuDe.Font = new System.Drawing.Font("Segoe UI",18F,System.Drawing.FontStyle.Bold);
            this.lblTieuDe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTieuDe.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblHoaDon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHoaDon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblSoTien.Font = new System.Drawing.Font("Segoe UI",22F,System.Drawing.FontStyle.Bold);
            this.lblSoTien.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSoTien.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.picMaQR.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picMaQR.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picMaQR.BackColor = System.Drawing.Color.White;
            this.lblTaiKhoan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTaiKhoan.Font = new System.Drawing.Font("Segoe UI",11F,System.Drawing.FontStyle.Bold);
            this.lblTaiKhoan.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblNoiDung.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNoiDung.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.flpThaoTac.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpThaoTac.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.btnXacNhan.Text = "Đã nhận tiền";
            this.btnXacNhan.Size = new System.Drawing.Size(160,40);
            this.btnXacNhan.Enabled = false;
            this.btnDong.Text = "Đóng";
            this.btnDong.Size = new System.Drawing.Size(110,40);
            this.btnDong.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.flpThaoTac.Controls.Add(this.btnXacNhan);
            this.flpThaoTac.Controls.Add(this.btnDong);
            this.tlpTrang.Controls.Add(this.lblTieuDe, 0, 0);
            this.tlpTrang.Controls.Add(this.lblHoaDon, 0, 1);
            this.tlpTrang.Controls.Add(this.lblSoTien, 0, 2);
            this.tlpTrang.Controls.Add(this.picMaQR, 0, 3);
            this.tlpTrang.Controls.Add(this.lblTaiKhoan, 0, 4);
            this.tlpTrang.Controls.Add(this.lblNoiDung, 0, 5);
            this.tlpTrang.Controls.Add(this.flpThaoTac, 0, 6);
            this.Font = new System.Drawing.Font("Segoe UI",10F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(540,720);
            this.MinimumSize = new System.Drawing.Size(500,560);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Name = "frmThanhToanQR";
            this.Text = "Thanh toán QR";
            this.CancelButton = this.btnDong;
            this.Controls.Add(this.tlpTrang);
            this.ResumeLayout(false);
        }
        System.Windows.Forms.TableLayoutPanel tlpTrang;
        System.Windows.Forms.Label lblTieuDe,lblHoaDon,lblSoTien,lblTaiKhoan,lblNoiDung;
        System.Windows.Forms.PictureBox picMaQR;
        System.Windows.Forms.FlowLayoutPanel flpThaoTac;
        System.Windows.Forms.Button btnXacNhan,btnDong;
    }
}
