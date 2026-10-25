using QLyCuaHangSuaXe.LopDungChung;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace QLyCuaHangSuaXe.DanhMuc.TiepNhan
{
    public partial class frmKiemTraTiepNhan : Form
    {
        readonly XuLyDuLieu duLieu = new XuLyDuLieu();
        readonly NghiepVuSuaXe nghiepVu = new NghiepVuSuaXe();
        readonly DataTable bangHangMuc = new DataTable();
        DataTable bangDinhMuc;
        public bool DuDieuKien { get; private set; }

        public string TinhTrangXe
        {
            get
            {
                return rtbTinhTrang.Text.Trim();
            }
        }

        public DataTable ChiTietDaChon
        {
            get
            {
                return bangHangMuc.Copy();
            }
        }

        public decimal TongDuKien
        {
            get
            {
                return bangHangMuc.AsEnumerable().Sum(dong => dong.Field<decimal>("ThanhTien"));
            }
        }

        public List<DichVuChon> DichVuDaChon
        {
            get
            {
                if (!DuDieuKien)
                    throw new InvalidOperationException("Hãy kiểm tra điều kiện trước khi chuyển sang tiếp nhận.");
                return LayDichVu();
            }
        }

        public frmKiemTraTiepNhan()
        {
            InitializeComponent();
            GiaoDien.ApDungMauTiepNhan(this);
            GiaoDien.DinhDangBang(dgvHangMuc);
            bangHangMuc.Columns.Add("MaDichVu");
            bangHangMuc.Columns.Add("TenDichVu");
            bangHangMuc.Columns.Add("PhuTung");
            bangHangMuc.Columns.Add("SoLuong", typeof(int));
            bangHangMuc.Columns["SoLuong"].AllowDBNull = false;
            bangHangMuc.Columns["SoLuong"].DefaultValue = 1;
            bangHangMuc.Columns.Add("TienCong", typeof(decimal));
            bangHangMuc.Columns.Add("GiaPhuTung", typeof(decimal));
            bangHangMuc.Columns.Add("ThanhTien", typeof(decimal), "(TienCong+GiaPhuTung)*SoLuong");
            bangHangMuc.Columns.Add("DieuKien");
            dgvHangMuc.DataSource = bangHangMuc;
            bangHangMuc.ColumnChanged += (nguoiGui, suKien) =>
            {
                if (suKien.Column.ColumnName == "SoLuong")
                    KhoaKetQua();
            };
            dgvHangMuc.ReadOnly = false;
            foreach (DataGridViewColumn cot in dgvHangMuc.Columns)
                cot.ReadOnly = cot.Name != "SoLuong";
            dgvHangMuc.Columns["PhuTung"].HeaderText = "Phụ tùng";
            dgvHangMuc.Columns["GiaPhuTung"].HeaderText = "Giá phụ tùng / DV";
            dgvHangMuc.Columns["DieuKien"].HeaderText = "Điều kiện";
            dgvHangMuc.DataBindingComplete += (nguoiGui, suKien) =>
            {
                dgvHangMuc.Columns["MaDichVu"].HeaderText = "Mã DV";
                dgvHangMuc.Columns["SoLuong"].HeaderText = "SL";
                dgvHangMuc.Columns["TienCong"].HeaderText = "Tiền công";
                dgvHangMuc.Columns["MaDichVu"].FillWeight = 65;
                dgvHangMuc.Columns["TenDichVu"].FillWeight = 150;
                dgvHangMuc.Columns["PhuTung"].FillWeight = 150;
                dgvHangMuc.Columns["SoLuong"].FillWeight = 55;
            };
            cboDichVu.SelectedIndexChanged += (nguoiGui, suKien) => HienThiDichVu();
            btnThem.Click += (nguoiGui, suKien) => GiaoDien.ThucHien(ThemDichVu);
            btnXoaDong.Click += (nguoiGui, suKien) =>
            {
                if (dgvHangMuc.CurrentRow != null)
                {
                    bangHangMuc.Rows.RemoveAt(dgvHangMuc.CurrentRow.Index);
                    KhoaKetQua();
                }
            };
            btnLamMoi.Click += (nguoiGui, suKien) =>
            {
                bangHangMuc.Clear();
                rtbTinhTrang.Clear();
                KhoaKetQua();
            };
            btnKiemTra.Click += (nguoiGui, suKien) => GiaoDien.ThucHien(KiemTraDieuKien);
            btnChuyen.Click += (nguoiGui, suKien) =>
            {
                if (DuDieuKien)
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
            };
            dgvHangMuc.CellValueChanged += (nguoiGui, suKien) =>
            {
                if (suKien.RowIndex >= 0 && suKien.ColumnIndex == dgvHangMuc.Columns["SoLuong"].Index)
                    KhoaKetQua();
            };
            dgvHangMuc.DataError += (nguoiGui, suKien) =>
            {
                suKien.ThrowException = false;
                KhoaKetQua();
                lblKetQua.Text = "Số lượng phải là số nguyên từ 1 đến 1000.";
            };
            KhoaKetQua();
        }

        protected override void OnLoad(EventArgs suKien)
        {
            base.OnLoad(suKien);
            if (!DesignMode && System.ComponentModel.LicenseManager.UsageMode != System.ComponentModel.LicenseUsageMode.Designtime)
                GiaoDien.ThucHien(TaiDuLieu);
        }

        public void TaiDuLieu()
        {
            PhienDangNhap.YeuCau("TIEP_NHAN");
            bangDinhMuc = duLieu.DocBang("SELECT m.MaDichVu,p.MaPhuTung,p.TenPhuTung,m.SoLuong,p.SoLuongTon,p.DonGiaBan,p.TrangThai FROM dbo.DinhMucDichVu m JOIN dbo.PhuTung p ON p.MaPhuTung=m.MaPhuTung");
            GiaoDien.GanDuLieu(cboDichVu, duLieu.DocBang("SELECT MaDichVu,TenDichVu,DonGiaDichVu FROM dbo.DichVu WHERE TrangThai=N'Đang sử dụng' ORDER BY TenDichVu"), "TenDichVu", "MaDichVu");
            if (cboDichVu.Items.Count > 0)
                cboDichVu.SelectedIndex = 0;
        }

        List<DataRow> PhuTungDichVu(string ma)
        {
            return bangDinhMuc == null ? new List<DataRow>() : bangDinhMuc.AsEnumerable().Where(dong => dong["MaDichVu"].ToString() == ma).ToList();
        }

        void HienThiDichVu()
        {
            var dichVu = cboDichVu.SelectedItem as DataRowView;
            if (dichVu == null)
                return;
            var phuTung = PhuTungDichVu(dichVu["MaDichVu"].ToString());
            cboPhuTung.Items.Clear();
            foreach (var dong in phuTung)
                cboPhuTung.Items.Add(dong["TenPhuTung"] + " × " + dong["SoLuong"]);
            if (phuTung.Count == 0)
                cboPhuTung.Items.Add("Dịch vụ chỉ tính tiền công");
            cboPhuTung.SelectedIndex = 0;
            txtTonKho.Text = phuTung.Count == 0 ? "Không dùng phụ tùng" : phuTung.Min(dong => Convert.ToInt32(dong["SoLuongTon"]) / Convert.ToInt32(dong["SoLuong"])).ToString();
            txtTienCong.Text = Convert.ToDecimal(dichVu["DonGiaDichVu"]).ToString("N0") + " đ";
            txtGiaPhuTung.Text = phuTung.Sum(dong => Convert.ToDecimal(dong["DonGiaBan"]) * Convert.ToInt32(dong["SoLuong"])).ToString("N0") + " đ";
        }

        void ThemDichVu()
        {
            var dichVu = cboDichVu.SelectedItem as DataRowView;
            if (dichVu == null)
                throw new InvalidOperationException("Chọn dịch vụ trước.");
            string ma = dichVu["MaDichVu"].ToString();
            var daCo = bangHangMuc.AsEnumerable().FirstOrDefault(dong => dong["MaDichVu"].ToString() == ma);
            if (daCo != null)
            {
                int soLuong = Convert.ToInt32(daCo["SoLuong"]) + (int)numSoLuong.Value;
                if (soLuong > 1000)
                    throw new InvalidOperationException("Số lượng mỗi dịch vụ tối đa 1000.");
                daCo["SoLuong"] = soLuong;
            }
            else
            {
                var phuTung = PhuTungDichVu(ma);
                bangHangMuc.Rows.Add(ma, dichVu["TenDichVu"], string.Join(", ", phuTung.Select(dong => dong["TenPhuTung"] + " × " + dong["SoLuong"])), (int)numSoLuong.Value, dichVu["DonGiaDichVu"], phuTung.Sum(dong => Convert.ToDecimal(dong["DonGiaBan"]) * Convert.ToInt32(dong["SoLuong"])), null, "Chưa kiểm tra");
            }

            KhoaKetQua();
        }

        List<DichVuChon> LayDichVu()
        {
            dgvHangMuc.EndEdit();
            return bangHangMuc.AsEnumerable().Select(dong => new DichVuChon { Ma = dong["MaDichVu"].ToString(), SoLuong = Convert.ToInt32(dong["SoLuong"]) }).ToList();
        }

        void KhoaKetQua()
        {
            DuDieuKien = false;
            btnChuyen.Enabled = false;
            lblKetQua.Text = "CHƯA KIỂM TRA";
            lblKetQua.ForeColor = Color.FromArgb(145, 103, 35);
            lblGiaiThich.Text = "Bấm Kiểm tra điều kiện sau khi chọn xong. Đổi hạng mục hoặc số lượng sẽ cần kiểm tra lại.";
            foreach (DataRow dong in bangHangMuc.Rows)
                dong["DieuKien"] = "Chưa kiểm tra";
            lblSoHangMuc.Text = bangHangMuc.Rows.Count + " hạng mục";
            lblTongTien.Text = "Dự kiến: " + TongDuKien.ToString("N0") + " đ";
            lblThanhPhan.Text = "Tiền công: " + bangHangMuc.AsEnumerable().Sum(dong => dong.Field<decimal>("TienCong") * dong.Field<int>("SoLuong")).ToString("N0") + " đ   |   Phụ tùng: " + bangHangMuc.AsEnumerable().Sum(dong => dong.Field<decimal>("GiaPhuTung") * dong.Field<int>("SoLuong")).ToString("N0") + " đ";
        }

        void KiemTraDieuKien()
        {
            var cacDichVu = LayDichVu();
            KhoaKetQua();
            try
            {
                nghiepVu.KiemTraKho(cacDichVu);
            }
            catch (InvalidOperationException loi)
            {
                lblKetQua.Text = "CHƯA ĐỦ ĐIỀU KIỆN";
                lblKetQua.ForeColor = Color.Firebrick;
                lblGiaiThich.Text = loi.Message;
                return;
            }

            DuDieuKien = true;
            btnChuyen.Enabled = true;
            foreach (DataRow dong in bangHangMuc.Rows)
                dong["DieuKien"] = "Đủ phụ tùng";
            lblKetQua.Text = "ĐỦ ĐIỀU KIỆN TIẾP NHẬN";
            lblKetQua.ForeColor = Color.SeaGreen;
            lblGiaiThich.Text = "Dịch vụ đang sử dụng và phụ tùng đủ theo tổng nhu cầu. Khi lưu phiếu sẽ kiểm tra lại tồn kho và thợ.";
        }
    }
}
