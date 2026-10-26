$ErrorActionPreference = 'Stop'
$ssms = Join-Path $env:ProgramFiles 'Microsoft SQL Server Management Studio 22/Release/Common7/IDE/SSMS.exe'
if (-not (Test-Path -LiteralPath $ssms)) { throw 'Không tìm thấy SQL Server Management Studio 22.' }
& sqllocaldb start MSSQLLocalDB
if ($LASTEXITCODE -ne 0) { throw 'Không khởi động được MSSQLLocalDB.' }
$script = Join-Path $PSScriptRoot 'database/KiemTraKetNoi.sql'
& $ssms $script -S '(localdb)\MSSQLLocalDB' -d QuanLySuaXe -nosplash
