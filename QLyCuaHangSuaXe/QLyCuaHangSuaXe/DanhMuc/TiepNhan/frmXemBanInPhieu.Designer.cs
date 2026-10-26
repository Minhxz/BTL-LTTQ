namespace QLyCuaHangSuaXe.DanhMuc.TiepNhan
{
    partial class frmXemBanInPhieu
    {
        System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (picPhieu != null && picPhieu.Image != null) { picPhieu.Image.Dispose(); picPhieu.Image = null; }
                if (components != null) components.Dispose();
            }
            base.Dispose(disposing);
        }

        void InitializeComponent()
        {
            this.tlpTrang = new System.Windows.Forms.TableLayoutPanel();
            this.flpThaoTac = new System.Windows.Forms.FlowLayoutPanel();
            this.btnIn = new System.Windows.Forms.Button();
            this.btnTruoc = new System.Windows.Forms.Button();
            this.btnSau = new System.Windows.Forms.Button();
            this.btnDong = new System.Windows.Forms.Button();
            this.lblTrang = new System.Windows.Forms.Label();
            this.pnlBanIn = new System.Windows.Forms.Panel();
            this.picPhieu = new System.Windows.Forms.PictureBox();
            this.tlpTrang.SuspendLayout();
            this.flpThaoTac.SuspendLayout();
            this.pnlBanIn.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)this.picPhieu).BeginInit();
            this.SuspendLayout();
            this.tlpTrang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpTrang.ColumnCount = 1;
            this.tlpTrang.RowCount = 2;
            this.tlpTrang.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTrang.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 66F));
            this.tlpTrang.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTrang.Controls.Add(this.flpThaoTac, 0, 0);
            this.tlpTrang.Controls.Add(this.pnlBanIn, 0, 1);
            this.flpThaoTac.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpThaoTac.Padding = new System.Windows.Forms.Padding(12, 8, 8, 8);
            this.flpThaoTac.WrapContents = false;
            this.flpThaoTac.Controls.Add(this.btnIn);
            this.flpThaoTac.Controls.Add(this.btnTruoc);
            this.flpThaoTac.Controls.Add(this.btnSau);
            this.flpThaoTac.Controls.Add(this.lblTrang);
            this.flpThaoTac.Controls.Add(this.btnDong);
            this.btnIn.Text = "In phiếu";
            this.btnIn.Size = new System.Drawing.Size(120, 38);
            this.btnTruoc.Text = "‹ Trang trước";
            this.btnTruoc.Size = new System.Drawing.Size(120, 38);
            this.btnSau.Text = "Trang sau ›";
            this.btnSau.Size = new System.Drawing.Size(120, 38);
            this.lblTrang.Text = "Xem trước bản in • A4";
            this.lblTrang.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTrang.Size = new System.Drawing.Size(245, 38);
            this.btnDong.Text = "Đóng";
            this.btnDong.Size = new System.Drawing.Size(90, 38);
            this.btnDong.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.pnlBanIn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBanIn.AutoScroll = true;
            this.pnlBanIn.BackColor = System.Drawing.Color.FromArgb(218, 226, 234);
            this.pnlBanIn.Controls.Add(this.picPhieu);
            this.picPhieu.Location = new System.Drawing.Point(24, 16);
            this.picPhieu.Size = new System.Drawing.Size(794, 1123);
            this.picPhieu.BackColor = System.Drawing.Color.White;
            this.picPhieu.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.ClientSize = new System.Drawing.Size(884, 820);
            this.MinimumSize = new System.Drawing.Size(870, 540);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Xem trước phiếu tiếp nhận";
            this.CancelButton = this.btnDong;
            this.Controls.Add(this.tlpTrang);
            this.tlpTrang.ResumeLayout(false);
            this.flpThaoTac.ResumeLayout(false);
            this.pnlBanIn.ResumeLayout(false);
            this.pnlBanIn.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)this.picPhieu).EndInit();
            this.ResumeLayout(false);
        }

        System.Windows.Forms.TableLayoutPanel tlpTrang;
        System.Windows.Forms.FlowLayoutPanel flpThaoTac;
        System.Windows.Forms.Button btnIn, btnTruoc, btnSau, btnDong;
        System.Windows.Forms.Label lblTrang;
        System.Windows.Forms.Panel pnlBanIn;
        System.Windows.Forms.PictureBox picPhieu;
    }
}
