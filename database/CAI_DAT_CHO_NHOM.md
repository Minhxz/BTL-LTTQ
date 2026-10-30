# Cài database cho nhóm

Bản chuẩn cho máy chưa có dữ liệu là **[QuanLySuaXe_DayDu_20261029.sql](QuanLySuaXe_DayDu_20261029.sql)**. Bản này chứa đủ cấu trúc và dữ liệu của **20 bảng**, gồm hai bảng lịch sử kho mới. Máy đang có bản 18 bảng dùng migration bổ sung ở phần dưới để giữ dữ liệu đã nhập.

Có thể gửi một tệp [QuanLySuaXe_ChoNhom_20261029.zip](QuanLySuaXe_ChoNhom_20261029.zip) cho nhóm; giải nén trước khi mở SQL và hướng dẫn.

## Máy chưa có database

1. Cài Visual Studio với Windows Forms/.NET Framework 4.8 và SQL Server hoặc LocalDB.
2. Mở SQL Client/SSMS, kết nối `(localdb)\MSSQLLocalDB` bằng Windows Authentication hoặc đúng SQL Server của nhóm.
3. Mở **QuanLySuaXe_DayDu_20261029.sql** và chạy toàn bộ file. Script tạo database `QuanLySuaXe` và dữ liệu tại vị trí mặc định của SQL Server; không chứa đường dẫn MDF của máy gửi. Nếu đã có database cùng tên, script dừng để giữ dữ liệu. Đóng cửa sổ truy vấn bị chặn, chuyển sang hướng dẫn cập nhật; không xóa database đang có.
4. Mở `QLyCuaHangSuaXe/QLyCuaHangSuaXe/QLyCuaHangSuaXe.csproj`, đặt làm Startup Project rồi nhấn F5. App.config mặc định dùng LocalDB và `QuanLySuaXe`; nếu dùng instance khác, đổi `Data Source` của project chạy.
5. Đăng nhập **minh / 123**. Đổi mật khẩu cá nhân ở **Tài khoản → Đổi mật khẩu của tôi**.

Năm tài khoản mẫu vẫn giữ:

| Họ tên | Tên đăng nhập | Chức vụ | Mật khẩu ban đầu |
| --- | --- | --- | --- |
| Minh | minh | Chủ | 123 |
| An | an | Thợ chính | 123 |
| Huy | huy | Thợ phụ | 123 |
| Mai | mai | Thu ngân | 123 |
| Cường | cuong | Thủ kho | 123 |

Dữ liệu hiện có sáu hồ sơ, gồm **Hường / Huong** được tạo thêm. Gói SQL giữ hồ sơ và dữ liệu phát sinh này. Tài khoản mới tạo qua ứng dụng mặc định `123`, không có ô mật khẩu ở phần tạo tài khoản. Người Nghỉ việc bị chặn đăng nhập. Có sáu chức vụ: Chủ, Quản lý, Thợ phụ, Thợ chính, Thu ngân, Thủ kho.

## Máy đang dùng database 18 bảng

Đây là nâng cấp bổ sung, không tạo lại database. Đóng ứng dụng trên các máy đang cùng dùng dữ liệu trước khi cập nhật.

Với LocalDB mặc định, mở PowerShell 7 tại gốc project và kiểm tra trước:

```powershell
pwsh -ExecutionPolicy Bypass -File .\checks\KiemTraCapNhatKho.ps1
```

Lệnh trên chỉ đọc cấu trúc, tài khoản và dấu vân tay dữ liệu. Khi đúng database, áp dụng:

```powershell
pwsh -ExecutionPolicy Bypass -File .\checks\KiemTraCapNhatKho.ps1 -ApDung -ThuMucSaoLuu .\SaoLuuSql
```

Công cụ tạo `.bak` COPY_ONLY/CHECKSUM và VERIFYONLY, chụp một bản SQL đầy đủ trước nâng cấp rồi chạy **[CapNhatKho_20261029.sql](CapNhatKho_20261029.sql)** trong giao dịch. Mọi bảng và tài khoản hiện có được đối chiếu SHA trước/sau; cột của 18 bảng cũ được giữ nguyên. Migration thêm `DieuChinhTonKho`, `DieuChinhGiaBan`, khóa/index/check và cho phép giá bán dương thấp hơn giá nhập. Chỉ commit khi đối chiếu đạt; có thể chạy lại mà không xóa lịch sử. Thông tin năm tài khoản mẫu không bị đặt lại; mọi nhân viên khác cũng được giữ.

Helper hiện dùng LocalDB cố định. Nếu chạy SQL Server khác, sao lưu và VERIFYONLY trên đúng server trước, rồi mở migration trong SSMS, chọn đúng database và chạy. Đối chiếu số dòng/dữ liệu trước sau; không chạy SQL gốc từ repo kho hoặc bản tạo mới lên database đang dùng. Migration chỉ dành cho cấu trúc 18 bảng hiện tại; nếu là bản cũ hơn hoặc báo sai cấu trúc, dừng để đối chiếu riêng.

## Nhiều máy dùng chung trực tiếp

Cài database một lần trên SQL Server chung. Mọi máy phải trỏ `Data Source` đến cùng máy chủ/instance và `Initial Catalog=QuanLySuaXe`, dùng tài khoản được server cho phép. Ví dụ sau khi đã cấu hình server:

```text
Data Source=TEN-MAY-CHU\TEN-INSTANCE;Initial Catalog=QuanLySuaXe;Integrated Security=True;TrustServerCertificate=True
```

LocalDB của từng máy là database riêng; cùng tên `QuanLySuaXe` không tạo đồng bộ. Project chưa tự dựng SQL Server chung hoặc cấu hình mạng/quyền truy cập cho nhóm.

## Dữ liệu và quy tắc bản mới

Bản xuất ngày **29/10/2026** giữ toàn bộ dữ liệu đang dùng, gồm sáu hồ sơ nhân viên và các chứng từ được nhập thêm. Số dòng và SHA-256 của cả 20 bảng ở **[QuanLySuaXe_DoiChieu_20261029.json](QuanLySuaXe_DoiChieu_20261029.json)**. Công cụ kiểm tra gói SQL cài vào database kiểm thử riêng, đối chiếu mọi giá trị, cột/computed, khóa, index và check.

Mã dùng tiền tố viết hoa và số tối thiểu ba chữ số: NV001, CV001, CA001, KDC001, KH001, XE001, DV001, PT001, NCC001, PN001, PTN001, HD001; kho mới có DC001 và DG001. Mã trên form là mã dự kiến; nếu phiên khác dùng trước thì làm mới để lấy mã kế tiếp.

Tất cả cột của 20 bảng không cho phép NULL, gồm `GiaTriChenhLech` computed `PERSISTED NOT NULL`. Trạng thái cũ giữ dạng chữ. Tiếp nhận không có địa chỉ khách hoặc số km; nhà cung cấp vẫn có địa chỉ. Phụ tùng không có đơn vị tính; định mức dịch vụ ở `ChiTietDichVu`.

Kho tích hợp từ repo nguồn commit **4ac7066** ngày **29/10/2026**. Điều chỉnh tồn/giá cần lý do và xác nhận, lưu người thực hiện từ phiên đăng nhập cùng lịch sử. Giá bán dưới giá nhập cần xác nhận riêng. Sửa/xóa phiếu nhập tính chênh lệch tồn, chặn thiếu hàng, giữ danh mục và các giá hiện tại của phụ tùng đã có; SHA-256 chống ghi đè phiếu bị phiên khác đổi. Chỉ xóa NCC chưa được phụ tùng dùng. Excel Kho có ba sheet theo các bảng đang hiển thị.

Ba ca hiện có: **07:30–12:00**, **13:00–17:30**, **17:30–21:00**. Công lưu giờ thực làm; vắng ca là 0. Tiền công = giờ thực làm × lương tháng ÷ giờ chuẩn, mặc định 208 có thể chỉnh; không cộng lại hệ số/phụ cấp vào lương tháng đã thỏa thuận.

Phiếu Hoàn thành chờ thu qua nút Hóa đơn trong Tiếp nhận; chỉ lưu hóa đơn khi xác nhận đã thanh toán. QR dùng **MB Bank / 0965299505**; nhân viên phải đối chiếu tiền nhận, đóng QR không tự ghi đã trả tiền.
