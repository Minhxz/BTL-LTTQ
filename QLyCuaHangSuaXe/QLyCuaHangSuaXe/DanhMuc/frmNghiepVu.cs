using QLyCuaHangSuaXe.LopDungChung;
using System;
using System.Data;
using System.Windows.Forms;

namespace QLyCuaHangSuaXe.DanhMuc
{
    public class frmPhanCong : TrangDuLieu
    {
        readonly NghiepVuSuaXe nghiepVu = new NghiepVuSuaXe();
        readonly ComboBox cboTho = GiaoDien.TaoDanhSachChon();
        readonly TextBox txtMaPhieu = new TextBox
        {
            Width = 125
        };
        public frmPhanCong() : base("Phân công & tiến độ sửa chữa")
        {
            var thanhThaoTac = GiaoDien.TaoThanhThaoTac();
            thanhThaoTac.Controls.Add(GiaoDien.TaoNut("Bắt đầu sửa", () => CapNhatTienDo("Đang sửa")));
            thanhThaoTac.Controls.Add(GiaoDien.TaoNut("Hoàn thành", () => CapNhatTienDo("Hoàn thành")));
            thanhThaoTac.Controls.Add(GiaoDien.TaoNut("Tải lại", TaiDuLieu));
            ThemVungNhap(thanhThaoTac);
            if (PhanQuyenNguoiDung.CoQuyen(PhienDangNhap.VaiTro, "NHAN_VIEN"))
            {
                var thanhPhanCong = GiaoDien.TaoThanhThaoTac();
                thanhPhanCong.Controls.Add(GiaoDien.TaoNhan("Phiếu chưa phân công"));
                thanhPhanCong.Controls.Add(txtMaPhieu);
                cboTho.Width = 240;
                thanhPhanCong.Controls.Add(cboTho);
                thanhPhanCong.Controls.Add(GiaoDien.TaoNut("Giao xe", PhanCong));
                thanhPhanCong.Controls.Add(GiaoDien.TaoNut("Chốt phụ tùng phiếu cũ", () =>
                {
                    nghiepVu.ChotPhieuCu(GiaoDien.GiaTriO(dgvDanhSach, "MaPhieuTiepNhan"));
                    TaiDuLieu();
                }));
                ThemVungNhap(thanhPhanCong);
            }

            if (PhanQuyenNguoiDung.CoQuyen(PhienDangNhap.VaiTro, "NHAN_VIEN"))
                thanhThaoTac.Controls.Add(GiaoDien.TaoNut("Hủy xe chưa sửa", () =>
                {
                    nghiepVu.HuyChuaSua(GiaoDien.GiaTriO(dgvDanhSach, "MaPhieuTiepNhan"));
                    TaiDuLieu();
                }));
            lblThongBao.Text = "Mỗi thợ 2 xe, thợ chính 3 xe mặc định. Đã phân công → Đang sửa → Hoàn thành. Hoàn thành mới ghi nhận tiền nhiệm vụ.";
        }

        public override void TaiDuLieu()
        {
            PhienDangNhap.YeuCau("PHAN_CONG");
            bool chuSoHuu = PhanQuyenNguoiDung.CoQuyen(PhienDangNhap.VaiTro, "NHAN_VIEN");
            dgvDanhSach.DataSource = duLieu.DocBang(@"SELECT p.MaPhieuTiepNhan,x.BienSo,k.TenKhachHang,n.MaNhanVien,n.TenNhanVien,c.TrangThaiCongViec,p.TrangThai,p.DaChotPhuTung,c.TienCong,c.ThoiGianBatDau,c.ThoiGianKetThuc,p.HenTra FROM dbo.PhieuTiepNhan p JOIN dbo.Xe x ON x.MaXe=p.MaXe JOIN dbo.KhachHang k ON k.MaKhachHang=p.MaKhachHang LEFT JOIN dbo.PhanCongTho c ON c.MaPhieuTiepNhan=p.MaPhieuTiepNhan LEFT JOIN dbo.NhanVien n ON n.MaNhanVien=c.MaNhanVien WHERE (@all=1 OR c.MaNhanVien=@nv) ORDER BY p.ThoiGianNhan DESC", XuLyDuLieu.ThamSo("@all", chuSoHuu), XuLyDuLieu.ThamSo("@nv", PhienDangNhap.MaNhanVien));
            if (chuSoHuu)
                GiaoDien.GanDuLieu(cboTho, nghiepVu.ThoRanh(), "HienThi", "MaNhanVien");
        }

        void CapNhatTienDo(string trangThai)
        {
            nghiepVu.TrangThaiCongViec(GiaoDien.GiaTriO(dgvDanhSach, "MaPhieuTiepNhan"), GiaoDien.GiaTriO(dgvDanhSach, "MaNhanVien"), trangThai);
            TaiDuLieu();
        }

        void PhanCong()
        {
            PhienDangNhap.YeuCau("NHAN_VIEN");
            string ma = GiaoDien.BatBuocNhap(txtMaPhieu, "Mã phiếu", 10), maTho = GiaoDien.GiaTriDangChon(cboTho);
            duLieu.TrongGiaoDich<int>((ketNoi, giaoDich) =>
            {
                var dongDuLieu = XuLyDuLieu.DocDuLieu(ketNoi, giaoDich, "SELECT 1 FROM dbo.PhieuTiepNhan WITH(UPDLOCK) WHERE MaPhieuTiepNhan=@id AND TrangThai NOT IN(N'Hoàn thành',N'Đã hủy') AND NOT EXISTS(SELECT 1 FROM dbo.HoaDon WHERE MaPhieuTiepNhan=@id)", XuLyDuLieu.ThamSo("@id", ma));
                if (dongDuLieu.Rows.Count != 1)
                    throw new InvalidOperationException("Phiếu không hợp lệ hoặc đã hoàn thành.");
                if (XuLyDuLieu.DocDuLieu(ketNoi, giaoDich, "SELECT 1 FROM dbo.PhanCongTho WHERE MaPhieuTiepNhan=@id", XuLyDuLieu.ThamSo("@id", ma)).Rows.Count > 0)
                    throw new InvalidOperationException("Phiếu đã có thợ phụ trách. Không thể phân công thêm để tránh tính trùng tiền nhiệm vụ.");
                var bangNhanVien = XuLyDuLieu.DocDuLieu(ketNoi, giaoDich, "SELECT n.SoXeDangSuaToiDa FROM dbo.NhanVien n WITH(UPDLOCK,HOLDLOCK) JOIN dbo.ChucVu v ON v.MaChucVu=n.MaChucVu WHERE n.MaNhanVien=@nv AND n.TrangThai=N'Đang làm' AND v.TenChucVu IN(N'Thợ',N'Thợ chính')", XuLyDuLieu.ThamSo("@nv", maTho));
                if (bangNhanVien.Rows.Count != 1)
                    throw new InvalidOperationException("Chọn thợ hợp lệ.");
                int soLuong = Convert.ToInt32(XuLyDuLieu.DocDuLieu(ketNoi, giaoDich, "SELECT COUNT(*) FROM dbo.PhanCongTho WITH(UPDLOCK,HOLDLOCK) WHERE MaNhanVien=@nv AND TrangThaiCongViec NOT IN(N'Hoàn thành',N'Đã hủy')", XuLyDuLieu.ThamSo("@nv", maTho)).Rows[0][0]);
                if (soLuong >= Convert.ToInt32(bangNhanVien.Rows[0][0]))
                    throw new InvalidOperationException("Thợ đã đủ xe.");
                return XuLyDuLieu.ThucThi(ketNoi, giaoDich, "INSERT dbo.PhanCongTho(MaPhieuTiepNhan,MaNhanVien,VaiTro,TienCong) SELECT @id,@nv,N'Phụ trách',COALESCE(SUM(SoLuong*LuongNhiemVu),0) FROM dbo.ChiTietPhieuTiepNhan WHERE MaPhieuTiepNhan=@id", XuLyDuLieu.ThamSo("@id", ma), XuLyDuLieu.ThamSo("@nv", maTho));
            });
            TaiDuLieu();
        }
    }

    public class frmHoaDon : TrangDuLieu
    {
        readonly NghiepVuSuaXe nghiepVu = new NghiepVuSuaXe();
        readonly ComboBox cboPhieuHoanThanh = GiaoDien.TaoDanhSachChon(), cboPhuongThuc = GiaoDien.TaoDanhSachChon();
        public frmHoaDon() : base("Hóa đơn")
        {
            var thanhThaoTac = GiaoDien.TaoThanhThaoTac();
            cboPhieuHoanThanh.Width = 250;
            thanhThaoTac.Controls.Add(GiaoDien.TaoNhan("Phiếu đã hoàn thành"));
            thanhThaoTac.Controls.Add(cboPhieuHoanThanh);
            thanhThaoTac.Controls.Add(GiaoDien.TaoNut("Phát hành hóa đơn", () =>
            {
                string ma = nghiepVu.LapHoaDon(GiaoDien.GiaTriDangChon(cboPhieuHoanThanh));
                TaiDuLieu();
                lblThongBao.Text = "Đã phát hành " + ma + ". Nội dung được khóa.";
            }));
            thanhThaoTac.Controls.Add(GiaoDien.TaoNut("Chi tiết", XemChiTiet));
            ThemVungNhap(thanhThaoTac);
            var thanhThanhToan = GiaoDien.TaoThanhThaoTac();
            cboPhuongThuc.Items.AddRange(new object[] { "Tiền mặt", "Chuyển khoản" });
            cboPhuongThuc.SelectedIndex = 0;
            thanhThanhToan.Controls.Add(cboPhuongThuc);
            thanhThanhToan.Controls.Add(GiaoDien.TaoNut("Ghi nhận thanh toán", () =>
            {
                nghiepVu.ThanhToan(GiaoDien.GiaTriO(dgvDanhSach, "MaHoaDon"), cboPhuongThuc.Text);
                TaiDuLieu();
            }));
            thanhThanhToan.Controls.Add(GiaoDien.TaoNut("Tải lại", TaiDuLieu));
            ThemVungNhap(thanhThanhToan);
            lblThongBao.Text = "Hóa đơn lấy giá đã chốt từ chi tiết phiếu tiếp nhận. Không sửa hay xóa hóa đơn; chỉ ghi nhận thanh toán một lần.";
        }

        public override void TaiDuLieu()
        {
            PhienDangNhap.YeuCau("HOA_DON");
            dgvDanhSach.DataSource = duLieu.DocBang(@"SELECT h.MaHoaDon,h.MaPhieuTiepNhan,x.BienSo,k.TenKhachHang,h.NgayLap,h.TongTienDichVu,h.TongTienPhuTung,h.TongTien,h.TrangThaiThanhToan,h.PhuongThucThanhToan FROM dbo.HoaDon h JOIN dbo.PhieuTiepNhan p ON p.MaPhieuTiepNhan=h.MaPhieuTiepNhan JOIN dbo.Xe x ON x.MaXe=p.MaXe JOIN dbo.KhachHang k ON k.MaKhachHang=p.MaKhachHang ORDER BY h.NgayLap DESC");
            GiaoDien.GanDuLieu(cboPhieuHoanThanh, duLieu.DocBang("SELECT p.MaPhieuTiepNhan,p.MaPhieuTiepNhan+N' — '+x.BienSo AS HienThi FROM dbo.PhieuTiepNhan p JOIN dbo.Xe x ON x.MaXe=p.MaXe WHERE p.TrangThai=N'Hoàn thành' AND p.DaChotPhuTung=1 AND NOT EXISTS(SELECT 1 FROM dbo.HoaDon h WHERE h.MaPhieuTiepNhan=p.MaPhieuTiepNhan)"), "HienThi", "MaPhieuTiepNhan");
        }

        void XemChiTiet()
        {
            string ma = GiaoDien.GiaTriO(dgvDanhSach, "MaHoaDon");
            if (ma.Length == 0)
                return;
            using (var frmChiTiet = new frmChiTietHoaDon(ma))
                frmChiTiet.ShowDialog(this);
        }
    }

    public class frmChiTietHoaDon : TrangDuLieu
    {
        readonly string maHoaDon;
        public frmChiTietHoaDon(string maHoaDon) : base("Chi tiết hóa đơn — " + maHoaDon)
        {
            this.maHoaDon = maHoaDon;
            lblThongBao.Text = "Dữ liệu chỉ đọc; đơn giá lấy từ chứng từ đã chốt.";
        }

        public override void TaiDuLieu()
        {
            PhienDangNhap.YeuCau("HOA_DON");
            dgvDanhSach.DataSource = duLieu.DocBang(@"SELECT N'Dịch vụ' AS Loai,TenDichVu AS NoiDung,SoLuong,DonGia,ThanhTien FROM dbo.ChiTietHoaDon WHERE MaHoaDon=@id UNION ALL SELECT N'Phụ tùng',p.TenPhuTung,p.SoLuong,p.DonGia,p.SoLuong*p.DonGia FROM dbo.PhuTungTiepNhan p JOIN dbo.HoaDon h ON h.MaPhieuTiepNhan=p.MaPhieuTiepNhan WHERE h.MaHoaDon=@id", XuLyDuLieu.ThamSo("@id", maHoaDon));
        }
    }

    public class frmThongKe : TrangDuLieu
    {
        readonly DateTimePicker dtpTuNgay = new DateTimePicker
        {
            Format = DateTimePickerFormat.Custom,
            CustomFormat = "dd/MM/yyyy",
            Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1)
        }, dtpDenNgay = new DateTimePicker
        {
            Format = DateTimePickerFormat.Custom,
            CustomFormat = "dd/MM/yyyy"
        };
        readonly ComboBox cboBaoCao = GiaoDien.TaoDanhSachChon();
        public frmThongKe() : base("Báo cáo & thống kê")
        {
            cboBaoCao.Items.AddRange(new object[] { "Doanh thu hóa đơn", "Tiền nhiệm vụ thợ", "Dịch vụ đã bán", "Tồn phụ tùng", "Nhập kho", "Tiếp nhận & tiến độ", "Bảng công & lương" });
            cboBaoCao.SelectedIndex = 0;
            var giaTriThuHai = GiaoDien.TaoThanhThaoTac();
            giaTriThuHai.Controls.Add(GiaoDien.TaoNhan("Từ ngày"));
            giaTriThuHai.Controls.Add(dtpTuNgay);
            giaTriThuHai.Controls.Add(GiaoDien.TaoNhan("Đến ngày"));
            giaTriThuHai.Controls.Add(dtpDenNgay);
            cboBaoCao.Width = 220;
            giaTriThuHai.Controls.Add(cboBaoCao);
            giaTriThuHai.Controls.Add(GiaoDien.TaoNut("Thống kê", TaiDuLieu));
            giaTriThuHai.Controls.Add(GiaoDien.TaoNut("Xuất Excel", () => XuatExcel.LuuExcel((DataTable)dgvDanhSach.DataSource, "Thống kê")));
            ThemVungNhap(giaTriThuHai);
        }

        public override void TaiDuLieu()
        {
            PhienDangNhap.YeuCau("BAO_CAO");
            if (dtpTuNgay.Value.Date > dtpDenNgay.Value.Date)
                throw new InvalidOperationException("Ngày bắt đầu phải trước ngày kết thúc.");
            string[] cauLenhSql =
            {
                "SELECT CAST(NgayLap AS DATE) AS Ngay,COUNT(*) AS SoHoaDon,SUM(TongTien) AS DaPhatHanh,SUM(CASE WHEN TrangThaiThanhToan=N'Đã thanh toán' THEN TongTien ELSE 0 END) AS DaThu,SUM(CASE WHEN TrangThaiThanhToan<>N'Đã thanh toán' THEN TongTien ELSE 0 END) AS ChuaThu FROM dbo.HoaDon WHERE NgayLap>=@a AND NgayLap<@b GROUP BY CAST(NgayLap AS DATE)",
                "SELECT n.MaNhanVien,n.TenNhanVien,COUNT(*) AS NhiemVuHoanThanh,SUM(p.TienCong) AS TienNhiemVu FROM dbo.PhanCongTho p JOIN dbo.NhanVien n ON n.MaNhanVien=p.MaNhanVien WHERE p.TrangThaiCongViec=N'Hoàn thành' AND p.NgayGhiNhanCong>=@a AND p.NgayGhiNhanCong<@b GROUP BY n.MaNhanVien,n.TenNhanVien",
                "SELECT c.MaDichVu,c.TenDichVu,SUM(c.SoLuong) AS SoLuong,SUM(c.ThanhTien) AS TienDichVu FROM dbo.ChiTietHoaDon c JOIN dbo.HoaDon h ON h.MaHoaDon=c.MaHoaDon WHERE h.NgayLap>=@a AND h.NgayLap<@b GROUP BY c.MaDichVu,c.TenDichVu",
                "SELECT MaPhuTung,TenPhuTung,SoLuongTon,DonGiaNhap,DonGiaBan,SoLuongTon*DonGiaNhap AS GiaTriTon,TrangThai FROM dbo.PhuTung",
                "SELECT p.MaPhieuNhap,p.NgayNhap,n.TenNhanVien,p.TongTien FROM dbo.PhieuNhap p JOIN dbo.NhanVien n ON n.MaNhanVien=p.MaNhanVienNhap WHERE p.NgayNhap>=@a AND p.NgayNhap<@b",
                "SELECT TrangThai,COUNT(*) AS SoPhieu,SUM(CASE WHEN HenTra<SYSDATETIME() AND TrangThai NOT IN(N'Hoàn thành',N'Đã hủy') THEN 1 ELSE 0 END) AS QuaHen FROM dbo.PhieuTiepNhan WHERE ThoiGianNhan>=@a AND ThoiGianNhan<@b GROUP BY TrangThai",
                "SELECT MaNhanVien,TenNhanVien,SUM(GioTinhCong) AS GioTinhCong,SUM(TienLuong) AS LuongTheoGio,SUM(PhutMuon) AS PhutMuon,SUM(CASE WHEN Vang=1 THEN 1 ELSE 0 END) AS CaVang FROM dbo.vBangCong WHERE Ngay>=@a AND Ngay<@b GROUP BY MaNhanVien,TenNhanVien"
            };
            dgvDanhSach.DataSource = duLieu.DocBang(cauLenhSql[cboBaoCao.SelectedIndex], XuLyDuLieu.ThamSo("@a", dtpTuNgay.Value.Date), XuLyDuLieu.ThamSo("@b", dtpDenNgay.Value.Date.AddDays(1)));
            lblThongBao.Text = cboBaoCao.SelectedIndex == 3 ? "Tồn kho hiện tại, không phải tồn tại ngày trong bộ lọc." : "Khoảng ngày bao gồm cả ngày kết thúc. Doanh thu theo ngày lập hóa đơn; tiền đã thu là trạng thái hiện tại.";
        }
    }
}
