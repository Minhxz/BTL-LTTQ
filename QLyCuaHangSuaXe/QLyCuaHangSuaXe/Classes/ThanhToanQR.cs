using System;
using System.Configuration;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using QRCoder;

namespace QLyCuaHangSuaXe.LopDungChung
{
    public static class ThanhToanQR
    {
        public static string NganHang { get { return ConfigurationManager.AppSettings["NganHangQR"] ?? "MB Bank"; } }
        public static string MaNganHang { get { return ConfigurationManager.AppSettings["MaNganHangQR"] ?? "970422"; } }
        public static string SoTaiKhoan { get { return ConfigurationManager.AppSettings["SoTaiKhoanQR"] ?? "0965299505"; } }

        static string Truong(string ma, string giaTri)
        {
            if (giaTri.Length > 99) throw new InvalidOperationException("Nội dung QR quá dài.");
            return ma + giaTri.Length.ToString("D2") + giaTri;
        }

        public static string MaKiemTra(string noiDung)
        {
            ushort crc = 0xffff;
            foreach (byte kyTu in Encoding.ASCII.GetBytes(noiDung))
            {
                crc ^= (ushort)(kyTu << 8);
                for (int bit = 0; bit < 8; bit++) crc = (ushort)((crc & 0x8000) != 0 ? (crc << 1) ^ 0x1021 : crc << 1);
            }
            return crc.ToString("X4");
        }

        public static string NoiDung(string maHoaDon, decimal soTien)
        {
            if (!Regex.IsMatch(MaNganHang, @"^\d{6}$") || !Regex.IsMatch(SoTaiKhoan, @"^[A-Za-z0-9]{1,25}$"))
                throw new InvalidOperationException("Kiểm tra tài khoản ngân hàng nhận tiền.");
            if (!Regex.IsMatch(maHoaDon ?? "", @"^[A-Za-z0-9-]{1,15}$") || soTien <= 0 || soTien != decimal.Truncate(soTien))
                throw new InvalidOperationException("Mã hóa đơn hoặc số tiền QR không hợp lệ; số tiền VND phải là số nguyên dương.");
            string nganHang = Truong("00", MaNganHang) + Truong("01", SoTaiKhoan);
            string thuHuong = Truong("00", "A000000727") + Truong("01", nganHang) + Truong("02", "QRIBFTTA");
            string noiDung = Truong("00", "01") + Truong("01", "12") + Truong("38", thuHuong) + Truong("53", "704")
                + Truong("54", soTien.ToString("0", CultureInfo.InvariantCulture)) + Truong("58", "VN") + Truong("62", Truong("08", "THANH TOAN " + maHoaDon)) + "6304";
            return noiDung + MaKiemTra(noiDung);
        }

        public static Bitmap TaoAnh(string maHoaDon, decimal soTien)
        {
            using (var boTao = new QRCodeGenerator())
            using (var duLieu = boTao.CreateQrCode(NoiDung(maHoaDon, soTien), QRCodeGenerator.ECCLevel.M))
            using (var maQR = new QRCode(duLieu))
                return maQR.GetGraphic(8, Color.Black, Color.White, true);
        }
    }
}
