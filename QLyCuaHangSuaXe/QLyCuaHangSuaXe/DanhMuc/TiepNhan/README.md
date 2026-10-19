# Tiếp nhận xe

Form nằm trong project `QLyCuaHangSuaXe`, namespace `QLyCuaHangSuaXe.DanhMuc.TiepNhan`. Bố cục gồm tìm kiếm phía trên, thông tin tiếp nhận ở giữa, các nút thao tác và danh sách phiếu ở dưới.

## Chạy thử

1. Mở project chính trong Visual Studio, nhấn **F5**.
2. Trên màn hình chính, chọn **Tiếp nhận xe**.
3. Thử thao tác với dữ liệu mẫu. Phần tiếp nhận không cần tạo hoặc cấu hình CSDL.

Bảng phía dưới là control **DataGridView** thật, tên `dgvPhieu`. Một `DataTable` trong form giữ dữ liệu tạm để bảng hiển thị. **Lưu** cập nhật dữ liệu trong bộ nhớ; đóng form rồi mở lại sẽ trở về dữ liệu mẫu. Nhóm sẽ thống nhất CSDL và làm phần lưu dữ liệu lâu dài sau.

## Các nút làm gì?

- **Tìm kiếm:** nhập mã phiếu, biển số hoặc tên khách rồi bấm **Tìm kiếm** hoặc Enter. Xóa từ khóa rồi tìm lại để hiện toàn bộ phiếu.
- **Thêm mới:** làm trống ô nhập, điền ngày hiện tại và hiện ngay mã phiếu tự động (ví dụ `TN-0005`). Nhập xong bấm **Lưu**; mã vừa hiện được giữ nguyên và xuất hiện thông báo **Lưu phiếu thành công.** Lưu chưa hợp lệ không làm tăng số phiếu.
- **Sửa:** chọn một dòng trong DataGridView, bấm **Sửa**, thay thông tin rồi **Lưu**. Mã phiếu và ngày nhận giữ nguyên; lưu xong báo **Sửa phiếu thành công.** Bấm Sửa chỉ mở ô nhập, chưa báo thành công.
- **Thoát:** đóng form tiếp nhận, quay lại màn hình chính để chọn chức năng khác. Nút này dùng được cả khi đang thêm hoặc sửa. Vì dữ liệu hiện chỉ giữ tạm trong form, đóng form sẽ mất các thay đổi đã lưu và chưa lưu.
- **Xóa:** chọn phiếu, bấm **Xóa** và xác nhận trước khi bỏ khỏi danh sách.

Biển số bắt buộc. Điện thoại được bỏ trống; nếu nhập thì phải có 8–15 chữ số. Loại xe, thợ và trạng thái chọn bằng `ComboBox`. Phiếu **Chờ khám** có thể chưa phân công thợ; **Đang sửa** hoặc **Hoàn thành** cần chọn thợ. Danh sách thợ dùng tên mẫu để chọn thủ công, chưa liên kết với tài khoản hoặc xử lý lịch rảnh/bận.

Form này quản lý thông tin tiếp nhận, lỗi ban đầu, thợ và trạng thái. Khi nhóm làm CSDL chung, form hóa đơn do thành viên khác làm sẽ tìm phiếu tiếp nhận bằng mã phiếu, lấy thông tin khách/xe rồi thêm phụ tùng, tiền công và tính tiền thanh toán. Nhóm cần thống nhất mã liên kết giữa phiếu và hóa đơn khi thiết kế CSDL. Hiện chưa có form hóa đơn hay chức năng bàn giao dữ liệu thật; nút Thoát chỉ đóng cửa sổ tiếp nhận.

## Đọc và sửa giao diện

- `frmTiepNhan.cs`: một `DataTable`, các hàm xử lý và sự kiện của nút.
- `frmTiepNhan.Designer.cs`: tạo các control, đặt vị trí và nối sự kiện.
- `frmTiepNhan.resx`: tài nguyên của form.

Chọn `frmTiepNhan.cs`, nhấn **Shift + F7** để mở Designer. Không cần đọc lớp xử lý SQL hoặc lớp mô hình riêng để hiểu phần tiếp nhận hiện tại.

Xem thêm [giải thích code](../../../../docs/tiepnhan/HUONG_DAN_GIAI_THICH_CODE.md) và [Toolbox](../../../../docs/tiepnhan/HUONG_DAN_TOOLBOX.md).
