using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using QLyCuaHangSuaXe.LopDungChung;
using QLyCuaHangSuaXe.DanhMuc.DangNhap;

class KiemTraTaiKhoan
{
    static readonly XuLyDuLieu duLieu = new XuLyDuLieu();
    static readonly BindingFlags co = BindingFlags.Instance | BindingFlags.NonPublic;
    static int soKiemTra;
    static T Lay<T>(object doiTuong, string ten) { return (T)doiTuong.GetType().GetField(ten, co).GetValue(doiTuong); }
    static void Goi(object doiTuong, string ten) { doiTuong.GetType().GetMethod(ten, co).Invoke(doiTuong, null); }
    static void KiemTra(bool dung, string thongBao) { if (!dung) throw new Exception(thongBao); soKiemTra++; Console.WriteLine("PASS: " + thongBao); }
    static object GiaTri(string sql, string ma) { return duLieu.DocBang(sql, XuLyDuLieu.ThamSo("@id", ma)).Rows[0][0]; }
    static void Hien(Form trang) { trang.StartPosition = FormStartPosition.Manual; trang.Location = new Point(-3000, -3000); trang.Show(); Application.DoEvents(); }
    static void ChoLuu(Button nut)
    {
        var dongHo = Stopwatch.StartNew();
        do { Application.DoEvents(); Thread.Sleep(20); } while (!nut.Enabled && dongHo.ElapsedMilliseconds < 10000);
        KiemTra(nut.Enabled, "Đổi mật khẩu hoàn tất trong thời hạn kiểm tra");
    }

    [STAThread]
    static int Main()
    {
        try
        {
            KiemTra(XuLyDuLieu.ChuoiKetNoi.Contains("CodexQuanLySuaXeTest"), "Chỉ dùng SQL kiểm thử riêng");
            string tienTo = "ui_" + Guid.NewGuid().ToString("N").Substring(0, 8) + "_", maChu = XuLyDuLieu.TaoMaMoi("VT");
            duLieu.CapNhatDuLieu("INSERT dbo.NhanVien(MaNhanVien,MaChucVu,TenNhanVien,TenDangNhap,MatKhauHash,SoXeDangSuaToiDa,LuongGio) SELECT @id,MaChucVu,N'Chủ kiểm tra',@u,@h,0,0 FROM dbo.ChucVu WHERE TenChucVu=N'Chủ/Quản lý'", XuLyDuLieu.ThamSo("@id", maChu), XuLyDuLieu.ThamSo("@u", tienTo + "chu"), XuLyDuLieu.ThamSo("@h", BaoMat.BamMatKhau("123")));
            KiemTra(PhienDangNhap.DangNhap(tienTo + "chu", "123"), "Đăng nhập chủ kiểm thử");
            Application.EnableVisualStyles(); Application.SetCompatibleTextRenderingDefault(false);
            using (var trang = new frmQuanLyTaiKhoan())
            {
                trang.TaiDuLieu(); Hien(trang);
                var cacThe = Lay<TabControl>(trang, "tabTaiKhoan");
                KiemTra(cacThe.TabCount == 2, "Chủ có quản lý tài khoản và đổi mật khẩu cùng form");
                Lay<Button>(trang, "btnThemMoi").PerformClick();
                KiemTra(!Lay<TextBox>(trang, "txtTenNhanVien").ReadOnly && !Lay<DataGridView>(trang, "dgvTaiKhoan").Enabled, "Tạo mới cho nhập họ tên và giữ bản nháp khỏi đổi dòng");
                Lay<TextBox>(trang, "txtTenNhanVien").Text = "Nhân viên tạo trực tiếp";
                Lay<TextBox>(trang, "txtTenDangNhap").Text = tienTo + "moi";
                Lay<TextBox>(trang, "txtMatKhau").Text = "123";
                Goi(trang, "Luu");
                KiemTra(Convert.ToInt32(duLieu.DocBang("SELECT COUNT(*) FROM dbo.NhanVien WHERE TenDangNhap=@u AND DaCapTaiKhoan=1", XuLyDuLieu.ThamSo("@u", tienTo + "moi")).Rows[0][0]) == 1, "Lưu ngay trên form tạo tài khoản SQL mới");
                KiemTra(Lay<TextBox>(trang, "txtMatKhau").Text.Length == 0, "Mật khẩu được xóa khỏi ô sau khi tạo");
                string maLienKet = QuanLyNhanSu.LuuNhanVien(new ThongTinNhanVien { HoTen = "Nhân viên có sẵn", DienThoai = "0912345678", ChucVu = "Thợ", TrangThai = "Đang làm", GioiHanXe = 2, LuongGio = 45678 });
                Lay<Button>(trang, "btnThemMoi").PerformClick();
                Lay<ComboBox>(trang, "cboNhanVienCoSan").SelectedValue = maLienKet;
                KiemTra(Lay<TextBox>(trang, "txtTenNhanVien").ReadOnly && Lay<TextBox>(trang, "txtMaTaiKhoan").Text == maLienKet, "Chọn nhân viên có sẵn trong cùng form");
                Lay<TextBox>(trang, "txtTenDangNhap").Text = tienTo + "lienket"; Lay<TextBox>(trang, "txtMatKhau").Text = "123"; Goi(trang, "Luu");
                KiemTra(Convert.ToDecimal(GiaTri("SELECT LuongGio FROM dbo.NhanVien WHERE MaNhanVien=@id", maLienKet)) == 45678, "Cấp tài khoản giữ nguyên lương nhân viên");
                cacThe.SelectedTab = Lay<TabPage>(trang, "tabMatKhau"); Application.DoEvents();
                var doiMatKhau = Lay<frmDoiMatKhau>(trang, "trangDoiMatKhau");
                KiemTra(doiMatKhau != null && !doiMatKhau.TopLevel && !Lay<Button>(doiMatKhau, "btnDong").Visible, "Form đổi mật khẩu cũ nhúng trong trang, không mở cửa sổ rời");
                KiemTra(Lay<Label>(doiMatKhau, "lblTaiKhoan").Text.Contains(tienTo + "chu"), "Đổi mật khẩu đúng người đang đăng nhập, không theo dòng đang chọn");
                string bamCu = GiaTri("SELECT MatKhauHash FROM dbo.NhanVien WHERE MaNhanVien=@id", maChu).ToString();
                Lay<TextBox>(doiMatKhau, "txtMatKhauHienTai").Text = "123"; Lay<TextBox>(doiMatKhau, "txtMatKhauMoi").Text = "456"; Lay<TextBox>(doiMatKhau, "txtXacNhanMatKhauMoi").Text = "789";
                var nutLuu = Lay<Button>(doiMatKhau, "btnDoiMatKhau"); nutLuu.PerformClick();
                KiemTra(GiaTri("SELECT MatKhauHash FROM dbo.NhanVien WHERE MaNhanVien=@id", maChu).ToString() == bamCu, "Xác nhận lệch không đổi mật khẩu");
                Lay<TextBox>(doiMatKhau, "txtMatKhauHienTai").Text = "sai"; Lay<TextBox>(doiMatKhau, "txtXacNhanMatKhauMoi").Text = "456"; nutLuu.PerformClick(); ChoLuu(nutLuu);
                KiemTra(GiaTri("SELECT MatKhauHash FROM dbo.NhanVien WHERE MaNhanVien=@id", maChu).ToString() == bamCu, "Sai mật khẩu cũ không đổi hash");
                Lay<TextBox>(doiMatKhau, "txtMatKhauHienTai").Text = "123"; nutLuu.PerformClick(); ChoLuu(nutLuu);
                KiemTra(PhienDangNhap.DangNhap(tienTo + "chu", "456") && !PhienDangNhap.DangNhap(tienTo + "chu", "123"), "Đổi mật khẩu trong form có hiệu lực khi đăng nhập lại");
                using (var anh = new Bitmap(trang.Width, trang.Height)) { trang.DrawToBitmap(anh, new Rectangle(Point.Empty, anh.Size)); anh.Save("checks/output/tai-khoan-doi-mat-khau.png"); }
                duLieu.CapNhatDuLieu("UPDATE dbo.NhanVien SET MaChucVu=(SELECT MaChucVu FROM dbo.ChucVu WHERE TenChucVu=N'Thợ') WHERE MaNhanVien=@id", XuLyDuLieu.ThamSo("@id", maChu));
                trang.TaiDuLieu();
                KiemTra(cacThe.TabCount == 1 && Lay<DataGridView>(trang, "dgvTaiKhoan").DataSource == null && Lay<DataTable>(trang, "bangTaiKhoan") == null, "Hạ quyền xóa dữ liệu quản lý trên form đã mở");
            }
            duLieu.CapNhatDuLieu("UPDATE dbo.NhanVien SET MaChucVu=(SELECT MaChucVu FROM dbo.ChucVu WHERE TenChucVu=N'Chủ/Quản lý') WHERE MaNhanVien=@id", XuLyDuLieu.ThamSo("@id", maChu));
            PhienDangNhap.DangNhap(tienTo + "chu", "456");
            foreach (string vaiTro in new[] { "Thợ", "Thợ chính", "Thu ngân" })
            {
                string ten = tienTo + (vaiTro == "Thợ" ? "tho" : vaiTro == "Thợ chính" ? "chinh" : "thungan");
                QuanLyNhanSu.TaoTaiKhoanMoi(null, vaiTro, ten, "123", vaiTro, true);
                KiemTra(PhienDangNhap.DangNhap(ten, "123"), "Đăng nhập vai trò " + vaiTro);
                using (var trang = new frmQuanLyTaiKhoan())
                {
                    trang.TaiDuLieu(); Hien(trang);
                    KiemTra(Lay<TabControl>(trang, "tabTaiKhoan").TabCount == 1 && Lay<DataGridView>(trang, "dgvTaiKhoan").DataSource == null, vaiTro + " chỉ thấy mật khẩu cá nhân");
                }
                using (var chinh = new QLyCuaHangSuaXe.frmChinh(ten, vaiTro))
                {
                    Hien(chinh);
                    KiemTra(Lay<Button>(chinh, "btnMoQuanLyTaiKhoan").Visible && typeof(QLyCuaHangSuaXe.frmChinh).GetField("btnMoDoiMatKhau", co) == null, vaiTro + " có một nút Tài khoản, không còn nút Đổi mật khẩu riêng");
                }
                PhienDangNhap.DangNhap(tienTo + "chu", "456");
            }
            Console.WriteLine("PASS: " + soKiemTra + " kiểm tra tài khoản.");
            return 0;
        }
        catch (Exception loi) { Console.Error.WriteLine(loi); return 1; }
    }
}
