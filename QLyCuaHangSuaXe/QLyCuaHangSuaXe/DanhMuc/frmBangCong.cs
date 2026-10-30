using QLyCuaHangSuaXe.LopDungChung;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace QLyCuaHangSuaXe.DanhMuc
{
    public partial class frmBangCong : TrangThietKe
    {
        readonly bool laQuanLy = PhanQuyenNguoiDung.CoQuyen(PhienDangNhap.VaiTro, "BANG_CONG");
        Control vungChinhSua, thanhChinhSua;
        string maNhanVienCu, maCaCu;
        DateTime? ngayCu;
        bool dangGan;
        public frmBangCong()
        {
            InitializeComponent();
            GanThietKe(dgvBangCong);
            cboBangCong.SelectedIndex = 0;
            vungChinhSua = grpNhapCong;
            thanhChinhSua = flpChinhSua;
            btnThemDongCong.Click += (nguoiGui, suKien) => GiaoDien.ThucHien(ThemMoi);
            btnLuuCong.Click += (nguoiGui, suKien) => GiaoDien.ThucHien(Luu);
            btnXemBangCong.Click += (nguoiGui, suKien) => GiaoDien.ThucHien(TaiDuLieu);
            btnXuatExcel.Click += (nguoiGui, suKien) => GiaoDien.ThucHien(() => XuatExcel.LuuExcel((DataTable)dgvDanhSach.DataSource, "Bảng công " + dtpTuNgay.Value.ToString("dd/MM/yyyy") + " – " + dtpDenNgay.Value.ToString("dd/MM/yyyy")));
            cboCa.SelectedIndexChanged += (nguoiGui, suKien) =>
            {
                if (dangGan)
                    return;
                var dong = cboCa.SelectedItem as DataRowView;
                if (dong != null)
                    numGioThucLam.Value = Math.Min(24, Convert.ToDecimal(dong["ThoiGianCaLam"]));
                HienDuTinh();
            };
            cboNhanVien.SelectedIndexChanged += (nguoiGui, suKien) => HienDuTinh();
            numGioThucLam.ValueChanged += (nguoiGui, suKien) => HienDuTinh();
            numGioChuanThang.ValueChanged += (nguoiGui, suKien) => HienDuTinh();
            dgvDanhSach.CurrentCellChanged += (nguoiGui, suKien) => ChonDong();
            if (!DesignMode && System.ComponentModel.LicenseManager.UsageMode != System.ComponentModel.LicenseUsageMode.Designtime)
            {
                dtpTuNgay.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                dtpDenNgay.Value = dtpNgayLam.Value = DateTime.Today;
                cboLocNhanVien.Visible = lblLocNhanVien.Visible = laQuanLy;
                DatCheDoChinhSua(laQuanLy);
            }
        }
        void DatCheDoChinhSua(bool hien)
        {
            vungChinhSua.Visible = thanhChinhSua.Visible = hien;
            tlpTrang.RowStyles[1].Height = hien ? 104 : 0;
            tlpTrang.RowStyles[2].Height = hien ? 48 : 0;
        }
        public override void TaiDuLieu()
        {
            PhienDangNhap.YeuCau(laQuanLy ? "BANG_CONG" : "CONG_CA_NHAN");
            if (dtpTuNgay.Value.Date > dtpDenNgay.Value.Date)
                throw new InvalidOperationException("Ngày bắt đầu phải trước ngày kết thúc.");
            string locCu = GiaoDien.GiaTriDangChon(cboLocNhanVien);
            dangGan = true;
            try
            {
                if (laQuanLy)
                {
                    GiaoDien.GanDuLieu(cboNhanVien, duLieu.DocBang("SELECT MaNhanVien,HoTen,Luong FROM dbo.NhanVien WHERE TrangThai=1 ORDER BY HoTen"), "HoTen", "MaNhanVien");
                    GiaoDien.GanDuLieu(cboCa, duLieu.DocBang("SELECT *,MaCaLam+N'  '+CONVERT(VARCHAR(5),ThoiGianBatDau,108)+N' – '+CONVERT(VARCHAR(5),ThoiGianKetThuc,108) AS TenCa FROM dbo.CaLam ORDER BY ThoiGianBatDau"), "TenCa", "MaCaLam");
                    var boLoc = duLieu.DocBang("SELECT MaNhanVien,HoTen FROM dbo.NhanVien ORDER BY HoTen");
                    var tatCa = boLoc.NewRow();
                    tatCa[0] = "";
                    tatCa[1] = "Tất cả nhân viên";
                    boLoc.Rows.InsertAt(tatCa, 0);
                    GiaoDien.GanDuLieu(cboLocNhanVien, boLoc, "HoTen", "MaNhanVien");
                    cboLocNhanVien.SelectedValue = locCu;
                    if (cboLocNhanVien.SelectedIndex < 0)
                        cboLocNhanVien.SelectedIndex = 0;
                }
            }
            finally { dangGan = false; }
            string[] sql ={
                @"SELECT ct.MaNhanVien,n.HoTen AS TenNhanVien,ct.NgayLam,ct.MaCaLam,c.ThoiGianBatDau,c.ThoiGianKetThuc,c.ThoiGianCaLam AS GioCa,ct.TongThoiGianLamThucTe AS GioThucLam,n.Luong AS LuongThang,CAST(n.Luong/@chuan AS DECIMAL(18,2)) AS DonGiaGio,ROUND(ct.TongThoiGianLamThucTe*n.Luong/@chuan,0) AS TienCong FROM dbo.ChiTietCaLam ct JOIN dbo.NhanVien n ON n.MaNhanVien=ct.MaNhanVien JOIN dbo.CaLam c ON c.MaCaLam=ct.MaCaLam WHERE ct.NgayLam>=@a AND ct.NgayLam<@b AND (@all=1 OR ct.MaNhanVien=@nv) AND (@loc='' OR ct.MaNhanVien=@loc) ORDER BY ct.NgayLam DESC,n.HoTen,c.ThoiGianBatDau",
                @"SELECT n.MaNhanVien,n.HoTen AS TenNhanVien,c.TenChucVu,n.Luong AS LuongThang,c.HeSoLuong,c.TienDieuChinh,CAST(n.Luong/@chuan AS DECIMAL(18,2)) AS DonGiaGio,COUNT(ct.MaCaLam) AS SoCa,SUM(CASE WHEN ct.TongThoiGianLamThucTe=0 THEN 1 ELSE 0 END) AS CaVang,COALESCE(SUM(ct.TongThoiGianLamThucTe),0) AS GioThucLam,ROUND(COALESCE(SUM(ct.TongThoiGianLamThucTe),0)*n.Luong/@chuan,0) AS TienCong FROM dbo.NhanVien n JOIN dbo.ChucVu c ON c.MaChucVu=n.MaChucVu LEFT JOIN dbo.ChiTietCaLam ct ON ct.MaNhanVien=n.MaNhanVien AND ct.NgayLam>=@a AND ct.NgayLam<@b WHERE (@all=1 OR n.MaNhanVien=@nv) AND (@loc='' OR n.MaNhanVien=@loc) GROUP BY n.MaNhanVien,n.HoTen,c.TenChucVu,n.Luong,c.HeSoLuong,c.TienDieuChinh ORDER BY n.HoTen",
                @"SELECT n.MaNhanVien,n.HoTen AS TenNhanVien,c.TenChucVu,k.TenKhoanDieuChinh,k.SoTien FROM dbo.NhanVien n JOIN dbo.ChucVu c ON c.MaChucVu=n.MaChucVu JOIN dbo.ChucVuDieuChinh cd ON cd.MaChucVu=c.MaChucVu JOIN dbo.KhoanDieuChinh k ON k.MaKhoanDieuChinh=cd.MaKhoanDieuChinh WHERE (@all=1 OR n.MaNhanVien=@nv) AND (@loc='' OR n.MaNhanVien=@loc) ORDER BY n.HoTen,k.MaKhoanDieuChinh"
            };
            bool chiTiet = cboBangCong.SelectedIndex == 0;
            DatCheDoChinhSua(laQuanLy && chiTiet);
            var bang = duLieu.DocBang(sql[cboBangCong.SelectedIndex], XuLyDuLieu.ThamSo("@a", dtpTuNgay.Value.Date), XuLyDuLieu.ThamSo("@b", dtpDenNgay.Value.Date.AddDays(1)), XuLyDuLieu.ThamSo("@all", laQuanLy), XuLyDuLieu.ThamSo("@nv", PhienDangNhap.MaNhanVien), XuLyDuLieu.ThamSo("@loc", laQuanLy ? GiaoDien.GiaTriDangChon(cboLocNhanVien) : ""), XuLyDuLieu.ThamSo("@chuan", numGioChuanThang.Value));
            dgvDanhSach.DataSource = bang;
            if (dgvDanhSach.Columns.Contains("LuongThang"))
                dgvDanhSach.Columns["LuongThang"].HeaderText = "Lương tháng (đ)";
            if (dgvDanhSach.Columns.Contains("DonGiaGio"))
                dgvDanhSach.Columns["DonGiaGio"].HeaderText = "Đơn giá giờ (đ)";
            if (dgvDanhSach.Columns.Contains("GioThucLam"))
            {
                dgvDanhSach.Columns["GioThucLam"].HeaderText = "Giờ thực làm";
                dgvDanhSach.Columns["GioThucLam"].DefaultCellStyle.Format = "N2";
            }
            if (dgvDanhSach.Columns.Contains("TienCong"))
                dgvDanhSach.Columns["TienCong"].HeaderText = "Tiền công (đ)";
            foreach (DataGridViewColumn cot in dgvDanhSach.Columns)
            {
                if (cot.ValueType == typeof(TimeSpan))
                    cot.DefaultCellStyle.Format = @"hh\:mm";
                if (cot.ValueType == typeof(DateTime))
                    cot.DefaultCellStyle.Format = "dd/MM/yyyy";
            }
            lblTongHop.Text = bang.Rows.Count + " dòng" + (bang.Columns.Contains("TienCong") ? "   •   Tổng tiền công: " + bang.AsEnumerable().Sum(d => Convert.ToDecimal(d["TienCong"])).ToString("N0") + " đ" : "");
            ChonDong();
        }
        void ThemMoi()
        {
            maNhanVienCu = maCaCu = null;
            ngayCu = null;
            dtpNgayLam.Value = DateTime.Today;
            if (cboCa.Items.Count > 0)
                cboCa.SelectedIndex = 0;
            var ca = cboCa.SelectedItem as DataRowView;
            numGioThucLam.Value = ca == null ? 0 : Math.Min(24, Convert.ToDecimal(ca["ThoiGianCaLam"]));
            HienDuTinh();
        }
        void ChonDong()
        {
            var dong = dgvDanhSach.CurrentRow == null ? null : dgvDanhSach.CurrentRow.DataBoundItem as DataRowView;
            if (dong == null)
            {
                lblCachTinh.Text = "";
                return;
            }
            if (dong.DataView.Table.Columns.Contains("NgayLam"))
            {
                if (laQuanLy)
                {
                    dangGan = true;
                    try
                    {
                        maNhanVienCu = dong["MaNhanVien"].ToString();
                        maCaCu = dong["MaCaLam"].ToString();
                        ngayCu = (DateTime)dong["NgayLam"];
                        cboNhanVien.SelectedValue = maNhanVienCu;
                        cboCa.SelectedValue = maCaCu;
                        dtpNgayLam.Value = ngayCu.Value;
                        numGioThucLam.Value = Convert.ToDecimal(dong["GioThucLam"]);
                    }
                    finally { dangGan = false; }
                }
                lblCachTinh.Text = dong["TenNhanVien"] + "  •  " + ((DateTime)dong["NgayLam"]).ToString("dd/MM/yyyy") + "\nTiền ca = " + Convert.ToDecimal(dong["GioThucLam"]).ToString("N2") + " giờ × " + Convert.ToDecimal(dong["LuongThang"]).ToString("N0") + " đ ÷ " + numGioChuanThang.Value + " giờ = " + Convert.ToDecimal(dong["TienCong"]).ToString("N0") + " đ.";
            }
            else if (dong.DataView.Table.Columns.Contains("LuongThang"))
                lblCachTinh.Text = dong["TenNhanVien"] + "  •  " + dong["SoCa"] + " ca  •  Vắng " + dong["CaVang"] + " ca\nTiền công = " + Convert.ToDecimal(dong["GioThucLam"]).ToString("N2") + " giờ × " + Convert.ToDecimal(dong["LuongThang"]).ToString("N0") + " đ ÷ " + numGioChuanThang.Value + " giờ = " + Convert.ToDecimal(dong["TienCong"]).ToString("N0") + " đ. Lương tháng đã là mức lương thỏa thuận; hệ số và khoản chức vụ không cộng thêm lần nữa.";
            else
                lblCachTinh.Text = dong["TenNhanVien"] + "  •  " + dong["TenKhoanDieuChinh"] + ": " + Convert.ToDecimal(dong["SoTien"]).ToString("N0") + " đ. Khoản quy định theo chức vụ; không tự trừ hoặc cộng lặp vào công thực tế.";
        }

        private void lblTieuDe_Click(object sender, EventArgs e)
        {

        }

        void HienDuTinh()
        {
            if (dangGan || !laQuanLy)
                return;
            var dong = cboNhanVien.SelectedItem as DataRowView;
            if (dong == null)
                return;
            decimal luong = Convert.ToDecimal(dong["Luong"]);
            lblCachTinh.Text = "Tiền ca dự kiến = " + numGioThucLam.Value.ToString("N2") + " giờ × " + luong.ToString("N0") + " đ/tháng ÷ " + numGioChuanThang.Value + " giờ = " + ChamCong.TinhTienCong(numGioThucLam.Value, luong, numGioChuanThang.Value).ToString("N0") + " đ.\nGiờ thực làm đã bao gồm thời gian đi muộn, về sớm và làm thêm; vắng cả ca nhập 0 giờ.";
        }
        void Luu()
        {
            ChamCong.LuuCong(GiaoDien.GiaTriDangChon(cboNhanVien), dtpNgayLam.Value.Date, GiaoDien.GiaTriDangChon(cboCa), numGioThucLam.Value, maNhanVienCu, ngayCu, maCaCu);
            TaiDuLieu();
        }
    }
}
