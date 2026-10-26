using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace QLyCuaHangSuaXe.LopDungChung
{
    public sealed class MauInPhieu
    {
        sealed class DongIn
        {
            public string[] NoiDung;
            public float Cao;
            public bool LaTieuDe;
        }

        readonly List<List<DongIn>> cacTrang = new List<List<DongIn>>();
        readonly string maPhieu;
        readonly decimal tongTien;
        public int SoTrang { get { return cacTrang.Count; } }
        public string MaPhieu { get { return maPhieu; } }

        public MauInPhieu(string ma, DataTable chiTiet, params string[] thongTin)
        {
            maPhieu = ma;
            tongTien = chiTiet.AsEnumerable().Sum(dong => Convert.ToDecimal(dong["ThanhTien"]));
            using (var anh = new Bitmap(1, 1))
            using (var ve = Graphics.FromImage(anh))
            using (var chu = new Font("Segoe UI", 10))
            {
                float viTri = 135;
                var trang = new List<DongIn>();
                cacTrang.Add(trang);
                Action<string[], bool> them = (noiDung, tieuDe) =>
                {
                    float cao = noiDung.Length == 1
                        ? ve.MeasureString(noiDung[0], chu, 688).Height + 12
                        : noiDung.Select((giaTri, cot) => ve.MeasureString(giaTri, chu, cot == 0 ? 340 : 100).Height + 16).Max();
                    cao = Math.Max(cao, 34);
                    if (viTri + cao > 920)
                    {
                        trang = new List<DongIn>();
                        cacTrang.Add(trang);
                        viTri = 135;
                        if (noiDung.Length > 1 && !tieuDe)
                        {
                            trang.Add(new DongIn { NoiDung = new[] { "Hạng mục", "SL", "Đơn giá", "Thành tiền" }, Cao = 36, LaTieuDe = true });
                            viTri += 36;
                        }
                    }
                    trang.Add(new DongIn { NoiDung = noiDung, Cao = cao, LaTieuDe = tieuDe });
                    viTri += cao;
                };
                foreach (string thongTinPhieu in thongTin)
                {
                    string[] cacDong = (thongTinPhieu ?? "").Replace("\r", "").Split('\n');
                    foreach (string dong in cacDong)
                    {
                        for (int dau = 0; dau < Math.Max(1, dong.Length); dau += 300)
                            them(new[] { dong.Substring(dau, Math.Min(300, dong.Length - dau)) }, false);
                    }
                }
                them(new[] { "Hạng mục", "SL", "Đơn giá", "Thành tiền" }, true);
                foreach (DataRow dong in chiTiet.Rows)
                {
                    bool duThao = chiTiet.Columns.Contains("TenDichVu");
                    string noiDung = duThao ? dong["TenDichVu"].ToString() : dong["Loai"] + ": " + dong["NoiDung"];
                    if (duThao && dong["PhuTung"].ToString().Length > 0)
                        noiDung += "\nPhụ tùng: " + dong["PhuTung"];
                    decimal donGia = duThao ? Convert.ToDecimal(dong["TienCong"]) + Convert.ToDecimal(dong["GiaPhuTung"]) : Convert.ToDecimal(dong["DonGia"]);
                    them(new[] { noiDung, dong["SoLuong"].ToString(), donGia.ToString("N0"), Convert.ToDecimal(dong["ThanhTien"]).ToString("N0") }, false);
                }
                if (viTri > 770)
                    cacTrang.Add(new List<DongIn>());
            }
        }

        public Bitmap TaoAnh(int soTrang)
        {
            if (soTrang < 0 || soTrang >= SoTrang)
                throw new ArgumentOutOfRangeException("soTrang");
            var anh = new Bitmap(794, 1123);
            anh.SetResolution(96, 96);
            using (var ve = Graphics.FromImage(anh))
            using (var chu = new Font("Segoe UI", 10))
            using (var dam = new Font("Segoe UI", 10, FontStyle.Bold))
            using (var tieuDe = new Font("Segoe UI", 21, FontStyle.Bold))
            using (var xanh = new SolidBrush(Color.FromArgb(30, 85, 130)))
            using (var nen = new SolidBrush(Color.FromArgb(233, 242, 249)))
            using (var vien = new Pen(Color.FromArgb(206, 216, 225)))
            {
                ve.Clear(Color.White);
                ve.SmoothingMode = SmoothingMode.AntiAlias;
                ve.DrawString("PHIẾU TIẾP NHẬN XE", tieuDe, xanh, 48, 46);
                ve.DrawString("Mã phiếu: " + maPhieu, dam, Brushes.DimGray, 50, 96);
                float y = 135;
                float[] x = { 50, 410, 510, 620, 744 };
                foreach (var dong in cacTrang[soTrang])
                {
                    if (dong.NoiDung.Length == 1)
                        ve.DrawString(dong.NoiDung[0], chu, Brushes.Black, new RectangleF(52, y + 4, 688, dong.Cao - 4));
                    else
                    {
                        if (dong.LaTieuDe)
                            ve.FillRectangle(nen, 50, y, 694, dong.Cao);
                        for (int cot = 0; cot < 4; cot++)
                        {
                            ve.DrawRectangle(vien, x[cot], y, x[cot + 1] - x[cot], dong.Cao);
                            using (var dinhDang = new StringFormat { Alignment = cot < 2 ? StringAlignment.Near : StringAlignment.Far })
                                ve.DrawString(dong.NoiDung[cot], dong.LaTieuDe ? dam : chu, Brushes.Black,
                                    new RectangleF(x[cot] + 8, y + 7, x[cot + 1] - x[cot] - 16, dong.Cao - 8), dinhDang);
                        }
                    }
                    y += dong.Cao;
                }
                if (soTrang == SoTrang - 1)
                {
                    y += 22;
                    ve.DrawString("TỔNG DỰ KIẾN: " + tongTien.ToString("N0") + " đ", dam, xanh, 410, y);
                    ve.DrawString("Chi phí được xác nhận theo hạng mục thực tế.", chu, Brushes.DimGray, 50, y + 40);
                    ve.DrawString("KHÁCH HÀNG", dam, Brushes.Black, 130, y + 90);
                    ve.DrawString("NGƯỜI TIẾP NHẬN", dam, Brushes.Black, 505, y + 90);
                    ve.DrawString("(Ký, ghi rõ họ tên)", chu, Brushes.DimGray, 122, y + 114);
                    ve.DrawString("(Ký, ghi rõ họ tên)", chu, Brushes.DimGray, 510, y + 114);
                }
                ve.DrawLine(vien, 50, 1050, 744, 1050);
                ve.DrawString("Phiếu " + maPhieu + "   •   Trang " + (soTrang + 1) + "/" + SoTrang, chu, Brushes.Gray, 50, 1064);
            }
            return anh;
        }
    }
}
