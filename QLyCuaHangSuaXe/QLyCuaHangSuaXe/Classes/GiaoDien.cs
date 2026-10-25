using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace QLyCuaHangSuaXe.LopDungChung
{
    public static class GiaoDien
    {
        static readonly Dictionary<string, string> Cot = new Dictionary<string, string>
        {
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
                "TenPhuTung",
                "Phụ tùng"
            },
            {
                "DonViTinh",
                "Đơn vị"
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
                "Tiền nhiệm vụ (đ)"
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
        public static void ApDungMauTiepNhan(Control goc)
        {
            goc.BackColor = Color.FromArgb(250, 252, 254);
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
                if (dieuKhien is GroupBox)
                    dieuKhien.ForeColor = Xanh;
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
            nut.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            nut.Cursor = Cursors.Hand;
            nut.FlatAppearance.MouseOverBackColor = chinh ? Color.FromArgb(24, 91, 151) : Color.FromArgb(235, 243, 251);
            nut.FlatAppearance.MouseDownBackColor = chinh ? Color.FromArgb(19, 76, 128) : Color.FromArgb(216, 234, 249);
            nut.UseVisualStyleBackColor = false;
        }

        public static void DinhDangBang(DataGridView bang)
        {
            bang.BackgroundColor = Color.White;
            bang.EnableHeadersVisualStyles = false;
            bang.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(235, 243, 251);
            bang.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(42, 42, 42);
            bang.ColumnHeadersHeight = 38;
            bang.RowTemplate.Height = 38;
            bang.DefaultCellStyle.SelectionBackColor = Color.FromArgb(216, 234, 249);
            bang.DefaultCellStyle.SelectionForeColor = Color.FromArgb(42, 42, 42);
            bang.AllowUserToAddRows = false;
            bang.ReadOnly = true;
            bang.RowHeadersVisible = false;
            bang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            bang.DataBindingComplete += (nguoiGui, suKien) =>
            {
                foreach (DataGridViewColumn cot in bang.Columns)
                {
                    cot.HeaderText = TenCot(cot.DataPropertyName.Length > 0 ? cot.DataPropertyName : cot.Name);
                    if (cot.ValueType == typeof(decimal))
                        cot.DefaultCellStyle.Format = "N0";
                    if (cot.ValueType == typeof(DateTime))
                        cot.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                }
            };
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
                MessageBox.Show(loi is System.Data.SqlClient.SqlException ? "Không thể xử lý dữ liệu. Kiểm tra SQL Server, App.config và chạy script nâng cấp.\n" + loi.Message : loi.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            dgvDanhSach.DataBindingComplete += (doiTuongGui, suKien) =>
            {
                foreach (DataGridViewColumn cot in dgvDanhSach.Columns)
                {
                    cot.HeaderText = TenCot(cot.Name);
                    cot.MinimumWidth = cot.ValueType == typeof(bool) ? 65 : 115;
                    if (cot.ValueType == typeof(decimal))
                    {
                        cot.DefaultCellStyle.Format = cot.Name == "GioTinhCong" ? "N2" : "N0";
                        cot.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    }

                    if (cot.ValueType == typeof(DateTime))
                        cot.DefaultCellStyle.Format = cot.Name == "Ngay" || cot.Name == "HanSuDung" ? "dd/MM/yyyy" : "dd/MM/yyyy HH:mm";
                }
            };
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
