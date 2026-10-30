# Nguồn và tích hợp chức năng kho

Nguồn: [Quan-ly-kho-cua-hang-sua-xe](https://github.com/cuong60126-source/Quan-ly-kho-cua-hang-sua-xe), nhánh `main`, commit **4ac7066c69c08f642e8b6fa92ca3d2cee79d0f56** (viết gọn **4ac7066**), lấy ngày **29/10/2026**.

Kho được đưa vào ứng dụng chính .NET Framework 4.8, dùng lớp kết nối/phiên/quyền chung; các form và Designer nằm tại `QLyCuaHangSuaXe/QLyCuaHangSuaXe/DanhMuc/Kho`. Giao diện giữ màu xanh và cách bố trí của Tiếp nhận. Không đưa `bin/obj`, project .NET 10 hoặc kết nối riêng của repo nguồn vào ứng dụng.

Phạm vi tích hợp gồm danh mục phụ tùng, phiếu nhập/chi tiết, lập và sửa/xóa phiếu nhập, thêm/xóa NCC chưa được dùng, điều chỉnh tồn, điều chỉnh giá bán, hai thẻ lịch sử và xuất Excel ba sheet. Người điều chỉnh lấy từ phiên đăng nhập; lý do bắt buộc. Giá bán dưới giá nhập cần xác nhận. Mã lịch sử tự cấp theo DC/DG.

Ghi sổ và sửa/xóa phiếu nhập xử lý trong giao dịch. Sửa tính phần chênh lệch số lượng; xóa rút lượng nhập khi kho còn đủ, giữ danh mục phụ tùng. Sửa/xóa không ghi đè các giá danh mục hiện tại của phụ tùng đã có. Dấu vết SHA-256 phát hiện phiếu đã bị phiên khác sửa. Mọi thay đổi tồn có lịch sử; giá trị chênh lệch do SQL tính từ lượng trước/sau và giá nhập lúc ghi.

Database hiện tại giữ 18 bảng cũ và bổ sung `DieuChinhTonKho`, `DieuChinhGiaBan`, thành 20 bảng. Các cột đều NOT NULL; `GiaTriChenhLech` là computed PERSISTED NOT NULL. Trạng thái cũ vẫn là chữ, không lấy BIT/INT từ SQL nguồn. Toàn bộ tài khoản và dữ liệu hiện có, gồm sáu nhân viên, được giữ; năm tài khoản mẫu tên/123 vẫn sử dụng được.

Máy trắng dùng `database/QuanLySuaXe_DayDu_20261029.sql`. Máy đang có bản 18 bảng dùng migration additive `database/CapNhatKho_20261029.sql` qua helper có sao lưu và đối chiếu; không chạy SQL nguyên bản của repo kho đè lên database đang dùng. Bản đầy đủ được xuất lại từ dữ liệu thực tế sau tích hợp.

In phiếu nhập mở mẫu A4/xem trước dùng chung. Excel là thao tác riêng; Excel ở Kho có Phụ tùng, Điều chỉnh tồn và Điều chỉnh giá bán. Các bảng lịch sử và sheet tồn có tổng thừa, thiếu và chênh lệch. Bố cục được khai báo trong Designer, không xóa Controls rồi dựng lại khi chạy.
