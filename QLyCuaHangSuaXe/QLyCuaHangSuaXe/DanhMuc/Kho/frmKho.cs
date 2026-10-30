using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using QLyCuaHangSuaXe.LopDungChung;

namespace btl
{
    public partial class frmKho : TrangThietKe
    {
        bool dangTai;
        public frmKho()
        {
            InitializeComponent();
            GanThietKe(dgvPhuTung);
            if (DesignMode || System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
                return;
            GiaoDien.DinhDangBang(dgvLichSuTon);
            GiaoDien.DinhDangBang(dgvLichSuGia);
            dgvLichSuTon.DataBindingComplete += DinhDangLichSu;
            dgvLichSuGia.DataBindingComplete += DinhDangLichSu;
        }
        public override void TaiDuLieu()
        {
            PhienDangNhap.YeuCau("NHAP_KHO");
            string maDangChon = GiaoDien.GiaTriO(dgvPhuTung, "MaPhuTung");
            dangTai = true;
            try
            {
                dgvPhuTung.DataSource = duLieu.DocBang(@"SELECT p.MaPhuTung,p.TenPhuTung,n.TenNhaCungCap AS TenNCC,p.SoLuongTon,p.DonGiaNhap,p.DonGiaBan,p.HanSuDung,p.TrangThai FROM dbo.PhuTung p JOIN dbo.NhaCungCap n ON n.MaNhaCungCap=p.MaNhaCungCap WHERE p.MaPhuTung LIKE @q OR p.TenPhuTung LIKE @q OR n.TenNhaCungCap LIKE @q ORDER BY p.MaPhuTung",
                    XuLyDuLieu.ThamSo("@q", "%" + txtTimPhuTung.Text.Trim() + "%"));
                foreach (DataGridViewRow dong in dgvPhuTung.Rows)
                    if (Convert.ToString(dong.Cells["MaPhuTung"].Value) == maDangChon)
                    {
                        dgvPhuTung.CurrentCell = dong.Cells["TenPhuTung"];
                        break;
                    }
                TaiLichSu();
                txtMaDieuChinhTon.Text = XuLyDuLieu.TaoMaMoi("DC");
                txtMaDieuChinhGia.Text = XuLyDuLieu.TaoMaMoi("DG");
                lblNguoiGhi.Text = "Người ghi: " + PhienDangNhap.MaNhanVien + " — " + PhienDangNhap.TenDangNhap;
            }
            finally { dangTai = false; }
            ChonPhuTung();
        }
        void TaiLichSu()
        {
            string tim = "%" + txtTimPhuTung.Text.Trim() + "%";
            dgvLichSuTon.DataSource = duLieu.DocBang(@"SELECT d.MaDieuChinh,d.NgayDieuChinh,d.MaPhuTung,p.TenPhuTung,d.SoLuongCu,d.SoLuongMoi,d.DonGiaNhap,d.GiaTriChenhLech,d.MaNhanVien,n.HoTen AS TenNhanVien,d.LyDo FROM dbo.DieuChinhTonKho d JOIN dbo.PhuTung p ON p.MaPhuTung=d.MaPhuTung JOIN dbo.NhanVien n ON n.MaNhanVien=d.MaNhanVien WHERE d.MaPhuTung LIKE @q OR p.TenPhuTung LIKE @q OR d.MaDieuChinh LIKE @q OR n.HoTen LIKE @q OR d.LyDo LIKE @q ORDER BY d.NgayDieuChinh DESC,d.MaDieuChinh DESC",
                XuLyDuLieu.ThamSo("@q", tim));
            dgvLichSuGia.DataSource = duLieu.DocBang(@"SELECT d.MaDieuChinhGia,d.NgayDieuChinh,d.MaPhuTung,p.TenPhuTung,d.GiaBanCu,d.GiaBanMoi,d.MaNhanVien,n.HoTen AS TenNhanVien,d.LyDo FROM dbo.DieuChinhGiaBan d JOIN dbo.PhuTung p ON p.MaPhuTung=d.MaPhuTung JOIN dbo.NhanVien n ON n.MaNhanVien=d.MaNhanVien WHERE d.MaPhuTung LIKE @q OR p.TenPhuTung LIKE @q OR d.MaDieuChinhGia LIKE @q OR n.HoTen LIKE @q OR d.LyDo LIKE @q ORDER BY d.NgayDieuChinh DESC,d.MaDieuChinhGia DESC",
                XuLyDuLieu.ThamSo("@q", tim));
            decimal thua = 0, thieu = 0;
            foreach (DataRow dong in ((DataTable)dgvLichSuTon.DataSource).Rows)
            {
                decimal lech = Convert.ToDecimal(dong["GiaTriChenhLech"]);
                if (lech > 0)
                    thua += lech;
                else
                    thieu += lech;
            }
            lblTongTienLech.Text = "Thừa: " + thua.ToString("N0") + " đ   |   Thiếu: " + thieu.ToString("N0") + " đ   |   Chênh lệch: " + (thua + thieu).ToString("N0") + " đ";
        }
        void DinhDangLichSu(object nguoiGui, DataGridViewBindingCompleteEventArgs suKien)
        {
            var bang = (DataGridView)nguoiGui;
            foreach (DataGridViewColumn cot in bang.Columns)
            {
                switch (cot.Name)
                {
                    case "MaDieuChinh":
                    case "MaDieuChinhGia":
                        cot.HeaderText = "Mã điều chỉnh";
                        break;
                    case "NgayDieuChinh":
                        cot.HeaderText = "Ngày điều chỉnh";
                        break;
                    case "SoLuongCu":
                        cot.HeaderText = "Tồn trước";
                        break;
                    case "SoLuongMoi":
                        cot.HeaderText = "Tồn sau";
                        break;
                    case "GiaTriChenhLech":
                        cot.HeaderText = "Tiền chênh lệch (đ)";
                        break;
                    case "GiaBanCu":
                        cot.HeaderText = "Giá bán trước (đ)";
                        break;
                    case "GiaBanMoi":
                        cot.HeaderText = "Giá bán mới (đ)";
                        break;
                    case "LyDo":
                        cot.HeaderText = "Lý do";
                        cot.MinimumWidth = 180;
                        cot.FillWeight = 200;
                        break;
                }
            }
        }
        void ChonPhuTung()
        {
            if (dangTai)
                return;
            var dong = dgvPhuTung.CurrentRow == null ? null : dgvPhuTung.CurrentRow.DataBoundItem as DataRowView;
            grbDieuChinh.Enabled = dong != null;
            grbDieuChinh.Text = dong == null ? "Điều chỉnh tồn và giá bán" : "Điều chỉnh " + dong["MaPhuTung"] + " — " + dong["TenPhuTung"];
            numSoLuongLech.Value = 0;
            numGiaBanMoi.Value = dong == null ? 1 : Math.Min(numGiaBanMoi.Maximum, Math.Max(1, Convert.ToDecimal(dong["DonGiaBan"])));
            txtLyDo.Clear();
        }
        DataRow PhuTungDangChon()
        {
            PhienDangNhap.YeuCau("NHAP_KHO");
            string ma = GiaoDien.GiaTriO(dgvPhuTung, "MaPhuTung");
            if (ma.Length == 0)
                throw new InvalidOperationException("Chọn phụ tùng trước khi điều chỉnh.");
            var bang = duLieu.DocBang("SELECT MaPhuTung,TenPhuTung,SoLuongTon,DonGiaNhap,DonGiaBan FROM dbo.PhuTung WHERE MaPhuTung=@ma", XuLyDuLieu.ThamSo("@ma", ma));
            if (bang.Rows.Count != 1)
                throw new InvalidOperationException("Không tìm thấy phụ tùng. Bấm Làm mới kho để tải lại.");
            return bang.Rows[0];
        }
        string LyDoDieuChinh()
        {
            string lyDo = txtLyDo.Text.Trim();
            if (lyDo.Length == 0 || lyDo.Length > 255)
                throw new InvalidOperationException("Nhập lý do điều chỉnh, tối đa 255 ký tự.");
            return lyDo;
        }
        void DieuChinhTon()
        {
            string lyDo = LyDoDieuChinh();
            int soLech = Convert.ToInt32(numSoLuongLech.Value);
            if (soLech == 0)
                throw new InvalidOperationException("Số lượng tăng/giảm phải khác 0.");
            var phuTung = PhuTungDangChon();
            int tonMoi;
            try
            {
                tonMoi = checked(Convert.ToInt32(phuTung["SoLuongTon"]) + soLech);
            }
            catch (OverflowException) { throw new InvalidOperationException("Số lượng tồn vượt giới hạn số nguyên."); }
            if (tonMoi < 0)
                throw new InvalidOperationException("Số lượng tồn sau điều chỉnh không được âm.");
            decimal tienLech = soLech * Convert.ToDecimal(phuTung["DonGiaNhap"]);
            string noiDung = "Ghi " + txtMaDieuChinhTon.Text + " cho " + phuTung["MaPhuTung"] + "?\nTăng/giảm: " + soLech.ToString("+0;-0") + "\nTồn hiện tại: " + phuTung["SoLuongTon"] + "; dự kiến sau điều chỉnh: " + tonMoi + "\nGiá trị chênh lệch: " + tienLech.ToString("N0") + " đ\nLý do: " + lyDo;
            if (MessageBox.Show(this, noiDung, "Xác nhận điều chỉnh tồn", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                return;
            QuanLyDieuChinhKho.DieuChinhTon(phuTung["MaPhuTung"].ToString(), soLech, lyDo, txtMaDieuChinhTon.Text);
            TaiDuLieu();
            tabKho.SelectedTab = tabLichSuTon;
        }
        void DieuChinhGia()
        {
            string lyDo = LyDoDieuChinh();
            var phuTung = PhuTungDangChon();
            decimal giaMoi = numGiaBanMoi.Value, giaNhap = Convert.ToDecimal(phuTung["DonGiaNhap"]), giaCu = Convert.ToDecimal(phuTung["DonGiaBan"]);
            if (giaMoi == giaCu)
                throw new InvalidOperationException("Giá bán mới trùng giá đang áp dụng.");
            bool duoiGiaNhap = giaMoi < giaNhap;
            string noiDung = "Ghi " + txtMaDieuChinhGia.Text + " cho " + phuTung["MaPhuTung"] + "?\nGiá hiện tại: " + giaCu.ToString("N0") + " đ\nGiá mới: " + giaMoi.ToString("N0") + " đ\nLý do: " + lyDo;
            if (duoiGiaNhap)
                noiDung += "\n\nGiá bán mới THẤP HƠN giá nhập " + giaNhap.ToString("N0") + " đ. Bạn vẫn muốn lưu?";
            if (MessageBox.Show(this, noiDung, duoiGiaNhap ? "Xác nhận bán dưới giá nhập" : "Xác nhận điều chỉnh giá", MessageBoxButtons.YesNo, duoiGiaNhap ? MessageBoxIcon.Warning : MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                return;
            QuanLyDieuChinhKho.DieuChinhGia(phuTung["MaPhuTung"].ToString(), giaMoi, lyDo, duoiGiaNhap, txtMaDieuChinhGia.Text);
            TaiDuLieu();
            tabKho.SelectedTab = tabLichSuGia;
        }
        void btnDieuChinhTon_Bam(object nguoiGui, EventArgs suKien)
        {
            GiaoDien.ThucHien(DieuChinhTon);
        }
        void btnDieuChinhGia_Bam(object nguoiGui, EventArgs suKien)
        {
            GiaoDien.ThucHien(DieuChinhGia);
        }
        void dgvPhuTung_ChonDong(object nguoiGui, EventArgs suKien)
        {
            ChonPhuTung();
        }
        void btnLamMoi_Bam(object nguoiGui, EventArgs suKien)
        {
            GiaoDien.ThucHien(TaiDuLieu);
        }
        DataTable BangLichSuTonXuat()
        {
            var nguon = dgvLichSuTon.DataSource as DataTable;
            if (nguon == null)
                throw new InvalidOperationException("Chưa có dữ liệu lịch sử tồn để xuất.");
            var bang = new DataTable();
            foreach (DataColumn cot in nguon.Columns)
                bang.Columns.Add(cot.ColumnName, cot.DataType);
            decimal thua = 0, thieu = 0;
            foreach (DataRow dong in nguon.Rows)
            {
                bang.Rows.Add(dong.ItemArray);
                decimal lech = Convert.ToDecimal(dong["GiaTriChenhLech"]);
                if (lech > 0)
                    thua += lech;
                else
                    thieu += lech;
            }
            string[] tenTong = { "Tổng thừa", "Tổng thiếu", "Tổng chênh lệch" };
            decimal[] cacTong = { thua, thieu, thua + thieu };
            for (int i = 0; i < tenTong.Length; i++)
            {
                var dong = bang.NewRow();
                dong["LyDo"] = tenTong[i];
                dong["GiaTriChenhLech"] = cacTong[i];
                bang.Rows.Add(dong);
            }
            return bang;
        }
        void btnXuatExcel_Bam(object nguoiGui, EventArgs suKien)
        {
            GiaoDien.ThucHien(() =>
            {
                PhienDangNhap.YeuCau("NHAP_KHO");
                XuatExcel.LuuNhieuBang(new[] { (DataTable)dgvPhuTung.DataSource, BangLichSuTonXuat(), (DataTable)dgvLichSuGia.DataSource }, new[] { "Phụ tùng", "Điều chỉnh tồn", "Điều chỉnh giá bán" });
            });
        }
        void btnNhapPhuTung_Bam(object nguoiGui, EventArgs suKien)
        {
            GiaoDien.ThucHien(() => { using (var trang = new frmNhapPhieuNhap()) trang.ShowDialog(this); TaiDuLieu(); });
        }
        void btnNhaCungCap_Bam(object nguoiGui, EventArgs suKien)
        {
            GiaoDien.ThucHien(() => { using (var trang = new frmThemNCC()) trang.ShowDialog(this); TaiDuLieu(); });
        }
        void txtTim_TextChanged(object nguoiGui, EventArgs suKien)
        {
            if (IsHandleCreated && !dangTai)
                GiaoDien.ThucHien(TaiDuLieu);
        }
        void btnDanhSachPhieuNhap_Bam(object nguoiGui, EventArgs suKien)
        {
            GiaoDien.ThucHien(() => { using (var trang = new frmPhieuNhap()) trang.ShowDialog(this); TaiDuLieu(); });
        }
        void btnThoat_Bam_1(object nguoiGui, EventArgs suKien)
        {
            Close();
        }
        void btnNgungSuDung_Bam(object nguoiGui, EventArgs suKien)
        {
            GiaoDien.ThucHien(() =>
            {
                PhienDangNhap.YeuCau("NHAP_KHO");
                string ma = GiaoDien.GiaTriO(dgvPhuTung, "MaPhuTung");
                if (ma.Length == 0)
                    throw new InvalidOperationException("Chọn phụ tùng.");
                string trangThai = GiaoDien.GiaTriO(dgvPhuTung, "TrangThai") == "Đang kinh doanh" ? "Ngừng kinh doanh" : "Đang kinh doanh";
                duLieu.CapNhatDuLieu("UPDATE dbo.PhuTung SET TrangThai=@s WHERE MaPhuTung=@id", XuLyDuLieu.ThamSo("@s", trangThai), XuLyDuLieu.ThamSo("@id", ma));
                TaiDuLieu();
            });
        }
    }
}
