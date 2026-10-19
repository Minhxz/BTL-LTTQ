IF DB_ID('QuanLyNhapKho') IS NOT NULL
BEGIN
    ALTER DATABASE QuanLyNhapKho SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE QuanLyNhapKho;
END;
GO

CREATE DATABASE QuanLyNhapKho;
GO

USE QuanLyNhapKho;
GO

CREATE TABLE NhaCungCap
(
    MaNCC VARCHAR(10) PRIMARY KEY,
    TenNCC NVARCHAR(100) NOT NULL,
    SoDienThoai VARCHAR(15) NOT NULL,
    DiaChi NVARCHAR(200) NOT NULL
);
GO

CREATE TABLE PhuTung
(
    MaPhuTung VARCHAR(10) PRIMARY KEY,
    TenPhuTung NVARCHAR(100) NOT NULL,
    DonGiaNhap DECIMAL(18,2) NOT NULL,
    DonViTinh NVARCHAR(30) NOT NULL,
    SoLuongTon INT NOT NULL DEFAULT 0,
    TrangThai BIT NOT NULL
        CONSTRAINT DF_PhuTung_TrangThai DEFAULT 1,

    CONSTRAINT CK_PhuTung_DonGiaNhap
        CHECK (DonGiaNhap >= 0),

    CONSTRAINT CK_PhuTung_SoLuongTon
        CHECK (SoLuongTon >= 0)
);
GO

CREATE TABLE PhieuNhap
(
    MaPhieuNhap VARCHAR(10) PRIMARY KEY,
    NgayNhap DATE NOT NULL,
    NguoiNhap NVARCHAR(100) NOT NULL
);
GO

CREATE TABLE ChiTietPhieuNhap
(
    MaCTPN INT IDENTITY(1,1) PRIMARY KEY,
    MaPhieuNhap VARCHAR(10) NOT NULL,
    MaNCC VARCHAR(10) NOT NULL,
    MaPhuTung VARCHAR(10) NOT NULL,
    SoLuong INT NOT NULL,
    DonGiaNhap DECIMAL(18,2) NOT NULL,
    HanSuDung DATE NULL,

    ThanhTien AS (SoLuong * DonGiaNhap) PERSISTED,

    CONSTRAINT FK_CTPN_PhieuNhap
        FOREIGN KEY (MaPhieuNhap)
        REFERENCES PhieuNhap(MaPhieuNhap),

    CONSTRAINT FK_CTPN_NhaCungCap
        FOREIGN KEY (MaNCC)
        REFERENCES NhaCungCap(MaNCC),

    CONSTRAINT FK_CTPN_PhuTung
        FOREIGN KEY (MaPhuTung)
        REFERENCES PhuTung(MaPhuTung),

    CONSTRAINT CK_CTPN_SoLuong
        CHECK (SoLuong > 0),

    CONSTRAINT CK_CTPN_DonGia
        CHECK (DonGiaNhap >= 0)
);
GO

INSERT INTO NhaCungCap
    (MaNCC, TenNCC, SoDienThoai, DiaChi)
VALUES
('NCC01', N'Công ty Dầu nhớt Minh Phát', '0901234567', N'Hà Nội'),
('NCC02', N'Phụ tùng xe máy Hoàng Nam', '0912345678', N'Hải Phòng'),
('NCC03', N'Nhà phân phối An Phát', '0987654321', N'Bắc Ninh'),
('NCC04', N'Cửa hàng phụ tùng Thành Công', '0978123456', N'Nam Định'),
('NCC05', N'Công ty phụ tùng Việt Nhật', '0967123456', N'Hưng Yên');
GO

INSERT INTO PhuTung
    (MaPhuTung, TenPhuTung, DonGiaNhap, DonViTinh)
VALUES
('PT001', N'Dầu nhớt Castrol', 80000, N'Chai'),
('PT002', N'Bugi NGK', 25000, N'Cái'),
('PT003', N'Ruột xe máy', 45000, N'Cái'),
('PT004', N'Má phanh trước', 60000, N'Bộ'),
('PT005', N'Lọc gió xe máy', 35000, N'Cái'),
('PT006', N'Nhông xích xe máy', 120000, N'Bộ'),
('PT007', N'Ắc quy xe máy', 280000, N'Bình'),
('PT008', N'Đèn pha xe máy', 95000, N'Cái'),
('PT009', N'Gương chiếu hậu', 40000, N'Cái'),
('PT010', N'Dây phanh xe máy', 30000, N'Sợi'),
('PT011', N'Bộ nồi xe máy', 350000, N'Bộ'),
('PT012', N'Dây curoa', 150000, N'Sợi');
GO

INSERT INTO PhieuNhap
    (MaPhieuNhap, NgayNhap, NguoiNhap)
VALUES
('PN001', '20261001', N'Nguyễn Văn An'),
('PN002', '20261005', N'Trần Văn Bình'),
('PN003', '20261010', N'Lê Minh Cường'),
('PN004', '20261015', N'Phạm Thu Hà'),
('PN005', '20261018', N'Hoàng Đức Long');
GO

INSERT INTO ChiTietPhieuNhap
    (MaPhieuNhap, MaNCC, MaPhuTung, SoLuong, DonGiaNhap, HanSuDung)
VALUES
('PN001', 'NCC01', 'PT001', 10, 80000, '20281001'),
('PN001', 'NCC02', 'PT002', 20, 25000, NULL),
('PN001', 'NCC03', 'PT005', 12, 35000, NULL),
('PN002', 'NCC02', 'PT003', 15, 45000, NULL),
('PN002', 'NCC01', 'PT004', 10, 60000, NULL),
('PN002', 'NCC03', 'PT006', 5, 120000, NULL),
('PN003', 'NCC04', 'PT007', 8, 280000, '20281010'),
('PN003', 'NCC05', 'PT008', 10, 95000, NULL),
('PN003', 'NCC01', 'PT009', 15, 40000, NULL),
('PN004', 'NCC02', 'PT010', 20, 30000, NULL),
('PN004', 'NCC04', 'PT011', 4, 350000, NULL),
('PN004', 'NCC05', 'PT012', 6, 150000, '20281015'),
('PN005', 'NCC03', 'PT001', 5, 79000, '20281018'),
('PN005', 'NCC05', 'PT003', 10, 44000, NULL),
('PN005', 'NCC02', 'PT007', 3, 275000, '20281020');
GO

UPDATE PT
SET SoLuongTon = ISNULL(Tong.SoLuong, 0)
FROM PhuTung PT
OUTER APPLY
(
    SELECT SUM(CT.SoLuong) AS SoLuong
    FROM ChiTietPhieuNhap CT
    WHERE CT.MaPhuTung = PT.MaPhuTung
) Tong;
GO

SELECT * FROM NhaCungCap;
SELECT * FROM PhuTung;
SELECT * FROM PhieuNhap;
SELECT * FROM ChiTietPhieuNhap;
GO

SELECT
    pn.MaPhieuNhap,
    pn.NgayNhap,
    pn.NguoiNhap,
    ncc.MaNCC,
    ncc.TenNCC,
    pt.MaPhuTung,
    pt.TenPhuTung,
    ct.SoLuong AS SoLuongNhap,
    ct.DonGiaNhap,
    ct.ThanhTien,
    ct.HanSuDung,
    pt.SoLuongTon,
    pt.TrangThai
FROM PhieuNhap pn
JOIN ChiTietPhieuNhap ct
    ON pn.MaPhieuNhap = ct.MaPhieuNhap
JOIN NhaCungCap ncc
    ON ct.MaNCC = ncc.MaNCC
JOIN PhuTung pt
    ON ct.MaPhuTung = pt.MaPhuTung
ORDER BY pn.MaPhieuNhap, ct.MaCTPN;
GO

UPDATE ChiTietPhieuNhap
SET HanSuDung = CASE MaCTPN
    WHEN 2 THEN '20271001'
    WHEN 3 THEN '20271005'
    WHEN 4 THEN '20271010'
    WHEN 5 THEN '20271015'
    WHEN 6 THEN '20271020'
    WHEN 8 THEN '20281010'
    WHEN 9 THEN '20281015'
    WHEN 10 THEN '20281020'
    WHEN 11 THEN '20281025'
    WHEN 13 THEN '20281018'
    WHEN 14 THEN '20281020'
    ELSE HanSuDung
END
WHERE HanSuDung IS NULL;