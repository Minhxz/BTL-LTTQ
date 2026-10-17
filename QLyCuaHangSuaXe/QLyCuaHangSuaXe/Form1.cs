using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLyCuaHangSuaXe
{
    public partial class Form1 : Form
    {
        private DanhMuc.TiepNhan.frmTiepNhan formTiepNhan;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnMoTiepNhan_Click(object sender, EventArgs e)
        {
            if (formTiepNhan == null || formTiepNhan.IsDisposed)
            {
                formTiepNhan = new DanhMuc.TiepNhan.frmTiepNhan();
                formTiepNhan.Show(this);
            }
            if (formTiepNhan.WindowState == FormWindowState.Minimized)
                formTiepNhan.WindowState = FormWindowState.Normal;
            formTiepNhan.Activate();
        }
    }
}
