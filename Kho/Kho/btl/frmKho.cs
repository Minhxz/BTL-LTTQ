using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace btl
{
	public partial class frmKho : Form
	{
		Classes.DatabaseProcessor dtbase = new Classes.DatabaseProcessor();
		Classes.Function function = new Classes.Function();
		private frmPhieuNhap formPhieuNhap;
		public frmKho()
		{
			InitializeComponent();
			btnPhieuNhap.Click += btnPhieuNhap_Click;
		}

		private void btnPhieuNhap_Click(object sender, EventArgs e)
		{
			if (formPhieuNhap == null || formPhieuNhap.IsDisposed)
			{
				formPhieuNhap = new frmPhieuNhap();
			}
			if (!formPhieuNhap.Visible)
				formPhieuNhap.Show(this);
			if (formPhieuNhap.WindowState == FormWindowState.Minimized)
				formPhieuNhap.WindowState = FormWindowState.Normal;
			formPhieuNhap.Activate();
		}

		private void frmKho_Load(object sender, EventArgs e)
		{
			string sqlPhuTung = "Select pt.MaPhuTung, pt.TenPhuTung, pt.SoLuongTon, ncc.TenNCC, ct.HanSuDung " +
				"From PhuTung pt Join ChiTietPhieuNhap ct On pt.MaPhuTung = ct.MaPhuTung " +
				"Join PhieuNhap pn On ct.MaPhieuNhap = pn.MaPhieuNhap " +
				"Join NhaCungCap ncc On pn.MaNCC = ncc.MaNCC " +
				"Order By pt.MaPhuTung Asc";

			dgvPhuTung.AutoGenerateColumns = false;
			try
			{
				dgvPhuTung.DataSource = dtbase.ReadData(sqlPhuTung);
			}
			catch (SqlException ex)
			{
				ThongBaoLoiTaiDuLieu(ex);
			}
			catch (InvalidOperationException ex)
			{
				ThongBaoLoiTaiDuLieu(ex);
			}
		}

		private void ThongBaoLoiTaiDuLieu(Exception error)
		{
			MessageBox.Show(this, "Không thể tải dữ liệu kho. Vui lòng kiểm tra kết nối SQL Server và cơ sở dữ liệu QuanLyNhapKho.\n" + error.Message,
				"Lỗi kết nối dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
		}

        private void frmKho_Load_1(object sender, EventArgs e)
        {

        }
    }
}
