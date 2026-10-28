# Database QuanLySuaXe

Bản đang dùng là **QuanLySuaXe_DayDu_20261028.sql**. File này có cấu trúc và dữ liệu SQL mới đã chỉnh về năm nhân viên theo yêu cầu, gồm **18 bảng**, không có cột cho phép NULL. Chạy duy nhất file này khi cài từ đầu; không chạy nối các script cũ.

- [Hướng dẫn cài cho nhóm](CAI_DAT_CHO_NHOM.md).
- [Bản SQL đầy đủ](QuanLySuaXe_DayDu_20261028.sql).
- [Số dòng và SHA-256 dữ liệu từng bảng](QuanLySuaXe_DoiChieu_20261028.json).

Tài khoản quản lý ban đầu: **minh / 123**. Có đúng năm nhân viên **Minh, An, Huy, Mai, Cường** tương ứng các tên đăng nhập **minh, an, huy, mai, cuong**, đều dùng mật khẩu ban đầu `123`. Nhân viên nghỉ việc bị chặn; tài khoản mới tạo qua ứng dụng cũng mặc định `123`. Mật khẩu lưu trong `NhanVien.MatKhau`, băm SHA-256 dạng HEX 64 ký tự.

| Nhóm | Bảng |
| --- | --- |
| Nhân sự và công | ChucVu, KhoanDieuChinh, ChucVuDieuChinh, NhanVien, CaLam, ChiTietCaLam |
| Khách, xe và phân công | KhachHang, Xe, ChiTietPhanCong |
| Dịch vụ và kho | DichVu, ChiTietDichVu, PhuTung, NhaCungCap, PhieuNhap, ChiTietPhieuNhap |
| Tiếp nhận và thanh toán | PhieuTiepNhan, ChiTietPhieuTiepNhan, HoaDon |

Giá trọn gói và tổng tiền phiếu lấy từ `ChiTietPhieuTiepNhan.ThanhTien`. Hóa đơn chỉ lưu khi xác nhận đã thanh toán; phiếu hoàn thành chưa có hóa đơn là khoản chờ thu. Công lấy giờ thực tế từ `ChiTietCaLam`, ca từ `CaLam`, lương tháng từ `NhanVien.Luong`.

App.config mặc định `(localdb)\MSSQLLocalDB / QuanLySuaXe`. LocalDB trên các máy khác nhau là các bản dữ liệu độc lập; muốn cập nhật chung phải cấu hình tất cả máy kết nối cùng SQL Server.

Các bản SQL, migration và kiểm thử dành cho cấu trúc cũ là tài liệu lưu trữ, không dùng để cài hoặc nâng cấp bản này. Không nhập SQL của repo kho riêng đè lên database hiện tại.
