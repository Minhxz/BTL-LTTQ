using System;
using System.Data;
using System.Data.SqlClient;

namespace QLyCuaHangSuaXe.DanhMuc.ThongKe
{
    // Lớp lấy dữ liệu cho form thống kê.
    // Form chỉ gọi 2 hàm bên dưới và nhận về DataTable với cột CỐ ĐỊNH,
    // nên khi đổi từ dữ liệu mẫu sang CSDL thật thì KHÔNG phải sửa form.
    public static class ThongKeDAL
    {
        // true  = dùng dữ liệu mẫu (chưa cần CSDL)
        // false = chạy SQL thật bên dưới
        public static bool DungDuLieuMau = true;

        // TODO: thay bằng chuỗi kết nối thật của nhóm (hoặc dùng hàm kết nối có sẵn trong DataProcessor.cs)
        const string ChuoiKetNoi =
            @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=QuanLySuaXe;Integrated Security=True";

        // ---------------------------------------------------------------------
        //  Cột trả về: MaPhieu, Ngay, KhachHang, ThoPhuTrach, TienDichVu, TienPhuTung
        // ---------------------------------------------------------------------
        public static DataTable LayDoanhThu(DateTime tu, DateTime den)
        {
            if (DungDuLieuMau) return MauDoanhThu(tu, den);

            // TODO: TÊN BẢNG VÀ TÊN CỘT DƯỚI ĐÂY LÀ GIẢ ĐỊNH.
            // Sửa lại cho khớp CSDL của nhóm sau khi có cấu trúc bảng.
            string sql = @"
                SELECT  p.MaPhieu,
                        p.NgayHoanThanh          AS Ngay,
                        p.TenKhach               AS KhachHang,
                        p.TenTho                 AS ThoPhuTrach,
                        ISNULL(p.TienDichVu, 0)  AS TienDichVu,
                        ISNULL(p.TienPhuTung, 0) AS TienPhuTung
                FROM    PHIEUTIEPNHAN p
                WHERE   p.TrangThai = N'Hoàn thành'
                  AND   p.NgayHoanThanh >= @tu
                  AND   p.NgayHoanThanh <  @den
                ORDER BY p.NgayHoanThanh DESC";

            return DocBang(sql,
                new SqlParameter("@tu", tu.Date),
                new SqlParameter("@den", den.Date.AddDays(1)));   // < ngày kế tiếp = gồm cả ngày kết thúc
        }

        // ---------------------------------------------------------------------
        //  Cột trả về: MaPT, TenPT, TonKho (trả TẤT CẢ phụ tùng, form tự lọc theo mức cảnh báo)
        // ---------------------------------------------------------------------
        public static DataTable LayTonKho()
        {
            if (DungDuLieuMau) return MauTonKho();

            // TODO: TÊN BẢNG VÀ TÊN CỘT DƯỚI ĐÂY LÀ GIẢ ĐỊNH.
            string sql = @"
                SELECT  MaPT,
                        TenPT,
                        ISNULL(SoLuongTon, 0) AS TonKho
                FROM    PHUTUNG
                ORDER BY SoLuongTon";

            return DocBang(sql);
        }

        // ---------------------------------------------------------------------
        //  Hàm chung: chạy SQL và trả về DataTable
        // ---------------------------------------------------------------------
        static DataTable DocBang(string sql, params SqlParameter[] ps)
        {
            var dt = new DataTable();
            using (var cn = new SqlConnection(ChuoiKetNoi))
            using (var cmd = new SqlCommand(sql, cn))
            {
                if (ps != null) cmd.Parameters.AddRange(ps);
                using (var da = new SqlDataAdapter(cmd)) da.Fill(dt);   // Fill tự mở và đóng kết nối
            }
            return dt;
        }

        // =====================================================================
        //  DỮ LIỆU MẪU (xóa khi đã nối CSDL)
        // =====================================================================
        static DataTable MauDoanhThu(DateTime tu, DateTime den)
        {
            var dt = new DataTable();
            dt.Columns.Add("MaPhieu", typeof(string));
            dt.Columns.Add("Ngay", typeof(DateTime));
            dt.Columns.Add("KhachHang", typeof(string));
            dt.Columns.Add("ThoPhuTrach", typeof(string));
            dt.Columns.Add("TienDichVu", typeof(decimal));
            dt.Columns.Add("TienPhuTung", typeof(decimal));

            // { mã, số ngày lùi so với hôm nay, khách, thợ, tiền dịch vụ, tiền phụ tùng }
            object[][] mau = new object[][]
            {
                new object[]{"TN020", 0,  "Nguyễn Hải An",   "Phạm Văn Nam", 300000, 850000},
                new object[]{"TN019", 0,  "Trần Ngọc Mai",   "Lê Minh Tuấn", 150000, 0},
                new object[]{"TN018", 1,  "Lê Hoàng Phúc",   "Phạm Văn Nam", 200000, 420000},
                new object[]{"TN017", 2,  "Vũ Thị Hoa",      "Lê Minh Tuấn", 250000, 600000},
                new object[]{"TN016", 2,  "Đặng Quốc Bảo",   "Phạm Văn Nam", 120000, 90000},
                new object[]{"TN015", 4,  "Bùi Văn Long",    "Trần Đức Anh", 400000, 1200000},
                new object[]{"TN014", 5,  "Hoàng Thu Trang", "Lê Minh Tuấn", 180000, 350000},
                new object[]{"TN013", 7,  "Phan Gia Huy",    "Trần Đức Anh", 300000, 780000},
                new object[]{"TN012", 9,  "Ngô Thanh Sơn",   "Phạm Văn Nam", 150000, 0},
                new object[]{"TN011", 12, "Đỗ Minh Châu",    "Trần Đức Anh", 500000, 1500000},
                new object[]{"TN010", 15, "Lý Văn Khải",     "Lê Minh Tuấn", 220000, 310000},
                new object[]{"TN009", 19, "Mai Anh Tuấn",    "Phạm Văn Nam", 350000, 950000},
                new object[]{"TN008", 24, "Cao Thị Lan",     "Trần Đức Anh", 130000, 200000},
                new object[]{"TN007", 29, "Đinh Công Hậu",   "Lê Minh Tuấn", 270000, 540000},
                new object[]{"TN006", 33, "Tạ Thu Hà",       "Phạm Văn Nam", 160000, 0},
                new object[]{"TN005", 36, "Chu Văn Thắng",   "Trần Đức Anh", 380000, 1100000},
                new object[]{"TN004", 40, "Kiều Mỹ Linh",    "Lê Minh Tuấn", 210000, 280000},
                new object[]{"TN003", 44, "Lương Quốc Việt", "Phạm Văn Nam", 330000, 700000},
                new object[]{"TN002", 48, "Hồ Ngọc Ánh",     "Trần Đức Anh", 140000, 120000},
                new object[]{"TN001", 52, "Trịnh Bá Dũng",   "Lê Minh Tuấn", 290000, 640000},
            };

            foreach (object[] r in mau)
            {
                DateTime ngay = DateTime.Today.AddDays(-(int)r[1]);
                if (ngay < tu.Date || ngay > den.Date) continue;
                dt.Rows.Add(r[0], ngay, r[2], r[3], Convert.ToDecimal(r[4]), Convert.ToDecimal(r[5]));
            }
            return dt;
        }

        static DataTable MauTonKho()
        {
            var dt = new DataTable();
            dt.Columns.Add("MaPT", typeof(string));
            dt.Columns.Add("TenPT", typeof(string));
            dt.Columns.Add("TonKho", typeof(int));

            dt.Rows.Add("PT001", "Nhớt Castrol 10W40", 2);
            dt.Rows.Add("PT002", "Lọc gió Exciter", 0);
            dt.Rows.Add("PT003", "Má phanh trước", 4);
            dt.Rows.Add("PT004", "Bugi NGK", 5);
            dt.Rows.Add("PT005", "Lốp Michelin 80/90", 12);
            dt.Rows.Add("PT006", "Xích tải DID", 20);
            dt.Rows.Add("PT007", "Dây ga", 9);
            dt.Rows.Add("PT008", "Ắc quy GS", 3);
            dt.Rows.Add("PT009", "Lọc nhớt", 25);
            dt.Rows.Add("PT010", "Nhông sên dĩa", 1);
            return dt;
        }
    }
}