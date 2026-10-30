using System;
using System.Data;

namespace QLyCuaHangSuaXe.LopDungChung
{
    public sealed class ChiTietTinhCong
    {
        public int PhutCa, PhutThucLam, PhutMuon, PhutVeSom, DieuChinhPhut, PhutTinhCong;
        public decimal LuongGio, TienCong;
        public bool Vang;
        public string GiaiThich()
        {
            return Vang ? "Vắng cả ca: 0 giờ, tiền công 0 đ." : "Tiền ca = " + PhutTinhCong + " ÷ 60 × " + LuongGio.ToString("N2") + " đ/giờ = " + TienCong.ToString("N0") + " đ.";
        }
    }
    public static class ChamCong
    {
        public static decimal TinhTienCong(decimal gioThucTe, decimal luongThang, decimal gioChuanThang = 208m)
        {
            if (gioThucTe < 0 || luongThang <= 0 || gioChuanThang <= 0)
                throw new InvalidOperationException("Giờ làm, lương tháng hoặc giờ chuẩn không hợp lệ.");
            return Math.Round(gioThucTe * luongThang / gioChuanThang, 0, MidpointRounding.AwayFromZero);
        }
        public static ChiTietTinhCong TinhChiTiet(TimeSpan batDau, TimeSpan ketThuc, TimeSpan gioVao, TimeSpan gioRa, bool vang, int dieuChinh, decimal luongGio)
        {
            int phut = PhutCong(batDau, ketThuc, gioVao, gioRa, vang, dieuChinh);
            return new ChiTietTinhCong { PhutCa = (int)(ketThuc - batDau).TotalMinutes, PhutThucLam = PhutCong(batDau, ketThuc, gioVao, gioRa, vang, 0), PhutTinhCong = phut, DieuChinhPhut = dieuChinh, LuongGio = luongGio, TienCong = Math.Round(phut * luongGio / 60m, 2, MidpointRounding.AwayFromZero), Vang = vang, PhutMuon = vang ? 0 : Math.Max(0, (int)(gioVao - batDau).TotalMinutes), PhutVeSom = vang ? 0 : Math.Max(0, (int)(ketThuc - gioRa).TotalMinutes) };
        }
        public static int PhutCong(TimeSpan batDau, TimeSpan ketThuc, TimeSpan gioVao, TimeSpan gioRa, bool vang, int dieuChinhPhut)
        {
            if (vang)
            {
                if (dieuChinhPhut != 0)
                    throw new InvalidOperationException("Ca vắng không cộng trừ phút.");
                return 0;
            }
            if (gioRa <= gioVao)
                throw new InvalidOperationException("Giờ ra phải sau giờ vào.");
            TimeSpan dau = gioVao > batDau ? gioVao : batDau, cuoi = gioRa < ketThuc ? gioRa : ketThuc;
            return Math.Max(0, (int)Math.Max(0, (cuoi - dau).TotalMinutes) + dieuChinhPhut);
        }
        public static void LuuCong(string maNhanVien, DateTime ngayLam, string maCaLam, decimal gioThucTe, string maNhanVienCu = null, DateTime? ngayLamCu = null, string maCaLamCu = null)
        {
            PhienDangNhap.YeuCau("BANG_CONG");
            if (gioThucTe < 0 || gioThucTe > 24 || decimal.Round(gioThucTe, 2) != gioThucTe)
                throw new InvalidOperationException("Giờ thực làm từ 0 đến 24, tối đa 2 chữ số thập phân.");
            bool sua = !string.IsNullOrEmpty(maNhanVienCu) && ngayLamCu.HasValue && !string.IsNullOrEmpty(maCaLamCu);
            new XuLyDuLieu().TrongGiaoDich<int>((ketNoi, giaoDich) =>
            {
                if (XuLyDuLieu.DocDuLieu(ketNoi, giaoDich, "SELECT 1 FROM dbo.NhanVien WITH(UPDLOCK) WHERE MaNhanVien=@nv AND TrangThai=1", XuLyDuLieu.ThamSo("@nv", maNhanVien)).Rows.Count != 1)
                    throw new InvalidOperationException("Chọn nhân viên đang làm.");
                if (XuLyDuLieu.DocDuLieu(ketNoi, giaoDich, "SELECT 1 FROM dbo.CaLam WHERE MaCaLam=@ca", XuLyDuLieu.ThamSo("@ca", maCaLam)).Rows.Count != 1)
                    throw new InvalidOperationException("Chọn ca làm.");
                if (sua && XuLyDuLieu.DocDuLieu(ketNoi, giaoDich, "SELECT 1 FROM dbo.ChiTietCaLam WITH(UPDLOCK,HOLDLOCK) WHERE MaNhanVien=@nv AND NgayLam=@ngay AND MaCaLam=@ca", XuLyDuLieu.ThamSo("@nv", maNhanVienCu), XuLyDuLieu.ThamSo("@ngay", ngayLamCu.Value.Date), XuLyDuLieu.ThamSo("@ca", maCaLamCu)).Rows.Count != 1)
                    throw new InvalidOperationException("Dòng công không còn tồn tại. Hãy tải lại.");
                var cungNgay = XuLyDuLieu.DocDuLieu(ketNoi, giaoDich, "SELECT MaNhanVien,MaCaLam,NgayLam,TongThoiGianLamThucTe FROM dbo.ChiTietCaLam WITH(UPDLOCK,HOLDLOCK) WHERE MaNhanVien=@nv AND NgayLam=@ngay", XuLyDuLieu.ThamSo("@nv", maNhanVien), XuLyDuLieu.ThamSo("@ngay", ngayLam.Date));
                decimal tong = gioThucTe;
                foreach (DataRow dong in cungNgay.Rows)
                {
                    bool dongCu = sua && dong["MaNhanVien"].ToString() == maNhanVienCu && (DateTime)dong["NgayLam"] == ngayLamCu.Value.Date && dong["MaCaLam"].ToString() == maCaLamCu;
                    if (dongCu)
                        continue;
                    if (dong["MaCaLam"].ToString() == maCaLam)
                        throw new InvalidOperationException("Nhân viên đã được chấm ca này trong ngày. Chọn dòng để sửa.");
                    tong += Convert.ToDecimal(dong["TongThoiGianLamThucTe"]);
                }
                if (tong > 24)
                    throw new InvalidOperationException("Tổng giờ làm trong ngày không vượt 24 giờ.");
                return XuLyDuLieu.ThucThi(ketNoi, giaoDich, sua ? "UPDATE dbo.ChiTietCaLam SET MaNhanVien=@nv,MaCaLam=@ca,NgayLam=@ngay,TongThoiGianLamThucTe=@gio WHERE MaNhanVien=@nvcu AND NgayLam=@ngaycu AND MaCaLam=@cacu" : "INSERT dbo.ChiTietCaLam(MaNhanVien,MaCaLam,NgayLam,TongThoiGianLamThucTe) VALUES(@nv,@ca,@ngay,@gio)", XuLyDuLieu.ThamSo("@nv", maNhanVien), XuLyDuLieu.ThamSo("@ca", maCaLam), XuLyDuLieu.ThamSo("@ngay", ngayLam.Date), XuLyDuLieu.ThamSo("@gio", gioThucTe), XuLyDuLieu.ThamSo("@nvcu", maNhanVienCu ?? ""), XuLyDuLieu.ThamSo("@ngaycu", ngayLamCu ?? ngayLam.Date), XuLyDuLieu.ThamSo("@cacu", maCaLamCu ?? ""));
            });
        }
    }
}
