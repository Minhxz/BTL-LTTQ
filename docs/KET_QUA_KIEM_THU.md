# Kết quả kiểm tra

- Build Debug và Release: ứng dụng chính, project DanhMuc và project Kho.
- 10 kiểm tra không cần SQL: mở đăng nhập, bốn vai trò, quyền truy cập và tính giờ.
- 86 kiểm tra tích hợp trên LocalDB riêng: đăng nhập SQL/đổi mật khẩu, thiếu và gom nhu cầu phụ tùng, giới hạn 2/3 xe, hai lượt nhận xe đồng thời, rollback, hoàn kho một lần, khóa giá và hóa đơn, thanh toán một lần, phạm vi dữ liệu từng thợ, công/ca/lương/lịch sử, các form và 7 báo cáo, xuất XLSX. Có kiểm tra bước kiểm tra trước tiếp nhận, đổi số lượng buộc kiểm tra lại, chuyển hạng mục sang phiếu, nút Kho lớn và ô chọn thợ không bị che.
- 12 form nạp thành công bằng bộ thiết kế WinForms. Không truy vấn SQL khi mở Designer.
- Migration chạy hai lần trên database kiểm thử.
- Database thật: đọc 9 trang và sidebar qua lớp kết nối chung, kiểm tra đăng nhập bằng thông tin được truyền khi chạy, không ghi chứng từ kiểm thử. Đã đối chiếu: 19 bảng, 4 nhân viên, 3 hóa đơn, tổng 770.000đ. Tài khoản chủ đổi thành `minh` theo yêu cầu; mật khẩu không lưu trong source. Thợ hiện có nhận tối đa 2 xe. Đã sao lưu trước khi thêm cột `SoKm` vào phiếu tiếp nhận.
- Backup trước nâng cấp: `checks/output/backups/QuanLySuaXe_before_20261024_224234.bak`, đã RESTORE VERIFYONLY WITH CHECKSUM.
- Ảnh render dữ liệu thật: `checks/output/database-thuc.png`. Ảnh trạng thái đủ phụ tùng từ database kiểm thử: `checks/output/sidebar-ready.png`. File Excel kiểm tra: `checks/output/BangCong.xlsx`.

Các file trong checks/output được gitignore. Bộ ghi nghiệp vụ dùng `(localdb)\CodexQuanLySuaXeTest / QuanLySuaXe_CodexChecks` để không ảnh hưởng chứng từ thật. `VerifyDatabaseReadOnly.cs` chỉ đọc dữ liệu thật; nhận thông tin đăng nhập qua biến môi trường hoặc dùng phiên quản lý trong tiến trình kiểm thử riêng.

`Clean.ps1 -BienDichLai` dọn bin/obj của đúng ba project rồi build lại. Các file kiểm thử và bản sao ứng dụng nằm ở `checks/output/kiemthu`; bin chỉ giữ ứng dụng, cấu hình và PDB của bản build. Đã bỏ form trống Form11, lớp Function không dùng và hai bộ kiểm tra giao diện cũ. Đã xóa comment trong mã C# và SQL.
