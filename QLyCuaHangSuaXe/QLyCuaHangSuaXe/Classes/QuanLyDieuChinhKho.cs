using System;
using System.Data;
using System.Data.SqlClient;

namespace QLyCuaHangSuaXe.LopDungChung
{
    public static class QuanLyDieuChinhKho
    {
        const decimal GiaToiDa = 999999999999999999m;
        static string KiemTraLyDo(string lyDo)
        {
            string noiDung = (lyDo ?? "").Trim();
            if (noiDung.Length == 0 || noiDung.Length > 255)
                throw new InvalidOperationException("Nhập lý do điều chỉnh, tối đa 255 ký tự.");
            return noiDung;
        }
        static DataRow KhoaPhuTung(SqlConnection ketNoi, SqlTransaction giaoDich, string maPhuTung)
        {
            var bang = XuLyDuLieu.DocDuLieu(ketNoi, giaoDich,
                "SELECT MaPhuTung,TenPhuTung,SoLuongTon,DonGiaNhap,DonGiaBan FROM dbo.PhuTung WITH(UPDLOCK,HOLDLOCK) WHERE MaPhuTung=@ma",
                XuLyDuLieu.ThamSo("@ma", maPhuTung));
            if (bang.Rows.Count != 1)
                throw new InvalidOperationException("Phụ tùng không còn tồn tại. Tải lại kho trước khi điều chỉnh.");
            return bang.Rows[0];
        }
        static string MaDieuChinh(SqlConnection ketNoi, SqlTransaction giaoDich, string tienTo, string maDuKien)
        {
            string ma = string.IsNullOrEmpty(maDuKien)
                ? XuLyDuLieu.TaoMaMoi(ketNoi, giaoDich, tienTo)
                : XuLyDuLieu.KiemTraMa(maDuKien, tienTo);
            string cauLenh = tienTo == "DC"
                ? "SELECT 1 FROM dbo.DieuChinhTonKho WITH(UPDLOCK,HOLDLOCK) WHERE MaDieuChinh=@ma"
                : "SELECT 1 FROM dbo.DieuChinhGiaBan WITH(UPDLOCK,HOLDLOCK) WHERE MaDieuChinhGia=@ma";
            if (XuLyDuLieu.DocDuLieu(ketNoi, giaoDich, cauLenh, XuLyDuLieu.ThamSo("@ma", ma)).Rows.Count != 0)
                throw new InvalidOperationException("Mã điều chỉnh đã được sử dụng trên máy khác. Bấm Làm mới kho để cấp lại mã.");
            return ma;
        }
        public static string DieuChinhTon(string maPhuTung, int soLuongLech, string lyDo, string maDuKien = null)
        {
            PhienDangNhap.YeuCau("NHAP_KHO");
            maPhuTung = XuLyDuLieu.KiemTraMa(maPhuTung, "PT");
            lyDo = KiemTraLyDo(lyDo);
            if (soLuongLech == 0)
                throw new InvalidOperationException("Số lượng điều chỉnh phải khác 0.");
            string maNhanVien = PhienDangNhap.MaNhanVien;
            return new XuLyDuLieu().TrongGiaoDich((ketNoi, giaoDich) =>
            {
                var phuTung = KhoaPhuTung(ketNoi, giaoDich, maPhuTung);
                int soLuongCu = Convert.ToInt32(phuTung["SoLuongTon"]), soLuongMoi;
                try
                {
                    soLuongMoi = checked(soLuongCu + soLuongLech);
                }
                catch (OverflowException) { throw new InvalidOperationException("Số lượng tồn vượt giới hạn số nguyên. Kiểm tra lại số lượng điều chỉnh."); }
                if (soLuongMoi < 0)
                    throw new InvalidOperationException("Số lượng tồn sau điều chỉnh không được âm.");
                string ma = MaDieuChinh(ketNoi, giaoDich, "DC", maDuKien);
                XuLyDuLieu.ThucThi(ketNoi, giaoDich,
                    "INSERT dbo.DieuChinhTonKho(MaDieuChinh,MaPhuTung,MaNhanVien,NgayDieuChinh,SoLuongCu,SoLuongMoi,DonGiaNhap,LyDo) VALUES(@ma,@pt,@nv,@ngay,@cu,@moi,@gia,@lyDo)",
                    XuLyDuLieu.ThamSo("@ma", ma), XuLyDuLieu.ThamSo("@pt", maPhuTung), XuLyDuLieu.ThamSo("@nv", maNhanVien),
                    XuLyDuLieu.ThamSo("@ngay", DateTime.Today), XuLyDuLieu.ThamSo("@cu", soLuongCu), XuLyDuLieu.ThamSo("@moi", soLuongMoi),
                    XuLyDuLieu.ThamSo("@gia", phuTung["DonGiaNhap"]), XuLyDuLieu.ThamSo("@lyDo", lyDo));
                if (XuLyDuLieu.ThucThi(ketNoi, giaoDich, "UPDATE dbo.PhuTung SET SoLuongTon=@moi WHERE MaPhuTung=@pt",
                    XuLyDuLieu.ThamSo("@moi", soLuongMoi), XuLyDuLieu.ThamSo("@pt", maPhuTung)) != 1)
                    throw new InvalidOperationException("Không thể cập nhật tồn phụ tùng. Điều chỉnh chưa được ghi.");
                return ma;
            });
        }
        public static string DieuChinhGia(string maPhuTung, decimal giaMoi, string lyDo, bool chapNhanDuoiGiaNhap = false, string maDuKien = null)
        {
            PhienDangNhap.YeuCau("NHAP_KHO");
            maPhuTung = XuLyDuLieu.KiemTraMa(maPhuTung, "PT");
            lyDo = KiemTraLyDo(lyDo);
            if (giaMoi <= 0 || giaMoi > GiaToiDa || decimal.Truncate(giaMoi) != giaMoi)
                throw new InvalidOperationException("Giá bán mới phải là số đồng nguyên lớn hơn 0, tối đa 18 chữ số.");
            string maNhanVien = PhienDangNhap.MaNhanVien;
            return new XuLyDuLieu().TrongGiaoDich((ketNoi, giaoDich) =>
            {
                var phuTung = KhoaPhuTung(ketNoi, giaoDich, maPhuTung);
                decimal giaCu = Convert.ToDecimal(phuTung["DonGiaBan"]), giaNhap = Convert.ToDecimal(phuTung["DonGiaNhap"]);
                if (giaMoi == giaCu)
                    throw new InvalidOperationException("Giá bán mới trùng giá đang áp dụng.");
                if (giaMoi < giaNhap && !chapNhanDuoiGiaNhap)
                    throw new InvalidOperationException("Giá bán thấp hơn giá nhập hiện tại. Kiểm tra và xác nhận lại trước khi lưu.");
                string ma = MaDieuChinh(ketNoi, giaoDich, "DG", maDuKien);
                XuLyDuLieu.ThucThi(ketNoi, giaoDich,
                    "INSERT dbo.DieuChinhGiaBan(MaDieuChinhGia,MaPhuTung,MaNhanVien,NgayDieuChinh,GiaBanCu,GiaBanMoi,LyDo) VALUES(@ma,@pt,@nv,@ngay,@cu,@moi,@lyDo)",
                    XuLyDuLieu.ThamSo("@ma", ma), XuLyDuLieu.ThamSo("@pt", maPhuTung), XuLyDuLieu.ThamSo("@nv", maNhanVien),
                    XuLyDuLieu.ThamSo("@ngay", DateTime.Today), XuLyDuLieu.ThamSo("@cu", giaCu), XuLyDuLieu.ThamSo("@moi", giaMoi), XuLyDuLieu.ThamSo("@lyDo", lyDo));
                if (XuLyDuLieu.ThucThi(ketNoi, giaoDich, "UPDATE dbo.PhuTung SET DonGiaBan=@moi WHERE MaPhuTung=@pt",
                    XuLyDuLieu.ThamSo("@moi", giaMoi), XuLyDuLieu.ThamSo("@pt", maPhuTung)) != 1)
                    throw new InvalidOperationException("Không thể cập nhật giá bán. Điều chỉnh chưa được ghi.");
                return ma;
            });
        }
    }
}
