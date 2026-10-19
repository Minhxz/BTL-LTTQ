using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace QLyCuaHangSuaXe.DanhMuc.TiepNhan
{
    public partial class frmTiepNhan : Form
    {
        // Một bảng giữ tạm dữ liệu khi form đang mở, chưa sử dụng CSDL.
        private DataTable bangPhieu = new DataTable();
        private DataRow phieuDangChon;
        private int soPhieuTiepTheo = 5;
        private DateTime ngayNhan;
        private bool dangThem, dangSua, dangNapDuLieu;

        public frmTiepNhan()
        {
            InitializeComponent();
            Text = "Tiếp nhận xe (dữ liệu tạm)";
            txtTimKiem.KeyDown += txtTimKiem_KeyDown;
        }

        private void frmTiepNhan_Load(object sender, EventArgs e)
        {
            TaoBangPhieu();
            cboThoPhuTrach.Items.AddRange(new object[] {
                "Chưa phân công", "Nguyễn Văn Hùng", "Trần Văn Nam", "Lê Văn Bình" });
            TaiDanhSach("");
        }

        private void TaoBangPhieu()
        {
            bangPhieu.Columns.Add("MaPhieu");
            bangPhieu.Columns.Add("NgayNhan", typeof(DateTime));
            bangPhieu.Columns.Add("BienSoXe");
            bangPhieu.Columns.Add("KhachHang");
            bangPhieu.Columns.Add("DienThoai");
            bangPhieu.Columns.Add("LoaiXe");
            bangPhieu.Columns.Add("TenTho");
            bangPhieu.Columns.Add("TrangThai");
            bangPhieu.Columns.Add("TinhTrangXe");

            // Bốn phiếu mẫu để xem và thử các nút khi chưa có CSDL.
            bangPhieu.Rows.Add("TN-0001", DateTime.Today.AddHours(8).AddMinutes(30), "29-B1 123.45",
                "Nguyễn Văn An", "0912345670", "Xe ga", "Nguyễn Văn Hùng", "Đang sửa", "Xe khó nổ máy.");
            bangPhieu.Rows.Add("TN-0002", DateTime.Today.AddHours(9).AddMinutes(15), "30-B2 456.78",
                "Trần Minh Đức", "0912345678", "Xe số", "Trần Văn Nam", "Đang sửa",
                "Phanh sau kêu, xe rung khi chạy chậm. Khách đề nghị kiểm tra phanh và bánh sau.");
            bangPhieu.Rows.Add("TN-0003", DateTime.Today.AddHours(9).AddMinutes(40), "29-C1 882.16",
                "Lê Thị Hoa", "0987654321", "Xe ga", "Chưa phân công", "Chờ khám", "Lốp sau bị xì hơi.");
            bangPhieu.Rows.Add("TN-0004", DateTime.Today.AddHours(10).AddMinutes(5), "30-F1 236.89",
                "Phạm Hoàng Long", "0901234567", "Xe côn tay", "Lê Văn Bình", "Hoàn thành", "Kiểm tra tiếng kêu ở xích.");
        }

        // Lọc bằng vòng lặp rồi gán bảng kết quả cho DataGridView.
        private void TaiDanhSach(string maCanChon)
        {
            string tuKhoa = txtTimKiem.Text.Trim().ToLower();
            DataTable ketQua = bangPhieu.Clone();
            foreach (DataRow dong in bangPhieu.Rows)
            {
                if (dong["MaPhieu"].ToString().ToLower().Contains(tuKhoa)
                    || dong["BienSoXe"].ToString().ToLower().Contains(tuKhoa)
                    || dong["KhachHang"].ToString().ToLower().Contains(tuKhoa))
                    ketQua.ImportRow(dong);
            }

            dangNapDuLieu = true;
            dgvPhieu.DataSource = ketQua;
            foreach (DataGridViewRow dong in dgvPhieu.Rows)
            {
                if (dong.Cells["colMaPhieu"].Value.ToString() == maCanChon)
                    dgvPhieu.CurrentCell = dong.Cells[0];
            }
            dangNapDuLieu = false;
            HienThiPhieu();
            DatCheDo(false, false);
        }

        private void HienThiPhieu()
        {
            phieuDangChon = null;
            if (dgvPhieu.CurrentRow != null)
            {
                string ma = dgvPhieu.CurrentRow.Cells["colMaPhieu"].Value.ToString();
                foreach (DataRow dong in bangPhieu.Rows)
                    if (dong["MaPhieu"].ToString() == ma) phieuDangChon = dong;
            }

            dangNapDuLieu = true;
            txtMaPhieu.Text = phieuDangChon == null ? "" : phieuDangChon["MaPhieu"].ToString();
            ngayNhan = phieuDangChon == null ? DateTime.Now : (DateTime)phieuDangChon["NgayNhan"];
            txtNgayNhan.Text = phieuDangChon == null ? "" : ngayNhan.ToString("dd/MM/yyyy HH:mm");
            txtBienSoXe.Text = phieuDangChon == null ? "" : phieuDangChon["BienSoXe"].ToString();
            txtKhachHang.Text = phieuDangChon == null ? "" : phieuDangChon["KhachHang"].ToString();
            txtDienThoai.Text = phieuDangChon == null ? "" : phieuDangChon["DienThoai"].ToString();
            cboLoaiXe.SelectedItem = phieuDangChon == null ? "Xe số" : phieuDangChon["LoaiXe"].ToString();
            cboThoPhuTrach.SelectedItem = phieuDangChon == null ? "Chưa phân công" : phieuDangChon["TenTho"].ToString();
            cboTrangThai.SelectedItem = phieuDangChon == null ? "Chờ khám" : phieuDangChon["TrangThai"].ToString();
            rtbTinhTrangXe.Text = phieuDangChon == null ? "" : phieuDangChon["TinhTrangXe"].ToString();
            dangNapDuLieu = false;
        }

        // Xem phiếu thì khóa nhập; bấm Thêm mới/Sửa mới cho nhập và Lưu.
        private void DatCheDo(bool them, bool sua)
        {
            dangThem = them;
            dangSua = sua;
            bool choNhap = them || sua;
            txtBienSoXe.ReadOnly = !choNhap;
            txtKhachHang.ReadOnly = !choNhap;
            txtDienThoai.ReadOnly = !choNhap;
            rtbTinhTrangXe.ReadOnly = !choNhap;
            cboLoaiXe.Enabled = choNhap;
            cboThoPhuTrach.Enabled = choNhap;
            cboTrangThai.Enabled = choNhap;
            btnThemMoi.Enabled = !choNhap;
            btnSua.Enabled = !choNhap && phieuDangChon != null;
            btnXoa.Enabled = !choNhap && phieuDangChon != null;
            btnLuu.Enabled = choNhap;
            btnLuu.BackColor = choNhap ? Color.FromArgb(30, 108, 176) : Color.FromArgb(235, 239, 244);
            btnLuu.ForeColor = choNhap ? Color.White : Color.Gray;
            dgvPhieu.Enabled = !choNhap;
            txtTimKiem.Enabled = !choNhap;
            btnTimKiem.Enabled = !choNhap;
        }

        private void TaoPhieuMoi()
        {
            dangNapDuLieu = true;
            // Hiện mã ngay khi thêm mới, người dùng không cần tự nhập.
            txtMaPhieu.Text = "TN-" + soPhieuTiepTheo.ToString("0000");
            ngayNhan = DateTime.Now;
            txtNgayNhan.Text = ngayNhan.ToString("dd/MM/yyyy HH:mm");
            txtBienSoXe.Clear();
            txtKhachHang.Clear();
            txtDienThoai.Clear();
            rtbTinhTrangXe.Clear();
            cboLoaiXe.SelectedIndex = 0;
            cboThoPhuTrach.SelectedIndex = 0;
            cboTrangThai.SelectedIndex = 0;
            dangNapDuLieu = false;
            DatCheDo(true, false);
            HienThongBao("Nhập thông tin xe mới rồi bấm Lưu.", false);
            txtBienSoXe.Focus();
        }

        private string KiemTraThongTin()
        {
            if (txtBienSoXe.Text.Trim() == "") return "Vui lòng nhập biển số xe.";
            string soDienThoai = txtDienThoai.Text.Trim();
            foreach (char kyTu in soDienThoai)
                if (kyTu < '0' || kyTu > '9') return "Số điện thoại chỉ được chứa chữ số.";
            if (soDienThoai != "" && (soDienThoai.Length < 8 || soDienThoai.Length > 15))
                return "Số điện thoại phải có từ 8 đến 15 chữ số.";
            if (cboLoaiXe.SelectedIndex < 0) return "Vui lòng chọn loại xe.";
            if (cboTrangThai.SelectedIndex < 0) return "Vui lòng chọn trạng thái.";
            if (cboTrangThai.Text != "Chờ khám" && cboThoPhuTrach.SelectedIndex <= 0)
                return "Hãy phân công thợ trước khi chuyển sang Đang sửa hoặc Hoàn thành.";
            return "";
        }

        private bool LuuPhieu()
        {
            if (!dangThem && !dangSua) return false;
            string loi = KiemTraThongTin();
            if (loi != "") { HienThongBao(loi, true); return false; }
            bool laSuaPhieu = dangSua;

            DataRow dong = phieuDangChon;
            if (dangThem)
            {
                dong = bangPhieu.NewRow();
                dong["MaPhieu"] = txtMaPhieu.Text;
                dong["NgayNhan"] = ngayNhan;
            }
            dong["BienSoXe"] = txtBienSoXe.Text.Trim().ToUpper();
            dong["KhachHang"] = txtKhachHang.Text.Trim();
            dong["DienThoai"] = txtDienThoai.Text.Trim();
            dong["LoaiXe"] = cboLoaiXe.Text;
            dong["TenTho"] = cboThoPhuTrach.Text;
            dong["TrangThai"] = cboTrangThai.Text;
            dong["TinhTrangXe"] = rtbTinhTrangXe.Text.Trim();
            if (dangThem)
            {
                bangPhieu.Rows.Add(dong);
                soPhieuTiepTheo++;
            }

            string ma = dong["MaPhieu"].ToString();
            txtTimKiem.Clear();
            TaiDanhSach(ma);
            string thongBao = laSuaPhieu ? "Sửa phiếu thành công." : "Lưu phiếu thành công.";
            HienThongBao(thongBao + " Mã phiếu: " + ma, false);
            MessageBox.Show(this, thongBao, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return true;
        }

        private void btnThemMoi_Click(object sender, EventArgs e) { TaoPhieuMoi(); }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (phieuDangChon == null) return;
            DatCheDo(false, true);
            HienThongBao("Đang sửa phiếu " + txtMaPhieu.Text + ".", false);
            txtBienSoXe.Focus();
        }

        private void btnLuu_Click(object sender, EventArgs e) { LuuPhieu(); }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (phieuDangChon == null) return;
            if (MessageBox.Show("Xóa phiếu " + txtMaPhieu.Text + "?", "Xóa phiếu",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            bangPhieu.Rows.Remove(phieuDangChon);
            TaiDanhSach("");
            HienThongBao("Đã xóa phiếu.", false);
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            TaiDanhSach("");
            HienThongBao("Tìm thấy " + dgvPhieu.Rows.Count + " phiếu.", false);
        }

        private void txtTimKiem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; btnTimKiem.PerformClick(); }
        }

        private void dgvPhieu_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dangNapDuLieu || dangThem || dangSua) return;
            HienThiPhieu();
            DatCheDo(false, false);
            HienThongBao("", false);
        }

        private void HienThongBao(string noiDung, bool laLoi)
        {
            lblThongBao.ForeColor = laLoi ? Color.Firebrick : Color.FromArgb(30, 108, 176);
            lblThongBao.Text = noiDung == "" ? "Dữ liệu tạm; đóng form sẽ mất thay đổi." : noiDung;
        }

    }
}
