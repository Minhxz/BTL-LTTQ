using System;
using System.Windows.Forms;

namespace QLyCuaHangSuaXe.DanhMuc.DangNhap
{
    public partial class frmDoiMatKhau : Form
    {
        public frmDoiMatKhau()
        {
            InitializeComponent();
        }

        private void chkHienMatKhau_CheckedChanged(object sender, EventArgs e)
        {
            bool anMatKhau = !chkHienMatKhau.Checked;
            txtMatKhauHienTai.UseSystemPasswordChar = anMatKhau;
            txtMatKhauMoi.UseSystemPasswordChar = anMatKhau;
            txtXacNhanMatKhauMoi.UseSystemPasswordChar = anMatKhau;
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmDoiMatKhau_Load(object sender, EventArgs e)
        {

        }
    }
}
