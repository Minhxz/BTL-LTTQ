using System;
using System.ComponentModel.Design;
using System.Windows.Forms;
using QLyCuaHangSuaXe;
using QLyCuaHangSuaXe.DanhMuc.DangNhap;
using QLyCuaHangSuaXe.DanhMuc.DichVu;
using QLyCuaHangSuaXe.DanhMuc.TiepNhan;
using QLyCuaHangSuaXe.LopDungChung;
using System.Linq;
using System.Reflection;
using System.IO;
using System.Text.RegularExpressions;

class KiemTraDesigner
{
    static void KiemTraChuHienThi(Control goc)
    {
        var danhSach = goc as ComboBox;
        if (danhSach != null && (danhSach.Name == "cboBangCong" || danhSach.Name == "cboBaoCao" || danhSach.Name == "cboTrangThaiThanhToan") && danhSach.SelectedIndex < 0)
            throw new Exception("Chưa chọn mặc định cho bộ lọc: " + danhSach.Name);
        if (goc is Form || goc is Button || goc is Label || goc is GroupBox || goc is TabPage)
        {
            string chu = (goc.Text ?? "").Trim();
            if (Regex.IsMatch(chu, @"^(?:frm[A-Z]\w*|btn[A-Z]\w*|lbl[A-Z]\w*|Form\d+|Button\d+|Label\d+)$") ||
                Regex.IsMatch(chu, @"\b(?:Save|Cancel|Delete|Search|Login|Password|Username|Dashboard|Settings|Previous)\b", RegexOptions.IgnoreCase))
                throw new Exception("Chữ hiển thị chưa được đổi sang tiếng Việt: " + chu);
        }
        foreach (Control con in goc.Controls) KiemTraChuHienThi(con);
    }

    static void KiemTraTaiNguyenKho()
    {
        string goc = Path.Combine("QLyCuaHangSuaXe", "QLyCuaHangSuaXe");
        string duAn = File.ReadAllText(Path.Combine(goc, "QLyCuaHangSuaXe.csproj"));
        string[] ten = { "frmNhapPhieuNhap", "frmKho", "frmPhieuNhap", "frmThemNCC" };
        string[] lop = { "frmNhapPhieuNhap", "frmKho", "frmPhieuNhap", "frmThemNCC" };
        var daNhung = typeof(btl.frmKho).Assembly.GetManifestResourceNames();
        for (int i = 0; i < ten.Length; i++)
        {
            string taiNguyen = "btl." + lop[i] + ".resources";
            if (!File.Exists(Path.Combine(goc, "DanhMuc", "Kho", ten[i] + ".resx")) ||
                !duAn.Contains("<LogicalName>" + taiNguyen + "</LogicalName>") || !daNhung.Contains(taiNguyen))
                throw new Exception("Thiếu đường dẫn hoặc tài nguyên Designer kho: " + ten[i]);
        }
        Console.WriteLine("PASS: Đường dẫn và bốn tài nguyên Designer kho đầy đủ.");
    }
    [STAThread]
    static int Main()
    {
        try
        {
            KiemTraTaiNguyenKho();
            Type[] cacForm =
            {
                typeof(frmChinh),
                typeof(frmDangNhap),
                typeof(frmTiepNhan),
                typeof(frmKiemTraTiepNhan),
                typeof(frmXemBanInPhieu),
                typeof(QLyCuaHangSuaXe.DanhMuc.frmThanhToanQR),
                typeof(QLyCuaHangSuaXe.DanhMuc.frmNhanVien),
                typeof(frmDanhMucDichVu),
                typeof(frmQuanLyTaiKhoan),
                typeof(frmDoiMatKhau),
                typeof(frmPhanQuyen),
                typeof(frmDinhMuc),
                typeof(frmChiTietPhieu),
                typeof(QLyCuaHangSuaXe.DanhMuc.frmBangCong),
                typeof(QLyCuaHangSuaXe.DanhMuc.frmPhanCong),
                typeof(QLyCuaHangSuaXe.DanhMuc.frmHoaDon),
                typeof(QLyCuaHangSuaXe.DanhMuc.frmChiTietHoaDon),
                typeof(QLyCuaHangSuaXe.DanhMuc.frmThongKe),
                typeof(btl.frmKho),
                typeof(btl.frmPhieuNhap),
                typeof(btl.frmNhapPhieuNhap),
                typeof(btl.frmThemNCC)
            };
            foreach (Type kieuForm in cacForm)
                {
                    if (typeof(TrangThietKe).IsAssignableFrom(kieuForm))
                    {
                        var tenKeThua = typeof(TrangThietKe).GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
                            .Where(truong => truong.IsFamily && typeof(System.ComponentModel.IComponent).IsAssignableFrom(truong.FieldType))
                            .Select(truong => truong.Name).ToArray();
                        var trungTen = kieuForm.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
                            .Where(truong => typeof(System.ComponentModel.IComponent).IsAssignableFrom(truong.FieldType) && tenKeThua.Contains(truong.Name, StringComparer.OrdinalIgnoreCase)).ToArray();
                        if (trungTen.Length > 0) throw new Exception(kieuForm.Name + ": Tên control trùng lớp cha: " + string.Join(", ", trungTen.Select(truong => truong.Name)));
                    }
                    var beMat = new DesignSurface();
                    beMat.BeginLoad(kieuForm);
                    if (!beMat.IsLoaded || beMat.LoadErrors.Count > 0)
                        throw new Exception(kieuForm.Name + ": " + string.Join("; ", System.Linq.Enumerable.Cast<object>(beMat.LoadErrors)));
                    var khung = beMat.View as Control;
                    if (khung == null)
                        throw new Exception(kieuForm.Name + ": Không tạo được vùng thiết kế.");
                    var chuThietKe = beMat.GetService(typeof(IDesignerHost)) as IDesignerHost;
                    if (chuThietKe != null && chuThietKe.RootComponent is Control) KiemTraChuHienThi((Control)chuThietKe.RootComponent);
                    if (chuThietKe != null)
                        foreach (var truong in kieuForm.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
                        {
                            var cot = truong.GetValue(chuThietKe.RootComponent) as DataGridViewColumn;
                            if (typeof(DataGridViewColumn).IsAssignableFrom(truong.FieldType) && cot == null)
                                throw new Exception(kieuForm.Name + ": Cột chưa được khởi tạo trong Designer: " + truong.Name);
                            if (cot != null && cot.Name != truong.Name)
                                throw new Exception(kieuForm.Name + ": Tên cột khác tên component, Designer có thể bỏ cột khi lưu: " + truong.Name);
                        }
                    Console.WriteLine("PASS: Designer " + kieuForm.Name);
                }

            Environment.Exit(0);
            return 0;
        }
        catch (Exception loi)
        {
            Console.Error.WriteLine(loi);
            return 1;
        }
    }
}
