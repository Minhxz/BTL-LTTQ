CREATE DATABASE QuanLyNhapKho;
GO

USE QuanLyNhapKho;
GO

CREATE TABLE NhaCungCap
(
    MaNCC VARCHAR(10) PRIMARY KEY,
    TenNCC NVARCHAR(100) NOT NULL,
    SoDienThoai VARCHAR(15),
    DiaChi NVARCHAR(200)
);
GO

CREATE TABLE PhuTung
(
    MaPhuTung VARCHAR(10) PRIMARY KEY,
    TenPhuTung NVARCHAR(100) NOT NULL,
    DonGiaNhap DECIMAL(18,2) NOT NULL,
    DonViTinh NVARCHAR(30),
    SoLuongTon INT NOT NULL DEFAULT 0
);
GO

CREATE TABLE PhieuNhap
(
    MaPhieuNhap VARCHAR(10) PRIMARY KEY,
    MaNCC VARCHAR(10) NOT NULL,
    NgayNhap DATE NOT NULL,
    NguoiNhap NVARCHAR(100),

    CONSTRAINT FK_PhieuNhap_NhaCungCap
        FOREIGN KEY (MaNCC) REFERENCES NhaCungCap(MaNCC)
);
GO

CREATE TABLE ChiTietPhieuNhap
(
    MaCTPN INT IDENTITY(1,1) PRIMARY KEY,
    MaPhieuNhap VARCHAR(10) NOT NULL,
    MaPhuTung VARCHAR(10) NOT NULL,
    SoLuong INT NOT NULL,
    DonGiaNhap DECIMAL(18,2) NOT NULL,
    HanSuDung DATE NULL,

    ThanhTien AS (SoLuong * DonGiaNhap) PERSISTED,

    CONSTRAINT FK_CTPN_PhieuNhap
        FOREIGN KEY (MaPhieuNhap) REFERENCES PhieuNhap(MaPhieuNhap),

    CONSTRAINT FK_CTPN_PhuTung
        FOREIGN KEY (MaPhuTung) REFERENCES PhuTung(MaPhuTung),

    CONSTRAINT CK_CTPN_SoLuong CHECK (SoLuong > 0),
    CONSTRAINT CK_CTPN_DonGia CHECK (DonGiaNhap >= 0)
);
GO

INSERT INTO NhaCungCap (MaNCC, TenNCC, SoDienThoai, DiaChi)
VALUES
('NCC01', N'Công ty Dầu nhớt Minh Phát', '0901234567', N'Hà Nội'),
('NCC02', N'Phụ tùng xe máy Hoàng Nam', '0912345678', N'Hải Phòng'),
('NCC03', N'Nhà phân phối An Phát', '0987654321', N'Bắc Ninh');
GO

INSERT INTO PhuTung
    (MaPhuTung, TenPhuTung, DonGiaNhap, DonViTinh, SoLuongTon)
VALUES
('PT001', N'Dầu nhớt Castrol', 80000, N'Chai', 0),
('PT002', N'Bugi NGK', 25000, N'Cái', 0),
('PT003', N'Ruột xe máy', 45000, N'Cái', 0),
('PT004', N'Má phanh trước', 60000, N'Bộ', 0),
('PT005', N'Lọc gió xe máy', 35000, N'Cái', 0),
('PT006', N'Nhông xích xe máy', 120000, N'Bộ', 0);
GO

INSERT INTO PhieuNhap (MaPhieuNhap, MaNCC, NgayNhap, NguoiNhap)
VALUES
('PN001', 'NCC01', '20261001', N'Nguyễn Văn An'),
('PN002', 'NCC02', '20261005', N'Trần Văn Bình');
GO

INSERT INTO ChiTietPhieuNhap
    (MaPhieuNhap, MaPhuTung, SoLuong, DonGiaNhap, HanSuDung)
VALUES
('PN001', 'PT001', 10, 80000, '20281001'),
('PN001', 'PT002', 20, 25000, NULL),
('PN001', 'PT005', 12, 35000, NULL),
('PN002', 'PT003', 15, 45000, NULL),
('PN002', 'PT004', 10, 60000, NULL),
('PN002', 'PT006', 5, 120000, NULL);
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