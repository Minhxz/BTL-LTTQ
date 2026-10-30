param(
    [ValidatePattern('^QuanLySuaXe(?:_[A-Za-z0-9_]+)?$')]
    [string]$Database='QuanLySuaXe',
    [switch]$ApDung,
    [switch]$KiemThu,
    [string]$ThuMucSaoLuu=(Join-Path (Split-Path (Split-Path $PSScriptRoot -Parent) -Parent) 'SaoLuuSuaXe')
)
$ErrorActionPreference='Stop'
. (Join-Path $PSScriptRoot 'SqlChung.ps1')
$gocDuAn=Split-Path $PSScriptRoot -Parent
$tepCapNhat=Join-Path $gocDuAn 'database/CapNhatKho_20261029.sql'
$bangCu=@('CaLam','ChiTietCaLam','ChiTietDichVu','ChiTietPhanCong','ChiTietPhieuNhap','ChiTietPhieuTiepNhan','ChucVu','ChucVuDieuChinh','DichVu','HoaDon','KhachHang','KhoanDieuChinh','NhaCungCap','NhanVien','PhieuNhap','PhieuTiepNhan','PhuTung','Xe')
$bangLichSu=@('DieuChinhTonKho','DieuChinhGiaBan')
$danhSachBangCu=($bangCu|ForEach-Object{"N'$_'"}) -join ','
if($KiemThu -and $Database -notmatch '^QuanLySuaXe_(?:Codex|KiemThu)[A-Za-z0-9_]*$'){
    throw 'Kiểm thử ghi thử/hoàn tác chỉ chạy trên database có tên QuanLySuaXe_Codex... hoặc QuanLySuaXe_KiemThu..., không chạy trên database thật.'
}
if(-not (Test-Path -LiteralPath $tepCapNhat)){throw 'Không tìm thấy bản cập nhật kho.'}

function BamChuoi([string]$noiDung){
    $bam=[Security.Cryptography.SHA256]::Create()
    try{return [BitConverter]::ToString($bam.ComputeHash([Text.Encoding]::UTF8.GetBytes($noiDung))).Replace('-','')}
    finally{$bam.Dispose()}
}
function BaoTen([string]$ten){return '['+$ten.Replace(']',']]')+']'}
function DocJson($ketNoi,[string]$sql,$giaoDich=$null){
    return (@((DocSql $ketNoi $sql $giaoDich).Rows|ForEach-Object{$_.Item(0)}) -join '')
}
function LayDauVanTay($ketNoi,$giaoDich=$null){
    $ketQua=@{}
    $cacBang=DocSql $ketNoi "SELECT object_id,name FROM sys.tables WHERE schema_id=SCHEMA_ID('dbo') AND is_ms_shipped=0 ORDER BY name" $giaoDich
    foreach($bang in $cacBang.Rows){
        $khoa=DocSql $ketNoi "SELECT c.name FROM sys.indexes i JOIN sys.index_columns k ON k.object_id=i.object_id AND k.index_id=i.index_id JOIN sys.columns c ON c.object_id=k.object_id AND c.column_id=k.column_id WHERE i.object_id=$($bang.object_id) AND i.is_primary_key=1 ORDER BY k.key_ordinal" $giaoDich
        if($khoa.Rows.Count -eq 0){throw ('Bảng chưa có khóa chính để đối chiếu dữ liệu: '+$bang.name)}
        $sapXep=@($khoa.Rows|ForEach-Object{BaoTen $_.name}) -join ','
        $duLieu=DocJson $ketNoi ('SELECT (SELECT * FROM dbo.'+(BaoTen $bang.name)+' ORDER BY '+$sapXep+' FOR JSON PATH,INCLUDE_NULL_VALUES) AS DuLieu') $giaoDich
        $soDong=(DocSql $ketNoi ('SELECT COUNT_BIG(*) AS So FROM dbo.'+(BaoTen $bang.name)) $giaoDich).Rows[0].So
        $ketQua[$bang.name]=[pscustomobject]@{Bang=$bang.name;SoDong=[long]$soDong;SHA256=(BamChuoi $duLieu)}
    }
    return $ketQua
}
function DoiChieuDauVanTay($truoc,$sau){
    foreach($ten in $truoc.Keys){
        if(-not $sau.ContainsKey($ten) -or $truoc[$ten].SoDong -ne $sau[$ten].SoDong -or $truoc[$ten].SHA256 -cne $sau[$ten].SHA256){
            throw ('Dữ liệu bảng '+$ten+' thay đổi. Không chấp nhận cập nhật, phải giữ nguyên mọi dữ liệu cũ.')
        }
    }
}
function LayCauTrucCotCu($ketNoi,$giaoDich=$null){
    return DocJson $ketNoi "SELECT t.name AS Bang,c.name AS Cot,y.name AS Kieu,c.max_length,c.precision,c.scale,c.is_nullable,c.is_identity,c.is_computed,c.collation_name FROM sys.tables t JOIN sys.columns c ON c.object_id=t.object_id JOIN sys.types y ON y.user_type_id=c.user_type_id WHERE t.schema_id=SCHEMA_ID('dbo') AND t.name IN($danhSachBangCu) ORDER BY t.name,c.column_id FOR JSON PATH,INCLUDE_NULL_VALUES" $giaoDich
}
function LayTaiKhoanGiuNguyen($ketNoi,$giaoDich=$null){
    $cacTaiKhoan=DocSql $ketNoi @'
SELECT n.MaNhanVien,n.HoTen,n.TenDangNhap,c.TenChucVu,n.TrangThai,
       CASE WHEN n.MatKhau=CONVERT(VARCHAR(64),HASHBYTES('SHA2_256','123'),2) THEN 1 ELSE 0 END AS MatKhau123
FROM dbo.NhanVien n JOIN dbo.ChucVu c ON c.MaChucVu=n.MaChucVu
WHERE n.TenDangNhap IN('minh','an','huy','mai','cuong') ORDER BY n.MaNhanVien;
'@ $giaoDich
    if($cacTaiKhoan.Rows.Count -ne 5 -or @($cacTaiKhoan.Rows|Where-Object{$_.MatKhau123 -ne 1}).Count){
        throw 'Năm tài khoản tên/123 chưa khớp dữ liệu dự kiến. Dừng để đối chiếu, không tự đặt lại mật khẩu.'
    }
    return @($cacTaiKhoan.Rows|ForEach-Object{[pscustomobject]@{MaNhanVien=$_.MaNhanVien;HoTen=$_.HoTen;TenDangNhap=$_.TenDangNhap;TenChucVu=$_.TenChucVu;TrangThai=$_.TrangThai;MatKhau123=[bool]$_.MatKhau123}})
}
function KiemTraCauTruc($ketNoi,[bool]$daCapNhat,$giaoDich=$null){
    $cacBang=DocSql $ketNoi "SELECT name FROM sys.tables WHERE schema_id=SCHEMA_ID('dbo') AND is_ms_shipped=0 ORDER BY name" $giaoDich
    $tenBang=@($cacBang.Rows|ForEach-Object{$_.name})
    if(@($bangCu|Where-Object{$_ -notin $tenBang}).Count){throw 'Không đủ 18 bảng gốc.'}
    if(@($tenBang|Where-Object{$_ -notin ($bangCu+$bangLichSu)}).Count){throw 'Có bảng ngoài cấu trúc 18 bảng gốc và hai bảng lịch sử dự kiến. Dừng để đối chiếu.'}
    if($daCapNhat -and @($bangLichSu|Where-Object{$_ -notin $tenBang}).Count){throw 'Chưa có đủ hai bảng lịch sử kho.'}
    if((DocSql $ketNoi 'SELECT COUNT(*) AS So FROM sys.tables t JOIN sys.columns c ON c.object_id=t.object_id WHERE c.is_nullable=1' $giaoDich).Rows[0].So -ne 0){throw 'Còn cột cho phép NULL.'}
    if((DocSql $ketNoi 'SELECT COUNT(*) AS So FROM sys.foreign_keys WHERE is_disabled=1 OR is_not_trusted=1' $giaoDich).Rows[0].So -ne 0){throw 'Có khóa ngoại bị tắt hoặc chưa đối chiếu dữ liệu.'}
    if((DocSql $ketNoi 'SELECT COUNT(*) AS So FROM sys.check_constraints WHERE is_disabled=1 OR is_not_trusted=1' $giaoDich).Rows[0].So -ne 0){throw 'Có điều kiện kiểm tra bị tắt hoặc chưa đối chiếu dữ liệu.'}
    if($daCapNhat){
        if((DocSql $ketNoi "SELECT COUNT(*) AS So FROM sys.computed_columns WHERE object_id=OBJECT_ID('dbo.DieuChinhTonKho') AND name='GiaTriChenhLech' AND is_persisted=1 AND is_nullable=0" $giaoDich).Rows[0].So -ne 1){throw 'Giá trị chênh lệch chưa là computed PERSISTED NOT NULL.'}
        if($tenBang.Count -ne 20){throw 'Cập nhật chưa đúng 20 bảng.'}
    }
    return $tenBang
}
function ThuTrongGiaoDich($ketNoi,[string]$ten,[string]$sql,[bool]$phaiBiChan=$false){
    $thu=$ketNoi.BeginTransaction()
    $biChan=$false
    try{ChaySql $ketNoi $sql $thu}
    catch [Data.SqlClient.SqlException]{
        if(-not $phaiBiChan -or $_.Exception.Number -notin @(547,515,2627,2601)){throw}
        $biChan=$true
    }
    finally{if($thu.Connection){$thu.Rollback()};$thu.Dispose()}
    if($phaiBiChan -and -not $biChan){throw ('Điều kiện chưa chặn: '+$ten)}
    Write-Output ('PASS kiểm thử tự hoàn tác: '+$ten)
}
function KiemThuLichSu($ketNoi){
    $truocThu=LayDauVanTay $ketNoi
    $maPT=(DocSql $ketNoi 'SELECT TOP(1) MaPhuTung FROM dbo.PhuTung ORDER BY MaPhuTung').Rows[0].MaPhuTung.Replace("'","''")
    $maNV=(DocSql $ketNoi 'SELECT TOP(1) MaNhanVien FROM dbo.NhanVien ORDER BY MaNhanVien').Rows[0].MaNhanVien.Replace("'","''")
    $soNgauNhien=Get-Random -Minimum 10000000 -Maximum 99999999
    $maDC='DC'+$soNgauNhien;$maDG='DG'+$soNgauNhien
    $chenTon="INSERT dbo.DieuChinhTonKho(MaDieuChinh,MaPhuTung,MaNhanVien,NgayDieuChinh,SoLuongCu,SoLuongMoi,DonGiaNhap,LyDo) VALUES('$maDC','$maPT','$maNV',CONVERT(date,GETDATE()),12,10,95000,N'Kiểm thử tự hoàn tác');"
    $chenGia="INSERT dbo.DieuChinhGiaBan(MaDieuChinhGia,MaPhuTung,MaNhanVien,NgayDieuChinh,GiaBanCu,GiaBanMoi,LyDo) VALUES('$maDG','$maPT','$maNV',CONVERT(date,GETDATE()),125000,90000,N'Kiểm thử tự hoàn tác');"
    ThuTrongGiaoDich $ketNoi 'Giảm tồn lưu chênh lệch âm đúng -190000' ($chenTon+" IF (SELECT GiaTriChenhLech FROM dbo.DieuChinhTonKho WHERE MaDieuChinh='$maDC')<>-190000 THROW 51200,N'Tính sai chênh lệch.',1;")
    ThuTrongGiaoDich $ketNoi 'Tăng tồn lưu chênh lệch dương' ($chenTon.Replace(',12,10,95000,',',10,12,95000,')+" IF (SELECT GiaTriChenhLech FROM dbo.DieuChinhTonKho WHERE MaDieuChinh='$maDC')<>190000 THROW 51201,N'Tính sai chênh lệch.',1;")
    ThuTrongGiaoDich $ketNoi 'Computed cập nhật lại khi số lượng và giá nhập thay đổi' ($chenTon+" UPDATE dbo.DieuChinhTonKho SET SoLuongMoi=14,DonGiaNhap=100000 WHERE MaDieuChinh='$maDC'; IF (SELECT GiaTriChenhLech FROM dbo.DieuChinhTonKho WHERE MaDieuChinh='$maDC')<>200000 THROW 51202,N'Computed không cập nhật.',1;")
    ThuTrongGiaoDich $ketNoi 'Tồn không đổi bị chặn' ($chenTon.Replace(',12,10,95000,',',12,12,95000,')) $true
    ThuTrongGiaoDich $ketNoi 'Tồn âm bị chặn' ($chenTon.Replace(',12,10,95000,',',12,-1,95000,')) $true
    ThuTrongGiaoDich $ketNoi 'Giá nhập bằng 0 bị chặn' ($chenTon.Replace(',12,10,95000,',',12,10,0,')) $true
    ThuTrongGiaoDich $ketNoi 'Lý do rỗng bị chặn' ($chenTon.Replace("N'Kiểm thử tự hoàn tác'","N'   '")) $true
    ThuTrongGiaoDich $ketNoi 'Lý do NULL bị chặn' ($chenTon.Replace("N'Kiểm thử tự hoàn tác'",'NULL')) $true
    ThuTrongGiaoDich $ketNoi 'Người điều chỉnh không tồn tại bị chặn' ($chenTon.Replace("'$maNV'","'NV9999999'")) $true
    ThuTrongGiaoDich $ketNoi 'Mã tồn sai định dạng bị chặn' ($chenTon.Replace("'$maDC'","'DCx'")) $true
    ThuTrongGiaoDich $ketNoi 'Lưu lịch sử giảm giá' $chenGia
    ThuTrongGiaoDich $ketNoi 'Giá không đổi bị chặn' ($chenGia.Replace(',125000,90000,',',125000,125000,')) $true
    ThuTrongGiaoDich $ketNoi 'Giá bán bằng 0 bị chặn' ($chenGia.Replace(',125000,90000,',',125000,0,')) $true
    ThuTrongGiaoDich $ketNoi 'Lý do đổi giá rỗng bị chặn' ($chenGia.Replace("N'Kiểm thử tự hoàn tác'","N'   '")) $true
    ThuTrongGiaoDich $ketNoi 'Phụ tùng giá bán thấp hơn giá nhập vẫn hợp lệ' ("UPDATE dbo.PhuTung SET DonGiaBan=1 WHERE MaPhuTung='$maPT';")
    DoiChieuDauVanTay $truocThu (LayDauVanTay $ketNoi)
    Write-Output 'PASS: mọi bản ghi kiểm thử đã hoàn tác, dấu vân tay toàn bộ dữ liệu giữ nguyên.'
}

$kn=MoSql $Database
$giaoDich=$null
$daKhoa=$false
$tepSaoLuu=$null
$tepChupDayDu=$null
$tepBaoCao=$null
try{
    if((DocSql $kn 'SELECT DB_NAME() AS Ten').Rows[0].Ten -cne $Database){throw 'Kết nối không đúng database được chọn.'}
    $tenBangTruoc=KiemTraCauTruc $kn $false
    $taiKhoanTruoc=LayTaiKhoanGiuNguyen $kn
    Write-Output ('Database: '+$Database+'; '+$tenBangTruoc.Count+' bảng; giữ mọi nhân viên hiện có, không đặt lại mật khẩu.')
    $taiKhoanTruoc|Format-Table -AutoSize|Out-String|Write-Output
    if(-not $ApDung){
        $dauVanTay=LayDauVanTay $kn
        $dauVanTay.Values|Sort-Object Bang|Format-Table Bang,SoDong,SHA256 -AutoSize|Out-String -Width 180|Write-Output
        if($KiemThu){[void](KiemTraCauTruc $kn $true);KiemThuLichSu $kn}
        else{Write-Output 'Chỉ kiểm tra, chưa cập nhật. Dùng -ApDung để sao lưu, chụp đầy đủ SQL và áp dụng migration additive.'}
        return
    }
    $thuMuc=[IO.Path]::GetFullPath($ThuMucSaoLuu)
    [void](New-Item -ItemType Directory -Path $thuMuc -Force)
    $thoiDiem=Get-Date -Format 'yyyyMMdd_HHmmss_fff'
    $tenGoc=$Database+'_TruocCapNhatKho_'+$thoiDiem
    $tepSaoLuu=Join-Path $thuMuc ($tenGoc+'.bak')
    $tepChupDayDu=Join-Path $thuMuc ($tenGoc+'.sql')
    $tepBaoCao=Join-Path $thuMuc ($tenGoc+'.json')
    $khoa=(DocSql $kn "DECLARE @k INT;EXEC @k=sys.sp_getapplock @Resource=N'CapNhatKho_20261029',@LockMode='Exclusive',@LockOwner='Session',@LockTimeout=10000;SELECT @k AS KetQua;").Rows[0].KetQua
    if($khoa -lt 0){throw 'Một phiên cập nhật kho khác đang chạy. Chưa thay đổi dữ liệu.'}
    $daKhoa=$true
    $duongDanSql=$tepSaoLuu.Replace("'","''")
    ChaySql $kn ("BACKUP DATABASE "+(BaoTen $Database)+" TO DISK=N'$duongDanSql' WITH COPY_ONLY,CHECKSUM,INIT;")
    ChaySql $kn "RESTORE VERIFYONLY FROM DISK=N'$duongDanSql' WITH CHECKSUM;"
    Write-Output ('Đã sao lưu và VERIFYONLY: '+$tepSaoLuu)
    $giaoDich=$kn.BeginTransaction([Data.IsolationLevel]::Serializable)
    foreach($ten in ($tenBangTruoc|Sort-Object)){
        [void](DocSql $kn ('SELECT COUNT_BIG(*) AS So FROM dbo.'+(BaoTen $ten)+' WITH(TABLOCKX,HOLDLOCK);') $giaoDich)
    }
    $truoc=LayDauVanTay $kn $giaoDich
    $cotTruoc=LayCauTrucCotCu $kn $giaoDich
    $taiKhoanTruoc=LayTaiKhoanGiuNguyen $kn $giaoDich
    & (Join-Path $PSScriptRoot 'XuatSqlDayDu.ps1') -Database $Database -File $tepChupDayDu -ExportName $Database -KetNoiHienCo $kn -GiaoDichHienCo $giaoDich
    [pscustomobject]@{Database=$Database;TrangThai='DaSaoLuuChuaCapNhat';SaoLuu=$tepSaoLuu;SqlDayDu=$tepChupDayDu;DuLieuTruoc=@($truoc.Values|Sort-Object Bang);TaiKhoan=$taiKhoanTruoc}|ConvertTo-Json -Depth 6|Set-Content -Encoding UTF8 -LiteralPath $tepBaoCao
    ChaySql $kn ([IO.File]::ReadAllText($tepCapNhat)) $giaoDich
    $sau=LayDauVanTay $kn $giaoDich
    DoiChieuDauVanTay $truoc $sau
    if($cotTruoc -cne (LayCauTrucCotCu $kn $giaoDich)){throw 'Cột của một trong 18 bảng cũ thay đổi.'}
    $taiKhoanSau=LayTaiKhoanGiuNguyen $kn $giaoDich
    if(($taiKhoanTruoc|ConvertTo-Json -Depth 4 -Compress) -cne ($taiKhoanSau|ConvertTo-Json -Depth 4 -Compress)){throw 'Thông tin năm tài khoản cần giữ đã thay đổi.'}
    [void](KiemTraCauTruc $kn $true $giaoDich)
    ChaySql $kn ([IO.File]::ReadAllText($tepCapNhat)) $giaoDich
    DoiChieuDauVanTay $sau (LayDauVanTay $kn $giaoDich)
    $giaoDich.Commit();$giaoDich.Dispose();$giaoDich=$null
    [pscustomobject]@{Database=$Database;TrangThai='ThanhCong';SaoLuu=$tepSaoLuu;SqlDayDu=$tepChupDayDu;DuLieuTruoc=@($truoc.Values|Sort-Object Bang);DuLieuSau=@($sau.Values|Sort-Object Bang);TaiKhoan=$taiKhoanSau;GiuNguyenDuLieu=$true;ChayHaiLanThanhCong=$true}|ConvertTo-Json -Depth 6|Set-Content -Encoding UTF8 -LiteralPath $tepBaoCao
    Write-Output ('PASS: cập nhật 20 bảng, computed NOT NULL; chạy migration hai lần; giữ nguyên SHA mọi bảng cũ và mọi tài khoản. Báo cáo: '+$tepBaoCao)
    if($KiemThu){KiemThuLichSu $kn}
}
catch{
    if($giaoDich){try{if($giaoDich.Connection){$giaoDich.Rollback()}}catch{};$giaoDich.Dispose();$giaoDich=$null}
    if($tepBaoCao){Write-Output ('Không hoàn tất. Giữ bản sao lưu và SQL đầy đủ để đối chiếu: '+$tepSaoLuu+'; '+$tepChupDayDu)}
    throw
}
finally{
    if($daKhoa -and $kn.State -eq [Data.ConnectionState]::Open){try{ChaySql $kn "EXEC sys.sp_releaseapplock @Resource=N'CapNhatKho_20261029',@LockOwner='Session';"}catch{}}
    $kn.Dispose()
}
