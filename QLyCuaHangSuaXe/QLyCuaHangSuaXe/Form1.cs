using System;
using System.Windows.Forms;

namespace QLyCuaHangSuaXe
{
    public partial class Form1 : Form
    {
        private DanhMuc.TiepNhan.frmTiepNhan formTiepNhan;
        private DanhMuc.DichVu.frmDanhMucDichVu formDanhMucDichVu;
        private DanhMuc.DangNhap.frmDangNhap formDangNhap;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnMoTiepNhan_Click(object sender, EventArgs e)
        {
            MoForm(ref formTiepNhan);
        }

        private void btnMoDanhMucDichVu_Click(object sender, EventArgs e)
        {
            MoForm(ref formDanhMucDichVu);
        }

        private void btnMoDangNhap_Click(object sender, EventArgs e)
        {
            if (formDangNhap == null || formDangNhap.IsDisposed)
                formDangNhap = new DanhMuc.DangNhap.frmDangNhap();
            formDangNhap.DangNhapThanhCong -= frmDangNhap_DangNhapThanhCong;
            formDangNhap.DangNhapThanhCong += frmDangNhap_DangNhapThanhCong;
            MoForm(ref formDangNhap);
        }

        private void frmDangNhap_DangNhapThanhCong(object sender, EventArgs e)
        {
            DanhMuc.DangNhap.frmDangNhap dangNhap = (DanhMuc.DangNhap.frmDangNhap)sender;
            lblHuongDan.Text = "Đã đăng nhập: " + dangNhap.TenDangNhap + " (tài khoản tạm)";
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MoForm<T>(ref T form) where T : Form, new()
        {
            if (form == null || form.IsDisposed)
                form = new T();
            if (!form.Visible)
            {
                form.StartPosition = FormStartPosition.CenterScreen;
                form.Show(this);
            }
            if (form.WindowState == FormWindowState.Minimized)
                form.WindowState = FormWindowState.Normal;
            form.Activate();
        }
    }
}
