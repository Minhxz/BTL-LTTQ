using QLyCuaHangSuaXe.LopDungChung;
using System;
using System.Data;
using System.Windows.Forms;

namespace QLyCuaHangSuaXe.DanhMuc
{
    public partial class frmNhanVien : TrangThietKe
    {
        DataTable bangNhanVien;
        bool dangThem, daTaiTrang;
        readonly bool moBangCongKhiMo;
        string trangThaiDangChon = "Đang làm", vaiTroBangCong;
        frmBangCong trangBangCong;

        public frmNhanVien() : this(false) { }
        public frmNhanVien(bool moBangCong)
        {
            InitializeComponent();
            moBangCongKhiMo = moBangCong;
            Text = "Quản lý nhân viên";
            GanThietKe(dgvNhanVien);
            numGioiHanXe.Enabled = false;
            cboChucVu.Items.AddRange(PhanQuyenNguoiDung.VaiTro);
            cboChucVu.SelectedIndexChanged += (nguoiGui, suKien) => numGioiHanXe.Value = PhanQuyenNguoiDung.GioiHanXe(cboChucVu.Text);
            btnThemMoi.Click += (nguoiGui, suKien) => GiaoDien.ThucHien(ThemMoi);
            btnLuu.Click += (nguoiGui, suKien) => GiaoDien.ThucHien(Luu);
            btnNghiViec.Click += (nguoiGui, suKien) => GiaoDien.ThucHien(NghiViec);
            btnTaiLai.Text = "Làm mới";
            btnTaiLai.Click += (nguoiGui, suKien) => GiaoDien.ThucHien(() => { TaiDuLieu(); ThemMoi(); });
            btnTimKiem.Click += (nguoiGui, suKien) => Loc();
            dgvNhanVien.CurrentCellChanged += (nguoiGui, suKien) => ChonDong();
            btnPhanCong.Click += (nguoiGui, suKien) => GiaoDien.ThucHien(() => { using (var trang = new frmPhanCong()) trang.ShowDialog(this); });
            btnBangCong.Click += (nguoiGui, suKien) => GiaoDien.ThucHien(() => { tabNhanSu.SelectedTab = tabBangCong; HienBangCong(); });
            tabNhanSu.SelectedIndexChanged += (nguoiGui, suKien) =>
            {
                if (tabNhanSu.SelectedTab == tabBangCong && !DesignMode && System.ComponentModel.LicenseManager.UsageMode != System.ComponentModel.LicenseUsageMode.Designtime)
                    GiaoDien.ThucHien(HienBangCong);
            };
        }

        public override void TaiDuLieu()
        {
            bool quanLy = PhanQuyenNguoiDung.CoQuyen(PhienDangNhap.VaiTro, "NHAN_VIEN");
            if (!quanLy)
            {
                PhienDangNhap.YeuCau("CONG_CA_NHAN");
                tabBangCong.Text = "Bảng công của tôi";
                if (tabNhanSu.TabPages.Contains(tabNhanVien))
                    tabNhanSu.TabPages.Remove(tabNhanVien);
                tabNhanSu.SelectedTab = tabBangCong;
                HienBangCong();
                return;
            }
            PhienDangNhap.YeuCau("NHAN_VIEN");
            if (!tabNhanSu.TabPages.Contains(tabNhanVien))
                tabNhanSu.TabPages.Insert(0, tabNhanVien);
            tabBangCong.Text = "Bảng công và lương";
            dangThem = false;
            bangNhanVien = duLieu.DocBang(@"SELECT n.MaNhanVien,n.HoTen AS TenNhanVien,n.SoDienThoai,v.TenChucVu,CASE WHEN n.TrangThai=1 THEN N'Đang làm' ELSE N'Nghỉ việc' END AS TrangThai,
                CASE WHEN v.TenChucVu IN(N'Thợ chính',N'Kỹ thuật viên chính') THEN 3 WHEN v.TenChucVu IN(N'Thợ phụ',N'Kỹ thuật viên') THEN 2 ELSE 0 END AS SoXeDangSuaToiDa,n.Luong AS LuongGio,
                (SELECT COUNT(DISTINCT p.MaXe) FROM dbo.ChiTietPhanCong p JOIN dbo.PhieuTiepNhan t ON t.MaXe=p.MaXe WHERE p.MaNhanVien=n.MaNhanVien AND t.TrangThai IN(N'Chờ sửa',N'Đang sửa')) AS SoXeDangNhan
                FROM dbo.NhanVien n JOIN dbo.ChucVu v ON v.MaChucVu=n.MaChucVu ORDER BY n.HoTen");
            Loc();
            if (!daTaiTrang && moBangCongKhiMo)
                tabNhanSu.SelectedTab = tabBangCong;
            daTaiTrang = true;
            if (tabNhanSu.SelectedTab == tabBangCong)
                HienBangCong();
        }

        void HienBangCong()
        {
            PhienDangNhap.YeuCau(PhanQuyenNguoiDung.CoQuyen(PhienDangNhap.VaiTro, "BANG_CONG") ? "BANG_CONG" : "CONG_CA_NHAN");
            if (trangBangCong == null || trangBangCong.IsDisposed || vaiTroBangCong != PhienDangNhap.VaiTro)
            {
                if (trangBangCong != null)
                    trangBangCong.Dispose();
                vaiTroBangCong = PhienDangNhap.VaiTro;
                trangBangCong = new frmBangCong { TopLevel = false, FormBorderStyle = FormBorderStyle.None, MinimumSize = System.Drawing.Size.Empty, Dock = DockStyle.Fill };
                pnlBangCong.Controls.Add(trangBangCong);
                trangBangCong.Show();
            }
            trangBangCong.TaiDuLieu();
        }

        void ThemMoi()
        {
            PhienDangNhap.YeuCau("NHAN_VIEN");
            dangThem = true;
            trangThaiDangChon = "Đang làm";
            txtMaNhanVien.Text = XuLyDuLieu.TaoMaMoi("NV");
            txtHoTen.Clear();
            txtDienThoai.Clear();
            cboChucVu.SelectedItem = "Thợ phụ";
            numGioiHanXe.Value = 2;
            numLuongGio.Value = 6000000;
            btnNghiViec.Enabled = false;
            txtHoTen.Focus();
        }

        void Loc()
        {
            if (bangNhanVien == null)
                return;
            dangThem = false;
            var loc = bangNhanVien.Clone();
            foreach (DataRow dong in bangNhanVien.Rows)
                if ((dong["MaNhanVien"] + " " + dong["TenNhanVien"] + " " + dong["SoDienThoai"]).IndexOf(txtTimKiem.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0)
                    loc.ImportRow(dong);
            dgvNhanVien.DataSource = loc;
            dgvNhanVien.Columns["SoXeDangNhan"].HeaderText = "Xe đang nhận";
            dgvNhanVien.Columns["LuongGio"].HeaderText = "Lương tháng (đ)";
            dgvNhanVien.Columns["TrangThai"].HeaderText = "Trạng thái";
            ChonDong();
        }

        void ChonDong()
        {
            if (dangThem)
                return;
            var dong = dgvNhanVien.CurrentRow == null ? null : dgvNhanVien.CurrentRow.DataBoundItem as DataRowView;
            txtMaNhanVien.Text = dong == null ? "" : dong["MaNhanVien"].ToString();
            txtHoTen.Text = dong == null ? "" : dong["TenNhanVien"].ToString();
            txtDienThoai.Text = dong == null ? "" : dong["SoDienThoai"].ToString();
            cboChucVu.SelectedItem = dong == null ? null : PhanQuyenNguoiDung.ChuanHoa(dong["TenChucVu"].ToString());
            trangThaiDangChon = dong == null ? "Đang làm" : dong["TrangThai"].ToString();
            numGioiHanXe.Value = dong == null ? 0 : Math.Min(numGioiHanXe.Maximum, Convert.ToDecimal(dong["SoXeDangSuaToiDa"]));
            numLuongGio.Value = dong == null ? 0 : Math.Min(numLuongGio.Maximum, Convert.ToDecimal(dong["LuongGio"]));
            btnNghiViec.Enabled = dong != null && trangThaiDangChon == "Đang làm" && txtMaNhanVien.Text != PhienDangNhap.MaNhanVien;
        }

        void Luu()
        {
            if (!dangThem && txtMaNhanVien.Text.Length == 0)
                throw new InvalidOperationException("Chọn nhân viên hoặc bấm Thêm nhân viên.");
            QuanLyNhanSu.LuuNhanVien(new ThongTinNhanVien { Ma = txtMaNhanVien.Text, TaoMoi = dangThem, HoTen = txtHoTen.Text.Trim(), DienThoai = txtDienThoai.Text.Trim(), ChucVu = cboChucVu.Text, TrangThai = trangThaiDangChon, GioiHanXe = (int)numGioiHanXe.Value, LuongGio = numLuongGio.Value });
            TaiDuLieu();
        }

        void NghiViec()
        {
            if (dangThem || txtMaNhanVien.Text.Length == 0)
                throw new InvalidOperationException("Chọn nhân viên cần chuyển sang nghỉ việc.");
            if (MessageBox.Show(this, "Cho nhân viên " + txtHoTen.Text + " nghỉ việc và khóa đăng nhập?", "Xác nhận nghỉ việc", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                return;
            QuanLyNhanSu.NghiViecNhanVien(txtMaNhanVien.Text);
            TaiDuLieu();
        }
    }
}
