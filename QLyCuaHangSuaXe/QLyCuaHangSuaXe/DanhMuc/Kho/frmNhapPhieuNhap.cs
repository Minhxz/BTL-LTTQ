using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using QLyCuaHangSuaXe.LopDungChung;

namespace btl
{
    public partial class frmNhapPhieuNhap : TrangThietKe
    {
        readonly DataTable bangChiTiet = new DataTable();
        DataTable bangPhuTung;
        int dongDangSua = -1;
        bool daKhoiTao;
        bool dangTaoPhuTungMoi;
        bool dangGanDuLieu;

        public frmNhapPhieuNhap()
        {
            InitializeComponent();
            foreach (string cot in new[] { "MaPhuTung", "TenPhuTung", "MaNhaCungCap", "TenNhaCungCap" }) bangChiTiet.Columns.Add(cot);
            bangChiTiet.Columns.Add("SoLuongNhap", typeof(int));
            bangChiTiet.Columns.Add("DonGiaNhap", typeof(decimal));
            bangChiTiet.Columns.Add("DonGiaBan", typeof(decimal));
            bangChiTiet.Columns.Add("ThanhTien", typeof(decimal));
            bangChiTiet.Columns.Add("HanSuDung", typeof(DateTime));
            bangChiTiet.Columns.Add("LaPhuTungMoi", typeof(bool)).DefaultValue = false;
            dgvPhuTung.DataBindingComplete += (s, e) =>
            {
                if (dgvPhuTung.Columns.Contains("LaPhuTungMoi")) dgvPhuTung.Columns["LaPhuTungMoi"].Visible = false;
                if (dgvPhuTung.Columns.Contains("MaNhaCungCap")) dgvPhuTung.Columns["MaNhaCungCap"].Visible = false;
                if (dgvPhuTung.Columns.Contains("TenNhaCungCap")) dgvPhuTung.Columns["TenNhaCungCap"].HeaderText = "Nhà cung cấp";
            };
            dgvPhuTung.DataSource = bangChiTiet;
            cbMaPhuTung.SelectedIndexChanged += (s, e) => ChonPhuTung();
            GanThietKe(dgvPhuTung);
            HienThiHan();
        }

        void HienThiHan() { dtpHanSuDung.CustomFormat = dtpHanSuDung.Checked ? "dd/MM/yyyy" : "'Không hạn'"; }
        void dtpHanSuDung_ValueChanged(object s, EventArgs e) { HienThiHan(); }
        void dtpHanSuDung_MouseUp(object s, MouseEventArgs e) { HienThiHan(); }
        void dtpHanSuDung_KeyUp(object s, KeyEventArgs e) { HienThiHan(); }
        void TaiNhaCungCap()
        {
            GiaoDien.GanDuLieu(cbNhaCungCap, duLieu.DocBang("SELECT MaNhaCungCap,TenNhaCungCap FROM dbo.NhaCungCap ORDER BY MaNhaCungCap"), "TenNhaCungCap", "MaNhaCungCap");
        }
        public override void TaiDuLieu()
        {
            PhienDangNhap.YeuCau("NHAP_KHO");
            TaiNhaCungCap();
            bangPhuTung = duLieu.DocBang("SELECT MaPhuTung,TenPhuTung,MaNhaCungCap,DonGiaNhap,DonGiaBan,HanSuDung FROM dbo.PhuTung WHERE TrangThai=N'Đang kinh doanh' AND HanSuDung>=CONVERT(date,GETDATE()) ORDER BY MaPhuTung");
            GiaoDien.GanDuLieu(cbMaPhuTung, bangPhuTung, "MaPhuTung", "MaPhuTung");
            if (!daKhoiTao)
            {
                txtMaPhieuNhap.Text = XuLyDuLieu.TaoMaMoi("PN");
                txtMaNVNhap.Text = PhienDangNhap.MaNhanVien;
                dtpNgayNhap.Value = DateTime.Today;
                daKhoiTao = true;
            }
            ChonPhuTung();
        }
        void CheDoPhuTung(bool moi)
        {
            dangTaoPhuTungMoi = moi;
            txtTenPhuTung.ReadOnly = !moi;
            cbNhaCungCap.Enabled = moi;
            dtpHanSuDung.Enabled = moi;
        }
        void ChonPhuTung()
        {
            if (dangGanDuLieu) return;
            var dong = cbMaPhuTung.SelectedItem as DataRowView;
            if (dong == null) return;
            CheDoPhuTung(false);
            cbNhaCungCap.SelectedValue = dong["MaNhaCungCap"];
            txtTenPhuTung.Text = dong["TenPhuTung"].ToString();
            txtDonGiaNhap.Text = Convert.ToDecimal(dong["DonGiaNhap"]).ToString("0.##");
            txtDonGiaBan.Text = Convert.ToDecimal(dong["DonGiaBan"]).ToString("0.##");
            DatHanSuDung(Convert.ToDateTime(dong["HanSuDung"]));
            TinhThanhTien();
        }
        void DatHanSuDung(DateTime han)
        {
            dtpHanSuDung.Checked = han.Year < 9999;
            if (dtpHanSuDung.Checked) dtpHanSuDung.Value = han < dtpHanSuDung.MinDate ? dtpHanSuDung.MinDate : han;
            HienThiHan();
        }
        void TaoPhuTungMoi()
        {
            string ma = XuLyDuLieu.TaoMaMoi("PT");
            int so = int.Parse(ma.Substring(2));
            foreach (DataRow dong in bangChiTiet.Rows) so = Math.Max(so, int.Parse(dong["MaPhuTung"].ToString().Substring(2)) + 1);
            dangGanDuLieu = true;
            cbMaPhuTung.DataSource = null;
            cbMaPhuTung.Items.Clear();
            cbMaPhuTung.Items.Add("PT" + so.ToString("D3"));
            cbMaPhuTung.SelectedIndex = 0;
            dangGanDuLieu = false;
            CheDoPhuTung(true);
            txtTenPhuTung.Clear();
            txtSoLuongNhap.Text = "1";
            txtDonGiaNhap.Clear();
            txtDonGiaBan.Clear();
            DatHanSuDung(DateTime.MaxValue.Date);
            dongDangSua = -1;
            txtTenPhuTung.Focus();
        }
        void ThemDong(bool sua)
        {
            string ma = XuLyDuLieu.KiemTraMa(cbMaPhuTung.Text, "PT");
            string ten = GiaoDien.BatBuocNhap(txtTenPhuTung, "Tên phụ tùng", 150);
            string nhaCungCap = GiaoDien.GiaTriDangChon(cbNhaCungCap);
            int soLuong;
            decimal giaNhap, giaBan;
            if (nhaCungCap.Length == 0 || !int.TryParse(txtSoLuongNhap.Text, out soLuong) || soLuong < 1 || soLuong > 100000 || !decimal.TryParse(txtDonGiaNhap.Text, out giaNhap) || !decimal.TryParse(txtDonGiaBan.Text, out giaBan) || giaNhap <= 0 || giaBan < giaNhap)
                throw new InvalidOperationException("Chọn nhà cung cấp; nhập số lượng từ 1 đến 100.000, giá nhập lớn hơn 0 và giá bán không thấp hơn giá nhập.");
            if (sua && (dongDangSua < 0 || dongDangSua >= bangChiTiet.Rows.Count)) throw new InvalidOperationException("Chọn dòng cần cập nhật.");
            if (bangChiTiet.AsEnumerable().Where((d, i) => !sua || i != dongDangSua).Any(d => string.Equals(d["MaPhuTung"].ToString(), ma, StringComparison.OrdinalIgnoreCase)))
                throw new InvalidOperationException("Phụ tùng đã có trong danh sách. Chọn dòng và bấm Cập nhật dòng để sửa số lượng.");
            DateTime han = dtpHanSuDung.Checked ? dtpHanSuDung.Value.Date : DateTime.MaxValue.Date;
            if (han < dtpNgayNhap.Value.Date) throw new InvalidOperationException("Hạn sử dụng không được trước ngày nhập.");
            var dong = sua ? bangChiTiet.Rows[dongDangSua] : bangChiTiet.NewRow();
            dong["MaPhuTung"] = ma;
            dong["TenPhuTung"] = ten;
            dong["MaNhaCungCap"] = nhaCungCap;
            dong["TenNhaCungCap"] = cbNhaCungCap.Text;
            dong["SoLuongNhap"] = soLuong;
            dong["DonGiaNhap"] = giaNhap;
            dong["DonGiaBan"] = giaBan;
            dong["ThanhTien"] = soLuong * giaNhap;
            dong["HanSuDung"] = han;
            dong["LaPhuTungMoi"] = dangTaoPhuTungMoi;
            if (!sua) bangChiTiet.Rows.Add(dong);
            dongDangSua = -1;
        }
        void Luu()
        {
            QuanLyKho.GhiSo(txtMaPhieuNhap.Text, dtpNgayNhap.Value.Date, txtGhiChu.Text.Trim(), bangChiTiet);
            DialogResult = DialogResult.OK;
            Close();
        }
        void TinhThanhTien()
        {
            decimal soLuong, gia;
            txtThanhTien.Text = decimal.TryParse(txtSoLuongNhap.Text, out soLuong) && decimal.TryParse(txtDonGiaNhap.Text, out gia) ? (soLuong * gia).ToString("N0") : "0";
        }
        void btnMoiPhuTung_Bam(object s, EventArgs e) { GiaoDien.ThucHien(TaoPhuTungMoi); }
        void btnThemPhuTung_Bam(object s, EventArgs e) { GiaoDien.ThucHien(() => ThemDong(false)); }
        void btnSuaPhuTung_Bam(object s, EventArgs e) { GiaoDien.ThucHien(() => ThemDong(true)); }
        void btnXoaPhuTung_Bam(object s, EventArgs e)
        {
            var dong = dgvPhuTung.CurrentRow == null ? null : dgvPhuTung.CurrentRow.DataBoundItem as DataRowView;
            if (dong == null) return;
            bangChiTiet.Rows.Remove(dong.Row);
            dongDangSua = -1;
        }
        void btnLamMoiPhuTung_Bam(object s, EventArgs e) { GiaoDien.ThucHien(() => { TaiDuLieu(); TaoPhuTungMoi(); }); }
        void btnThoat_Bam(object s, EventArgs e) { Close(); }
        void btnLuuPhieu_Bam(object s, EventArgs e) { GiaoDien.ThucHien(Luu); }
        void btnThemNCC_Bam(object s, EventArgs e)
        {
            GiaoDien.ThucHien(() =>
            {
                string nhaCungCapCu = GiaoDien.GiaTriDangChon(cbNhaCungCap);
                using (var trang = new frmThemNCC())
                    if (trang.ShowDialog(this) == DialogResult.OK)
                    {
                        TaiNhaCungCap();
                        cbNhaCungCap.SelectedValue = dangTaoPhuTungMoi ? trang.MaNhaCungCapMoi : nhaCungCapCu;
                    }
            });
        }
        void txtDonGiaNhap_TextChanged(object s, EventArgs e) { TinhThanhTien(); }
        void txtSoLuongNhap_TextChanged(object s, EventArgs e) { TinhThanhTien(); }
        void dgvPhuTung_CellClick(object s, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var hienThi = dgvPhuTung.Rows[e.RowIndex].DataBoundItem as DataRowView;
            if (hienThi == null) return;
            var dong = hienThi.Row;
            dongDangSua = bangChiTiet.Rows.IndexOf(dong);
            dangGanDuLieu = true;
            cbMaPhuTung.DataSource = null;
            cbMaPhuTung.Items.Clear();
            cbMaPhuTung.Items.Add(dong["MaPhuTung"].ToString());
            cbMaPhuTung.SelectedIndex = 0;
            dangGanDuLieu = false;
            CheDoPhuTung(Convert.ToBoolean(dong["LaPhuTungMoi"]));
            cbNhaCungCap.SelectedValue = dong["MaNhaCungCap"];
            txtTenPhuTung.Text = dong["TenPhuTung"].ToString();
            txtSoLuongNhap.Text = dong["SoLuongNhap"].ToString();
            txtDonGiaNhap.Text = dong["DonGiaNhap"].ToString();
            txtDonGiaBan.Text = dong["DonGiaBan"].ToString();
            DatHanSuDung(Convert.ToDateTime(dong["HanSuDung"]));
            TinhThanhTien();
        }
    }
}
