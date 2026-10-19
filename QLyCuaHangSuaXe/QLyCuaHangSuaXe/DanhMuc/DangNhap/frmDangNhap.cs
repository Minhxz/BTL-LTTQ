using System;
using System.Windows.Forms;

namespace QLyCuaHangSuaXe.DanhMuc.DangNhap
{
    public partial class frmDangNhap : Form
    {
        private frmDoiMatKhau formDoiMatKhau;
        private frmQuanLyTaiKhoan formQuanLyTaiKhoan;

        public string TenDangNhap { get; private set; }
        public event EventHandler DangNhapThanhCong;

        public frmDangNhap()
        {
            InitializeComponent();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenDangNhap.Text) || txtMatKhau.Text.Length == 0)
            {
                HienLoiDangNhap("Vui lòng nhập tên đăng nhập và mật khẩu.");
                if (string.IsNullOrWhiteSpace(txtTenDangNhap.Text))
                    txtTenDangNhap.Focus();
                else
                    txtMatKhau.Focus();
                return;
            }

            // Tài khoản tạm để chạy thử trước khi kết nối dữ liệu tài khoản.
            if (!string.Equals(txtTenDangNhap.Text.Trim(), "minh", StringComparison.Ordinal)
                || txtMatKhau.Text != "123")
            {
                HienLoiDangNhap("Tên đăng nhập hoặc mật khẩu không đúng.");
                txtMatKhau.Clear();
                txtMatKhau.Focus();
                return;
            }

            TenDangNhap = "minh";
            txtMatKhau.Clear();
            EventHandler handler = DangNhapThanhCong;
            if (handler != null)
                handler(this, EventArgs.Empty);
            DialogResult = DialogResult.OK;
            Close();
        }

        private void HienLoiDangNhap(string thongBao)
        {
            lblMoTa.ForeColor = System.Drawing.Color.Firebrick;
            lblMoTa.Text = thongBao;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void chkHienMatKhau_CheckedChanged(object sender, EventArgs e)
        {
            txtMatKhau.UseSystemPasswordChar = !chkHienMatKhau.Checked;
        }

        private void btnMoDoiMatKhau_Click(object sender, EventArgs e)
        {
            MoForm(ref formDoiMatKhau);
        }

        private void btnMoQuanLyTaiKhoan_Click(object sender, EventArgs e)
        {
            MoForm(ref formQuanLyTaiKhoan);
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

        private void lblMoTa_Click(object sender, EventArgs e)
        {

        }
    }
}
