using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DanhMuc.Classes;

namespace DanhMuc
{
    public partial class frmDanhMucDichVu : Form
    {
        // Khởi tạo đối tượng xử lý CSDL từ thư mục Classes
        private DataProcessor dtBase = new DataProcessor();

        public frmDanhMucDichVu()
        {
            InitializeComponent();
        }

        // 1. Khi Form tải lên
        private void frmDanhMucDichVu_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        // 2. Tải toàn bộ danh sách dịch vụ lên DataGridView
        private void LoadData()
        {
            string sqlSelect = "SELECT MaDV AS [Mã dịch vụ], TenDV AS [Tên dịch vụ], DonGia AS [Đơn giá (Tiền công)], GhiChu AS [Ghi chú] FROM DichVu ORDER BY MaDV";
            DataTable dt = dtBase.ReadData(sqlSelect);

            dgvDichVu.DataSource = dt;

            // Định dạng hiển thị tiền tệ có dấu phân cách hàng nghìn (VD: 100,000)
            if (dgvDichVu.Columns.Count > 2)
            {
                dgvDichVu.Columns[2].DefaultCellStyle.Format = "N0";
                dgvDichVu.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            // Cập nhật tổng số lượng bản ghi
            lblTongSo.Text = $"Tổng số bản ghi: {dt.Rows.Count}";
        }

        // 3. Xóa trắng form nhập liệu
        private void XoaTrang()
        {
            txtMaDV.Clear();
            txtTenDV.Clear();
            numDonGia.Value = 0;
            txtGhiChu.Clear();
            txtTimKiem.Clear();

            txtMaDV.ReadOnly = false; // Cho phép nhập mã mới
            txtMaDV.Focus();
        }

        // 4. Nhấp vào dòng trên DataGridView thì đổ dữ liệu lên TextBox
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

                txtGhiChu.Text = row.Cells[3].Value?.ToString();

                // Đã chọn một dòng thì khóa Mã lại không cho sửa mã khóa chính
                txtMaDV.ReadOnly = true;
            }
        }

        // 5. Nút Thêm mới
        private void btnThem_Click(object sender, EventArgs e)
        {
            string ma = txtMaDV.Text.Trim();
            string ten = txtTenDV.Text.Trim();
            decimal gia = numDonGia.Value;
            string ghiChu = txtGhiChu.Text.Trim();

            // Kiểm tra rỗng
            if (string.IsNullOrEmpty(ma) || string.IsNullOrEmpty(ten))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Mã dịch vụ và Tên dịch vụ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaDV.Focus();
                return;
            }

            // Kiểm tra trùng mã
            string sqlCheck = $"SELECT COUNT(*) FROM DichVu WHERE MaDV = '{ma}'";
            object tonTai = dtBase.GetScalar(sqlCheck);
            if (tonTai != null && Convert.ToInt32(tonTai) > 0)
            {
                MessageBox.Show($"Mã dịch vụ [{ma}] đã tồn tại trong hệ thống, vui lòng nhập mã khác!", "Trùng khóa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaDV.Focus();
                return;
            }

            // Thực thi Thêm
            string sqlInsert = $"INSERT INTO DichVu (MaDV, TenDV, DonGia, GhiChu) VALUES ('{ma}', N'{ten}', {gia}, N'{ghiChu}')";
            dtBase.ChangeData(sqlInsert);

            MessageBox.Show("Thêm mới dịch vụ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadData();
            XoaTrang();
        }

        // 6. Nút Sửa
        private void btnSua_Click(object sender, EventArgs e)
        {
            string ma = txtMaDV.Text.Trim();
            string ten = txtTenDV.Text.Trim();
            decimal gia = numDonGia.Value;
            string ghiChu = txtGhiChu.Text.Trim();

            if (string.IsNullOrEmpty(ma))
            {
                MessageBox.Show("Vui lòng chọn dịch vụ từ danh sách để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(ten))
            {
                MessageBox.Show("Tên dịch vụ không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenDV.Focus();
                return;
            }

            string sqlUpdate = $"UPDATE DichVu SET TenDV = N'{ten}', DonGia = {gia}, GhiChu = N'{ghiChu}' WHERE MaDV = '{ma}'";
            dtBase.ChangeData(sqlUpdate);

            MessageBox.Show("Cập nhật thông tin dịch vụ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadData();
            XoaTrang();
        }

        // 7. Nút Xóa
        private void btnXoa_Click(object sender, EventArgs e)
        {
            string ma = txtMaDV.Text.Trim();

            if (string.IsNullOrEmpty(ma))
            {
                MessageBox.Show("Vui lòng chọn dịch vụ muốn xóa từ bảng danh sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult dr = MessageBox.Show($"Bạn có chắc chắn muốn xóa dịch vụ [{ma}] - {txtTenDV.Text} không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                string sqlDelete = $"DELETE FROM DichVu WHERE MaDV = '{ma}'";
                dtBase.ChangeData(sqlDelete);

                MessageBox.Show("Đã xóa dịch vụ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
                XoaTrang();
            }
        }

        // 8. Nút Làm mới
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            XoaTrang();
            LoadData();
        }

        // 9. Nút Đóng
        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // 10. Tìm kiếm theo tên hoặc mã dịch vụ
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
            string sqlSearch = $"SELECT MaDV AS [Mã dịch vụ], TenDV AS [Tên dịch vụ], DonGia AS [Đơn giá (Tiền công)], GhiChu AS [Ghi chú] FROM DichVu WHERE TenDV LIKE N'%{tuKhoa}%' OR MaDV LIKE N'%{tuKhoa}%' ORDER BY MaDV";

            DataTable dt = dtBase.ReadData(sqlSearch);
            dgvDichVu.DataSource = dt;
            lblTongSo.Text = $"Kết quả tìm kiếm: {dt.Rows.Count} bản ghi";
        }
    }
}
