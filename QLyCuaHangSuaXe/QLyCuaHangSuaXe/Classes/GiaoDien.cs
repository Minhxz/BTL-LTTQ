using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace QLyCuaHangSuaXe.LopDungChung
{
    public static class GiaoDien
    {
        static readonly Dictionary<string, string> Cot = new Dictionary<string, string>
        {
            { "LuongThang", "Lương tháng (đ)" },
            { "DonGiaGio", "Đơn giá giờ (đ)" },
            { "GioThucLam", "Giờ thực làm" },
            { "GioCa", "Giờ theo ca" },
            { "SoXeHoanThanh", "Xe hoàn thành" },
            { "TongPhut", "Tổng phút" },
            { "TenKhoanDieuChinh", "Khoản điều chỉnh" },
            { "SoTien", "Số tiền (đ)" },
            { "Can", "Cần dùng" },
            { "Ton", "Tồn kho" },
            { "HoTen", "Họ tên" },
            { "Luong", "Lương tháng (đ)" },
            { "HeSoLuong", "Hệ số lương" },
            { "TienDieuChinh", "Điều chỉnh chức vụ (đ)" },
            { "MaChucVu", "Mã chức vụ" },
            { "MaCaLam", "Mã ca làm" },
            { "NgayLam", "Ngày làm" },
            { "ThoiGianCaLam", "Số giờ của ca" },
            { "ThoiGianHoanThanh", "Hoàn thành dự kiến" },
            { "TongThoiGianLamThucTe", "Giờ thực làm" },
            { "MaNhaCungCap", "Mã nhà cung cấp" },
            { "TenNhaCungCap", "Nhà cung cấp" },
            { "TongTienNhap", "Tổng tiền nhập (đ)" },
            { "ThanhTienDichVu", "Giá dịch vụ (đ)" },
            { "TienThanhToan", "Thanh toán (đ)" },
            { "NgayThanhToan", "Ngày thanh toán" },
            { "BienXe", "Biển số xe" },
            { "HangSanXuat", "Hãng sản xuất" },
            { "MauSac", "Màu sắc" },
            { "LoaiXe", "Loại xe" },
            { "NgayHen", "Hẹn trả xe" },
            { "MaXe", "Mã xe" },
            { "MaKhachHang", "Mã khách hàng" },
            {
                "TenTho",
                "Thợ phụ trách"
            },
            {
                "TrangThaiHienThi",
                "Trạng thái"
            },
            {
                "NhacHen",
                "Nhắc hẹn"
            },
            {
                "PhuTung",
                "Phụ tùng"
            },
            {
                "GiaPhuTung",
                "Đơn giá PT"
            },
            {
                "DieuKien",
                "Điều kiện"
            },
            {
                "Chon",
                "Chọn"
            },
            {
                "MaDichVu",
                "Mã dịch vụ"
            },
            {
                "TenDichVu",
                "Tên dịch vụ"
            },
            {
                "SoLuong",
                "Số lượng"
            },
            {
                "DonGiaDichVu",
                "Giá dịch vụ (đ)"
            },
            {
                "ThoiGianDuKien",
                "Phút dự kiến"
            },
            {
                "MaPhieuTiepNhan",
                "Mã tiếp nhận"
            },
            {
                "ThoiGianNhan",
                "Ngày nhận"
            },
            {
                "TenKhachHang",
                "Khách hàng"
            },
            {
                "SoDienThoai",
                "Điện thoại"
            },
            {
                "BienSo",
                "Biển số"
            },
            {
                "TrangThai",
                "Trạng thái"
            },
            {
                "HenTra",
                "Hẹn trả"
            },
            {
                "DaChotPhuTung",
                "Đã chốt phụ tùng"
            },
            {
                "MaNhanVien",
                "Mã nhân viên"
            },
            {
                "TenNhanVien",
                "Nhân viên"
            },
            {
                "TenDangNhap",
                "Tên đăng nhập"
            },
            {
                "TenChucVu",
                "Chức vụ"
            },
            {
                "SoXeDangSuaToiDa",
                "Số xe tối đa"
            },
            {
                "LuongGio",
                "Lương giờ (đ)"
            },
            {
                "LuongNhiemVu",
                "Lương nhiệm vụ (đ)"
            },
            {
                "MoTa",
                "Mô tả"
            },
            {
                "MaPhuTung",
                "Mã phụ tùng"
            },
            {
                "MaChiTietPhieuNhap",
                "Mã chi tiết"
            },
            {
                "TenPhuTung",
                "Phụ tùng"
            },
            {
                "SoLuongNhap",
                "Số lượng nhập"
            },
            {
                "TongSoLuongNhap",
                "Tổng số lượng"
            },
            {
                "SoLuongTon",
                "Tồn kho"
            },
            {
                "DonGiaNhap",
                "Giá nhập (đ)"
            },
            {
                "DonGiaBan",
                "Giá bán (đ)"
            },
            {
                "MaPhieuNhap",
                "Mã phiếu nhập"
            },
            {
                "NgayNhap",
                "Ngày nhập"
            },
            {
                "TongTien",
                "Tổng tiền (đ)"
            },
            {
                "GhiChu",
                "Ghi chú"
            },
            {
                "MaNCC",
                "Mã NCC"
            },
            {
                "TenNCC",
                "Nhà cung cấp"
            },
            {
                "DiaChi",
                "Địa chỉ"
            },
            {
                "HanSuDung",
                "Hạn sử dụng"
            },
            {
                "ThanhTien",
                "Thành tiền (đ)"
            },
            {
                "DonGia",
                "Đơn giá (đ)"
            },
            {
                "MaHoaDon",
                "Mã hóa đơn"
            },
            {
                "NgayLap",
                "Ngày lập"
            },
            {
                "TongTienDichVu",
                "Tiền dịch vụ (đ)"
            },
            {
                "TongTienPhuTung",
                "Tiền phụ tùng (đ)"
            },
            {
                "TrangThaiThanhToan",
                "Thanh toán"
            },
            {
                "PhuongThucThanhToan",
                "Phương thức"
            },
            {
                "TrangThaiCongViec",
                "Tiến độ thợ"
            },
            {
                "TienCong",
                "Tiền công (đ)"
            },
            {
                "ThoiGianBatDau",
                "Bắt đầu"
            },
            {
                "ThoiGianKetThuc",
                "Kết thúc"
            },
            {
                "MaBangCong",
                "Mã công"
            },
            {
                "Ngay",
                "Ngày"
            },
            {
                "MaCa",
                "Mã ca"
            },
            {
                "TenCa",
                "Ca làm"
            },
            {
                "GioVao",
                "Giờ vào"
            },
            {
                "GioRa",
                "Giờ ra"
            },
            {
                "Vang",
                "Vắng"
            },
            {
                "DieuChinhPhut",
                "Điều chỉnh (phút)"
            },
            {
                "LyDo",
                "Lý do"
            },
            {
                "GioTinhCong",
                "Giờ tính công"
            },
            {
                "PhutMuon",
                "Muộn (phút)"
            },
            {
                "PhutVeSom",
                "Về sớm (phút)"
            },
            {
                "TienLuong",
                "Lương ca (đ)"
            },
            {
                "TongLuong",
                "Tổng lương (đ)"
            },
            {
                "Loai",
                "Loại"
            },
            {
                "NoiDung",
                "Nội dung"
            },
            {
                "SoHoaDon",
                "Số hóa đơn"
            },
            {
                "DaPhatHanh",
                "Đã phát hành (đ)"
            },
            {
                "DaThu",
                "Đã thu (đ)"
            },
            {
                "ChuaThu",
                "Chưa thu (đ)"
            },
            {
                "NhiemVuHoanThanh",
                "Nhiệm vụ xong"
            },
            {
                "TienNhiemVu",
                "Tiền nhiệm vụ (đ)"
            },
            {
                "TienDichVu",
                "Tiền dịch vụ (đ)"
            },
            {
                "GiaTriTon",
                "Giá trị tồn (đ)"
            },
            {
                "SoPhieu",
                "Số phiếu"
            },
            {
                "QuaHen",
                "Quá hẹn"
            },
            {
                "LuongTheoGio",
                "Lương theo giờ (đ)"
            },
            {
                "CaVang",
                "Ca vắng"
            }
        };
        public static string TenCot(string khoa)
        {
            string giaTri;
            return Cot.TryGetValue(khoa, out giaTri) ? giaTri : khoa;
        }

        public static readonly Color Xanh = Color.FromArgb(30, 108, 176);
        sealed class TrangThaiNut
        {
            public bool DieuHuong, DiChuot, DangNhan;
            public string BieuTuong;
        }

        static readonly ConditionalWeakTable<Button, TrangThaiNut> cacNut = new ConditionalWeakTable<Button, TrangThaiNut>();
        static readonly ConditionalWeakTable<GroupBox, object> cacKhung = new ConditionalWeakTable<GroupBox, object>();
        static readonly Font chuNut = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        static readonly Font chuDieuHuong = new Font("Segoe UI", 11F, FontStyle.Bold);
        static readonly Font chuBieuTuong = new Font("Segoe MDL2 Assets", 14F);

        static bool TrongKhung(Control dieuKhien)
        {
            for (Control cha = dieuKhien.Parent; cha != null; cha = cha.Parent)
                if (cha is GroupBox) return true;
            return false;
        }

        public static void AnHuongDan(Control nhan)
        {
            nhan.Visible = false;
            var bang = nhan.Parent as TableLayoutPanel;
            if (bang == null) return;
            int dong = bang.GetRow(nhan), soDieuKhien = 0;
            foreach (Control con in bang.Controls) if (bang.GetRow(con) == dong) soDieuKhien++;
            if (soDieuKhien == 1 && dong < bang.RowStyles.Count)
            {
                bang.RowStyles[dong].SizeType = SizeType.Absolute;
                bang.RowStyles[dong].Height = 0;
            }
        }

        public static void ApDungMauTiepNhan(Control goc)
        {
            goc.BackColor = goc is GroupBox || TrongKhung(goc) ? Color.White : Color.FromArgb(244, 247, 251);
            goc.ForeColor = Color.FromArgb(42, 42, 42);
            foreach (Control dieuKhien in goc.Controls)
            {
                ApDungMauTiepNhan(dieuKhien);
                var nut = dieuKhien as Button;
                if (nut != null)
                {
                    DinhDangNut(nut);
                }

                if (dieuKhien is TextBoxBase || dieuKhien is ComboBox || dieuKhien is NumericUpDown)
                    dieuKhien.BackColor = Color.White;
                if (dieuKhien is Label && dieuKhien.Font.Size >= 16)
                    dieuKhien.ForeColor = Xanh;
                if (dieuKhien.Name == "lblHuongDan") AnHuongDan(dieuKhien);
                if (dieuKhien is GroupBox)
                {
                    dieuKhien.ForeColor = Xanh;
                    DinhDangKhung((GroupBox)dieuKhien);
                }
                var bang = dieuKhien as DataGridView;
                if (bang != null)
                    DinhDangBang(bang);
            }
        }

        public static void DinhDangNut(Button nut)
        {
            bool chinh = nut.Text.StartsWith("Lưu") || nut.Text.StartsWith("Kiểm tra phụ tùng") || nut.Text.StartsWith("Ghi sổ") || nut.Text.StartsWith("Đăng nhập");
            bool xoa = nut.Text.StartsWith("Xóa") || nut.Text.StartsWith("Ngừng");
            nut.FlatStyle = FlatStyle.Flat;
            nut.FlatAppearance.BorderSize = 1;
            nut.FlatAppearance.BorderColor = chinh ? Xanh : xoa ? Color.FromArgb(218, 172, 172) : Color.FromArgb(191, 207, 220);
            nut.BackColor = chinh ? Xanh : Color.White;
            nut.ForeColor = chinh ? Color.White : xoa ? Color.FromArgb(164, 59, 59) : Color.FromArgb(30, 85, 130);
            nut.Font = chuNut;
            if (nut.AutoSize)
                nut.Padding = new Padding(Math.Max(12, nut.Padding.Left), nut.Padding.Top, Math.Max(12, nut.Padding.Right), nut.Padding.Bottom);
            nut.Cursor = Cursors.Hand;
            nut.FlatAppearance.MouseOverBackColor = chinh ? Color.FromArgb(24, 91, 151) : Color.FromArgb(235, 243, 251);
            nut.FlatAppearance.MouseDownBackColor = chinh ? Color.FromArgb(19, 76, 128) : Color.FromArgb(216, 234, 249);
            nut.UseVisualStyleBackColor = false;
            var trangThai = GanHieuUngNut(nut);
            trangThai.DieuHuong = false;
            trangThai.BieuTuong = null;
            BoGocNut(nut, trangThai);
            nut.Invalidate();
        }

        public static void DinhDangNutDieuHuong(Button nut, string bieuTuong)
        {
            nut.FlatStyle = FlatStyle.Flat;
            nut.FlatAppearance.BorderSize = 0;
            nut.Font = chuDieuHuong;
            nut.ForeColor = Color.White;
            nut.Cursor = Cursors.Hand;
            nut.UseVisualStyleBackColor = false;
            var trangThai = GanHieuUngNut(nut);
            trangThai.DieuHuong = true;
            trangThai.BieuTuong = bieuTuong ?? "";
            BoGocNut(nut, trangThai);
            nut.Invalidate();
        }

        static GraphicsPath DuongBoGoc(RectangleF khung, float banKinh)
        {
            var duong = new GraphicsPath();
            float duongKinh = Math.Min(banKinh * 2, Math.Min(khung.Width, khung.Height));
            if (duongKinh < 1) { duong.AddRectangle(khung); return duong; }
            duong.AddArc(khung.X, khung.Y, duongKinh, duongKinh, 180, 90);
            duong.AddArc(khung.Right - duongKinh, khung.Y, duongKinh, duongKinh, 270, 90);
            duong.AddArc(khung.Right - duongKinh, khung.Bottom - duongKinh, duongKinh, duongKinh, 0, 90);
            duong.AddArc(khung.X, khung.Bottom - duongKinh, duongKinh, duongKinh, 90, 90);
            duong.CloseFigure();
            return duong;
        }

        static Color PhaMau(Color mau, Color mauPhu, float tiLe)
        {
            return Color.FromArgb((int)(mau.R * (1 - tiLe) + mauPhu.R * tiLe), (int)(mau.G * (1 - tiLe) + mauPhu.G * tiLe), (int)(mau.B * (1 - tiLe) + mauPhu.B * tiLe));
        }

        static void BoGocNut(Button nut, TrangThaiNut trangThai)
        {
            if (nut.Width < 2 || nut.Height < 2 || System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime || nut.Site != null && nut.Site.DesignMode) return;
            using (var duong = DuongBoGoc(new RectangleF(0, 0, nut.Width, nut.Height), (trangThai.DieuHuong ? 8 : 6) * nut.DeviceDpi / 96F))
            {
                var vungCu = nut.Region;
                nut.Region = new Region(duong);
                if (vungCu != null) vungCu.Dispose();
            }
        }

        static TrangThaiNut GanHieuUngNut(Button nut)
        {
            TrangThaiNut trangThai;
            if (cacNut.TryGetValue(nut, out trangThai)) return trangThai;
            trangThai = new TrangThaiNut();
            cacNut.Add(nut, trangThai);
            nut.MouseEnter += (nguoiGui, suKien) => { trangThai.DiChuot = true; nut.Invalidate(); };
            nut.MouseLeave += (nguoiGui, suKien) => { trangThai.DiChuot = false; trangThai.DangNhan = false; nut.Invalidate(); };
            nut.MouseDown += (nguoiGui, suKien) => { if (suKien.Button == MouseButtons.Left) trangThai.DangNhan = true; nut.Invalidate(); };
            nut.MouseUp += (nguoiGui, suKien) => { trangThai.DangNhan = false; nut.Invalidate(); };
            nut.MouseCaptureChanged += (nguoiGui, suKien) => { if (!nut.Capture) trangThai.DangNhan = false; nut.Invalidate(); };
            nut.KeyDown += (nguoiGui, suKien) => { if (suKien.KeyCode == Keys.Space || suKien.KeyCode == Keys.Enter) trangThai.DangNhan = true; nut.Invalidate(); };
            nut.KeyUp += (nguoiGui, suKien) => { trangThai.DangNhan = false; nut.Invalidate(); };
            nut.GotFocus += (nguoiGui, suKien) => nut.Invalidate();
            nut.LostFocus += (nguoiGui, suKien) => { trangThai.DangNhan = false; nut.Invalidate(); };
            nut.EnabledChanged += (nguoiGui, suKien) => { trangThai.DangNhan = false; trangThai.DiChuot = false; nut.Invalidate(); };
            nut.Resize += (nguoiGui, suKien) => { BoGocNut(nut, trangThai); nut.Invalidate(); };
            nut.Paint += (nguoiGui, suKien) => VeNut(nut, trangThai, suKien.Graphics);
            return trangThai;
        }

        static void VeNut(Button nut, TrangThaiNut trangThai, Graphics ve)
        {
            if (nut.Width < 2 || nut.Height < 2) return;
            float tiLe = nut.DeviceDpi / 96F;
            Color nen = nut.BackColor, chu = nut.ForeColor;
            bool nenDam = nen.GetBrightness() < 0.65F;
            Color vien = trangThai.DieuHuong ? PhaMau(nen, Color.White, 0.09F) : nut.FlatAppearance.BorderColor;
            if (!nut.Enabled)
            {
                nen = trangThai.DieuHuong ? Color.FromArgb(43, 65, 83) : Color.FromArgb(235, 240, 245);
                chu = trangThai.DieuHuong ? Color.FromArgb(145, 166, 184) : Color.FromArgb(126, 143, 159);
                vien = trangThai.DieuHuong ? nen : Color.FromArgb(213, 222, 231);
            }
            else if (trangThai.DangNhan)
                nen = nenDam ? PhaMau(nen, Color.Black, 0.15F) : Color.FromArgb(218, 234, 247);
            else if (trangThai.DiChuot)
                nen = nenDam ? PhaMau(nen, Color.White, 0.10F) : Color.FromArgb(238, 246, 253);
            var trangThaiVe = ve.Save();
            ve.Clear(nut.Parent == null ? SystemColors.Control : nut.Parent.BackColor);
            ve.SmoothingMode = SmoothingMode.AntiAlias;
            using (var duong = DuongBoGoc(new RectangleF(0.5F, 0.5F, nut.Width - 1, nut.Height - 1), (trangThai.DieuHuong ? 8 : 6) * tiLe))
            using (var coNen = new SolidBrush(nen))
            using (var net = new Pen(vien, tiLe))
            {
                ve.FillPath(coNen, duong);
                ve.DrawPath(net, duong);
            }
            if (nut.Focused && nut.Enabled && nut.Width > 6 * tiLe && nut.Height > 6 * tiLe)
            {
                using (var duong = DuongBoGoc(new RectangleF(3 * tiLe, 3 * tiLe, nut.Width - 6 * tiLe, nut.Height - 6 * tiLe), (trangThai.DieuHuong ? 5 : 3) * tiLe))
                using (var net = new Pen(nenDam ? PhaMau(nen, Color.White, 0.65F) : Xanh, tiLe))
                {
                    net.DashStyle = DashStyle.Dot;
                    ve.DrawPath(net, duong);
                }
            }
            ve.Restore(trangThaiVe);
            var khungChu = new Rectangle(8, 0, Math.Max(1, nut.Width - 16), nut.Height);
            var canChu = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.SingleLine | TextFormatFlags.EndEllipsis | TextFormatFlags.NoPadding | TextFormatFlags.PreserveGraphicsClipping;
            if (!nut.UseMnemonic) canChu |= TextFormatFlags.NoPrefix;
            else canChu |= TextFormatFlags.HidePrefix;
            if (trangThai.DieuHuong)
            {
                int leTrai = (int)(18 * tiLe);
                TextRenderer.DrawText(ve, trangThai.BieuTuong, chuBieuTuong, new Rectangle(leTrai, 0, (int)(25 * tiLe), nut.Height), chu, TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.NoPadding);
                khungChu = new Rectangle((int)(52 * tiLe), 0, Math.Max(1, nut.Width - (int)(64 * tiLe)), nut.Height);
                canChu = (canChu & ~TextFormatFlags.HorizontalCenter) | TextFormatFlags.Left;
            }
            TextRenderer.DrawText(ve, nut.Text, nut.Font, khungChu, chu, canChu);
        }

        static void DinhDangKhung(GroupBox khung)
        {
            khung.BackColor = Color.White;
            object daGan;
            if (cacKhung.TryGetValue(khung, out daGan)) return;
            cacKhung.Add(khung, new object());
            khung.Paint += (nguoiGui, suKien) =>
            {
                if (khung.Width < 4 || khung.Height < 4) return;
                var ve = suKien.Graphics;
                var trangThaiVe = ve.Save();
                ve.SmoothingMode = SmoothingMode.AntiAlias;
                ve.Clear(Color.White);
                float y = khung.Font.Height / 2F + 2;
                using (var duong = DuongBoGoc(new RectangleF(0.5F, y, khung.Width - 1, Math.Max(1, khung.Height - y - 1)), 6))
                using (var net = new Pen(Color.FromArgb(216, 226, 236)))
                    ve.DrawPath(net, duong);
                using (var chu = new Font(khung.Font, FontStyle.Bold))
                {
                    var coChu = TextRenderer.MeasureText(ve, khung.Text, chu, new Size(Math.Max(1, khung.Width - 32), khung.Font.Height + 8), TextFormatFlags.SingleLine | TextFormatFlags.NoPadding);
                    ve.FillRectangle(Brushes.White, 12, 0, coChu.Width + 12, coChu.Height + 2);
                    TextRenderer.DrawText(ve, khung.Text, chu, new Rectangle(18, 0, Math.Max(1, khung.Width - 36), coChu.Height + 2), Xanh, TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.SingleLine | TextFormatFlags.EndEllipsis | TextFormatFlags.NoPadding);
                }
                ve.Restore(trangThaiVe);
            };
            khung.Resize += (nguoiGui, suKien) => khung.Invalidate();
        }

        static void DinhDangNgayTrong(object nguoiGui, DataGridViewCellFormattingEventArgs suKien)
        {
            if (!(suKien.Value is DateTime)) return;
            var ngay = (DateTime)suKien.Value;
            if (ngay.Year == 1 || ngay.Year == 9999)
            {
                suKien.Value = ngay.Year == 9999 ? "Không hạn" : "";
                suKien.FormattingApplied = true;
            }
        }

        public static void DinhDangBang(DataGridView bang)
        {
            bang.CellFormatting -= DinhDangNgayTrong;
            bang.CellFormatting += DinhDangNgayTrong;
            bang.BackgroundColor = Color.White;
            bang.EnableHeadersVisualStyles = false;
            bang.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(235, 243, 251);
            bang.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(42, 42, 42);
            bang.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            bang.ColumnHeadersDefaultCellStyle.Padding = new Padding(6, 3, 6, 3);
            bang.ColumnHeadersHeight = 44;
            bang.RowTemplate.Height = 38;
            bang.DefaultCellStyle.Padding = new Padding(6, 0, 6, 0);
            bang.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 249, 253);
            bang.GridColor = Color.FromArgb(216, 225, 233);
            bang.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            bang.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            bang.DefaultCellStyle.SelectionBackColor = Color.FromArgb(216, 234, 249);
            bang.DefaultCellStyle.SelectionForeColor = Color.FromArgb(42, 42, 42);
            bang.AllowUserToAddRows = false;
            bang.ReadOnly = true;
            bang.RowHeadersVisible = false;
            bang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            bang.DataBindingComplete -= DinhDangCot;
            bang.DataBindingComplete += DinhDangCot;
        }

        static void DinhDangCot(object nguoiGui, DataGridViewBindingCompleteEventArgs suKien)
        {
            var bang = (DataGridView)nguoiGui;
            foreach (DataGridViewColumn cot in bang.Columns)
            {
                string ten = cot.DataPropertyName.Length > 0 ? cot.DataPropertyName : cot.Name;
                cot.HeaderText = TenCot(ten);
                bool tenDai = ten.StartsWith("Ten") || ten == "HoTen" || ten == "NoiDung" || ten == "MoTa";
                cot.FillWeight = tenDai ? 170 : ten.StartsWith("Ma") ? 80 : 115;
                cot.MinimumWidth = tenDai ? 135 : ten.StartsWith("Ma") ? 80 : 100;
                if (cot.ValueType == typeof(decimal))
                {
                    bool soGio = (ten.Contains("Gio") && !ten.Contains("Gia") && !ten.StartsWith("Luong")) || ten.Contains("ThoiGian") || ten.Contains("HeSo");
                    cot.DefaultCellStyle.Format = soGio || ten == "DonGiaGio" ? "N2" : "N0";
                    cot.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
                if (cot.ValueType == typeof(TimeSpan)) cot.DefaultCellStyle.Format = @"hh\:mm";
                if (cot.ValueType == typeof(DateTime))
                    cot.DefaultCellStyle.Format = ten.StartsWith("Ngay") || ten == "HanSuDung" || ten == "ThoiGianNhan" || ten == "HenTra" ? "dd/MM/yyyy" : "dd/MM/yyyy HH:mm";
            }
        }

        public static void ChuanHoa(Form bieuMau)
        {
            bieuMau.Font = new Font("Segoe UI", 10);
            bieuMau.BackColor = Color.White;
            bieuMau.ForeColor = Color.FromArgb(28, 42, 56);
            bieuMau.Size = new Size(1180, 800);
            bieuMau.MinimumSize = new Size(900, 650);
            bieuMau.StartPosition = FormStartPosition.CenterScreen;
        }

        public static Label TaoNhan(string noiDung)
        {
            return new Label
            {
                Text = noiDung,
                AutoSize = true,
                Margin = new Padding(8, 10, 8, 4)
            };
        }

        public static Button TaoNut(string noiDung, Action thaoTacBam)
        {
            var btnThaoTac = new Button
            {
                Text = noiDung,
                AutoSize = true,
                MinimumSize = new Size(100, 36),
                Height = 38,
                BackColor = Xanh,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Margin = new Padding(5)
            };
            btnThaoTac.FlatAppearance.BorderSize = 0;
            DinhDangNut(btnThaoTac);
            btnThaoTac.Click += (doiTuongGui, suKien) => ThucHien(thaoTacBam);
            return btnThaoTac;
        }

        public static void ThucHien(Action thaoTac)
        {
            try
            {
                thaoTac();
            }
            catch (Exception loi)
            {
                MessageBox.Show(loi is System.Data.SqlClient.SqlException ? "Không thể xử lý dữ liệu. Kiểm tra kết nối SQL và dữ liệu vừa nhập.\n" + loi.Message : loi.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public static DataGridView TaoBang()
        {
            var dgvDanhSach = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AutoGenerateColumns = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                RowHeadersVisible = false,
                EnableHeadersVisualStyles = false
            };
            dgvDanhSach.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(222, 235, 247);
            dgvDanhSach.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(20, 50, 75);
            dgvDanhSach.ColumnHeadersHeight = 44;
            dgvDanhSach.RowTemplate.Height = 32;
            dgvDanhSach.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 248, 252);
            dgvDanhSach.DefaultCellStyle.SelectionBackColor = Color.FromArgb(201, 226, 249);
            dgvDanhSach.DefaultCellStyle.SelectionForeColor = Color.Black;
            DinhDangBang(dgvDanhSach);
            return dgvDanhSach;
        }

        public static FlowLayoutPanel TaoThanhThaoTac()
        {
            return new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                WrapContents = true,
                Padding = new Padding(8)
            };
        }

        public static TableLayoutPanel TaoBangNhap()
        {
            var tblThongTin = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                ColumnCount = 4,
                Padding = new Padding(8)
            };
            tblThongTin.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 140));
            tblThongTin.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            tblThongTin.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 140));
            tblThongTin.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            return tblThongTin;
        }

        public static void ThemTruongNhap(TableLayoutPanel tblThongTin, string tieuDe, Control dieuKhien)
        {
            int chiSoMuc = tblThongTin.Controls.Count / 2;
            int dong = chiSoMuc / 2, cot = (chiSoMuc % 2) * 2;
            tblThongTin.RowCount = Math.Max(tblThongTin.RowCount, dong + 1);
            dieuKhien.Dock = DockStyle.Fill;
            dieuKhien.Margin = new Padding(4, 6, 12, 6);
            tblThongTin.Controls.Add(TaoNhan(tieuDe), cot, dong);
            tblThongTin.Controls.Add(dieuKhien, cot + 1, dong);
        }

        public static ComboBox TaoDanhSachChon()
        {
            return new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList
            };
        }

        public static NumericUpDown TaoONhapSo(decimal toiDa = 1000000000, int soLe = 0)
        {
            return new NumericUpDown
            {
                Maximum = toiDa,
                DecimalPlaces = soLe,
                ThousandsSeparator = true
            };
        }

        public static void GanDuLieu(ComboBox danhSachChon, DataTable bangDuLieu, string cotHienThi, string giaTri)
        {
            danhSachChon.DisplayMember = cotHienThi;
            danhSachChon.ValueMember = giaTri;
            danhSachChon.DataSource = bangDuLieu;
            danhSachChon.SelectedIndex = -1;
        }

        public static string GiaTriDangChon(ComboBox danhSachChon)
        {
            return danhSachChon.SelectedValue == null ? "" : danhSachChon.SelectedValue.ToString();
        }

        public static string GiaTriO(DataGridView dgvDanhSach, string ten)
        {
            return dgvDanhSach.CurrentRow == null ? "" : Convert.ToString(dgvDanhSach.CurrentRow.Cells[ten].Value);
        }

        public static string BatBuocNhap(TextBox bangDuLieu, string tieuDe, int toiDa)
        {
            string giaTri = bangDuLieu.Text.Trim();
            if (giaTri.Length == 0 || giaTri.Length > toiDa)
                throw new InvalidOperationException(tieuDe + " bắt buộc, tối đa " + toiDa + " ký tự.");
            return giaTri;
        }

        public static string KiemTraDienThoai(TextBox bangDuLieu)
        {
            string giaTri = BatBuocNhap(bangDuLieu, "Điện thoại", 15);
            if (giaTri.Length < 8 || System.Text.RegularExpressions.Regex.IsMatch(giaTri, @"\D"))
                throw new InvalidOperationException("Điện thoại cần 8–15 chữ số.");
            return giaTri;
        }
    }

    public class TrangDuLieu : Form
    {
        public TrangDuLieu() : this("Trang dữ liệu")
        {
        }

        protected readonly XuLyDuLieu duLieu = new XuLyDuLieu();
        protected readonly DataGridView dgvDanhSach = GiaoDien.TaoBang();
        protected readonly Panel VungNhap = new Panel
        {
            Dock = DockStyle.Top,
            AutoSize = true
        };
        protected readonly Label lblThongBao = new Label
        {
            Dock = DockStyle.Bottom,
            Height = 34,
            ForeColor = Color.FromArgb(30, 70, 110),
            Padding = new Padding(8)
        };
        public TrangDuLieu(string tieuDe)
        {
            Text = tieuDe;
            GiaoDien.ChuanHoa(this);
            var lblTieuDe = GiaoDien.TaoNhan(tieuDe);
            lblTieuDe.Text = tieuDe.ToUpper();
            lblTieuDe.ForeColor = GiaoDien.Xanh;
            lblTieuDe.Font = new Font("Segoe UI", 18, FontStyle.Bold);
            lblTieuDe.Dock = DockStyle.Top;
            lblTieuDe.AutoSize = false;
            lblTieuDe.Height = 58;
            lblTieuDe.Padding = new Padding(12, 8, 0, 0);
            Controls.Add(dgvDanhSach);
            Controls.Add(VungNhap);
            Controls.Add(lblTieuDe);
            Controls.Add(lblThongBao);
            lblThongBao.Visible = false;
        }

        protected void ThemVungNhap(Control dieuKhien)
        {
            VungNhap.Controls.Add(dieuKhien);
            dieuKhien.BringToFront();
        }

        protected override void OnLoad(EventArgs suKien)
        {
            base.OnLoad(suKien);
            if (!DesignMode && System.ComponentModel.LicenseManager.UsageMode != System.ComponentModel.LicenseUsageMode.Designtime)
                GiaoDien.ThucHien(TaiDuLieu);
        }

        public virtual void TaiDuLieu()
        {
        }
    }
}
