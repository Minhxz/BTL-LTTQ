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
            if (Vang) return "Vắng cả ca: 0 phút → 0 đ. Không cộng hoặc trừ phút trên ca vắng.";
            return "Trong ca: " + PhutCa + " phút • Đi muộn: " + PhutMuon + " phút • Về sớm: " + PhutVeSom + " phút\n"
                + "Phút tính công = " + PhutThucLam + " phút thực làm " + (DieuChinhPhut >= 0 ? "+ " : "− ") + Math.Abs(DieuChinhPhut) + " phút điều chỉnh = " + PhutTinhCong + " phút\n"
                + "Tiền ca = " + PhutTinhCong + " ÷ 60 × " + LuongGio.ToString("N0") + " đ/giờ = " + TienCong.ToString("N2") + " đ. Đi muộn/về sớm đã nằm trong giờ thực làm.";
        }
    }

    public static class ChamCong
    {
        public static ChiTietTinhCong TinhChiTiet(TimeSpan batDau, TimeSpan ketThuc, TimeSpan gioVao, TimeSpan gioRa, bool vang, int dieuChinh, decimal luongGio)
        {
            gioVao = new TimeSpan(gioVao.Hours, gioVao.Minutes, 0);
            gioRa = new TimeSpan(gioRa.Hours, gioRa.Minutes, 0);
            var chiTiet = new ChiTietTinhCong { PhutCa = (int)(ketThuc - batDau).TotalMinutes, Vang = vang, LuongGio = luongGio, DieuChinhPhut = dieuChinh };
            chiTiet.PhutThucLam = PhutCong(batDau, ketThuc, gioVao, gioRa, vang, 0);
            chiTiet.PhutTinhCong = PhutCong(batDau, ketThuc, gioVao, gioRa, vang, dieuChinh);
            chiTiet.PhutMuon = vang ? 0 : Math.Max(0, (int)(gioVao - batDau).TotalMinutes);
            chiTiet.PhutVeSom = vang ? 0 : Math.Max(0, (int)(ketThuc - gioRa).TotalMinutes);
            chiTiet.TienCong = Math.Round(chiTiet.PhutTinhCong * luongGio / 60m, 2, MidpointRounding.AwayFromZero);
            return chiTiet;
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
            TimeSpan giaTriThuNhat = gioVao > batDau ? gioVao : batDau, giaTriThuHai = gioRa < ketThuc ? gioRa : ketThuc;
            return Math.Max(0, (int)Math.Max(0, (giaTriThuHai - giaTriThuNhat).TotalMinutes) + dieuChinhPhut);
        }

        public static void LuuCong(int ma, string maTho, DateTime ngay, string maCa, TimeSpan gioVao, TimeSpan gioRa, bool vang, int dieuChinhPhut, string lyDo)
        {
            PhienDangNhap.YeuCau("BANG_CONG");
            lyDo = (lyDo ?? "").Trim();
            gioVao = new TimeSpan(gioVao.Hours, gioVao.Minutes, 0);
            gioRa = new TimeSpan(gioRa.Hours, gioRa.Minutes, 0);
            if (Math.Abs(dieuChinhPhut) > 390 || (dieuChinhPhut != 0 && string.IsNullOrWhiteSpace(lyDo)) || lyDo.Length > 300)
                throw new InvalidOperationException("Điều chỉnh trong ±390 phút và bắt buộc có lý do (tối đa 300 ký tự).");
            if (vang && string.IsNullOrWhiteSpace(lyDo))
                throw new InvalidOperationException("Ghi lý do vắng.");
            new XuLyDuLieu().TrongGiaoDich<int>((ketNoi, giaoDich) =>
            {
                if (ma != 0 && XuLyDuLieu.DocDuLieu(ketNoi, giaoDich, "SELECT MaBangCong FROM dbo.BangCong WITH(UPDLOCK,HOLDLOCK) WHERE MaBangCong=@id", XuLyDuLieu.ThamSo("@id", ma)).Rows.Count != 1)
                    throw new InvalidOperationException("Dòng công không còn tồn tại. Hãy tải lại bảng công.");
                var bangNhanVien = XuLyDuLieu.DocDuLieu(ketNoi, giaoDich, "SELECT LuongGio FROM dbo.NhanVien WITH(UPDLOCK) WHERE MaNhanVien=@nv AND TrangThai=N'Đang làm'", XuLyDuLieu.ThamSo("@nv", maTho));
                if (bangNhanVien.Rows.Count != 1)
                    throw new InvalidOperationException("Chọn nhân viên đang làm.");
                var ca = XuLyDuLieu.DocDuLieu(ketNoi, giaoDich, "SELECT GioBatDau,GioKetThuc FROM dbo.CaLam WHERE MaCa=@ca", XuLyDuLieu.ThamSo("@ca", maCa));
                if (ca.Rows.Count != 1)
                    throw new InvalidOperationException("Chọn ca làm.");
                TimeSpan batDau = (TimeSpan)ca.Rows[0][0], ketThuc = (TimeSpan)ca.Rows[0][1];
                int tong = PhutCong(batDau, ketThuc, gioVao, gioRa, vang, dieuChinhPhut);
                if (tong > 780)
                    throw new InvalidOperationException("Tổng phút ca vượt giới hạn.");
                if (!vang && (gioRa <= batDau || gioVao >= ketThuc))
                    throw new InvalidOperationException("Giờ vào/ra phải giao với ca làm.");
                var cacCaKhac = XuLyDuLieu.DocDuLieu(ketNoi, giaoDich, "SELECT b.GioVao,b.GioRa,c.GioBatDau,c.GioKetThuc FROM dbo.BangCong b WITH(UPDLOCK,HOLDLOCK) JOIN dbo.CaLam c ON c.MaCa=b.MaCa WHERE b.MaNhanVien=@nv AND b.Ngay=@ngay AND b.MaBangCong<>@id AND b.Vang=0", XuLyDuLieu.ThamSo("@nv", maTho), XuLyDuLieu.ThamSo("@ngay", ngay.Date), XuLyDuLieu.ThamSo("@id", ma));
                TimeSpan giaTriThuNhat = gioVao > batDau ? gioVao : batDau, giaTriThuHai = gioRa < ketThuc ? gioRa : ketThuc;
                foreach (DataRow dongDuLieu in cacCaKhac.Rows)
                {
                    TimeSpan batDauCaKhac = (TimeSpan)dongDuLieu[0] > (TimeSpan)dongDuLieu[2] ? (TimeSpan)dongDuLieu[0] : (TimeSpan)dongDuLieu[2], ketThucCaKhac = (TimeSpan)dongDuLieu[1] < (TimeSpan)dongDuLieu[3] ? (TimeSpan)dongDuLieu[1] : (TimeSpan)dongDuLieu[3];
                    if (!vang && giaTriThuNhat < ketThucCaKhac && batDauCaKhac < giaTriThuHai)
                        throw new InvalidOperationException("Giờ công trùng với ca khác. Hai ca giao nhau 13:30–14:00: chỉnh giờ ra/vào thực tế để không tính trùng.");
                }

                return XuLyDuLieu.ThucThi(ketNoi, giaoDich, ma == 0 ? "INSERT dbo.BangCong(MaNhanVien,Ngay,MaCa,GioVao,GioRa,Vang,DieuChinhPhut,LyDo,LuongGio,MaNguoiCapNhat) VALUES(@nv,@ngay,@ca,@vao,@ra,@vang,@dc,@ld,@lg,@by)" : "UPDATE dbo.BangCong SET LuongGio=CASE WHEN MaNhanVien=@nv THEN LuongGio ELSE @lg END,MaNhanVien=@nv,Ngay=@ngay,MaCa=@ca,GioVao=@vao,GioRa=@ra,Vang=@vang,DieuChinhPhut=@dc,LyDo=@ld,MaNguoiCapNhat=@by,CapNhatLuc=SYSDATETIME() WHERE MaBangCong=@id", XuLyDuLieu.ThamSo("@nv", maTho), XuLyDuLieu.ThamSo("@ngay", ngay.Date), XuLyDuLieu.ThamSo("@ca", maCa), XuLyDuLieu.ThamSo("@vao", vang ? (object)DBNull.Value : gioVao), XuLyDuLieu.ThamSo("@ra", vang ? (object)DBNull.Value : gioRa), XuLyDuLieu.ThamSo("@vang", vang), XuLyDuLieu.ThamSo("@dc", dieuChinhPhut), XuLyDuLieu.ThamSo("@ld", lyDo.Trim()), XuLyDuLieu.ThamSo("@lg", bangNhanVien.Rows[0][0]), XuLyDuLieu.ThamSo("@by", PhienDangNhap.MaNhanVien), XuLyDuLieu.ThamSo("@id", ma));
            });
        }
    }
}
