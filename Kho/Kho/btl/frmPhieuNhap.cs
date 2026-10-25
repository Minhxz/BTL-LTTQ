using QLyCuaHangSuaXe.LopDungChung;
using System.Windows.Forms;

namespace btl
{
    public partial class frmPhieuNhap : TrangThietKe
    {
        public frmPhieuNhap()
        {
            InitializeComponent();
            GanThietKe(dgvPhieuNhap, lblHuongDan);
            Text = "Danh sách phiếu nhập";
            dgvPhieuNhap.Columns.Clear();
            dgvPhieuNhap.AutoGenerateColumns = true;
            dgvChiTiet.Columns.Clear();
            dgvChiTiet.AutoGenerateColumns = true;
            GiaoDien.DinhDangBang(dgvChiTiet);
            btnThemPhieuNhap.Click += (nguoiGui, suKien) => GiaoDien.ThucHien(() =>
            {
                using (var trang = new frmNhapPhieuNhap())
                    trang.ShowDialog(this);
                TaiDuLieu();
            });
            btnSuaPhieuNhap.Text = "Tải lại";
            btnSuaPhieuNhap.Click += (nguoiGui, suKien) => GiaoDien.ThucHien(TaiDuLieu);
            btnXoaPhieuNhap.Text = "Quy định phiếu";
            btnXoaPhieuNhap.Click += (nguoiGui, suKien) => MessageBox.Show("Phiếu đã ghi sổ được giữ nguyên để đối chiếu tồn kho.");
            btnInPhieuNhap.Text = "Xuất Excel";
            btnInPhieuNhap.Click += (nguoiGui, suKien) => GiaoDien.ThucHien(() => XuatExcel.LuuExcel((System.Data.DataTable)dgvChiTiet.DataSource, "Chi tiết phiếu nhập"));
            btnThoat.Click += (nguoiGui, suKien) => Close();
            dgvPhieuNhap.SelectionChanged += (nguoiGui, suKien) => GiaoDien.ThucHien(() =>
            {
                var ma = GiaoDien.GiaTriO(dgvPhieuNhap, "MaPhieuNhap");
                if (ma.Length == 0)
                    return;
                dgvChiTiet.DataSource = duLieu.DocBang("SELECT p.TenPhuTung,n.TenNCC,c.SoLuong,c.DonGiaNhap,c.ThanhTien,c.HanSuDung FROM dbo.ChiTietPhieuNhap c JOIN dbo.PhuTung p ON p.MaPhuTung=c.MaPhuTung LEFT JOIN dbo.NhaCungCap n ON n.MaNCC=c.MaNCC WHERE c.MaPhieuNhap=@id", XuLyDuLieu.ThamSo("@id", ma));
            });
            txtTimKiem.TextChanged += (nguoiGui, suKien) =>
            {
                var bang = dgvPhieuNhap.DataSource as System.Data.DataTable;
                if (bang != null)
                    bang.DefaultView.RowFilter = "MaPhieuNhap LIKE '%" + txtTimKiem.Text.Replace("'", "''").Replace("[", "[[]").Replace("%", "[%]").Replace("*", "[*]") + "%'";
            };
        }

        public override void TaiDuLieu()
        {
            PhienDangNhap.YeuCau("NHAP_KHO");
            dgvDanhSach.DataSource = duLieu.DocBang("SELECT p.MaPhieuNhap,p.NgayNhap,n.TenNhanVien,p.TongTien,p.GhiChu FROM dbo.PhieuNhap p JOIN dbo.NhanVien n ON n.MaNhanVien=p.MaNhanVienNhap ORDER BY p.NgayNhap DESC");
        }
    }

    public class frmChiTietNhap : TrangDuLieu
    {
        readonly string maPhieuNhap;
        public frmChiTietNhap(string khoa) : base("Chi tiết nhập — " + khoa)
        {
            maPhieuNhap = khoa;
        }

        public override void TaiDuLieu()
        {
            PhienDangNhap.YeuCau("NHAP_KHO");
            dgvDanhSach.DataSource = duLieu.DocBang("SELECT p.TenPhuTung,n.TenNCC,c.SoLuong,c.DonGiaNhap,c.ThanhTien,c.HanSuDung FROM dbo.ChiTietPhieuNhap c JOIN dbo.PhuTung p ON p.MaPhuTung=c.MaPhuTung LEFT JOIN dbo.NhaCungCap n ON n.MaNCC=c.MaNCC WHERE c.MaPhieuNhap=@id", XuLyDuLieu.ThamSo("@id", maPhieuNhap));
        }
    }
}
