using QLyCuaHangSuaXe.LopDungChung;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace QLyCuaHangSuaXe
{
    public partial class frmChinh : Form
    {
        public string TenDangNhap { get; private set; }
        public string VaiTro { get; private set; }

        readonly Dictionary<string, Form> cacTrang = new Dictionary<string, Form>();
        readonly Dictionary<string, Button> cacNutMenu = new Dictionary<string, Button>();
        static readonly Color mauNutMenu = Color.FromArgb(29, 52, 75);
        public frmChinh() : this(PhienDangNhap.TenDangNhap ?? "", PhienDangNhap.VaiTro ?? "")
        {
        }

        public frmChinh(string tenDangNhap) : this(tenDangNhap, "Chủ/Quản lý")
        {
        }

        public frmChinh(string tenDangNhap, string vaiTro)
        {
            InitializeComponent();
            TenDangNhap = tenDangNhap;
            VaiTro = PhanQuyenNguoiDung.ChuanHoa(vaiTro);
            GiaoDien.ChuanHoa(this);
            MinimumSize = new Size(1200, 800);
            Text = "Quản lý cửa hàng sửa xe";
            WindowState = FormWindowState.Maximized;
            lblThuongHieu.Text = "QUẢN LÝ SỬA XE\n" + tenDangNhap + " • " + VaiTro;
            lblThuongHieu.Size = new Size(230, 138);
            lblThuongHieu.Paint += VeThuongHieu;
            pnlMenu.BackColor = flpDieuHuong.BackColor = Color.FromArgb(17, 36, 55);
            pnlNoiDung.Padding = new Padding(12);
            NoiNutCoSan(btnMoTiepNhan, "TIEP_NHAN", "Phiếu tiếp nhận", () => new DanhMuc.TiepNhan.frmTiepNhan());
            NoiNutCoSan(btnMoDanhMucDichVu, "DICH_VU", "Dịch vụ", () => new DanhMuc.DichVu.frmDanhMucDichVu());
            if (VaiTro != "Chủ/Quản lý")
                ThemChucNang("PHAN_CONG", "Phân công sửa chữa", () => new DanhMuc.frmPhanCong());
            ThemChucNang("CONG_CA_NHAN", "Bảng công của tôi", () => new DanhMuc.frmBangCong());
            NoiNutCoSan(btnMoKho, "NHAP_KHO", "Kho phụ tùng", () => new btl.frmKho());
            NoiNutCoSan(btnMoBaoCao, "BAO_CAO", "Thống kê", () => new DanhMuc.frmThongKe());
            NoiNutCoSan(btnMoNhanVien, "NHAN_VIEN", "Nhân viên", () => new DanhMuc.frmNhanVien());
            NoiNutCoSan(btnMoQuanLyTaiKhoan, "TAI_KHOAN_CA_NHAN", "Tài khoản", () => new DanhMuc.DangNhap.frmQuanLyTaiKhoan());
            btnThoat.Click += (nguoiGui, suKien) => Close();
            foreach (Control dieuKhien in flpDieuHuong.Controls)
            {
                var nut = dieuKhien as Button;
                if (nut == null) continue;
                nut.BackColor = mauNutMenu;
                string bieuTuong = nut == btnMoTiepNhan ? "\uE8A5" : nut == btnMoDanhMucDichVu ? "\uE90F" : nut == btnMoKho ? "\uE7B8" : nut == btnMoNhanVien ? "\uE716" : nut == btnMoQuanLyTaiKhoan ? "\uE77B" : nut == btnMoBaoCao ? "\uE9D9" : nut == btnThoat ? "\uE8BB" : "\uE787";
                GiaoDien.DinhDangNutDieuHuong(nut, bieuTuong);
            }
            flpDieuHuong.Controls.SetChildIndex(btnThoat, flpDieuHuong.Controls.Count - 1);
            Shown += (doiTuongGui, suKien) =>
            {
                if (PhanQuyenNguoiDung.CoQuyen(VaiTro, "TIEP_NHAN"))
                    MoTrang("TIEP_NHAN", () => new DanhMuc.TiepNhan.frmTiepNhan());
                else if (PhanQuyenNguoiDung.CoQuyen(VaiTro, "PHAN_CONG"))
                    MoTrang("PHAN_CONG", () => new DanhMuc.frmPhanCong());
            };
        }

        void NoiNutCoSan(Button nut, string maChucNang, string tieuDe, Func<Form> taoTrang)
        {
            nut.Visible = PhanQuyenNguoiDung.CoQuyen(VaiTro, maChucNang);
            if (!PhanQuyenNguoiDung.CoQuyen(VaiTro, maChucNang))
                return;
            nut.Text = tieuDe;
            cacNutMenu[maChucNang] = nut;
            nut.Enabled = true;
            nut.Click += (nguoiGui, suKien) => GiaoDien.ThucHien(() => MoTrang(maChucNang, taoTrang));
        }

        Button TaoNutDieuHuong(string noiDung, Action thaoTac)
        {
            var btnThaoTac = GiaoDien.TaoNut(noiDung, thaoTac);
            btnThaoTac.Width = 230;
            btnThaoTac.Height = 58;
            btnThaoTac.AutoSize = false;
            btnThaoTac.ForeColor = Color.White;
            btnThaoTac.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            btnThaoTac.TextAlign = ContentAlignment.MiddleLeft;
            btnThaoTac.BackColor = mauNutMenu;
            return btnThaoTac;
        }

        void ThemChucNang(string maChucNang, string tieuDe, Func<Form> taoTrang)
        {
            if (PhanQuyenNguoiDung.CoQuyen(VaiTro, maChucNang))
            {
                var nut = TaoNutDieuHuong(tieuDe, () => MoTrang(maChucNang, taoTrang));
                cacNutMenu[maChucNang] = nut;
                flpDieuHuong.Controls.Add(nut);
            }
        }

        void MoTrang(string maChucNang, Func<Form> taoTrang)
        {
            if (!PhanQuyenNguoiDung.CoQuyen(VaiTro, maChucNang))
                return;
            Form trang;
            if (!cacTrang.TryGetValue(maChucNang, out trang) || trang.IsDisposed)
            {
                trang = taoTrang();
                trang.TopLevel = false;
                trang.FormBorderStyle = FormBorderStyle.None;
                trang.Dock = DockStyle.Fill;
                trang.MinimumSize = Size.Empty;
                cacTrang[maChucNang] = trang;
                pnlNoiDung.Controls.Add(trang);
                trang.Show();
            }
            else
            {
                var trangDuLieu = trang as TrangDuLieu;
                if (trangDuLieu != null)
                    GiaoDien.ThucHien(trangDuLieu.TaiDuLieu);
                var trangThietKe = trang as TrangThietKe;
                if (trangThietKe != null)
                    GiaoDien.ThucHien(trangThietKe.TaiDuLieu);
            }

            foreach (Control dieuKhien in flpDieuHuong.Controls)
            {
                var nut = dieuKhien as Button;
                if (nut != null)
                    nut.BackColor = mauNutMenu;
            }

            Button nutDangChon;
            if (cacNutMenu.TryGetValue(maChucNang, out nutDangChon))
                nutDangChon.BackColor = GiaoDien.Xanh;
            trang.BringToFront();
        }

        protected override void OnFormClosing(FormClosingEventArgs suKien)
        {
            base.OnFormClosing(suKien);
            if (!suKien.Cancel && suKien.CloseReason == CloseReason.UserClosing && Visible && !DesignMode)
                suKien.Cancel = MessageBox.Show(this, "Bạn có chắc muốn thoát ứng dụng?", "Xác nhận thoát",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes;
        }

        void VeThuongHieu(object nguoiGui, PaintEventArgs suKien)
        {
            suKien.Graphics.Clear(flpDieuHuong.BackColor);
            using (var tieuDe = new Font("Segoe UI", 19, FontStyle.Bold))
            using (var chuNho = new Font("Segoe UI", 8.5F))
            using (var ten = new Font("Segoe UI", 11, FontStyle.Bold))
            using (var net = new Pen(Color.FromArgb(49, 72, 94)))
            {
                TextRenderer.DrawText(suKien.Graphics, "SỬA XE", tieuDe, new Point(10, 7), Color.White);
                TextRenderer.DrawText(suKien.Graphics, "QUẢN LÝ CỬA HÀNG", chuNho, new Point(12, 45), Color.FromArgb(151, 182, 208));
                suKien.Graphics.DrawLine(net, 12, 78, 216, 78);
                string tenHienThi = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(TenDangNhap ?? "");
                TextRenderer.DrawText(suKien.Graphics, tenHienThi, ten, new Point(12, 88), Color.White);
                TextRenderer.DrawText(suKien.Graphics, VaiTro, chuNho, new Point(12, 113), Color.FromArgb(151, 182, 208));
            }
        }
    }
}
