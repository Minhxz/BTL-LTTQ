USE QuanLySuaXe;
GO
SET XACT_ABORT ON;
BEGIN TRANSACTION;
IF COL_LENGTH('dbo.NhanVien','DaCapTaiKhoan') IS NULL
    ALTER TABLE dbo.NhanVien ADD DaCapTaiKhoan BIT NOT NULL CONSTRAINT DF_NV_DaCapTaiKhoan DEFAULT 1;
IF COL_LENGTH('dbo.NhanVien','TaiKhoanHoatDong') IS NULL
    ALTER TABLE dbo.NhanVien ADD TaiKhoanHoatDong BIT NOT NULL CONSTRAINT DF_NV_TaiKhoanHoatDong DEFAULT 1;
IF COL_LENGTH('dbo.NhanVien','LuongGio') IS NULL
    ALTER TABLE dbo.NhanVien ADD LuongGio DECIMAL(18,2) NOT NULL CONSTRAINT DF_NV_LuongGio DEFAULT 0;
IF COL_LENGTH('dbo.DichVu','LuongNhiemVu') IS NULL
    ALTER TABLE dbo.DichVu ADD LuongNhiemVu DECIMAL(18,2) NOT NULL CONSTRAINT DF_DV_Luong DEFAULT 0;
IF COL_LENGTH('dbo.ChiTietPhieuTiepNhan','TenDichVu') IS NULL
    ALTER TABLE dbo.ChiTietPhieuTiepNhan ADD TenDichVu NVARCHAR(100) NULL;
IF COL_LENGTH('dbo.ChiTietPhieuTiepNhan','LuongNhiemVu') IS NULL
    ALTER TABLE dbo.ChiTietPhieuTiepNhan ADD LuongNhiemVu DECIMAL(18,2) NOT NULL CONSTRAINT DF_CTTN_Luong DEFAULT 0;
IF COL_LENGTH('dbo.PhanCongTho','TienCong') IS NULL
    ALTER TABLE dbo.PhanCongTho ADD TienCong DECIMAL(18,2) NOT NULL CONSTRAINT DF_PC_Tien DEFAULT 0;
IF COL_LENGTH('dbo.PhanCongTho','NgayGhiNhanCong') IS NULL
    ALTER TABLE dbo.PhanCongTho ADD NgayGhiNhanCong DATETIME2 NULL;
IF COL_LENGTH('dbo.PhieuTiepNhan','DaChotPhuTung') IS NULL
    ALTER TABLE dbo.PhieuTiepNhan ADD DaChotPhuTung BIT NOT NULL CONSTRAINT DF_TN_Chot DEFAULT 0;
IF COL_LENGTH('dbo.PhieuTiepNhan','HenTra') IS NULL
    ALTER TABLE dbo.PhieuTiepNhan ADD HenTra DATETIME2 NULL;
IF COL_LENGTH('dbo.PhieuTiepNhan','SoKm') IS NULL
    ALTER TABLE dbo.PhieuTiepNhan ADD SoKm INT NOT NULL CONSTRAINT DF_TN_SoKm DEFAULT 0 CONSTRAINT CK_TN_SoKm CHECK(SoKm BETWEEN 0 AND 9999999);
IF COL_LENGTH('dbo.Xe','LoaiXe') IS NULL
    ALTER TABLE dbo.Xe ADD LoaiXe NVARCHAR(30) NULL;
IF COL_LENGTH('dbo.HoaDon','DaKhoaNoiDung') IS NULL
    ALTER TABLE dbo.HoaDon ADD DaKhoaNoiDung BIT NOT NULL CONSTRAINT DF_HD_Khoa DEFAULT 1;
IF OBJECT_ID('dbo.DinhMucDichVu','U') IS NULL
CREATE TABLE dbo.DinhMucDichVu (
    MaDichVu VARCHAR(10) NOT NULL REFERENCES dbo.DichVu(MaDichVu),
    MaPhuTung VARCHAR(10) NOT NULL REFERENCES dbo.PhuTung(MaPhuTung),
    SoLuong INT NOT NULL CHECK(SoLuong>0), PRIMARY KEY(MaDichVu,MaPhuTung)
);
IF OBJECT_ID('dbo.PhuTungTiepNhan','U') IS NULL
CREATE TABLE dbo.PhuTungTiepNhan (
    MaPhieuTiepNhan VARCHAR(10) NOT NULL REFERENCES dbo.PhieuTiepNhan(MaPhieuTiepNhan),
    MaPhuTung VARCHAR(10) NOT NULL REFERENCES dbo.PhuTung(MaPhuTung),
    TenPhuTung NVARCHAR(100) NOT NULL, SoLuong INT NOT NULL CHECK(SoLuong>0),
    DonGia DECIMAL(18,2) NOT NULL CHECK(DonGia>=0), PRIMARY KEY(MaPhieuTiepNhan,MaPhuTung)
);
IF OBJECT_ID('dbo.CaLam','U') IS NULL
CREATE TABLE dbo.CaLam (MaCa VARCHAR(10) PRIMARY KEY, TenCa NVARCHAR(50) NOT NULL, GioBatDau TIME NOT NULL, GioKetThuc TIME NOT NULL, CHECK(GioKetThuc>GioBatDau));
IF OBJECT_ID('dbo.BangCong','U') IS NULL
CREATE TABLE dbo.BangCong (
    MaBangCong INT IDENTITY PRIMARY KEY, MaNhanVien VARCHAR(10) NOT NULL REFERENCES dbo.NhanVien(MaNhanVien),
    Ngay DATE NOT NULL, MaCa VARCHAR(10) NOT NULL REFERENCES dbo.CaLam(MaCa),
    GioVao TIME NULL, GioRa TIME NULL, Vang BIT NOT NULL DEFAULT 0,
    DieuChinhPhut INT NOT NULL DEFAULT 0, LyDo NVARCHAR(300) NULL,
    LuongGio DECIMAL(18,2) NOT NULL CHECK(LuongGio>=0),
    MaNguoiCapNhat VARCHAR(10) NOT NULL REFERENCES dbo.NhanVien(MaNhanVien),
    CapNhatLuc DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    CONSTRAINT UQ_BangCong UNIQUE(MaNhanVien,Ngay,MaCa),
    CHECK((Vang=1 AND GioVao IS NULL AND GioRa IS NULL AND DieuChinhPhut=0) OR (Vang=0 AND GioVao IS NOT NULL AND GioRa>GioVao)),
    CHECK(DieuChinhPhut BETWEEN -390 AND 390),
    CHECK(DieuChinhPhut=0 OR LEN(LTRIM(RTRIM(ISNULL(LyDo,N''))))>0)
);
IF OBJECT_ID('dbo.LichSuBangCong','U') IS NULL
CREATE TABLE dbo.LichSuBangCong (Id INT IDENTITY PRIMARY KEY, MaBangCong INT NOT NULL, NoiDungCu NVARCHAR(MAX), NoiDungMoi NVARCHAR(MAX), ThoiGian DATETIME2 NOT NULL DEFAULT SYSDATETIME(), MaNguoiCapNhat VARCHAR(10) NOT NULL);
IF NOT EXISTS(SELECT 1 FROM dbo.ChucVu WHERE TenChucVu=N'Chủ/Quản lý') INSERT dbo.ChucVu VALUES('CVQL',N'Chủ/Quản lý',N'Toàn quyền quản lý');
IF NOT EXISTS(SELECT 1 FROM dbo.ChucVu WHERE TenChucVu=N'Thợ') INSERT dbo.ChucVu VALUES('CVTHO',N'Thợ',N'Mặc định tối đa 2 xe');
IF NOT EXISTS(SELECT 1 FROM dbo.ChucVu WHERE TenChucVu=N'Thợ chính') INSERT dbo.ChucVu VALUES('CVTC',N'Thợ chính',N'Mặc định tối đa 3 xe');
IF NOT EXISTS(SELECT 1 FROM dbo.ChucVu WHERE TenChucVu=N'Thu ngân') INSERT dbo.ChucVu VALUES('CVTN',N'Thu ngân',N'Tiếp nhận và thanh toán');
IF NOT EXISTS(SELECT 1 FROM dbo.CaLam WHERE MaCa='SANG') INSERT dbo.CaLam VALUES('SANG',N'Sáng 07:30 – 14:00','07:30','14:00');
IF NOT EXISTS(SELECT 1 FROM dbo.CaLam WHERE MaCa='TOI') INSERT dbo.CaLam VALUES('TOI',N'Chiều tối 13:30 – 20:00','13:30','20:00');
COMMIT;
GO
IF NOT EXISTS(SELECT 1 FROM sys.indexes WHERE object_id=OBJECT_ID('dbo.PhanCongTho') AND name='IX_PhanCongTho_ThoTrangThai')
    CREATE INDEX IX_PhanCongTho_ThoTrangThai ON dbo.PhanCongTho(MaNhanVien,TrangThaiCongViec);
IF NOT EXISTS(SELECT 1 FROM sys.indexes WHERE object_id=OBJECT_ID('dbo.PhieuTiepNhan') AND name='IX_PhieuTiepNhan_XeTrangThai')
    CREATE INDEX IX_PhieuTiepNhan_XeTrangThai ON dbo.PhieuTiepNhan(MaXe,TrangThai);
GO
UPDATE dbo.PhieuTiepNhan SET TrangThai=N'Hoàn thành' WHERE TrangThai=N'Đã hoàn thành';
UPDATE dbo.PhanCongTho SET TrangThaiCongViec=N'Hoàn thành' WHERE TrangThaiCongViec=N'Đã hoàn thành';
INSERT dbo.DinhMucDichVu(MaDichVu,MaPhuTung,SoLuong)
SELECT d.MaDichVu,d.MaPhuTung,1 FROM dbo.DichVu d
WHERE d.MaPhuTung IS NOT NULL AND NOT EXISTS(SELECT 1 FROM dbo.DinhMucDichVu m WHERE m.MaDichVu=d.MaDichVu);
UPDATE c SET TenDichVu=d.TenDichVu FROM dbo.ChiTietPhieuTiepNhan c JOIN dbo.DichVu d ON d.MaDichVu=c.MaDichVu WHERE c.TenDichVu IS NULL;
GO
CREATE OR ALTER VIEW dbo.vBangCong AS
SELECT b.*,n.TenNhanVien,c.TenCa,c.GioBatDau,c.GioKetThuc,
    x.PhutThucTe,
    CASE WHEN b.Vang=1 THEN 0 WHEN x.PhutThucTe+b.DieuChinhPhut<0 THEN 0 ELSE x.PhutThucTe+b.DieuChinhPhut END AS PhutTinhCong,
    CAST((CASE WHEN b.Vang=1 THEN 0 WHEN x.PhutThucTe+b.DieuChinhPhut<0 THEN 0 ELSE x.PhutThucTe+b.DieuChinhPhut END)/60.0 AS DECIMAL(10,2)) AS GioTinhCong,
    CAST((CASE WHEN b.Vang=1 THEN 0 WHEN x.PhutThucTe+b.DieuChinhPhut<0 THEN 0 ELSE x.PhutThucTe+b.DieuChinhPhut END)*b.LuongGio/60.0 AS DECIMAL(18,2)) AS TienLuong,
    CASE WHEN b.Vang=1 OR b.GioVao<=c.GioBatDau THEN 0 ELSE DATEDIFF(MINUTE,c.GioBatDau,b.GioVao) END AS PhutMuon,
    CASE WHEN b.Vang=1 OR b.GioRa>=c.GioKetThuc THEN 0 ELSE DATEDIFF(MINUTE,b.GioRa,c.GioKetThuc) END AS PhutVeSom
FROM dbo.BangCong b JOIN dbo.NhanVien n ON n.MaNhanVien=b.MaNhanVien JOIN dbo.CaLam c ON c.MaCa=b.MaCa
CROSS APPLY(SELECT CASE WHEN b.Vang=1 OR b.GioRa<=c.GioBatDau OR b.GioVao>=c.GioKetThuc THEN 0 ELSE DATEDIFF(MINUTE,CASE WHEN b.GioVao<c.GioBatDau THEN c.GioBatDau ELSE b.GioVao END,CASE WHEN b.GioRa>c.GioKetThuc THEN c.GioKetThuc ELSE b.GioRa END) END AS PhutThucTe) x;
GO
CREATE OR ALTER TRIGGER dbo.TR_BangCong_LichSu ON dbo.BangCong AFTER INSERT,UPDATE AS
BEGIN
    SET NOCOUNT ON;
    INSERT dbo.LichSuBangCong(MaBangCong,NoiDungCu,NoiDungMoi,MaNguoiCapNhat)
    SELECT i.MaBangCong,(SELECT d.* FOR JSON PATH,WITHOUT_ARRAY_WRAPPER),(SELECT i.* FOR JSON PATH,WITHOUT_ARRAY_WRAPPER),i.MaNguoiCapNhat
    FROM inserted i LEFT JOIN deleted d ON d.MaBangCong=i.MaBangCong;
END;
GO
CREATE OR ALTER TRIGGER dbo.TR_HoaDon_BatBien ON dbo.HoaDon AFTER UPDATE,DELETE AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS(SELECT 1 FROM deleted d LEFT JOIN inserted i ON i.MaHoaDon=d.MaHoaDon WHERE i.MaHoaDon IS NULL OR i.MaPhieuTiepNhan<>d.MaPhieuTiepNhan OR i.NgayLap<>d.NgayLap OR i.TongTien<>d.TongTien OR i.TongTienDichVu<>d.TongTienDichVu OR i.TongTienPhuTung<>d.TongTienPhuTung OR i.MaNhanVien<>d.MaNhanVien OR d.TrangThaiThanhToan=N'Đã thanh toán' OR (d.DaKhoaNoiDung=1 AND i.DaKhoaNoiDung<>1))
        THROW 50100,N'Hóa đơn đã phát hành không được sửa hoặc xóa; chỉ được ghi nhận thanh toán một lần.',1;
END;
GO
CREATE OR ALTER TRIGGER dbo.TR_ChiTietHoaDon_BatBien ON dbo.ChiTietHoaDon AFTER INSERT,UPDATE,DELETE AS
BEGIN
    IF EXISTS(SELECT 1 FROM deleted) OR EXISTS(SELECT 1 FROM inserted i JOIN dbo.HoaDon h ON h.MaHoaDon=i.MaHoaDon WHERE h.DaKhoaNoiDung=1)
        THROW 50101,N'Không được thêm, sửa hoặc xóa chi tiết hóa đơn đã phát hành.',1;
END;
GO
CREATE OR ALTER TRIGGER dbo.TR_PhuTungTiepNhan_Khoa ON dbo.PhuTungTiepNhan AFTER INSERT,UPDATE,DELETE AS
BEGIN
    IF EXISTS(SELECT 1 FROM dbo.HoaDon h JOIN (SELECT MaPhieuTiepNhan FROM inserted UNION SELECT MaPhieuTiepNhan FROM deleted) p ON p.MaPhieuTiepNhan=h.MaPhieuTiepNhan)
        THROW 50103,N'Không được đổi phụ tùng của phiếu đã có hóa đơn.',1;
END;
GO
CREATE OR ALTER TRIGGER dbo.TR_ChiTietTiepNhan_Khoa ON dbo.ChiTietPhieuTiepNhan AFTER INSERT,UPDATE,DELETE AS
BEGIN
    IF EXISTS(SELECT 1 FROM dbo.HoaDon h JOIN (SELECT MaPhieuTiepNhan FROM inserted UNION SELECT MaPhieuTiepNhan FROM deleted) p ON p.MaPhieuTiepNhan=h.MaPhieuTiepNhan)
        THROW 50102,N'Phiếu đã có hóa đơn không được đổi chi tiết.',1;
END;
GO

