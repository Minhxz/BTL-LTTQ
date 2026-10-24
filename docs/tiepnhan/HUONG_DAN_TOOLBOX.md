# Toolbox của form tiếp nhận

Chọn `frmTiepNhan.cs`, nhấn **Shift + F7** để mở Designer. **Ctrl + Alt + X** mở Toolbox; **F4** mở Properties. `(Name)` là tên dùng trong code, `Text` là chữ hiển thị.

Giao diện xanh trắng chia thành hai nhóm **Khách hàng và xe** và **Thông tin tiếp nhận**. Khu tìm kiếm có từ khóa và trạng thái; các nút thao tác, cảnh báo và lưới nằm phía dưới.

| Control | Tên trong code | Công dụng |
|---|---|---|
| `GroupBox` | `grbKhachHangXe` | Gom thông tin khách hàng và xe |
| `GroupBox` | `grbTiepNhan` | Gom thông tin tiếp nhận, thợ và hẹn trả |
| `TextBox` | `txtTimKiem` | Tìm mã phiếu, biển số, tên khách hoặc điện thoại |
| `ComboBox` | `cboLocTrangThai` | Lọc Tất cả, Chờ khám, Đang sửa, Hoàn thành; đổi lựa chọn lọc ngay |
| `Button` | `btnTimKiem` | Tìm theo từ khóa và trạng thái; cũng gọi bằng Enter |
| `TextBox` | `txtMaPhieu` | Mã tự hiện khi bấm Thêm mới, chỉ xem |
| `TextBox` | `txtNgayNhan` | Ngày giờ nhận tự điền, chỉ xem |
| `TextBox` | `txtKhachHang` | Nhập tên khách |
| `TextBox` | `txtDienThoai` | Nhập điện thoại, được bỏ trống |
| `TextBox` | `txtBienSoXe` | Biển số bắt buộc |
| `ComboBox` | `cboLoaiXe` | Chọn loại xe |
| `TextBox` | `txtDongXe` | Nhập dòng xe |
| `NumericUpDown` | `numSoKm` | Nhập số km dạng số nguyên |
| `ComboBox` | `cboThoPhuTrach` | Chọn tên thợ mẫu hoặc Chưa phân công |
| `ComboBox` | `cboTrangThai` | Trạng thái của phiếu đang nhập |
| `DateTimePicker` | `dtpHenTra` | Chọn ngày giờ hẹn trả, không trước ngày nhận |
| `RichTextBox` | `rtbTinhTrangXe` | Ghi tình trạng xe hoặc yêu cầu của khách |
| `Button` | `btnThemMoi`, `btnSua`, `btnXoa`, `btnLuu` | Thêm, sửa, xóa và lưu phiếu |
| `Button` | `btnInPhieu` | Mở cửa sổ xem trước phiếu đã lưu |
| `Button` | `btnThoat` | Đóng form tiếp nhận |
| `Label` | `lblThongBao` | Hướng dẫn hoặc kết quả thao tác |
| `Label` | `lblCanhBao` | Cảnh báo phiếu đang chọn bị quá hẹn |
| `Label` | `lblTongPhieu` | Số phiếu đang hiển thị |
| `Label` | `lblChuThich` | Giải thích màu của các dòng |
| `DataGridView` | `dgvPhieu` | Hiển thị và chọn phiếu |
| `Timer` | `timerNhacHen` | Cập nhật nhắc hẹn mỗi 30 giây, `Interval = 30000` |

`Timer` là thành phần không hiện trên form khi chạy. `PrintPreviewDialog` và `PrintDocument` được tạo trong `btnInPhieu_Click`, phục vụ xem trước/vẽ phiếu in, không cần kéo thêm lên form. `PrintDocument` dùng khổ A4 và có thể chia nhiều trang nếu nội dung dài.

## Bảng phía dưới

`dgvPhieu` là DataGridView chuẩn của Windows Forms. Các thuộc tính chính:

- `ReadOnly = true`: không sửa trực tiếp trong bảng.
- `SelectionMode = FullRowSelect`: bấm ô sẽ chọn cả dòng.
- `MultiSelect = false`: mỗi lần chọn một phiếu.
- `AutoGenerateColumns = false`: dùng tám cột đã đặt trong Designer.
- `DataSource`: nhận bảng kết quả lọc từ DataTable tạm.

| Dữ liệu | Nội dung hiển thị |
|---|---|
| `MaPhieu` | Mã phiếu |
| `NgayNhan` | Ngày nhận |
| `BienSoXe` | Biển số xe |
| `KhachHang` | Khách hàng |
| `TenTho` | Thợ phụ trách |
| `HenTra` | Hẹn trả |
| `TrangThai` | Trạng thái |
| `NhacHen` | Nhắc hẹn |

`DataPropertyName` của mỗi cột trùng tên cột trong bảng. Loại xe, dòng xe, số km, điện thoại và tình trạng xe vẫn có trong bảng tạm nhưng được xem ở các ô phía trên.

Dòng quá hẹn có nền đỏ nhạt; hoàn thành xanh nhạt; các dòng còn lại trắng. Điều kiện quá hẹn là đã qua giờ hẹn trả và chưa hoàn thành, kể cả đang **Chờ khám**. `lblCanhBao` hiện cảnh báo của phiếu đang chọn. Bộ hẹn giờ không ghi đè các ô đang thêm hoặc sửa.

## Ô chọn, bố cục và sự kiện

ComboBox dùng `DropDownList` để chọn trong danh sách. `cboTrangThai` sửa trạng thái của phiếu; `cboLocTrangThai` chỉ lọc danh sách. Tên thợ là dữ liệu mẫu, chưa đọc CSDL. `NumericUpDown` giới hạn số km ở dạng số nguyên; `DateTimePicker` chọn cả ngày và giờ hẹn trả.

Các khung bố cục chia giao diện thành hàng, cột và hai nhóm thông tin. Đây là control có sẵn, dùng để sắp xếp ô nhập. File `.Designer.cs` nối các sự kiện; nội dung xử lý nằm trong `frmTiepNhan.cs`. Khi đổi `(Name)`, kiểm tra các nơi dùng tên đó. Đặt `TabIndex` theo thứ tự nhập để di chuyển bằng Tab.

**Lưu** kiểm tra trực tiếp bằng `if`, hiện `MessageBox.Show`, gọi `Focus` và `return` khi có lỗi. **In phiếu** mở `PrintPreviewDialog`; người dùng tự bấm in hoặc đóng cửa sổ xem trước. Cửa sổ này gửi lệnh in đến máy in mặc định của Windows khi người dùng bấm in. **Thoát** gọi `Close()` và luôn dùng được.

Nhấn **F5** để thử form thật. Dữ liệu hiện giữ bằng `DataTable`; đóng rồi mở lại form sẽ nạp lại các phiếu mẫu.
