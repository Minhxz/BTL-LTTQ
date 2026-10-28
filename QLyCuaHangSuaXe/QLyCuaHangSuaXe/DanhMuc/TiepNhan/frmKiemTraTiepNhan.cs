using QLyCuaHangSuaXe.LopDungChung;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace QLyCuaHangSuaXe.DanhMuc.TiepNhan
{
    public partial class frmKiemTraTiepNhan : Form
    {
        readonly XuLyDuLieu duLieu = new XuLyDuLieu();
        readonly NghiepVuSuaXe nghiepVu = new NghiepVuSuaXe();
        readonly DataTable bangHangMuc = new DataTable();
        DataTable bangDinhMuc;
        public bool DuDieuKien { get; private set; }
        public string TinhTrangXe { get { return ""; } }
        public DataTable ChiTietDaChon { get { return bangHangMuc.Copy(); } }
        public decimal TongDuKien { get { return bangHangMuc.AsEnumerable().Sum(d => d.Field<decimal>("ThanhTien")); } }
        public List<DichVuChon> DichVuDaChon
        {
            get { if (!DuDieuKien) throw new InvalidOperationException("Hãy kiểm tra điều kiện trước khi chuyển sang tiếp nhận."); return LayDichVu(); }
        }
        public frmKiemTraTiepNhan()
        {
            InitializeComponent(); GiaoDien.ApDungMauTiepNhan(this); GiaoDien.DinhDangBang(dgvHangMuc);
            bangHangMuc.Columns.Add("MaDichVu"); bangHangMuc.Columns.Add("TenDichVu"); bangHangMuc.Columns.Add("PhuTung"); bangHangMuc.Columns.Add("SoLuong", typeof(int));
            bangHangMuc.Columns["SoLuong"].AllowDBNull = false; bangHangMuc.Columns["SoLuong"].DefaultValue = 1;
            bangHangMuc.Columns.Add("DonGia", typeof(decimal)); bangHangMuc.Columns.Add("ThanhTien", typeof(decimal), "DonGia*SoLuong"); bangHangMuc.Columns.Add("DieuKien");
            dgvHangMuc.DataSource = bangHangMuc; dgvHangMuc.ReadOnly = false;
            foreach (DataGridViewColumn cot in dgvHangMuc.Columns) cot.ReadOnly = cot.Name != "SoLuong";
            string[] cotTen = { "Mã dịch vụ", "Dịch vụ", "Phụ tùng đi kèm", "Số lượng", "Giá dịch vụ (đ)", "Thành tiền (đ)", "Điều kiện" };
            for (int i = 0; i < cotTen.Length; i++) dgvHangMuc.Columns[i].HeaderText = cotTen[i];
            dgvHangMuc.Columns["MaDichVu"].FillWeight = 65; dgvHangMuc.Columns["TenDichVu"].FillWeight = 180; dgvHangMuc.Columns["PhuTung"].FillWeight = 180; dgvHangMuc.Columns["SoLuong"].FillWeight = 55;
            dgvHangMuc.Columns["DonGia"].DefaultCellStyle.Format = dgvHangMuc.Columns["ThanhTien"].DefaultCellStyle.Format = "N0";
            bangHangMuc.ColumnChanged += (nguoiGui, suKien) => { if (suKien.Column.ColumnName == "SoLuong") KhoaKetQua(); };
            cboDichVu.SelectedIndexChanged += (nguoiGui, suKien) => HienThiDichVu();
            btnThem.Click += (nguoiGui, suKien) => GiaoDien.ThucHien(ThemDichVu);
            btnXoaDong.Click += (nguoiGui, suKien) => { if (dgvHangMuc.CurrentRow != null) { bangHangMuc.Rows.RemoveAt(dgvHangMuc.CurrentRow.Index); KhoaKetQua(); } };
            btnLamMoi.Click += (nguoiGui, suKien) => GiaoDien.ThucHien(() => { bangHangMuc.Clear(); TaiDuLieu(); KhoaKetQua(); });
            btnKiemTra.Click += (nguoiGui, suKien) => GiaoDien.ThucHien(KiemTraDieuKien);
            btnChuyen.Click += (nguoiGui, suKien) => { if (DuDieuKien) { DialogResult = DialogResult.OK; Close(); } };
            dgvHangMuc.CellValueChanged += (nguoiGui, suKien) => { if (suKien.RowIndex >= 0 && suKien.ColumnIndex == dgvHangMuc.Columns["SoLuong"].Index) KhoaKetQua(); };
            dgvHangMuc.DataError += (nguoiGui, suKien) => { suKien.ThrowException = false; KhoaKetQua(); lblKetQua.Text = "Số lượng phải là số nguyên từ 1 đến 1000."; };
            KhoaKetQua();
        }
        protected override void OnLoad(EventArgs suKien)
        {
            base.OnLoad(suKien);
            if (!DesignMode && System.ComponentModel.LicenseManager.UsageMode != System.ComponentModel.LicenseUsageMode.Designtime) GiaoDien.ThucHien(TaiDuLieu);
        }
        public void TaiDuLieu()
        {
            PhienDangNhap.YeuCau("TIEP_NHAN");
            bangDinhMuc = duLieu.DocBang("SELECT m.MaDichVu,p.MaPhuTung,p.TenPhuTung,m.SoLuong,p.SoLuongTon,p.DonGiaBan,p.TrangThai,p.HanSuDung FROM dbo.ChiTietDichVu m JOIN dbo.PhuTung p ON p.MaPhuTung=m.MaPhuTung");
            GiaoDien.GanDuLieu(cboDichVu, duLieu.DocBang("SELECT MaDichVu,TenDichVu,ThanhTienDichVu FROM dbo.DichVu WHERE TrangThai=N'Đang áp dụng' ORDER BY TenDichVu"), "TenDichVu", "MaDichVu");
            if (cboDichVu.Items.Count > 0) cboDichVu.SelectedIndex = 0;
            HienThiDichVu();
        }
        List<DataRow> PhuTungDichVu(string ma) { return bangDinhMuc == null ? new List<DataRow>() : bangDinhMuc.AsEnumerable().Where(d => d["MaDichVu"].ToString() == ma).ToList(); }
        void HienThiDichVu()
        {
            var dv = cboDichVu.SelectedItem as DataRowView;
            cboPhuTung.Items.Clear(); txtTonKho.Clear(); txtGiaDichVu.Clear();
            if (dv == null) return;
            var pt = PhuTungDichVu(dv["MaDichVu"].ToString());
            foreach (var dong in pt) cboPhuTung.Items.Add(dong["TenPhuTung"] + " × " + dong["SoLuong"]);
            if (pt.Count == 0) cboPhuTung.Items.Add("Không dùng phụ tùng"); cboPhuTung.SelectedIndex = 0;
            txtTonKho.Text = pt.Count == 0 ? "Không giới hạn" : pt.Min(d => d["TrangThai"].ToString() != "Đang kinh doanh" || Convert.ToDateTime(d["HanSuDung"]).Date < DateTime.Today ? 0 : Convert.ToInt32(d["SoLuongTon"]) / Convert.ToInt32(d["SoLuong"])).ToString();
            txtGiaDichVu.Text = Convert.ToDecimal(dv["ThanhTienDichVu"]).ToString("N0") + " đ";
        }
        void ThemDichVu()
        {
            var dv = cboDichVu.SelectedItem as DataRowView; if (dv == null) throw new InvalidOperationException("Chọn dịch vụ trước.");
            string ma = dv["MaDichVu"].ToString(); var daCo = bangHangMuc.AsEnumerable().FirstOrDefault(d => d["MaDichVu"].ToString() == ma);
            if (daCo != null) { int sl = Convert.ToInt32(daCo["SoLuong"]) + (int)numSoLuong.Value; if (sl > 1000) throw new InvalidOperationException("Số lượng mỗi dịch vụ tối đa 1000."); daCo["SoLuong"] = sl; }
            else bangHangMuc.Rows.Add(ma, dv["TenDichVu"], string.Join(", ", PhuTungDichVu(ma).Select(d => d["TenPhuTung"] + " × " + d["SoLuong"])), (int)numSoLuong.Value, dv["ThanhTienDichVu"], null, "Chưa kiểm tra");
            KhoaKetQua();
        }
        List<DichVuChon> LayDichVu() { dgvHangMuc.EndEdit(); return bangHangMuc.AsEnumerable().Select(d => new DichVuChon { Ma = d["MaDichVu"].ToString(), SoLuong = Convert.ToInt32(d["SoLuong"]) }).ToList(); }
        void KhoaKetQua()
        {
            DuDieuKien = false; btnChuyen.Enabled = false; lblKetQua.Text = "CHƯA KIỂM TRA"; lblKetQua.ForeColor = Color.FromArgb(145, 103, 35); lblGiaiThich.Text = "";
            foreach (DataRow dong in bangHangMuc.Rows) dong["DieuKien"] = "Chưa kiểm tra";
            lblSoHangMuc.Text = bangHangMuc.Rows.Count + " hạng mục"; lblTongTien.Text = "Dự kiến: " + TongDuKien.ToString("N0") + " đ";
        }
        void KiemTraDieuKien()
        {
            var chon = LayDichVu(); KhoaKetQua();
            try { nghiepVu.KiemTraKho(chon); }
            catch (InvalidOperationException loi) { lblKetQua.Text = "CHƯA ĐỦ ĐIỀU KIỆN"; lblKetQua.ForeColor = Color.Firebrick; lblGiaiThich.Text = loi.Message; return; }
            DuDieuKien = true; btnChuyen.Enabled = true;
            foreach (DataRow dong in bangHangMuc.Rows) dong["DieuKien"] = "Đủ phụ tùng";
            lblKetQua.Text = "ĐỦ ĐIỀU KIỆN TIẾP NHẬN"; lblKetQua.ForeColor = Color.SeaGreen; lblGiaiThich.Text = "";
        }
    }
}
