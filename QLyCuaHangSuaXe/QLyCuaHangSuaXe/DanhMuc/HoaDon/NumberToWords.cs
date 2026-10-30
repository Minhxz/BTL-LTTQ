using System;
using System.Collections.Generic;

namespace HoaDon
{
    [System.ComponentModel.DesignerCategory("Code")]
    public static class NumberToWords
    {
        private static readonly string[] ChuSo = { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };
        private static readonly string[] DonViTien = { "", "nghìn", "triệu", "tỷ", "nghìn tỷ", "triệu tỷ" };

        private static string DocCum3So(int number, bool docTram)
        {
            int tram = number / 100;
            int chuc = (number % 100) / 10;
            int donVi = number % 10;
            string res = "";

            if (docTram || tram > 0)
            {
                res += " " + ChuSo[tram] + " trăm";
                if (chuc == 0 && donVi > 0)
                {
                    res += " lẻ " + ChuSo[donVi];
                    return res;
                }
            }

            if (chuc > 1)
            {
                res += " " + ChuSo[chuc] + " mươi";
                if (donVi == 1) res += " mốt";
                else if (donVi == 5) res += " lăm";
                else if (donVi > 0) res += " " + ChuSo[donVi];
            }
            else if (chuc == 1)
            {
                res += " mười";
                if (donVi == 1) res += " một";
                else if (donVi == 5) res += " lăm";
                else if (donVi > 0) res += " " + ChuSo[donVi];
            }
            else if (donVi > 0)
            {
                res += " " + ChuSo[donVi];
            }

            return res;
        }

        public static string ChuyenSoThanhChu(decimal amount)
        {
            long soTien = (long)Math.Round(amount);
            if (soTien == 0) return "Không đồng";
            if (soTien < 0) return "Âm " + ChuyenSoThanhChu(Math.Abs(soTien));

            List<int> blocks = new List<int>();
            while (soTien > 0)
            {
                blocks.Add((int)(soTien % 1000));
                soTien /= 1000;
            }

            string ketQua = "";
            for (int i = blocks.Count - 1; i >= 0; i--)
            {
                int b = blocks[i];
                if (b > 0)
                {
                    bool docTram = (i < blocks.Count - 1); // Chỉ đọc "trăm" nếu không phải khối cao nhất (trừ khi khối cao nhất có hàng trăm)
                    string docB = DocCum3So(b, docTram);
                    ketQua += docB + " " + DonViTien[i] + " ";
                }
            }

            ketQua = ketQua.Trim();
            while (ketQua.Contains("  "))
            {
                ketQua = ketQua.Replace("  ", " ");
            }

            if (string.IsNullOrEmpty(ketQua)) return "Không đồng";
            return char.ToUpper(ketQua[0]) + ketQua.Substring(1) + " đồng";
        }
    }
}
