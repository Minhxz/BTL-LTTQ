using QLyCuaHangSuaXe.LopDungChung;
using System.Windows.Forms;

namespace QLyCuaHangSuaXe.DanhMuc.DangNhap
{
    public partial class frmPhanQuyen : TrangThietKe
    {
        public frmPhanQuyen()
        {
            InitializeComponent();
            GanThietKe(null);
            lstVaiTro.Items.Clear();
            lstVaiTro.Items.AddRange(PhanQuyenNguoiDung.VaiTro);
            lstVaiTro.SelectedIndexChanged += (nguoiGui, suKien) => HienThiQuyen();
            btnDong.Click += (nguoiGui, suKien) => Close();
        }

        void HienThiQuyen()
        {
            string vaiTro = lstVaiTro.Text;
            CheckBox[] cacO =
            {
                chkQuanLyTaiKhoan,
                chkPhanQuyen,
                chkTiepNhanSuaChua,
                chkDanhMucDichVu,
                chkNhapKhoPhuTung,
                chkBaoCaoThongKe,
                chkNhanVien, chkPhanCong, chkBangCong, chkHoaDon
            };
            string[] cacQuyen =
            {
                "QUAN_LY_TAI_KHOAN",
                "PHAN_QUYEN",
                "TIEP_NHAN",
                "DICH_VU",
                "NHAP_KHO",
                "BAO_CAO",
                "NHAN_VIEN", "PHAN_CONG", "CONG_CA_NHAN", "HOA_DON"
            };
            for (int viTri = 0; viTri < cacO.Length; viTri++)
            {
                cacO[viTri].Checked = PhanQuyenNguoiDung.CoQuyen(vaiTro, cacQuyen[viTri]);
                cacO[viTri].AutoCheck = false;
            }


        }

        public override void TaiDuLieu()
        {
            PhienDangNhap.YeuCau("PHAN_QUYEN");
            lstVaiTro.SelectedIndex = 0;
            HienThiQuyen();
        }
    }
}
