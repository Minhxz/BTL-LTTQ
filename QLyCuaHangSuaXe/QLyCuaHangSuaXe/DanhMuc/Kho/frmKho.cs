using System;
using System.Data;
using System.Windows.Forms;
using QLyCuaHangSuaXe.LopDungChung;

namespace btl
{
    public partial class frmKho : TrangThietKe
    {
        public frmKho()
        {
            InitializeComponent();
            GanThietKe(dgvPhuTung);
        }
        public override void TaiDuLieu()
        {
            PhienDangNhap.YeuCau("NHAP_KHO");
            dgvPhuTung.DataSource=duLieu.DocBang(@"SELECT p.MaPhuTung,p.TenPhuTung,n.TenNhaCungCap AS TenNCC,p.SoLuongTon,p.DonGiaNhap,p.DonGiaBan,p.HanSuDung,p.TrangThai FROM dbo.PhuTung p JOIN dbo.NhaCungCap n ON n.MaNhaCungCap=p.MaNhaCungCap WHERE p.MaPhuTung LIKE @q OR p.TenPhuTung LIKE @q OR n.TenNhaCungCap LIKE @q ORDER BY p.MaPhuTung",XuLyDuLieu.ThamSo("@q","%"+txtTimPhuTung.Text.Trim()+"%"));
        }
        void btnNhapPhuTung_Bam(object s, EventArgs e) { GiaoDien.ThucHien(() => { using (var trang = new frmNhapPhieuNhap()) trang.ShowDialog(this); TaiDuLieu(); }); }
        void btnNhaCungCap_Bam(object s, EventArgs e) { GiaoDien.ThucHien(() => { using (var trang = new frmThemNCC()) trang.ShowDialog(this); TaiDuLieu(); }); }
        void txtTim_TextChanged(object nguoiGui,EventArgs suKien){if(IsHandleCreated)GiaoDien.ThucHien(TaiDuLieu);}
        void btnDanhSachPhieuNhap_Bam(object nguoiGui,EventArgs suKien){GiaoDien.ThucHien(()=>{using(var trang=new frmPhieuNhap())trang.ShowDialog(this);TaiDuLieu();});}
        void btnThoat_Bam_1(object nguoiGui,EventArgs suKien){Close();}
        void btnNgungSuDung_Bam(object nguoiGui,EventArgs suKien)
        {
            GiaoDien.ThucHien(()=>{
                PhienDangNhap.YeuCau("NHAP_KHO");string ma=GiaoDien.GiaTriO(dgvPhuTung,"MaPhuTung");if(ma.Length==0)throw new InvalidOperationException("Chọn phụ tùng.");
                string trangThai=GiaoDien.GiaTriO(dgvPhuTung,"TrangThai")=="Đang kinh doanh"?"Ngừng kinh doanh":"Đang kinh doanh";
                duLieu.CapNhatDuLieu("UPDATE dbo.PhuTung SET TrangThai=@s WHERE MaPhuTung=@id",XuLyDuLieu.ThamSo("@s",trangThai),XuLyDuLieu.ThamSo("@id",ma));TaiDuLieu();
            });
        }
    }
}
