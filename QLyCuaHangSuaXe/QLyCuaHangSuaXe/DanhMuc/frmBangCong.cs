using QLyCuaHangSuaXe.LopDungChung;
using System;
using System.Data;
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
                var thanhChinhSua = GiaoDien.TaoThanhThaoTac();
                thanhChinhSua.Controls.Add(GiaoDien.TaoNut("Thêm dòng công", () =>
                {
                    maBangCong = 0;
                    dtpNgayLam.Value = DateTime.Today;
                    cboCa.SelectedIndex = -1;
                    cboNhanVien.SelectedIndex = -1;
                    chkVang.Checked = false;
                    numDieuChinhPhut.Value = 0;
                    txtLyDo.Clear();
                    lblThongBao.Text = "Đang thêm dòng mới.";
                }));
                thanhChinhSua.Controls.Add(GiaoDien.TaoNut("Lưu / đổi ca", Luu));
                ThemVungNhap(thanhChinhSua);
            }

            cboBangCong.Items.AddRange(new object[] { "Chi tiết công", "Tổng hợp lương" });
            cboBangCong.SelectedIndex = 0;
            var giaTriThuHai = GiaoDien.TaoThanhThaoTac();
            giaTriThuHai.Controls.Add(GiaoDien.TaoNhan("Từ"));
            giaTriThuHai.Controls.Add(dtpTuNgay);
            giaTriThuHai.Controls.Add(GiaoDien.TaoNhan("Đến"));
            giaTriThuHai.Controls.Add(dtpDenNgay);
            giaTriThuHai.Controls.Add(cboBangCong);
            giaTriThuHai.Controls.Add(GiaoDien.TaoNut("Xem bảng công", TaiDuLieu));
            giaTriThuHai.Controls.Add(GiaoDien.TaoNut("Xuất Excel", () => XuatExcel.LuuExcel((DataTable)dgvDanhSach.DataSource, "Bảng công " + dtpTuNgay.Value.ToString("dd/MM/yyyy") + " – " + dtpDenNgay.Value.ToString("dd/MM/yyyy"))));
            ThemVungNhap(giaTriThuHai);
            cboCa.SelectedIndexChanged += (doiTuongGui, suKien) =>
            {
                var dongHienThi = cboCa.SelectedItem as DataRowView;
                if (dongHienThi == null)
                    return;
                dtpGioVao.Value = DateTime.Today.Add((TimeSpan)dongHienThi["GioBatDau"]);
                dtpGioRa.Value = DateTime.Today.Add((TimeSpan)dongHienThi["GioKetThuc"]);
            };
            chkVang.CheckedChanged += (doiTuongGui, suKien) =>
            {
                dtpGioVao.Enabled = dtpGioRa.Enabled = !chkVang.Checked;
                if (chkVang.Checked)
                    numDieuChinhPhut.Value = 0;
                numDieuChinhPhut.Enabled = !chkVang.Checked;
            };
            dgvDanhSach.SelectionChanged += (doiTuongGui, suKien) => ChonDong();
        }

        public override void TaiDuLieu()
        {
            PhienDangNhap.YeuCau(laQuanLy ? "BANG_CONG" : "CONG_CA_NHAN");
            if (dtpTuNgay.Value.Date > dtpDenNgay.Value.Date)
                throw new InvalidOperationException("Ngày bắt đầu phải trước ngày kết thúc.");
            if (laQuanLy && cboNhanVien.DataSource == null)
            {
                GiaoDien.GanDuLieu(cboNhanVien, duLieu.DocBang("SELECT MaNhanVien,TenNhanVien FROM dbo.NhanVien WHERE TrangThai=N'Đang làm'"), "TenNhanVien", "MaNhanVien");
                GiaoDien.GanDuLieu(cboCa, duLieu.DocBang("SELECT * FROM dbo.CaLam ORDER BY GioBatDau"), "TenCa", "MaCa");
            }

            string cauLenhSql = cboBangCong.SelectedIndex == 0 ? "SELECT MaBangCong,MaNhanVien,TenNhanVien,Ngay,MaCa,TenCa,GioVao,GioRa,Vang,DieuChinhPhut,LyDo,LuongGio,GioTinhCong,PhutMuon,PhutVeSom,TienLuong FROM dbo.vBangCong WHERE Ngay>=@a AND Ngay<@b AND (@all=1 OR MaNhanVien=@nv) ORDER BY Ngay DESC,TenNhanVien" : @"SELECT n.MaNhanVien,n.TenNhanVien,COALESCE(b.Gio,0) AS GioTinhCong,COALESCE(b.Luong,0) AS LuongTheoGio,COALESCE(p.Tien,0) AS LuongNhiemVu,COALESCE(b.Luong,0)+COALESCE(p.Tien,0) AS TongLuong FROM dbo.NhanVien n OUTER APPLY(SELECT SUM(GioTinhCong) AS Gio,SUM(TienLuong) AS Luong FROM dbo.vBangCong c WHERE c.MaNhanVien=n.MaNhanVien AND Ngay>=@a AND Ngay<@b) b OUTER APPLY(SELECT SUM(TienCong) AS Tien FROM dbo.PhanCongTho c WHERE c.MaNhanVien=n.MaNhanVien AND TrangThaiCongViec=N'Hoàn thành' AND NgayGhiNhanCong>=@a AND NgayGhiNhanCong<@b) p WHERE (@all=1 OR n.MaNhanVien=@nv)";
            dgvDanhSach.DataSource = duLieu.DocBang(cauLenhSql, XuLyDuLieu.ThamSo("@a", dtpTuNgay.Value.Date), XuLyDuLieu.ThamSo("@b", dtpDenNgay.Value.Date.AddDays(1)), XuLyDuLieu.ThamSo("@all", laQuanLy), XuLyDuLieu.ThamSo("@nv", PhienDangNhap.MaNhanVien));
            lblThongBao.Text = "Sáng 07:30–14:00; chiều tối 13:30–20:00. Lương = giờ thực tính × giá giờ đã chốt + nhiệm vụ hoàn thành. Mọi điều chỉnh công đều có lịch sử.";
        }

        void ChonDong()
        {
            if (!laQuanLy || dgvDanhSach.CurrentRow == null || !dgvDanhSach.Columns.Contains("MaBangCong"))
                return;
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
        }

        void Luu()
        {
            ChamCong.LuuCong(maBangCong, GiaoDien.GiaTriDangChon(cboNhanVien), dtpNgayLam.Value, GiaoDien.GiaTriDangChon(cboCa), dtpGioVao.Value.TimeOfDay, dtpGioRa.Value.TimeOfDay, chkVang.Checked, (int)numDieuChinhPhut.Value, txtLyDo.Text);
            TaiDuLieu();
            lblThongBao.Text = "Đã lưu công và lịch sử điều chỉnh.";
        }
    }
}
