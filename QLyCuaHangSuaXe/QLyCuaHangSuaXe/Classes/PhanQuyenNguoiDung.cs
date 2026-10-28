using System.Collections.Generic;

namespace QLyCuaHangSuaXe.LopDungChung
{
    public static class PhanQuyenNguoiDung
    {
        public static readonly string[] VaiTro = { "Chủ", "Quản lý", "Thợ phụ", "Thợ chính", "Thu ngân", "Thủ kho" };
        static readonly Dictionary<string, HashSet<string>> Quyen = new Dictionary<string, HashSet<string>>
        {
            { "Chủ", new HashSet<string> { "TIEP_NHAN", "DICH_VU", "NHAP_KHO", "HOA_DON", "NHAN_VIEN", "NHAN_SU", "PHAN_CONG", "BANG_CONG", "CONG_CA_NHAN", "BAO_CAO", "QUAN_LY_TAI_KHOAN", "TAI_KHOAN_CA_NHAN", "PHAN_QUYEN" } },
            { "Quản lý", new HashSet<string> { "TIEP_NHAN", "DICH_VU", "NHAP_KHO", "HOA_DON", "NHAN_VIEN", "NHAN_SU", "PHAN_CONG", "BANG_CONG", "CONG_CA_NHAN", "BAO_CAO", "QUAN_LY_TAI_KHOAN", "TAI_KHOAN_CA_NHAN", "PHAN_QUYEN" } },
            { "Thợ chính", new HashSet<string> { "PHAN_CONG", "NHAN_SU", "CONG_CA_NHAN", "TAI_KHOAN_CA_NHAN" } },
            { "Thợ phụ", new HashSet<string> { "PHAN_CONG", "NHAN_SU", "CONG_CA_NHAN", "TAI_KHOAN_CA_NHAN" } },
            { "Thủ kho", new HashSet<string> { "NHAP_KHO", "NHAN_SU", "CONG_CA_NHAN", "TAI_KHOAN_CA_NHAN" } },
            { "Thu ngân", new HashSet<string> { "TIEP_NHAN", "HOA_DON", "NHAN_SU", "CONG_CA_NHAN", "TAI_KHOAN_CA_NHAN" } }
        };
        public static string ChuanHoa(string vaiTro)
        {
            switch (vaiTro)
            {
                case "Chủ cửa hàng": case "Chủ/Quản lý": case "Quản lý cửa hàng": return "Chủ";
                case "Kỹ thuật viên chính": return "Thợ chính";
                case "Thợ": case "Kỹ thuật viên": return "Thợ phụ";
                case "Kế toán - Thu ngân": case "Lễ tân - Cố vấn dịch vụ": return "Thu ngân";
                default: return vaiTro;
            }
        }
        public static bool CoVaiTro(string vaiTro) { return vaiTro != null && Quyen.ContainsKey(ChuanHoa(vaiTro)); }
        public static bool CoQuyen(string vaiTro, string maChucNang)
        {
            HashSet<string> quyen;
            return vaiTro != null && maChucNang != null && Quyen.TryGetValue(ChuanHoa(vaiTro), out quyen) && quyen.Contains(maChucNang);
        }
        public static bool LaTho(string vaiTro) { string ten = ChuanHoa(vaiTro); return ten == "Thợ chính" || ten == "Thợ phụ"; }
        public static int GioiHanXe(string vaiTro) { return ChuanHoa(vaiTro) == "Thợ chính" ? 3 : LaTho(vaiTro) ? 2 : 0; }
    }
}
