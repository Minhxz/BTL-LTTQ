using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace QLyCuaHangSuaXe.LopDungChung
{
    public sealed class DichVuChon
    {
        public string Ma;
        public int SoLuong;
    }

    public sealed class ThongTinNhanXe
    {
        public string TenKhach, DienThoai, BienSo, DongXe, LoaiXe, TinhTrang, MaTho;
        public DateTime HenTra;
        public int SoKm;
    }

    public class NghiepVuSuaXe
    {
        readonly XuLyDuLieu duLieu = new XuLyDuLieu();
        static SqlParameter ThamSo(string ten, object giaTri)
        {
            return XuLyDuLieu.ThamSo(ten, giaTri);
        }

        static DataTable DichVu(SqlConnection ketNoi, SqlTransaction giaoDich, List<DichVuChon> cacDichVu)
        {
            if (cacDichVu == null || cacDichVu.Count == 0 || cacDichVu.Select(dichVu => dichVu.Ma).Distinct().Count() != cacDichVu.Count || cacDichVu.Any(dichVu => dichVu.SoLuong <= 0 || dichVu.SoLuong > 1000))
                throw new InvalidOperationException("Chọn ít nhất một dịch vụ, số lượng 1–1000, không trùng dịch vụ.");
            var ketQua = new DataTable();
            ketQua.Columns.Add("MaDichVu");
            ketQua.Columns.Add("TenDichVu");
            ketQua.Columns.Add("SoLuong", typeof(int));
            ketQua.Columns.Add("DonGia", typeof(decimal));
            ketQua.Columns.Add("Luong", typeof(decimal));
            foreach (var dichVuDangChon in cacDichVu.OrderBy(dichVu => dichVu.Ma))
            {
                var bangDichVu = XuLyDuLieu.DocDuLieu(ketNoi, giaoDich, "SELECT TenDichVu,DonGiaDichVu,LuongNhiemVu FROM dbo.DichVu WITH(UPDLOCK,HOLDLOCK) WHERE MaDichVu=@id AND TrangThai=N'Đang sử dụng'", ThamSo("@id", dichVuDangChon.Ma));
                if (bangDichVu.Rows.Count != 1)
                    throw new InvalidOperationException("Dịch vụ đã ngừng sử dụng hoặc không tồn tại.");
                ketQua.Rows.Add(dichVuDangChon.Ma, bangDichVu.Rows[0][0], dichVuDangChon.SoLuong, bangDichVu.Rows[0][1], bangDichVu.Rows[0][2]);
            }

            return ketQua;
        }

        static DataTable PhuTung(SqlConnection ketNoi, SqlTransaction giaoDich, List<DichVuChon> cacDichVu)
        {
            var tongNhuCau = new SortedDictionary<string, int>();
            foreach (var dichVuDangChon in cacDichVu.OrderBy(dichVu => dichVu.Ma))
            {
                var dinhMuc = XuLyDuLieu.DocDuLieu(ketNoi, giaoDich, "SELECT MaPhuTung,SoLuong FROM dbo.DinhMucDichVu WITH(HOLDLOCK) WHERE MaDichVu=@id", ThamSo("@id", dichVuDangChon.Ma));
                foreach (DataRow dong in dinhMuc.Rows)
                {
                    string khoa = dong[0].ToString();
                    if (!tongNhuCau.ContainsKey(khoa))
                        tongNhuCau[khoa] = 0;
                    tongNhuCau[khoa] = checked(tongNhuCau[khoa] + Convert.ToInt32(dong[1]) * dichVuDangChon.SoLuong);
                }
            }

            var ketQua = new DataTable();
            ketQua.Columns.Add("MaPhuTung");
            ketQua.Columns.Add("TenPhuTung");
            ketQua.Columns.Add("Can", typeof(int));
            ketQua.Columns.Add("Ton", typeof(int));
            ketQua.Columns.Add("DonGia", typeof(decimal));
            ketQua.Columns.Add("TrangThai");
            foreach (var capGiaTri in tongNhuCau)
            {
                var bangPhuTung = XuLyDuLieu.DocDuLieu(ketNoi, giaoDich, "SELECT TenPhuTung,SoLuongTon,DonGiaBan,TrangThai FROM dbo.PhuTung WITH(UPDLOCK,HOLDLOCK) WHERE MaPhuTung=@id", ThamSo("@id", capGiaTri.Key));
                if (bangPhuTung.Rows.Count != 1)
                    throw new InvalidOperationException("Không tìm thấy phụ tùng " + capGiaTri.Key);
                ketQua.Rows.Add(capGiaTri.Key, bangPhuTung.Rows[0][0], capGiaTri.Value, bangPhuTung.Rows[0][1], bangPhuTung.Rows[0][2], bangPhuTung.Rows[0][3]);
            }

            return ketQua;
        }

        static void DuKho(DataTable bangPhuTung)
        {
            var phuTungThieu = bangPhuTung.AsEnumerable().Where(dongDuLieu => dongDuLieu.Field<int>("Can") > dongDuLieu.Field<int>("Ton") || dongDuLieu.Field<string>("TrangThai") != "Đang sử dụng").Select(dongDuLieu => dongDuLieu["TenPhuTung"] + ": cần " + dongDuLieu["Can"] + ", tồn " + dongDuLieu["Ton"] + " (" + dongDuLieu["TrangThai"] + ")");
            if (phuTungThieu.Any())
                throw new InvalidOperationException("Chưa đủ phụ tùng:\n" + string.Join("\n", phuTungThieu));
        }

        public DataTable KiemTraKho(List<DichVuChon> cacDichVu)
        {
            PhienDangNhap.YeuCau("TIEP_NHAN");
            return duLieu.TrongGiaoDich((ketNoi, giaoDich) =>
            {
                DichVu(ketNoi, giaoDich, cacDichVu);
                var bangPhuTung = PhuTung(ketNoi, giaoDich, cacDichVu);
                DuKho(bangPhuTung);
                return bangPhuTung;
            });
        }

        public DataTable ThoRanh()
        {
            PhienDangNhap.YeuCau(PhanQuyenNguoiDung.CoQuyen(PhienDangNhap.VaiTro, "NHAN_VIEN") ? "NHAN_VIEN" : "TIEP_NHAN");
            return duLieu.DocBang(@"SELECT n.MaNhanVien,n.TenNhanVien+N' — '+CAST(n.SoXeDangSuaToiDa-COUNT(p.MaPhieuTiepNhan) AS NVARCHAR(10))+N' chỗ trống' AS HienThi FROM dbo.NhanVien n JOIN dbo.ChucVu v ON v.MaChucVu=n.MaChucVu LEFT JOIN dbo.PhanCongTho p ON p.MaNhanVien=n.MaNhanVien AND p.TrangThaiCongViec NOT IN(N'Hoàn thành',N'Đã hủy') WHERE n.TrangThai=N'Đang làm' AND v.TenChucVu IN(N'Thợ',N'Thợ chính') GROUP BY n.MaNhanVien,n.TenNhanVien,n.SoXeDangSuaToiDa HAVING COUNT(p.MaPhieuTiepNhan)<n.SoXeDangSuaToiDa");
        }

        static void KiemTraTho(SqlConnection ketNoi, SqlTransaction giaoDich, string ma)
        {
            var bangNhanVien = XuLyDuLieu.DocDuLieu(ketNoi, giaoDich, @"SELECT n.SoXeDangSuaToiDa FROM dbo.NhanVien n WITH(UPDLOCK,HOLDLOCK) JOIN dbo.ChucVu v ON v.MaChucVu=n.MaChucVu WHERE n.MaNhanVien=@id AND n.TrangThai=N'Đang làm' AND v.TenChucVu IN(N'Thợ',N'Thợ chính')", ThamSo("@id", ma));
            if (bangNhanVien.Rows.Count != 1)
                throw new InvalidOperationException("Hãy chọn thợ đang làm việc.");
            var soLuong = XuLyDuLieu.DocDuLieu(ketNoi, giaoDich, "SELECT COUNT(*) FROM dbo.PhanCongTho WITH(UPDLOCK,HOLDLOCK) WHERE MaNhanVien=@id AND TrangThaiCongViec NOT IN(N'Hoàn thành',N'Đã hủy')", ThamSo("@id", ma));
            if (Convert.ToInt32(soLuong.Rows[0][0]) >= Convert.ToInt32(bangNhanVien.Rows[0][0]))
                throw new InvalidOperationException("Thợ đã đủ số xe được nhận. Vui lòng chọn thợ khác.");
        }

        static void ChotKho(SqlConnection ketNoi, SqlTransaction giaoDich, string ma, DataTable bangPhuTung)
        {
            DuKho(bangPhuTung);
            foreach (DataRow dongDuLieu in bangPhuTung.Rows)
            {
                int soDongCapNhat = XuLyDuLieu.ThucThi(ketNoi, giaoDich, "UPDATE dbo.PhuTung SET SoLuongTon=SoLuongTon-@q WHERE MaPhuTung=@p AND SoLuongTon>=@q AND TrangThai=N'Đang sử dụng'", ThamSo("@q", dongDuLieu["Can"]), ThamSo("@p", dongDuLieu["MaPhuTung"]));
                if (soDongCapNhat != 1)
                    throw new InvalidOperationException("Tồn kho đã thay đổi, hãy kiểm tra lại.");
                XuLyDuLieu.ThucThi(ketNoi, giaoDich, "INSERT dbo.PhuTungTiepNhan VALUES(@id,@p,@name,@q,@price)", ThamSo("@id", ma), ThamSo("@p", dongDuLieu["MaPhuTung"]), ThamSo("@name", dongDuLieu["TenPhuTung"]), ThamSo("@q", dongDuLieu["Can"]), ThamSo("@price", dongDuLieu["DonGia"]));
            }

            XuLyDuLieu.ThucThi(ketNoi, giaoDich, "UPDATE dbo.PhieuTiepNhan SET DaChotPhuTung=1 WHERE MaPhieuTiepNhan=@id", ThamSo("@id", ma));
        }

        public string TiepNhan(List<DichVuChon> cacDichVu, ThongTinNhanXe thongTin)
        {
            PhienDangNhap.YeuCau("TIEP_NHAN");
            if (thongTin != null && (thongTin.SoKm < 0 || thongTin.SoKm > 9999999))
                throw new InvalidOperationException("Số km phải từ 0 đến 9999999.");
            if (thongTin == null || string.IsNullOrWhiteSpace(thongTin.TenKhach) || thongTin.TenKhach.Length > 100 || string.IsNullOrWhiteSpace(thongTin.BienSo) || thongTin.BienSo.Length > 15 || string.IsNullOrWhiteSpace(thongTin.DienThoai) || !System.Text.RegularExpressions.Regex.IsMatch(thongTin.DienThoai, @"^\d{8,15}$") || string.IsNullOrWhiteSpace(thongTin.TinhTrang) || thongTin.TinhTrang.Length > 500 || thongTin.HenTra < DateTime.Now)
                throw new InvalidOperationException("Kiểm tra khách hàng, điện thoại, biển số, tình trạng và giờ hẹn trả.");
            return duLieu.TrongGiaoDich((ketNoi, giaoDich) =>
            {
                var dv = DichVu(ketNoi, giaoDich, cacDichVu);
                var bangPhuTung = PhuTung(ketNoi, giaoDich, cacDichVu);
                DuKho(bangPhuTung);
                KiemTraTho(ketNoi, giaoDich, thongTin.MaTho);
                var daTonTai = XuLyDuLieu.DocDuLieu(ketNoi, giaoDich, "SELECT MaXe,MaKhachHang FROM dbo.Xe WITH(UPDLOCK,HOLDLOCK) WHERE BienSo=@b", ThamSo("@b", thongTin.BienSo));
                string kh, xe;
                if (daTonTai.Rows.Count > 0)
                {
                    xe = daTonTai.Rows[0][0].ToString();
                    kh = daTonTai.Rows[0][1].ToString();
                    var chuSoHuu = XuLyDuLieu.DocDuLieu(ketNoi, giaoDich, "SELECT TenKhachHang,SoDienThoai FROM dbo.KhachHang WHERE MaKhachHang=@id", ThamSo("@id", kh));
                    if (!string.Equals(chuSoHuu.Rows[0][1].ToString(), thongTin.DienThoai, StringComparison.Ordinal) || !string.Equals(chuSoHuu.Rows[0][0].ToString().Trim(), thongTin.TenKhach.Trim(), StringComparison.OrdinalIgnoreCase))
                        throw new InvalidOperationException("Biển số đã thuộc khách hàng khác. Nhập đúng khách hiện có; việc chuyển chủ xe cần xử lý riêng.");
                }
                else
                {
                    kh = XuLyDuLieu.TaoMaMoi("KH");
                    xe = XuLyDuLieu.TaoMaMoi("XE");
                    XuLyDuLieu.ThucThi(ketNoi, giaoDich, "INSERT dbo.KhachHang(MaKhachHang,TenKhachHang,SoDienThoai) VALUES(@id,@name,@phone)", ThamSo("@id", kh), ThamSo("@name", thongTin.TenKhach), ThamSo("@phone", thongTin.DienThoai));
                    XuLyDuLieu.ThucThi(ketNoi, giaoDich, "INSERT dbo.Xe(MaXe,MaKhachHang,BienSo,DongXe,LoaiXe) VALUES(@id,@kh,@b,@d,@l)", ThamSo("@id", xe), ThamSo("@kh", kh), ThamSo("@b", thongTin.BienSo), ThamSo("@d", thongTin.DongXe), ThamSo("@l", thongTin.LoaiXe));
                }

                if (XuLyDuLieu.DocDuLieu(ketNoi, giaoDich, "SELECT 1 FROM dbo.PhieuTiepNhan WITH(UPDLOCK,HOLDLOCK) WHERE MaXe=@xe AND TrangThai NOT IN(N'Hoàn thành',N'Đã hủy')", ThamSo("@xe", xe)).Rows.Count > 0)
                    throw new InvalidOperationException("Xe đang có phiếu sửa chưa hoàn thành.");
                string ma = XuLyDuLieu.TaoMaMoi("TN");
                XuLyDuLieu.ThucThi(ketNoi, giaoDich, "INSERT dbo.PhieuTiepNhan(MaPhieuTiepNhan,MaKhachHang,MaXe,MaNhanVien,MoTaTinhTrangXe,HenTra,SoKm) VALUES(@id,@kh,@xe,@nv,@tt,@hen,@km)", ThamSo("@id", ma), ThamSo("@kh", kh), ThamSo("@xe", xe), ThamSo("@nv", PhienDangNhap.MaNhanVien), ThamSo("@tt", thongTin.TinhTrang), ThamSo("@hen", thongTin.HenTra), ThamSo("@km", thongTin.SoKm));
                decimal tienCong = 0;
                foreach (DataRow dongDuLieu in dv.Rows)
                {
                    XuLyDuLieu.ThucThi(ketNoi, giaoDich, "INSERT dbo.ChiTietPhieuTiepNhan(MaPhieuTiepNhan,MaDichVu,SoLuong,DonGiaDichVu,TenDichVu,LuongNhiemVu) VALUES(@id,@dv,@q,@price,@name,@w)", ThamSo("@id", ma), ThamSo("@dv", dongDuLieu["MaDichVu"]), ThamSo("@q", dongDuLieu["SoLuong"]), ThamSo("@price", dongDuLieu["DonGia"]), ThamSo("@name", dongDuLieu["TenDichVu"]), ThamSo("@w", dongDuLieu["Luong"]));
                    tienCong += Convert.ToDecimal(dongDuLieu["Luong"]) * Convert.ToInt32(dongDuLieu["SoLuong"]);
                }

                ChotKho(ketNoi, giaoDich, ma, bangPhuTung);
                XuLyDuLieu.ThucThi(ketNoi, giaoDich, "INSERT dbo.PhanCongTho(MaPhieuTiepNhan,MaNhanVien,VaiTro,TienCong) VALUES(@id,@nv,N'Phụ trách',@w)", ThamSo("@id", ma), ThamSo("@nv", thongTin.MaTho), ThamSo("@w", tienCong));
                return ma;
            });
        }

        public void ChotPhieuCu(string ma)
        {
            PhienDangNhap.YeuCau("NHAN_VIEN");
            duLieu.TrongGiaoDich<int>((ketNoi, giaoDich) =>
            {
                var dong = XuLyDuLieu.DocDuLieu(ketNoi, giaoDich, "SELECT DaChotPhuTung FROM dbo.PhieuTiepNhan WITH(UPDLOCK) WHERE MaPhieuTiepNhan=@id AND TrangThai<>N'Đã hủy' AND NOT EXISTS(SELECT 1 FROM dbo.HoaDon h WHERE h.MaPhieuTiepNhan=@id)", ThamSo("@id", ma));
                if (dong.Rows.Count != 1 || Convert.ToBoolean(dong.Rows[0][0]))
                    throw new InvalidOperationException("Phiếu đã chốt, đã hủy, đã có hóa đơn hoặc không tồn tại.");
                var bangDichVu = XuLyDuLieu.DocDuLieu(ketNoi, giaoDich, "SELECT MaDichVu,SoLuong FROM dbo.ChiTietPhieuTiepNhan WHERE MaPhieuTiepNhan=@id", ThamSo("@id", ma));
                var cacDichVu = bangDichVu.AsEnumerable().Select(dongDuLieu => new DichVuChon { Ma = dongDuLieu[0].ToString(), SoLuong = Convert.ToInt32(dongDuLieu[1]) }).ToList();
                if (cacDichVu.Count == 0)
                    throw new InvalidOperationException("Phiếu không có dịch vụ.");
                ChotKho(ketNoi, giaoDich, ma, PhuTung(ketNoi, giaoDich, cacDichVu));
                XuLyDuLieu.ThucThi(ketNoi, giaoDich, "UPDATE dbo.PhanCongTho SET TrangThaiCongViec=CASE WHEN ThoiGianBatDau IS NULL THEN N'Đã phân công' ELSE N'Đang sửa' END WHERE MaPhieuTiepNhan=@id AND TrangThaiCongViec=N'Chờ phụ tùng'", ThamSo("@id", ma));
                return 0;
            });
        }

        public void TrangThaiCongViec(string ma, string maTho, string trangThai)
        {
            PhienDangNhap.YeuCau("PHAN_CONG");
            bool chuSoHuu = PhanQuyenNguoiDung.CoQuyen(PhienDangNhap.VaiTro, "NHAN_VIEN");
            if (!chuSoHuu && maTho != PhienDangNhap.MaNhanVien)
                throw new UnauthorizedAccessException("Chỉ được cập nhật xe được giao cho mình.");
            if (trangThai != "Đang sửa" && trangThai != "Hoàn thành")
                throw new InvalidOperationException("Trạng thái không hợp lệ.");
            duLieu.TrongGiaoDich<int>((ketNoi, giaoDich) =>
            {
                var maPhieu = XuLyDuLieu.DocDuLieu(ketNoi, giaoDich, "SELECT DaChotPhuTung FROM dbo.PhieuTiepNhan WITH(UPDLOCK) WHERE MaPhieuTiepNhan=@id AND NOT EXISTS(SELECT 1 FROM dbo.HoaDon h WHERE h.MaPhieuTiepNhan=@id)", ThamSo("@id", ma));
                if (maPhieu.Rows.Count != 1 || !Convert.ToBoolean(maPhieu.Rows[0][0]))
                    throw new InvalidOperationException("Phiếu chưa chốt phụ tùng hoặc đã có hóa đơn.");
                var congViec = XuLyDuLieu.DocDuLieu(ketNoi, giaoDich, "SELECT TrangThaiCongViec FROM dbo.PhanCongTho WITH(UPDLOCK) WHERE MaPhieuTiepNhan=@id AND MaNhanVien=@nv", ThamSo("@id", ma), ThamSo("@nv", maTho));
                if (congViec.Rows.Count != 1 || (trangThai == "Đang sửa" && congViec.Rows[0][0].ToString() != "Đã phân công") || (trangThai == "Hoàn thành" && congViec.Rows[0][0].ToString() != "Đang sửa"))
                    throw new InvalidOperationException("Chỉ chuyển Đã phân công → Đang sửa → Hoàn thành.");
                XuLyDuLieu.ThucThi(ketNoi, giaoDich, "UPDATE dbo.PhanCongTho SET TrangThaiCongViec=@s,ThoiGianBatDau=COALESCE(ThoiGianBatDau,SYSDATETIME()),ThoiGianKetThuc=CASE WHEN @s=N'Hoàn thành' THEN SYSDATETIME() ELSE NULL END,NgayGhiNhanCong=CASE WHEN @s=N'Hoàn thành' THEN SYSDATETIME() ELSE NULL END WHERE MaPhieuTiepNhan=@id AND MaNhanVien=@nv", ThamSo("@s", trangThai), ThamSo("@id", ma), ThamSo("@nv", maTho));
                XuLyDuLieu.ThucThi(ketNoi, giaoDich, @"UPDATE dbo.PhieuTiepNhan SET TrangThai=CASE WHEN EXISTS(SELECT 1 FROM dbo.PhanCongTho WHERE MaPhieuTiepNhan=@id AND TrangThaiCongViec NOT IN(N'Hoàn thành',N'Đã hủy')) THEN N'Đang sửa' ELSE N'Hoàn thành' END,ThoiGianHoanThanh=CASE WHEN EXISTS(SELECT 1 FROM dbo.PhanCongTho WHERE MaPhieuTiepNhan=@id AND TrangThaiCongViec NOT IN(N'Hoàn thành',N'Đã hủy')) THEN NULL ELSE SYSDATETIME() END WHERE MaPhieuTiepNhan=@id", ThamSo("@id", ma));
                return 0;
            });
        }

        public string LapHoaDon(string maPhieu)
        {
            PhienDangNhap.YeuCau("HOA_DON");
            return duLieu.TrongGiaoDich((ketNoi, giaoDich) =>
            {
                var dongDuLieu = XuLyDuLieu.DocDuLieu(ketNoi, giaoDich, "SELECT TrangThai,DaChotPhuTung FROM dbo.PhieuTiepNhan WITH(UPDLOCK) WHERE MaPhieuTiepNhan=@id", ThamSo("@id", maPhieu));
                if (dongDuLieu.Rows.Count != 1 || dongDuLieu.Rows[0][0].ToString() != "Hoàn thành" || !Convert.ToBoolean(dongDuLieu.Rows[0][1]))
                    throw new InvalidOperationException("Phiếu phải hoàn thành và đã chốt phụ tùng.");
                if (XuLyDuLieu.DocDuLieu(ketNoi, giaoDich, "SELECT 1 FROM dbo.HoaDon WHERE MaPhieuTiepNhan=@id", ThamSo("@id", maPhieu)).Rows.Count > 0)
                    throw new InvalidOperationException("Phiếu đã có hóa đơn.");
                if (XuLyDuLieu.DocDuLieu(ketNoi, giaoDich, "SELECT 1 FROM dbo.ChiTietPhieuTiepNhan WHERE MaPhieuTiepNhan=@id", ThamSo("@id", maPhieu)).Rows.Count == 0)
                    throw new InvalidOperationException("Phiếu không có dịch vụ.");
                if (XuLyDuLieu.DocDuLieu(ketNoi, giaoDich, "SELECT 1 FROM dbo.PhanCongTho WHERE MaPhieuTiepNhan=@id AND TrangThaiCongViec NOT IN(N'Hoàn thành',N'Đã hủy')", ThamSo("@id", maPhieu)).Rows.Count > 0)
                    throw new InvalidOperationException("Còn công việc chưa hoàn thành.");
                string ma = XuLyDuLieu.TaoMaMoi("HD");
                XuLyDuLieu.ThucThi(ketNoi, giaoDich, @"DECLARE @dv DECIMAL(18,2)=(SELECT COALESCE(SUM(SoLuong*DonGiaDichVu),0) FROM dbo.ChiTietPhieuTiepNhan WHERE MaPhieuTiepNhan=@r),@pt DECIMAL(18,2)=(SELECT COALESCE(SUM(SoLuong*DonGia),0) FROM dbo.PhuTungTiepNhan WHERE MaPhieuTiepNhan=@r); INSERT dbo.HoaDon(MaHoaDon,MaPhieuTiepNhan,TongTienDichVu,TongTienPhuTung,TongTien,MaNhanVien,DaKhoaNoiDung) VALUES(@id,@r,@dv,@pt,@dv+@pt,@nv,0); INSERT dbo.ChiTietHoaDon(MaHoaDon,MaDichVu,TenDichVu,SoLuong,DonGia) SELECT @id,MaDichVu,TenDichVu,SoLuong,DonGiaDichVu FROM dbo.ChiTietPhieuTiepNhan WHERE MaPhieuTiepNhan=@r; UPDATE dbo.HoaDon SET DaKhoaNoiDung=1 WHERE MaHoaDon=@id;", ThamSo("@id", ma), ThamSo("@r", maPhieu), ThamSo("@nv", PhienDangNhap.MaNhanVien));
                return ma;
            });
        }

        public void ThanhToan(string ma, string phuongThuc)
        {
            PhienDangNhap.YeuCau("HOA_DON");
            if (phuongThuc != "Tiền mặt" && phuongThuc != "Chuyển khoản")
                throw new InvalidOperationException("Chọn phương thức thanh toán.");
            if (duLieu.CapNhatDuLieu("UPDATE dbo.HoaDon SET TrangThaiThanhToan=N'Đã thanh toán',PhuongThucThanhToan=@m WHERE MaHoaDon=@id AND TrangThaiThanhToan=N'Chưa thanh toán'", ThamSo("@m", phuongThuc), ThamSo("@id", ma)) != 1)
                throw new InvalidOperationException("Hóa đơn không tồn tại hoặc đã thanh toán.");
        }

        public void HuyChuaSua(string ma)
        {
            PhienDangNhap.YeuCau("NHAN_VIEN");
            duLieu.TrongGiaoDich<int>((ketNoi, giaoDich) =>
            {
                var dongDuLieu = XuLyDuLieu.DocDuLieu(ketNoi, giaoDich, "SELECT DaChotPhuTung FROM dbo.PhieuTiepNhan WITH(UPDLOCK) WHERE MaPhieuTiepNhan=@id AND TrangThai=N'Mới tiếp nhận' AND NOT EXISTS(SELECT 1 FROM dbo.HoaDon WHERE MaPhieuTiepNhan=@id)", ThamSo("@id", ma));
                if (dongDuLieu.Rows.Count != 1 || XuLyDuLieu.DocDuLieu(ketNoi, giaoDich, "SELECT 1 FROM dbo.PhanCongTho WITH(UPDLOCK) WHERE MaPhieuTiepNhan=@id AND TrangThaiCongViec<>N'Đã phân công'", ThamSo("@id", ma)).Rows.Count > 0)
                    throw new InvalidOperationException("Chỉ hủy phiếu mới tiếp nhận và chưa bắt đầu sửa.");
                if (Convert.ToBoolean(dongDuLieu.Rows[0][0]))
                    XuLyDuLieu.ThucThi(ketNoi, giaoDich, "UPDATE p SET SoLuongTon=p.SoLuongTon+s.SoLuong FROM dbo.PhuTung p JOIN dbo.PhuTungTiepNhan s ON s.MaPhuTung=p.MaPhuTung WHERE s.MaPhieuTiepNhan=@id", ThamSo("@id", ma));
                XuLyDuLieu.ThucThi(ketNoi, giaoDich, "UPDATE dbo.PhanCongTho SET TrangThaiCongViec=N'Đã hủy' WHERE MaPhieuTiepNhan=@id; UPDATE dbo.PhieuTiepNhan SET TrangThai=N'Đã hủy' WHERE MaPhieuTiepNhan=@id", ThamSo("@id", ma));
                return 0;
            });
        }
    }
}
