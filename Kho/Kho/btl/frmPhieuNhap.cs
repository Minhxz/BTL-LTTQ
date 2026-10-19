using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace btl
{
	public partial class frmPhieuNhap : Form
	{
		private frmNhapPhieuNhap formNhapPhieuNhap;

		public frmPhieuNhap()
		{
			InitializeComponent();
			btnThemPhieuNhap.Click += btnThemPhieuNhap_Click;
			btnThoat.Click += btnThoat_Click;
		}

		private void btnThemPhieuNhap_Click(object sender, EventArgs e)
		{
			if (formNhapPhieuNhap == null || formNhapPhieuNhap.IsDisposed)
			{
				formNhapPhieuNhap = new frmNhapPhieuNhap();
			}
			if (!formNhapPhieuNhap.Visible)
				formNhapPhieuNhap.Show(this);
			if (formNhapPhieuNhap.WindowState == FormWindowState.Minimized)
				formNhapPhieuNhap.WindowState = FormWindowState.Normal;
			formNhapPhieuNhap.Activate();
		}

		private void btnThoat_Click(object sender, EventArgs e)
		{
			Close();
		}

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
