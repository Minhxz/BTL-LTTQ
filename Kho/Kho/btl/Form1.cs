using System.Data;
using System.Linq;

namespace btl
{
	public partial class frmNhapPhieuNhap : Form
	{
		Classes.DatabaseProcessor dtbase = new Classes.DatabaseProcessor();
		Classes.Function function = new Classes.Function();
		public frmNhapPhieuNhap()
		{
			InitializeComponent();
		}
		public frmNhapPhieuNhap(string maPhieuNhap)
		{
			InitializeComponent();
			txtMaPhieuNhap.Text = maPhieuNhap;
			txtMaPhieuNhap.Enabled = false;
		}

		private void CheckBtnSua()
		{
			string sql = "Select * From PhieuNhap Where MaPhieuNhap = '" + txtMaPhieuNhap.Text + "'";
			DataTable data = dtbase.ReadData(sql);

			if(data.Rows.Count > 0)
			{
				string sqlDgvPhuTung = "Select pn.MaPhieuNhap, ncc.TenNCC, pn.NgayNhap, pt.MaPhuTung, pt.TenPhuTung, ct.SoLuong As SoLuongNhap, ct.DonGiaNhap, ct.ThanhTien, ct.HanSuDung, pn.NguoiNhap " +
					"From PhuTung pt Join ChiTietPhieuNhap ct On pt.MaPhuTung = ct.MaPhuTung " +
					"Join PhieuNhap pn On pn.MaPhieuNhap = ct.MaPhieuNhap " +
					"Join NhaCungCap ncc On ct.MaNCC = ncc.MaNCC " +
					"Where pn.MaPhieuNhap = '" + txtMaPhieuNhap.Text + "'";

				dgvPhuTung.DataSource = dtbase.ReadData(sqlDgvPhuTung);
			}
		}

		private void frmKho_Load(object sender, EventArgs e)
		{
			function.NumberOnly(txtSoLuongNhap);
			function.NumberOnly(txtDonGiaNhap);

			txtThanhTien.Enabled = false;
			dtpNgayNhap.Enabled = false;

			function.AddComboBox(cbNhaCungCap, dtbase.ReadData("Select TenNCC, MaNCC From NhaCungCap"), "TenNCC", "MaNCC");
			cbNhaCungCap.SelectedIndex = -1;
			function.AddComboBox(cbMaPhuTung, dtbase.ReadData("Select MaPhuTung From PhuTung"), "MaPhuTung", "MaPhuTung");
			cbMaPhuTung.SelectedIndex = -1;

			dgvPhuTung.AutoGenerateColumns = false;
			dgvPhuTung.DataSource = null;
			dgvPhuTung.Rows.Clear();

			btnXoaNCC.Enabled = false;
			btnSuaPhuTung.Enabled = false;
			btnXoaPhuTung.Enabled = false;
			btnHuyPhuTung.Enabled = false;

			CheckBtnSua();
		}

		private void ResetForm()
		{
			txtMaNVNhap.Clear();
			cbNhaCungCap.SelectedIndex = -1;
			cbNhaCungCap.Text = "";
			cbMaPhuTung.SelectedIndex = -1;
			cbMaPhuTung.Text = "";
			txtTenPhuTung.Clear();
			dtpHanSuDung.Value = DateTime.Now;
			txtSoLuongNhap.Clear();
			txtDonGiaNhap.Clear();
			txtThanhTien.Clear();
		}

		private void btnThemNCC_Click(object sender, EventArgs e)
		{
			frmThemNCC form = new frmThemNCC();
			form.ShowDialog();

			if (form.DialogResult == DialogResult.OK)
			{
				function.AddComboBox(cbNhaCungCap, dtbase.ReadData("Select TenNCC, MaNCC From NhaCungCap"), "TenNCC", "MaNCC");
				cbNhaCungCap.SelectedIndex = -1;
			}
		}

		private void btnXoaNCC_Click(object sender, EventArgs e)
		{
			if (cbNhaCungCap.SelectedValue == null || cbNhaCungCap.Text.Trim() == "")
			{
				MessageBox.Show("Chọn nhà cung cấp muốn xóa!", "Thông báo");
				return;
			}

			if (MessageBox.Show("Chắc chắn muốn xóa?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				string sql = "Delete From NhaCungCap Where MaNCC = '" + cbNhaCungCap.SelectedValue.ToString() + "'";
				dtbase.ChangeData(sql);

				MessageBox.Show("Xóa thành công!", "Thông báo");
				function.AddComboBox(cbNhaCungCap, dtbase.ReadData("Select TenNCC, MaNCC From NhaCungCap"), "TenNCC", "MaNCC");
				cbNhaCungCap.SelectedIndex = -1;
			}
		}

		private void btnThemPhuTung_Click(object sender, EventArgs e)
		{
			if (cbMaPhuTung.Text.Trim() == "" || txtTenPhuTung.Text.Trim() == "" || txtSoLuongNhap.Text.Trim() == "" || txtDonGiaNhap.Text.Trim() == "" || txtMaPhieuNhap.Text.Trim() == "" || txtMaNVNhap.Text.Trim() == "" || cbNhaCungCap.SelectedValue == null)
			{
				MessageBox.Show("Không được để trống!", "Thông báo");
				return;
			}

			if (dtpHanSuDung.Value.Date < DateTime.Today)
			{
				MessageBox.Show("Giá trị hạn sử dụng không hợp lệ", "Thông báo");
				dtpHanSuDung.Focus();
				return;
			}

			if (Convert.ToInt32(txtSoLuongNhap.Text) <= 0 || Double.Parse(txtDonGiaNhap.Text) <= 0)
			{
				MessageBox.Show("Giá trị số lượng hoặc đơn giá nhập không hợp lệ!", "Thông báo");
				return;
			}

			dgvPhuTung.Rows.Add(
				txtMaPhieuNhap.Text,
				cbNhaCungCap.Text,
				dtpNgayNhap.Value.Date,
				cbMaPhuTung.Text,
				txtTenPhuTung.Text,
				txtSoLuongNhap.Text,
				txtDonGiaNhap.Text,
				txtThanhTien.Text,
				dtpHanSuDung.Value.Date,
				txtMaNVNhap.Text
				);

			ResetForm();
			txtMaPhieuNhap.Enabled = false;
			btnXoaNCC.Enabled = false;
		}

		private void CalThanhTien()
		{
			if (double.TryParse(txtSoLuongNhap.Text, out double soLuong) && double.TryParse(txtDonGiaNhap.Text, out double donGia))
			{
				txtThanhTien.Text = (soLuong * donGia).ToString();
			}
			else
			{
				txtThanhTien.Clear();
			}
		}

		private void txtSoLuongNhap_TextChanged(object sender, EventArgs e)
		{
			if (txtDonGiaNhap.Text.Trim() == "")
			{
				return;
			}
			CalThanhTien();
		}

		private void txtDonGiaNhap_TextChanged(object sender, EventArgs e)
		{
			if (txtSoLuongNhap.Text.Trim() == "")
			{
				return;
			}
			CalThanhTien();
		}

		private void cbNhaCungCap_TextChanged(object sender, EventArgs e)
		{
			btnXoaNCC.Enabled = true;
		}

		int selectedRowIndex = -1;

		private void dgvPhuTung_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex < 0)
			{
				return;
			}
			selectedRowIndex = e.RowIndex;

			btnSuaPhuTung.Enabled = true;
			btnXoaPhuTung.Enabled = true;
			btnHuyPhuTung.Enabled = true;
			btnThemPhuTung.Enabled = false;
			btnXoaNCC.Enabled = false;

			txtMaPhieuNhap.Text = dgvPhuTung.CurrentRow.Cells["colMaPhieu"].Value.ToString();
			dtpNgayNhap.Value = Convert.ToDateTime(dgvPhuTung.CurrentRow.Cells["colNgayNhap"].Value);
			txtMaNVNhap.Text = dgvPhuTung.CurrentRow.Cells["colMaNVNhap"].Value.ToString();
			cbNhaCungCap.Text = dgvPhuTung.CurrentRow.Cells["colTenNCC"].Value.ToString();
			cbMaPhuTung.Text = dgvPhuTung.CurrentRow.Cells["colMaPhuTung"].Value.ToString();
			txtTenPhuTung.Text = dgvPhuTung.CurrentRow.Cells["colTenPhuTung"].Value.ToString();
			dtpHanSuDung.Value = Convert.ToDateTime(dgvPhuTung.CurrentRow.Cells["colHanSuDung"].Value);
			txtSoLuongNhap.Text = dgvPhuTung.CurrentRow.Cells["colSoLuongNhap"].Value.ToString();
			txtDonGiaNhap.Text = dgvPhuTung.CurrentRow.Cells["colDonGiaNhap"].Value.ToString();
			txtThanhTien.Text = dgvPhuTung.CurrentRow.Cells["colThanhTien"].Value.ToString();
		}

		private void btnSuaPhuTung_Click(object sender, EventArgs e)
		{
			if (selectedRowIndex == -1)
			{
				return;
			}

			if (MessageBox.Show("Xác nhận sửa?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				DataGridViewRow row = dgvPhuTung.Rows[selectedRowIndex];
				row.Cells["colMaPhieu"].Value = txtMaPhieuNhap.Text;
				row.Cells["colNgayNhap"].Value = dtpNgayNhap.Value;
				row.Cells["colMaNVNhap"].Value = txtMaNVNhap.Text;
				row.Cells["colTenNCC"].Value = cbNhaCungCap.Text;
				row.Cells["colMaPhuTung"].Value = cbMaPhuTung.Text;
				row.Cells["colTenPhuTung"].Value = txtTenPhuTung.Text;
				row.Cells["colHanSuDung"].Value = dtpHanSuDung.Value;
				row.Cells["colSoLuongNhap"].Value = txtSoLuongNhap.Text;
				row.Cells["colDonGiaNhap"].Value = txtDonGiaNhap.Text;
				row.Cells["colThanhTien"].Value = txtThanhTien.Text;

				ResetForm();

				btnThemPhuTung.Enabled = true;
				btnSuaPhuTung.Enabled = false;
				btnXoaPhuTung.Enabled = false;
				btnHuyPhuTung.Enabled = false;
				btnXoaNCC.Enabled = false;
			}
			else
			{
				return;
			}
		}

		private void btnXoaPhuTung_Click(object sender, EventArgs e)
		{
			if (selectedRowIndex == -1)
			{
				return;
			}

			if (MessageBox.Show("Xác nhận xóa?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				dgvPhuTung.Rows.RemoveAt(selectedRowIndex);

				selectedRowIndex = -1;

				btnSuaPhuTung.Enabled = false;
				btnXoaPhuTung.Enabled = false;
				btnHuyPhuTung.Enabled = false;
				btnThemPhuTung.Enabled = true;
			}

			if (dgvPhuTung.Rows.Count == 0)
			{
				txtMaPhieuNhap.Enabled = true;
			}
		}

		private void btnHuyPhuTung_Click(object sender, EventArgs e)
		{
			selectedRowIndex = -1;

			btnSuaPhuTung.Enabled = false;
			btnXoaPhuTung.Enabled = false;
			btnHuyPhuTung.Enabled = false;
			btnThemPhuTung.Enabled = true;

			dgvPhuTung.ClearSelection();
		}

		private void btnLamMoiPhuTung_Click(object sender, EventArgs e)
		{
			cbMaPhuTung.SelectedIndex = -1;
			cbMaPhuTung.Text = "";
			txtTenPhuTung.Clear();
			dtpHanSuDung.Value = DateTime.Now;
			txtSoLuongNhap.Clear();
			txtDonGiaNhap.Clear();
			txtThanhTien.Clear();
		}

		private void btnLuuPhieu_Click(object sender, EventArgs e)
		{
			var rows = dgvPhuTung.Rows.Cast<DataGridViewRow>().Where(row => !row.IsNewRow).ToList();

			if (rows.Count == 0)
			{
				MessageBox.Show("Thêm thông tin phiếu nhập trước!", "Thông báo");
				return;
			}
			try
			{
				DataTable dataLuu = dtbase.ReadData("Select * From PhieuNhap Where MaPhieuNhap = '" + txtMaPhieuNhap.Text + "'");
				if (dataLuu.Rows.Count > 0)
				{
					DateTime updateNgayNhap = Convert.ToDateTime(rows[0].Cells["colNgayNhap"].Value);
					string sqlUpdateNgayNhap = "Update PhieuNhap Set NgayNhap = " + updateNgayNhap + "Where MaPhieuNhap = '" + txtMaPhieuNhap.Text + "'";
					dtbase.ChangeData(sqlUpdateNgayNhap);

					string updateMaNV = rows[0].Cells["colMaNVNhap"].Value.ToString();
					string sqlUpdateMaNV = "Update PhieuNhap Set nguoiNhap = '" + updateMaNV + "' Where MaPhieuNhap = '" + txtMaPhieuNhap.Text + "'";
					dtbase.ChangeData(sqlUpdateMaNV);

					dtbase.ChangeData("Delete From ChiTietPhieuNhap Where MaPhieuNhap = '" + txtMaPhieuNhap.Text + "'");
				}
				else
				{
					string maPhieuNhap = txtMaPhieuNhap.Text;

					DateTime ngayNhap = Convert.ToDateTime(rows[0].Cells["colNgayNhap"].Value);
					string maNV = rows[0].Cells["colMaNVNhap"].Value.ToString();

					string sqlPhieu = "Insert Into PhieuNhap (MaPhieuNhap, NgayNhap, NguoiNhap) " +
						"Values('" + maPhieuNhap + "', '" + ngayNhap + "', '" + maNV + "')";

					dtbase.ChangeData(sqlPhieu);
				}

				foreach (DataGridViewRow row in rows)
				{
					string tenNCC = row.Cells["colTenNCC"].Value.ToString();
					DataTable data = dtbase.ReadData("Select MaNCC From NhaCungCap Where TenNCC = N'" + tenNCC + "'");
					string maNCC = data.Rows[0]["MaNCC"].ToString();

					string sqlChiTietPhieuNhap = "Insert Into ChiTietPhieuNhap (MaPhieuNhap, MaNCC, MaPhuTung, SoLuong, DonGiaNhap, hanSuDung) Values" +
						"('" + row.Cells["colMaPhieu"].Value.ToString() + "', " +
						"'" + maNCC + "', " +
						"'" + row.Cells["colMaPhuTung"].Value.ToString() + "', " +
						"'" + Convert.ToInt32(row.Cells["colSoLuongNhap"].Value.ToString()) + "', " +
						"'" + Convert.ToDecimal(row.Cells["colDonGiaNhap"].Value.ToString()) + "', " +
						"'" + Convert.ToDateTime(row.Cells["colHanSuDung"].Value) + "')";

					dtbase.ChangeData(sqlChiTietPhieuNhap);
				}

				MessageBox.Show("Lưu phiếu nhập thành công!", "Thông báo");

				ResetForm();
				txtMaPhieuNhap.Clear();
			}
			catch (Exception ex)
			{
				MessageBox.Show("Lỗi khi lưu phiếu nhập: " + ex.Message, "Thông báo");
			}
		}

		private void btnThoat_Click(object sender, EventArgs e)
		{
			if(MessageBox.Show("Chắc chắn muốn thoát?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				this.Close();
			}
		}
	}
}