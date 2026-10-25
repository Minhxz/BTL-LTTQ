using System;
using System.Windows.Forms;

namespace DanhMuc
{
    internal static class ChuongTrinh
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new QLyCuaHangSuaXe.LopDungChung.DieuPhoiUngDung());
        }
    }
}
