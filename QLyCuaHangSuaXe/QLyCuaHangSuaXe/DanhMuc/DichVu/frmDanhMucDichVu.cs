using QLyCuaHangSuaXe.LopDungChung;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace QLyCuaHangSuaXe.DanhMuc.DichVu
{
    public partial class frmDanhMucDichVu : TrangThietKe
    {
        string maDichVuDangChon = "";
        public frmDanhMucDichVu()
        {
            InitializeComponent(); GanThietKe(dgvDichVu);
            cboTrangThai.Items.Clear(); cboTrangThai.Items.AddRange(new object[] { "Đang áp dụng", "Ngừng áp dụng" }); cboTrangThai.SelectedIndex = 0;
            txtMaDV.ReadOnly = true;
            btnDinhMuc.Click += (nguoiGui, suKien) => GiaoDien.ThucHien(MoBoPhuTung);
            btnKho.Click += (nguoiGui, suKien) => GiaoDien.ThucHien(() => { using (var trang = new btl.frmKho()) trang.ShowDialog(this); });
            btnThem.Click += (nguoiGui, suKien) => GiaoDien.ThucHien(LamMoi);
            btnSua.Text = "Lưu dịch vụ"; btnSua.Click += (nguoiGui, suKien) => GiaoDien.ThucHien(Luu);
            btnNgungSuDung.Click += (nguoiGui, suKien) => GiaoDien.ThucHien(NgungSuDung);
            btnTimKiem.Click += (nguoiGui, suKien) => GiaoDien.ThucHien(TaiDuLieu);
            btnLamMoi.Click += (nguoiGui, suKien) => GiaoDien.ThucHien(() => { TaiDuLieu(); LamMoi(); });
            btnDong.Click += (nguoiGui, suKien) => Close();
            dgvDichVu.CurrentCellChanged += (nguoiGui, suKien) => ChonDong();
        }
        void LamMoi()
        {
            maDichVuDangChon = ""; txtMaDV.Text = XuLyDuLieu.TaoMaMoi("DV"); txtTenDichVu.Clear(); txtMoTa.Clear(); cboTrangThai.SelectedIndex = 0; numGiaDichVu.Value = 1; txtTenDichVu.Focus();
        }
        public override void TaiDuLieu()
        {
            PhienDangNhap.YeuCau("DICH_VU");
            dgvDichVu.DataSource = duLieu.DocBang("SELECT MaDichVu,TenDichVu,ThanhTienDichVu,MoTa,TrangThai FROM dbo.DichVu WHERE TenDichVu LIKE @q OR MaDichVu LIKE @q ORDER BY MaDichVu", XuLyDuLieu.ThamSo("@q", "%" + txtTimKiem.Text.Trim() + "%"));
            dgvDichVu.Columns["ThanhTienDichVu"].HeaderText = "Giá dịch vụ (đ)"; dgvDichVu.Columns["ThanhTienDichVu"].DefaultCellStyle.Format = "N0";
            if (dgvDichVu.Rows.Count == 0) LamMoi(); lblThongBao.Text = "";
        }
        void ChonDong()
        {
            if (dgvDichVu.CurrentRow == null) return;
            maDichVuDangChon = GiaoDien.GiaTriO(dgvDichVu, "MaDichVu"); txtMaDV.Text = maDichVuDangChon; txtTenDichVu.Text = GiaoDien.GiaTriO(dgvDichVu, "TenDichVu"); txtMoTa.Text = GiaoDien.GiaTriO(dgvDichVu, "MoTa");
            numGiaDichVu.Value = Convert.ToDecimal(dgvDichVu.CurrentRow.Cells["ThanhTienDichVu"].Value); cboTrangThai.SelectedItem = GiaoDien.GiaTriO(dgvDichVu, "TrangThai");
        }
        void Luu()
        {
            PhienDangNhap.YeuCau("DICH_VU");
            string ten = GiaoDien.BatBuocNhap(txtTenDichVu, "Tên dịch vụ", 150);
            if (txtMoTa.Text.Trim().Length > 255 || numGiaDichVu.Value <= 0) throw new InvalidOperationException("Mô tả tối đa 255 ký tự; giá dịch vụ phải lớn hơn 0.");
            bool themMoi = maDichVuDangChon.Length == 0; string khoa = themMoi ? XuLyDuLieu.KiemTraMa(txtMaDV.Text, "DV") : maDichVuDangChon;
            int soDong = duLieu.CapNhatDuLieu(themMoi ? "INSERT dbo.DichVu(MaDichVu,TenDichVu,MoTa,ThanhTienDichVu,TrangThai) VALUES(@id,@ten,@mt,@gia,@tt)" : "UPDATE dbo.DichVu SET TenDichVu=@ten,MoTa=@mt,ThanhTienDichVu=@gia,TrangThai=@tt WHERE MaDichVu=@id", XuLyDuLieu.ThamSo("@id", khoa), XuLyDuLieu.ThamSo("@ten", ten), XuLyDuLieu.ThamSo("@mt", txtMoTa.Text.Trim()), XuLyDuLieu.ThamSo("@gia", numGiaDichVu.Value), XuLyDuLieu.ThamSo("@tt", cboTrangThai.Text));
            if (soDong != 1) throw new InvalidOperationException("Không thể lưu dịch vụ. Hãy tải lại danh sách.");
            TaiDuLieu(); ChonMa(khoa);
        }
        void ChonMa(string ma)
        {
            foreach (DataGridViewRow dong in dgvDichVu.Rows) if (Convert.ToString(dong.Cells["MaDichVu"].Value) == ma) { dgvDichVu.CurrentCell = dong.Cells["TenDichVu"]; break; }
            ChonDong();
        }
        void NgungSuDung()
        {
            PhienDangNhap.YeuCau("DICH_VU"); if (maDichVuDangChon.Length == 0) throw new InvalidOperationException("Chọn dịch vụ cần ngừng áp dụng.");
            string khoa = maDichVuDangChon;
            if (duLieu.CapNhatDuLieu("UPDATE dbo.DichVu SET TrangThai=N'Ngừng áp dụng' WHERE MaDichVu=@id", XuLyDuLieu.ThamSo("@id", khoa)) != 1) throw new InvalidOperationException("Dịch vụ không còn tồn tại.");
            TaiDuLieu(); ChonMa(khoa);
        }
        void MoBoPhuTung()
        {
            if (maDichVuDangChon.Length == 0) throw new InvalidOperationException("Lưu hoặc chọn dịch vụ trước.");
            using (var trang = new frmDinhMuc(maDichVuDangChon)) trang.ShowDialog(this);
        }

        private void lblTieuDe_Click(object sender, EventArgs e)
        {

        }
    }

}
