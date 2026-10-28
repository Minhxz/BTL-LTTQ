using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;

namespace QLyCuaHangSuaXe.LopDungChung
{
    public sealed class DichVuChon
    {
        public string Ma;
        public int SoLuong;
    }

    public sealed class ThongTinNhanXe
    {
        public string TenKhach, DienThoai, BienSo, DongXe, LoaiXe, MaTho, MauSac, MaPhieu;
        public DateTime HenTra;
    }

    public class NghiepVuSuaXe
    {
        readonly XuLyDuLieu duLieu = new XuLyDuLieu();
        static SqlParameter ThamSo(string ten, object giaTri) { return XuLyDuLieu.ThamSo(ten, giaTri); }

        static DataTable DichVu(SqlConnection ketNoi, SqlTransaction giaoDich, List<DichVuChon> cacDichVu)
        {
            if (cacDichVu == null || cacDichVu.Count == 0 || cacDichVu.Select(d => d.Ma).Distinct().Count() != cacDichVu.Count || cacDichVu.Any(d => d.SoLuong <= 0 || d.SoLuong > 1000))
                throw new InvalidOperationException("Chọn ít nhất một dịch vụ; số lượng từ 1 đến 1000 và không trùng dịch vụ.");
            var bang = new DataTable();
            bang.Columns.Add("MaDichVu"); bang.Columns.Add("TenDichVu");
            bang.Columns.Add("SoLuong", typeof(int)); bang.Columns.Add("DonGia", typeof(decimal));
            foreach (var chon in cacDichVu.OrderBy(d => d.Ma))
            {
                var dv = XuLyDuLieu.DocDuLieu(ketNoi, giaoDich, "SELECT TenDichVu,ThanhTienDichVu FROM dbo.DichVu WITH(UPDLOCK,HOLDLOCK) WHERE MaDichVu=@id AND TrangThai=N'Đang áp dụng'", ThamSo("@id", chon.Ma));
                if (dv.Rows.Count != 1) throw new InvalidOperationException("Dịch vụ đã ngừng áp dụng hoặc không tồn tại.");
                bang.Rows.Add(chon.Ma, dv.Rows[0][0], chon.SoLuong, dv.Rows[0][1]);
            }
            return bang;
        }

        static DataTable PhuTung(SqlConnection ketNoi, SqlTransaction giaoDich, List<DichVuChon> cacDichVu)
        {
            var nhuCau = new SortedDictionary<string, int>();
            foreach (var chon in cacDichVu.OrderBy(d => d.Ma))
            {
                var dm = XuLyDuLieu.DocDuLieu(ketNoi, giaoDich, "SELECT MaPhuTung,SoLuong FROM dbo.ChiTietDichVu WITH(HOLDLOCK) WHERE MaDichVu=@id", ThamSo("@id", chon.Ma));
                foreach (DataRow dong in dm.Rows)
                {
                    string ma = dong[0].ToString();
                    if (!nhuCau.ContainsKey(ma)) nhuCau[ma] = 0;
                    nhuCau[ma] = checked(nhuCau[ma] + Convert.ToInt32(dong[1]) * chon.SoLuong);
                }
            }
            var bang = new DataTable();
            bang.Columns.Add("MaPhuTung"); bang.Columns.Add("TenPhuTung"); bang.Columns.Add("Can", typeof(int));
            bang.Columns.Add("Ton", typeof(int)); bang.Columns.Add("DonGia", typeof(decimal)); bang.Columns.Add("TrangThai");
            bang.Columns.Add("HanSuDung", typeof(DateTime));
            foreach (var cap in nhuCau)
            {
                var pt = XuLyDuLieu.DocDuLieu(ketNoi, giaoDich, "SELECT TenPhuTung,SoLuongTon,DonGiaBan,TrangThai,HanSuDung FROM dbo.PhuTung WITH(UPDLOCK,HOLDLOCK) WHERE MaPhuTung=@id", ThamSo("@id", cap.Key));
                if (pt.Rows.Count != 1) throw new InvalidOperationException("Không tìm thấy phụ tùng " + cap.Key);
                bang.Rows.Add(cap.Key, pt.Rows[0][0], cap.Value, pt.Rows[0][1], pt.Rows[0][2], pt.Rows[0][3], pt.Rows[0][4]);
            }
            return bang;
        }

        static void DuKho(DataTable bang)
        {
            var thieu = bang.AsEnumerable().Where(d => d.Field<int>("Can") > d.Field<int>("Ton") || d.Field<string>("TrangThai") != "Đang kinh doanh" || d.Field<DateTime>("HanSuDung").Date < DateTime.Today)
                .Select(d => d["TenPhuTung"] + ": cần " + d["Can"] + ", tồn " + d["Ton"] + (d.Field<DateTime>("HanSuDung").Date < DateTime.Today ? " (hết hạn)" : " (" + d["TrangThai"] + ")"));
            if (thieu.Any()) throw new InvalidOperationException("Phụ tùng chưa đủ điều kiện:\n" + string.Join("\n", thieu));
        }

        public DataTable KiemTraKho(List<DichVuChon> cacDichVu)
        {
            PhienDangNhap.YeuCau("TIEP_NHAN");
            return duLieu.TrongGiaoDich((kn, gd) => { DichVu(kn, gd, cacDichVu); var pt = PhuTung(kn, gd, cacDichVu); DuKho(pt); return pt; });
        }

        public DataTable ThoRanh()
        {
            PhienDangNhap.YeuCau(PhanQuyenNguoiDung.CoQuyen(PhienDangNhap.VaiTro, "NHAN_VIEN") ? "NHAN_VIEN" : "TIEP_NHAN");
            return duLieu.DocBang(@"SELECT n.MaNhanVien,n.HoTen+N' — '+CAST((CASE WHEN n.MaChucVu='CV002' THEN 3 ELSE 2 END)-COUNT(DISTINCT p.MaXe) AS NVARCHAR(10))+N' chỗ trống' AS HienThi
                FROM dbo.NhanVien n LEFT JOIN dbo.ChiTietPhanCong c ON c.MaNhanVien=n.MaNhanVien
                LEFT JOIN dbo.PhieuTiepNhan p ON p.MaXe=c.MaXe AND p.TrangThai IN(N'Chờ sửa',N'Đang sửa')
                WHERE n.TrangThai=N'Đang làm' AND n.MaChucVu IN('CV002','CV003','CV004')
                GROUP BY n.MaNhanVien,n.HoTen,n.MaChucVu HAVING COUNT(DISTINCT p.MaXe)<CASE WHEN n.MaChucVu='CV002' THEN 3 ELSE 2 END ORDER BY n.HoTen");
        }

        static void KiemTraTho(SqlConnection kn, SqlTransaction gd, string ma)
        {
            var nv = XuLyDuLieu.DocDuLieu(kn, gd, "SELECT CASE WHEN MaChucVu='CV002' THEN 3 ELSE 2 END FROM dbo.NhanVien WITH(UPDLOCK,HOLDLOCK) WHERE MaNhanVien=@id AND TrangThai=N'Đang làm' AND MaChucVu IN('CV002','CV003','CV004')", ThamSo("@id", ma));
            if (nv.Rows.Count != 1) throw new InvalidOperationException("Chọn thợ đang làm việc.");
            var dem = XuLyDuLieu.DocDuLieu(kn, gd, "SELECT COUNT(DISTINCT c.MaXe) FROM dbo.ChiTietPhanCong c WITH(UPDLOCK,HOLDLOCK) JOIN dbo.PhieuTiepNhan p ON p.MaXe=c.MaXe WHERE c.MaNhanVien=@id AND p.TrangThai IN(N'Chờ sửa',N'Đang sửa')", ThamSo("@id", ma));
            if (Convert.ToInt32(dem.Rows[0][0]) >= Convert.ToInt32(nv.Rows[0][0])) throw new InvalidOperationException("Thợ đã nhận đủ xe. Hãy chọn thợ khác.");
        }

        public string TiepNhan(List<DichVuChon> cacDichVu, ThongTinNhanXe thongTin)
        {
            PhienDangNhap.YeuCau("TIEP_NHAN");
            if (thongTin == null || string.IsNullOrWhiteSpace(thongTin.TenKhach) || thongTin.TenKhach.Length > 100 || string.IsNullOrWhiteSpace(thongTin.BienSo) || thongTin.BienSo.Length > 20 || !Regex.IsMatch(thongTin.DienThoai ?? "", @"^\d{8,15}$") || string.IsNullOrWhiteSpace(thongTin.DongXe) || thongTin.DongXe.Length > 50 || string.IsNullOrWhiteSpace(thongTin.MauSac) || thongTin.MauSac.Length > 30 || (string.IsNullOrWhiteSpace(thongTin.LoaiXe) || thongTin.LoaiXe.Length > 50) || thongTin.HenTra.Date < DateTime.Today)
                throw new InvalidOperationException("Kiểm tra khách hàng, điện thoại, biển số, hãng xe, màu sắc và ngày hẹn trả.");
            return duLieu.TrongGiaoDich((kn, gd) =>
            {
                var dv = DichVu(kn, gd, cacDichVu); var pt = PhuTung(kn, gd, cacDichVu); DuKho(pt); KiemTraTho(kn, gd, thongTin.MaTho);
                if (XuLyDuLieu.DocDuLieu(kn, gd, "SELECT 1 FROM dbo.Xe x WITH(UPDLOCK,HOLDLOCK) JOIN dbo.PhieuTiepNhan p ON p.MaXe=x.MaXe WHERE x.BienXe=@b AND p.TrangThai IN(N'Chờ sửa',N'Đang sửa')", ThamSo("@b", thongTin.BienSo.Trim())).Rows.Count > 0)
                    throw new InvalidOperationException("Xe đang có phiếu sửa chưa hoàn thành.");
                var khach = XuLyDuLieu.DocDuLieu(kn, gd, "SELECT MaKhachHang,TenKhachHang FROM dbo.KhachHang WITH(UPDLOCK,HOLDLOCK) WHERE SoDienThoai=@dt", ThamSo("@dt", thongTin.DienThoai));
                string kh;
                if (khach.Rows.Count == 0)
                {
                    kh = XuLyDuLieu.TaoMaMoi(kn, gd, "KH");
                    XuLyDuLieu.ThucThi(kn, gd, "INSERT dbo.KhachHang(MaKhachHang,TenKhachHang,SoDienThoai) VALUES(@id,@ten,@dt)", ThamSo("@id", kh), ThamSo("@ten", thongTin.TenKhach.Trim()), ThamSo("@dt", thongTin.DienThoai));
                }
                else
                {
                    kh = khach.Rows[0][0].ToString();
                    if (!string.Equals(khach.Rows[0][1].ToString().Trim(), thongTin.TenKhach.Trim(), StringComparison.CurrentCultureIgnoreCase)) throw new InvalidOperationException("Số điện thoại đã thuộc khách " + khach.Rows[0][1] + ". Hãy nhập đúng tên khách.");
                }
                string xe = XuLyDuLieu.TaoMaMoi(kn, gd, "XE");
                XuLyDuLieu.ThucThi(kn, gd, "INSERT dbo.Xe(MaXe,MaKhachHang,BienXe,LoaiXe,HangSanXuat,MauSac) VALUES(@id,@kh,@b,@l,@h,@m)", ThamSo("@id", xe), ThamSo("@kh", kh), ThamSo("@b", thongTin.BienSo.Trim().ToUpperInvariant()), ThamSo("@l", thongTin.LoaiXe), ThamSo("@h", thongTin.DongXe.Trim()), ThamSo("@m", thongTin.MauSac.Trim()));
                string ma = string.IsNullOrEmpty(thongTin.MaPhieu) ? XuLyDuLieu.TaoMaMoi(kn, gd, "PTN") : XuLyDuLieu.KiemTraMa(thongTin.MaPhieu, "PTN");
                XuLyDuLieu.ThucThi(kn, gd, "INSERT dbo.PhieuTiepNhan(MaPhieuTiepNhan,MaXe,MaNhanVien,NgayLap,NgayHen,TrangThai) VALUES(@id,@xe,@nv,@ngay,@hen,N'Chờ sửa')", ThamSo("@id", ma), ThamSo("@xe", xe), ThamSo("@nv", PhienDangNhap.MaNhanVien), ThamSo("@ngay", DateTime.Today), ThamSo("@hen", thongTin.HenTra.Date));
                foreach (DataRow dong in dv.Rows)
                    XuLyDuLieu.ThucThi(kn, gd, "INSERT dbo.ChiTietPhieuTiepNhan(MaPhieuTiepNhan,MaDichVu,SoLuong,ThanhTien) VALUES(@id,@dv,@sl,@tt)", ThamSo("@id", ma), ThamSo("@dv", dong["MaDichVu"]), ThamSo("@sl", dong["SoLuong"]), ThamSo("@tt", Convert.ToDecimal(dong["DonGia"]) * Convert.ToInt32(dong["SoLuong"])));
                foreach (DataRow dong in pt.Rows)
                    if (XuLyDuLieu.ThucThi(kn, gd, "UPDATE dbo.PhuTung SET SoLuongTon=SoLuongTon-@sl WHERE MaPhuTung=@id AND SoLuongTon>=@sl AND TrangThai=N'Đang kinh doanh' AND HanSuDung>=@ngay", ThamSo("@id", dong["MaPhuTung"]), ThamSo("@sl", dong["Can"]), ThamSo("@ngay", DateTime.Today)) != 1) throw new InvalidOperationException("Tồn kho đã thay đổi. Hãy kiểm tra lại.");
                DateTime batDau = DateTime.Now; DateTime ketThuc = thongTin.HenTra.Date.AddDays(1);
                if (ketThuc <= batDau) ketThuc = batDau.AddHours(1);
                XuLyDuLieu.ThucThi(kn, gd, "INSERT dbo.ChiTietPhanCong(MaNhanVien,MaXe,ThoiGianBatDau,ThoiGianHoanThanh) VALUES(@nv,@xe,@bd,@kt)", ThamSo("@nv", thongTin.MaTho), ThamSo("@xe", xe), ThamSo("@bd", batDau), ThamSo("@kt", ketThuc));
                return ma;
            });
        }

        public void ChotPhieuCu(string ma) { throw new InvalidOperationException("Phụ tùng được kiểm tra và trừ kho ngay khi lưu phiếu tiếp nhận."); }

        public void TrangThaiCongViec(string ma, string maTho, string trangThai)
        {
            PhienDangNhap.YeuCau("PHAN_CONG");
            if (!PhanQuyenNguoiDung.CoQuyen(PhienDangNhap.VaiTro, "NHAN_VIEN") && maTho != PhienDangNhap.MaNhanVien) throw new UnauthorizedAccessException("Chỉ được cập nhật xe được giao cho mình.");
            if (trangThai != "Đang sửa" && trangThai != "Hoàn thành") throw new InvalidOperationException("Trạng thái không hợp lệ.");
            duLieu.TrongGiaoDich<int>((kn, gd) =>
            {
                var phieu = XuLyDuLieu.DocDuLieu(kn, gd, "SELECT p.MaXe,p.TrangThai FROM dbo.PhieuTiepNhan p WITH(UPDLOCK,HOLDLOCK) JOIN dbo.ChiTietPhanCong c ON c.MaXe=p.MaXe AND c.MaNhanVien=@nv WHERE p.MaPhieuTiepNhan=@id", ThamSo("@nv", maTho), ThamSo("@id", ma));
                if (phieu.Rows.Count != 1 || phieu.Rows[0][1].ToString() != (trangThai == "Đang sửa" ? "Chờ sửa" : "Đang sửa")) throw new InvalidOperationException("Chỉ chuyển Chờ sửa → Đang sửa → Hoàn thành.");
                if (trangThai == "Hoàn thành") XuLyDuLieu.ThucThi(kn, gd, "UPDATE dbo.ChiTietPhanCong SET ThoiGianHoanThanh=CASE WHEN SYSDATETIME()>DATEADD(SECOND,1,ThoiGianBatDau) THEN SYSDATETIME() ELSE DATEADD(SECOND,1,ThoiGianBatDau) END WHERE MaXe=@xe", ThamSo("@xe", phieu.Rows[0][0]));
                XuLyDuLieu.ThucThi(kn, gd, "UPDATE dbo.PhieuTiepNhan SET TrangThai=@tt WHERE MaPhieuTiepNhan=@id", ThamSo("@tt", trangThai), ThamSo("@id", ma));
                return 0;
            });
        }

        public void SuaPhieu(string ma, DateTime henTra, string trangThaiMoi, string maTho)
        {
            PhienDangNhap.YeuCau("TIEP_NHAN");
            duLieu.TrongGiaoDich<int>((kn, gd) =>
            {
                var bang = XuLyDuLieu.DocDuLieu(kn, gd, "SELECT MaXe,NgayLap,TrangThai FROM dbo.PhieuTiepNhan WITH(UPDLOCK,HOLDLOCK) WHERE MaPhieuTiepNhan=@id", ThamSo("@id", ma));
                if (bang.Rows.Count != 1 || !new[] { "Chờ sửa", "Đang sửa" }.Contains(bang.Rows[0]["TrangThai"].ToString())) throw new InvalidOperationException("Phiếu đã hoàn thành hoặc đã hủy, không thể sửa.");
                if (henTra.Date < Convert.ToDateTime(bang.Rows[0]["NgayLap"]).Date) throw new InvalidOperationException("Ngày hẹn không được trước ngày nhận.");
                string trangThaiCu = bang.Rows[0]["TrangThai"].ToString();
                if (trangThaiMoi != trangThaiCu)
                {
                    PhienDangNhap.YeuCau("PHAN_CONG");
                    if (!PhanQuyenNguoiDung.CoQuyen(PhienDangNhap.VaiTro, "NHAN_VIEN") && maTho != PhienDangNhap.MaNhanVien) throw new UnauthorizedAccessException("Chỉ được cập nhật xe được giao cho mình.");
                    if (!(trangThaiCu == "Chờ sửa" && trangThaiMoi == "Đang sửa" || trangThaiCu == "Đang sửa" && trangThaiMoi == "Hoàn thành")) throw new InvalidOperationException("Chỉ chuyển Chờ sửa → Đang sửa → Hoàn thành.");
                    if (XuLyDuLieu.DocDuLieu(kn, gd, "SELECT 1 FROM dbo.ChiTietPhanCong WHERE MaXe=@xe AND MaNhanVien=@nv", ThamSo("@xe", bang.Rows[0]["MaXe"]), ThamSo("@nv", maTho)).Rows.Count != 1) throw new InvalidOperationException("Phiếu chưa được giao cho thợ này.");
                    if (trangThaiMoi == "Hoàn thành") XuLyDuLieu.ThucThi(kn, gd, "UPDATE dbo.ChiTietPhanCong SET ThoiGianHoanThanh=CASE WHEN SYSDATETIME()>DATEADD(SECOND,1,ThoiGianBatDau) THEN SYSDATETIME() ELSE DATEADD(SECOND,1,ThoiGianBatDau) END WHERE MaXe=@xe", ThamSo("@xe", bang.Rows[0]["MaXe"]));
                }
                XuLyDuLieu.ThucThi(kn, gd, "UPDATE dbo.PhieuTiepNhan SET NgayHen=@hen,TrangThai=@tt WHERE MaPhieuTiepNhan=@id", ThamSo("@hen", henTra.Date), ThamSo("@tt", trangThaiMoi), ThamSo("@id", ma));
                return 0;
            });
        }

        public string LapHoaDon(string maPhieu)
        {
            PhienDangNhap.YeuCau("HOA_DON");
            var phieu = duLieu.DocBang("SELECT h.MaHoaDon FROM dbo.PhieuTiepNhan p LEFT JOIN dbo.HoaDon h ON h.MaPhieuTiepNhan=p.MaPhieuTiepNhan WHERE p.MaPhieuTiepNhan=@id AND p.TrangThai=N'Hoàn thành'", ThamSo("@id", maPhieu));
            if (phieu.Rows.Count != 1) throw new InvalidOperationException("Phiếu phải hoàn thành trước khi lập hóa đơn.");
            return phieu.Rows[0][0] == DBNull.Value ? XuLyDuLieu.TaoMaMoi("HD") : phieu.Rows[0][0].ToString();
        }

        public void DongBoHoaDonHoanThanh() { PhienDangNhap.YeuCau("HOA_DON"); }

        public string ThanhToanPhieu(string maPhieu, string phuongThuc, string maHoaDonDuKien = null)
        {
            PhienDangNhap.YeuCau("HOA_DON");
            if (phuongThuc != "Tiền mặt" && phuongThuc != "Chuyển khoản") throw new InvalidOperationException("Chọn phương thức thanh toán.");
            return duLieu.TrongGiaoDich((kn, gd) =>
            {
                var phieu = XuLyDuLieu.DocDuLieu(kn, gd, "SELECT NgayLap FROM dbo.PhieuTiepNhan WITH(UPDLOCK,HOLDLOCK) WHERE MaPhieuTiepNhan=@id AND TrangThai=N'Hoàn thành'", ThamSo("@id", maPhieu));
                if (phieu.Rows.Count != 1) throw new InvalidOperationException("Phiếu chưa hoàn thành.");
                if (XuLyDuLieu.DocDuLieu(kn, gd, "SELECT 1 FROM dbo.HoaDon WITH(UPDLOCK,HOLDLOCK) WHERE MaPhieuTiepNhan=@id", ThamSo("@id", maPhieu)).Rows.Count > 0) throw new InvalidOperationException("Phiếu đã được thanh toán.");
                var tong = XuLyDuLieu.DocDuLieu(kn, gd, "SELECT COUNT(*),COALESCE(SUM(ThanhTien),0) FROM dbo.ChiTietPhieuTiepNhan WHERE MaPhieuTiepNhan=@id", ThamSo("@id", maPhieu));
                if (Convert.ToInt32(tong.Rows[0][0]) == 0) throw new InvalidOperationException("Phiếu không có dịch vụ.");
                string ma = string.IsNullOrEmpty(maHoaDonDuKien) ? XuLyDuLieu.TaoMaMoi(kn, gd, "HD") : XuLyDuLieu.KiemTraMa(maHoaDonDuKien, "HD");
                if (XuLyDuLieu.DocDuLieu(kn, gd, "SELECT 1 FROM dbo.HoaDon WITH(UPDLOCK,HOLDLOCK) WHERE MaHoaDon=@id", ThamSo("@id", ma)).Rows.Count > 0) throw new InvalidOperationException("Mã hóa đơn đã được sử dụng trên máy khác. Hãy tải lại hóa đơn trước khi nhận tiền.");
                DateTime ngay = DateTime.Today < Convert.ToDateTime(phieu.Rows[0][0]) ? Convert.ToDateTime(phieu.Rows[0][0]) : DateTime.Today;
                XuLyDuLieu.ThucThi(kn, gd, "INSERT dbo.HoaDon(MaHoaDon,MaPhieuTiepNhan,TienThanhToan,NgayLap,NgayThanhToan) VALUES(@id,@ptn,@tien,@ngay,@ngay)", ThamSo("@id", ma), ThamSo("@ptn", maPhieu), ThamSo("@tien", tong.Rows[0][1]), ThamSo("@ngay", ngay));
                return ma;
            });
        }

        public void ThanhToan(string ma, string phuongThuc)
        {
            if (ma != null && ma.StartsWith("PTN", StringComparison.OrdinalIgnoreCase)) { ThanhToanPhieu(ma, phuongThuc); return; }
            throw new InvalidOperationException("Hóa đơn đã thu tiền. Chọn phiếu hoàn thành chưa thanh toán.");
        }

        public void HuyChuaSua(string ma)
        {
            PhienDangNhap.YeuCau("NHAN_VIEN");
            duLieu.TrongGiaoDich<int>((kn, gd) =>
            {
                var phieu = XuLyDuLieu.DocDuLieu(kn, gd, "SELECT MaXe FROM dbo.PhieuTiepNhan WITH(UPDLOCK,HOLDLOCK) WHERE MaPhieuTiepNhan=@id AND TrangThai=N'Chờ sửa'", ThamSo("@id", ma));
                if (phieu.Rows.Count != 1) throw new InvalidOperationException("Chỉ hủy phiếu đang chờ sửa.");
                XuLyDuLieu.ThucThi(kn, gd, "UPDATE p SET SoLuongTon=p.SoLuongTon+n.SoLuong FROM dbo.PhuTung p JOIN (SELECT d.MaPhuTung,SUM(c.SoLuong*d.SoLuong) SoLuong FROM dbo.ChiTietPhieuTiepNhan c JOIN dbo.ChiTietDichVu d ON d.MaDichVu=c.MaDichVu WHERE c.MaPhieuTiepNhan=@id GROUP BY d.MaPhuTung) n ON n.MaPhuTung=p.MaPhuTung", ThamSo("@id", ma));
                XuLyDuLieu.ThucThi(kn, gd, "UPDATE dbo.PhieuTiepNhan SET TrangThai=N'Đã hủy' WHERE MaPhieuTiepNhan=@id", ThamSo("@id", ma));
                return 0;
            });
        }
    }
}
