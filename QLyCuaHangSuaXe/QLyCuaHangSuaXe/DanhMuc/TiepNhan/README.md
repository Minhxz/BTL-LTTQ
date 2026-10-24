# Tiếp nhận xe

Form nằm trong project `QLyCuaHangSuaXe`, namespace `QLyCuaHangSuaXe.DanhMuc.TiepNhan`. Giao diện xanh trắng gồm hai nhóm **Khách hàng và xe**, **Thông tin tiếp nhận**, các nút thao tác và danh sách phiếu bên dưới.

## Chạy thử

1. Mở project chính trong Visual Studio, nhấn **F5**.
2. Trên màn hình chính, chọn **Tiếp nhận xe**.
3. Thử thao tác với dữ liệu mẫu. Phần tiếp nhận không cần tạo hoặc cấu hình CSDL.

`DataTable bangPhieu` giữ dữ liệu tạm trong bộ nhớ, chưa kết nối SQL Server. Mỗi lần mở form có bốn phiếu mẫu: `TN-0002` quá hẹn và `TN-0004` đã hoàn thành để thử màu/cảnh báo. **Lưu** cập nhật bảng tạm; đóng rồi mở lại form sẽ trở về dữ liệu mẫu.

## Thao tác trên form

- **Tìm kiếm:** nhập mã phiếu, biển số, tên khách hoặc điện thoại rồi bấm **Tìm kiếm** hoặc Enter. Kết quả phải đồng thời khớp từ khóa và trạng thái đang lọc. Đổi trạng thái lọc sẽ cập nhật danh sách ngay; chọn **Tất cả** và xóa từ khóa để xem toàn bộ.
- **Thêm mới:** làm trống ô nhập, tự điền ngày nhận và mã như `TN-0005`. Nhập thông tin rồi bấm **Lưu**. Lưu thành công mới tăng số phiếu; nhập sai chưa tạo phiếu mới.
- **Sửa:** chọn dòng, bấm **Sửa**, thay thông tin rồi **Lưu**. Mã phiếu và ngày nhận giữ nguyên. Sau khi lưu mới hoặc sửa, từ khóa được xóa và bộ lọc về **Tất cả** để chọn lại phiếu vừa lưu.
- **Xóa:** chọn phiếu, bấm **Xóa** và xác nhận trước khi bỏ khỏi bảng tạm.
- **In phiếu:** chọn phiếu đã lưu rồi bấm **In phiếu** để mở `PrintPreviewDialog`. Người dùng xem trước và tự bấm in; nút này không tự gửi phiếu đến máy in. Khi bấm in trong cửa sổ xem trước, phiếu được gửi đến máy in mặc định của Windows.
- **Thoát:** đóng form tiếp nhận để quay về màn hình chính. Các thay đổi trong bảng tạm sẽ mất khi đóng, kể cả đã bấm Lưu.

Biển số bắt buộc. Điện thoại được bỏ trống; nếu nhập phải có 8–15 chữ số. Có thêm dòng xe, số km và ngày giờ hẹn trả. Hẹn trả phải bằng hoặc sau ngày nhận. Phiếu **Chờ khám** có thể chưa phân công thợ; **Đang sửa** hoặc **Hoàn thành** cần chọn thợ. Danh sách thợ hiện là tên mẫu.

## Nhắc hẹn và danh sách

Lưới có tám cột: mã phiếu, ngày nhận, biển số, khách hàng, thợ, hẹn trả, trạng thái và nhắc hẹn. Nhãn tổng cho biết số phiếu đang hiển thị; chú thích dưới bảng giải thích màu dòng.

Phiếu quá hẹn khi `HenTra < DateTime.Now` và trạng thái chưa **Hoàn thành**, kể cả **Chờ khám**. Dòng quá hẹn có nền đỏ nhạt; dòng hoàn thành xanh nhạt; các dòng còn lại trắng. Khi chọn phiếu quá hẹn, `lblCanhBao` hiện cảnh báo riêng.

`timerNhacHen` cập nhật nhắc hẹn mỗi 30 giây. Khi đang thêm hoặc sửa, việc cập nhật không ghi đè các ô đang nhập. Phiếu hoàn thành luôn được loại khỏi cảnh báo quá hẹn.

## Đọc code và sửa giao diện

- `frmTiepNhan.cs`: bảng tạm, các hàm hỗ trợ ngắn và xử lý sự kiện. Cách viết trực tiếp giống bài `Hoc_vien`; bài mẫu `D:\LTTQ\Hoc_vien` được giữ nguyên.
- `frmTiepNhan.Designer.cs`: tạo control, bố trí giao diện và nối sự kiện.
- `frmTiepNhan.resx`: tài nguyên của form.

`frmTiepNhan_Load` tạo bảng và dữ liệu mẫu. `MaPhieu` là khóa; sửa hoặc xóa tìm dòng bằng `bangPhieu.Rows.Find(txtMaPhieu.Text)`. Sự kiện chọn dòng đưa dữ liệu lên ô nhập. `btnLuu_Click` kiểm tra ngay bằng `if`, `MessageBox.Show`, `Focus` và `return`, rồi mới cập nhật bảng.

`CapNhatNhacHen()` cập nhật nội dung nhắc hẹn; `dgvPhieu_CellFormatting` tô màu dòng. `btnInPhieu_Click` mở xem trước, `inPhieu_BeginPrint` lấy dữ liệu đã lưu và `inPhieu_PrintPage` vẽ phiếu A4, chia trang nếu nội dung dài. Các xử lý vẫn nằm trong form, chưa cần lớp xử lý SQL riêng.

Chọn `frmTiepNhan.cs`, nhấn **Shift + F7** để mở Designer. Xem thêm [giải thích code](../../../../docs/tiepnhan/HUONG_DAN_GIAI_THICH_CODE.md) và [Toolbox](../../../../docs/tiepnhan/HUONG_DAN_TOOLBOX.md).

Khi nhóm có CSDL chung, form hóa đơn có thể đọc phiếu theo mã để lấy thông tin khách/xe rồi xử lý phụ tùng, tiền công và thanh toán. Hiện phần tiếp nhận chưa lưu lâu dài hoặc chuyển dữ liệu sang form hóa đơn.
