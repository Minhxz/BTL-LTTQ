using System;
using System.Linq;
using System.Windows.Forms;
using QLyCuaHangSuaXe.LopDungChung;
using QLyCuaHangSuaXe.DanhMuc.DangNhap;

class KiemTraDangNhap
{
    static int soKiemTra;
    static void KiemTra(bool hopLe, string thongBao)
    {
        if (!hopLe)
            throw new Exception(thongBao);
        soKiemTra++;
        Console.WriteLine("PASS: " + thongBao);
    }

    [STAThread]
    static int Main()
    {
        try
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            using (var context = new DieuPhoiUngDung())
            {
                KiemTra(context.MainForm is frmDangNhap, "Application opens login before main window");
                context.MainForm.Close();
            }

            KiemTra(PhanQuyenNguoiDung.VaiTro.Length == 4, "Exactly four supported roles");
            KiemTra(PhanQuyenNguoiDung.CoQuyen("Chủ/Quản lý", "QUAN_LY_TAI_KHOAN"), "Owner can manage accounts");
            KiemTra(!PhanQuyenNguoiDung.CoQuyen("Thu ngân", "DICH_VU") && PhanQuyenNguoiDung.CoQuyen("Thu ngân", "HOA_DON"), "Cashier invoice scope");
            KiemTra(!PhanQuyenNguoiDung.CoQuyen("Thợ", "TIEP_NHAN") && PhanQuyenNguoiDung.CoQuyen("Thợ", "PHAN_CONG"), "Worker assignment scope");
            KiemTra(!PhanQuyenNguoiDung.CoQuyen("Kho", "NHAP_KHO"), "Unsupported legacy role cannot access modules");
            KiemTra(ChamCong.PhutCong(new TimeSpan(7, 30, 0), new TimeSpan(14, 0, 0), new TimeSpan(7, 0, 0), new TimeSpan(15, 0, 0), false, 0) == 390, "Shift boundaries cap counted minutes");
            KiemTra(ChamCong.PhutCong(new TimeSpan(7, 30, 0), new TimeSpan(14, 0, 0), new TimeSpan(8, 0, 0), new TimeSpan(13, 30, 0), false, -15) == 315, "Late and early minutes deducted");
            KiemTra(ChamCong.PhutCong(new TimeSpan(7, 30, 0), new TimeSpan(14, 0, 0), TimeSpan.Zero, TimeSpan.Zero, true, 0) == 0, "Absence counts zero");
            try
            {
                PhienDangNhap.YeuCau("TIEP_NHAN");
                throw new Exception("Unauthenticated access was allowed");
            }
            catch (UnauthorizedAccessException)
            {
                KiemTra(true, "Database operations require authentication");
            }

            Console.WriteLine("PASS: " + soKiemTra + " checks without database access.");
            return 0;
        }
        catch (Exception loi)
        {
            Console.Error.WriteLine(loi);
            return 1;
        }
    }
}
