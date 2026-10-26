$ErrorActionPreference = 'Stop'
Add-Type -AssemblyName System.Data
$ketNoi = New-Object System.Data.SqlClient.SqlConnection 'Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=QuanLySuaXe;Integrated Security=True;Connect Timeout=10;TrustServerCertificate=True'
$giaoDich = $null
function DocBang([string]$cauLenh, $thamSo = @{}) {
    $lenh = $ketNoi.CreateCommand()
    if ($giaoDich) { $lenh.Transaction = $giaoDich }
    $lenh.CommandText = $cauLenh
    foreach ($ten in $thamSo.Keys) { [void]$lenh.Parameters.AddWithValue($ten, $thamSo[$ten]) }
    $boNap = New-Object System.Data.SqlClient.SqlDataAdapter $lenh
    $bang = New-Object System.Data.DataTable
    try { [void]$boNap.Fill($bang); return ,$bang } finally { $boNap.Dispose(); $lenh.Dispose() }
}
function ThucThi([string]$cauLenh, $thamSo = @{}) {
    $lenh = $ketNoi.CreateCommand()
    if ($giaoDich) { $lenh.Transaction = $giaoDich }
    $lenh.CommandTimeout = 60
    $lenh.CommandText = $cauLenh
    foreach ($ten in $thamSo.Keys) { [void]$lenh.Parameters.AddWithValue($ten, $thamSo[$ten]) }
    try { return $lenh.ExecuteNonQuery() } finally { $lenh.Dispose() }
}
try {
    $ketNoi.Open()
    if ($ketNoi.Database -ne 'QuanLySuaXe') { throw 'Sai cơ sở dữ liệu.' }
    $cacDoiTuong = @(
        @{Ma='NV001';TaiKhoan='minh';VaiTroCu='Chủ cửa hàng';Ten='Minh';VaiTro='Chủ/Quản lý';GioiHan=0},
        @{Ma='NV002';TaiKhoan='mau_thungan';VaiTroCu='Thu ngân';Ten='Mai';VaiTro='Thu ngân';GioiHan=0},
        @{Ma='NV003';TaiKhoan='mau_kho';VaiTroCu='Kho';Ten='An';VaiTro='Thợ chính';GioiHan=3},
        @{Ma='NV004';TaiKhoan='mau_tho';VaiTroCu='Thợ';Ten='Huy';VaiTro='Thợ';GioiHan=2}
    )
    $truoc = DocBang "SELECT n.*,v.TenChucVu FROM dbo.NhanVien n JOIN dbo.ChucVu v ON v.MaChucVu=n.MaChucVu"
    if ($truoc.Rows.Count -ne 4) { throw 'Danh sách không còn đúng 4 nhân viên. Dừng cập nhật.' }
    foreach ($doiTuong in $cacDoiTuong) {
        $dong = @($truoc.Rows | Where-Object { $_.MaNhanVien -eq $doiTuong.Ma })
        if ($dong.Count -ne 1 -or $dong[0].TenDangNhap -ne $doiTuong.TaiKhoan -or $dong[0].TenChucVu -notin @($doiTuong.VaiTroCu, $doiTuong.VaiTro)) {
            throw "Hồ sơ $($doiTuong.Ma) đã khác dự kiến. Dừng cập nhật."
        }
    }
    $thuMucSaoLuu = Join-Path $PSScriptRoot 'output/backups'
    [void](New-Item -ItemType Directory -Path $thuMucSaoLuu -Force)
    $tepSaoLuu = Join-Path $thuMucSaoLuu ('QuanLySuaXe_TruocDoiTenNhanVien_' + (Get-Date -Format 'yyyyMMdd_HHmmss') + '.bak')
    [void](ThucThi 'BACKUP DATABASE [QuanLySuaXe] TO DISK=@tep WITH COPY_ONLY,CHECKSUM' @{'@tep'=$tepSaoLuu})
    [void](ThucThi 'RESTORE VERIFYONLY FROM DISK=@tep WITH CHECKSUM' @{'@tep'=$tepSaoLuu})
    $giaoDich = $ketNoi.BeginTransaction([System.Data.IsolationLevel]::Serializable)
    $truoc = DocBang "SELECT n.*,v.TenChucVu FROM dbo.NhanVien n WITH(UPDLOCK,HOLDLOCK) JOIN dbo.ChucVu v ON v.MaChucVu=n.MaChucVu"
    if ($truoc.Rows.Count -ne 4) { throw 'Danh sách nhân viên vừa thay đổi. Dừng cập nhật.' }
    $hoaDonTruoc = (DocBang 'SELECT COUNT(*) AS SoHoaDon,SUM(TongTien) AS TongTien FROM dbo.HoaDon').Rows[0]
    foreach ($doiTuong in $cacDoiTuong) {
        $dong = @($truoc.Rows | Where-Object { $_.MaNhanVien -eq $doiTuong.Ma })
        if ($dong.Count -ne 1 -or $dong[0].TenDangNhap -ne $doiTuong.TaiKhoan -or $dong[0].TenChucVu -notin @($doiTuong.VaiTroCu, $doiTuong.VaiTro)) {
            throw "Hồ sơ $($doiTuong.Ma) vừa thay đổi. Dừng cập nhật."
        }
        $vaiTro = DocBang 'SELECT MaChucVu FROM dbo.ChucVu WHERE TenChucVu=@ten' @{'@ten'=$doiTuong.VaiTro}
        if ($vaiTro.Rows.Count -ne 1) { throw "Không xác định được chức vụ $($doiTuong.VaiTro)." }
        $congViec = DocBang "SELECT COUNT(*) AS SoXe FROM dbo.PhanCongTho WITH(UPDLOCK,HOLDLOCK) WHERE MaNhanVien=@id AND TrangThaiCongViec NOT IN(N'Hoàn thành',N'Đã hủy')" @{'@id'=$doiTuong.Ma}
        if ([int]$congViec.Rows[0].SoXe -gt $doiTuong.GioiHan) { throw "$($doiTuong.Ten) đang nhận nhiều xe hơn giới hạn mới." }
        $soDong = ThucThi 'UPDATE dbo.NhanVien SET TenNhanVien=@ten,MaChucVu=@vaiTro,SoXeDangSuaToiDa=@gioiHan WHERE MaNhanVien=@id' @{'@id'=$doiTuong.Ma;'@ten'=$doiTuong.Ten;'@vaiTro'=$vaiTro.Rows[0].MaChucVu;'@gioiHan'=$doiTuong.GioiHan}
        if ($soDong -ne 1) { throw "Không cập nhật đúng một hồ sơ $($doiTuong.Ma)." }
    }
    $sau = DocBang 'SELECT n.*,v.TenChucVu FROM dbo.NhanVien n JOIN dbo.ChucVu v ON v.MaChucVu=n.MaChucVu'
    if ($sau.Rows.Count -ne $truoc.Rows.Count) { throw 'Số nhân viên thay đổi.' }
    foreach ($dongCu in $truoc.Rows) {
        $dongMoi = @($sau.Rows | Where-Object { $_.MaNhanVien -eq $dongCu.MaNhanVien })[0]
        foreach ($cot in $truoc.Columns) {
            if ($cot.ColumnName -in @('TenNhanVien','MaChucVu','TenChucVu','SoXeDangSuaToiDa')) { continue }
            if (-not [object]::Equals($dongCu[$cot.ColumnName], $dongMoi[$cot.ColumnName])) { throw "Dữ liệu $($cot.ColumnName) của $($dongCu.MaNhanVien) bị thay đổi ngoài yêu cầu." }
        }
    }
    $hoaDonSau = (DocBang 'SELECT COUNT(*) AS SoHoaDon,SUM(TongTien) AS TongTien FROM dbo.HoaDon').Rows[0]
    if ($hoaDonTruoc.SoHoaDon -ne $hoaDonSau.SoHoaDon -or $hoaDonTruoc.TongTien -ne $hoaDonSau.TongTien) { throw 'Số liệu hóa đơn thay đổi.' }
    $giaoDich.Commit()
    $giaoDich.Dispose()
    $giaoDich = $null
    $sau | Select-Object MaNhanVien,TenNhanVien,TenDangNhap,TenChucVu,SoXeDangSuaToiDa | Format-Table -AutoSize
    Write-Output "PASS: Giữ nguyên 4 mã nhân viên, tên đăng nhập, mật khẩu và toàn bộ dữ liệu khác. Hóa đơn: $($hoaDonSau.SoHoaDon); tổng: $($hoaDonSau.TongTien)."
    Write-Output "BackupDaKiemTra=$tepSaoLuu"
} catch {
    if ($giaoDich) { $giaoDich.Rollback() }
    throw
} finally {
    if ($giaoDich) { $giaoDich.Dispose() }
    $ketNoi.Dispose()
}
