using System;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace QLyCuaHangSuaXe.LopDungChung
{
    public sealed class ThongTinNhanVien
    {
        public string Ma, HoTen, DienThoai, ChucVu, TrangThai;
        public int GioiHanXe;
        public decimal LuongGio;
        public bool TaoMoi;
    }
    public static class QuanLyNhanSu
    {
        static object MaChucVu(SqlConnection ketNoi, SqlTransaction giaoDich, string chucVu)
        {
            chucVu = PhanQuyenNguoiDung.ChuanHoa(chucVu);
            if (!PhanQuyenNguoiDung.CoVaiTro(chucVu)) throw new InvalidOperationException("Chọn chức vụ hợp lệ.");
            var bang = XuLyDuLieu.DocDuLieu(ketNoi, giaoDich, "SELECT MaChucVu FROM dbo.ChucVu WHERE TenChucVu=@r", XuLyDuLieu.ThamSo("@r", chucVu));
            if (bang.Rows.Count != 1) throw new InvalidOperationException("Không tìm thấy chức vụ.");
            return bang.Rows[0][0];
        }
        static DataRow KhoaNhanVien(SqlConnection ketNoi, SqlTransaction giaoDich, string ma)
        {
            var bang = XuLyDuLieu.DocDuLieu(ketNoi, giaoDich, "SELECT n.*,c.TenChucVu FROM dbo.NhanVien n WITH(UPDLOCK,HOLDLOCK) JOIN dbo.ChucVu c ON c.MaChucVu=n.MaChucVu WHERE n.MaNhanVien=@id", XuLyDuLieu.ThamSo("@id", ma));
            if (bang.Rows.Count != 1) throw new InvalidOperationException("Nhân viên không còn tồn tại. Hãy tải lại danh sách.");
            return bang.Rows[0];
        }
        static void KiemTraCongViec(SqlConnection ketNoi, SqlTransaction giaoDich, string ma, string chucVu, string trangThai)
        {
            int soXe = Convert.ToInt32(XuLyDuLieu.DocDuLieu(ketNoi, giaoDich, @"SELECT COUNT(DISTINCT c.MaXe) FROM dbo.ChiTietPhanCong c WITH(UPDLOCK,HOLDLOCK) JOIN dbo.PhieuTiepNhan p ON p.MaXe=c.MaXe WHERE c.MaNhanVien=@id AND p.TrangThai IN(N'Chờ sửa',N'Đang sửa')", XuLyDuLieu.ThamSo("@id", ma)).Rows[0][0]);
            if (soXe > PhanQuyenNguoiDung.GioiHanXe(chucVu) || soXe > 0 && trangThai != "Đang làm")
                throw new InvalidOperationException("Nhân viên còn xe đang sửa. Hoàn tất hoặc chuyển phân công trước khi đổi chức vụ hay nghỉ việc.");
        }
        static void KiemTraDienThoai(string dienThoai)
        {
            if (string.IsNullOrEmpty(dienThoai) || !Regex.IsMatch(dienThoai, @"^0\d{9}$")) throw new InvalidOperationException("Điện thoại gồm 10 số và bắt đầu bằng 0.");
        }
        static void KiemTraTenDangNhap(string ten)
        {
            if (string.IsNullOrEmpty(ten) || ten.Length > 50 || !Regex.IsMatch(ten, @"^[A-Za-z0-9_.-]+$")) throw new InvalidOperationException("Tên đăng nhập dùng chữ không dấu, số, dấu chấm, gạch ngang hoặc gạch dưới; tối đa 50 ký tự.");
        }
        static void KiemTraTrung(SqlConnection ketNoi, SqlTransaction giaoDich, string ma, string ten, string dienThoai)
        {
            if (XuLyDuLieu.DocDuLieu(ketNoi, giaoDich, "SELECT 1 FROM dbo.NhanVien WITH(UPDLOCK,HOLDLOCK) WHERE MaNhanVien<>@id AND (TenDangNhap=@u OR SoDienThoai=@p)", XuLyDuLieu.ThamSo("@id", ma), XuLyDuLieu.ThamSo("@u", ten), XuLyDuLieu.ThamSo("@p", dienThoai)).Rows.Count > 0)
                throw new InvalidOperationException("Tên đăng nhập hoặc số điện thoại đã được sử dụng.");
        }
        public static string LuuNhanVien(ThongTinNhanVien nhanVien)
        {
            PhienDangNhap.YeuCau("NHAN_VIEN");
            if (nhanVien == null || string.IsNullOrWhiteSpace(nhanVien.HoTen) || nhanVien.HoTen.Trim().Length > 100) throw new InvalidOperationException("Nhập họ tên nhân viên, tối đa 100 ký tự.");
            KiemTraDienThoai(nhanVien.DienThoai);

            if (nhanVien.LuongGio <= 0 || nhanVien.LuongGio > 1000000000) throw new InvalidOperationException("Lương tháng phải lớn hơn 0 và không vượt 1 tỷ đồng.");
            string chucVu = PhanQuyenNguoiDung.ChuanHoa(nhanVien.ChucVu);
            bool them = nhanVien.TaoMoi || string.IsNullOrEmpty(nhanVien.Ma);
            if (nhanVien.Ma == PhienDangNhap.MaNhanVien && chucVu != PhienDangNhap.VaiTro) throw new InvalidOperationException("Không được tự đổi chức vụ tài khoản đang đăng nhập.");
            return new XuLyDuLieu().TrongGiaoDich<string>((ketNoi, giaoDich) =>
            {
                string ma = them ? (string.IsNullOrEmpty(nhanVien.Ma) ? XuLyDuLieu.TaoMaMoi(ketNoi,giaoDich,"NV") : XuLyDuLieu.KiemTraMa(nhanVien.Ma, "NV")) : nhanVien.Ma;
                DataRow cu = them ? null : KhoaNhanVien(ketNoi, giaoDich, ma);
                string trangThai = them ? "Đang làm" : cu["TrangThai"].ToString();
                string tenDangNhap = them ? ma.ToLowerInvariant() : cu["TenDangNhap"].ToString();
                KiemTraTrung(ketNoi, giaoDich, ma, tenDangNhap, nhanVien.DienThoai);
                if (!them) KiemTraCongViec(ketNoi, giaoDich, ma, chucVu, trangThai);
                XuLyDuLieu.ThucThi(ketNoi, giaoDich, them
                    ? "INSERT dbo.NhanVien(MaNhanVien,MaChucVu,HoTen,SoDienThoai,Luong,TenDangNhap,MatKhau,TrangThai) VALUES(@id,@r,@n,@p,@l,@u,@h,@s)"
                    : "UPDATE dbo.NhanVien SET MaChucVu=@r,HoTen=@n,SoDienThoai=@p,Luong=@l,TrangThai=@s WHERE MaNhanVien=@id",
                    XuLyDuLieu.ThamSo("@id", ma), XuLyDuLieu.ThamSo("@r", MaChucVu(ketNoi,giaoDich,chucVu)), XuLyDuLieu.ThamSo("@n", nhanVien.HoTen.Trim()), XuLyDuLieu.ThamSo("@p", nhanVien.DienThoai), XuLyDuLieu.ThamSo("@l",nhanVien.LuongGio), XuLyDuLieu.ThamSo("@u",tenDangNhap), XuLyDuLieu.ThamSo("@h",BaoMat.BamMatKhau("123")), XuLyDuLieu.ThamSo("@s",trangThai));
                return ma;
            });
        }
        public static string TaoTaiKhoanMoi(string maNhanVien, string hoTen, string tenDangNhap, string matKhau, string chucVu, bool hoatDong, string maDuKien = null, string dienThoai = null, decimal luong = 6000000)
        {
            PhienDangNhap.YeuCau("QUAN_LY_TAI_KHOAN");
            KiemTraTenDangNhap(tenDangNhap); KiemTraDienThoai(dienThoai);
            if (!string.IsNullOrEmpty(maNhanVien)) throw new InvalidOperationException("Nhân viên đã có tài khoản; chọn dòng để chỉnh sửa.");
            if (string.IsNullOrWhiteSpace(hoTen) || hoTen.Trim().Length > 100) throw new InvalidOperationException("Nhập họ tên nhân viên, tối đa 100 ký tự.");
            if (luong <= 0 || luong > 1000000000) throw new InvalidOperationException("Lương tháng phải lớn hơn 0 và không vượt 1 tỷ đồng.");
            return new XuLyDuLieu().TrongGiaoDich<string>((ketNoi, giaoDich) =>
            {
                string ma = string.IsNullOrEmpty(maDuKien) ? XuLyDuLieu.TaoMaMoi(ketNoi,giaoDich,"NV") : XuLyDuLieu.KiemTraMa(maDuKien,"NV");
                KiemTraTrung(ketNoi, giaoDich, ma, tenDangNhap, dienThoai);
                XuLyDuLieu.ThucThi(ketNoi, giaoDich, "INSERT dbo.NhanVien(MaNhanVien,MaChucVu,HoTen,SoDienThoai,Luong,TenDangNhap,MatKhau,TrangThai) VALUES(@id,@r,@n,@p,@l,@u,@h,@s)", XuLyDuLieu.ThamSo("@id",ma), XuLyDuLieu.ThamSo("@r",MaChucVu(ketNoi,giaoDich,chucVu)), XuLyDuLieu.ThamSo("@n",hoTen.Trim()), XuLyDuLieu.ThamSo("@p",dienThoai), XuLyDuLieu.ThamSo("@l",luong), XuLyDuLieu.ThamSo("@u",tenDangNhap), XuLyDuLieu.ThamSo("@h",BaoMat.BamMatKhau(matKhau)), XuLyDuLieu.ThamSo("@s",hoatDong?"Đang làm":"Nghỉ việc"));
                return ma;
            });
        }
        public static void LuuTaiKhoan(string ma, string tenDangNhap, string matKhau, string chucVu, bool hoatDong)
        {
            PhienDangNhap.YeuCau("QUAN_LY_TAI_KHOAN");
            KiemTraTenDangNhap(tenDangNhap);
            chucVu = PhanQuyenNguoiDung.ChuanHoa(chucVu);
            if (string.IsNullOrEmpty(ma)) throw new InvalidOperationException("Chọn nhân viên cần sửa tài khoản.");
            if (!string.IsNullOrEmpty(matKhau)) throw new InvalidOperationException("Đổi mật khẩu tại trang Đổi mật khẩu của tôi.");
            if (ma == PhienDangNhap.MaNhanVien && (!hoatDong || chucVu != PhienDangNhap.VaiTro)) throw new InvalidOperationException("Không được tự khóa hoặc đổi quyền tài khoản đang đăng nhập.");
            new XuLyDuLieu().TrongGiaoDich<int>((ketNoi, giaoDich) =>
            {
                var cu = KhoaNhanVien(ketNoi,giaoDich,ma);
                KiemTraTrung(ketNoi,giaoDich,ma,tenDangNhap,cu["SoDienThoai"].ToString());
                KiemTraCongViec(ketNoi,giaoDich,ma,chucVu,hoatDong?"Đang làm":"Nghỉ việc");
                return XuLyDuLieu.ThucThi(ketNoi,giaoDich,"UPDATE dbo.NhanVien SET MaChucVu=@r,TenDangNhap=@u,TrangThai=@s WHERE MaNhanVien=@id",XuLyDuLieu.ThamSo("@r",MaChucVu(ketNoi,giaoDich,chucVu)),XuLyDuLieu.ThamSo("@u",tenDangNhap),XuLyDuLieu.ThamSo("@s",hoatDong?"Đang làm":"Nghỉ việc"),XuLyDuLieu.ThamSo("@id",ma));
            });
        }
        public static void LuuThongTinTaiKhoan(string ma, string tenDangNhap, string chucVu)
        {
            PhienDangNhap.YeuCau("QUAN_LY_TAI_KHOAN");
            KiemTraTenDangNhap(tenDangNhap);
            chucVu = PhanQuyenNguoiDung.ChuanHoa(chucVu);
            if (string.IsNullOrEmpty(ma)) throw new InvalidOperationException("Chọn nhân viên cần sửa tài khoản.");
            if (ma == PhienDangNhap.MaNhanVien && chucVu != PhienDangNhap.VaiTro)
                throw new InvalidOperationException("Không được tự đổi quyền tài khoản đang đăng nhập.");
            new XuLyDuLieu().TrongGiaoDich<int>((ketNoi, giaoDich) =>
            {
                var cu = KhoaNhanVien(ketNoi, giaoDich, ma);
                KiemTraTrung(ketNoi, giaoDich, ma, tenDangNhap, cu["SoDienThoai"].ToString());
                KiemTraCongViec(ketNoi, giaoDich, ma, chucVu, cu["TrangThai"].ToString());
                return XuLyDuLieu.ThucThi(ketNoi, giaoDich,
                    "UPDATE dbo.NhanVien SET MaChucVu=@r,TenDangNhap=@u WHERE MaNhanVien=@id",
                    XuLyDuLieu.ThamSo("@r", MaChucVu(ketNoi, giaoDich, chucVu)),
                    XuLyDuLieu.ThamSo("@u", tenDangNhap), XuLyDuLieu.ThamSo("@id", ma));
            });
        }
        public static void NghiViec(string ma)
        {
            PhienDangNhap.YeuCau("QUAN_LY_TAI_KHOAN");
            ChuyenNghiViec(ma);
        }
        public static void NghiViecNhanVien(string ma)
        {
            PhienDangNhap.YeuCau("NHAN_VIEN");
            ChuyenNghiViec(ma);
        }
        static void ChuyenNghiViec(string ma)
        {
            if (string.IsNullOrEmpty(ma) || ma == PhienDangNhap.MaNhanVien) throw new InvalidOperationException("Chọn nhân viên khác tài khoản đang đăng nhập.");
            new XuLyDuLieu().TrongGiaoDich<int>((ketNoi,giaoDich) =>
            {
                var cu = KhoaNhanVien(ketNoi,giaoDich,ma);
                KiemTraCongViec(ketNoi,giaoDich,ma,cu["TenChucVu"].ToString(),"Nghỉ việc");
                return XuLyDuLieu.ThucThi(ketNoi,giaoDich,"UPDATE dbo.NhanVien SET TrangThai=N'Nghỉ việc' WHERE MaNhanVien=@id",XuLyDuLieu.ThamSo("@id",ma));
            });
        }
    }
}
