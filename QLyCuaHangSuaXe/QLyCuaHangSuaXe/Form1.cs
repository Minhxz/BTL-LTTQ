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
            lblThuongHieu.Text = "QUẢN LÝ SỬA XE\n\n" + tenDangNhap + "\n" + VaiTro;
            NoiNutCoSan(btnMoTiepNhan, "TIEP_NHAN", "Phiếu tiếp nhận", () => new DanhMuc.TiepNhan.frmTiepNhan());
            NoiNutCoSan(btnMoDanhMucDichVu, "DICH_VU", "Dịch vụ", () => new DanhMuc.DichVu.frmDanhMucDichVu());
            if (VaiTro != "Chủ/Quản lý")
                ThemChucNang("PHAN_CONG", "Phân công sửa chữa", () => new DanhMuc.frmPhanCong());
            ThemChucNang("CONG_CA_NHAN", "Bảng công của tôi", () => new DanhMuc.frmBangCong());
            NoiNutCoSan(btnMoKho, "NHAP_KHO", "Kho phụ tùng", () => new btl.frmKho());
            NoiNutCoSan(btnMoBaoCao, "BAO_CAO", "Thống kê", () => new DanhMuc.frmThongKe());
            NoiNutCoSan(btnMoQuanLyTaiKhoan, "QUAN_LY_TAI_KHOAN", "Nhân sự", () => new DanhMuc.frmNhanVien());
            btnMoDoiMatKhau.Visible = tenDangNhap.Length > 0;
            btnMoDoiMatKhau.Click += (nguoiGui, suKien) => GiaoDien.ThucHien(() => MoTrang("DOI_MK", () => new DanhMuc.DangNhap.frmDoiMatKhau()));
            btnThoat.Click += (nguoiGui, suKien) => Close();
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
            btnThaoTac.BackColor = Color.FromArgb(32, 66, 93);
            return btnThaoTac;
        }

        void ThemChucNang(string maChucNang, string tieuDe, Func<Form> taoTrang)
        {
            if (PhanQuyenNguoiDung.CoQuyen(VaiTro, maChucNang))
                flpDieuHuong.Controls.Add(TaoNutDieuHuong(tieuDe, () => MoTrang(maChucNang, taoTrang)));
        }

        void MoTrang(string maChucNang, Func<Form> taoTrang)
        {
            if (maChucNang != "DOI_MK" && !PhanQuyenNguoiDung.CoQuyen(VaiTro, maChucNang))
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
                    nut.BackColor = Color.FromArgb(32, 66, 93);
            }

            Button nutDangChon = maChucNang == "TIEP_NHAN" ? btnMoTiepNhan : maChucNang == "DICH_VU" ? btnMoDanhMucDichVu : maChucNang == "NHAP_KHO" ? btnMoKho : maChucNang == "QUAN_LY_TAI_KHOAN" ? btnMoQuanLyTaiKhoan : maChucNang == "BAO_CAO" ? btnMoBaoCao : null;
            if (nutDangChon != null)
                nutDangChon.BackColor = GiaoDien.Xanh;
            trang.BringToFront();
        }
    }
}
