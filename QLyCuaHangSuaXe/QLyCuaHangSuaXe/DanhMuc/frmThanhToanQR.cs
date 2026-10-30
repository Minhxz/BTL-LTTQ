using QLyCuaHangSuaXe.LopDungChung;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace QLyCuaHangSuaXe.DanhMuc
{
    public partial class frmThanhToanQR : Form
    {
        string maPhieu, maHoaDon;
        public frmThanhToanQR() : this(null, null) { }
        public frmThanhToanQR(string ma, string maHoaDonDuKien = null)
        {
            InitializeComponent();
            maPhieu = ma;
            maHoaDon = maHoaDonDuKien;
            GiaoDien.ApDungMauTiepNhan(this);
            btnDong.Click += (nguoiGui, suKien) => Close();
            btnXacNhan.Click += (nguoiGui, suKien) => GiaoDien.ThucHien(() =>
            {
                if (MessageBox.Show(this, "Đã kiểm tra và nhận đủ tiền chuyển khoản của hóa đơn này?", "Xác nhận nhận tiền", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                    return;
                new NghiepVuSuaXe().ThanhToanPhieu(maPhieu, "Chuyển khoản", maHoaDon);
                DialogResult = DialogResult.OK;
                Close();
            });
        }
        protected override void OnLoad(EventArgs suKien)
        {
            base.OnLoad(suKien);
            if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime || string.IsNullOrEmpty(maPhieu))
                return;
            Height = Math.Min(Height, Screen.FromControl(this).WorkingArea.Height - 32);
            GiaoDien.ThucHien(TaiMaQR);
        }
        public void TaiMaQR()
        {
            PhienDangNhap.YeuCau("HOA_DON");
            var dl = new XuLyDuLieu();
            if (maPhieu.StartsWith("HD", StringComparison.OrdinalIgnoreCase))
            {
                var hd = dl.DocBang("SELECT MaPhieuTiepNhan FROM dbo.HoaDon WHERE MaHoaDon=@id", XuLyDuLieu.ThamSo("@id", maPhieu));
                if (hd.Rows.Count == 1)
                    maPhieu = hd.Rows[0][0].ToString();
            }
            var bang = dl.DocBang(@"SELECT SUM(c.ThanhTien) AS TongTien FROM dbo.PhieuTiepNhan p JOIN dbo.ChiTietPhieuTiepNhan c ON c.MaPhieuTiepNhan=p.MaPhieuTiepNhan WHERE p.MaPhieuTiepNhan=@id AND p.TrangThai=N'Hoàn thành' AND NOT EXISTS(SELECT 1 FROM dbo.HoaDon h WHERE h.MaPhieuTiepNhan=p.MaPhieuTiepNhan) GROUP BY p.MaPhieuTiepNhan", XuLyDuLieu.ThamSo("@id", maPhieu));
            if (bang.Rows.Count != 1)
            {
                btnXacNhan.Enabled = false;
                throw new InvalidOperationException("Phiếu chưa hoàn thành hoặc đã thanh toán.");
            }
            if (string.IsNullOrEmpty(maHoaDon))
                maHoaDon = new NghiepVuSuaXe().LapHoaDon(maPhieu);
            decimal soTien = Convert.ToDecimal(bang.Rows[0]["TongTien"]);
            lblHoaDon.Text = "Hóa đơn " + maHoaDon;
            lblSoTien.Text = soTien.ToString("N0") + " đ";
            lblTaiKhoan.Text = ThanhToanQR.NganHang + "  •  " + ThanhToanQR.SoTaiKhoan;
            lblNoiDung.Text = "Nội dung: THANH TOAN " + maHoaDon;
            if (picMaQR.Image != null)
                picMaQR.Image.Dispose();
            picMaQR.Image = ThanhToanQR.TaoAnh(maHoaDon, soTien);
            btnXacNhan.Enabled = true;
        }
    }
}
