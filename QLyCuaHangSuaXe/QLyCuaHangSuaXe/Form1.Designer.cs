namespace QLyCuaHangSuaXe
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.lblHuongDan = new System.Windows.Forms.Label();
            this.btnMoTiepNhan = new System.Windows.Forms.Button();
            this.SuspendLayout();
            //
            // lblTieuDe
            //
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTieuDe.ForeColor = System.Drawing.Color.FromArgb(28, 73, 103);
            this.lblTieuDe.Location = new System.Drawing.Point(34, 30);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(450, 32);
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "QUẢN LÝ CỬA HÀNG SỬA XE";
            //
            // lblHuongDan
            //
            this.lblHuongDan.AutoSize = true;
            this.lblHuongDan.Location = new System.Drawing.Point(37, 80);
            this.lblHuongDan.Name = "lblHuongDan";
            this.lblHuongDan.Size = new System.Drawing.Size(180, 15);
            this.lblHuongDan.TabIndex = 1;
            this.lblHuongDan.Text = "Chọn chức năng để bắt đầu.";
            //
            // btnMoTiepNhan
            //
            this.btnMoTiepNhan.BackColor = System.Drawing.Color.FromArgb(40, 91, 145);
            this.btnMoTiepNhan.FlatAppearance.BorderSize = 0;
            this.btnMoTiepNhan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMoTiepNhan.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnMoTiepNhan.ForeColor = System.Drawing.Color.White;
            this.btnMoTiepNhan.Location = new System.Drawing.Point(40, 122);
            this.btnMoTiepNhan.Name = "btnMoTiepNhan";
            this.btnMoTiepNhan.Size = new System.Drawing.Size(290, 52);
            this.btnMoTiepNhan.TabIndex = 2;
            this.btnMoTiepNhan.Text = "Tiếp nhận và sửa chữa xe";
            this.btnMoTiepNhan.UseVisualStyleBackColor = false;
            this.btnMoTiepNhan.Click += new System.EventHandler(this.btnMoTiepNhan_Click);
            //
            // Form1
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnMoTiepNhan);
            this.Controls.Add(this.lblHuongDan);
            this.Controls.Add(this.lblTieuDe);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý cửa hàng sửa xe";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.Label lblHuongDan;
        private System.Windows.Forms.Button btnMoTiepNhan;
    }
}

