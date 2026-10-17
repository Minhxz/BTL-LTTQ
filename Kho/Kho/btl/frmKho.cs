using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace btl
{
	public partial class frmKho : Form
	{
		Classes.DatabaseProcessor dtbase = new Classes.DatabaseProcessor();
		Classes.Function function = new Classes.Function();
		public frmKho()
		{
			InitializeComponent();
		}

		private void frmKho_Load(object sender, EventArgs e)
		{
			string sqlPhuTung = "Select pt.MaPhuTung, pt.TenPhuTung, pt.SoLuongTon, ncc.TenNCC, ct.HanSuDung " +
				"From PhuTung pt Join ChiTietPhieuNhap ct On pt.MaPhuTung = ct.MaPhuTung " +
				"Join PhieuNhap pn On ct.MaPhieuNhap = pn.MaPhieuNhap " +
				"Join NhaCungCap ncc On pn.MaNCC = ncc.MaNCC " +
				"Order By pt.MaPhuTung Asc";

			dgvPhuTung.AutoGenerateColumns = false;
			dgvPhuTung.DataSource = dtbase.ReadData(sqlPhuTung);
		}
	}
}
