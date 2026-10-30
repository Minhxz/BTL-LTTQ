param([ValidateSet('Debug','Release')][string]$Configuration='Debug')
$ErrorActionPreference='Stop'
$goc=Split-Path -Parent $PSScriptRoot
$dauRa=Join-Path $goc "QLyCuaHangSuaXe/QLyCuaHangSuaXe/bin/$Configuration"
$assembly=Join-Path $dauRa 'QLyCuaHangSuaXe.exe'
$thuMuc=Join-Path (Split-Path -Parent $goc) 'TichHopKho_QA/ThongKe_20261029'
[void](New-Item -ItemType Directory -Path $thuMuc -Force)
foreach($tep in @((Get-Item -LiteralPath $assembly)) + @(Get-ChildItem -LiteralPath $dauRa -Filter '*.dll' -File)){
    Copy-Item -LiteralPath $tep.FullName -Destination (Join-Path $thuMuc $tep.Name) -Force
}
$chuongTrinh=Join-Path $thuMuc 'VerifyThongKeGiaoDien.exe'
$bienDich=Join-Path $env:WINDIR 'Microsoft.NET/Framework64/v4.0.30319/csc.exe'
$bieuDo=Join-Path ${env:ProgramFiles(x86)} 'Reference Assemblies/Microsoft/Framework/.NETFramework/v4.8/System.Windows.Forms.DataVisualization.dll'
& $bienDich /nologo /target:exe "/out:$chuongTrinh" "/reference:$assembly" "/reference:$bieuDo" /reference:System.dll /reference:System.Core.dll /reference:System.Data.dll /reference:System.Data.DataSetExtensions.dll /reference:System.Drawing.dll /reference:System.Windows.Forms.dll (Join-Path $PSScriptRoot 'VerifyThongKeGiaoDien.cs')
if($LASTEXITCODE -ne 0){throw 'Không biên dịch được kiểm tra thống kê.'}
Copy-Item -LiteralPath (Join-Path $dauRa 'QLyCuaHangSuaXe.exe.config') -Destination "$chuongTrinh.config" -Force
Push-Location $goc
try{& $chuongTrinh $thuMuc;if($LASTEXITCODE -ne 0){throw 'Kiểm tra thống kê chưa đạt.'}}finally{Pop-Location}
