using QLyCuaHangSuaXe.LopDungChung;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace QLyCuaHangSuaXe.DanhMuc
{
    public class frmBangCong : TrangDuLieu
    {
        readonly bool laQuanLy = PhanQuyenNguoiDung.CoQuyen(PhienDangNhap.VaiTro, "BANG_CONG");
        readonly ComboBox cboNhanVien = GiaoDien.TaoDanhSachChon(), cboCa = GiaoDien.TaoDanhSachChon(), cboBangCong = GiaoDien.TaoDanhSachChon();
        readonly DateTimePicker dtpNgayLam = new DateTimePicker
        {
            Format = DateTimePickerFormat.Custom,
            CustomFormat = "dd/MM/yyyy"
        }, dtpTuNgay = new DateTimePicker
        {
            Format = DateTimePickerFormat.Custom,
            CustomFormat = "dd/MM/yyyy",
            Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1)
        }, dtpDenNgay = new DateTimePicker
        {
            Format = DateTimePickerFormat.Custom,
            CustomFormat = "dd/MM/yyyy"
        };
        readonly DateTimePicker dtpGioVao = new DateTimePicker
        {
            Format = DateTimePickerFormat.Custom,
            CustomFormat = "HH:mm",
            ShowUpDown = true
        }, dtpGioRa = new DateTimePicker
        {
            Format = DateTimePickerFormat.Custom,
            CustomFormat = "HH:mm",
            ShowUpDown = true
        };
        readonly CheckBox chkVang = new CheckBox
        {
            Text = "Vắng cả ca",
            AutoSize = true
        };
        readonly NumericUpDown numDieuChinhPhut = new NumericUpDown
        {
            Minimum = -390,
            Maximum = 390
        };
        readonly TextBox txtLyDo = new TextBox();
        readonly Label lblCachTinh = new Label { Dock = DockStyle.Fill, Padding = new Padding(12, 8, 12, 4), ForeColor = Color.FromArgb(37, 65, 90) };
        readonly Label lblTongHop = new Label { Dock = DockStyle.Top, Height = 44, Padding = new Padding(12, 8, 8, 4), Font = new Font("Segoe UI", 10, FontStyle.Bold), ForeColor = GiaoDien.Xanh };
        readonly GroupBox grbCachTinh = new GroupBox { Text = "Cách tính tiền công", Dock = DockStyle.Top, Height = 130, Padding = new Padding(8), BackColor = Color.FromArgb(240, 247, 253) };
        readonly ComboBox cboLocNhanVien = GiaoDien.TaoDanhSachChon();
        Control vungChinhSua, thanhChinhSua;
        Button btnLuuCong;
        bool dangChonDong;
        decimal? luongGioDaChot;
        string maNhanVienDaChot;
        int maBangCong;
        public frmBangCong() : base("Bảng công & lương")
        {
            if (laQuanLy)
            {
                var tblThongTin = GiaoDien.TaoBangNhap();
                GiaoDien.ThemTruongNhap(tblThongTin, "Nhân viên *", cboNhanVien);
                GiaoDien.ThemTruongNhap(tblThongTin, "Ngày làm", dtpNgayLam);
                GiaoDien.ThemTruongNhap(tblThongTin, "Ca làm *", cboCa);
                GiaoDien.ThemTruongNhap(tblThongTin, "Vắng", chkVang);
                GiaoDien.ThemTruongNhap(tblThongTin, "Giờ vào", dtpGioVao);
                GiaoDien.ThemTruongNhap(tblThongTin, "Giờ ra", dtpGioRa);
                GiaoDien.ThemTruongNhap(tblThongTin, "Cộng/trừ phút", numDieuChinhPhut);
                GiaoDien.ThemTruongNhap(tblThongTin, "Lý do", txtLyDo);
                ThemVungNhap(tblThongTin);
                vungChinhSua = tblThongTin;
                thanhChinhSua = GiaoDien.TaoThanhThaoTac();
                thanhChinhSua.Controls.Add(GiaoDien.TaoNut("Thêm dòng công", () =>
                {
                    maBangCong = 0;
                    luongGioDaChot = null;
                    dtpNgayLam.Value = DateTime.Today;
                    cboCa.SelectedIndex = -1;
                    cboNhanVien.SelectedIndex = -1;
                    chkVang.Checked = false;
                    numDieuChinhPhut.Value = 0;
                    txtLyDo.Clear();
                    lblThongBao.Text = "Đang thêm dòng mới.";
                    HienDuTinh();
                }));
                btnLuuCong = GiaoDien.TaoNut("Lưu / đổi ca", Luu);
                thanhChinhSua.Controls.Add(btnLuuCong);
                ThemVungNhap(thanhChinhSua);
            }

            cboBangCong.Items.AddRange(new object[] { "Chi tiết công", "Tổng hợp lương", "Tiền nhiệm vụ hoàn thành" });
            cboBangCong.SelectedIndex = 0;
            cboBangCong.Width = 205;
            dtpTuNgay.Width = dtpDenNgay.Width = 120;
            cboLocNhanVien.Width = 200;
            var giaTriThuHai = GiaoDien.TaoThanhThaoTac();
            giaTriThuHai.Controls.Add(GiaoDien.TaoNhan("Từ"));
            giaTriThuHai.Controls.Add(dtpTuNgay);
            giaTriThuHai.Controls.Add(GiaoDien.TaoNhan("Đến"));
            giaTriThuHai.Controls.Add(dtpDenNgay);
            if (laQuanLy) giaTriThuHai.Controls.Add(cboLocNhanVien);
            giaTriThuHai.Controls.Add(cboBangCong);
            giaTriThuHai.Controls.Add(GiaoDien.TaoNut("Xem bảng công", TaiDuLieu));
            giaTriThuHai.Controls.Add(GiaoDien.TaoNut("Xuất Excel", () => XuatExcel.LuuExcel((DataTable)dgvDanhSach.DataSource, "Bảng công " + dtpTuNgay.Value.ToString("dd/MM/yyyy") + " – " + dtpDenNgay.Value.ToString("dd/MM/yyyy"))));
            ThemVungNhap(giaTriThuHai);
            grbCachTinh.Controls.Add(lblCachTinh);
            ThemVungNhap(grbCachTinh);
            ThemVungNhap(lblTongHop);
            cboCa.SelectedIndexChanged += (doiTuongGui, suKien) =>
            {
                var dongHienThi = cboCa.SelectedItem as DataRowView;
                if (dongHienThi == null)
                    return;
                dtpGioVao.Value = DateTime.Today.Add((TimeSpan)dongHienThi["GioBatDau"]);
                dtpGioRa.Value = DateTime.Today.Add((TimeSpan)dongHienThi["GioKetThuc"]);
                HienDuTinh();
            };
            chkVang.CheckedChanged += (doiTuongGui, suKien) =>
            {
                dtpGioVao.Enabled = dtpGioRa.Enabled = !chkVang.Checked;
                if (chkVang.Checked)
                    numDieuChinhPhut.Value = 0;
                numDieuChinhPhut.Enabled = !chkVang.Checked;
                HienDuTinh();
            };
            dtpGioVao.ValueChanged += (nguoiGui, suKien) => HienDuTinh();
            dtpGioRa.ValueChanged += (nguoiGui, suKien) => HienDuTinh();
            numDieuChinhPhut.ValueChanged += (nguoiGui, suKien) => HienDuTinh();
            cboNhanVien.SelectedIndexChanged += (nguoiGui, suKien) => HienDuTinh();
            txtLyDo.MaxLength = 300;
            dgvDanhSach.SelectionChanged += (doiTuongGui, suKien) => ChonDong();
        }

        public override void TaiDuLieu()
        {
            PhienDangNhap.YeuCau(laQuanLy ? "BANG_CONG" : "CONG_CA_NHAN");
            if (dtpTuNgay.Value.Date > dtpDenNgay.Value.Date)
                throw new InvalidOperationException("Ngày bắt đầu phải trước ngày kết thúc.");
            if (laQuanLy)
            {
                string nhanVienCu = GiaoDien.GiaTriDangChon(cboNhanVien), caCu = GiaoDien.GiaTriDangChon(cboCa), locCu = GiaoDien.GiaTriDangChon(cboLocNhanVien);
                dangChonDong = true;
                try
                {
                GiaoDien.GanDuLieu(cboNhanVien, duLieu.DocBang("SELECT MaNhanVien,TenNhanVien,LuongGio FROM dbo.NhanVien WHERE TrangThai=N'Đang làm'"), "TenNhanVien", "MaNhanVien");
                GiaoDien.GanDuLieu(cboCa, duLieu.DocBang("SELECT * FROM dbo.CaLam ORDER BY GioBatDau"), "TenCa", "MaCa");
                var boLoc = duLieu.DocBang("SELECT MaNhanVien,TenNhanVien FROM dbo.NhanVien ORDER BY TenNhanVien");
                var tatCa = boLoc.NewRow(); tatCa[0] = ""; tatCa[1] = "Tất cả nhân viên"; boLoc.Rows.InsertAt(tatCa, 0);
                GiaoDien.GanDuLieu(cboLocNhanVien, boLoc, "TenNhanVien", "MaNhanVien");
                cboLocNhanVien.SelectedIndex = 0;
                if (nhanVienCu.Length > 0) cboNhanVien.SelectedValue = nhanVienCu;
                if (caCu.Length > 0) cboCa.SelectedValue = caCu;
                cboLocNhanVien.SelectedValue = locCu;
                }
                finally { dangChonDong = false; }
            }

            string[] cauLenh = {
                @"SELECT MaBangCong,MaNhanVien,TenNhanVien,Ngay,MaCa,TenCa,GioBatDau,GioKetThuc,GioVao,GioRa,Vang,PhutThucTe,DieuChinhPhut,LyDo,LuongGio,PhutTinhCong,GioTinhCong,PhutMuon,PhutVeSom,TienLuong FROM dbo.vBangCong WHERE Ngay>=@a AND Ngay<@b AND (@all=1 OR MaNhanVien=@nv) AND (@loc='' OR MaNhanVien=@loc) ORDER BY Ngay DESC,TenNhanVien",
                @"SELECT n.MaNhanVien,n.TenNhanVien,COALESCE(b.SoCa,0) AS SoCaCoMat,COALESCE(b.Vang,0) AS SoCaVang,COALESCE(b.Muon,0) AS PhutMuon,COALESCE(b.Som,0) AS PhutVeSom,COALESCE(b.DieuChinh,0) AS DieuChinhPhut,CAST(COALESCE(b.Phut,0)/60.0 AS DECIMAL(12,2)) AS GioTinhCong,COALESCE(b.Luong,0) AS LuongTheoGio,COALESCE(p.SoViec,0) AS NhiemVuHoanThanh,COALESCE(p.Tien,0) AS LuongNhiemVu,COALESCE(b.Luong,0)+COALESCE(p.Tien,0) AS TongLuong
                FROM dbo.NhanVien n OUTER APPLY(SELECT SUM(CASE WHEN Vang=0 THEN 1 ELSE 0 END) AS SoCa,SUM(CASE WHEN Vang=1 THEN 1 ELSE 0 END) AS Vang,SUM(PhutMuon) AS Muon,SUM(PhutVeSom) AS Som,SUM(DieuChinhPhut) AS DieuChinh,SUM(PhutTinhCong) AS Phut,SUM(TienLuong) AS Luong FROM dbo.vBangCong c WHERE c.MaNhanVien=n.MaNhanVien AND Ngay>=@a AND Ngay<@b) b
                OUTER APPLY(SELECT COUNT(*) AS SoViec,SUM(TienCong) AS Tien FROM dbo.PhanCongTho c WHERE c.MaNhanVien=n.MaNhanVien AND TrangThaiCongViec=N'Hoàn thành' AND NgayGhiNhanCong>=@a AND NgayGhiNhanCong<@b) p WHERE (@all=1 OR n.MaNhanVien=@nv) AND (@loc='' OR n.MaNhanVien=@loc)",
                @"SELECT p.MaNhanVien,n.TenNhanVien,p.MaPhieuTiepNhan,x.BienSo,p.ThoiGianKetThuc,p.NgayGhiNhanCong,p.TienCong FROM dbo.PhanCongTho p JOIN dbo.NhanVien n ON n.MaNhanVien=p.MaNhanVien JOIN dbo.PhieuTiepNhan t ON t.MaPhieuTiepNhan=p.MaPhieuTiepNhan JOIN dbo.Xe x ON x.MaXe=t.MaXe
                WHERE p.TrangThaiCongViec=N'Hoàn thành' AND p.NgayGhiNhanCong>=@a AND p.NgayGhiNhanCong<@b AND (@all=1 OR p.MaNhanVien=@nv) AND (@loc='' OR p.MaNhanVien=@loc) ORDER BY p.NgayGhiNhanCong DESC"
            };
            bool chiTiet = cboBangCong.SelectedIndex == 0;
            if (laQuanLy) { vungChinhSua.Visible = thanhChinhSua.Visible = chiTiet; btnLuuCong.Enabled = chiTiet; }
            var bang = duLieu.DocBang(cauLenh[cboBangCong.SelectedIndex], XuLyDuLieu.ThamSo("@a", dtpTuNgay.Value.Date), XuLyDuLieu.ThamSo("@b", dtpDenNgay.Value.Date.AddDays(1)), XuLyDuLieu.ThamSo("@all", laQuanLy), XuLyDuLieu.ThamSo("@nv", PhienDangNhap.MaNhanVien), XuLyDuLieu.ThamSo("@loc", laQuanLy ? GiaoDien.GiaTriDangChon(cboLocNhanVien) : ""));
            dgvDanhSach.DataSource = bang;
            string[] cacCot = chiTiet ? new[] { "TenNhanVien", "Ngay", "TenCa", "GioVao", "GioRa", "PhutTinhCong", "DieuChinhPhut", "TienLuong" }
                : cboBangCong.SelectedIndex == 1 ? new[] { "TenNhanVien", "SoCaCoMat", "SoCaVang", "GioTinhCong", "LuongTheoGio", "LuongNhiemVu", "TongLuong" }
                : new[] { "TenNhanVien", "MaPhieuTiepNhan", "BienSo", "ThoiGianKetThuc", "NgayGhiNhanCong", "TienCong" };
            for (int i = cacCot.Length - 1; i >= 0; i--) dgvDanhSach.Columns[cacCot[i]].DisplayIndex = 0;
            foreach (DataGridViewColumn cot in dgvDanhSach.Columns)
            {
                cot.Visible = cacCot.Contains(cot.Name);
                cot.MinimumWidth = 60;
                if (cot.ValueType == typeof(TimeSpan)) cot.DefaultCellStyle.Format = @"hh\:mm";
            }
            DoiTenCot("PhutTinhCong", "Phút tính công"); DoiTenCot("DieuChinhPhut", "+/− phút"); DoiTenCot("TienLuong", "Tiền ca (đ)");
            DoiTenCot("SoCaCoMat", "Ca có mặt"); DoiTenCot("SoCaVang", "Ca vắng"); DoiTenCot("LuongTheoGio", "Lương giờ (đ)"); DoiTenCot("LuongNhiemVu", "Nhiệm vụ (đ)"); DoiTenCot("TongLuong", "Tổng lương (đ)");
            DoiTenCot("TienCong", "Tiền nhiệm vụ (đ)"); DoiTenCot("NgayGhiNhanCong", "Ngày ghi nhận");
            string cotTien = chiTiet ? "TienLuong" : cboBangCong.SelectedIndex == 1 ? "TongLuong" : "TienCong";
            lblTongHop.Text = dtpTuNgay.Value.ToString("dd/MM") + " – " + dtpDenNgay.Value.ToString("dd/MM/yyyy") + "   •   " + bang.Rows.Count + (chiTiet ? " dòng công" : cboBangCong.SelectedIndex == 1 ? " nhân viên" : " nhiệm vụ") + "   •   Tổng: " + bang.AsEnumerable().Sum(dong => Convert.ToDecimal(dong[cotTien])).ToString("N2") + " đ";
            lblThongBao.Text = "Ca sáng 07:30–14:00; chiều tối 13:30–20:00. Chọn dòng để xem cách tính. Xuất Excel có đầy đủ số phút, đơn giá và lý do.";
            ChonDong();
        }

        void ChonDong()
        {
            if (dgvDanhSach.CurrentRow == null) { lblCachTinh.Text = "Chưa có dữ liệu trong khoảng ngày đã chọn. Chưa chấm công không được tự tính thành vắng."; return; }
            var dong = ((DataRowView)dgvDanhSach.CurrentRow.DataBoundItem).Row;
            if (!dong.Table.Columns.Contains("MaBangCong"))
            {
                if (dong.Table.Columns.Contains("TongLuong"))
                    lblCachTinh.Text = dong["TenNhanVien"] + " • Có mặt " + dong["SoCaCoMat"] + " ca • Vắng " + dong["SoCaVang"] + " ca • Muộn " + dong["PhutMuon"] + " phút • Về sớm " + dong["PhutVeSom"] + " phút\n"
                        + "Tổng lương = " + Convert.ToDecimal(dong["LuongTheoGio"]).ToString("N2") + " đ tiền ca + " + Convert.ToDecimal(dong["LuongNhiemVu"]).ToString("N2") + " đ từ " + dong["NhiemVuHoanThanh"] + " nhiệm vụ hoàn thành = " + Convert.ToDecimal(dong["TongLuong"]).ToString("N2") + " đ.\n"
                        + "Tiền ca cộng từ từng dòng công theo đơn giá đã chốt; nhiệm vụ tính theo ngày ghi nhận hoàn thành. Chưa chấm công không tự tính là vắng.";
                else lblCachTinh.Text = dong["TenNhanVien"] + " • Phiếu " + dong["MaPhieuTiepNhan"] + " • Biển số " + dong["BienSo"] + "\nTiền nhiệm vụ đã chốt: " + Convert.ToDecimal(dong["TienCong"]).ToString("N2") + " đ. Chỉ cộng sau khi hoàn thành.\nKhoản này cộng riêng với tiền ca, không nhân tiếp với số giờ làm.";
                return;
            }
            var tinhCong = ChamCong.TinhChiTiet((TimeSpan)dong["GioBatDau"], (TimeSpan)dong["GioKetThuc"], dong["GioVao"] == DBNull.Value ? TimeSpan.Zero : (TimeSpan)dong["GioVao"], dong["GioRa"] == DBNull.Value ? TimeSpan.Zero : (TimeSpan)dong["GioRa"], Convert.ToBoolean(dong["Vang"]), Convert.ToInt32(dong["DieuChinhPhut"]), Convert.ToDecimal(dong["LuongGio"]));
            lblCachTinh.Text = "ĐÃ LƯU • " + dong["TenNhanVien"] + " • " + Convert.ToDateTime(dong["Ngay"]).ToString("dd/MM/yyyy") + " • " + dong["TenCa"] + "\n" + tinhCong.GiaiThich();
            if (dong["LyDo"].ToString().Length > 0) lblCachTinh.Text += "\nLý do: " + dong["LyDo"];
            if (!laQuanLy) return;
            dangChonDong = true;
            try
            {
            maBangCong = Convert.ToInt32(GiaoDien.GiaTriO(dgvDanhSach, "MaBangCong"));
            cboNhanVien.SelectedValue = GiaoDien.GiaTriO(dgvDanhSach, "MaNhanVien");
            dtpNgayLam.Value = Convert.ToDateTime(dgvDanhSach.CurrentRow.Cells["Ngay"].Value);
            cboCa.SelectedValue = GiaoDien.GiaTriO(dgvDanhSach, "MaCa");
            chkVang.Checked = Convert.ToBoolean(dgvDanhSach.CurrentRow.Cells["Vang"].Value);
            if (!chkVang.Checked)
            {
                dtpGioVao.Value = DateTime.Today.Add((TimeSpan)dgvDanhSach.CurrentRow.Cells["GioVao"].Value);
                dtpGioRa.Value = DateTime.Today.Add((TimeSpan)dgvDanhSach.CurrentRow.Cells["GioRa"].Value);
            }

            numDieuChinhPhut.Value = Convert.ToDecimal(dgvDanhSach.CurrentRow.Cells["DieuChinhPhut"].Value);
            txtLyDo.Text = GiaoDien.GiaTriO(dgvDanhSach, "LyDo");
            maNhanVienDaChot = dong["MaNhanVien"].ToString();
            luongGioDaChot = Convert.ToDecimal(dong["LuongGio"]);
            }
            finally { dangChonDong = false; }
        }

        void DoiTenCot(string ten, string nhan)
        {
            if (dgvDanhSach.Columns.Contains(ten)) dgvDanhSach.Columns[ten].HeaderText = nhan;
        }

        void HienDuTinh()
        {
            if (dangChonDong || !laQuanLy) return;
            var ca = cboCa.SelectedItem as DataRowView;
            var nv = cboNhanVien.SelectedItem as DataRowView;
            if (ca == null || nv == null) { lblCachTinh.Text = "Chọn nhân viên và ca làm để xem trước cách tính tiền ca."; return; }
            decimal giaGio = luongGioDaChot.HasValue && nv["MaNhanVien"].ToString() == maNhanVienDaChot ? luongGioDaChot.Value : Convert.ToDecimal(nv["LuongGio"]);
            try
            {
                var tinh = ChamCong.TinhChiTiet((TimeSpan)ca["GioBatDau"], (TimeSpan)ca["GioKetThuc"], dtpGioVao.Value.TimeOfDay, dtpGioRa.Value.TimeOfDay, chkVang.Checked, (int)numDieuChinhPhut.Value, giaGio);
                lblCachTinh.Text = "DỰ TÍNH CHƯA LƯU • " + nv["TenNhanVien"] + " • Đơn giá " + (luongGioDaChot.HasValue && nv["MaNhanVien"].ToString() == maNhanVienDaChot ? "đã chốt" : "hiện tại") + "\n" + tinh.GiaiThich();
            }
            catch (InvalidOperationException loi) { lblCachTinh.Text = loi.Message; }
        }

        void Luu()
        {
            ChamCong.LuuCong(maBangCong, GiaoDien.GiaTriDangChon(cboNhanVien), dtpNgayLam.Value, GiaoDien.GiaTriDangChon(cboCa), dtpGioVao.Value.TimeOfDay, dtpGioRa.Value.TimeOfDay, chkVang.Checked, (int)numDieuChinhPhut.Value, txtLyDo.Text);
            TaiDuLieu();
            lblThongBao.Text = "Đã lưu công và lịch sử điều chỉnh.";
        }
    }
}
