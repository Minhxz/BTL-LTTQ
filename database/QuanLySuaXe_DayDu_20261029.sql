USE [master];
GO
IF DB_ID(N'QuanLySuaXe') IS NOT NULL BEGIN PRINT N'Database đã tồn tại. Hãy cài vào một database mới, không ghi đè dữ liệu hiện có.'; SET NOEXEC ON; END;
GO
CREATE DATABASE [QuanLySuaXe] COLLATE Vietnamese_CI_AS;
GO
USE [QuanLySuaXe];
GO
SET XACT_ABORT ON; SET NOCOUNT ON; SET ANSI_NULLS ON; SET QUOTED_IDENTIFIER ON; SET ANSI_PADDING ON; SET ANSI_WARNINGS ON; SET ARITHABORT ON; SET CONCAT_NULL_YIELDS_NULL ON; SET NUMERIC_ROUNDABORT OFF;
GO
CREATE TABLE dbo.[CaLam] (
    [MaCaLam] varchar(10) COLLATE Vietnamese_CI_AS NOT NULL,
    [ThoiGianCaLam] decimal(4,2) NOT NULL,
    [ThoiGianBatDau] time(0) NOT NULL,
    [ThoiGianKetThuc] time(0) NOT NULL
);
GO
ALTER TABLE dbo.[CaLam] ADD CONSTRAINT [PK_CaLam] PRIMARY KEY CLUSTERED ([MaCaLam] ASC);
GO
ALTER TABLE dbo.[CaLam] WITH CHECK ADD CONSTRAINT [CK_CaLam_ThoiGian] CHECK ([ThoiGianKetThuc]>[ThoiGianBatDau] AND [ThoiGianCaLam]>(0));
GO
CREATE TABLE dbo.[ChiTietCaLam] (
    [MaNhanVien] varchar(10) COLLATE Vietnamese_CI_AS NOT NULL,
    [MaCaLam] varchar(10) COLLATE Vietnamese_CI_AS NOT NULL,
    [NgayLam] date NOT NULL,
    [TongThoiGianLamThucTe] decimal(4,2) NOT NULL
);
GO
ALTER TABLE dbo.[ChiTietCaLam] ADD CONSTRAINT [PK_ChiTietCaLam] PRIMARY KEY CLUSTERED ([MaNhanVien] ASC,[MaCaLam] ASC,[NgayLam] ASC);
GO
ALTER TABLE dbo.[ChiTietCaLam] WITH CHECK ADD CONSTRAINT [CK_CTCL_TongThoiGian] CHECK ([TongThoiGianLamThucTe]>=(0));
GO
CREATE TABLE dbo.[ChiTietDichVu] (
    [MaDichVu] varchar(10) COLLATE Vietnamese_CI_AS NOT NULL,
    [MaPhuTung] varchar(10) COLLATE Vietnamese_CI_AS NOT NULL,
    [SoLuong] int NOT NULL
);
GO
ALTER TABLE dbo.[ChiTietDichVu] ADD CONSTRAINT [PK_ChiTietDichVu] PRIMARY KEY CLUSTERED ([MaDichVu] ASC,[MaPhuTung] ASC);
GO
ALTER TABLE dbo.[ChiTietDichVu] WITH CHECK ADD CONSTRAINT [CK_CTDV_SoLuong] CHECK ([SoLuong]>(0));
GO
CREATE TABLE dbo.[ChiTietPhanCong] (
    [MaNhanVien] varchar(10) COLLATE Vietnamese_CI_AS NOT NULL,
    [MaXe] varchar(10) COLLATE Vietnamese_CI_AS NOT NULL,
    [ThoiGianBatDau] datetime2(0) NOT NULL,
    [ThoiGianHoanThanh] datetime2(0) NOT NULL
);
GO
ALTER TABLE dbo.[ChiTietPhanCong] ADD CONSTRAINT [PK_ChiTietPhanCong] PRIMARY KEY CLUSTERED ([MaNhanVien] ASC,[MaXe] ASC);
GO
ALTER TABLE dbo.[ChiTietPhanCong] WITH CHECK ADD CONSTRAINT [CK_CTPC_ThoiGian] CHECK ([ThoiGianHoanThanh]>[ThoiGianBatDau]);
GO
CREATE TABLE dbo.[ChiTietPhieuNhap] (
    [MaPhieuNhap] varchar(10) COLLATE Vietnamese_CI_AS NOT NULL,
    [MaPhuTung] varchar(10) COLLATE Vietnamese_CI_AS NOT NULL,
    [SoLuongNhap] int NOT NULL,
    [ThanhTien] decimal(18,0) NOT NULL
);
GO
ALTER TABLE dbo.[ChiTietPhieuNhap] ADD CONSTRAINT [PK_ChiTietPhieuNhap] PRIMARY KEY CLUSTERED ([MaPhieuNhap] ASC,[MaPhuTung] ASC);
GO
ALTER TABLE dbo.[ChiTietPhieuNhap] WITH CHECK ADD CONSTRAINT [CK_CTPN_SoLuong] CHECK ([SoLuongNhap]>(0) AND [ThanhTien]>=(0));
GO
CREATE TABLE dbo.[ChiTietPhieuTiepNhan] (
    [MaPhieuTiepNhan] varchar(10) COLLATE Vietnamese_CI_AS NOT NULL,
    [MaDichVu] varchar(10) COLLATE Vietnamese_CI_AS NOT NULL,
    [SoLuong] int NOT NULL,
    [ThanhTien] decimal(18,0) NOT NULL
);
GO
ALTER TABLE dbo.[ChiTietPhieuTiepNhan] ADD CONSTRAINT [PK_ChiTietPhieuTiepNhan] PRIMARY KEY CLUSTERED ([MaPhieuTiepNhan] ASC,[MaDichVu] ASC);
GO
ALTER TABLE dbo.[ChiTietPhieuTiepNhan] WITH CHECK ADD CONSTRAINT [CK_CTPTN_SoLuong] CHECK ([SoLuong]>(0) AND [ThanhTien]>=(0));
GO
CREATE TABLE dbo.[ChucVu] (
    [MaChucVu] varchar(10) COLLATE Vietnamese_CI_AS NOT NULL,
    [TenChucVu] nvarchar(100) COLLATE Vietnamese_CI_AS NOT NULL,
    [MoTa] nvarchar(255) COLLATE Vietnamese_CI_AS NOT NULL,
    [HeSoLuong] decimal(4,2) NOT NULL,
    [TienDieuChinh] decimal(18,0) NOT NULL
);
GO
ALTER TABLE dbo.[ChucVu] ADD CONSTRAINT [PK_ChucVu] PRIMARY KEY CLUSTERED ([MaChucVu] ASC);
GO
ALTER TABLE dbo.[ChucVu] ADD CONSTRAINT [UQ_ChucVu_Ten] UNIQUE NONCLUSTERED ([TenChucVu] ASC);
GO
ALTER TABLE dbo.[ChucVu] WITH CHECK ADD CONSTRAINT [CK_ChucVu_HeSoLuong] CHECK ([HeSoLuong]>(0));
GO
CREATE TABLE dbo.[ChucVuDieuChinh] (
    [MaChucVu] varchar(10) COLLATE Vietnamese_CI_AS NOT NULL,
    [MaKhoanDieuChinh] varchar(10) COLLATE Vietnamese_CI_AS NOT NULL
);
GO
ALTER TABLE dbo.[ChucVuDieuChinh] ADD CONSTRAINT [PK_ChucVuDieuChinh] PRIMARY KEY CLUSTERED ([MaChucVu] ASC,[MaKhoanDieuChinh] ASC);
GO
CREATE TABLE dbo.[DichVu] (
    [MaDichVu] varchar(10) COLLATE Vietnamese_CI_AS NOT NULL,
    [TenDichVu] nvarchar(150) COLLATE Vietnamese_CI_AS NOT NULL,
    [MoTa] nvarchar(255) COLLATE Vietnamese_CI_AS NOT NULL,
    [ThanhTienDichVu] decimal(18,0) NOT NULL,
    [TrangThai] nvarchar(20) COLLATE Vietnamese_CI_AS NOT NULL
);
GO
ALTER TABLE dbo.[DichVu] ADD CONSTRAINT [PK_DichVu] PRIMARY KEY CLUSTERED ([MaDichVu] ASC);
GO
ALTER TABLE dbo.[DichVu] WITH CHECK ADD CONSTRAINT [CK_DichVu_Gia] CHECK ([ThanhTienDichVu]>(0));
GO
ALTER TABLE dbo.[DichVu] WITH CHECK ADD CONSTRAINT [CK_DichVu_TrangThai] CHECK ([TrangThai]=N'Ngừng áp dụng' OR [TrangThai]=N'Đang áp dụng');
GO
CREATE TABLE dbo.[DieuChinhGiaBan] (
    [MaDieuChinhGia] varchar(10) COLLATE Vietnamese_CI_AS NOT NULL,
    [MaPhuTung] varchar(10) COLLATE Vietnamese_CI_AS NOT NULL,
    [MaNhanVien] varchar(10) COLLATE Vietnamese_CI_AS NOT NULL,
    [NgayDieuChinh] date NOT NULL,
    [GiaBanCu] decimal(18,0) NOT NULL,
    [GiaBanMoi] decimal(18,0) NOT NULL,
    [LyDo] nvarchar(255) COLLATE Vietnamese_CI_AS NOT NULL
);
GO
ALTER TABLE dbo.[DieuChinhGiaBan] ADD CONSTRAINT [PK_DieuChinhGiaBan] PRIMARY KEY CLUSTERED ([MaDieuChinhGia] ASC);
GO
CREATE NONCLUSTERED INDEX [IX_DCGB_PhuTung_Ngay] ON dbo.[DieuChinhGiaBan] ([MaPhuTung] ASC,[NgayDieuChinh] DESC,[MaDieuChinhGia] DESC) INCLUDE ([MaNhanVien],[GiaBanCu],[GiaBanMoi]);
GO
CREATE NONCLUSTERED INDEX [IX_DCGB_NhanVien_Ngay] ON dbo.[DieuChinhGiaBan] ([MaNhanVien] ASC,[NgayDieuChinh] DESC);
GO
ALTER TABLE dbo.[DieuChinhGiaBan] WITH CHECK ADD CONSTRAINT [CK_DCGB_Gia] CHECK ([GiaBanCu]>(0) AND [GiaBanMoi]>(0) AND [GiaBanMoi]<>[GiaBanCu]);
GO
ALTER TABLE dbo.[DieuChinhGiaBan] WITH CHECK ADD CONSTRAINT [CK_DCGB_LyDo] CHECK (len(ltrim(rtrim([LyDo])))>(0));
GO
ALTER TABLE dbo.[DieuChinhGiaBan] WITH CHECK ADD CONSTRAINT [CK_DCGB_Ma] CHECK (len([MaDieuChinhGia])>=(5) AND len([MaDieuChinhGia])<=(10) AND left([MaDieuChinhGia],(2))='DG' AND NOT substring([MaDieuChinhGia],(3),(8)) like '%[^0-9]%');
GO
CREATE TABLE dbo.[DieuChinhTonKho] (
    [MaDieuChinh] varchar(10) COLLATE Vietnamese_CI_AS NOT NULL,
    [MaPhuTung] varchar(10) COLLATE Vietnamese_CI_AS NOT NULL,
    [MaNhanVien] varchar(10) COLLATE Vietnamese_CI_AS NOT NULL,
    [NgayDieuChinh] date NOT NULL,
    [SoLuongCu] int NOT NULL,
    [SoLuongMoi] int NOT NULL,
    [DonGiaNhap] decimal(18,0) NOT NULL,
    [GiaTriChenhLech] AS (([SoLuongMoi]-[SoLuongCu])*[DonGiaNhap]) PERSISTED NOT NULL,
    [LyDo] nvarchar(255) COLLATE Vietnamese_CI_AS NOT NULL
);
GO
ALTER TABLE dbo.[DieuChinhTonKho] ADD CONSTRAINT [PK_DieuChinhTonKho] PRIMARY KEY CLUSTERED ([MaDieuChinh] ASC);
GO
CREATE NONCLUSTERED INDEX [IX_DCTK_PhuTung_Ngay] ON dbo.[DieuChinhTonKho] ([MaPhuTung] ASC,[NgayDieuChinh] DESC,[MaDieuChinh] DESC) INCLUDE ([MaNhanVien],[SoLuongCu],[SoLuongMoi],[DonGiaNhap]);
GO
CREATE NONCLUSTERED INDEX [IX_DCTK_NhanVien_Ngay] ON dbo.[DieuChinhTonKho] ([MaNhanVien] ASC,[NgayDieuChinh] DESC);
GO
ALTER TABLE dbo.[DieuChinhTonKho] WITH CHECK ADD CONSTRAINT [CK_DCTK_SoLuong] CHECK ([SoLuongCu]>=(0) AND [SoLuongMoi]>=(0) AND [SoLuongMoi]<>[SoLuongCu]);
GO
ALTER TABLE dbo.[DieuChinhTonKho] WITH CHECK ADD CONSTRAINT [CK_DCTK_DonGiaNhap] CHECK ([DonGiaNhap]>(0));
GO
ALTER TABLE dbo.[DieuChinhTonKho] WITH CHECK ADD CONSTRAINT [CK_DCTK_LyDo] CHECK (len(ltrim(rtrim([LyDo])))>(0));
GO
ALTER TABLE dbo.[DieuChinhTonKho] WITH CHECK ADD CONSTRAINT [CK_DCTK_Ma] CHECK (len([MaDieuChinh])>=(5) AND len([MaDieuChinh])<=(10) AND left([MaDieuChinh],(2))='DC' AND NOT substring([MaDieuChinh],(3),(8)) like '%[^0-9]%');
GO
CREATE TABLE dbo.[HoaDon] (
    [MaHoaDon] varchar(10) COLLATE Vietnamese_CI_AS NOT NULL,
    [MaPhieuTiepNhan] varchar(10) COLLATE Vietnamese_CI_AS NOT NULL,
    [TienThanhToan] decimal(18,0) NOT NULL,
    [NgayLap] date NOT NULL,
    [NgayThanhToan] date NOT NULL
);
GO
ALTER TABLE dbo.[HoaDon] ADD CONSTRAINT [PK_HoaDon] PRIMARY KEY CLUSTERED ([MaHoaDon] ASC);
GO
ALTER TABLE dbo.[HoaDon] ADD CONSTRAINT [UQ_HoaDon_PhieuTiepNhan] UNIQUE NONCLUSTERED ([MaPhieuTiepNhan] ASC);
GO
ALTER TABLE dbo.[HoaDon] WITH CHECK ADD CONSTRAINT [CK_HoaDon_Ngay] CHECK ([NgayThanhToan]>=[NgayLap]);
GO
ALTER TABLE dbo.[HoaDon] WITH CHECK ADD CONSTRAINT [CK_HoaDon_Tien] CHECK ([TienThanhToan]>=(0));
GO
CREATE TABLE dbo.[KhachHang] (
    [MaKhachHang] varchar(10) COLLATE Vietnamese_CI_AS NOT NULL,
    [TenKhachHang] nvarchar(100) COLLATE Vietnamese_CI_AS NOT NULL,
    [SoDienThoai] varchar(15) COLLATE Vietnamese_CI_AS NOT NULL
);
GO
ALTER TABLE dbo.[KhachHang] ADD CONSTRAINT [PK_KhachHang] PRIMARY KEY CLUSTERED ([MaKhachHang] ASC);
GO
ALTER TABLE dbo.[KhachHang] ADD CONSTRAINT [UQ_KhachHang_SoDienThoai] UNIQUE NONCLUSTERED ([SoDienThoai] ASC);
GO
CREATE TABLE dbo.[KhoanDieuChinh] (
    [MaKhoanDieuChinh] varchar(10) COLLATE Vietnamese_CI_AS NOT NULL,
    [TenKhoanDieuChinh] nvarchar(150) COLLATE Vietnamese_CI_AS NOT NULL,
    [SoTien] decimal(18,0) NOT NULL
);
GO
ALTER TABLE dbo.[KhoanDieuChinh] ADD CONSTRAINT [PK_KhoanDieuChinh] PRIMARY KEY CLUSTERED ([MaKhoanDieuChinh] ASC);
GO
ALTER TABLE dbo.[KhoanDieuChinh] WITH CHECK ADD CONSTRAINT [CK_KhoanDieuChinh_SoTien] CHECK ([SoTien]<>(0));
GO
CREATE TABLE dbo.[NhaCungCap] (
    [MaNhaCungCap] varchar(10) COLLATE Vietnamese_CI_AS NOT NULL,
    [TenNhaCungCap] nvarchar(150) COLLATE Vietnamese_CI_AS NOT NULL,
    [SoDienThoai] varchar(15) COLLATE Vietnamese_CI_AS NOT NULL,
    [DiaChi] nvarchar(255) COLLATE Vietnamese_CI_AS NOT NULL
);
GO
ALTER TABLE dbo.[NhaCungCap] ADD CONSTRAINT [PK_NhaCungCap] PRIMARY KEY CLUSTERED ([MaNhaCungCap] ASC);
GO
CREATE TABLE dbo.[NhanVien] (
    [MaNhanVien] varchar(10) COLLATE Vietnamese_CI_AS NOT NULL,
    [MaChucVu] varchar(10) COLLATE Vietnamese_CI_AS NOT NULL,
    [HoTen] nvarchar(100) COLLATE Vietnamese_CI_AS NOT NULL,
    [SoDienThoai] varchar(15) COLLATE Vietnamese_CI_AS NOT NULL,
    [Luong] decimal(18,0) NOT NULL,
    [TenDangNhap] varchar(50) COLLATE Vietnamese_CI_AS NOT NULL,
    [MatKhau] varchar(64) COLLATE Vietnamese_CI_AS NOT NULL,
    [TrangThai] nvarchar(20) COLLATE Vietnamese_CI_AS NOT NULL
);
GO
ALTER TABLE dbo.[NhanVien] ADD CONSTRAINT [PK_NhanVien] PRIMARY KEY CLUSTERED ([MaNhanVien] ASC);
GO
ALTER TABLE dbo.[NhanVien] ADD CONSTRAINT [UQ_NhanVien_TenDangNhap] UNIQUE NONCLUSTERED ([TenDangNhap] ASC);
GO
ALTER TABLE dbo.[NhanVien] ADD CONSTRAINT [UQ_NhanVien_SoDienThoai] UNIQUE NONCLUSTERED ([SoDienThoai] ASC);
GO
ALTER TABLE dbo.[NhanVien] WITH CHECK ADD CONSTRAINT [CK_NhanVien_Luong] CHECK ([Luong]>(0));
GO
ALTER TABLE dbo.[NhanVien] WITH CHECK ADD CONSTRAINT [CK_NhanVien_TrangThai] CHECK ([TrangThai]=N'Nghỉ việc' OR [TrangThai]=N'Đang làm');
GO
CREATE TABLE dbo.[PhieuNhap] (
    [MaPhieuNhap] varchar(10) COLLATE Vietnamese_CI_AS NOT NULL,
    [MaNhanVienNhap] varchar(10) COLLATE Vietnamese_CI_AS NOT NULL,
    [NgayNhap] date NOT NULL,
    [GhiChu] nvarchar(255) COLLATE Vietnamese_CI_AS NOT NULL,
    [TongTienNhap] decimal(18,0) NOT NULL
);
GO
ALTER TABLE dbo.[PhieuNhap] ADD CONSTRAINT [PK_PhieuNhap] PRIMARY KEY CLUSTERED ([MaPhieuNhap] ASC);
GO
ALTER TABLE dbo.[PhieuNhap] WITH CHECK ADD CONSTRAINT [CK_PhieuNhap_TongTien] CHECK ([TongTienNhap]>=(0));
GO
CREATE TABLE dbo.[PhieuTiepNhan] (
    [MaPhieuTiepNhan] varchar(10) COLLATE Vietnamese_CI_AS NOT NULL,
    [MaXe] varchar(10) COLLATE Vietnamese_CI_AS NOT NULL,
    [MaNhanVien] varchar(10) COLLATE Vietnamese_CI_AS NOT NULL,
    [NgayLap] date NOT NULL,
    [NgayHen] date NOT NULL,
    [TrangThai] nvarchar(20) COLLATE Vietnamese_CI_AS NOT NULL
);
GO
ALTER TABLE dbo.[PhieuTiepNhan] ADD CONSTRAINT [PK_PhieuTiepNhan] PRIMARY KEY CLUSTERED ([MaPhieuTiepNhan] ASC);
GO
ALTER TABLE dbo.[PhieuTiepNhan] WITH CHECK ADD CONSTRAINT [CK_PTN_NgayHen] CHECK ([NgayHen]>=[NgayLap]);
GO
ALTER TABLE dbo.[PhieuTiepNhan] WITH CHECK ADD CONSTRAINT [CK_PTN_TrangThai] CHECK ([TrangThai]=N'Đã hủy' OR [TrangThai]=N'Hoàn thành' OR [TrangThai]=N'Đang sửa' OR [TrangThai]=N'Chờ sửa');
GO
CREATE TABLE dbo.[PhuTung] (
    [MaPhuTung] varchar(10) COLLATE Vietnamese_CI_AS NOT NULL,
    [MaNhaCungCap] varchar(10) COLLATE Vietnamese_CI_AS NOT NULL,
    [TenPhuTung] nvarchar(150) COLLATE Vietnamese_CI_AS NOT NULL,
    [DonGiaNhap] decimal(18,0) NOT NULL,
    [DonGiaBan] decimal(18,0) NOT NULL,
    [SoLuongTon] int NOT NULL,
    [HanSuDung] date NOT NULL,
    [TrangThai] nvarchar(20) COLLATE Vietnamese_CI_AS NOT NULL
);
GO
ALTER TABLE dbo.[PhuTung] ADD CONSTRAINT [PK_PhuTung] PRIMARY KEY CLUSTERED ([MaPhuTung] ASC);
GO
ALTER TABLE dbo.[PhuTung] WITH CHECK ADD CONSTRAINT [CK_PhuTung_Gia] CHECK ([DonGiaNhap]>(0) AND [DonGiaBan]>(0));
GO
ALTER TABLE dbo.[PhuTung] WITH CHECK ADD CONSTRAINT [CK_PhuTung_SoLuongTon] CHECK ([SoLuongTon]>=(0));
GO
ALTER TABLE dbo.[PhuTung] WITH CHECK ADD CONSTRAINT [CK_PhuTung_TrangThai] CHECK ([TrangThai]=N'Ngừng kinh doanh' OR [TrangThai]=N'Đang kinh doanh');
GO
CREATE TABLE dbo.[Xe] (
    [MaXe] varchar(10) COLLATE Vietnamese_CI_AS NOT NULL,
    [MaKhachHang] varchar(10) COLLATE Vietnamese_CI_AS NOT NULL,
    [BienXe] varchar(20) COLLATE Vietnamese_CI_AS NOT NULL,
    [LoaiXe] nvarchar(50) COLLATE Vietnamese_CI_AS NOT NULL,
    [HangSanXuat] nvarchar(50) COLLATE Vietnamese_CI_AS NOT NULL,
    [MauSac] nvarchar(30) COLLATE Vietnamese_CI_AS NOT NULL
);
GO
ALTER TABLE dbo.[Xe] ADD CONSTRAINT [PK_Xe] PRIMARY KEY CLUSTERED ([MaXe] ASC);
GO
INSERT dbo.[CaLam] ([MaCaLam],[ThoiGianCaLam],[ThoiGianBatDau],[ThoiGianKetThuc]) VALUES (N'CA001',4.50,N'07:30:00',N'12:00:00');
GO
INSERT dbo.[CaLam] ([MaCaLam],[ThoiGianCaLam],[ThoiGianBatDau],[ThoiGianKetThuc]) VALUES (N'CA002',4.50,N'13:00:00',N'17:30:00');
GO
INSERT dbo.[CaLam] ([MaCaLam],[ThoiGianCaLam],[ThoiGianBatDau],[ThoiGianKetThuc]) VALUES (N'CA003',3.50,N'17:30:00',N'21:00:00');
GO
INSERT dbo.[ChiTietCaLam] ([MaNhanVien],[MaCaLam],[NgayLam],[TongThoiGianLamThucTe]) VALUES (N'NV001',N'CA001',N'2026-10-21T00:00:00.0000000',4.50);
GO
INSERT dbo.[ChiTietCaLam] ([MaNhanVien],[MaCaLam],[NgayLam],[TongThoiGianLamThucTe]) VALUES (N'NV001',N'CA001',N'2026-10-23T00:00:00.0000000',4.50);
GO
INSERT dbo.[ChiTietCaLam] ([MaNhanVien],[MaCaLam],[NgayLam],[TongThoiGianLamThucTe]) VALUES (N'NV001',N'CA001',N'2026-10-24T00:00:00.0000000',4.25);
GO
INSERT dbo.[ChiTietCaLam] ([MaNhanVien],[MaCaLam],[NgayLam],[TongThoiGianLamThucTe]) VALUES (N'NV001',N'CA001',N'2026-10-25T00:00:00.0000000',5.00);
GO
INSERT dbo.[ChiTietCaLam] ([MaNhanVien],[MaCaLam],[NgayLam],[TongThoiGianLamThucTe]) VALUES (N'NV001',N'CA001',N'2026-10-26T00:00:00.0000000',4.50);
GO
INSERT dbo.[ChiTietCaLam] ([MaNhanVien],[MaCaLam],[NgayLam],[TongThoiGianLamThucTe]) VALUES (N'NV001',N'CA002',N'2026-10-23T00:00:00.0000000',4.75);
GO
INSERT dbo.[ChiTietCaLam] ([MaNhanVien],[MaCaLam],[NgayLam],[TongThoiGianLamThucTe]) VALUES (N'NV001',N'CA002',N'2026-10-24T00:00:00.0000000',4.50);
GO
INSERT dbo.[ChiTietCaLam] ([MaNhanVien],[MaCaLam],[NgayLam],[TongThoiGianLamThucTe]) VALUES (N'NV001',N'CA002',N'2026-10-26T00:00:00.0000000',4.50);
GO
INSERT dbo.[ChiTietCaLam] ([MaNhanVien],[MaCaLam],[NgayLam],[TongThoiGianLamThucTe]) VALUES (N'NV001',N'CA002',N'2026-10-27T00:00:00.0000000',4.25);
GO
INSERT dbo.[ChiTietCaLam] ([MaNhanVien],[MaCaLam],[NgayLam],[TongThoiGianLamThucTe]) VALUES (N'NV001',N'CA003',N'2026-10-27T00:00:00.0000000',3.50);
GO
INSERT dbo.[ChiTietCaLam] ([MaNhanVien],[MaCaLam],[NgayLam],[TongThoiGianLamThucTe]) VALUES (N'NV001',N'CA003',N'2026-10-28T00:00:00.0000000',3.50);
GO
INSERT dbo.[ChiTietCaLam] ([MaNhanVien],[MaCaLam],[NgayLam],[TongThoiGianLamThucTe]) VALUES (N'NV002',N'CA001',N'2026-10-21T00:00:00.0000000',4.50);
GO
INSERT dbo.[ChiTietCaLam] ([MaNhanVien],[MaCaLam],[NgayLam],[TongThoiGianLamThucTe]) VALUES (N'NV002',N'CA001',N'2026-10-23T00:00:00.0000000',4.50);
GO
INSERT dbo.[ChiTietCaLam] ([MaNhanVien],[MaCaLam],[NgayLam],[TongThoiGianLamThucTe]) VALUES (N'NV002',N'CA001',N'2026-10-24T00:00:00.0000000',4.75);
GO
INSERT dbo.[ChiTietCaLam] ([MaNhanVien],[MaCaLam],[NgayLam],[TongThoiGianLamThucTe]) VALUES (N'NV002',N'CA001',N'2026-10-26T00:00:00.0000000',4.50);
GO
INSERT dbo.[ChiTietCaLam] ([MaNhanVien],[MaCaLam],[NgayLam],[TongThoiGianLamThucTe]) VALUES (N'NV002',N'CA002',N'2026-10-21T00:00:00.0000000',4.50);
GO
INSERT dbo.[ChiTietCaLam] ([MaNhanVien],[MaCaLam],[NgayLam],[TongThoiGianLamThucTe]) VALUES (N'NV002',N'CA002',N'2026-10-22T00:00:00.0000000',4.50);
GO
INSERT dbo.[ChiTietCaLam] ([MaNhanVien],[MaCaLam],[NgayLam],[TongThoiGianLamThucTe]) VALUES (N'NV002',N'CA002',N'2026-10-23T00:00:00.0000000',4.50);
GO
INSERT dbo.[ChiTietCaLam] ([MaNhanVien],[MaCaLam],[NgayLam],[TongThoiGianLamThucTe]) VALUES (N'NV002',N'CA002',N'2026-10-24T00:00:00.0000000',5.00);
GO
INSERT dbo.[ChiTietCaLam] ([MaNhanVien],[MaCaLam],[NgayLam],[TongThoiGianLamThucTe]) VALUES (N'NV002',N'CA002',N'2026-10-26T00:00:00.0000000',5.00);
GO
INSERT dbo.[ChiTietCaLam] ([MaNhanVien],[MaCaLam],[NgayLam],[TongThoiGianLamThucTe]) VALUES (N'NV002',N'CA002',N'2026-10-27T00:00:00.0000000',4.75);
GO
INSERT dbo.[ChiTietCaLam] ([MaNhanVien],[MaCaLam],[NgayLam],[TongThoiGianLamThucTe]) VALUES (N'NV002',N'CA003',N'2026-10-22T00:00:00.0000000',3.00);
GO
INSERT dbo.[ChiTietCaLam] ([MaNhanVien],[MaCaLam],[NgayLam],[TongThoiGianLamThucTe]) VALUES (N'NV003',N'CA001',N'2026-10-22T00:00:00.0000000',4.50);
GO
INSERT dbo.[ChiTietCaLam] ([MaNhanVien],[MaCaLam],[NgayLam],[TongThoiGianLamThucTe]) VALUES (N'NV003',N'CA001',N'2026-10-23T00:00:00.0000000',4.50);
GO
INSERT dbo.[ChiTietCaLam] ([MaNhanVien],[MaCaLam],[NgayLam],[TongThoiGianLamThucTe]) VALUES (N'NV003',N'CA001',N'2026-10-25T00:00:00.0000000',4.50);
GO
INSERT dbo.[ChiTietCaLam] ([MaNhanVien],[MaCaLam],[NgayLam],[TongThoiGianLamThucTe]) VALUES (N'NV003',N'CA001',N'2026-10-26T00:00:00.0000000',4.00);
GO
INSERT dbo.[ChiTietCaLam] ([MaNhanVien],[MaCaLam],[NgayLam],[TongThoiGianLamThucTe]) VALUES (N'NV003',N'CA002',N'2026-10-22T00:00:00.0000000',4.50);
GO
INSERT dbo.[ChiTietCaLam] ([MaNhanVien],[MaCaLam],[NgayLam],[TongThoiGianLamThucTe]) VALUES (N'NV003',N'CA002',N'2026-10-23T00:00:00.0000000',4.50);
GO
INSERT dbo.[ChiTietCaLam] ([MaNhanVien],[MaCaLam],[NgayLam],[TongThoiGianLamThucTe]) VALUES (N'NV003',N'CA002',N'2026-10-25T00:00:00.0000000',5.00);
GO
INSERT dbo.[ChiTietCaLam] ([MaNhanVien],[MaCaLam],[NgayLam],[TongThoiGianLamThucTe]) VALUES (N'NV003',N'CA002',N'2026-10-26T00:00:00.0000000',4.75);
GO
INSERT dbo.[ChiTietCaLam] ([MaNhanVien],[MaCaLam],[NgayLam],[TongThoiGianLamThucTe]) VALUES (N'NV003',N'CA002',N'2026-10-27T00:00:00.0000000',5.00);
GO
INSERT dbo.[ChiTietCaLam] ([MaNhanVien],[MaCaLam],[NgayLam],[TongThoiGianLamThucTe]) VALUES (N'NV003',N'CA003',N'2026-10-27T00:00:00.0000000',3.50);
GO
INSERT dbo.[ChiTietCaLam] ([MaNhanVien],[MaCaLam],[NgayLam],[TongThoiGianLamThucTe]) VALUES (N'NV004',N'CA001',N'2026-10-21T00:00:00.0000000',4.50);
GO
INSERT dbo.[ChiTietCaLam] ([MaNhanVien],[MaCaLam],[NgayLam],[TongThoiGianLamThucTe]) VALUES (N'NV004',N'CA001',N'2026-10-22T00:00:00.0000000',4.75);
GO
INSERT dbo.[ChiTietCaLam] ([MaNhanVien],[MaCaLam],[NgayLam],[TongThoiGianLamThucTe]) VALUES (N'NV004',N'CA001',N'2026-10-23T00:00:00.0000000',4.50);
GO
INSERT dbo.[ChiTietCaLam] ([MaNhanVien],[MaCaLam],[NgayLam],[TongThoiGianLamThucTe]) VALUES (N'NV004',N'CA001',N'2026-10-25T00:00:00.0000000',4.50);
GO
INSERT dbo.[ChiTietCaLam] ([MaNhanVien],[MaCaLam],[NgayLam],[TongThoiGianLamThucTe]) VALUES (N'NV004',N'CA001',N'2026-10-26T00:00:00.0000000',5.00);
GO
INSERT dbo.[ChiTietCaLam] ([MaNhanVien],[MaCaLam],[NgayLam],[TongThoiGianLamThucTe]) VALUES (N'NV004',N'CA001',N'2026-10-27T00:00:00.0000000',4.75);
GO
INSERT dbo.[ChiTietCaLam] ([MaNhanVien],[MaCaLam],[NgayLam],[TongThoiGianLamThucTe]) VALUES (N'NV004',N'CA002',N'2026-10-21T00:00:00.0000000',4.50);
GO
INSERT dbo.[ChiTietCaLam] ([MaNhanVien],[MaCaLam],[NgayLam],[TongThoiGianLamThucTe]) VALUES (N'NV004',N'CA002',N'2026-10-22T00:00:00.0000000',4.50);
GO
INSERT dbo.[ChiTietCaLam] ([MaNhanVien],[MaCaLam],[NgayLam],[TongThoiGianLamThucTe]) VALUES (N'NV004',N'CA002',N'2026-10-23T00:00:00.0000000',4.25);
GO
INSERT dbo.[ChiTietCaLam] ([MaNhanVien],[MaCaLam],[NgayLam],[TongThoiGianLamThucTe]) VALUES (N'NV004',N'CA002',N'2026-10-25T00:00:00.0000000',4.75);
GO
INSERT dbo.[ChiTietCaLam] ([MaNhanVien],[MaCaLam],[NgayLam],[TongThoiGianLamThucTe]) VALUES (N'NV004',N'CA002',N'2026-10-26T00:00:00.0000000',4.25);
GO
INSERT dbo.[ChiTietCaLam] ([MaNhanVien],[MaCaLam],[NgayLam],[TongThoiGianLamThucTe]) VALUES (N'NV004',N'CA002',N'2026-10-27T00:00:00.0000000',4.50);
GO
INSERT dbo.[ChiTietCaLam] ([MaNhanVien],[MaCaLam],[NgayLam],[TongThoiGianLamThucTe]) VALUES (N'NV005',N'CA001',N'2026-10-21T00:00:00.0000000',4.50);
GO
INSERT dbo.[ChiTietCaLam] ([MaNhanVien],[MaCaLam],[NgayLam],[TongThoiGianLamThucTe]) VALUES (N'NV005',N'CA001',N'2026-10-22T00:00:00.0000000',4.50);
GO
INSERT dbo.[ChiTietCaLam] ([MaNhanVien],[MaCaLam],[NgayLam],[TongThoiGianLamThucTe]) VALUES (N'NV005',N'CA001',N'2026-10-25T00:00:00.0000000',4.00);
GO
INSERT dbo.[ChiTietCaLam] ([MaNhanVien],[MaCaLam],[NgayLam],[TongThoiGianLamThucTe]) VALUES (N'NV005',N'CA001',N'2026-10-26T00:00:00.0000000',4.50);
GO
INSERT dbo.[ChiTietCaLam] ([MaNhanVien],[MaCaLam],[NgayLam],[TongThoiGianLamThucTe]) VALUES (N'NV005',N'CA002',N'2026-10-21T00:00:00.0000000',4.50);
GO
INSERT dbo.[ChiTietCaLam] ([MaNhanVien],[MaCaLam],[NgayLam],[TongThoiGianLamThucTe]) VALUES (N'NV005',N'CA002',N'2026-10-22T00:00:00.0000000',4.00);
GO
INSERT dbo.[ChiTietCaLam] ([MaNhanVien],[MaCaLam],[NgayLam],[TongThoiGianLamThucTe]) VALUES (N'NV005',N'CA002',N'2026-10-23T00:00:00.0000000',4.50);
GO
INSERT dbo.[ChiTietCaLam] ([MaNhanVien],[MaCaLam],[NgayLam],[TongThoiGianLamThucTe]) VALUES (N'NV005',N'CA002',N'2026-10-24T00:00:00.0000000',4.00);
GO
INSERT dbo.[ChiTietCaLam] ([MaNhanVien],[MaCaLam],[NgayLam],[TongThoiGianLamThucTe]) VALUES (N'NV005',N'CA002',N'2026-10-26T00:00:00.0000000',4.50);
GO
INSERT dbo.[ChiTietCaLam] ([MaNhanVien],[MaCaLam],[NgayLam],[TongThoiGianLamThucTe]) VALUES (N'NV005',N'CA003',N'2026-10-23T00:00:00.0000000',3.75);
GO
INSERT dbo.[ChiTietCaLam] ([MaNhanVien],[MaCaLam],[NgayLam],[TongThoiGianLamThucTe]) VALUES (N'NV005',N'CA003',N'2026-10-24T00:00:00.0000000',3.50);
GO
INSERT dbo.[ChiTietDichVu] ([MaDichVu],[MaPhuTung],[SoLuong]) VALUES (N'DV001',N'PT001',1);
GO
INSERT dbo.[ChiTietDichVu] ([MaDichVu],[MaPhuTung],[SoLuong]) VALUES (N'DV002',N'PT003',1);
GO
INSERT dbo.[ChiTietDichVu] ([MaDichVu],[MaPhuTung],[SoLuong]) VALUES (N'DV003',N'PT004',1);
GO
INSERT dbo.[ChiTietDichVu] ([MaDichVu],[MaPhuTung],[SoLuong]) VALUES (N'DV004',N'PT005',1);
GO
INSERT dbo.[ChiTietDichVu] ([MaDichVu],[MaPhuTung],[SoLuong]) VALUES (N'DV004',N'PT007',1);
GO
INSERT dbo.[ChiTietDichVu] ([MaDichVu],[MaPhuTung],[SoLuong]) VALUES (N'DV004',N'PT022',1);
GO
INSERT dbo.[ChiTietDichVu] ([MaDichVu],[MaPhuTung],[SoLuong]) VALUES (N'DV005',N'PT006',1);
GO
INSERT dbo.[ChiTietDichVu] ([MaDichVu],[MaPhuTung],[SoLuong]) VALUES (N'DV005',N'PT008',1);
GO
INSERT dbo.[ChiTietDichVu] ([MaDichVu],[MaPhuTung],[SoLuong]) VALUES (N'DV006',N'PT021',1);
GO
INSERT dbo.[ChiTietDichVu] ([MaDichVu],[MaPhuTung],[SoLuong]) VALUES (N'DV007',N'PT009',1);
GO
INSERT dbo.[ChiTietDichVu] ([MaDichVu],[MaPhuTung],[SoLuong]) VALUES (N'DV008',N'PT009',1);
GO
INSERT dbo.[ChiTietDichVu] ([MaDichVu],[MaPhuTung],[SoLuong]) VALUES (N'DV009',N'PT011',1);
GO
INSERT dbo.[ChiTietDichVu] ([MaDichVu],[MaPhuTung],[SoLuong]) VALUES (N'DV010',N'PT012',1);
GO
INSERT dbo.[ChiTietDichVu] ([MaDichVu],[MaPhuTung],[SoLuong]) VALUES (N'DV011',N'PT013',1);
GO
INSERT dbo.[ChiTietDichVu] ([MaDichVu],[MaPhuTung],[SoLuong]) VALUES (N'DV012',N'PT014',1);
GO
INSERT dbo.[ChiTietDichVu] ([MaDichVu],[MaPhuTung],[SoLuong]) VALUES (N'DV013',N'PT007',1);
GO
INSERT dbo.[ChiTietDichVu] ([MaDichVu],[MaPhuTung],[SoLuong]) VALUES (N'DV014',N'PT015',1);
GO
INSERT dbo.[ChiTietDichVu] ([MaDichVu],[MaPhuTung],[SoLuong]) VALUES (N'DV015',N'PT017',1);
GO
INSERT dbo.[ChiTietDichVu] ([MaDichVu],[MaPhuTung],[SoLuong]) VALUES (N'DV016',N'PT019',1);
GO
INSERT dbo.[ChiTietDichVu] ([MaDichVu],[MaPhuTung],[SoLuong]) VALUES (N'DV017',N'PT020',1);
GO
INSERT dbo.[ChiTietDichVu] ([MaDichVu],[MaPhuTung],[SoLuong]) VALUES (N'DV018',N'PT023',1);
GO
INSERT dbo.[ChiTietDichVu] ([MaDichVu],[MaPhuTung],[SoLuong]) VALUES (N'DV019',N'PT024',1);
GO
INSERT dbo.[ChiTietPhanCong] ([MaNhanVien],[MaXe],[ThoiGianBatDau],[ThoiGianHoanThanh]) VALUES (N'NV002',N'XE101',N'2026-10-18T13:50:00.0000000',N'2026-10-18T15:00:00.0000000');
GO
INSERT dbo.[ChiTietPhanCong] ([MaNhanVien],[MaXe],[ThoiGianBatDau],[ThoiGianHoanThanh]) VALUES (N'NV002',N'XE108',N'2026-10-22T15:45:00.0000000',N'2026-10-22T16:20:00.0000000');
GO
INSERT dbo.[ChiTietPhanCong] ([MaNhanVien],[MaXe],[ThoiGianBatDau],[ThoiGianHoanThanh]) VALUES (N'NV002',N'XE109',N'2026-10-27T22:57:38.0000000',N'2026-10-28T00:57:38.0000000');
GO
INSERT dbo.[ChiTietPhanCong] ([MaNhanVien],[MaXe],[ThoiGianBatDau],[ThoiGianHoanThanh]) VALUES (N'NV002',N'XE114',N'2026-10-28T02:12:42.0000000',N'2026-10-31T00:00:00.0000000');
GO
INSERT dbo.[ChiTietPhanCong] ([MaNhanVien],[MaXe],[ThoiGianBatDau],[ThoiGianHoanThanh]) VALUES (N'NV002',N'XE115',N'2026-10-28T02:03:37.0000000',N'2026-10-30T00:00:00.0000000');
GO
INSERT dbo.[ChiTietPhanCong] ([MaNhanVien],[MaXe],[ThoiGianBatDau],[ThoiGianHoanThanh]) VALUES (N'NV002',N'XE116',N'2026-10-28T19:55:48.0000000',N'2026-10-31T00:00:00.0000000');
GO
INSERT dbo.[ChiTietPhanCong] ([MaNhanVien],[MaXe],[ThoiGianBatDau],[ThoiGianHoanThanh]) VALUES (N'NV003',N'XE102',N'2026-10-19T08:15:00.0000000',N'2026-10-19T09:15:00.0000000');
GO
INSERT dbo.[ChiTietPhanCong] ([MaNhanVien],[MaXe],[ThoiGianBatDau],[ThoiGianHoanThanh]) VALUES (N'NV003',N'XE103',N'2026-10-21T08:05:00.0000000',N'2026-10-21T10:30:00.0000000');
GO
INSERT dbo.[ChiTietPhanCong] ([MaNhanVien],[MaXe],[ThoiGianBatDau],[ThoiGianHoanThanh]) VALUES (N'NV003',N'XE104',N'2026-10-20T14:00:00.0000000',N'2026-10-20T14:35:00.0000000');
GO
INSERT dbo.[ChiTietPhanCong] ([MaNhanVien],[MaXe],[ThoiGianBatDau],[ThoiGianHoanThanh]) VALUES (N'NV003',N'XE105',N'2026-10-20T13:05:00.0000000',N'2026-10-20T13:30:00.0000000');
GO
INSERT dbo.[ChiTietPhanCong] ([MaNhanVien],[MaXe],[ThoiGianBatDau],[ThoiGianHoanThanh]) VALUES (N'NV003',N'XE106',N'2026-10-21T09:30:00.0000000',N'2026-10-21T11:30:00.0000000');
GO
INSERT dbo.[ChiTietPhanCong] ([MaNhanVien],[MaXe],[ThoiGianBatDau],[ThoiGianHoanThanh]) VALUES (N'NV003',N'XE107',N'2026-10-21T15:35:00.0000000',N'2026-10-21T16:55:00.0000000');
GO
INSERT dbo.[ChiTietPhanCong] ([MaNhanVien],[MaXe],[ThoiGianBatDau],[ThoiGianHoanThanh]) VALUES (N'NV003',N'XE110',N'2026-10-28T01:57:38.0000000',N'2026-10-28T03:57:38.0000000');
GO
INSERT dbo.[ChiTietPhieuNhap] ([MaPhieuNhap],[MaPhuTung],[SoLuongNhap],[ThanhTien]) VALUES (N'PN022',N'PT017',11,3080000);
GO
INSERT dbo.[ChiTietPhieuNhap] ([MaPhieuNhap],[MaPhuTung],[SoLuongNhap],[ThanhTien]) VALUES (N'PN022',N'PT018',5,1600000);
GO
INSERT dbo.[ChiTietPhieuNhap] ([MaPhieuNhap],[MaPhuTung],[SoLuongNhap],[ThanhTien]) VALUES (N'PN023',N'PT011',5,2600000);
GO
INSERT dbo.[ChiTietPhieuNhap] ([MaPhieuNhap],[MaPhuTung],[SoLuongNhap],[ThanhTien]) VALUES (N'PN023',N'PT012',5,2900000);
GO
INSERT dbo.[ChiTietPhieuNhap] ([MaPhieuNhap],[MaPhuTung],[SoLuongNhap],[ThanhTien]) VALUES (N'PN023',N'PT013',4,1000000);
GO
INSERT dbo.[ChiTietPhieuNhap] ([MaPhieuNhap],[MaPhuTung],[SoLuongNhap],[ThanhTien]) VALUES (N'PN023',N'PT014',8,2320000);
GO
INSERT dbo.[ChiTietPhieuNhap] ([MaPhieuNhap],[MaPhuTung],[SoLuongNhap],[ThanhTien]) VALUES (N'PN024',N'PT007',15,675000);
GO
INSERT dbo.[ChiTietPhieuNhap] ([MaPhieuNhap],[MaPhuTung],[SoLuongNhap],[ThanhTien]) VALUES (N'PN024',N'PT008',9,378000);
GO
INSERT dbo.[ChiTietPhieuNhap] ([MaPhieuNhap],[MaPhuTung],[SoLuongNhap],[ThanhTien]) VALUES (N'PN024',N'PT009',6,330000);
GO
INSERT dbo.[ChiTietPhieuNhap] ([MaPhieuNhap],[MaPhuTung],[SoLuongNhap],[ThanhTien]) VALUES (N'PN024',N'PT010',11,495000);
GO
INSERT dbo.[ChiTietPhieuTiepNhan] ([MaPhieuTiepNhan],[MaDichVu],[SoLuong],[ThanhTien]) VALUES (N'PTN087',N'DV003',1,50000);
GO
INSERT dbo.[ChiTietPhieuTiepNhan] ([MaPhieuTiepNhan],[MaDichVu],[SoLuong],[ThanhTien]) VALUES (N'PTN087',N'DV006',1,200000);
GO
INSERT dbo.[ChiTietPhieuTiepNhan] ([MaPhieuTiepNhan],[MaDichVu],[SoLuong],[ThanhTien]) VALUES (N'PTN101',N'DV003',1,50000);
GO
INSERT dbo.[ChiTietPhieuTiepNhan] ([MaPhieuTiepNhan],[MaDichVu],[SoLuong],[ThanhTien]) VALUES (N'PTN101',N'DV004',1,395000);
GO
INSERT dbo.[ChiTietPhieuTiepNhan] ([MaPhieuTiepNhan],[MaDichVu],[SoLuong],[ThanhTien]) VALUES (N'PTN102',N'DV001',1,140000);
GO
INSERT dbo.[ChiTietPhieuTiepNhan] ([MaPhieuTiepNhan],[MaDichVu],[SoLuong],[ThanhTien]) VALUES (N'PTN102',N'DV021',2,100000);
GO
INSERT dbo.[ChiTietPhieuTiepNhan] ([MaPhieuTiepNhan],[MaDichVu],[SoLuong],[ThanhTien]) VALUES (N'PTN103',N'DV003',1,50000);
GO
INSERT dbo.[ChiTietPhieuTiepNhan] ([MaPhieuTiepNhan],[MaDichVu],[SoLuong],[ThanhTien]) VALUES (N'PTN103',N'DV004',1,395000);
GO
INSERT dbo.[ChiTietPhieuTiepNhan] ([MaPhieuTiepNhan],[MaDichVu],[SoLuong],[ThanhTien]) VALUES (N'PTN103',N'DV014',1,370000);
GO
INSERT dbo.[ChiTietPhieuTiepNhan] ([MaPhieuTiepNhan],[MaDichVu],[SoLuong],[ThanhTien]) VALUES (N'PTN103',N'DV020',1,40000);
GO
INSERT dbo.[ChiTietPhieuTiepNhan] ([MaPhieuTiepNhan],[MaDichVu],[SoLuong],[ThanhTien]) VALUES (N'PTN104',N'DV002',1,160000);
GO
INSERT dbo.[ChiTietPhieuTiepNhan] ([MaPhieuTiepNhan],[MaDichVu],[SoLuong],[ThanhTien]) VALUES (N'PTN104',N'DV020',1,40000);
GO
INSERT dbo.[ChiTietPhieuTiepNhan] ([MaPhieuTiepNhan],[MaDichVu],[SoLuong],[ThanhTien]) VALUES (N'PTN105',N'DV020',1,40000);
GO
INSERT dbo.[ChiTietPhieuTiepNhan] ([MaPhieuTiepNhan],[MaDichVu],[SoLuong],[ThanhTien]) VALUES (N'PTN106',N'DV001',1,140000);
GO
INSERT dbo.[ChiTietPhieuTiepNhan] ([MaPhieuTiepNhan],[MaDichVu],[SoLuong],[ThanhTien]) VALUES (N'PTN106',N'DV004',1,395000);
GO
INSERT dbo.[ChiTietPhieuTiepNhan] ([MaPhieuTiepNhan],[MaDichVu],[SoLuong],[ThanhTien]) VALUES (N'PTN106',N'DV006',1,200000);
GO
INSERT dbo.[ChiTietPhieuTiepNhan] ([MaPhieuTiepNhan],[MaDichVu],[SoLuong],[ThanhTien]) VALUES (N'PTN107',N'DV001',1,140000);
GO
INSERT dbo.[ChiTietPhieuTiepNhan] ([MaPhieuTiepNhan],[MaDichVu],[SoLuong],[ThanhTien]) VALUES (N'PTN107',N'DV006',1,200000);
GO
INSERT dbo.[ChiTietPhieuTiepNhan] ([MaPhieuTiepNhan],[MaDichVu],[SoLuong],[ThanhTien]) VALUES (N'PTN107',N'DV008',1,105000);
GO
INSERT dbo.[ChiTietPhieuTiepNhan] ([MaPhieuTiepNhan],[MaDichVu],[SoLuong],[ThanhTien]) VALUES (N'PTN108',N'DV001',1,140000);
GO
INSERT dbo.[ChiTietPhieuTiepNhan] ([MaPhieuTiepNhan],[MaDichVu],[SoLuong],[ThanhTien]) VALUES (N'PTN108',N'DV015',1,380000);
GO
INSERT dbo.[ChiTietPhieuTiepNhan] ([MaPhieuTiepNhan],[MaDichVu],[SoLuong],[ThanhTien]) VALUES (N'PTN109',N'DV002',1,160000);
GO
INSERT dbo.[ChiTietPhieuTiepNhan] ([MaPhieuTiepNhan],[MaDichVu],[SoLuong],[ThanhTien]) VALUES (N'PTN109',N'DV012',1,410000);
GO
INSERT dbo.[ChiTietPhieuTiepNhan] ([MaPhieuTiepNhan],[MaDichVu],[SoLuong],[ThanhTien]) VALUES (N'PTN109',N'DV021',1,50000);
GO
INSERT dbo.[ChiTietPhieuTiepNhan] ([MaPhieuTiepNhan],[MaDichVu],[SoLuong],[ThanhTien]) VALUES (N'PTN110',N'DV001',1,140000);
GO
INSERT dbo.[ChiTietPhieuTiepNhan] ([MaPhieuTiepNhan],[MaDichVu],[SoLuong],[ThanhTien]) VALUES (N'PTN111',N'DV003',1,50000);
GO
INSERT dbo.[ChiTietPhieuTiepNhan] ([MaPhieuTiepNhan],[MaDichVu],[SoLuong],[ThanhTien]) VALUES (N'PTN111',N'DV019',1,105000);
GO
INSERT dbo.[ChiTietPhieuTiepNhan] ([MaPhieuTiepNhan],[MaDichVu],[SoLuong],[ThanhTien]) VALUES (N'PTN112',N'DV013',1,85000);
GO
INSERT dbo.[ChiTietPhieuTiepNhan] ([MaPhieuTiepNhan],[MaDichVu],[SoLuong],[ThanhTien]) VALUES (N'PTN112',N'DV019',1,105000);
GO
INSERT dbo.[ChiTietPhieuTiepNhan] ([MaPhieuTiepNhan],[MaDichVu],[SoLuong],[ThanhTien]) VALUES (N'PTN112',N'DV020',1,40000);
GO
INSERT dbo.[ChiTietPhieuTiepNhan] ([MaPhieuTiepNhan],[MaDichVu],[SoLuong],[ThanhTien]) VALUES (N'PTN113',N'DV001',1,140000);
GO
INSERT dbo.[ChiTietPhieuTiepNhan] ([MaPhieuTiepNhan],[MaDichVu],[SoLuong],[ThanhTien]) VALUES (N'PTN113',N'DV005',1,275000);
GO
INSERT dbo.[ChiTietPhieuTiepNhan] ([MaPhieuTiepNhan],[MaDichVu],[SoLuong],[ThanhTien]) VALUES (N'PTN114',N'DV006',1,200000);
GO
INSERT dbo.[ChiTietPhieuTiepNhan] ([MaPhieuTiepNhan],[MaDichVu],[SoLuong],[ThanhTien]) VALUES (N'PTN114',N'DV015',1,380000);
GO
INSERT dbo.[ChiTietPhieuTiepNhan] ([MaPhieuTiepNhan],[MaDichVu],[SoLuong],[ThanhTien]) VALUES (N'PTN114',N'DV020',1,40000);
GO
INSERT dbo.[ChiTietPhieuTiepNhan] ([MaPhieuTiepNhan],[MaDichVu],[SoLuong],[ThanhTien]) VALUES (N'PTN115',N'DV009',1,690000);
GO
INSERT dbo.[ChiTietPhieuTiepNhan] ([MaPhieuTiepNhan],[MaDichVu],[SoLuong],[ThanhTien]) VALUES (N'PTN115',N'DV015',1,380000);
GO
INSERT dbo.[ChiTietPhieuTiepNhan] ([MaPhieuTiepNhan],[MaDichVu],[SoLuong],[ThanhTien]) VALUES (N'PTN116',N'DV011',1,350000);
GO
INSERT dbo.[ChucVu] ([MaChucVu],[TenChucVu],[MoTa],[HeSoLuong],[TienDieuChinh]) VALUES (N'CV001',N'Chủ',N'Chủ cửa hàng',3.50,3300000);
GO
INSERT dbo.[ChucVu] ([MaChucVu],[TenChucVu],[MoTa],[HeSoLuong],[TienDieuChinh]) VALUES (N'CV002',N'Thợ chính',N'Phụ trách sửa chữa, tối đa 3 xe',2.60,2400000);
GO
INSERT dbo.[ChucVu] ([MaChucVu],[TenChucVu],[MoTa],[HeSoLuong],[TienDieuChinh]) VALUES (N'CV003',N'Thợ phụ',N'Thực hiện sửa chữa, tối đa 2 xe',2.00,1400000);
GO
INSERT dbo.[ChucVu] ([MaChucVu],[TenChucVu],[MoTa],[HeSoLuong],[TienDieuChinh]) VALUES (N'CV004',N'Quản lý',N'Quản lý hoạt động cửa hàng',1.50,1400000);
GO
INSERT dbo.[ChucVu] ([MaChucVu],[TenChucVu],[MoTa],[HeSoLuong],[TienDieuChinh]) VALUES (N'CV006',N'Thủ kho',N'Quản lý kho phụ tùng',1.80,1400000);
GO
INSERT dbo.[ChucVu] ([MaChucVu],[TenChucVu],[MoTa],[HeSoLuong],[TienDieuChinh]) VALUES (N'CV007',N'Thu ngân',N'Tiếp nhận và thanh toán',2.00,1100000);
GO
INSERT dbo.[ChucVuDieuChinh] ([MaChucVu],[MaKhoanDieuChinh]) VALUES (N'CV001',N'KDC001');
GO
INSERT dbo.[ChucVuDieuChinh] ([MaChucVu],[MaKhoanDieuChinh]) VALUES (N'CV001',N'KDC003');
GO
INSERT dbo.[ChucVuDieuChinh] ([MaChucVu],[MaKhoanDieuChinh]) VALUES (N'CV001',N'KDC004');
GO
INSERT dbo.[ChucVuDieuChinh] ([MaChucVu],[MaKhoanDieuChinh]) VALUES (N'CV001',N'KDC005');
GO
INSERT dbo.[ChucVuDieuChinh] ([MaChucVu],[MaKhoanDieuChinh]) VALUES (N'CV001',N'KDC007');
GO
INSERT dbo.[ChucVuDieuChinh] ([MaChucVu],[MaKhoanDieuChinh]) VALUES (N'CV002',N'KDC002');
GO
INSERT dbo.[ChucVuDieuChinh] ([MaChucVu],[MaKhoanDieuChinh]) VALUES (N'CV002',N'KDC003');
GO
INSERT dbo.[ChucVuDieuChinh] ([MaChucVu],[MaKhoanDieuChinh]) VALUES (N'CV002',N'KDC004');
GO
INSERT dbo.[ChucVuDieuChinh] ([MaChucVu],[MaKhoanDieuChinh]) VALUES (N'CV002',N'KDC005');
GO
INSERT dbo.[ChucVuDieuChinh] ([MaChucVu],[MaKhoanDieuChinh]) VALUES (N'CV002',N'KDC007');
GO
INSERT dbo.[ChucVuDieuChinh] ([MaChucVu],[MaKhoanDieuChinh]) VALUES (N'CV002',N'KDC008');
GO
INSERT dbo.[ChucVuDieuChinh] ([MaChucVu],[MaKhoanDieuChinh]) VALUES (N'CV002',N'KDC010');
GO
INSERT dbo.[ChucVuDieuChinh] ([MaChucVu],[MaKhoanDieuChinh]) VALUES (N'CV003',N'KDC003');
GO
INSERT dbo.[ChucVuDieuChinh] ([MaChucVu],[MaKhoanDieuChinh]) VALUES (N'CV003',N'KDC004');
GO
INSERT dbo.[ChucVuDieuChinh] ([MaChucVu],[MaKhoanDieuChinh]) VALUES (N'CV003',N'KDC005');
GO
INSERT dbo.[ChucVuDieuChinh] ([MaChucVu],[MaKhoanDieuChinh]) VALUES (N'CV003',N'KDC007');
GO
INSERT dbo.[ChucVuDieuChinh] ([MaChucVu],[MaKhoanDieuChinh]) VALUES (N'CV003',N'KDC008');
GO
INSERT dbo.[ChucVuDieuChinh] ([MaChucVu],[MaKhoanDieuChinh]) VALUES (N'CV003',N'KDC010');
GO
INSERT dbo.[ChucVuDieuChinh] ([MaChucVu],[MaKhoanDieuChinh]) VALUES (N'CV004',N'KDC004');
GO
INSERT dbo.[ChucVuDieuChinh] ([MaChucVu],[MaKhoanDieuChinh]) VALUES (N'CV004',N'KDC005');
GO
INSERT dbo.[ChucVuDieuChinh] ([MaChucVu],[MaKhoanDieuChinh]) VALUES (N'CV004',N'KDC007');
GO
INSERT dbo.[ChucVuDieuChinh] ([MaChucVu],[MaKhoanDieuChinh]) VALUES (N'CV004',N'KDC010');
GO
INSERT dbo.[ChucVuDieuChinh] ([MaChucVu],[MaKhoanDieuChinh]) VALUES (N'CV006',N'KDC004');
GO
INSERT dbo.[ChucVuDieuChinh] ([MaChucVu],[MaKhoanDieuChinh]) VALUES (N'CV006',N'KDC005');
GO
INSERT dbo.[ChucVuDieuChinh] ([MaChucVu],[MaKhoanDieuChinh]) VALUES (N'CV006',N'KDC006');
GO
INSERT dbo.[ChucVuDieuChinh] ([MaChucVu],[MaKhoanDieuChinh]) VALUES (N'CV006',N'KDC007');
GO
INSERT dbo.[ChucVuDieuChinh] ([MaChucVu],[MaKhoanDieuChinh]) VALUES (N'CV007',N'KDC003');
GO
INSERT dbo.[ChucVuDieuChinh] ([MaChucVu],[MaKhoanDieuChinh]) VALUES (N'CV007',N'KDC004');
GO
INSERT dbo.[ChucVuDieuChinh] ([MaChucVu],[MaKhoanDieuChinh]) VALUES (N'CV007',N'KDC005');
GO
INSERT dbo.[ChucVuDieuChinh] ([MaChucVu],[MaKhoanDieuChinh]) VALUES (N'CV007',N'KDC007');
GO
INSERT dbo.[ChucVuDieuChinh] ([MaChucVu],[MaKhoanDieuChinh]) VALUES (N'CV007',N'KDC009');
GO
INSERT dbo.[DichVu] ([MaDichVu],[TenDichVu],[MoTa],[ThanhTienDichVu],[TrangThai]) VALUES (N'DV001',N'Thay nhớt máy (Castrol Power1)',N'Xả nhớt cũ, thay nhớt mới, kiểm tra rò rỉ',140000,N'Ngừng áp dụng');
GO
INSERT dbo.[DichVu] ([MaDichVu],[TenDichVu],[MoTa],[ThanhTienDichVu],[TrangThai]) VALUES (N'DV002',N'Thay nhớt máy cao cấp (Motul 3100)',N'Thay nhớt tổng hợp cao cấp cho xe chạy nhiều',160000,N'Ngừng áp dụng');
GO
INSERT dbo.[DichVu] ([MaDichVu],[TenDichVu],[MoTa],[ThanhTienDichVu],[TrangThai]) VALUES (N'DV003',N'Thay nhớt hộp số (xe tay ga)',N'Thay nhớt láp cho xe tay ga',50000,N'Đang áp dụng');
GO
INSERT dbo.[DichVu] ([MaDichVu],[TenDichVu],[MoTa],[ThanhTienDichVu],[TrangThai]) VALUES (N'DV004',N'Bảo dưỡng định kỳ xe tay ga',N'Vệ sinh nồi, thay lọc gió, thay bugi, kiểm tra tổng quát',395000,N'Đang áp dụng');
GO
INSERT dbo.[DichVu] ([MaDichVu],[TenDichVu],[MoTa],[ThanhTienDichVu],[TrangThai]) VALUES (N'DV005',N'Bảo dưỡng định kỳ xe số - côn tay',N'Thay lọc gió, thay bugi, căng chỉnh xích, kiểm tra tổng quát',275000,N'Đang áp dụng');
GO
INSERT dbo.[DichVu] ([MaDichVu],[TenDichVu],[MoTa],[ThanhTienDichVu],[TrangThai]) VALUES (N'DV006',N'Vệ sinh kim phun - buồng đốt',N'Vệ sinh kim phun, họng ga và buồng đốt',200000,N'Đang áp dụng');
GO
INSERT dbo.[DichVu] ([MaDichVu],[TenDichVu],[MoTa],[ThanhTienDichVu],[TrangThai]) VALUES (N'DV007',N'Thay má phanh trước',N'Thay má phanh trước, vệ sinh cụm phanh',120000,N'Đang áp dụng');
GO
INSERT dbo.[DichVu] ([MaDichVu],[TenDichVu],[MoTa],[ThanhTienDichVu],[TrangThai]) VALUES (N'DV008',N'Thay má phanh sau',N'Thay má phanh sau, căng chỉnh phanh',105000,N'Đang áp dụng');
GO
INSERT dbo.[DichVu] ([MaDichVu],[TenDichVu],[MoTa],[ThanhTienDichVu],[TrangThai]) VALUES (N'DV009',N'Thay lốp trước xe tay ga',N'Tháo bánh, thay lốp trước, cân chỉnh',690000,N'Đang áp dụng');
GO
INSERT dbo.[DichVu] ([MaDichVu],[TenDichVu],[MoTa],[ThanhTienDichVu],[TrangThai]) VALUES (N'DV010',N'Thay lốp sau xe tay ga',N'Tháo bánh, thay lốp sau, cân chỉnh',770000,N'Đang áp dụng');
GO
INSERT dbo.[DichVu] ([MaDichVu],[TenDichVu],[MoTa],[ThanhTienDichVu],[TrangThai]) VALUES (N'DV011',N'Thay lốp trước xe số',N'Tháo bánh, thay lốp trước xe số',350000,N'Đang áp dụng');
GO
INSERT dbo.[DichVu] ([MaDichVu],[TenDichVu],[MoTa],[ThanhTienDichVu],[TrangThai]) VALUES (N'DV012',N'Thay lốp sau xe số',N'Tháo bánh, thay lốp sau xe số',410000,N'Đang áp dụng');
GO
INSERT dbo.[DichVu] ([MaDichVu],[TenDichVu],[MoTa],[ThanhTienDichVu],[TrangThai]) VALUES (N'DV013',N'Thay bugi',N'Thay bugi mới, kiểm tra đánh lửa',85000,N'Đang áp dụng');
GO
INSERT dbo.[DichVu] ([MaDichVu],[TenDichVu],[MoTa],[ThanhTienDichVu],[TrangThai]) VALUES (N'DV014',N'Thay dây curoa (xe tay ga)',N'Thay dây curoa, vệ sinh bộ truyền động',370000,N'Đang áp dụng');
GO
INSERT dbo.[DichVu] ([MaDichVu],[TenDichVu],[MoTa],[ThanhTienDichVu],[TrangThai]) VALUES (N'DV015',N'Thay ắc quy',N'Thay ắc quy mới, kiểm tra sạc',380000,N'Đang áp dụng');
GO
INSERT dbo.[DichVu] ([MaDichVu],[TenDichVu],[MoTa],[ThanhTienDichVu],[TrangThai]) VALUES (N'DV016',N'Thay bộ nhông xích đĩa (xe số)',N'Thay trọn bộ nhông, xích, đĩa',430000,N'Đang áp dụng');
GO
INSERT dbo.[DichVu] ([MaDichVu],[TenDichVu],[MoTa],[ThanhTienDichVu],[TrangThai]) VALUES (N'DV017',N'Thay bộ nhông xích đĩa (xe côn tay)',N'Thay trọn bộ nhông, xích, đĩa xe côn tay',660000,N'Đang áp dụng');
GO
INSERT dbo.[DichVu] ([MaDichVu],[TenDichVu],[MoTa],[ThanhTienDichVu],[TrangThai]) VALUES (N'DV018',N'Thay nước làm mát',N'Xả và thay nước làm mát két nước',115000,N'Đang áp dụng');
GO
INSERT dbo.[DichVu] ([MaDichVu],[TenDichVu],[MoTa],[ThanhTienDichVu],[TrangThai]) VALUES (N'DV019',N'Thay dầu phanh',N'Xả gió và thay dầu phanh đĩa',105000,N'Đang áp dụng');
GO
INSERT dbo.[DichVu] ([MaDichVu],[TenDichVu],[MoTa],[ThanhTienDichVu],[TrangThai]) VALUES (N'DV020',N'Rửa xe - dưỡng bóng',N'Rửa xe bọt tuyết, xịt dưỡng nhựa nhám',40000,N'Đang áp dụng');
GO
INSERT dbo.[DichVu] ([MaDichVu],[TenDichVu],[MoTa],[ThanhTienDichVu],[TrangThai]) VALUES (N'DV021',N'Vá lốp không săm',N'Vá lốp bằng nấm vá, bơm hơi kiểm tra',50000,N'Đang áp dụng');
GO
INSERT dbo.[DichVu] ([MaDichVu],[TenDichVu],[MoTa],[ThanhTienDichVu],[TrangThai]) VALUES (N'DV022',N'Sơn dặm - đánh bóng dàn áo',N'Sơn dặm vết xước, đánh bóng dàn áo',350000,N'Ngừng áp dụng');
GO
INSERT dbo.[DichVu] ([MaDichVu],[TenDichVu],[MoTa],[ThanhTienDichVu],[TrangThai]) VALUES (N'DV023',N'Test dịch vụ',N'TEST',50000,N'Đang áp dụng');
GO
INSERT dbo.[HoaDon] ([MaHoaDon],[MaPhieuTiepNhan],[TienThanhToan],[NgayLap],[NgayThanhToan]) VALUES (N'HD099',N'PTN101',445000,N'2026-10-18T00:00:00.0000000',N'2026-10-19T00:00:00.0000000');
GO
INSERT dbo.[HoaDon] ([MaHoaDon],[MaPhieuTiepNhan],[TienThanhToan],[NgayLap],[NgayThanhToan]) VALUES (N'HD100',N'PTN102',240000,N'2026-10-19T00:00:00.0000000',N'2026-10-19T00:00:00.0000000');
GO
INSERT dbo.[HoaDon] ([MaHoaDon],[MaPhieuTiepNhan],[TienThanhToan],[NgayLap],[NgayThanhToan]) VALUES (N'HD101',N'PTN104',200000,N'2026-10-20T00:00:00.0000000',N'2026-10-20T00:00:00.0000000');
GO
INSERT dbo.[HoaDon] ([MaHoaDon],[MaPhieuTiepNhan],[TienThanhToan],[NgayLap],[NgayThanhToan]) VALUES (N'HD102',N'PTN105',40000,N'2026-10-20T00:00:00.0000000',N'2026-10-20T00:00:00.0000000');
GO
INSERT dbo.[HoaDon] ([MaHoaDon],[MaPhieuTiepNhan],[TienThanhToan],[NgayLap],[NgayThanhToan]) VALUES (N'HD103',N'PTN103',855000,N'2026-10-21T00:00:00.0000000',N'2026-10-21T00:00:00.0000000');
GO
INSERT dbo.[HoaDon] ([MaHoaDon],[MaPhieuTiepNhan],[TienThanhToan],[NgayLap],[NgayThanhToan]) VALUES (N'HD104',N'PTN106',735000,N'2026-10-21T00:00:00.0000000',N'2026-10-21T00:00:00.0000000');
GO
INSERT dbo.[HoaDon] ([MaHoaDon],[MaPhieuTiepNhan],[TienThanhToan],[NgayLap],[NgayThanhToan]) VALUES (N'HD105',N'PTN107',445000,N'2026-10-21T00:00:00.0000000',N'2026-10-21T00:00:00.0000000');
GO
INSERT dbo.[HoaDon] ([MaHoaDon],[MaPhieuTiepNhan],[TienThanhToan],[NgayLap],[NgayThanhToan]) VALUES (N'HD106',N'PTN108',520000,N'2026-10-22T00:00:00.0000000',N'2026-10-22T00:00:00.0000000');
GO
INSERT dbo.[KhachHang] ([MaKhachHang],[TenKhachHang],[SoDienThoai]) VALUES (N'KH002',N'Cao Minh Toàn',N'0394464605');
GO
INSERT dbo.[KhachHang] ([MaKhachHang],[TenKhachHang],[SoDienThoai]) VALUES (N'KH003',N'Hoàng Gia Hiếu',N'0349105910');
GO
INSERT dbo.[KhachHang] ([MaKhachHang],[TenKhachHang],[SoDienThoai]) VALUES (N'KH007',N'Nguyễn Ngọc Thái',N'0771224865');
GO
INSERT dbo.[KhachHang] ([MaKhachHang],[TenKhachHang],[SoDienThoai]) VALUES (N'KH012',N'Lê Tiến Long',N'0917102223');
GO
INSERT dbo.[KhachHang] ([MaKhachHang],[TenKhachHang],[SoDienThoai]) VALUES (N'KH014',N'Lê Quang Đạt',N'0372494838');
GO
INSERT dbo.[KhachHang] ([MaKhachHang],[TenKhachHang],[SoDienThoai]) VALUES (N'KH019',N'Đỗ Mai Hoa',N'0895778628');
GO
INSERT dbo.[KhachHang] ([MaKhachHang],[TenKhachHang],[SoDienThoai]) VALUES (N'KH025',N'Phan Tiến Long',N'0379895299');
GO
INSERT dbo.[KhachHang] ([MaKhachHang],[TenKhachHang],[SoDienThoai]) VALUES (N'KH029',N'Phạm Thành Sơn',N'0377301978');
GO
INSERT dbo.[KhachHang] ([MaKhachHang],[TenKhachHang],[SoDienThoai]) VALUES (N'KH030',N'Phạm Văn Tuấn',N'0399150362');
GO
INSERT dbo.[KhachHang] ([MaKhachHang],[TenKhachHang],[SoDienThoai]) VALUES (N'KH032',N'Mai Phương Linh',N'0930237138');
GO
INSERT dbo.[KhachHang] ([MaKhachHang],[TenKhachHang],[SoDienThoai]) VALUES (N'KH036',N'Nguyễn Khánh Linh',N'0374604290');
GO
INSERT dbo.[KhachHang] ([MaKhachHang],[TenKhachHang],[SoDienThoai]) VALUES (N'KH039',N'Trịnh Trọng Sơn',N'0702877186');
GO
INSERT dbo.[KhachHang] ([MaKhachHang],[TenKhachHang],[SoDienThoai]) VALUES (N'KH042',N'Trịnh Thị Hương',N'0985855243');
GO
INSERT dbo.[KhachHang] ([MaKhachHang],[TenKhachHang],[SoDienThoai]) VALUES (N'KH043',N'Nguyễn Thành Khoa',N'0364301350');
GO
INSERT dbo.[KhachHang] ([MaKhachHang],[TenKhachHang],[SoDienThoai]) VALUES (N'KH044',N'1',N'11111111');
GO
INSERT dbo.[KhachHang] ([MaKhachHang],[TenKhachHang],[SoDienThoai]) VALUES (N'KH045',N'Bùi Quang Minh',N'0965299505');
GO
INSERT dbo.[KhoanDieuChinh] ([MaKhoanDieuChinh],[TenKhoanDieuChinh],[SoTien]) VALUES (N'KDC001',N'Phụ cấp trách nhiệm quản lý',2000000);
GO
INSERT dbo.[KhoanDieuChinh] ([MaKhoanDieuChinh],[TenKhoanDieuChinh],[SoTien]) VALUES (N'KDC002',N'Phụ cấp tay nghề kỹ thuật',1000000);
GO
INSERT dbo.[KhoanDieuChinh] ([MaKhoanDieuChinh],[TenKhoanDieuChinh],[SoTien]) VALUES (N'KDC003',N'Phụ cấp xăng xe',300000);
GO
INSERT dbo.[KhoanDieuChinh] ([MaKhoanDieuChinh],[TenKhoanDieuChinh],[SoTien]) VALUES (N'KDC004',N'Phụ cấp ăn trưa',600000);
GO
INSERT dbo.[KhoanDieuChinh] ([MaKhoanDieuChinh],[TenKhoanDieuChinh],[SoTien]) VALUES (N'KDC005',N'Thưởng chuyên cần',500000);
GO
INSERT dbo.[KhoanDieuChinh] ([MaKhoanDieuChinh],[TenKhoanDieuChinh],[SoTien]) VALUES (N'KDC006',N'Phụ cấp trách nhiệm kho',400000);
GO
INSERT dbo.[KhoanDieuChinh] ([MaKhoanDieuChinh],[TenKhoanDieuChinh],[SoTien]) VALUES (N'KDC007',N'Phạt đi muộn',-100000);
GO
INSERT dbo.[KhoanDieuChinh] ([MaKhoanDieuChinh],[TenKhoanDieuChinh],[SoTien]) VALUES (N'KDC008',N'Phạt làm sai quy trình kỹ thuật',-300000);
GO
INSERT dbo.[KhoanDieuChinh] ([MaKhoanDieuChinh],[TenKhoanDieuChinh],[SoTien]) VALUES (N'KDC009',N'Phạt chênh lệch quỹ tiền mặt',-200000);
GO
INSERT dbo.[KhoanDieuChinh] ([MaKhoanDieuChinh],[TenKhoanDieuChinh],[SoTien]) VALUES (N'KDC010',N'Phụ cấp độc hại (dầu nhớt, hóa chất)',400000);
GO
INSERT dbo.[NhaCungCap] ([MaNhaCungCap],[TenNhaCungCap],[SoDienThoai],[DiaChi]) VALUES (N'NCC001',N'Công ty TNHH Phụ tùng Minh Phát',N'02251800617',N'125 Lạch Tray, Ngô Quyền, Hải Phòng');
GO
INSERT dbo.[NhaCungCap] ([MaNhaCungCap],[TenNhaCungCap],[SoDienThoai],[DiaChi]) VALUES (N'NCC002',N'Công ty TNHH Dầu nhớt An Khang',N'02253786292',N'48 Tô Hiệu, Lê Chân, Hải Phòng');
GO
INSERT dbo.[NhaCungCap] ([MaNhaCungCap],[TenNhaCungCap],[SoDienThoai],[DiaChi]) VALUES (N'NCC003',N'Đại lý Lốp xe Hồng Hà',N'02252597999',N'312 Nguyễn Văn Linh, Lê Chân, Hải Phòng');
GO
INSERT dbo.[NhaCungCap] ([MaNhaCungCap],[TenNhaCungCap],[SoDienThoai],[DiaChi]) VALUES (N'NCC004',N'Công ty CP Ắc quy Thành Đạt',N'02258166706',N'27 Đà Nẵng, Ngô Quyền, Hải Phòng');
GO
INSERT dbo.[NhaCungCap] ([MaNhaCungCap],[TenNhaCungCap],[SoDienThoai],[DiaChi]) VALUES (N'NCC005',N'Cửa hàng Phụ tùng Tuấn Hưng',N'02250774447',N'86 Trần Nguyên Hãn, Lê Chân, Hải Phòng');
GO
INSERT dbo.[NhaCungCap] ([MaNhaCungCap],[TenNhaCungCap],[SoDienThoai],[DiaChi]) VALUES (N'NCC006',N'Công ty TNHH Thương mại Hải Âu',N'02253061097',N'15 Phạm Văn Đồng, Dương Kinh, Hải Phòng');
GO
INSERT dbo.[NhaCungCap] ([MaNhaCungCap],[TenNhaCungCap],[SoDienThoai],[DiaChi]) VALUES (N'NCC007',N'Đại lý Phụ tùng Ngọc Lan',N'02256735854',N'203 Hai Bà Trưng, Lê Chân, Hải Phòng');
GO
INSERT dbo.[NhaCungCap] ([MaNhaCungCap],[TenNhaCungCap],[SoDienThoai],[DiaChi]) VALUES (N'NCC008',N'Công ty TNHH Hóa chất Bảo dưỡng Việt Tiến',N'02251447679',N'9 Lê Hồng Phong, Hải An, Hải Phòng');
GO
INSERT dbo.[NhanVien] ([MaNhanVien],[MaChucVu],[HoTen],[SoDienThoai],[Luong],[TenDangNhap],[MatKhau],[TrangThai]) VALUES (N'NV001',N'CV001',N'Minh',N'0351104489',15300000,N'minh',N'A665A45920422F9D417E4867EFDC4FB8A04A1F3FFF1FA07E998E86F7F7A27AE3',N'Đang làm');
GO
INSERT dbo.[NhanVien] ([MaNhanVien],[MaChucVu],[HoTen],[SoDienThoai],[Luong],[TenDangNhap],[MatKhau],[TrangThai]) VALUES (N'NV002',N'CV002',N'An',N'0398728274',11700000,N'an',N'A665A45920422F9D417E4867EFDC4FB8A04A1F3FFF1FA07E998E86F7F7A27AE3',N'Đang làm');
GO
INSERT dbo.[NhanVien] ([MaNhanVien],[MaChucVu],[HoTen],[SoDienThoai],[Luong],[TenDangNhap],[MatKhau],[TrangThai]) VALUES (N'NV003',N'CV003',N'Huy',N'0707366128',10000000,N'huy',N'A665A45920422F9D417E4867EFDC4FB8A04A1F3FFF1FA07E998E86F7F7A27AE3',N'Đang làm');
GO
INSERT dbo.[NhanVien] ([MaNhanVien],[MaChucVu],[HoTen],[SoDienThoai],[Luong],[TenDangNhap],[MatKhau],[TrangThai]) VALUES (N'NV004',N'CV007',N'Mai',N'0399685154',9100000,N'mai',N'A665A45920422F9D417E4867EFDC4FB8A04A1F3FFF1FA07E998E86F7F7A27AE3',N'Đang làm');
GO
INSERT dbo.[NhanVien] ([MaNhanVien],[MaChucVu],[HoTen],[SoDienThoai],[Luong],[TenDangNhap],[MatKhau],[TrangThai]) VALUES (N'NV005',N'CV006',N'Cường',N'0850113539',7800000,N'cuong',N'A665A45920422F9D417E4867EFDC4FB8A04A1F3FFF1FA07E998E86F7F7A27AE3',N'Đang làm');
GO
INSERT dbo.[NhanVien] ([MaNhanVien],[MaChucVu],[HoTen],[SoDienThoai],[Luong],[TenDangNhap],[MatKhau],[TrangThai]) VALUES (N'NV006',N'CV003',N'Hường',N'0123456789',600000000,N'Huong',N'A665A45920422F9D417E4867EFDC4FB8A04A1F3FFF1FA07E998E86F7F7A27AE3',N'Đang làm');
GO
INSERT dbo.[PhieuNhap] ([MaPhieuNhap],[MaNhanVienNhap],[NgayNhap],[GhiChu],[TongTienNhap]) VALUES (N'PN022',N'NV005',N'2026-09-28T00:00:00.0000000',N'Nhập hàng đợt tháng 10 từ Công ty CP Ắc quy Thành Đạt',4680000);
GO
INSERT dbo.[PhieuNhap] ([MaPhieuNhap],[MaNhanVienNhap],[NgayNhap],[GhiChu],[TongTienNhap]) VALUES (N'PN023',N'NV005',N'2026-09-29T00:00:00.0000000',N'Nhập hàng đợt tháng 10 từ Đại lý Lốp xe Hồng Hà',8820000);
GO
INSERT dbo.[PhieuNhap] ([MaPhieuNhap],[MaNhanVienNhap],[NgayNhap],[GhiChu],[TongTienNhap]) VALUES (N'PN024',N'NV005',N'2026-09-30T00:00:00.0000000',N'Nhập hàng đợt tháng 10 từ Cửa hàng Phụ tùng Tuấn Hưng',1878000);
GO
INSERT dbo.[PhieuTiepNhan] ([MaPhieuTiepNhan],[MaXe],[MaNhanVien],[NgayLap],[NgayHen],[TrangThai]) VALUES (N'PTN087',N'XE087',N'NV004',N'2026-10-08T00:00:00.0000000',N'2026-10-09T00:00:00.0000000',N'Đã hủy');
GO
INSERT dbo.[PhieuTiepNhan] ([MaPhieuTiepNhan],[MaXe],[MaNhanVien],[NgayLap],[NgayHen],[TrangThai]) VALUES (N'PTN101',N'XE101',N'NV004',N'2026-10-18T00:00:00.0000000',N'2026-10-18T00:00:00.0000000',N'Hoàn thành');
GO
INSERT dbo.[PhieuTiepNhan] ([MaPhieuTiepNhan],[MaXe],[MaNhanVien],[NgayLap],[NgayHen],[TrangThai]) VALUES (N'PTN102',N'XE102',N'NV004',N'2026-10-19T00:00:00.0000000',N'2026-10-20T00:00:00.0000000',N'Hoàn thành');
GO
INSERT dbo.[PhieuTiepNhan] ([MaPhieuTiepNhan],[MaXe],[MaNhanVien],[NgayLap],[NgayHen],[TrangThai]) VALUES (N'PTN103',N'XE103',N'NV004',N'2026-10-19T00:00:00.0000000',N'2026-10-21T00:00:00.0000000',N'Hoàn thành');
GO
INSERT dbo.[PhieuTiepNhan] ([MaPhieuTiepNhan],[MaXe],[MaNhanVien],[NgayLap],[NgayHen],[TrangThai]) VALUES (N'PTN104',N'XE104',N'NV004',N'2026-10-20T00:00:00.0000000',N'2026-10-20T00:00:00.0000000',N'Hoàn thành');
GO
INSERT dbo.[PhieuTiepNhan] ([MaPhieuTiepNhan],[MaXe],[MaNhanVien],[NgayLap],[NgayHen],[TrangThai]) VALUES (N'PTN105',N'XE105',N'NV004',N'2026-10-20T00:00:00.0000000',N'2026-10-20T00:00:00.0000000',N'Hoàn thành');
GO
INSERT dbo.[PhieuTiepNhan] ([MaPhieuTiepNhan],[MaXe],[MaNhanVien],[NgayLap],[NgayHen],[TrangThai]) VALUES (N'PTN106',N'XE106',N'NV004',N'2026-10-21T00:00:00.0000000',N'2026-10-21T00:00:00.0000000',N'Hoàn thành');
GO
INSERT dbo.[PhieuTiepNhan] ([MaPhieuTiepNhan],[MaXe],[MaNhanVien],[NgayLap],[NgayHen],[TrangThai]) VALUES (N'PTN107',N'XE107',N'NV004',N'2026-10-21T00:00:00.0000000',N'2026-10-21T00:00:00.0000000',N'Hoàn thành');
GO
INSERT dbo.[PhieuTiepNhan] ([MaPhieuTiepNhan],[MaXe],[MaNhanVien],[NgayLap],[NgayHen],[TrangThai]) VALUES (N'PTN108',N'XE108',N'NV004',N'2026-10-22T00:00:00.0000000',N'2026-10-22T00:00:00.0000000',N'Hoàn thành');
GO
INSERT dbo.[PhieuTiepNhan] ([MaPhieuTiepNhan],[MaXe],[MaNhanVien],[NgayLap],[NgayHen],[TrangThai]) VALUES (N'PTN109',N'XE109',N'NV004',N'2026-10-24T00:00:00.0000000',N'2026-10-25T00:00:00.0000000',N'Hoàn thành');
GO
INSERT dbo.[PhieuTiepNhan] ([MaPhieuTiepNhan],[MaXe],[MaNhanVien],[NgayLap],[NgayHen],[TrangThai]) VALUES (N'PTN110',N'XE110',N'NV004',N'2026-10-25T00:00:00.0000000',N'2026-10-27T00:00:00.0000000',N'Đang sửa');
GO
INSERT dbo.[PhieuTiepNhan] ([MaPhieuTiepNhan],[MaXe],[MaNhanVien],[NgayLap],[NgayHen],[TrangThai]) VALUES (N'PTN111',N'XE111',N'NV004',N'2026-10-26T00:00:00.0000000',N'2026-10-28T00:00:00.0000000',N'Chờ sửa');
GO
INSERT dbo.[PhieuTiepNhan] ([MaPhieuTiepNhan],[MaXe],[MaNhanVien],[NgayLap],[NgayHen],[TrangThai]) VALUES (N'PTN112',N'XE112',N'NV004',N'2026-10-26T00:00:00.0000000',N'2026-10-29T00:00:00.0000000',N'Chờ sửa');
GO
INSERT dbo.[PhieuTiepNhan] ([MaPhieuTiepNhan],[MaXe],[MaNhanVien],[NgayLap],[NgayHen],[TrangThai]) VALUES (N'PTN113',N'XE113',N'NV004',N'2026-10-27T00:00:00.0000000',N'2026-10-28T00:00:00.0000000',N'Chờ sửa');
GO
INSERT dbo.[PhieuTiepNhan] ([MaPhieuTiepNhan],[MaXe],[MaNhanVien],[NgayLap],[NgayHen],[TrangThai]) VALUES (N'PTN114',N'XE114',N'NV004',N'2026-10-27T00:00:00.0000000',N'2026-10-30T00:00:00.0000000',N'Chờ sửa');
GO
INSERT dbo.[PhieuTiepNhan] ([MaPhieuTiepNhan],[MaXe],[MaNhanVien],[NgayLap],[NgayHen],[TrangThai]) VALUES (N'PTN115',N'XE115',N'NV001',N'2026-10-28T00:00:00.0000000',N'2026-10-29T00:00:00.0000000',N'Chờ sửa');
GO
INSERT dbo.[PhieuTiepNhan] ([MaPhieuTiepNhan],[MaXe],[MaNhanVien],[NgayLap],[NgayHen],[TrangThai]) VALUES (N'PTN116',N'XE116',N'NV001',N'2026-10-28T00:00:00.0000000',N'2026-10-30T00:00:00.0000000',N'Chờ sửa');
GO
INSERT dbo.[PhuTung] ([MaPhuTung],[MaNhaCungCap],[TenPhuTung],[DonGiaNhap],[DonGiaBan],[SoLuongTon],[HanSuDung],[TrangThai]) VALUES (N'PT001',N'NCC002',N'Nhớt Castrol Power1 4T 10W-40 0.8L',95000,125000,24,N'2028-06-30T00:00:00.0000000',N'Đang kinh doanh');
GO
INSERT dbo.[PhuTung] ([MaPhuTung],[MaNhaCungCap],[TenPhuTung],[DonGiaNhap],[DonGiaBan],[SoLuongTon],[HanSuDung],[TrangThai]) VALUES (N'PT002',N'NCC006',N'Nhớt Castrol Power1 4T 10W-40 0.8L',92000,125000,23,N'2028-12-31T00:00:00.0000000',N'Đang kinh doanh');
GO
INSERT dbo.[PhuTung] ([MaPhuTung],[MaNhaCungCap],[TenPhuTung],[DonGiaNhap],[DonGiaBan],[SoLuongTon],[HanSuDung],[TrangThai]) VALUES (N'PT003',N'NCC002',N'Nhớt Motul 3100 Gold 4T 10W-40 0.8L',108000,145000,21,N'2028-09-30T00:00:00.0000000',N'Đang kinh doanh');
GO
INSERT dbo.[PhuTung] ([MaPhuTung],[MaNhaCungCap],[TenPhuTung],[DonGiaNhap],[DonGiaBan],[SoLuongTon],[HanSuDung],[TrangThai]) VALUES (N'PT004',N'NCC001',N'Nhớt hộp số Honda 120ml',22000,35000,14,N'2028-03-31T00:00:00.0000000',N'Đang kinh doanh');
GO
INSERT dbo.[PhuTung] ([MaPhuTung],[MaNhaCungCap],[TenPhuTung],[DonGiaNhap],[DonGiaBan],[SoLuongTon],[HanSuDung],[TrangThai]) VALUES (N'PT005',N'NCC001',N'Lọc gió Honda Vision',85000,120000,18,N'2030-12-31T00:00:00.0000000',N'Đang kinh doanh');
GO
INSERT dbo.[PhuTung] ([MaPhuTung],[MaNhaCungCap],[TenPhuTung],[DonGiaNhap],[DonGiaBan],[SoLuongTon],[HanSuDung],[TrangThai]) VALUES (N'PT006',N'NCC007',N'Lọc gió Yamaha Sirius/Exciter',80000,115000,18,N'2030-12-31T00:00:00.0000000',N'Đang kinh doanh');
GO
INSERT dbo.[PhuTung] ([MaPhuTung],[MaNhaCungCap],[TenPhuTung],[DonGiaNhap],[DonGiaBan],[SoLuongTon],[HanSuDung],[TrangThai]) VALUES (N'PT007',N'NCC005',N'Bugi NGK CPR8EA-9',45000,65000,20,N'2031-12-31T00:00:00.0000000',N'Đang kinh doanh');
GO
INSERT dbo.[PhuTung] ([MaPhuTung],[MaNhaCungCap],[TenPhuTung],[DonGiaNhap],[DonGiaBan],[SoLuongTon],[HanSuDung],[TrangThai]) VALUES (N'PT008',N'NCC005',N'Bugi Denso U24EPR9',42000,60000,20,N'2031-12-31T00:00:00.0000000',N'Đang kinh doanh');
GO
INSERT dbo.[PhuTung] ([MaPhuTung],[MaNhaCungCap],[TenPhuTung],[DonGiaNhap],[DonGiaBan],[SoLuongTon],[HanSuDung],[TrangThai]) VALUES (N'PT009',N'NCC005',N'Má phanh trước Elig',55000,80000,15,N'2031-06-30T00:00:00.0000000',N'Đang kinh doanh');
GO
INSERT dbo.[PhuTung] ([MaPhuTung],[MaNhaCungCap],[TenPhuTung],[DonGiaNhap],[DonGiaBan],[SoLuongTon],[HanSuDung],[TrangThai]) VALUES (N'PT010',N'NCC005',N'Má phanh sau Elig',45000,65000,20,N'2031-06-30T00:00:00.0000000',N'Đang kinh doanh');
GO
INSERT dbo.[PhuTung] ([MaPhuTung],[MaNhaCungCap],[TenPhuTung],[DonGiaNhap],[DonGiaBan],[SoLuongTon],[HanSuDung],[TrangThai]) VALUES (N'PT011',N'NCC003',N'Lốp Michelin City Grip 2 80/90-14',520000,650000,14,N'2030-06-30T00:00:00.0000000',N'Đang kinh doanh');
GO
INSERT dbo.[PhuTung] ([MaPhuTung],[MaNhaCungCap],[TenPhuTung],[DonGiaNhap],[DonGiaBan],[SoLuongTon],[HanSuDung],[TrangThai]) VALUES (N'PT012',N'NCC003',N'Lốp Michelin City Grip 2 90/90-14',580000,720000,18,N'2030-06-30T00:00:00.0000000',N'Đang kinh doanh');
GO
INSERT dbo.[PhuTung] ([MaPhuTung],[MaNhaCungCap],[TenPhuTung],[DonGiaNhap],[DonGiaBan],[SoLuongTon],[HanSuDung],[TrangThai]) VALUES (N'PT013',N'NCC003',N'Lốp IRC 70/90-17',250000,320000,11,N'2030-06-30T00:00:00.0000000',N'Đang kinh doanh');
GO
INSERT dbo.[PhuTung] ([MaPhuTung],[MaNhaCungCap],[TenPhuTung],[DonGiaNhap],[DonGiaBan],[SoLuongTon],[HanSuDung],[TrangThai]) VALUES (N'PT014',N'NCC003',N'Lốp IRC 80/90-17',290000,370000,16,N'2030-06-30T00:00:00.0000000',N'Đang kinh doanh');
GO
INSERT dbo.[PhuTung] ([MaPhuTung],[MaNhaCungCap],[TenPhuTung],[DonGiaNhap],[DonGiaBan],[SoLuongTon],[HanSuDung],[TrangThai]) VALUES (N'PT015',N'NCC006',N'Dây curoa Bando Honda Vision/Lead',220000,290000,18,N'2030-12-31T00:00:00.0000000',N'Đang kinh doanh');
GO
INSERT dbo.[PhuTung] ([MaPhuTung],[MaNhaCungCap],[TenPhuTung],[DonGiaNhap],[DonGiaBan],[SoLuongTon],[HanSuDung],[TrangThai]) VALUES (N'PT016',N'NCC007',N'Dây curoa Yamaha Janus/Grande',230000,300000,14,N'2030-12-31T00:00:00.0000000',N'Đang kinh doanh');
GO
INSERT dbo.[PhuTung] ([MaPhuTung],[MaNhaCungCap],[TenPhuTung],[DonGiaNhap],[DonGiaBan],[SoLuongTon],[HanSuDung],[TrangThai]) VALUES (N'PT017',N'NCC004',N'Ắc quy GS GTZ5S 12V-3.5Ah',280000,360000,17,N'2027-12-31T00:00:00.0000000',N'Đang kinh doanh');
GO
INSERT dbo.[PhuTung] ([MaPhuTung],[MaNhaCungCap],[TenPhuTung],[DonGiaNhap],[DonGiaBan],[SoLuongTon],[HanSuDung],[TrangThai]) VALUES (N'PT018',N'NCC004',N'Ắc quy Đồng Nai GTZ6V 12V-5Ah',320000,410000,12,N'2027-12-31T00:00:00.0000000',N'Đang kinh doanh');
GO
INSERT dbo.[PhuTung] ([MaPhuTung],[MaNhaCungCap],[TenPhuTung],[DonGiaNhap],[DonGiaBan],[SoLuongTon],[HanSuDung],[TrangThai]) VALUES (N'PT019',N'NCC006',N'Bộ nhông xích đĩa DID Wave/Future',280000,360000,19,N'2031-12-31T00:00:00.0000000',N'Đang kinh doanh');
GO
INSERT dbo.[PhuTung] ([MaPhuTung],[MaNhaCungCap],[TenPhuTung],[DonGiaNhap],[DonGiaBan],[SoLuongTon],[HanSuDung],[TrangThai]) VALUES (N'PT020',N'NCC007',N'Bộ nhông xích đĩa Recto Exciter/Winner',450000,580000,11,N'2031-12-31T00:00:00.0000000',N'Đang kinh doanh');
GO
INSERT dbo.[PhuTung] ([MaPhuTung],[MaNhaCungCap],[TenPhuTung],[DonGiaNhap],[DonGiaBan],[SoLuongTon],[HanSuDung],[TrangThai]) VALUES (N'PT021',N'NCC008',N'Dung dịch vệ sinh kim phun Liqui Moly 300ml',110000,150000,16,N'2028-10-31T00:00:00.0000000',N'Đang kinh doanh');
GO
INSERT dbo.[PhuTung] ([MaPhuTung],[MaNhaCungCap],[TenPhuTung],[DonGiaNhap],[DonGiaBan],[SoLuongTon],[HanSuDung],[TrangThai]) VALUES (N'PT022',N'NCC008',N'Dung dịch vệ sinh nồi RTV 400ml',65000,90000,13,N'2028-08-31T00:00:00.0000000',N'Đang kinh doanh');
GO
INSERT dbo.[PhuTung] ([MaPhuTung],[MaNhaCungCap],[TenPhuTung],[DonGiaNhap],[DonGiaBan],[SoLuongTon],[HanSuDung],[TrangThai]) VALUES (N'PT023',N'NCC001',N'Nước làm mát Honda 1L',60000,85000,15,N'2029-03-31T00:00:00.0000000',N'Đang kinh doanh');
GO
INSERT dbo.[PhuTung] ([MaPhuTung],[MaNhaCungCap],[TenPhuTung],[DonGiaNhap],[DonGiaBan],[SoLuongTon],[HanSuDung],[TrangThai]) VALUES (N'PT024',N'NCC002',N'Dầu phanh Castrol DOT4 250ml',55000,75000,16,N'2028-05-31T00:00:00.0000000',N'Đang kinh doanh');
GO
INSERT dbo.[PhuTung] ([MaPhuTung],[MaNhaCungCap],[TenPhuTung],[DonGiaNhap],[DonGiaBan],[SoLuongTon],[HanSuDung],[TrangThai]) VALUES (N'PT025',N'NCC001',N'Lọc gió Honda Air Blade/Lead',95000,130000,19,N'2030-12-31T00:00:00.0000000',N'Đang kinh doanh');
GO
INSERT dbo.[PhuTung] ([MaPhuTung],[MaNhaCungCap],[TenPhuTung],[DonGiaNhap],[DonGiaBan],[SoLuongTon],[HanSuDung],[TrangThai]) VALUES (N'PT026',N'NCC006',N'Bugi NGK CPR8EA-9',43000,65000,13,N'2031-12-31T00:00:00.0000000',N'Đang kinh doanh');
GO
INSERT dbo.[PhuTung] ([MaPhuTung],[MaNhaCungCap],[TenPhuTung],[DonGiaNhap],[DonGiaBan],[SoLuongTon],[HanSuDung],[TrangThai]) VALUES (N'PT027',N'NCC001',N'Má phanh trước Honda chính hãng',90000,125000,9,N'2031-06-30T00:00:00.0000000',N'Đang kinh doanh');
GO
INSERT dbo.[PhuTung] ([MaPhuTung],[MaNhaCungCap],[TenPhuTung],[DonGiaNhap],[DonGiaBan],[SoLuongTon],[HanSuDung],[TrangThai]) VALUES (N'PT028',N'NCC007',N'Nước làm mát Yamaha 1L',58000,80000,18,N'2029-06-30T00:00:00.0000000',N'Đang kinh doanh');
GO
INSERT dbo.[PhuTung] ([MaPhuTung],[MaNhaCungCap],[TenPhuTung],[DonGiaNhap],[DonGiaBan],[SoLuongTon],[HanSuDung],[TrangThai]) VALUES (N'PT029',N'NCC007',N'Nhớt Yamalube 4AT 10W-40 0.8L',88000,115000,15,N'2028-11-30T00:00:00.0000000',N'Đang kinh doanh');
GO
INSERT dbo.[PhuTung] ([MaPhuTung],[MaNhaCungCap],[TenPhuTung],[DonGiaNhap],[DonGiaBan],[SoLuongTon],[HanSuDung],[TrangThai]) VALUES (N'PT030',N'NCC006',N'Đèn pha LED Honda Vision',350000,450000,6,N'2030-12-31T00:00:00.0000000',N'Ngừng kinh doanh');
GO
INSERT dbo.[Xe] ([MaXe],[MaKhachHang],[BienXe],[LoaiXe],[HangSanXuat],[MauSac]) VALUES (N'XE087',N'KH007',N'16G9-929.36',N'Lead 125',N'Honda',N'Trắng ngọc trai');
GO
INSERT dbo.[Xe] ([MaXe],[MaKhachHang],[BienXe],[LoaiXe],[HangSanXuat],[MauSac]) VALUES (N'XE101',N'KH014',N'29T2-215.57',N'Vision',N'Honda',N'Xám xi măng');
GO
INSERT dbo.[Xe] ([MaXe],[MaKhachHang],[BienXe],[LoaiXe],[HangSanXuat],[MauSac]) VALUES (N'XE102',N'KH043',N'15A9-507.76',N'Air Blade 125',N'Honda',N'Xanh rêu');
GO
INSERT dbo.[Xe] ([MaXe],[MaKhachHang],[BienXe],[LoaiXe],[HangSanXuat],[MauSac]) VALUES (N'XE103',N'KH029',N'15B2-762.94',N'Air Blade 125',N'Honda',N'Trắng');
GO
INSERT dbo.[Xe] ([MaXe],[MaKhachHang],[BienXe],[LoaiXe],[HangSanXuat],[MauSac]) VALUES (N'XE104',N'KH002',N'29K5-425.50',N'Attila 50',N'SYM',N'Xanh rêu');
GO
INSERT dbo.[Xe] ([MaXe],[MaKhachHang],[BienXe],[LoaiXe],[HangSanXuat],[MauSac]) VALUES (N'XE105',N'KH030',N'14P8-560.37',N'Sirius',N'Yamaha',N'Xanh rêu');
GO
INSERT dbo.[Xe] ([MaXe],[MaKhachHang],[BienXe],[LoaiXe],[HangSanXuat],[MauSac]) VALUES (N'XE106',N'KH019',N'34F5-760.74',N'Vision',N'Honda',N'Trắng ngọc trai');
GO
INSERT dbo.[Xe] ([MaXe],[MaKhachHang],[BienXe],[LoaiXe],[HangSanXuat],[MauSac]) VALUES (N'XE107',N'KH039',N'16B6-326.34',N'Air Blade 125',N'Honda',N'Đen nhám');
GO
INSERT dbo.[Xe] ([MaXe],[MaKhachHang],[BienXe],[LoaiXe],[HangSanXuat],[MauSac]) VALUES (N'XE108',N'KH025',N'16U1-820.50',N'Sirius',N'Yamaha',N'Xanh rêu');
GO
INSERT dbo.[Xe] ([MaXe],[MaKhachHang],[BienXe],[LoaiXe],[HangSanXuat],[MauSac]) VALUES (N'XE109',N'KH039',N'16X4-974.98',N'Winner X',N'Honda',N'Đỏ');
GO
INSERT dbo.[Xe] ([MaXe],[MaKhachHang],[BienXe],[LoaiXe],[HangSanXuat],[MauSac]) VALUES (N'XE110',N'KH032',N'15T7-293.49',N'Winner X',N'Honda',N'Bạc');
GO
INSERT dbo.[Xe] ([MaXe],[MaKhachHang],[BienXe],[LoaiXe],[HangSanXuat],[MauSac]) VALUES (N'XE111',N'KH012',N'34Z6-715.85',N'Air Blade 125',N'Honda',N'Đỏ');
GO
INSERT dbo.[Xe] ([MaXe],[MaKhachHang],[BienXe],[LoaiXe],[HangSanXuat],[MauSac]) VALUES (N'XE112',N'KH036',N'16C9-599.13',N'Wave Alpha 110',N'Honda',N'Xanh dương');
GO
INSERT dbo.[Xe] ([MaXe],[MaKhachHang],[BienXe],[LoaiXe],[HangSanXuat],[MauSac]) VALUES (N'XE113',N'KH042',N'15A2-527.30',N'Wave Alpha 110',N'Honda',N'Đỏ');
GO
INSERT dbo.[Xe] ([MaXe],[MaKhachHang],[BienXe],[LoaiXe],[HangSanXuat],[MauSac]) VALUES (N'XE114',N'KH003',N'34F1-156.41',N'Vision',N'Honda',N'Bạc');
GO
INSERT dbo.[Xe] ([MaXe],[MaKhachHang],[BienXe],[LoaiXe],[HangSanXuat],[MauSac]) VALUES (N'XE115',N'KH044',N'111',N'Air Blade 125',N'11',N'1');
GO
INSERT dbo.[Xe] ([MaXe],[MaKhachHang],[BienXe],[LoaiXe],[HangSanXuat],[MauSac]) VALUES (N'XE116',N'KH045',N'15P1-02183',N'Winner X',N'Honda',N'Xám');
GO
ALTER TABLE dbo.[ChiTietCaLam] WITH CHECK ADD CONSTRAINT [FK_CTCL_CaLam] FOREIGN KEY ([MaCaLam]) REFERENCES dbo.[CaLam] ([MaCaLam]) ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
ALTER TABLE dbo.[ChiTietCaLam] WITH CHECK ADD CONSTRAINT [FK_CTCL_NhanVien] FOREIGN KEY ([MaNhanVien]) REFERENCES dbo.[NhanVien] ([MaNhanVien]) ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
ALTER TABLE dbo.[ChiTietDichVu] WITH CHECK ADD CONSTRAINT [FK_CTDV_DichVu] FOREIGN KEY ([MaDichVu]) REFERENCES dbo.[DichVu] ([MaDichVu]) ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
ALTER TABLE dbo.[ChiTietDichVu] WITH CHECK ADD CONSTRAINT [FK_CTDV_PhuTung] FOREIGN KEY ([MaPhuTung]) REFERENCES dbo.[PhuTung] ([MaPhuTung]) ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
ALTER TABLE dbo.[ChiTietPhanCong] WITH CHECK ADD CONSTRAINT [FK_CTPC_NhanVien] FOREIGN KEY ([MaNhanVien]) REFERENCES dbo.[NhanVien] ([MaNhanVien]) ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
ALTER TABLE dbo.[ChiTietPhanCong] WITH CHECK ADD CONSTRAINT [FK_CTPC_Xe] FOREIGN KEY ([MaXe]) REFERENCES dbo.[Xe] ([MaXe]) ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
ALTER TABLE dbo.[ChiTietPhieuNhap] WITH CHECK ADD CONSTRAINT [FK_CTPN_PhieuNhap] FOREIGN KEY ([MaPhieuNhap]) REFERENCES dbo.[PhieuNhap] ([MaPhieuNhap]) ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
ALTER TABLE dbo.[ChiTietPhieuNhap] WITH CHECK ADD CONSTRAINT [FK_CTPN_PhuTung] FOREIGN KEY ([MaPhuTung]) REFERENCES dbo.[PhuTung] ([MaPhuTung]) ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
ALTER TABLE dbo.[ChiTietPhieuTiepNhan] WITH CHECK ADD CONSTRAINT [FK_CTPTN_DichVu] FOREIGN KEY ([MaDichVu]) REFERENCES dbo.[DichVu] ([MaDichVu]) ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
ALTER TABLE dbo.[ChiTietPhieuTiepNhan] WITH CHECK ADD CONSTRAINT [FK_CTPTN_PhieuTiepNhan] FOREIGN KEY ([MaPhieuTiepNhan]) REFERENCES dbo.[PhieuTiepNhan] ([MaPhieuTiepNhan]) ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
ALTER TABLE dbo.[ChucVuDieuChinh] WITH CHECK ADD CONSTRAINT [FK_CVDC_ChucVu] FOREIGN KEY ([MaChucVu]) REFERENCES dbo.[ChucVu] ([MaChucVu]) ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
ALTER TABLE dbo.[ChucVuDieuChinh] WITH CHECK ADD CONSTRAINT [FK_CVDC_KhoanDieuChinh] FOREIGN KEY ([MaKhoanDieuChinh]) REFERENCES dbo.[KhoanDieuChinh] ([MaKhoanDieuChinh]) ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
ALTER TABLE dbo.[DieuChinhGiaBan] WITH CHECK ADD CONSTRAINT [FK_DCGB_NhanVien] FOREIGN KEY ([MaNhanVien]) REFERENCES dbo.[NhanVien] ([MaNhanVien]) ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
ALTER TABLE dbo.[DieuChinhGiaBan] WITH CHECK ADD CONSTRAINT [FK_DCGB_PhuTung] FOREIGN KEY ([MaPhuTung]) REFERENCES dbo.[PhuTung] ([MaPhuTung]) ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
ALTER TABLE dbo.[DieuChinhTonKho] WITH CHECK ADD CONSTRAINT [FK_DCTK_NhanVien] FOREIGN KEY ([MaNhanVien]) REFERENCES dbo.[NhanVien] ([MaNhanVien]) ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
ALTER TABLE dbo.[DieuChinhTonKho] WITH CHECK ADD CONSTRAINT [FK_DCTK_PhuTung] FOREIGN KEY ([MaPhuTung]) REFERENCES dbo.[PhuTung] ([MaPhuTung]) ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
ALTER TABLE dbo.[HoaDon] WITH CHECK ADD CONSTRAINT [FK_HoaDon_PhieuTiepNhan] FOREIGN KEY ([MaPhieuTiepNhan]) REFERENCES dbo.[PhieuTiepNhan] ([MaPhieuTiepNhan]) ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
ALTER TABLE dbo.[NhanVien] WITH CHECK ADD CONSTRAINT [FK_NhanVien_ChucVu] FOREIGN KEY ([MaChucVu]) REFERENCES dbo.[ChucVu] ([MaChucVu]) ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
ALTER TABLE dbo.[PhieuTiepNhan] WITH CHECK ADD CONSTRAINT [FK_PTN_NhanVien] FOREIGN KEY ([MaNhanVien]) REFERENCES dbo.[NhanVien] ([MaNhanVien]) ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
ALTER TABLE dbo.[PhieuTiepNhan] WITH CHECK ADD CONSTRAINT [FK_PTN_Xe] FOREIGN KEY ([MaXe]) REFERENCES dbo.[Xe] ([MaXe]) ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
ALTER TABLE dbo.[PhieuNhap] WITH CHECK ADD CONSTRAINT [FK_PhieuNhap_NhanVien] FOREIGN KEY ([MaNhanVienNhap]) REFERENCES dbo.[NhanVien] ([MaNhanVien]) ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
ALTER TABLE dbo.[PhuTung] WITH CHECK ADD CONSTRAINT [FK_PhuTung_NhaCungCap] FOREIGN KEY ([MaNhaCungCap]) REFERENCES dbo.[NhaCungCap] ([MaNhaCungCap]) ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
ALTER TABLE dbo.[Xe] WITH CHECK ADD CONSTRAINT [FK_Xe_KhachHang] FOREIGN KEY ([MaKhachHang]) REFERENCES dbo.[KhachHang] ([MaKhachHang]) ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
