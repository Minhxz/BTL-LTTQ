param([ValidateSet('Debug','Release')][string]$Configuration='Debug',[switch]$SkipBuild,[switch]$Integration,[switch]$Designer,[switch]$ReadOnly,[switch]$Performance,[switch]$Accounts,[switch]$Flow,[switch]$Sync,[switch]$SqlMoi)
$ErrorActionPreference='Stop'
$root=Split-Path -Parent $PSScriptRoot
$project=Join-Path $root 'QLyCuaHangSuaXe/QLyCuaHangSuaXe/QLyCuaHangSuaXe.csproj'
$output=Join-Path $root "QLyCuaHangSuaXe/QLyCuaHangSuaXe/bin/$Configuration"
if(-not $SkipBuild){
    $vswhere=Join-Path ${env:ProgramFiles(x86)} 'Microsoft Visual Studio/Installer/vswhere.exe'
    $msbuild=& $vswhere -latest -products '*' -requires Microsoft.Component.MSBuild -find 'MSBuild\**\Bin\MSBuild.exe'|Select-Object -First 1
    if(-not $msbuild){throw 'MSBuild was not found.'}
    & $msbuild $project /t:Build "/p:Configuration=$Configuration" /verbosity:minimal /nologo /p:UseSharedCompilation=false /nr:false
    if($LASTEXITCODE -ne 0){throw "Build failed: $project"}
}
$assembly=Join-Path $output 'QLyCuaHangSuaXe.exe'
$compiler=Join-Path $env:WINDIR 'Microsoft.NET/Framework64/v4.0.30319/csc.exe'
$ghiDuLieu=$SqlMoi -or $Integration -or $Accounts -or $Flow -or $Sync
$source=if($ghiDuLieu){'VerifySqlMoi'}elseif($ReadOnly){'VerifyDatabaseReadOnly'}elseif($Performance){'VerifyLoginPerformance'}else{'VerifyDesigner'}
$testOutput=Join-Path $PSScriptRoot "output/kiemthu/$Configuration"
[void](New-Item -ItemType Directory -Path $testOutput -Force)
foreach($name in @('QLyCuaHangSuaXe.exe','QLyCuaHangSuaXe.exe.config','QLyCuaHangSuaXe.pdb','QRCoder.dll')){
    $file=Join-Path $output $name
    if(Test-Path -LiteralPath $file){Copy-Item -LiteralPath $file -Destination (Join-Path $testOutput $name) -Force}
}
$verification=Join-Path $testOutput "$source.exe"
$cacNguon=@((Join-Path $PSScriptRoot "$source.cs"))
if($ghiDuLieu){$cacNguon+=@('VerifyKhoSqlMoi.cs','VerifyNhanSuSqlMoi.cs','VerifyTiepNhanSqlMoi.cs') | ForEach-Object {Join-Path $PSScriptRoot $_}}
& $compiler /nologo /target:exe "/out:$verification" "/reference:$assembly" /reference:System.dll /reference:System.Core.dll /reference:System.Data.dll /reference:System.Data.DataSetExtensions.dll /reference:System.Drawing.dll /reference:System.Windows.Forms.dll /reference:System.Design.dll /reference:System.IO.Compression.dll /reference:System.IO.Compression.FileSystem.dll $cacNguon
if($LASTEXITCODE -ne 0){throw 'Verification compile failed.'}
Copy-Item -LiteralPath (Join-Path $output 'QLyCuaHangSuaXe.exe.config') -Destination "$verification.config" -Force
if($ghiDuLieu){
    & (Join-Path $PSScriptRoot 'Prepare-TestDatabase.ps1')
    . (Join-Path $PSScriptRoot 'SqlChung.ps1')
    $connection=MoSql 'QuanLySuaXe_CodexChecks'
    [xml]$config=Get-Content -Raw -LiteralPath (Join-Path $root 'QLyCuaHangSuaXe/QLyCuaHangSuaXe/App.config')
    $config.configuration.connectionStrings.add.connectionString=$connection.ConnectionString
    $connection.Dispose();$config.Save("$verification.config")
}
Push-Location $root
try{& $verification;if($LASTEXITCODE -ne 0){throw 'Verification failed.'}}finally{Pop-Location}
