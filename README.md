# BTL-LTTQ

Ứng dụng Windows Forms quản lý cửa hàng sửa xe, dùng .NET Framework 4.8.

## Mở và chạy

1. Mở [QLyCuaHangSuaXe.slnx](QLyCuaHangSuaXe/QLyCuaHangSuaXe.slnx) bằng Visual Studio có workload **.NET desktop development** và targeting pack **.NET Framework 4.8**. Có thể mở trực tiếp [QLyCuaHangSuaXe.csproj](QLyCuaHangSuaXe/QLyCuaHangSuaXe/QLyCuaHangSuaXe.csproj) nếu Visual Studio chưa hỗ trợ `.slnx`.
2. Chọn project **QLyCuaHangSuaXe** làm Startup Project, nhấn **F5**.
3. Trên cửa sổ chính, chọn chức năng cần mở. Các module đã được ghép vào project **QLyCuaHangSuaXe**, không cần chạy từng project riêng.

## Liên kết giữa các form

`Form1.cs` nằm ở gốc project **QLyCuaHangSuaXe** và là màn hình chính được chạy từ `Program.cs`. Màn hình này có 5 nhóm chức năng:

- **Đăng nhập & Phân quyền** → form đăng nhập gọn → **Đổi mật khẩu** hoặc **Quản lý tài khoản**. Form quản lý có hai tab **Tài khoản** và **Phân quyền**.
- **Tiếp nhận & Sửa chữa xe** → form tiếp nhận.
- **Danh mục Dịch vụ (Tiền công)** → form quản lý dịch vụ.
- **Nhập kho phụ tùng** → Kho → **Phiếu nhập** → **Thêm phiếu nhập** → form lập phiếu → **Thêm nhà cung cấp**.
- **Báo cáo & Thống kê** → nút chờ, ghi **Chưa xây dựng**, chưa mở form vì module này chưa có.
- **Thoát** trên màn hình chính đóng ứng dụng; **Đóng/Thoát** trong form con đóng cửa sổ đó.

Các form chức năng mở cùng màn hình chính. Bấm lại chức năng sẽ quay về cửa sổ đang mở (kể cả khi đang thu nhỏ); đóng rồi mở lại sẽ tạo cửa sổ mới. Form thêm nhà cung cấp mở dạng hộp thoại và cập nhật lại danh sách nhà cung cấp khi thêm thành công.

Project chính dùng liên kết đến các file nguồn trong `DanhMuc/DanhMuc` và `Kho/Kho/btl`, giữ một bản mã nguồn cho mỗi form. Nguồn module Kho đã được điều chỉnh để biên dịch chung trên .NET Framework 4.8.

Ba form đăng nhập nằm trong [DanhMuc/DangNhap](QLyCuaHangSuaXe/QLyCuaHangSuaXe/DanhMuc/DangNhap): `frmDangNhap` (đăng nhập và nút mở form con), `frmDoiMatKhau` (đổi mật khẩu), `frmQuanLyTaiKhoan` (tài khoản và phân quyền). Mỗi form có file `.cs`, `.Designer.cs` và `.resx` riêng để mở bằng Visual Studio Designer. Đóng form con sẽ quay lại đăng nhập; bấm lại nút sẽ dùng cửa sổ đang mở. Danh sách vị trí trong phần quản lý gồm **Chủ cửa hàng**, **Thu ngân**, **Kho**, **Thợ**.

### Phạm vi hiện tại

Tài khoản đăng nhập tạm: **minh**, mật khẩu **123**. Từ màn hình chính, mở **Đăng nhập & Phân quyền**, nhập tài khoản rồi bấm **Đăng nhập** (hoặc Enter). Đúng tài khoản sẽ đóng form đăng nhập và hiển thị tên trên màn hình chính; nhập sai hoặc để trống sẽ báo ngay trên form. Tài khoản này được kiểm tra trong mã nguồn, chưa dùng database.

Màn hình đổi mật khẩu và quản trị/phân quyền vẫn chỉ có giao diện; ứng dụng vẫn khởi động từ màn hình chính. Các nút sửa/xóa phiếu nhập và thao tác kho chưa có nghiệp vụ từ mã nguồn ban đầu.

Danh mục dịch vụ sử dụng SQL Server `.` với database `AutoFixDb`; Kho sử dụng SQL Server `localhost` với database `QuanLyNhapKho`. Cần cấu hình kết nối trong lớp xử lý dữ liệu tương ứng để thao tác dữ liệu thật. Script tạo database Kho nằm ở [Kho/SQLQuery1.sql](Kho/SQLQuery1.sql); repo chưa có script database dịch vụ. Hai module vẫn dùng cơ sở dữ liệu riêng. Khi không kết nối được database, form báo lỗi để người dùng biết.

## Form tiếp nhận

Mã nguồn nằm trong [DanhMuc/TiepNhan](QLyCuaHangSuaXe/QLyCuaHangSuaXe/DanhMuc/TiepNhan), đã ghép vào project chung. Không cần chạy project `tiepnhan` riêng.

Bản mới chỉ có một form: nhập thông tin khách và xe, thêm/xóa phụ tùng và dịch vụ, tính tiền, lưu và mở lại phiếu ngay trên cùng form. Màn hình chính mở một cửa sổ tiếp nhận; bấm lại nút sẽ quay về cửa sổ đang mở.

Dữ liệu lưu trong bộ nhớ đến khi đóng cửa sổ tiếp nhận, chưa có database. **Chuyển thanh toán** kiểm tra, tự lưu phiếu và chuẩn bị `PhieuChuyenThanhToan`; form thu ngân thật sẽ do thành viên khác ghép sau.

- [Hướng dẫn chạy thử và bàn giao](QLyCuaHangSuaXe/QLyCuaHangSuaXe/DanhMuc/TiepNhan/README.md)
- [Giải thích code](docs/tiepnhan/HUONG_DAN_GIAI_THICH_CODE.md)
- [Tên các control Toolbox](docs/tiepnhan/HUONG_DAN_TOOLBOX.md)

## Kiểm tra và ảnh chạy thử

Chạy `powershell -ExecutionPolicy Bypass -File .\checks\Run.ps1` để biên dịch, kiểm tra module tiếp nhận và điều hướng giữa các form. Công cụ kiểm tra nằm ngoài project ứng dụng; dữ liệu kiểm tra không xuất hiện khi chạy F5. Kiểm tra điều hướng bỏ qua truy vấn database khi tải form, không thay đổi dữ liệu SQL Server.

[Màn hình chính](docs/tiepnhan/ManHinhChinh.png) · [Form trống](docs/tiepnhan/TiepNhan-Trong.png) · [Form có phiếu đã lưu](docs/tiepnhan/TiepNhan-Tu-ManHinhChinh.png)
