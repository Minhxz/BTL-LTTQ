Kho được chuyển từ https://github.com/cuong60126-source/Quan-ly-kho-cua-hang-sua-xe, commit 40e41b8 (nhánh main, tải ngày 27/10/2026).

Các Designer và nhóm chức năng kho, phiếu nhập/chi tiết, lập phiếu nhập, thêm nhà cung cấp được đưa vào project hiện tại và chuyển từ .NET 10 sang .NET Framework 4.8. Kết nối dùng Classes chung, truy vấn có tham số, nhập hàng trong một giao dịch; giao diện được sắp xếp lại theo màu xanh của Tiếp nhận. Không đưa bin/obj hay lớp kết nối riêng của repo nguồn vào bài.

Bố cục cuối được khai báo trong bốn file Designer; không xóa Controls rồi dựng lại khi chạy. In phiếu nhập sử dụng mẫu A4/xem trước dùng chung, Excel là thao tác riêng. Các handler trống và nút không dùng đã bỏ. Nhập phụ tùng mới có kiểm tra trùng mã để không ghi đè phụ tùng đã được tạo ở phiên khác.
