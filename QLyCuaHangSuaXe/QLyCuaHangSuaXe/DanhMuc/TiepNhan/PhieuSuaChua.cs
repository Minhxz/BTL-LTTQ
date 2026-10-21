using System;
using System.Data;

namespace QLyCuaHangSuaXe.DanhMuc.TiepNhan
{
    public class PhieuSuaChua
    {
        public string MaPhieu { get; set; }
        public DateTime NgayNhan { get; set; }
        public DateTime HenTraXe { get; set; }
        public string NguoiTiepNhan { get; set; }
        public string TrangThai { get; set; }
        public string KhachHang { get; set; }
        public string DienThoai { get; set; }
        public string BienSoXe { get; set; }
        public string DongXe { get; set; }
        public int SoKm { get; set; }
        public string ThoPhuTrach { get; set; }
        public string TinhTrangXe { get; set; }
        public string NoiDungSuaChua { get; set; }
        public DataTable PhuTung { get; set; }
        public DataTable DichVu { get; set; }
        public decimal TienPhuTung { get; set; }
        public decimal TienCong { get; set; }
        public decimal TongCong { get { return TienPhuTung + TienCong; } }

        public override string ToString()
        {
            return MaPhieu + " - " + KhachHang + " - " + BienSoXe;
        }

    }
}
