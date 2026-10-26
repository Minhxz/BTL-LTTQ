using System;
using System.Linq;
using System.Windows.Forms;
using QLyCuaHangSuaXe.LopDungChung;
using QLyCuaHangSuaXe.DanhMuc.DangNhap;
using System.Security.Cryptography;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using QLyCuaHangSuaXe.DanhMuc.TiepNhan;

class KiemTraDangNhap
{
    static int soKiemTra;
    static void KiemTra(bool hopLe, string thongBao)
    {
        if (!hopLe)
            throw new Exception(thongBao);
        soKiemTra++;
        Console.WriteLine("PASS: " + thongBao);
    }

    [STAThread]
    static int Main()
    {
        try
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            using (var context = new DieuPhoiUngDung())
            {
                KiemTra(context.MainForm is frmDangNhap, "Application opens login before main window");
                context.MainForm.Close();
            }

            KiemTra(PhanQuyenNguoiDung.VaiTro.Length == 4, "Exactly four supported roles");
            foreach (string matKhau in new[] { "123", "Tiếng Việt 🔧" })
            {
                byte[] muoi = Enumerable.Range(0, 16).Select(i => (byte)i).ToArray();
                using (var thamChieu = new Rfc2898DeriveBytes(matKhau, muoi, 10000, HashAlgorithmName.SHA256))
                    KiemTra(BaoMat.KiemTraMatKhau(matKhau, "PBKDF2-SHA256$10000$" + Convert.ToBase64String(muoi) + "$" + Convert.ToBase64String(thamChieu.GetBytes(32))), "Native password verification matches managed UTF8 reference");
                string bam = BaoMat.BamMatKhau(matKhau);
                KiemTra(BaoMat.KiemTraMatKhau(matKhau, bam) && !BaoMat.KiemTraMatKhau(matKhau + "x", bam), "New password hashes preserve format and reject wrong password");
            }
            var chiTiet = new DataTable();
            chiTiet.Columns.Add("Loai"); chiTiet.Columns.Add("NoiDung");
            chiTiet.Columns.Add("SoLuong", typeof(int)); chiTiet.Columns.Add("DonGia", typeof(decimal)); chiTiet.Columns.Add("ThanhTien", typeof(decimal));
            chiTiet.Rows.Add("Dịch vụ", "Bảo dưỡng xe", 1, 50000m, 50000m);
            var dongHo = Stopwatch.StartNew();
            var mau = new MauInPhieu("TN-THU", chiTiet, "Khách hàng: Nguyễn Văn Minh", "Biển số: 29-A1 12345", "Tình trạng: Kiểm tra xe");
            Directory.CreateDirectory("checks/output");
            using (var anh = mau.TaoAnh(0))
            {
                KiemTra(anh.Size == new Size(794, 1123) && mau.SoTrang == 1, "Receipt preview renders A4 without SQL or printer");
                anh.Save("checks/output/xem-truoc-phieu.png");
            }
            Console.WriteLine("TaoBanXemTruocMs=" + dongHo.ElapsedMilliseconds);
            using (var trang = new frmXemBanInPhieu(mau))
            {
                trang.Show(); Application.DoEvents();
                KiemTra(trang.Visible, "Print preview opens without selecting a printer");
                trang.Close();
            }
            for (int i = 0; i < 100; i++) chiTiet.Rows.Add("Phụ tùng", "Phụ tùng " + i, 1, 10000m, 10000m);
            var nhieuTrang = new MauInPhieu("TN-NHIEU", chiTiet, new string('a', 1500));
            KiemTra(nhieuTrang.SoTrang > 3, "Long receipts paginate");
            for (int i = 0; i < nhieuTrang.SoTrang; i++) using (var anh = nhieuTrang.TaoAnh(i)) { }
            KiemTra(PhanQuyenNguoiDung.CoQuyen("Chủ/Quản lý", "QUAN_LY_TAI_KHOAN"), "Owner can manage accounts");
            KiemTra(!PhanQuyenNguoiDung.CoQuyen("Thu ngân", "DICH_VU") && PhanQuyenNguoiDung.CoQuyen("Thu ngân", "HOA_DON"), "Cashier invoice scope");
            KiemTra(!PhanQuyenNguoiDung.CoQuyen("Thợ", "TIEP_NHAN") && PhanQuyenNguoiDung.CoQuyen("Thợ", "PHAN_CONG"), "Worker assignment scope");
            KiemTra(!PhanQuyenNguoiDung.CoQuyen("Kho", "NHAP_KHO"), "Unsupported legacy role cannot access modules");
            KiemTra(ChamCong.PhutCong(new TimeSpan(7, 30, 0), new TimeSpan(14, 0, 0), new TimeSpan(7, 0, 0), new TimeSpan(15, 0, 0), false, 0) == 390, "Shift boundaries cap counted minutes");
            KiemTra(ChamCong.PhutCong(new TimeSpan(7, 30, 0), new TimeSpan(14, 0, 0), new TimeSpan(8, 0, 0), new TimeSpan(13, 30, 0), false, -15) == 315, "Late and early minutes deducted");
            KiemTra(ChamCong.PhutCong(new TimeSpan(7, 30, 0), new TimeSpan(14, 0, 0), TimeSpan.Zero, TimeSpan.Zero, true, 0) == 0, "Absence counts zero");
            var caMuon = ChamCong.TinhChiTiet(new TimeSpan(7, 30, 0), new TimeSpan(14, 0, 0), new TimeSpan(8, 0, 40), new TimeSpan(13, 30, 20), false, -15, 60000);
            KiemTra(caMuon.PhutThucLam == 330 && caMuon.PhutMuon == 30 && caMuon.PhutVeSom == 30 && caMuon.PhutTinhCong == 315 && caMuon.TienCong == 315000, "Detailed pay uses minute precision and does not deduct lateness twice");
            var caSom = ChamCong.TinhChiTiet(new TimeSpan(7, 30, 0), new TimeSpan(14, 0, 0), new TimeSpan(7, 0, 0), new TimeSpan(15, 0, 0), false, 0, 60000);
            KiemTra(caSom.PhutTinhCong == 390 && caSom.TienCong == 390000, "Early arrival and late departure stay within shift unless adjusted");
            var caLe = ChamCong.TinhChiTiet(new TimeSpan(7, 30, 0), new TimeSpan(14, 0, 0), new TimeSpan(7, 30, 0), new TimeSpan(7, 31, 0), false, 0, 1000);
            KiemTra(caLe.TienCong == 16.67m, "Pay calculated from exact minutes before currency rounding");
            try
            {
                PhienDangNhap.YeuCau("TIEP_NHAN");
                throw new Exception("Unauthenticated access was allowed");
            }
            catch (UnauthorizedAccessException)
            {
                KiemTra(true, "Database operations require authentication");
            }

            Console.WriteLine("PASS: " + soKiemTra + " checks without database access.");
            return 0;
        }
        catch (Exception loi)
        {
            Console.Error.WriteLine(loi);
            return 1;
        }
    }
}
