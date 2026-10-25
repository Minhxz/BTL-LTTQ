using System.Drawing;
using System.Windows.Forms;

namespace btl
{
    partial class frmThemNCC
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblTieuDe = new Label();
            lblMaNhaCungCap = new Label();
            txtMaNCC = new TextBox();
            txtDienThoai = new TextBox();
            lblDienThoai = new Label();
            txtTenNhaCungCap = new TextBox();
            lblTenNhaCungCap = new Label();
            lblDiaChi = new Label();
            btnXacNhan = new Button();
            btnHuy = new Button();
            txtDiaChi = new RichTextBox();
            SuspendLayout();
            lblTieuDe.AutoSize = true;
            lblTieuDe.Location = new Point(264, 30);
            lblTieuDe.Name = "lblTieuDe";
            lblTieuDe.Size = new Size(160, 20);
            lblTieuDe.TabIndex = 0;
            lblTieuDe.Text = "THÊM NHÀ CUNG CẤP";
            lblMaNhaCungCap.AutoSize = true;
            lblMaNhaCungCap.Location = new Point(43, 93);
            lblMaNhaCungCap.Name = "lblMaNhaCungCap";
            lblMaNhaCungCap.Size = new Size(66, 20);
            lblMaNhaCungCap.TabIndex = 1;
            lblMaNhaCungCap.Text = "Mã NCC:";
            txtMaNCC.Location = new Point(135, 90);
            txtMaNCC.Name = "txtMaNCC";
            txtMaNCC.Size = new Size(183, 27);
            txtMaNCC.TabIndex = 2;
            txtDienThoai.Location = new Point(475, 90);
            txtDienThoai.Name = "txtDienThoai";
            txtDienThoai.Size = new Size(179, 27);
            txtDienThoai.TabIndex = 4;
            lblDienThoai.AutoSize = true;
            lblDienThoai.Location = new Point(388, 93);
            lblDienThoai.Name = "lblDienThoai";
            lblDienThoai.Size = new Size(36, 20);
            lblDienThoai.TabIndex = 3;
            lblDienThoai.Text = "SĐT";
            txtTenNhaCungCap.Location = new Point(135, 162);
            txtTenNhaCungCap.Name = "txtTenNhaCungCap";
            txtTenNhaCungCap.Size = new Size(183, 27);
            txtTenNhaCungCap.TabIndex = 6;
            lblTenNhaCungCap.AutoSize = true;
            lblTenNhaCungCap.Location = new Point(43, 165);
            lblTenNhaCungCap.Name = "lblTenNhaCungCap";
            lblTenNhaCungCap.Size = new Size(68, 20);
            lblTenNhaCungCap.TabIndex = 5;
            lblTenNhaCungCap.Text = "Tên NCC:";
            lblDiaChi.AutoSize = true;
            lblDiaChi.Location = new Point(388, 165);
            lblDiaChi.Name = "lblDiaChi";
            lblDiaChi.Size = new Size(58, 20);
            lblDiaChi.TabIndex = 7;
            lblDiaChi.Text = "Địa chỉ:";
            btnXacNhan.Location = new Point(148, 260);
            btnXacNhan.Name = "btnXacNhan";
            btnXacNhan.Size = new Size(147, 48);
            btnXacNhan.TabIndex = 9;
            btnXacNhan.Text = "Xác nhận";
            btnXacNhan.UseVisualStyleBackColor = true;
            btnXacNhan.FlatStyle = FlatStyle.Flat;
            btnXacNhan.FlatAppearance.BorderSize = 0;
            btnXacNhan.BackColor = Color.FromArgb(30, 108, 176);
            btnXacNhan.ForeColor = Color.White;
            btnXacNhan.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnXacNhan.Cursor = Cursors.Hand;
            btnXacNhan.UseVisualStyleBackColor = false;
            btnHuy.Location = new Point(387, 260);
            btnHuy.Name = "btnHuy";
            btnHuy.Size = new Size(147, 48);
            btnHuy.TabIndex = 10;
            btnHuy.Text = "Hủy";
            btnHuy.UseVisualStyleBackColor = true;
            btnHuy.FlatStyle = FlatStyle.Flat;
            btnHuy.FlatAppearance.BorderColor = Color.FromArgb(191, 207, 220);
            btnHuy.BackColor = Color.White;
            btnHuy.ForeColor = Color.FromArgb(30, 85, 130);
            btnHuy.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnHuy.Cursor = Cursors.Hand;
            btnHuy.UseVisualStyleBackColor = false;
            txtDiaChi.Location = new Point(475, 165);
            txtDiaChi.Name = "txtDiaChi";
            txtDiaChi.Size = new Size(179, 76);
            txtDiaChi.TabIndex = 11;
            txtDiaChi.Text = "";
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(711, 339);
            Controls.Add(txtDiaChi);
            Controls.Add(btnHuy);
            Controls.Add(btnXacNhan);
            Controls.Add(lblDiaChi);
            Controls.Add(txtTenNhaCungCap);
            Controls.Add(lblTenNhaCungCap);
            Controls.Add(txtDienThoai);
            Controls.Add(lblDienThoai);
            Controls.Add(txtMaNCC);
            Controls.Add(lblMaNhaCungCap);
            Controls.Add(lblTieuDe);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "frmThemNCC";
            Text = "frmThemNCC";
            ResumeLayout(false);
            PerformLayout();
        }

        private Label lblTieuDe;
        private Label lblMaNhaCungCap;
        private TextBox txtMaNCC;
        private TextBox txtDienThoai;
        private Label lblDienThoai;
        private TextBox txtTenNhaCungCap;
        private Label lblTenNhaCungCap;
        private Label lblDiaChi;
        private Button btnXacNhan;
        private Button btnHuy;
        private RichTextBox txtDiaChi;
    }
}
