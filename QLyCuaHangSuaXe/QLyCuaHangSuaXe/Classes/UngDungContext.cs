using QLyCuaHangSuaXe.DanhMuc.DangNhap;
using System;
using System.Windows.Forms;

namespace QLyCuaHangSuaXe.LopDungChung
{
    public sealed class DieuPhoiUngDung : ApplicationContext
    {
        public DieuPhoiUngDung()
        {
            frmDangNhap dangNhap = new frmDangNhap();
            dangNhap.DangNhapThanhCong += DangNhapThanhCong;
            MainForm = dangNhap;
        }

        private void DangNhapThanhCong(object doiTuongGui, EventArgs suKien)
        {
            frmDangNhap dangNhap = (frmDangNhap)doiTuongGui;
            frmChinh danhMuc = new frmChinh(dangNhap.TenDangNhap, dangNhap.VaiTro);
            MainForm = danhMuc;
            danhMuc.Show();
            dangNhap.Close();
        }
    }
}
