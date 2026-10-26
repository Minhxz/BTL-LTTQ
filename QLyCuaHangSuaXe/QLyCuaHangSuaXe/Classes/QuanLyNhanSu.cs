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
    }

    public static class QuanLyNhanSu
    {
        static object MaChucVu(SqlConnection ketNoi, SqlTransaction giaoDich, string chucVu)
        {
            if (!PhanQuyenNguoiDung.CoVaiTro(chucVu))
                throw new InvalidOperationException("Chọn một trong bốn chức vụ hiện có.");
            var bang = XuLyDuLieu.DocDuLieu(ketNoi, giaoDich, "SELECT MaChucVu FROM dbo.ChucVu WHERE TenChucVu=@r", XuLyDuLieu.ThamSo("@r", chucVu));
            if (bang.Rows.Count != 1) throw new InvalidOperationException("Chưa có chức vụ trong cơ sở dữ liệu.");
            return bang.Rows[0][0];
        }

        static DataRow KhoaNhanVien(SqlConnection ketNoi, SqlTransaction giaoDich, string ma)
        {
            var bang = XuLyDuLieu.DocDuLieu(ketNoi, giaoDich, "SELECT n.*,v.TenChucVu FROM dbo.NhanVien n WITH(UPDLOCK,HOLDLOCK) JOIN dbo.ChucVu v ON v.MaChucVu=n.MaChucVu WHERE n.MaNhanVien=@id", XuLyDuLieu.ThamSo("@id", ma));
            if (bang.Rows.Count != 1) throw new InvalidOperationException("Nhân viên không còn tồn tại. Hãy tải lại danh sách.");
            return bang.Rows[0];
        }

        static void KiemTraCongViec(SqlConnection ketNoi, SqlTransaction giaoDich, string ma, string chucVu, string trangThai, int gioiHan)
        {
            var bang = XuLyDuLieu.DocDuLieu(ketNoi, giaoDich, "SELECT COUNT(*) FROM dbo.PhanCongTho WITH(UPDLOCK,HOLDLOCK) WHERE MaNhanVien=@id AND TrangThaiCongViec NOT IN(N'Hoàn thành',N'Đã hủy')", XuLyDuLieu.ThamSo("@id", ma));
            int soXe = Convert.ToInt32(bang.Rows[0][0]);
            if (soXe > gioiHan || soXe > 0 && (trangThai != "Đang làm" || chucVu != "Thợ" && chucVu != "Thợ chính"))
                throw new InvalidOperationException("Nhân viên còn xe đang sửa. Cần hoàn tất hoặc chuyển phân công trước khi thay đổi chức vụ, nghỉ việc hay giảm giới hạn xe.");
        }

        public static string LuuNhanVien(ThongTinNhanVien nhanVien)
        {
            PhienDangNhap.YeuCau("NHAN_VIEN");
            if (nhanVien == null || string.IsNullOrWhiteSpace(nhanVien.HoTen) || nhanVien.HoTen.Trim().Length > 100)
                throw new InvalidOperationException("Nhập họ tên nhân viên, tối đa 100 ký tự.");
            if (!string.IsNullOrEmpty(nhanVien.DienThoai) && !Regex.IsMatch(nhanVien.DienThoai, @"^0\d{9}$"))
                throw new InvalidOperationException("Điện thoại gồm 10 số và bắt đầu bằng 0.");
            if (nhanVien.TrangThai != "Đang làm" && nhanVien.TrangThai != "Đã nghỉ")
                throw new InvalidOperationException("Chọn trạng thái làm việc.");
            if (nhanVien.GioiHanXe < 0 || nhanVien.GioiHanXe > 20 || nhanVien.LuongGio < 0 || nhanVien.LuongGio > 10000000)
                throw new InvalidOperationException("Giới hạn xe hoặc lương giờ không hợp lệ.");
            bool them = string.IsNullOrEmpty(nhanVien.Ma);
            string ma = them ? XuLyDuLieu.TaoMaMoi("NV") : nhanVien.Ma;
            if (ma == PhienDangNhap.MaNhanVien && (nhanVien.ChucVu != PhienDangNhap.VaiTro || nhanVien.TrangThai != "Đang làm"))
                throw new InvalidOperationException("Không được tự đổi chức vụ hoặc cho tài khoản đang đăng nhập nghỉ việc.");
            string bamChoCap = them ? BaoMat.BamMatKhau(Guid.NewGuid().ToString("N")) : null;
            new XuLyDuLieu().TrongGiaoDich<int>((ketNoi, giaoDich) =>
            {
                if (!them) KhoaNhanVien(ketNoi, giaoDich, ma);
                object vaiTro = MaChucVu(ketNoi, giaoDich, nhanVien.ChucVu);
                if (!them) KiemTraCongViec(ketNoi, giaoDich, ma, nhanVien.ChucVu, nhanVien.TrangThai, nhanVien.GioiHanXe);
                return XuLyDuLieu.ThucThi(ketNoi, giaoDich,
                    them ? "INSERT dbo.NhanVien(MaNhanVien,MaChucVu,TenNhanVien,TenDangNhap,MatKhauHash,SoDienThoai,TrangThai,SoXeDangSuaToiDa,LuongGio,DaCapTaiKhoan,TaiKhoanHoatDong) VALUES(@id,@r,@n,@u,@h,@p,@s,@cap,@hour,0,0)"
                    : "UPDATE dbo.NhanVien SET MaChucVu=@r,TenNhanVien=@n,SoDienThoai=@p,TrangThai=@s,SoXeDangSuaToiDa=@cap,LuongGio=@hour WHERE MaNhanVien=@id",
                    XuLyDuLieu.ThamSo("@id", ma), XuLyDuLieu.ThamSo("@r", vaiTro), XuLyDuLieu.ThamSo("@n", nhanVien.HoTen.Trim()),
                    XuLyDuLieu.ThamSo("@u", "__cho_cap_" + ma), XuLyDuLieu.ThamSo("@h", bamChoCap), XuLyDuLieu.ThamSo("@p", nhanVien.DienThoai ?? ""),
                    XuLyDuLieu.ThamSo("@s", nhanVien.TrangThai), XuLyDuLieu.ThamSo("@cap", nhanVien.GioiHanXe), XuLyDuLieu.ThamSo("@hour", nhanVien.LuongGio));
            });
            return ma;
        }

        public static string TaoTaiKhoanMoi(string maNhanVien, string hoTen, string tenDangNhap, string matKhau, string chucVu, bool hoatDong)
        {
            PhienDangNhap.YeuCau("QUAN_LY_TAI_KHOAN");
            if (string.IsNullOrEmpty(tenDangNhap) || tenDangNhap.Length > 50 || !Regex.IsMatch(tenDangNhap, @"^[A-Za-z0-9_.-]+$"))
                throw new InvalidOperationException("Tên đăng nhập dùng chữ không dấu, số, dấu chấm, gạch ngang hoặc gạch dưới; tối đa 50 ký tự.");
            if (string.IsNullOrWhiteSpace(matKhau))
                throw new InvalidOperationException("Tài khoản mới cần mật khẩu.");
            if (!PhanQuyenNguoiDung.CoVaiTro(chucVu))
                throw new InvalidOperationException("Chọn một trong bốn chức vụ hiện có.");
            bool taoNhanVien = string.IsNullOrEmpty(maNhanVien);
            if (taoNhanVien && (string.IsNullOrWhiteSpace(hoTen) || hoTen.Trim().Length > 100))
                throw new InvalidOperationException("Nhập họ tên nhân viên, tối đa 100 ký tự.");
            string ma = taoNhanVien ? XuLyDuLieu.TaoMaMoi("NV") : maNhanVien;
            string bam = BaoMat.BamMatKhau(matKhau);
            return new XuLyDuLieu().TrongGiaoDich<string>((ketNoi, giaoDich) =>
            {
                DataRow nhanVien = taoNhanVien ? null : KhoaNhanVien(ketNoi, giaoDich, ma);
                if (nhanVien != null && Convert.ToBoolean(nhanVien["DaCapTaiKhoan"]))
                    throw new InvalidOperationException("Nhân viên đã có tài khoản. Chọn tài khoản đó để chỉnh sửa hoặc đổi mật khẩu.");
                if (nhanVien != null && hoatDong && nhanVien["TrangThai"].ToString() != "Đang làm")
                    throw new InvalidOperationException("Nhân viên đã nghỉ việc, không thể bật tài khoản.");
                var trungTen = XuLyDuLieu.DocDuLieu(ketNoi, giaoDich, "SELECT MaNhanVien FROM dbo.NhanVien WITH(UPDLOCK,HOLDLOCK) WHERE TenDangNhap=@u AND MaNhanVien<>@id", XuLyDuLieu.ThamSo("@u", tenDangNhap), XuLyDuLieu.ThamSo("@id", ma));
                if (trungTen.Rows.Count > 0)
                    throw new InvalidOperationException("Tên đăng nhập đã được sử dụng.");
                object maVaiTro = MaChucVu(ketNoi, giaoDich, chucVu);
                int gioiHan = nhanVien != null && nhanVien["TenChucVu"].ToString() == chucVu
                    ? Convert.ToInt32(nhanVien["SoXeDangSuaToiDa"])
                    : chucVu == "Thợ chính" ? 3 : chucVu == "Thợ" ? 2 : 0;
                if (nhanVien != null)
                    KiemTraCongViec(ketNoi, giaoDich, ma, chucVu, nhanVien["TrangThai"].ToString(), gioiHan);
                int soDong = XuLyDuLieu.ThucThi(ketNoi, giaoDich,
                    taoNhanVien
                        ? "INSERT dbo.NhanVien(MaNhanVien,MaChucVu,TenNhanVien,TenDangNhap,MatKhauHash,SoDienThoai,TrangThai,SoXeDangSuaToiDa,LuongGio,DaCapTaiKhoan,TaiKhoanHoatDong) VALUES(@id,@r,@n,@u,@h,'',N'Đang làm',@cap,0,1,@active)"
                        : "UPDATE dbo.NhanVien SET MaChucVu=@r,TenDangNhap=@u,MatKhauHash=@h,DaCapTaiKhoan=1,TaiKhoanHoatDong=@active,SoXeDangSuaToiDa=@cap WHERE MaNhanVien=@id AND DaCapTaiKhoan=0",
                    XuLyDuLieu.ThamSo("@id", ma), XuLyDuLieu.ThamSo("@r", maVaiTro), XuLyDuLieu.ThamSo("@n", taoNhanVien ? hoTen.Trim() : ""),
                    XuLyDuLieu.ThamSo("@u", tenDangNhap), XuLyDuLieu.ThamSo("@h", bam), XuLyDuLieu.ThamSo("@cap", gioiHan), XuLyDuLieu.ThamSo("@active", hoatDong));
                if (soDong != 1)
                    throw new InvalidOperationException("Chưa tạo được tài khoản. Hãy tải lại danh sách nhân viên.");
                return ma;
            });
        }

        public static void LuuTaiKhoan(string ma, string tenDangNhap, string matKhau, string chucVu, bool hoatDong)
        {
            PhienDangNhap.YeuCau("QUAN_LY_TAI_KHOAN");
            if (string.IsNullOrEmpty(ma)) throw new InvalidOperationException("Chọn nhân viên cần cấp hoặc sửa tài khoản.");
            if (string.IsNullOrEmpty(tenDangNhap) || tenDangNhap.Length > 50 || !Regex.IsMatch(tenDangNhap, @"^[A-Za-z0-9_.-]+$"))
                throw new InvalidOperationException("Tên đăng nhập dùng chữ không dấu, số, dấu chấm, gạch ngang hoặc gạch dưới; tối đa 50 ký tự.");
            if (ma == PhienDangNhap.MaNhanVien && (!hoatDong || chucVu != PhienDangNhap.VaiTro))
                throw new InvalidOperationException("Không được tự khóa hoặc đổi quyền tài khoản đang đăng nhập.");
            string bam = string.IsNullOrEmpty(matKhau) ? null : BaoMat.BamMatKhau(matKhau);
            new XuLyDuLieu().TrongGiaoDich<int>((ketNoi, giaoDich) =>
            {
                var nhanVien = KhoaNhanVien(ketNoi, giaoDich, ma);
                if (!Convert.ToBoolean(nhanVien["DaCapTaiKhoan"]) && bam == null)
                    throw new InvalidOperationException("Tài khoản mới cần mật khẩu.");
                if (hoatDong && nhanVien["TrangThai"].ToString() != "Đang làm")
                    throw new InvalidOperationException("Nhân viên đã nghỉ việc, không thể bật tài khoản.");
                var trungTen = XuLyDuLieu.DocDuLieu(ketNoi, giaoDich, "SELECT MaNhanVien FROM dbo.NhanVien WITH(UPDLOCK,HOLDLOCK) WHERE TenDangNhap=@u AND MaNhanVien<>@id", XuLyDuLieu.ThamSo("@u", tenDangNhap), XuLyDuLieu.ThamSo("@id", ma));
                if (trungTen.Rows.Count > 0) throw new InvalidOperationException("Tên đăng nhập đã được sử dụng.");
                int gioiHan = nhanVien["TenChucVu"].ToString() == chucVu ? Convert.ToInt32(nhanVien["SoXeDangSuaToiDa"]) : chucVu == "Thợ chính" ? 3 : chucVu == "Thợ" ? 2 : 0;
                KiemTraCongViec(ketNoi, giaoDich, ma, chucVu, nhanVien["TrangThai"].ToString(), gioiHan);
                return XuLyDuLieu.ThucThi(ketNoi, giaoDich, "UPDATE dbo.NhanVien SET MaChucVu=@r,TenDangNhap=@u,MatKhauHash=COALESCE(@h,MatKhauHash),DaCapTaiKhoan=1,TaiKhoanHoatDong=@active,SoXeDangSuaToiDa=@cap WHERE MaNhanVien=@id",
                    XuLyDuLieu.ThamSo("@id", ma), XuLyDuLieu.ThamSo("@r", MaChucVu(ketNoi, giaoDich, chucVu)), XuLyDuLieu.ThamSo("@u", tenDangNhap),
                    XuLyDuLieu.ThamSo("@h", bam), XuLyDuLieu.ThamSo("@active", hoatDong), XuLyDuLieu.ThamSo("@cap", gioiHan));
            });
        }
    }
}
