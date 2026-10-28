SET XACT_ABORT ON;
BEGIN TRY
    BEGIN TRANSACTION;
    UPDATE dbo.ChucVu SET TenChucVu=N'__doi_'+MaChucVu WHERE MaChucVu IN('CV001','CV002','CV003','CV004','CV005','CV006','CV007');
    UPDATE dbo.ChucVu SET TenChucVu=N'Chủ',MoTa=N'Chủ cửa hàng' WHERE MaChucVu='CV001';
    UPDATE dbo.ChucVu SET TenChucVu=N'Thợ chính',MoTa=N'Phụ trách sửa chữa, tối đa 3 xe' WHERE MaChucVu='CV002';
    UPDATE dbo.ChucVu SET TenChucVu=N'Thợ phụ',MoTa=N'Thực hiện sửa chữa, tối đa 2 xe' WHERE MaChucVu='CV003';
    UPDATE dbo.ChucVu SET TenChucVu=N'Quản lý',MoTa=N'Quản lý hoạt động cửa hàng' WHERE MaChucVu='CV004';
    UPDATE dbo.ChucVu SET TenChucVu=N'Thủ kho',MoTa=N'Quản lý kho phụ tùng' WHERE MaChucVu='CV006';
    UPDATE dbo.ChucVu SET TenChucVu=N'Thu ngân',MoTa=N'Tiếp nhận và thanh toán' WHERE MaChucVu='CV007';
    UPDATE dbo.NhanVien SET MaChucVu='CV007' WHERE MaChucVu='CV005';
    DELETE dbo.ChucVuDieuChinh WHERE MaChucVu='CV005';
    DELETE dbo.ChucVu WHERE MaChucVu='CV005';
    IF (SELECT COUNT(*) FROM dbo.ChucVu)<>6 THROW 51000,N'Chưa đúng sáu chức vụ.',1;
    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT>0 ROLLBACK TRANSACTION;
    THROW;
END CATCH;
