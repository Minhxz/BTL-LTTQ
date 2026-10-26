# Quản lý cửa hàng sửa xe

Ứng dụng Windows Forms, .NET Framework 4.8. Mở `QLyCuaHangSuaXe/QLyCuaHangSuaXe.slnx` hoặc mở trực tiếp project `.csproj` cùng tên. Các project cũ `DanhMuc` và `Kho` tham chiếu ứng dụng chính và cũng yêu cầu đăng nhập.

## Kết nối database thật

1. Bật SQL Server của bạn. `App.config` mặc định dùng `(localdb)\MSSQLLocalDB`, Windows Authentication, database `QuanLySuaXe`. Nếu SSMS/SQL Client dùng instance khác, sửa `Data Source` cho đúng trong `QLyCuaHangSuaXe/QLyCuaHangSuaXe/App.config`. Hai project mở riêng cũng có `App.config` tương ứng.
2. Với database đã có, chạy `database/QuanLySuaXe.sql` để bảo đảm 14 bảng nền và liên kết nhà cung cấp. Script không xóa dữ liệu.
3. Nếu cần dữ liệu demo, chạy `database/QuanLySuaXe_FakeData.sql` **trước** script nâng cấp nghiệp vụ. Có `demo_admin / Demo1234` và `demo_thungan / Demo1234`. Đăng nhập được xác thực từ bảng nhân viên trong SQL.
4. Chạy `database/QuanLySuaXe_NghiepVu.sql`. Script bổ sung định mức phụ tùng, giá chốt, tiền nhiệm vụ, ca làm, bảng công, lịch sử sửa công và khóa hóa đơn; có thể chạy lại.
5. Build và chạy. Chủ/quản lý và thu ngân vào ngay Phiếu tiếp nhận trong sidebar; thợ vào Phân công sửa chữa.

**Tình trạng kiểm tra:** đã kết nối và nâng cấp `QuanLySuaXe` thật trên `(localdb)\MSSQLLocalDB`, sau khi sao lưu và kiểm tra checksum. Database giữ 4 nhân viên: Minh — Chủ/Quản lý, An — Thợ chính (3 xe), Huy — Thợ (2 xe), Mai — Thu ngân; giữ nguyên mã nhân viên và các liên kết. Vẫn có 3 hóa đơn, tổng 770.000đ. Tài khoản chủ là `minh`. LocalDB và SQL Client đã được kiểm tra kết nối thành công. Lớp kết nối có đường dự phòng lấy pipe hiện hành từ log của đúng instance, không cố định mã pipe trong cấu hình. Kiểm thử ghi nghiệp vụ dùng LocalDB riêng `CodexQuanLySuaXeTest` / `QuanLySuaXe_CodexChecks`.

## Nghiệp vụ đã nối SQL

- **Tiếp nhận:** bắt buộc chọn ít nhất một dịch vụ và số lượng; kiểm tra tổng phụ tùng dùng chung giữa các dịch vụ. Form Kiểm tra trước tiếp nhận cho phép chọn dịch vụ, nhập số lượng và kiểm tra tổng nhu cầu phụ tùng. Đủ kho mới chuyển sang nhập khách/xe trên phiếu. Biển số cũ tự tìm khách hàng. Chọn thợ còn chỗ, lưu khách/xe, chi tiết, xuất phụ tùng và phân công trong một transaction. Khi lưu kiểm tra lại kho và giới hạn thợ để chống hai người nhận cùng lúc.
- **Chi tiết phiếu:** giá dịch vụ, tên dịch vụ, phụ tùng, giá bán và tiền nhiệm vụ được chốt khi nhận xe. Giá danh mục thay đổi không làm đổi chứng từ đã chốt.
- **Phân công:** mặc định Thợ 2 xe, Thợ chính 3 xe. Có thể chỉnh giới hạn từng người tại trang nhân viên. Đếm cả xe máy và ô tô là một xe; chưa dùng hệ số quy đổi ô tô. Trạng thái Đã phân công → Đang sửa → Hoàn thành. Thợ chỉ xem và cập nhật phần việc của mình. Chủ có thể phân công phiếu cũ chưa có thợ, hoặc hủy phiếu mới chưa bắt đầu sửa; hủy hoàn phụ tùng và giải phóng chỗ thợ.
- **Hóa đơn:** chỉ phát hành khi phiếu đã hoàn thành và chốt phụ tùng. Một phiếu có tối đa một hóa đơn. Nội dung lấy từ chi tiết phiếu; màn hình chỉ đọc và SQL chặn sửa/xóa/thêm dòng sau phát hành. Có thể ghi nhận thanh toán một lần bằng tiền mặt hoặc chuyển khoản.
- **Dịch vụ:** thêm, đổi giá, mô tả, thời gian, tiền nhiệm vụ và bộ nhiều phụ tùng/số lượng. Dịch vụ chưa sử dụng có thể xóa; đã có lịch sử thì chuyển ngừng dùng.
- **Phụ tùng:** thêm/sửa danh mục, giá nhập/bán, trạng thái; tìm kiếm; nhà cung cấp; nhập kho nhiều dòng, mỗi dòng có NCC và hạn sử dụng tùy chọn. Ghi sổ phiếu nhập và cộng tồn trong một transaction. Phiếu nhập đã ghi sổ chỉ đọc.
- **Tài khoản:** chủ/quản lý tạo tài khoản ngay trong form, cho nhân viên mới hoặc nhân viên có sẵn chưa được cấp đăng nhập. Tạo hồ sơ và tài khoản trong một giao dịch; trùng tên không để lại hồ sơ dư. Khóa tài khoản tách riêng trạng thái nghỉ việc. Thẻ **Đổi mật khẩu của tôi** dùng lại form đổi mật khẩu hiện có, yêu cầu mật khẩu hiện tại; cả bốn vai trò truy cập được thẻ này, chỉ chủ/quản lý được xem danh sách tài khoản. Băm PBKDF2-SHA256 với salt riêng, không hiển thị hash/mật khẩu trong bảng; không cho tự khóa/đổi quyền tài khoản đang dùng.
- **Nhân viên:** trang riêng quản lý họ tên, điện thoại, chức vụ, giới hạn xe, lương giờ và trạng thái làm việc. Có nút mở phân công và bảng công. Không cho giảm giới hạn dưới số xe đang nhận hoặc chuyển khỏi vai trò thợ khi còn công việc chưa xong.
- **Bảng công:** Sáng 07:30–14:00, Chiều tối 13:30–20:00, mỗi ca 390 phút. Hiển thị cách tính từng dòng: phút thực làm +/− phút điều chỉnh = phút tính công; tiền ca = phút tính công ÷ 60 × lương giờ đã chốt. Đi muộn/về sớm đã phản ánh vào phút thực làm, không trừ lần hai. Có dự tính trước khi lưu, lọc theo nhân viên, tổng hợp tiền ca và danh sách nhiệm vụ hoàn thành để đối chiếu tổng lương. Vắng cả ca tính 0; chưa chấm công không tự coi là vắng. Điều chỉnh ±390 phút cần lý do, lưu lịch sử trước/sau trong SQL. Hai ca cùng ngày không được trùng giờ thực tính. Xuất đầy đủ chi tiết hoặc tổng hợp ra `.xlsx` thật.
- **Thống kê:** hóa đơn đã phát hành, đã thu/chưa thu; dịch vụ bán; tiền nhiệm vụ; tồn phụ tùng; nhập kho; phiếu và quá hẹn; công và lương. Có lọc ngày, xuất Excel. Báo cáo tồn là số hiện tại. Doanh thu và đã thu đang nhóm theo ngày lập hóa đơn, chưa theo ngày giao dịch thanh toán.

## Bốn chức vụ

| Chức vụ | Quyền |
| --- | --- |
| Chủ/Quản lý | Tất cả trang và thao tác quản lý |
| Thợ | Xe được giao, cập nhật tiến độ, xem/xuất công cá nhân |
| Thợ chính | Như thợ, mặc định được nhận 3 xe |
| Thu ngân | Tiếp nhận, chi tiết phiếu, hóa đơn, thanh toán |

Quyền được kiểm tra khi mở trang và khi thực hiện nghiệp vụ, có đối chiếu lại trạng thái/chức vụ hiện tại trong SQL. Trang Phân quyền hiển thị ma trận cố định; đổi chức vụ ở trang Tài khoản. Vai trò cũ `Chủ cửa hàng`/`Quản lý` được hiểu là `Chủ/Quản lý`; vai trò `Kho` cũ không nằm trong bốn vai trò mới và không được đăng nhập. Chủ có thể chuyển nhân viên đó sang một trong bốn chức vụ. Migration giữ giới hạn xe hiện có để không sửa cấu hình người dùng; 2/3 là mặc định khi tạo hoặc đổi chức vụ qua UI.

## Dữ liệu cũ

Không tự trừ lại phụ tùng hoặc suy diễn giá lịch sử cho phiếu cũ. Các phiếu chưa có hóa đơn mang `DaChotPhuTung=0`: chủ chọn đúng phiếu ở Phân công và bấm **Chốt phụ tùng phiếu cũ** sau khi đối chiếu xem trước đây đã xuất kho chưa. Thao tác này dùng định mức/giá hiện hành và trừ kho một lần. Nếu chứng từ cũ đã xuất kho nhưng chưa đánh dấu, cần đối soát SQL trước, tránh trừ hai lần.

Các hóa đơn cũ vẫn giữ nguyên tổng và chi tiết gốc. Không tự tạo thêm dòng phụ tùng/tiền nhiệm vụ lịch sử vì schema cũ không lưu đủ giá chốt. Nhân viên cũ có lương giờ và dịch vụ cũ có lương nhiệm vụ mặc định 0, cần chủ nhập mức thực tế. Tiền nhiệm vụ lịch sử không được tự cộng vào kỳ lương mới.

## Cấu trúc

`Classes` tại gốc project chính chứa toàn bộ kết nối, xác thực, quyền, nghiệp vụ sửa xe, công, xuất Excel và giao diện dùng chung. Đã bỏ `Classes` riêng của dịch vụ và kho. Nguồn form kho vẫn ở `Kho/Kho/btl` và được link vào project chính; hai project cũ tham chiếu project chính thay vì nhân bản lớp dữ liệu. Thư mục `LTTQ` cạnh repo là bài luyện tập khác, không thuộc ứng dụng này.

## Kiểm tra

```powershell
powershell -ExecutionPolicy Bypass -File .\checks\Run.ps1
powershell -ExecutionPolicy Bypass -File .\checks\Run.ps1 -Integration
powershell -ExecutionPolicy Bypass -File .\checks\Run.ps1 -Accounts
powershell -ExecutionPolicy Bypass -File .\checks\Run.ps1 -Configuration Release
```

`Clean.ps1 -BienDichLai` dọn đúng thư mục bin/obj của ba project rồi build lại. Có thể kiểm tra Designer bằng `checks/Run.ps1 -Designer`; bộ kiểm thử và bản sao ứng dụng chạy trong `checks/output/kiemthu`, không đặt file kiểm thử trong bin.

Lệnh đầu/ba build cả ba project và chạy kiểm tra không cần SQL. `-Integration` tạo/khởi động LocalDB chuyên kiểm thử, chạy cấu trúc + dữ liệu mẫu + migration hai lần, kiểm tra nghiệp vụ SQL, render form và xuất XLSX. Không chạy các kiểm tra này trên database thật. Kết quả render và workbook ở `checks/output` (đã gitignore). Chạy kiểm tra tích hợp cần quyền dùng LocalDB của Windows user.

Xem `docs/DANH_GIA_SQL_VA_Y_TUONG.md` để đối chiếu schema và các ý tưởng bổ sung.
