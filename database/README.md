# Database QuanLySuaXe

Xem README ở gốc repo để cấu hình kết nối, phân quyền và chạy ứng dụng.

Chạy theo thứ tự: `QuanLySuaXe.sql` → `QuanLySuaXe_FakeData.sql` (chỉ khi cần demo) → `QuanLySuaXe_NghiepVu.sql`. Chạy script bằng SQL Server Management Studio hoặc SQL Client trên đúng instance chứa dữ liệu của bạn. Các script cấu trúc không xóa dữ liệu.

`QuanLySuaXe.sql` chứa 14 bảng ban đầu. `QuanLySuaXe_NghiepVu.sql` bổ sung 5 bảng định mức dịch vụ, phụ tùng chốt cho tiếp nhận, ca làm, bảng công và lịch sử công; thêm giá/tên/lương chốt và trigger khóa hóa đơn.

Tài khoản mẫu dùng `Demo1234`, lưu PBKDF2-SHA256: `demo_admin`, `demo_thungan`, `demo_tho01`, `demo_tho02`. Vai trò `Kho` cũ không thuộc bốn chức vụ mới; chủ phải đổi vai trò của `demo_kho` nếu muốn dùng. `demo_nghiviec` không được đăng nhập. Tài khoản, số điện thoại và chứng từ mẫu đều giả lập.

Migration giữ nguyên dữ liệu cũ, không tự trừ kho phiếu cũ, không suy diễn giá hoặc lương lịch sử. Chủ phải đối soát phiếu cũ trước khi chốt phụ tùng. Lương giờ và lương nhiệm vụ mới có mặc định 0 để chủ nhập chính xác mức của cửa hàng.

Đã chạy cấu trúc, dữ liệu mẫu và migration hai lần trên LocalDB kiểm thử riêng; 80 kiểm tra tích hợp đã qua. Database thật `(localdb)\MSSQLLocalDB / QuanLySuaXe` cũng đã được sao lưu, kiểm tra checksum và nâng cấp. Tài khoản hiện có trên database thật là `mau_admin`, `mau_thungan`, `mau_kho`, `mau_tho`, dùng mật khẩu cũ; `mau_kho` cần chủ đổi sang một trong bốn chức vụ mới nếu muốn đăng nhập. Các tài khoản `demo_` chỉ thuộc bộ dữ liệu mẫu, không được nạp vào database thật trong lần nâng cấp này.
