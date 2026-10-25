using System;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using System.Text;

namespace QLyCuaHangSuaXe.LopDungChung
{
    public static class BaoMat
    {
        [DllImport("bcrypt.dll", CharSet = CharSet.Unicode)]
        static extern int BCryptOpenAlgorithmProvider(out IntPtr thuatToan, string ten, string nhaCungCap, uint co);
        [DllImport("bcrypt.dll")]
        static extern int BCryptDeriveKeyPBKDF2(IntPtr thuatToan, byte[] matKhau, uint doDaiMatKhau, byte[] muoi, uint doDaiMuoi, ulong soVong, [Out] byte[] ketQua, uint doDaiKetQua, uint co);
        [DllImport("bcrypt.dll")]
        static extern int BCryptCloseAlgorithmProvider(IntPtr thuatToan, uint co);

        static byte[] SuyDanKhoa(string matKhau, byte[] muoi, int soVong)
        {
            IntPtr thuatToan = IntPtr.Zero;
            byte[] duLieuMatKhau = Encoding.UTF8.GetBytes(matKhau);
            try
            {
                if (BCryptOpenAlgorithmProvider(out thuatToan, "SHA256", null, 8) == 0)
                {
                    byte[] ketQua = new byte[32];
                    if (BCryptDeriveKeyPBKDF2(thuatToan, duLieuMatKhau, (uint)duLieuMatKhau.Length, muoi, (uint)muoi.Length, (ulong)soVong, ketQua, (uint)ketQua.Length, 0) == 0)
                        return ketQua;
                }
            }
            catch (DllNotFoundException) { }
            catch (EntryPointNotFoundException) { }
            finally
            {
                Array.Clear(duLieuMatKhau, 0, duLieuMatKhau.Length);
                if (thuatToan != IntPtr.Zero) BCryptCloseAlgorithmProvider(thuatToan, 0);
            }
            using (var boBam = new Rfc2898DeriveBytes(matKhau, muoi, soVong, HashAlgorithmName.SHA256))
                return boBam.GetBytes(32);
        }

        public static string BamMatKhau(string matKhau)
        {
            if (string.IsNullOrWhiteSpace(matKhau))
                throw new InvalidOperationException("Mật khẩu không được để trống.");
            byte[] muoi = new byte[16];
            using (var boNgauNhien = RandomNumberGenerator.Create())
                boNgauNhien.GetBytes(muoi);
            return "PBKDF2-SHA256$600000$" + Convert.ToBase64String(muoi) + "$" + Convert.ToBase64String(SuyDanKhoa(matKhau, muoi, 600000));
        }

        public static bool KiemTraMatKhau(string matKhau, string chuoiBam)
        {
            try
            {
                var cacPhan = chuoiBam.Split('$');
                int soVong;
                if (cacPhan.Length != 4 || cacPhan[0] != "PBKDF2-SHA256" || !int.TryParse(cacPhan[1], out soVong) || soVong < 10000 || soVong > 2000000)
                    return false;
                var bamMongDoi = Convert.FromBase64String(cacPhan[3]);
                if (bamMongDoi.Length != 32)
                    return false;
                {
                    byte[] bamThucTe = SuyDanKhoa(matKhau, Convert.FromBase64String(cacPhan[2]), soVong);
                    int saiKhac = 0;
                    for (int chiSo = 0; chiSo < bamThucTe.Length; chiSo++)
                        saiKhac |= bamThucTe[chiSo] ^ bamMongDoi[chiSo];
                    return saiKhac == 0;
                }
            }
            catch (FormatException)
            {
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }
        }
    }

    public static class PhienDangNhap
    {
        public static string MaNhanVien { get; private set; }
        public static string TenDangNhap { get; private set; }
        public static string VaiTro { get; private set; }

        public static bool DangNhap(string tenDangNhap, string matKhau)
        {
            var cacDong = new XuLyDuLieu().DocBang(@"SELECT n.MaNhanVien,n.TenDangNhap,n.MatKhauHash,c.TenChucVu FROM dbo.NhanVien n JOIN dbo.ChucVu c ON c.MaChucVu=n.MaChucVu WHERE n.TenDangNhap=@u AND n.TrangThai=N'Đang làm'", XuLyDuLieu.ThamSo("@u", tenDangNhap));
            if (cacDong.Rows.Count != 1 || !BaoMat.KiemTraMatKhau(matKhau, cacDong.Rows[0]["MatKhauHash"].ToString()))
                return false;
            string vaiTro = PhanQuyenNguoiDung.ChuanHoa(cacDong.Rows[0]["TenChucVu"].ToString());
            if (!PhanQuyenNguoiDung.CoVaiTro(vaiTro))
                return false;
            MaNhanVien = cacDong.Rows[0]["MaNhanVien"].ToString();
            TenDangNhap = tenDangNhap;
            VaiTro = vaiTro;
            return true;
        }

        public static void DoiMatKhau(string matKhauCu, string matKhauMoi)
        {
            if (string.IsNullOrEmpty(MaNhanVien))
                throw new InvalidOperationException("Cần đăng nhập.");
            new XuLyDuLieu().TrongGiaoDich<int>((ketNoi, giaoDich) =>
            {
                var dong = XuLyDuLieu.DocDuLieu(ketNoi, giaoDich, "SELECT MatKhauHash FROM dbo.NhanVien WITH(UPDLOCK) WHERE MaNhanVien=@id AND TrangThai=N'Đang làm'", XuLyDuLieu.ThamSo("@id", MaNhanVien));
                if (dong.Rows.Count != 1 || !BaoMat.KiemTraMatKhau(matKhauCu, dong.Rows[0][0].ToString()))
                    throw new InvalidOperationException("Mật khẩu hiện tại không đúng.");
                return XuLyDuLieu.ThucThi(ketNoi, giaoDich, "UPDATE dbo.NhanVien SET MatKhauHash=@h WHERE MaNhanVien=@id", XuLyDuLieu.ThamSo("@h", BaoMat.BamMatKhau(matKhauMoi)), XuLyDuLieu.ThamSo("@id", MaNhanVien));
            });
        }

        public static void YeuCau(string maQuyen)
        {
            if (string.IsNullOrEmpty(MaNhanVien))
                throw new UnauthorizedAccessException("Cần đăng nhập trước khi sử dụng chức năng.");
            var hienTai = new XuLyDuLieu().DocBang("SELECT v.TenChucVu FROM dbo.NhanVien n JOIN dbo.ChucVu v ON v.MaChucVu=n.MaChucVu WHERE n.MaNhanVien=@id AND n.TrangThai=N'Đang làm'", XuLyDuLieu.ThamSo("@id", MaNhanVien));
            if (hienTai.Rows.Count != 1)
                throw new UnauthorizedAccessException("Tài khoản đã bị khóa hoặc nghỉ việc.");
            VaiTro = PhanQuyenNguoiDung.ChuanHoa(hienTai.Rows[0][0].ToString());
            if (!PhanQuyenNguoiDung.CoQuyen(VaiTro, maQuyen))
                throw new UnauthorizedAccessException("Tài khoản không có quyền thực hiện chức năng này.");
        }
    }
}
