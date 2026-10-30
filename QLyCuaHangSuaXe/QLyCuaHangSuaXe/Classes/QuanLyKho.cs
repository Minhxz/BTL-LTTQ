using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace QLyCuaHangSuaXe.LopDungChung
{
    public static class QuanLyKho
    {
        const decimal GioiHanTien = 999999999999999999m;

        public static void GhiSo(string maPhieu, string ghiChu, DataTable chiTiet)
        {
            GhiSo(maPhieu, DateTime.Today, ghiChu, chiTiet);
        }

        public static void GhiSo(string maPhieu, DateTime ngayNhap, string ghiChu, DataTable chiTiet)
        {
            GhiSo(maPhieu, ngayNhap, ghiChu, chiTiet, false);
        }

        public static void GhiSo(string maPhieu, DateTime ngayNhap, string ghiChu, DataTable chiTiet, bool chapNhanDuoiGiaNhap)
        {
            PhienDangNhap.YeuCau("NHAP_KHO");
            var cacDong = KiemTraPhieu(maPhieu, ngayNhap, ghiChu, chiTiet, chapNhanDuoiGiaNhap);
            new XuLyDuLieu().TrongGiaoDich<int>((ketNoi, giaoDich) =>
            {
                ThemPhieuNhap(ketNoi, giaoDich, maPhieu, ngayNhap, ghiChu, TongTien(cacDong));
                foreach (DataRow dong in cacDong.AsEnumerable().OrderBy(d => d["MaPhuTung"].ToString(), StringComparer.Ordinal))
                {
                    NhapPhuTung(ketNoi, giaoDich, maPhieu, ngayNhap, dong);
                    ThemChiTiet(ketNoi, giaoDich, maPhieu, dong);
                }
                return 0;
            });
        }

        static void ThemPhieuNhap(SqlConnection ketNoi, SqlTransaction giaoDich, string maPhieu, DateTime ngayNhap, string ghiChu, decimal tongTien)
        {
            const string sql = "INSERT dbo.PhieuNhap(MaPhieuNhap,MaNhanVienNhap,NgayNhap,GhiChu,TongTienNhap) VALUES(@ma,@nv,@ngay,@ghiChu,@tong)";
            XuLyDuLieu.ThucThi(ketNoi, giaoDich, sql,
                XuLyDuLieu.ThamSo("@ma", maPhieu),
                XuLyDuLieu.ThamSo("@nv", PhienDangNhap.MaNhanVien),
                XuLyDuLieu.ThamSo("@ngay", ngayNhap.Date),
                XuLyDuLieu.ThamSo("@ghiChu", ghiChu ?? ""),
                XuLyDuLieu.ThamSo("@tong", tongTien));
        }

        static void NhapPhuTung(SqlConnection ketNoi, SqlTransaction giaoDich, string maPhieu, DateTime ngayNhap, DataRow dong)
        {
            DataRow phuTung = LayPhuTung(ketNoi, giaoDich, dong);
            int soLuongNhap = Convert.ToInt32(dong["SoLuongNhap"]);
            if (phuTung == null)
            {
                ThemPhuTung(ketNoi, giaoDich, dong, soLuongNhap);
                return;
            }

            KiemTraNhapThem(phuTung, ngayNhap.Date);
            bool coGiaGoc = dong.Table.Columns.Contains("GiaNhapGoc") && dong.Table.Columns.Contains("GiaBanGoc");
            KiemTraGiaKhongThayDoi(phuTung, dong, coGiaGoc);

            decimal giaNhap = Convert.ToDecimal(dong["DonGiaNhap"]);
            decimal giaBan = Convert.ToDecimal(dong["DonGiaBan"]);
            decimal giaBanCu = Convert.ToDecimal(phuTung["DonGiaBan"]);
            string sql = coGiaGoc
                ? "UPDATE dbo.PhuTung SET DonGiaNhap=@gn,DonGiaBan=@gb,SoLuongTon=SoLuongTon+@sl WHERE MaPhuTung=@ma"
                : "UPDATE dbo.PhuTung SET SoLuongTon=SoLuongTon+@sl WHERE MaPhuTung=@ma";
            XuLyDuLieu.ThucThi(ketNoi, giaoDich, sql,
                XuLyDuLieu.ThamSo("@ma", dong["MaPhuTung"]),
                XuLyDuLieu.ThamSo("@gn", giaNhap),
                XuLyDuLieu.ThamSo("@gb", giaBan),
                XuLyDuLieu.ThamSo("@sl", soLuongNhap));

            if (coGiaGoc && giaBanCu != giaBan)
                GhiDoiGia(ketNoi, giaoDich, dong["MaPhuTung"].ToString(), giaBanCu, giaBan, "Nhập kho theo phiếu " + maPhieu);
        }

        static void KiemTraGiaKhongThayDoi(DataRow phuTung, DataRow dong, bool coGiaGoc)
        {
            if (!coGiaGoc)
                return;

            bool giaNhapDaDoi = Convert.ToDecimal(phuTung["DonGiaNhap"]) != Convert.ToDecimal(dong["GiaNhapGoc"]);
            bool giaBanDaDoi = Convert.ToDecimal(phuTung["DonGiaBan"]) != Convert.ToDecimal(dong["GiaBanGoc"]);
            if (giaNhapDaDoi || giaBanDaDoi)
                throw new InvalidOperationException("Giá phụ tùng " + dong["MaPhuTung"] + " vừa được thay đổi. Tải lại phụ tùng rồi lập lại dòng nhập để tránh ghi đè giá mới.");
        }

        public static DataSet DocPhieuNhap(string maPhieu)
        {
            PhienDangNhap.YeuCau("NHAP_KHO");
            XuLyDuLieu.KiemTraMa(maPhieu, "PN");
            return new XuLyDuLieu().TrongGiaoDich((ketNoi, giaoDich) => DocPhieuNhap(ketNoi, giaoDich, maPhieu));
        }

        static DataSet DocPhieuNhap(SqlConnection ketNoi, SqlTransaction giaoDich, string maPhieu)
        {
            var phieu = new DataSet();
            var thongTin = XuLyDuLieu.DocDuLieu(ketNoi, giaoDich,
                "SELECT MaPhieuNhap,MaNhanVienNhap,NgayNhap,GhiChu,TongTienNhap FROM dbo.PhieuNhap WITH(UPDLOCK,HOLDLOCK) WHERE MaPhieuNhap=@ma", XuLyDuLieu.ThamSo("@ma", maPhieu));
            if (thongTin.Rows.Count != 1)
                throw new InvalidOperationException("Phiếu nhập không còn tồn tại. Tải lại danh sách.");
            thongTin.TableName = "ThongTin";
            var chiTiet = XuLyDuLieu.DocDuLieu(ketNoi, giaoDich,
                "SELECT c.MaPhuTung,p.TenPhuTung,p.MaNhaCungCap,n.TenNhaCungCap,c.SoLuongNhap,CAST(c.ThanhTien/c.SoLuongNhap AS decimal(18,0)) AS DonGiaNhap,p.DonGiaBan,c.ThanhTien,p.HanSuDung,CAST(0 AS bit) AS LaPhuTungMoi,p.DonGiaNhap AS GiaNhapGoc,p.DonGiaBan AS GiaBanGoc FROM dbo.ChiTietPhieuNhap c WITH(UPDLOCK,HOLDLOCK) JOIN dbo.PhuTung p ON p.MaPhuTung=c.MaPhuTung JOIN dbo.NhaCungCap n ON n.MaNhaCungCap=p.MaNhaCungCap WHERE c.MaPhieuNhap=@ma ORDER BY c.MaPhuTung", XuLyDuLieu.ThamSo("@ma", maPhieu));
            chiTiet.TableName = "ChiTiet";
            phieu.Tables.Add(thongTin);
            phieu.Tables.Add(chiTiet);
            phieu.ExtendedProperties["DauVet"] = DauVet(thongTin, chiTiet);
            return phieu;
        }

        static string DauVet(DataTable thongTin, DataTable chiTiet)
        {
            var chuoi = new StringBuilder();
            foreach (DataColumn cot in thongTin.Columns)
            {
                string giaTri = Convert.ToString(thongTin.Rows[0][cot], CultureInfo.InvariantCulture);
                chuoi.Append(giaTri.Length).Append(':').Append(giaTri);
            }
            foreach (DataRow dong in chiTiet.Rows)
                foreach (string cot in new[] { "MaPhuTung", "SoLuongNhap", "ThanhTien" })
                {
                    string giaTri = Convert.ToString(dong[cot], CultureInfo.InvariantCulture);
                    chuoi.Append(giaTri.Length).Append(':').Append(giaTri);
                }
            using (var bam = SHA256.Create())
                return Convert.ToBase64String(bam.ComputeHash(Encoding.UTF8.GetBytes(chuoi.ToString())));
        }

        public static void SuaPhieu(string maPhieu, DateTime ngayNhap, string ghiChu, DataTable chiTiet, string dauVetCu, bool chapNhanDuoiGiaNhap = false)
        {
            PhienDangNhap.YeuCau("NHAP_KHO");
            var cacDong = KiemTraPhieu(maPhieu, ngayNhap, ghiChu, chiTiet, chapNhanDuoiGiaNhap);
            new XuLyDuLieu().TrongGiaoDich<int>((ketNoi, giaoDich) =>
            {
                var daLuu = DocPhieuNhap(ketNoi, giaoDich, maPhieu);
                KiemTraDauVet(daLuu, dauVetCu);
                CapNhatTonKhiSuaPhieu(ketNoi, giaoDich, maPhieu, ngayNhap, daLuu.Tables["ChiTiet"], cacDong);
                LuuChiTietSauKhiSua(ketNoi, giaoDich, maPhieu, ngayNhap, ghiChu, cacDong);
                return 0;
            });
        }

        static void CapNhatTonKhiSuaPhieu(SqlConnection ketNoi, SqlTransaction giaoDich, string maPhieu, DateTime ngayNhap, DataTable chiTietCu, DataTable chiTietMoi)
        {
            var maCu = chiTietCu.AsEnumerable().Select(dong => dong["MaPhuTung"].ToString());
            var maMoi = chiTietMoi.AsEnumerable().Select(dong => dong["MaPhuTung"].ToString());
            var cacMa = maCu.Concat(maMoi).Distinct(StringComparer.OrdinalIgnoreCase).OrderBy(ma => ma, StringComparer.Ordinal);

            foreach (string maPhuTung in cacMa)
            {
                DataRow dongCu = TimDongPhuTung(chiTietCu, maPhuTung);
                DataRow dongMoi = TimDongPhuTung(chiTietMoi, maPhuTung);
                int soLuongCu = dongCu == null ? 0 : Convert.ToInt32(dongCu["SoLuongNhap"]);
                int soLuongMoi = dongMoi == null ? 0 : Convert.ToInt32(dongMoi["SoLuongNhap"]);
                DataRow phuTung = LayPhuTung(ketNoi, giaoDich, dongMoi ?? dongCu);

                if (phuTung == null)
                {
                    ThemPhuTung(ketNoi, giaoDich, dongMoi, 0);
                    phuTung = XuLyDuLieu.DocDuLieu(ketNoi, giaoDich,
                        "SELECT * FROM dbo.PhuTung WITH(UPDLOCK,HOLDLOCK) WHERE MaPhuTung=@ma",
                        XuLyDuLieu.ThamSo("@ma", maPhuTung)).Rows[0];
                }

                int chenhLech = soLuongMoi - soLuongCu;
                if (chenhLech > 0)
                    KiemTraNhapThem(phuTung, ngayNhap.Date);
                if (chenhLech != 0)
                    DoiTon(ketNoi, giaoDich, phuTung, chenhLech, "Sửa phiếu nhập " + maPhieu);
            }
        }

        static DataRow TimDongPhuTung(DataTable chiTiet, string maPhuTung)
        {
            return chiTiet.AsEnumerable().SingleOrDefault(dong => dong["MaPhuTung"].ToString() == maPhuTung);
        }

        static void LuuChiTietSauKhiSua(SqlConnection ketNoi, SqlTransaction giaoDich, string maPhieu, DateTime ngayNhap, string ghiChu, DataTable chiTiet)
        {
            XuLyDuLieu.ThucThi(ketNoi, giaoDich, "DELETE dbo.ChiTietPhieuNhap WHERE MaPhieuNhap=@ma", XuLyDuLieu.ThamSo("@ma", maPhieu));
            foreach (DataRow dong in chiTiet.Rows)
                ThemChiTiet(ketNoi, giaoDich, maPhieu, dong);

            const string sql = "UPDATE dbo.PhieuNhap SET NgayNhap=@ngay,GhiChu=@ghi,TongTienNhap=@tong WHERE MaPhieuNhap=@ma";
            XuLyDuLieu.ThucThi(ketNoi, giaoDich, sql,
                XuLyDuLieu.ThamSo("@ma", maPhieu),
                XuLyDuLieu.ThamSo("@ngay", ngayNhap.Date),
                XuLyDuLieu.ThamSo("@ghi", ghiChu ?? ""),
                XuLyDuLieu.ThamSo("@tong", TongTien(chiTiet)));
        }

        public static void XoaPhieu(string maPhieu, string dauVetCu)
        {
            PhienDangNhap.YeuCau("NHAP_KHO");
            XuLyDuLieu.KiemTraMa(maPhieu, "PN");
            new XuLyDuLieu().TrongGiaoDich<int>((ketNoi, giaoDich) =>
            {
                var daLuu = DocPhieuNhap(ketNoi, giaoDich, maPhieu);
                KiemTraDauVet(daLuu, dauVetCu);
                foreach (DataRow dong in daLuu.Tables["ChiTiet"].Rows)
                {
                    var hienTai = LayPhuTung(ketNoi, giaoDich, dong);
                    DoiTon(ketNoi, giaoDich, hienTai, -Convert.ToInt32(dong["SoLuongNhap"]), "Xóa phiếu nhập " + maPhieu);
                }
                XuLyDuLieu.ThucThi(ketNoi, giaoDich, "DELETE dbo.ChiTietPhieuNhap WHERE MaPhieuNhap=@ma; DELETE dbo.PhieuNhap WHERE MaPhieuNhap=@ma", XuLyDuLieu.ThamSo("@ma", maPhieu));
                return 0;
            });
        }

        static void KiemTraDauVet(DataSet phieu, string dauVetCu)
        {
            if (string.IsNullOrEmpty(dauVetCu) || !string.Equals(phieu.ExtendedProperties["DauVet"].ToString(), dauVetCu, StringComparison.Ordinal))
                throw new InvalidOperationException("Phiếu nhập vừa được người khác thay đổi. Đóng phiếu và mở lại để tải dữ liệu mới.");
        }

        public static void XoaNhaCungCap(string ma)
        {
            PhienDangNhap.YeuCau("NHAP_KHO");
            XuLyDuLieu.KiemTraMa(ma, "NCC");
            new XuLyDuLieu().TrongGiaoDich<int>((ketNoi, giaoDich) =>
            {
                if (XuLyDuLieu.DocDuLieu(ketNoi, giaoDich, "SELECT MaPhuTung FROM dbo.PhuTung WITH(UPDLOCK,HOLDLOCK) WHERE MaNhaCungCap=@ma", XuLyDuLieu.ThamSo("@ma", ma)).Rows.Count > 0)
                    throw new InvalidOperationException("Nhà cung cấp đang được phụ tùng sử dụng nên không thể xóa.");
                if (XuLyDuLieu.ThucThi(ketNoi, giaoDich, "DELETE dbo.NhaCungCap WHERE MaNhaCungCap=@ma", XuLyDuLieu.ThamSo("@ma", ma)) != 1)
                    throw new InvalidOperationException("Nhà cung cấp không còn tồn tại. Tải lại danh sách.");
                return 0;
            });
        }

        static DataTable KiemTraPhieu(string maPhieu, DateTime ngayNhap, string ghiChu, DataTable chiTiet, bool chapNhanDuoiGiaNhap)
        {
            XuLyDuLieu.KiemTraMa(maPhieu, "PN");
            if (ngayNhap.Date > DateTime.Today)
                throw new InvalidOperationException("Ngày nhập không được sau hôm nay.");
            if (chiTiet == null || chiTiet.Rows.Count == 0)
                throw new InvalidOperationException("Thêm ít nhất một phụ tùng vào phiếu nhập.");
            foreach (string cot in new[] { "MaPhuTung", "TenPhuTung", "MaNhaCungCap", "SoLuongNhap", "DonGiaNhap", "DonGiaBan", "HanSuDung", "LaPhuTungMoi" })
                if (!chiTiet.Columns.Contains(cot))
                    throw new InvalidOperationException("Mở lại phiếu nhập để tải đúng thông tin phụ tùng.");
            if ((ghiChu ?? "").Length > 255)
                throw new InvalidOperationException("Ghi chú tối đa 255 ký tự.");
            var cacDong = chiTiet.Copy();
            if (cacDong.AsEnumerable().GroupBy(d => d["MaPhuTung"].ToString(), StringComparer.OrdinalIgnoreCase).Any(g => g.Count() > 1))
                throw new InvalidOperationException("Mỗi phụ tùng chỉ được có một dòng trong phiếu nhập. Cập nhật số lượng của dòng đã có.");
            foreach (DataRow dong in cacDong.Rows)
                KiemTraDongNhap(dong, ngayNhap, chapNhanDuoiGiaNhap);
            TongTien(cacDong);
            return cacDong;
        }

        static void KiemTraDongNhap(DataRow dong, DateTime ngayNhap, bool chapNhanDuoiGiaNhap)
        {
            XuLyDuLieu.KiemTraMa(dong["MaPhuTung"].ToString(), "PT");
            string tenPhuTung = dong["TenPhuTung"].ToString();
            string maNhaCungCap = dong["MaNhaCungCap"].ToString();
            if (string.IsNullOrWhiteSpace(tenPhuTung) || tenPhuTung.Length > 150 || string.IsNullOrWhiteSpace(maNhaCungCap))
                throw new InvalidOperationException("Nhập tên phụ tùng và chọn nhà cung cấp.");

            decimal soLuong = Convert.ToDecimal(dong["SoLuongNhap"]);
            decimal giaNhap = Convert.ToDecimal(dong["DonGiaNhap"]);
            decimal giaBan = Convert.ToDecimal(dong["DonGiaBan"]);
            bool soLuongHopLe = soLuong >= 1 && soLuong <= 100000 && soLuong == decimal.Truncate(soLuong);
            if (!soLuongHopLe || !GiaHopLe(giaNhap) || !GiaHopLe(giaBan))
                throw new InvalidOperationException("Số lượng từ 1 đến 100.000; giá nhập và giá bán là số nguyên dương theo đồng Việt Nam.");
            if (giaBan < giaNhap && !chapNhanDuoiGiaNhap)
                throw new InvalidOperationException("Giá bán thấp hơn giá nhập. Cần xác nhận rõ trước khi ghi phiếu.");
            if (Convert.ToDateTime(dong["HanSuDung"]).Date < ngayNhap.Date)
                throw new InvalidOperationException("Hạn sử dụng không được trước ngày nhập.");
        }

        static bool GiaHopLe(decimal gia)
        {
            return gia > 0 && gia <= GioiHanTien && gia == decimal.Truncate(gia);
        }

        static decimal TongTien(DataTable chiTiet)
        {
            decimal tong = chiTiet.AsEnumerable().Sum(d => Convert.ToInt32(d["SoLuongNhap"]) * Convert.ToDecimal(d["DonGiaNhap"]));
            if (tong > GioiHanTien)
                throw new InvalidOperationException("Tổng tiền phiếu nhập vượt giới hạn cho phép.");
            return tong;
        }

        static DataRow LayPhuTung(SqlConnection ketNoi, SqlTransaction giaoDich, DataRow dong)
        {
            var daCo = XuLyDuLieu.DocDuLieu(ketNoi, giaoDich, "SELECT * FROM dbo.PhuTung WITH(UPDLOCK,HOLDLOCK) WHERE MaPhuTung=@ma", XuLyDuLieu.ThamSo("@ma", dong["MaPhuTung"]));
            bool laMoi = Convert.ToBoolean(dong["LaPhuTungMoi"]);
            if (laMoi && daCo.Rows.Count > 0)
                throw new InvalidOperationException("Mã phụ tùng mới đã được sử dụng. Tạo lại phụ tùng để lấy mã khác.");
            if (!laMoi && daCo.Rows.Count == 0)
                throw new InvalidOperationException("Phụ tùng không còn tồn tại. Tải lại danh mục trước khi nhập.");
            if (daCo.Rows.Count == 0)
                return null;
            var hienTai = daCo.Rows[0];
            if (hienTai["MaNhaCungCap"].ToString() != dong["MaNhaCungCap"].ToString() || Convert.ToDateTime(hienTai["HanSuDung"]).Date != Convert.ToDateTime(dong["HanSuDung"]).Date || hienTai["TenPhuTung"].ToString() != dong["TenPhuTung"].ToString())
                throw new InvalidOperationException("Nhà cung cấp, hạn sử dụng hoặc tên khác phải tạo mã phụ tùng mới.");
            return hienTai;
        }

        static void KiemTraNhapThem(DataRow phuTung, DateTime ngayNhap)
        {
            if (phuTung["TrangThai"].ToString() != "Đang kinh doanh")
                throw new InvalidOperationException("Phụ tùng đã ngừng kinh doanh.");
            if (Convert.ToDateTime(phuTung["HanSuDung"]).Date < ngayNhap)
                throw new InvalidOperationException("Phụ tùng đã hết hạn tại ngày nhập.");
        }

        static void ThemPhuTung(SqlConnection ketNoi, SqlTransaction giaoDich, DataRow dong, int soLuong)
        {
            XuLyDuLieu.ThucThi(ketNoi, giaoDich,
                "INSERT dbo.PhuTung(MaPhuTung,MaNhaCungCap,TenPhuTung,DonGiaNhap,DonGiaBan,SoLuongTon,HanSuDung,TrangThai) VALUES(@ma,@ncc,@ten,@gn,@gb,@sl,@han,N'Đang kinh doanh')",
                XuLyDuLieu.ThamSo("@ma", dong["MaPhuTung"]), XuLyDuLieu.ThamSo("@ncc", dong["MaNhaCungCap"]), XuLyDuLieu.ThamSo("@ten", dong["TenPhuTung"]), XuLyDuLieu.ThamSo("@gn", dong["DonGiaNhap"]), XuLyDuLieu.ThamSo("@gb", dong["DonGiaBan"]), XuLyDuLieu.ThamSo("@sl", soLuong), XuLyDuLieu.ThamSo("@han", dong["HanSuDung"]));
        }

        static void ThemChiTiet(SqlConnection ketNoi, SqlTransaction giaoDich, string maPhieu, DataRow dong)
        {
            XuLyDuLieu.ThucThi(ketNoi, giaoDich, "INSERT dbo.ChiTietPhieuNhap(MaPhieuNhap,MaPhuTung,SoLuongNhap,ThanhTien) VALUES(@ma,@pt,@sl,@tien)",
                XuLyDuLieu.ThamSo("@ma", maPhieu), XuLyDuLieu.ThamSo("@pt", dong["MaPhuTung"]), XuLyDuLieu.ThamSo("@sl", dong["SoLuongNhap"]), XuLyDuLieu.ThamSo("@tien", Convert.ToInt32(dong["SoLuongNhap"]) * Convert.ToDecimal(dong["DonGiaNhap"])));
        }

        static void DoiTon(SqlConnection ketNoi, SqlTransaction giaoDich, DataRow hienTai, int chenhLech, string lyDo)
        {
            int soCu = Convert.ToInt32(hienTai["SoLuongTon"]);
            long soMoi = (long)soCu + chenhLech;
            if (soMoi < 0 || soMoi > int.MaxValue)
                throw new InvalidOperationException("Tồn phụ tùng " + hienTai["MaPhuTung"] + " không đủ để điều chỉnh phiếu nhập. Chưa thay đổi dữ liệu.");
            XuLyDuLieu.ThucThi(ketNoi, giaoDich, "UPDATE dbo.PhuTung SET SoLuongTon=@sl WHERE MaPhuTung=@ma", XuLyDuLieu.ThamSo("@ma", hienTai["MaPhuTung"]), XuLyDuLieu.ThamSo("@sl", (int)soMoi));
            XuLyDuLieu.ThucThi(ketNoi, giaoDich, "INSERT dbo.DieuChinhTonKho(MaDieuChinh,MaPhuTung,MaNhanVien,NgayDieuChinh,SoLuongCu,SoLuongMoi,DonGiaNhap,LyDo) VALUES(@dc,@pt,@nv,@ngay,@cu,@moi,@gia,@lyDo)",
                XuLyDuLieu.ThamSo("@dc", XuLyDuLieu.TaoMaMoi(ketNoi, giaoDich, "DC")), XuLyDuLieu.ThamSo("@pt", hienTai["MaPhuTung"]), XuLyDuLieu.ThamSo("@nv", PhienDangNhap.MaNhanVien), XuLyDuLieu.ThamSo("@ngay", DateTime.Today), XuLyDuLieu.ThamSo("@cu", soCu), XuLyDuLieu.ThamSo("@moi", (int)soMoi), XuLyDuLieu.ThamSo("@gia", hienTai["DonGiaNhap"]), XuLyDuLieu.ThamSo("@lyDo", lyDo));
        }

        static void GhiDoiGia(SqlConnection ketNoi, SqlTransaction giaoDich, string maPhuTung, decimal giaCu, decimal giaMoi, string lyDo)
        {
            XuLyDuLieu.ThucThi(ketNoi, giaoDich, "INSERT dbo.DieuChinhGiaBan(MaDieuChinhGia,MaPhuTung,MaNhanVien,NgayDieuChinh,GiaBanCu,GiaBanMoi,LyDo) VALUES(@dg,@pt,@nv,@ngay,@cu,@moi,@lyDo)",
                XuLyDuLieu.ThamSo("@dg", XuLyDuLieu.TaoMaMoi(ketNoi, giaoDich, "DG")), XuLyDuLieu.ThamSo("@pt", maPhuTung), XuLyDuLieu.ThamSo("@nv", PhienDangNhap.MaNhanVien), XuLyDuLieu.ThamSo("@ngay", DateTime.Today), XuLyDuLieu.ThamSo("@cu", giaCu), XuLyDuLieu.ThamSo("@moi", giaMoi), XuLyDuLieu.ThamSo("@lyDo", lyDo));
        }
    }
}
