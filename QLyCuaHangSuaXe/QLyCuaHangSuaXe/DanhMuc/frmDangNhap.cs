using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace QLyCuaHangSuaXe.DanhMuc
{
    public partial class frmDangNhap : Form
    {
        Classes.DataProcessor dtBase = new Classes.DataProcessor();
        Classes.Function ft = new Classes.Function();
        public frmDangNhap()
        {
            InitializeComponent();
        }

        private void frmDangNhap_Load(object sender, EventArgs e)
        {

        }

        private void dgvTaiKhoan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
