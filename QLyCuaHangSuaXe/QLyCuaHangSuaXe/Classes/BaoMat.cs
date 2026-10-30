using System;
using System.Security.Cryptography;
using System.Text;

namespace QLyCuaHangSuaXe.LopDungChung
{
    public static class BaoMat
    {
        public static string BamMatKhau(string matKhau)
        {
            if (string.IsNullOrWhiteSpace(matKhau))
                throw new InvalidOperationException("Mật khẩu không được để trống.");
            using (var boBam = SHA256.Create())
                return BitConverter.ToString(boBam.ComputeHash(Encoding.UTF8.GetBytes(matKhau))).Replace("-", "");
        }
        public static bool KiemTraMatKhau(string matKhau, string chuoiBam)
        {
            if (string.IsNullOrWhiteSpace(matKhau) || chuoiBam == null || chuoiBam.Length != 64)
                return false;
            string bam = BamMatKhau(matKhau);
            int saiKhac = 0;
            for (int i = 0; i < bam.Length; i++)
                saiKhac |= bam[i] ^ char.ToUpperInvariant(chuoiBam[i]);
            return saiKhac == 0;
        }
    }
    public static class PhienDangNhap
    {
        public static string MaNhanVien
        {
            get; private set;
        }
        public static string TenDangNhap
        {
            get; private set;
        }
        public static string VaiTro
        {
            get; private set;
        }
        public static void DangXuat()
        {
            MaNhanVien = TenDangNhap = VaiTro = null;
        }
        public static bool DangNhap(string tenDangNhap, string matKhau)
        {
            DangXuat();
            var bang = new XuLyDuLieu().DocBang(@"SELECT n.MaNhanVien,n.TenDangNhap,n.MatKhau,c.TenChucVu FROM dbo.NhanVien n JOIN dbo.ChucVu c ON c.MaChucVu=n.MaChucVu WHERE n.TenDangNhap=@u AND n.TrangThai=1", XuLyDuLieu.ThamSo("@u", tenDangNhap));
            if (bang.Rows.Count != 1 || !BaoMat.KiemTraMatKhau(matKhau, bang.Rows[0]["MatKhau"].ToString()))
                return false;
            string vaiTro = PhanQuyenNguoiDung.ChuanHoa(bang.Rows[0]["TenChucVu"].ToString());
            if (!PhanQuyenNguoiDung.CoVaiTro(vaiTro))
                return false;
            MaNhanVien = bang.Rows[0]["MaNhanVien"].ToString();
            TenDangNhap = bang.Rows[0]["TenDangNhap"].ToString();
            VaiTro = vaiTro;
            return true;
        }
        public static void DoiMatKhau(string matKhauCu, string matKhauMoi)
        {
            if (string.IsNullOrEmpty(MaNhanVien))
                throw new InvalidOperationException("Cần đăng nhập.");
            string ma = MaNhanVien;
            new XuLyDuLieu().TrongGiaoDich<int>((ketNoi, giaoDich) =>
            {
                var bang = XuLyDuLieu.DocDuLieu(ketNoi, giaoDich, "SELECT MatKhau FROM dbo.NhanVien WITH(UPDLOCK) WHERE MaNhanVien=@id AND TrangThai=1", XuLyDuLieu.ThamSo("@id", ma));
                if (bang.Rows.Count != 1 || !BaoMat.KiemTraMatKhau(matKhauCu, bang.Rows[0][0].ToString()))
                    throw new InvalidOperationException("Mật khẩu hiện tại không đúng.");
                return XuLyDuLieu.ThucThi(ketNoi, giaoDich, "UPDATE dbo.NhanVien SET MatKhau=@h WHERE MaNhanVien=@id", XuLyDuLieu.ThamSo("@h", BaoMat.BamMatKhau(matKhauMoi)), XuLyDuLieu.ThamSo("@id", ma));
            });
        }
        public static void YeuCau(string maQuyen)
        {
            if (string.IsNullOrEmpty(MaNhanVien))
                throw new UnauthorizedAccessException("Cần đăng nhập trước khi sử dụng chức năng.");
            var bang = new XuLyDuLieu().DocBang("SELECT c.TenChucVu,n.TenDangNhap FROM dbo.NhanVien n JOIN dbo.ChucVu c ON c.MaChucVu=n.MaChucVu WHERE n.MaNhanVien=@id AND n.TrangThai=1", XuLyDuLieu.ThamSo("@id", MaNhanVien));
            if (bang.Rows.Count != 1)
                throw new UnauthorizedAccessException("Tài khoản đã nghỉ việc.");
            VaiTro = PhanQuyenNguoiDung.ChuanHoa(bang.Rows[0][0].ToString());
            TenDangNhap = bang.Rows[0][1].ToString();
            if (!PhanQuyenNguoiDung.CoQuyen(VaiTro, maQuyen))
                throw new UnauthorizedAccessException("Tài khoản không có quyền thực hiện chức năng này.");
        }
    }
}
