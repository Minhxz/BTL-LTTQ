using QLyCuaHangSuaXe.DanhMuc.DangNhap;
using System;
using System.Windows.Forms;

namespace QLyCuaHangSuaXe.LopDungChung
{
    public sealed class DieuPhoiUngDung : ApplicationContext
    {
        public DieuPhoiUngDung()
        {
            MoDangNhap();
        }

        void MoDangNhap()
        {
            PhienDangNhap.DangXuat();
            frmDangNhap dangNhap = new frmDangNhap();
            dangNhap.DangNhapThanhCong += DangNhapThanhCong;
            MainForm = dangNhap;
        }

        private void DangNhapThanhCong(object doiTuongGui, EventArgs suKien)
        {
            frmDangNhap dangNhap = (frmDangNhap)doiTuongGui;
            frmChinh danhMuc = new frmChinh(dangNhap.TenDangNhap, dangNhap.VaiTro);
            danhMuc.DangXuatDuocYeuCau += (nguoiGui, suKienDangXuat) => DangXuat();
            MainForm = danhMuc;
            danhMuc.Show();
            dangNhap.Close();
        }

        public void DangXuat()
        {
            var trangCu = MainForm as frmChinh;
            if (trangCu == null)
                return;
            MoDangNhap();
            MainForm.Show();
            trangCu.DongSauDangXuat();
        }
    }
}
