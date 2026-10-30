using QLyCuaHangSuaXe.LopDungChung;
using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace QLyCuaHangSuaXe.DanhMuc
{
    public partial class frmThongKe : TrangThietKe
    {
        public frmThongKe()
        {
            InitializeComponent();
            cboBaoCao.SelectedIndex = 0;
            GanThietKe(dgvThongKe);
            DinhDangTrang();
            btnThongKe.Click += (nguoiGui, suKien) => GiaoDien.ThucHien(TaiDuLieu);
            btnXuatExcel.Click += (nguoiGui, suKien) => GiaoDien.ThucHien(() => XuatExcel.LuuExcel((DataTable)dgvDanhSach.DataSource, "Thống kê"));
            if (!DesignMode && System.ComponentModel.LicenseManager.UsageMode != System.ComponentModel.LicenseUsageMode.Designtime)
            {
                dtpTuNgay.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                dtpDenNgay.Value = DateTime.Today;
            }
        }
        public override void TaiDuLieu()
        {
            PhienDangNhap.YeuCau("BAO_CAO");
            if (dtpTuNgay.Value.Date > dtpDenNgay.Value.Date)
            {
                throw new InvalidOperationException("Ngày bắt đầu phải trước ngày kết thúc.");
            }
            string[] sql ={
                "SELECT NgayThanhToan AS Ngay,COUNT(*) AS SoHoaDon,SUM(TienThanhToan) AS DoanhThu FROM dbo.HoaDon WHERE NgayThanhToan>=@a AND NgayThanhToan<@b GROUP BY NgayThanhToan ORDER BY NgayThanhToan",
                @"SELECT n.MaNhanVien,n.HoTen AS TenNhanVien,COUNT(DISTINCT c.MaXe) AS SoXeHoanThanh,SUM(DATEDIFF(MINUTE,c.ThoiGianBatDau,c.ThoiGianHoanThanh)) AS TongPhut FROM dbo.ChiTietPhanCong c JOIN dbo.NhanVien n ON n.MaNhanVien=c.MaNhanVien WHERE c.ThoiGianHoanThanh>=@a AND c.ThoiGianHoanThanh<@b AND EXISTS(SELECT 1 FROM dbo.PhieuTiepNhan p WHERE p.MaXe=c.MaXe AND p.TrangThai=N'Hoàn thành') GROUP BY n.MaNhanVien,n.HoTen ORDER BY SoXeHoanThanh DESC",
                @"SELECT c.MaDichVu,d.TenDichVu,SUM(c.SoLuong) AS SoLuong,SUM(c.ThanhTien) AS DoanhThu FROM dbo.ChiTietPhieuTiepNhan c JOIN dbo.HoaDon h ON h.MaPhieuTiepNhan=c.MaPhieuTiepNhan JOIN dbo.DichVu d ON d.MaDichVu=c.MaDichVu WHERE h.NgayThanhToan>=@a AND h.NgayThanhToan<@b GROUP BY c.MaDichVu,d.TenDichVu ORDER BY DoanhThu DESC",
                "SELECT MaPhuTung,TenPhuTung,SoLuongTon,DonGiaNhap,DonGiaBan,SoLuongTon*DonGiaNhap AS GiaTriTon,HanSuDung,TrangThai FROM dbo.PhuTung ORDER BY MaPhuTung",
                "SELECT p.MaPhieuNhap,p.NgayNhap,n.MaNhanVien,n.HoTen AS TenNhanVien,p.TongTienNhap AS TongTien FROM dbo.PhieuNhap p JOIN dbo.NhanVien n ON n.MaNhanVien=p.MaNhanVienNhap WHERE p.NgayNhap>=@a AND p.NgayNhap<@b ORDER BY p.NgayNhap DESC",
                "SELECT TrangThai,COUNT(*) AS SoPhieu,SUM(CASE WHEN NgayHen<CAST(GETDATE() AS DATE) AND TrangThai IN(N'Chờ sửa',N'Đang sửa') THEN 1 ELSE 0 END) AS QuaHen FROM dbo.PhieuTiepNhan WHERE NgayLap>=@a AND NgayLap<@b GROUP BY TrangThai",
                "SELECT n.MaNhanVien,n.HoTen AS TenNhanVien,COUNT(*) AS SoCa,SUM(c.TongThoiGianLamThucTe) AS GioThucLam,SUM(CASE WHEN c.TongThoiGianLamThucTe=0 THEN 1 ELSE 0 END) AS CaVang FROM dbo.ChiTietCaLam c JOIN dbo.NhanVien n ON n.MaNhanVien=c.MaNhanVien WHERE c.NgayLam>=@a AND c.NgayLam<@b GROUP BY n.MaNhanVien,n.HoTen ORDER BY n.HoTen"
            };
            DataTable bang = duLieu.DocBang(sql[cboBaoCao.SelectedIndex], XuLyDuLieu.ThamSo("@a", dtpTuNgay.Value.Date), XuLyDuLieu.ThamSo("@b", dtpDenNgay.Value.Date.AddDays(1)));
            dgvDanhSach.DataSource = bang;
            CapNhatThongKe(bang, cboBaoCao.SelectedIndex);
        }

        void NenTrang(Control goc)
        {
            goc.BackColor = Color.White;
            foreach (Control con in goc.Controls)
                NenTrang(con);
        }

        void DinhDangTrang()
        {
            foreach (Control khung in new Control[] { pnlChiSoMot, pnlChiSoHai, pnlChiSoBa, pnlBieuDo, pnlBangDuLieu })
                NenTrang(khung);
            btnThongKe.BackColor = GiaoDien.Xanh;
            btnThongKe.ForeColor = Color.White;
            btnThongKe.FlatAppearance.BorderColor = GiaoDien.Xanh;
            foreach (Label nhan in new[] { lblGiaTriMot, lblGiaTriHai, lblGiaTriBa, lblTenBieuDo, lblKetQua })
                nhan.ForeColor = Color.FromArgb(30, 93, 145);
            foreach (Label nhan in new[] { lblChiSoMot, lblChiSoHai, lblChiSoBa, lblMoTaMot, lblMoTaHai, lblMoTaBa, lblMoTaBieuDo, lblChuThichBieuDo, lblTrangThaiBaoCao })
                nhan.ForeColor = Color.FromArgb(91, 108, 123);
            bieuDoThongKe.BackColor = Color.White;
        }

        static readonly CultureInfo vanHoa = CultureInfo.GetCultureInfo("vi-VN");
        static decimal So(DataRow dong, string cot)
        {
            return dong[cot] == DBNull.Value ? 0 : Convert.ToDecimal(dong[cot]);
        }

        static decimal Tong(DataTable bang, string cot)
        {
            return bang.AsEnumerable().Sum(dong => So(dong, cot));
        }

        static string DinhDang(decimal so, bool tien = false, bool gio = false)
        {
            string dinhDangSo = gio ? "N2" : "N0";
            string donVi = tien ? " đ" : gio ? " giờ" : "";
            return so.ToString(dinhDangSo, vanHoa) + donVi;
        }
        void GanChiSo(Label ten, Label giaTri, Label moTa, string tieuDe, decimal so, string chuThich, bool tien = false, bool gio = false)
        {
            ten.Text = tieuDe;
            giaTri.Text = DinhDang(so, tien, gio);
            moTa.Text = chuThich;
        }
        void CapNhatThongKe(DataTable bang, int loai)
        {
            decimal giaTriMot = 0, giaTriHai = 0, giaTriBa = 0;
            string[] tenChiSoMot = { "Tổng doanh thu", "Xe hoàn thành", "Doanh thu dịch vụ", "Giá trị tồn kho", "Tổng tiền nhập", "Tổng phiếu", "Giờ thực làm" };
            string[] tenChiSoHai = { "Hóa đơn", "Nhân viên", "Lượt dịch vụ", "Số lượng tồn", "Phiếu nhập", "Đã hoàn thành", "Tổng số ca" };
            string[] tenChiSoBa = { "Bình quân / hóa đơn", "Tổng giờ sửa", "Dịch vụ đã bán", "Phụ tùng sắp hết", "Nhân viên nhập", "Phiếu quá hẹn", "Ca vắng" };
            switch (loai)
            {
                case 0:
                    giaTriMot = Tong(bang, "DoanhThu");
                    giaTriHai = Tong(bang, "SoHoaDon");
                    giaTriBa = giaTriHai == 0 ? 0 : giaTriMot / giaTriHai;
                    break;
                case 1:
                    giaTriMot = Tong(bang, "SoXeHoanThanh");
                    giaTriHai = bang.Rows.Count;
                    giaTriBa = Tong(bang, "TongPhut") / 60m;
                    break;
                case 2:
                    giaTriMot = Tong(bang, "DoanhThu");
                    giaTriHai = Tong(bang, "SoLuong");
                    giaTriBa = bang.Rows.Count;
                    break;
                case 3:
                    giaTriMot = Tong(bang, "GiaTriTon");
                    giaTriHai = Tong(bang, "SoLuongTon");
                    giaTriBa = bang.AsEnumerable().Count(dong => So(dong, "SoLuongTon") <= 5);
                    break;
                case 4:
                    giaTriMot = Tong(bang, "TongTien");
                    giaTriHai = bang.Rows.Count;
                    giaTriBa = bang.AsEnumerable().Select(dong => dong["MaNhanVien"].ToString()).Distinct().Count();
                    break;
                case 5:
                    giaTriMot = Tong(bang, "SoPhieu");
                    giaTriHai = bang.AsEnumerable().Where(dong => dong["TrangThai"].ToString() == "Hoàn thành").Sum(dong => So(dong, "SoPhieu"));
                    giaTriBa = Tong(bang, "QuaHen");
                    break;
                case 6:
                    giaTriMot = Tong(bang, "GioThucLam");
                    giaTriHai = Tong(bang, "SoCa");
                    giaTriBa = Tong(bang, "CaVang");
                    break;
            }
            string phamVi = loai == 3 ? "Tồn kho hiện tại" : "Trong khoảng đã chọn";
            GanChiSo(lblChiSoMot, lblGiaTriMot, lblMoTaMot, tenChiSoMot[loai], giaTriMot, phamVi, loai == 0 || loai == 2 || loai == 3 || loai == 4, loai == 6);
            GanChiSo(lblChiSoHai, lblGiaTriHai, lblMoTaHai, tenChiSoHai[loai], giaTriHai, phamVi);
            GanChiSo(lblChiSoBa, lblGiaTriBa, lblMoTaBa, tenChiSoBa[loai], giaTriBa, loai == 3 ? "Tồn từ 5 trở xuống" : phamVi, loai == 0, loai == 1);
            lblTenBieuDo.Text = cboBaoCao.Text;
            lblMoTaBieuDo.Text = loai == 3 ? "Giá trị tồn theo phụ tùng" : "Dữ liệu theo khoảng thời gian đã chọn";
            lblKetQua.Text = "CHI TIẾT BÁO CÁO  •  " + bang.Rows.Count.ToString("N0", vanHoa) + " dòng";
            lblTrangThaiBaoCao.Text = loai == 3 ? "Tồn kho hiện tại" : dtpTuNgay.Value.ToString("dd/MM/yyyy") + "  –  " + dtpDenNgay.Value.ToString("dd/MM/yyyy");
            VeBieuDo(bang, loai);
        }
        void VeBieuDo(DataTable bang, int loai)
        {
            bieuDoThongKe.Series.Clear();
            bieuDoThongKe.Legends.Clear();
            ChartArea vung = bieuDoThongKe.ChartAreas["VungBieuDo"];
            vung.BackColor = Color.White;
            vung.AxisX.MajorGrid.Enabled = false;
            vung.AxisY.MajorGrid.LineColor = Color.FromArgb(231, 237, 243);
            vung.AxisX.LineColor = vung.AxisY.LineColor = Color.FromArgb(205, 218, 230);
            vung.AxisX.LabelStyle.ForeColor = vung.AxisY.LabelStyle.ForeColor = Color.FromArgb(90, 108, 125);
            vung.AxisX.LabelStyle.Font = vung.AxisY.LabelStyle.Font = new Font("Segoe UI", 8F);
            vung.AxisY.LabelStyle.Format = "N0";
            vung.AxisY.Minimum = 0;
            vung.AxisX.Interval = 1;
            var chuoi = new Series("Số liệu") { ChartArea = "VungBieuDo", Color = Color.FromArgb(38, 122, 188), BorderWidth = 3, Font = new Font("Segoe UI", 8F), IsValueShownAsLabel = false };
            chuoi.ChartType = loai == 0 ? SeriesChartType.Line : loai == 5 ? SeriesChartType.Doughnut : loai == 4 ? SeriesChartType.Column : SeriesChartType.Bar;
            if (loai == 0)
            {
                chuoi.MarkerStyle = MarkerStyle.Circle;
                chuoi.MarkerSize = 7;
            }
            if (loai == 5)
            {
                chuoi["DoughnutRadius"] = "65";
                chuoi["PieLabelStyle"] = "Outside";
            }
            bieuDoThongKe.Series.Add(chuoi);
            string cot = new[] { "DoanhThu", "SoXeHoanThanh", "DoanhThu", "GiaTriTon", "TongTien", "SoPhieu", "GioThucLam" }[loai];
            string nhan = new[] { "Ngay", "TenNhanVien", "TenDichVu", "TenPhuTung", "MaPhieuNhap", "TrangThai", "TenNhanVien" }[loai];
            string donVi = loai == 0 || loai == 2 || loai == 3 || loai == 4 ? "đ" : loai == 6 ? "giờ" : loai == 1 ? "xe" : loai == 5 ? "phiếu" : "";
            if (loai == 0 && bang.Rows.Count > 31)
            {
                var thang = bang.AsEnumerable().GroupBy(d => new DateTime(Convert.ToDateTime(d["Ngay"]).Year, Convert.ToDateTime(d["Ngay"]).Month, 1)).OrderBy(n => n.Key).ToArray();
                if (thang.Length > 24)
                foreach (var nam in bang.AsEnumerable().GroupBy(d => Convert.ToDateTime(d["Ngay"]).Year).OrderBy(n => n.Key))
                    ThemDiem(chuoi, nam.Key.ToString(), nam.Sum(d => So(d, cot)), donVi);
                else
                foreach (var muc in thang)
                    ThemDiem(chuoi, muc.Key.ToString("MM/yyyy"), muc.Sum(d => So(d, cot)), donVi);
                lblChuThichBieuDo.Text = thang.Length > 24 ? "Doanh thu theo năm" : "Doanh thu theo tháng";
            }
            else
            {
                System.Collections.Generic.IEnumerable<DataRow> dong = bang.AsEnumerable();
                if (loai != 0 && loai != 5)
                    dong = dong.OrderByDescending(d => So(d, cot)).Take(8);
                if (loai == 5)
                    dong = dong.Where(d => So(d, cot) > 0);
                foreach (DataRow d in dong)
                    ThemDiem(chuoi, loai == 0 ? Convert.ToDateTime(d[nhan]).ToString("dd/MM") : Convert.ToString(d[nhan]), So(d, cot), donVi);
                lblChuThichBieuDo.Text = loai == 0 ? "Doanh thu theo ngày" : loai == 5 ? "Số phiếu theo trạng thái" : "Tối đa 8 mục có giá trị cao nhất";
            }
            if (loai == 5)
            {
                Color[] mau = { Color.FromArgb(39, 120, 185), Color.FromArgb(68, 163, 135), Color.FromArgb(235, 167, 76), Color.FromArgb(173, 95, 100), Color.FromArgb(127, 142, 175) };
                for (int i = 0; i < chuoi.Points.Count; i++)
                {
                    chuoi.Points[i].Color = mau[i % mau.Length];
                    chuoi.Points[i].Label = chuoi.Points[i].AxisLabel + " (" + chuoi.Points[i].YValues[0].ToString("N0", vanHoa) + ")";
                }
            }
            lblKhongDuLieu.Visible = chuoi.Points.Count == 0;
            if (lblKhongDuLieu.Visible)
                lblKhongDuLieu.BringToFront();
            else
                bieuDoThongKe.BringToFront();
        }
        static void ThemDiem(Series chuoi, string nhan, decimal giaTri, string donVi)
        {
            int viTri = chuoi.Points.AddXY(nhan, (double)giaTri);
            var diem = chuoi.Points[viTri];
            diem.ToolTip = nhan + ": " + giaTri.ToString("N2", vanHoa) + " " + donVi;
        }
    }
}
