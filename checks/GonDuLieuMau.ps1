param([string]$ThuMucSaoLuu='C:\Users\buiqu\Desktop\BTL-LTTQ\SaoLuuSuaXe')
$ErrorActionPreference='Stop'
. (Join-Path $PSScriptRoot 'SqlChung.ps1')
$gocDuAn=Split-Path -Parent $PSScriptRoot
$gocChoPhep=[IO.Path]::GetFullPath((Split-Path -Parent $gocDuAn)).TrimEnd('\')+'\'
$thuMuc=[IO.Path]::GetFullPath($ThuMucSaoLuu).TrimEnd('\')
if(-not ($thuMuc+'\').StartsWith($gocChoPhep,[StringComparison]::OrdinalIgnoreCase)){throw 'Thư mục sao lưu phải nằm trong thư mục bài đã được cho phép.'}
[void](New-Item -ItemType Directory -Path $thuMuc -Force)
$gio=Get-Date -Format 'yyyyMMdd_HHmmss'
$tepSaoLuu=Join-Path $thuMuc "QuanLySuaXe_TruocGonDuLieu_$gio.bak"
$tepBaoCao=Join-Path $PSScriptRoot "output/GonDuLieuMau_$gio.json"
[void](New-Item -ItemType Directory -Path (Split-Path -Parent $tepBaoCao) -Force)
$kn=MoSql 'QuanLySuaXe'
function DemBang {
    $bang=DocSql $kn 'SELECT t.name AS Bang,SUM(p.rows) AS SoDong FROM sys.tables t JOIN sys.partitions p ON p.object_id=t.object_id AND p.index_id IN(0,1) GROUP BY t.name ORDER BY t.name'
    return @($bang.Rows | ForEach-Object {[pscustomobject]@{Bang=$_.Bang;SoDong=[int]$_.SoDong}})
}
function NhanDienDanhMuc {
    $ketQua=@{}
    foreach($ten in @('NhanVien','ChucVu','KhoanDieuChinh','ChucVuDieuChinh','CaLam','DichVu','ChiTietDichVu','PhuTung','NhaCungCap')){
        $cot=DocSql $kn "SELECT c.name FROM sys.indexes i JOIN sys.index_columns k ON k.object_id=i.object_id AND k.index_id=i.index_id JOIN sys.columns c ON c.object_id=k.object_id AND c.column_id=k.column_id WHERE i.object_id=OBJECT_ID(N'dbo.$ten') AND i.is_primary_key=1 ORDER BY k.key_ordinal"
        $sapXep=@($cot.Rows|ForEach-Object {'['+$_.name+']'}) -join ','
        $json=@((DocSql $kn "SELECT * FROM dbo.[$ten] ORDER BY $sapXep FOR JSON PATH,INCLUDE_NULL_VALUES").Rows | ForEach-Object {$_.Item(0)}) -join ''
        $sha=[Security.Cryptography.SHA256]::Create()
        try {$ketQua[$ten]=[BitConverter]::ToString($sha.ComputeHash([Text.Encoding]::UTF8.GetBytes($json))).Replace('-','')} finally {$sha.Dispose()}
    }
    return $ketQua
}
try {
    if((DocSql $kn 'SELECT DB_NAME() AS Ten').Rows[0].Ten -cne 'QuanLySuaXe'){throw 'Chỉ được dọn database QuanLySuaXe.'}
    if((DocSql $kn 'SELECT COUNT(*) AS So FROM dbo.NhanVien').Rows[0].So -ne 5){throw 'Database chưa đúng bộ năm nhân viên. Dừng để giữ dữ liệu.'}
    if((DocSql $kn "SELECT COUNT(*) AS So FROM dbo.NhanVien WHERE TenDangNhap IN('minh','an','huy','mai','cuong') AND TrangThai=N'Đang làm' AND MatKhau=CONVERT(VARCHAR(64),HASHBYTES('SHA2_256',CONVERT(VARCHAR(3),'123')),2)").Rows[0].So -ne 5){throw 'Tên đăng nhập hoặc mật khẩu năm nhân viên không khớp.'}
    $truoc=DemBang
    $dauDanhMuc=NhanDienDanhMuc
    $duongDanSql=$tepSaoLuu.Replace("'","''")
    ChaySql $kn "BACKUP DATABASE [QuanLySuaXe] TO DISK=N'$duongDanSql' WITH COPY_ONLY,CHECKSUM,INIT;"
    ChaySql $kn "RESTORE VERIFYONLY FROM DISK=N'$duongDanSql' WITH CHECKSUM;"
    Write-Output "Đã sao lưu và kiểm tra: $tepSaoLuu"
    $sql=@'
SET XACT_ABORT ON;
SET NOCOUNT ON;
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
BEGIN TRY
    BEGIN TRANSACTION;
    DECLARE @SoPhieuCu INT=(SELECT COUNT(*) FROM dbo.PhieuTiepNhan);
    DECLARE @GiuPhieu TABLE(MaPhieuTiepNhan VARCHAR(10) PRIMARY KEY);
    INSERT @GiuPhieu SELECT TOP(6) p.MaPhieuTiepNhan FROM dbo.PhieuTiepNhan p WHERE p.TrangThai IN(N'Chờ sửa',N'Đang sửa') ORDER BY CASE WHEN p.TrangThai=N'Đang sửa' THEN 0 ELSE 1 END,p.NgayLap DESC,p.MaPhieuTiepNhan DESC;
    INSERT @GiuPhieu SELECT TOP(2) p.MaPhieuTiepNhan FROM dbo.PhieuTiepNhan p WHERE p.TrangThai=N'Hoàn thành' AND NOT EXISTS(SELECT 1 FROM dbo.HoaDon h WHERE h.MaPhieuTiepNhan=p.MaPhieuTiepNhan) AND NOT EXISTS(SELECT 1 FROM @GiuPhieu g WHERE g.MaPhieuTiepNhan=p.MaPhieuTiepNhan) ORDER BY p.NgayLap DESC,p.MaPhieuTiepNhan DESC;
    INSERT @GiuPhieu SELECT TOP(1) p.MaPhieuTiepNhan FROM dbo.PhieuTiepNhan p WHERE p.TrangThai=N'Đã hủy' AND NOT EXISTS(SELECT 1 FROM @GiuPhieu g WHERE g.MaPhieuTiepNhan=p.MaPhieuTiepNhan) ORDER BY p.NgayLap DESC,p.MaPhieuTiepNhan DESC;
    DECLARE @ConCho INT=15-(SELECT COUNT(*) FROM @GiuPhieu);
    INSERT @GiuPhieu SELECT TOP(@ConCho) p.MaPhieuTiepNhan FROM dbo.PhieuTiepNhan p WHERE NOT EXISTS(SELECT 1 FROM @GiuPhieu g WHERE g.MaPhieuTiepNhan=p.MaPhieuTiepNhan) ORDER BY CASE WHEN EXISTS(SELECT 1 FROM dbo.HoaDon h WHERE h.MaPhieuTiepNhan=p.MaPhieuTiepNhan) THEN 0 ELSE 1 END,p.NgayLap DESC,p.MaPhieuTiepNhan DESC;
    IF (SELECT COUNT(*) FROM @GiuPhieu)<12 THROW 51100,N'Không đủ phiếu mẫu để chọn bộ gọn.',1;
    DECLARE @GiuNhap TABLE(MaPhieuNhap VARCHAR(10) PRIMARY KEY);
    INSERT @GiuNhap SELECT TOP(3) MaPhieuNhap FROM dbo.PhieuNhap ORDER BY NgayNhap DESC,MaPhieuNhap DESC;
    DECLARE @NgayCongCuoi DATE=(SELECT MAX(NgayLam) FROM dbo.ChiTietCaLam);
    IF @NgayCongCuoi IS NULL THROW 51101,N'Chưa có ngày công để chọn bộ mẫu.',1;
    DELETE h FROM dbo.HoaDon h WHERE NOT EXISTS(SELECT 1 FROM @GiuPhieu g WHERE g.MaPhieuTiepNhan=h.MaPhieuTiepNhan);
    DELETE c FROM dbo.ChiTietPhieuTiepNhan c WHERE NOT EXISTS(SELECT 1 FROM @GiuPhieu g WHERE g.MaPhieuTiepNhan=c.MaPhieuTiepNhan);
    DELETE c FROM dbo.ChiTietPhanCong c WHERE NOT EXISTS(SELECT 1 FROM dbo.PhieuTiepNhan p JOIN @GiuPhieu g ON g.MaPhieuTiepNhan=p.MaPhieuTiepNhan WHERE p.MaXe=c.MaXe);
    DELETE p FROM dbo.PhieuTiepNhan p WHERE NOT EXISTS(SELECT 1 FROM @GiuPhieu g WHERE g.MaPhieuTiepNhan=p.MaPhieuTiepNhan);
    DELETE x FROM dbo.Xe x WHERE NOT EXISTS(SELECT 1 FROM dbo.PhieuTiepNhan p WHERE p.MaXe=x.MaXe) AND NOT EXISTS(SELECT 1 FROM dbo.ChiTietPhanCong c WHERE c.MaXe=x.MaXe);
    DELETE k FROM dbo.KhachHang k WHERE NOT EXISTS(SELECT 1 FROM dbo.Xe x WHERE x.MaKhachHang=k.MaKhachHang);
    DELETE c FROM dbo.ChiTietPhieuNhap c WHERE NOT EXISTS(SELECT 1 FROM @GiuNhap g WHERE g.MaPhieuNhap=c.MaPhieuNhap);
    DELETE p FROM dbo.PhieuNhap p WHERE NOT EXISTS(SELECT 1 FROM @GiuNhap g WHERE g.MaPhieuNhap=p.MaPhieuNhap);
    DELETE FROM dbo.ChiTietCaLam WHERE NgayLam<DATEADD(DAY,-6,@NgayCongCuoi);
    IF @SoPhieuCu>15 AND NOT EXISTS(SELECT 1 FROM dbo.PhieuTiepNhan WHERE TrangThai=N'Đang sửa') AND NOT EXISTS(SELECT 1 FROM dbo.PhieuTiepNhan p WHERE p.TrangThai=N'Hoàn thành' AND NOT EXISTS(SELECT 1 FROM dbo.HoaDon h WHERE h.MaPhieuTiepNhan=p.MaPhieuTiepNhan))
    BEGIN
        DECLARE @MaHoanThanh VARCHAR(10),@MaDangSua VARCHAR(10),@MaXeHoanThanh VARCHAR(10),@MaXeDangSua VARCHAR(10),@LucNay DATETIME2(0)=SYSDATETIME();
        SELECT TOP(1) @MaHoanThanh=p.MaPhieuTiepNhan,@MaXeHoanThanh=p.MaXe FROM dbo.PhieuTiepNhan p WHERE p.TrangThai=N'Chờ sửa' AND NOT EXISTS(SELECT 1 FROM dbo.ChiTietPhanCong c WHERE c.MaXe=p.MaXe) ORDER BY p.NgayLap,p.MaPhieuTiepNhan;
        SELECT TOP(1) @MaDangSua=p.MaPhieuTiepNhan,@MaXeDangSua=p.MaXe FROM dbo.PhieuTiepNhan p WHERE p.TrangThai=N'Chờ sửa' AND p.MaPhieuTiepNhan<>@MaHoanThanh AND NOT EXISTS(SELECT 1 FROM dbo.ChiTietPhanCong c WHERE c.MaXe=p.MaXe) ORDER BY p.NgayLap,p.MaPhieuTiepNhan;
        IF @MaHoanThanh IS NULL OR @MaDangSua IS NULL THROW 51102,N'Không đủ phiếu chờ chưa phân công để biểu diễn luồng mẫu.',1;
        UPDATE dbo.PhieuTiepNhan SET TrangThai=N'Hoàn thành' WHERE MaPhieuTiepNhan=@MaHoanThanh;
        INSERT dbo.ChiTietPhanCong(MaNhanVien,MaXe,ThoiGianBatDau,ThoiGianHoanThanh) VALUES('NV002',@MaXeHoanThanh,DATEADD(HOUR,-3,@LucNay),DATEADD(HOUR,-1,@LucNay));
        UPDATE dbo.PhieuTiepNhan SET TrangThai=N'Đang sửa' WHERE MaPhieuTiepNhan=@MaDangSua;
        INSERT dbo.ChiTietPhanCong(MaNhanVien,MaXe,ThoiGianBatDau,ThoiGianHoanThanh) VALUES('NV003',@MaXeDangSua,@LucNay,DATEADD(HOUR,2,@LucNay));
    END;
    IF EXISTS(SELECT c.MaNhanVien FROM dbo.ChiTietPhanCong c JOIN dbo.PhieuTiepNhan p ON p.MaXe=c.MaXe JOIN dbo.NhanVien n ON n.MaNhanVien=c.MaNhanVien JOIN dbo.ChucVu v ON v.MaChucVu=n.MaChucVu WHERE p.TrangThai IN(N'Chờ sửa',N'Đang sửa') GROUP BY c.MaNhanVien,v.TenChucVu HAVING COUNT(DISTINCT c.MaXe)>CASE WHEN v.TenChucVu=N'Kỹ thuật viên chính' THEN 3 WHEN v.TenChucVu IN(N'Kỹ thuật viên',N'Thợ phụ') THEN 2 ELSE 0 END)
        THROW 51103,N'Phân công mẫu vượt giới hạn nhận xe.',1;
    IF EXISTS(SELECT 1 FROM dbo.ChiTietCaLam GROUP BY MaNhanVien,NgayLam HAVING SUM(TongThoiGianLamThucTe)>24)
        THROW 51104,N'Giờ công mẫu vượt 24 giờ mỗi ngày.',1;
    IF (SELECT COUNT(*) FROM sys.tables WHERE is_ms_shipped=0)<>18 OR EXISTS(SELECT 1 FROM sys.columns c JOIN sys.tables t ON t.object_id=c.object_id WHERE c.is_nullable=1)
        THROW 51105,N'Cấu trúc 18 bảng không NULL không còn đúng.',1;
    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT>0 ROLLBACK TRANSACTION;
    THROW;
END CATCH;
'@
    ChaySql $kn $sql
    $sau=DemBang
    $sauDanhMuc=NhanDienDanhMuc
    foreach($ten in $dauDanhMuc.Keys){if($dauDanhMuc[$ten] -cne $sauDanhMuc[$ten]){throw "Danh mục đã thay đổi ngoài phạm vi: $ten"}}
    if((DocSql $kn 'DBCC CHECKCONSTRAINTS WITH ALL_CONSTRAINTS,NO_INFOMSGS').Rows.Count){throw 'Có ràng buộc dữ liệu không hợp lệ.'}
    if((DocSql $kn 'DBCC CHECKDB([QuanLySuaXe]) WITH PHYSICAL_ONLY,NO_INFOMSGS,MAXDOP=1').Rows.Count){throw 'Kiểm tra tính toàn vẹn database có lỗi.'}
    $trangThai=DocSql $kn "SELECT p.TrangThai,CASE WHEN h.MaHoaDon IS NULL THEN N'Chưa thu' ELSE N'Đã thu' END AS ThanhToan,COUNT(*) AS SoPhieu FROM dbo.PhieuTiepNhan p LEFT JOIN dbo.HoaDon h ON h.MaPhieuTiepNhan=p.MaPhieuTiepNhan GROUP BY p.TrangThai,CASE WHEN h.MaHoaDon IS NULL THEN N'Chưa thu' ELSE N'Đã thu' END ORDER BY p.TrangThai"
    $doanhThu=DocSql $kn 'SELECT COUNT(*) SoHoaDon,COALESCE(SUM(TienThanhToan),0) TienThanhToan FROM dbo.HoaDon'
    $baoCao=[ordered]@{Database='QuanLySuaXe';SaoLuu=$tepSaoLuu;DaKiemTraSaoLuu=$true;Truoc=$truoc;Sau=$sau;TrangThai=@($trangThai.Rows|ForEach-Object {[pscustomobject]@{TrangThai=$_.TrangThai;ThanhToan=$_.ThanhToan;SoPhieu=[int]$_.SoPhieu}});SoHoaDon=[int]$doanhThu.Rows[0].SoHoaDon;TienThanhToan=[decimal]$doanhThu.Rows[0].TienThanhToan;DanhMucVaTonKhoGiuNguyen=$true;RangBuocHopLe=$true;KiemTraVatLyHopLe=$true;LoaiKiemTraToanVen='CHECKCONSTRAINTS và CHECKDB PHYSICAL_ONLY MAXDOP 1'}
    $baoCao | ConvertTo-Json -Depth 8 | Set-Content -Encoding utf8 -LiteralPath $tepBaoCao
    $sau | Format-Table
    $trangThai.Rows | Format-Table
    Write-Output "Đã dọn dữ liệu mẫu, giữ nguyên toàn bộ danh mục và tồn kho. Báo cáo: $tepBaoCao"
} finally {$kn.Dispose()}

