# Đối chiếu SQL và đề xuất nghiệp vụ

## Schema ban đầu còn thiếu gì?

Đã đọc trực tiếp schema của `QuanLySuaXe` trên `(localdb)\MSSQLLocalDB` và đối chiếu 14 bảng với script trong repo. Đã sao lưu, kiểm tra checksum và áp dụng migration lên database thật; sau nâng cấp có 19 bảng, giữ nguyên 4 nhân viên và 3 hóa đơn với tổng 770.000đ. Lỗi ban đầu là công cụ LocalDB báo Stopped trong khi tiến trình SQL vẫn chạy; đã kết nối qua pipe hiện hành và bổ sung đường kết nối dự phòng.

| Yêu cầu | Thiết kế cũ | Đã bổ sung |
| --- | --- | --- |
| Một dịch vụ cần một bộ nhiều phụ tùng | `DichVu.MaPhuTung` chỉ biểu diễn một phụ tùng, mặc định một đơn vị | `DinhMucDichVu`: nhiều phụ tùng và số lượng; gom nhu cầu chung để kiểm tra tồn |
| Hóa đơn không đổi khi danh mục tăng giá | Chi tiết tiếp nhận chỉ chốt giá dịch vụ, chưa chốt phụ tùng hoặc tên | Chốt tên dịch vụ, tiền nhiệm vụ; `PhuTungTiepNhan` chốt tên, lượng và giá phụ tùng; trigger khóa nội dung đã phát hành |
| Tồn kho không âm khi nhiều người lưu | Có CHECK không âm nhưng chưa có giao dịch giữa nhiều bảng | Transaction Serializable, khóa dữ liệu, UPDATE có điều kiện đủ tồn, rollback cả phiếu khi thất bại |
| Giới hạn thợ 2 xe, thợ chính 3 xe | Có `SoXeDangSuaToiDa`, chưa kiểm tra phân công đồng thời; thiếu chức vụ thợ chính | Bốn vai trò trong UI; bổ sung thợ chính, giới hạn cấu hình từng nhân viên, kiểm tra sức chứa khi lưu |
| Trả lương theo nhiệm vụ | Chưa có định mức và tiền đã chốt | `DichVu.LuongNhiemVu`, chi tiết phiếu chốt mức lương, `PhanCongTho.TienCong` và ngày ghi nhận khi hoàn thành |
| Hai ca cố định, muộn/vắng/đổi ca | Chưa có bảng ca hoặc công | `CaLam`, `BangCong`, `vBangCong`, `LichSuBangCong`; lương giờ theo dòng công; xuất Excel |
| Hẹn trả và loại xe | Thiếu hẹn trả và loại xe tách biệt | `HenTra`, `Xe.LoaiXe`; báo cáo quá hẹn |

Dữ liệu thật dùng trạng thái `Đã hoàn thành`, khác tên `Hoàn thành` trong ứng dụng. Migration đã chuẩn hóa hai cách gọi này để không tính nhầm xe đã xong vào số xe đang nhận; giữ nguyên ngày và tiền.

Migration bổ sung 5 bảng, đưa cấu trúc từ 14 lên 19 bảng. Không xóa dữ liệu, không đổi hàng loạt vai trò/giới hạn cũ, không tự gán lương hoặc diễn giải giá lịch sử. Các cột mới có giá trị mặc định hoặc cho phép NULL để giữ chứng từ cũ.

## Ý tưởng thực tế cho bảng công

1. Dùng chi tiết công để nhập ngày, ca, giờ vào/ra và lý do điều chỉnh; tổng hợp lương để chốt kỳ. Hai trang hiện nằm chung trong bộ chọn Chi tiết công / Tổng hợp lương.
2. Với thợ phụ hoặc bán thời gian, trả lương giờ; thợ nhận việc có thể hưởng thêm mức cố định theo dịch vụ. Phần mềm hiện cộng hai khoản này. Có thể đặt một khoản bằng 0 nếu cửa hàng chỉ áp dụng một cách tính.
3. Giữ 30 phút giao ca để bàn giao xe; nếu một người làm cả hai ca thì chốt giờ ra ca sáng lúc 13:30 hoặc giờ vào ca tối lúc 14:00. Không tính hai lần cùng khoảng thời gian.
4. Đi muộn/về sớm trừ theo giờ thực tế. Chỉ dùng Cộng/trừ phút cho bù giờ hoặc điều chỉnh đã được duyệt, có lý do và lịch sử. Vắng ca tính 0; không tự suy diễn thành một khoản phạt tiền.

## Phần nên bổ sung tiếp

- **Chốt kỳ lương:** bảng kỳ lương, chi tiết bảng lương, khoản thưởng/phạt/ứng lương và người duyệt. Bảng công hiện có thể được chủ sửa và có lịch sử, nhưng chưa khóa kỳ đã trả lương.
- **Lịch ca dự kiến:** tách lịch đăng ký ca, xin đổi ca và phê duyệt khỏi công thực tế. Hiện việc đổi ca là sửa dòng công đã lưu, chưa có quy trình xin duyệt.
- **Sổ xuất/nhập và tồn theo lô:** hiện tồn là số gộp trên `PhuTung`; chưa trừ theo từng lô/hạn sử dụng. Bổ sung `PhieuXuat`, chi tiết lô, kiểm kê và điều chỉnh tồn sẽ giúp loại trừ phụ tùng hết hạn.
- **Thanh toán theo ngày thực thu:** thêm bảng giao dịch, thời gian và số tiền thanh toán, mã chuyển khoản. Hiện chỉ ghi trạng thái/phương thức một lần; thống kê không phân bổ được khách trả nhiều lần hay ngày thu khác ngày hóa đơn.
- **Bảo hành và sửa phát sinh:** tách phiếu bảo hành, bổ sung dịch vụ cần khách duyệt và chứng từ điều chỉnh hóa đơn. Hiện phiếu chốt khi tiếp nhận, hóa đơn giữ nguyên; không có sửa phát sinh sau phát hành.
- **Ô tô và độ khó:** thêm năng lực theo loại xe hoặc điểm tải công việc. Hiện cả xe máy và ô tô chiếm một chỗ trong giới hạn đã thống nhất 2/3.
- **Chia tiền nhiều thợ:** hiện UI phân công một thợ phụ trách mỗi phiếu. Database vẫn giữ khả năng nhiều thợ cho dữ liệu cũ; nếu dùng nhiều người trên một xe cần quy định tỷ lệ chia tiền nhiệm vụ và kiểm tra tổng tỷ lệ 100%.
- **Cấp quyền tùy biến:** hiện dùng bốn bộ quyền cố định được thực thi trong mã. Nếu muốn chỉnh từng quyền trên UI và lưu SQL, thêm `ChucNang`, `QuyenChucVu` và lịch sử thay đổi; không dùng checkbox chỉ lưu trong bộ nhớ như trước.

Khuyến nghị ưu tiên chốt kỳ lương và sổ tồn theo lô sau khi đối soát phiếu cũ. Hai phần này giúp số thống kê đối chiếu được với tiền trả và phụ tùng thực tế.
