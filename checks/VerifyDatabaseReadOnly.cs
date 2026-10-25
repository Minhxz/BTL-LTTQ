using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using QLyCuaHangSuaXe.LopDungChung;
using QLyCuaHangSuaXe.DanhMuc;
using QLyCuaHangSuaXe.DanhMuc.DangNhap;
using QLyCuaHangSuaXe.DanhMuc.TiepNhan;
using QLyCuaHangSuaXe.DanhMuc.DichVu;

class KiemTraDuLieuChiDoc
{
    [STAThread]
    static int Main()
    {
        try
        {
            var duLieu = new XuLyDuLieu();
            var chuSoHuu = duLieu.DocBang("SELECT TOP(1) n.MaNhanVien,n.TenDangNhap FROM dbo.NhanVien n JOIN dbo.ChucVu v ON v.MaChucVu=n.MaChucVu WHERE v.TenChucVu IN(N'Chủ cửa hàng',N'Chủ/Quản lý',N'Quản lý') AND n.TrangThai=N'Đang làm'");
            if (chuSoHuu.Rows.Count != 1)
                throw new Exception("Không tìm thấy quản lý đang làm.");
            var truocKiemTra = duLieu.DocBang("SELECT COUNT(*) AS SoHoaDon,SUM(TongTien) AS TongTien FROM dbo.HoaDon");
            typeof(PhienDangNhap).GetProperty("MaNhanVien").SetValue(null, chuSoHuu.Rows[0][0], null);
            typeof(PhienDangNhap).GetProperty("TenDangNhap").SetValue(null, chuSoHuu.Rows[0][1], null);
            typeof(PhienDangNhap).GetProperty("VaiTro").SetValue(null, "Chủ/Quản lý", null);
            string taiKhoanKiemTra = Environment.GetEnvironmentVariable("SUA_XE_TAI_KHOAN");
            string matKhauKiemTra = Environment.GetEnvironmentVariable("SUA_XE_MAT_KHAU");
            if (!string.IsNullOrEmpty(taiKhoanKiemTra))
            {
                if (!PhienDangNhap.DangNhap(taiKhoanKiemTra, matKhauKiemTra))
                    throw new Exception("Đăng nhập thật không thành công.");
                Console.WriteLine("PASS: Đăng nhập SQL bằng tài khoản được truyền khi chạy.");
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form[] cacForm =
            {
                new frmTiepNhan(),
                new frmKiemTraTiepNhan(),
                new frmDanhMucDichVu(),
                new btl.frmKho(),
                new frmHoaDon(),
                new frmQuanLyTaiKhoan(),
                new frmPhanCong(),
                new frmBangCong(),
                new frmThongKe()
            };
            foreach (var bieuMau in cacForm)
                using (bieuMau)
                {
                    if (bieuMau is frmKiemTraTiepNhan)
                        ((frmKiemTraTiepNhan)bieuMau).TaiDuLieu();
                    else if (bieuMau is TrangDuLieu)
                        ((TrangDuLieu)bieuMau).TaiDuLieu();
                    else
                        ((TrangThietKe)bieuMau).TaiDuLieu();
                    bieuMau.StartPosition = FormStartPosition.Manual;
                    bieuMau.Location = new Point(-3000, -3000);
                    bieuMau.Size = new Size(1180, 820);
                    bieuMau.Show();
                    Application.DoEvents();
                    Directory.CreateDirectory("checks/output");
                    using (var anh = new Bitmap(bieuMau.Width, bieuMau.Height))
                    {
                        bieuMau.DrawToBitmap(anh, new Rectangle(0, 0, anh.Width, anh.Height));
                        anh.Save("checks/output/thuc-" + bieuMau.GetType().Name + ".png");
                    }

                    Console.WriteLine("PASS: Real database read — " + bieuMau.Text);
                }

            Directory.CreateDirectory("checks/output");
            using (var frmChinh = new QLyCuaHangSuaXe.frmChinh(PhienDangNhap.TenDangNhap, PhienDangNhap.VaiTro))
            {
                frmChinh.WindowState = FormWindowState.Normal;
                frmChinh.Size = new Size(1440, 950);
                frmChinh.StartPosition = FormStartPosition.Manual;
                frmChinh.Location = new Point(-3000, -3000);
                frmChinh.Show();
                Application.DoEvents();
                using (var bitmap = new Bitmap(frmChinh.Width, frmChinh.Height))
                {
                    frmChinh.DrawToBitmap(bitmap, new Rectangle(0, 0, frmChinh.Width, frmChinh.Height));
                    bitmap.Save("checks/output/database-thuc.png");
                }

                frmChinh.Close();
            }

            var sauKiemTra = duLieu.DocBang("SELECT COUNT(*) AS SoHoaDon,SUM(TongTien) AS TongTien FROM dbo.HoaDon");
            if (!Equals(truocKiemTra.Rows[0][0], sauKiemTra.Rows[0][0]) || !Equals(truocKiemTra.Rows[0][1], sauKiemTra.Rows[0][1]))
                throw new Exception("Số liệu hóa đơn thay đổi.");
            Console.WriteLine("PASS: Read-only smoke preserved invoice count and totals.");
            return 0;
        }
        catch (Exception loi)
        {
            Console.Error.WriteLine(loi);
            return 1;
        }
    }
}
