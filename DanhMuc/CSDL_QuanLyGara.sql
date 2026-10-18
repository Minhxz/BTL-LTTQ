-- TẠO CSDL VÀ BẢNG DỊCH VỤ (TIỀN CÔNG)
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'QuanLyGara')
BEGIN
    CREATE DATABASE QuanLyGara;
END
GO

USE QuanLyGara;
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='DichVu' and xtype='U')
BEGIN
    CREATE TABLE DichVu (
        MaDV NVARCHAR(20) PRIMARY KEY,
        TenDV NVARCHAR(100) NOT NULL,
        DonGia DECIMAL(18, 0) NOT NULL DEFAULT 0,
        GhiChu NVARCHAR(200)
    );

    -- Dữ liệu mẫu
    INSERT INTO DichVu (MaDV, TenDV, DonGia, GhiChu) VALUES
    ('DV01', N'Vá săm xe máy', 25000, N'Vá ép nhiệt chuẩn'),
    ('DV02', N'Thay dầu máy + kiểm tra phanh', 30000, N'Tiền công thay dầu'),
    ('DV03', N'Bảo dưỡng toàn bộ xe ga', 200000, N'Vệ sinh côn, nồi, lọc gió'),
    ('DV04', N'Đại tu động cơ', 800000, N'Hạ máy, làm lại hơi, cam cò'),
    ('DV05', N'Cân vành xe máy', 50000, N'Rút căm, cân vành');
END
GO
