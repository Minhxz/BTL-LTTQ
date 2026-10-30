using QLyCuaHangSuaXe.LopDungChung;
using System;
using System.Data;
using System.Windows.Forms;

namespace QLyCuaHangSuaXe.DanhMuc
{
    public partial class frmPhanCong : TrangThietKe
    {
        readonly NghiepVuSuaXe nghiepVu = new NghiepVuSuaXe();
        public frmPhanCong()
        {
            InitializeComponent();
            GanThietKe(dgvPhanCong);
            btnBatDauSua.Click += (nguoiGui, suKien) => GiaoDien.ThucHien(() => CapNhatTienDo("Đang sửa"));
            btnHoanThanh.Click += (nguoiGui, suKien) => GiaoDien.ThucHien(() => CapNhatTienDo("Hoàn thành"));
            btnLamMoi.Click += (nguoiGui, suKien) => GiaoDien.ThucHien(TaiDuLieu);
            btnGiaoXe.Click += (nguoiGui, suKien) => GiaoDien.ThucHien(PhanCong);
            btnHuyPhieu.Click += (nguoiGui, suKien) => GiaoDien.ThucHien(() =>
            {
                nghiepVu.HuyChuaSua(GiaoDien.GiaTriO(dgvPhanCong, "MaPhieuTiepNhan"));
                TaiDuLieu();
            });
            dgvPhanCong.CurrentCellChanged += (nguoiGui, suKien) => txtMaPhieu.Text = GiaoDien.GiaTriO(dgvPhanCong, "MaPhieuTiepNhan");
        }
        public override void TaiDuLieu()
        {
            PhienDangNhap.YeuCau("PHAN_CONG");
            bool quanLy = PhanQuyenNguoiDung.CoQuyen(PhienDangNhap.VaiTro, "NHAN_VIEN");
            dgvDanhSach.DataSource = duLieu.DocBang(@"SELECT p.MaPhieuTiepNhan,x.BienXe AS BienSo,k.TenKhachHang,COALESCE(n.MaNhanVien,'') AS MaNhanVien,COALESCE(n.HoTen,N'Chưa phân công') AS TenNhanVien,p.TrangThai,c.ThoiGianBatDau,c.ThoiGianHoanThanh,p.NgayHen FROM dbo.PhieuTiepNhan p JOIN dbo.Xe x ON x.MaXe=p.MaXe JOIN dbo.KhachHang k ON k.MaKhachHang=x.MaKhachHang LEFT JOIN dbo.ChiTietPhanCong c ON c.MaXe=p.MaXe LEFT JOIN dbo.NhanVien n ON n.MaNhanVien=c.MaNhanVien WHERE (@all=1 OR c.MaNhanVien=@nv) ORDER BY p.NgayLap DESC,p.MaPhieuTiepNhan DESC", XuLyDuLieu.ThamSo("@all", quanLy), XuLyDuLieu.ThamSo("@nv", PhienDangNhap.MaNhanVien));
            grbGiaoXe.Visible = btnHuyPhieu.Visible = quanLy;
            tlpTrang.RowStyles[2].Height = quanLy ? 86F : 0F;
            if (quanLy)
                GiaoDien.GanDuLieu(cboTho, nghiepVu.ThoRanh(), "HienThi", "MaNhanVien");
            if (dgvDanhSach.Columns.Contains("ThoiGianHoanThanh"))
                dgvDanhSach.Columns["ThoiGianHoanThanh"].HeaderText = "Kết thúc / dự kiến";
        }
        void CapNhatTienDo(string trangThai)
        {
            nghiepVu.TrangThaiCongViec(GiaoDien.GiaTriO(dgvDanhSach, "MaPhieuTiepNhan"), GiaoDien.GiaTriO(dgvDanhSach, "MaNhanVien"), trangThai);
            TaiDuLieu();
        }
        void PhanCong()
        {
            PhienDangNhap.YeuCau("NHAN_VIEN");
            string ma = GiaoDien.BatBuocNhap(txtMaPhieu, "Mã phiếu", 10), maTho = GiaoDien.GiaTriDangChon(cboTho);
            duLieu.TrongGiaoDich<int>((ketNoi, giaoDich) =>
            {
                var phieu = XuLyDuLieu.DocDuLieu(ketNoi, giaoDich, "SELECT MaXe,NgayHen FROM dbo.PhieuTiepNhan WITH(UPDLOCK,HOLDLOCK) WHERE MaPhieuTiepNhan=@id AND TrangThai IN(N'Chờ sửa',N'Đang sửa')", XuLyDuLieu.ThamSo("@id", ma));
                if (phieu.Rows.Count != 1)
                    throw new InvalidOperationException("Chọn phiếu đang chờ sửa hoặc đang sửa.");
                string maXe = phieu.Rows[0]["MaXe"].ToString();
                if (XuLyDuLieu.DocDuLieu(ketNoi, giaoDich, "SELECT 1 FROM dbo.ChiTietPhanCong WITH(UPDLOCK,HOLDLOCK) WHERE MaXe=@xe", XuLyDuLieu.ThamSo("@xe", maXe)).Rows.Count > 0)
                    throw new InvalidOperationException("Xe đã có thợ phụ trách.");
                var tho = XuLyDuLieu.DocDuLieu(ketNoi, giaoDich, "SELECT c.TenChucVu FROM dbo.NhanVien n WITH(UPDLOCK,HOLDLOCK) JOIN dbo.ChucVu c ON c.MaChucVu=n.MaChucVu WHERE n.MaNhanVien=@nv AND n.TrangThai=1", XuLyDuLieu.ThamSo("@nv", maTho));
                if (tho.Rows.Count != 1 || !PhanQuyenNguoiDung.LaTho(tho.Rows[0][0].ToString()))
                    throw new InvalidOperationException("Chọn thợ đang làm.");
                int soXe = Convert.ToInt32(XuLyDuLieu.DocDuLieu(ketNoi, giaoDich, "SELECT COUNT(DISTINCT c.MaXe) FROM dbo.ChiTietPhanCong c WITH(UPDLOCK,HOLDLOCK) JOIN dbo.PhieuTiepNhan p ON p.MaXe=c.MaXe WHERE c.MaNhanVien=@nv AND p.TrangThai IN(N'Chờ sửa',N'Đang sửa')", XuLyDuLieu.ThamSo("@nv", maTho)).Rows[0][0]);
                if (soXe >= PhanQuyenNguoiDung.GioiHanXe(tho.Rows[0][0].ToString()))
                    throw new InvalidOperationException("Thợ đã nhận đủ xe.");
                DateTime batDau = DateTime.Now, ketThuc = ((DateTime)phieu.Rows[0]["NgayHen"]).Date.AddDays(1);
                if (ketThuc <= batDau)
                    ketThuc = batDau.AddHours(1);
                return XuLyDuLieu.ThucThi(ketNoi, giaoDich, "INSERT dbo.ChiTietPhanCong(MaNhanVien,MaXe,ThoiGianBatDau,ThoiGianHoanThanh) VALUES(@nv,@xe,@dau,@cuoi)", XuLyDuLieu.ThamSo("@nv", maTho), XuLyDuLieu.ThamSo("@xe", maXe), XuLyDuLieu.ThamSo("@dau", batDau), XuLyDuLieu.ThamSo("@cuoi", ketThuc));
            });
            TaiDuLieu();
        }
    }
}
