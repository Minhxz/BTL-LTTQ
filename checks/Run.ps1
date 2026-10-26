param([ValidateSet('Debug','Release')][string]$Configuration='Debug',[switch]$SkipBuild,[switch]$Integration,[switch]$Designer,[switch]$ReadOnly,[switch]$Performance,[switch]$Accounts)
$ErrorActionPreference='Stop'
$root=Split-Path -Parent $PSScriptRoot
$project=Join-Path $root 'QLyCuaHangSuaXe/QLyCuaHangSuaXe/QLyCuaHangSuaXe.csproj'
$output=Join-Path $root "QLyCuaHangSuaXe/QLyCuaHangSuaXe/bin/$Configuration"
if(-not $SkipBuild){
    $vswhere=Join-Path ${env:ProgramFiles(x86)} 'Microsoft Visual Studio/Installer/vswhere.exe'
    $msbuild=& $vswhere -latest -products '*' -requires Microsoft.Component.MSBuild -find 'MSBuild\**\Bin\MSBuild.exe'|Select-Object -First 1
    if(-not $msbuild){throw 'MSBuild was not found.'}
    foreach($path in @($project,(Join-Path $root 'DanhMuc/DanhMuc/DanhMuc.csproj'),(Join-Path $root 'Kho/Kho/btl/btl.csproj'))){
        & $msbuild $path /t:Build "/p:Configuration=$Configuration" /verbosity:minimal /nologo /p:UseSharedCompilation=false /nr:false
        if($LASTEXITCODE -ne 0){throw "Build failed: $path"}
    }
}
$assembly=Join-Path $output 'QLyCuaHangSuaXe.exe'
$compiler=Join-Path $env:WINDIR 'Microsoft.NET/Framework64/v4.0.30319/csc.exe'
$source=if($Accounts){'VerifyTaiKhoan'}elseif($Integration){'VerifyNghiepVu'}elseif($Designer){'VerifyDesigner'}elseif($ReadOnly){'VerifyDatabaseReadOnly'}elseif($Performance){'VerifyLoginPerformance'}else{'VerifyDangNhap'}
$testOutput=Join-Path $PSScriptRoot "output/kiemthu/$Configuration"
[void](New-Item -ItemType Directory -Path $testOutput -Force)
foreach($name in @('QLyCuaHangSuaXe.exe','QLyCuaHangSuaXe.exe.config','QLyCuaHangSuaXe.pdb')){
    $file=Join-Path $output $name
    if(Test-Path -LiteralPath $file){Copy-Item -LiteralPath $file -Destination (Join-Path $testOutput $name) -Force}
}
$verification=Join-Path $testOutput "$source.exe"
& $compiler /nologo /target:exe "/out:$verification" "/reference:$assembly" /reference:System.dll /reference:System.Core.dll /reference:System.Data.dll /reference:System.Data.DataSetExtensions.dll /reference:System.Drawing.dll /reference:System.Windows.Forms.dll /reference:System.Design.dll /reference:System.IO.Compression.dll /reference:System.IO.Compression.FileSystem.dll (Join-Path $PSScriptRoot "$source.cs")
if($LASTEXITCODE -ne 0){throw 'Verification compile failed.'}
Copy-Item -LiteralPath (Join-Path $output 'QLyCuaHangSuaXe.exe.config') -Destination "$verification.config" -Force
if($Integration -or $Accounts){
    & sqllocaldb start CodexQuanLySuaXeTest
    if($LASTEXITCODE -ne 0){& sqllocaldb create CodexQuanLySuaXeTest -s;if($LASTEXITCODE -ne 0){throw 'Could not create dedicated test LocalDB.'}}
    & (Join-Path $PSScriptRoot 'Prepare-TestDatabase.ps1')
    $config=Get-Content -Raw -LiteralPath (Join-Path $root 'QLyCuaHangSuaXe/QLyCuaHangSuaXe/App.config')
    $config.Replace('Data Source=(localdb)\MSSQLLocalDB','Data Source=(localdb)\CodexQuanLySuaXeTest').Replace('Initial Catalog=QuanLySuaXe;','Initial Catalog=QuanLySuaXe_CodexChecks;')|Set-Content -Encoding utf8 -LiteralPath "$verification.config"
}
Push-Location $root
try{& $verification;if($LASTEXITCODE -ne 0){throw 'Verification failed.'}}finally{Pop-Location}
