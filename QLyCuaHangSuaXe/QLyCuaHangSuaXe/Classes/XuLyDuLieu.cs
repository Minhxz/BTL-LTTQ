using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;

namespace QLyCuaHangSuaXe.LopDungChung
{
    public class XuLyDuLieu
    {
        private static string chuoiKetNoiLocalDbDaTim;
        public static string ChuoiKetNoi
        {
            get
            {
                var cauHinhKetNoi = ConfigurationManager.ConnectionStrings["QuanLySuaXe"];
                return cauHinhKetNoi == null ? "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=QuanLySuaXe;Integrated Security=True;Connect Timeout=5;TrustServerCertificate=True" : cauHinhKetNoi.ConnectionString;
            }
        }

        public static SqlParameter ThamSo(string ten, object giaTri)
        {
            return new SqlParameter(ten, giaTri ?? DBNull.Value);
        }

        public static string TaoMaMoi(string tienTo)
        {
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime) return tienTo + "001";
            using (var ketNoi = MoKetNoi()) return TaoMaMoi(ketNoi, null, tienTo);
        }

        public static string TaoMaMoi(SqlConnection ketNoi, SqlTransaction giaoDich, string tienTo)
        {
            string bang, cot;
            switch (tienTo)
            {
                case "DV": bang="DichVu"; cot="MaDichVu"; break;
                case "PT": bang="PhuTung"; cot="MaPhuTung"; break;
                case "PN": bang="PhieuNhap"; cot="MaPhieuNhap"; break;
                case "NC": tienTo="NCC"; bang="NhaCungCap"; cot="MaNhaCungCap"; break;
                case "NCC": bang="NhaCungCap"; cot="MaNhaCungCap"; break;
                case "NV": bang="NhanVien"; cot="MaNhanVien"; break;
                case "KH": bang="KhachHang"; cot="MaKhachHang"; break;
                case "XE": bang="Xe"; cot="MaXe"; break;
                case "TN": tienTo="PTN"; bang="PhieuTiepNhan"; cot="MaPhieuTiepNhan"; break;
                case "PTN": bang="PhieuTiepNhan"; cot="MaPhieuTiepNhan"; break;
                case "HD": bang="HoaDon"; cot="MaHoaDon"; break;
                default: throw new ArgumentException("Loại mã không hợp lệ.");
            }
            string khoa = giaoDich == null ? "" : " WITH(UPDLOCK,HOLDLOCK)";
            int viTri = tienTo.Length + 1;
            var ketQua = DocDuLieu(ketNoi, giaoDich, "SELECT ISNULL(MAX(TRY_CONVERT(INT,SUBSTRING(["+cot+"],@v,10))),0)+1 FROM dbo.["+bang+"]"+khoa+" WHERE ["+cot+"] LIKE @p AND SUBSTRING(["+cot+"],@v,10) NOT LIKE '%[^0-9]%'", ThamSo("@p", tienTo+"%"), ThamSo("@v", viTri));
            string ma = tienTo + Convert.ToInt32(ketQua.Rows[0][0]).ToString("D3");
            if (ma.Length > 10) throw new InvalidOperationException("Đã hết dải mã. Hãy kiểm tra dữ liệu trước khi tạo mới.");
            return ma;
        }

        public static string KiemTraMa(string ma, string tienTo)
        {
            if (tienTo == "NC") tienTo = "NCC";
            if (tienTo == "TN") tienTo = "PTN";
            if (string.IsNullOrEmpty(ma) || ma.Length > 10 || !Regex.IsMatch(ma, "^" + Regex.Escape(tienTo) + "[0-9]{3,8}$"))
                throw new InvalidOperationException("Mã không hợp lệ. Hãy bấm tạo mới để cấp lại mã.");
            return ma;
        }

        public DataTable DocBang(string cauLenhSql, params SqlParameter[] cacThamSo)
        {
            using (var ketNoi = MoKetNoi())
                return DocDuLieu(ketNoi, null, cauLenhSql, cacThamSo);
        }

        public int CapNhatDuLieu(string cauLenhSql, params SqlParameter[] cacThamSo)
        {
            using (var ketNoi = MoKetNoi())
            {
                return ThucThi(ketNoi, null, cauLenhSql, cacThamSo);
            }
        }

        public static DataTable DocDuLieu(SqlConnection ketNoi, SqlTransaction giaoDich, string cauLenhSql, params SqlParameter[] cacThamSo)
        {
            using (var lenhSql = new SqlCommand(cauLenhSql, ketNoi, giaoDich))
            using (var boNapDuLieu = new SqlDataAdapter(lenhSql))
            {
                lenhSql.Parameters.AddRange(cacThamSo);
                var ketQua = new DataTable();
                boNapDuLieu.Fill(ketQua);
                return ketQua;
            }
        }

        public static int ThucThi(SqlConnection ketNoi, SqlTransaction giaoDich, string cauLenhSql, params SqlParameter[] cacThamSo)
        {
            using (var lenhSql = new SqlCommand(cauLenhSql, ketNoi, giaoDich))
            {
                lenhSql.Parameters.AddRange(cacThamSo);
                return lenhSql.ExecuteNonQuery();
            }
        }

        public T TrongGiaoDich<T>(Func<SqlConnection, SqlTransaction, T> thaoTac)
        {
            using (var ketNoi = MoKetNoi())
            {
                using (var giaoDich = ketNoi.BeginTransaction(IsolationLevel.Serializable))
                {
                    try
                    {
                        T ketQua = thaoTac(ketNoi, giaoDich);
                        giaoDich.Commit();
                        return ketQua;
                    }
                    catch
                    {
                        try
                        {
                            if (giaoDich.Connection != null)
                                giaoDich.Rollback();
                        }
                        catch (SqlException)
                        {
                        }
                        catch (InvalidOperationException)
                        {
                        }

                        throw;
                    }
                }
            }
        }

        public static SqlConnection MoKetNoi()
        {
            string daLuu = chuoiKetNoiLocalDbDaTim;
            if (daLuu != null)
            {
                try
                {
                    return KetNoi(daLuu);
                }
                catch (SqlException)
                {
                    chuoiKetNoiLocalDbDaTim = null;
                }
            }

            string duongOngHienTai = TimDuongOngLocalDb(ChuoiKetNoi);
            if (duongOngHienTai != null)
            {
                try
                {
                    var ketNoiHienTai = KetNoi(duongOngHienTai);
                    chuoiKetNoiLocalDbDaTim = duongOngHienTai;
                    return ketNoiHienTai;
                }
                catch (SqlException) { }
            }

            try
            {
                return KetNoi(ChuoiKetNoi);
            }
            catch (SqlException)
            {
                string duPhong = TimDuongOngLocalDb(ChuoiKetNoi);
                if (duPhong == null)
                    throw;
                var ketNoi = KetNoi(duPhong);
                chuoiKetNoiLocalDbDaTim = duPhong;
                return ketNoi;
            }
        }

        static SqlConnection KetNoi(string chuoiKetNoi)
        {
            var ketNoi = new SqlConnection(chuoiKetNoi);
            try
            {
                ketNoi.Open();
                return ketNoi;
            }
            catch
            {
                ketNoi.Dispose();
                throw;
            }
        }

        static string TimDuongOngLocalDb(string chuoiKetNoi)
        {
            var boTaoChuoi = new SqlConnectionStringBuilder(chuoiKetNoi);
            var ketQuaTim = Regex.Match(boTaoChuoi.DataSource, @"^\(localdb\)\\([A-Za-z0-9_]+)$", RegexOptions.IgnoreCase);
            if (!ketQuaTim.Success)
                return null;
            string duongDan = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Microsoft", "Microsoft SQL Server Local DB", "Instances", ketQuaTim.Groups[1].Value, "error.log");
            try
            {
                string nhatKy;
                using (var luongTep = new FileStream(duongDan, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (var boDoc = new StreamReader(luongTep))
                    nhatKy = boDoc.ReadToEnd();
                var cacDuongOng = Regex.Matches(nhatKy, @"\\\\\.\\pipe\\LOCALDB#[A-Fa-f0-9]+\\tsql\\query");
                if (cacDuongOng.Count == 0)
                    return null;
                boTaoChuoi.DataSource = "np:" + cacDuongOng[cacDuongOng.Count - 1].Value;
                return boTaoChuoi.ConnectionString;
            }
            catch (IOException)
            {
                return null;
            }
            catch (UnauthorizedAccessException)
            {
                return null;
            }
        }
    }
}
