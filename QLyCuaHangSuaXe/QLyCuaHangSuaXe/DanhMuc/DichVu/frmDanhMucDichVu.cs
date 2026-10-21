using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using QLyCuaHangSuaXe.DanhMuc.DichVu.Classes;

namespace QLyCuaHangSuaXe.DanhMuc.DichVu
{
    public partial class frmDanhMucDichVu : Form
    {
        private DataProcessor dtBase = new DataProcessor();

        public frmDanhMucDichVu()
        {
            InitializeComponent();
        }

        private void frmDanhMucDichVu_Load(object sender, EventArgs e)
        {
            cboTrangThai.SelectedIndex = 0;
            LoadData();
        }

        private void LoadData()
        {
            string sqlSelect = @"SELECT 
                                    ServiceId AS [Mã DV], 
                                    ServiceName AS [Tên dịch vụ], 
                                    BasePrice AS [Giá niêm yết (VNĐ)], 
                                    Description AS [Mô tả], 
                                    CASE WHEN IsActive = 1 THEN N'Đang hoạt động' ELSE N'Ngừng hoạt động' END AS [Trạng thái], 
                                    IsActive 
                                 FROM DichVu 
                                 ORDER BY ServiceId ASC";

            DataTable dt = dtBase.ReadData(sqlSelect);
            dgvDichVu.DataSource = dt;

            if (dgvDichVu.Columns.Contains("IsActive"))
            {
                dgvDichVu.Columns["IsActive"].Visible = false;
            }

            if (dgvDichVu.Columns.Count > 2)
            {
                dgvDichVu.Columns[2].DefaultCellStyle.Format = "N0";
                dgvDichVu.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            if (dgvDichVu.Columns.Count > 0)
            {
                dgvDichVu.Columns[0].Width = 75;
            }

            lblTongSo.Text = $"Tổng số bản ghi: {dt.Rows.Count}";
        }

        private void XoaTrang()
        {
            txtMaDV.Clear();
            txtTenDV.Clear();
            numDonGia.Value = 0;
            txtMoTa.Clear();
            cboTrangThai.SelectedIndex = 0;
            txtTimKiem.Clear();

            txtTenDV.Focus();
        }

        private void dgvDichVu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvDichVu.Rows.Count)
            {
                DataGridViewRow row = dgvDichVu.Rows[e.RowIndex];

                txtMaDV.Text = row.Cells[0].Value?.ToString();
                txtTenDV.Text = row.Cells[1].Value?.ToString();

                if (decimal.TryParse(row.Cells[2].Value?.ToString(), out decimal donGia))
                {
                    numDonGia.Value = donGia;
                }
                else
                {
                    numDonGia.Value = 0;
                }

                txtMoTa.Text = row.Cells[3].Value?.ToString();

                string trangThai = row.Cells[4].Value?.ToString();
                cboTrangThai.SelectedItem = (trangThai == "Ngừng hoạt động") ? "Ngừng hoạt động" : "Đang hoạt động";
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string ten = txtTenDV.Text.Trim();
            decimal gia = numDonGia.Value;
            string moTa = txtMoTa.Text.Trim();
            int isActive = (cboTrangThai.SelectedIndex == 0) ? 1 : 0;

            if (string.IsNullOrEmpty(ten))
            {
                MessageBox.Show("Vui lòng nhập Tên dịch vụ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenDV.Focus();
                return;
            }

            string sqlCheck = $"SELECT COUNT(*) FROM DichVu WHERE ServiceName = N'{ten}'";
            object tonTai = dtBase.GetScalar(sqlCheck);
            if (tonTai != null && Convert.ToInt32(tonTai) > 0)
            {
                MessageBox.Show($"Tên dịch vụ [{ten}] đã tồn tại trong danh mục niêm yết!", "Trùng dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenDV.Focus();
                return;
            }

            string sqlInsert = $"INSERT INTO DichVu (ServiceName, BasePrice, Description, IsActive) VALUES (N'{ten}', {gia}, N'{moTa}', {isActive})";
            dtBase.ChangeData(sqlInsert);

            MessageBox.Show("Thêm mới dịch vụ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadData();
            XoaTrang();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtMaDV.Text, out int maDV))
            {
                MessageBox.Show("Vui lòng chọn dịch vụ từ bảng danh sách để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string ten = txtTenDV.Text.Trim();
            decimal gia = numDonGia.Value;
            string moTa = txtMoTa.Text.Trim();
            int isActive = (cboTrangThai.SelectedIndex == 0) ? 1 : 0;

            if (string.IsNullOrEmpty(ten))
            {
                MessageBox.Show("Tên dịch vụ không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenDV.Focus();
                return;
            }

            string sqlCheck = $"SELECT COUNT(*) FROM DichVu WHERE ServiceName = N'{ten}' AND ServiceId <> {maDV}";
            object tonTai = dtBase.GetScalar(sqlCheck);
            if (tonTai != null && Convert.ToInt32(tonTai) > 0)
            {
                MessageBox.Show($"Tên dịch vụ [{ten}] đã được sử dụng bởi dịch vụ khác!", "Trùng tên dịch vụ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenDV.Focus();
                return;
            }

            string sqlUpdate = $"UPDATE DichVu SET ServiceName = N'{ten}', BasePrice = {gia}, Description = N'{moTa}', IsActive = {isActive} WHERE ServiceId = {maDV}";
            dtBase.ChangeData(sqlUpdate);

            MessageBox.Show("Cập nhật thông tin dịch vụ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadData();
            XoaTrang();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtMaDV.Text, out int maDV))
            {
                MessageBox.Show("Vui lòng chọn dịch vụ muốn xóa từ bảng danh sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string sqlCheckCT = $"SELECT COUNT(*) FROM ChiTietPhieuSuaChua WHERE ServiceId = {maDV}";
            string sqlCheckLH = $"SELECT COUNT(*) FROM LichHen WHERE ServiceId = {maDV}";
            int countCT = Convert.ToInt32(dtBase.GetScalar(sqlCheckCT) ?? 0);
            int countLH = Convert.ToInt32(dtBase.GetScalar(sqlCheckLH) ?? 0);

            if (countCT > 0 || countLH > 0)
            {
                DialogResult hoiKhoa = MessageBox.Show(
                    $"Dịch vụ [{txtTenDV.Text}] đã có dữ liệu trong Phiếu sửa chữa hoặc Lịch hẹn, không thể xóa vĩnh viễn!\n\nBạn có muốn chuyển trạng thái dịch vụ sang 'Ngừng hoạt động' không?",
                    "Cảnh báo ràng buộc toàn vẹn",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (hoiKhoa == DialogResult.Yes)
                {
                    string sqlDeactivate = $"UPDATE DichVu SET IsActive = 0 WHERE ServiceId = {maDV}";
                    dtBase.ChangeData(sqlDeactivate);
                    MessageBox.Show("Đã chuyển dịch vụ sang trạng thái 'Ngừng hoạt động' thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    XoaTrang();
                }
                return;
            }

            DialogResult dr = MessageBox.Show($"Bạn có chắc chắn muốn xóa dịch vụ [Mã: {maDV}] - {txtTenDV.Text} không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                string sqlDelete = $"DELETE FROM DichVu WHERE ServiceId = {maDV}";
                dtBase.ChangeData(sqlDelete);

                MessageBox.Show("Đã xóa dịch vụ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
                XoaTrang();
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            XoaTrang();
            LoadData();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            TimKiem();
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            TimKiem();
        }

        private void TimKiem()
        {
            string tuKhoa = txtTimKiem.Text.Trim();
            string sqlSearch = $@"SELECT 
                                    ServiceId AS [Mã DV], 
                                    ServiceName AS [Tên dịch vụ], 
                                    BasePrice AS [Giá niêm yết (VNĐ)], 
                                    Description AS [Mô tả], 
                                    CASE WHEN IsActive = 1 THEN N'Đang hoạt động' ELSE N'Ngừng hoạt động' END AS [Trạng thái], 
                                    IsActive 
                                  FROM DichVu 
                                  WHERE ServiceName LIKE N'%{tuKhoa}%' OR Description LIKE N'%{tuKhoa}%' 
                                  ORDER BY ServiceId ASC";

            DataTable dt = dtBase.ReadData(sqlSearch);
            dgvDichVu.DataSource = dt;

            if (dgvDichVu.Columns.Contains("IsActive"))
            {
                dgvDichVu.Columns["IsActive"].Visible = false;
            }

            lblTongSo.Text = $"Kết quả tìm kiếm: {dt.Rows.Count} bản ghi";
        }
    }
}
