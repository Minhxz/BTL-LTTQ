using QLyCuaHangSuaXe.LopDungChung;
using System;
using System.Windows.Forms;

namespace QLyCuaHangSuaXe.DanhMuc.DangNhap
{
    public partial class frmDoiMatKhau : TrangThietKe
    {
        public frmDoiMatKhau()
        {
            InitializeComponent();
            GanThietKe(null);
            btnDong.Click += (nguoiGui, suKien) => Close();
            chkHienMatKhau.CheckedChanged += (nguoiGui, suKien) => txtMatKhauHienTai.UseSystemPasswordChar = txtMatKhauMoi.UseSystemPasswordChar = txtXacNhanMatKhauMoi.UseSystemPasswordChar = !chkHienMatKhau.Checked;
            btnDoiMatKhau.Click += (nguoiGui, suKien) => GiaoDien.ThucHien(() =>
            {
                if (txtMatKhauMoi.Text != txtXacNhanMatKhauMoi.Text)
                    throw new InvalidOperationException("Xác nhận mật khẩu mới không khớp.");
                PhienDangNhap.DoiMatKhau(txtMatKhauHienTai.Text, txtMatKhauMoi.Text);
                txtMatKhauHienTai.Clear();
                txtMatKhauMoi.Clear();
                txtXacNhanMatKhauMoi.Clear();
                lblGhiChuMatKhau.Text = "Đã đổi mật khẩu thành công.";
            });
        }

        public override void TaiDuLieu()
        {
            if (string.IsNullOrEmpty(PhienDangNhap.MaNhanVien))
                throw new UnauthorizedAccessException("Cần đăng nhập trước khi đổi mật khẩu.");
        }
    }
}
