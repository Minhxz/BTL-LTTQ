SET XACT_ABORT ON;
SET NOCOUNT ON;
BEGIN TRY
    BEGIN TRANSACTION;
    DECLARE @MatKhau VARCHAR(64)=CONVERT(VARCHAR(64),HASHBYTES('SHA2_256',CONVERT(VARCHAR(3),'123')),2);
    DECLARE @Mau TABLE(MaNhanVien VARCHAR(10) PRIMARY KEY,MaCu VARCHAR(10),HoTen NVARCHAR(100),TenDangNhap VARCHAR(50),MaChucVu VARCHAR(10));
    INSERT @Mau VALUES ('NV001','NV001',N'Minh','minh','CV001'),('NV002','NV002',N'An','an','CV002'),('NV003','NV005',N'Huy','huy','CV003'),('NV004','NV020',N'Mai','mai','CV007'),('NV005','NV018',N'Cường','cuong','CV006');
    IF EXISTS(SELECT 1 FROM @Mau m WHERE NOT EXISTS(SELECT 1 FROM dbo.ChucVu c WHERE c.MaChucVu=m.MaChucVu))
        THROW 51000,N'Thiếu chức vụ đã chuẩn hóa cho bộ năm nhân viên.',1;
    IF (SELECT COUNT(*) FROM dbo.NhanVien)=5 AND NOT EXISTS(SELECT 1 FROM @Mau m WHERE NOT EXISTS(SELECT 1 FROM dbo.NhanVien n WHERE n.MaNhanVien=m.MaNhanVien))
    BEGIN
        UPDATE n SET n.HoTen=m.HoTen,n.TenDangNhap=m.TenDangNhap,n.MaChucVu=m.MaChucVu,n.MatKhau=@MatKhau,n.TrangThai=N'Đang làm'
        FROM dbo.NhanVien n JOIN @Mau m ON m.MaNhanVien=n.MaNhanVien;
    END
    ELSE
    BEGIN
        IF (SELECT COUNT(*) FROM dbo.NhanVien)<>22 OR EXISTS(SELECT 1 FROM @Mau m WHERE NOT EXISTS(SELECT 1 FROM dbo.NhanVien n WHERE n.MaNhanVien=m.MaCu))
            THROW 51001,N'Dữ liệu nhân viên không phải bộ mẫu 22 người hoặc bộ năm người đã chuyển đổi. Dừng để giữ dữ liệu.',1;
        DECLARE @SoPhieu INT=(SELECT COUNT(*) FROM dbo.PhieuTiepNhan),@SoHoaDon INT=(SELECT COUNT(*) FROM dbo.HoaDon),@SoNhap INT=(SELECT COUNT(*) FROM dbo.PhieuNhap),@SoKhach INT=(SELECT COUNT(*) FROM dbo.KhachHang),@SoXe INT=(SELECT COUNT(*) FROM dbo.Xe),@SoPhuTung INT=(SELECT COUNT(*) FROM dbo.PhuTung),@SoCTNhap INT=(SELECT COUNT(*) FROM dbo.ChiTietPhieuNhap),@SoCTPhieu INT=(SELECT COUNT(*) FROM dbo.ChiTietPhieuTiepNhan);
        DECLARE @TienHoaDon DECIMAL(38,0)=(SELECT COALESCE(SUM(TienThanhToan),0) FROM dbo.HoaDon),@TienNhap DECIMAL(38,0)=(SELECT COALESCE(SUM(TongTienNhap),0) FROM dbo.PhieuNhap),@TienPhieu DECIMAL(38,0)=(SELECT COALESCE(SUM(ThanhTien),0) FROM dbo.ChiTietPhieuTiepNhan);
        DECLARE @XeDaPhanCong INT=(SELECT COUNT(DISTINCT MaXe) FROM dbo.ChiTietPhanCong),@ChoChuaPhanCong INT=(SELECT COUNT(*) FROM dbo.PhieuTiepNhan p WHERE p.TrangThai=N'Chờ sửa' AND NOT EXISTS(SELECT 1 FROM dbo.ChiTietPhanCong c WHERE c.MaXe=p.MaXe));
        SELECT m.MaNhanVien,m.HoTen,m.TenDangNhap,m.MaChucVu,n.SoDienThoai,n.Luong INTO #NhanVienMoi FROM @Mau m JOIN dbo.NhanVien n ON n.MaNhanVien=m.MaCu;
        SELECT n.MaNhanVien AS MaCu,CASE WHEN n.MaChucVu='CV001' THEN 'NV001' ELSE 'NV004' END AS MaLapPhieu,CASE WHEN n.MaChucVu='CV001' THEN 'NV001' ELSE 'NV005' END AS MaNhapKho,CASE WHEN n.MaChucVu='CV002' THEN 'NV002' ELSE 'NV003' END AS MaTho INTO #DoiMa FROM dbo.NhanVien n;
        SELECT d.MaTho AS MaNhanVien,c.MaXe,MIN(c.ThoiGianBatDau) AS ThoiGianBatDau,MAX(c.ThoiGianHoanThanh) AS ThoiGianHoanThanh INTO #PhanCongMoi FROM dbo.ChiTietPhanCong c JOIN #DoiMa d ON d.MaCu=c.MaNhanVien GROUP BY d.MaTho,c.MaXe;
        SELECT m.MaNhanVien,c.MaCaLam,c.NgayLam,c.TongThoiGianLamThucTe INTO #CongMoi FROM dbo.ChiTietCaLam c JOIN @Mau m ON m.MaCu=c.MaNhanVien;
        IF EXISTS(SELECT 1 FROM #CongMoi GROUP BY MaNhanVien,NgayLam HAVING SUM(TongThoiGianLamThucTe)>24)
            THROW 51002,N'Công của nhân viên đại diện vượt 24 giờ/ngày. Dừng chuyển đổi.',1;
        UPDATE p SET p.MaNhanVien=d.MaLapPhieu FROM dbo.PhieuTiepNhan p JOIN #DoiMa d ON d.MaCu=p.MaNhanVien;
        UPDATE p SET p.MaNhanVienNhap=d.MaNhapKho FROM dbo.PhieuNhap p JOIN #DoiMa d ON d.MaCu=p.MaNhanVienNhap;
        DELETE FROM dbo.ChiTietPhanCong;
        DELETE FROM dbo.ChiTietCaLam;
        DELETE n FROM dbo.NhanVien n WHERE NOT EXISTS(SELECT 1 FROM @Mau m WHERE m.MaNhanVien=n.MaNhanVien);
        UPDATE n SET n.SoDienThoai='_'+n.MaNhanVien,n.TenDangNhap='__doi_'+n.MaNhanVien FROM dbo.NhanVien n;
        UPDATE n SET n.HoTen=m.HoTen,n.TenDangNhap=m.TenDangNhap,n.MaChucVu=m.MaChucVu,n.SoDienThoai=m.SoDienThoai,n.Luong=m.Luong,n.MatKhau=@MatKhau,n.TrangThai=N'Đang làm'
        FROM dbo.NhanVien n JOIN #NhanVienMoi m ON m.MaNhanVien=n.MaNhanVien;
        INSERT dbo.ChiTietPhanCong(MaNhanVien,MaXe,ThoiGianBatDau,ThoiGianHoanThanh) SELECT MaNhanVien,MaXe,ThoiGianBatDau,ThoiGianHoanThanh FROM #PhanCongMoi;
        INSERT dbo.ChiTietCaLam(MaNhanVien,MaCaLam,NgayLam,TongThoiGianLamThucTe) SELECT MaNhanVien,MaCaLam,NgayLam,TongThoiGianLamThucTe FROM #CongMoi;
        IF @SoPhieu<>(SELECT COUNT(*) FROM dbo.PhieuTiepNhan) OR @SoHoaDon<>(SELECT COUNT(*) FROM dbo.HoaDon) OR @SoNhap<>(SELECT COUNT(*) FROM dbo.PhieuNhap) OR @SoKhach<>(SELECT COUNT(*) FROM dbo.KhachHang) OR @SoXe<>(SELECT COUNT(*) FROM dbo.Xe) OR @SoPhuTung<>(SELECT COUNT(*) FROM dbo.PhuTung) OR @SoCTNhap<>(SELECT COUNT(*) FROM dbo.ChiTietPhieuNhap) OR @SoCTPhieu<>(SELECT COUNT(*) FROM dbo.ChiTietPhieuTiepNhan)
            THROW 51003,N'Số chứng từ, khách, xe hoặc phụ tùng thay đổi. Đã hủy chuyển đổi.',1;
        IF @TienHoaDon<>(SELECT COALESCE(SUM(TienThanhToan),0) FROM dbo.HoaDon) OR @TienNhap<>(SELECT COALESCE(SUM(TongTienNhap),0) FROM dbo.PhieuNhap) OR @TienPhieu<>(SELECT COALESCE(SUM(ThanhTien),0) FROM dbo.ChiTietPhieuTiepNhan)
            THROW 51004,N'Tổng tiền chứng từ thay đổi. Đã hủy chuyển đổi.',1;
        IF @XeDaPhanCong<>(SELECT COUNT(DISTINCT MaXe) FROM dbo.ChiTietPhanCong) OR @ChoChuaPhanCong<>(SELECT COUNT(*) FROM dbo.PhieuTiepNhan p WHERE p.TrangThai=N'Chờ sửa' AND NOT EXISTS(SELECT 1 FROM dbo.ChiTietPhanCong c WHERE c.MaXe=p.MaXe))
            THROW 51005,N'Lịch sử xe được giao hoặc phiếu chờ chưa giao thay đổi. Đã hủy chuyển đổi.',1;
        IF EXISTS(SELECT MaNhanVien,MaCaLam,NgayLam,TongThoiGianLamThucTe FROM #CongMoi EXCEPT SELECT MaNhanVien,MaCaLam,NgayLam,TongThoiGianLamThucTe FROM dbo.ChiTietCaLam)
            THROW 51006,N'Giờ công năm nhân viên đại diện không được giữ nguyên.',1;
        DROP TABLE #NhanVienMoi;
        DROP TABLE #DoiMa;
        DROP TABLE #PhanCongMoi;
        DROP TABLE #CongMoi;
    END;
    IF (SELECT COUNT(*) FROM dbo.NhanVien)<>5 OR EXISTS(SELECT 1 FROM @Mau m LEFT JOIN dbo.NhanVien n ON n.MaNhanVien=m.MaNhanVien WHERE n.MaNhanVien IS NULL OR n.HoTen<>m.HoTen OR n.TenDangNhap<>m.TenDangNhap OR n.MaChucVu<>m.MaChucVu OR n.MatKhau<>@MatKhau OR n.TrangThai<>N'Đang làm')
        THROW 51007,N'Chưa tạo đúng năm nhân viên theo yêu cầu.',1;
    COMMIT TRANSACTION;
    SELECT n.MaNhanVien,n.HoTen,n.TenDangNhap,c.TenChucVu,n.TrangThai FROM dbo.NhanVien n JOIN dbo.ChucVu c ON c.MaChucVu=n.MaChucVu ORDER BY n.MaNhanVien;
    SELECT (SELECT COUNT(*) FROM dbo.NhanVien) AS SoNhanVien,(SELECT COUNT(*) FROM dbo.ChiTietCaLam) AS SoDongCong,(SELECT COUNT(*) FROM dbo.ChiTietPhanCong) AS SoDongPhanCong,(SELECT COUNT(*) FROM dbo.PhieuTiepNhan) AS SoPhieu,(SELECT COUNT(*) FROM dbo.HoaDon) AS SoHoaDon,(SELECT SUM(TienThanhToan) FROM dbo.HoaDon) AS TienHoaDon,(SELECT COUNT(*) FROM dbo.PhieuNhap) AS SoPhieuNhap;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT>0 ROLLBACK TRANSACTION;
    THROW;
END CATCH;
