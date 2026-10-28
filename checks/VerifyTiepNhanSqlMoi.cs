using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using QLyCuaHangSuaXe.LopDungChung;
using QLyCuaHangSuaXe.DanhMuc;
using QLyCuaHangSuaXe.DanhMuc.DichVu;
using QLyCuaHangSuaXe.DanhMuc.TiepNhan;

public static class KiemTraTiepNhan
{
    static readonly XuLyDuLieu dl = new XuLyDuLieu();
    static readonly BindingFlags co = BindingFlags.NonPublic | BindingFlags.Instance;
    static int so;
    static T Lay<T>(object o, string n) { return (T)o.GetType().GetField(n, co).GetValue(o); }
    static object Goi(object o, string n, params object[] a)
    {
        try { return o.GetType().GetMethod(n, co).Invoke(o, a); }
        catch (TargetInvocationException e) { throw e.InnerException; }
    }
    static object GiaTri(string sql) { return dl.DocBang(sql).Rows[0][0]; }
    static void Dung(bool dk, string ten) { if (!dk) throw new Exception(ten); so++; Console.WriteLine("PASS: " + ten); }
    static void TuChoi(Action a, string ten)
    {
        try { a(); } catch (InvalidOperationException) { Dung(true, ten); return; }
        throw new Exception("Không chặn: " + ten);
    }
    static void Hien(Form f) { f.StartPosition = FormStartPosition.Manual; f.Location = new Point(-3000, -3000); f.Show(); Application.DoEvents(); }
    static void Anh(Form f, string ten) { using (var b = new Bitmap(f.Width, f.Height)) { f.DrawToBitmap(b, new Rectangle(Point.Empty, b.Size)); b.Save("checks/output/" + ten + ".png"); } }
    static string TaoTho(string chucVu, string ten)
    {
        string ma = XuLyDuLieu.TaoMaMoi("NV");
        dl.CapNhatDuLieu("INSERT dbo.NhanVien(MaNhanVien,MaChucVu,HoTen,SoDienThoai,Luong,TenDangNhap,MatKhau,TrangThai) VALUES(@ma,@cv,@ten,@dt,8000000,@tk,@mk,N'Đang làm')", XuLyDuLieu.ThamSo("@ma", ma), XuLyDuLieu.ThamSo("@cv", chucVu), XuLyDuLieu.ThamSo("@ten", ten), XuLyDuLieu.ThamSo("@dt", "0918" + ma.Substring(2).PadLeft(6, '0')), XuLyDuLieu.ThamSo("@tk", "thu_" + ma), XuLyDuLieu.ThamSo("@mk", BaoMat.BamMatKhau("123")));
        return ma;
    }
    static ThongTinNhanXe Khach(string tho, string bien)
    {
        return new ThongTinNhanXe { TenKhach = "Khách kiểm thử tiếp nhận", DienThoai = "0998333444", BienSo = bien, DongXe = "Honda", LoaiXe = "Vision", MauSac = "Đỏ", MaTho = tho, HenTra = DateTime.Today.AddDays(1) };
    }
    static void KiemTraTimTheoKhach(frmTiepNhan f, string maPhieu)
    {
        var loc = Lay<ComboBox>(f, "cboLocTrangThai");
        var tim = Lay<TextBox>(f, "txtTimKiem");
        var nut = Lay<Button>(f, "btnTimKiem");
        var bang = Lay<DataGridView>(f, "dgvPhieu");
        Dung(loc.Items.Cast<object>().Select(x => x.ToString()).SequenceEqual(new[] { "Tất cả", "Chờ sửa", "Hoàn thành", "Đã hủy" }) && loc.Text == "Tất cả", "Bộ lọc tiếp nhận đúng bốn lựa chọn và mặc định Tất cả");
        Dung(!bang.Columns.Contains("NhacHen") && bang.Columns.Cast<DataGridViewColumn>().Count(c => c.Visible) == 7, "Danh sách tiếp nhận bỏ hẳn cột nhắc hẹn");
        tim.Text = "KHÁCH KIỂM THỬ TIẾP NHẬN"; nut.PerformClick();
        Dung(bang.Rows.Count == 1 && Convert.ToString(bang.Rows[0].Cells["MaPhieuTiepNhan"].Value) == maPhieu, "Bấm tìm theo tên khách không phân biệt chữ hoa thường");
        tim.Text = "0998333444"; nut.PerformClick();
        Dung(bang.Rows.Count == 1 && Convert.ToString(bang.Rows[0].Cells["MaPhieuTiepNhan"].Value) == maPhieu, "Bấm tìm theo số điện thoại khách hàng");
        tim.Text = maPhieu; nut.PerformClick();
        Dung(bang.Rows.Count == 0, "Ô tìm khách hàng không tìm theo mã phiếu");
        tim.Text = "KT-TEST-01"; nut.PerformClick();
        Dung(bang.Rows.Count == 0, "Ô tìm khách hàng không tìm theo biển số");
        tim.Text = "0998333444' OR 1=1--"; nut.PerformClick();
        Dung(bang.Rows.Count == 0, "Nội dung tìm có dấu nháy được đối chiếu nguyên văn");
        tim.Clear(); nut.PerformClick();
        Dung(bang.Rows.Count == Convert.ToInt32(GiaTri("SELECT COUNT(*) FROM dbo.PhieuTiepNhan")), "Xóa từ khóa trả về toàn bộ phiếu");
        loc.SelectedItem = "Chờ sửa";
        Dung(bang.Rows.Count == Convert.ToInt32(GiaTri("SELECT COUNT(*) FROM dbo.PhieuTiepNhan WHERE TrangThai IN(N'Chờ sửa',N'Đang sửa')")), "Lọc chờ sửa bao gồm xe đang thực hiện để không bỏ sót công việc");
        loc.SelectedItem = "Hoàn thành";
        Dung(bang.Rows.Count == Convert.ToInt32(GiaTri("SELECT COUNT(*) FROM dbo.PhieuTiepNhan WHERE TrangThai=N'Hoàn thành'")), "Lọc hoàn thành theo tiến độ, gồm cả phiếu đã thu và chưa thu tiền");
        loc.SelectedItem = "Đã hủy";
        Dung(bang.Rows.Count == Convert.ToInt32(GiaTri("SELECT COUNT(*) FROM dbo.PhieuTiepNhan WHERE TrangThai=N'Đã hủy'")), "Lọc phiếu đã hủy đúng trạng thái trong SQL");
        loc.SelectedItem = "Tất cả";
    }
    public static int Chay()
    {
        so = 0;
        Dung(new SqlConnectionStringBuilder(XuLyDuLieu.ChuoiKetNoi).InitialCatalog.EndsWith("_CodexChecks", StringComparison.Ordinal), "Tiếp nhận chỉ kiểm thử ghi trên bản sao riêng");
        Dung(PhienDangNhap.DangNhap("minh", "123"), "Đăng nhập quản lý dữ liệu mới để kiểm thử tiếp nhận");
        var nv = new NghiepVuSuaXe();
        string dv = XuLyDuLieu.TaoMaMoi("DV"), dvPhu, pt = Convert.ToString(GiaTri("SELECT TOP(1) MaPhuTung FROM dbo.PhuTung WHERE TrangThai=N'Đang kinh doanh' AND HanSuDung>GETDATE() ORDER BY MaPhuTung"));
        using (var f = new frmDanhMucDichVu())
        {
            Hien(f); Lay<Button>(f, "btnThem").PerformClick(); Dung(Lay<TextBox>(f, "txtMaDV").Text == dv, "Nút tạo dịch vụ hiển thị sẵn mã tăng tiếp đúng định dạng");
            Lay<TextBox>(f, "txtTenDichVu").Text = "Dịch vụ thử đủ luồng"; Lay<TextBox>(f, "txtMoTa").Text = "Kiểm thử riêng"; Lay<NumericUpDown>(f, "numGiaDichVu").Value = 86000; Lay<Button>(f, "btnSua").PerformClick();
            Dung(Convert.ToDecimal(GiaTri("SELECT ThanhTienDichVu FROM dbo.DichVu WHERE MaDichVu='" + dv + "'")) == 86000, "Bấm Lưu thêm dịch vụ đúng schema mới không thiếu cột");
            dl.CapNhatDuLieu("INSERT dbo.ChiTietDichVu(MaDichVu,MaPhuTung,SoLuong) VALUES(@dv,@pt,2)", XuLyDuLieu.ThamSo("@dv", dv), XuLyDuLieu.ThamSo("@pt", pt));
            Lay<Button>(f, "btnNgungSuDung").PerformClick();
            Dung(Convert.ToString(GiaTri("SELECT TrangThai FROM dbo.DichVu WHERE MaDichVu='" + dv + "'")) == "Ngừng áp dụng" && Convert.ToInt32(GiaTri("SELECT COUNT(*) FROM dbo.ChiTietDichVu WHERE MaDichVu='" + dv + "'")) == 1, "Ngừng áp dụng giữ nguyên dịch vụ chưa sử dụng và bộ phụ tùng");
            Lay<ComboBox>(f, "cboTrangThai").SelectedItem = "Đang áp dụng"; Lay<Button>(f, "btnSua").PerformClick();
            Dung(f.GetType().GetField("numLuongNhiemVu", co) == null && f.GetType().GetField("numPhutDuKien", co) == null, "Designer dịch vụ không còn các trường lương/thời gian đã bỏ trong SQL"); Anh(f, "sql-moi-dich-vu");
        }
        dvPhu = XuLyDuLieu.TaoMaMoi("DV");
        dl.CapNhatDuLieu("INSERT dbo.DichVu(MaDichVu,TenDichVu,MoTa,ThanhTienDichVu,TrangThai) VALUES(@id,N'Dịch vụ phụ thử',N'',99000,N'Đang áp dụng');INSERT dbo.ChiTietDichVu(MaDichVu,MaPhuTung,SoLuong) VALUES(@id,@pt,2);UPDATE dbo.PhuTung SET SoLuongTon=3,TrangThai=N'Đang kinh doanh',HanSuDung='20991231' WHERE MaPhuTung=@pt", XuLyDuLieu.ThamSo("@id", dvPhu), XuLyDuLieu.ThamSo("@pt", pt));
        var mot = new List<DichVuChon> { new DichVuChon { Ma = dv, SoLuong = 1 } };
        var hai = new List<DichVuChon> { new DichVuChon { Ma = dv, SoLuong = 1 }, new DichVuChon { Ma = dvPhu, SoLuong = 1 } };
        TuChoi(() => nv.KiemTraKho(hai), "Kiểm tra tổng nhu cầu khi hai dịch vụ cùng dùng một phụ tùng");
        dl.CapNhatDuLieu("UPDATE dbo.PhuTung SET SoLuongTon=5,HanSuDung='20000101' WHERE MaPhuTung=@id", XuLyDuLieu.ThamSo("@id", pt));
        TuChoi(() => nv.KiemTraKho(mot), "Chặn phụ tùng hết hạn dù vẫn còn tồn");
        dl.CapNhatDuLieu("UPDATE dbo.PhuTung SET HanSuDung='20991231',TrangThai=N'Ngừng kinh doanh' WHERE MaPhuTung=@id", XuLyDuLieu.ThamSo("@id", pt));
        TuChoi(() => nv.KiemTraKho(mot), "Chặn phụ tùng ngừng kinh doanh");
        dl.CapNhatDuLieu("UPDATE dbo.PhuTung SET TrangThai=N'Đang kinh doanh' WHERE MaPhuTung=@id", XuLyDuLieu.ThamSo("@id", pt));
        DataTable hangMuc;
        using (var f = new frmKiemTraTiepNhan())
        {
            Hien(f); Lay<ComboBox>(f, "cboDichVu").SelectedValue = dv; Lay<Button>(f, "btnThem").PerformClick(); Lay<Button>(f, "btnKiemTra").PerformClick();
            Dung(f.DuDieuKien && f.TongDuKien == 86000, "Bấm chọn và kiểm tra dịch vụ tính giá trọn gói, không cộng phụ tùng lần hai");
            var bang = (DataTable)Lay<DataGridView>(f, "dgvHangMuc").DataSource; bang.Rows[0]["SoLuong"] = 2;
            Dung(!f.DuDieuKien && !Lay<Button>(f, "btnChuyen").Enabled, "Đổi số lượng vô hiệu kết quả kiểm tra cũ");
            Lay<Button>(f, "btnKiemTra").PerformClick(); Dung(f.DuDieuKien && f.TongDuKien == 172000, "Bấm kiểm tra lại sau đổi số lượng");
            bang.Rows[0]["SoLuong"] = 1; Lay<Button>(f, "btnKiemTra").PerformClick(); hangMuc = f.ChiTietDaChon;
            Dung(f.GetType().GetField("rtbTinhTrang", co) == null, "Bỏ thông tin tình trạng không có cột lưu trong SQL mới"); Anh(f, "sql-moi-kiem-tra");
        }
        string tho = TaoTho("CV003", "Thợ thử tiếp nhận"), chinh = TaoTho("CV002", "Thợ chính thử tiếp nhận"), tn;
        int khachTruoc = Convert.ToInt32(GiaTri("SELECT COUNT(*) FROM dbo.KhachHang"));
        using (var f = new frmTiepNhan())
        {
            Hien(f); f.NhanKetQuaKiemTra(mot, hangMuc, ""); tn = Lay<TextBox>(f, "txtMaPhieu").Text;
            Lay<TextBox>(f, "txtKhachHang").Text = "Khách kiểm thử tiếp nhận"; Lay<TextBox>(f, "txtDienThoai").Text = "0998333444"; Lay<TextBox>(f, "txtBienSoXe").Text = "KT-TEST-01"; Lay<TextBox>(f, "txtDongXe").Text = "Honda"; Lay<TextBox>(f, "txtMauSac").Text = "Đỏ"; Lay<ComboBox>(f, "cboLoaiXe").Text = "Vision"; Lay<ComboBox>(f, "cboThoPhuTrach").SelectedValue = tho;
            Lay<Button>(f, "btnLuu").PerformClick();
            Dung(Convert.ToInt32(GiaTri("SELECT COUNT(*) FROM dbo.PhieuTiepNhan p JOIN dbo.Xe x ON x.MaXe=p.MaXe WHERE p.MaPhieuTiepNhan='" + tn + "' AND x.MauSac=N'Đỏ' AND x.LoaiXe='Vision' AND x.HangSanXuat='Honda'")) == 1, "Bấm Lưu tiếp nhận ghi đúng màu sắc, dòng xe và hãng theo SQL mới");
            KiemTraTimTheoKhach(f, tn);
            Dung(f.GetType().GetField("txtDiaChi", co) == null && f.GetType().GetField("rtbTinhTrangXe", co) == null, "Designer tiếp nhận bỏ hẳn địa chỉ và tình trạng đã xóa trong database"); Anh(f, "sql-moi-tiep-nhan");
        }
        Dung(Convert.ToInt32(GiaTri("SELECT SoLuongTon FROM dbo.PhuTung WHERE MaPhuTung='" + pt + "'")) == 3, "Lưu phiếu trừ đúng định mức phụ tùng trong giao dịch");
        string tn2 = nv.TiepNhan(mot, Khach(tho, "KT-TEST-02"));
        Dung(Convert.ToInt32(GiaTri("SELECT COUNT(*) FROM dbo.KhachHang")) == khachTruoc + 1, "Khách cùng số điện thoại dùng lại hồ sơ, mỗi lượt xe có bản ghi riêng");
        int phieuTruoc = Convert.ToInt32(GiaTri("SELECT COUNT(*) FROM dbo.PhieuTiepNhan"));
        dl.CapNhatDuLieu("UPDATE dbo.PhuTung SET SoLuongTon=100 WHERE MaPhuTung=@id", XuLyDuLieu.ThamSo("@id", pt));
        TuChoi(() => nv.TiepNhan(mot, Khach(tho, "KT-TEST-03")), "Thợ nhận tối đa hai xe đang xử lý");
        Dung(Convert.ToInt32(GiaTri("SELECT COUNT(*) FROM dbo.PhieuTiepNhan")) == phieuTruoc && Convert.ToInt32(GiaTri("SELECT SoLuongTon FROM dbo.PhuTung WHERE MaPhuTung='" + pt + "'")) == 100, "Nhận vượt giới hạn không sinh phiếu, khách, xe hoặc trừ tồn dở dang");
        string a = nv.TiepNhan(mot, Khach(chinh, "KT-CHINH-01")), b = nv.TiepNhan(mot, Khach(chinh, "KT-CHINH-02")), c = nv.TiepNhan(mot, Khach(chinh, "KT-CHINH-03"));
        TuChoi(() => nv.TiepNhan(mot, Khach(chinh, "KT-CHINH-04")), "Thợ chính nhận tối đa ba xe đang xử lý");
        TuChoi(() => nv.SuaPhieu(tn, DateTime.Today.AddDays(4), "Hoàn thành", tho), "Chặn bỏ qua trạng thái đang sửa");
        Dung(Convert.ToDateTime(GiaTri("SELECT NgayHen FROM dbo.PhieuTiepNhan WHERE MaPhieuTiepNhan='" + tn + "'")) == DateTime.Today.AddDays(1), "Sửa phiếu thất bại không cập nhật riêng ngày hẹn");
        nv.TrangThaiCongViec(tn, tho, "Đang sửa"); nv.TrangThaiCongViec(tn, tho, "Hoàn thành");
        string hd = nv.LapHoaDon(tn);
        Dung(Convert.ToInt32(GiaTri("SELECT COUNT(*) FROM dbo.HoaDon WHERE MaPhieuTiepNhan='" + tn + "'")) == 0, "Hoàn thành sẵn sàng hóa đơn, không ghi ngày thanh toán giả");
        dl.CapNhatDuLieu("UPDATE dbo.DichVu SET ThanhTienDichVu=98000 WHERE MaDichVu=@id", XuLyDuLieu.ThamSo("@id", dv));
        nv.ThanhToanPhieu(tn, "Tiền mặt", hd);
        Dung(Convert.ToDecimal(GiaTri("SELECT TienThanhToan FROM dbo.HoaDon WHERE MaHoaDon='" + hd + "'")) == 86000, "Hóa đơn lấy tiền chi tiết tiếp nhận đã chốt dù giá danh mục thay đổi");
        TuChoi(() => nv.ThanhToanPhieu(tn, "Tiền mặt", hd), "Chặn thanh toán lặp một phiếu");
        TuChoi(() => nv.SuaPhieu(tn, DateTime.Today.AddDays(3), "Hoàn thành", tho), "Phiếu hoàn thành đã thanh toán chỉ đọc");
        nv.TrangThaiCongViec(tn2, tho, "Đang sửa"); nv.TrangThaiCongViec(tn2, tho, "Hoàn thành");
        using (var f = new frmThanhToanQR(tn2, nv.LapHoaDon(tn2)))
        {
            Hien(f); Dung(Lay<PictureBox>(f, "picMaQR").Image != null && Lay<Label>(f, "lblSoTien").Text.Contains("86"), "Mở QR có mã ngân hàng và đúng tiền chi tiết phiếu ngay"); Anh(f, "sql-moi-qr"); Lay<Button>(f, "btnDong").PerformClick();
        }
        Dung(Convert.ToInt32(GiaTri("SELECT COUNT(*) FROM dbo.HoaDon WHERE MaPhieuTiepNhan='" + tn2 + "'")) == 0, "Đóng QR chưa xác nhận không ghi thanh toán");
        nv.ThanhToanPhieu(tn2, "Chuyển khoản", nv.LapHoaDon(tn2));
        Dung(Convert.ToInt32(GiaTri("SELECT COUNT(*) FROM dbo.HoaDon WHERE MaPhieuTiepNhan='" + tn2 + "' AND NgayThanhToan>=NgayLap")) == 1, "Xác nhận chuyển khoản lưu hóa đơn không NULL đúng ràng buộc ngày");
        int ton = Convert.ToInt32(GiaTri("SELECT SoLuongTon FROM dbo.PhuTung WHERE MaPhuTung='" + pt + "'")); nv.HuyChuaSua(a);
        Dung(Convert.ToInt32(GiaTri("SELECT SoLuongTon FROM dbo.PhuTung WHERE MaPhuTung='" + pt + "'")) == ton + 2 && Convert.ToString(GiaTri("SELECT TrangThai FROM dbo.PhieuTiepNhan WHERE MaPhieuTiepNhan='" + a + "'")) == "Đã hủy", "Hủy phiếu chờ sửa hoàn đúng phụ tùng và giữ lịch sử");
        using (var f = new frmDinhMuc(dv)) { Hien(f); TuChoi(() => Goi(f, "Luu"), "Không đổi định mức khi còn phiếu đang sử dụng dịch vụ"); }
        using (var f = new frmHoaDon()) { Hien(f); Dung(f.Controls.Count > 0, "Mở danh sách hóa đơn với schema mới"); Anh(f, "sql-moi-hoa-don"); }
        var ct = dl.DocBang("SELECT N'Dịch vụ' AS Loai,d.TenDichVu AS NoiDung,c.SoLuong,c.ThanhTien/c.SoLuong AS DonGia,c.ThanhTien FROM dbo.ChiTietPhieuTiepNhan c JOIN dbo.DichVu d ON d.MaDichVu=c.MaDichVu WHERE c.MaPhieuTiepNhan='" + tn + "'");
        using (var anh = new MauInPhieu(tn, ct, "Khách thử").TaoAnh(0)) { Dung(anh.Width > 0, "Tạo ngay bản in từ chi tiết đúng schema"); }
        return so;
    }
}
