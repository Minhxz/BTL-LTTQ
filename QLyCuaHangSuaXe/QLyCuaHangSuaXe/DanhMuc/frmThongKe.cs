using QLyCuaHangSuaXe.LopDungChung;
using System;
using System.Data;
using System.Windows.Forms;

namespace QLyCuaHangSuaXe.DanhMuc
{
    public partial class frmThongKe : TrangThietKe
    {
        public frmThongKe()
        {
            InitializeComponent();
            cboBaoCao.SelectedIndex = 0;
            GanThietKe(dgvThongKe);
            btnThongKe.Click += (nguoiGui,suKien) => GiaoDien.ThucHien(TaiDuLieu);
            btnXuatExcel.Click += (nguoiGui,suKien) => GiaoDien.ThucHien(() => XuatExcel.LuuExcel((DataTable)dgvDanhSach.DataSource,"Thống kê"));
            if (!DesignMode && System.ComponentModel.LicenseManager.UsageMode != System.ComponentModel.LicenseUsageMode.Designtime)
            {
                dtpTuNgay.Value = new DateTime(DateTime.Today.Year,DateTime.Today.Month,1);
                dtpDenNgay.Value = DateTime.Today;
            }
        }
        public override void TaiDuLieu()
        {
            PhienDangNhap.YeuCau("BAO_CAO");if(dtpTuNgay.Value.Date>dtpDenNgay.Value.Date)throw new InvalidOperationException("Ngày bắt đầu phải trước ngày kết thúc.");
            string[] sql={
                "SELECT NgayThanhToan AS Ngay,COUNT(*) AS SoHoaDon,SUM(TienThanhToan) AS DoanhThu FROM dbo.HoaDon WHERE NgayThanhToan>=@a AND NgayThanhToan<@b GROUP BY NgayThanhToan ORDER BY NgayThanhToan",
                @"SELECT n.MaNhanVien,n.HoTen AS TenNhanVien,COUNT(DISTINCT c.MaXe) AS SoXeHoanThanh,SUM(DATEDIFF(MINUTE,c.ThoiGianBatDau,c.ThoiGianHoanThanh)) AS TongPhut FROM dbo.ChiTietPhanCong c JOIN dbo.NhanVien n ON n.MaNhanVien=c.MaNhanVien WHERE c.ThoiGianHoanThanh>=@a AND c.ThoiGianHoanThanh<@b AND EXISTS(SELECT 1 FROM dbo.PhieuTiepNhan p WHERE p.MaXe=c.MaXe AND p.TrangThai=N'Hoàn thành') GROUP BY n.MaNhanVien,n.HoTen ORDER BY SoXeHoanThanh DESC",
                @"SELECT c.MaDichVu,d.TenDichVu,SUM(c.SoLuong) AS SoLuong,SUM(c.ThanhTien) AS DoanhThu FROM dbo.ChiTietPhieuTiepNhan c JOIN dbo.HoaDon h ON h.MaPhieuTiepNhan=c.MaPhieuTiepNhan JOIN dbo.DichVu d ON d.MaDichVu=c.MaDichVu WHERE h.NgayThanhToan>=@a AND h.NgayThanhToan<@b GROUP BY c.MaDichVu,d.TenDichVu ORDER BY DoanhThu DESC",
                "SELECT MaPhuTung,TenPhuTung,SoLuongTon,DonGiaNhap,DonGiaBan,SoLuongTon*DonGiaNhap AS GiaTriTon,HanSuDung,TrangThai FROM dbo.PhuTung ORDER BY MaPhuTung",
                "SELECT p.MaPhieuNhap,p.NgayNhap,n.HoTen AS TenNhanVien,p.TongTienNhap AS TongTien FROM dbo.PhieuNhap p JOIN dbo.NhanVien n ON n.MaNhanVien=p.MaNhanVienNhap WHERE p.NgayNhap>=@a AND p.NgayNhap<@b ORDER BY p.NgayNhap DESC",
                "SELECT TrangThai,COUNT(*) AS SoPhieu,SUM(CASE WHEN NgayHen<CAST(GETDATE() AS DATE) AND TrangThai IN(N'Chờ sửa',N'Đang sửa') THEN 1 ELSE 0 END) AS QuaHen FROM dbo.PhieuTiepNhan WHERE NgayLap>=@a AND NgayLap<@b GROUP BY TrangThai",
                "SELECT n.MaNhanVien,n.HoTen AS TenNhanVien,COUNT(*) AS SoCa,SUM(c.TongThoiGianLamThucTe) AS GioThucLam,SUM(CASE WHEN c.TongThoiGianLamThucTe=0 THEN 1 ELSE 0 END) AS CaVang FROM dbo.ChiTietCaLam c JOIN dbo.NhanVien n ON n.MaNhanVien=c.MaNhanVien WHERE c.NgayLam>=@a AND c.NgayLam<@b GROUP BY n.MaNhanVien,n.HoTen ORDER BY n.HoTen"
            };
            dgvDanhSach.DataSource=duLieu.DocBang(sql[cboBaoCao.SelectedIndex],XuLyDuLieu.ThamSo("@a",dtpTuNgay.Value.Date),XuLyDuLieu.ThamSo("@b",dtpDenNgay.Value.Date.AddDays(1)));
        }
    }
}
