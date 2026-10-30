using QLyCuaHangSuaXe.LopDungChung;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace QLyCuaHangSuaXe.DanhMuc.DichVu
{
    public partial class frmDinhMuc : TrangThietKe
    {
        readonly string maDichVu;
        public frmDinhMuc() : this(null) { }
        public frmDinhMuc(string maDichVuDuocChon)
        {
            InitializeComponent();
            maDichVu = maDichVuDuocChon;
            GanThietKe(dgvDinhMuc);
            dgvDinhMuc.ReadOnly = false;
            if (!string.IsNullOrEmpty(maDichVu))
            {
                Text = "Bộ phụ tùng dịch vụ " + maDichVu;
                lblTieuDe.Text = "BỘ PHỤ TÙNG DỊCH VỤ " + maDichVu;
            }
        }
        public override void TaiDuLieu()
        {
            if (string.IsNullOrEmpty(maDichVu))
                return;
            PhienDangNhap.YeuCau("DICH_VU");
            dgvDanhSach.DataSource = duLieu.DocBang("SELECT CAST(CASE WHEN d.MaPhuTung IS NULL THEN 0 ELSE 1 END AS BIT) AS Chon,p.MaPhuTung,p.TenPhuTung,COALESCE(d.SoLuong,1) AS SoLuong,p.SoLuongTon,p.TrangThai FROM dbo.PhuTung p LEFT JOIN dbo.ChiTietDichVu d ON d.MaPhuTung=p.MaPhuTung AND d.MaDichVu=@id ORDER BY p.MaPhuTung", XuLyDuLieu.ThamSo("@id", maDichVu));
            foreach (DataGridViewColumn cot in dgvDanhSach.Columns)
                cot.ReadOnly = cot.Name != "Chon" && cot.Name != "SoLuong";
            dgvDanhSach.Columns["Chon"].HeaderText = "Sử dụng";
        }
        void btnLuuDinhMuc_Bam(object nguoiGui, EventArgs suKien)
        {
            GiaoDien.ThucHien(Luu);
        }
        void btnDong_Bam(object nguoiGui, EventArgs suKien)
        {
            Close();
        }
        void Luu()
        {
            PhienDangNhap.YeuCau("DICH_VU");
            dgvDanhSach.EndEdit();
            var chon = ((DataTable)dgvDanhSach.DataSource).AsEnumerable().Where(d => Convert.ToBoolean(d["Chon"])).ToList();
            if (chon.Any(d => Convert.ToInt32(d["SoLuong"]) <= 0 || Convert.ToInt32(d["SoLuong"]) > 1000))
                throw new InvalidOperationException("Số lượng phải từ 1 đến 1000.");
            duLieu.TrongGiaoDich<int>((kn, gd) =>
            {
                if (XuLyDuLieu.DocDuLieu(kn, gd, "SELECT 1 FROM dbo.DichVu WITH(UPDLOCK,HOLDLOCK) WHERE MaDichVu=@id", XuLyDuLieu.ThamSo("@id", maDichVu)).Rows.Count != 1)
                    throw new InvalidOperationException("Dịch vụ không tồn tại.");
                if (XuLyDuLieu.DocDuLieu(kn, gd, "SELECT 1 FROM dbo.ChiTietPhieuTiepNhan c JOIN dbo.PhieuTiepNhan p ON p.MaPhieuTiepNhan=c.MaPhieuTiepNhan WHERE c.MaDichVu=@id AND p.TrangThai IN(N'Chờ sửa',N'Đang sửa')", XuLyDuLieu.ThamSo("@id", maDichVu)).Rows.Count > 0)
                    throw new InvalidOperationException("Dịch vụ đang có xe chờ sửa hoặc đang sửa. Hoàn tất các phiếu trước khi đổi bộ phụ tùng.");
                XuLyDuLieu.ThucThi(kn, gd, "DELETE dbo.ChiTietDichVu WHERE MaDichVu=@id", XuLyDuLieu.ThamSo("@id", maDichVu));
                foreach (var dong in chon)
                    XuLyDuLieu.ThucThi(kn, gd, "INSERT dbo.ChiTietDichVu(MaDichVu,MaPhuTung,SoLuong) VALUES(@id,@pt,@sl)", XuLyDuLieu.ThamSo("@id", maDichVu), XuLyDuLieu.ThamSo("@pt", dong["MaPhuTung"]), XuLyDuLieu.ThamSo("@sl", dong["SoLuong"]));
                return 0;
            });
            Close();
        }

        private void lblTieuDe_Click(object sender, EventArgs e)
        {

        }
    }
}
