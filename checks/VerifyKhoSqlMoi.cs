using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using QLyCuaHangSuaXe.LopDungChung;
using QLyCuaHangSuaXe.DanhMuc.TiepNhan;

public static class KiemTraKho
{
    static readonly XuLyDuLieu duLieu = new XuLyDuLieu();
    static readonly BindingFlags co = BindingFlags.Instance | BindingFlags.NonPublic;
    static int soKiemTra;

    static T Lay<T>(object trang, string ten)
    {
        var truong = trang.GetType().GetField(ten, co);
        if (truong == null) throw new Exception("Không có thành phần " + ten);
        return (T)truong.GetValue(trang);
    }
    static object Goi(object trang, string ten, params object[] thamSo)
    {
        try { return trang.GetType().GetMethod(ten, co).Invoke(trang, thamSo); }
        catch (TargetInvocationException loi) { throw loi.InnerException ?? loi; }
    }
    static object GiaTri(string sql, params SqlParameter[] thamSo)
    {
        return duLieu.DocBang(sql, thamSo).Rows[0][0];
    }
    static void Dung(bool dieuKien, string noiDung)
    {
        if (!dieuKien) throw new Exception("KHO: " + noiDung);
        soKiemTra++;
        Console.WriteLine("PASS KHO: " + noiDung);
    }
    static void Hien(Form trang)
    {
        trang.StartPosition = FormStartPosition.Manual;
        trang.Location = new Point(-4000, -4000);
        trang.Show();
        Application.DoEvents();
    }
    static void Anh(Form trang, string ten)
    {
        Directory.CreateDirectory("checks/output/sql-moi");
        using (var anh = new Bitmap(trang.Width, trang.Height))
        {
            trang.DrawToBitmap(anh, new Rectangle(Point.Empty, anh.Size));
            anh.Save("checks/output/sql-moi/" + ten + ".png");
        }
    }
    static bool KhongCoPhieu(string ma)
    {
        return Convert.ToInt32(GiaTri("SELECT COUNT(*) FROM dbo.PhieuNhap WHERE MaPhieuNhap=@ma", XuLyDuLieu.ThamSo("@ma", ma))) == 0;
    }
    static int Ton(string ma)
    {
        return Convert.ToInt32(GiaTri("SELECT SoLuongTon FROM dbo.PhuTung WHERE MaPhuTung=@ma", XuLyDuLieu.ThamSo("@ma", ma)));
    }
    static void PhaiBaoLoi(Action thaoTac, Type loai)
    {
        try { thaoTac(); }
        catch (Exception loi)
        {
            if (loai.IsInstanceOfType(loi)) return;
            throw;
        }
        throw new Exception("Phải từ chối dữ liệu sai: " + loai.Name);
    }
    public static int Chay()
    {
        soKiemTra = 0;
        Dung(new SqlConnectionStringBuilder(XuLyDuLieu.ChuoiKetNoi).InitialCatalog == "QuanLySuaXe_CodexChecks" && GiaTri("SELECT DB_NAME()").ToString() == "QuanLySuaXe_CodexChecks", "Chỉ ghi dữ liệu vào database kiểm thử riêng");
        Dung(PhienDangNhap.DangNhap("minh", "123"), "Đăng nhập quản lý từ dữ liệu SQL mới");
        string nhaCungCap;
        using (var trang = new btl.frmThemNCC())
        {
            Hien(trang);
            nhaCungCap = Lay<TextBox>(trang, "txtMaNCC").Text;
            Dung(System.Text.RegularExpressions.Regex.IsMatch(nhaCungCap, "^NCC[0-9]{3,}$"), "Mã nhà cung cấp có sẵn và đúng định dạng");
            Lay<TextBox>(trang, "txtTenNCC").Text = "Nhà cung cấp kiểm thử SQL mới";
            Lay<TextBox>(trang, "txtSDT").Text = "0987654321";
            Lay<RichTextBox>(trang, "txtDiaChi").Text = "Hải Phòng";
            Lay<Button>(trang, "btnXacNhan").PerformClick();
            Dung(trang.DialogResult == DialogResult.OK && Convert.ToInt32(GiaTri("SELECT COUNT(*) FROM dbo.NhaCungCap WHERE MaNhaCungCap=@ma AND TenNhaCungCap=N'Nhà cung cấp kiểm thử SQL mới'", XuLyDuLieu.ThamSo("@ma", nhaCungCap))) == 1, "Nút lưu nhà cung cấp ghi đúng schema mới");
        }
        int tonCu = Ton("PT001");
        decimal giaNhapCu = Convert.ToDecimal(GiaTri("SELECT DonGiaNhap FROM dbo.PhuTung WHERE MaPhuTung='PT001'"));
        decimal giaBanCu = Convert.ToDecimal(GiaTri("SELECT DonGiaBan FROM dbo.PhuTung WHERE MaPhuTung='PT001'"));
        var lichSu = duLieu.DocBang("SELECT TOP(1) MaPhieuNhap,SoLuongNhap,ThanhTien FROM dbo.ChiTietPhieuNhap WHERE MaPhuTung='PT001' ORDER BY MaPhieuNhap").Rows[0];
        decimal giaLichSu = Convert.ToDecimal(lichSu["ThanhTien"]) / Convert.ToInt32(lichSu["SoLuongNhap"]);
        string phuTungMoi, phieuNhap;
        DataTable chiTietDaLuu;
        using (var trang = new btl.frmNhapPhieuNhap())
        {
            Hien(trang);
            phieuNhap = Lay<TextBox>(trang, "txtMaPhieuNhap").Text;
            Dung(System.Text.RegularExpressions.Regex.IsMatch(phieuNhap, "^PN[0-9]{3,}$") && Lay<TextBox>(trang, "txtMaPhieuNhap").ReadOnly, "Mã phiếu nhập cấp sẵn và không sửa thủ công");
            Lay<ComboBox>(trang, "cbMaPhuTung").SelectedValue = "PT001";
            Dung(Lay<TextBox>(trang, "txtTenPhuTung").ReadOnly && !Lay<ComboBox>(trang, "cbNhaCungCap").Enabled && !Lay<DateTimePicker>(trang, "dtpHanSuDung").Enabled, "Khóa tên, nhà cung cấp và hạn của mã phụ tùng đã có");
            Lay<TextBox>(trang, "txtSoLuongNhap").Text = "2";
            Lay<TextBox>(trang, "txtDonGiaNhap").Text = (giaNhapCu + 1000).ToString("0");
            Lay<TextBox>(trang, "txtDonGiaBan").Text = Math.Max(giaBanCu, giaNhapCu + 1000).ToString("0");
            Lay<Button>(trang, "btnThemPhuTung").PerformClick();
            Lay<Button>(trang, "btnMoiPhuTung").PerformClick();
            phuTungMoi = Lay<ComboBox>(trang, "cbMaPhuTung").Text;
            Lay<ComboBox>(trang, "cbNhaCungCap").SelectedValue = nhaCungCap;
            Lay<TextBox>(trang, "txtTenPhuTung").Text = "Phụ tùng kiểm thử SQL mới";
            Lay<TextBox>(trang, "txtSoLuongNhap").Text = "5";
            Lay<TextBox>(trang, "txtDonGiaNhap").Text = "10000";
            Lay<TextBox>(trang, "txtDonGiaBan").Text = "15000";
            Lay<Button>(trang, "btnThemPhuTung").PerformClick();
            var chiTiet = Lay<DataTable>(trang, "bangChiTiet");
            Dung(chiTiet.Rows.Count == 2, "Hai lần bấm thêm tạo đủ dòng phụ tùng cũ và mới");
            PhaiBaoLoi(() => Goi(trang, "ThemDong", false), typeof(InvalidOperationException));
            Dung(chiTiet.Rows.Count == 2, "Chặn dòng trùng trước khi ghi vào khóa ghép");
            var luoiNhap = Lay<DataGridView>(trang, "dgvPhuTung");
            luoiNhap.Sort(luoiNhap.Columns["MaPhuTung"], System.ComponentModel.ListSortDirection.Descending);
            Application.DoEvents();
            Goi(trang, "dgvPhuTung_CellClick", luoiNhap, new DataGridViewCellEventArgs(0, 0));
            Dung(Lay<ComboBox>(trang, "cbMaPhuTung").Text == phuTungMoi, "Sắp xếp bảng vẫn chọn đúng phụ tùng để sửa");
            Lay<TextBox>(trang, "txtSoLuongNhap").Text = "6";
            Lay<Button>(trang, "btnSuaPhuTung").PerformClick();
            Dung(Convert.ToInt32(chiTiet.AsEnumerable().Single(d => d["MaPhuTung"].ToString() == phuTungMoi)["SoLuongNhap"]) == 6 && Convert.ToInt32(chiTiet.AsEnumerable().Single(d => d["MaPhuTung"].ToString() == "PT001")["SoLuongNhap"]) == 2, "Cập nhật sau sắp xếp không sửa nhầm dòng");
            luoiNhap.CurrentCell = luoiNhap.Rows[0].Cells["MaPhuTung"];
            Lay<Button>(trang, "btnXoaPhuTung").PerformClick();
            Dung(chiTiet.Rows.Count == 1 && chiTiet.Rows[0]["MaPhuTung"].ToString() == "PT001", "Bỏ dòng sau sắp xếp không xóa nhầm phụ tùng");
            Lay<TextBox>(trang, "txtSoLuongNhap").Text = "5";
            Lay<Button>(trang, "btnThemPhuTung").PerformClick();
            chiTietDaLuu = chiTiet.Copy();
            Lay<RichTextBox>(trang, "txtGhiChu").Text = "Phiếu kiểm thử, chỉ nằm trong bản sao";
            Anh(trang, "lap-phieu-nhap");
            Lay<Button>(trang, "btnLuuPhieu").PerformClick();
            Dung(trang.DialogResult == DialogResult.OK, "Nút ghi sổ lưu thành công phiếu nhiều phụ tùng");
        }
        Dung(Ton("PT001") == tonCu + 2 && Ton(phuTungMoi) == 5, "Cộng tồn chính xác cho phụ tùng đã có và phụ tùng mới");
        Dung(Convert.ToDecimal(GiaTri("SELECT TongTienNhap FROM dbo.PhieuNhap WHERE MaPhieuNhap=@ma", XuLyDuLieu.ThamSo("@ma", phieuNhap))) == 2 * (giaNhapCu + 1000) + 50000 && Convert.ToInt32(GiaTri("SELECT COUNT(*) FROM dbo.ChiTietPhieuNhap WHERE MaPhieuNhap=@ma", XuLyDuLieu.ThamSo("@ma", phieuNhap))) == 2, "Tổng tiền và khóa ghép chi tiết phiếu nhập khớp dữ liệu");
        Dung(Convert.ToDateTime(GiaTri("SELECT HanSuDung FROM dbo.PhuTung WHERE MaPhuTung=@ma", XuLyDuLieu.ThamSo("@ma", phuTungMoi))).Year == 9999, "Phụ tùng không hạn vẫn có ngày hợp lệ, không NULL");

        string phieuLoi = XuLyDuLieu.TaoMaMoi("PN");
        PhaiBaoLoi(() => QuanLyKho.GhiSo(phieuLoi, "", chiTietDaLuu), typeof(InvalidOperationException));
        Dung(KhongCoPhieu(phieuLoi) && Ton("PT001") == tonCu + 2 && Ton(phuTungMoi) == 5, "Mã phụ tùng mới bị chiếm hoàn tác cả phiếu và tồn dòng trước");
        var saiLienKet = chiTietDaLuu.Copy();
        saiLienKet.Rows[0]["GiaNhapGoc"] = GiaTri("SELECT DonGiaNhap FROM dbo.PhuTung WHERE MaPhuTung='PT001'");
        saiLienKet.Rows[0]["GiaBanGoc"] = GiaTri("SELECT DonGiaBan FROM dbo.PhuTung WHERE MaPhuTung='PT001'");
        string maPhuTungLoi = XuLyDuLieu.TaoMaMoi("PT");
        saiLienKet.Rows[1]["MaPhuTung"] = maPhuTungLoi;
        saiLienKet.Rows[1]["MaNhaCungCap"] = "NCC999999";
        PhaiBaoLoi(() => QuanLyKho.GhiSo(phieuLoi, "", saiLienKet), typeof(SqlException));
        Dung(KhongCoPhieu(phieuLoi) && Ton("PT001") == tonCu + 2 && Convert.ToInt32(GiaTri("SELECT COUNT(*) FROM dbo.PhuTung WHERE MaPhuTung=@ma", XuLyDuLieu.ThamSo("@ma", maPhuTungLoi))) == 0, "Lỗi nhà cung cấp hoàn tác đầu phiếu, dòng cũ và phụ tùng mới");
        var saiLoHang = chiTietDaLuu.Clone();
        saiLoHang.ImportRow(chiTietDaLuu.Rows[0]);
        saiLoHang.Rows[0]["MaNhaCungCap"] = nhaCungCap;
        PhaiBaoLoi(() => QuanLyKho.GhiSo(phieuLoi, "", saiLoHang), typeof(InvalidOperationException));
        Dung(KhongCoPhieu(phieuLoi) && Ton("PT001") == tonCu + 2, "Không đổi nhà cung cấp của mã phụ tùng cũ trong lúc nhập");
        var giaKhongHopLe = chiTietDaLuu.Clone();
        giaKhongHopLe.ImportRow(chiTietDaLuu.Rows[0]);
        giaKhongHopLe.Rows[0]["DonGiaNhap"] = 0m;
        PhaiBaoLoi(() => QuanLyKho.GhiSo(phieuLoi, "", giaKhongHopLe), typeof(InvalidOperationException));
        Dung(KhongCoPhieu(phieuLoi), "Giá nhập bằng 0 bị từ chối trước khi ghi SQL");
        var hetHan = chiTietDaLuu.Clone();
        hetHan.ImportRow(chiTietDaLuu.Rows[0]);
        hetHan.Rows[0]["HanSuDung"] = DateTime.Today.AddDays(-1);
        PhaiBaoLoi(() => QuanLyKho.GhiSo(phieuLoi, "", hetHan), typeof(InvalidOperationException));
        Dung(KhongCoPhieu(phieuLoi), "Phụ tùng hết hạn không được nhập kho");

        using (var trang = new btl.frmKho())
        {
            Hien(trang);
            Lay<TextBox>(trang, "txtTimPhuTung").Text = phuTungMoi;
            Application.DoEvents();
            var bang = Lay<DataGridView>(trang, "dgvPhuTung");
            Dung(bang.Rows.Count == 1 && GiaoDien.GiaTriO(bang, "MaPhuTung") == phuTungMoi && !bang.Columns.Contains("DonViTinh"), "Tìm phụ tùng vừa nhập và không hiện đơn vị đã bỏ");
            Lay<Button>(trang, "btnNgungSuDung").PerformClick();
            Dung(GiaTri("SELECT TrangThai FROM dbo.PhuTung WHERE MaPhuTung=@ma", XuLyDuLieu.ThamSo("@ma", phuTungMoi)).ToString() == "Ngừng kinh doanh", "Nút ngừng kinh doanh đổi trạng thái, không xóa phụ tùng");
            Lay<Button>(trang, "btnNgungSuDung").PerformClick();
            Dung(GiaTri("SELECT TrangThai FROM dbo.PhuTung WHERE MaPhuTung=@ma", XuLyDuLieu.ThamSo("@ma", phuTungMoi)).ToString() == "Đang kinh doanh", "Bật kinh doanh lại giữ nguyên tồn và lịch sử");
            Anh(trang, "kho-phu-tung");
        }
        using (var trang = new btl.frmPhieuNhap())
        {
            Hien(trang);
            Lay<TextBox>(trang, "txtTimPhieuNhap").Text = phieuNhap;
            Application.DoEvents();
            var danhSach = Lay<DataGridView>(trang, "dgvPhieuNhap");
            var chiTiet = Lay<DataGridView>(trang, "dgvChiTietPhieuNhap");
            Dung(danhSach.Rows.Count == 1 && chiTiet.Rows.Count == 2 && danhSach.ReadOnly && chiTiet.ReadOnly, "Danh sách và chi tiết phiếu đã ghi sổ cùng trang, chỉ đọc");
            bool daMoXemTruoc = false;
            trang.BeginInvoke(new Action(() =>
            {
                var xem = Application.OpenForms.OfType<frmXemBanInPhieu>().FirstOrDefault();
                if (xem == null) return;
                daMoXemTruoc = Lay<PictureBox>(xem, "picPhieu").Image != null;
                Anh(xem, "xem-truoc-phieu-nhap");
                Lay<Button>(xem, "btnDong").PerformClick();
            }));
            Lay<Button>(trang, "btnInPhieuNhap").PerformClick();
            Dung(daMoXemTruoc, "Nút in phiếu nhập mở ngay bản xem trước, không chọn máy in");
            string tepExcel = Path.GetFullPath("checks/output/sql-moi/chi-tiet-phieu-nhap.xlsx");
            XuatExcel.GhiTep((DataTable)chiTiet.DataSource, tepExcel, "Chi tiết phiếu nhập");
            using (var nen = ZipFile.OpenRead(tepExcel))
            using (var doc = new StreamReader(nen.GetEntry("xl/worksheets/sheet1.xml").Open()))
            {
                string noiDung = doc.ReadToEnd();
                Dung(noiDung.Contains(phuTungMoi) && noiDung.Contains("50000") && noiDung.Contains("Phụ tùng kiểm thử SQL mới"), "Xuất Excel chứa đủ dữ liệu chi tiết đang chọn");
            }
            Lay<TextBox>(trang, "txtTimPhieuNhap").Text = lichSu["MaPhieuNhap"].ToString();
            Application.DoEvents();
            var dongLichSu = ((DataTable)chiTiet.DataSource).AsEnumerable().First(d => d["MaPhuTung"].ToString() == "PT001");
            Dung(Convert.ToDecimal(dongLichSu["DonGiaNhap"]) == giaLichSu, "Giá nhập phiếu cũ giữ nguyên sau khi nhập hàng với giá mới");
            Anh(trang, "phieu-nhap-kho");
        }
        Console.WriteLine("PASS KHO: " + soKiemTra + " kiểm tra SQL mới");
        return soKiemTra;
    }
}
