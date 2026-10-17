# Toolbox của form tiếp nhận

Trong Visual Studio, chọn frmTiepNhan.cs rồi nhấn Shift + F7 để mở Designer. Ctrl + Alt + X mở Toolbox; F4 mở Properties. (Name) là tên dùng trong C#, Text là chữ hiển thị. Các control đều là thành phần Windows Forms tiêu chuẩn.

Bản này chỉ có một form. Khách, xe, phụ tùng và dịch vụ đều nhập trực tiếp. Danh sách phiếu đã lưu nằm trên cùng, không mở thêm form phụ. Các ô tiền chỉ đọc; các nút Click đã được gắn trong Designer.

| Control | (Name) | Text hoặc tiêu đề |
|---|---|---|
| Panel | pnlNoiDung |  |
| Panel | pnlBieuMau |  |
| Label | lblTieuDe | TIẾP NHẬN VÀ SỬA CHỮA XE |
| Panel | pnlPhieuDaLuu |  |
| Label | lblPhieuDaLuu | Phiếu đã lưu: |
| ComboBox | cboPhieuDaLuu |  |
| Button | btnMoPhieu | Mở phiếu |
| GroupBox | grpThongTinPhieu | Thông tin phiếu |
| Label | lblMaPhieu | Mã phiếu: |
| TextBox | txtMaPhieu |  |
| Label | lblNgayNhan | Ngày nhận: |
| DateTimePicker | dtpNgayNhan |  |
| Label | lblNguoiTiepNhan | Người tiếp nhận: |
| TextBox | txtNguoiTiepNhan |  |
| Label | lblTrangThai | Trạng thái: |
| ComboBox | cboTrangThai |  |
| GroupBox | grpKhachHangXe | Khách hàng và xe |
| Label | lblKhachHang | Khách hàng: |
| TextBox | txtKhachHang |  |
| Label | lblDienThoai | Điện thoại: |
| TextBox | txtDienThoai |  |
| Label | lblBienSoXe | Biển số xe: |
| TextBox | txtBienSoXe |  |
| Label | lblDongXe | Dòng xe: |
| TextBox | txtDongXe |  |
| Label | lblSoKm | Số km: |
| NumericUpDown | nudSoKm |  |
| Label | lblDonViKm | km |
| GroupBox | grpThongTinSuaChua | Thông tin sửa chữa |
| Label | lblTinhTrangXe | Tình trạng xe / lỗi ban đầu: |
| TextBox | txtTinhTrangXe |  |
| Label | lblThoPhuTrach | Thợ phụ trách: |
| TextBox | txtThoPhuTrach |  |
| Label | lblHenTraXe | Hẹn trả xe: |
| DateTimePicker | dtpHenTraXe |  |
| GroupBox | grpPhuTung | Phụ tùng thay thế |
| Label | lblTenPhuTung | Tên phụ tùng: |
| TextBox | txtTenPhuTung |  |
| Label | lblDonViTinh | ĐVT: |
| TextBox | txtDonViTinh |  |
| Label | lblSoLuongPhuTung | SL: |
| NumericUpDown | nudSoLuongPhuTung |  |
| Label | lblDonGiaPhuTung | Đơn giá (đ): |
| NumericUpDown | nudDonGiaPhuTung |  |
| Button | btnThemPhuTung | Thêm |
| Button | btnXoaPhuTung | Xóa dòng |
| DataGridView | dgvPhuTung |  |
| DataGridViewTextBoxColumn | colMaPhuTung | Mã PT |
| DataGridViewTextBoxColumn | colTenPhuTung | Tên phụ tùng |
| DataGridViewTextBoxColumn | colDonViTinh | ĐVT |
| DataGridViewTextBoxColumn | colSoLuongPhuTung | SL |
| DataGridViewTextBoxColumn | colDonGiaPhuTung | Đơn giá |
| DataGridViewTextBoxColumn | colThanhTienPhuTung | Thành tiền |
| GroupBox | grpDichVu | Dịch vụ / Tiền công |
| Label | lblTenDichVu | Tên dịch vụ: |
| TextBox | txtTenDichVu |  |
| Label | lblSoLuongDichVu | SL: |
| NumericUpDown | nudSoLuongDichVu |  |
| Label | lblDonGiaDichVu | Đơn giá (đ): |
| NumericUpDown | nudDonGiaDichVu |  |
| Button | btnThemDichVu | Thêm |
| Button | btnXoaDichVu | Xóa dòng |
| DataGridView | dgvDichVu |  |
| DataGridViewTextBoxColumn | colMaDichVu | Mã DV |
| DataGridViewTextBoxColumn | colTenDichVu | Tên dịch vụ |
| DataGridViewTextBoxColumn | colSoLuongDichVu | SL |
| DataGridViewTextBoxColumn | colDonGiaDichVu | Đơn giá |
| DataGridViewTextBoxColumn | colThanhTienDichVu | Thành tiền |
| GroupBox | grpNoiDungSuaChua | Nội dung sửa chữa |
| TextBox | txtNoiDungSuaChua |  |
| Panel | pnlTongTien |  |
| Label | lblTienPhuTung | Tiền phụ tùng: |
| TextBox | txtTienPhuTung |  |
| Label | lblTienCong | Tiền công: |
| TextBox | txtTienCong |  |
| Label | lblTongCong | TỔNG CỘNG: |
| TextBox | txtTongCong |  |
| Button | btnPhieuMoi | Phiếu mới |
| Button | btnLuuPhieu | Lưu phiếu |
| Button | btnChuyenThanhToan | Chuyển thanh toán |
| Button | btnDong | Đóng |

Số lượng từ 1 đến 999, đơn giá nhập bằng NumericUpDown và phải lớn hơn 0 khi thêm. Hai bảng chỉ cho chọn dòng để xóa, không cho gõ trực tiếp vào ô. Ngày dùng định dạng dd/MM/yyyy HH:mm. Tổng tiền tính từ DataTable, không nhập thủ công.
