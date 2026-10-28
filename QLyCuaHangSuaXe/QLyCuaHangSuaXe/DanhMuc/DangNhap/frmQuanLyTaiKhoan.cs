using QLyCuaHangSuaXe.LopDungChung;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace QLyCuaHangSuaXe.DanhMuc.DangNhap
{
    public partial class frmQuanLyTaiKhoan : TrangThietKe
    {
        string maNhanVienDangChon = "", maTaiKhoanMoi;
        DataTable bangTaiKhoan;
        bool dangThem;
        frmDoiMatKhau trangDoiMatKhau;
        public frmQuanLyTaiKhoan()
        {
            InitializeComponent();
            GanThietKe(dgvTaiKhoan);
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
            btnLuuTaiKhoan.Click += (nguoiGui, suKien) => GiaoDien.ThucHien(Luu);
            btnLamMoi.Text = "Làm mới";
            btnLamMoi.Click += (nguoiGui, suKien) => GiaoDien.ThucHien(() => { TaiDuLieu(); BatDauThem(); });
            btnTimKiem.Click += (nguoiGui, suKien) => Loc();
            cboLocVaiTro.SelectedIndexChanged += (nguoiGui, suKien) => Loc();
            btnNghiViec.Click += (nguoiGui, suKien) => GiaoDien.ThucHien(() =>
            {
                if (MessageBox.Show(this, "Chuyển nhân viên đang chọn sang Nghỉ việc và khóa đăng nhập?", "Nghỉ việc", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes) return;
                QuanLyNhanSu.NghiViec(maNhanVienDangChon); TaiDuLieu();
            });
            btnDong.Click += (nguoiGui, suKien) => Close();
            tabTaiKhoan.SelectedIndexChanged += (nguoiGui, suKien) =>
            {
                if (trangDoiMatKhau != null) trangDoiMatKhau.XoaNoiDung();
                if (tabTaiKhoan.SelectedTab == tabMatKhau) GiaoDien.ThucHien(HienDoiMatKhau);
            };
            btnPhanQuyen.Click += (nguoiGui, suKien) => GiaoDien.ThucHien(() => { PhienDangNhap.YeuCau("PHAN_QUYEN"); using (var trang = new frmPhanQuyen()) trang.ShowDialog(this); });
            CheDoThem(false);
            DatQuyenQuanLy(string.IsNullOrEmpty(PhienDangNhap.MaNhanVien) || PhanQuyenNguoiDung.CoQuyen(PhienDangNhap.VaiTro, "QUAN_LY_TAI_KHOAN"));
        }
        void DatQuyenQuanLy(bool duocQuanLy)
        {
            if (duocQuanLy) { if (!tabTaiKhoan.TabPages.Contains(tabQuanLy)) tabTaiKhoan.TabPages.Insert(0, tabQuanLy); }
            else
            {
                CheDoThem(false); bangTaiKhoan = null; dgvTaiKhoan.DataSource = null; maNhanVienDangChon = "";
                txtMaTaiKhoan.Clear(); txtTenNhanVien.Clear(); txtTenDangNhap.Clear(); txtDienThoai.Clear(); cboChucVu.SelectedIndex = -1;
                btnLuuTaiKhoan.Enabled = false; tabTaiKhoan.TabPages.Remove(tabQuanLy);
            }
        }
        void VeTheTaiKhoan(object nguoiGui, DrawItemEventArgs suKien)
        {
            bool dangChon = suKien.Index == tabTaiKhoan.SelectedIndex;
            using (var nen = new SolidBrush(dangChon ? GiaoDien.Xanh : Color.FromArgb(235, 243, 251))) suKien.Graphics.FillRectangle(nen, suKien.Bounds);
            using (var chu = new Font(tabTaiKhoan.Font, FontStyle.Bold)) TextRenderer.DrawText(suKien.Graphics, tabTaiKhoan.TabPages[suKien.Index].Text, chu, suKien.Bounds, dangChon ? Color.White : Color.FromArgb(30, 85, 130), TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }
        public override void TaiDuLieu()
        {
            PhienDangNhap.YeuCau("TAI_KHOAN_CA_NHAN");
            bool duocQuanLy = PhanQuyenNguoiDung.CoQuyen(PhienDangNhap.VaiTro, "QUAN_LY_TAI_KHOAN");
            DatQuyenQuanLy(duocQuanLy);
            if (!duocQuanLy) { HienDoiMatKhau(); return; }
            PhienDangNhap.YeuCau("QUAN_LY_TAI_KHOAN"); CheDoThem(false);
            bangTaiKhoan = duLieu.DocBang(@"SELECT n.MaNhanVien,n.HoTen AS TenNhanVien,n.TenDangNhap,c.TenChucVu,n.SoDienThoai,n.Luong,CASE WHEN n.TrangThai=N'Đang làm' THEN N'Đang hoạt động' ELSE N'Đã khóa' END AS TinhTrangTaiKhoan,n.TrangThai FROM dbo.NhanVien n JOIN dbo.ChucVu c ON c.MaChucVu=n.MaChucVu ORDER BY n.HoTen");
            Loc(); if (tabTaiKhoan.SelectedTab == tabMatKhau) HienDoiMatKhau();
        }
        void HienDoiMatKhau()
        {
            PhienDangNhap.YeuCau("TAI_KHOAN_CA_NHAN");
            if (trangDoiMatKhau == null || trangDoiMatKhau.IsDisposed)
            {
                trangDoiMatKhau = new frmDoiMatKhau(true) { TopLevel = false, FormBorderStyle = FormBorderStyle.None, MinimumSize = Size.Empty, Dock = DockStyle.Fill };
                pnlDoiMatKhau.Controls.Add(trangDoiMatKhau); trangDoiMatKhau.Show();
            }
            trangDoiMatKhau.TaiDuLieu(); trangDoiMatKhau.XoaNoiDung();
        }
        void CheDoThem(bool them)
        {
            dangThem = them; dgvTaiKhoan.Enabled = flpTimKiem.Enabled = !them; btnThemMoi.Enabled = btnNghiViec.Enabled = !them; btnHuyThem.Visible = them;
            txtTenNhanVien.ReadOnly = txtDienThoai.ReadOnly = !them; numLuongThang.Enabled = them;
            grpThongTin.Text = them ? "Tạo tài khoản mới" : "Tài khoản nhân viên"; btnLuuTaiKhoan.Text = them ? "Tạo tài khoản" : "Lưu tài khoản";
        }
        void BatDauThem()
        {
            PhienDangNhap.YeuCau("QUAN_LY_TAI_KHOAN"); CheDoThem(true); maNhanVienDangChon = ""; maTaiKhoanMoi = XuLyDuLieu.TaoMaMoi("NV");
            txtMaTaiKhoan.Text = maTaiKhoanMoi; txtTenNhanVien.Clear(); txtTenDangNhap.Clear(); txtDienThoai.Clear(); cboChucVu.SelectedItem = "Thợ phụ";
            numLuongThang.Value = 6000000; btnLuuTaiKhoan.Enabled = true; txtTenNhanVien.Focus();
        }
        void Loc()
        {
            if (bangTaiKhoan == null || dangThem) return;
            var loc = bangTaiKhoan.Clone();
            foreach (DataRow dong in bangTaiKhoan.Rows)
                if ((dong["TenNhanVien"] + " " + dong["TenDangNhap"] + " " + dong["MaNhanVien"]).IndexOf(txtTimKiem.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0 && (cboLocVaiTro.SelectedIndex <= 0 || dong["TenChucVu"].ToString() == cboLocVaiTro.Text)) loc.ImportRow(dong);
            dgvTaiKhoan.DataSource = loc;
            dgvTaiKhoan.Columns["Luong"].HeaderText = "Lương tháng (đ)"; dgvTaiKhoan.Columns["TinhTrangTaiKhoan"].HeaderText = "Tài khoản"; dgvTaiKhoan.Columns["TrangThai"].HeaderText = "Làm việc";
            ChonDong();
        }
        void ChonDong()
        {
            if (dangThem) return;
            var dong = dgvTaiKhoan.CurrentRow == null ? null : dgvTaiKhoan.CurrentRow.DataBoundItem as DataRowView;
            maNhanVienDangChon = dong == null ? "" : dong["MaNhanVien"].ToString(); txtMaTaiKhoan.Text = maNhanVienDangChon;
            txtTenNhanVien.Text = dong == null ? "" : dong["TenNhanVien"].ToString(); txtTenDangNhap.Text = dong == null ? "" : dong["TenDangNhap"].ToString(); txtDienThoai.Text = dong == null ? "" : dong["SoDienThoai"].ToString();
            numLuongThang.Value = dong == null ? 0 : Math.Min(numLuongThang.Maximum, Convert.ToDecimal(dong["Luong"]));
            cboChucVu.SelectedItem = dong == null ? null : PhanQuyenNguoiDung.ChuanHoa(dong["TenChucVu"].ToString());
            btnLuuTaiKhoan.Enabled = dong != null;
            btnNghiViec.Enabled = dong != null && dong["TrangThai"].ToString() == "Đang làm" && maNhanVienDangChon != PhienDangNhap.MaNhanVien;
        }
        void Luu()
        {
            string ma = maNhanVienDangChon;
            if (dangThem) ma = QuanLyNhanSu.TaoTaiKhoanMoi("",txtTenNhanVien.Text.Trim(),txtTenDangNhap.Text.Trim(),"123",cboChucVu.Text,true,maTaiKhoanMoi,txtDienThoai.Text.Trim(),numLuongThang.Value);
            else QuanLyNhanSu.LuuThongTinTaiKhoan(ma,txtTenDangNhap.Text.Trim(),cboChucVu.Text);
            txtTimKiem.Clear(); cboLocVaiTro.SelectedIndex = 0; TaiDuLieu();
            foreach (DataGridViewRow dong in dgvTaiKhoan.Rows) if (dong.Cells["MaNhanVien"].Value.ToString() == ma) { dgvTaiKhoan.CurrentCell = dong.Cells["TenNhanVien"]; break; }
            ChonDong();
        }
    }
}
