param(
    [ValidateSet('Debug', 'Release')]
    [string]$Configuration = 'Debug',
    [switch]$SkipBuild
)

$ErrorActionPreference = 'Stop'
$stageRoot = Split-Path -Parent $PSScriptRoot
$project = Join-Path $stageRoot 'QLyCuaHangSuaXe\QLyCuaHangSuaXe\QLyCuaHangSuaXe.csproj'
$output = Join-Path $stageRoot ('QLyCuaHangSuaXe\QLyCuaHangSuaXe\bin\' + $Configuration)

if (-not $SkipBuild) {
    $vswhere = Join-Path ${env:ProgramFiles(x86)} 'Microsoft Visual Studio\Installer\vswhere.exe'
    $msbuild = & $vswhere -latest -products '*' -requires Microsoft.Component.MSBuild -find 'MSBuild\**\Bin\MSBuild.exe' | Select-Object -First 1
    if (-not $msbuild) { throw 'MSBuild from Visual Studio was not found.' }
    & $msbuild $project /t:Build "/p:Configuration=$Configuration" /verbosity:minimal /nologo
    if ($LASTEXITCODE -ne 0) { throw "Application build failed (exit $LASTEXITCODE)." }
}

$assembly = Join-Path $output 'QLyCuaHangSuaXe.exe'
if (-not (Test-Path -LiteralPath $assembly)) { throw "Build output does not exist: $assembly" }
$compiler = Join-Path $env:WINDIR 'Microsoft.NET\Framework64\v4.0.30319\csc.exe'
$verification = Join-Path $output 'VerifyShared.exe'
& $compiler /nologo /target:exe "/out:$verification" "/reference:$assembly" /reference:System.dll /reference:System.Core.dll /reference:System.Data.dll /reference:System.Drawing.dll /reference:System.Windows.Forms.dll (Join-Path $PSScriptRoot 'Verify.cs')
if ($LASTEXITCODE -ne 0) { throw "Verification compile failed (exit $LASTEXITCODE)." }
& $verification
if ($LASTEXITCODE -ne 0) { throw "Verification failed (exit $LASTEXITCODE)." }
