using System;
using System.Windows.Forms;
using QLyCuaHangSuaXe.LopDungChung;
namespace btl
{
    public partial class frmThemNCC : TrangThietKe
    {
        public string MaNhaCungCapMoi { get { return txtMaNCC.Text; } }
        public frmThemNCC()
        {
            InitializeComponent();
            GanThietKe(null);
        }
        public override void TaiDuLieu(){PhienDangNhap.YeuCau("NHAP_KHO");txtMaNCC.Text=XuLyDuLieu.TaoMaMoi("NCC");}
        void Luu()
        {
            PhienDangNhap.YeuCau("NHAP_KHO");string ten=GiaoDien.BatBuocNhap(txtTenNCC,"Tên nhà cung cấp",150),sdt=GiaoDien.KiemTraDienThoai(txtSDT),diaChi=txtDiaChi.Text.Trim();
            if(diaChi.Length==0||diaChi.Length>255)throw new InvalidOperationException("Nhập địa chỉ, tối đa 255 ký tự.");
            duLieu.CapNhatDuLieu("INSERT dbo.NhaCungCap(MaNhaCungCap,TenNhaCungCap,SoDienThoai,DiaChi) VALUES(@id,@n,@p,@a)",XuLyDuLieu.ThamSo("@id",XuLyDuLieu.KiemTraMa(txtMaNCC.Text,"NCC")),XuLyDuLieu.ThamSo("@n",ten),XuLyDuLieu.ThamSo("@p",sdt),XuLyDuLieu.ThamSo("@a",diaChi));
        }
        void btnXacNhan_Bam(object s,EventArgs e){GiaoDien.ThucHien(()=>{Luu();DialogResult=DialogResult.OK;Close();});}
        void btnHuy_Bam(object s,EventArgs e){Close();}
    }
}
