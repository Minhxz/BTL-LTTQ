# Cài database cho nhóm

Dùng **QuanLySuaXe_DayDu_20261028.sql** đi cùng mã nguồn hiện tại. Đây là bản xuất đủ cấu trúc và dữ liệu của database mới: 18 bảng, khóa chính/ngoại, điều kiện kiểm tra và toàn bộ bản ghi. Không cần chạy bản SQL cũ, dữ liệu mẫu cũ hoặc migration nào khác.

## Máy chưa có database

1. Cài Visual Studio với Windows Forms/.NET Framework 4.8 và SQL Server hoặc LocalDB.
2. Mở SQL Client/SSMS, kết nối `(localdb)\MSSQLLocalDB` bằng Windows Authentication, hoặc kết nối đúng instance SQL Server của nhóm.
3. Mở **QuanLySuaXe_DayDu_20261028.sql** và chạy toàn bộ file. Script tạo `QuanLySuaXe` tại thư mục mặc định của SQL Server, không chứa đường dẫn MDF của máy gửi. Nếu database đã tồn tại, script dừng để giữ dữ liệu; dùng database mới hoặc sao lưu/đối soát bản đang có trước khi thay. Sau lần chạy bị chặn, đóng cửa sổ truy vấn đó trước khi thử lại.
4. Mở `QLyCuaHangSuaXe/QLyCuaHangSuaXe/QLyCuaHangSuaXe.csproj`, đặt làm Startup Project, nhấn F5. App.config mặc định dùng LocalDB và database `QuanLySuaXe`. Nếu cài trên instance khác, sửa `Data Source` trong App.config của project chạy.
5. Đăng nhập quản lý bằng **minh / 123**. Đổi mật khẩu ở **Tài khoản → Đổi mật khẩu của tôi**.

Năm tài khoản hiện có:

| Họ tên | Tên đăng nhập | Chức vụ | Mật khẩu ban đầu |
| --- | --- | --- | --- |
| Minh | minh | Chủ | 123 |
| An | an | Thợ chính | 123 |
| Huy | huy | Thợ phụ | 123 |
| Mai | mai | Thu ngân | 123 |
| Cường | cuong | Thủ kho | 123 |

Nhân viên có trạng thái Nghỉ việc không đăng nhập được. Tài khoản mới tạo qua ứng dụng cũng dùng mật khẩu ban đầu `123`; không có ô mật khẩu tại phần tạo tài khoản. Danh mục vẫn có đủ sáu chức vụ.
## Nhiều máy dùng cùng dữ liệu trực tiếp

Cài database **một lần** trên SQL Server chung. Mọi máy trỏ `Data Source` đến cùng máy chủ/instance và `Initial Catalog=QuanLySuaXe`, dùng tài khoản được máy chủ cho phép. Ví dụ giá trị kết nối khi đã có server chung: `Data Source=TEN-MAY-CHU\TEN-INSTANCE;Initial Catalog=QuanLySuaXe;Integrated Security=True;TrustServerCertificate=True`.

LocalDB mặc định của từng máy là database riêng; cùng tên `QuanLySuaXe` không làm chúng đồng bộ. Phải cấu hình kết nối và quyền truy cập trên SQL Server chung trước khi các máy khác kết nối được. Project chưa tự dựng máy chủ hay cấu hình mạng cho nhóm.

## Dữ liệu giao cùng project

Ngày xuất **28/10/2026**. Bản xuất có 5 nhân viên, 6 chức vụ, 3 ca và 55 dòng công trong 7 ngày mẫu của năm hồ sơ đại diện; có 15 khách, 16 lượt xe/phiếu tiếp nhận, 8 hóa đơn, 22 dịch vụ, 30 phụ tùng, 8 nhà cung cấp, 3 phiếu nhập và 10 chi tiết nhập. Các bảng liên kết và chi tiết khác cũng có đủ dữ liệu.

Bản cài lại đã được đối chiếu từng giá trị với database thật. Số dòng và SHA-256 của cả 18 bảng nằm trong **QuanLySuaXe_DoiChieu_20261028.json**. Bản xuất không chứa dữ liệu kiểm thử mới.

Mã dùng tiền tố viết hoa, số tối thiểu ba chữ số: NV001, CV001, CA001, KDC001, KH001, XE001, DV001, PT001, NCC001, PN001, PTN001, HD001. Việc chuẩn hóa mã được áp dụng đồng bộ ở mọi khóa ngoại. Đã thu gọn 22 hồ sơ mẫu về năm nhân viên theo yêu cầu, chuyển người lập phiếu/nhập kho và gộp phân công trùng theo nhân viên–xe. Chỉ giữ công của năm hồ sơ đại diện, không cộng dồn giờ của 22 người vào năm người. Đã thu gọn dữ liệu lịch sử, giữ các phiếu mẫu đại diện và dữ liệu mới đang có. Mã hiện trước khi lưu là mã dự kiến; khi bị máy khác dùng trước, làm mới để lấy mã tiếp theo.

Tất cả cột trong 18 bảng đều không cho phép NULL. Khách hàng/tiếp nhận không có địa chỉ hoặc số km; nhà cung cấp vẫn có địa chỉ theo SQL mới. Phụ tùng không có đơn vị tính. Định mức dịch vụ nằm trong `ChiTietDichVu`.

## Ca làm, tính công và thanh toán

Ba ca hiện có: **07:30–12:00**, **13:00–17:30**, **17:30–21:00**. Công lưu giờ thực làm, vắng ca là 0 giờ. Giờ chuẩn tháng mặc định 208 là giả định của màn hình tính công và có thể chỉnh; tiền công = giờ thực làm × lương tháng ÷ giờ chuẩn. Không cộng lại hệ số hoặc khoản điều chỉnh vào lương tháng đã thỏa thuận.

Phiếu hoàn thành hiện trong danh sách chờ thanh toán qua nút Hóa đơn của Tiếp nhận. Chỉ khi thu tiền, hệ thống mới lưu hóa đơn với ngày thanh toán thật. QR hiện cấu hình **MB Bank / 0965299505**; đóng QR không tự coi là đã trả tiền. Chỉ xác nhận khi đã đối chiếu giao dịch nhận tiền.

