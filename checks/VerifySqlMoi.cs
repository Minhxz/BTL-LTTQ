using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using QLyCuaHangSuaXe.LopDungChung;

class KiemTraSqlMoi
{
    [STAThread]
    static int Main()
    {
        try
        {
            if (new SqlConnectionStringBuilder(XuLyDuLieu.ChuoiKetNoi).InitialCatalog != "QuanLySuaXe_CodexChecks")
                throw new InvalidOperationException("Chỉ được chạy trên database kiểm thử riêng.");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var duLieu = new XuLyDuLieu();
            if (Convert.ToInt32(duLieu.DocBang("SELECT COUNT(*) FROM sys.tables").Rows[0][0]) != 18)
                throw new Exception("Ứng dụng chưa dùng cấu trúc SQL mới.");
            if (Convert.ToInt32(duLieu.DocBang("SELECT COUNT(*) FROM sys.columns c JOIN sys.tables t ON t.object_id=c.object_id WHERE c.is_nullable=1").Rows[0][0]) != 0)
                throw new Exception("Còn cột cho phép NULL.");
            int so = KiemTraKho.Chay();
            so += KiemTraNhanSu.Chay();
            so += KiemTraTiepNhan.Chay();
            Console.WriteLine("PASS: " + so + " kiểm tra chức năng trên SQL mới.");
            return 0;
        }
        catch (Exception loi)
        {
            Console.Error.WriteLine(loi);
            return 1;
        }
    }
}
