$ErrorActionPreference = 'Stop'
Add-Type -AssemblyName System.Data
$root = Split-Path -Parent $PSScriptRoot
$builder = New-Object System.Data.SqlClient.SqlConnectionStringBuilder 'Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=QuanLySuaXe;Integrated Security=True;Connect Timeout=5;TrustServerCertificate=True'
$connection = New-Object System.Data.SqlClient.SqlConnection $builder.ConnectionString
try { $connection.Open() }
catch {
    $connection.Dispose()
    $log = Join-Path $env:LOCALAPPDATA 'Microsoft/Microsoft SQL Server Local DB/Instances/MSSQLLocalDB/error.log'
    $stream = [IO.File]::Open($log, [IO.FileMode]::Open, [IO.FileAccess]::Read, [IO.FileShare]::ReadWrite)
    $reader = New-Object IO.StreamReader $stream
    try { $content = $reader.ReadToEnd() } finally { $reader.Dispose() }
    $pipes = [regex]::Matches($content, '\\\\\.\\pipe\\LOCALDB#[A-Fa-f0-9]+\\tsql\\query')
    if ($pipes.Count -eq 0) { throw 'Không tìm thấy kết nối LocalDB.' }
    $builder.DataSource = 'np:' + $pipes[$pipes.Count - 1].Value
    $connection = New-Object System.Data.SqlClient.SqlConnection $builder.ConnectionString
    $connection.Open()
}
try {
    $command = $connection.CreateCommand()
    $command.CommandText = "SELECT DB_NAME(),COUNT(*),SUM(TongTien) FROM dbo.HoaDon"
    $reader = $command.ExecuteReader()
    try { [void]$reader.Read(); if ($reader.GetString(0) -ne 'QuanLySuaXe') { throw 'Sai database.' }; $beforeCount = $reader.GetInt32(1); $beforeTotal = $reader.GetValue(2) } finally { $reader.Close() }
    $folder = Join-Path $PSScriptRoot 'output/backups'
    [void](New-Item -ItemType Directory -Path $folder -Force)
    $backup = Join-Path $folder ('QuanLySuaXe_TruocTachTaiKhoan_' + (Get-Date -Format 'yyyyMMdd_HHmmss') + '.bak')
    $command.CommandTimeout = 60
    $command.CommandText = 'BACKUP DATABASE [QuanLySuaXe] TO DISK=@path WITH COPY_ONLY,CHECKSUM'
    [void]$command.Parameters.AddWithValue('@path', $backup)
    [void]$command.ExecuteNonQuery()
    $command.CommandText = 'RESTORE VERIFYONLY FROM DISK=@path WITH CHECKSUM'
    [void]$command.ExecuteNonQuery()
    $command.Parameters.Clear()
    $command.CommandText = "SET XACT_ABORT ON; BEGIN TRANSACTION;
        IF COL_LENGTH('dbo.NhanVien','DaCapTaiKhoan') IS NULL ALTER TABLE dbo.NhanVien ADD DaCapTaiKhoan BIT NOT NULL CONSTRAINT DF_NV_DaCapTaiKhoan DEFAULT 1;
        IF COL_LENGTH('dbo.NhanVien','TaiKhoanHoatDong') IS NULL ALTER TABLE dbo.NhanVien ADD TaiKhoanHoatDong BIT NOT NULL CONSTRAINT DF_NV_TaiKhoanHoatDong DEFAULT 1;
        COMMIT;"
    [void]$command.ExecuteNonQuery()
    $command.CommandText = 'SELECT COUNT(*),SUM(TongTien) FROM dbo.HoaDon'
    $reader = $command.ExecuteReader()
    try { [void]$reader.Read(); if ($reader.GetInt32(0) -ne $beforeCount -or $reader.GetValue(1) -ne $beforeTotal) { throw 'Số liệu hóa đơn thay đổi.' } } finally { $reader.Close() }
    $command.Dispose()
    Write-Output 'PASS: Đã sao lưu, kiểm tra backup và thêm hai cột trạng thái tài khoản. Chứng từ giữ nguyên.'
} finally { $connection.Dispose() }
