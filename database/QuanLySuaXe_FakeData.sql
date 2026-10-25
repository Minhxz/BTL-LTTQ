/*
    DỮ LIỆU MẪU QUẢN LÝ SỬA XE
    Cấu trúc: QuanLySuaXe.sql (14 bảng, đã bổ sung NhaCungCap).

    Cách chạy trong SQL Server Management Studio:
    1. Nếu chưa có database/bảng, chạy QuanLySuaXe.sql trước một lần.
    2. Chạy toàn bộ file này trên cùng SQL Server.

    Mã mẫu bắt đầu bằng DM; mọi người, số điện thoại và địa chỉ đều giả lập.
    Chỉ thêm bản ghi chưa tồn tại, không xóa hoặc ghi đè dữ liệu đã có.
    Riêng các dòng DMPN đã tạo từ bản mẫu cũ và chưa có MaNCC được bổ sung
    đúng nhà cung cấp mẫu; các chứng từ khác không bị sửa.
    Có thể chạy lại; toàn bộ phần thêm dữ liệu nằm trong một transaction.

    Tài khoản mẫu: demo_admin, demo_thungan, demo_kho, demo_tho01,
                  demo_tho02, demo_nghiviec.
    Mật khẩu mẫu: Demo1234 (lưu PBKDF2-SHA256, 600000 vòng, salt riêng).
    demo_nghiviec có trạng thái Đã nghỉ để thử trường hợp không được đăng nhập.
    File này chỉ nạp dữ liệu; ứng dụng cần xác thực bằng bảng NhanVien để dùng
    các tài khoản mẫu, thay cho tài khoản đang kiểm tra trong mã nguồn.

    Tồn kho mẫu = số lượng nhập - phụ tùng đã dùng cho ba phiếu hoàn thành.
    Mỗi dịch vụ có MaPhuTung dùng một phụ tùng cho mỗi đơn vị dịch vụ.
    Tiền công và tiền phụ tùng được tính riêng theo đúng cấu trúc HoaDon.
*/

USE QuanLySuaXe;
GO

SET NOCOUNT ON;
SET XACT_ABORT ON;

BEGIN TRY
    IF DB_NAME() <> N'QuanLySuaXe'
        THROW 50002, N'Chỉ được nạp dữ liệu mẫu vào database QuanLySuaXe.', 1;

    BEGIN TRANSACTION;

    IF EXISTS (
        SELECT 1
        FROM (VALUES
            (N'ChucVu'), (N'NhanVien'), (N'KhachHang'), (N'Xe'),
            (N'PhuTung'), (N'DichVu'), (N'PhieuTiepNhan'),
            (N'ChiTietPhieuTiepNhan'), (N'PhanCongTho'), (N'HoaDon'),
            (N'ChiTietHoaDon'), (N'PhieuNhap'), (N'NhaCungCap'), (N'ChiTietPhieuNhap')
        ) AS bang(TenBang)
        WHERE OBJECT_ID(N'dbo.' + bang.TenBang, N'U') IS NULL
    )
        THROW 50001, N'Thiếu bảng. Hãy tạo cấu trúc bằng file QuanLySuaXe.sql trước khi nạp dữ liệu mẫu.', 1;

    IF COL_LENGTH(N'dbo.ChiTietPhieuNhap', N'MaNCC') IS NULL
        THROW 50003, N'Hãy chạy lại QuanLySuaXe.sql để bổ sung liên kết nhà cung cấp trước.', 1;
    DECLARE @PhuTung TABLE (
        MaPhuTung VARCHAR(10) PRIMARY KEY, TenPhuTung NVARCHAR(100),
        DonViTinh NVARCHAR(20), DonGiaNhap DECIMAL(18,2),
        DonGiaBan DECIMAL(18,2), TrangThai NVARCHAR(30)
    );
    INSERT INTO @PhuTung VALUES
        ('DMPT01', N'Dầu nhớt xe máy 0,8 lít', N'Chai', 80000, 100000, N'Đang sử dụng'),
        ('DMPT02', N'Bugi xe máy', N'Cái', 25000, 40000, N'Đang sử dụng'),
        ('DMPT03', N'Má phanh trước', N'Bộ', 60000, 90000, N'Đang sử dụng'),
        ('DMPT04', N'Lọc gió xe tay ga', N'Cái', 35000, 50000, N'Đang sử dụng'),
        ('DMPT05', N'Ắc quy xe máy 12V', N'Bình', 280000, 350000, N'Đang sử dụng'),
        ('DMPT06', N'Dây curoa xe tay ga', N'Sợi', 150000, 200000, N'Đang sử dụng'),
        ('DMPT07', N'Bóng đèn pha', N'Cái', 95000, 125000, N'Đang sử dụng'),
        ('DMPT08', N'Gương chiếu hậu mẫu cũ', N'Cặp', 40000, 60000, N'Ngừng sử dụng');

    DECLARE @DichVu TABLE (
        MaDichVu VARCHAR(10) PRIMARY KEY, TenDichVu NVARCHAR(100),
        MoTa NVARCHAR(300), DonGiaDichVu DECIMAL(18,2), ThoiGianDuKien INT,
        MaPhuTung VARCHAR(10), TrangThai NVARCHAR(30)
    );
    INSERT INTO @DichVu VALUES
        ('DMDV01', N'Thay dầu máy', N'Tiền công thay dầu; dầu nhớt tính riêng.', 30000, 15, 'DMPT01', N'Đang sử dụng'),
        ('DMDV02', N'Thay bugi', N'Thay và kiểm tra đánh lửa; bugi tính riêng.', 20000, 15, 'DMPT02', N'Đang sử dụng'),
        ('DMDV03', N'Thay má phanh trước', N'Thay má phanh, căn chỉnh và thử phanh.', 40000, 30, 'DMPT03', N'Đang sử dụng'),
        ('DMDV04', N'Thay lọc gió', N'Thay lọc gió và vệ sinh hộp lọc.', 20000, 20, 'DMPT04', N'Đang sử dụng'),
        ('DMDV05', N'Thay ắc quy', N'Lắp ắc quy và kiểm tra điện sạc.', 30000, 20, 'DMPT05', N'Đang sử dụng'),
        ('DMDV06', N'Thay dây curoa', N'Thay dây curoa, vệ sinh bộ truyền động.', 60000, 45, 'DMPT06', N'Đang sử dụng'),
        ('DMDV07', N'Kiểm tra hệ thống điện', N'Đo điện áp, kiểm tra cầu chì và dây dẫn.', 50000, 30, NULL, N'Đang sử dụng'),
        ('DMDV08', N'Rửa xe chi tiết', N'Dịch vụ mẫu đang tạm ngừng.', 30000, 25, NULL, N'Ngừng sử dụng');

    DECLARE @Nhap TABLE (
        MaPhieuNhap VARCHAR(10), MaPhuTung VARCHAR(10), MaNCC VARCHAR(10),
        SoLuong INT, DonGiaNhap DECIMAL(18,2), HanSuDung DATE,
        PRIMARY KEY (MaPhieuNhap, MaPhuTung)
    );
    INSERT INTO @Nhap VALUES
        ('DMPN01', 'DMPT01', 'DMNCC01', 30, 80000, '20281001'),
        ('DMPN01', 'DMPT02', 'DMNCC02', 20, 25000, NULL),
        ('DMPN01', 'DMPT03', 'DMNCC02', 15, 60000, NULL),
        ('DMPN02', 'DMPT04', 'DMNCC02', 20, 35000, NULL),
        ('DMPN02', 'DMPT05', 'DMNCC03', 8, 280000, '20281010'),
        ('DMPN02', 'DMPT06', 'DMNCC03', 10, 150000, NULL),
        ('DMPN03', 'DMPT07', 'DMNCC03', 12, 95000, NULL),
        ('DMPN03', 'DMPT08', 'DMNCC02', 10, 40000, NULL);

    DECLARE @YeuCau TABLE (
        MaPhieuTiepNhan VARCHAR(10), MaDichVu VARCHAR(10),
        SoLuong INT, DonGiaDichVu DECIMAL(18,2),
        PRIMARY KEY (MaPhieuTiepNhan, MaDichVu)
    );
    INSERT INTO @YeuCau VALUES
        ('DMTN01', 'DMDV01', 1, 30000), ('DMTN01', 'DMDV02', 1, 20000),
        ('DMTN02', 'DMDV03', 1, 40000), ('DMTN02', 'DMDV04', 1, 20000),
        ('DMTN03', 'DMDV05', 1, 30000), ('DMTN03', 'DMDV07', 1, 50000),
        ('DMTN04', 'DMDV06', 1, 60000), ('DMTN04', 'DMDV04', 1, 20000),
        ('DMTN05', 'DMDV01', 1, 30000),
        ('DMTN06', 'DMDV02', 1, 20000), ('DMTN06', 'DMDV07', 1, 50000);

    DECLARE @HoaDon TABLE (
        MaHoaDon VARCHAR(10) PRIMARY KEY, MaPhieuTiepNhan VARCHAR(10),
        NgayLap DATETIME2, TrangThaiThanhToan NVARCHAR(30),
        PhuongThucThanhToan NVARCHAR(30), MaNhanVien VARCHAR(10)
    );
    INSERT INTO @HoaDon VALUES
        ('DMHD01', 'DMTN01', '2026-10-20T09:05:00', N'Đã thanh toán', N'Tiền mặt', 'DMNV02'),
        ('DMHD02', 'DMTN02', '2026-10-21T10:15:00', N'Đã thanh toán', N'Chuyển khoản', 'DMNV02'),
        ('DMHD03', 'DMTN03', '2026-10-22T15:10:00', N'Chưa thanh toán', NULL, 'DMNV02');
    INSERT INTO dbo.ChucVu (MaChucVu, TenChucVu, MoTa)
    SELECT v.* FROM (VALUES
        ('DMCV01', N'Chủ cửa hàng', N'Quản lý hoạt động cửa hàng.'),
        ('DMCV02', N'Thu ngân', N'Tiếp nhận xe, lập hóa đơn và thu tiền.'),
        ('DMCV03', N'Kho', N'Quản lý nhập kho và tồn phụ tùng.'),
        ('DMCV04', N'Thợ', N'Kiểm tra và sửa chữa xe được phân công.')
    ) AS v(MaChucVu, TenChucVu, MoTa)
    WHERE NOT EXISTS (SELECT 1 FROM dbo.ChucVu AS t WHERE t.MaChucVu = v.MaChucVu);
    INSERT INTO dbo.NhanVien
        (MaNhanVien, MaChucVu, TenNhanVien, TenDangNhap, MatKhauHash,
         SoDienThoai, TrangThai, SoXeDangSuaToiDa)
    SELECT v.* FROM (VALUES
        ('DMNV01', 'DMCV01', N'Nguyễn Quang Minh', 'demo_admin',
         'PBKDF2-SHA256$600000$WaWcJdk5KD+cN9pnhUM65A==$xrykyzU1l6QixzmDdAKlWZazfHNdXydzoRCsg2OSZJk=',
         '0900000001', N'Đang làm', 0),
        ('DMNV02', 'DMCV02', N'Trần Thu Hà', 'demo_thungan',
         'PBKDF2-SHA256$600000$nfyi1c/5cQuWx8fj9pfNRQ==$Y8WAL+Htw2PMuHeEITSMKQ9ljmeXNsAdtG8R5HA/y00=',
         '0900000002', N'Đang làm', 0),
        ('DMNV03', 'DMCV03', N'Lê Đức Huy', 'demo_kho',
         'PBKDF2-SHA256$600000$8B4rqvCGNKa11qfP+3G3Jg==$EN11ho7vf/miQsqNYMbIRmcKdU8nMHToK1WF7rHLkoE=',
         '0900000003', N'Đang làm', 0),
        ('DMNV04', 'DMCV04', N'Phạm Văn Nam', 'demo_tho01',
         'PBKDF2-SHA256$600000$Y/aq3qLMD0nJEapiy924+g==$ex9UJQ7dw/fPKb9SpBRqItZhbguiJTNd8GX3MeEjUIU=',
         '0900000004', N'Đang làm', 3),
        ('DMNV05', 'DMCV04', N'Hoàng Anh Tuấn', 'demo_tho02',
         'PBKDF2-SHA256$600000$9S6lq+umMSn/VHcOq0oBKQ==$pmYcOBR+huSj+CSAnoCEEXRg8Lflwg5AnpY/FSxRCqo=',
         '0900000005', N'Đang làm', 2),
        ('DMNV06', 'DMCV04', N'Đỗ Minh Long', 'demo_nghiviec',
         'PBKDF2-SHA256$600000$4G9SHippXkxSgzinxlAOsg==$JiGxtSaZ0P5bGtVsvOen1YnSDwJOu7mNAkuDj+Q1/W8=',
         '0900000006', N'Đã nghỉ', 0)
    ) AS v(MaNhanVien, MaChucVu, TenNhanVien, TenDangNhap, MatKhauHash,
           SoDienThoai, TrangThai, SoXeDangSuaToiDa)
    WHERE NOT EXISTS (SELECT 1 FROM dbo.NhanVien AS t WHERE t.MaNhanVien = v.MaNhanVien);
    INSERT INTO dbo.KhachHang (MaKhachHang, TenKhachHang, SoDienThoai, DiaChi)
    SELECT v.* FROM (VALUES
        ('DMKH01', N'Nguyễn Hải An', '0910000001', N'Số 10 đường Mẫu, Hà Nội'),
        ('DMKH02', N'Trần Ngọc Mai', '0910000002', N'Số 20 đường Mẫu, Hà Nội'),
        ('DMKH03', N'Lê Hoàng Phúc', '0910000003', N'Số 30 đường Mẫu, Hà Nội'),
        ('DMKH04', N'Phạm Thanh Bình', '0910000004', N'Số 40 đường Mẫu, Hà Nội'),
        ('DMKH05', N'Vũ Thu Trang', '0910000005', N'Số 50 đường Mẫu, Hà Nội')
    ) AS v(MaKhachHang, TenKhachHang, SoDienThoai, DiaChi)
    WHERE NOT EXISTS (SELECT 1 FROM dbo.KhachHang AS t WHERE t.MaKhachHang = v.MaKhachHang);
    INSERT INTO dbo.Xe
        (MaXe, MaKhachHang, BienSo, HangXe, DongXe, NamSanXuat, MauSac, SoKhung)
    SELECT v.* FROM (VALUES
        ('DMXE01', 'DMKH01', '29A1-900.01', N'Honda', N'Wave Alpha', 2021, N'Đỏ', 'DEMO-SOKHUNG-001'),
        ('DMXE02', 'DMKH02', '29B1-900.02', N'Honda', N'Vision', 2022, N'Trắng', 'DEMO-SOKHUNG-002'),
        ('DMXE03', 'DMKH03', '29C1-900.03', N'Yamaha', N'Exciter', 2020, N'Xanh', 'DEMO-SOKHUNG-003'),
        ('DMXE04', 'DMKH04', '29D1-900.04', N'Honda', N'Air Blade', 2023, N'Đen', 'DEMO-SOKHUNG-004'),
        ('DMXE05', 'DMKH05', '29E1-900.05', N'Yamaha', N'Janus', 2022, N'Xám', 'DEMO-SOKHUNG-005'),
        ('DMXE06', 'DMKH01', '29F1-900.06', N'Honda', N'Future', 2019, N'Nâu', 'DEMO-SOKHUNG-006')
    ) AS v(MaXe, MaKhachHang, BienSo, HangXe, DongXe, NamSanXuat, MauSac, SoKhung)
    WHERE NOT EXISTS (SELECT 1 FROM dbo.Xe AS t WHERE t.MaXe = v.MaXe);
    INSERT INTO dbo.PhuTung
        (MaPhuTung, TenPhuTung, DonViTinh, DonGiaNhap, DonGiaBan, SoLuongTon, TrangThai)
    SELECT p.MaPhuTung, p.TenPhuTung, p.DonViTinh, p.DonGiaNhap, p.DonGiaBan,
           ISNULL((SELECT SUM(n.SoLuong) FROM @Nhap AS n WHERE n.MaPhuTung = p.MaPhuTung), 0)
           - ISNULL((SELECT SUM(y.SoLuong)
                     FROM @YeuCau AS y
                     JOIN @HoaDon AS h ON h.MaPhieuTiepNhan = y.MaPhieuTiepNhan
                     JOIN @DichVu AS d ON d.MaDichVu = y.MaDichVu
                     WHERE d.MaPhuTung = p.MaPhuTung), 0),
           p.TrangThai
    FROM @PhuTung AS p
    WHERE NOT EXISTS (SELECT 1 FROM dbo.PhuTung AS t WHERE t.MaPhuTung = p.MaPhuTung);
    INSERT INTO dbo.DichVu
        (MaDichVu, TenDichVu, MoTa, DonGiaDichVu, ThoiGianDuKien, MaPhuTung, TrangThai)
    SELECT d.* FROM @DichVu AS d
    WHERE NOT EXISTS (SELECT 1 FROM dbo.DichVu AS t WHERE t.MaDichVu = d.MaDichVu);
    INSERT INTO dbo.PhieuTiepNhan
        (MaPhieuTiepNhan, MaKhachHang, MaXe, MaNhanVien, ThoiGianNhan,
         ThoiGianHoanThanh, MoTaTinhTrangXe, TrangThai, GhiChu)
    SELECT v.* FROM (VALUES
        ('DMTN01', 'DMKH01', 'DMXE01', 'DMNV02', '2026-10-20T08:00:00', '2026-10-20T09:00:00',
         N'Xe khó nổ, đã đến kỳ thay dầu.', N'Đã hoàn thành', N'Đã bàn giao và thu tiền.'),
        ('DMTN02', 'DMKH02', 'DMXE02', 'DMNV02', '2026-10-21T09:00:00', '2026-10-21T10:10:00',
         N'Phanh trước kêu, lọc gió bẩn.', N'Đã hoàn thành', N'Đã kiểm tra phanh sau sửa chữa.'),
        ('DMTN03', 'DMKH03', 'DMXE03', 'DMNV02', '2026-10-22T14:00:00', '2026-10-22T15:05:00',
         N'Ắc quy yếu, đề điện không hoạt động.', N'Đã hoàn thành', N'Khách chưa thanh toán.'),
        ('DMTN04', 'DMKH04', 'DMXE04', 'DMNV02', '2026-10-24T08:20:00', NULL,
         N'Bộ truyền động rung khi tăng ga.', N'Đang sửa', N'Đang tháo kiểm tra dây curoa; chưa xuất phụ tùng.'),
        ('DMTN05', 'DMKH05', 'DMXE05', 'DMNV02', '2026-10-24T09:30:00', NULL,
         N'Khách yêu cầu thay dầu định kỳ.', N'Mới tiếp nhận', N'Chờ phân công thợ.'),
        ('DMTN06', 'DMKH01', 'DMXE06', 'DMNV02', '2026-10-24T08:00:00', NULL,
         N'Xe thỉnh thoảng chết máy, đánh lửa yếu.', N'Chờ phụ tùng', N'Chờ loại bugi phù hợp; chưa xuất phụ tùng.')
    ) AS v(MaPhieuTiepNhan, MaKhachHang, MaXe, MaNhanVien, ThoiGianNhan,
           ThoiGianHoanThanh, MoTaTinhTrangXe, TrangThai, GhiChu)
    WHERE NOT EXISTS (SELECT 1 FROM dbo.PhieuTiepNhan AS t WHERE t.MaPhieuTiepNhan = v.MaPhieuTiepNhan);
    INSERT INTO dbo.ChiTietPhieuTiepNhan
        (MaPhieuTiepNhan, MaDichVu, SoLuong, DonGiaDichVu)
    SELECT y.* FROM @YeuCau AS y
    WHERE NOT EXISTS (
        SELECT 1 FROM dbo.ChiTietPhieuTiepNhan AS t
        WHERE t.MaPhieuTiepNhan = y.MaPhieuTiepNhan AND t.MaDichVu = y.MaDichVu
    );
    INSERT INTO dbo.PhanCongTho
        (MaPhieuTiepNhan, MaNhanVien, VaiTro, TrangThaiCongViec, ThoiGianBatDau, ThoiGianKetThuc)
    SELECT v.* FROM (VALUES
        ('DMTN01', 'DMNV04', N'Thợ chính', N'Đã hoàn thành', '2026-10-20T08:10:00', '2026-10-20T08:55:00'),
        ('DMTN02', 'DMNV05', N'Thợ chính', N'Đã hoàn thành', '2026-10-21T09:10:00', '2026-10-21T10:05:00'),
        ('DMTN03', 'DMNV04', N'Thợ chính', N'Đã hoàn thành', '2026-10-22T14:10:00', '2026-10-22T15:00:00'),
        ('DMTN03', 'DMNV05', N'Thợ hỗ trợ', N'Đã hoàn thành', '2026-10-22T14:15:00', '2026-10-22T14:55:00'),
        ('DMTN04', 'DMNV04', N'Thợ chính', N'Đang sửa', '2026-10-24T08:30:00', NULL),
        ('DMTN06', 'DMNV05', N'Thợ chính', N'Chờ phụ tùng', '2026-10-24T08:15:00', NULL)
    ) AS v(MaPhieuTiepNhan, MaNhanVien, VaiTro, TrangThaiCongViec, ThoiGianBatDau, ThoiGianKetThuc)
    WHERE NOT EXISTS (
        SELECT 1 FROM dbo.PhanCongTho AS t
        WHERE t.MaPhieuTiepNhan = v.MaPhieuTiepNhan AND t.MaNhanVien = v.MaNhanVien
    );
    INSERT INTO dbo.HoaDon
        (MaHoaDon, MaPhieuTiepNhan, NgayLap, TongTienDichVu, TongTienPhuTung,
         TongTien, TrangThaiThanhToan, PhuongThucThanhToan, MaNhanVien)
    SELECT h.MaHoaDon, h.MaPhieuTiepNhan, h.NgayLap, tien.DichVu, tien.PhuTung,
           tien.DichVu + tien.PhuTung, h.TrangThaiThanhToan, h.PhuongThucThanhToan, h.MaNhanVien
    FROM @HoaDon AS h
    CROSS APPLY (
        SELECT SUM(y.SoLuong * y.DonGiaDichVu) AS DichVu,
               SUM(y.SoLuong * ISNULL(p.DonGiaBan, 0)) AS PhuTung
        FROM @YeuCau AS y
        JOIN @DichVu AS d ON d.MaDichVu = y.MaDichVu
        LEFT JOIN @PhuTung AS p ON p.MaPhuTung = d.MaPhuTung
        WHERE y.MaPhieuTiepNhan = h.MaPhieuTiepNhan
    ) AS tien
    WHERE NOT EXISTS (SELECT 1 FROM dbo.HoaDon AS t WHERE t.MaHoaDon = h.MaHoaDon);
    INSERT INTO dbo.ChiTietHoaDon (MaHoaDon, MaDichVu, TenDichVu, SoLuong, DonGia)
    SELECT h.MaHoaDon, y.MaDichVu, d.TenDichVu, y.SoLuong, y.DonGiaDichVu
    FROM @HoaDon AS h
    JOIN @YeuCau AS y ON y.MaPhieuTiepNhan = h.MaPhieuTiepNhan
    JOIN @DichVu AS d ON d.MaDichVu = y.MaDichVu
    WHERE NOT EXISTS (
        SELECT 1 FROM dbo.ChiTietHoaDon AS t
        WHERE t.MaHoaDon = h.MaHoaDon AND t.MaDichVu = y.MaDichVu
    );
    INSERT INTO dbo.PhieuNhap (MaPhieuNhap, NgayNhap, MaNhanVienNhap, GhiChu, TongTien)
    SELECT v.MaPhieuNhap, v.NgayNhap, 'DMNV03', v.GhiChu,
           (SELECT SUM(n.SoLuong * n.DonGiaNhap) FROM @Nhap AS n WHERE n.MaPhieuNhap = v.MaPhieuNhap)
    FROM (VALUES
        ('DMPN01', '2026-10-01T08:00:00', N'Nhập dầu nhớt, bugi và má phanh mẫu.'),
        ('DMPN02', '2026-10-10T09:00:00', N'Nhập lọc gió, ắc quy và dây curoa mẫu.'),
        ('DMPN03', '2026-10-20T07:30:00', N'Nhập bóng đèn và gương mẫu.')
    ) AS v(MaPhieuNhap, NgayNhap, GhiChu)
    WHERE NOT EXISTS (SELECT 1 FROM dbo.PhieuNhap AS t WHERE t.MaPhieuNhap = v.MaPhieuNhap);
    INSERT INTO dbo.NhaCungCap (MaNCC, TenNCC, SoDienThoai, DiaChi)
    SELECT v.* FROM (VALUES
        ('DMNCC01', N'Nhà cung cấp dầu nhớt Minh Phát (mẫu)', '0920000001', N'Số 60 đường Mẫu, Hà Nội'),
        ('DMNCC02', N'Nhà cung cấp phụ tùng An Bình (mẫu)', '0920000002', N'Số 70 đường Mẫu, Hà Nội'),
        ('DMNCC03', N'Nhà cung cấp điện máy Thành Nam (mẫu)', '0920000003', N'Số 80 đường Mẫu, Hà Nội')
    ) AS v(MaNCC, TenNCC, SoDienThoai, DiaChi)
    WHERE NOT EXISTS (SELECT 1 FROM dbo.NhaCungCap AS t WHERE t.MaNCC = v.MaNCC);
    UPDATE t SET MaNCC = n.MaNCC
    FROM dbo.ChiTietPhieuNhap AS t
    JOIN @Nhap AS n ON n.MaPhieuNhap = t.MaPhieuNhap AND n.MaPhuTung = t.MaPhuTung
    WHERE t.MaNCC IS NULL;
    INSERT INTO dbo.ChiTietPhieuNhap (MaPhieuNhap, MaPhuTung, MaNCC, SoLuong, DonGiaNhap, HanSuDung)
    SELECT n.* FROM @Nhap AS n
    WHERE NOT EXISTS (
        SELECT 1 FROM dbo.ChiTietPhieuNhap AS t
        WHERE t.MaPhieuNhap = n.MaPhieuNhap AND t.MaPhuTung = n.MaPhuTung
    );

    COMMIT TRANSACTION;
    PRINT N'Đã nạp dữ liệu mẫu QuanLySuaXe. Các bản ghi có sẵn được giữ nguyên.';
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
    THROW;
END CATCH;
GO
SELECT N'ChucVu' AS TenBang, COUNT(*) AS SoBanGhiMau FROM dbo.ChucVu WHERE MaChucVu LIKE 'DMCV%'
UNION ALL SELECT N'NhanVien', COUNT(*) FROM dbo.NhanVien WHERE MaNhanVien LIKE 'DMNV%'
UNION ALL SELECT N'KhachHang', COUNT(*) FROM dbo.KhachHang WHERE MaKhachHang LIKE 'DMKH%'
UNION ALL SELECT N'Xe', COUNT(*) FROM dbo.Xe WHERE MaXe LIKE 'DMXE%'
UNION ALL SELECT N'PhuTung', COUNT(*) FROM dbo.PhuTung WHERE MaPhuTung LIKE 'DMPT%'
UNION ALL SELECT N'DichVu', COUNT(*) FROM dbo.DichVu WHERE MaDichVu LIKE 'DMDV%'
UNION ALL SELECT N'PhieuTiepNhan', COUNT(*) FROM dbo.PhieuTiepNhan WHERE MaPhieuTiepNhan LIKE 'DMTN%'
UNION ALL SELECT N'ChiTietPhieuTiepNhan', COUNT(*) FROM dbo.ChiTietPhieuTiepNhan WHERE MaPhieuTiepNhan LIKE 'DMTN%'
UNION ALL SELECT N'PhanCongTho', COUNT(*) FROM dbo.PhanCongTho WHERE MaPhieuTiepNhan LIKE 'DMTN%'
UNION ALL SELECT N'HoaDon', COUNT(*) FROM dbo.HoaDon WHERE MaHoaDon LIKE 'DMHD%'
UNION ALL SELECT N'ChiTietHoaDon', COUNT(*) FROM dbo.ChiTietHoaDon WHERE MaHoaDon LIKE 'DMHD%'
UNION ALL SELECT N'PhieuNhap', COUNT(*) FROM dbo.PhieuNhap WHERE MaPhieuNhap LIKE 'DMPN%'
UNION ALL SELECT N'NhaCungCap', COUNT(*) FROM dbo.NhaCungCap WHERE MaNCC LIKE 'DMNCC%'
UNION ALL SELECT N'ChiTietPhieuNhap', COUNT(*) FROM dbo.ChiTietPhieuNhap WHERE MaPhieuNhap LIKE 'DMPN%';

SELECT nv.TenDangNhap, nv.TenNhanVien, cv.TenChucVu, nv.TrangThai
FROM dbo.NhanVien AS nv
JOIN dbo.ChucVu AS cv ON cv.MaChucVu = nv.MaChucVu
WHERE nv.MaNhanVien LIKE 'DMNV%'
ORDER BY nv.MaNhanVien;

SELECT MaHoaDon, MaPhieuTiepNhan, TongTienDichVu, TongTienPhuTung, TongTien,
       TrangThaiThanhToan, PhuongThucThanhToan
FROM dbo.HoaDon WHERE MaHoaDon LIKE 'DMHD%'
ORDER BY MaHoaDon;

SELECT MaPhuTung, TenPhuTung, SoLuongTon, TrangThai
FROM dbo.PhuTung WHERE MaPhuTung LIKE 'DMPT%'
ORDER BY MaPhuTung;

SELECT ct.MaPhieuNhap, ct.MaPhuTung, ncc.MaNCC, ncc.TenNCC,
       ct.SoLuong, ct.DonGiaNhap, ct.ThanhTien
FROM dbo.ChiTietPhieuNhap AS ct
JOIN dbo.NhaCungCap AS ncc ON ncc.MaNCC = ct.MaNCC
WHERE ct.MaPhieuNhap LIKE 'DMPN%'
ORDER BY ct.MaPhieuNhap, ct.MaPhuTung;
GO
