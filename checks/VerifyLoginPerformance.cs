using System;
using System.Diagnostics;
using QLyCuaHangSuaXe.LopDungChung;

class DoThoiGianDangNhap
{
    static int Main()
    {
        try
        {
            string ten = Environment.GetEnvironmentVariable("SUA_XE_TAI_KHOAN");
            string matKhau = Environment.GetEnvironmentVariable("SUA_XE_MAT_KHAU");
            if (string.IsNullOrEmpty(ten) || matKhau == null) throw new Exception("Cần truyền thông tin đăng nhập qua biến môi trường.");
            var dongHo = Stopwatch.StartNew();
            using (var ketNoi = XuLyDuLieu.MoKetNoi()) { }
            Console.WriteLine("KetNoiDauTienMs=" + dongHo.ElapsedMilliseconds);
            dongHo.Restart();
            var dong = new XuLyDuLieu().DocBang("SELECT MatKhauHash FROM dbo.NhanVien WHERE TenDangNhap=@ten", XuLyDuLieu.ThamSo("@ten", ten));
            Console.WriteLine("TruyVanTaiKhoanMs=" + dongHo.ElapsedMilliseconds);
            if (dong.Rows.Count != 1) throw new Exception("Không tìm được tài khoản kiểm tra.");
            dongHo.Restart();
            if (!BaoMat.KiemTraMatKhau(matKhau, dong.Rows[0][0].ToString())) throw new Exception("Mật khẩu kiểm tra không đúng.");
            Console.WriteLine("XacThucMatKhauMs=" + dongHo.ElapsedMilliseconds);
            dongHo.Restart();
            if (!PhienDangNhap.DangNhap(ten, matKhau)) throw new Exception("Đăng nhập không thành công.");
            Console.WriteLine("DangNhapDaCoKetNoiMs=" + dongHo.ElapsedMilliseconds);
            return 0;
        }
        catch (Exception loi) { Console.Error.WriteLine(loi.Message); return 1; }
    }
}
