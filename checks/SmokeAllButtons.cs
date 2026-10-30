using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using QLyCuaHangSuaXe;
using QLyCuaHangSuaXe.DanhMuc;
using QLyCuaHangSuaXe.DanhMuc.DangNhap;
using QLyCuaHangSuaXe.DanhMuc.DichVu;
using QLyCuaHangSuaXe.DanhMuc.TiepNhan;
using QLyCuaHangSuaXe.LopDungChung;

public static class KiemTraTatCaNut
{
    static readonly object khoaGhi = new object();
    static readonly JavaScriptSerializer json = new JavaScriptSerializer();
    static readonly string thuMuc = Path.GetFullPath("checks/output/smoke-all-buttons");
    static readonly List<TruongHop> cacTruongHop = new List<TruongHop>();
    static readonly Type[] cacLoai = {
        typeof(frmChinh), typeof(frmDangNhap), typeof(frmTiepNhan), typeof(frmKiemTraTiepNhan),
        typeof(frmXemBanInPhieu), typeof(frmThanhToanQR), typeof(frmNhanVien), typeof(frmDanhMucDichVu),
        typeof(frmQuanLyTaiKhoan), typeof(frmDoiMatKhau), typeof(frmPhanQuyen), typeof(frmDinhMuc),
        typeof(frmChiTietPhieu), typeof(frmBangCong), typeof(frmPhanCong), typeof(frmHoaDon),
        typeof(frmChiTietHoaDon), typeof(frmThongKe), typeof(btl.frmKho), typeof(btl.frmPhieuNhap),
        typeof(btl.frmNhapPhieuNhap), typeof(btl.frmThemNCC)
    };
    sealed class TruongHop
    {
        public int So;
        public Type Kieu;
        public string TenNut;
    }
    static void Ghi(string loai, object duLieu)
    {
        lock (khoaGhi)
        {
            string dong = loai + " " + json.Serialize(duLieu);
            Console.WriteLine(dong);
            Console.Out.Flush();
            File.AppendAllText(Path.Combine(thuMuc, "su-kien.jsonl"), json.Serialize(new { time = DateTime.UtcNow.ToString("o"), kind = loai, data = duLieu }) + Environment.NewLine, new UTF8Encoding(false));
        }
    }
    static void KiemTraDatabase()
    {
        if (new SqlConnectionStringBuilder(XuLyDuLieu.ChuoiKetNoi).InitialCatalog != "QuanLySuaXe_CodexChecks")
            throw new InvalidOperationException("Chỉ được chạy trên QuanLySuaXe_CodexChecks.");
        if (new XuLyDuLieu().DocBang("SELECT DB_NAME()").Rows[0][0].ToString() != "QuanLySuaXe_CodexChecks")
            throw new InvalidOperationException("SQL thực tế không trỏ đúng bản sao kiểm thử.");
    }
    static string MaDau(string cauLenh)
    {
        var bang = new XuLyDuLieu().DocBang(cauLenh);
        return bang.Rows.Count == 0 ? null : bang.Rows[0][0].ToString();
    }
    static string PhieuHoanThanh()
    {
        KiemTraDatabase();
        string coSan = MaDau("SELECT TOP(1) p.MaPhieuTiepNhan FROM dbo.PhieuTiepNhan p WHERE p.TrangThai=N'Hoàn thành' AND NOT EXISTS(SELECT 1 FROM dbo.HoaDon h WHERE h.MaPhieuTiepNhan=p.MaPhieuTiepNhan) AND EXISTS(SELECT 1 FROM dbo.ChiTietPhieuTiepNhan c WHERE c.MaPhieuTiepNhan=p.MaPhieuTiepNhan AND c.ThanhTien>0) ORDER BY p.MaPhieuTiepNhan");
        if (!string.IsNullOrEmpty(coSan)) return coSan;
        string moi = new XuLyDuLieu().TrongGiaoDich((ketNoi, giaoDich) =>
        {
            var nguon = XuLyDuLieu.DocDuLieu(ketNoi, giaoDich, "SELECT TOP(1) p.MaPhieuTiepNhan,p.MaXe FROM dbo.PhieuTiepNhan p WHERE p.TrangThai=N'Hoàn thành' AND EXISTS(SELECT 1 FROM dbo.ChiTietPhieuTiepNhan c WHERE c.MaPhieuTiepNhan=p.MaPhieuTiepNhan AND c.ThanhTien>0) ORDER BY p.MaPhieuTiepNhan");
            if (nguon.Rows.Count == 0) throw new InvalidOperationException("Không có phiếu nguồn để tạo tình huống kiểm thử thanh toán.");
            string maXe = XuLyDuLieu.TaoMaMoi(ketNoi, giaoDich, "XE");
            string maPhieu = XuLyDuLieu.TaoMaMoi(ketNoi, giaoDich, "PTN");
            XuLyDuLieu.ThucThi(ketNoi, giaoDich,
                "INSERT dbo.Xe(MaXe,MaKhachHang,BienXe,LoaiXe,HangSanXuat,MauSac) SELECT @xe,MaKhachHang,@bien,LoaiXe,HangSanXuat,MauSac FROM dbo.Xe WHERE MaXe=@xeCu; INSERT dbo.PhieuTiepNhan(MaPhieuTiepNhan,MaXe,MaNhanVien,NgayLap,NgayHen,TrangThai) VALUES(@phieu,@xe,@nv,CONVERT(date,GETDATE()),CONVERT(date,GETDATE()),N'Hoàn thành'); INSERT dbo.ChiTietPhieuTiepNhan(MaPhieuTiepNhan,MaDichVu,SoLuong,ThanhTien) SELECT @phieu,MaDichVu,SoLuong,ThanhTien FROM dbo.ChiTietPhieuTiepNhan WHERE MaPhieuTiepNhan=@nguon",
                XuLyDuLieu.ThamSo("@xe", maXe), XuLyDuLieu.ThamSo("@bien", "KT-NUT-" + maXe), XuLyDuLieu.ThamSo("@xeCu", nguon.Rows[0]["MaXe"]), XuLyDuLieu.ThamSo("@phieu", maPhieu), XuLyDuLieu.ThamSo("@nv", PhienDangNhap.MaNhanVien), XuLyDuLieu.ThamSo("@nguon", nguon.Rows[0]["MaPhieuTiepNhan"]));
            return maPhieu;
        });
        Ghi("FIXTURE", new { database = "QuanLySuaXe_CodexChecks", receipt = moi, note = "Tình huống hoàn thành/chưa thu tiền chỉ trong bản sao kiểm thử; không trừ kho hoặc sửa dữ liệu thật." });
        return moi;
    }
    static Form TaoForm(Type kieu)
    {
        if (kieu == typeof(frmChinh)) return new frmChinh("minh", PhienDangNhap.VaiTro);
        if (kieu == typeof(frmDinhMuc)) return new frmDinhMuc(MaDau("SELECT TOP(1) MaDichVu FROM dbo.DichVu ORDER BY MaDichVu"));
        if (kieu == typeof(frmChiTietPhieu)) return new frmChiTietPhieu(MaDau("SELECT TOP(1) MaPhieuTiepNhan FROM dbo.PhieuTiepNhan ORDER BY MaPhieuTiepNhan"));
        if (kieu == typeof(frmChiTietHoaDon)) return new frmChiTietHoaDon(PhieuHoanThanh());
        if (kieu == typeof(frmThanhToanQR))
        {
            return new frmThanhToanQR(PhieuHoanThanh());
        }
        if (kieu == typeof(frmXemBanInPhieu))
        {
            var bang = new DataTable();
            bang.Columns.Add("Loai"); bang.Columns.Add("NoiDung"); bang.Columns.Add("SoLuong", typeof(int));
            bang.Columns.Add("DonGia", typeof(decimal)); bang.Columns.Add("ThanhTien", typeof(decimal));
            for (int i = 1; i <= 65; i++) bang.Rows.Add("Dịch vụ", "Hạng mục xem trước " + i, 1, 10000m, 10000m);
            return new frmXemBanInPhieu(new MauInPhieu("PTN001", bang, "Bản xem trước nhiều trang trong bộ kiểm thử, không ghi dữ liệu SQL."));
        }
        return (Form)Activator.CreateInstance(kieu);
    }
    static IEnumerable<Button> NutCuaForm(Control goc, Form chu)
    {
        foreach (Control con in goc.Controls)
        {
            if (con is Form && con != chu) continue;
            if (con is Button) yield return (Button)con;
            foreach (var nut in NutCuaForm(con, chu)) yield return nut;
        }
    }
    static void HienTheChua(Control dieuKhien)
    {
        for (Control cha = dieuKhien.Parent; cha != null; cha = cha.Parent)
        {
            var the = cha as TabPage;
            if (the != null && the.Parent is TabControl) ((TabControl)the.Parent).SelectedTab = the;
        }
    }
    static void TaoDanhSach()
    {
        foreach (Type kieu in cacLoai)
            foreach (var truong in kieu.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly).Where(t => typeof(Button).IsAssignableFrom(t.FieldType)).OrderBy(t => t.MetadataToken))
                cacTruongHop.Add(new TruongHop { So = cacTruongHop.Count + 1, Kieu = kieu, TenNut = truong.Name });
    }
    static T DieuKhien<T>(Form trang, string ten) where T : Control
    {
        return trang.Controls.Find(ten, true).OfType<T>().Single();
    }
    static void ChuanBiCa(Form trang, TruongHop ca)
    {
        if (ca.Kieu == typeof(frmTiepNhan) && ca.TenNut == "btnLuu")
        {
            var bang = DieuKhien<DataGridView>(trang, "dgvPhieu");
            var dong = bang.Rows.Cast<DataGridViewRow>().FirstOrDefault(d => d.DataBoundItem is DataRowView && new[] { "Chờ sửa", "Đang sửa" }.Contains(((DataRowView)d.DataBoundItem)["TrangThai"].ToString()));
            if (dong == null) throw new InvalidOperationException("Không còn phiếu đang xử lý để kiểm tra Lưu phiếu.");
            bang.CurrentCell = dong.Cells["MaPhieuTiepNhan"];
            DieuKhien<Button>(trang, "btnSua").PerformClick();
        }
        if (ca.Kieu == typeof(frmKiemTraTiepNhan) && ca.TenNut == "btnChuyen")
        {
            var chon = DieuKhien<ComboBox>(trang, "cboDichVu");
            string ma = null;
            foreach (DataRowView dong in chon.Items)
            {
                string thu = dong["MaDichVu"].ToString();
                try { new NghiepVuSuaXe().KiemTraKho(new List<DichVuChon> { new DichVuChon { Ma = thu, SoLuong = 1 } }); ma = thu; break; }
                catch (InvalidOperationException) { }
            }
            if (ma == null) throw new InvalidOperationException("Không có dịch vụ đủ điều kiện để kiểm tra nút Chuyển.");
            chon.SelectedValue = ma;
            DieuKhien<Button>(trang, "btnThem").PerformClick();
            DieuKhien<Button>(trang, "btnKiemTra").PerformClick();
            if (!((frmKiemTraTiepNhan)trang).DuDieuKien) throw new InvalidOperationException("Kiểm tra dịch vụ chưa mở được nút Chuyển.");
        }
        if (ca.Kieu == typeof(frmXemBanInPhieu) && ca.TenNut == "btnTruoc") DieuKhien<Button>(trang, "btnSau").PerformClick();
        if (ca.Kieu == typeof(frmQuanLyTaiKhoan) && ca.TenNut == "btnHuyThem") DieuKhien<Button>(trang, "btnThemMoi").PerformClick();
        Ghi("PREPARED", new { index = ca.So, form = ca.Kieu.Name, button = ca.TenNut });
    }
    [STAThread]
    public static int Main(string[] thamSo)
    {
        try
        {
            Console.OutputEncoding = new UTF8Encoding(false);
            Console.InputEncoding = new UTF8Encoding(false);
            Directory.CreateDirectory(thuMuc);
            KiemTraDatabase();
            TaoDanhSach();
            if (thamSo.Contains("--list"))
            {
                foreach (var ca in cacTruongHop) Ghi("CASE", new { index = ca.So, form = ca.Kieu.Name, button = ca.TenNut });
                return 0;
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            using (var dieuKhien = new DieuKhienKiemThu()) Application.Run(dieuKhien);
            return 0;
        }
        catch (Exception loi) { Console.Error.WriteLine(loi); return 1; }
    }
    sealed class DieuKhienKiemThu : Form
    {
        readonly System.Windows.Forms.Timer nhip = new System.Windows.Forms.Timer { Interval = 400 };
        readonly Dictionary<Form, DateTime> cacTrangCon = new Dictionary<Form, DateTime>();
        readonly List<string> trangDaQuanSat = new List<string>();
        Form trang;
        Button nut;
        TruongHop hienTai;
        int keTiep;
        bool dangChay, dangMo, daBam, daTraVe, muonThoat, chayToanBo;
        Queue<TruongHop> cacCaThuLai;
        bool truocEnabled, truocVisible;
        string noiDungNut;
        DateTime lucBam, lucTraVe, lucBaoCho;
        Exception loiCa;
        public DieuKhienKiemThu()
        {
            Name = "DieuKhienKiemThu"; Text = "Điều khiển kiểm thử từng nút";
            ShowInTaskbar = false; Opacity = 0; StartPosition = FormStartPosition.Manual;
            Location = new Point(-5000, -5000); ClientSize = new Size(1, 1);
            nhip.Tick += Nhip;
            Application.ThreadException += LoiGiaoDien;
            Shown += (s, e) =>
            {
                Ghi("READY", new { total = cacTruongHop.Count, forms = cacLoai.Length, database = "QuanLySuaXe_CodexChecks", commands = "list | all | retry | pause | next [số] | open [số] | click | state | skip | quit", note = "Mỗi ca dùng form mới. Dialog Windows chờ Computer Use xử lý. clicked chỉ xác nhận đã gọi PerformClick, không kết luận nghiệp vụ đã đúng." });
                var docLenh = new Thread(() =>
                {
                    string lenh;
                    while ((lenh = Console.ReadLine()) != null)
                    {
                        string banSao = lenh;
                        try { BeginInvoke(new Action(() => NhanLenh(banSao))); }
                        catch (InvalidOperationException) { return; }
                    }
                    Ghi("INPUT_EOF", new { note = "Không còn stdin; cửa sổ kiểm thử vẫn mở. Chạy trong phiên console tương tác để gửi lệnh." });
                }) { IsBackground = true };
                docLenh.Start();
            };
        }
        void LoiGiaoDien(object s, ThreadExceptionEventArgs e)
        {
            loiCa = e.Exception;
            Ghi("UI_EXCEPTION", new { index = hienTai == null ? 0 : hienTai.So, error = e.Exception.ToString() });
        }
        void NhanLenh(string lenh)
        {
            var phan = (lenh ?? "").Trim().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (phan.Length == 0) return;
            string ten = phan[0].ToLowerInvariant();
            if (ten == "pause") { chayToanBo = false; Ghi("PAUSED", new { note = "Dừng tự chuyển ca sau khi đóng hộp thoại hiện tại." }); return; }
            if (ten == "all")
            {
                cacCaThuLai = null;
                chayToanBo = true;
                if (!dangChay && !dangMo && keTiep < cacTruongHop.Count) MoCa(cacTruongHop[keTiep++], true);
                return;
            }
            if (ten == "retry")
            {
                if (dangChay || dangMo) { Ghi("BUSY", new { note = "Chờ ca hiện tại kết thúc trước khi thử lại." }); return; }
                var ketQuaCu = new Dictionary<int, string>();
                foreach (string dong in File.ReadAllLines(Path.Combine(thuMuc, "su-kien.jsonl"), Encoding.UTF8))
                {
                    var muc = json.Deserialize<Dictionary<string, object>>(dong);
                    if (Convert.ToString(muc["kind"]) != "RESULT") continue;
                    var duLieu = (Dictionary<string, object>)muc["data"];
                    ketQuaCu[Convert.ToInt32(duLieu["index"])] = Convert.ToString(duLieu["status"]);
                }
                cacCaThuLai = new Queue<TruongHop>(cacTruongHop.Where(c => ketQuaCu.ContainsKey(c.So) && ketQuaCu[c.So] == "blocked"));
                Ghi("RETRY", new { count = cacCaThuLai.Count, indexes = cacCaThuLai.Select(c => c.So).ToArray() });
                chayToanBo = true;
                if (cacCaThuLai.Count > 0) MoCa(cacCaThuLai.Dequeue(), true);
                else { chayToanBo = false; Ghi("DONE", new { total = 0, retry = true }); }
                return;
            }
            if (ten == "list")
            {
                foreach (var ca in cacTruongHop) Ghi("CASE", new { index = ca.So, form = ca.Kieu.Name, button = ca.TenNut });
                return;
            }
            if (ten == "state")
            {
                Ghi("STATE", new { index = hienTai == null ? 0 : hienTai.So, busy = dangChay, loading = dangMo, clicked = daBam, returned = daTraVe, buttons = trang == null || trang.IsDisposed ? new object[0] : NutCuaForm(trang, trang).Select(b => (object)new { name = b.Name, text = b.Text, enabled = b.Enabled, visible = b.Visible }).ToArray() });
                return;
            }
            if (ten == "quit")
            {
                if (dangChay || dangMo) { muonThoat = true; Ghi("QUIT_PENDING", new { note = "Đóng hoặc hủy dialog đang mở trước; runner không tự thao tác dialog Windows." }); }
                else KetThuc();
                return;
            }
            if (ten == "click")
            {
                if (dangChay || dangMo || trang == null) { Ghi("BUSY", new { note = "Chưa có ca sẵn sàng để bấm." }); return; }
                Bam(); return;
            }
            if (ten == "skip" && !dangChay && !dangMo && hienTai != null)
            {
                KetQua("skipped", true, "Người kiểm thử bỏ qua ca này."); return;
            }
            if (ten != "next" && ten != "open") { Ghi("UNKNOWN_COMMAND", new { command = lenh }); return; }
            if (dangChay || dangMo) { Ghi("BUSY", new { index = hienTai == null ? 0 : hienTai.So, note = "Đang chờ nút hoặc dialog kết thúc." }); return; }
            if (phan.Length > 1)
            {
                int so;
                if (!int.TryParse(phan[1], out so) || so < 1 || so > cacTruongHop.Count) { Ghi("BAD_INDEX", new { min = 1, max = cacTruongHop.Count }); return; }
                keTiep = so - 1;
            }
            if (keTiep >= cacTruongHop.Count) { Ghi("DONE", new { total = cacTruongHop.Count, note = "Đã duyệt hết danh sách; đọc RESULT để phân biệt clicked/blocked/skipped/error." }); return; }
            MoCa(cacTruongHop[keTiep++], ten == "next");
        }
        void MoCa(TruongHop ca, bool tuBam)
        {
            DonTrang();
            hienTai = ca; daBam = daTraVe = false; loiCa = null;
            noiDungNut = ""; truocEnabled = truocVisible = false;
            trangDaQuanSat.Clear(); cacTrangCon.Clear();
            Ghi("NEXT", new { index = ca.So, total = cacTruongHop.Count, form = ca.Kieu.Name, button = ca.TenNut, automatic = tuBam });
            dangMo = true;
            nhip.Start();
            try
            {
                KiemTraDatabase();
                if (!PhienDangNhap.DangNhap("minh", "123")) throw new InvalidOperationException("Không đăng nhập được minh/123 trên bản sao kiểm thử.");
                trang = TaoForm(ca.Kieu);
                trang.StartPosition = FormStartPosition.Manual;
                trang.WindowState = FormWindowState.Normal;
                trang.Location = new Point(20, 20);
                trang.Size = new Size(1280, 900);
                trang.Show();
                trang.WindowState = FormWindowState.Normal;
                trang.Size = new Size(1280, 900);
                nut = NutCuaForm(trang, trang).SingleOrDefault(b => b.Name == ca.TenNut);
                if (nut == null) throw new InvalidOperationException("Không tìm được nút riêng của form: " + ca.TenNut);
                HienTheChua(nut);
                ChuanBiCa(trang, ca);
                if (trang is frmDangNhap)
                {
                    var ten = trang.Controls.Find("txtTenDangNhap", true).OfType<TextBox>().FirstOrDefault();
                    var matKhau = trang.Controls.Find("txtMatKhau", true).OfType<TextBox>().FirstOrDefault();
                    if (ten != null) ten.Text = "minh";
                    if (matKhau != null) matKhau.Text = "123";
                }
                dangMo = false;
                Ghi("OPEN", new { index = ca.So, form = ca.Kieu.Name, button = nut.Name, text = nut.Text, enabled = nut.Enabled, visible = nut.Visible });
                if (tuBam) BeginInvoke(new Action(Bam));
                else { nhip.Stop(); Ghi("WAIT_FOR_CLICK", new { index = ca.So, note = "Có thể nhập/chọn dữ liệu trong form bằng Computer Use, rồi gửi click." }); }
            }
            catch (Exception loi)
            {
                dangMo = false; loiCa = loi; KetQua("open_error", true, loi.Message);
            }
        }
        void Bam()
        {
            if (nut == null || trang == null || trang.IsDisposed) { KetQua("missing_button", true, "Form hoặc nút đã đóng."); return; }
            HienTheChua(nut);
            noiDungNut = nut.Text; truocEnabled = nut.Enabled; truocVisible = nut.Visible;
            if (!truocEnabled || !truocVisible) { KetQua("blocked", true, hienTai.Kieu == typeof(frmChinh) && hienTai.TenNut == "btnMoPhanCong" ? "Nút phân công riêng được ẩn có chủ đích; chức năng đã gộp qua Nhân viên → Phân công." : !truocVisible ? "Nút đang ẩn theo trạng thái/phân quyền." : "Nút đang bị vô hiệu theo trạng thái dữ liệu."); return; }
            dangChay = true; daBam = true; daTraVe = false; lucBam = lucBaoCho = DateTime.UtcNow;
            nhip.Start();
            Ghi("INVOKE", new { index = hienTai.So, form = hienTai.Kieu.Name, button = nut.Name, text = nut.Text, enabled = truocEnabled, visible = truocVisible });
            try { nut.PerformClick(); }
            catch (Exception loi) { loiCa = loi; }
            finally { daTraVe = true; lucTraVe = DateTime.UtcNow; }
        }
        void Nhip(object s, EventArgs e)
        {
            foreach (Form con in Application.OpenForms.Cast<Form>().ToArray())
            {
                if (con == this || con == trang || con.IsDisposed || !con.TopLevel || con.GetType().Assembly != typeof(frmChinh).Assembly) continue;
                if (!cacTrangCon.ContainsKey(con))
                {
                    cacTrangCon.Add(con, DateTime.UtcNow); trangDaQuanSat.Add(con.GetType().Name);
                    Ghi("OWN_DIALOG", new { index = hienTai == null ? 0 : hienTai.So, form = con.GetType().Name, title = con.Text, buttons = NutCuaForm(con, con).Select(b => new { name = b.Name, text = b.Text, enabled = b.Enabled, visible = b.Visible }).ToArray() });
                }
                else if ((DateTime.UtcNow - cacTrangCon[con]).TotalMilliseconds > 1800)
                {
                    Ghi("CLOSE_OWN_DIALOG", new { index = hienTai == null ? 0 : hienTai.So, form = con.GetType().Name, note = "Chỉ đóng Form thuộc assembly ứng dụng, không thao tác dialog Windows." });
                    con.Close();
                }
            }
            if (!dangChay) return;
            if (!daTraVe)
            {
                if ((DateTime.UtcNow - lucBaoCho).TotalSeconds >= 5)
                {
                    lucBaoCho = DateTime.UtcNow;
                    Ghi("WAITING_DIALOG", new { index = hienTai.So, form = hienTai.Kieu.Name, button = hienTai.TenNut, seconds = (int)(DateTime.UtcNow - lucBam).TotalSeconds, note = "PerformClick chưa trả về. Computer Use kiểm tra và xử lý dialog Windows nếu đang mở." });
                }
                return;
            }
            if ((DateTime.UtcNow - lucTraVe).TotalMilliseconds < 1300) return;
            bool dangBatDongBo = trang != null && !trang.IsDisposed && nut != null && !nut.IsDisposed && !nut.Enabled && (trang is frmDangNhap && string.IsNullOrEmpty(((frmDangNhap)trang).TenDangNhap) || trang is frmDoiMatKhau);
            if (dangBatDongBo && (DateTime.UtcNow - lucTraVe).TotalSeconds < 15) return;
            KetQua(loiCa != null ? "error" : dangBatDongBo ? "async_pending" : "clicked", false, loiCa == null ? "Đã gọi PerformClick; cần đối chiếu hành vi/SQL theo chức năng." : loiCa.Message);
        }
        void KetQua(string ketQua, bool biChan, string lyDo)
        {
            nhip.Stop(); dangChay = dangMo = false;
            Ghi("RESULT", new { index = hienTai == null ? 0 : hienTai.So, form = hienTai == null ? "" : hienTai.Kieu.Name, button = hienTai == null ? "" : hienTai.TenNut, text = noiDungNut, enabled = truocEnabled, visible = truocVisible, clicked = daBam, blocked = biChan, status = ketQua, reason = lyDo, ownDialogs = trangDaQuanSat.ToArray(), error = loiCa == null ? null : loiCa.ToString() });
            if (trang != null && !trang.IsDisposed)
            {
                try
                {
                    using (var anh = new Bitmap(trang.Width, trang.Height))
                    {
                        trang.DrawToBitmap(anh, new Rectangle(Point.Empty, anh.Size));
                        anh.Save(Path.Combine(thuMuc, hienTai.So.ToString("D3") + "-" + hienTai.Kieu.Name + "-" + hienTai.TenNut + ".png"));
                    }
                }
                catch (Exception loi) { Ghi("SCREENSHOT_ERROR", new { index = hienTai.So, error = loi.Message }); }
            }
            DonTrang();
            if (muonThoat) KetThuc();
            else if (chayToanBo && cacCaThuLai != null && cacCaThuLai.Count > 0) BeginInvoke(new Action(() => MoCa(cacCaThuLai.Dequeue(), true)));
            else if (chayToanBo && cacCaThuLai == null && keTiep < cacTruongHop.Count) BeginInvoke(new Action(() => MoCa(cacTruongHop[keTiep++], true)));
            else if (chayToanBo) { chayToanBo = false; Ghi("DONE", new { total = cacTruongHop.Count, retry = cacCaThuLai != null }); }
        }
        void DonTrang()
        {
            foreach (Form con in Application.OpenForms.Cast<Form>().ToArray())
                if (con != this && con != trang && con.TopLevel && con.GetType().Assembly == typeof(frmChinh).Assembly) con.Dispose();
            if (trang != null) { trang.Dispose(); trang = null; }
            nut = null;
        }
        void KetThuc()
        {
            nhip.Stop(); DonTrang();
            Ghi("QUIT", new { next = keTiep + 1, total = cacTruongHop.Count });
            Close();
        }
        protected override void Dispose(bool dangHuy)
        {
            if (dangHuy) { Application.ThreadException -= LoiGiaoDien; nhip.Dispose(); }
            base.Dispose(dangHuy);
        }
    }
}
