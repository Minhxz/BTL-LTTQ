using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using QLyCuaHangSuaXe.DanhMuc.TiepNhan;
using QLyCuaHangSuaXe.LopDungChung;

public static class KiemTraChonPhieu
{
    static readonly BindingFlags co = BindingFlags.Instance | BindingFlags.NonPublic;
    static T Lay<T>(object trang, string ten) { return (T)trang.GetType().GetField(ten, co).GetValue(trang); }
    static int so;
    static void Dung(bool dung, string ten)
    {
        if (!dung) throw new InvalidOperationException(ten);
        so++; Console.WriteLine("PASS chọn phiếu: " + ten);
    }
    static void DoiChieu(frmTiepNhan trang, DataGridView bang)
    {
        var dong = ((DataRowView)bang.CurrentRow.DataBoundItem).Row;
        string ma = dong["MaPhieuTiepNhan"].ToString();
        Dung(Lay<TextBox>(trang, "txtMaPhieu").Text == ma && Lay<string>(trang, "maPhieuDangChon") == ma, "Mã hiển thị và mã nghiệp vụ khớp CurrentRow " + ma);
        Dung(Lay<ComboBox>(trang, "cboTrangThai").Text == dong["TrangThai"].ToString() && Lay<TextBox>(trang, "txtBienSoXe").Text == dong["BienSo"].ToString(), "Trạng thái và biển số khớp CurrentRow " + ma);
    }
    [STAThread]
    public static int Main()
    {
        try
        {
            var duLieu = new XuLyDuLieu();
            if (new SqlConnectionStringBuilder(XuLyDuLieu.ChuoiKetNoi).InitialCatalog != "QuanLySuaXe_CodexChecks" || duLieu.DocBang("SELECT DB_NAME()").Rows[0][0].ToString() != "QuanLySuaXe_CodexChecks") throw new InvalidOperationException("Chỉ kiểm tra trên bản sao QuanLySuaXe_CodexChecks.");
            if (!PhienDangNhap.DangNhap("minh", "123")) throw new InvalidOperationException("Không đăng nhập được tài khoản kiểm thử.");
            Application.EnableVisualStyles(); Application.SetCompatibleTextRenderingDefault(false);
            using (var trang = new frmTiepNhan())
            {
                trang.StartPosition = FormStartPosition.Manual; trang.Location = new Point(-4000, -4000);
                trang.Show(); Application.DoEvents();
                var bang = Lay<DataGridView>(trang, "dgvPhieu");
                var xong = bang.Rows.Cast<DataGridViewRow>().First(d => d.Cells["TrangThai"].Value.ToString() == "Hoàn thành");
                var cho = bang.Rows.Cast<DataGridViewRow>().First(d => d.Cells["TrangThai"].Value.ToString() == "Chờ sửa" && ((DataRowView)d.DataBoundItem)["MaTho"].ToString().Length > 0);
                foreach (var dong in new[] { xong, cho, xong, cho })
                {
                    bang.CurrentCell = dong.Cells["MaPhieuTiepNhan"];
                    DoiChieu(trang, bang);
                }
                string maSua = cho.Cells["MaPhieuTiepNhan"].Value.ToString();
                Lay<Button>(trang, "btnSua").PerformClick();
                Dung(Lay<Button>(trang, "btnLuu").Enabled && !bang.Enabled, "Bắt đầu sửa khóa đổi dòng để tránh lưu nhầm phiếu/thợ");
                Lay<Button>(trang, "btnLuu").PerformClick();
                Dung(bang.Enabled && !Lay<Button>(trang, "btnLuu").Enabled, "Lưu xong mở lại danh sách và kết thúc chế độ sửa");
                Dung(Lay<TextBox>(trang, "txtMaPhieu").Text == maSua && Lay<Label>(trang, "lblTrangThaiPhieu").Text.Contains(maSua), "Lưu đúng phiếu vừa chọn và giữ phiếu đó sau tải lại");
                DoiChieu(trang, bang);
                trang.Close();
            }
            Console.WriteLine("PASS: " + so + " kiểm tra hồi quy chọn/sửa phiếu."); return 0;
        }
        catch (Exception loi) { Console.Error.WriteLine(loi); return 1; }
    }
}
