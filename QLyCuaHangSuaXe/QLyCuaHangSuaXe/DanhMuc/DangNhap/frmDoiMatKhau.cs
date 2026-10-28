using QLyCuaHangSuaXe.LopDungChung;
using System;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Drawing;

namespace QLyCuaHangSuaXe.DanhMuc.DangNhap
{
    public partial class frmDoiMatKhau : TrangThietKe
    {
        public frmDoiMatKhau() : this(false) { }

        public frmDoiMatKhau(bool nhungTrongTaiKhoan)
        {
            InitializeComponent();
            GanThietKe(null);
            AcceptButton = btnDoiMatKhau;
            if (nhungTrongTaiKhoan) { btnDong.Visible = false; CancelButton = null; }
            btnDong.Click += (nguoiGui, suKien) => Close();
            chkHienMatKhau.CheckedChanged += (nguoiGui, suKien) => txtMatKhauHienTai.UseSystemPasswordChar = txtMatKhauMoi.UseSystemPasswordChar = txtXacNhanMatKhauMoi.UseSystemPasswordChar = !chkHienMatKhau.Checked;
            btnDoiMatKhau.Click += async (nguoiGui, suKien) =>
            {
                if (!btnDoiMatKhau.Enabled) return;
                if (txtMatKhauMoi.Text != txtXacNhanMatKhauMoi.Text || string.IsNullOrWhiteSpace(txtMatKhauMoi.Text))
                {
                    lblGhiChuMatKhau.ForeColor = Color.Firebrick;
                    lblGhiChuMatKhau.Text = "Nhập mật khẩu mới và xác nhận giống nhau, không để trống.";
                    return;
                }
                string cu = txtMatKhauHienTai.Text, moi = txtMatKhauMoi.Text;
                btnDoiMatKhau.Enabled = btnDong.Enabled = false;
                lblGhiChuMatKhau.Text = "Đang lưu mật khẩu…";
                try
                {
                    await Task.Run(() => PhienDangNhap.DoiMatKhau(cu, moi));
                    if (IsDisposed) return;
                    txtMatKhauHienTai.Clear();
                    txtMatKhauMoi.Clear();
                    txtXacNhanMatKhauMoi.Clear();
                    lblGhiChuMatKhau.ForeColor = Color.SeaGreen;
                    lblGhiChuMatKhau.Text = "Đã đổi mật khẩu thành công.";
                }
                catch (Exception loi)
                {
                    if (!IsDisposed)
                    {
                        lblGhiChuMatKhau.ForeColor = Color.Firebrick;
                        lblGhiChuMatKhau.Text = loi is System.Data.SqlClient.SqlException ? "Chưa kết nối được SQL. Hãy thử lại." : loi.Message;
                    }
                }
                finally { if (!IsDisposed) btnDoiMatKhau.Enabled = btnDong.Enabled = true; }
            };
        }

        public override void TaiDuLieu()
        {
            PhienDangNhap.YeuCau("TAI_KHOAN_CA_NHAN");
            lblTaiKhoan.Text = "Tài khoản: " + PhienDangNhap.TenDangNhap;
        }

        public void XoaNoiDung()
        {
            txtMatKhauHienTai.Clear(); txtMatKhauMoi.Clear(); txtXacNhanMatKhauMoi.Clear();
            chkHienMatKhau.Checked = false;
            lblGhiChuMatKhau.ForeColor = Color.FromArgb(64, 82, 99);
            lblGhiChuMatKhau.Text = "";
        }
    }
}
