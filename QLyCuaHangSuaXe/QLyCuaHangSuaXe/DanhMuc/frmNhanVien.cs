using QLyCuaHangSuaXe.LopDungChung;
using System;
using System.Data;
using System.Windows.Forms;

namespace QLyCuaHangSuaXe.DanhMuc
{
    public partial class frmNhanVien : TrangThietKe
    {
        DataTable bangNhanVien;
        bool dangThem;

        public frmNhanVien()
        {
            InitializeComponent();
            Text = "Quản lý nhân viên";
            GanThietKe(dgvNhanVien, lblHuongDan);
            cboChucVu.Items.AddRange(PhanQuyenNguoiDung.VaiTro);
            cboTrangThai.Items.AddRange(new object[] { "Đang làm", "Đã nghỉ" });
            cboTrangThai.SelectedIndex = 0;
            cboChucVu.SelectedIndexChanged += (nguoiGui, suKien) => numGioiHanXe.Value = cboChucVu.Text == "Thợ chính" ? 3 : cboChucVu.Text == "Thợ" ? 2 : 0;
            btnThemMoi.Click += (nguoiGui, suKien) =>
            {
                dangThem = true;
                txtMaNhanVien.Clear();
                txtHoTen.Clear();
                txtDienThoai.Clear();
                cboChucVu.SelectedItem = "Thợ";
                cboTrangThai.SelectedItem = "Đang làm";
                numGioiHanXe.Value = 2;
                numLuongGio.Value = 0;
                txtHoTen.Focus();
                lblHuongDan.Text = "Nhân viên mới chưa có tài khoản. Cấp đăng nhập ở trang Tài khoản sau khi lưu.";
            };
            btnLuu.Click += (nguoiGui, suKien) => GiaoDien.ThucHien(Luu);
            btnTaiLai.Click += (nguoiGui, suKien) => GiaoDien.ThucHien(TaiDuLieu);
            btnTimKiem.Click += (nguoiGui, suKien) => Loc();
            dgvNhanVien.SelectionChanged += (nguoiGui, suKien) => ChonDong();
            btnPhanCong.Click += (nguoiGui, suKien) => GiaoDien.ThucHien(() => { using (var trang = new frmPhanCong()) trang.ShowDialog(this); });
            btnBangCong.Click += (nguoiGui, suKien) => GiaoDien.ThucHien(() => { using (var trang = new frmBangCong()) trang.ShowDialog(this); });
        }

        public override void TaiDuLieu()
        {
            PhienDangNhap.YeuCau("NHAN_VIEN");
            dangThem = false;
            bangNhanVien = duLieu.DocBang(@"SELECT n.MaNhanVien,n.TenNhanVien,n.SoDienThoai,v.TenChucVu,n.TrangThai,n.SoXeDangSuaToiDa,n.LuongGio,
                (SELECT COUNT(*) FROM dbo.PhanCongTho p WHERE p.MaNhanVien=n.MaNhanVien AND p.TrangThaiCongViec NOT IN(N'Hoàn thành',N'Đã hủy')) AS SoXeDangNhan
                FROM dbo.NhanVien n JOIN dbo.ChucVu v ON v.MaChucVu=n.MaChucVu ORDER BY n.TenNhanVien");
            Loc();
            lblHuongDan.Text = "Hồ sơ, lương giờ và giới hạn nhận xe. Thợ mặc định 2 xe; thợ chính 3 xe.";
        }

        void Loc()
        {
            if (bangNhanVien == null) return;
            dangThem = false;
            var loc = bangNhanVien.Clone();
            foreach (DataRow dong in bangNhanVien.Rows)
                if ((dong["MaNhanVien"] + " " + dong["TenNhanVien"] + " " + dong["SoDienThoai"]).IndexOf(txtTimKiem.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0) loc.ImportRow(dong);
            dgvNhanVien.DataSource = loc;
            dgvNhanVien.Columns["SoXeDangNhan"].HeaderText = "Xe đang nhận";
            ChonDong();
        }

        void ChonDong()
        {
            if (dangThem) return;
            var dong = dgvNhanVien.CurrentRow == null ? null : dgvNhanVien.CurrentRow.DataBoundItem as DataRowView;
            txtMaNhanVien.Text = dong == null ? "" : dong["MaNhanVien"].ToString();
            txtHoTen.Text = dong == null ? "" : dong["TenNhanVien"].ToString();
            txtDienThoai.Text = dong == null ? "" : dong["SoDienThoai"].ToString();
            cboChucVu.SelectedItem = dong == null ? null : PhanQuyenNguoiDung.ChuanHoa(dong["TenChucVu"].ToString());
            cboTrangThai.SelectedItem = dong == null ? null : dong["TrangThai"].ToString();
            numGioiHanXe.Value = dong == null ? 0 : Math.Min(numGioiHanXe.Maximum, Convert.ToDecimal(dong["SoXeDangSuaToiDa"]));
            numLuongGio.Value = dong == null ? 0 : Math.Min(numLuongGio.Maximum, Convert.ToDecimal(dong["LuongGio"]));
        }

        void Luu()
        {
            if (!dangThem && txtMaNhanVien.Text.Length == 0) throw new InvalidOperationException("Chọn nhân viên hoặc bấm Thêm nhân viên.");
            string ma = QuanLyNhanSu.LuuNhanVien(new ThongTinNhanVien { Ma = dangThem ? null : txtMaNhanVien.Text, HoTen = txtHoTen.Text.Trim(), DienThoai = txtDienThoai.Text.Trim(), ChucVu = cboChucVu.Text, TrangThai = cboTrangThai.Text, GioiHanXe = (int)numGioiHanXe.Value, LuongGio = numLuongGio.Value });
            TaiDuLieu();
            lblHuongDan.Text = "Đã lưu nhân viên " + ma + ". Quản lý đăng nhập tại trang Tài khoản.";
        }
    }
}
