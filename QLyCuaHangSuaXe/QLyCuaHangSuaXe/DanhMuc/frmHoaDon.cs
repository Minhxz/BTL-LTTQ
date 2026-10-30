using QLyCuaHangSuaXe.LopDungChung;
using System;
using System.Data;
using System.Windows.Forms;

namespace QLyCuaHangSuaXe.DanhMuc
{
    public partial class frmHoaDon : TrangThietKe
    {
        public frmHoaDon()
        {
            InitializeComponent();
            GanThietKe(dgvHoaDon);
            cboTrangThaiThanhToan.SelectedIndex = 0;
            btnXemChiTiet.Click += (nguoiGui, suKien) => GiaoDien.ThucHien(XemChiTiet);
            btnLamMoi.Click += (nguoiGui, suKien) => GiaoDien.ThucHien(TaiDuLieu);
            btnDong.Click += (nguoiGui, suKien) => Close();
            cboTrangThaiThanhToan.SelectedIndexChanged += (nguoiGui, suKien) => GiaoDien.ThucHien(TaiDuLieu);
            dgvHoaDon.CellDoubleClick += (nguoiGui, suKien) => { if (suKien.RowIndex >= 0) GiaoDien.ThucHien(XemChiTiet); };
        }
        public override void TaiDuLieu()
        {
            PhienDangNhap.YeuCau("HOA_DON");
            dgvDanhSach.DataSource = duLieu.DocBang(@"SELECT p.MaPhieuTiepNhan,COALESCE(h.MaHoaDon,'') AS MaHoaDon,x.BienXe AS BienSo,k.TenKhachHang,p.NgayLap,COALESCE(h.TienThanhToan,t.TongTien,0) AS TongTien,CASE WHEN h.MaHoaDon IS NULL THEN N'Chưa thanh toán' ELSE N'Đã thanh toán' END AS TrangThaiThanhToan,h.NgayThanhToan FROM dbo.PhieuTiepNhan p JOIN dbo.Xe x ON x.MaXe=p.MaXe JOIN dbo.KhachHang k ON k.MaKhachHang=x.MaKhachHang LEFT JOIN dbo.HoaDon h ON h.MaPhieuTiepNhan=p.MaPhieuTiepNhan OUTER APPLY(SELECT SUM(ThanhTien) AS TongTien FROM dbo.ChiTietPhieuTiepNhan c WHERE c.MaPhieuTiepNhan=p.MaPhieuTiepNhan)t WHERE p.TrangThai=N'Hoàn thành' AND (@s=N'Tất cả' OR @s=N'Chưa thanh toán' AND h.MaHoaDon IS NULL OR @s=N'Đã thanh toán' AND h.MaHoaDon IS NOT NULL) ORDER BY p.NgayLap DESC,p.MaPhieuTiepNhan DESC", XuLyDuLieu.ThamSo("@s", cboTrangThaiThanhToan.Text));
        }
        void XemChiTiet()
        {
            string ma = GiaoDien.GiaTriO(dgvDanhSach, "MaPhieuTiepNhan");
            if (ma.Length == 0)
                throw new InvalidOperationException("Chọn phiếu cần xem hoặc thanh toán.");
            using (var trang = new frmChiTietHoaDon(ma))
                trang.ShowDialog(this);
            TaiDuLieu();
        }

        private void lblTieuDe_Click(object sender, EventArgs e)
        {

        }
    }
}
