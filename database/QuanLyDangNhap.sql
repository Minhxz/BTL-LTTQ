USE QuanLySuaXe;
GO
SELECT MaChucVu, TenChucVu, MoTa FROM dbo.ChucVu ORDER BY MaChucVu;
SELECT nv.MaNhanVien, nv.TenDangNhap, nv.TenNhanVien, cv.TenChucVu,
       nv.SoDienThoai, nv.TrangThai, nv.SoXeDangSuaToiDa
FROM dbo.NhanVien AS nv
JOIN dbo.ChucVu AS cv ON cv.MaChucVu = nv.MaChucVu
ORDER BY nv.MaNhanVien;
GO
