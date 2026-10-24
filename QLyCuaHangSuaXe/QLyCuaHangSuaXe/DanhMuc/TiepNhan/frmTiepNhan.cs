using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace QLyCuaHangSuaXe.DanhMuc.TiepNhan
{
    public partial class frmTiepNhan : Form
    {
        // Giữ dữ liệu tạm khi form đang mở, chưa kết nối SQL Server.
        private DataTable bangPhieu = new DataTable();
        private int soPhieuTiepTheo = 5;
        private DateTime ngayNhan;
        private bool dangThem;
        private bool dangTaiDanhSach;
        private string noiDungIn;
        private int viTriIn;

        public frmTiepNhan()
        {
            InitializeComponent();
            Text = "Tiếp nhận xe";
        }

        private void frmTiepNhan_Load(object sender, EventArgs e)
        {
            cboThoPhuTrach.Items.AddRange(new object[] {
                "Chưa phân công", "Nguyễn Văn Hùng", "Trần Văn Nam", "Lê Văn Bình" });

            // Tạo bảng tạm thay cho bảng trong CSDL.
            bangPhieu.Columns.Add("MaPhieu");
            bangPhieu.Columns.Add("NgayNhan", typeof(DateTime));
            bangPhieu.Columns.Add("BienSoXe");
            bangPhieu.Columns.Add("KhachHang");
            bangPhieu.Columns.Add("DienThoai");
            bangPhieu.Columns.Add("LoaiXe");
            bangPhieu.Columns.Add("TenTho");
            bangPhieu.Columns.Add("TrangThai");
            bangPhieu.Columns.Add("TinhTrangXe");
            bangPhieu.Columns.Add("DongXe");
            bangPhieu.Columns.Add("SoKm", typeof(int));
            bangPhieu.Columns.Add("HenTra", typeof(DateTime));
            bangPhieu.Columns.Add("NhacHen");

            // Đặt mã phiếu làm khóa để tìm đúng dòng khi sửa hoặc xóa.
            bangPhieu.PrimaryKey = new DataColumn[] { bangPhieu.Columns["MaPhieu"] };

            // Dữ liệu mẫu có cả xe đúng hẹn, quá hẹn và đã hoàn thành để chạy thử.
            DateTime bayGio = DateTime.Now;
            bangPhieu.Rows.Add("TN-0001", bayGio.AddHours(-3), "29-B1 123.45",
                "Nguyễn Văn An", "0912345670", "Xe ga", "Nguyễn Văn Hùng", "Đang sửa", "Xe khó nổ máy.",
                "Honda Vision", 18000, bayGio.AddHours(2), "");
            bangPhieu.Rows.Add("TN-0002", bayGio.AddHours(-5), "30-B2 456.78",
                "Trần Minh Đức", "0912345678", "Xe số", "Trần Văn Nam", "Đang sửa",
                "Phanh sau kêu, xe rung khi chạy chậm. Khách đề nghị kiểm tra phanh và bánh sau.",
                "Honda Wave RSX", 12500, bayGio.AddHours(-1), "");
            bangPhieu.Rows.Add("TN-0003", bayGio.AddMinutes(-30), "29-C1 882.16",
                "Lê Thị Hoa", "0987654321", "Xe ga", "Chưa phân công", "Chờ khám", "Lốp sau bị xì hơi.",
                "Yamaha Grande", 8000, bayGio.AddHours(4), "");
            bangPhieu.Rows.Add("TN-0004", bayGio.AddDays(-1), "30-F1 236.89",
                "Phạm Hoàng Long", "0901234567", "Xe côn tay", "Lê Văn Bình", "Hoàn thành", "Kiểm tra tiếng kêu ở xích.",
                "Honda Winner X", 22000, bayGio.AddHours(-2), "");

            cboLocTrangThai.SelectedIndex = 0;
            TaiDanhSach("");
            lblThongBao.Text = "Dữ liệu tạm; đóng form sẽ mất thay đổi.";
            timerNhacHen.Start();
        }

        private void TaiDanhSach(string maCanChon)
        {
            // Khi đổi DataSource, đợi lưới tải xong mới đọc dòng đang chọn.
            dangTaiDanhSach = true;
            CapNhatNhacHen();
            string tuKhoa = txtTimKiem.Text.Trim().ToLower();
            DataTable ketQua = bangPhieu.Clone();

            // Sao chép các phiếu phù hợp sang bảng hiển thị.
            foreach (DataRow dong in bangPhieu.Rows)
            {
                bool dungTuKhoa = dong["MaPhieu"].ToString().ToLower().Contains(tuKhoa)
                    || dong["BienSoXe"].ToString().ToLower().Contains(tuKhoa)
                    || dong["KhachHang"].ToString().ToLower().Contains(tuKhoa)
                    || dong["DienThoai"].ToString().Contains(tuKhoa);
                bool dungTrangThai = cboLocTrangThai.Text == "Tất cả"
                    || dong["TrangThai"].ToString() == cboLocTrangThai.Text;
                if (dungTuKhoa && dungTrangThai)
                    ketQua.ImportRow(dong);
            }

            DatCheDo(false);
            dgvPhieu.DataSource = ketQua;
            foreach (DataGridViewRow dong in dgvPhieu.Rows)
            {
                if (dong.Cells["colMaPhieu"].Value.ToString() == maCanChon)
                    dgvPhieu.CurrentCell = dong.Cells[0];
            }
            dangTaiDanhSach = false;
            dgvPhieu_CurrentCellChanged(dgvPhieu, EventArgs.Empty);
        }

        private void XoaTrangForm()
        {
            txtMaPhieu.Clear();
            txtNgayNhan.Clear();
            txtBienSoXe.Clear();
            txtKhachHang.Clear();
            txtDienThoai.Clear();
            txtDongXe.Clear();
            numSoKm.Value = 0;
            dtpHenTra.Value = DateTime.Now.AddHours(2);
            rtbTinhTrangXe.Clear();
            cboLoaiXe.SelectedIndex = 0;
            cboThoPhuTrach.SelectedIndex = 0;
            cboTrangThai.SelectedIndex = 0;
        }

        // true: cho nhập và lưu; false: chỉ xem phiếu.
        private void DatCheDo(bool choNhap)
        {
            txtBienSoXe.ReadOnly = !choNhap;
            txtKhachHang.ReadOnly = !choNhap;
            txtDienThoai.ReadOnly = !choNhap;
            txtDongXe.ReadOnly = !choNhap;
            numSoKm.Enabled = choNhap;
            dtpHenTra.Enabled = choNhap;
            rtbTinhTrangXe.ReadOnly = !choNhap;
            cboLoaiXe.Enabled = choNhap;
            cboThoPhuTrach.Enabled = choNhap;
            cboTrangThai.Enabled = choNhap;
            btnThemMoi.Enabled = !choNhap;
            btnSua.Enabled = !choNhap && dgvPhieu.CurrentRow != null;
            btnXoa.Enabled = !choNhap && dgvPhieu.CurrentRow != null;
            btnLuu.Enabled = choNhap;
            btnInPhieu.Enabled = !choNhap && dgvPhieu.CurrentRow != null;
            btnLuu.BackColor = choNhap ? Color.FromArgb(30, 108, 176) : Color.FromArgb(235, 239, 244);
            btnLuu.ForeColor = choNhap ? Color.White : Color.Gray;
            dgvPhieu.Enabled = !choNhap;
            txtTimKiem.Enabled = !choNhap;
            btnTimKiem.Enabled = !choNhap;
            cboLocTrangThai.Enabled = !choNhap;
            lblCanhBao.Visible = !choNhap;
        }

        private void btnThemMoi_Click(object sender, EventArgs e)
        {
            dangThem = true;
            XoaTrangForm();
            txtMaPhieu.Text = "TN-" + soPhieuTiepTheo.ToString("0000");
            ngayNhan = DateTime.Now;
            txtNgayNhan.Text = ngayNhan.ToString("dd/MM/yyyy HH:mm");
            dtpHenTra.Value = ngayNhan.AddHours(2);
            DatCheDo(true);
            lblThongBao.Text = "Nhập thông tin xe mới rồi bấm Lưu.";
            txtBienSoXe.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvPhieu.CurrentRow == null) return;
            dangThem = false;
            DatCheDo(true);
            lblThongBao.Text = "Đang sửa phiếu " + txtMaPhieu.Text + ".";
            txtBienSoXe.Focus();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (!btnLuu.Enabled) return;

            // Kiểm tra từng ô giống cách làm trong bài Học_viên.
            if (string.IsNullOrWhiteSpace(txtBienSoXe.Text))
            {
                MessageBox.Show("Vui lòng nhập biển số xe.");
                txtBienSoXe.Focus();
                return;
            }

            // Giữ số điện thoại là chuỗi để không mất số 0 ở đầu.
            string soDienThoai = txtDienThoai.Text.Trim();
            foreach (char kyTu in soDienThoai)
            {
                if (kyTu < '0' || kyTu > '9')
                {
                    MessageBox.Show("Số điện thoại chỉ được chứa chữ số.");
                    txtDienThoai.Focus();
                    return;
                }
            }
            if (soDienThoai != "" && (soDienThoai.Length < 8 || soDienThoai.Length > 15))
            {
                MessageBox.Show("Số điện thoại phải có từ 8 đến 15 chữ số.");
                txtDienThoai.Focus();
                return;
            }
            if (cboLoaiXe.SelectedIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn loại xe.");
                cboLoaiXe.Focus();
                return;
            }
            if (cboTrangThai.SelectedIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn trạng thái.");
                cboTrangThai.Focus();
                return;
            }
            if (cboTrangThai.Text != "Chờ khám" && cboThoPhuTrach.SelectedIndex <= 0)
            {
                MessageBox.Show("Hãy phân công thợ trước khi chuyển sang Đang sửa hoặc Hoàn thành.");
                cboThoPhuTrach.Focus();
                return;
            }
            if (dtpHenTra.Value < ngayNhan)
            {
                MessageBox.Show("Hẹn trả xe không được trước ngày nhận xe.");
                dtpHenTra.Focus();
                return;
            }

            // Thêm thì tạo dòng mới, sửa thì tìm dòng theo mã đang hiện.
            DataRow dong;
            if (dangThem)
            {
                dong = bangPhieu.NewRow();
                dong["MaPhieu"] = txtMaPhieu.Text;
                dong["NgayNhan"] = ngayNhan;
            }
            else
            {
                dong = bangPhieu.Rows.Find(txtMaPhieu.Text);
            }

            dong["BienSoXe"] = txtBienSoXe.Text.Trim().ToUpper();
            dong["KhachHang"] = txtKhachHang.Text.Trim();
            dong["DienThoai"] = txtDienThoai.Text.Trim();
            dong["LoaiXe"] = cboLoaiXe.Text;
            dong["TenTho"] = cboThoPhuTrach.Text;
            dong["TrangThai"] = cboTrangThai.Text;
            dong["TinhTrangXe"] = rtbTinhTrangXe.Text.Trim();
            dong["DongXe"] = txtDongXe.Text.Trim();
            dong["SoKm"] = (int)numSoKm.Value;
            dong["HenTra"] = dtpHenTra.Value;

            if (dangThem)
            {
                bangPhieu.Rows.Add(dong);
                soPhieuTiepTheo++;
            }

            string ma = dong["MaPhieu"].ToString();
            string thongBao = dangThem ? "Lưu phiếu thành công." : "Sửa phiếu thành công.";
            txtTimKiem.Clear();
            cboLocTrangThai.SelectedIndex = 0;
            TaiDanhSach(ma);
            lblThongBao.Text = thongBao + " Mã phiếu: " + ma;
            MessageBox.Show(this, thongBao, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvPhieu.CurrentRow == null) return;
            DialogResult traLoi = MessageBox.Show("Xóa phiếu " + txtMaPhieu.Text + "?", "Xóa phiếu",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (traLoi != DialogResult.Yes) return;

            DataRow dong = bangPhieu.Rows.Find(txtMaPhieu.Text);
            bangPhieu.Rows.Remove(dong);
            TaiDanhSach("");
            lblThongBao.Text = "Đã xóa phiếu.";
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            TaiDanhSach("");
            lblThongBao.Text = "Tìm thấy " + dgvPhieu.Rows.Count + " phiếu.";
        }

        private void txtTimKiem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnTimKiem.PerformClick();
            }
        }

        private void dgvPhieu_CurrentCellChanged(object sender, EventArgs e)
        {
            // Khi đang nhập thì không lấy dữ liệu từ dòng khác đè lên ô nhập.
            if (btnLuu.Enabled || dangTaiDanhSach) return;
            if (dgvPhieu.CurrentRow == null)
            {
                XoaTrangForm();
            }
            else
            {
                // Đưa dữ liệu của dòng đang chọn lên ô nhập, như bài Học_viên.
                txtMaPhieu.Text = dgvPhieu.CurrentRow.Cells["colMaPhieu"].Value.ToString();
                ngayNhan = (DateTime)dgvPhieu.CurrentRow.Cells["colNgayNhan"].Value;
                txtNgayNhan.Text = ngayNhan.ToString("dd/MM/yyyy HH:mm");
                txtBienSoXe.Text = dgvPhieu.CurrentRow.Cells["colBienSoXe"].Value.ToString();
                txtKhachHang.Text = dgvPhieu.CurrentRow.Cells["colKhachHang"].Value.ToString();
                cboThoPhuTrach.Text = dgvPhieu.CurrentRow.Cells["colTenTho"].Value.ToString();
                cboTrangThai.Text = dgvPhieu.CurrentRow.Cells["colTrangThai"].Value.ToString();

                // Các cột không hiện trên lưới lấy từ bảng tạm theo mã phiếu.
                DataRow dong = bangPhieu.Rows.Find(txtMaPhieu.Text);
                cboLoaiXe.Text = dong["LoaiXe"].ToString();
                txtDienThoai.Text = dong["DienThoai"].ToString();
                rtbTinhTrangXe.Text = dong["TinhTrangXe"].ToString();
                txtDongXe.Text = dong["DongXe"].ToString();
                numSoKm.Value = (int)dong["SoKm"];
                dtpHenTra.Value = (DateTime)dong["HenTra"];
            }
            DatCheDo(false);
            CapNhatNhacHen();
            lblThongBao.Text = txtMaPhieu.Text == "" ? "Không có phiếu phù hợp."
                : "Đang xem phiếu " + txtMaPhieu.Text + ".";
        }

        private void cboLocTrangThai_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bangPhieu.Rows.Count == 0 || btnLuu.Enabled) return;
            TaiDanhSach("");
        }

        // Cập nhật nhắc hẹn mà không tải lại lưới hoặc làm mất ô đang nhập.
        private void CapNhatNhacHen()
        {
            DateTime bayGio = DateTime.Now;
            foreach (DataRow dong in bangPhieu.Rows)
            {
                if (dong["TrangThai"].ToString() == "Hoàn thành")
                    dong["NhacHen"] = "Đã xong";
                else if ((DateTime)dong["HenTra"] < bayGio)
                    dong["NhacHen"] = "Quá hẹn";
                else
                    dong["NhacHen"] = "Đúng hẹn";
            }

            // Lưới đang hiển thị bảng kết quả lọc, nên cập nhật cả bảng đó.
            DataTable ketQua = dgvPhieu.DataSource as DataTable;
            if (ketQua != null)
            {
                foreach (DataRow dong in ketQua.Rows)
                {
                    DataRow dongGoc = bangPhieu.Rows.Find(dong["MaPhieu"]);
                    if (dongGoc != null && dong["NhacHen"].ToString() != dongGoc["NhacHen"].ToString())
                        dong["NhacHen"] = dongGoc["NhacHen"];
                }
            }
            dgvPhieu.Refresh();
            lblTongPhieu.Text = "Dữ liệu tạm • Hiển thị: " + dgvPhieu.Rows.Count
                + " / " + bangPhieu.Rows.Count + " phiếu";
            if (btnLuu.Enabled) return;

            DataRow phieu = bangPhieu.Rows.Find(txtMaPhieu.Text);
            if (phieu != null && phieu["NhacHen"].ToString() == "Quá hẹn")
            {
                lblCanhBao.Text = "Quá hẹn trả xe — cần liên hệ khách";
                lblCanhBao.BackColor = Color.MistyRose;
                lblCanhBao.ForeColor = Color.Firebrick;
            }
            else
            {
                lblCanhBao.Text = phieu == null ? "Chọn phiếu để xem hẹn trả xe."
                    : phieu["NhacHen"].ToString() == "Đã xong" ? "Xe đã hoàn thành sửa chữa."
                    : "Phiếu còn trong thời gian hẹn trả xe.";
                lblCanhBao.BackColor = Color.FromArgb(235, 241, 247);
                lblCanhBao.ForeColor = Color.FromArgb(30, 108, 176);
            }
        }

        private void timerNhacHen_Tick(object sender, EventArgs e)
        {
            CapNhatNhacHen();
        }

        private void dgvPhieu_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;
            string nhacHen = Convert.ToString(dgvPhieu.Rows[e.RowIndex].Cells["colNhacHen"].Value);
            if (nhacHen == "Quá hẹn")
            {
                e.CellStyle.BackColor = Color.MistyRose;
                e.CellStyle.ForeColor = Color.Firebrick;
                e.CellStyle.SelectionBackColor = Color.FromArgb(255, 210, 210);
                e.CellStyle.SelectionForeColor = Color.Firebrick;
            }
            else if (nhacHen == "Đã xong")
            {
                e.CellStyle.BackColor = Color.Honeydew;
                e.CellStyle.ForeColor = Color.DarkGreen;
                e.CellStyle.SelectionBackColor = Color.FromArgb(210, 240, 210);
                e.CellStyle.SelectionForeColor = Color.DarkGreen;
            }
            else
            {
                e.CellStyle.BackColor = Color.White;
                e.CellStyle.ForeColor = Color.Black;
                e.CellStyle.SelectionBackColor = Color.FromArgb(222, 237, 250);
                e.CellStyle.SelectionForeColor = Color.Black;
            }
        }

        private void btnInPhieu_Click(object sender, EventArgs e)
        {
            if (!btnInPhieu.Enabled || dgvPhieu.CurrentRow == null) return;
            using (PrintDocument inPhieu = new PrintDocument())
            using (PrintPreviewDialog xemTruoc = new PrintPreviewDialog())
            {
                inPhieu.DocumentName = "Phiếu tiếp nhận " + txtMaPhieu.Text;
                inPhieu.DefaultPageSettings.PaperSize = new PaperSize("A4", 827, 1169);
                inPhieu.DefaultPageSettings.Margins = new Margins(55, 55, 55, 55);
                inPhieu.BeginPrint += inPhieu_BeginPrint;
                inPhieu.PrintPage += inPhieu_PrintPage;
                xemTruoc.Document = inPhieu;
                xemTruoc.Text = "Xem trước phiếu tiếp nhận";
                xemTruoc.Size = new Size(1000, 800);
                xemTruoc.StartPosition = FormStartPosition.CenterParent;
                try
                {
                    xemTruoc.ShowDialog(this);
                }
                catch (InvalidPrinterException)
                {
                    MessageBox.Show("Chưa có máy in khả dụng. Hãy cài hoặc chọn máy in mặc định trong Windows.",
                        "In phiếu", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void inPhieu_BeginPrint(object sender, PrintEventArgs e)
        {
            // Chỉ in dữ liệu đã lưu trong bảng, không lấy nội dung đang sửa dở.
            DataRow dong = bangPhieu.Rows.Find(txtMaPhieu.Text);
            if (dong == null)
            {
                e.Cancel = true;
                return;
            }
            viTriIn = 0;
            noiDungIn = "Mã phiếu: " + dong["MaPhieu"]
                + "\nNgày nhận: " + ((DateTime)dong["NgayNhan"]).ToString("dd/MM/yyyy HH:mm")
                + "\nHẹn trả xe: " + ((DateTime)dong["HenTra"]).ToString("dd/MM/yyyy HH:mm")
                + "\n\nKhách hàng: " + dong["KhachHang"]
                + "\nĐiện thoại: " + dong["DienThoai"]
                + "\nBiển số xe: " + dong["BienSoXe"]
                + "\nLoại xe: " + dong["LoaiXe"]
                + "\nHãng / dòng xe: " + dong["DongXe"]
                + "\nSố km: " + ((int)dong["SoKm"]).ToString("N0")
                + "\n\nThợ phụ trách: " + dong["TenTho"]
                + "\nTrạng thái: " + dong["TrangThai"]
                + "\n\nTình trạng xe / yêu cầu của khách:\n" + dong["TinhTrangXe"];
        }

        private void inPhieu_PrintPage(object sender, PrintPageEventArgs e)
        {
            using (Font fontTieuDe = new Font("Segoe UI", 18, FontStyle.Bold))
            using (Font fontNoiDung = new Font("Segoe UI", 11))
            using (StringFormat dinhDang = new StringFormat())
            {
                e.Graphics.DrawString("PHIẾU TIẾP NHẬN XE", fontTieuDe, Brushes.Black,
                    e.MarginBounds.Left, e.MarginBounds.Top);
                e.Graphics.DrawLine(Pens.Gray, e.MarginBounds.Left, e.MarginBounds.Top + 40,
                    e.MarginBounds.Right, e.MarginBounds.Top + 40);
                RectangleF vungIn = new RectangleF(e.MarginBounds.Left, e.MarginBounds.Top + 60,
                    e.MarginBounds.Width, e.MarginBounds.Height - 145);

                // Nếu nội dung dài, in tiếp phần còn lại ở trang sau.
                string conLai = noiDungIn.Substring(viTriIn);
                int soKyTu, soDong;
                e.Graphics.MeasureString(conLai, fontNoiDung, vungIn.Size, dinhDang, out soKyTu, out soDong);
                e.Graphics.DrawString(conLai.Substring(0, soKyTu), fontNoiDung, Brushes.Black, vungIn, dinhDang);
                viTriIn += soKyTu;
                e.HasMorePages = viTriIn < noiDungIn.Length;
                if (!e.HasMorePages)
                {
                    e.Graphics.DrawString("Khách hàng\n(Ký, ghi rõ họ tên)", fontNoiDung, Brushes.Black,
                        e.MarginBounds.Left + 40, e.MarginBounds.Bottom - 65);
                    e.Graphics.DrawString("Người tiếp nhận\n(Ký, ghi rõ họ tên)", fontNoiDung, Brushes.Black,
                        e.MarginBounds.Right - 210, e.MarginBounds.Bottom - 65);
                }
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}
