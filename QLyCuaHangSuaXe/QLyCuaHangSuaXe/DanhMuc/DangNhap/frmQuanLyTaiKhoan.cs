using QLyCuaHangSuaXe.LopDungChung;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace QLyCuaHangSuaXe.DanhMuc.DangNhap
{
    public partial class frmQuanLyTaiKhoan : TrangThietKe
    {
        string maNhanVienDangChon = "";
        DataTable bangTaiKhoan;
        bool dangThem, dangGanNhanVien;
        frmDoiMatKhau trangDoiMatKhau;

        public frmQuanLyTaiKhoan()
        {
            InitializeComponent();
            Text = "Tài khoản";
            GanThietKe(dgvTaiKhoan, lblHuongDan);
            tabTaiKhoan.DrawMode = TabDrawMode.OwnerDrawFixed;
            tabTaiKhoan.DrawItem += VeTheTaiKhoan;
            tabMatKhau.Resize += (nguoiGui, suKien) => pnlDoiMatKhau.Left = Math.Max(12, (tabMatKhau.ClientSize.Width - pnlDoiMatKhau.Width) / 2);
            cboChucVu.Items.AddRange(PhanQuyenNguoiDung.VaiTro);
            cboLocVaiTro.Items.Add("Tất cả chức vụ");
            cboLocVaiTro.Items.AddRange(PhanQuyenNguoiDung.VaiTro);
            cboLocVaiTro.SelectedIndex = 0;
            dgvTaiKhoan.SelectionChanged += (nguoiGui, suKien) => ChonDong();
            btnThemMoi.Click += (nguoiGui, suKien) => GiaoDien.ThucHien(BatDauThem);
            btnHuyThem.Click += (nguoiGui, suKien) => { CheDoThem(false); ChonDong(); };
            cboNhanVienCoSan.SelectedIndexChanged += (nguoiGui, suKien) => ChonNhanVienCoSan();
            btnLuuTaiKhoan.Click += (nguoiGui, suKien) => GiaoDien.ThucHien(Luu);
            btnLamMoi.Click += (nguoiGui, suKien) => GiaoDien.ThucHien(TaiDuLieu);
            btnTimKiem.Click += (nguoiGui, suKien) => Loc();
            cboLocVaiTro.SelectedIndexChanged += (nguoiGui, suKien) => Loc();
            btnDong.Click += (nguoiGui, suKien) => Close();
            tabTaiKhoan.SelectedIndexChanged += (nguoiGui, suKien) =>
            {
                if (trangDoiMatKhau != null) trangDoiMatKhau.XoaNoiDung();
                txtMatKhau.Clear();
                if (tabTaiKhoan.SelectedTab == tabMatKhau)
                    GiaoDien.ThucHien(HienDoiMatKhau);
            };
            btnPhanQuyen.Click += (nguoiGui, suKien) => GiaoDien.ThucHien(() =>
            {
                PhienDangNhap.YeuCau("PHAN_QUYEN");
                using (var trang = new frmPhanQuyen()) trang.ShowDialog(this);
            });
            CheDoThem(false);
            DatQuyenQuanLy(string.IsNullOrEmpty(PhienDangNhap.MaNhanVien) || PhanQuyenNguoiDung.CoQuyen(PhienDangNhap.VaiTro, "QUAN_LY_TAI_KHOAN"));
        }

        void DatQuyenQuanLy(bool duocQuanLy)
        {
            if (duocQuanLy)
            {
                if (!tabTaiKhoan.TabPages.Contains(tabQuanLy)) tabTaiKhoan.TabPages.Insert(0, tabQuanLy);
            }
            else
            {
                CheDoThem(false);
                bangTaiKhoan = null;
                dgvTaiKhoan.DataSource = null;
                cboNhanVienCoSan.DataSource = null;
                maNhanVienDangChon = "";
                txtMaTaiKhoan.Clear(); txtTenNhanVien.Clear(); txtTenDangNhap.Clear(); txtMatKhau.Clear();
                cboChucVu.SelectedIndex = -1;
                btnLuuTaiKhoan.Enabled = false;
                tabTaiKhoan.TabPages.Remove(tabQuanLy);
            }
        }

        void VeTheTaiKhoan(object nguoiGui, DrawItemEventArgs suKien)
        {
            bool dangChon = suKien.Index == tabTaiKhoan.SelectedIndex;
            using (var mauNen = new SolidBrush(dangChon ? GiaoDien.Xanh : Color.FromArgb(235, 243, 251)))
                suKien.Graphics.FillRectangle(mauNen, suKien.Bounds);
            using (var chu = new Font(tabTaiKhoan.Font, FontStyle.Bold))
                TextRenderer.DrawText(suKien.Graphics, tabTaiKhoan.TabPages[suKien.Index].Text, chu,
                    suKien.Bounds, dangChon ? Color.White : Color.FromArgb(30, 85, 130), TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }

        public override void TaiDuLieu()
        {
            PhienDangNhap.YeuCau("TAI_KHOAN_CA_NHAN");
            bool duocQuanLy = PhanQuyenNguoiDung.CoQuyen(PhienDangNhap.VaiTro, "QUAN_LY_TAI_KHOAN");
            DatQuyenQuanLy(duocQuanLy);
            if (!duocQuanLy)
            {
                HienDoiMatKhau();
                return;
            }
            PhienDangNhap.YeuCau("QUAN_LY_TAI_KHOAN");
            CheDoThem(false);
            bangTaiKhoan = duLieu.DocBang(@"SELECT n.MaNhanVien,n.TenNhanVien,CASE WHEN n.DaCapTaiKhoan=1 THEN n.TenDangNhap ELSE '' END AS TenDangNhap,v.TenChucVu,
                CASE WHEN n.DaCapTaiKhoan=0 THEN N'Chưa cấp' WHEN n.TaiKhoanHoatDong=1 THEN N'Đang bật' ELSE N'Đã khóa' END AS TinhTrangTaiKhoan,
                n.TrangThai,n.DaCapTaiKhoan,n.TaiKhoanHoatDong FROM dbo.NhanVien n JOIN dbo.ChucVu v ON v.MaChucVu=n.MaChucVu ORDER BY n.TenNhanVien");
            Loc();
            lblHuongDan.Text = "Tạo tài khoản ngay tại đây. Chọn dòng để sửa; để trống mật khẩu mới nếu giữ mật khẩu hiện tại.";
            if (tabTaiKhoan.SelectedTab == tabMatKhau) HienDoiMatKhau();
        }

        void HienDoiMatKhau()
        {
            PhienDangNhap.YeuCau("TAI_KHOAN_CA_NHAN");
            if (trangDoiMatKhau == null || trangDoiMatKhau.IsDisposed)
            {
                trangDoiMatKhau = new frmDoiMatKhau(true);
                trangDoiMatKhau.TopLevel = false;
                trangDoiMatKhau.FormBorderStyle = FormBorderStyle.None;
                trangDoiMatKhau.MinimumSize = System.Drawing.Size.Empty;
                trangDoiMatKhau.Dock = DockStyle.Fill;
                pnlDoiMatKhau.Controls.Add(trangDoiMatKhau);
                trangDoiMatKhau.Show();
            }
            trangDoiMatKhau.TaiDuLieu();
            trangDoiMatKhau.XoaNoiDung();
        }

        void CheDoThem(bool them)
        {
            dangThem = them;
            dgvTaiKhoan.Enabled = flpTimKiem.Enabled = !them;
            btnThemMoi.Enabled = !them;
            btnHuyThem.Visible = them;
            cboNhanVienCoSan.Visible = lblNhanVienCoSan.Visible = them;
            tlpNhap.RowStyles[3].Height = them ? 46 : 0;
            tlpTrang.RowStyles[1].Height = them ? 226 : 180;
            txtTenNhanVien.ReadOnly = !them;
            grpThongTin.Text = them ? "Tạo tài khoản mới" : "Tài khoản của nhân viên đang chọn";
            lbltxtMatKhau.Text = them ? "Mật khẩu *" : "Mật khẩu mới";
            btnLuuTaiKhoan.Text = them ? "Tạo tài khoản" : "Lưu tài khoản";
        }

        void BatDauThem()
        {
            PhienDangNhap.YeuCau("QUAN_LY_TAI_KHOAN");
            var nhanVien = duLieu.DocBang(@"SELECT n.MaNhanVien,n.TenNhanVien,v.TenChucVu FROM dbo.NhanVien n JOIN dbo.ChucVu v ON v.MaChucVu=n.MaChucVu WHERE n.DaCapTaiKhoan=0 AND n.TrangThai=N'Đang làm' ORDER BY n.TenNhanVien");
            var moi = nhanVien.NewRow(); moi["MaNhanVien"] = ""; moi["TenNhanVien"] = "Nhân viên mới — nhập họ tên ở trên"; moi["TenChucVu"] = "Thợ"; nhanVien.Rows.InsertAt(moi, 0);
            CheDoThem(true);
            maNhanVienDangChon = "";
            txtMaTaiKhoan.Text = "Tự động khi tạo";
            txtTenNhanVien.Clear(); txtTenDangNhap.Clear(); txtMatKhau.Clear();
            cboChucVu.SelectedItem = "Thợ";
            chkTaiKhoanHoatDong.Checked = true;
            dangGanNhanVien = true;
            try
            {
                GiaoDien.GanDuLieu(cboNhanVienCoSan, nhanVien, "TenNhanVien", "MaNhanVien");
                cboNhanVienCoSan.SelectedIndex = 0;
            }
            finally { dangGanNhanVien = false; }
            ChonNhanVienCoSan();
            btnLuuTaiKhoan.Enabled = true;
            lblHuongDan.Text = "Nhập họ tên, tên đăng nhập, mật khẩu và quyền; hoặc chọn nhân viên có sẵn để cấp tài khoản.";
            txtTenNhanVien.Focus();
        }

        void ChonNhanVienCoSan()
        {
            if (!dangThem || dangGanNhanVien) return;
            var dong = cboNhanVienCoSan.SelectedItem as DataRowView;
            maNhanVienDangChon = dong == null ? "" : dong["MaNhanVien"].ToString();
            bool moi = maNhanVienDangChon.Length == 0;
            txtMaTaiKhoan.Text = moi ? "Tự động khi tạo" : maNhanVienDangChon;
            txtTenNhanVien.Text = moi ? "" : dong["TenNhanVien"].ToString();
            txtTenNhanVien.ReadOnly = !moi;
            cboChucVu.SelectedItem = dong == null ? "Thợ" : PhanQuyenNguoiDung.ChuanHoa(dong["TenChucVu"].ToString());
        }

        void Loc()
        {
            if (bangTaiKhoan == null || dangThem) return;
            var loc = bangTaiKhoan.Clone();
            foreach (DataRow dong in bangTaiKhoan.Rows)
                if ((dong["TenNhanVien"] + " " + dong["TenDangNhap"] + " " + dong["MaNhanVien"]).IndexOf(txtTimKiem.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0 &&
                    (cboLocVaiTro.SelectedIndex <= 0 || PhanQuyenNguoiDung.ChuanHoa(dong["TenChucVu"].ToString()) == cboLocVaiTro.Text)) loc.ImportRow(dong);
            dgvTaiKhoan.DataSource = loc;
            dgvTaiKhoan.Columns["DaCapTaiKhoan"].Visible = false;
            dgvTaiKhoan.Columns["TaiKhoanHoatDong"].Visible = false;
            dgvTaiKhoan.Columns["TinhTrangTaiKhoan"].HeaderText = "Tài khoản";
            dgvTaiKhoan.Columns["TrangThai"].HeaderText = "Làm việc";
            ChonDong();
        }

        void ChonDong()
        {
            if (dangThem) return;
            var dong = dgvTaiKhoan.CurrentRow == null ? null : dgvTaiKhoan.CurrentRow.DataBoundItem as DataRowView;
            maNhanVienDangChon = dong == null ? "" : dong["MaNhanVien"].ToString();
            txtMaTaiKhoan.Text = maNhanVienDangChon;
            txtTenNhanVien.Text = dong == null ? "" : dong["TenNhanVien"].ToString();
            txtTenDangNhap.Text = dong == null ? "" : dong["TenDangNhap"].ToString();
            cboChucVu.SelectedItem = dong == null ? null : PhanQuyenNguoiDung.ChuanHoa(dong["TenChucVu"].ToString());
            chkTaiKhoanHoatDong.Checked = dong != null && (!Convert.ToBoolean(dong["DaCapTaiKhoan"]) || Convert.ToBoolean(dong["TaiKhoanHoatDong"]));
            txtMatKhau.Clear();
            txtTenNhanVien.ReadOnly = true;
            btnLuuTaiKhoan.Enabled = dong != null;
        }

        void Luu()
        {
            string ma = maNhanVienDangChon;
            bool taoMoi = dangThem;
            if (taoMoi)
                ma = QuanLyNhanSu.TaoTaiKhoanMoi(ma, txtTenNhanVien.Text.Trim(), txtTenDangNhap.Text.Trim(), txtMatKhau.Text, cboChucVu.Text, chkTaiKhoanHoatDong.Checked);
            else
                QuanLyNhanSu.LuuTaiKhoan(ma, txtTenDangNhap.Text.Trim(), txtMatKhau.Text, cboChucVu.Text, chkTaiKhoanHoatDong.Checked);
            txtMatKhau.Clear();
            txtTimKiem.Clear(); cboLocVaiTro.SelectedIndex = 0;
            TaiDuLieu();
            foreach (DataGridViewRow dong in dgvTaiKhoan.Rows)
                if (dong.Cells["MaNhanVien"].Value.ToString() == ma) { dgvTaiKhoan.CurrentCell = dong.Cells["TenNhanVien"]; break; }
            lblHuongDan.Text = taoMoi ? "Đã tạo tài khoản. Có thể đăng nhập bằng tên và mật khẩu vừa nhập; hồ sơ nằm trong trang Nhân viên." : "Đã lưu tài khoản. Đổi mật khẩu của bạn ở thẻ Đổi mật khẩu của tôi.";
        }
    }
}
