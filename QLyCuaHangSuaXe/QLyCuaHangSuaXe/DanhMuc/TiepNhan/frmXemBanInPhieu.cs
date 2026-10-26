using QLyCuaHangSuaXe.LopDungChung;
using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace QLyCuaHangSuaXe.DanhMuc.TiepNhan
{
    public partial class frmXemBanInPhieu : Form
    {
        readonly MauInPhieu phieu;
        int trangHienTai;

        public frmXemBanInPhieu() : this(null) { }

        public frmXemBanInPhieu(MauInPhieu mauPhieu)
        {
            InitializeComponent();
            phieu = mauPhieu;
            GiaoDien.DinhDangNut(btnIn);
            btnIn.BackColor = GiaoDien.Xanh;
            btnIn.ForeColor = Color.White;
            GiaoDien.DinhDangNut(btnDong);
            GiaoDien.DinhDangNut(btnTruoc);
            GiaoDien.DinhDangNut(btnSau);
            btnDong.Click += (nguoiGui, suKien) => Close();
            btnTruoc.Click += (nguoiGui, suKien) => HienTrang(trangHienTai - 1);
            btnSau.Click += (nguoiGui, suKien) => HienTrang(trangHienTai + 1);
            btnIn.Click += (nguoiGui, suKien) => GiaoDien.ThucHien(In);
            if (phieu != null)
            {
                Text = "Xem trước phiếu " + phieu.MaPhieu;
                HienTrang(0);
            }
        }

        void HienTrang(int soTrang)
        {
            if (soTrang < 0 || soTrang >= phieu.SoTrang) return;
            var anhCu = picPhieu.Image;
            picPhieu.Image = phieu.TaoAnh(soTrang);
            if (anhCu != null) anhCu.Dispose();
            trangHienTai = soTrang;
            lblTrang.Text = "Trang " + (soTrang + 1) + " / " + phieu.SoTrang + "    •    Khổ A4";
            btnTruoc.Enabled = soTrang > 0;
            btnSau.Enabled = soTrang < phieu.SoTrang - 1;
            pnlBanIn.AutoScrollPosition = Point.Empty;
        }

        void In()
        {
            if (phieu == null) return;
            using (var taiLieu = new PrintDocument())
            using (var chonMayIn = new PrintDialog { Document = taiLieu, UseEXDialog = true, AllowSomePages = true })
            {
                taiLieu.DocumentName = "Phiếu tiếp nhận " + phieu.MaPhieu;
                taiLieu.DefaultPageSettings.PaperSize = new PaperSize("A4", 827, 1169);
                taiLieu.DefaultPageSettings.Landscape = false;
                taiLieu.PrinterSettings.MinimumPage = 1;
                taiLieu.PrinterSettings.MaximumPage = phieu.SoTrang;
                taiLieu.PrinterSettings.FromPage = 1;
                taiLieu.PrinterSettings.ToPage = phieu.SoTrang;
                if (chonMayIn.ShowDialog(this) != DialogResult.OK) return;
                int trang = taiLieu.PrinterSettings.PrintRange == PrintRange.SomePages ? taiLieu.PrinterSettings.FromPage - 1 : 0;
                int cuoi = taiLieu.PrinterSettings.PrintRange == PrintRange.SomePages ? taiLieu.PrinterSettings.ToPage : phieu.SoTrang;
                taiLieu.PrintPage += (nguoiGui, suKien) =>
                {
                    using (var anh = phieu.TaoAnh(trang))
                    {
                        float tiLe = Math.Min((float)suKien.MarginBounds.Width / anh.Width, (float)suKien.MarginBounds.Height / anh.Height);
                        float rong = anh.Width * tiLe, cao = anh.Height * tiLe;
                        suKien.Graphics.DrawImage(anh, suKien.MarginBounds.Left + (suKien.MarginBounds.Width - rong) / 2, suKien.MarginBounds.Top, rong, cao);
                    }
                    trang++;
                    suKien.HasMorePages = trang < cuoi;
                };
                taiLieu.Print();
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs suKien)
        {
            if (picPhieu.Image != null) { picPhieu.Image.Dispose(); picPhieu.Image = null; }
            base.OnFormClosed(suKien);
        }
    }
}
