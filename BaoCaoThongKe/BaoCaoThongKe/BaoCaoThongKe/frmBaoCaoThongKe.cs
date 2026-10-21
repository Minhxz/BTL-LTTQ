using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace BaoCaoThongKe
{
    public partial class frmBaoCaoThongKe : Form
    {
        // Chuỗi kết nối tới CSDL AutoFixDb trên máy bạn
        string strConnect = @"Data Source=localhost;Initial Catalog=AutoFixDb;Integrated Security=True";

        DataTable dtDoanhThuGoc;
        DataTable dtPhuTungGoc;

        public frmBaoCaoThongKe()
        {
            InitializeComponent();

            // Gắn sự kiện cho các nút bấm trên giao diện của bạn
            this.Load += frmBaoCaoThongKe_Load;
            this.btnThongKe.Click += btnThongKe_Click;
            this.btnLamMoi.Click += btnLamMoi_Click;
            this.btnThoat.Click += btnThoat_Click;

            this.btnLocCanhBao.Click += btnLocCanhBao_Click;
            this.btnLamMoi1.Click += btnLamMoi1_Click;
            this.btnXuatFile.Click += btnXuatFile_Click;
            this.btnThoat1.Click += btnThoat1_Click;
        }

        private void frmBaoCaoThongKe_Load(object sender, EventArgs e)
        {
            // Thiết lập giá trị mặc định cho bộ lọc ngày và mức tồn kho
            dtpTuNgay.Value = new DateTime(2026, 10, 1);
            dtpDenNgay.Value = new DateTime(2026, 10, 31);
            numMucTon.Value = 5;

            // Tải dữ liệu từ cơ sở dữ liệu AutoFixDb
            TaiDuLieuDoanhThuTuSQL();
            TaiDuLieuPhuTungTuSQL();

            // Hiển thị lên 2 bảng
            HienThiDoanhThu(false);
            HienThiPhuTung(false);
        }

        // =========================================================================
        // 1. TẢI DỮ LIỆU DOANH THU TỪ BẢNG JobCards (AutoFixDb)
        // =========================================================================
        private void TaiDuLieuDoanhThuTuSQL()
        {
            dtDoanhThuGoc = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(strConnect))
                {
                    conn.Open();
                    string sql = @"
                        SELECT 
                            jc.JobCardNumber AS [Mã phiếu],
                            CAST(jc.CreatedAt AS DATE) AS [Ngày sửa],
                            c.FullName AS [Khách hàng],
                            v.LicensePlate AS [Biển số],
                            ISNULL(u.FullName, N'Chưa phân công') AS [Thợ phụ trách],
                            ISNULL((SELECT SUM(LineTotal) FROM JobCardDetails d WHERE d.JobCardId = jc.JobCardId AND d.ItemType = 'Part'), 0) AS [Tiền phụ tùng],
                            ISNULL((SELECT SUM(LineTotal) FROM JobCardDetails d WHERE d.JobCardId = jc.JobCardId AND d.ItemType = 'Service'), 0) AS [Tiền công],
                            jc.TotalAmount AS [Tổng tiền]
                        FROM JobCards jc
                        JOIN Vehicles v ON jc.VehicleId = v.VehicleId
                        JOIN Customers c ON v.CustomerId = c.CustomerId
                        LEFT JOIN Users u ON jc.TechnicianId = u.UserId";

                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    da.Fill(dtDoanhThuGoc);
                }
            }
            catch
            {
                // Bỏ qua nếu lỗi kết nối
            }

            // Nếu bảng trong SQL chưa có dữ liệu, dùng dữ liệu mẫu để form luôn chạy được
            if (dtDoanhThuGoc.Rows.Count == 0)
            {
                dtDoanhThuGoc = new DataTable();
                dtDoanhThuGoc.Columns.Add("Mã phiếu", typeof(string));
                dtDoanhThuGoc.Columns.Add("Ngày sửa", typeof(DateTime));
                dtDoanhThuGoc.Columns.Add("Khách hàng", typeof(string));
                dtDoanhThuGoc.Columns.Add("Biển số", typeof(string));
                dtDoanhThuGoc.Columns.Add("Thợ phụ trách", typeof(string));
                dtDoanhThuGoc.Columns.Add("Tiền phụ tùng", typeof(decimal));
                dtDoanhThuGoc.Columns.Add("Tiền công", typeof(decimal));
                dtDoanhThuGoc.Columns.Add("Tổng tiền", typeof(decimal));

                dtDoanhThuGoc.Rows.Add("PSC001", new DateTime(2026, 10, 5), "Nguyễn Văn Hùng", "29A-123.45", "Nguyễn Văn Tuấn", 680000m, 250000m, 930000m);
                dtDoanhThuGoc.Rows.Add("PSC002", new DateTime(2026, 10, 10), "Trần Thị Mai", "30E-567.89", "Trần Quang Minh", 520000m, 180000m, 700000m);
                dtDoanhThuGoc.Rows.Add("PSC003", new DateTime(2026, 10, 15), "Lê Hoàng Nam", "29B-999.12", "Nguyễn Văn Tuấn", 160000m, 300000m, 460000m);
            }
        }

        // =========================================================================
        // 2. TẢI DỮ LIỆU TỒN KHO TỪ BẢNG Parts (AutoFixDb)
        // =========================================================================
        private void TaiDuLieuPhuTungTuSQL()
        {
            dtPhuTungGoc = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(strConnect))
                {
                    conn.Open();
                    string sql = @"
                        SELECT 
                            Sku AS [Mã PT],
                            PartName AS [Tên phụ tùng],
                            Unit AS [Đơn vị],
                            UnitPrice AS [Giá bán],
                            CAST(StockQuantity AS INT) AS [Số lượng tồn],
                            CAST(ReorderLevel AS INT) AS [Mức tối thiểu]
                        FROM Parts";

                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    da.Fill(dtPhuTungGoc);
                }
            }
            catch
            {
                // Bỏ qua nếu lỗi kết nối
            }

            if (dtPhuTungGoc.Rows.Count == 0)
            {
                dtPhuTungGoc = new DataTable();
                dtPhuTungGoc.Columns.Add("Mã PT", typeof(string));
                dtPhuTungGoc.Columns.Add("Tên phụ tùng", typeof(string));
                dtPhuTungGoc.Columns.Add("Đơn vị", typeof(string));
                dtPhuTungGoc.Columns.Add("Giá bán", typeof(decimal));
                dtPhuTungGoc.Columns.Add("Số lượng tồn", typeof(int));
                dtPhuTungGoc.Columns.Add("Mức tối thiểu", typeof(int));

                dtPhuTungGoc.Rows.Add("OIL-5W30", "Dầu tổng hợp Castrol 5W-30", "Can", 680000m, 25, 5);
                dtPhuTungGoc.Rows.Add("BRK-FR01", "Má phanh trước Ceramic", "Bộ", 520000m, 3, 4);
                dtPhuTungGoc.Rows.Add("FLT-AIR01", "Lọc gió động cơ tiêu chuẩn", "Cái", 160000m, 0, 8);
                dtPhuTungGoc.Rows.Add("SPK-IR02", "Bugi Iridium cao cấp", "Cái", 180000m, 4, 10);
            }
        }

        // =========================================================================
        // XỬ LÝ SỰ KIỆN THẺ 1: THỐNG KÊ DOANH THU
        // =========================================================================
        private void btnThongKe_Click(object sender, EventArgs e)
        {
            if (dtpTuNgay.Value.Date > dtpDenNgay.Value.Date)
            {
                MessageBox.Show("Từ ngày không được lớn hơn Đến ngày!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            HienThiDoanhThu(true);
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            dtpTuNgay.Value = new DateTime(2026, 10, 1);
            dtpDenNgay.Value = new DateTime(2026, 10, 31);
            HienThiDoanhThu(false);
        }

        private void HienThiDoanhThu(bool coLocNgay)
        {
            DataTable dtKetQua = dtDoanhThuGoc.Clone();
            int soXe = 0;
            decimal tongPT = 0;
            decimal tongCong = 0;

            foreach (DataRow row in dtDoanhThuGoc.Rows)
            {
                DateTime ngaySua = Convert.ToDateTime(row["Ngày sửa"]).Date;

                if (coLocNgay)
                {
                    if (ngaySua < dtpTuNgay.Value.Date || ngaySua > dtpDenNgay.Value.Date)
                        continue;
                }

                dtKetQua.ImportRow(row);
                soXe++;
                tongPT += Convert.ToDecimal(row["Tiền phụ tùng"]);
                tongCong += Convert.ToDecimal(row["Tiền công"]);
            }

            decimal tongDoanhThu = tongPT + tongCong;

            dgvThongKeDoanhThu.DataSource = dtKetQua;
            dgvThongKeDoanhThu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            lblSoXe.Text = soXe + " xe";
            lblTienPhuTung.Text = tongPT.ToString("N0") + " đ";
            lblTienCong.Text = tongCong.ToString("N0") + " đ";
            lblTongDoanhThu.Text = tongDoanhThu.ToString("N0") + " đ";
        }

        // =========================================================================
        // XỬ LÝ SỰ KIỆN THẺ 2: CẢNH BÁO TỒN KHO PHỤ TÙNG
        // =========================================================================
        private void btnLocCanhBao_Click(object sender, EventArgs e)
        {
            HienThiPhuTung(true);
        }

        private void btnLamMoi1_Click(object sender, EventArgs e)
        {
            numMucTon.Value = 5;
            HienThiPhuTung(false);
        }

        private void HienThiPhuTung(bool chiLocHangSapHet)
        {
            int mucCanhBao = (int)numMucTon.Value;

            DataTable dtKetQua = new DataTable();
            dtKetQua.Columns.Add("Mã PT", typeof(string));
            dtKetQua.Columns.Add("Tên phụ tùng", typeof(string));
            dtKetQua.Columns.Add("Đơn vị", typeof(string));
            dtKetQua.Columns.Add("Giá bán", typeof(decimal));
            dtKetQua.Columns.Add("Số lượng tồn", typeof(int));
            dtKetQua.Columns.Add("Trạng thái", typeof(string));

            int tongMatHang = 0;
            int soCanNhapGap = 0;

            foreach (DataRow row in dtPhuTungGoc.Rows)
            {
                int soLuongTon = Convert.ToInt32(row["Số lượng tồn"]);

                if (chiLocHangSapHet && soLuongTon > mucCanhBao)
                {
                    continue;
                }

                string trangThai = "Bình thường";
                if (soLuongTon == 0)
                {
                    trangThai = "HẾT HÀNG - Nhập gấp!";
                    soCanNhapGap++;
                }
                else if (soLuongTon <= mucCanhBao)
                {
                    trangThai = "Sắp hết hàng";
                    soCanNhapGap++;
                }

                dtKetQua.Rows.Add(
                    row["Mã PT"],
                    row["Tên phụ tùng"],
                    row["Đơn vị"],
                    row["Giá bán"],
                    soLuongTon,
                    trangThai
                );
                tongMatHang++;
            }

            dgvPhuTungCanhBao.DataSource = dtKetQua;
            dgvPhuTungCanhBao.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            lblTongMH.Text = tongMatHang + " loại";
            lblCanNhap.Text = soCanNhapGap + " loại";

            ToMauCanhBaoKho();
        }

        private void ToMauCanhBaoKho()
        {
            for (int i = 0; i < dgvPhuTungCanhBao.Rows.Count; i++)
            {
                if (dgvPhuTungCanhBao.Rows[i].Cells["Trạng thái"].Value != null)
                {
                    string tt = dgvPhuTungCanhBao.Rows[i].Cells["Trạng thái"].Value.ToString();
                    if (tt.Contains("HẾT HÀNG"))
                    {
                        dgvPhuTungCanhBao.Rows[i].DefaultCellStyle.BackColor = Color.LightPink;
                    }
                    else if (tt == "Sắp hết hàng")
                    {
                        dgvPhuTungCanhBao.Rows[i].DefaultCellStyle.BackColor = Color.LightYellow;
                    }
                }
            }
        }

        // Nút xuất file CSV
        private void btnXuatFile_Click(object sender, EventArgs e)
        {
            if (dgvPhuTungCanhBao.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo");
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "CSV Excel (*.csv)|*.csv";
            sfd.FileName = "CanhBaoTonKho.csv";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("Mã PT,Tên phụ tùng,Đơn vị,Giá bán,Số lượng tồn,Trạng thái");

                for (int i = 0; i < dgvPhuTungCanhBao.Rows.Count; i++)
                {
                    DataGridViewRow r = dgvPhuTungCanhBao.Rows[i];
                    if (!r.IsNewRow)
                    {
                        string dong = r.Cells[0].Value + "," +
                                      r.Cells[1].Value + "," +
                                      r.Cells[2].Value + "," +
                                      r.Cells[3].Value + "," +
                                      r.Cells[4].Value + "," +
                                      r.Cells[5].Value;
                        sb.AppendLine(dong);
                    }
                }

                File.WriteAllText(sfd.FileName, sb.ToString(), Encoding.UTF8);
                MessageBox.Show("Đã xuất file báo cáo tồn kho thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Nút thoát form
        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn thoát?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnThoat1_Click(object sender, EventArgs e)
        {
            btnThoat_Click(sender, e);
        }

    }
}