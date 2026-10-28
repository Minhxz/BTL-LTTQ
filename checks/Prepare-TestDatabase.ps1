param([ValidateSet('CodexQuanLySuaXeTest')][string]$Instance='CodexQuanLySuaXeTest')
$ErrorActionPreference='Stop'
. (Join-Path $PSScriptRoot 'SqlChung.ps1')
$root=Split-Path $PSScriptRoot -Parent
& (Join-Path $PSScriptRoot 'CaiDatabaseMoi.ps1') -Database QuanLySuaXe_CodexChecks -ThayThe
