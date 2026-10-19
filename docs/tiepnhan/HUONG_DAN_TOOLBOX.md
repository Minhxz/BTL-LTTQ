# Toolbox của form tiếp nhận

Chọn `frmTiepNhan.cs`, nhấn **Shift + F7** để mở Designer. **Ctrl + Alt + X** mở Toolbox; **F4** mở Properties. `(Name)` là tên dùng trong code, `Text` là chữ hiển thị.

| Control | Tên trong code | Công dụng |
|---|---|---|
| `TextBox` | `txtTimKiem` | Nhập mã phiếu, biển số hoặc tên khách cần tìm |
| `Button` | `btnTimKiem` | Tìm phiếu; cũng gọi bằng Enter trong ô tìm |
| `TextBox` | `txtMaPhieu` | Mã tự hiện khi bấm Thêm mới, chỉ xem |
| `TextBox` | `txtNgayNhan` | Ngày giờ nhận tự điền, chỉ xem |
| `TextBox` | `txtBienSoXe` | Biển số bắt buộc, tối đa 20 ký tự |
| `ComboBox` | `cboLoaiXe` | Xe số, Xe ga, Xe côn tay |
| `TextBox` | `txtKhachHang` | Tên khách, tối đa 100 ký tự |
| `TextBox` | `txtDienThoai` | Điện thoại, có thể bỏ trống |
| `ComboBox` | `cboThoPhuTrach` | Tên thợ mẫu hoặc Chưa phân công |
| `ComboBox` | `cboTrangThai` | Chờ khám, Đang sửa, Hoàn thành |
| `RichTextBox` | `rtbTinhTrangXe` | Ghi lỗi ban đầu/yêu cầu của khách |
| `Button` | `btnThemMoi`, `btnSua`, `btnXoa`, `btnLuu` | Các thao tác với phiếu |
| `Button` | `btnThoat` | Đóng form tiếp nhận để quay về màn hình chính |
| `Label` | `lblThongBao` | Hướng dẫn, kết quả hoặc lỗi nhập liệu |
| `DataGridView` | `dgvPhieu` | Danh sách phiếu; chọn dòng để đưa lên các ô nhập |

## Bảng phía dưới

Đây là control **DataGridView** chuẩn của Windows Forms. Các thuộc tính chính:

- `ReadOnly = true`: không sửa trực tiếp trong ô bảng.
- `SelectionMode = FullRowSelect`: bấm ô sẽ chọn cả dòng.
- `MultiSelect = false`: mỗi lần chọn một phiếu.
- `AutoGenerateColumns = false`: dùng bảy cột đã đặt trong Designer.
- `DataSource`: nhận dữ liệu từ DataTable tạm của form.

Bảy cột hiển thị gồm mã phiếu, ngày nhận, biển số, khách hàng, loại xe, thợ và trạng thái. Mỗi cột có `DataPropertyName` trùng tên cột dữ liệu để hiển thị đúng giá trị.

## Ô chọn và sự kiện

ComboBox dùng `DropDownList` để chọn trong danh sách. Ô thợ dùng `Items` chứa các tên mẫu, chưa đọc từ CSDL.

Các khung `TableLayoutPanel` chia giao diện thành hàng và cột, giúp các ô thẳng hàng khi đổi kích thước cửa sổ. Đây là control có sẵn trong Toolbox; chỉ dùng để sắp xếp giao diện, không xử lý dữ liệu.

File `.Designer.cs` nối các nút với sự kiện `Click`; nội dung xử lý nằm trong `frmTiepNhan.cs`. Khi đổi `(Name)`, kiểm tra cả nơi dùng tên đó trong code. Đặt `TabIndex` theo thứ tự nhập để di chuyển bằng Tab.

Nút **Lưu** hiện hộp thoại bằng `MessageBox.Show(...)` khi lưu mới hoặc sửa thành công. `MessageBox` được gọi trong code, không cần kéo thêm control từ Toolbox. Nút **Thoát** nối với `btnThoat_Click` và gọi `Close()`; nút này luôn được bật.

Nhấn **F5** để thử trên form thật. Phần tiếp nhận hiện không cần CSDL; đóng form rồi mở lại sẽ nạp dữ liệu mẫu ban đầu.
