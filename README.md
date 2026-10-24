# BTL-LTTQ

Ứng dụng Windows Forms quản lý cửa hàng sửa xe, dùng .NET Framework 4.8.

## Mở và chạy

1. Mở [QLyCuaHangSuaXe.slnx](QLyCuaHangSuaXe/QLyCuaHangSuaXe.slnx) bằng Visual Studio có workload **.NET desktop development** và targeting pack **.NET Framework 4.8**. Có thể mở trực tiếp [QLyCuaHangSuaXe.csproj](QLyCuaHangSuaXe/QLyCuaHangSuaXe/QLyCuaHangSuaXe.csproj) nếu Visual Studio chưa hỗ trợ `.slnx`.
2. Chọn project **QLyCuaHangSuaXe** làm Startup Project, nhấn **F5**.
3. Trên cửa sổ chính, chọn chức năng cần mở. Các module đã được ghép vào project **QLyCuaHangSuaXe**, không cần chạy từng project riêng.

## Liên kết giữa các form

`Form1.cs` nằm ở gốc project **QLyCuaHangSuaXe** và là màn hình chính được chạy từ `Program.cs`. Màn hình này có 5 nhóm chức năng:

- **Đăng nhập & Phân quyền** → form đăng nhập gọn → **Đổi mật khẩu** hoặc **Quản lý tài khoản**. Form quản lý có hai tab **Tài khoản** và **Phân quyền**.
- **Tiếp nhận xe** → lập phiếu, ghi tình trạng xe và phân công thợ.
- **Danh mục Dịch vụ (Tiền công)** → form quản lý dịch vụ.
- **Nhập kho phụ tùng** → nút chờ, mã nguồn module Kho hiện đã được gỡ khỏi repo.
- **Báo cáo & Thống kê** → nút chờ, ghi **Chưa xây dựng**, chưa mở form vì module này chưa có.
- **Thoát** trên màn hình chính đóng ứng dụng; **Đóng/Thoát** trong form con đóng cửa sổ đó.

Các form chức năng mở cùng màn hình chính. Bấm lại chức năng sẽ quay về cửa sổ đang mở (kể cả khi đang thu nhỏ); đóng rồi mở lại sẽ tạo cửa sổ mới.

Mã nguồn ứng dụng được gom theo chức năng trong project chính:

```text
QLyCuaHangSuaXe/QLyCuaHangSuaXe/
├── Form1.cs / Form1.Designer.cs
├── Program.cs
├── DanhMuc/
│   ├── DangNhap/
│   ├── DichVu/
│   │   └── Classes/
│   └── TiepNhan/
├── Classes/
├── Properties/
└── App.config
```

`Form1` là màn hình chính; `Classes` ở gốc chứa các lớp dùng chung, còn lớp xử lý dữ liệu riêng của Dịch vụ nằm trong `DanhMuc/DichVu/Classes`. Project dịch vụ độc lập cũ ở `DanhMuc/DanhMuc` dùng liên kết tới nguồn mới để tránh nhân đôi mã. Các file kiểm tra nằm trong `checks`, tài liệu nằm trong `docs` ở gốc repo.

Ba form đăng nhập nằm trong [DanhMuc/DangNhap](QLyCuaHangSuaXe/QLyCuaHangSuaXe/DanhMuc/DangNhap): `frmDangNhap` (đăng nhập và nút mở form con), `frmDoiMatKhau` (đổi mật khẩu), `frmQuanLyTaiKhoan` (tài khoản và phân quyền). Mỗi form có file `.cs`, `.Designer.cs` và `.resx` riêng để mở bằng Visual Studio Designer. Đóng form con sẽ quay lại đăng nhập; bấm lại nút sẽ dùng cửa sổ đang mở. Danh sách vị trí trong phần quản lý gồm **Chủ cửa hàng**, **Thu ngân**, **Kho**, **Thợ**.

### Phạm vi hiện tại

Tài khoản đăng nhập tạm: **minh**, mật khẩu **123**. Từ màn hình chính, mở **Đăng nhập & Phân quyền**, nhập tài khoản rồi bấm **Đăng nhập** (hoặc Enter). Trước khi đăng nhập, **Đổi mật khẩu** và **Quản lý tài khoản** bị khóa. Đăng nhập đúng sẽ giữ form mở, bật hai nút đó, khóa phần nhập tài khoản và hiển thị tên trên màn hình chính; nhập sai hoặc để trống vẫn giữ hai nút bị khóa. Đóng rồi mở lại form sẽ yêu cầu đăng nhập lại. Tài khoản này được kiểm tra trong mã nguồn, chưa dùng database.

Màn hình đổi mật khẩu và quản trị/phân quyền vẫn chỉ có giao diện; ứng dụng vẫn khởi động từ màn hình chính.

Danh mục dịch vụ sử dụng SQL Server `.` với database `AutoFixDb`. Cấu hình kết nối nằm trong `DanhMuc/DichVu/Classes/DataProcessor.cs`; repo chưa có script database dịch vụ. Khi không kết nối được database, form báo lỗi để người dùng biết.

## Form tiếp nhận

Mã nguồn nằm trong [DanhMuc/TiepNhan](QLyCuaHangSuaXe/QLyCuaHangSuaXe/DanhMuc/TiepNhan), đã ghép vào project chung. Không cần chạy project `tiepnhan` riêng.

Form tiếp nhận dùng giao diện xanh trắng, chia thành hai nhóm **Khách hàng và xe** và **Thông tin tiếp nhận**, cùng danh sách phiếu phía dưới. Có thể nhập dòng xe, số km và ngày giờ hẹn trả; thao tác bằng các nút **Thêm mới**, **Sửa**, **Xóa**, **Lưu**, **In phiếu**, **Thoát**.

Mã phiếu hiển thị tự động ngay khi bấm **Thêm mới**. Nút **Lưu** giữ nguyên mã đang hiện; mã và ngày nhận không cho nhập tay.

Lưu phiếu mới sẽ hiện hộp thông báo **Lưu phiếu thành công.** Chọn phiếu, bấm **Sửa** rồi **Lưu** sẽ báo **Sửa phiếu thành công.** Nút **Thoát** đóng form tiếp nhận để quay về màn hình chính và chọn chức năng khác.

Tìm theo mã phiếu, biển số, tên khách hoặc điện thoại, kết hợp bộ lọc trạng thái. Đổi trạng thái lọc sẽ cập nhật danh sách ngay. Sau khi lưu, từ khóa được xóa và bộ lọc trở về **Tất cả** để chọn lại phiếu vừa lưu.

Ngày hẹn trả không được trước ngày nhận. Phiếu đã qua giờ hẹn mà chưa **Hoàn thành** được tô đỏ nhạt và có nhắc hẹn; phiếu hoàn thành tô xanh nhạt, các phiếu còn lại nền trắng. Nhắc hẹn tự cập nhật mỗi 30 giây và không ghi đè nội dung đang nhập. **In phiếu** mở cửa sổ xem trước; người dùng tự chọn in.

Danh sách phía dưới là control **DataGridView** thật, tên `dgvPhieu`. Dữ liệu tạm nằm trong `DataTable bangPhieu` ở `frmTiepNhan.cs`. Code viết trực tiếp trong các sự kiện, giống cách làm bài `Hoc_vien`: tạo bảng trong `Load`, chọn dòng để đưa dữ liệu lên ô nhập, kiểm tra bằng `if` và `MessageBox.Show` rồi thêm/sửa/xóa dòng. `MaPhieu` là khóa của bảng tạm; sửa hoặc xóa dùng `Rows.Find(txtMaPhieu.Text)` để tìm đúng phiếu. Bài mẫu `D:\LTTQ\Hoc_vien` được giữ nguyên.

Phần tiếp nhận hiện dùng **dữ liệu tạm trong bộ nhớ**, chưa kết nối SQL Server. Mỗi lần mở form có bốn phiếu mẫu để thử; **Lưu** chỉ cập nhật `bangPhieu`. Đóng form rồi mở lại sẽ trở về dữ liệu mẫu. CSDL tiếp nhận sẽ được bổ sung và nối vào form sau; danh sách thợ hiện cũng là tên mẫu.

Sau khi nhóm ghép CSDL chung, form hóa đơn do thành viên khác làm sẽ tìm lại phiếu tiếp nhận theo mã phiếu, lấy thông tin khách/xe rồi bổ sung phụ tùng, tiền công và thanh toán. Hiện chưa có form hóa đơn hoặc chức năng chuyển dữ liệu sang hóa đơn.

- [Hướng dẫn chạy thử và bàn giao](QLyCuaHangSuaXe/QLyCuaHangSuaXe/DanhMuc/TiepNhan/README.md)
- [Giải thích code](docs/tiepnhan/HUONG_DAN_GIAI_THICH_CODE.md)
- [Tên các control Toolbox](docs/tiepnhan/HUONG_DAN_TOOLBOX.md)

## Kiểm tra và ảnh chạy thử

Nhấn **F5**, mở **Tiếp nhận xe** và thử thêm, sửa, tìm kiếm, lọc trạng thái, xóa và xem trước phiếu in với dữ liệu mẫu. Chọn một dòng trong DataGridView để xem thông tin và cảnh báo quá hẹn nếu có.

Chạy `powershell -ExecutionPolicy Bypass -File .\checks\Run.ps1` để biên dịch và kiểm tra phần tiếp nhận, không cần kết nối SQL.

[Màn hình chính](docs/tiepnhan/ManHinhChinh.png)
