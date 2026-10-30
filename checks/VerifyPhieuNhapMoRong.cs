using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using QLyCuaHangSuaXe.LopDungChung;

public static class KiemTraPhieuNhapMoRong
{
    static readonly XuLyDuLieu duLieu = new XuLyDuLieu();
    static int soKiemTra;
    static object GiaTri(string sql, params SqlParameter[] thamSo) { return duLieu.DocBang(sql, thamSo).Rows[0][0]; }
    static int Ton(string ma) { return Convert.ToInt32(GiaTri("SELECT SoLuongTon FROM dbo.PhuTung WHERE MaPhuTung=@ma", XuLyDuLieu.ThamSo("@ma", ma))); }
    static int SoDieuChinh(string ma) { return Convert.ToInt32(GiaTri("SELECT COUNT(*) FROM dbo.DieuChinhTonKho WHERE LyDo LIKE @ly", XuLyDuLieu.ThamSo("@ly", "%phiếu nhập " + ma))); }
    static void Dung(bool dieuKien, string ten)
    {
        if (!dieuKien) throw new Exception("PHIẾU NHẬP MỞ RỘNG: " + ten);
        soKiemTra++;
        Console.WriteLine("PASS PHIẾU NHẬP MỞ RỘNG: " + ten);
    }
    static void TuChoi(Action thaoTac, Type loai = null)
    {
        try { thaoTac(); }
        catch (Exception loi)
        {
            if ((loai ?? typeof(InvalidOperationException)).IsInstanceOfType(loi)) return;
            throw;
        }
        throw new Exception("Thao tác không hợp lệ chưa bị từ chối.");
    }
    static T Lay<T>(object trang, string ten) { return (T)trang.GetType().GetField(ten, BindingFlags.NonPublic | BindingFlags.Instance).GetValue(trang); }
    static string NhaCungCap()
    {
        string ma = XuLyDuLieu.TaoMaMoi("NCC");
        duLieu.CapNhatDuLieu("INSERT dbo.NhaCungCap(MaNhaCungCap,TenNhaCungCap,SoDienThoai,DiaChi) VALUES(@ma,N'NCC kiểm thử phiếu nhập', '0987654321',N'Hà Nội')", XuLyDuLieu.ThamSo("@ma", ma));
        return ma;
    }
    static DataTable BangNhap()
    {
        var bang = new DataTable();
        foreach (string cot in new[] { "MaPhuTung", "TenPhuTung", "MaNhaCungCap", "TenNhaCungCap" }) bang.Columns.Add(cot);
        bang.Columns.Add("SoLuongNhap", typeof(int));
        bang.Columns.Add("DonGiaNhap", typeof(decimal));
        bang.Columns.Add("DonGiaBan", typeof(decimal));
        bang.Columns.Add("ThanhTien", typeof(decimal));
        bang.Columns.Add("HanSuDung", typeof(DateTime));
        bang.Columns.Add("LaPhuTungMoi", typeof(bool));
        bang.Columns.Add("GiaNhapGoc", typeof(decimal));
        bang.Columns.Add("GiaBanGoc", typeof(decimal));
        return bang;
    }
    public static int Chay()
    {
        soKiemTra = 0;
        Dung(new SqlConnectionStringBuilder(XuLyDuLieu.ChuoiKetNoi).InitialCatalog == "QuanLySuaXe_CodexChecks" && GiaTri("SELECT DB_NAME()").ToString() == "QuanLySuaXe_CodexChecks", "Chỉ thay đổi bản sao kiểm thử riêng");
        Dung(PhienDangNhap.DangNhap("minh", "123"), "Đăng nhập chủ bằng tên và mật khẩu đang dùng");
        string ncc = NhaCungCap(), maPhieu = XuLyDuLieu.TaoMaMoi("PN"), pt1 = XuLyDuLieu.TaoMaMoi("PT");
        string pt2 = "PT" + (int.Parse(pt1.Substring(2)) + 1).ToString("D3");
        var bang = BangNhap();
        bang.Rows.Add(pt1, "Phụ tùng kiểm thử sửa phiếu A", ncc, "NCC kiểm thử phiếu nhập", 10, 100m, 90m, 1000m, DateTime.MaxValue.Date, true, 0m, 0m);
        bang.Rows.Add(pt2, "Phụ tùng kiểm thử sửa phiếu B", ncc, "NCC kiểm thử phiếu nhập", 20, 200m, 250m, 4000m, DateTime.MaxValue.Date, true, 0m, 0m);
        TuChoi(() => QuanLyKho.GhiSo(maPhieu, DateTime.Today, "Phiếu mở rộng", bang));
        Dung(Convert.ToInt32(GiaTri("SELECT COUNT(*) FROM dbo.PhieuNhap WHERE MaPhieuNhap=@ma", XuLyDuLieu.ThamSo("@ma", maPhieu))) == 0, "Giá bán dưới giá nhập cần xác nhận trước khi ghi");
        QuanLyKho.GhiSo(maPhieu, DateTime.Today, "Phiếu mở rộng", bang, true);
        Dung(Ton(pt1) == 10 && Ton(pt2) == 20, "Xác nhận giá thấp vẫn nhập đủ cả hai phụ tùng");
        var banCu = QuanLyKho.DocPhieuNhap(maPhieu);
        string dauVetCu = banCu.ExtendedProperties["DauVet"].ToString();
        using (var trang = new btl.frmNhapPhieuNhap(maPhieu))
        {
            trang.TaiDuLieu();
            Dung(Lay<DataTable>(trang, "bangChiTiet").Rows.Count == 2 && Lay<TextBox>(trang, "txtMaPhieuNhap").Text == maPhieu && Lay<TextBox>(trang, "txtMaPhieuNhap").ReadOnly, "Form nhập sẵn mở lại đúng phiếu cần sửa");
            Dung(Lay<TextBox>(trang, "txtDonGiaBan").ReadOnly && Lay<Button>(trang, "btnXoaNCC").Text == "Xóa nhà cung cấp", "Sửa chứng từ giữ giá bán danh mục và có nút xóa NCC");
        }
        duLieu.CapNhatDuLieu("UPDATE dbo.PhuTung SET DonGiaNhap=555,DonGiaBan=888 WHERE MaPhuTung=@ma", XuLyDuLieu.ThamSo("@ma", pt1));
        var sua = banCu.Tables["ChiTiet"].Copy();
        sua.Rows[0]["SoLuongNhap"] = 13;
        sua.Rows[0]["DonGiaNhap"] = 111m;
        sua.Rows[1]["SoLuongNhap"] = 18;
        sua.Rows[1]["DonGiaNhap"] = 222m;
        QuanLyKho.SuaPhieu(maPhieu, DateTime.Today, "Sửa số lượng và giá chứng từ", sua, dauVetCu, true);
        Dung(Ton(pt1) == 13 && Ton(pt2) == 18, "Sửa phiếu tính tồn theo chênh lệch tăng và giảm");
        Dung(Convert.ToDecimal(GiaTri("SELECT TongTienNhap FROM dbo.PhieuNhap WHERE MaPhieuNhap=@ma", XuLyDuLieu.ThamSo("@ma", maPhieu))) == 13 * 111m + 18 * 222m, "Tổng tiền dùng đơn giá chứng từ đã sửa");
        Dung(Convert.ToDecimal(GiaTri("SELECT DonGiaNhap FROM dbo.PhuTung WHERE MaPhuTung=@ma", XuLyDuLieu.ThamSo("@ma", pt1))) == 555m && Convert.ToDecimal(GiaTri("SELECT DonGiaBan FROM dbo.PhuTung WHERE MaPhuTung=@ma", XuLyDuLieu.ThamSo("@ma", pt1))) == 888m, "Phiếu cũ không ghi đè giá catalog đã thay đổi sau đó");
        Dung(SoDieuChinh(maPhieu) == 2 && Convert.ToInt32(GiaTri("SELECT COUNT(*) FROM dbo.DieuChinhTonKho WHERE LyDo=@ly AND MaNhanVien=@nv", XuLyDuLieu.ThamSo("@ly", "Sửa phiếu nhập " + maPhieu), XuLyDuLieu.ThamSo("@nv", PhienDangNhap.MaNhanVien))) == 2, "Mỗi delta có lịch sử trong cùng giao dịch và đúng người sửa");
        TuChoi(() => QuanLyKho.SuaPhieu(maPhieu, DateTime.Today, "Ghi đè bản cũ", sua, dauVetCu, true));
        Dung(Ton(pt1) == 13 && SoDieuChinh(maPhieu) == 2, "Chặn phiên sửa cũ trước khi thay tồn hoặc lịch sử");
        var hienTai = QuanLyKho.DocPhieuNhap(maPhieu);
        var saiTen = hienTai.Tables["ChiTiet"].Copy();
        saiTen.Rows[0]["TenPhuTung"] = "Không được đổi tên từ chứng từ";
        TuChoi(() => QuanLyKho.SuaPhieu(maPhieu, DateTime.Today, "Đổi metadata", saiTen, hienTai.ExtendedProperties["DauVet"].ToString(), true));
        Dung(Ton(pt1) == 13 && SoDieuChinh(maPhieu) == 2, "Không đổi tên, NCC hoặc hạn của mã phụ tùng cũ từ phiếu nhập");
        duLieu.CapNhatDuLieu("UPDATE dbo.PhuTung SET SoLuongTon=1 WHERE MaPhuTung=@ma", XuLyDuLieu.ThamSo("@ma", pt2));
        var khongDu = hienTai.Tables["ChiTiet"].Copy();
        khongDu.Rows[0]["SoLuongNhap"] = 15;
        khongDu.Rows[1]["SoLuongNhap"] = 5;
        TuChoi(() => QuanLyKho.SuaPhieu(maPhieu, DateTime.Today, "Không đủ hàng rút lại", khongDu, hienTai.ExtendedProperties["DauVet"].ToString(), true));
        Dung(Ton(pt1) == 13 && Ton(pt2) == 1 && SoDieuChinh(maPhieu) == 2 && Convert.ToInt32(GiaTri("SELECT SoLuongNhap FROM dbo.ChiTietPhieuNhap WHERE MaPhieuNhap=@ma AND MaPhuTung=@pt", XuLyDuLieu.ThamSo("@ma", maPhieu), XuLyDuLieu.ThamSo("@pt", pt1))) == 13, "Dòng sau thiếu hàng hoàn tác tồn, lịch sử và chi tiết của dòng trước");
        var boDong = hienTai.Tables["ChiTiet"].Copy();
        boDong.Rows.RemoveAt(0);
        QuanLyKho.SuaPhieu(maPhieu, DateTime.Today, "Bỏ một dòng", boDong, hienTai.ExtendedProperties["DauVet"].ToString(), true);
        Dung(Ton(pt1) == 0 && Ton(pt2) == 1 && QuanLyKho.DocPhieuNhap(maPhieu).Tables["ChiTiet"].Rows.Count == 1, "Bỏ dòng rút đúng tồn đã nhập và giữ danh mục phụ tùng");
        var truocXoa = QuanLyKho.DocPhieuNhap(maPhieu);
        TuChoi(() => QuanLyKho.XoaPhieu(maPhieu, truocXoa.ExtendedProperties["DauVet"].ToString()));
        Dung(QuanLyKho.DocPhieuNhap(maPhieu).Tables["ChiTiet"].Rows.Count == 1 && Ton(pt2) == 1, "Xóa phiếu bị chặn nếu hàng đã dùng khiến tồn không đủ");
        duLieu.CapNhatDuLieu("UPDATE dbo.PhuTung SET SoLuongTon=18 WHERE MaPhuTung=@ma", XuLyDuLieu.ThamSo("@ma", pt2));
        QuanLyKho.XoaPhieu(maPhieu, truocXoa.ExtendedProperties["DauVet"].ToString());
        Dung(Ton(pt2) == 0 && Convert.ToInt32(GiaTri("SELECT COUNT(*) FROM dbo.PhieuNhap WHERE MaPhieuNhap=@ma", XuLyDuLieu.ThamSo("@ma", maPhieu))) == 0 && SoDieuChinh(maPhieu) == 4, "Xóa chứng từ rút tồn và giữ đầy đủ lịch sử có mã phiếu");
        Dung(int.Parse(XuLyDuLieu.TaoMaMoi("PN").Substring(2)) > int.Parse(maPhieu.Substring(2)), "Không cấp lại mã phiếu đã xóa còn được nhắc trong lịch sử điều chỉnh");
        TuChoi(() => QuanLyKho.XoaNhaCungCap(ncc));
        Dung(Convert.ToInt32(GiaTri("SELECT COUNT(*) FROM dbo.NhaCungCap WHERE MaNhaCungCap=@ma", XuLyDuLieu.ThamSo("@ma", ncc))) == 1, "NCC còn phụ tùng tham chiếu không bị xóa kể cả tồn bằng không");
        string nccRong = NhaCungCap();
        QuanLyKho.XoaNhaCungCap(nccRong);
        Dung(Convert.ToInt32(GiaTri("SELECT COUNT(*) FROM dbo.NhaCungCap WHERE MaNhaCungCap=@ma", XuLyDuLieu.ThamSo("@ma", nccRong))) == 0, "Xóa được NCC chưa được sử dụng");
        var giaCu = truocXoa.Tables["ChiTiet"].Clone();
        giaCu.ImportRow(truocXoa.Tables["ChiTiet"].Rows[0]);
        string phieuMoi = XuLyDuLieu.TaoMaMoi("PN");
        duLieu.CapNhatDuLieu("UPDATE dbo.PhuTung SET DonGiaBan=999 WHERE MaPhuTung=@ma", XuLyDuLieu.ThamSo("@ma", pt2));
        TuChoi(() => QuanLyKho.GhiSo(phieuMoi, DateTime.Today, "Giá cũ", giaCu, true));
        Dung(Ton(pt2) == 0 && Convert.ToInt32(GiaTri("SELECT COUNT(*) FROM dbo.PhieuNhap WHERE MaPhieuNhap=@ma", XuLyDuLieu.ThamSo("@ma", phieuMoi))) == 0, "Nhập phiếu mới từ giá cũ không ghi đè giá vừa đổi");
        Dung(PhienDangNhap.DangNhap("mai", "123"), "Chuyển sang thu ngân để kiểm tra quyền");
        TuChoi(() => QuanLyKho.XoaNhaCungCap(ncc), typeof(UnauthorizedAccessException));
        TuChoi(() => QuanLyKho.DocPhieuNhap(maPhieu), typeof(UnauthorizedAccessException));
        Dung(true, "Backend từ chối thu ngân thao tác nhập kho");
        Dung(PhienDangNhap.DangNhap("minh", "123"), "Khôi phục phiên chủ cho các kiểm tra sau");
        return soKiemTra;
    }
}
