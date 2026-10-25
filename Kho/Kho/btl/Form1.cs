using QLyCuaHangSuaXe.LopDungChung;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace btl
{
    public partial class frmNhapPhieuNhap : TrangThietKe
    {
        readonly DataTable bangChiTiet = new DataTable();
        public frmNhapPhieuNhap()
        {
            InitializeComponent();
            Text = "Lập phiếu nhập kho";
            GanThietKe(dgvPhuTung, lblHuongDan);
            dgvPhuTung.Columns.Clear();
            dgvPhuTung.AutoGenerateColumns = true;
            txtMaPhieuNhap.Text = "Tự động khi lưu";
            txtMaNVNhap.Text = PhienDangNhap.TenDangNhap;
            dtpNgayNhap.Enabled = false;
            btnThemPhuTung.Click += (nguoiGui, suKien) => GiaoDien.ThucHien(ThemDong);
            btnXoaPhuTung.Click += (nguoiGui, suKien) =>
            {
                if (dgvDanhSach.CurrentRow != null)
                {
                    ((DataRowView)dgvDanhSach.CurrentRow.DataBoundItem).Row.Delete();
                    bangChiTiet.AcceptChanges();
                }
            };
            btnSuaPhuTung.Click += (nguoiGui, suKien) => GiaoDien.ThucHien(() =>
            {
                if (dgvDanhSach.CurrentRow == null)
                    return;
                int viTri = dgvDanhSach.CurrentRow.Index;
                ThemDong();
                bangChiTiet.Rows[viTri].Delete();
                bangChiTiet.AcceptChanges();
            });
            btnLuuPhieu.Click += (nguoiGui, suKien) => GiaoDien.ThucHien(Luu);
            btnThoat.Click += (nguoiGui, suKien) => Close();
            btnThemNCC.Click += (nguoiGui, suKien) => GiaoDien.ThucHien(() =>
            {
                using (var trang = new frmThemNCC())
                    trang.ShowDialog(this);
                TaiDuLieu();
            });
            bangChiTiet.Columns.Add("MaPhuTung");
            bangChiTiet.Columns.Add("TenPhuTung");
            bangChiTiet.Columns.Add("MaNCC");
            bangChiTiet.Columns.Add("TenNCC");
            bangChiTiet.Columns.Add("SoLuong", typeof(int));
            bangChiTiet.Columns.Add("DonGiaNhap", typeof(decimal));
            bangChiTiet.Columns.Add("HanSuDung", typeof(DateTime));
            dgvDanhSach.DataSource = bangChiTiet;
        }

        public override void TaiDuLieu()
        {
            PhienDangNhap.YeuCau("NHAP_KHO");
            GiaoDien.GanDuLieu(cboPhuTung, duLieu.DocBang("SELECT MaPhuTung,TenPhuTung,DonGiaNhap FROM dbo.PhuTung WHERE TrangThai=N'Đang sử dụng'"), "TenPhuTung", "MaPhuTung");
            GiaoDien.GanDuLieu(cboNhaCungCap, duLieu.DocBang("SELECT MaNCC,TenNCC FROM dbo.NhaCungCap"), "TenNCC", "MaNCC");
            cboPhuTung.SelectedIndexChanged += (doiTuongGui, suKien) =>
            {
                var dongHienThi = cboPhuTung.SelectedItem as DataRowView;
                if (dongHienThi != null)
                    numGiaNhap.Value = Convert.ToDecimal(dongHienThi["DonGiaNhap"]);
            };
        }

        void ThemDong()
        {
            if (cboPhuTung.SelectedIndex < 0 || cboNhaCungCap.SelectedIndex < 0)
                throw new InvalidOperationException("Chọn phụ tùng và nhà cung cấp.");
            if (dtpHanSuDung.Checked && dtpHanSuDung.Value.Date < DateTime.Today)
                throw new InvalidOperationException("Không nhập phụ tùng đã hết hạn.");
            bangChiTiet.Rows.Add(GiaoDien.GiaTriDangChon(cboPhuTung), cboPhuTung.Text, GiaoDien.GiaTriDangChon(cboNhaCungCap), cboNhaCungCap.Text, (int)numSoLuong.Value, numGiaNhap.Value, dtpHanSuDung.Checked ? (object)dtpHanSuDung.Value.Date : DBNull.Value);
            lblThongBao.Text = "Tổng phiếu: " + bangChiTiet.AsEnumerable().Sum(dongDuLieu => dongDuLieu.Field<int>("SoLuong") * dongDuLieu.Field<decimal>("DonGiaNhap")).ToString("N0") + " đ";
        }

        void Luu()
        {
            PhienDangNhap.YeuCau("NHAP_KHO");
            if (bangChiTiet.Rows.Count == 0)
                throw new InvalidOperationException("Thêm ít nhất một dòng nhập.");
            if (txtGhiChu.Text.Length > 300)
                throw new InvalidOperationException("Ghi chú tối đa 300 ký tự.");
            string ma = XuLyDuLieu.TaoMaMoi("PN");
            duLieu.TrongGiaoDich<int>((ketNoi, giaoDich) =>
            {
                decimal tong = bangChiTiet.AsEnumerable().Sum(dongDuLieu => dongDuLieu.Field<int>("SoLuong") * dongDuLieu.Field<decimal>("DonGiaNhap"));
                XuLyDuLieu.ThucThi(ketNoi, giaoDich, "INSERT dbo.PhieuNhap(MaPhieuNhap,MaNhanVienNhap,GhiChu,TongTien) VALUES(@id,@nv,@note,@total)", XuLyDuLieu.ThamSo("@id", ma), XuLyDuLieu.ThamSo("@nv", PhienDangNhap.MaNhanVien), XuLyDuLieu.ThamSo("@note", txtGhiChu.Text.Trim()), XuLyDuLieu.ThamSo("@total", tong));
                foreach (DataRow dong in bangChiTiet.AsEnumerable().OrderBy(dongDuLieu => dongDuLieu["MaPhuTung"]))
                {
                    int soLuong = XuLyDuLieu.ThucThi(ketNoi, giaoDich, "UPDATE dbo.PhuTung SET SoLuongTon=SoLuongTon+@q,DonGiaNhap=@p WHERE MaPhuTung=@pt AND TrangThai=N'Đang sử dụng'", XuLyDuLieu.ThamSo("@q", dong["SoLuong"]), XuLyDuLieu.ThamSo("@p", dong["DonGiaNhap"]), XuLyDuLieu.ThamSo("@pt", dong["MaPhuTung"]));
                    if (soLuong != 1)
                        throw new InvalidOperationException("Phụ tùng đã ngừng dùng. Tải lại danh mục.");
                    XuLyDuLieu.ThucThi(ketNoi, giaoDich, "INSERT dbo.ChiTietPhieuNhap(MaPhieuNhap,MaPhuTung,MaNCC,SoLuong,DonGiaNhap,HanSuDung) VALUES(@id,@pt,@ncc,@q,@p,@hsd)", XuLyDuLieu.ThamSo("@id", ma), XuLyDuLieu.ThamSo("@pt", dong["MaPhuTung"]), XuLyDuLieu.ThamSo("@ncc", dong["MaNCC"]), XuLyDuLieu.ThamSo("@q", dong["SoLuong"]), XuLyDuLieu.ThamSo("@p", dong["DonGiaNhap"]), XuLyDuLieu.ThamSo("@hsd", dong["HanSuDung"]));
                }

                return 0;
            });
            MessageBox.Show("Đã ghi sổ " + ma);
            Close();
        }
    }
}
