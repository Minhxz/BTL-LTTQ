using System;
using System.Data;
using System.Windows.Forms;
using QLyCuaHangSuaXe.LopDungChung;

namespace btl
{
    public partial class frmPhieuNhap : TrangThietKe
    {
        public frmPhieuNhap()
        {
            InitializeComponent();
            dgvPhieuNhap.SelectionChanged += (s, e) => GiaoDien.ThucHien(TaiChiTiet);
            GanThietKe(dgvPhieuNhap);
        }
        public override void TaiDuLieu()
        {
            PhienDangNhap.YeuCau("NHAP_KHO");
            dgvPhieuNhap.DataSource=duLieu.DocBang(@"SELECT p.MaPhieuNhap,p.NgayNhap,n.HoTen AS TenNhanVien,COALESCE(SUM(c.SoLuongNhap),0) AS TongSoLuongNhap,p.TongTienNhap AS TongTien,p.GhiChu FROM dbo.PhieuNhap p JOIN dbo.NhanVien n ON n.MaNhanVien=p.MaNhanVienNhap LEFT JOIN dbo.ChiTietPhieuNhap c ON c.MaPhieuNhap=p.MaPhieuNhap WHERE p.MaPhieuNhap LIKE @q OR n.HoTen LIKE @q GROUP BY p.MaPhieuNhap,p.NgayNhap,n.HoTen,p.TongTienNhap,p.GhiChu ORDER BY p.NgayNhap DESC,p.MaPhieuNhap DESC",XuLyDuLieu.ThamSo("@q","%"+txtTimPhieuNhap.Text.Trim()+"%"));TaiChiTiet();
        }
        void TaiChiTiet()
        {
            string ma=GiaoDien.GiaTriO(dgvPhieuNhap,"MaPhieuNhap");
            dgvChiTietPhieuNhap.DataSource=duLieu.DocBang(@"SELECT p.MaPhuTung,p.TenPhuTung,n.TenNhaCungCap AS TenNCC,c.SoLuongNhap,CAST(c.ThanhTien/c.SoLuongNhap AS decimal(18,0)) AS DonGiaNhap,c.ThanhTien,p.HanSuDung FROM dbo.ChiTietPhieuNhap c JOIN dbo.PhuTung p ON p.MaPhuTung=c.MaPhuTung JOIN dbo.NhaCungCap n ON n.MaNhaCungCap=p.MaNhaCungCap WHERE c.MaPhieuNhap=@id ORDER BY c.MaPhuTung",XuLyDuLieu.ThamSo("@id",ma));
        }
        void dgvPhieuNhap_CellClick(object s,DataGridViewCellEventArgs e){GiaoDien.ThucHien(TaiChiTiet);}
        void btnThemPhieuNhap_Bam(object s,EventArgs e){GiaoDien.ThucHien(()=>{using(var trang=new frmNhapPhieuNhap())trang.ShowDialog(this);TaiDuLieu();});}
        void btnSuaPhieuNhap_Bam(object s,EventArgs e){GiaoDien.ThucHien(TaiDuLieu);}
        MauInPhieu TaoBanIn()
        {
            PhienDangNhap.YeuCau("NHAP_KHO");
            string ma = GiaoDien.GiaTriO(dgvPhieuNhap, "MaPhieuNhap");
            if (ma.Length == 0) throw new InvalidOperationException("Chọn phiếu nhập cần in.");
            var thongTin = duLieu.DocBang("SELECT p.NgayNhap,n.HoTen AS TenNhanVien,p.GhiChu FROM dbo.PhieuNhap p JOIN dbo.NhanVien n ON n.MaNhanVien=p.MaNhanVienNhap WHERE p.MaPhieuNhap=@id", XuLyDuLieu.ThamSo("@id",ma));
            if (thongTin.Rows.Count != 1) throw new InvalidOperationException("Không tìm thấy phiếu nhập.");
            var chiTiet = duLieu.DocBang("SELECT N'Phụ tùng' AS Loai,p.TenPhuTung+N' — '+n.TenNhaCungCap AS NoiDung,c.SoLuongNhap AS SoLuong,CAST(c.ThanhTien/c.SoLuongNhap AS decimal(18,0)) AS DonGia,c.ThanhTien FROM dbo.ChiTietPhieuNhap c JOIN dbo.PhuTung p ON p.MaPhuTung=c.MaPhuTung JOIN dbo.NhaCungCap n ON n.MaNhaCungCap=p.MaNhaCungCap WHERE c.MaPhieuNhap=@id ORDER BY c.MaPhuTung", XuLyDuLieu.ThamSo("@id",ma));
            return new MauInPhieu(ma, chiTiet,
                "Ngày nhập: " + Convert.ToDateTime(thongTin.Rows[0]["NgayNhap"]).ToString("dd/MM/yyyy"),
                "Nhân viên nhập: " + thongTin.Rows[0]["TenNhanVien"],
                "Ghi chú: " + thongTin.Rows[0]["GhiChu"]) { LaPhieuNhapKho = true };
        }
        void btnInPhieuNhap_Bam(object s,EventArgs e){GiaoDien.ThucHien(()=>{using(var trang=new QLyCuaHangSuaXe.DanhMuc.TiepNhan.frmXemBanInPhieu(TaoBanIn()))trang.ShowDialog(this);});}
        void btnXuatChiTiet_Bam(object s,EventArgs e){GiaoDien.ThucHien(()=>XuatExcel.LuuExcel((DataTable)dgvChiTietPhieuNhap.DataSource,"Chi tiết phiếu nhập"));}
        void btnThoat_Bam(object s,EventArgs e){Close();}
        void txtTim_TextChanged(object s,EventArgs e){if(IsHandleCreated)GiaoDien.ThucHien(TaiDuLieu);}
    }
}
