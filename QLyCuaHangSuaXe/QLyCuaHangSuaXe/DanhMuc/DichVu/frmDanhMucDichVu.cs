using QLyCuaHangSuaXe.LopDungChung;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace QLyCuaHangSuaXe.DanhMuc.DichVu
{
    public partial class frmDanhMucDichVu : TrangThietKe
    {
        string maDichVuDangChon = "";
        public frmDanhMucDichVu()
        {
            InitializeComponent();
            GanThietKe(dgvDichVu, lblHuongDan);
            Text = "Danh mục dịch vụ";
            dgvDichVu.Columns.Clear();
            dgvDichVu.AutoGenerateColumns = true;
            cboTrangThai.Items.Clear();
            cboTrangThai.Items.AddRange(new object[] { "Đang sử dụng", "Ngừng sử dụng" });
            cboTrangThai.SelectedIndex = 0;
            txtMaDV.ReadOnly = true;
            lblTieuDe.Text = "DANH MỤC DỊCH VỤ";
            btnDinhMuc.Click += (nguoiGui, suKien) => GiaoDien.ThucHien(MoBoPhuTung);
            btnKho.Click += (nguoiGui, suKien) => GiaoDien.ThucHien(() =>
            {
                using (var trang = new btl.frmKho())
                    trang.ShowDialog(this);
            });
            btnThem.Click += (nguoiGui, suKien) =>
            {
                maDichVuDangChon = "";
                txtMaDV.Clear();
                txtTenDichVu.Clear();
                txtMoTa.Clear();
                numGiaDichVu.Value = numLuongNhiemVu.Value = numPhutDuKien.Value = 0;
            };
            btnSua.Text = "Lưu dịch vụ";
            btnSua.Click += (nguoiGui, suKien) => GiaoDien.ThucHien(Luu);
            btnXoa.Click += (nguoiGui, suKien) => GiaoDien.ThucHien(Xoa);
            btnTimKiem.Click += (nguoiGui, suKien) => GiaoDien.ThucHien(TaiDuLieu);
            btnLamMoi.Click += (nguoiGui, suKien) => GiaoDien.ThucHien(TaiDuLieu);
            btnDong.Click += (nguoiGui, suKien) => Close();
            dgvDanhSach.SelectionChanged += (nguoiGui, suKien) => ChonDong();
        }

        public override void TaiDuLieu()
        {
            PhienDangNhap.YeuCau("DICH_VU");
            dgvDanhSach.DataSource = duLieu.DocBang("SELECT MaDichVu,TenDichVu,DonGiaDichVu,LuongNhiemVu,ThoiGianDuKien,MoTa,TrangThai FROM dbo.DichVu WHERE TenDichVu LIKE @q ORDER BY TenDichVu", XuLyDuLieu.ThamSo("@q", "%" + txtTimKiem.Text.Trim() + "%"));
            lblThongBao.Text = "Chọn dịch vụ để đổi giá; Bộ phụ tùng cho phép cấu hình nhiều phụ tùng và số lượng.";
        }

        void ChonDong()
        {
            if (dgvDanhSach.CurrentRow == null)
                return;
            maDichVuDangChon = GiaoDien.GiaTriO(dgvDanhSach, "MaDichVu");
            txtMaDV.Text = maDichVuDangChon;
            txtTenDichVu.Text = GiaoDien.GiaTriO(dgvDanhSach, "TenDichVu");
            txtMoTa.Text = GiaoDien.GiaTriO(dgvDanhSach, "MoTa");
            numGiaDichVu.Value = Convert.ToDecimal(dgvDanhSach.CurrentRow.Cells["DonGiaDichVu"].Value);
            numLuongNhiemVu.Value = Convert.ToDecimal(dgvDanhSach.CurrentRow.Cells["LuongNhiemVu"].Value);
            decimal soPhut;
            numPhutDuKien.Value = decimal.TryParse(GiaoDien.GiaTriO(dgvDanhSach, "ThoiGianDuKien"), out soPhut) ? Math.Min(soPhut, numPhutDuKien.Maximum) : 0;
            cboTrangThai.SelectedItem = GiaoDien.GiaTriO(dgvDanhSach, "TrangThai");
        }

        void Luu()
        {
            PhienDangNhap.YeuCau("DICH_VU");
            string ten = GiaoDien.BatBuocNhap(txtTenDichVu, "Tên dịch vụ", 100);
            if (txtMoTa.Text.Length > 300)
                throw new InvalidOperationException("Mô tả tối đa 300 ký tự.");
            bool themMoi = maDichVuDangChon.Length == 0;
            string khoa = themMoi ? XuLyDuLieu.TaoMaMoi("DV") : maDichVuDangChon;
            duLieu.CapNhatDuLieu(themMoi ? "INSERT dbo.DichVu(MaDichVu,TenDichVu,DonGiaDichVu,LuongNhiemVu,ThoiGianDuKien,MoTa,TrangThai) VALUES(@id,@n,@p,@w,@m,@d,@s)" : "UPDATE dbo.DichVu SET TenDichVu=@n,DonGiaDichVu=@p,LuongNhiemVu=@w,ThoiGianDuKien=@m,MoTa=@d,TrangThai=@s WHERE MaDichVu=@id", XuLyDuLieu.ThamSo("@id", khoa), XuLyDuLieu.ThamSo("@n", ten), XuLyDuLieu.ThamSo("@p", numGiaDichVu.Value), XuLyDuLieu.ThamSo("@w", numLuongNhiemVu.Value), XuLyDuLieu.ThamSo("@m", (int)numPhutDuKien.Value), XuLyDuLieu.ThamSo("@d", txtMoTa.Text.Trim()), XuLyDuLieu.ThamSo("@s", cboTrangThai.Text));
            TaiDuLieu();
            lblThongBao.Text = "Đã lưu " + khoa;
        }

        void Xoa()
        {
            PhienDangNhap.YeuCau("DICH_VU");
            if (maDichVuDangChon.Length == 0)
                return;
            string khoa = maDichVuDangChon;
            duLieu.TrongGiaoDich<int>((ketNoi, giaoDich) =>
            {
                var daSuDung = XuLyDuLieu.DocDuLieu(ketNoi, giaoDich, "SELECT 1 FROM dbo.ChiTietPhieuTiepNhan WHERE MaDichVu=@id UNION ALL SELECT 1 FROM dbo.ChiTietHoaDon WHERE MaDichVu=@id", XuLyDuLieu.ThamSo("@id", khoa));
                if (daSuDung.Rows.Count > 0)
                    return XuLyDuLieu.ThucThi(ketNoi, giaoDich, "UPDATE dbo.DichVu SET TrangThai=N'Ngừng sử dụng' WHERE MaDichVu=@id", XuLyDuLieu.ThamSo("@id", khoa));
                XuLyDuLieu.ThucThi(ketNoi, giaoDich, "DELETE dbo.DinhMucDichVu WHERE MaDichVu=@id", XuLyDuLieu.ThamSo("@id", khoa));
                return XuLyDuLieu.ThucThi(ketNoi, giaoDich, "DELETE dbo.DichVu WHERE MaDichVu=@id", XuLyDuLieu.ThamSo("@id", khoa));
            });
            maDichVuDangChon = "";
            TaiDuLieu();
            lblThongBao.Text = "Đã xóa dịch vụ chưa sử dụng hoặc ngừng dùng dịch vụ có lịch sử.";
        }

        void MoBoPhuTung()
        {
            if (maDichVuDangChon.Length == 0)
                throw new InvalidOperationException("Lưu hoặc chọn dịch vụ trước.");
            using (var frmChiTiet = new frmDinhMuc(maDichVuDangChon))
                frmChiTiet.ShowDialog(this);
        }
    }

    public class frmDinhMuc : TrangDuLieu
    {
        readonly string maDichVu;
        public frmDinhMuc(string maDichVuDuocChon) : base("Bộ phụ tùng — " + maDichVuDuocChon)
        {
            maDichVu = maDichVuDuocChon;
            dgvDanhSach.ReadOnly = false;
            var giaTriThuHai = GiaoDien.TaoThanhThaoTac();
            giaTriThuHai.Controls.Add(GiaoDien.TaoNut("Lưu định mức", Luu));
            ThemVungNhap(giaTriThuHai);
            lblThongBao.Text = "Tick phụ tùng và nhập số lượng cho mỗi đơn vị dịch vụ. Không chọn dòng nào = dịch vụ chỉ có tiền công.";
        }

        public override void TaiDuLieu()
        {
            PhienDangNhap.YeuCau("DICH_VU");
            dgvDanhSach.DataSource = duLieu.DocBang("SELECT CAST(CASE WHEN m.MaPhuTung IS NULL THEN 0 ELSE 1 END AS BIT) AS Chon,p.MaPhuTung,p.TenPhuTung,COALESCE(m.SoLuong,1) AS SoLuong,p.TrangThai FROM dbo.PhuTung p LEFT JOIN dbo.DinhMucDichVu m ON m.MaPhuTung=p.MaPhuTung AND m.MaDichVu=@id", XuLyDuLieu.ThamSo("@id", maDichVu));
            foreach (DataGridViewColumn cotHienThi in dgvDanhSach.Columns)
                cotHienThi.ReadOnly = cotHienThi.Name != "Chon" && cotHienThi.Name != "SoLuong";
        }

        void Luu()
        {
            PhienDangNhap.YeuCau("DICH_VU");
            dgvDanhSach.EndEdit();
            var cacDong = ((DataTable)dgvDanhSach.DataSource).AsEnumerable().Where(dongDuLieu => Convert.ToBoolean(dongDuLieu["Chon"])).ToList();
            if (cacDong.Any(dongDuLieu => Convert.ToInt32(dongDuLieu["SoLuong"]) <= 0 || Convert.ToInt32(dongDuLieu["SoLuong"]) > 1000))
                throw new InvalidOperationException("Số lượng phải từ 1–1000.");
            duLieu.TrongGiaoDich<int>((ketNoi, giaoDich) =>
            {
                XuLyDuLieu.ThucThi(ketNoi, giaoDich, "SELECT MaDichVu FROM dbo.DichVu WITH(UPDLOCK,HOLDLOCK) WHERE MaDichVu=@id", XuLyDuLieu.ThamSo("@id", maDichVu));
                XuLyDuLieu.ThucThi(ketNoi, giaoDich, "DELETE dbo.DinhMucDichVu WHERE MaDichVu=@id", XuLyDuLieu.ThamSo("@id", maDichVu));
                foreach (var dongDuLieu in cacDong)
                    XuLyDuLieu.ThucThi(ketNoi, giaoDich, "INSERT dbo.DinhMucDichVu VALUES(@id,@p,@q)", XuLyDuLieu.ThamSo("@id", maDichVu), XuLyDuLieu.ThamSo("@p", dongDuLieu["MaPhuTung"]), XuLyDuLieu.ThamSo("@q", dongDuLieu["SoLuong"]));
                XuLyDuLieu.ThucThi(ketNoi, giaoDich, "UPDATE dbo.DichVu SET MaPhuTung=NULL WHERE MaDichVu=@id", XuLyDuLieu.ThamSo("@id", maDichVu));
                return 0;
            });
            Close();
        }
    }
}
