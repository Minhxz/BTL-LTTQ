USE QuanLySuaXe;
GO
SELECT MaNCC, TenNCC, SoDienThoai, DiaChi FROM dbo.NhaCungCap ORDER BY MaNCC;
SELECT MaPhuTung, TenPhuTung, DonGiaNhap, DonGiaBan, SoLuongTon, TrangThai
FROM dbo.PhuTung ORDER BY MaPhuTung;
SELECT pn.MaPhieuNhap, pn.NgayNhap, pn.MaNhanVienNhap, nv.TenNhanVien,
       pn.GhiChu, pn.TongTien
FROM dbo.PhieuNhap AS pn
JOIN dbo.NhanVien AS nv ON nv.MaNhanVien = pn.MaNhanVienNhap
ORDER BY pn.MaPhieuNhap;
SELECT pn.MaPhieuNhap, pn.NgayNhap, nv.TenNhanVien AS NguoiNhap,
       ct.MaNCC, ncc.TenNCC, ct.MaPhuTung, pt.TenPhuTung,
       ct.SoLuong, ct.DonGiaNhap, ct.ThanhTien, ct.HanSuDung
FROM dbo.ChiTietPhieuNhap AS ct
JOIN dbo.PhieuNhap AS pn ON pn.MaPhieuNhap = ct.MaPhieuNhap
JOIN dbo.NhanVien AS nv ON nv.MaNhanVien = pn.MaNhanVienNhap
JOIN dbo.PhuTung AS pt ON pt.MaPhuTung = ct.MaPhuTung
LEFT JOIN dbo.NhaCungCap AS ncc ON ncc.MaNCC = ct.MaNCC
ORDER BY pn.MaPhieuNhap, ct.MaChiTietPhieuNhap;
GO
