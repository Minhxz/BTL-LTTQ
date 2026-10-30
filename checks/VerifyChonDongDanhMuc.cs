using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using QLyCuaHangSuaXe.DanhMuc;
using QLyCuaHangSuaXe.DanhMuc.DangNhap;
using QLyCuaHangSuaXe.DanhMuc.DichVu;
using QLyCuaHangSuaXe.LopDungChung;

public static class KiemTraChonDongDanhMuc
{
    static int dung, sai;
    static T Lay<T>(object trang, string ten)
    {
        for (Type kieu = trang.GetType(); kieu != null; kieu = kieu.BaseType)
        {
            var truong = kieu.GetField(ten, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.DeclaredOnly);
            if (truong != null) return (T)truong.GetValue(trang);
        }
        throw new Exception("Không tìm thấy " + ten);
    }
    static void KiemTra(Form trang, string tenBang, Func<DataRow, bool> doiChieu)
    {
        using (trang)
        {
            trang.StartPosition = FormStartPosition.Manual; trang.Location = new Point(-4000, -4000);
            trang.Show(); Application.DoEvents();
            var bang = Lay<DataGridView>(trang, tenBang);
            if (bang.Rows.Count < 2) throw new Exception("Cần ít nhất hai dòng để kiểm tra " + trang.GetType().Name);
            int[] viTri = { 0, bang.Rows.Count - 1, 0, 1 };
            foreach (int chiSo in viTri)
            {
                var o = bang.Rows[chiSo].Cells.Cast<DataGridViewCell>().First(c => c.Visible);
                bang.CurrentCell = o;
                var dong = ((DataRowView)bang.CurrentRow.DataBoundItem).Row;
                bool khop = doiChieu(dong);
                if (khop) dung++; else sai++;
                Console.WriteLine((khop ? "PASS" : "FAIL") + " chọn dòng " + trang.GetType().Name + " row " + chiSo);
            }
        }
    }
    [STAThread]
    public static int Main()
    {
        try
        {
            var duLieu = new XuLyDuLieu();
            if (new SqlConnectionStringBuilder(XuLyDuLieu.ChuoiKetNoi).InitialCatalog != "QuanLySuaXe_CodexChecks" || duLieu.DocBang("SELECT DB_NAME()").Rows[0][0].ToString() != "QuanLySuaXe_CodexChecks") throw new Exception("Chỉ kiểm tra trên bản sao QuanLySuaXe_CodexChecks.");
            if (!PhienDangNhap.DangNhap("minh", "123")) throw new Exception("Không đăng nhập được.");
            Application.EnableVisualStyles(); Application.SetCompatibleTextRenderingDefault(false);
            var nhanVien = new frmNhanVien();
            KiemTra(nhanVien, "dgvNhanVien", d => Lay<TextBox>(nhanVien, "txtMaNhanVien").Text == d["MaNhanVien"].ToString() && Lay<TextBox>(nhanVien, "txtHoTen").Text == d["TenNhanVien"].ToString());
            var taiKhoan = new frmQuanLyTaiKhoan();
            KiemTra(taiKhoan, "dgvTaiKhoan", d => Lay<TextBox>(taiKhoan, "txtMaTaiKhoan").Text == d["MaNhanVien"].ToString() && Lay<TextBox>(taiKhoan, "txtTenDangNhap").Text == d["TenDangNhap"].ToString());
            var dichVu = new frmDanhMucDichVu();
            KiemTra(dichVu, "dgvDichVu", d => Lay<TextBox>(dichVu, "txtMaDV").Text == d["MaDichVu"].ToString());
            var kho = new btl.frmKho();
            KiemTra(kho, "dgvPhuTung", d => Lay<GroupBox>(kho, "grbDieuChinh").Text.StartsWith("Điều chỉnh " + d["MaPhuTung"] + " —") && Lay<NumericUpDown>(kho, "numGiaBanMoi").Value == Convert.ToDecimal(d["DonGiaBan"]));
            var phanCong = new frmPhanCong();
            KiemTra(phanCong, "dgvPhanCong", d => Lay<TextBox>(phanCong, "txtMaPhieu").Text == d["MaPhieuTiepNhan"].ToString());
            var cong = new frmBangCong();
            KiemTra(cong, "dgvBangCong", d => Lay<Label>(cong, "lblCachTinh").Text.StartsWith(d["TenNhanVien"] + "  •  "));
            var phieu = new btl.frmPhieuNhap();
            KiemTra(phieu, "dgvPhieuNhap", d =>
            {
                var hien = (DataTable)Lay<DataGridView>(phieu, "dgvChiTietPhieuNhap").DataSource;
                var cho = duLieu.DocBang("SELECT MaPhuTung,SoLuongNhap,ThanhTien FROM dbo.ChiTietPhieuNhap WHERE MaPhieuNhap=@ma ORDER BY MaPhuTung", XuLyDuLieu.ThamSo("@ma", d["MaPhieuNhap"]));
                return hien.Rows.Count == cho.Rows.Count && hien.AsEnumerable().Select(r => r["MaPhuTung"] + ":" + r["SoLuongNhap"] + ":" + r["ThanhTien"]).SequenceEqual(cho.AsEnumerable().Select(r => r["MaPhuTung"] + ":" + r["SoLuongNhap"] + ":" + r["ThanhTien"]));
            });
            Console.WriteLine("Kết quả chọn dòng: " + dung + " PASS; " + sai + " FAIL.");
            return sai == 0 ? 0 : 1;
        }
        catch (Exception loi) { Console.Error.WriteLine(loi); return 1; }
    }
}
