using System;
using System.Diagnostics;
using QLyCuaHangSuaXe.LopDungChung;
class DoThoiGianDangNhap
{
    static int Main()
    {
        try
        {
            string ten=Environment.GetEnvironmentVariable("SUA_XE_TAI_KHOAN")??"minh",matKhau=Environment.GetEnvironmentVariable("SUA_XE_MAT_KHAU")??"123";
            var dongHo=Stopwatch.StartNew();
            if(!PhienDangNhap.DangNhap(ten,matKhau))throw new Exception("Đăng nhập lần đầu không thành công.");
            Console.WriteLine("DangNhapDauTienMs="+dongHo.ElapsedMilliseconds);
            dongHo.Restart();var dong=new XuLyDuLieu().DocBang("SELECT MatKhau FROM dbo.NhanVien WHERE TenDangNhap=@ten",XuLyDuLieu.ThamSo("@ten",ten));Console.WriteLine("TruyVanTaiKhoanMs="+dongHo.ElapsedMilliseconds);
            if(dong.Rows.Count!=1)throw new Exception("Không tìm được tài khoản kiểm tra.");
            dongHo.Restart();if(!BaoMat.KiemTraMatKhau(matKhau,dong.Rows[0][0].ToString()))throw new Exception("Mật khẩu kiểm tra không đúng.");Console.WriteLine("XacThucMatKhauMs="+dongHo.ElapsedMilliseconds);
            long tong=0;
            for(int i=0;i<5;i++){dongHo.Restart();if(!PhienDangNhap.DangNhap(ten,matKhau))throw new Exception("Đăng nhập lặp không thành công.");tong+=dongHo.ElapsedMilliseconds;}
            Console.WriteLine("DangNhapTrungBinh5LanMs="+(tong/5m).ToString("N1"));return 0;
        }
        catch(Exception loi){Console.Error.WriteLine(loi.Message);return 1;}
    }
}
