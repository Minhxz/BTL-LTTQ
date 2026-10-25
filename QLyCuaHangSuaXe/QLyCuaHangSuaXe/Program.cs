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
            using (LopDungChung.DieuPhoiUngDung ungDung = new LopDungChung.DieuPhoiUngDung())
                Application.Run(ungDung);
        }
    }
}
