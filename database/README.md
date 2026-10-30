# Database QuanLySuaXe

Máy chưa có dữ liệu dùng **[QuanLySuaXe_DayDu_20261029.sql](QuanLySuaXe_DayDu_20261029.sql)**: đủ cấu trúc và dữ liệu của **20 bảng**, không có cột cho phép NULL. Bản xuất giữ mọi tài khoản và dữ liệu đang dùng, gồm các hồ sơ được tạo thêm; không chỉ giới hạn ở bộ năm nhân viên mẫu.

- [Hướng dẫn cài máy mới và cập nhật máy đang dùng](CAI_DAT_CHO_NHOM.md).
- [Migration bổ sung kho](CapNhatKho_20261029.sql) dành cho database 18 bảng hiện tại.
- [Số dòng và SHA-256 dữ liệu từng bảng](QuanLySuaXe_DoiChieu_20261029.json).
- [Gói SQL và hướng dẫn để gửi nhóm](QuanLySuaXe_ChoNhom_20261029.zip).

Database hiện có sáu nhân viên. Năm tài khoản mẫu **minh, an, huy, mai, cuong** tương ứng **Minh, An, Huy, Mai, Cường** vẫn có mật khẩu `123`; hồ sơ **Hường / Huong** tạo thêm được giữ nguyên. Cập nhật kho không đặt lại mật khẩu hay xóa nhân viên. Mật khẩu nằm trong `NhanVien.MatKhau`, băm SHA-256 dạng HEX 64 ký tự. Tài khoản mới qua ứng dụng mặc định `123`; người Nghỉ việc không đăng nhập được.

| Nhóm | Bảng |
| --- | --- |
| Nhân sự và công | ChucVu, KhoanDieuChinh, ChucVuDieuChinh, NhanVien, CaLam, ChiTietCaLam |
| Khách, xe và phân công | KhachHang, Xe, ChiTietPhanCong |
| Dịch vụ và nhập kho | DichVu, ChiTietDichVu, PhuTung, NhaCungCap, PhieuNhap, ChiTietPhieuNhap |
| Lịch sử kho mới | DieuChinhTonKho, DieuChinhGiaBan |
| Tiếp nhận và thanh toán | PhieuTiepNhan, ChiTietPhieuTiepNhan, HoaDon |

Kho tích hợp từ commit **4ac7066** ngày **29/10/2026** của repo nguồn. Migration chỉ thêm hai bảng lịch sử và đổi điều kiện giá phụ tùng để giá nhập/bán đều dương, cho phép bán dưới giá nhập khi ứng dụng xác nhận. Trạng thái của các bảng cũ vẫn là chữ; không chuyển sang BIT/INT. `GiaTriChenhLech` là cột computed `PERSISTED NOT NULL`, không ghi trong INSERT.

Điều chỉnh tồn/giá ghi mã DC/DG, lý do, ngày và người thực hiện từ phiên đăng nhập. Sửa/xóa phiếu nhập điều chỉnh tồn theo chênh lệch, chặn thiếu tồn và ghi lịch sử; giữ danh mục/giá hiện tại của phụ tùng đã có. Dấu vết SHA-256 phát hiện phiếu bị phiên khác sửa. Chỉ xóa được nhà cung cấp chưa được phụ tùng tham chiếu. Excel Kho có ba sheet phụ tùng, điều chỉnh tồn và điều chỉnh giá.

Tiền hóa đơn lấy từ `ChiTietPhieuTiepNhan.ThanhTien`; chỉ lưu hóa đơn khi xác nhận đã thu tiền. Công lấy giờ thực tế từ `ChiTietCaLam`, ba ca từ `CaLam`, lương tháng từ `NhanVien.Luong`; giờ chuẩn mặc định 208 có thể chỉnh trên màn hình.

App.config mặc định `(localdb)\MSSQLLocalDB / QuanLySuaXe`. LocalDB của từng máy là dữ liệu độc lập; dùng chung trực tiếp cần cùng kết nối một SQL Server đã được cấp quyền.

Máy đã có dữ liệu không chạy bản SQL tạo mới để thay database. Dùng công cụ cập nhật có backup COPY_ONLY/CHECKSUM, VERIFYONLY, snapshot SQL và đối chiếu SHA trước/sau. Không nhập `database/nguon/QuanLySuaXe.sql` hoặc SQL nguyên bản của repo kho đè lên dữ liệu hiện tại.
