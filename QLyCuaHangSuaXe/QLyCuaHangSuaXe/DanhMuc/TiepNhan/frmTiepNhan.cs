using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Windows.Forms;

namespace QLyCuaHangSuaXe.DanhMuc.TiepNhan
{
    public partial class frmTiepNhan : Form
    {
        private DataTable dtPhuTung;
        private DataTable dtDichVu;
        private List<PhieuSuaChua> danhSachPhieu = new List<PhieuSuaChua>();
        private int soPhieu = 1, soPhuTung = 1, soDichVu = 1;
        private bool dangNapDuLieu = true;
        private bool coThayDoi = false;
        private bool daKhoiTao = false;
        private bool dangChuyenThanhToan = false;
        private CultureInfo vanHoa = CultureInfo.GetCultureInfo("vi-VN");

        // Form thu ngân sẽ lấy thông tin từ thuộc tính này khi ghép bài.
        public PhieuSuaChua PhieuChuyenThanhToan { get; private set; }

        public frmTiepNhan()
        {
            InitializeComponent();
        }

        private void frmTiepNhan_Load(object sender, EventArgs e)
        {
            if (daKhoiTao) return;
            daKhoiTao = true;
            dtPhuTung = TaoBangChiTiet(true);
            dtDichVu = TaoBangChiTiet(false);
            dgvPhuTung.DataSource = dtPhuTung;
            dgvDichVu.DataSource = dtDichVu;
            cboTrangThai.Items.AddRange(new object[] {
                "Mới tiếp nhận", "Đang sửa", "Đã sửa xong", "Chờ thanh toán" });
            CapNhatDanhSachPhieu("");
            TaoPhieuMoi();
        }

        // Tạo các cột để hai DataGridView hiển thị dữ liệu.
        private DataTable TaoBangChiTiet(bool laPhuTung)
        {
            DataTable bang = new DataTable();
            bang.Columns.Add(laPhuTung ? "MaPT" : "MaDV", typeof(string));
            bang.Columns.Add(laPhuTung ? "TenPhuTung" : "TenDichVu", typeof(string));
            if (laPhuTung) bang.Columns.Add("DonViTinh", typeof(string));
            bang.Columns.Add("SoLuong", typeof(int));
            bang.Columns.Add("DonGia", typeof(decimal));
            bang.Columns.Add("ThanhTien", typeof(decimal));
            return bang;
        }

        private void ThongTin_ThayDoi(object sender, EventArgs e)
        {
            if (dangNapDuLieu) return;
            coThayDoi = true;
            PhieuChuyenThanhToan = null;
        }

        private void TaoPhieuMoi()
        {
            dangNapDuLieu = true;
            txtMaPhieu.Text = "TN-" + soPhieu.ToString("0000");
            soPhieu++;
            dtpNgayNhan.Value = DateTime.Now;
            dtpHenTraXe.Value = dtpNgayNhan.Value.AddHours(2);
            cboTrangThai.SelectedItem = "Mới tiếp nhận";
            txtKhachHang.Clear();
            txtDienThoai.Clear();
            txtBienSoXe.Clear();
            txtDongXe.Clear();
            nudSoKm.Value = 0;
            txtThoPhuTrach.Clear();
            txtTinhTrangXe.Clear();
            txtNoiDungSuaChua.Clear();
            txtTenPhuTung.Clear();
            txtDonViTinh.Clear();
            txtTenDichVu.Clear();
            nudSoLuongPhuTung.Value = 1;
            nudSoLuongDichVu.Value = 1;
            nudDonGiaPhuTung.Value = 0;
            nudDonGiaDichVu.Value = 0;
            dtPhuTung.Clear();
            dtDichVu.Clear();
            TinhTongTien();
            PhieuChuyenThanhToan = null;
            dangNapDuLieu = false;
            coThayDoi = false;
        }

        private void btnThemPhuTung_Click(object sender, EventArgs e)
        {
            string ten = txtTenPhuTung.Text.Trim();
            string donVi = txtDonViTinh.Text.Trim();
            int soLuong = (int)nudSoLuongPhuTung.Value;
            decimal donGia = nudDonGiaPhuTung.Value;
            if (ten == "" || donVi == "" || donGia <= 0)
            {
                MessageBox.Show("Nhập tên, đơn vị tính và đơn giá phụ tùng lớn hơn 0.");
                return;
            }
            // Cùng tên, đơn vị và giá thì cộng số lượng vào dòng đã có.
            foreach (DataRow dong in dtPhuTung.Rows)
            {
                if (string.Equals(dong["TenPhuTung"].ToString(), ten, StringComparison.OrdinalIgnoreCase)
                    && string.Equals(dong["DonViTinh"].ToString(), donVi, StringComparison.OrdinalIgnoreCase)
                    && (decimal)dong["DonGia"] == donGia)
                {
                    int tongSoLuong = (int)dong["SoLuong"] + soLuong;
                    if (tongSoLuong > 999) { MessageBox.Show("Mỗi dòng tối đa 999 phụ tùng."); return; }
                    dong["SoLuong"] = tongSoLuong;
                    dong["ThanhTien"] = tongSoLuong * donGia;
                    CapNhatSauKhiSuaChiTiet();
                    return;
                }
            }
            dtPhuTung.Rows.Add("PT" + soPhuTung.ToString("000"), ten, donVi, soLuong, donGia, soLuong * donGia);
            soPhuTung++;
            CapNhatSauKhiSuaChiTiet();
        }

        private void btnThemDichVu_Click(object sender, EventArgs e)
        {
            string ten = txtTenDichVu.Text.Trim();
            int soLuong = (int)nudSoLuongDichVu.Value;
            decimal donGia = nudDonGiaDichVu.Value;
            if (ten == "" || donGia <= 0)
            {
                MessageBox.Show("Nhập tên dịch vụ và đơn giá lớn hơn 0.");
                return;
            }
            foreach (DataRow dong in dtDichVu.Rows)
            {
                if (string.Equals(dong["TenDichVu"].ToString(), ten, StringComparison.OrdinalIgnoreCase)
                    && (decimal)dong["DonGia"] == donGia)
                {
                    int tongSoLuong = (int)dong["SoLuong"] + soLuong;
                    if (tongSoLuong > 999) { MessageBox.Show("Mỗi dòng tối đa 999 dịch vụ."); return; }
                    dong["SoLuong"] = tongSoLuong;
                    dong["ThanhTien"] = tongSoLuong * donGia;
                    CapNhatSauKhiSuaChiTiet();
                    return;
                }
            }
            dtDichVu.Rows.Add("DV" + soDichVu.ToString("000"), ten, soLuong, donGia, soLuong * donGia);
            soDichVu++;
            CapNhatSauKhiSuaChiTiet();
        }

        private void btnXoaPhuTung_Click(object sender, EventArgs e)
        {
            if (dgvPhuTung.SelectedRows.Count == 0) { MessageBox.Show("Chọn dòng phụ tùng cần xóa."); return; }
            dtPhuTung.Rows.RemoveAt(dgvPhuTung.SelectedRows[0].Index);
            CapNhatSauKhiSuaChiTiet();
        }

        private void btnXoaDichVu_Click(object sender, EventArgs e)
        {
            if (dgvDichVu.SelectedRows.Count == 0) { MessageBox.Show("Chọn dòng dịch vụ cần xóa."); return; }
            dtDichVu.Rows.RemoveAt(dgvDichVu.SelectedRows[0].Index);
            CapNhatSauKhiSuaChiTiet();
        }

        private void CapNhatSauKhiSuaChiTiet()
        {
            TinhTongTien();
            coThayDoi = true;
            PhieuChuyenThanhToan = null;
        }

        private decimal TinhTienBang(DataTable bang)
        {
            decimal tong = 0;
            foreach (DataRow dong in bang.Rows) tong += (decimal)dong["ThanhTien"];
            return tong;
        }

        private void TinhTongTien()
        {
            decimal tienPhuTung = TinhTienBang(dtPhuTung);
            decimal tienCong = TinhTienBang(dtDichVu);
            txtTienPhuTung.Text = tienPhuTung.ToString("N0", vanHoa) + " đ";
            txtTienCong.Text = tienCong.ToString("N0", vanHoa) + " đ";
            txtTongCong.Text = (tienPhuTung + tienCong).ToString("N0", vanHoa) + " đ";
        }

        // Trả về lỗi đầu tiên; chuỗi rỗng nghĩa là thông tin hợp lệ.
        private string KiemTraThongTin(bool deThanhToan)
        {
            if (txtKhachHang.Text.Trim() == "") return "Vui lòng nhập tên khách hàng.";
            int soChuSo = 0;
            foreach (char kyTu in txtDienThoai.Text.Trim())
            {
                if (kyTu >= '0' && kyTu <= '9') soChuSo++;
                else if (kyTu != '+' && kyTu != '-' && kyTu != ' ' && kyTu != '(' && kyTu != ')')
                    return "Số điện thoại không hợp lệ.";
            }
            if (soChuSo < 8 || soChuSo > 15) return "Số điện thoại phải có từ 8 đến 15 chữ số.";
            if (txtBienSoXe.Text.Trim() == "") return "Vui lòng nhập biển số xe.";
            if (txtDongXe.Text.Trim() == "") return "Vui lòng nhập dòng xe.";
            if (txtNguoiTiepNhan.Text.Trim() == "") return "Vui lòng nhập người tiếp nhận.";
            if (txtThoPhuTrach.Text.Trim() == "") return "Vui lòng nhập thợ phụ trách.";
            if (txtTinhTrangXe.Text.Trim() == "") return "Vui lòng nhập tình trạng xe.";
            if (dtpHenTraXe.Value < dtpNgayNhan.Value) return "Hẹn trả xe không được trước ngày nhận.";
            if (deThanhToan)
            {
                if (dtPhuTung.Rows.Count == 0 && dtDichVu.Rows.Count == 0)
                    return "Thêm ít nhất một phụ tùng hoặc dịch vụ trước khi chuyển thanh toán.";
                if (txtNoiDungSuaChua.Text.Trim() == "") return "Vui lòng nhập nội dung sửa chữa.";
            }
            return "";
        }

        private PhieuSuaChua LayThongTinPhieu()
        {
            PhieuSuaChua phieu = new PhieuSuaChua();
            phieu.MaPhieu = txtMaPhieu.Text;
            phieu.NgayNhan = dtpNgayNhan.Value;
            phieu.HenTraXe = dtpHenTraXe.Value;
            phieu.NguoiTiepNhan = txtNguoiTiepNhan.Text.Trim();
            phieu.TrangThai = cboTrangThai.Text;
            phieu.KhachHang = txtKhachHang.Text.Trim();
            phieu.DienThoai = txtDienThoai.Text.Trim();
            phieu.BienSoXe = txtBienSoXe.Text.Trim();
            phieu.DongXe = txtDongXe.Text.Trim();
            phieu.SoKm = (int)nudSoKm.Value;
            phieu.ThoPhuTrach = txtThoPhuTrach.Text.Trim();
            phieu.TinhTrangXe = txtTinhTrangXe.Text.Trim();
            phieu.NoiDungSuaChua = txtNoiDungSuaChua.Text.Trim();
            // Copy để sửa trên form không làm thay đổi phiếu đã lưu.
            phieu.PhuTung = dtPhuTung.Copy();
            phieu.DichVu = dtDichVu.Copy();
            phieu.TienPhuTung = TinhTienBang(dtPhuTung);
            phieu.TienCong = TinhTienBang(dtDichVu);
            return phieu;
        }

        private bool LuuPhieu(bool deThanhToan)
        {
            string loi = KiemTraThongTin(deThanhToan || cboTrangThai.Text == "Chờ thanh toán");
            if (loi != "") { MessageBox.Show(loi, "Kiểm tra thông tin"); return false; }
            if (deThanhToan)
            {
                dangNapDuLieu = true;
                cboTrangThai.SelectedItem = "Chờ thanh toán";
                dangNapDuLieu = false;
            }
            PhieuSuaChua phieu = LayThongTinPhieu();
            int viTri = -1;
            for (int i = 0; i < danhSachPhieu.Count; i++)
            {
                if (danhSachPhieu[i].MaPhieu == phieu.MaPhieu) { viTri = i; break; }
            }
            if (viTri == -1) danhSachPhieu.Add(phieu);
            else danhSachPhieu[viTri] = phieu;
            CapNhatDanhSachPhieu(phieu.MaPhieu);
            coThayDoi = false;
            return true;
        }

        private void CapNhatDanhSachPhieu(string maChon)
        {
            cboPhieuDaLuu.Items.Clear();
            cboPhieuDaLuu.Items.Add("— Chọn mã phiếu —");
            cboPhieuDaLuu.SelectedIndex = 0;
            foreach (PhieuSuaChua phieu in danhSachPhieu)
            {
                cboPhieuDaLuu.Items.Add(phieu);
                if (phieu.MaPhieu == maChon) cboPhieuDaLuu.SelectedItem = phieu;
            }
        }

        private void btnLuuPhieu_Click(object sender, EventArgs e)
        {
            if (LuuPhieu(false)) MessageBox.Show("Đã lưu phiếu " + txtMaPhieu.Text + " trong phiên hiện tại.");
        }

        private bool ChoPhepDoiPhieu()
        {
            if (!coThayDoi) return true;
            DialogResult chon = MessageBox.Show("Lưu phiếu hiện tại trước khi tiếp tục?", "Thông tin chưa lưu",
                MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (chon == DialogResult.Cancel) return false;
            if (chon == DialogResult.Yes) return LuuPhieu(false);
            return true;
        }

        private void btnPhieuMoi_Click(object sender, EventArgs e)
        {
            if (ChoPhepDoiPhieu()) TaoPhieuMoi();
        }

        private void btnMoPhieu_Click(object sender, EventArgs e)
        {
            PhieuSuaChua phieu = cboPhieuDaLuu.SelectedItem as PhieuSuaChua;
            if (phieu == null) { MessageBox.Show("Chọn một phiếu đã lưu trước."); return; }
            string maCanMo = phieu.MaPhieu;
            if (!ChoPhepDoiPhieu()) return;
            // Lưu trong hộp thoại trên có thể đã cập nhật lại danh sách.
            foreach (PhieuSuaChua daLuu in danhSachPhieu)
            {
                if (daLuu.MaPhieu == maCanMo) { HienThiPhieu(daLuu); break; }
            }
            CapNhatDanhSachPhieu(maCanMo);
        }

        private void HienThiPhieu(PhieuSuaChua phieu)
        {
            dangNapDuLieu = true;
            txtMaPhieu.Text = phieu.MaPhieu;
            dtpNgayNhan.Value = phieu.NgayNhan;
            dtpHenTraXe.Value = phieu.HenTraXe;
            txtNguoiTiepNhan.Text = phieu.NguoiTiepNhan;
            cboTrangThai.SelectedItem = phieu.TrangThai;
            txtKhachHang.Text = phieu.KhachHang;
            txtDienThoai.Text = phieu.DienThoai;
            txtBienSoXe.Text = phieu.BienSoXe;
            txtDongXe.Text = phieu.DongXe;
            nudSoKm.Value = phieu.SoKm;
            txtThoPhuTrach.Text = phieu.ThoPhuTrach;
            txtTinhTrangXe.Text = phieu.TinhTrangXe;
            txtNoiDungSuaChua.Text = phieu.NoiDungSuaChua;
            txtTenPhuTung.Clear();
            txtDonViTinh.Clear();
            txtTenDichVu.Clear();
            nudSoLuongPhuTung.Value = 1;
            nudSoLuongDichVu.Value = 1;
            nudDonGiaPhuTung.Value = 0;
            nudDonGiaDichVu.Value = 0;
            dtPhuTung = phieu.PhuTung.Copy();
            dtDichVu = phieu.DichVu.Copy();
            dgvPhuTung.DataSource = dtPhuTung;
            dgvDichVu.DataSource = dtDichVu;
            TinhTongTien();
            PhieuChuyenThanhToan = null;
            dangNapDuLieu = false;
            coThayDoi = false;
        }

        private bool ChuanBiChuyenThanhToan()
        {
            if (!LuuPhieu(true)) return false;
            PhieuChuyenThanhToan = LayThongTinPhieu();
            return true;
        }

        private void btnChuyenThanhToan_Click(object sender, EventArgs e)
        {
            if (!ChuanBiChuyenThanhToan()) return;
            if (Modal)
            {
                // Form gọi nhận DialogResult.OK và lấy PhieuChuyenThanhToan.
                dangChuyenThanhToan = true;
                DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Đã chuẩn bị phiếu " + PhieuChuyenThanhToan.MaPhieu
                    + ". Chưa liên kết form thu ngân; dữ liệu nằm trong PhieuChuyenThanhToan.");
            }
        }

        private void btnDong_Click(object sender, EventArgs e) { Close(); }

        private void frmTiepNhan_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (dangChuyenThanhToan || !daKhoiTao) return;
            if (coThayDoi || danhSachPhieu.Count > 0)
                e.Cancel = MessageBox.Show("Dữ liệu đang lưu trong bộ nhớ. Đóng form sẽ mất dữ liệu. Bạn có muốn đóng?",
                    "Đóng form", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes;
        }
    }
}
