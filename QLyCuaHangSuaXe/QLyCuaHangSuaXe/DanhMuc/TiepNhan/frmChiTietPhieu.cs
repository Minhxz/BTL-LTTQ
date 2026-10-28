using QLyCuaHangSuaXe.LopDungChung;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace QLyCuaHangSuaXe.DanhMuc.TiepNhan
{
    public partial class frmChiTietPhieu : TrangThietKe
    {
        readonly string maPhieu;
        readonly DataTable chiTiet;
        public frmChiTietPhieu() : this(null) { }
        public frmChiTietPhieu(string maPhieu, DataTable chiTiet = null)
        {
            InitializeComponent();
            this.maPhieu = maPhieu;
            this.chiTiet = chiTiet;
            GanThietKe(dgvChiTietPhieu);
            if (!string.IsNullOrEmpty(maPhieu))
            {
                Text = "Chi tiết phiếu " + maPhieu;
                lblTieuDe.Text = "CHI TIẾT PHIẾU " + maPhieu;
            }
        }
        public override void TaiDuLieu()
        {
            if (string.IsNullOrEmpty(maPhieu)) return;
            PhienDangNhap.YeuCau("TIEP_NHAN");
            DataTable bang = chiTiet ?? duLieu.DocBang("SELECT N'Dịch vụ' AS Loai,d.TenDichVu AS NoiDung,c.SoLuong,c.ThanhTien/NULLIF(c.SoLuong,0) AS DonGia,c.ThanhTien FROM dbo.ChiTietPhieuTiepNhan c JOIN dbo.DichVu d ON d.MaDichVu=c.MaDichVu WHERE c.MaPhieuTiepNhan=@ma", XuLyDuLieu.ThamSo("@ma", maPhieu));
            if (bang.Columns.Contains("TenDichVu"))
            {
                var bangHienThi = new DataTable();
                bangHienThi.Columns.Add("Loai");
                bangHienThi.Columns.Add("NoiDung");
                bangHienThi.Columns.Add("SoLuong", typeof(int));
                bangHienThi.Columns.Add("DonGia", typeof(decimal));
                bangHienThi.Columns.Add("ThanhTien", typeof(decimal));
                foreach (DataRow dong in bang.Rows)
                {
                    string noiDung = dong["TenDichVu"].ToString();
                    if (bang.Columns.Contains("PhuTung") && dong["PhuTung"].ToString().Length > 0) noiDung += "\nPhụ tùng: " + dong["PhuTung"];
                    int soLuong = Convert.ToInt32(dong["SoLuong"]);
                    decimal thanhTien = Convert.ToDecimal(dong["ThanhTien"]);
                    bangHienThi.Rows.Add("Dịch vụ", noiDung, soLuong, soLuong == 0 ? 0 : thanhTien / soLuong, thanhTien);
                }
                bang = bangHienThi;
            }
            dgvChiTietPhieu.DataSource = bang;
            lblTongTien.Text = bang.Rows.Count + " hạng mục  |  Tổng tiền: " + bang.AsEnumerable().Sum(d => Convert.ToDecimal(d["ThanhTien"])).ToString("N0") + " đ";
        }
        void btnDong_Bam(object nguoiGui, EventArgs suKien) { Close(); }
    }
}
