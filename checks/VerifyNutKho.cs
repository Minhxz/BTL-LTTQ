using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using QLyCuaHangSuaXe.DanhMuc.TiepNhan;
using QLyCuaHangSuaXe.LopDungChung;

public static class KiemTraNutKho
{
    static readonly XuLyDuLieu duLieu = new XuLyDuLieu();
    static readonly BindingFlags co = BindingFlags.Instance | BindingFlags.NonPublic;
    static int soKiemTra;
    static T Lay<T>(object trang, string ten) { return (T)trang.GetType().GetField(ten, co).GetValue(trang); }
    static object GiaTri(string sql, params SqlParameter[] thamSo) { return duLieu.DocBang(sql, thamSo).Rows[0][0]; }
    static int SoDong(string bang) { return Convert.ToInt32(GiaTri("SELECT COUNT(*) FROM dbo.[" + bang + "]")); }
    static void Dung(bool dieuKien, string noiDung)
    {
        if (!dieuKien) throw new Exception("NÚT KHO: " + noiDung);
        soKiemTra++;
        Console.WriteLine("PASS NÚT KHO: " + noiDung);
    }
    static void Hien(Form trang)
    {
        trang.StartPosition = FormStartPosition.Manual;
        trang.Location = new Point(-4000, -4000);
        trang.Show();
        Application.DoEvents();
    }
    static void BamMoHopThoai<T>(Form cha, string tenNut, Action<T> kiemTra, string nutDong) where T : Form
    {
        Exception loi = null;
        bool daMo = false;
        cha.BeginInvoke(new Action(() =>
        {
            var trang = Application.OpenForms.OfType<T>().FirstOrDefault(t => t.Owner == cha);
            try
            {
                if (trang == null) throw new Exception("Nút " + tenNut + " chưa mở đúng form " + typeof(T).Name);
                daMo = true;
                kiemTra(trang);
            }
            catch (Exception ngoaiLe) { loi = ngoaiLe; }
            finally
            {
                if (trang != null && !trang.IsDisposed && trang.Visible)
                {
                    var nut = Lay<Button>(trang, nutDong);
                    if (nut == null) trang.Close();
                    else nut.PerformClick();
                }
            }
        }));
        Lay<Button>(cha, tenNut).PerformClick();
        if (loi != null) throw loi;
        Dung(daMo, "Nút " + tenNut + " mở đúng hộp thoại và đóng bằng nút của form");
    }
    static string ThemDongMoi(btl.frmNhapPhieuNhap trang, string ncc, string ten, int soLuong, decimal giaNhap, decimal giaBan)
    {
        Lay<Button>(trang, "btnMoiPhuTung").PerformClick();
        string ma = Lay<ComboBox>(trang, "cbMaPhuTung").Text;
        Lay<ComboBox>(trang, "cbNhaCungCap").SelectedValue = ncc;
        Lay<TextBox>(trang, "txtTenPhuTung").Text = ten;
        Lay<TextBox>(trang, "txtSoLuongNhap").Text = soLuong.ToString();
        Lay<TextBox>(trang, "txtDonGiaNhap").Text = giaNhap.ToString("0");
        Lay<TextBox>(trang, "txtDonGiaBan").Text = giaBan.ToString("0");
        Lay<Button>(trang, "btnThemPhuTung").PerformClick();
        return ma;
    }
    static void ChonDong(btl.frmNhapPhieuNhap trang, string ma)
    {
        var luoi = Lay<DataGridView>(trang, "dgvPhuTung");
        int viTri = luoi.Rows.Cast<DataGridViewRow>().Single(d => d.Cells["MaPhuTung"].Value.ToString() == ma).Index;
        luoi.CurrentCell = luoi.Rows[viTri].Cells["MaPhuTung"];
        try { trang.GetType().GetMethod("dgvPhuTung_CellClick", co).Invoke(trang, new object[] { luoi, new DataGridViewCellEventArgs(0, viTri) }); }
        catch (TargetInvocationException loi) { throw loi.InnerException ?? loi; }
    }
    public static int Chay()
    {
        soKiemTra = 0;
        Dung(new SqlConnectionStringBuilder(XuLyDuLieu.ChuoiKetNoi).InitialCatalog == "QuanLySuaXe_CodexChecks" && GiaTri("SELECT DB_NAME()").ToString() == "QuanLySuaXe_CodexChecks", "Chỉ kiểm tra nút trên database kiểm thử riêng");
        Dung(PhienDangNhap.DangNhap("minh", "123"), "Đăng nhập bằng tài khoản chủ hiện tại");
        string ncc = GiaTri("SELECT TOP(1) MaNhaCungCap FROM dbo.NhaCungCap ORDER BY MaNhaCungCap").ToString();
        int soPhieuCu = SoDong("PhieuNhap"), soPhuTungCu = SoDong("PhuTung"), soNccCu = SoDong("NhaCungCap");
        string maPhieuDaLuu = "", maPhuTungDaLuu = "";
        using (var danhSach = new btl.frmPhieuNhap())
        {
            Hien(danhSach);
            BamMoHopThoai<btl.frmNhapPhieuNhap>(danhSach, "btnThemPhieuNhap", trang =>
            {
                Dung(Lay<TextBox>(trang, "txtMaPhieuNhap").ReadOnly && Lay<TextBox>(trang, "txtMaPhieuNhap").Text.StartsWith("PN"), "Nút lập phiếu mở mã tự cấp sẵn");
                string maA = ThemDongMoi(trang, ncc, "Dòng nút thử A", 2, 1000m, 1500m);
                Lay<Button>(trang, "btnLamMoiPhuTung").PerformClick();
                var bang = Lay<DataTable>(trang, "bangChiTiet");
                Dung(bang.Rows.Count == 1 && Lay<ComboBox>(trang, "cbMaPhuTung").Text != maA, "Nhập lại cấp mã khác và giữ dòng dự thảo đã thêm");
                string maB = ThemDongMoi(trang, ncc, "Dòng nút thử B", 3, 2000m, 2500m);
                var luoi = Lay<DataGridView>(trang, "dgvPhuTung");
                luoi.Sort(luoi.Columns["MaPhuTung"], System.ComponentModel.ListSortDirection.Descending);
                Application.DoEvents();
                ChonDong(trang, maB);
                Lay<TextBox>(trang, "txtSoLuongNhap").Text = "4";
                Lay<Button>(trang, "btnSuaPhuTung").PerformClick();
                Dung(Convert.ToInt32(bang.AsEnumerable().Single(d => d["MaPhuTung"].ToString() == maB)["SoLuongNhap"]) == 4 && Convert.ToInt32(bang.AsEnumerable().Single(d => d["MaPhuTung"].ToString() == maA)["SoLuongNhap"]) == 2, "Nút cập nhật sửa đúng dòng sau khi sắp xếp");
                ChonDong(trang, maB);
                Lay<Button>(trang, "btnXoaPhuTung").PerformClick();
                Dung(bang.Rows.Count == 1 && bang.Rows[0]["MaPhuTung"].ToString() == maA, "Nút bỏ dòng xóa đúng dòng đang chọn");
                BamMoHopThoai<btl.frmThemNCC>(trang, "btnThemNCC", themNcc =>
                {
                    Dung(Lay<TextBox>(themNcc, "txtMaNCC").Text.StartsWith("NCC") && Lay<TextBox>(themNcc, "txtMaNCC").ReadOnly, "Nút thêm NCC mở form hiện có với mã sẵn");
                    Lay<TextBox>(themNcc, "txtTenNCC").Text = "NCC dự thảo không được ghi";
                }, "btnHuy");
                Dung(SoDong("NhaCungCap") == soNccCu && bang.Rows.Count == 1, "Hủy thêm NCC không ghi dữ liệu và không làm mất dòng nhập");
            }, "btnThoat");
            Dung(SoDong("PhieuNhap") == soPhieuCu && SoDong("PhuTung") == soPhuTungCu, "Đóng phiếu chưa ghi sổ không tạo phụ tùng hay chứng từ");
            BamMoHopThoai<btl.frmNhapPhieuNhap>(danhSach, "btnThemPhieuNhap", trang =>
            {
                maPhieuDaLuu = Lay<TextBox>(trang, "txtMaPhieuNhap").Text;
                maPhuTungDaLuu = ThemDongMoi(trang, ncc, "Phụ tùng kiểm thử nút ghi sổ", 5, 3000m, 4000m);
                Lay<RichTextBox>(trang, "txtGhiChu").Text = "Phiếu kiểm thử nút";
                Lay<Button>(trang, "btnLuuPhieu").PerformClick();
                Dung(trang.DialogResult == DialogResult.OK, "Nút ghi sổ của form mới lưu phiếu và trả kết quả thành công");
            }, "btnThoat");
            Dung(SoDong("PhieuNhap") == soPhieuCu + 1 && Convert.ToInt32(GiaTri("SELECT SoLuongTon FROM dbo.PhuTung WHERE MaPhuTung=@ma", XuLyDuLieu.ThamSo("@ma", maPhuTungDaLuu))) == 5, "Nút ghi sổ tạo đúng một phiếu và cộng tồn một lần");
            Lay<TextBox>(danhSach, "txtTimPhieuNhap").Text = maPhieuDaLuu;
            Application.DoEvents();
            duLieu.CapNhatDuLieu("UPDATE dbo.PhieuNhap SET GhiChu=N'Ghi chú tải lại từ SQL' WHERE MaPhieuNhap=@ma", XuLyDuLieu.ThamSo("@ma", maPhieuDaLuu));
            Lay<Button>(danhSach, "btnTaiLaiPhieu").PerformClick();
            var luoiPhieu = Lay<DataGridView>(danhSach, "dgvPhieuNhap");
            Dung(luoiPhieu.Rows.Count == 1 && GiaoDien.GiaTriO(luoiPhieu, "GhiChu") == "Ghi chú tải lại từ SQL", "Nút tải lại lấy dữ liệu mới từ SQL và giữ bộ lọc");
            BamMoHopThoai<btl.frmNhapPhieuNhap>(danhSach, "btnSuaPhieuNhap", trang =>
            {
                Dung(Lay<TextBox>(trang, "txtMaPhieuNhap").Text == maPhieuDaLuu && Lay<DataTable>(trang, "bangChiTiet").Rows.Count == 1 && Lay<TextBox>(trang, "txtDonGiaBan").ReadOnly, "Nút sửa mở đúng phiếu, đúng chi tiết và giữ giá bán chỉ đọc");
                ChonDong(trang, maPhuTungDaLuu);
                Lay<TextBox>(trang, "txtSoLuongNhap").Text = "6";
                Lay<Button>(trang, "btnSuaPhuTung").PerformClick();
                Dung(Convert.ToInt32(Lay<DataTable>(trang, "bangChiTiet").Rows[0]["SoLuongNhap"]) == 6, "Nút cập nhật dòng trong chế độ sửa thay đổi bản dự thảo");
            }, "btnThoat");
            Dung(Convert.ToInt32(GiaTri("SELECT SoLuongNhap FROM dbo.ChiTietPhieuNhap WHERE MaPhieuNhap=@ma", XuLyDuLieu.ThamSo("@ma", maPhieuDaLuu))) == 5 && Convert.ToInt32(GiaTri("SELECT SoLuongTon FROM dbo.PhuTung WHERE MaPhuTung=@ma", XuLyDuLieu.ThamSo("@ma", maPhuTungDaLuu))) == 5, "Đóng form sửa trước khi lưu giữ nguyên chi tiết và tồn đã ghi");
            BamMoHopThoai<frmXemBanInPhieu>(danhSach, "btnInPhieuNhap", trang =>
            {
                Dung(Lay<PictureBox>(trang, "picPhieu").Image != null, "Nút in phiếu mở sẵn ảnh xem trước mà chưa chọn máy in");
            }, "btnDong");
            Lay<Button>(danhSach, "btnThoat").PerformClick();
            Dung(!danhSach.Visible, "Nút đóng danh sách kết thúc đúng form phiếu nhập");
        }
        Console.WriteLine("CHƯA TỰ BẤM: xác nhận native Xóa phiếu nhập, Xóa nhà cung cấp và Lưu sửa phiếu; cần kiểm tra thủ công bằng giao diện.");
        return soKiemTra;
    }
}
