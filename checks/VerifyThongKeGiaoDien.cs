using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using QLyCuaHangSuaXe.DanhMuc;
using QLyCuaHangSuaXe.LopDungChung;

public static class KiemTraThongKeGiaoDien
{
    static readonly XuLyDuLieu duLieu = new XuLyDuLieu();
    static string thuMuc;
    static int soKiemTra;

    static T Lay<T>(object trang, string ten)
    {
        for (Type kieu = trang.GetType(); kieu != null; kieu = kieu.BaseType)
        {
            var truong = kieu.GetField(ten, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.DeclaredOnly);
            if (truong != null) return (T)truong.GetValue(trang);
        }
        throw new InvalidOperationException("Không có điều khiển " + ten);
    }

    static void Dung(bool dieuKien, string ten)
    {
        if (!dieuKien) throw new InvalidOperationException(ten);
        soKiemTra++;
        Console.WriteLine("PASS thống kê: " + ten);
    }

    static string BaoTen(string ten) { return "[" + ten.Replace("]", "]]") + "]"; }

    static Dictionary<string, string> DauVanTay()
    {
        var ketQua = new Dictionary<string, string>();
        foreach (DataRow bang in duLieu.DocBang("SELECT s.name AS LuocDo,t.name AS Bang,t.object_id FROM sys.tables t JOIN sys.schemas s ON s.schema_id=t.schema_id ORDER BY s.name,t.name").Rows)
        {
            string ten = BaoTen(bang["LuocDo"].ToString()) + "." + BaoTen(bang["Bang"].ToString());
            var khoa = duLieu.DocBang("SELECT c.name FROM sys.indexes i JOIN sys.index_columns ic ON ic.object_id=i.object_id AND ic.index_id=i.index_id JOIN sys.columns c ON c.object_id=ic.object_id AND c.column_id=ic.column_id WHERE i.object_id=@id AND i.is_primary_key=1 ORDER BY ic.key_ordinal", XuLyDuLieu.ThamSo("@id", bang["object_id"]));
            string sapXep = string.Join(",", khoa.AsEnumerable().Select(d => BaoTen(d[0].ToString())));
            var json = duLieu.DocBang("SELECT (SELECT * FROM " + ten + (sapXep.Length > 0 ? " ORDER BY " + sapXep : "") + " FOR JSON PATH,INCLUDE_NULL_VALUES) AS DuLieu");
            string noiDung = string.Concat(json.AsEnumerable().Select(d => d[0].ToString()));
            using (var bam = SHA256.Create()) ketQua[ten] = Convert.ToBase64String(bam.ComputeHash(Encoding.UTF8.GetBytes(noiDung)));
        }
        return ketQua;
    }

    static DataPoint[] CacDiem(Chart bieuDo)
    {
        return bieuDo.Series.Cast<Series>().Where(s => s.Enabled).SelectMany(s => s.Points.Cast<DataPoint>()).Where(d => !d.IsEmpty).ToArray();
    }

    static void Chup(Form trang, string ten)
    {
        Application.DoEvents();
        using (var anh = new Bitmap(trang.Width, trang.Height))
        {
            trang.DrawToBitmap(anh, new Rectangle(Point.Empty, anh.Size));
            anh.Save(Path.Combine(thuMuc, ten + ".png"));
        }
    }

    static void KiemTraBaoCao(frmThongKe trang, int chiSo)
    {
        var bang = (DataTable)Lay<DataGridView>(trang, "dgvThongKe").DataSource;
        var bieuDo = Lay<Chart>(trang, "bieuDoThongKe");
        var cacDiem = CacDiem(bieuDo);
        Dung(bang != null && bieuDo.ChartAreas.Count > 0, "Báo cáo " + chiSo + " có bảng và vùng biểu đồ");
        Dung(cacDiem.All(d => d.YValues.Length > 0 && !double.IsNaN(d.YValues[0]) && !double.IsInfinity(d.YValues[0]) && d.YValues[0] >= 0), "Báo cáo " + chiSo + " không có giá trị biểu đồ âm hoặc không hợp lệ");
        string[] cacCot = { "DoanhThu", "SoXeHoanThanh", "DoanhThu", "GiaTriTon", "TongTien", "SoPhieu", "GioThucLam" };
        double[] duKien;
        if (chiSo == 0 && bang.Rows.Count > 31)
        {
            var theoThang = bang.AsEnumerable().GroupBy(d => new DateTime(Convert.ToDateTime(d["Ngay"]).Year, Convert.ToDateTime(d["Ngay"]).Month, 1));
            duKien = theoThang.Count() > 24
                ? bang.AsEnumerable().GroupBy(d => Convert.ToDateTime(d["Ngay"]).Year).Select(n => n.Sum(d => Convert.ToDouble(d["DoanhThu"]))).OrderByDescending(d => d).ToArray()
                : theoThang.Select(n => n.Sum(d => Convert.ToDouble(d["DoanhThu"]))).OrderByDescending(d => d).ToArray();
        }
        else duKien = bang.AsEnumerable().Select(d => Convert.ToDouble(d[cacCot[chiSo]], CultureInfo.InvariantCulture)).Where(d => chiSo != 5 || d > 0).OrderByDescending(d => d).Take(chiSo == 0 || chiSo == 5 ? int.MaxValue : 8).ToArray();
        double[] thucTe = cacDiem.Select(d => d.YValues[0]).OrderByDescending(d => d).ToArray();
        Dung(duKien.Length == thucTe.Length && duKien.Zip(thucTe, (a, b) => Math.Abs(a - b) < Math.Max(0.001, Math.Abs(a) * 0.0000001)).All(d => d), "Báo cáo " + chiSo + " biểu đồ khớp giá trị bảng và giới hạn hạng mục");
        decimal[] cacChiSo = ChiSoDuKien(bang, chiSo);
        string[] cacTen = { "lblGiaTriMot", "lblGiaTriHai", "lblGiaTriBa" };
        for (int i = 0; i < cacTen.Length; i++)
        {
            string giaTri = Lay<Label>(trang, cacTen[i]).Text;
            Dung(!string.IsNullOrWhiteSpace(giaTri) && !giaTri.Contains("NaN") && !giaTri.Contains("Infinity"), "Báo cáo " + chiSo + " hiển thị chỉ số " + cacTen[i]);
            string so = new string(giaTri.Where(c => char.IsDigit(c) || c == '.' || c == ',' || c == '-').ToArray());
            int soLe = so.Contains(',') ? so.Length - so.LastIndexOf(',') - 1 : 0;
            decimal giaTriHienThi = so.Length == 0 ? 0 : decimal.Parse(so, NumberStyles.Number, CultureInfo.GetCultureInfo("vi-VN"));
            Dung(giaTriHienThi == Math.Round(cacChiSo[i], soLe, MidpointRounding.ToEven), "Báo cáo " + chiSo + " chỉ số " + (i + 1) + " khớp tổng dữ liệu: " + giaTri);
        }
        Dung(Lay<Button>(trang, "btnThongKe").Visible && Lay<Button>(trang, "btnXuatExcel").Visible, "Báo cáo " + chiSo + " giữ hai thao tác trên form");
    }

    static decimal Tong(DataTable bang, string cot)
    {
        return bang.AsEnumerable().Sum(d => Convert.ToDecimal(d[cot], CultureInfo.InvariantCulture));
    }

    static decimal[] ChiSoDuKien(DataTable bang, int chiSo)
    {
        switch (chiSo)
        {
            case 0:
                decimal doanhThu = Tong(bang, "DoanhThu"), hoaDon = Tong(bang, "SoHoaDon");
                return new[] { doanhThu, hoaDon, hoaDon == 0 ? 0 : doanhThu / hoaDon };
            case 1: return new[] { Tong(bang, "SoXeHoanThanh"), bang.Rows.Count, Tong(bang, "TongPhut") / 60 };
            case 2: return new[] { Tong(bang, "DoanhThu"), Tong(bang, "SoLuong"), bang.Rows.Count };
            case 3: return new[] { Tong(bang, "GiaTriTon"), Tong(bang, "SoLuongTon"), bang.AsEnumerable().Count(d => Convert.ToInt32(d["SoLuongTon"]) <= 5) };
            case 4: return new[] { Tong(bang, "TongTien"), bang.Rows.Count, bang.AsEnumerable().Select(d => d["MaNhanVien"].ToString()).Distinct().Count() };
            case 5: return new[] { Tong(bang, "SoPhieu"), bang.AsEnumerable().Where(d => d["TrangThai"].ToString() == "Hoàn thành").Sum(d => Convert.ToDecimal(d["SoPhieu"])), Tong(bang, "QuaHen") };
            case 6: return new[] { Tong(bang, "GioThucLam"), Tong(bang, "SoCa"), Tong(bang, "CaVang") };
            default: throw new InvalidOperationException("Không có báo cáo " + chiSo);
        }
    }

    [STAThread]
    public static int Main(string[] thamSo)
    {
        try
        {
            thuMuc = thamSo.Length > 0 ? Path.GetFullPath(thamSo[0]) : Path.GetFullPath("../TichHopKho_QA/ThongKe_20261029");
            Directory.CreateDirectory(thuMuc);
            if (new SqlConnectionStringBuilder(XuLyDuLieu.ChuoiKetNoi).InitialCatalog != "QuanLySuaXe" || duLieu.DocBang("SELECT DB_NAME()").Rows[0][0].ToString() != "QuanLySuaXe") throw new InvalidOperationException("Chỉ kiểm tra chỉ đọc trên QuanLySuaXe.");
            var truoc = DauVanTay();
            if (!PhienDangNhap.DangNhap("minh", "123")) throw new InvalidOperationException("Không đăng nhập được minh/123.");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            using (var trang = new frmThongKe())
            {
                trang.StartPosition = FormStartPosition.Manual;
                trang.Location = new Point(-4000, -4000);
                trang.Size = new Size(1280, 900);
                trang.Show();
                Application.DoEvents();
                Lay<DateTimePicker>(trang, "dtpTuNgay").Value = new DateTime(2026, 1, 1);
                Lay<DateTimePicker>(trang, "dtpDenNgay").Value = new DateTime(2026, 12, 31);
                for (int chiSo = 0; chiSo < 7; chiSo++)
                {
                    Lay<ComboBox>(trang, "cboBaoCao").SelectedIndex = chiSo;
                    Lay<Button>(trang, "btnThongKe").PerformClick();
                    Application.DoEvents();
                    KiemTraBaoCao(trang, chiSo);
                    Chup(trang, "thong-ke-" + chiSo + "-1280");
                }
                Lay<ComboBox>(trang, "cboBaoCao").SelectedIndex = 0;
                Lay<Button>(trang, "btnThongKe").PerformClick();
                trang.Size = new Size(1000, 780);
                Chup(trang, "thong-ke-0-1000");
                Dung(Lay<Chart>(trang, "bieuDoThongKe").Height >= 120 && Lay<DataGridView>(trang, "dgvThongKe").Height >= 100, "Biểu đồ và bảng còn đủ vùng hiển thị tại 1000 x 780");
                trang.Size = new Size(1280, 900);
                trang.Scale(new SizeF(1.25F, 1.25F));
                Chup(trang, "thong-ke-0-125-phan-tram");
                Dung(Lay<Button>(trang, "btnThongKe").Width >= 100 && Lay<Button>(trang, "btnXuatExcel").Width >= 100, "Hai nút không co nhỏ ở mức 125%");
                Lay<DateTimePicker>(trang, "dtpTuNgay").Value = new DateTime(2090, 1, 1);
                Lay<DateTimePicker>(trang, "dtpDenNgay").Value = new DateTime(2090, 1, 31);
                for (int chiSo = 0; chiSo < 7; chiSo++)
                {
                    if (chiSo == 3) continue;
                    Lay<ComboBox>(trang, "cboBaoCao").SelectedIndex = chiSo;
                    Lay<Button>(trang, "btnThongKe").PerformClick();
                    Application.DoEvents();
                    Dung(((DataTable)Lay<DataGridView>(trang, "dgvThongKe").DataSource).Rows.Count == 0 && CacDiem(Lay<Chart>(trang, "bieuDoThongKe")).Length == 0, "Báo cáo " + chiSo + " xóa biểu đồ cũ khi không có dữ liệu");
                    KiemTraBaoCao(trang, chiSo);
                }
                Chup(trang, "thong-ke-rong");
                Lay<ComboBox>(trang, "cboBaoCao").SelectedIndex = 0;
                Lay<DateTimePicker>(trang, "dtpTuNgay").Value = new DateTime(2090, 2, 1);
                bool biChan = false;
                try { trang.TaiDuLieu(); } catch (InvalidOperationException) { biChan = true; }
                Dung(biChan, "Chặn khoảng ngày đảo ngược trước khi cập nhật báo cáo");
            }
            var sau = DauVanTay();
            Dung(truoc.Count == sau.Count && truoc.All(d => sau.ContainsKey(d.Key) && sau[d.Key] == d.Value), "SHA-256 toàn bộ " + sau.Count + " bảng SQL giữ nguyên");
            Console.WriteLine("PASS tổng: " + soKiemTra + " kiểm tra thống kê; ảnh tại " + thuMuc);
            return 0;
        }
        catch (Exception loi) { Console.Error.WriteLine(loi); return 1; }
    }
}
