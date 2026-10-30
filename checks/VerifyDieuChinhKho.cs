using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using QLyCuaHangSuaXe.LopDungChung;

public static class KiemTraDieuChinhKho
{
    static readonly XuLyDuLieu duLieu = new XuLyDuLieu();
    static readonly BindingFlags co = BindingFlags.Instance | BindingFlags.NonPublic;
    static int soKiemTra;

    static void Dung(bool dieuKien, string noiDung)
    {
        if (!dieuKien) throw new Exception("ĐIỀU CHỈNH KHO: " + noiDung);
        soKiemTra++;
        Console.WriteLine("PASS ĐIỀU CHỈNH KHO: " + noiDung);
    }
    static T Lay<T>(object trang, string ten)
    {
        var truong = trang.GetType().GetField(ten, co);
        if (truong == null) throw new Exception("Không có thành phần " + ten);
        return (T)truong.GetValue(trang);
    }
    static object Goi(object trang, string ten)
    {
        try { return trang.GetType().GetMethod(ten, co).Invoke(trang, new object[0]); }
        catch (TargetInvocationException loi) { throw loi.InnerException ?? loi; }
    }
    static object GiaTri(string cauLenh, params SqlParameter[] thamSo)
    {
        return duLieu.DocBang(cauLenh, thamSo).Rows[0][0];
    }
    static DataRow PhuTung(string ma)
    {
        return duLieu.DocBang("SELECT * FROM dbo.PhuTung WHERE MaPhuTung=@ma", XuLyDuLieu.ThamSo("@ma", ma)).Rows[0];
    }
    static int Dem(string bang, string ma)
    {
        if (bang != "DieuChinhTonKho" && bang != "DieuChinhGiaBan") throw new ArgumentException("Sai bảng kiểm thử.");
        return Convert.ToInt32(GiaTri("SELECT COUNT(*) FROM dbo." + bang + " WHERE MaPhuTung=@ma", XuLyDuLieu.ThamSo("@ma", ma)));
    }
    static void Chan<T>(Action thaoTac, string noiDung) where T : Exception
    {
        try { thaoTac(); }
        catch (T) { Dung(true, noiDung); return; }
        throw new Exception("Không từ chối: " + noiDung);
    }
    static void Hien(Form trang)
    {
        trang.StartPosition = FormStartPosition.Manual;
        trang.Location = new Point(-4000, -4000);
        trang.Show();
        Application.DoEvents();
    }
    static void Anh(Form trang, string ten)
    {
        Directory.CreateDirectory("checks/output/sql-moi");
        using (var anh = new Bitmap(trang.Width, trang.Height))
        {
            trang.DrawToBitmap(anh, new Rectangle(Point.Empty, anh.Size));
            anh.Save("checks/output/sql-moi/" + ten + ".png");
        }
    }
    static string DocMuc(ZipArchive tep, string ten)
    {
        var muc = tep.GetEntry(ten);
        if (muc == null) throw new Exception("Excel thiếu " + ten);
        using (var doc = new StreamReader(muc.Open())) return doc.ReadToEnd();
    }
    static void KiemTraExcel(DataTable[] cacBang, string ma)
    {
        Directory.CreateDirectory("checks/output/sql-moi");
        string duongDan = Path.GetFullPath("checks/output/sql-moi/kho-ba-trang.xlsx");
        XuatExcel.GhiNhieuBang(cacBang, duongDan, new[] { "Phụ tùng", "Điều chỉnh tồn", "Điều chỉnh giá bán" });
        using (var tep = ZipFile.OpenRead(duongDan))
        {
            Dung(tep.Entries.Count(e => e.FullName.StartsWith("xl/worksheets/sheet") && e.FullName.EndsWith(".xml")) == 3, "Excel có đủ ba trang kho, lịch sử tồn và lịch sử giá");
            string ton = DocMuc(tep, "xl/worksheets/sheet2.xml");
            Dung(ton.Contains(ma) && ton.Contains("Kiểm kê O'Neil — đủ hàng") && ton.Contains("Tổng thừa") && ton.Contains("Tổng thiếu") && ton.Contains("Tổng chênh lệch"), "Excel giữ tiếng Việt, dấu nháy và ba tổng của lịch sử tồn");
            var taiLieu = new XmlDocument(); taiLieu.LoadXml(ton);
            var khongGian = new XmlNamespaceManager(taiLieu.NameTable);
            khongGian.AddNamespace("x", "http://schemas.openxmlformats.org/spreadsheetml/2006/main");
            Dung(taiLieu.SelectNodes("//x:c[not(@t)]/x:v", khongGian).Cast<XmlNode>().Any(n => n.InnerText == "-100000"), "Giá trị thiếu kho được xuất dưới dạng số âm để Excel cộng được");
            Dung(DocMuc(tep, "xl/worksheets/sheet3.xml").Contains("Xả hàng có xác nhận"), "Excel xuất được lịch sử giá bán dưới giá nhập đã xác nhận");
        }
        string tenDai = "Kho:/\\?*[] phụ tùng và lịch sử kiểm kê rất dài";
        string tepTen = Path.GetFullPath("checks/output/sql-moi/kho-ten-trang.xlsx");
        XuatExcel.GhiNhieuBang(cacBang, tepTen, new[] { tenDai, tenDai, "[]:*?/\\" });
        using (var tep = ZipFile.OpenRead(tepTen))
        {
            var taiLieu = new XmlDocument(); taiLieu.LoadXml(DocMuc(tep, "xl/workbook.xml"));
            var khongGian = new XmlNamespaceManager(taiLieu.NameTable);
            khongGian.AddNamespace("x", "http://schemas.openxmlformats.org/spreadsheetml/2006/main");
            var cacTen = taiLieu.SelectNodes("//x:sheet", khongGian).Cast<XmlNode>().Select(n => n.Attributes["name"].Value).ToArray();
            Dung(cacTen.Length == 3 && cacTen.Distinct(StringComparer.OrdinalIgnoreCase).Count() == 3 && cacTen.All(t => t.Length > 0 && t.Length <= 31 && !Regex.IsMatch(t, @"[\[\]:*?/\\]")) && cacTen[1].EndsWith(" 2"), "Tên trang Excel bỏ ký tự cấm, giới hạn 31 ký tự và thêm hậu tố khi trùng");
        }
        Chan<InvalidOperationException>(() => XuatExcel.GhiNhieuBang(new[] { cacBang[0] }, tepTen, new string[0]), "Excel từ chối số bảng và số tiêu đề không khớp");
    }
    public static int Chay()
    {
        soKiemTra = 0;
        Dung(new SqlConnectionStringBuilder(XuLyDuLieu.ChuoiKetNoi).InitialCatalog == "QuanLySuaXe_CodexChecks" && GiaTri("SELECT DB_NAME()").ToString() == "QuanLySuaXe_CodexChecks", "Chỉ ghi vào bản sao QuanLySuaXe_CodexChecks");
        Dung(PhienDangNhap.DangNhap("minh", "123"), "Đăng nhập quản lý để điều chỉnh kho");
        string nguoiGhi = PhienDangNhap.MaNhanVien;
        string ma = XuLyDuLieu.TaoMaMoi("PT");
        string nhaCungCap = GiaTri("SELECT TOP(1) MaNhaCungCap FROM dbo.NhaCungCap ORDER BY MaNhaCungCap").ToString();
        duLieu.CapNhatDuLieu("INSERT dbo.PhuTung(MaPhuTung,MaNhaCungCap,TenPhuTung,DonGiaNhap,DonGiaBan,SoLuongTon,HanSuDung,TrangThai) VALUES(@ma,@ncc,N'Phụ tùng kiểm thử điều chỉnh kho',100000,150000,10,@han,N'Đang kinh doanh')", XuLyDuLieu.ThamSo("@ma", ma), XuLyDuLieu.ThamSo("@ncc", nhaCungCap), XuLyDuLieu.ThamSo("@han", DateTime.Today.AddYears(5)));
        try
        {
            Chan<InvalidOperationException>(() => QuanLyDieuChinhKho.DieuChinhTon(ma, 0, "Không thay đổi"), "Chặn điều chỉnh tồn bằng 0");
            Chan<InvalidOperationException>(() => QuanLyDieuChinhKho.DieuChinhTon(ma, 1, "  "), "Bắt buộc lý do điều chỉnh tồn");
            Chan<InvalidOperationException>(() => QuanLyDieuChinhKho.DieuChinhTon(ma, 1, new string('a', 256)), "Lý do quá 255 ký tự bị từ chối trước khi ghi");
            Chan<InvalidOperationException>(() => QuanLyDieuChinhKho.DieuChinhTon(ma, -11, "Thiếu hàng"), "Không cho giảm tồn xuống số âm");
            Chan<InvalidOperationException>(() => QuanLyDieuChinhKho.DieuChinhTon("PT99999999", 1, "Không có phụ tùng"), "Không điều chỉnh mã phụ tùng không tồn tại");
            Dung(Convert.ToInt32(PhuTung(ma)["SoLuongTon"]) == 10 && Dem("DieuChinhTonKho", ma) == 0, "Các dữ liệu tồn sai không đổi kho và không tạo lịch sử");

            string dc = QuanLyDieuChinhKho.DieuChinhTon(ma, 3, "  Kiểm kê O'Neil — đủ hàng  ");
            var lichSu = duLieu.DocBang("SELECT * FROM dbo.DieuChinhTonKho WHERE MaDieuChinh=@ma", XuLyDuLieu.ThamSo("@ma", dc)).Rows[0];
            Dung(Regex.IsMatch(dc, "^DC[0-9]{3,}$") && Convert.ToInt32(PhuTung(ma)["SoLuongTon"]) == 13 && Convert.ToInt32(lichSu["SoLuongCu"]) == 10 && Convert.ToInt32(lichSu["SoLuongMoi"]) == 13, "Tăng tồn tạo mã DC thống nhất và lưu đúng tồn trước/sau");
            Dung(lichSu["MaNhanVien"].ToString() == nguoiGhi && lichSu["LyDo"].ToString() == "Kiểm kê O'Neil — đủ hàng" && Convert.ToDateTime(lichSu["NgayDieuChinh"]).Date == DateTime.Today && Convert.ToDecimal(lichSu["DonGiaNhap"]) == 100000m && Convert.ToDecimal(lichSu["GiaTriChenhLech"]) == 300000m, "Lịch sử lấy người ghi từ phiên, lưu lý do Unicode và giá nhập tại thời điểm điều chỉnh");
            QuanLyDieuChinhKho.DieuChinhTon(ma, -1, "Kiểm kê thiếu một phụ tùng");
            Dung(Convert.ToInt32(PhuTung(ma)["SoLuongTon"]) == 12 && Convert.ToDecimal(GiaTri("SELECT MIN(GiaTriChenhLech) FROM dbo.DieuChinhTonKho WHERE MaPhuTung=@ma", XuLyDuLieu.ThamSo("@ma", ma))) == -100000m, "Giảm tồn lưu giá trị thiếu kho âm");
            Chan<InvalidOperationException>(() => QuanLyDieuChinhKho.DieuChinhTon(ma, 2, "Mã màn hình cũ", dc), "Mã điều chỉnh đã dùng bị chặn khi màn hình khác lưu trước");
            Dung(Convert.ToInt32(PhuTung(ma)["SoLuongTon"]) == 12 && Dem("DieuChinhTonKho", ma) == 2, "Trùng mã không cập nhật tồn hoặc tạo lịch sử dở");

            Task.WaitAll(Task.Run(() => QuanLyDieuChinhKho.DieuChinhTon(ma, 1, "Máy thứ nhất")), Task.Run(() => QuanLyDieuChinhKho.DieuChinhTon(ma, 1, "Máy thứ hai")));
            var dongSongSong = duLieu.DocBang("SELECT SoLuongCu,SoLuongMoi FROM dbo.DieuChinhTonKho WHERE MaPhuTung=@ma AND LyDo IN(N'Máy thứ nhất',N'Máy thứ hai') ORDER BY SoLuongCu", XuLyDuLieu.ThamSo("@ma", ma));
            Dung(Convert.ToInt32(PhuTung(ma)["SoLuongTon"]) == 14 && dongSongSong.Rows.Count == 2 && Convert.ToInt32(dongSongSong.Rows[0]["SoLuongCu"]) == 12 && Convert.ToInt32(dongSongSong.Rows[1]["SoLuongCu"]) == 13, "Hai máy tăng cùng phụ tùng được tuần tự hóa, không mất cập nhật tồn");
            duLieu.CapNhatDuLieu("UPDATE dbo.PhuTung SET SoLuongTon=@so WHERE MaPhuTung=@ma", XuLyDuLieu.ThamSo("@so", int.MaxValue), XuLyDuLieu.ThamSo("@ma", ma));
            Chan<InvalidOperationException>(() => QuanLyDieuChinhKho.DieuChinhTon(ma, 1, "Tràn số"), "Cộng tồn vượt Int32 bị chặn có thông báo rõ");
            Dung(Convert.ToInt32(PhuTung(ma)["SoLuongTon"]) == int.MaxValue && Dem("DieuChinhTonKho", ma) == 4, "Tràn số không ghi một phần dữ liệu");
            duLieu.CapNhatDuLieu("UPDATE dbo.PhuTung SET SoLuongTon=14 WHERE MaPhuTung=@ma", XuLyDuLieu.ThamSo("@ma", ma));

            Chan<InvalidOperationException>(() => QuanLyDieuChinhKho.DieuChinhGia(ma, 0, "Giá không hợp lệ"), "Giá bán mới phải lớn hơn 0");
            Chan<InvalidOperationException>(() => QuanLyDieuChinhKho.DieuChinhGia(ma, 100000.5m, "Đồng lẻ"), "Giá bán phải là số đồng nguyên đúng kiểu SQL");
            Chan<InvalidOperationException>(() => QuanLyDieuChinhKho.DieuChinhGia(ma, 1000000000000000000m, "Vượt cột SQL"), "Giá bán vượt 18 chữ số bị chặn");
            Chan<InvalidOperationException>(() => QuanLyDieuChinhKho.DieuChinhGia(ma, 150000, "Giá không thay đổi"), "Chặn giá bán mới trùng giá hiện tại");
            Chan<InvalidOperationException>(() => QuanLyDieuChinhKho.DieuChinhGia(ma, 160000, ""), "Đổi giá cũng bắt buộc lý do");
            Chan<InvalidOperationException>(() => QuanLyDieuChinhKho.DieuChinhGia(ma, 90000, "Chưa xác nhận"), "Bán dưới giá nhập bị chặn khi chưa xác nhận rõ");
            Dung(Dem("DieuChinhGiaBan", ma) == 0 && Convert.ToDecimal(PhuTung(ma)["DonGiaBan"]) == 150000m, "Các giá không hợp lệ giữ nguyên giá đang dùng và lịch sử");
            string dg = QuanLyDieuChinhKho.DieuChinhGia(ma, 90000, "Xả hàng có xác nhận", true);
            var lichSuGia = duLieu.DocBang("SELECT * FROM dbo.DieuChinhGiaBan WHERE MaDieuChinhGia=@ma", XuLyDuLieu.ThamSo("@ma", dg)).Rows[0];
            Dung(Regex.IsMatch(dg, "^DG[0-9]{3,}$") && lichSuGia["MaNhanVien"].ToString() == nguoiGhi && Convert.ToDecimal(lichSuGia["GiaBanCu"]) == 150000m && Convert.ToDecimal(lichSuGia["GiaBanMoi"]) == 90000m && Convert.ToDecimal(PhuTung(ma)["DonGiaBan"]) == 90000m, "Xác nhận dưới giá nhập lưu giá mới và đủ lịch sử DG theo phiên");
            Chan<InvalidOperationException>(() => QuanLyDieuChinhKho.DieuChinhGia(ma, 170000, "Mã giá cũ", false, dg), "Mã đổi giá trùng bị chặn");
            Dung(Dem("DieuChinhGiaBan", ma) == 1 && Convert.ToDecimal(PhuTung(ma)["DonGiaBan"]) == 90000m && Convert.ToDecimal(PhuTung(ma)["DonGiaNhap"]) == 100000m && Convert.ToInt32(PhuTung(ma)["SoLuongTon"]) == 14, "Trùng mã giá không đổi dữ liệu, đổi giá không đổi tồn hoặc giá nhập");

            duLieu.CapNhatDuLieu("CREATE TRIGGER dbo.CodexKhoFailUpdate ON dbo.PhuTung AFTER UPDATE AS BEGIN SET NOCOUNT ON; IF EXISTS(SELECT 1 FROM inserted WHERE MaPhuTung='" + ma + "') THROW 51991,N'Lỗi kiểm thử sau khi đã ghi lịch sử',1; END");
            try
            {
                Chan<SqlException>(() => QuanLyDieuChinhKho.DieuChinhTon(ma, 2, "Kiểm tra hoàn tác tồn"), "Lỗi cập nhật tồn sau khi chèn lịch sử được trả về");
                Chan<SqlException>(() => QuanLyDieuChinhKho.DieuChinhGia(ma, 180000, "Kiểm tra hoàn tác giá"), "Lỗi cập nhật giá sau khi chèn lịch sử được trả về");
                Dung(Dem("DieuChinhTonKho", ma) == 4 && Dem("DieuChinhGiaBan", ma) == 1 && Convert.ToInt32(PhuTung(ma)["SoLuongTon"]) == 14 && Convert.ToDecimal(PhuTung(ma)["DonGiaBan"]) == 90000m, "Giao dịch hoàn tác cả lịch sử và phụ tùng khi UPDATE thất bại");
            }
            finally { duLieu.CapNhatDuLieu("DROP TRIGGER dbo.CodexKhoFailUpdate"); }

            using (var trang = new btl.frmKho())
            {
                Hien(trang);
                Lay<TextBox>(trang, "txtTimPhuTung").Text = ma;
                Application.DoEvents();
                var tab = Lay<TabControl>(trang, "tabKho");
                var ton = Lay<DataGridView>(trang, "dgvLichSuTon");
                var gia = Lay<DataGridView>(trang, "dgvLichSuGia");
                Dung(tab.TabPages.Count == 3 && Lay<DataGridView>(trang, "dgvPhuTung").Rows.Count == 1 && ton.Rows.Count == 4 && gia.Rows.Count == 1 && ton.ReadOnly && gia.ReadOnly, "Kho có ba thẻ dùng chung bộ lọc, hai lịch sử chỉ đọc");
                Dung(Lay<TextBox>(trang, "txtMaDieuChinhTon").ReadOnly && Lay<TextBox>(trang, "txtMaDieuChinhGia").ReadOnly && Regex.IsMatch(Lay<TextBox>(trang, "txtMaDieuChinhTon").Text, "^DC[0-9]{3,}$") && Regex.IsMatch(Lay<TextBox>(trang, "txtMaDieuChinhGia").Text, "^DG[0-9]{3,}$") && Lay<Label>(trang, "lblNguoiGhi").Text.Contains(nguoiGhi), "Mã DC/DG hiện sẵn chỉ đọc và người ghi đúng tài khoản đăng nhập");
                Dung(Lay<Label>(trang, "lblTongTienLech").Text.Contains("500") && Lay<Label>(trang, "lblTongTienLech").Text.Contains("100") && Lay<Label>(trang, "lblTongTienLech").Text.Contains("400"), "Tổng thừa 500.000, thiếu -100.000, chênh lệch 400.000 theo bộ lọc");
                var bangTonXuat = (DataTable)Goi(trang, "BangLichSuTonXuat");
                Dung(bangTonXuat.Rows.Count == 7 && Convert.ToDecimal(bangTonXuat.Rows[6]["GiaTriChenhLech"]) == 400000m && ton.Rows.Count == 4, "Bản xuất thêm dòng tổng nhưng không sửa bảng lịch sử trên màn hình");
                KiemTraExcel(new[] { (DataTable)Lay<DataGridView>(trang, "dgvPhuTung").DataSource, bangTonXuat, (DataTable)gia.DataSource }, ma);
                foreach (TabPage the in tab.TabPages) { tab.SelectedTab = the; Application.DoEvents(); Anh(trang, "kho-moi-" + the.Name); }
                Lay<TextBox>(trang, "txtTimPhuTung").Text = "O'Neil";
                Application.DoEvents();
                Dung(ton.Rows.Count == 1 && ton.Rows[0].Cells["LyDo"].Value.ToString().Contains("O'Neil"), "Lọc lịch sử theo lý do có dấu nháy bằng truy vấn tham số");
                Lay<TextBox>(trang, "txtTimPhuTung").Text = ma;
                string maCu = Lay<TextBox>(trang, "txtMaDieuChinhTon").Text;
                QuanLyDieuChinhKho.DieuChinhTon(ma, 1, "Cập nhật từ máy khác");
                Lay<Button>(trang, "btnLamMoi").PerformClick();
                Dung(Lay<TextBox>(trang, "txtMaDieuChinhTon").Text != maCu && ton.Rows.Count == 5 && Lay<NumericUpDown>(trang, "numSoLuongLech").Value == 0 && Lay<TextBox>(trang, "txtLyDo").Text == "", "Làm mới tải dữ liệu máy khác, cấp lại mã và xóa nội dung điều chỉnh cũ");
                trang.ClientSize = new Size(1020, 700);
                Application.DoEvents(); tab.SelectedIndex = 0; Application.DoEvents(); Anh(trang, "kho-moi-cua-so-nho");
            }
            Dung(PhienDangNhap.DangNhap("mai", "123"), "Đăng nhập thu ngân để kiểm tra quyền kho");
            Chan<UnauthorizedAccessException>(() => QuanLyDieuChinhKho.DieuChinhTon(ma, 1, "Sai quyền"), "Thu ngân không thể gọi thẳng hàm điều chỉnh tồn");
            Chan<UnauthorizedAccessException>(() => QuanLyDieuChinhKho.DieuChinhGia(ma, 160000, "Sai quyền"), "Thu ngân không thể gọi thẳng hàm điều chỉnh giá");
            Dung(Convert.ToInt32(PhuTung(ma)["SoLuongTon"]) == 15 && Convert.ToDecimal(PhuTung(ma)["DonGiaBan"]) == 90000m, "Bị chặn quyền không thay đổi dữ liệu kho");
        }
        finally
        {
            duLieu.CapNhatDuLieu("IF OBJECT_ID(N'dbo.CodexKhoFailUpdate',N'TR') IS NOT NULL DROP TRIGGER dbo.CodexKhoFailUpdate");
            duLieu.CapNhatDuLieu("DELETE dbo.DieuChinhTonKho WHERE MaPhuTung=@ma; DELETE dbo.DieuChinhGiaBan WHERE MaPhuTung=@ma; DELETE dbo.PhuTung WHERE MaPhuTung=@ma", XuLyDuLieu.ThamSo("@ma", ma));
            PhienDangNhap.DangNhap("minh", "123");
        }
        Dung(Convert.ToInt32(GiaTri("SELECT COUNT(*) FROM dbo.PhuTung WHERE MaPhuTung=@ma", XuLyDuLieu.ThamSo("@ma", ma))) == 0, "Dọn phụ tùng và lịch sử thử nghiệm trên bản sao kiểm thử");
        Console.WriteLine("PASS ĐIỀU CHỈNH KHO: " + soKiemTra + " kiểm tra");
        return soKiemTra;
    }
}
