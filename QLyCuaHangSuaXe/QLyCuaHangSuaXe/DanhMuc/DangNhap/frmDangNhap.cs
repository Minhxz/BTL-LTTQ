using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace QLyCuaHangSuaXe.DanhMuc.DangNhap
{
    public partial class frmDangNhap : Form
    {
        public string TenDangNhap { get; private set; }
        public string VaiTro { get; private set; }

        public event EventHandler DangNhapThanhCong;
        public frmDangNhap()
        {
            InitializeComponent();
            txtTenDangNhap.Enter += delegate
            {
                pnlTenDangNhap.Invalidate();
            };
            txtTenDangNhap.Leave += delegate
            {
                pnlTenDangNhap.Invalidate();
            };
            txtMatKhau.Enter += delegate
            {
                pnlMatKhau.Invalidate();
            };
            txtMatKhau.Leave += delegate
            {
                pnlMatKhau.Invalidate();
            };
            pnlTenDangNhap.Click += delegate
            {
                txtTenDangNhap.Focus();
            };
            pnlMatKhau.Click += delegate
            {
                txtMatKhau.Focus();
            };
        }

        private static GraphicsPath KhungBoGoc(RectangleF khung, float banKinh)
        {
            float duongKinh = banKinh * 2;
            GraphicsPath duongDan = new GraphicsPath();
            duongDan.AddArc(khung.Left, khung.Top, duongKinh, duongKinh, 180, 90);
            duongDan.AddArc(khung.Right - duongKinh, khung.Top, duongKinh, duongKinh, 270, 90);
            duongDan.AddArc(khung.Right - duongKinh, khung.Bottom - duongKinh, duongKinh, duongKinh, 0, 90);
            duongDan.AddArc(khung.Left, khung.Bottom - duongKinh, duongKinh, duongKinh, 90, 90);
            duongDan.CloseFigure();
            return duongDan;
        }

        private void KhungNhap_Ve(object doiTuongGui, PaintEventArgs suKienVe)
        {
            Panel khungNhap = (Panel)doiTuongGui;
            suKienVe.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            using (GraphicsPath duongDan = KhungBoGoc(new RectangleF(1, 1, khungNhap.Width - 2, khungNhap.Height - 2), 8))
            using (Brush nen = new SolidBrush(Color.FromArgb(246, 248, 250)))
            using (Pen vien = new Pen(khungNhap.ContainsFocus ? Color.FromArgb(32, 125, 143) : Color.FromArgb(220, 229, 235), khungNhap.ContainsFocus ? 2 : 1))
            {
                suKienVe.Graphics.FillPath(nen, duongDan);
                suKienVe.Graphics.DrawPath(vien, duongDan);
            }
        }

        private void pnlThuongHieu_Ve(object doiTuongGui, PaintEventArgs suKienVe)
        {
            Graphics doHoa = suKienVe.Graphics;
            doHoa.SmoothingMode = SmoothingMode.AntiAlias;
            using (Brush nenThuongHieu = new LinearGradientBrush(pnlThuongHieu.ClientRectangle, Color.FromArgb(22, 43, 61), Color.FromArgb(17, 67, 79), 90F))
                doHoa.FillRectangle(nenThuongHieu, pnlThuongHieu.ClientRectangle);
            GraphicsState trangThai = doHoa.Save();
            doHoa.ScaleTransform(pnlThuongHieu.Width / 400F, pnlThuongHieu.Height / 660F);
            using (Pen trangTri = new Pen(Color.FromArgb(14, 220, 241, 242), 1F))
            {
                doHoa.DrawEllipse(trangTri, 220, -130, 330, 330);
                doHoa.DrawEllipse(trangTri, 190, -160, 390, 390);
                doHoa.DrawEllipse(trangTri, -145, 370, 410, 410);
            }

            doHoa.TranslateTransform(24, 352);
            using (Pen netVe = new Pen(Color.FromArgb(193, 232, 230), 3F))
            using (Pen netPhu = new Pen(Color.FromArgb(79, 135, 148), 2F))
            using (Pen netNhan = new Pen(Color.FromArgb(115, 215, 192), 5F))
            using (Brush nenBinhXang = new SolidBrush(Color.FromArgb(33, 94, 108)))
            using (GraphicsPath binhXang = new GraphicsPath())
            {
                netVe.StartCap = netVe.EndCap = LineCap.Round;
                netPhu.StartCap = netPhu.EndCap = LineCap.Round;
                netNhan.StartCap = netNhan.EndCap = LineCap.Round;
                doHoa.DrawLine(netPhu, 20, 163, 330, 163);
                doHoa.DrawEllipse(netVe, 35, 85, 64, 64);
                doHoa.DrawEllipse(netVe, 241, 85, 64, 64);
                doHoa.DrawEllipse(netPhu, 45, 95, 44, 44);
                doHoa.DrawEllipse(netPhu, 251, 95, 44, 44);
                doHoa.DrawLines(netVe, new[] { new Point(67, 117), new Point(129, 53), new Point(193, 117), new Point(67, 117) });
                doHoa.DrawLine(netPhu, 67, 117, 182, 62);
                doHoa.DrawLine(netVe, 193, 117, 240, 57);
                doHoa.DrawLine(netVe, 273, 117, 239, 27);
                doHoa.DrawLines(netVe, new[] { new Point(221, 12), new Point(244, 12), new Point(255, 30) });
                doHoa.DrawLine(netNhan, 98, 42, 153, 42);
                doHoa.DrawLine(netPhu, 77, 59, 111, 59);
                doHoa.DrawLine(netVe, 250, 41, 267, 41);
                binhXang.AddBezier(145, 60, 170, 28, 206, 32, 237, 56);
                binhXang.AddLine(237, 56, 212, 79);
                binhXang.AddLine(212, 79, 157, 77);
                binhXang.CloseFigure();
                doHoa.FillPath(nenBinhXang, binhXang);
                doHoa.DrawPath(netVe, binhXang);
                using (GraphicsPath dongCo = KhungBoGoc(new RectangleF(148, 85, 43, 28), 6))
                    doHoa.DrawPath(netPhu, dongCo);
                doHoa.DrawLine(netVe, 193, 125, 229, 125);
                doHoa.DrawLine(netVe, 229, 125, 241, 117);
            }

            doHoa.Restore(trangThai);
        }

        private async void btnDangNhap_Bam(object doiTuongGui, EventArgs suKien)
        {
            if (!btnDangNhap.Enabled || !string.IsNullOrEmpty(TenDangNhap))
                return;
            if (string.IsNullOrWhiteSpace(txtTenDangNhap.Text) || txtMatKhau.Text.Length == 0)
            {
                HienLoiDangNhap("Vui lòng nhập tên đăng nhập và mật khẩu.");
                if (string.IsNullOrWhiteSpace(txtTenDangNhap.Text))
                    txtTenDangNhap.Focus();
                else
                    txtMatKhau.Focus();
                return;
            }

            bool hopLe;
            string tenTaiKhoan = txtTenDangNhap.Text.Trim();
            string matKhau = txtMatKhau.Text;
            try
            {
                UseWaitCursor = true;
                btnDangNhap.Enabled = false;
                hopLe = await System.Threading.Tasks.Task.Run(() => LopDungChung.PhienDangNhap.DangNhap(tenTaiKhoan, matKhau));
            }
            catch (System.Data.SqlClient.SqlException)
            {
                if (!IsDisposed) HienLoiDangNhap("Không kết nối được QuanLySuaXe. Kiểm tra SQL Server và App.config.");
                return;
            }
            finally
            {
                if (!IsDisposed) { UseWaitCursor = false; btnDangNhap.Enabled = true; }
            }

            if (IsDisposed || Disposing) return;

            if (!hopLe)
            {
                HienLoiDangNhap("Tên đăng nhập hoặc mật khẩu không đúng.");
                txtMatKhau.Clear();
                txtMatKhau.Focus();
                return;
            }

            TenDangNhap = LopDungChung.PhienDangNhap.TenDangNhap;
            VaiTro = LopDungChung.PhienDangNhap.VaiTro;
            txtMatKhau.Clear();
            txtTenDangNhap.Enabled = false;
            txtMatKhau.Enabled = false;
            chkHienMatKhau.Enabled = false;
            btnDangNhap.Enabled = false;
            lblMoTa.ForeColor = System.Drawing.Color.DarkGreen;
            lblMoTa.Text = "Đăng nhập thành công.";
            EventHandler xuLySuKien = DangNhapThanhCong;
            if (xuLySuKien != null)
                xuLySuKien(this, EventArgs.Empty);
        }

        private void HienLoiDangNhap(string thongBao)
        {
            lblMoTa.ForeColor = System.Drawing.Color.Firebrick;
            lblMoTa.Text = thongBao;
        }

        private void btnThoat_Bam(object doiTuongGui, EventArgs suKien)
        {
            Close();
        }

        protected override void OnFormClosing(FormClosingEventArgs suKien)
        {
            base.OnFormClosing(suKien);
            if (!suKien.Cancel && string.IsNullOrEmpty(TenDangNhap) && suKien.CloseReason == CloseReason.UserClosing && Visible && !DesignMode)
                suKien.Cancel = MessageBox.Show(this, "Bạn có chắc muốn thoát ứng dụng?", "Xác nhận thoát",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes;
        }

        private void chkHienMatKhau_DoiTrangThai(object doiTuongGui, EventArgs suKien)
        {
            txtMatKhau.UseSystemPasswordChar = !chkHienMatKhau.Checked;
        }

        private void lblMoTa_Bam(object doiTuongGui, EventArgs suKien)
        {
        }

        private void frmDangNhap_Tai(object doiTuongGui, EventArgs suKien)
        {
        }
    }
}
