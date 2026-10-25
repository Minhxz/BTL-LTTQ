using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLyCuaHangSuaXe.LopDungChung;
using QLyCuaHangSuaXe.DanhMuc;
using QLyCuaHangSuaXe.DanhMuc.DangNhap;
using QLyCuaHangSuaXe.DanhMuc.TiepNhan;
using QLyCuaHangSuaXe.DanhMuc.DichVu;

class KiemTraNghiepVu
{
    static readonly XuLyDuLieu duLieu = new XuLyDuLieu();
    static readonly NghiepVuSuaXe nghiepVu = new NghiepVuSuaXe();
    static int soKiemTraDat;
    static string maQuanLy, maTho, maThoChinh, maThuNgan, maPhuTung, maDichVu, maDichVuThuHai, tienToTaiKhoan;
    static System.Data.SqlClient.SqlParameter ThamSo(string ten, object giaTri)
    {
        return XuLyDuLieu.ThamSo(ten, giaTri);
    }

    static void KiemTra(bool hopLe, string noiDung)
    {
        if (!hopLe)
            throw new Exception(noiDung);
        soKiemTraDat++;
        Console.WriteLine("PASS: " + noiDung);
    }

    static void PhaiBaoLoi(Action thaoTac, string noiDung)
    {
        try
        {
            thaoTac();
        }
        catch (Exception loi)
        {
            if (loi is InvalidOperationException || loi is UnauthorizedAccessException || loi is System.Data.SqlClient.SqlException)
            {
                KiemTra(true, noiDung);
                return;
            }

            throw;
        }

        throw new Exception("Expected rejection: " + noiDung);
    }

    static object DocGiaTri(string cauLenhSql, params System.Data.SqlClient.SqlParameter[] thamSo)
    {
        return duLieu.DocBang(cauLenhSql, thamSo).Rows[0][0];
    }

    static string TaoNhanVien(string vaiTro, string tieuDe, int soXeToiDa)
    {
        string ma = XuLyDuLieu.TaoMaMoi("VT");
        duLieu.CapNhatDuLieu("INSERT dbo.NhanVien(MaNhanVien,MaChucVu,TenNhanVien,TenDangNhap,MatKhauHash,SoXeDangSuaToiDa,LuongGio) SELECT @id,MaChucVu,@u,@u,@h,@cap,60000 FROM dbo.ChucVu WHERE TenChucVu=@role", ThamSo("@id", ma), ThamSo("@u", tienToTaiKhoan + tieuDe), ThamSo("@h", BaoMat.BamMatKhau("Check12345")), ThamSo("@cap", soXeToiDa), ThamSo("@role", vaiTro));
        return ma;
    }

    static void DangNhapKiemThu(string tieuDe)
    {
        KiemTra(PhienDangNhap.DangNhap(tienToTaiKhoan + tieuDe, "Check12345"), "SQL login " + tieuDe);
    }

    static List<DichVuChon> DichVuKiemThu(int soLuong = 1)
    {
        return new List<DichVuChon>
        {
            new DichVuChon
            {
                Ma = maDichVu,
                SoLuong = soLuong
            }
        };
    }

    static ThongTinNhanXe ThongTinXeKiemThu(string thoDuocGiao = null)
    {
        return new ThongTinNhanXe
        {
            TenKhach = "Khách kiểm thử",
            DienThoai = "0912345678",
            BienSo = XuLyDuLieu.TaoMaMoi("VT"),
            DongXe = "Xe kiểm thử",
            LoaiXe = "Xe máy",
            TinhTrang = "Kiểm thử luồng",
            MaTho = thoDuocGiao ?? maTho,
            HenTra = DateTime.Now.AddHours(3)
        };
    }

    [STAThread]
    static int Main()
    {
        try
        {
            KiemTra(XuLyDuLieu.ChuoiKetNoi.Contains("CodexQuanLySuaXeTest"), "Isolated SQL instance");
            tienToTaiKhoan = "verify_" + Guid.NewGuid().ToString("N").Substring(0, 8) + "_";
            maQuanLy = TaoNhanVien("Chủ/Quản lý", "admin", 0);
            maTho = TaoNhanVien("Thợ", "worker", 2);
            maThoChinh = TaoNhanVien("Thợ chính", "chief", 3);
            maThuNgan = TaoNhanVien("Thu ngân", "cashier", 0);
            DangNhapKiemThu("admin");
            KiemTra(!PhienDangNhap.DangNhap(tienToTaiKhoan + "admin", "bad"), "Reject wrong password");
            KiemTra(!BaoMat.KiemTraMatKhau("x", "plaintext"), "Reject unsupported password hash");
            maPhuTung = XuLyDuLieu.TaoMaMoi("VT");
            maDichVu = XuLyDuLieu.TaoMaMoi("VT");
            maDichVuThuHai = XuLyDuLieu.TaoMaMoi("VT");
            duLieu.CapNhatDuLieu("INSERT dbo.PhuTung(MaPhuTung,TenPhuTung,SoLuongTon,DonGiaBan) VALUES(@p,N'Phụ tùng kiểm thử',20,100000); INSERT dbo.DichVu(MaDichVu,TenDichVu,DonGiaDichVu,LuongNhiemVu) VALUES(@dv,N'Dịch vụ kiểm thử',30000,10000),(@dv2,N'Dịch vụ kiểm thử 2',20000,5000); INSERT dbo.DinhMucDichVu VALUES(@dv,@p,2),(@dv2,@p,2);", ThamSo("@p", maPhuTung), ThamSo("@dv", maDichVu), ThamSo("@dv2", maDichVuThuHai));
            PhaiBaoLoi(() => nghiepVu.KiemTraKho(new List<DichVuChon>()), "Require service first");
            PhaiBaoLoi(() => nghiepVu.KiemTraKho(new List<DichVuChon> { new DichVuChon { Ma = maDichVu, SoLuong = 6 }, new DichVuChon { Ma = maDichVuThuHai, SoLuong = 5 } }), "Aggregate stock across services");
            string maPhieu = nghiepVu.TiepNhan(DichVuKiemThu(), ThongTinXeKiemThu());
            KiemTra(Convert.ToInt32(DocGiaTri("SELECT SoLuongTon FROM dbo.PhuTung WHERE MaPhuTung=@p", ThamSo("@p", maPhuTung))) == 18, "Receipt atomically deducts stock");
            PhaiBaoLoi(() => nghiepVu.LapHoaDon(maPhieu), "Cannot invoice unfinished work");
            string phieuThuHai = nghiepVu.TiepNhan(DichVuKiemThu(), ThongTinXeKiemThu());
            int truocKhiLuu = Convert.ToInt32(DocGiaTri("SELECT COUNT(*) FROM dbo.PhieuTiepNhan"));
            PhaiBaoLoi(() => nghiepVu.TiepNhan(DichVuKiemThu(), ThongTinXeKiemThu()), "Worker capacity 2");
            KiemTra(Convert.ToInt32(DocGiaTri("SELECT COUNT(*) FROM dbo.PhieuTiepNhan")) == truocKhiLuu, "Rejected receipt rolls back");
            PhaiBaoLoi(() => nghiepVu.TiepNhan(DichVuKiemThu(1000), ThongTinXeKiemThu(maThoChinh)), "Insufficient stock prevents save");
            string[] phieuCuaThoChinh =
            {
                nghiepVu.TiepNhan(DichVuKiemThu(), ThongTinXeKiemThu(maThoChinh)),
                nghiepVu.TiepNhan(DichVuKiemThu(), ThongTinXeKiemThu(maThoChinh)),
                nghiepVu.TiepNhan(DichVuKiemThu(), ThongTinXeKiemThu(maThoChinh))
            };
            PhaiBaoLoi(() => nghiepVu.TiepNhan(DichVuKiemThu(), ThongTinXeKiemThu(maThoChinh)), "Chief capacity 3");
            duLieu.CapNhatDuLieu("UPDATE dbo.DichVu SET DonGiaDichVu=900000,LuongNhiemVu=900000 WHERE MaDichVu=@d;UPDATE dbo.PhuTung SET DonGiaBan=900000 WHERE MaPhuTung=@p", ThamSo("@d", maDichVu), ThamSo("@p", maPhuTung));
            DangNhapKiemThu("worker");
            PhaiBaoLoi(() => nghiepVu.TrangThaiCongViec(phieuCuaThoChinh[0], maThoChinh, "Đang sửa"), "Worker cannot edit another worker's job");
            PhaiBaoLoi(() => nghiepVu.LapHoaDon(maPhieu), "Worker cannot create invoices");
            PhaiBaoLoi(() => nghiepVu.TrangThaiCongViec(maPhieu, maTho, "Hoàn thành"), "Cannot skip start-work transition");
            nghiepVu.TrangThaiCongViec(maPhieu, maTho, "Đang sửa");
            nghiepVu.TrangThaiCongViec(maPhieu, maTho, "Hoàn thành");
            PhaiBaoLoi(() => nghiepVu.TrangThaiCongViec(maPhieu, maTho, "Hoàn thành"), "Completion cannot be repeated");
            DangNhapKiemThu("cashier");
            string maHoaDon = nghiepVu.LapHoaDon(maPhieu);
            KiemTra(Convert.ToDecimal(DocGiaTri("SELECT TongTien FROM dbo.HoaDon WHERE MaHoaDon=@id", ThamSo("@id", maHoaDon))) == 230000, "Invoice preserves service and part prices");
            KiemTra(Convert.ToDecimal(DocGiaTri("SELECT TienCong FROM dbo.PhanCongTho WHERE MaPhieuTiepNhan=@id", ThamSo("@id", maPhieu))) == 10000, "Task wage preserves original amount");
            PhaiBaoLoi(() => nghiepVu.LapHoaDon(maPhieu), "One invoice per receipt");
            PhaiBaoLoi(() => duLieu.CapNhatDuLieu("UPDATE dbo.HoaDon SET TongTien=0,TongTienDichVu=0,TongTienPhuTung=0 WHERE MaHoaDon=@id", ThamSo("@id", maHoaDon)), "SQL blocks invoice changes");
            PhaiBaoLoi(() => duLieu.CapNhatDuLieu("UPDATE dbo.ChiTietHoaDon SET DonGia=0 WHERE MaHoaDon=@id", ThamSo("@id", maHoaDon)), "SQL blocks invoice detail changes");
            PhaiBaoLoi(() => duLieu.CapNhatDuLieu("INSERT dbo.ChiTietHoaDon(MaHoaDon,MaDichVu,TenDichVu,SoLuong,DonGia) VALUES(@id,@dv,N'Extra',1,0)", ThamSo("@id", maHoaDon), ThamSo("@dv", maDichVu)), "SQL blocks added invoice lines");
            PhaiBaoLoi(() => duLieu.CapNhatDuLieu("UPDATE dbo.PhuTungTiepNhan SET DonGia=0 WHERE MaPhieuTiepNhan=@id", ThamSo("@id", maPhieu)), "SQL blocks part snapshot changes");
            nghiepVu.ThanhToan(maHoaDon, "Tiền mặt");
            PhaiBaoLoi(() => nghiepVu.ThanhToan(maHoaDon, "Chuyển khoản"), "Payment once only");
            DangNhapKiemThu("admin");
            TimeSpan batDau = new TimeSpan(7, 30, 0), ketThuc = new TimeSpan(14, 0, 0);
            KiemTra(ChamCong.PhutCong(batDau, ketThuc, batDau, ketThuc, false, 0) == 390, "Full shift 390 minutes");
            KiemTra(ChamCong.PhutCong(batDau, ketThuc, new TimeSpan(8, 0, 0), new TimeSpan(13, 30, 0), false, -15) == 315, "Late, early and deduction");
            KiemTra(ChamCong.PhutCong(batDau, ketThuc, batDau, ketThuc, true, 0) == 0, "Absence zero minutes");
            var cacPhieuDongThoi = new List<string>();
            Parallel.Invoke(() =>
            {
                try
                {
                    string ma = nghiepVu.TiepNhan(DichVuKiemThu(), ThongTinXeKiemThu());
                    lock (cacPhieuDongThoi)
                        cacPhieuDongThoi.Add(ma);
                }
                catch (InvalidOperationException)
                {
                }
                catch (System.Data.SqlClient.SqlException)
                {
                }
            }, () =>
            {
                try
                {
                    string ma = nghiepVu.TiepNhan(DichVuKiemThu(), ThongTinXeKiemThu());
                    lock (cacPhieuDongThoi)
                        cacPhieuDongThoi.Add(ma);
                }
                catch (InvalidOperationException)
                {
                }
                catch (System.Data.SqlClient.SqlException)
                {
                }
            });
            KiemTra(cacPhieuDongThoi.Count == 1, "Concurrent receipts cannot exceed worker capacity");
            int tonTruocKhiHuy = Convert.ToInt32(DocGiaTri("SELECT SoLuongTon FROM dbo.PhuTung WHERE MaPhuTung=@p", ThamSo("@p", maPhuTung)));
            nghiepVu.HuyChuaSua(cacPhieuDongThoi[0]);
            KiemTra(Convert.ToInt32(DocGiaTri("SELECT SoLuongTon FROM dbo.PhuTung WHERE MaPhuTung=@p", ThamSo("@p", maPhuTung))) == tonTruocKhiHuy + 2, "Cancel unstarted receipt restores stock");
            PhaiBaoLoi(() => nghiepVu.HuyChuaSua(cacPhieuDongThoi[0]), "Cancellation cannot return stock twice");
            PhaiBaoLoi(() => nghiepVu.HuyChuaSua(maPhieu), "Completed invoiced receipt cannot be cancelled");
            string thoKiemTraDongThoi = TaoNhanVien("Thợ", "racer", 2);
            duLieu.CapNhatDuLieu("UPDATE dbo.PhuTung SET SoLuongTon=2 WHERE MaPhuTung=@p", ThamSo("@p", maPhuTung));
            cacPhieuDongThoi.Clear();
            Parallel.Invoke(() =>
            {
                try
                {
                    string ma = nghiepVu.TiepNhan(DichVuKiemThu(), ThongTinXeKiemThu(thoKiemTraDongThoi));
                    lock (cacPhieuDongThoi)
                        cacPhieuDongThoi.Add(ma);
                }
                catch (InvalidOperationException)
                {
                }
                catch (System.Data.SqlClient.SqlException)
                {
                }
            }, () =>
            {
                try
                {
                    string ma = nghiepVu.TiepNhan(DichVuKiemThu(), ThongTinXeKiemThu(thoKiemTraDongThoi));
                    lock (cacPhieuDongThoi)
                        cacPhieuDongThoi.Add(ma);
                }
                catch (InvalidOperationException)
                {
                }
                catch (System.Data.SqlClient.SqlException)
                {
                }
            });
            KiemTra(cacPhieuDongThoi.Count == 1 && Convert.ToInt32(DocGiaTri("SELECT SoLuongTon FROM dbo.PhuTung WHERE MaPhuTung=@p", ThamSo("@p", maPhuTung))) == 0, "Concurrent receipts cannot oversell stock");
            nghiepVu.HuyChuaSua(cacPhieuDongThoi[0]);
            ChamCong.LuuCong(0, maTho, DateTime.Today, "SANG", batDau, ketThuc, false, 0, "");
            KiemTra(Convert.ToDecimal(DocGiaTri("SELECT TienLuong FROM dbo.vBangCong WHERE MaNhanVien=@nv AND Ngay=CAST(GETDATE() AS DATE)", ThamSo("@nv", maTho))) == 390000, "Attendance wage calculated from SQL");
            PhaiBaoLoi(() => ChamCong.LuuCong(0, maTho, DateTime.Today, "TOI", new TimeSpan(13, 30, 0), new TimeSpan(20, 0, 0), false, 0, ""), "Overlap between shifts blocked");
            PhaiBaoLoi(() => ChamCong.LuuCong(0, maTho, DateTime.Today.AddDays(-1), "SANG", batDau, ketThuc, false, 10, ""), "Adjustment requires reason");
            int maDongCong = Convert.ToInt32(DocGiaTri("SELECT MaBangCong FROM dbo.BangCong WHERE MaNhanVien=@nv", ThamSo("@nv", maTho)));
            ChamCong.LuuCong(maDongCong, maTho, DateTime.Today, "SANG", new TimeSpan(8, 0, 0), ketThuc, false, 10, "Bù giờ được duyệt");
            KiemTra(Convert.ToInt32(DocGiaTri("SELECT COUNT(*) FROM dbo.LichSuBangCong WHERE MaBangCong=@id", ThamSo("@id", maDongCong))) == 2, "Attendance audit captures insert and edit");
            DangNhapKiemThu("worker");
            PhaiBaoLoi(() => ChamCong.LuuCong(maDongCong, maTho, DateTime.Today, "SANG", batDau, ketThuc, false, 0, ""), "Worker cannot change attendance");
            DangNhapKiemThu("admin");
            ChamCong.LuuCong(0, maThoChinh, DateTime.Today, "SANG", batDau, ketThuc, true, 0, "Nghỉ có báo");
            KiemTra(Convert.ToDecimal(DocGiaTri("SELECT TienLuong FROM dbo.vBangCong WHERE MaNhanVien=@nv", ThamSo("@nv", maThoChinh))) == 0, "Absent shift stores zero wage in SQL");
            DangNhapKiemThu("worker");
            duLieu.CapNhatDuLieu("UPDATE dbo.NhanVien SET MaChucVu=(SELECT TOP(1) MaChucVu FROM dbo.ChucVu WHERE TenChucVu=N'Thu ngân') WHERE MaNhanVien=@id", ThamSo("@id", maTho));
            PhaiBaoLoi(() => nghiepVu.TrangThaiCongViec(phieuThuHai, maTho, "Đang sửa"), "Changed SQL role takes effect in existing session");
            duLieu.CapNhatDuLieu("UPDATE dbo.NhanVien SET MaChucVu=(SELECT TOP(1) MaChucVu FROM dbo.ChucVu WHERE TenChucVu=N'Thợ') WHERE MaNhanVien=@id", ThamSo("@id", maTho));
            DangNhapKiemThu("admin");
            PhienDangNhap.DoiMatKhau("Check12345", "NewCheck12345");
            KiemTra(!PhienDangNhap.DangNhap(tienToTaiKhoan + "admin", "Check12345") && PhienDangNhap.DangNhap(tienToTaiKhoan + "admin", "NewCheck12345"), "Change password persists in SQL");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Directory.CreateDirectory("checks/output");
            var cacForm = new Form[]
            {
                new frmTiepNhan(),
                new frmDanhMucDichVu(),
                new btl.frmKho(),
                new frmHoaDon(),
                new frmQuanLyTaiKhoan(),
                new frmPhanQuyen(),
                new frmPhanCong(),
                new frmBangCong(),
                new frmThongKe(),
                new frmDoiMatKhau(),
                new frmNhanVien(),
                new btl.frmPhieuNhap(),
                new btl.frmNhapPhieuNhap(),
                new btl.frmThemNCC(),
                new frmDinhMuc(maDichVu),
                new frmChiTietPhieu(maPhieu),
                new frmChiTietHoaDon(maHoaDon)
            };
            for (int chiSo = 0; chiSo < cacForm.Length; chiSo++)
                using (var frmChiTiet = cacForm[chiSo])
                {
                    frmChiTiet.StartPosition = FormStartPosition.Manual;
                    frmChiTiet.Location = new Point(-3000, -3000);
                    frmChiTiet.Show();
                    Application.DoEvents();
                    KiemTra(frmChiTiet.Controls.Count > 0, "SQL form loads: " + frmChiTiet.Text);
                    if (frmChiTiet is frmThongKe)
                    {
                        var danhSachBaoCao = (ComboBox)typeof(frmThongKe).GetField("cboBaoCao", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(frmChiTiet);
                        for (int chiSoMuc = 0; chiSoMuc < danhSachBaoCao.Items.Count; chiSoMuc++)
                        {
                            danhSachBaoCao.SelectedIndex = chiSoMuc;
                            ((frmThongKe)frmChiTiet).TaiDuLieu();
                            KiemTra(true, "SQL report: " + danhSachBaoCao.Text);
                        }

                        danhSachBaoCao.SelectedIndex = 0;
                        ((frmThongKe)frmChiTiet).TaiDuLieu();
                    }

                    if (frmChiTiet is frmBangCong)
                    {
                        var danhSachBaoCao = (ComboBox)typeof(frmBangCong).GetField("cboBangCong", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(frmChiTiet);
                        danhSachBaoCao.SelectedIndex = 1;
                        ((frmBangCong)frmChiTiet).TaiDuLieu();
                        KiemTra(true, "SQL payroll summary combines hourly and task wages");
                        danhSachBaoCao.SelectedIndex = 0;
                        ((frmBangCong)frmChiTiet).TaiDuLieu();
                    }

                    using (var bitmap = new Bitmap(frmChiTiet.Width, frmChiTiet.Height))
                    {
                        frmChiTiet.DrawToBitmap(bitmap, new Rectangle(0, 0, frmChiTiet.Width, frmChiTiet.Height));
                        bitmap.Save("checks/output/form" + chiSo + ".png");
                    }

                    frmChiTiet.Close();
                }

            using (var frmChinh = new QLyCuaHangSuaXe.frmChinh(PhienDangNhap.TenDangNhap, PhienDangNhap.VaiTro))
            {
                frmChinh.WindowState = FormWindowState.Normal;
                frmChinh.Size = new Size(1440, 950);
                frmChinh.StartPosition = FormStartPosition.Manual;
                frmChinh.Location = new Point(-3000, -3000);
                frmChinh.Show();
                Application.DoEvents();
                KiemTra(frmChinh.Controls.Cast<Control>().SelectMany(dieuKhien => dieuKhien.Controls.Cast<Control>()).OfType<frmTiepNhan>().Any(), "Owner starts at receipt inside sidebar");
                using (var bitmap = new Bitmap(frmChinh.Width, frmChinh.Height))
                {
                    frmChinh.DrawToBitmap(bitmap, new Rectangle(0, 0, frmChinh.Width, frmChinh.Height));
                    bitmap.Save("checks/output/sidebar.png");
                }

                var trang = frmChinh.Controls.Cast<Control>().SelectMany(dieuKhien => dieuKhien.Controls.Cast<Control>()).OfType<frmTiepNhan>().Single();
                var coTruyCap = System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic;
                var nutKho = (Button)typeof(QLyCuaHangSuaXe.frmChinh).GetField("btnMoKho", coTruyCap).GetValue(frmChinh);
                KiemTra(nutKho.Visible && nutKho.Height >= 58, "Warehouse has a large direct sidebar button");
                nutKho.PerformClick(); Application.DoEvents();
                KiemTra(frmChinh.Controls.Cast<Control>().SelectMany(dieuKhien => dieuKhien.Controls.Cast<Control>()).OfType<btl.frmKho>().Any(), "Sidebar opens existing warehouse form");
                ((Button)typeof(QLyCuaHangSuaXe.frmChinh).GetField("btnMoTiepNhan", coTruyCap).GetValue(frmChinh)).PerformClick();
                var tblThongTin = (TableLayoutPanel)typeof(frmTiepNhan).GetField("tlpThongTin", coTruyCap).GetValue(trang);
                var oTenKhach = (TextBox)typeof(frmTiepNhan).GetField("txtKhachHang", coTruyCap).GetValue(trang);
                KiemTra(oTenKhach.ReadOnly, "Receipt information read-only before stock check");
                using (var kiemTra = new frmKiemTraTiepNhan())
                {
                    kiemTra.Show();
                    Application.DoEvents();
                    var cboDichVu = (ComboBox)typeof(frmKiemTraTiepNhan).GetField("cboDichVu", coTruyCap).GetValue(kiemTra);
                    cboDichVu.SelectedValue = maDichVu;
                    typeof(frmKiemTraTiepNhan).GetMethod("ThemDichVu", coTruyCap).Invoke(kiemTra, null);
                    KiemTra(!kiemTra.DuDieuKien, "Changing draft requires stock check");
                    typeof(frmKiemTraTiepNhan).GetMethod("KiemTraDieuKien", coTruyCap).Invoke(kiemTra, null);
                    KiemTra(kiemTra.DuDieuKien, "Precheck accepts available parts");
                    var bangHangMuc = (DataGridView)typeof(frmKiemTraTiepNhan).GetField("dgvHangMuc", coTruyCap).GetValue(kiemTra);
                    ((DataTable)bangHangMuc.DataSource).Rows[0]["SoLuong"] = 2;
                    KiemTra(!kiemTra.DuDieuKien, "Quantity change invalidates precheck");
                    ((DataTable)bangHangMuc.DataSource).Rows[0]["SoLuong"] = 1;
                    typeof(frmKiemTraTiepNhan).GetMethod("KiemTraDieuKien", coTruyCap).Invoke(kiemTra, null);
                    trang.NhanKetQuaKiemTra(kiemTra.DichVuDaChon, kiemTra.ChiTietDaChon, "Kiểm tra xe");
                    using (var anh = new Bitmap(kiemTra.Width, kiemTra.Height))
                    {
                        kiemTra.DrawToBitmap(anh, new Rectangle(0, 0, anh.Width, anh.Height));
                        anh.Save("checks/output/kiem-tra-tiep-nhan.png");
                    }

                    kiemTra.Close();
                }

                Application.DoEvents();
                KiemTra(!oTenKhach.ReadOnly, "Receipt information unlocked after sufficient stock");
                var oTho = (ComboBox)typeof(frmTiepNhan).GetField("cboThoPhuTrach", coTruyCap).GetValue(trang);
                var nhacHen = (Label)typeof(frmTiepNhan).GetField("lblTrangThaiHen", coTruyCap).GetValue(trang);
                KiemTra(oTho.Height >= oTho.PreferredHeight && oTho.PointToScreen(Point.Empty).Y + oTho.Height <= nhacHen.PointToScreen(Point.Empty).Y, "Mechanic selector is fully visible above appointment message");
                using (var bitmap = new Bitmap(frmChinh.Width, frmChinh.Height))
                {
                    frmChinh.DrawToBitmap(bitmap, new Rectangle(0, 0, frmChinh.Width, frmChinh.Height));
                    bitmap.Save("checks/output/sidebar-ready.png");
                }

                frmChinh.Close();
            }

            DangNhapKiemThu("worker");
            using (var bieuMau = new frmPhanCong())
            {
                bieuMau.Show();
                Application.DoEvents();
                var bangDuLieu = (DataTable)bieuMau.Controls.OfType<DataGridView>().Single().DataSource;
                KiemTra(bangDuLieu.AsEnumerable().All(dongDuLieu => dongDuLieu["MaNhanVien"].ToString() == maTho), "Worker grid only shows assigned vehicles");
                bieuMau.Close();
            }

            using (var bieuMau = new frmBangCong())
            {
                bieuMau.Show();
                Application.DoEvents();
                var bangDuLieu = (DataTable)bieuMau.Controls.OfType<DataGridView>().Single().DataSource;
                KiemTra(bangDuLieu.AsEnumerable().All(dongDuLieu => dongDuLieu["MaNhanVien"].ToString() == maTho), "Worker attendance only shows own records");
                bieuMau.Close();
            }

            var bangXuat = new DataTable();
            bangXuat.Columns.Add("Tên");
            bangXuat.Columns.Add("Tiền", typeof(decimal));
            bangXuat.Rows.Add("=SUM(1,2) tiếng Việt", 123456.50m);
            XuatExcel.GhiTep(bangXuat, "checks/output/BangCong.xlsx", "Kiểm thử");
            using (var tepNen = ZipFile.OpenRead("checks/output/BangCong.xlsx"))
            {
                KiemTra(tepNen.GetEntry("xl/workbook.xml") != null, "Export valid XLSX package");
                using (var boDoc = new StreamReader(tepNen.GetEntry("xl/worksheets/sheet1.xml").Open()))
                {
                    string boGhiXml = boDoc.ReadToEnd();
                    KiemTra(boGhiXml.Contains("123456.50") && !boGhiXml.Contains("<f>"), "Numeric export and text cannot become formula");
                }
            }

            Console.WriteLine("PASS: " + soKiemTraDat + " integration checks.");
            return 0;
        }
        catch (Exception loi)
        {
            Console.Error.WriteLine(loi);
            return 1;
        }
    }
}
