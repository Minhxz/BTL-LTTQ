using System.Collections.Generic;

namespace QLyCuaHangSuaXe.LopDungChung
{
    public static class PhanQuyenNguoiDung
    {
        public static readonly string[] VaiTro =
        {
            "Chủ/Quản lý",
            "Thợ",
            "Thợ chính",
            "Thu ngân"
        };
        static readonly Dictionary<string, HashSet<string>> Quyen = new Dictionary<string, HashSet<string>>
        {
            {
                "Chủ/Quản lý",
                new HashSet<string>
                {
                    "TIEP_NHAN",
                    "DICH_VU",
                    "NHAP_KHO",
                    "HOA_DON",
                    "NHAN_VIEN",
                    "PHAN_CONG",
                    "BANG_CONG",
                    "BAO_CAO",
                    "QUAN_LY_TAI_KHOAN",
                    "PHAN_QUYEN"
                }
            },
            {
                "Thợ",
                new HashSet<string>
                {
                    "PHAN_CONG",
                    "CONG_CA_NHAN"
                }
            },
            {
                "Thợ chính",
                new HashSet<string>
                {
                    "PHAN_CONG",
                    "CONG_CA_NHAN"
                }
            },
            {
                "Thu ngân",
                new HashSet<string>
                {
                    "TIEP_NHAN",
                    "HOA_DON"
                }
            }
        };
        public static string ChuanHoa(string vaiTro)
        {
            return vaiTro == "Chủ cửa hàng" || vaiTro == "Quản lý" ? "Chủ/Quản lý" : vaiTro;
        }

        public static bool CoVaiTro(string vaiTro)
        {
            return vaiTro != null && Quyen.ContainsKey(ChuanHoa(vaiTro));
        }

        public static bool CoQuyen(string vaiTro, string maChucNang)
        {
            HashSet<string> cacQuyen;
            return vaiTro != null && maChucNang != null && Quyen.TryGetValue(ChuanHoa(vaiTro), out cacQuyen) && cacQuyen.Contains(maChucNang);
        }
    }
}
