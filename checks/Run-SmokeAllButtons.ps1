param([ValidateSet('Debug','Release')][string]$Configuration='Debug',[switch]$ListOnly)
$ErrorActionPreference='Stop'
$goc=Split-Path -Parent $PSScriptRoot
$dauRa=Join-Path $goc "QLyCuaHangSuaXe/QLyCuaHangSuaXe/bin/$Configuration"
$assembly=Join-Path $dauRa 'QLyCuaHangSuaXe.exe'
if(-not (Test-Path -LiteralPath $assembly)){throw 'Cần build ứng dụng trước khi mở runner kiểm thử.'}
$thuMuc=Join-Path $PSScriptRoot 'output/smoke-all-buttons'
[void](New-Item -ItemType Directory -Path $thuMuc -Force)
foreach($tep in @((Get-Item -LiteralPath $assembly)) + @(Get-ChildItem -LiteralPath $dauRa -Filter '*.dll' -File)){
    Copy-Item -LiteralPath $tep.FullName -Destination (Join-Path $thuMuc $tep.Name) -Force
}
$chuongTrinh=Join-Path $thuMuc 'SmokeAllButtons.exe'
$bienDich=Join-Path $env:WINDIR 'Microsoft.NET/Framework64/v4.0.30319/csc.exe'
& $bienDich /nologo /target:exe "/out:$chuongTrinh" "/reference:$assembly" /reference:System.dll /reference:System.Core.dll /reference:System.Data.dll /reference:System.Data.DataSetExtensions.dll /reference:System.Drawing.dll /reference:System.Windows.Forms.dll /reference:System.Web.Extensions.dll (Join-Path $PSScriptRoot 'SmokeAllButtons.cs')
if($LASTEXITCODE -ne 0){throw 'Không biên dịch được runner kiểm thử nút.'}
[xml]$cauHinh=Get-Content -Raw -LiteralPath (Join-Path $dauRa 'QLyCuaHangSuaXe.exe.config')
$ketNoi=New-Object System.Data.SqlClient.SqlConnectionStringBuilder ([string]$cauHinh.configuration.connectionStrings.add.connectionString)
$ketNoi['Initial Catalog']='QuanLySuaXe_CodexChecks'
$cauHinh.configuration.connectionStrings.add.connectionString=$ketNoi.ConnectionString
$cauHinh.Save("$chuongTrinh.config")
Push-Location $goc
try{
    if($ListOnly){& $chuongTrinh --list}else{& $chuongTrinh}
    if($LASTEXITCODE -ne 0){throw 'Runner kiểm thử kết thúc có lỗi.'}
}finally{Pop-Location}
