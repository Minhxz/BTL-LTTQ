using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace HoaDon
{
    [System.ComponentModel.DesignerCategory("Form")]
    public partial class frmHoaDon : Form
    {
        private readonly DataProcessor db = new DataProcessor();

        // Bảng tạm lưu các mục trong hóa đơn hiện tại (Dịch vụ / Phụ tùng)
        private DataTable dtChiTietHD;

        // Trạng thái thao tác: true khi đang bấm "Thêm mới HĐ"
        private bool isThemMoi = false;

        // Mã phiếu tiếp nhận nếu mở từ form tiếp nhận
        private string maPhieuBanDau = null;

        public frmHoaDon() : this(null)
        {
        }

        public frmHoaDon(string maPhieuTiepNhan)
        {
            this.maPhieuBanDau = maPhieuTiepNhan;
            InitializeComponent();
            SetupModernAppearance();
            SetupEventHandlers();
        }

        private void SetupEventHandlers()
        {
            this.Load += frmHoaDon_Load;

            // Xử lý ComboBox
            this.cboMaNhanVien.SelectedIndexChanged += cboMaNhanVien_SelectedIndexChanged;
            this.cboMaKhachHang.SelectedIndexChanged += cboMaKhachHang_SelectedIndexChanged;

            // Thao tác danh mục / giỏ hàng
            this.txtTimKiemPhuTung.TextChanged += txtTimKiemPhuTung_TextChanged;
            this.btnThemVaoHD.Click += btnThemVaoHD_Click;
            this.btnXoaKhoiHD.Click += btnXoaKhoiHD_Click;

            // Tính tiền
            this.txtGiamGia.TextChanged += txtGiamGia_TextChanged;
            this.txtGiamGia.KeyPress += txtGiamGia_KeyPress;

            // Các nút chức năng chính
            this.btnThemHD.Click += btnThemHD_Click;
            this.btnLuuHD.Click += btnLuuHD_Click;
            this.btnHuyHD.Click += btnHuyHD_Click;
            this.btnInHD.Click += btnInHD_Click;
            this.btnDong.Click += btnDong_Click;

            // Cấu hình hiển thị DataGridView
            dgvPhuTungDaChon.CellClick += dgvPhuTungDaChon_CellClick;
        }

        private void frmHoaDon_Load(object sender, EventArgs e)
        {
            if (DesignMode || System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
                return;

            InitDataTableChiTiet();
            LoadComboBoxNhanVien();
            LoadComboBoxKhachHang();
            LoadKhoDichVuPhuTung();

            // Mặc định ban đầu
            ResetForm();
            SetButtonState(true);

            if (!string.IsNullOrEmpty(maPhieuBanDau))
            {
                NapTuPhieuTiepNhan(maPhieuBanDau);
            }
        }

        #region Khởi tạo cấu trúc bảng Chi Tiết Hóa Đơn
        private void InitDataTableChiTiet()
        {
            dtChiTietHD = new DataTable();
            dtChiTietHD.Columns.Add("MaDichVu", typeof(string));
            dtChiTietHD.Columns.Add("TenDichVu", typeof(string));
            dtChiTietHD.Columns.Add("Loai", typeof(string)); // "Dịch vụ" hoặc "Phụ tùng"
            dtChiTietHD.Columns.Add("SoLuong", typeof(int));
            dtChiTietHD.Columns.Add("DonGia", typeof(decimal));
            dtChiTietHD.Columns.Add("ThanhTien", typeof(decimal), "SoLuong * DonGia");

            dgvPhuTungDaChon.DataSource = dtChiTietHD;

            if (dgvPhuTungDaChon.Columns["MaDichVu"] != null)
                dgvPhuTungDaChon.Columns["MaDichVu"].HeaderText = "Mã DV/PT";
            if (dgvPhuTungDaChon.Columns["TenDichVu"] != null)
                dgvPhuTungDaChon.Columns["TenDichVu"].HeaderText = "Tên dịch vụ / Phụ tùng";
            if (dgvPhuTungDaChon.Columns["Loai"] != null)
                dgvPhuTungDaChon.Columns["Loai"].HeaderText = "Phân loại";
            if (dgvPhuTungDaChon.Columns["SoLuong"] != null)
                dgvPhuTungDaChon.Columns["SoLuong"].HeaderText = "Số lượng";
            if (dgvPhuTungDaChon.Columns["DonGia"] != null)
            {
                dgvPhuTungDaChon.Columns["DonGia"].HeaderText = "Đơn giá (VNĐ)";
                dgvPhuTungDaChon.Columns["DonGia"].DefaultCellStyle.Format = "N0";
            }
            if (dgvPhuTungDaChon.Columns["ThanhTien"] != null)
            {
                dgvPhuTungDaChon.Columns["ThanhTien"].HeaderText = "Thành tiền (VNĐ)";
                dgvPhuTungDaChon.Columns["ThanhTien"].DefaultCellStyle.Format = "N0";
            }
        }
        #endregion

        #region Load Dữ Liệu Ban Đầu
        private void LoadComboBoxNhanVien()
        {
            try
            {
                string sql = "SELECT MaNhanVien, TenNhanVien FROM NhanVien WHERE TrangThai = N'Đang làm'";
                DataTable dt = db.ReadData(sql);
                cboMaNhanVien.DataSource = dt;
                cboMaNhanVien.DisplayMember = "MaNhanVien";
                cboMaNhanVien.ValueMember = "MaNhanVien";
                cboMaNhanVien.SelectedIndex = -1;
                txtTenNhanVien.Text = "";
            }
            catch
            {
                // Fallback nếu chưa kết nối SQL Server
                cboMaNhanVien.DataSource = null;
            }
        }

        private void LoadComboBoxKhachHang()
        {
            try
            {
                string sql = @"SELECT kh.MaKhachHang, kh.TenKhachHang, kh.SoDienThoai, kh.DiaChi,
                                      x.BienSo, x.LoaiXe, x.DongXe
                               FROM KhachHang kh
                               LEFT JOIN Xe x ON kh.MaKhachHang = x.MaKhachHang";
                DataTable dt = db.ReadData(sql);
                cboMaKhachHang.DataSource = dt;
                cboMaKhachHang.DisplayMember = "MaKhachHang";
                cboMaKhachHang.ValueMember = "MaKhachHang";
                cboMaKhachHang.SelectedIndex = -1;
            }
            catch
            {
                cboMaKhachHang.DataSource = null;
            }
        }

        private void LoadKhoDichVuPhuTung(string keyword = "")
        {
            try
            {
                string sql = @"
                    SELECT MaDichVu AS Ma, TenDichVu AS Ten, DonGiaDichVu AS DonGia, N'Dịch vụ' AS Loai, 999 AS TonKho 
                    FROM DichVu 
                    WHERE TrangThai = N'Đang sử dụng' AND (MaDichVu LIKE @kw OR TenDichVu LIKE @kw)
                    UNION ALL
                    SELECT MaPhuTung AS Ma, TenPhuTung AS Ten, DonGiaBan AS DonGia, N'Phụ tùng' AS Loai, SoLuongTon AS TonKho 
                    FROM PhuTung 
                    WHERE TrangThai = N'Đang sử dụng' AND (MaPhuTung LIKE @kw OR TenPhuTung LIKE @kw)";

                SqlParameter[] pars = {
                    new SqlParameter("@kw", "%" + keyword.Trim() + "%")
                };

                DataTable dt = db.ReadData(sql, pars);
                dgvKhoPhuTung.DataSource = dt;

                if (dgvKhoPhuTung.Columns["Ma"] != null)
                    dgvKhoPhuTung.Columns["Ma"].HeaderText = "Mã";
                if (dgvKhoPhuTung.Columns["Ten"] != null)
                    dgvKhoPhuTung.Columns["Ten"].HeaderText = "Tên DV / Phụ tùng";
                if (dgvKhoPhuTung.Columns["DonGia"] != null)
                {
                    dgvKhoPhuTung.Columns["DonGia"].HeaderText = "Đơn giá (VNĐ)";
                    dgvKhoPhuTung.Columns["DonGia"].DefaultCellStyle.Format = "N0";
                }
                if (dgvKhoPhuTung.Columns["Loai"] != null)
                    dgvKhoPhuTung.Columns["Loai"].HeaderText = "Loại";
                if (dgvKhoPhuTung.Columns["TonKho"] != null)
                    dgvKhoPhuTung.Columns["TonKho"].HeaderText = "Tồn";
            }
            catch
            {
                // Xử lý nạp dữ liệu rỗng nếu chưa bật server
            }
        }

        public void NapTuPhieuTiepNhan(string maTN)
        {
            try
            {
                btnThemHD_Click(null, null);

                string sql = @"SELECT p.MaPhieuTiepNhan, p.MaKhachHang, p.MaNhanVien, p.ThoiGianNhan, p.SoKm,
                                      x.BienSo, x.LoaiXe, kh.TenKhachHang, kh.SoDienThoai, kh.DiaChi
                               FROM PhieuTiepNhan p
                               LEFT JOIN KhachHang kh ON p.MaKhachHang = kh.MaKhachHang
                               LEFT JOIN Xe x ON p.MaXe = x.MaXe
                               WHERE p.MaPhieuTiepNhan = @ma";
                DataTable dt = db.ReadData(sql, new SqlParameter[] { new SqlParameter("@ma", maTN) });
                if (dt.Rows.Count > 0)
                {
                    DataRow r = dt.Rows[0];
                    if (r["MaNhanVien"] != DBNull.Value && !string.IsNullOrEmpty(r["MaNhanVien"].ToString()))
                        cboMaNhanVien.SelectedValue = r["MaNhanVien"].ToString();
                    if (r["MaKhachHang"] != DBNull.Value && !string.IsNullOrEmpty(r["MaKhachHang"].ToString()))
                        cboMaKhachHang.SelectedValue = r["MaKhachHang"].ToString();

                    if (r["TenKhachHang"] != DBNull.Value)
                        txtTenKhachHang.Text = r["TenKhachHang"].ToString();
                    if (r["SoDienThoai"] != DBNull.Value)
                        txtSDT.Text = r["SoDienThoai"].ToString();
                    if (r["DiaChi"] != DBNull.Value)
                        txtDiaChi.Text = r["DiaChi"].ToString();
                    if (r["BienSo"] != DBNull.Value)
                        txtBKS.Text = r["BienSo"].ToString();
                    if (r["SoKm"] != DBNull.Value)
                        txtSoKM.Text = r["SoKm"].ToString();
                    if (r["LoaiXe"] != DBNull.Value && !string.IsNullOrEmpty(r["LoaiXe"].ToString()))
                        cboLoaiXe.Text = r["LoaiXe"].ToString();
                    if (r["ThoiGianNhan"] != DBNull.Value)
                        dtpNgayNhanXe.Value = Convert.ToDateTime(r["ThoiGianNhan"]);

                    dtChiTietHD.Clear();

                    // Nạp dịch vụ từ ChiTietPhieuTiepNhan
                    string sqlDV = @"SELECT ct.MaDichVu, ct.TenDichVu, N'Dịch vụ' AS Loai, ct.SoLuong, ct.DonGiaDichVu AS DonGia
                                     FROM ChiTietPhieuTiepNhan ct
                                     WHERE ct.MaPhieuTiepNhan = @ma";
                    DataTable dtDV = db.ReadData(sqlDV, new SqlParameter[] { new SqlParameter("@ma", maTN) });
                    foreach (DataRow row in dtDV.Rows)
                    {
                        DataRow nr = dtChiTietHD.NewRow();
                        nr["MaDichVu"] = row["MaDichVu"];
                        nr["TenDichVu"] = row["TenDichVu"];
                        nr["Loai"] = row["Loai"];
                        nr["SoLuong"] = Convert.ToInt32(row["SoLuong"]);
                        nr["DonGia"] = Convert.ToDecimal(row["DonGia"]);
                        dtChiTietHD.Rows.Add(nr);
                    }

                    // Nạp phụ tùng từ PhuTungTiepNhan
                    string sqlPT = @"SELECT pt.MaPhuTung AS MaDichVu, pt.TenPhuTung AS TenDichVu, N'Phụ tùng' AS Loai, pt.SoLuong, pt.DonGia
                                     FROM PhuTungTiepNhan pt
                                     WHERE pt.MaPhieuTiepNhan = @ma";
                    DataTable dtPT = db.ReadData(sqlPT, new SqlParameter[] { new SqlParameter("@ma", maTN) });
                    foreach (DataRow row in dtPT.Rows)
                    {
                        DataRow nr = dtChiTietHD.NewRow();
                        nr["MaDichVu"] = row["MaDichVu"];
                        nr["TenDichVu"] = row["TenDichVu"];
                        nr["Loai"] = row["Loai"];
                        nr["SoLuong"] = Convert.ToInt32(row["SoLuong"]);
                        nr["DonGia"] = Convert.ToDecimal(row["DonGia"]);
                        dtChiTietHD.Rows.Add(nr);
                    }

                    CapNhatTongTien();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nạp thông tin từ phiếu tiếp nhận: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Tương Tác Chọn Nhân Viên & Khách Hàng
        private void cboMaNhanVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMaNhanVien.SelectedIndex >= 0 && cboMaNhanVien.SelectedValue != null)
            {
                string maNV = cboMaNhanVien.SelectedValue.ToString();
                try
                {
                    DataTable dt = db.ReadData("SELECT TenNhanVien FROM NhanVien WHERE MaNhanVien = @ma",
                        new SqlParameter[] { new SqlParameter("@ma", maNV) });
                    if (dt.Rows.Count > 0)
                    {
                        txtTenNhanVien.Text = dt.Rows[0]["TenNhanVien"].ToString();
                    }
                }
                catch { }
            }
            else
            {
                txtTenNhanVien.Text = "";
            }
        }

        private void cboMaKhachHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMaKhachHang.SelectedIndex >= 0 && cboMaKhachHang.SelectedValue != null)
            {
                string maKH = cboMaKhachHang.SelectedValue.ToString();
                try
                {
                    string sql = @"SELECT kh.TenKhachHang, kh.SoDienThoai, kh.DiaChi,
                                          x.BienSo, x.LoaiXe
                                   FROM KhachHang kh
                                   LEFT JOIN Xe x ON kh.MaKhachHang = x.MaKhachHang
                                   WHERE kh.MaKhachHang = @ma";
                    DataTable dt = db.ReadData(sql, new SqlParameter[] { new SqlParameter("@ma", maKH) });
                    if (dt.Rows.Count > 0)
                    {
                        DataRow row = dt.Rows[0];
                        txtTenKhachHang.Text = row["TenKhachHang"].ToString();
                        txtSDT.Text = row["SoDienThoai"].ToString();
                        txtDiaChi.Text = row["DiaChi"].ToString();
                        txtBKS.Text = row["BienSo"] != DBNull.Value ? row["BienSo"].ToString() : "";

                        string loaiXe = row["LoaiXe"] != DBNull.Value ? row["LoaiXe"].ToString() : "";
                        if (!string.IsNullOrEmpty(loaiXe))
                        {
                            cboLoaiXe.Text = loaiXe;
                        }
                    }
                }
                catch { }
            }
            else
            {
                txtTenKhachHang.Text = "";
                txtSDT.Text = "";
                txtDiaChi.Text = "";
                txtBKS.Text = "";
            }
        }

        private void txtTimKiemPhuTung_TextChanged(object sender, EventArgs e)
        {
            LoadKhoDichVuPhuTung(txtTimKiemPhuTung.Text);
        }
        #endregion

        #region Thao Tác Chi Tiết Hóa Đơn (Thêm / Xóa / Tính Toán)
        private void btnThemVaoHD_Click(object sender, EventArgs e)
        {
            if (!isThemMoi)
            {
                MessageBox.Show("Vui lòng bấm 'Thêm mới HĐ' trước khi chọn dịch vụ/phụ tùng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (dgvKhoPhuTung.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn 1 dịch vụ hoặc phụ tùng từ danh sách bên phải!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow cur = dgvKhoPhuTung.CurrentRow;
            string ma = cur.Cells["Ma"].Value != null ? cur.Cells["Ma"].Value.ToString() : "";
            string ten = cur.Cells["Ten"].Value != null ? cur.Cells["Ten"].Value.ToString() : "";
            string loai = cur.Cells["Loai"].Value != null ? cur.Cells["Loai"].Value.ToString() : "";
            decimal donGia = Convert.ToDecimal(cur.Cells["DonGia"].Value != null ? cur.Cells["DonGia"].Value : 0);
            int tonKho = Convert.ToInt32(cur.Cells["TonKho"].Value != null ? cur.Cells["TonKho"].Value : 0);
            int soLuong = (int)nudSoLuong.Value;

            if (loai == "Phụ tùng" && soLuong > tonKho)
            {
                MessageBox.Show(string.Format("Số lượng phụ tùng '{0}' trong kho chỉ còn {1}. Không đủ để xuất!", ten, tonKho),
                    "Cảnh báo tồn kho", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra xem mục này đã có trong bảng hóa đơn chưa
            DataRow existingRow = null;
            foreach (DataRow r in dtChiTietHD.Rows)
            {
                if (r["MaDichVu"].ToString() == ma)
                {
                    existingRow = r;
                    break;
                }
            }

            if (existingRow != null)
            {
                int newQty = Convert.ToInt32(existingRow["SoLuong"]) + soLuong;
                if (loai == "Phụ tùng" && newQty > tonKho)
                {
                    MessageBox.Show(string.Format("Tổng số lượng '{0}' ({1}) vượt quá tồn kho hiện tại ({2})!", ten, newQty, tonKho),
                        "Cảnh báo tồn kho", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                existingRow["SoLuong"] = newQty;
            }
            else
            {
                DataRow newRow = dtChiTietHD.NewRow();
                newRow["MaDichVu"] = ma;
                newRow["TenDichVu"] = ten;
                newRow["Loai"] = loai;
                newRow["SoLuong"] = soLuong;
                newRow["DonGia"] = donGia;
                dtChiTietHD.Rows.Add(newRow);
            }

            nudSoLuong.Value = 1;
            CapNhatTongTien();
        }

        private void btnXoaKhoiHD_Click(object sender, EventArgs e)
        {
            if (!isThemMoi)
            {
                MessageBox.Show("Hóa đơn đã khóa hoặc chưa ở chế độ tạo mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (dgvPhuTungDaChon.CurrentRow != null)
            {
                int index = dgvPhuTungDaChon.CurrentRow.Index;
                if (index >= 0 && index < dtChiTietHD.Rows.Count)
                {
                    dtChiTietHD.Rows.RemoveAt(index);
                    CapNhatTongTien();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn dòng cần xóa khỏi hóa đơn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgvPhuTungDaChon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Bắt sự kiện chọn dòng nếu cần
        }

        private void CapNhatTongTien()
        {
            decimal tongDichVu = 0;
            decimal tongPhuTung = 0;

            foreach (DataRow r in dtChiTietHD.Rows)
            {
                decimal tt = Convert.ToDecimal(r["SoLuong"]) * Convert.ToDecimal(r["DonGia"]);
                string loai = r["Loai"].ToString();
                if (loai == "Dịch vụ")
                {
                    tongDichVu += tt;
                }
                else
                {
                    tongPhuTung += tt;
                }
            }

            decimal tienCong = 0;
            decimal.TryParse(txtTienCong.Text.Replace(",", "").Replace(".", ""), out tienCong);

            decimal tienPhuTungVaDV = tongDichVu + tongPhuTung;
            txtTienPhuTung.Text = tienPhuTungVaDV.ToString("N0");
            txtTienCongThoTong.Text = tienCong.ToString("N0");

            decimal giamGiaPt = 0;
            decimal.TryParse(txtGiamGia.Text, out giamGiaPt);
            if (giamGiaPt < 0) giamGiaPt = 0;
            if (giamGiaPt > 100) giamGiaPt = 100;

            decimal subTotal = tienPhuTungVaDV + tienCong;
            decimal tongTien = subTotal - (subTotal * giamGiaPt / 100);

            txtTongTien.Text = tongTien.ToString("N0");
            lblBangChu.Text = "Bằng chữ: " + NumberToWords.ChuyenSoThanhChu(tongTien);
        }

        private void txtGiamGia_TextChanged(object sender, EventArgs e)
        {
            CapNhatTongTien();
        }

        private void txtGiamGia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        #endregion

        #region Các Nút Chức Năng Chính (Thêm / Lưu / Hủy / In / Đóng)
        private void btnThemHD_Click(object sender, EventArgs e)
        {
            isThemMoi = true;
            ResetForm();
            txtMaHD.Text = SinhMaHoaDonTuDong();
            dtpNgayNhanXe.Value = DateTime.Now;
            SetButtonState(false);

            if (cboMaNhanVien.Items.Count > 0)
                cboMaNhanVien.SelectedIndex = 0;
            if (cboMaKhachHang.Items.Count > 0)
                cboMaKhachHang.SelectedIndex = 0;

            cboMaNhanVien.Focus();
        }

        private string SinhMaHoaDonTuDong()
        {
            try
            {
                object result = db.ReadScalar("SELECT MAX(CAST(SUBSTRING(MaHoaDon, 3, 10) AS INT)) FROM HoaDon WHERE MaHoaDon LIKE 'HD%'");
                int nextIndex = 1;
                if (result != null && result != DBNull.Value)
                {
                    nextIndex = Convert.ToInt32(result) + 1;
                }
                return string.Format("HD{0:D3}", nextIndex);
            }
            catch
            {
                return "HD" + DateTime.Now.ToString("HHmmss");
            }
        }

        private string SinhMaPhieuTiepNhanTuDong()
        {
            try
            {
                object result = db.ReadScalar("SELECT MAX(CAST(SUBSTRING(MaPhieuTiepNhan, 3, 10) AS INT)) FROM PhieuTiepNhan WHERE MaPhieuTiepNhan LIKE 'TN%'");
                int nextIndex = 1;
                if (result != null && result != DBNull.Value)
                {
                    nextIndex = Convert.ToInt32(result) + 1;
                }
                return string.Format("TN{0:D3}", nextIndex);
            }
            catch
            {
                return "TN" + DateTime.Now.ToString("HHmmss");
            }
        }

        private void btnLuuHD_Click(object sender, EventArgs e)
        {
            if (!isThemMoi) return;

            // Kiểm tra dữ liệu hợp lệ
            if (cboMaNhanVien.SelectedIndex < 0 || cboMaNhanVien.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn nhân viên lập hóa đơn!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboMaNhanVien.Focus();
                return;
            }

            if (cboMaKhachHang.SelectedIndex < 0 || cboMaKhachHang.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn khách hàng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboMaKhachHang.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtBKS.Text))
            {
                MessageBox.Show("Vui lòng nhập biển số xe!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBKS.Focus();
                return;
            }

            if (dtChiTietHD.Rows.Count == 0)
            {
                MessageBox.Show("Hóa đơn phải có ít nhất 1 dịch vụ hoặc phụ tùng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maHD = txtMaHD.Text.Trim();
            string maNV = cboMaNhanVien.SelectedValue.ToString();
            string maKH = cboMaKhachHang.SelectedValue.ToString();
            string bienSo = txtBKS.Text.Trim();
            int soKm = 0;
            int.TryParse(txtSoKM.Text, out soKm);

            decimal tongDichVu = 0;
            decimal tongPhuTung = 0;
            foreach (DataRow r in dtChiTietHD.Rows)
            {
                decimal tt = Convert.ToDecimal(r["SoLuong"]) * Convert.ToDecimal(r["DonGia"]);
                if (r["Loai"].ToString() == "Dịch vụ")
                    tongDichVu += tt;
                else
                    tongPhuTung += tt;
            }

            // Theo CHECK ([TongTien] = [TongTienDichVu] + [TongTienPhuTung]) trong CSDL SQL Server
            decimal tongTien = tongDichVu + tongPhuTung;

            // Bắt đầu lưu thông qua Transaction để toàn vẹn dữ liệu
            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    // 1. Kiểm tra / lấy MaXe tương ứng với MaKhachHang
                    string getXeSql = "SELECT TOP 1 MaXe FROM Xe WHERE MaKhachHang = @kh AND BienSo = @bs";
                    string maXe = null;
                    using (SqlCommand cmd = new SqlCommand(getXeSql, conn, trans))
                    {
                        cmd.Parameters.AddWithValue("@kh", maKH);
                        cmd.Parameters.AddWithValue("@bs", bienSo);
                        object res = cmd.ExecuteScalar();
                        if (res != null) maXe = res.ToString();
                    }

                    if (string.IsNullOrEmpty(maXe))
                    {
                        // Thử tìm theo Biển Số nếu khách hàng đã đổi
                        using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 MaXe FROM Xe WHERE BienSo = @bs", conn, trans))
                        {
                            cmd.Parameters.AddWithValue("@bs", bienSo);
                            object res = cmd.ExecuteScalar();
                            if (res != null) maXe = res.ToString();
                        }
                    }

                    if (string.IsNullOrEmpty(maXe))
                    {
                        // Sinh mã xe mới
                        string getMaxXe = "SELECT MAX(CAST(SUBSTRING(MaXe, 3, 10) AS INT)) FROM Xe WHERE MaXe LIKE 'XE%'";
                        int nextXe = 1;
                        using (SqlCommand cmd = new SqlCommand(getMaxXe, conn, trans))
                        {
                            object res = cmd.ExecuteScalar();
                            if (res != null && res != DBNull.Value) nextXe = Convert.ToInt32(res) + 1;
                        }
                        maXe = string.Format("XE{0:D3}", nextXe);

                        string insXe = @"INSERT INTO Xe(MaXe, MaKhachHang, BienSo, HangXe, DongXe, NamSanXuat, MauSac, SoKhung, LoaiXe)
                                         VALUES (@MaXe, @MaKhachHang, @BienSo, N'Khác', N'Mặc định', 2022, N'Đen', @SoKhung, @LoaiXe)";
                        using (SqlCommand cmd = new SqlCommand(insXe, conn, trans))
                        {
                            cmd.Parameters.AddWithValue("@MaXe", maXe);
                            cmd.Parameters.AddWithValue("@MaKhachHang", maKH);
                            cmd.Parameters.AddWithValue("@BienSo", bienSo);
                            cmd.Parameters.AddWithValue("@SoKhung", "SK-" + maXe);
                            cmd.Parameters.AddWithValue("@LoaiXe", string.IsNullOrEmpty(cboLoaiXe.Text) ? "Xe máy" : cboLoaiXe.Text);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    // 2. Tạo Phiếu Tiếp Nhận tương ứng
                    string maPhieuTN = SinhMaPhieuTiepNhanTuDong();
                    string insPhieuTN = @"INSERT INTO PhieuTiepNhan (MaPhieuTiepNhan, MaKhachHang, MaXe, MaNhanVien, ThoiGianNhan, ThoiGianHoanThanh, MoTaTinhTrangXe, TrangThai, GhiChu, DaChotPhuTung, SoKm)
                                          VALUES (@MaTN, @MaKH, @MaXe, @MaNV, @ThoiGianNhan, @ThoiGianHT, N'Khách hàng bảo dưỡng & sửa chữa theo hóa đơn', N'Hoàn thành', N'Đã thanh toán', 1, @SoKm)";
                    using (SqlCommand cmd = new SqlCommand(insPhieuTN, conn, trans))
                    {
                        cmd.Parameters.AddWithValue("@MaTN", maPhieuTN);
                        cmd.Parameters.AddWithValue("@MaKH", maKH);
                        cmd.Parameters.AddWithValue("@MaXe", maXe);
                        cmd.Parameters.AddWithValue("@MaNV", maNV);
                        cmd.Parameters.AddWithValue("@ThoiGianNhan", dtpNgayNhanXe.Value);
                        cmd.Parameters.AddWithValue("@ThoiGianHT", DateTime.Now);
                        cmd.Parameters.AddWithValue("@SoKm", soKm);
                        cmd.ExecuteNonQuery();
                    }

                    // 3. Tạo Hóa Đơn
                    string insHD = @"INSERT INTO HoaDon (MaHoaDon, MaPhieuTiepNhan, NgayLap, TongTienDichVu, TongTienPhuTung, TongTien, TrangThaiThanhToan, PhuongThucThanhToan, MaNhanVien, DaKhoaNoiDung)
                                     VALUES (@MaHD, @MaTN, @NgayLap, @TongDV, @TongPT, @TongTien, N'Đã thanh toán', N'Tiền mặt', @MaNV, 1)";
                    using (SqlCommand cmd = new SqlCommand(insHD, conn, trans))
                    {
                        cmd.Parameters.AddWithValue("@MaHD", maHD);
                        cmd.Parameters.AddWithValue("@MaTN", maPhieuTN);
                        cmd.Parameters.AddWithValue("@NgayLap", DateTime.Now);
                        cmd.Parameters.AddWithValue("@TongDV", tongDichVu);
                        cmd.Parameters.AddWithValue("@TongPT", tongPhuTung);
                        cmd.Parameters.AddWithValue("@TongTien", tongTien);
                        cmd.Parameters.AddWithValue("@MaNV", maNV);
                        cmd.ExecuteNonQuery();
                    }

                    // 4. Lưu Chi Tiết Hóa Đơn và trừ kho phụ tùng nếu có
                    foreach (DataRow r in dtChiTietHD.Rows)
                    {
                        string maItem = r["MaDichVu"].ToString();
                        string tenItem = r["TenDichVu"].ToString();
                        string loaiItem = r["Loai"].ToString();
                        int sl = Convert.ToInt32(r["SoLuong"]);
                        decimal dg = Convert.ToDecimal(r["DonGia"]);

                        if (loaiItem == "Dịch vụ")
                        {
                            string insCTHD = @"INSERT INTO ChiTietHoaDon (MaHoaDon, MaDichVu, TenDichVu, SoLuong, DonGia)
                                               VALUES (@MaHD, @MaDV, @TenDV, @SoLuong, @DonGia)";
                            using (SqlCommand cmd = new SqlCommand(insCTHD, conn, trans))
                            {
                                cmd.Parameters.AddWithValue("@MaHD", maHD);
                                cmd.Parameters.AddWithValue("@MaDV", maItem);
                                cmd.Parameters.AddWithValue("@TenDV", tenItem);
                                cmd.Parameters.AddWithValue("@SoLuong", sl);
                                cmd.Parameters.AddWithValue("@DonGia", dg);
                                cmd.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            string insPTTN = @"INSERT INTO PhuTungTiepNhan (MaPhieuTiepNhan, MaPhuTung, TenPhuTung, SoLuong, DonGia)
                                               VALUES (@MaTN, @MaPT, @TenPT, @SoLuong, @DonGia)";
                            using (SqlCommand cmd = new SqlCommand(insPTTN, conn, trans))
                            {
                                cmd.Parameters.AddWithValue("@MaTN", maPhieuTN);
                                cmd.Parameters.AddWithValue("@MaPT", maItem);
                                cmd.Parameters.AddWithValue("@TenPT", tenItem);
                                cmd.Parameters.AddWithValue("@SoLuong", sl);
                                cmd.Parameters.AddWithValue("@DonGia", dg);
                                cmd.ExecuteNonQuery();
                            }

                            string updateKho = "UPDATE PhuTung SET SoLuongTon = SoLuongTon - @sl WHERE MaPhuTung = @MaPT";
                            using (SqlCommand cmd = new SqlCommand(updateKho, conn, trans))
                            {
                                cmd.Parameters.AddWithValue("@sl", sl);
                                cmd.Parameters.AddWithValue("@MaPT", maItem);
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }

                    trans.Commit();
                    MessageBox.Show(string.Format("Lưu hóa đơn {0} thành công!", maHD), "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    isThemMoi = false;
                    SetButtonState(true);
                    LoadKhoDichVuPhuTung();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    MessageBox.Show("Lỗi khi lưu hóa đơn: " + ex.Message, "Lỗi cơ sở dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnHuyHD_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn hủy thao tác lập hóa đơn hiện tại?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                isThemMoi = false;
                ResetForm();
                SetButtonState(true);
            }
        }

        private void btnInHD_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaHD.Text))
            {
                MessageBox.Show("Chưa có hóa đơn nào để in!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string info = string.Format(
                "========================================================\n" +
                "               HÓA ĐƠN DỊCH VỤ SỬA CHỮA XE              \n" +
                "========================================================\n" +
                "Mã HĐ: {0}                      Ngày lập: {1}\n" +
                "Thu ngân: {2} ({3})\n" +
                "Khách hàng: {4}               Điện thoại: {5}\n" +
                "Biển số xe: {6}                 Loại xe: {7}\n" +
                "--------------------------------------------------------\n" +
                "DANH SÁCH CHI TIẾT DỊCH VỤ & PHỤ TÙNG:\n",
                txtMaHD.Text, dtpNgayNhanXe.Value.ToString("dd/MM/yyyy HH:mm"),
                txtTenNhanVien.Text, cboMaNhanVien.Text,
                txtTenKhachHang.Text, txtSDT.Text,
                txtBKS.Text, cboLoaiXe.Text);

            int stt = 1;
            foreach (DataRow r in dtChiTietHD.Rows)
            {
                info += string.Format("{0}. {1} ({2}) - SL: {3} x {4:N0} = {5:N0} VNĐ\n",
                    stt++, r["TenDichVu"], r["Loai"], r["SoLuong"], r["DonGia"], r["ThanhTien"]);
            }

            info += "--------------------------------------------------------\n";
            info += string.Format("Tiền phụ tùng & DV: {0} VNĐ\n", txtTienPhuTung.Text);
            info += string.Format("Tiền công thợ:     {0} VNĐ\n", txtTienCongThoTong.Text);
            info += string.Format("Giảm giá:          {0} %\n", txtGiamGia.Text);
            info += string.Format("TỔNG TIỀN PHẢI TRẢ:{0} VNĐ\n", txtTongTien.Text);
            info += string.Format("{0}\n", lblBangChu.Text);
            info += "========================================================\n" +
                    "           CẢM ƠN QUÝ KHÁCH VÀ HẸN GẶP LẠI!           ";

            MessageBox.Show(info, "In Hóa Đơn", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn đóng form Hóa Đơn?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }
        #endregion

        #region Tiện ích hỗ trợ
        private void ResetForm()
        {
            txtMaHD.Text = "";
            dtpNgayNhanXe.Value = DateTime.Now;
            cboMaNhanVien.SelectedIndex = -1;
            txtTenNhanVien.Text = "";
            cboMaKhachHang.SelectedIndex = -1;
            txtTenKhachHang.Text = "";
            txtSDT.Text = "";
            txtDiaChi.Text = "";
            txtBKS.Text = "";
            txtSoKM.Text = "0";
            if (cboLoaiXe.Items.Count > 0)
                cboLoaiXe.SelectedIndex = 0;
            txtTienCong.Text = "0";

            if (dtChiTietHD != null)
                dtChiTietHD.Clear();

            txtTienPhuTung.Text = "0";
            txtTienCongThoTong.Text = "0";
            txtGiamGia.Text = "0";
            txtTongTien.Text = "0";
            lblBangChu.Text = "Bằng chữ: Không đồng";
        }

        private void SetButtonState(bool normal)
        {
            btnThemHD.Enabled = normal;
            btnLuuHD.Enabled = !normal;
            btnHuyHD.Enabled = !normal;
            btnInHD.Enabled = normal;
            btnDong.Enabled = true;

            btnThemVaoHD.Enabled = !normal;
            btnXoaKhoiHD.Enabled = !normal;

            cboMaNhanVien.Enabled = !normal;
            cboMaKhachHang.Enabled = !normal;
            txtBKS.ReadOnly = normal;
            txtSoKM.ReadOnly = normal;
            cboLoaiXe.Enabled = !normal;
            txtGiamGia.ReadOnly = normal;

            btnThemHD.Invalidate();
            btnLuuHD.Invalidate();
            btnHuyHD.Invalidate();
            btnInHD.Invalidate();
            btnThemVaoHD.Invalidate();
            btnXoaKhoiHD.Invalidate();
        }
        #endregion

        #region Giao Diện & Đồng Bộ Màu Sắc Theo Hệ Thống
        private static readonly Color MauXanhChinh = Color.FromArgb(30, 108, 176);     // #1E6CB0 Primary Brand Blue
        private static readonly Color MauXanhDam = Color.FromArgb(30, 85, 130);       // #1E5582 Button text
        private static readonly Color MauVienNhe = Color.FromArgb(216, 226, 236);     // Border light
        private static readonly Color MauVienNut = Color.FromArgb(191, 207, 220);     // Button border
        private static readonly Color MauDoHuy = Color.FromArgb(164, 59, 59);         // Danger text
        private static readonly Color MauVienDo = Color.FromArgb(218, 172, 172);      // Danger border
        private static readonly Color MauNenForm = Color.FromArgb(250, 252, 254);     // Form background
        private static readonly Color MauNenPhu = Color.FromArgb(235, 243, 251);      // Soft blue pill & readonly

        private void SetupModernAppearance()
        {
            this.BackColor = MauNenForm;
            this.ForeColor = Color.FromArgb(42, 42, 42);

            // Bo góc và đổ màu mượt cho GroupBoxes
            DinhDangKhung(groupBoxThongTinChung);
            DinhDangKhung(groupBoxHoaDonChiTiet);
            DinhDangKhung(groupBoxKhoPhuTung);
            DinhDangKhung(groupBoxThanhToan);

            // Nút Thao Tác
            DinhDangNut(btnThemHD, false, false);
            DinhDangNut(btnLuuHD, true, false);
            DinhDangNut(btnHuyHD, false, true);
            DinhDangNut(btnInHD, false, false);
            DinhDangNut(btnDong, false, false);
            DinhDangNut(btnThemVaoHD, true, false);
            DinhDangNut(btnXoaKhoiHD, false, true);

            // Thẻ Card Tổng Kết
            pnlCard4.Paint += (s, e) =>
            {
                using (var pen = new Pen(MauXanhChinh, 1.5F))
                {
                    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    e.Graphics.DrawRectangle(pen, 0, 0, pnlCard4.Width - 1, pnlCard4.Height - 1);
                }
            };

            pnlBangChu.Paint += (s, e) =>
            {
                using (var pen = new Pen(MauVienNhe, 1F))
                {
                    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    e.Graphics.DrawRectangle(pen, 0, 0, pnlBangChu.Width - 1, pnlBangChu.Height - 1);
                }
            };
        }

        private static void DinhDangKhung(GroupBox khung)
        {
            khung.BackColor = Color.White;
            khung.Paint += (sender, e) =>
            {
                if (khung.Width < 4 || khung.Height < 4) return;
                var g = e.Graphics;
                var state = g.Save();
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.Clear(Color.White);

                float y = khung.Font.Height / 2F + 2;
                using (var path = DuongBoGoc(new RectangleF(0.5F, y, khung.Width - 1, Math.Max(1, khung.Height - y - 1)), 6))
                using (var pen = new Pen(MauVienNhe))
                {
                    g.DrawPath(pen, path);
                }

                using (var font = new Font(khung.Font, FontStyle.Bold))
                {
                    var size = TextRenderer.MeasureText(g, khung.Text, font, new Size(Math.Max(1, khung.Width - 32), khung.Font.Height + 8), TextFormatFlags.SingleLine | TextFormatFlags.NoPadding);
                    g.FillRectangle(Brushes.White, 12, 0, size.Width + 12, size.Height + 2);
                    TextRenderer.DrawText(g, khung.Text, font, new Rectangle(18, 0, Math.Max(1, khung.Width - 36), size.Height + 2), MauXanhChinh, TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.SingleLine | TextFormatFlags.EndEllipsis | TextFormatFlags.NoPadding);
                }
                g.Restore(state);
            };
            khung.Resize += (sender, e) => khung.Invalidate();
        }

        private sealed class TrangThaiNut
        {
            public bool DiChuot;
            public bool DangNhan;
            public bool LaChinh;
            public bool LaHuy;
        }

        private static readonly ConditionalWeakTable<Button, TrangThaiNut> CtdNut = new ConditionalWeakTable<Button, TrangThaiNut>();

        private static void DinhDangNut(Button nut, bool laChinh, bool laHuy)
        {
            nut.FlatStyle = FlatStyle.Flat;
            nut.FlatAppearance.BorderSize = 1;
            nut.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            nut.Cursor = Cursors.Hand;
            nut.UseVisualStyleBackColor = false;

            TrangThaiNut st;
            if (!CtdNut.TryGetValue(nut, out st))
            {
                st = new TrangThaiNut();
                CtdNut.Add(nut, st);

                nut.MouseEnter += (s, e) => { st.DiChuot = true; nut.Invalidate(); };
                nut.MouseLeave += (s, e) => { st.DiChuot = false; st.DangNhan = false; nut.Invalidate(); };
                nut.MouseDown += (s, e) => { if (e.Button == MouseButtons.Left) st.DangNhan = true; nut.Invalidate(); };
                nut.MouseUp += (s, e) => { st.DangNhan = false; nut.Invalidate(); };
                nut.Paint += (s, e) => VeNut(nut, st, e.Graphics);
                nut.Resize += (s, e) => { BoGocNut(nut); nut.Invalidate(); };
            }

            st.LaChinh = laChinh;
            st.LaHuy = laHuy;
            BoGocNut(nut);
            nut.Invalidate();
        }

        private static void BoGocNut(Button nut)
        {
            if (nut.Width < 2 || nut.Height < 2) return;
            using (var path = DuongBoGoc(new RectangleF(0, 0, nut.Width, nut.Height), 6))
            {
                var old = nut.Region;
                nut.Region = new Region(path);
                if (old != null) old.Dispose();
            }
        }

        private static void VeNut(Button nut, TrangThaiNut st, Graphics g)
        {
            if (nut.Width < 2 || nut.Height < 2) return;

            Color nen, chu, vien;

            if (!nut.Enabled)
            {
                nen = Color.FromArgb(235, 240, 245);
                chu = Color.FromArgb(126, 143, 159);
                vien = Color.FromArgb(213, 222, 231);
            }
            else if (st.LaChinh)
            {
                chu = Color.White;
                vien = MauXanhChinh;
                if (st.DangNhan)
                    nen = Color.FromArgb(19, 76, 128);
                else if (st.DiChuot)
                    nen = Color.FromArgb(24, 91, 151);
                else
                    nen = MauXanhChinh;
            }
            else if (st.LaHuy)
            {
                chu = MauDoHuy;
                vien = MauVienDo;
                if (st.DangNhan)
                    nen = Color.FromArgb(245, 218, 218);
                else if (st.DiChuot)
                    nen = Color.FromArgb(253, 242, 242);
                else
                    nen = Color.White;
            }
            else
            {
                chu = MauXanhDam;
                vien = MauVienNut;
                if (st.DangNhan)
                    nen = Color.FromArgb(216, 234, 249);
                else if (st.DiChuot)
                    nen = Color.FromArgb(235, 243, 251);
                else
                    nen = Color.White;
            }

            var state = g.Save();
            g.Clear(nut.Parent != null ? nut.Parent.BackColor : MauNenForm);
            g.SmoothingMode = SmoothingMode.AntiAlias;

            using (var path = DuongBoGoc(new RectangleF(0.5F, 0.5F, nut.Width - 1, nut.Height - 1), 6))
            using (var brush = new SolidBrush(nen))
            using (var pen = new Pen(vien, 1F))
            {
                g.FillPath(brush, path);
                g.DrawPath(pen, path);
            }

            if (nut.Focused && nut.Enabled)
            {
                using (var path = DuongBoGoc(new RectangleF(3, 3, nut.Width - 6, nut.Height - 6), 3))
                using (var pen = new Pen(st.LaChinh ? Color.White : MauXanhChinh, 1F))
                {
                    pen.DashStyle = DashStyle.Dot;
                    g.DrawPath(pen, path);
                }
            }

            g.Restore(state);

            var flags = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.SingleLine | TextFormatFlags.EndEllipsis | TextFormatFlags.NoPadding;
            TextRenderer.DrawText(g, nut.Text, nut.Font, new Rectangle(4, 0, nut.Width - 8, nut.Height), chu, flags);
        }

        private static GraphicsPath DuongBoGoc(RectangleF rect, float r)
        {
            var path = new GraphicsPath();
            float d = Math.Min(r * 2, Math.Min(rect.Width, rect.Height));
            if (d < 1) { path.AddRectangle(rect); return path; }
            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }
        #endregion
    }
}
