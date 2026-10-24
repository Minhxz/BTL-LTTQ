# Hiểu code tiếp nhận xe

Code nằm trong `frmTiepNhan.cs`, viết trực tiếp trong sự kiện như bài `Hoc_vien`: **bấm nút → kiểm tra ô nhập → cập nhật bảng tạm → hiện lại danh sách**. Bài mẫu `D:\LTTQ\Hoc_vien` được giữ nguyên.

## Dữ liệu trong form

| Biến | Công dụng |
|---|---|
| `DataTable bangPhieu` | Giữ các phiếu trong bộ nhớ |
| `int soPhieuTiepTheo` | Tạo mã mới, bắt đầu từ 5 sau bốn phiếu mẫu |
| `DateTime ngayNhan` | Giữ ngày giờ nhận của phiếu |
| `bool dangThem` | Phân biệt lưu mới với sửa |
| `bool dangTaiDanhSach` | Đợi lưới tải xong rồi mới đọc dòng đang chọn |
| `string noiDungIn` | Giữ nội dung của phiếu đã lưu để in |
| `int viTriIn` | Nhớ phần nội dung đã vẽ khi phiếu có nhiều trang |

`frmTiepNhan_Load` tạo cột, đặt `MaPhieu` làm khóa, thêm bốn phiếu và tên thợ mẫu. Ngoài thông tin khách/xe, bảng có `DongXe` kiểu chuỗi, `SoKm` kiểu số nguyên, `HenTra` kiểu `DateTime` và `NhacHen` kiểu chuỗi.

`dgvPhieu` là DataGridView hiển thị tám cột: `MaPhieu`, `NgayNhan`, `BienSoXe`, `KhachHang`, `TenTho`, `HenTra`, `TrangThai`, `NhacHen`. Các trường khác vẫn nằm trong bảng tạm và được đưa lên ô nhập khi chọn phiếu. Để sửa hoặc xóa đúng dòng, dùng mã đang hiện:

```csharp
DataRow dong = bangPhieu.Rows.Find(txtMaPhieu.Text);
```

**Lưu** chỉ cập nhật `bangPhieu`; đóng rồi mở lại form sẽ trở về dữ liệu mẫu.

## Thêm, sửa và kiểm tra dữ liệu

**Thêm mới** đặt `dangThem = true`, xóa trắng ô nhập, tự điền mã và ngày nhận. **Sửa** đặt `dangThem = false`, mở các ô của phiếu đang chọn; mã và ngày nhận giữ nguyên.

`btnLuu_Click` kiểm tra trực tiếp từng trường bằng `if`. Có lỗi thì dùng `MessageBox.Show`, gọi `Focus` về ô cần sửa rồi `return`. Biển số bắt buộc; điện thoại có thể trống nhưng nếu nhập phải có 8–15 chữ số. Hẹn trả từ `dtpHenTra` không được trước `ngayNhan`. **Đang sửa** hoặc **Hoàn thành** cần phân công thợ.

Hợp lệ thì tạo `DataRow` mới hoặc tìm dòng cũ, gán từng cột rồi tải lại danh sách. Chỉ thêm thành công mới tăng `soPhieuTiepTheo`. Sau khi lưu, xóa từ khóa, đưa lọc trạng thái về **Tất cả** và chọn lại phiếu vừa lưu. Thêm mới báo **Lưu phiếu thành công.**; sửa báo **Sửa phiếu thành công.**

**Xóa** hỏi xác nhận rồi bỏ dòng khỏi bảng. **Thoát** gọi `Close()`; bảng tạm mất khi form đóng.

## Tìm kiếm và chọn dòng

`TaiDanhSach(string maCanChon)` tạo bảng kết quả từ `bangPhieu.Clone()`, dùng `foreach` và `Contains` để tìm mã phiếu, biển số, tên khách hoặc điện thoại. Điều kiện từ khóa kết hợp với trạng thái bằng **và**: phiếu phải khớp cả hai. `cboLocTrangThai` đổi lựa chọn sẽ gọi lọc ngay; từ khóa được tìm như nội dung bình thường.

Sự kiện `dgvPhieu_CurrentCellChanged` đưa thông tin dòng đang chọn lên form. Sự kiện bỏ qua lúc đang nhập để tránh ghi đè, hoặc lúc `dangTaiDanhSach` đang bật để không đọc nhầm dòng trong khi đổi bảng. Tải xong mới hiển thị dòng được chọn. `XoaTrangForm()` làm trống các ô; `DatCheDo(bool choNhap)` mở/khóa ô nhập và bật/tắt nút tương ứng. Dòng hướng dẫn/kết quả được gán vào `lblThongBao.Text`.

## Nhắc hẹn

`CapNhatNhacHen()` xét từng phiếu theo điều kiện quá hẹn:

```csharp
HenTra < DateTime.Now && TrangThai != "Hoàn thành"
```

Điều kiện này áp dụng cả phiếu **Chờ khám**. `NhacHen` nhận một trong ba nội dung **Quá hẹn**, **Đã xong**, **Đúng hẹn**. `dgvPhieu_CellFormatting` dựa vào đó để tô dòng quá hẹn đỏ nhạt, hoàn thành xanh nhạt, còn lại trắng. `lblCanhBao` hiển thị cảnh báo khi phiếu đang chọn bị quá hẹn; `lblTongPhieu` cho biết số phiếu hiển thị/tổng số phiếu và `lblChuThich` giải thích màu.

`timerNhacHen_Tick` gọi `CapNhatNhacHen()` mỗi 30 giây. Hàm cập nhật bảng gốc và bảng kết quả đang hiển thị, không tải lại lưới hoặc ghi đè các ô đang nhập. Phiếu **Hoàn thành** không bị đánh dấu quá hẹn. Dữ liệu mẫu dùng thời gian tương đối khi mở form: `TN-0002` quá hẹn, `TN-0004` đã hoàn thành.

## In phiếu

`btnInPhieu_Click` tạo `PrintDocument` khổ A4 và mở `PrintPreviewDialog` bằng `ShowDialog`. Nút in bị khóa khi đang thêm hoặc sửa. `inPhieu_BeginPrint` lấy thông tin phiếu đã lưu vào `noiDungIn` và đặt `viTriIn = 0`. `inPhieu_PrintPage` vẽ nội dung; nếu dài thì đặt `HasMorePages` để in tiếp trang sau.

Người dùng xem trước và tự bấm in từ cửa sổ này; chương trình không tự gửi lệnh in khi bấm **In phiếu**. Lệnh in trong cửa sổ xem trước dùng máy in mặc định của Windows.

## Tự chạy từng bước

Đặt điểm dừng ở `btnLuu_Click`, nhấn F10 để theo dõi kiểm tra ô nhập và cập nhật bảng. Thử thêm dòng xe, số km, hẹn trả; thử hẹn trước ngày nhận để thấy kiểm tra. Tìm theo điện thoại kết hợp lọc trạng thái, chọn phiếu quá hẹn để xem cảnh báo, đổi sang **Hoàn thành** rồi lưu để thấy hết cảnh báo. Bấm **In phiếu** để xem trước và có thể đóng mà không in. Đóng rồi mở lại form để thấy bốn phiếu mẫu được nạp lại.

Phần tiếp nhận chưa cần SQL Server. Khi có CSDL chung, form hóa đơn sẽ đọc lại phiếu theo mã và xử lý phụ tùng, tiền công, thanh toán; hiện chưa có thao tác chuyển dữ liệu thật sang hóa đơn.
