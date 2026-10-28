# Quản lý cửa hàng sửa xe máy

Ứng dụng Windows Forms, .NET Framework 4.8, sử dụng SQL Server. Mở `QLyCuaHangSuaXe/QLyCuaHangSuaXe.slnx` hoặc project `QLyCuaHangSuaXe/QLyCuaHangSuaXe/QLyCuaHangSuaXe.csproj`, đặt project chính làm Startup Project rồi nhấn F5.

## Cài và đăng nhập

Dùng duy nhất [database/QuanLySuaXe_DayDu_20261028.sql](database/QuanLySuaXe_DayDu_20261028.sql) cho máy chưa có dữ liệu. File chứa đủ **18 bảng và dữ liệu mới**, đã thu gọn nhân viên còn Minh, An, Huy, Mai, Cường theo yêu cầu; mã được chuẩn hóa về tiền tố viết hoa và số tối thiểu ba chữ số. Không chạy nối thêm các bản SQL hoặc migration cũ. Xem [hướng dẫn cài cho nhóm](database/CAI_DAT_CHO_NHOM.md).

App.config mặc định kết nối `(localdb)\MSSQLLocalDB`, database `QuanLySuaXe`, Windows Authentication. Tài khoản quản lý có sẵn: **minh / 123**. Năm tài khoản **minh, an, huy, mai, cuong** đều có mật khẩu ban đầu `123`; tài khoản mới tạo qua ứng dụng cũng dùng `123`. Chỉ đổi mật khẩu ở thẻ **Đổi mật khẩu của tôi** trong **Tài khoản**.

Mỗi máy dùng LocalDB sẽ có dữ liệu riêng. Muốn nhóm cùng cập nhật dữ liệu trực tiếp, cài database một lần trên một SQL Server chung và cấu hình App.config của mọi máy trỏ đến cùng server/database đó. Chép file SQL vào nhiều máy không tạo ra dữ liệu dùng chung.

## Nghiệp vụ

- **Tiếp nhận:** chọn dịch vụ trước, kiểm tra tổng định mức phụ tùng, tồn và hạn dùng. Đủ điều kiện mới nhập khách, điện thoại, biển số, dòng xe, hãng và màu sắc; chọn thợ còn chỗ rồi lưu. Không còn ô địa chỉ khách, số km hay tình trạng xe vì SQL mới không lưu các trường này. Một lượt vào sửa tạo một bản ghi xe; khách cùng số điện thoại được dùng lại hồ sơ.
- **Phân công:** kỹ thuật viên chính tối đa 3 xe đang xử lý; kỹ thuật viên và thợ phụ tối đa 2 xe. Trạng thái đi từ Chờ sửa → Đang sửa → Hoàn thành. Thợ chỉ xem/cập nhật phần việc được giao. Quản lý được phân công và hủy xe chưa sửa; hủy hoàn phụ tùng trong cùng giao dịch.
- **Hóa đơn:** mở bằng nút trong Phiếu tiếp nhận. Phiếu hoàn thành xuất hiện ở danh sách chờ thanh toán. Hóa đơn chỉ được ghi vào SQL khi xác nhận đã thu tiền; một phiếu có tối đa một hóa đơn. Tiền lấy từ chi tiết tiếp nhận đã lưu, không cộng tiền phụ tùng thêm lần nữa. Có tiền mặt hoặc QR **MB Bank / 0965299505**. Đóng QR chưa xác nhận không ghi thanh toán; người thu tiền phải tự đối chiếu giao dịch ngân hàng.
- **Dịch vụ:** quản lý tên, mô tả, giá trọn gói, trạng thái và bộ phụ tùng trong `ChiTietDichVu`. Nút ngừng áp dụng chỉ đổi trạng thái, giữ dữ liệu và lịch sử. Không đổi bộ phụ tùng khi dịch vụ còn phiếu đang xử lý.
- **Kho:** danh mục phụ tùng, nhà cung cấp, nhập nhiều dòng và xem phiếu/chi tiết cùng trang. Phụ tùng có nhà cung cấp và hạn dùng riêng, không có đơn vị tính. Ghi sổ nhập cộng tồn trong giao dịch; chứng từ đã ghi sổ dùng để xem và in. Nút in mở bản xem trước ngay; xuất Excel tạo tệp `.xlsx`.
- **Nhân viên và tài khoản:** hai trang riêng. Nhân viên lưu họ tên, điện thoại, chức vụ và lương tháng. Trạng thái chỉ hiện trong danh sách; dùng nút Nghỉ việc riêng. Tạo tài khoản mới cần đủ hồ sơ, không có ô mật khẩu mới tại phần quản lý. Nghỉ việc khóa đăng nhập; chặn tự cho mình nghỉ hoặc cho thợ còn xe đang xử lý nghỉ việc. Xác thực dùng SHA-256 dạng HEX 64 ký tự theo cấu trúc SQL mới.
- **Bảng công trong Nhân viên:** đọc ba ca hiện có từ SQL. Lưu giờ thực làm theo nhân viên/ngày/ca, hỗ trợ sửa giờ, đổi ca, vắng ca nhập 0 giờ; chặn chấm trùng và tổng ngày vượt 24 giờ. Có chi tiết công, tổng hợp lương, khoản quy định theo chức vụ và xuất Excel. Công dự tính = giờ thực làm × lương tháng ÷ giờ chuẩn tháng. Mặc định **208 giờ = 26 ngày × 8 giờ** là giả định tính công, có thể đổi trên form; lương tháng đã là mức thỏa thuận, không cộng lại hệ số/phụ cấp lần nữa.
- **Thống kê:** doanh thu theo ngày thanh toán, khối lượng công việc, dịch vụ đã bán, tồn hiện tại, nhập kho, tiến độ và giờ công. Có lọc ngày và xuất Excel.

Sidebar có **Đăng xuất**, xác nhận rồi quay về đăng nhập. Dấu X cửa sổ chính đóng ứng dụng sau xác nhận. Các mã dự kiến hiện sẵn khi thêm/làm mới, ví dụ DV022 → DV023; nếu hai máy cùng lấy một mã, khóa duy nhất chặn ghi trùng và người dùng cần làm mới.

## Năm nhân viên hiện có

| Họ tên | Tên đăng nhập | Chức vụ | Mật khẩu ban đầu |
| --- | --- | --- | --- |
| Minh | minh | Chủ | 123 |
| An | an | Thợ chính | 123 |
| Huy | huy | Thợ phụ | 123 |
| Mai | mai | Thu ngân | 123 |
| Cường | cuong | Thủ kho | 123 |

Danh mục vẫn giữ đủ sáu chức vụ để dùng khi tạo thêm nhân viên.

## Chức vụ

| Chức vụ trong SQL | Chức năng chính |
| --- | --- |
| Chủ | Toàn bộ chức năng |
| Thợ chính | Phân công được giao, tiến độ, công cá nhân; tối đa 3 xe |
| Thợ phụ | Phân công được giao, tiến độ, công cá nhân; tối đa 2 xe |
| Quản lý | Quản lý các trang và nhân sự |
| Thủ kho | Kho, công cá nhân, tài khoản cá nhân |
| Thu ngân | Tiếp nhận, hóa đơn, thu tiền, công cá nhân, tài khoản cá nhân |

Quyền được kiểm tra lại với SQL khi thực hiện thao tác. Trang Phân quyền hiển thị bộ quyền cố định; đổi chức vụ tại trang quản lý nhân viên hoặc tài khoản.

## Mã nguồn và kiểm tra

Toàn bộ lớp dùng chung ở `QLyCuaHangSuaXe/QLyCuaHangSuaXe/Classes`. Các form kho nằm trực tiếp trong `DanhMuc/Kho` của ứng dụng chính. Các trang phân công, hóa đơn và thống kê có file riêng để mở Designer. Bài chỉ giữ một project `QLyCuaHangSuaXe`; nguồn gốc phần kho được ghi tại `Kho/NGUON.md`.

Chạy bằng PowerShell 7 tại gốc repo:

```powershell
pwsh -ExecutionPolicy Bypass -File .\checks\Run.ps1 -Designer
pwsh -ExecutionPolicy Bypass -File .\checks\Run.ps1 -SqlMoi
pwsh -ExecutionPolicy Bypass -File .\checks\Run.ps1 -ReadOnly
pwsh -ExecutionPolicy Bypass -File .\checks\Run.ps1 -Performance
pwsh -ExecutionPolicy Bypass -File .\checks\Run.ps1 -Configuration Release -Designer
```

`-SqlMoi` dựng bản sao kiểm thử riêng rồi thử thao tác ghi; không ghi dữ liệu thử vào database thật. `-ReadOnly` mở các form bằng dữ liệu thật và đối chiếu toàn bộ dữ liệu trước/sau. `-Designer` kiểm tra tải bề mặt thiết kế và tên hiển thị. Các tệp kiểm thử nằm trong `checks/output`, tách khỏi `bin`. `Clean.ps1 -BienDichLai` dọn `bin/obj` của ứng dụng và build lại; đóng ứng dụng trước khi chạy.

Xem [đối chiếu thiết kế SQL](docs/DANH_GIA_SQL_VA_Y_TUONG.md) và [kết quả kiểm tra](docs/KET_QUA_KIEM_THU.md).

Clean.ps1 mặc định dọn rồi biên dịch lại để Designer nạp lớp cha. Khi vừa dọn tay bin/obj, hãy Build trước và đóng/mở lại tab Designer cũ. Chỉ dùng -ChiDon khi chủ động muốn dọn mà chưa biên dịch lại.
