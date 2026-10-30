# Đối chiếu SQL mới và phạm vi ứng dụng

Ứng dụng dùng cấu trúc từ file `QuanLySuaXe.sql` được cung cấp ngày 28/10/2026, bổ sung hai bảng lịch sử kho ngày 29/10/2026. Bản giao cho nhóm là `database/QuanLySuaXe_DayDu_20261029.sql`, gồm đủ 20 bảng và mọi dữ liệu hiện đang dùng. Dữ liệu nhân viên giữ Minh (chủ), An (thợ chính), Huy (thợ phụ), Mai (thu ngân), Cường (thủ kho) và hồ sơ Hường / Huong được tạo thêm. Năm tên đăng nhập mẫu không dấu vẫn dùng mật khẩu ban đầu 123. Migration không đặt lại tài khoản hoặc dữ liệu đã nhập.

## Những thay đổi theo SQL

| Nội dung | Cách ứng dụng đang xử lý |
| --- | --- |
| Tài khoản nằm trong NhanVien | Đọc HoTen, TenDangNhap, MatKhau, TrangThai; SHA-256 HEX 64 ký tự; Nghỉ việc chặn đăng nhập |
| Sáu chức vụ | Chủ, Quản lý, Thợ phụ, Thợ chính, Thu ngân, Thủ kho; quyền cố định theo chức vụ |
| Khách hàng bỏ địa chỉ | Tiếp nhận chỉ nhập tên, điện thoại và các trường xe có trong SQL; bỏ địa chỉ, số km và tình trạng không có cột lưu |
| Mỗi lượt sửa có Xe riêng | Dùng lại khách theo số điện thoại; tạo lượt xe mới, liên kết phiếu qua MaXe |
| ChiTietDichVu là bộ phụ tùng | Gom nhu cầu dùng chung, kiểm tra trạng thái, hạn dùng và tồn trước khi nhận xe; kiểm tra lại lúc lưu |
| Giá dịch vụ trọn gói | Thành tiền chi tiết phiếu được chốt khi nhận, không cộng phụ tùng thêm lần nữa vào thanh toán |
| ChiTietPhanCong theo nhân viên/xe | Giới hạn thợ chính 3 xe, thợ phụ 2 xe đang xử lý; đếm theo trạng thái phiếu |
| HoaDon bắt buộc ngày thanh toán | Phiếu hoàn thành nằm trong danh sách chờ thu; chỉ tạo HoaDon khi nhận tiền, dùng ngày thật và chặn thanh toán lặp |
| Công theo nhân viên/ngày/ca | ChiTietCaLam lưu giờ thực tế; sửa/đổi ca theo khóa ghép, không dùng mã bảng công cũ |
| Phụ tùng có nguồn và hạn dùng | Mã phụ tùng phân biệt nhà cung cấp/hạn dùng; không có đơn vị tính; nhập kho và cập nhật tồn trong giao dịch |
| Hai lịch sử kho | DieuChinhTonKho lưu lượng cũ/mới, giá nhập và giá trị chênh lệch tự tính; DieuChinhGiaBan lưu giá cũ/mới, ngày, lý do và người thực hiện |
| Sửa/xóa phiếu nhập | Tính chênh lệch lượng nhập để sửa tồn, chặn thiếu hàng; giữ danh mục/giá hiện tại, dùng dấu vết để phát hiện phiếu đã bị phiên khác sửa |

Tất cả cột đều NOT NULL. Thời điểm kết thúc của phân công đang xử lý là thời điểm dự kiến hợp lệ; khi hoàn thành cập nhật thời điểm kết thúc thật. Trạng thái đang xử lý lấy từ phiếu, không suy ra từ việc ngày kết thúc đã đến hay chưa. Hóa đơn chưa thu không dùng ngày thanh toán giả.

## Cách tính công hiện tại

Ba ca lấy trực tiếp từ SQL: 07:30–12:00 (4,5 giờ), 13:00–17:30 (4,5 giờ), 17:30–21:00 (3,5 giờ). Giờ thực tế có thể nhỏ/lớn hơn giờ ca; vắng ca nhập 0. Một nhân viên/ngày/ca chỉ có một dòng, tổng giờ ngày không vượt 24.

`NhanVien.Luong` được hiểu là mức lương tháng đã thỏa thuận. Màn hình dự tính:

**Tiền công = tổng giờ thực làm × lương tháng ÷ giờ chuẩn tháng.**

Mặc định 208 giờ (26 ngày × 8 giờ) là giả định của ứng dụng, có ô điều chỉnh theo quy định cửa hàng. Hệ số và khoản điều chỉnh chức vụ được hiển thị để đối chiếu, không tự cộng thêm lần nữa vào mức lương tháng. Các khoản trong `KhoanDieuChinh` là quy định theo chức vụ, không phải ghi nhận phạt/thưởng thực tế cho từng nhân viên trong từng tháng.

Database mới không lưu giờ vào/ra, lý do sửa công, tiền nhiệm vụ, đơn giá giờ lịch sử hay kỳ đã chốt. Ứng dụng vì vậy không hiển thị những chi tiết này như dữ liệu đã được lưu. Báo cáo công theo mức lương tháng hiện tại; đổi lương có thể làm thay kết quả tính lại các kỳ trước.

## Những phần chưa được cấu trúc SQL biểu diễn

- **Khóa kỳ lương và lịch sử:** cần bảng kỳ lương, chi tiết lương đã chốt, đơn giá áp dụng và lịch sử sửa công nếu muốn tái hiện chính xác số đã trả.
- **Thưởng/phạt thực tế:** cần bảng ghi nhận từng khoản, ngày áp dụng, nhân viên, lý do và người duyệt; không nên tự áp mọi khoản phạt trong danh mục cho tất cả nhân viên mỗi tháng.
- **Lịch ca dự kiến:** hiện lưu công thực tế và đổi trực tiếp ca đã nhập, chưa có đăng ký/xin đổi/duyệt ca.
- **Theo dõi giao dịch thanh toán:** HoaDon hiện lưu tổng tiền và ngày thanh toán, chưa có cột phương thức, mã giao dịch ngân hàng hoặc bảng thu nhiều đợt. Tiền mặt và QR đều ghi hóa đơn một lần khi thu đủ; chưa có xác nhận nhận tiền tự động từ ngân hàng.
- **Lịch sử chi tiết phụ tùng xuất sửa:** định mức có thể thay đổi sau khi xe đã hoàn thành; SQL chưa lưu bản chụp tên/giá từng phụ tùng trên phiếu tiếp nhận. Không nên suy diễn phần tách tiền phụ tùng lịch sử từ định mức hiện tại.
- **Sổ xuất và kiểm kê có duyệt:** đã có lịch sử điều chỉnh tồn/giá và lịch sử khi sửa/xóa phiếu nhập. Giảm tồn do sửa chữa vẫn gắn với phiếu tiếp nhận; chưa có chứng từ xuất độc lập và quy trình duyệt kiểm kê.
- **Quyền tùy biến:** các chức vụ có bộ quyền trong mã, trang Phân quyền dùng để xem. Muốn chỉnh từng quyền và lưu SQL cần thêm cấu trúc quyền tương ứng.

Các phần trên là ý tưởng mở rộng, chưa triển khai. Hai bảng lịch sử kho đã được thêm theo yêu cầu tích hợp kho; các cột của 18 bảng trước đó được giữ nguyên. Ưu tiên chốt kỳ lương và ghi nhận giao dịch nếu nhóm cần đối soát tiền công hoặc thanh toán sau này.
