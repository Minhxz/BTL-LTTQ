USE QuanLySuaXe;
GO
SELECT DB_NAME() AS CoSoDuLieu, @@SERVERNAME AS MayChu, SUSER_SNAME() AS TaiKhoanWindows;
SELECT name AS Bang FROM sys.tables ORDER BY name;
SELECT COUNT(*) AS SoNhanVien FROM dbo.NhanVien;
SELECT COUNT(*) AS SoHoaDon, SUM(TienThanhToan) AS TongTienHoaDon FROM dbo.HoaDon;
GO
