param(
    [ValidateSet('QuanLySuaXe','QuanLySuaXe_CodexChecks','QuanLySuaXe_CodexInstall','QuanLySuaXe_CodexUpgrade')]
    [string]$Database='QuanLySuaXe_CodexUpgrade',
    [switch]$ThayThe
)
$ErrorActionPreference='Stop'
. (Join-Path $PSScriptRoot 'SqlChung.ps1')
$goc=Split-Path $PSScriptRoot -Parent
$nguon=Join-Path $goc 'database/nguon/QuanLySuaXe.sql'
$noiDung=[IO.File]::ReadAllText($nguon)
$noiDung=[regex]::Replace($noiDung,'(?s)/\*.*?\*/','')
$noiDung=[regex]::Replace($noiDung,'(?m)--[^\r\n]*','')
$noiDung=[regex]::Replace($noiDung,"'((?:CV|CA|KDC|DV|NCC))([0-9]{2})'", {param($m) "'"+$m.Groups[1].Value+([int]$m.Groups[2].Value).ToString('D3')+"'"})
$batDau=$noiDung.IndexOf('CREATE TABLE ChucVu')
$ketThuc=$noiDung.IndexOf('SELECT t.name AS Bang')
if($batDau -lt 0 -or $ketThuc -le $batDau){throw 'Không nhận dạng được cấu trúc SQL mới.'}
$noiDung=$noiDung.Substring($batDau,$ketThuc-$batDau)
$chu=MoSql master
try {
    $tonTai=(DocSql $chu "SELECT DB_ID(N'$Database') AS Ma").Rows[0].Ma -isnot [DBNull]
    if($tonTai -and !$ThayThe){throw "Database $Database đã tồn tại. Chỉ dùng -ThayThe khi đã chủ động chọn thay dữ liệu."}
    if($tonTai -and $Database -eq 'QuanLySuaXe'){
        $thuMuc=Join-Path $PSScriptRoot 'output/backups'
        [void](New-Item -ItemType Directory -Path $thuMuc -Force)
        $banSao=Join-Path $thuMuc ('QuanLySuaXe_TruocSQLMoi_'+(Get-Date -Format 'yyyyMMdd_HHmmss')+'.bak')
        $tep=$banSao.Replace("'","''")
        ChaySql $chu "BACKUP DATABASE [$Database] TO DISK=N'$tep' WITH COPY_ONLY,CHECKSUM,INIT;"
        ChaySql $chu "RESTORE VERIFYONLY FROM DISK=N'$tep' WITH CHECKSUM;"
        Write-Output "Đã sao lưu và xác minh: $banSao"
    }
    if($tonTai){ChaySql $chu "ALTER DATABASE [$Database] SET SINGLE_USER WITH ROLLBACK IMMEDIATE; DROP DATABASE [$Database];"}
    ChaySql $chu "CREATE DATABASE [$Database] COLLATE Vietnamese_CI_AS;"
    $chu.ChangeDatabase($Database)
    ChaySql $chu 'SET XACT_ABORT ON;'
    foreach($doan in [regex]::Split($noiDung,'(?im)^\s*GO\s*\r?$')){if($doan.Trim()){ChaySql $chu $doan}}
    ChayTepSql $chu (Join-Path $PSScriptRoot 'NamNhanVien.sql')
    ChayTepSql $chu (Join-Path $PSScriptRoot 'SauChucVu.sql')
    $soBang=(DocSql $chu 'SELECT COUNT(*) AS So FROM sys.tables').Rows[0].So
    if($soBang -ne 18){throw "Số bảng không đúng SQL mới: $soBang"}
    $nullable=DocSql $chu 'SELECT t.name,c.name FROM sys.tables t JOIN sys.columns c ON c.object_id=t.object_id WHERE c.is_nullable=1'
    if($nullable.Rows.Count){throw 'Còn cột cho phép NULL.'}
    $ketQua=DocSql $chu 'SELECT t.name AS Bang,SUM(p.rows) AS SoDong FROM sys.tables t JOIN sys.partitions p ON p.object_id=t.object_id AND p.index_id IN(0,1) GROUP BY t.name ORDER BY t.name'
    $ketQua | Format-Table -AutoSize | Out-String | Write-Output
    Write-Output "PASS: $Database dùng 18 bảng mới, dữ liệu đầy đủ, không có cột NULL."
} finally {$chu.Dispose()}
