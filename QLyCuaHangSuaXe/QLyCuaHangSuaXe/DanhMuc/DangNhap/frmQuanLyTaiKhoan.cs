using QLyCuaHangSuaXe.LopDungChung;
using System;
using System.Windows.Forms;

namespace QLyCuaHangSuaXe.DanhMuc.DangNhap
{
    public partial class frmQuanLyTaiKhoan : TrangThietKe
    {
        string maNhanVienDangChon = "";
        public frmQuanLyTaiKhoan() : this("Quản lý tài khoản")
        {
        }

        protected frmQuanLyTaiKhoan(string tieuDe)
        {
            InitializeComponent();
            Text = lblTieuDe.Text = tieuDe;
            GanThietKe(dgvTaiKhoan, lblHuongDan);
            dgvTaiKhoan.Columns.Clear();
            dgvTaiKhoan.AutoGenerateColumns = true;
            cboChucVu.Items.Clear();
            cboChucVu.Items.AddRange(PhanQuyenNguoiDung.VaiTro);
            cboTrangThai.Items.AddRange(new object[] { "Đang làm", "Đã nghỉ" });
            cboTrangThai.SelectedIndex = 0;
            cboChucVu.SelectedIndexChanged += (nguoiGui, suKien) => numSoXeToiDa.Value = cboChucVu.Text == "Thợ chính" ? 3 : cboChucVu.Text == "Thợ" ? 2 : 0;
            txtMaTaiKhoan.ReadOnly = true;
            chkTaiKhoanHoatDong.CheckedChanged += (nguoiGui, suKien) => cboTrangThai.SelectedIndex = chkTaiKhoanHoatDong.Checked ? 0 : 1;
            cboTrangThai.SelectedIndexChanged += (nguoiGui, suKien) => chkTaiKhoanHoatDong.Checked = cboTrangThai.SelectedIndex == 0;
            btnPhanQuyen.Click += (nguoiGui, suKien) => GiaoDien.ThucHien(() =>
            {
                using (var trang = new frmPhanQuyen())
                    trang.ShowDialog(this);
            });
            btnPhanCong.Click += (nguoiGui, suKien) => GiaoDien.ThucHien(() =>
            {
                using (var trang = new QLyCuaHangSuaXe.DanhMuc.frmPhanCong())
                    trang.ShowDialog(this);
            });
            btnBangCong.Click += (nguoiGui, suKien) => GiaoDien.ThucHien(() =>
            {
                using (var trang = new QLyCuaHangSuaXe.DanhMuc.frmBangCong())
                    trang.ShowDialog(this);
            });
            btnThemMoi.Click += (nguoiGui, suKien) =>
            {
                maNhanVienDangChon = "";
                txtMaTaiKhoan.Clear();
                txtTenNhanVien.Clear();
                txtTenDangNhap.Clear();
                txtMatKhau.Clear();
                txtDienThoai.Clear();
                cboChucVu.SelectedIndex = -1;
            };
            btnLuuTaiKhoan.Click += (nguoiGui, suKien) => GiaoDien.ThucHien(Luu);
            btnLamMoi.Click += (nguoiGui, suKien) => GiaoDien.ThucHien(TaiDuLieu);
            btnDong.Click += (nguoiGui, suKien) => Close();
            cboLocVaiTro.Items.Clear();
            cboLocVaiTro.Items.Add("Tất cả");
            cboLocVaiTro.Items.AddRange(PhanQuyenNguoiDung.VaiTro);
            cboLocVaiTro.SelectedIndex = 0;
            btnTimKiem.Click += (nguoiGui, suKien) => GiaoDien.ThucHien(() =>
            {
                TaiDuLieu();
                var bang = (System.Data.DataTable)dgvDanhSach.DataSource;
                var loc = bang.Clone();
                foreach (System.Data.DataRow dong in bang.Rows)
                    if ((dong["TenNhanVien"] + " " + dong["TenDangNhap"]).IndexOf(txtTimKiem.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0 && (cboLocVaiTro.SelectedIndex <= 0 || dong["TenChucVu"].ToString() == cboLocVaiTro.Text))
                        loc.ImportRow(dong);
                dgvDanhSach.DataSource = loc;
            });
            dgvDanhSach.SelectionChanged += (nguoiGui, suKien) => ChonDong();
        }

        public override void TaiDuLieu()
        {
            PhienDangNhap.YeuCau("QUAN_LY_TAI_KHOAN");
            dgvDanhSach.DataSource = duLieu.DocBang(@"SELECT n.MaNhanVien,n.TenNhanVien,n.TenDangNhap,v.TenChucVu,n.SoDienThoai,n.TrangThai,n.SoXeDangSuaToiDa,n.LuongGio FROM dbo.NhanVien n JOIN dbo.ChucVu v ON v.MaChucVu=n.MaChucVu ORDER BY n.TenNhanVien");
            lblThongBao.Text = "Danh sách không hiển thị mật khẩu. Để trống mật khẩu mới khi giữ mật khẩu hiện tại.";
        }

        void ChonDong()
        {
            if (dgvDanhSach.CurrentRow == null)
                return;
            maNhanVienDangChon = GiaoDien.GiaTriO(dgvDanhSach, "MaNhanVien");
            txtMaTaiKhoan.Text = maNhanVienDangChon;
            txtTenNhanVien.Text = GiaoDien.GiaTriO(dgvDanhSach, "TenNhanVien");
            txtTenDangNhap.Text = GiaoDien.GiaTriO(dgvDanhSach, "TenDangNhap");
            txtDienThoai.Text = GiaoDien.GiaTriO(dgvDanhSach, "SoDienThoai");
            cboChucVu.SelectedItem = PhanQuyenNguoiDung.ChuanHoa(GiaoDien.GiaTriO(dgvDanhSach, "TenChucVu"));
            cboTrangThai.SelectedItem = GiaoDien.GiaTriO(dgvDanhSach, "TrangThai");
            numSoXeToiDa.Value = Math.Min(Convert.ToDecimal(dgvDanhSach.CurrentRow.Cells["SoXeDangSuaToiDa"].Value), numSoXeToiDa.Maximum);
            numLuongGio.Value = Convert.ToDecimal(dgvDanhSach.CurrentRow.Cells["LuongGio"].Value);
            txtMatKhau.Clear();
        }

        void Luu()
        {
            PhienDangNhap.YeuCau("QUAN_LY_TAI_KHOAN");
            string ten = GiaoDien.BatBuocNhap(txtTenNhanVien, "Họ tên", 100), tenTaiKhoan = GiaoDien.BatBuocNhap(txtTenDangNhap, "Tên đăng nhập", 50);
            if (!System.Text.RegularExpressions.Regex.IsMatch(tenTaiKhoan, @"^[A-Za-z0-9_.-]+$"))
                throw new InvalidOperationException("Tên đăng nhập chỉ dùng chữ không dấu, số, dấu chấm, gạch ngang và gạch dưới.");
            if (cboChucVu.SelectedIndex < 0)
                throw new InvalidOperationException("Chọn một trong 4 chức vụ. Tài khoản vai trò cũ cần chuyển vai trò để đăng nhập.");
            if (txtDienThoai.Text.Length > 0)
                GiaoDien.KiemTraDienThoai(txtDienThoai);
            bool themMoi = maNhanVienDangChon.Length == 0;
            string khoa = themMoi ? XuLyDuLieu.TaoMaMoi("NV") : maNhanVienDangChon;
            if (khoa == PhienDangNhap.MaNhanVien && (cboChucVu.Text != PhienDangNhap.VaiTro || cboTrangThai.Text != "Đang làm"))
                throw new InvalidOperationException("Không được tự khóa hoặc đổi chức vụ tài khoản đang đăng nhập.");
            string boBam = txtMatKhau.Text.Length > 0 ? BaoMat.BamMatKhau(txtMatKhau.Text) : null;
            if (themMoi && boBam == null)
                throw new InvalidOperationException("Tài khoản mới cần mật khẩu.");
            duLieu.TrongGiaoDich<int>((ketNoi, giaoDich) =>
            {
                var dongDuLieu = XuLyDuLieu.DocDuLieu(ketNoi, giaoDich, "SELECT TOP(1) MaChucVu FROM dbo.ChucVu WHERE TenChucVu=@r", XuLyDuLieu.ThamSo("@r", cboChucVu.Text));
                if (dongDuLieu.Rows.Count != 1)
                    throw new InvalidOperationException("Chạy script nâng cấp để tạo 4 chức vụ.");
                if (!themMoi)
                {
                    XuLyDuLieu.DocDuLieu(ketNoi, giaoDich, "SELECT MaNhanVien FROM dbo.NhanVien WITH(UPDLOCK,HOLDLOCK) WHERE MaNhanVien=@id", XuLyDuLieu.ThamSo("@id", khoa));
                    var cacCongViec = XuLyDuLieu.DocDuLieu(ketNoi, giaoDich, "SELECT COUNT(*) FROM dbo.PhanCongTho WITH(UPDLOCK,HOLDLOCK) WHERE MaNhanVien=@id AND TrangThaiCongViec NOT IN(N'Hoàn thành',N'Đã hủy')", XuLyDuLieu.ThamSo("@id", khoa));
                    int soLuong = Convert.ToInt32(cacCongViec.Rows[0][0]);
                    if (soLuong > numSoXeToiDa.Value || (soLuong > 0 && (cboTrangThai.Text != "Đang làm" || (cboChucVu.Text != "Thợ" && cboChucVu.Text != "Thợ chính"))))
                        throw new InvalidOperationException("Nhân viên còn xe đang sửa: không thể khóa, đổi sang chức vụ khác hoặc hạ giới hạn dưới số xe đang nhận.");
                }

                return XuLyDuLieu.ThucThi(ketNoi, giaoDich, themMoi ? "INSERT dbo.NhanVien(MaNhanVien,MaChucVu,TenNhanVien,TenDangNhap,MatKhauHash,SoDienThoai,TrangThai,SoXeDangSuaToiDa,LuongGio) VALUES(@id,@r,@n,@u,@h,@p,@s,@cap,@hour)" : "UPDATE dbo.NhanVien SET MaChucVu=@r,TenNhanVien=@n,TenDangNhap=@u,MatKhauHash=COALESCE(@h,MatKhauHash),SoDienThoai=@p,TrangThai=@s,SoXeDangSuaToiDa=@cap,LuongGio=@hour WHERE MaNhanVien=@id", XuLyDuLieu.ThamSo("@id", khoa), XuLyDuLieu.ThamSo("@r", dongDuLieu.Rows[0][0]), XuLyDuLieu.ThamSo("@n", ten), XuLyDuLieu.ThamSo("@u", tenTaiKhoan), XuLyDuLieu.ThamSo("@h", boBam), XuLyDuLieu.ThamSo("@p", txtDienThoai.Text.Trim()), XuLyDuLieu.ThamSo("@s", cboTrangThai.Text), XuLyDuLieu.ThamSo("@cap", (int)numSoXeToiDa.Value), XuLyDuLieu.ThamSo("@hour", numLuongGio.Value));
            });
            txtMatKhau.Clear();
            TaiDuLieu();
            lblThongBao.Text = "Đã lưu nhân viên " + khoa;
        }
    }
}
