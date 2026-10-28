using System;
using System.Drawing;
using System.Windows.Forms;

namespace QLyCuaHangSuaXe.LopDungChung
{
    public class TrangThietKe : Form
    {
        protected readonly XuLyDuLieu duLieu = new XuLyDuLieu();
        protected DataGridView dgvDanhSach;
        protected Label lblThongBao = new Label
        {
            Dock = DockStyle.Bottom,
            Height = 36,
            Padding = new Padding(8)
        };
        protected void GanThietKe(DataGridView bang, Label thongBao = null)
        {
            dgvDanhSach = bang;
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime || DesignMode)
                return;
            if (thongBao != null)
                lblThongBao = thongBao;
            else if (bang != null)
                Controls.Add(lblThongBao);
            lblThongBao.AutoEllipsis = true;
            if (thongBao != null && GetType().Name != "frmTiepNhan" && GetType().Name != "frmKiemTraTiepNhan") GiaoDien.AnHuongDan(thongBao);
            if (thongBao == null) lblThongBao.Visible = false;
            StartPosition = FormStartPosition.CenterParent;
            GiaoDien.ApDungMauTiepNhan(this);
            if (bang != null)
                GiaoDien.DinhDangBang(bang);
        }

        protected override void OnLoad(EventArgs suKien)
        {
            base.OnLoad(suKien);
            if (!DesignMode && !System.ComponentModel.LicenseManager.UsageMode.Equals(System.ComponentModel.LicenseUsageMode.Designtime))
                GiaoDien.ThucHien(TaiDuLieu);
        }

        public virtual void TaiDuLieu()
        {
        }
    }
}
