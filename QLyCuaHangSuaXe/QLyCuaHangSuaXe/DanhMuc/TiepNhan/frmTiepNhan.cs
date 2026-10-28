using QLyCuaHangSuaXe.LopDungChung;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace QLyCuaHangSuaXe.DanhMuc.TiepNhan
{
    public partial class frmTiepNhan : TrangThietKe
    {
        readonly NghiepVuSuaXe nghiepVu = new NghiepVuSuaXe();
        List<DichVuChon> dichVuDaKiemTra;
        DataTable bangPhieuGoc, bangDuThao, bangChiTiet;
        bool dangThem, dangSua, dangTai;
        string maPhieuDangChon = "";
        public frmTiepNhan()
        {
            InitializeComponent();
            GanThietKe(dgvPhieu, lblTrangThaiPhieu);
            DinhDangNutHuy(btnXoa);

            cboTrangThai.Items.AddRange(new object[] { "Chờ sửa", "Đang sửa", "Hoàn thành", "Đã hủy" });
            cboTrangThai.SelectedIndexChanged += (nguoiGui, suKien) => DinhDangMauTrangThai();
            DinhDangMauTrangThai();
            cboLocTrangThai.SelectedIndex = 0;
            btnThemMoi.Click += (nguoiGui, suKien) => GiaoDien.ThucHien(TiepNhanMoi);
            btnSua.Click += (nguoiGui, suKien) => GiaoDien.ThucHien(BatDauSua);
            btnXoa.Click += (nguoiGui, suKien) => GiaoDien.ThucHien(HuyPhieu);
            btnLuu.Click += (nguoiGui, suKien) => GiaoDien.ThucHien(Luu);
            btnChiTiet.Click += (nguoiGui, suKien) => GiaoDien.ThucHien(XemChiTiet);
            btnHoaDon.Click += (nguoiGui, suKien) => GiaoDien.ThucHien(() => { using (var trang = new QLyCuaHangSuaXe.DanhMuc.frmHoaDon()) trang.ShowDialog(this); TaiDuLieu(); });
            btnInPhieu.Click += (nguoiGui, suKien) => GiaoDien.ThucHien(InPhieu);
            btnTimKiem.Click += (nguoiGui, suKien) => LocPhieu();
            txtTimKiem.KeyDown += (nguoiGui, suKien) => { if (suKien.KeyCode == Keys.Enter) { LocPhieu(); suKien.SuppressKeyPress = true; } };
            cboLocTrangThai.SelectedIndexChanged += (nguoiGui, suKien) => LocPhieu();
            dgvPhieu.SelectionChanged += (nguoiGui, suKien) => HienThiPhieu();
            dgvPhieu.DataBindingComplete += (nguoiGui, suKien) => SapXepCot();
            dgvPhieu.CellFormatting += DinhDangPhieu;
            txtBienSoXe.Leave += (nguoiGui, suKien) => GiaoDien.ThucHien(TimXeCu);
            KhoaThongTin();
        }

        static void DinhDangNutHuy(Button nut)
        {
            nut.BackColor = nut.FlatAppearance.BorderColor = Color.FromArgb(166, 57, 57);
            nut.ForeColor = Color.White;
            nut.FlatAppearance.MouseOverBackColor = Color.FromArgb(148, 45, 45);
            nut.FlatAppearance.MouseDownBackColor = Color.FromArgb(128, 36, 36);
            nut.UseVisualStyleBackColor = false;
        }

        void DinhDangMauTrangThai()
        {
            string trangThai = cboTrangThai.Text;
            Color nen = Color.FromArgb(235, 243, 251), chu = GiaoDien.Xanh;
            if (trangThai == "Đã hủy") { nen = Color.FromArgb(255, 235, 233); chu = Color.FromArgb(166, 57, 57); }
            lblTrangThaiHen.BackColor = nen;
            lblTrangThaiHen.ForeColor = cboTrangThai.ForeColor = chu;
        }

        public override void TaiDuLieu()
        {
            PhienDangNhap.YeuCau("TIEP_NHAN");
            cboLoaiXe.Items.Clear();
            foreach (DataRow xe in duLieu.DocBang("SELECT DISTINCT LoaiXe FROM dbo.Xe ORDER BY LoaiXe").Rows) cboLoaiXe.Items.Add(xe[0].ToString());
            bangPhieuGoc = duLieu.DocBang(@"SELECT p.MaPhieuTiepNhan,p.MaXe,p.NgayLap AS ThoiGianNhan,p.NgayHen AS HenTra,p.TrangThai,k.TenKhachHang,k.SoDienThoai,x.BienXe AS BienSo,x.LoaiXe,x.HangSanXuat AS DongXe,x.MauSac,
                COALESCE(n.HoTen,N'Chưa phân công') AS TenTho,COALESCE(c.MaNhanVien,'') AS MaTho,COALESCE(h.MaHoaDon,'') AS MaHoaDon,
                (SELECT COUNT(*) FROM dbo.ChiTietPhieuTiepNhan d WHERE d.MaPhieuTiepNhan=p.MaPhieuTiepNhan) AS SoHangMuc,
                COALESCE((SELECT SUM(d.ThanhTien) FROM dbo.ChiTietPhieuTiepNhan d WHERE d.MaPhieuTiepNhan=p.MaPhieuTiepNhan),0) AS TongDuKien
                FROM dbo.PhieuTiepNhan p JOIN dbo.Xe x ON x.MaXe=p.MaXe JOIN dbo.KhachHang k ON k.MaKhachHang=x.MaKhachHang
                OUTER APPLY(SELECT TOP(1) pc.MaNhanVien FROM dbo.ChiTietPhanCong pc JOIN dbo.NhanVien nv ON nv.MaNhanVien=pc.MaNhanVien WHERE pc.MaXe=p.MaXe ORDER BY CASE WHEN nv.MaChucVu='CV002' THEN 0 ELSE 1 END,pc.MaNhanVien) c
                LEFT JOIN dbo.NhanVien n ON n.MaNhanVien=c.MaNhanVien LEFT JOIN dbo.HoaDon h ON h.MaPhieuTiepNhan=p.MaPhieuTiepNhan ORDER BY p.NgayLap DESC,p.MaPhieuTiepNhan DESC");
            bangChiTiet = duLieu.DocBang(@"SELECT c.MaPhieuTiepNhan,N'Dịch vụ' AS Loai,d.TenDichVu AS NoiDung,c.SoLuong,c.ThanhTien/NULLIF(c.SoLuong,0) AS DonGia,c.ThanhTien FROM dbo.ChiTietPhieuTiepNhan c JOIN dbo.DichVu d ON d.MaDichVu=c.MaDichVu");
            LocPhieu();
        }

        void LocPhieu()
        {
            if (bangPhieuGoc == null || dangThem || dangSua) return;
            string tuKhoa = txtTimKiem.Text.Trim(); var ketQua = bangPhieuGoc.Clone();
            foreach (DataRow dong in bangPhieuGoc.Rows)
            {
                bool dungKhachHang = tuKhoa.Length == 0 || dong["TenKhachHang"].ToString().IndexOf(tuKhoa, StringComparison.CurrentCultureIgnoreCase) >= 0
                    || dong["SoDienThoai"].ToString().IndexOf(tuKhoa, StringComparison.Ordinal) >= 0;
                string trangThai = dong["TrangThai"].ToString();
                bool dungTrangThai = cboLocTrangThai.Text == "Tất cả" || cboLocTrangThai.Text == "Chờ sửa" && (trangThai == "Chờ sửa" || trangThai == "Đang sửa")
                    || trangThai == cboLocTrangThai.Text;
                if (dungKhachHang && dungTrangThai) ketQua.ImportRow(dong);
            }
            dangTai = true;
            try
            {
                dgvPhieu.DataSource = ketQua;
                string[] hien = { "MaPhieuTiepNhan", "ThoiGianNhan", "BienSo", "TenKhachHang", "TenTho", "HenTra", "TrangThai" };
                foreach (DataGridViewColumn cot in dgvPhieu.Columns) cot.Visible = hien.Contains(cot.Name);
                SapXepCot();
                string[] ten = { "Mã phiếu", "Ngày nhận", "Biển số xe", "Khách hàng", "Thợ phụ trách", "Hẹn trả", "Trạng thái" };
                for (int i = 0; i < hien.Length; i++) dgvPhieu.Columns[hien[i]].HeaderText = ten[i];
                dgvPhieu.Columns["ThoiGianNhan"].DefaultCellStyle.Format = dgvPhieu.Columns["HenTra"].DefaultCellStyle.Format = "dd/MM/yyyy";
                lblTongPhieu.Text = "Hiển thị: " + ketQua.Rows.Count + " / " + bangPhieuGoc.Rows.Count + " phiếu";
            }
            finally { dangTai = false; }
            HienThiPhieu();
        }

        void SapXepCot()
        {
            string[] cot = { "MaPhieuTiepNhan", "ThoiGianNhan", "BienSo", "TenKhachHang", "TenTho", "HenTra", "TrangThai" };
            for (int i = cot.Length - 1; i >= 0; i--) if (dgvPhieu.Columns.Contains(cot[i])) dgvPhieu.Columns[cot[i]].DisplayIndex = 0;
        }

        void KhoaThongTin()
        {
            txtKhachHang.ReadOnly = txtDienThoai.ReadOnly = txtBienSoXe.ReadOnly = txtDongXe.ReadOnly = txtMauSac.ReadOnly = true;
            cboLoaiXe.Enabled = cboThoPhuTrach.Enabled = dtpHenTra.Enabled = cboTrangThai.Enabled = false;
            btnLuu.Enabled = false;
            btnXoa.Enabled = PhanQuyenNguoiDung.CoQuyen(PhienDangNhap.VaiTro, "NHAN_VIEN");
            btnHoaDon.Enabled = PhanQuyenNguoiDung.CoQuyen(PhienDangNhap.VaiTro, "HOA_DON");
            lblTrangThaiPhieu.Text = "";
        }

        void TiepNhanMoi()
        {
            if ((dangThem || dangSua) && MessageBox.Show(this, "Bỏ thay đổi đang nhập để tạo phiếu mới?", "Phiếu tiếp nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            using (var kiemTra = new frmKiemTraTiepNhan()) if (kiemTra.ShowDialog(this) == DialogResult.OK) NhanKetQuaKiemTra(kiemTra.DichVuDaChon, kiemTra.ChiTietDaChon, "");
        }

        public void NhanKetQuaKiemTra(List<DichVuChon> cacDichVu, DataTable chiTiet, string tinhTrang)
        {
            nghiepVu.KiemTraKho(cacDichVu);
            if (chiTiet == null) throw new InvalidOperationException("Thiếu chi tiết hạng mục đã chọn.");
            dichVuDaKiemTra = cacDichVu.Select(d => new DichVuChon { Ma = d.Ma, SoLuong = d.SoLuong }).ToList(); bangDuThao = chiTiet.Copy();
            dangThem = true; dangSua = false; maPhieuDangChon = "";
            txtKhachHang.Clear(); txtDienThoai.Clear(); txtBienSoXe.Clear(); txtDongXe.Clear(); txtMauSac.Clear();
            txtMaPhieu.Text = XuLyDuLieu.TaoMaMoi("PTN"); txtNgayNhan.Text = DateTime.Today.ToString("dd/MM/yyyy"); dtpHenTra.Value = DateTime.Today.AddDays(1);
            if (cboLoaiXe.Items.Count > 0) cboLoaiXe.SelectedIndex = 0; cboTrangThai.SelectedItem = "Chờ sửa";
            txtKhachHang.ReadOnly = txtDienThoai.ReadOnly = txtBienSoXe.ReadOnly = txtDongXe.ReadOnly = txtMauSac.ReadOnly = false;
            cboLoaiXe.Enabled = dtpHenTra.Enabled = true; cboTrangThai.Enabled = false;
            GiaoDien.GanDuLieu(cboThoPhuTrach, nghiepVu.ThoRanh(), "HienThi", "MaNhanVien"); cboThoPhuTrach.Enabled = true;
            btnLuu.Enabled = btnInPhieu.Enabled = btnChiTiet.Enabled = true;
            btnSua.Enabled = btnXoa.Enabled = false;
            lblHangMuc.Text = "Hạng mục: " + chiTiet.Rows.Count + "  |  Dự kiến: " + chiTiet.AsEnumerable().Sum(d => Convert.ToDecimal(d["ThanhTien"])).ToString("N0") + " đ";
            lblTrangThaiHen.Text = "Phiếu mới"; lblTrangThaiPhieu.Text = ""; txtKhachHang.Focus();
        }

        void HienThiPhieu()
        {
            if (dangTai || dangThem || dangSua) return;
            KhoaThongTin(); btnSua.Enabled = btnInPhieu.Enabled = btnChiTiet.Enabled = dgvPhieu.CurrentRow != null;
            if (dgvPhieu.CurrentRow == null)
            {
                maPhieuDangChon = ""; txtMaPhieu.Text = XuLyDuLieu.TaoMaMoi("PTN"); txtKhachHang.Clear(); txtDienThoai.Clear(); txtBienSoXe.Clear(); txtDongXe.Clear(); txtMauSac.Clear(); txtNgayNhan.Text = DateTime.Today.ToString("dd/MM/yyyy");
                cboThoPhuTrach.DataSource = null; cboThoPhuTrach.Items.Clear(); cboTrangThai.SelectedIndex = -1; cboLoaiXe.SelectedIndex = -1;
                lblHangMuc.Text = "Hạng mục: 0"; lblTrangThaiHen.Text = ""; return;
            }
            var dong = ((DataRowView)dgvPhieu.CurrentRow.DataBoundItem).Row;
            maPhieuDangChon = dong["MaPhieuTiepNhan"].ToString(); txtMaPhieu.Text = maPhieuDangChon; txtNgayNhan.Text = Convert.ToDateTime(dong["ThoiGianNhan"]).ToString("dd/MM/yyyy");
            txtKhachHang.Text = dong["TenKhachHang"].ToString(); txtDienThoai.Text = dong["SoDienThoai"].ToString(); txtBienSoXe.Text = dong["BienSo"].ToString(); txtDongXe.Text = dong["DongXe"].ToString(); txtMauSac.Text = dong["MauSac"].ToString();
            cboLoaiXe.Text = dong["LoaiXe"].ToString(); cboTrangThai.SelectedItem = dong["TrangThai"].ToString(); dtpHenTra.Value = Convert.ToDateTime(dong["HenTra"]);
            cboThoPhuTrach.DataSource = null; cboThoPhuTrach.Items.Clear(); cboThoPhuTrach.Items.Add(dong["TenTho"].ToString()); cboThoPhuTrach.SelectedIndex = 0;
            lblHangMuc.Text = "Hạng mục: " + dong["SoHangMuc"] + "  |  Tổng dự kiến: " + Convert.ToDecimal(dong["TongDuKien"]).ToString("N0") + " đ";
            lblTrangThaiHen.Text = dong["TrangThai"].ToString() == "Hoàn thành" ? "Đã xong" : dong["TrangThai"].ToString() == "Đã hủy" ? "Đã hủy" : LaQuaHen(dong) ? "Quá hẹn" : "Đúng hẹn";
            btnXoa.Enabled = btnXoa.Enabled && dong["TrangThai"].ToString() == "Chờ sửa";
        }

        void TimXeCu()
        {
            if (!dangThem || txtBienSoXe.Text.Trim().Length == 0) return;
            var xe = duLieu.DocBang("SELECT TOP(1) k.TenKhachHang,k.SoDienThoai,x.HangSanXuat,x.LoaiXe,x.MauSac FROM dbo.Xe x JOIN dbo.KhachHang k ON k.MaKhachHang=x.MaKhachHang WHERE x.BienXe=@b ORDER BY x.MaXe DESC", XuLyDuLieu.ThamSo("@b", txtBienSoXe.Text.Trim().ToUpperInvariant()));
            if (xe.Rows.Count == 0) return;
            txtKhachHang.Text = xe.Rows[0][0].ToString(); txtDienThoai.Text = xe.Rows[0][1].ToString(); txtDongXe.Text = xe.Rows[0][2].ToString(); cboLoaiXe.Text = xe.Rows[0][3].ToString(); txtMauSac.Text = xe.Rows[0][4].ToString();
        }

        void Luu()
        {
            if (dangSua)
            {
                var dong = ((DataRowView)dgvPhieu.CurrentRow.DataBoundItem).Row;
                nghiepVu.SuaPhieu(maPhieuDangChon, dtpHenTra.Value.Date, cboTrangThai.Text, dong["MaTho"].ToString());
                dangSua = false; TaiDuLieu(); lblTrangThaiPhieu.Text = "Đã cập nhật " + maPhieuDangChon + "."; return;
            }
            if (!dangThem || dichVuDaKiemTra == null) throw new InvalidOperationException("Chọn dịch vụ và kiểm tra phụ tùng trước khi lưu phiếu.");
            var thongTin = new ThongTinNhanXe { TenKhach = GiaoDien.BatBuocNhap(txtKhachHang, "Tên khách", 100), DienThoai = GiaoDien.KiemTraDienThoai(txtDienThoai), BienSo = GiaoDien.BatBuocNhap(txtBienSoXe, "Biển số", 20).ToUpperInvariant(), DongXe = GiaoDien.BatBuocNhap(txtDongXe, "Hãng sản xuất", 50), MauSac = GiaoDien.BatBuocNhap(txtMauSac, "Màu sắc", 30), LoaiXe = cboLoaiXe.Text, MaPhieu = txtMaPhieu.Text, MaTho = GiaoDien.GiaTriDangChon(cboThoPhuTrach), HenTra = dtpHenTra.Value.Date };
            string ma = nghiepVu.TiepNhan(dichVuDaKiemTra, thongTin); dangThem = false; dichVuDaKiemTra = null; bangDuThao = null; TaiDuLieu(); lblTrangThaiPhieu.Text = "Đã lưu " + ma + ".";
        }

        void BatDauSua()
        {
            if (dangThem || dgvPhieu.CurrentRow == null) throw new InvalidOperationException("Chọn phiếu đã lưu trước.");
            var dong = ((DataRowView)dgvPhieu.CurrentRow.DataBoundItem).Row;
            if (dong["TrangThai"].ToString() == "Hoàn thành" || dong["TrangThai"].ToString() == "Đã hủy") throw new InvalidOperationException("Phiếu đã hoàn thành hoặc đã hủy nên không thể sửa.");
            dangSua = true; dtpHenTra.Enabled = true; cboTrangThai.Enabled = PhanQuyenNguoiDung.CoQuyen(PhienDangNhap.VaiTro, "PHAN_CONG"); btnLuu.Enabled = true; btnThemMoi.Enabled = true;
        }

        void HuyPhieu()
        {
            if (maPhieuDangChon.Length == 0 || dangThem) throw new InvalidOperationException("Chọn phiếu đã lưu cần hủy.");
            if (MessageBox.Show(this, "Hủy phiếu chưa sửa và hoàn lại phụ tùng?", "Hủy phiếu", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            nghiepVu.HuyChuaSua(maPhieuDangChon); TaiDuLieu();
        }
        DataTable LayChiTiet()
        {
            if (dangThem) return bangDuThao.Copy();
            if (maPhieuDangChon.Length == 0 || bangChiTiet == null) throw new InvalidOperationException("Chọn phiếu cần xem.");
            var ketQua = bangChiTiet.Clone(); foreach (DataRow dong in bangChiTiet.Rows) if (dong["MaPhieuTiepNhan"].ToString() == maPhieuDangChon) ketQua.ImportRow(dong); ketQua.Columns.Remove("MaPhieuTiepNhan"); return ketQua;
        }
        void XemChiTiet() { using (var trang = new frmChiTietPhieu(txtMaPhieu.Text, LayChiTiet())) trang.ShowDialog(this); }
        void InPhieu()
        {
            var phieu = new MauInPhieu(txtMaPhieu.Text, LayChiTiet(), "Khách hàng: " + txtKhachHang.Text + "    •    Điện thoại: " + txtDienThoai.Text, "Biển số: " + txtBienSoXe.Text + "    •    Loại xe: " + cboLoaiXe.Text, "Hãng sản xuất: " + txtDongXe.Text + "    •    Màu sắc: " + txtMauSac.Text, "Ngày nhận: " + txtNgayNhan.Text + "    •    Hẹn trả: " + dtpHenTra.Value.ToString("dd/MM/yyyy"), "Thợ phụ trách: " + cboThoPhuTrach.Text, dangThem ? "BẢN DỰ THẢO" : "Trạng thái: " + cboTrangThai.Text);
            using (var trang = new frmXemBanInPhieu(phieu)) trang.ShowDialog(this);
        }
        static bool LaQuaHen(DataRow dong)
        {
            string trangThai = dong["TrangThai"].ToString();
            return (trangThai == "Chờ sửa" || trangThai == "Đang sửa") && Convert.ToDateTime(dong["HenTra"]).Date < DateTime.Today;
        }

        void DinhDangPhieu(object nguoiGui, DataGridViewCellFormattingEventArgs suKien)
        {
            if (suKien.RowIndex < 0 || dgvPhieu.Rows[suKien.RowIndex].DataBoundItem == null) return;
            var dong = ((DataRowView)dgvPhieu.Rows[suKien.RowIndex].DataBoundItem).Row;
            if (dong["TrangThai"].ToString() != "Đã hủy") return;
            suKien.CellStyle.BackColor = Color.FromArgb(255, 235, 233);
            suKien.CellStyle.ForeColor = suKien.CellStyle.SelectionForeColor = Color.FromArgb(166, 57, 57);
            suKien.CellStyle.SelectionBackColor = Color.FromArgb(247, 205, 200);
        }
    }

}
