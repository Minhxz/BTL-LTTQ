namespace BaoCaoThongKe
{
    partial class frmBaoCaoThongKe
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnThoat = new System.Windows.Forms.Button();
            this.lblTongDoanhThu = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblTienCong = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblTienPhuTung = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblSoXe = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dgvThongKeDoanhThu = new System.Windows.Forms.DataGridView();
            this.grbBoLocTG = new System.Windows.Forms.GroupBox();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.btnThongKe = new System.Windows.Forms.Button();
            this.dtpDenNgay = new System.Windows.Forms.DateTimePicker();
            this.dtpTuNgay = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnThoat1 = new System.Windows.Forms.Button();
            this.lblCanNhap = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblTongMH = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.dgvPhuTungCanhBao = new System.Windows.Forms.DataGridView();
            this.grbKiemTraKPT = new System.Windows.Forms.GroupBox();
            this.btnXuatFile = new System.Windows.Forms.Button();
            this.btnLamMoi1 = new System.Windows.Forms.Button();
            this.btnLocCanhBao = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.numMucTon = new System.Windows.Forms.NumericUpDown();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvThongKeDoanhThu)).BeginInit();
            this.grbBoLocTG.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhuTungCanhBao)).BeginInit();
            this.grbKiemTraKPT.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMucTon)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1058, 664);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Controls.Add(this.dgvThongKeDoanhThu);
            this.tabPage1.Controls.Add(this.grbBoLocTG);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1050, 631);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Thống kê doanh thu";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.btnThoat);
            this.panel1.Controls.Add(this.lblTongDoanhThu);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.lblTienCong);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.lblTienPhuTung);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.lblSoXe);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Location = new System.Drawing.Point(3, 502);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1044, 126);
            this.panel1.TabIndex = 7;
            // 
            // btnThoat
            // 
            this.btnThoat.Location = new System.Drawing.Point(919, 79);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(120, 40);
            this.btnThoat.TabIndex = 6;
            this.btnThoat.Text = "&Thoát";
            this.btnThoat.UseVisualStyleBackColor = true;
            // 
            // lblTongDoanhThu
            // 
            this.lblTongDoanhThu.AutoSize = true;
            this.lblTongDoanhThu.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTongDoanhThu.Location = new System.Drawing.Point(345, 85);
            this.lblTongDoanhThu.Name = "lblTongDoanhThu";
            this.lblTongDoanhThu.Size = new System.Drawing.Size(34, 25);
            this.lblTongDoanhThu.TabIndex = 13;
            this.lblTongDoanhThu.Text = "0đ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(86, 85);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(253, 29);
            this.label7.TabIndex = 12;
            this.label7.Text = "TỔNG DOANH THU:";
            // 
            // lblTienCong
            // 
            this.lblTienCong.AutoSize = true;
            this.lblTienCong.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTienCong.Location = new System.Drawing.Point(855, 34);
            this.lblTienCong.Name = "lblTienCong";
            this.lblTienCong.Size = new System.Drawing.Size(34, 25);
            this.lblTienCong.TabIndex = 11;
            this.lblTienCong.Text = "0đ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(701, 34);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(148, 25);
            this.label6.TabIndex = 10;
            this.label6.Text = "Tổng tiền công:";
            // 
            // lblTienPhuTung
            // 
            this.lblTienPhuTung.AutoSize = true;
            this.lblTienPhuTung.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTienPhuTung.Location = new System.Drawing.Point(561, 34);
            this.lblTienPhuTung.Name = "lblTienPhuTung";
            this.lblTienPhuTung.Size = new System.Drawing.Size(34, 25);
            this.lblTienPhuTung.TabIndex = 9;
            this.lblTienPhuTung.Text = "0đ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(374, 34);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(181, 25);
            this.label5.TabIndex = 8;
            this.label5.Text = "Tổng tiền phụ tùng:";
            // 
            // lblSoXe
            // 
            this.lblSoXe.AutoSize = true;
            this.lblSoXe.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSoXe.Location = new System.Drawing.Point(225, 34);
            this.lblSoXe.Name = "lblSoXe";
            this.lblSoXe.Size = new System.Drawing.Size(49, 25);
            this.lblSoXe.TabIndex = 7;
            this.lblSoXe.Text = "0 xe";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(86, 34);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(133, 25);
            this.label4.TabIndex = 6;
            this.label4.Text = "Số xe đã sửa:";
            // 
            // dgvThongKeDoanhThu
            // 
            this.dgvThongKeDoanhThu.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvThongKeDoanhThu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvThongKeDoanhThu.Location = new System.Drawing.Point(8, 210);
            this.dgvThongKeDoanhThu.Name = "dgvThongKeDoanhThu";
            this.dgvThongKeDoanhThu.RowHeadersWidth = 62;
            this.dgvThongKeDoanhThu.RowTemplate.Height = 28;
            this.dgvThongKeDoanhThu.Size = new System.Drawing.Size(1034, 286);
            this.dgvThongKeDoanhThu.TabIndex = 6;
            // 
            // grbBoLocTG
            // 
            this.grbBoLocTG.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grbBoLocTG.Controls.Add(this.btnLamMoi);
            this.grbBoLocTG.Controls.Add(this.btnThongKe);
            this.grbBoLocTG.Controls.Add(this.dtpDenNgay);
            this.grbBoLocTG.Controls.Add(this.dtpTuNgay);
            this.grbBoLocTG.Controls.Add(this.label3);
            this.grbBoLocTG.Controls.Add(this.label2);
            this.grbBoLocTG.Location = new System.Drawing.Point(8, 6);
            this.grbBoLocTG.Name = "grbBoLocTG";
            this.grbBoLocTG.Size = new System.Drawing.Size(1034, 198);
            this.grbBoLocTG.TabIndex = 0;
            this.grbBoLocTG.TabStop = false;
            this.grbBoLocTG.Text = "Bộ lọc thời gian";
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.Location = new System.Drawing.Point(310, 138);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(120, 40);
            this.btnLamMoi.TabIndex = 5;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.UseVisualStyleBackColor = true;
            // 
            // btnThongKe
            // 
            this.btnThongKe.Location = new System.Drawing.Point(86, 138);
            this.btnThongKe.Name = "btnThongKe";
            this.btnThongKe.Size = new System.Drawing.Size(120, 40);
            this.btnThongKe.TabIndex = 4;
            this.btnThongKe.Text = "Thống kê";
            this.btnThongKe.UseVisualStyleBackColor = true;
            // 
            // dtpDenNgay
            // 
            this.dtpDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDenNgay.Location = new System.Drawing.Point(628, 54);
            this.dtpDenNgay.Name = "dtpDenNgay";
            this.dtpDenNgay.Size = new System.Drawing.Size(200, 26);
            this.dtpDenNgay.TabIndex = 3;
            // 
            // dtpTuNgay
            // 
            this.dtpTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTuNgay.Location = new System.Drawing.Point(186, 54);
            this.dtpTuNgay.Name = "dtpTuNgay";
            this.dtpTuNgay.Size = new System.Drawing.Size(200, 26);
            this.dtpTuNgay.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(510, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 25);
            this.label3.TabIndex = 1;
            this.label3.Text = "Đến ngày:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(81, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 25);
            this.label2.TabIndex = 0;
            this.label2.Text = "Từ ngày:";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.panel2);
            this.tabPage2.Controls.Add(this.dgvPhuTungCanhBao);
            this.tabPage2.Controls.Add(this.grbKiemTraKPT);
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1050, 631);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Cảnh báo tồn kho";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnThoat1
            // 
            this.btnThoat1.Location = new System.Drawing.Point(856, 46);
            this.btnThoat1.Name = "btnThoat1";
            this.btnThoat1.Size = new System.Drawing.Size(120, 40);
            this.btnThoat1.TabIndex = 8;
            this.btnThoat1.Text = "&Thoát";
            this.btnThoat1.UseVisualStyleBackColor = true;
            // 
            // lblCanNhap
            // 
            this.lblCanNhap.AutoSize = true;
            this.lblCanNhap.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCanNhap.Location = new System.Drawing.Point(620, 17);
            this.lblCanNhap.Name = "lblCanNhap";
            this.lblCanNhap.Size = new System.Drawing.Size(58, 25);
            this.lblCanNhap.TabIndex = 12;
            this.lblCanNhap.Text = "0 loại";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(339, 17);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(248, 25);
            this.label8.TabIndex = 11;
            this.label8.Text = "Số phụ tùng cần nhập gấp:";
            // 
            // lblTongMH
            // 
            this.lblTongMH.AutoSize = true;
            this.lblTongMH.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTongMH.Location = new System.Drawing.Point(224, 17);
            this.lblTongMH.Name = "lblTongMH";
            this.lblTongMH.Size = new System.Drawing.Size(58, 25);
            this.lblTongMH.TabIndex = 9;
            this.lblTongMH.Text = "0 loại";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(23, 17);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(176, 25);
            this.label9.TabIndex = 10;
            this.label9.Text = "Tổng số mặt hàng:";
            // 
            // dgvPhuTungCanhBao
            // 
            this.dgvPhuTungCanhBao.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvPhuTungCanhBao.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPhuTungCanhBao.Location = new System.Drawing.Point(6, 225);
            this.dgvPhuTungCanhBao.Name = "dgvPhuTungCanhBao";
            this.dgvPhuTungCanhBao.RowHeadersWidth = 62;
            this.dgvPhuTungCanhBao.RowTemplate.Height = 28;
            this.dgvPhuTungCanhBao.Size = new System.Drawing.Size(1041, 299);
            this.dgvPhuTungCanhBao.TabIndex = 1;
            // 
            // grbKiemTraKPT
            // 
            this.grbKiemTraKPT.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grbKiemTraKPT.Controls.Add(this.btnXuatFile);
            this.grbKiemTraKPT.Controls.Add(this.btnLamMoi1);
            this.grbKiemTraKPT.Controls.Add(this.btnLocCanhBao);
            this.grbKiemTraKPT.Controls.Add(this.label1);
            this.grbKiemTraKPT.Controls.Add(this.numMucTon);
            this.grbKiemTraKPT.Location = new System.Drawing.Point(6, 6);
            this.grbKiemTraKPT.Name = "grbKiemTraKPT";
            this.grbKiemTraKPT.Size = new System.Drawing.Size(1036, 213);
            this.grbKiemTraKPT.TabIndex = 0;
            this.grbKiemTraKPT.TabStop = false;
            this.grbKiemTraKPT.Text = "Kiểm tra tồn kho phụ tùng";
            // 
            // btnXuatFile
            // 
            this.btnXuatFile.Location = new System.Drawing.Point(490, 139);
            this.btnXuatFile.Name = "btnXuatFile";
            this.btnXuatFile.Size = new System.Drawing.Size(120, 40);
            this.btnXuatFile.TabIndex = 7;
            this.btnXuatFile.Text = "Xuất file";
            this.btnXuatFile.UseVisualStyleBackColor = true;
            // 
            // btnLamMoi1
            // 
            this.btnLamMoi1.Location = new System.Drawing.Point(255, 139);
            this.btnLamMoi1.Name = "btnLamMoi1";
            this.btnLamMoi1.Size = new System.Drawing.Size(120, 40);
            this.btnLamMoi1.TabIndex = 6;
            this.btnLamMoi1.Text = "Làm mới";
            this.btnLamMoi1.UseVisualStyleBackColor = true;
            // 
            // btnLocCanhBao
            // 
            this.btnLocCanhBao.Location = new System.Drawing.Point(25, 139);
            this.btnLocCanhBao.Name = "btnLocCanhBao";
            this.btnLocCanhBao.Size = new System.Drawing.Size(140, 40);
            this.btnLocCanhBao.TabIndex = 5;
            this.btnLocCanhBao.Text = "Lọc cảnh báo";
            this.btnLocCanhBao.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(20, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(217, 25);
            this.label1.TabIndex = 4;
            this.label1.Text = "Mức tồn cảnh báo (<=):";
            // 
            // numMucTon
            // 
            this.numMucTon.Location = new System.Drawing.Point(267, 53);
            this.numMucTon.Name = "numMucTon";
            this.numMucTon.Size = new System.Drawing.Size(116, 26);
            this.numMucTon.TabIndex = 2;
            this.numMucTon.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.btnThoat1);
            this.panel2.Controls.Add(this.lblTongMH);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.lblCanNhap);
            this.panel2.Location = new System.Drawing.Point(8, 528);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1034, 100);
            this.panel2.TabIndex = 13;
            // 
            // frmBaoCaoThongKe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1058, 664);
            this.Controls.Add(this.tabControl1);
            this.Name = "frmBaoCaoThongKe";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvThongKeDoanhThu)).EndInit();
            this.grbBoLocTG.ResumeLayout(false);
            this.grbBoLocTG.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhuTungCanhBao)).EndInit();
            this.grbKiemTraKPT.ResumeLayout(false);
            this.grbKiemTraKPT.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMucTon)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox grbBoLocTG;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpDenNgay;
        private System.Windows.Forms.DateTimePicker dtpTuNgay;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnThongKe;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvThongKeDoanhThu;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblTienCong;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblTienPhuTung;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblSoXe;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblTongDoanhThu;
        private System.Windows.Forms.DataGridView dgvPhuTungCanhBao;
        private System.Windows.Forms.GroupBox grbKiemTraKPT;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.NumericUpDown numMucTon;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnLamMoi1;
        private System.Windows.Forms.Button btnLocCanhBao;
        private System.Windows.Forms.Button btnThoat1;
        private System.Windows.Forms.Button btnXuatFile;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblTongMH;
        private System.Windows.Forms.Label lblCanNhap;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel2;
    }
}

