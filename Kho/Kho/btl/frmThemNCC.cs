using QLyCuaHangSuaXe.LopDungChung;
using System.Windows.Forms;

namespace btl
{
    public partial class frmThemNCC : TrangThietKe
    {
        string maNhaCungCapDangChon = "";
        public frmThemNCC()
        {
            InitializeComponent();
            GanThietKe(null);
            Text = "Nhà cung cấp";
            txtMaNCC.ReadOnly = true;
            txtMaNCC.Text = "Tự động khi lưu";
            btnXacNhan.Click += (nguoiGui, suKien) => GiaoDien.ThucHien(() =>
            {
                Luu();
                Close();
            });
            btnHuy.Click += (nguoiGui, suKien) => Close();
        }

        public override void TaiDuLieu()
        {
            PhienDangNhap.YeuCau("NHAP_KHO");
        }

        void Luu()
        {
            PhienDangNhap.YeuCau("NHAP_KHO");
            string ten = GiaoDien.BatBuocNhap(txtTenNhaCungCap, "Tên nhà cung cấp", 100), thamSo = GiaoDien.KiemTraDienThoai(txtDienThoai), giaTriThuNhat = txtDiaChi.Text.Trim();
            if (giaTriThuNhat.Length == 0 || giaTriThuNhat.Length > 200)
                throw new System.InvalidOperationException("Địa chỉ bắt buộc, tối đa 200 ký tự.");
            bool themMoi = maNhaCungCapDangChon.Length == 0;
            string khoa = themMoi ? XuLyDuLieu.TaoMaMoi("NC") : maNhaCungCapDangChon;
            duLieu.CapNhatDuLieu(themMoi ? "INSERT dbo.NhaCungCap VALUES(@id,@n,@p,@a)" : "UPDATE dbo.NhaCungCap SET TenNCC=@n,SoDienThoai=@p,DiaChi=@a WHERE MaNCC=@id", XuLyDuLieu.ThamSo("@id", khoa), XuLyDuLieu.ThamSo("@n", ten), XuLyDuLieu.ThamSo("@p", thamSo), XuLyDuLieu.ThamSo("@a", giaTriThuNhat));
            TaiDuLieu();
        }
    }
}
