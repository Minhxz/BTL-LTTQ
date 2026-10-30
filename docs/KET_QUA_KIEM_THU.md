# Kết quả kiểm tra

## SQL và gói dữ liệu ngày 29/10/2026

Database thật `QuanLySuaXe` đã nâng cấp bổ sung lên **20 bảng**. Công cụ sao lưu tạo `.bak` COPY_ONLY/CHECKSUM, chạy VERIFYONLY, chụp SQL trước nâng cấp rồi so sánh dữ liệu và tài khoản trước/sau. Cấu trúc cột và mọi giá trị dữ liệu của 18 bảng cũ được giữ nguyên; hai bảng lịch sử kho mới ban đầu rỗng. Migration đã chạy lại hai lần để kiểm tra không tạo trùng hoặc mất dữ liệu.

Đã xuất [SQL đầy đủ ngày 29/10](../database/QuanLySuaXe_DayDu_20261029.sql), cài từ đầu vào `QuanLySuaXe_CodexInstall`, đối chiếu từng giá trị của cả 20 bảng cùng cột, computed, khóa, chỉ mục và check: khớp hoàn toàn. Không có cột nullable; `GiaTriChenhLech` giữ computed PERSISTED NOT NULL. Database thật có **23 khóa ngoại và 27 check**, tất cả bật và được tin cậy; DBCC CHECKCONSTRAINTS không trả dòng lỗi.

Bản xuất giữ đủ **6 nhân viên**, **17 phiếu tiếp nhận**, **17 lượt xe**, **16 khách**, **8 hóa đơn**, **3 phiếu nhập**, **10 chi tiết nhập**, **38 chi tiết tiếp nhận**, **13 phân công**, **55 dòng công**, **23 dịch vụ**, **30 phụ tùng** và **8 nhà cung cấp**. Năm tài khoản minh/an/huy/mai/cuong vẫn dùng `123`; hồ sơ Hường/Huong tạo thêm cũng được giữ nguyên. Số dòng và SHA-256 từng bảng ở [bản đối chiếu](../database/QuanLySuaXe_DoiChieu_20261029.json). Database cài thử `CodexInstall` đã được gỡ sau kiểm tra; không tác động các database bài khác.

Gói [SQL cho nhóm ngày 29/10](../database/QuanLySuaXe_ChoNhom_20261029.zip) gồm SQL đủ dữ liệu, đối chiếu, hướng dẫn cài và migration. Bản SQL ngày 28/10 được giữ làm bản cũ; dùng bản ngày 29/10 cho máy mới.

## Lượt kiểm tra trước ngày 28/10/2026

Debug và Release đã biên dịch thành công. Bộ kiểm tra WinForms DesignSurface nạp đủ 22 form. Đã sửa frmChinh: tên file trùng tên lớp, lớp cha Form ghi đầy đủ, constructor dành cho Designer chỉ khởi tạo giao diện; bỏ tham chiếu System.dll theo đường dẫn riêng của máy. Khôi phục cột native Designer của hóa đơn và phân công. Designer frmChinh đã mở trực tiếp trong Visual Studio, hiển thị đầy đủ sidebar và không còn lỗi System.Void. Designer Nhân viên cũng đã mở trực tiếp, hiển thị hai thẻ và nút Nghỉ việc. Cần biên dịch lại sau Clean, đóng/mở lại Designer lỗi cũ để Visual Studio nạp lớp cha mới. Cả 22 form đã nạp qua DesignSurface. Đã mở trực tiếp bốn Designer: frmChinh, frmNhanVien, frmBangCong, frmChiTietHoaDon. Chưa xác nhận mở tay từng form còn lại: công cụ điều khiển liên tục báo user input was detected dù đã đọc lại cửa sổ, nên dừng để tránh can thiệp thao tác người dùng.

135 kiểm tra chức năng đã đạt trên database thử riêng: kho, dịch vụ, tiếp nhận, thanh toán, giới hạn xe, tài khoản, nghỉ việc, sáu chức vụ, công cá nhân và bảng công tích hợp. Có kiểm tra tìm tên/điện thoại, không tìm mã phiếu/biển số; bộ lọc bốn mục và không còn cột Nhắc hẹn.

Đã mở 22 form với SQL thật, 7 thống kê và 3 bảng công. SHA-256 toàn bộ 18 bảng không thay đổi sau lượt chỉ đọc. Sidebar thẳng hàng/cùng kích thước ở 100% và 125%, không còn nút bảng công riêng. Đăng nhập đầu tiên đo 395 ms, truy vấn tài khoản 4 ms.

Database được thu gọn, giữ đúng năm tài khoản minh/an/huy/mai/cuong, mật khẩu 123. Bản xuất hiện có 16 phiếu tiếp nhận, 16 lượt xe, 15 khách, 8 hóa đơn, 3 phiếu nhập, 10 chi tiết nhập, 37 chi tiết tiếp nhận, 12 phân công và 55 dòng công. Giữ 22 dịch vụ, 30 phụ tùng, 8 nhà cung cấp. Sáu chức vụ: Chủ, Quản lý, Thợ phụ, Thợ chính, Thu ngân, Thủ kho. Không có cột cho phép NULL. DBCC CHECKDB PHYSICAL_ONLY/MAXDOP1 và kiểm tra ràng buộc đạt.

Đã xuất database/QuanLySuaXe_DayDu_20261028.sql, cài lại vào bản thử và đối chiếu cấu trúc cùng từng giá trị của 18 bảng; khớp hoàn toàn. ZIP giao nhóm gồm SQL, số dòng/SHA-256 và hướng dẫn cài. Sau kiểm tra đã gỡ đúng ba database CodexChecks/CodexInstall/CodexUpgrade, giữ database thật và các bài khác.

Đã bỏ hai project mở trung gian, công cụ tạm và kết quả cũ; bản sao trước sửa được giữ ngoài project tại ../SaoLuuSuaXe. Bin Debug chỉ giữ ứng dụng, cấu hình, PDB và QRCoder.dll. Clean mặc định biên dịch lại sau dọn; -ChiDon chỉ dùng khi chủ động muốn bỏ cả đầu ra cần cho Designer. Mã C# sản phẩm không có comment thừa; tên điều khiển/hiển thị tiếng Việt, API và biến do Designer sinh theo chuẩn Windows Forms.

Chạy lại: checks/Run.ps1 -Designer, -SqlMoi, -ReadOnly, -Performance. Các lượt ghi chỉ vào database thử. Cần gỡ database thử sau khi chạy xong. LocalDB trên mỗi máy độc lập; để dùng chung trực tiếp cần một SQL Server chung và chỉnh kết nối tất cả máy.

## Chốt kiểm tra ứng dụng và tài liệu ngày 29/10/2026

Debug và Release build thành công; cả 22 form qua WinForms DesignSurface trên hai cấu hình. Đã đạt 226 kiểm tra nghiệp vụ và 49 kiểm tra hồi quy bổ sung. Sửa lỗi phụ tùng chưa chọn mã nhưng còn tên/giá cũ; sửa lệch thông tin chi tiết khi chọn dòng ở tám form. Tiếp nhận khóa chuyển dòng khi sửa, lưu đúng phiếu và chọn lại phiếu vừa lưu.

Đã duyệt đủ 102 nút khai báo: 101 nút được gọi PerformClick trên form thật, một nút Phân công riêng được ẩn có chủ đích vì đã gộp vào Nhân viên. Các nút cần dữ liệu được thử lại ở trạng thái hợp lệ. Lượt bấm xác nhận sự kiện và trạng thái, không có nghĩa mọi lượt bấm mặc định đều lưu thành công; bộ nghiệp vụ riêng đối chiếu kết quả và dữ liệu. Không in giấy hoặc chuyển tiền thật. Nhật ký cuối: ../TichHopKho_QA/KetQuaCuoi_20261029/bao-phu-nut-cuoi-20261029.json.

SQL thật giữ dữ liệu và tài khoản, bản SQL đầy đủ đã cài thử và đối chiếu khớp. Đã gỡ đúng ba database thử CodexChecks, CodexKhoMigrationChecks, CodexKhoRoundTrip; giữ QuanLySuaXe và các bài khác. Kết quả chạy thử đã chuyển ra ngoài project.

Hai Word riêng đã cập nhật và kiểm tra trực quan 42 trang: mô tả chức năng 12 trang; ôn logic code và trình bày bảo vệ 30 trang với mục lục 28 liên kết. Nội dung khớp các sửa cuối.
