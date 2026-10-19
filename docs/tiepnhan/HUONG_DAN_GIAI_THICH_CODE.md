# Hiểu code tiếp nhận xe

Code xử lý nằm trong `frmTiepNhan.cs`. Có thể đọc theo luồng: **bấm nút → kiểm tra ô nhập → cập nhật dữ liệu tạm → hiện lại DataGridView**.

## DataGridView và DataTable khác nhau thế nào?

`dgvPhieu` là control **DataGridView** kéo từ Toolbox, dùng để hiển thị và chọn phiếu. `DataTable` là bảng giữ dữ liệu trong bộ nhớ. Gán `DataSource` giúp DataGridView hiển thị các dòng của DataTable.

Bảng dữ liệu có các cột: `MaPhieu`, `NgayNhan`, `BienSoXe`, `KhachHang`, `DienThoai`, `LoaiXe`, `TenTho`, `TrangThai`, `TinhTrangXe`. Giao diện chỉ hiện bảy cột chính. Điện thoại và tình trạng xe được đưa lên ô nhập khi chọn một dòng.

Phần này chưa dùng CSDL. Nút **Lưu** chỉ cập nhật bảng trong bộ nhớ; đóng form sẽ mất thay đổi. Danh sách thợ hiện là các tên mẫu trong ComboBox.

## Đọc các hàm theo thứ tự

| Hàm | Công việc |
|---|---|
| `TaoBangPhieu()` | Tạo các cột và dữ liệu mẫu cho bảng tạm |
| `TaiDanhSach(...)` | Đưa danh sách phiếu lên DataGridView |
| `HienThiPhieu()` | Đưa thông tin phiếu đang chọn lên các ô |
| `DatCheDo(...)` | Mở/khóa ô nhập và bật/tắt nút khi xem, thêm hoặc sửa |
| `TaoPhieuMoi()` | Làm trống ô nhập và hiện ngay mã phiếu tiếp theo |
| `KiemTraThongTin()` | Kiểm tra ô nhập trước khi lưu |
| `LuuPhieu()` | Thêm dòng mới hoặc sửa dòng đã chọn rồi cập nhật bảng |

Các hàm `btn..._Click` xử lý khi bấm nút. Ví dụ, **Lưu** gọi `LuuPhieu()`; **Tìm kiếm** đọc từ khóa rồi gọi hàm tải danh sách. Tìm kiếm chỉ kiểm tra mã phiếu, biển số và tên khách.

**Mã phiếu tự động:** bấm **Thêm mới** sẽ hiện ngay mã như `TN-0005`. **Lưu** giữ đúng mã đang hiển thị rồi tăng số cho phiếu tiếp theo. Nhập sai chưa làm tăng số; khi sửa thì mã cũ giữ nguyên.

**Thông báo thành công:** `LuuPhieu()` dùng `MessageBox.Show(...)` sau khi dữ liệu hợp lệ và đã được cập nhật. Lưu mới báo **Lưu phiếu thành công.**; lưu sau khi sửa báo **Sửa phiếu thành công.** Bấm Sửa chỉ chuyển sang chế độ nhập, chưa thay dữ liệu nên chưa báo thành công.

**Thoát form:** sự kiện `btnThoat_Click` gọi `Close()` để đóng cửa sổ tiếp nhận. Màn hình chính vẫn mở để chọn chức năng khác. Nút Thoát luôn dùng được; dữ liệu tạm của form sẽ mất khi đóng, kể cả các phiếu vừa bấm Lưu.

`phieuDangChon` giữ dòng đang xem để sửa/xóa đúng phiếu. `dangThem` và `dangSua` cho biết chế độ hiện tại. `dangNapDuLieu` tránh xử lý chọn dòng khi chương trình đang gán lại danh sách cho DataGridView.

## Những thao tác dữ liệu cần hiểu

- Thêm: tạo một `DataRow`, gán giá trị từng cột rồi thêm vào bảng.
- Sửa: lấy dòng đang chọn rồi gán lại các cột cần thay.
- Xóa: bỏ dòng đã chọn sau khi người dùng xác nhận.
- Tìm kiếm: dùng vòng lặp kiểm tra từng phiếu, đưa các dòng phù hợp lên bảng.

Khi bấm một dòng trong DataGridView, thông tin của dòng đó xuất hiện ở các ô phía trên. Muốn sửa thì bấm **Sửa**; bảng không cho sửa trực tiếp trong ô.

Biển số không được trống. Điện thoại có thể bỏ qua; nếu nhập thì phải có 8–15 chữ số. Các ô chọn dùng danh sách có sẵn để hạn chế nhập sai. Phiếu **Chờ khám** chưa cần thợ; **Đang sửa** hoặc **Hoàn thành** phải chọn thợ.

## Tự chạy từng bước

Đặt điểm dừng trong sự kiện nút **Lưu**. Nhấn F10 để theo dõi kiểm tra thông tin, cập nhật dòng, hiển thị lại danh sách và báo thành công. Thử thêm phiếu, sửa tên khách, tìm theo biển số rồi bấm **Thoát**. Mở lại form để thấy dữ liệu mẫu được nạp lại.

## Sau này nối với hóa đơn

Khi có CSDL chung, form hóa đơn sẽ đọc lại phiếu tiếp nhận theo mã phiếu và lấy thông tin khách/xe. Người làm form hóa đơn sẽ xử lý phụ tùng, tiền công và thanh toán. Phần tiếp nhận hiện chỉ minh họa thao tác bằng DataTable; chưa lưu lâu dài hoặc chuyển dữ liệu sang form hóa đơn.
