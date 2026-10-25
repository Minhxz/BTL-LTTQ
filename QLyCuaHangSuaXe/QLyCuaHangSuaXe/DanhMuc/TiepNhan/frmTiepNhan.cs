using QLyCuaHangSuaXe.LopDungChung;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QLyCuaHangSuaXe.DanhMuc.TiepNhan
{
    public partial class frmTiepNhan : TrangThietKe
    {
        readonly NghiepVuSuaXe nghiepVu = new NghiepVuSuaXe();
        List<DichVuChon> dichVuDaKiemTra;
        DataTable bangPhieuGoc, bangDuThao;
        bool dangThem, dangSua, dangTai;
        string maPhieuDangChon = "";
        public frmTiepNhan()
        {
            InitializeComponent();
            GanThietKe(dgvPhieu, lblThongBao);
            lblHangMuc.ForeColor = lblDanhSachPhieu.ForeColor = lblTrangThaiHen.ForeColor = GiaoDien.Xanh;
            lblTrangThaiHen.BackColor = Color.FromArgb(235, 243, 251);
            dgvPhieu.Columns.Clear();
            dgvPhieu.AutoGenerateColumns = true;
            cboLoaiXe.Items.Clear();
            cboLoaiXe.Items.AddRange(new object[] { "Xe số", "Xe ga", "Xe côn tay", "Xe máy", "Ô tô" });
            cboTrangThai.Items.Clear();
            cboTrangThai.Items.AddRange(new object[] { "Chờ sửa", "Đang sửa", "Chờ thanh toán", "Hoàn thành", "Đã hủy" });
            cboLocTrangThai.Items.Clear();
            cboLocTrangThai.Items.Add("Tất cả");
            cboLocTrangThai.Items.AddRange(cboTrangThai.Items.Cast<object>().ToArray());
            cboLocTrangThai.SelectedIndex = 0;
            txtMaPhieu.ReadOnly = txtNgayNhan.ReadOnly = true;
            btnThemMoi.Click += (nguoiGui, suKien) => GiaoDien.ThucHien(TiepNhanMoi);
            btnSua.Click += (nguoiGui, suKien) => GiaoDien.ThucHien(BatDauSua);
            btnXoa.Click += (nguoiGui, suKien) => GiaoDien.ThucHien(HuyPhieu);
            btnLuu.Click += (nguoiGui, suKien) => GiaoDien.ThucHien(Luu);
            btnHuySua.Click += (nguoiGui, suKien) => HuyThaoTac();
            btnChiTiet.Click += (nguoiGui, suKien) => GiaoDien.ThucHien(XemChiTiet);
            btnHoaDon.Click += (nguoiGui, suKien) => GiaoDien.ThucHien(() =>
            {
                using (var trang = new QLyCuaHangSuaXe.DanhMuc.frmHoaDon())
                    trang.ShowDialog(this);
                TaiDuLieu();
            });
            btnInPhieu.Click += (nguoiGui, suKien) => GiaoDien.ThucHien(InPhieu);
            btnThoat.Click += (nguoiGui, suKien) => Close();
            btnTimKiem.Click += (nguoiGui, suKien) => LocPhieu();
            cboLocTrangThai.SelectedIndexChanged += (nguoiGui, suKien) => LocPhieu();
            dgvPhieu.SelectionChanged += (nguoiGui, suKien) => HienThiPhieu();
            dgvPhieu.DataBindingComplete += (nguoiGui, suKien) => SapXepCot();
            dgvPhieu.CellFormatting += DinhDangPhieu;
            txtBienSoXe.Leave += (nguoiGui, suKien) => GiaoDien.ThucHien(TimXeCu);
            KhoaThongTin();
        }

        public override void TaiDuLieu()
        {
            PhienDangNhap.YeuCau("TIEP_NHAN");
            bangPhieuGoc = duLieu.DocBang(@"SELECT p.*,k.TenKhachHang,k.SoDienThoai,x.BienSo,x.LoaiXe,x.DongXe,
                COALESCE(n.TenNhanVien,N'Chưa phân công') AS TenTho,c.MaNhanVien AS MaTho,h.TrangThaiThanhToan,
                (SELECT COUNT(*) FROM dbo.ChiTietPhieuTiepNhan d WHERE d.MaPhieuTiepNhan=p.MaPhieuTiepNhan) AS SoHangMuc,
                COALESCE((SELECT SUM(d.SoLuong*d.DonGiaDichVu) FROM dbo.ChiTietPhieuTiepNhan d WHERE d.MaPhieuTiepNhan=p.MaPhieuTiepNhan),0)
                +COALESCE((SELECT SUM(t.SoLuong*t.DonGia) FROM dbo.PhuTungTiepNhan t WHERE t.MaPhieuTiepNhan=p.MaPhieuTiepNhan),0) AS TongDuKien
                FROM dbo.PhieuTiepNhan p JOIN dbo.KhachHang k ON k.MaKhachHang=p.MaKhachHang JOIN dbo.Xe x ON x.MaXe=p.MaXe
                OUTER APPLY(SELECT TOP(1) pc.MaNhanVien FROM dbo.PhanCongTho pc WHERE pc.MaPhieuTiepNhan=p.MaPhieuTiepNhan ORDER BY CASE WHEN pc.VaiTro=N'Phụ trách' THEN 0 WHEN pc.VaiTro=N'Thợ chính' THEN 1 ELSE 2 END,pc.MaNhanVien) c
                LEFT JOIN dbo.NhanVien n ON n.MaNhanVien=c.MaNhanVien LEFT JOIN dbo.HoaDon h ON h.MaPhieuTiepNhan=p.MaPhieuTiepNhan ORDER BY p.ThoiGianNhan DESC");
            bangPhieuGoc.Columns.Add("TrangThaiHienThi");
            bangPhieuGoc.Columns.Add("NhacHen");
            foreach (DataRow dong in bangPhieuGoc.Rows)
            {
                string trangThai = dong["TrangThai"].ToString();
                bool choThu = dong["TrangThaiThanhToan"].ToString() == "Chưa thanh toán";
                dong["TrangThaiHienThi"] = trangThai == "Mới tiếp nhận" ? "Chờ sửa" : trangThai == "Hoàn thành" && choThu ? "Chờ thanh toán" : trangThai;
                dong["NhacHen"] = trangThai == "Đã hủy" ? "Đã hủy" : trangThai == "Hoàn thành" && !choThu ? "Đã xong" : dong["HenTra"] != DBNull.Value && Convert.ToDateTime(dong["HenTra"]) < DateTime.Now ? "Quá hẹn" : "Đúng hẹn";
            }

            LocPhieu();
        }

        void LocPhieu()
        {
            if (bangPhieuGoc == null)
                return;
            string tuKhoa = txtTimKiem.Text.Trim();
            var ketQua = bangPhieuGoc.Clone();
            foreach (DataRow dong in bangPhieuGoc.Rows)
                if ((tuKhoa.Length == 0 || (dong["TenKhachHang"] + " " + dong["BienSo"] + " " + dong["MaPhieuTiepNhan"]).IndexOf(tuKhoa, StringComparison.CurrentCultureIgnoreCase) >= 0) && (cboLocTrangThai.SelectedIndex <= 0 || dong["TrangThaiHienThi"].ToString() == cboLocTrangThai.Text))
                    ketQua.ImportRow(dong);
            dangTai = true;
            try
            {
                dgvPhieu.DataSource = ketQua;
                string[] cotHien =
                {
                    "MaPhieuTiepNhan",
                    "ThoiGianNhan",
                    "BienSo",
                    "TenKhachHang",
                    "TenTho",
                    "HenTra",
                    "TrangThaiHienThi",
                    "NhacHen"
                };
                foreach (DataGridViewColumn cot in dgvPhieu.Columns)
                    cot.Visible = cotHien.Contains(cot.Name);
                SapXepCot();
                dgvPhieu.Columns["TenTho"].HeaderText = "Thợ phụ trách";
                dgvPhieu.Columns["TrangThaiHienThi"].HeaderText = "Trạng thái";
                dgvPhieu.Columns["NhacHen"].HeaderText = "Nhắc hẹn";
                lblTongPhieu.Text = "Hiển thị: " + ketQua.Rows.Count + " / " + bangPhieuGoc.Rows.Count + " phiếu";
            }
            finally
            {
                dangTai = false;
            }

            HienThiPhieu();
        }

        void KhoaThongTin()
        {
            tlpThongTin.Enabled = tlpTinhTrangXe.Enabled = grbKhachHangXe.Enabled = true;
            txtKhachHang.ReadOnly = txtDienThoai.ReadOnly = txtBienSoXe.ReadOnly = txtDongXe.ReadOnly = rtbTinhTrangXe.ReadOnly = true;
            cboLoaiXe.Enabled = cboThoPhuTrach.Enabled = numSoKm.Enabled = dtpHenTra.Enabled = false;
            btnLuu.Enabled = btnHuySua.Enabled = false;
            cboTrangThai.Enabled = false;
            btnXoa.Enabled = PhanQuyenNguoiDung.CoQuyen(PhienDangNhap.VaiTro, "NHAN_VIEN");
            lblThongBao.Text = "Bấm Tiếp nhận mới để chọn dịch vụ và kiểm tra phụ tùng trước.";
        }

        void SapXepCot()
        {
            string[] cacCot =
            {
                "MaPhieuTiepNhan",
                "ThoiGianNhan",
                "BienSo",
                "TenKhachHang",
                "TenTho",
                "HenTra",
                "TrangThaiHienThi",
                "NhacHen"
            };
            for (int viTri = cacCot.Length - 1; viTri >= 0; viTri--)
                if (dgvPhieu.Columns.Contains(cacCot[viTri]))
                    dgvPhieu.Columns[cacCot[viTri]].DisplayIndex = 0;
        }

        void TiepNhanMoi()
        {
            using (var kiemTra = new frmKiemTraTiepNhan())
                if (kiemTra.ShowDialog(this) == DialogResult.OK)
                    NhanKetQuaKiemTra(kiemTra.DichVuDaChon, kiemTra.ChiTietDaChon, kiemTra.TinhTrangXe);
        }

        public void NhanKetQuaKiemTra(List<DichVuChon> cacDichVu, DataTable chiTiet, string tinhTrang)
        {
            nghiepVu.KiemTraKho(cacDichVu);
            if (chiTiet == null)
                throw new InvalidOperationException("Thiếu chi tiết hạng mục đã chọn.");
            dichVuDaKiemTra = cacDichVu.Select(dichVu => new DichVuChon { Ma = dichVu.Ma, SoLuong = dichVu.SoLuong }).ToList();
            bangDuThao = chiTiet.Copy();
            dangThem = true;
            dangSua = false;
            maPhieuDangChon = "";
            txtKhachHang.Clear();
            txtDienThoai.Clear();
            txtBienSoXe.Clear();
            txtDongXe.Clear();
            numSoKm.Value = 0;
            txtMaPhieu.Text = "Tự động khi lưu";
            txtNgayNhan.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            dtpHenTra.Value = DateTime.Now.AddHours(2);
            cboLoaiXe.SelectedIndex = 0;
            cboTrangThai.SelectedItem = "Chờ sửa";
            rtbTinhTrangXe.Text = tinhTrang;
            tlpThongTin.Enabled = tlpTinhTrangXe.Enabled = grbKhachHangXe.Enabled = true;
            cboTrangThai.Enabled = false;
            txtKhachHang.ReadOnly = txtDienThoai.ReadOnly = txtBienSoXe.ReadOnly = txtDongXe.ReadOnly = rtbTinhTrangXe.ReadOnly = false;
            cboLoaiXe.Enabled = numSoKm.Enabled = dtpHenTra.Enabled = true;
            GiaoDien.GanDuLieu(cboThoPhuTrach, nghiepVu.ThoRanh(), "HienThi", "MaNhanVien");
            cboThoPhuTrach.Enabled = true;
            btnLuu.Enabled = btnHuySua.Enabled = true;
            lblHangMuc.Text = "Hạng mục đã kiểm tra: " + chiTiet.Rows.Count + "  |  Dự kiến: " + chiTiet.AsEnumerable().Sum(dong => Convert.ToDecimal(dong["ThanhTien"])).ToString("N0") + " đ";
            lblTrangThaiHen.Text = "Nhập khách hàng, xe và chọn thợ còn rảnh.";
            lblThongBao.Text = "Đã kiểm tra phụ tùng. Nhập thông tin xe rồi Lưu phiếu; tồn kho và thợ được kiểm tra lại khi lưu.";
            txtKhachHang.Focus();
        }

        void HienThiPhieu()
        {
            if (dangTai || dangThem || dangSua || dgvPhieu.CurrentRow == null)
                return;
            var dong = ((DataRowView)dgvPhieu.CurrentRow.DataBoundItem).Row;
            maPhieuDangChon = dong["MaPhieuTiepNhan"].ToString();
            txtMaPhieu.Text = maPhieuDangChon;
            txtNgayNhan.Text = Convert.ToDateTime(dong["ThoiGianNhan"]).ToString("dd/MM/yyyy HH:mm");
            txtKhachHang.Text = dong["TenKhachHang"].ToString();
            txtDienThoai.Text = dong["SoDienThoai"].ToString();
            txtBienSoXe.Text = dong["BienSo"].ToString();
            txtDongXe.Text = dong["DongXe"].ToString();
            cboLoaiXe.SelectedItem = dong["LoaiXe"].ToString();
            cboTrangThai.SelectedItem = dong["TrangThaiHienThi"].ToString();
            numSoKm.Value = dong.Table.Columns.Contains("SoKm") && dong["SoKm"] != DBNull.Value ? Math.Min(Convert.ToDecimal(dong["SoKm"]), numSoKm.Maximum) : 0;
            dtpHenTra.Value = dong["HenTra"] == DBNull.Value ? DateTime.Now.AddHours(2) : Convert.ToDateTime(dong["HenTra"]);
            rtbTinhTrangXe.Text = dong["MoTaTinhTrangXe"].ToString();
            cboThoPhuTrach.DataSource = null;
            cboThoPhuTrach.Items.Clear();
            cboThoPhuTrach.Items.Add(dong["TenTho"].ToString());
            cboThoPhuTrach.SelectedIndex = 0;
            KhoaThongTin();
            lblHangMuc.Text = "Hạng mục: " + dong["SoHangMuc"] + "  |  Tổng dự kiến: " + Convert.ToDecimal(dong["TongDuKien"]).ToString("N0") + " đ";
            lblTrangThaiHen.Text = dong["NhacHen"].ToString() == "Quá hẹn" ? "Phiếu đã quá thời gian hẹn trả xe." : "Phiếu " + dong["NhacHen"].ToString().ToLower() + ".";
            lblThongBao.Text = "Đang xem phiếu " + maPhieuDangChon + ".";
        }

        void TimXeCu()
        {
            if (!dangThem || txtBienSoXe.Text.Trim().Length == 0)
                return;
            var cacDong = duLieu.DocBang("SELECT k.TenKhachHang,k.SoDienThoai,x.DongXe,x.LoaiXe FROM dbo.Xe x JOIN dbo.KhachHang k ON k.MaKhachHang=x.MaKhachHang WHERE x.BienSo=@b", XuLyDuLieu.ThamSo("@b", txtBienSoXe.Text.Trim().ToUpperInvariant()));
            if (cacDong.Rows.Count == 1)
            {
                txtKhachHang.Text = cacDong.Rows[0][0].ToString();
                txtDienThoai.Text = cacDong.Rows[0][1].ToString();
                txtDongXe.Text = cacDong.Rows[0][2].ToString();
                cboLoaiXe.SelectedItem = cacDong.Rows[0][3].ToString();
            }
        }

        string LayTinhTrang()
        {
            string tinhTrang = rtbTinhTrangXe.Text.Trim();
            if (tinhTrang.Length == 0 || tinhTrang.Length > 500)
                throw new InvalidOperationException("Tình trạng xe bắt buộc, tối đa 500 ký tự.");
            return tinhTrang;
        }

        void Luu()
        {
            if (dangSua)
            {
                PhienDangNhap.YeuCau("TIEP_NHAN");
                int soDong = duLieu.CapNhatDuLieu("UPDATE dbo.PhieuTiepNhan SET HenTra=@hen,MoTaTinhTrangXe=@tt WHERE MaPhieuTiepNhan=@ma AND TrangThai NOT IN(N'Hoàn thành',N'Đã hủy') AND NOT EXISTS(SELECT 1 FROM dbo.HoaDon WHERE MaPhieuTiepNhan=@ma)", XuLyDuLieu.ThamSo("@hen", dtpHenTra.Value), XuLyDuLieu.ThamSo("@tt", LayTinhTrang()), XuLyDuLieu.ThamSo("@ma", maPhieuDangChon));
                if (soDong != 1)
                    throw new InvalidOperationException("Phiếu đã hoàn thành hoặc đã có hóa đơn, không thể sửa.");
                dangSua = false;
                TaiDuLieu();
                lblThongBao.Text = "Đã cập nhật hẹn trả và tình trạng xe.";
                return;
            }

            if (!dangThem || dichVuDaKiemTra == null)
                throw new InvalidOperationException("Bấm Tiếp nhận mới và kiểm tra dịch vụ trước.");
            var thongTin = new ThongTinNhanXe
            {
                TenKhach = GiaoDien.BatBuocNhap(txtKhachHang, "Tên khách", 100),
                DienThoai = GiaoDien.KiemTraDienThoai(txtDienThoai),
                BienSo = GiaoDien.BatBuocNhap(txtBienSoXe, "Biển số", 15).ToUpperInvariant(),
                DongXe = txtDongXe.Text.Trim(),
                LoaiXe = cboLoaiXe.Text,
                SoKm = (int)numSoKm.Value,
                TinhTrang = LayTinhTrang(),
                MaTho = GiaoDien.GiaTriDangChon(cboThoPhuTrach),
                HenTra = dtpHenTra.Value
            };
            string ma = nghiepVu.TiepNhan(dichVuDaKiemTra, thongTin);
            dangThem = false;
            dichVuDaKiemTra = null;
            bangDuThao = null;
            TaiDuLieu();
            lblThongBao.Text = "Đã lưu " + ma + ", chốt phụ tùng và phân công thợ.";
        }

        void BatDauSua()
        {
            if (dangThem || maPhieuDangChon.Length == 0)
                throw new InvalidOperationException("Chọn phiếu đã lưu trước.");
            var dong = ((DataRowView)dgvPhieu.CurrentRow.DataBoundItem).Row;
            if (dong["TrangThai"].ToString() == "Hoàn thành" || dong["TrangThai"].ToString() == "Đã hủy" || dong["TrangThaiThanhToan"] != DBNull.Value)
                throw new InvalidOperationException("Phiếu đã hoàn thành, đã hủy hoặc đã có hóa đơn nên không thể sửa.");
            dangSua = true;
            dtpHenTra.Enabled = true;
            rtbTinhTrangXe.ReadOnly = false;
            btnLuu.Enabled = btnHuySua.Enabled = true;
            lblThongBao.Text = "Đang sửa hẹn trả và tình trạng xe. Dịch vụ và đơn giá đã chốt được giữ nguyên.";
        }

        void HuyThaoTac()
        {
            dangThem = dangSua = false;
            bangDuThao = null;
            dichVuDaKiemTra = null;
            KhoaThongTin();
            HienThiPhieu();
        }

        void HuyPhieu()
        {
            if (maPhieuDangChon.Length == 0 || dangThem)
                throw new InvalidOperationException("Chọn phiếu đã lưu cần hủy.");
            if (MessageBox.Show("Hủy phiếu chưa sửa và hoàn lại phụ tùng đã chốt?", "Hủy phiếu", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            nghiepVu.HuyChuaSua(maPhieuDangChon);
            TaiDuLieu();
        }

        DataTable LayChiTiet()
        {
            if (dangThem)
                return bangDuThao.Copy();
            if (maPhieuDangChon.Length == 0)
                throw new InvalidOperationException("Chọn phiếu cần xem.");
            return duLieu.DocBang(@"SELECT N'Dịch vụ' AS Loai,TenDichVu AS NoiDung,SoLuong,DonGiaDichVu AS DonGia,SoLuong*DonGiaDichVu AS ThanhTien FROM dbo.ChiTietPhieuTiepNhan WHERE MaPhieuTiepNhan=@ma UNION ALL SELECT N'Phụ tùng',TenPhuTung,SoLuong,DonGia,SoLuong*DonGia FROM dbo.PhuTungTiepNhan WHERE MaPhieuTiepNhan=@ma", XuLyDuLieu.ThamSo("@ma", maPhieuDangChon));
        }

        void XemChiTiet()
        {
            using (var trang = new frmChiTietPhieu(dangThem ? "CHƯA LƯU" : maPhieuDangChon, LayChiTiet()))
                trang.ShowDialog(this);
        }

        void InPhieu()
        {
            var chiTiet = LayChiTiet();
            var noiDung = new StringBuilder("PHIẾU TIẾP NHẬN " + txtMaPhieu.Text + "\n\n");
            noiDung.AppendLine("Khách hàng: " + txtKhachHang.Text + "    Điện thoại: " + txtDienThoai.Text);
            noiDung.AppendLine("Biển số: " + txtBienSoXe.Text + "    Dòng xe: " + txtDongXe.Text);
            noiDung.AppendLine("Ngày nhận: " + txtNgayNhan.Text + "    Hẹn trả: " + dtpHenTra.Value.ToString("dd/MM/yyyy HH:mm"));
            noiDung.AppendLine("Thợ: " + cboThoPhuTrach.Text + "\nTình trạng: " + rtbTinhTrangXe.Text + "\n");
            foreach (DataRow dong in chiTiet.Rows)
                noiDung.AppendLine(string.Join(" | ", dong.ItemArray.Select(giaTri => giaTri is decimal ? ((decimal)giaTri).ToString("N0") : giaTri.ToString())));
            noiDung.AppendLine("\nTổng dự kiến: " + chiTiet.AsEnumerable().Sum(dong => Convert.ToDecimal(dong["ThanhTien"])).ToString("N0") + " đ");
            using (var taiLieu = new PrintDocument())
            using (var font = new Font("Segoe UI", 10))
            using (var hopThoai = new PrintPreviewDialog
            {
                Document = taiLieu,
                Width = 1000,
                Height = 760
            }

            )
            {
                int viTri = 0;
                string vanBan = noiDung.ToString();
                taiLieu.BeginPrint += (nguoiGui, suKien) => viTri = 0;
                taiLieu.PrintPage += (nguoiGui, suKien) =>
                {
                    int soKyTu, soDong;
                    string conLai = vanBan.Substring(viTri);
                    suKien.Graphics.MeasureString(conLai, font, suKien.MarginBounds.Size, StringFormat.GenericDefault, out soKyTu, out soDong);
                    suKien.Graphics.DrawString(conLai.Substring(0, soKyTu), font, Brushes.Black, suKien.MarginBounds);
                    viTri += soKyTu;
                    suKien.HasMorePages = viTri < vanBan.Length;
                };
                hopThoai.ShowDialog(this);
            }
        }

        void DinhDangPhieu(object nguoiGui, DataGridViewCellFormattingEventArgs suKien)
        {
            if (suKien.RowIndex < 0 || dgvPhieu.Rows[suKien.RowIndex].DataBoundItem == null)
                return;
            var dong = ((DataRowView)dgvPhieu.Rows[suKien.RowIndex].DataBoundItem).Row;
            if (dong["NhacHen"].ToString() == "Quá hẹn")
            {
                suKien.CellStyle.BackColor = Color.FromArgb(255, 235, 233);
                suKien.CellStyle.ForeColor = Color.FromArgb(166, 57, 57);
            }
            else if (dong["TrangThaiHienThi"].ToString() == "Hoàn thành")
            {
                suKien.CellStyle.BackColor = Color.FromArgb(236, 250, 238);
                suKien.CellStyle.ForeColor = Color.FromArgb(49, 115, 65);
            }
        }
    }

    public class frmChiTietPhieu : TrangDuLieu
    {
        readonly string maPhieu;
        readonly DataTable chiTiet;
        public frmChiTietPhieu(string maPhieu, DataTable chiTiet = null) : base("Chi tiết phiếu — " + maPhieu)
        {
            this.maPhieu = maPhieu;
            this.chiTiet = chiTiet;
        }

        public override void TaiDuLieu()
        {
            PhienDangNhap.YeuCau("TIEP_NHAN");
            dgvDanhSach.DataSource = chiTiet ?? duLieu.DocBang(@"SELECT N'Dịch vụ' AS Loai,TenDichVu AS NoiDung,SoLuong,DonGiaDichVu AS DonGia,SoLuong*DonGiaDichVu AS ThanhTien FROM dbo.ChiTietPhieuTiepNhan WHERE MaPhieuTiepNhan=@ma UNION ALL SELECT N'Phụ tùng',TenPhuTung,SoLuong,DonGia,SoLuong*DonGia FROM dbo.PhuTungTiepNhan WHERE MaPhieuTiepNhan=@ma", XuLyDuLieu.ThamSo("@ma", maPhieu));
        }
    }
}
