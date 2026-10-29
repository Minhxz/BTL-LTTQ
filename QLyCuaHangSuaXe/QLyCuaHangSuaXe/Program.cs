using System;
using System.Windows.Forms;


namespace QLyCuaHangSuaXe
{
    internal static class ChuongTrinh
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // CHẠY RIÊNG FORM THỐNG KÊ
            Application.Run(new QLyCuaHangSuaXe.DanhMuc.ThongKe.frmThongKe());

            // Code gốc của nhóm, tạm để comment:
            // using (LopDungChung.DieuPhoiUngDung ungDung = new LopDungChung.DieuPhoiUngDung())
            //     Application.Run(ungDung);
        }
    }
}
