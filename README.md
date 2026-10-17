# BTL-LTTQ

Ứng dụng Windows Forms quản lý cửa hàng sửa xe, dùng .NET Framework 4.8.

## Mở và chạy

1. Mở [QLyCuaHangSuaXe.slnx](QLyCuaHangSuaXe/QLyCuaHangSuaXe.slnx) bằng Visual Studio có workload **.NET desktop development** và targeting pack **.NET Framework 4.8**. Có thể mở trực tiếp [QLyCuaHangSuaXe.csproj](QLyCuaHangSuaXe/QLyCuaHangSuaXe/QLyCuaHangSuaXe.csproj) nếu Visual Studio chưa hỗ trợ `.slnx`.
2. Chọn project **QLyCuaHangSuaXe** làm Startup Project, nhấn **F5**.
3. Trên cửa sổ chính, bấm **Tiếp nhận và sửa chữa xe**.

## Form tiếp nhận

Mã nguồn nằm trong [DanhMuc/TiepNhan](QLyCuaHangSuaXe/QLyCuaHangSuaXe/DanhMuc/TiepNhan), đã ghép vào project chung. Không cần chạy project `tiepnhan` riêng.

Bản mới chỉ có một form: nhập thông tin khách và xe, thêm/xóa phụ tùng và dịch vụ, tính tiền, lưu và mở lại phiếu ngay trên cùng form. Màn hình chính mở một cửa sổ tiếp nhận; bấm lại nút sẽ quay về cửa sổ đang mở.

Dữ liệu lưu trong bộ nhớ đến khi đóng cửa sổ tiếp nhận, chưa có database. **Chuyển thanh toán** kiểm tra, tự lưu phiếu và chuẩn bị `PhieuChuyenThanhToan`; form thu ngân thật sẽ do thành viên khác ghép sau.

- [Hướng dẫn chạy thử và bàn giao](QLyCuaHangSuaXe/QLyCuaHangSuaXe/DanhMuc/TiepNhan/README.md)
- [Giải thích code](docs/tiepnhan/HUONG_DAN_GIAI_THICH_CODE.md)
- [Tên các control Toolbox](docs/tiepnhan/HUONG_DAN_TOOLBOX.md)

## Kiểm tra và ảnh chạy thử

Chạy `powershell -ExecutionPolicy Bypass -File .\checks\Run.ps1` để biên dịch và kiểm tra module cùng nút mở trên màn hình chính. Công cụ kiểm tra nằm ngoài project ứng dụng; dữ liệu kiểm tra không xuất hiện khi chạy F5.

[Màn hình chính](docs/tiepnhan/ManHinhChinh.png) · [Form trống](docs/tiepnhan/TiepNhan-Trong.png) · [Form có phiếu đã lưu](docs/tiepnhan/TiepNhan-Tu-ManHinhChinh.png)