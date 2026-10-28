using System;
using System.Data;
using System.Linq;

namespace QLyCuaHangSuaXe.LopDungChung
{
    public static class QuanLyKho
    {
        public static void GhiSo(string maPhieu, string ghiChu, DataTable chiTiet)
        {
            GhiSo(maPhieu, DateTime.Today, ghiChu, chiTiet);
        }

        public static void GhiSo(string maPhieu, DateTime ngayNhap, string ghiChu, DataTable chiTiet)
        {
            PhienDangNhap.YeuCau("NHAP_KHO");
            XuLyDuLieu.KiemTraMa(maPhieu, "PN");
            if (ngayNhap.Date > DateTime.Today) throw new InvalidOperationException("Ngày nhập không được sau hôm nay.");
            if (chiTiet == null || chiTiet.Rows.Count == 0) throw new InvalidOperationException("Thêm ít nhất một phụ tùng vào phiếu nhập.");
            if (!chiTiet.Columns.Contains("LaPhuTungMoi")) throw new InvalidOperationException("Mở lại phiếu nhập để tải đúng thông tin phụ tùng.");
            if ((ghiChu ?? "").Length > 255) throw new InvalidOperationException("Ghi chú tối đa 255 ký tự.");
            if (chiTiet.AsEnumerable().GroupBy(d => d["MaPhuTung"].ToString(), StringComparer.OrdinalIgnoreCase).Any(g => g.Count() > 1))
                throw new InvalidOperationException("Mỗi phụ tùng chỉ được có một dòng trong phiếu nhập. Cập nhật số lượng của dòng đã có.");
            foreach (DataRow dong in chiTiet.Rows)
            {
                XuLyDuLieu.KiemTraMa(dong["MaPhuTung"].ToString(), "PT");
                if (string.IsNullOrWhiteSpace(dong["TenPhuTung"].ToString()) || dong["TenPhuTung"].ToString().Length > 150 || string.IsNullOrWhiteSpace(dong["MaNhaCungCap"].ToString()))
                    throw new InvalidOperationException("Nhập tên phụ tùng và chọn nhà cung cấp.");
                int soLuong = Convert.ToInt32(dong["SoLuongNhap"]);
                decimal giaNhap = Convert.ToDecimal(dong["DonGiaNhap"]), giaBan = Convert.ToDecimal(dong["DonGiaBan"]);
                if (soLuong < 1 || soLuong > 100000 || giaNhap <= 0 || giaBan < giaNhap)
                    throw new InvalidOperationException("Số lượng từ 1 đến 100.000; giá nhập lớn hơn 0 và giá bán không thấp hơn giá nhập.");
                if (Convert.ToDateTime(dong["HanSuDung"]).Date < ngayNhap.Date)
                    throw new InvalidOperationException("Không được nhập phụ tùng đã hết hạn sử dụng.");
            }
            new XuLyDuLieu().TrongGiaoDich<int>((ketNoi, giaoDich) =>
            {
                decimal tongTien = chiTiet.AsEnumerable().Sum(d => Convert.ToInt32(d["SoLuongNhap"]) * Convert.ToDecimal(d["DonGiaNhap"]));
                XuLyDuLieu.ThucThi(ketNoi, giaoDich,
                    "INSERT dbo.PhieuNhap(MaPhieuNhap,MaNhanVienNhap,NgayNhap,GhiChu,TongTienNhap) VALUES(@ma,@nv,@ngay,@ghiChu,@tong)",
                    XuLyDuLieu.ThamSo("@ma", maPhieu), XuLyDuLieu.ThamSo("@nv", PhienDangNhap.MaNhanVien), XuLyDuLieu.ThamSo("@ngay", ngayNhap.Date),
                    XuLyDuLieu.ThamSo("@ghiChu", ghiChu ?? ""), XuLyDuLieu.ThamSo("@tong", tongTien));
                foreach (DataRow dong in chiTiet.AsEnumerable().OrderBy(d => d["MaPhuTung"].ToString(), StringComparer.Ordinal))
                {
                    string maPhuTung = dong["MaPhuTung"].ToString();
                    var daCo = XuLyDuLieu.DocDuLieu(ketNoi, giaoDich,
                        "SELECT TrangThai,TenPhuTung,MaNhaCungCap,HanSuDung FROM dbo.PhuTung WITH(UPDLOCK,HOLDLOCK) WHERE MaPhuTung=@ma",
                        XuLyDuLieu.ThamSo("@ma", maPhuTung));
                    bool laMoi = Convert.ToBoolean(dong["LaPhuTungMoi"]);
                    if (laMoi && daCo.Rows.Count > 0) throw new InvalidOperationException("Mã phụ tùng mới đã được sử dụng. Tạo lại phụ tùng để lấy mã khác.");
                    if (!laMoi && daCo.Rows.Count == 0) throw new InvalidOperationException("Phụ tùng không còn tồn tại. Tải lại danh mục trước khi nhập.");
                    if (daCo.Rows.Count > 0)
                    {
                        var hienTai = daCo.Rows[0];
                        if (hienTai["TrangThai"].ToString() != "Đang kinh doanh") throw new InvalidOperationException("Phụ tùng đã ngừng kinh doanh.");
                        if (hienTai["MaNhaCungCap"].ToString() != dong["MaNhaCungCap"].ToString() || Convert.ToDateTime(hienTai["HanSuDung"]).Date != Convert.ToDateTime(dong["HanSuDung"]).Date || hienTai["TenPhuTung"].ToString() != dong["TenPhuTung"].ToString())
                            throw new InvalidOperationException("Nhà cung cấp, hạn sử dụng hoặc tên khác phải tạo mã phụ tùng mới.");
                    }
                    int soLuong = Convert.ToInt32(dong["SoLuongNhap"]);
                    decimal giaNhap = Convert.ToDecimal(dong["DonGiaNhap"]);
                    if (laMoi)
                        XuLyDuLieu.ThucThi(ketNoi, giaoDich,
                            "INSERT dbo.PhuTung(MaPhuTung,MaNhaCungCap,TenPhuTung,DonGiaNhap,DonGiaBan,SoLuongTon,HanSuDung,TrangThai) VALUES(@ma,@ncc,@ten,@giaNhap,@giaBan,@sl,@han,N'Đang kinh doanh')",
                            XuLyDuLieu.ThamSo("@ma", maPhuTung), XuLyDuLieu.ThamSo("@ncc", dong["MaNhaCungCap"]), XuLyDuLieu.ThamSo("@ten", dong["TenPhuTung"]),
                            XuLyDuLieu.ThamSo("@giaNhap", giaNhap), XuLyDuLieu.ThamSo("@giaBan", dong["DonGiaBan"]), XuLyDuLieu.ThamSo("@sl", soLuong), XuLyDuLieu.ThamSo("@han", dong["HanSuDung"]));
                    else
                        XuLyDuLieu.ThucThi(ketNoi, giaoDich,
                            "UPDATE dbo.PhuTung SET DonGiaNhap=@giaNhap,DonGiaBan=@giaBan,SoLuongTon=SoLuongTon+@sl WHERE MaPhuTung=@ma",
                            XuLyDuLieu.ThamSo("@ma", maPhuTung), XuLyDuLieu.ThamSo("@giaNhap", giaNhap), XuLyDuLieu.ThamSo("@giaBan", dong["DonGiaBan"]), XuLyDuLieu.ThamSo("@sl", soLuong));
                    XuLyDuLieu.ThucThi(ketNoi, giaoDich,
                        "INSERT dbo.ChiTietPhieuNhap(MaPhieuNhap,MaPhuTung,SoLuongNhap,ThanhTien) VALUES(@phieu,@pt,@sl,@tien)",
                        XuLyDuLieu.ThamSo("@phieu", maPhieu), XuLyDuLieu.ThamSo("@pt", maPhuTung), XuLyDuLieu.ThamSo("@sl", soLuong), XuLyDuLieu.ThamSo("@tien", soLuong * giaNhap));
                }
                return 0;
            });
        }
    }
}
