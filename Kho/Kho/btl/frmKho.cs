using QLyCuaHangSuaXe.LopDungChung;
using System;
using System.Windows.Forms;

namespace btl
{
    public partial class frmKho : TrangThietKe
    {
        string maPhuTungDangChon = "";
        public frmKho()
        {
            InitializeComponent();
            GanThietKe(dgvPhuTung, lblHuongDan);
            Text = "Kho phụ tùng";
            cboTrangThai.Items.AddRange(new object[] { "Đang sử dụng", "Ngừng sử dụng" });
            cboTrangThai.SelectedIndex = 0;
            btnThemPhuTung.Click += (nguoiGui, suKien) =>
            {
                maPhuTungDangChon = "";
                txtTenPhuTung.Clear();
                txtDonViTinh.Clear();
                numGiaNhap.Value = numGiaBan.Value = 0;
                cboTrangThai.SelectedIndex = 0;
            };
            btnLuuPhuTung.Click += (nguoiGui, suKien) => GiaoDien.ThucHien(Luu);
            btnDanhSachPhieuNhap.Click += (nguoiGui, suKien) => GiaoDien.ThucHien(() =>
            {
                using (var trang = new frmPhieuNhap())
                    trang.ShowDialog(this);
                TaiDuLieu();
            });
            btnNhaCungCap.Click += (nguoiGui, suKien) => GiaoDien.ThucHien(() =>
            {
                using (var trang = new frmThemNCC())
                    trang.ShowDialog(this);
            });
            btnNgungSuDung.Click += (nguoiGui, suKien) => GiaoDien.ThucHien(() =>
            {
                if (maPhuTungDangChon.Length == 0)
                    return;
                cboTrangThai.SelectedItem = "Ngừng sử dụng";
                Luu();
            });
            btnThoat.Click += (nguoiGui, suKien) => Close();
            txtTimKiem.TextChanged += (nguoiGui, suKien) => GiaoDien.ThucHien(TaiDuLieu);
            dgvDanhSach.SelectionChanged += (nguoiGui, suKien) => ChonDong();
        }

        public override void TaiDuLieu()
        {
            PhienDangNhap.YeuCau("NHAP_KHO");
            dgvDanhSach.DataSource = duLieu.DocBang("SELECT MaPhuTung,TenPhuTung,DonViTinh,SoLuongTon,DonGiaNhap,DonGiaBan,TrangThai FROM dbo.PhuTung WHERE TenPhuTung LIKE @q OR MaPhuTung LIKE @q ORDER BY TenPhuTung", XuLyDuLieu.ThamSo("@q", "%" + txtTimKiem.Text.Trim() + "%"));
            lblThongBao.Text = "Tồn kho tăng bằng phiếu nhập, giảm khi chốt phiếu tiếp nhận. Phụ tùng chưa nhập hàng vẫn hiện trong danh sách.";
        }

        void ChonDong()
        {
            if (dgvDanhSach.CurrentRow == null)
                return;
            maPhuTungDangChon = GiaoDien.GiaTriO(dgvDanhSach, "MaPhuTung");
            txtTenPhuTung.Text = GiaoDien.GiaTriO(dgvDanhSach, "TenPhuTung");
            txtDonViTinh.Text = GiaoDien.GiaTriO(dgvDanhSach, "DonViTinh");
            numGiaNhap.Value = Convert.ToDecimal(dgvDanhSach.CurrentRow.Cells["DonGiaNhap"].Value);
            numGiaBan.Value = Convert.ToDecimal(dgvDanhSach.CurrentRow.Cells["DonGiaBan"].Value);
            cboTrangThai.SelectedItem = GiaoDien.GiaTriO(dgvDanhSach, "TrangThai");
        }

        void Luu()
        {
            PhienDangNhap.YeuCau("NHAP_KHO");
            string ten = GiaoDien.BatBuocNhap(txtTenPhuTung, "Tên phụ tùng", 100);
            if (txtDonViTinh.Text.Length > 20)
                throw new InvalidOperationException("Đơn vị tính tối đa 20 ký tự.");
            bool themMoi = maPhuTungDangChon.Length == 0;
            string khoa = themMoi ? XuLyDuLieu.TaoMaMoi("PT") : maPhuTungDangChon;
            duLieu.CapNhatDuLieu(themMoi ? "INSERT dbo.PhuTung(MaPhuTung,TenPhuTung,DonViTinh,DonGiaNhap,DonGiaBan,TrangThai) VALUES(@id,@n,@u,@b,@s,@t)" : "UPDATE dbo.PhuTung SET TenPhuTung=@n,DonViTinh=@u,DonGiaNhap=@b,DonGiaBan=@s,TrangThai=@t WHERE MaPhuTung=@id", XuLyDuLieu.ThamSo("@id", khoa), XuLyDuLieu.ThamSo("@n", ten), XuLyDuLieu.ThamSo("@u", txtDonViTinh.Text.Trim()), XuLyDuLieu.ThamSo("@b", numGiaNhap.Value), XuLyDuLieu.ThamSo("@s", numGiaBan.Value), XuLyDuLieu.ThamSo("@t", cboTrangThai.Text));
            TaiDuLieu();
            lblThongBao.Text = "Đã lưu " + khoa;
        }
    }
}
