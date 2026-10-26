# Kết quả kiểm tra

- Build Debug và Release: ứng dụng chính, project DanhMuc và project Kho.
- 20 kiểm tra không cần SQL: mở đăng nhập, bốn vai trò, quyền truy cập, tính giờ và tiền công chính xác theo phút; đối chiếu băm mật khẩu với PBKDF2 tham chiếu; xem trước phiếu A4 không cần SQL hoặc máy in và phân trang phiếu dài.
- 124 kiểm tra tích hợp trên LocalDB riêng: đăng nhập/đổi mật khẩu, tạo tài khoản và hồ sơ nguyên tử, cấp tài khoản cho nhân viên có sẵn, chống tên đăng nhập trùng, khóa tài khoản tách trạng thái làm việc, tiền ca khớp SQL, giữ lương giờ đã chốt, đối chiếu nhiệm vụ và tổng lương; thiếu/gom phụ tùng, giới hạn 2/3 xe, tiếp nhận đồng thời, rollback, hoàn kho một lần, khóa giá/hóa đơn, thanh toán một lần, quyền từng thợ, các form và 7 báo cáo, xuất XLSX.
- 25 kiểm tra thao tác tài khoản: tạo mới ngay trong form, liên kết nhân viên cũ và giữ lương, đổi mật khẩu nhúng trong cùng trang, sai mật khẩu cũ/xác nhận lệch giữ nguyên hash, đổi thành công đăng nhập được, hạ quyền xóa dữ liệu quản lý trên form đang mở; cả ba vai trò còn lại chỉ xem mật khẩu cá nhân.
- 14 form nạp thành công bằng bộ thiết kế WinForms. Có kiểm tra tên component không trùng field kế thừa; phiếu tiếp nhận dùng nhãn riêng `lblTrangThaiPhieu` để tránh trùng `lblThongBao` của lớp cha.
- Migration chạy hai lần trên database kiểm thử.
- Database thật: đọc 11 trang và sidebar, kiểm tra đăng nhập bằng thông tin được truyền khi chạy, không ghi chứng từ kiểm thử. Đã đối chiếu: 19 bảng, 4 nhân viên, 3 hóa đơn, tổng 770.000đ. Minh — Chủ/Quản lý, An — Thợ chính (3 xe), Huy — Thợ (2 xe), Mai — Thu ngân. Giữ nguyên mã nhân viên, tên đăng nhập và password hash khi đổi tên. Tài khoản chủ là `minh`; thông tin đăng nhập thật không ghi trong source.
- Sao lưu trước tách trạng thái tài khoản và đổi tên nhân viên bằng COPY_ONLY CHECKSUM, đã RESTORE VERIFYONLY; các bản sao nằm ở `checks/output/backups`.
- Giao diện dùng màu xanh gốc: sidebar xanh đậm có icon/nút bo góc, nút chọn hiện màu xanh, khối thông tin trắng và bảng dễ đọc. Đã thêm xác nhận khi thoát cửa sổ chính/đăng nhập và nút Thoát phiếu tiếp nhận.
- Backup trước nâng cấp: `checks/output/backups/QuanLySuaXe_before_20261024_224234.bak`, đã RESTORE VERIFYONLY WITH CHECKSUM.
- Ảnh render dữ liệu thật: `checks/output/database-thuc.png`. Ảnh trạng thái đủ phụ tùng từ database kiểm thử: `checks/output/sidebar-ready.png`. File Excel kiểm tra: `checks/output/BangCong.xlsx`.

Các file trong checks/output được gitignore. Bộ ghi nghiệp vụ dùng `(localdb)\CodexQuanLySuaXeTest / QuanLySuaXe_CodexChecks` để không ảnh hưởng chứng từ thật. `VerifyDatabaseReadOnly.cs` chỉ đọc dữ liệu thật; nhận thông tin đăng nhập qua biến môi trường hoặc dùng phiên quản lý trong tiến trình kiểm thử riêng.

`Clean.ps1 -BienDichLai` dọn bin/obj của đúng ba project rồi build lại. Các file kiểm thử và bản sao ứng dụng nằm ở `checks/output/kiemthu`; bin chỉ giữ ứng dụng, cấu hình và PDB của bản build. Đã bỏ form trống Form11, lớp Function không dùng và hai bộ kiểm tra giao diện cũ. Đã xóa comment trong mã C# và SQL.
