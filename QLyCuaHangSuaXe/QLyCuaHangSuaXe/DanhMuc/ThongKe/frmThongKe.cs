using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace QLyCuaHangSuaXe.DanhMuc.ThongKe
{
    // Code-behind cho form được kéo thả trong Designer.
    // Tên control trong Designer phải KHỚP với tên dùng ở đây.
    public partial class frmThongKe : Form
    {
        // ===== Bảng màu: chủ yếu xanh - trắng cho đồng bộ với form chung =====
        static readonly Color XanhChinh = ColorTranslator.FromHtml("#1F6FB5");
        static readonly Color XanhVua = ColorTranslator.FromHtml("#6FA8DC");
        static readonly Color XanhRatNhat = ColorTranslator.FromHtml("#F3F8FD");
        static readonly Color XanhTieuDe = ColorTranslator.FromHtml("#DCEBFA");
        static readonly Color VienNhat = ColorTranslator.FromHtml("#DCE6F0");
        static readonly Color NenForm = ColorTranslator.FromHtml("#F4F7FB");
        static readonly Color ChuPhu = ColorTranslator.FromHtml("#6B7785");

        // ===== Màu bắt buộc phải có ý nghĩa =====
        static readonly Color MauHetHang = ColorTranslator.FromHtml("#D64545");
        static readonly Color MauSapHet = ColorTranslator.FromHtml("#E8892B");
        static readonly Color NenHetHang = ColorTranslator.FromHtml("#FDE2E2");
        static readonly Color NenSapHet = ColorTranslator.FromHtml("#FFF3D6");
        static readonly Color MauTang = ColorTranslator.FromHtml("#2E9E6B");
        static readonly Color MauGiam = ColorTranslator.FromHtml("#D64545");

        // Gom 4 thẻ KPI vào mảng để xử lý một lượt
        Panel[] pnlKpi, pnlVach;
        Label[] lblKpiTieuDe, lblKpiGiaTri, lblKpiSoSanh;

        public frmThongKe()
        {
            InitializeComponent();
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
            KhoiTao();   // không cần gán sự kiện Load trong Designer
        }

        //  KHỞI TẠO: tô màu, tạo cột bảng, gán sự kiện, nạp dữ liệu lần đầu
        void KhoiTao()
        {
            Font = new Font("Segoe UI", 9F);
            BackColor = NenForm;

            foreach (Control c in new Control[] { pnlTieuDe, tlpKpi, pnlBieuDo, pnlTim, pnlTongHop })
                c.BackColor = NenForm;
            tabDoanhThu.BackColor = NenForm;
            tabTonKho.BackColor = NenForm;

            // Tiêu đề
            lblTieuDe.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTieuDe.ForeColor = XanhChinh;
            lblPhuDe.ForeColor = ChuPhu;

            // Hai GroupBox: tiêu đề xanh, chữ bên trong màu đen
            foreach (GroupBox g in new GroupBox[] { grpBoLoc, grpTonKho })
            {
                g.BackColor = Color.White;
                foreach (Control c in g.Controls) c.ForeColor = Color.Black;
                g.ForeColor = XanhChinh;
            }

            // Nút: nút hành động chính màu xanh đặc, còn lại trắng viền nhạt chữ xanh
            NutChinh(btnThongKe);
            NutChinh(btnLocCanhBao);
            NutPhu(btnLamMoi);
            NutPhu(btnXuatDoanhThu);
            NutPhu(btnLamMoiKho);
            NutPhu(btnXuatKho);

            // 4 thẻ KPI
            pnlKpi = new Panel[] { pnlKpi1, pnlKpi2, pnlKpi3, pnlKpi4 };
            pnlVach = new Panel[] { pnlVach1, pnlVach2, pnlVach3, pnlVach4 };
            lblKpiTieuDe = new Label[] { lblKpiTieuDe1, lblKpiTieuDe2, lblKpiTieuDe3, lblKpiTieuDe4 };
            lblKpiGiaTri = new Label[] { lblKpiGiaTri1, lblKpiGiaTri2, lblKpiGiaTri3, lblKpiGiaTri4 };
            lblKpiSoSanh = new Label[] { lblKpiSoSanh1, lblKpiSoSanh2, lblKpiSoSanh3, lblKpiSoSanh4 };
            for (int i = 0; i < 4; i++)
            {
                pnlKpi[i].BackColor = Color.White;
                pnlKpi[i].Margin = new Padding(0, 6, 10, 6);
                pnlVach[i].BackColor = XanhChinh;

                lblKpiTieuDe[i].ForeColor = ChuPhu;
                lblKpiTieuDe[i].TextAlign = ContentAlignment.MiddleLeft;

                lblKpiGiaTri[i].Font = new Font("Segoe UI", 16F, FontStyle.Bold);
                lblKpiGiaTri[i].ForeColor = XanhChinh;
                lblKpiGiaTri[i].TextAlign = ContentAlignment.MiddleLeft;
                lblKpiGiaTri[i].Text = "0";

                lblKpiSoSanh[i].Font = new Font("Segoe UI", 8.5F);
                lblKpiSoSanh[i].ForeColor = ChuPhu;
                lblKpiSoSanh[i].TextAlign = ContentAlignment.MiddleLeft;
                lblKpiSoSanh[i].Text = "";
            }

            // Ô nhập
            dtpTuNgay.Format = DateTimePickerFormat.Custom;
            dtpTuNgay.CustomFormat = "dd/MM/yyyy";
            dtpDenNgay.Format = DateTimePickerFormat.Custom;
            dtpDenNgay.CustomFormat = "dd/MM/yyyy";
            numMucTon.Minimum = 0;
            numMucTon.Maximum = 1000;
            numMucTon.Value = 5;

            StyleGrid(dgvDoanhThu);
            StyleGrid(dgvTonKho);
            TaoCotBang();
            CauHinhChart();

            // Sự kiện
            btnThongKe.Click += delegate { TaiDoanhThu(); };
            btnLamMoi.Click += delegate { txtTim.Text = ""; DatKhoangMacDinh(); };
            btnXuatDoanhThu.Click += delegate { XuatFile(dgvDoanhThu, "DoanhThu"); };
            btnLocCanhBao.Click += delegate { TaiTonKho(); };
            btnLamMoiKho.Click += delegate { numMucTon.Value = 5; TaiTonKho(); };
            btnXuatKho.Click += delegate { XuatFile(dgvTonKho, "TonKho"); };
            txtTim.TextChanged += delegate { LocBangDoanhThu(); };
            dgvDoanhThu.CellDoubleClick += dgvDoanhThu_CellDoubleClick;

            // Nạp dữ liệu lần đầu
            TaiTonKho();
            DatKhoangMacDinh();   // 30 ngày gần nhất, tự gọi TaiDoanhThu()
            tabDoanhThu.Resize += delegate { CanTiLe(); };
            tabTonKho.Resize += delegate { CanTiLe(); };
            Shown += delegate { CanTiLe(); };
            void CanTiLe()
            {
                chartCoCau.Width = (int)(tabDoanhThu.ClientSize.Width * 0.30);
                chartTonKho.Width = (int)(tabTonKho.ClientSize.Width * 0.38);
            }
        }

        //  TẠO GIAO DIỆN PHỤ (style)     
        void NutChinh(Button b)
        {
            b.FlatStyle = FlatStyle.Flat;
            b.FlatAppearance.BorderSize = 0;
            b.BackColor = XanhChinh;
            b.ForeColor = Color.White;
            b.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            b.Cursor = Cursors.Hand;
        }

        void NutPhu(Button b)
        {
            b.FlatStyle = FlatStyle.Flat;
            b.FlatAppearance.BorderSize = 1;
            b.FlatAppearance.BorderColor = VienNhat;
            b.FlatAppearance.MouseOverBackColor = XanhRatNhat;
            b.BackColor = Color.White;
            b.ForeColor = XanhChinh;
            b.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            b.Cursor = Cursors.Hand;
        }

        void StyleGrid(DataGridView g)
        {
            g.BorderStyle = BorderStyle.None;
            g.BackgroundColor = Color.White;
            g.EnableHeadersVisualStyles = false;
            g.RowHeadersVisible = false;
            g.AllowUserToAddRows = false;
            g.AllowUserToDeleteRows = false;
            g.ReadOnly = true;
            g.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            g.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            g.GridColor = VienNhat;
            g.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;

            g.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            g.ColumnHeadersHeight = 34;
            g.ColumnHeadersDefaultCellStyle.BackColor = XanhTieuDe;
            g.ColumnHeadersDefaultCellStyle.ForeColor = XanhChinh;
            g.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);

            g.RowTemplate.Height = 28;
            g.AlternatingRowsDefaultCellStyle.BackColor = XanhRatNhat;
            g.DefaultCellStyle.SelectionBackColor = XanhChinh;
            g.DefaultCellStyle.SelectionForeColor = Color.White;
        }

        void TaoCotBang()
        {
            dgvDoanhThu.Columns.Clear();
            dgvDoanhThu.Columns.Add("colMa", "Mã phiếu");
            dgvDoanhThu.Columns.Add("colNgay", "Ngày");
            dgvDoanhThu.Columns.Add("colKhach", "Khách hàng");
            dgvDoanhThu.Columns.Add("colTho", "Thợ phụ trách");
            dgvDoanhThu.Columns.Add("colDV", "Tiền dịch vụ");
            dgvDoanhThu.Columns.Add("colPT", "Tiền phụ tùng");
            dgvDoanhThu.Columns.Add("colTong", "Tổng cộng");
            foreach (string c in new string[] { "colDV", "colPT", "colTong" })
            {
                dgvDoanhThu.Columns[c].DefaultCellStyle.Format = "N0";
                dgvDoanhThu.Columns[c].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvDoanhThu.Columns[c].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            dgvTonKho.Columns.Clear();
            dgvTonKho.Columns.Add("colMaPT", "Mã PT");
            dgvTonKho.Columns.Add("colTenPT", "Tên phụ tùng");
            dgvTonKho.Columns.Add("colTon", "Tồn kho");
            dgvTonKho.Columns.Add("colTT", "Trạng thái");

            dgvTonKho.Columns["colMaPT"].FillWeight = 15;
            dgvTonKho.Columns["colTenPT"].FillWeight = 45;
            dgvTonKho.Columns["colTon"].FillWeight = 15;
            dgvTonKho.Columns["colTT"].FillWeight = 25;
            dgvTonKho.Columns["colTon"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvTonKho.Columns["colTon"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        Title TieuDeChart(string text)
        {
            return new Title(text, Docking.Top, new Font("Segoe UI", 10F, FontStyle.Bold), XanhChinh);
        }

        void CauHinhChart()
        {
            foreach (Chart ch in new Chart[] { chartDoanhThu, chartCoCau, chartTonKho })
            {
                ch.BackColor = Color.White;
                ch.BorderlineDashStyle = ChartDashStyle.Solid;
                ch.BorderlineColor = VienNhat;
                ch.BorderlineWidth = 1;
                ch.Titles.Clear();
                ch.ChartAreas[0].BackColor = Color.White;
                ch.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
                ch.ChartAreas[0].AxisY.MajorGrid.LineColor = VienNhat;
            }

            // Biểu đồ cột doanh thu theo ngày
            chartDoanhThu.Legends.Clear();
            chartDoanhThu.Titles.Add(TieuDeChart("Doanh thu theo ngày (triệu đồng)"));
            chartDoanhThu.Series[0].ChartType = SeriesChartType.Column;
            chartDoanhThu.Series[0].Color = XanhChinh;

            // Biểu đồ tròn cơ cấu: hai tông xanh
            if (chartCoCau.Legends.Count == 0) chartCoCau.Legends.Add(new Legend("Legend1"));
            chartCoCau.Legends[0].Docking = Docking.Bottom;
            chartCoCau.Titles.Add(TieuDeChart("Cơ cấu doanh thu"));
            Series c = chartCoCau.Series[0];
            c.ChartType = SeriesChartType.Doughnut;
            c.Label = "#PERCENT{P0}";
            c.LabelForeColor = Color.White;
            c["DoughnutRadius"] = "55";

            // Biểu đồ cột ngang tồn kho
            chartTonKho.Legends.Clear();
            chartTonKho.Titles.Add(TieuDeChart("Phụ tùng sắp hết (số lượng tồn)"));
            chartTonKho.Series[0].ChartType = SeriesChartType.Bar;
            chartTonKho.ChartAreas[0].AxisX.Interval = 1;
        }

        //  THỐNG KÊ DOANH THU
        void DatKhoangMacDinh()
        {
            dtpDenNgay.Value = DateTime.Today;
            dtpTuNgay.Value = DateTime.Today.AddDays(-29);
            TaiDoanhThu();
        }

        static void TinhTong(DataTable dt, out decimal dv, out decimal pt, out int soXe)
        {
            dv = 0; pt = 0; soXe = 0;
            foreach (DataRow r in dt.Rows)
            {
                dv += Convert.ToDecimal(r["TienDichVu"]);
                pt += Convert.ToDecimal(r["TienPhuTung"]);
                soXe++;
            }
        }

        void DatSoSanh(Label l, decimal hienTai, decimal truoc)
        {
            if (truoc == 0)
            {
                l.Text = hienTai > 0 ? "▲ Mới phát sinh" : "Chưa có dữ liệu kỳ trước";
                l.ForeColor = hienTai > 0 ? MauTang : ChuPhu;
                return;
            }
            double pct = (double)((hienTai - truoc) / truoc * 100m);
            l.Text = string.Format("{0} {1:0.#}% so với kỳ trước", pct >= 0 ? "▲" : "▼", Math.Abs(pct));
            l.ForeColor = pct >= 0 ? MauTang : MauGiam;
        }

        void TaiDoanhThu()
        {
            DateTime tu = dtpTuNgay.Value.Date;
            DateTime den = dtpDenNgay.Value.Date;
            if (tu > den)
            {
                MessageBox.Show("Ngày bắt đầu phải nhỏ hơn hoặc bằng ngày kết thúc.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kỳ trước có độ dài bằng kỳ đang chọn, nằm ngay trước đó
            int soNgay = (den - tu).Days + 1;
            DateTime tuTruoc = tu.AddDays(-soNgay);
            DateTime denTruoc = tu.AddDays(-1);

            DataTable dt, dtTruoc;
            try
            {
                dt = ThongKeDAL.LayDoanhThu(tu, den);
                dtTruoc = ThongKeDAL.LayDoanhThu(tuTruoc, denTruoc);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không đọc được dữ liệu doanh thu:\n" + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Bảng chi tiết + gom theo ngày cho biểu đồ
            dgvDoanhThu.Rows.Clear();
            var theoNgay = new SortedDictionary<DateTime, decimal>();
            foreach (DataRow r in dt.Rows)
            {
                decimal dv = Convert.ToDecimal(r["TienDichVu"]);
                decimal pt = Convert.ToDecimal(r["TienPhuTung"]);
                DateTime ngay = Convert.ToDateTime(r["Ngay"]).Date;

                if (theoNgay.ContainsKey(ngay)) theoNgay[ngay] += dv + pt; else theoNgay[ngay] = dv + pt;

                dgvDoanhThu.Rows.Add(r["MaPhieu"], ngay.ToString("dd/MM/yyyy"), r["KhachHang"],
                    r["ThoPhuTrach"], dv, pt, dv + pt);
            }

            // Thẻ KPI
            decimal tongDV, tongPT, truocDV, truocPT;
            int soXe, truocXe;
            TinhTong(dt, out tongDV, out tongPT, out soXe);
            TinhTong(dtTruoc, out truocDV, out truocPT, out truocXe);

            lblKpiGiaTri[0].Text = string.Format("{0:N0} đ", tongDV + tongPT);
            lblKpiGiaTri[1].Text = string.Format("{0:N0} đ", tongDV);
            lblKpiGiaTri[2].Text = string.Format("{0:N0} đ", tongPT);
            lblKpiGiaTri[3].Text = soXe + " xe";
            DatSoSanh(lblKpiSoSanh[0], tongDV + tongPT, truocDV + truocPT);
            DatSoSanh(lblKpiSoSanh[1], tongDV, truocDV);
            DatSoSanh(lblKpiSoSanh[2], tongPT, truocPT);
            DatSoSanh(lblKpiSoSanh[3], soXe, truocXe);

            // Biểu đồ cột
            Series s = chartDoanhThu.Series[0];
            s.Points.Clear();
            foreach (KeyValuePair<DateTime, decimal> kv in theoNgay)
                s.Points.AddXY(kv.Key.ToString("dd/MM"), (double)(kv.Value / 1000000m));

            // Biểu đồ tròn
            Series c = chartCoCau.Series[0];
            c.Points.Clear();
            if (tongDV + tongPT > 0)
            {
                int i1 = c.Points.AddXY("Dịch vụ", (double)tongDV);
                c.Points[i1].Color = XanhChinh;
                c.Points[i1].LegendText = "Dịch vụ";
                int i2 = c.Points.AddXY("Phụ tùng", (double)tongPT);
                c.Points[i2].Color = XanhVua;
                c.Points[i2].LegendText = "Phụ tùng";
            }

            dgvDoanhThu.ClearSelection();
            LocBangDoanhThu();
        }

        void LocBangDoanhThu()
        {
            string k = txtTim.Text.Trim().ToLower();
            dgvDoanhThu.CurrentCell = null;   // bắt buộc trước khi ẩn dòng
            foreach (DataGridViewRow r in dgvDoanhThu.Rows)
            {
                if (k == "") { r.Visible = true; continue; }
                string noiDung = (Convert.ToString(r.Cells["colMa"].Value) + " " +
                                  Convert.ToString(r.Cells["colKhach"].Value) + " " +
                                  Convert.ToString(r.Cells["colTho"].Value)).ToLower();
                r.Visible = noiDung.Contains(k);
            }
        }

        void dgvDoanhThu_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DataGridViewRow r = dgvDoanhThu.Rows[e.RowIndex];
            // Sau này: mở form chi tiết phiếu theo mã phiếu
            MessageBox.Show(
                "Phiếu: " + r.Cells["colMa"].Value + "\n" +
                "Khách hàng: " + r.Cells["colKhach"].Value + "\n" +
                "Thợ phụ trách: " + r.Cells["colTho"].Value + "\n" +
                "Tổng cộng: " + string.Format("{0:N0} đ", r.Cells["colTong"].Value),
                "Chi tiết phiếu", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        //  CẢNH BÁO TỒN KHO
        void TaiTonKho()
        {
            DataTable dt;
            try
            {
                dt = ThongKeDAL.LayTonKho();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không đọc được dữ liệu tồn kho:\n" + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int muc = (int)numMucTon.Value;
            dgvTonKho.Rows.Clear();
            Series s = chartTonKho.Series[0];
            s.Points.Clear();

            dt.DefaultView.Sort = "TonKho ASC";
            int canNhap = 0;
            foreach (DataRowView r in dt.DefaultView)
            {
                int ton = Convert.ToInt32(r["TonKho"]);
                if (ton > muc) continue;
                canNhap++;

                int idx = dgvTonKho.Rows.Add(r["MaPT"], r["TenPT"], ton, ton == 0 ? "Hết hàng" : "Sắp hết");
                dgvTonKho.Rows[idx].DefaultCellStyle.BackColor = ton == 0 ? NenHetHang : NenSapHet;
                dgvTonKho.Rows[idx].DefaultCellStyle.ForeColor = Color.Black;

                if (s.Points.Count < 10)   // biểu đồ chỉ vẽ 10 mặt hàng thấp nhất
                {
                    int p = s.Points.AddXY(Convert.ToString(r["TenPT"]), ton);
                    s.Points[p].Color = ton == 0 ? MauHetHang : MauSapHet;
                    s.Points[p].Label = ton.ToString();
                }
            }

            lblTongMatHang.Text = "Tổng số mặt hàng: " + dt.Rows.Count + " loại";
            lblCanNhapGap.Text = "Cần nhập gấp: " + canNhap + " loại";
            lblCanNhapGap.ForeColor = canNhap > 0 ? MauHetHang : ChuPhu;
            dgvTonKho.ClearSelection();
        }

        //  XUẤT FILE (CSV mở được bằng Excel, không cần cài thêm thư viện)
        void XuatFile(DataGridView g, string tenFile)
        {
            if (g.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "CSV (*.csv)|*.csv";
                sfd.FileName = tenFile + "_" + DateTime.Now.ToString("yyyyMMdd_HHmm") + ".csv";
                if (sfd.ShowDialog() != DialogResult.OK) return;

                var sb = new StringBuilder();
                var hs = new List<string>();
                foreach (DataGridViewColumn c in g.Columns) hs.Add(Csv(c.HeaderText));
                sb.AppendLine(string.Join(",", hs.ToArray()));

                foreach (DataGridViewRow r in g.Rows)
                {
                    if (!r.Visible) continue;
                    var cs = new List<string>();
                    foreach (DataGridViewCell c in r.Cells) cs.Add(Csv(GiaTri(c.Value)));
                    sb.AppendLine(string.Join(",", cs.ToArray()));
                }

                try
                {
                    File.WriteAllText(sfd.FileName, sb.ToString(), new UTF8Encoding(true));
                    MessageBox.Show("Đã xuất file:\n" + sfd.FileName, "Thành công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (IOException)
                {
                    MessageBox.Show("Không ghi được file. Hãy đóng file này trong Excel nếu đang mở rồi thử lại.",
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        // Số xuất ra dạng thuần (1150000) để Excel nhận là số
        static string GiaTri(object v)
        {
            if (v == null) return "";
            if (v is decimal || v is int || v is double) return Convert.ToString(v, CultureInfo.InvariantCulture);
            return Convert.ToString(v);
        }

        static string Csv(string s)
        {
            if (s == null) return "";
            return "\"" + s.Replace("\"", "\"\"") + "\"";
        }
    }
}