
USE master;
GO

IF DB_ID(N'QuanLySuaXe') IS NULL
    EXEC(N'CREATE DATABASE QuanLySuaXe');
GO

USE QuanLySuaXe;
GO
IF DB_NAME() = N'QuanLySuaXe' AND OBJECT_ID(N'dbo.ChucVu', N'U') IS NULL
CREATE TABLE dbo.ChucVu (
    MaChucVu VARCHAR(10) PRIMARY KEY,
    TenChucVu NVARCHAR(50) NOT NULL,
    MoTa NVARCHAR(200)
);
GO
IF DB_NAME() = N'QuanLySuaXe' AND OBJECT_ID(N'dbo.NhanVien', N'U') IS NULL
CREATE TABLE dbo.NhanVien (
    MaNhanVien VARCHAR(10) PRIMARY KEY,
    MaChucVu VARCHAR(10) NOT NULL,
    TenNhanVien NVARCHAR(100) NOT NULL,
    TenDangNhap VARCHAR(50) NOT NULL UNIQUE,
    MatKhauHash VARCHAR(255) NOT NULL,
    SoDienThoai VARCHAR(15),
    TrangThai NVARCHAR(30) NOT NULL
        DEFAULT N'Đang làm',
    SoXeDangSuaToiDa INT NOT NULL DEFAULT 3,

    CONSTRAINT FK_NhanVien_ChucVu
        FOREIGN KEY (MaChucVu)
        REFERENCES dbo.ChucVu(MaChucVu),

    CONSTRAINT CK_NhanVien_SoXe
        CHECK (SoXeDangSuaToiDa >= 0)
);
GO
IF DB_NAME() = N'QuanLySuaXe' AND OBJECT_ID(N'dbo.KhachHang', N'U') IS NULL
CREATE TABLE dbo.KhachHang (
    MaKhachHang VARCHAR(10) PRIMARY KEY,
    TenKhachHang NVARCHAR(100) NOT NULL,
    SoDienThoai VARCHAR(15) NOT NULL,
    DiaChi NVARCHAR(200)
);
GO
IF DB_NAME() = N'QuanLySuaXe' AND OBJECT_ID(N'dbo.Xe', N'U') IS NULL
CREATE TABLE dbo.Xe (
    MaXe VARCHAR(10) PRIMARY KEY,
    MaKhachHang VARCHAR(10) NOT NULL,
    BienSo VARCHAR(15) NOT NULL UNIQUE,
    HangXe NVARCHAR(50),
    DongXe NVARCHAR(50),
    NamSanXuat INT,
    MauSac NVARCHAR(30),
    SoKhung VARCHAR(50),

    CONSTRAINT FK_Xe_KhachHang
        FOREIGN KEY (MaKhachHang)
        REFERENCES dbo.KhachHang(MaKhachHang),

    CONSTRAINT UQ_Xe_KhachHang
        UNIQUE (MaXe, MaKhachHang)
);
GO
IF DB_NAME() = N'QuanLySuaXe' AND OBJECT_ID(N'dbo.PhuTung', N'U') IS NULL
CREATE TABLE dbo.PhuTung (
    MaPhuTung VARCHAR(10) PRIMARY KEY,
    TenPhuTung NVARCHAR(100) NOT NULL,
    DonViTinh NVARCHAR(20),
    DonGiaNhap DECIMAL(18,2) NOT NULL DEFAULT 0,
    DonGiaBan DECIMAL(18,2) NOT NULL DEFAULT 0,
    SoLuongTon INT NOT NULL DEFAULT 0,
    TrangThai NVARCHAR(30) NOT NULL
        DEFAULT N'Đang sử dụng',

    CONSTRAINT CK_PhuTung_Gia
        CHECK (
            DonGiaNhap >= 0
            AND DonGiaBan >= 0
        ),

    CONSTRAINT CK_PhuTung_SoLuong
        CHECK (SoLuongTon >= 0)
);
GO
IF DB_NAME() = N'QuanLySuaXe' AND OBJECT_ID(N'dbo.DichVu', N'U') IS NULL
CREATE TABLE dbo.DichVu (
    MaDichVu VARCHAR(10) PRIMARY KEY,
    TenDichVu NVARCHAR(100) NOT NULL,
    MoTa NVARCHAR(300),
    DonGiaDichVu DECIMAL(18,2) NOT NULL DEFAULT 0,
    ThoiGianDuKien INT,
    MaPhuTung VARCHAR(10) NULL,
    TrangThai NVARCHAR(30) NOT NULL
        DEFAULT N'Đang sử dụng',

    CONSTRAINT FK_DichVu_PhuTung
        FOREIGN KEY (MaPhuTung)
        REFERENCES dbo.PhuTung(MaPhuTung),

    CONSTRAINT CK_DichVu_Gia
        CHECK (DonGiaDichVu >= 0),

    CONSTRAINT CK_DichVu_ThoiGian
        CHECK (
            ThoiGianDuKien IS NULL
            OR ThoiGianDuKien >= 0
        )
);
GO
IF DB_NAME() = N'QuanLySuaXe' AND OBJECT_ID(N'dbo.PhieuTiepNhan', N'U') IS NULL
CREATE TABLE dbo.PhieuTiepNhan (
    MaPhieuTiepNhan VARCHAR(10) PRIMARY KEY,
    MaKhachHang VARCHAR(10) NOT NULL,
    MaXe VARCHAR(10) NOT NULL,
    MaNhanVien VARCHAR(10) NOT NULL,

    ThoiGianNhan DATETIME2 NOT NULL
        DEFAULT SYSDATETIME(),

    ThoiGianHoanThanh DATETIME2 NULL,

    MoTaTinhTrangXe NVARCHAR(500),

    TrangThai NVARCHAR(30) NOT NULL
        DEFAULT N'Mới tiếp nhận',

    GhiChu NVARCHAR(300),

    CONSTRAINT FK_PhieuTN_KhachHang
        FOREIGN KEY (MaKhachHang)
        REFERENCES dbo.KhachHang(MaKhachHang),

    CONSTRAINT FK_PhieuTN_Xe
        FOREIGN KEY (MaXe, MaKhachHang)
        REFERENCES dbo.Xe(MaXe, MaKhachHang),

    CONSTRAINT FK_PhieuTN_NhanVien
        FOREIGN KEY (MaNhanVien)
        REFERENCES dbo.NhanVien(MaNhanVien),

    CONSTRAINT CK_PhieuTN_ThoiGian
        CHECK (
            ThoiGianHoanThanh IS NULL
            OR ThoiGianHoanThanh >= ThoiGianNhan
        )
);
GO
IF DB_NAME() = N'QuanLySuaXe' AND OBJECT_ID(N'dbo.ChiTietPhieuTiepNhan', N'U') IS NULL
CREATE TABLE dbo.ChiTietPhieuTiepNhan (
    MaChiTietTiepNhan INT IDENTITY(1,1) PRIMARY KEY,
    MaPhieuTiepNhan VARCHAR(10) NOT NULL,
    MaDichVu VARCHAR(10) NOT NULL,
    SoLuong INT NOT NULL DEFAULT 1,
    DonGiaDichVu DECIMAL(18,2) NOT NULL,

    CONSTRAINT FK_CTPN_PhieuTN
        FOREIGN KEY (MaPhieuTiepNhan)
        REFERENCES dbo.PhieuTiepNhan(MaPhieuTiepNhan),

    CONSTRAINT FK_CTPN_DichVu
        FOREIGN KEY (MaDichVu)
        REFERENCES dbo.DichVu(MaDichVu),

    CONSTRAINT CK_CTPN_SoLuong
        CHECK (SoLuong > 0),

    CONSTRAINT CK_CTPN_DonGia
        CHECK (DonGiaDichVu >= 0),

    CONSTRAINT UQ_CTPN_Phieu_DichVu
        UNIQUE (MaPhieuTiepNhan, MaDichVu)
);
GO
IF DB_NAME() = N'QuanLySuaXe' AND OBJECT_ID(N'dbo.PhanCongTho', N'U') IS NULL
CREATE TABLE dbo.PhanCongTho (
    MaPhieuTiepNhan VARCHAR(10) NOT NULL,
    MaNhanVien VARCHAR(10) NOT NULL,
    VaiTro NVARCHAR(50),

    TrangThaiCongViec NVARCHAR(30) NOT NULL
        DEFAULT N'Đã phân công',

    ThoiGianBatDau DATETIME2 NULL,
    ThoiGianKetThuc DATETIME2 NULL,

    CONSTRAINT PK_PhanCongTho
        PRIMARY KEY (
            MaPhieuTiepNhan,
            MaNhanVien
        ),

    CONSTRAINT FK_PhanCong_PhieuTN
        FOREIGN KEY (MaPhieuTiepNhan)
        REFERENCES dbo.PhieuTiepNhan(MaPhieuTiepNhan),

    CONSTRAINT FK_PhanCong_NhanVien
        FOREIGN KEY (MaNhanVien)
        REFERENCES dbo.NhanVien(MaNhanVien),

    CONSTRAINT CK_PhanCong_ThoiGian
        CHECK (
            ThoiGianKetThuc IS NULL
            OR (
                ThoiGianBatDau IS NOT NULL
                AND ThoiGianKetThuc >= ThoiGianBatDau
            )
        )
);
GO
IF DB_NAME() = N'QuanLySuaXe' AND OBJECT_ID(N'dbo.HoaDon', N'U') IS NULL
CREATE TABLE dbo.HoaDon (
    MaHoaDon VARCHAR(10) PRIMARY KEY,

    MaPhieuTiepNhan VARCHAR(10) NOT NULL UNIQUE,

    NgayLap DATETIME2 NOT NULL
        DEFAULT SYSDATETIME(),

    TongTienDichVu DECIMAL(18,2) NOT NULL
        DEFAULT 0,

    TongTienPhuTung DECIMAL(18,2) NOT NULL
        DEFAULT 0,

    TongTien DECIMAL(18,2) NOT NULL
        DEFAULT 0,

    TrangThaiThanhToan NVARCHAR(30) NOT NULL
        DEFAULT N'Chưa thanh toán',

    PhuongThucThanhToan NVARCHAR(30),

    MaNhanVien VARCHAR(10) NOT NULL,

    CONSTRAINT FK_HoaDon_PhieuTN
        FOREIGN KEY (MaPhieuTiepNhan)
        REFERENCES dbo.PhieuTiepNhan(MaPhieuTiepNhan),

    CONSTRAINT FK_HoaDon_NhanVien
        FOREIGN KEY (MaNhanVien)
        REFERENCES dbo.NhanVien(MaNhanVien),

    CONSTRAINT CK_HoaDon_Tien
        CHECK (
            TongTienDichVu >= 0
            AND TongTienPhuTung >= 0
            AND TongTien >= 0
            AND TongTien = TongTienDichVu + TongTienPhuTung
        )
);
GO
IF DB_NAME() = N'QuanLySuaXe' AND OBJECT_ID(N'dbo.ChiTietHoaDon', N'U') IS NULL
CREATE TABLE dbo.ChiTietHoaDon (
    MaChiTietHoaDon INT IDENTITY(1,1) PRIMARY KEY,
    MaHoaDon VARCHAR(10) NOT NULL,
    MaDichVu VARCHAR(10) NOT NULL,
    TenDichVu NVARCHAR(100) NOT NULL,
    SoLuong INT NOT NULL,
    DonGia DECIMAL(18,2) NOT NULL,

    ThanhTien AS (SoLuong * DonGia),

    CONSTRAINT FK_CTHD_HoaDon
        FOREIGN KEY (MaHoaDon)
        REFERENCES dbo.HoaDon(MaHoaDon),

    CONSTRAINT FK_CTHD_DichVu
        FOREIGN KEY (MaDichVu)
        REFERENCES dbo.DichVu(MaDichVu),

    CONSTRAINT CK_CTHD_SoLuong
        CHECK (SoLuong > 0),

    CONSTRAINT CK_CTHD_DonGia
        CHECK (DonGia >= 0)
);
GO
IF DB_NAME() = N'QuanLySuaXe' AND OBJECT_ID(N'dbo.PhieuNhap', N'U') IS NULL
CREATE TABLE dbo.PhieuNhap (
    MaPhieuNhap VARCHAR(10) PRIMARY KEY,

    NgayNhap DATETIME2 NOT NULL
        DEFAULT SYSDATETIME(),

    MaNhanVienNhap VARCHAR(10) NOT NULL,

    GhiChu NVARCHAR(300),

    TongTien DECIMAL(18,2) NOT NULL
        DEFAULT 0,

    CONSTRAINT FK_PhieuNhap_NhanVien
        FOREIGN KEY (MaNhanVienNhap)
        REFERENCES dbo.NhanVien(MaNhanVien),

    CONSTRAINT CK_PhieuNhap_TongTien
        CHECK (TongTien >= 0)
);
GO
IF DB_NAME() = N'QuanLySuaXe' AND OBJECT_ID(N'dbo.NhaCungCap', N'U') IS NULL
CREATE TABLE dbo.NhaCungCap (
    MaNCC VARCHAR(10) PRIMARY KEY,
    TenNCC NVARCHAR(100) NOT NULL,
    SoDienThoai VARCHAR(15) NOT NULL,
    DiaChi NVARCHAR(200) NOT NULL
);
GO
IF DB_NAME() = N'QuanLySuaXe' AND OBJECT_ID(N'dbo.ChiTietPhieuNhap', N'U') IS NULL
CREATE TABLE dbo.ChiTietPhieuNhap (
    MaChiTietPhieuNhap INT IDENTITY(1,1) PRIMARY KEY,

    MaPhieuNhap VARCHAR(10) NOT NULL,
    MaPhuTung VARCHAR(10) NOT NULL,
    MaNCC VARCHAR(10) NOT NULL,
    SoLuong INT NOT NULL,
    DonGiaNhap DECIMAL(18,2) NOT NULL,
    HanSuDung DATE NULL,

    ThanhTien AS (SoLuong * DonGiaNhap),

    CONSTRAINT FK_CTPNhap_PhieuNhap
        FOREIGN KEY (MaPhieuNhap)
        REFERENCES dbo.PhieuNhap(MaPhieuNhap),

    CONSTRAINT FK_CTPNhap_PhuTung
        FOREIGN KEY (MaPhuTung)
        REFERENCES dbo.PhuTung(MaPhuTung),

    CONSTRAINT FK_CTPNhap_NhaCungCap
        FOREIGN KEY (MaNCC)
        REFERENCES dbo.NhaCungCap(MaNCC),

    CONSTRAINT CK_CTPNhap_SoLuong
        CHECK (SoLuong > 0),

    CONSTRAINT CK_CTPNhap_DonGia
        CHECK (DonGiaNhap >= 0)
);
GO
SET XACT_ABORT ON;
BEGIN TRY
    IF DB_NAME() <> N'QuanLySuaXe'
        THROW 50002, N'Chỉ được cập nhật cấu trúc trong database QuanLySuaXe.', 1;

    BEGIN TRANSACTION;
    IF COL_LENGTH(N'dbo.ChiTietPhieuNhap', N'MaNCC') IS NULL
        ALTER TABLE dbo.ChiTietPhieuNhap ADD MaNCC VARCHAR(10) NULL;

    IF NOT EXISTS (
        SELECT 1 FROM sys.foreign_keys
        WHERE parent_object_id = OBJECT_ID(N'dbo.ChiTietPhieuNhap')
          AND name = N'FK_CTPNhap_NhaCungCap'
    )
        EXEC(N'ALTER TABLE dbo.ChiTietPhieuNhap WITH CHECK
            ADD CONSTRAINT FK_CTPNhap_NhaCungCap FOREIGN KEY (MaNCC)
            REFERENCES dbo.NhaCungCap(MaNCC);');

    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
    THROW;
END CATCH;
GO
SELECT TABLE_NAME
FROM INFORMATION_SCHEMA.TABLES
WHERE TABLE_TYPE = 'BASE TABLE'
ORDER BY TABLE_NAME;
GO
SELECT COLUMN_NAME, DATA_TYPE, IS_NULLABLE
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'PhieuTiepNhan'
ORDER BY ORDINAL_POSITION;
GO
SELECT MaChiTietPhieuNhap, MaPhieuNhap, MaPhuTung, MaNCC
FROM dbo.ChiTietPhieuNhap
WHERE MaNCC IS NULL;
GO
