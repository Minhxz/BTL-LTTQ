using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using QLyCuaHangSuaXe.LopDungChung;

public static class KiemTraNhapKhoMacDinh
{
    static readonly BindingFlags co = BindingFlags.Instance | BindingFlags.NonPublic;
    static T Lay<T>(object trang, string ten) { return (T)trang.GetType().GetField(ten, co).GetValue(trang); }
    static int so;
    static void Dung(bool dung, string noiDung)
    {
        if (!dung) throw new InvalidOperationException(noiDung);
        so++;
        Console.WriteLine("PASS nhập kho mặc định: " + noiDung);
    }
    [STAThread]
    public static int Main()
    {
        try
        {
            var duLieu = new XuLyDuLieu();
            if (new SqlConnectionStringBuilder(XuLyDuLieu.ChuoiKetNoi).InitialCatalog != "QuanLySuaXe_CodexChecks" || duLieu.DocBang("SELECT DB_NAME()").Rows[0][0].ToString() != "QuanLySuaXe_CodexChecks") throw new InvalidOperationException("Chỉ kiểm tra trên bản sao QuanLySuaXe_CodexChecks.");
            if (!PhienDangNhap.DangNhap("minh", "123")) throw new InvalidOperationException("Không đăng nhập được tài khoản kiểm thử.");
            string truoc = duLieu.DocBang("SELECT (SELECT COUNT(*) FROM dbo.PhieuNhap) AS PN,(SELECT COUNT(*) FROM dbo.ChiTietPhieuNhap) AS CT,(SELECT COUNT(*) FROM dbo.PhuTung) AS PT FOR JSON PATH").Rows[0][0].ToString();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            using (var trang = new btl.frmNhapPhieuNhap())
            {
                trang.StartPosition = FormStartPosition.Manual;
                trang.Location = new Point(-4000, -4000);
                trang.Show(); Application.DoEvents();
                var chon = Lay<ComboBox>(trang, "cbMaPhuTung");
                var dong = chon.SelectedItem as DataRowView;
                Dung(dong != null && chon.Text == dong["MaPhuTung"].ToString(), "Mở phiếu hiện sẵn mã phụ tùng đang chọn, không còn mã trống kèm thông tin cũ");
                Dung(Lay<TextBox>(trang, "txtTenPhuTung").Text == dong["TenPhuTung"].ToString() && decimal.Parse(Lay<TextBox>(trang, "txtDonGiaNhap").Text) == Convert.ToDecimal(dong["DonGiaNhap"]) && decimal.Parse(Lay<TextBox>(trang, "txtDonGiaBan").Text) == Convert.ToDecimal(dong["DonGiaBan"]), "Tên và hai mức giá khớp phụ tùng đang chọn");
                Dung(Lay<ComboBox>(trang, "cbNhaCungCap").SelectedValue.ToString() == dong["MaNhaCungCap"].ToString(), "Nhà cung cấp khớp phụ tùng đang chọn");
                Lay<Button>(trang, "btnThemPhuTung").PerformClick();
                var bang = Lay<DataTable>(trang, "bangChiTiet");
                Dung(bang.Rows.Count == 1 && bang.Rows[0]["MaPhuTung"].ToString() == chon.Text && Convert.ToInt32(bang.Rows[0]["SoLuongNhap"]) == 1, "Bấm Thêm dòng nhập ngay sau mở phiếu thêm đúng một dòng dự thảo");
                chon.SelectedIndex = -1;
                Dung(Lay<TextBox>(trang, "txtTenPhuTung").Text.Length == 0 && Lay<TextBox>(trang, "txtDonGiaNhap").Text.Length == 0 && Lay<TextBox>(trang, "txtDonGiaBan").Text.Length == 0 && Lay<TextBox>(trang, "txtThanhTien").Text == "0", "Bỏ chọn phụ tùng xóa thông tin cũ và đặt thành tiền bằng 0");
                trang.TaiDuLieu();
                Dung(chon.SelectedIndex >= 0 && bang.Rows.Count == 1, "Tải lại chọn đúng phụ tùng và giữ dòng dự thảo");
                Lay<Button>(trang, "btnMoiPhuTung").PerformClick();
                Dung(chon.Text.StartsWith("PT") && Lay<TextBox>(trang, "txtTenPhuTung").Text.Length == 0 && !Lay<TextBox>(trang, "txtTenPhuTung").ReadOnly, "Tạo phụ tùng mới vẫn cấp mã và cho nhập tên mới");
                trang.Close();
            }
            string sau = duLieu.DocBang("SELECT (SELECT COUNT(*) FROM dbo.PhieuNhap) AS PN,(SELECT COUNT(*) FROM dbo.ChiTietPhieuNhap) AS CT,(SELECT COUNT(*) FROM dbo.PhuTung) AS PT FOR JSON PATH").Rows[0][0].ToString();
            Dung(truoc == sau, "Bấm thử dự thảo không ghi phiếu hoặc phụ tùng vào SQL");
            Console.WriteLine("PASS: " + so + " kiểm tra hồi quy nhập kho mặc định.");
            return 0;
        }
        catch (Exception loi) { Console.Error.WriteLine(loi); return 1; }
    }
}
