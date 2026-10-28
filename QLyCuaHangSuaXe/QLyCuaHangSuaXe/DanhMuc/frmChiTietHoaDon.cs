using QLyCuaHangSuaXe.LopDungChung;
using System;
using System.Data;
using System.Windows.Forms;

namespace QLyCuaHangSuaXe.DanhMuc
{
    public partial class frmChiTietHoaDon : TrangThietKe
    {
        readonly string maDaChon;
        string maPhieu,maHoaDonDuKien;
        readonly NghiepVuSuaXe nghiepVu=new NghiepVuSuaXe();
        public frmChiTietHoaDon()
        {
            InitializeComponent();
            GanThietKe(dgvChiTietHoaDon);
            btnTienMat.Click += (nguoiGui, suKien) => GiaoDien.ThucHien(ThanhToanTienMat);
            btnQR.Click += (nguoiGui, suKien) => GiaoDien.ThucHien(ThanhToanBangQR);
            btnDong.Click += (nguoiGui, suKien) => Close();
        }
        public frmChiTietHoaDon(string maHoaDon) : this()
        {
            maDaChon = maHoaDon;
        }
        public override void TaiDuLieu()
        {
            if (string.IsNullOrEmpty(maDaChon)) return;
            PhienDangNhap.YeuCau("HOA_DON");
            var bang=duLieu.DocBang(@"SELECT p.MaPhieuTiepNhan,p.TrangThai,COALESCE(h.MaHoaDon,'') AS MaHoaDon,COALESCE(h.TienThanhToan,t.TongTien,0) AS TongTien FROM dbo.PhieuTiepNhan p LEFT JOIN dbo.HoaDon h ON h.MaPhieuTiepNhan=p.MaPhieuTiepNhan OUTER APPLY(SELECT SUM(ThanhTien) AS TongTien FROM dbo.ChiTietPhieuTiepNhan c WHERE c.MaPhieuTiepNhan=p.MaPhieuTiepNhan)t WHERE p.MaPhieuTiepNhan=@id OR h.MaHoaDon=@id",XuLyDuLieu.ThamSo("@id",maDaChon));
            if(bang.Rows.Count!=1)throw new InvalidOperationException("Không tìm thấy phiếu hoặc hóa đơn.");var dong=bang.Rows[0];if(dong["TrangThai"].ToString()!="Hoàn thành")throw new InvalidOperationException("Phiếu chưa hoàn thành sửa chữa.");
            maPhieu=dong["MaPhieuTiepNhan"].ToString();bool chuaThu=dong["MaHoaDon"].ToString().Length==0;
            maHoaDonDuKien=chuaThu?nghiepVu.LapHoaDon(maPhieu):dong["MaHoaDon"].ToString();Text="Hóa đơn "+maHoaDonDuKien;
            btnTienMat.Enabled=btnQR.Enabled=chuaThu;lblThanhToan.Text=maHoaDonDuKien+"  •  "+Convert.ToDecimal(dong["TongTien"]).ToString("N0")+" đ  •  "+(chuaThu?"Chưa thanh toán":"Đã thanh toán");
            dgvDanhSach.DataSource=duLieu.DocBang(@"SELECT c.MaDichVu,d.TenDichVu,c.SoLuong,CAST(c.ThanhTien/c.SoLuong AS DECIMAL(18,0)) AS DonGia,c.ThanhTien FROM dbo.ChiTietPhieuTiepNhan c JOIN dbo.DichVu d ON d.MaDichVu=c.MaDichVu WHERE c.MaPhieuTiepNhan=@id ORDER BY c.MaDichVu",XuLyDuLieu.ThamSo("@id",maPhieu));
        }
        void ThanhToanTienMat()
        {
            if(MessageBox.Show(this,"Đã nhận đủ tiền mặt cho hóa đơn này?","Thanh toán tiền mặt",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2)!=DialogResult.Yes)return;
            nghiepVu.ThanhToanPhieu(maPhieu,"Tiền mặt",maHoaDonDuKien);TaiDuLieu();
        }
        void ThanhToanBangQR()
        {
            using(var trang=new frmThanhToanQR(maPhieu,maHoaDonDuKien))trang.ShowDialog(this);TaiDuLieu();
        }
    }
}
