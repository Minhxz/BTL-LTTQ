param([ValidateSet('CodexQuanLySuaXeTest')][string]$Instance='CodexQuanLySuaXeTest')
$ErrorActionPreference='Stop'
. (Join-Path $PSScriptRoot 'SqlChung.ps1')
$root=Split-Path $PSScriptRoot -Parent
& (Join-Path $PSScriptRoot 'CaiDatabaseMoi.ps1') -Database QuanLySuaXe_CodexChecks -ThayThe
$ketNoi=MoSql 'QuanLySuaXe_CodexChecks'
try {ChayTepSql $ketNoi (Join-Path $root 'database/CapNhatKho_20261029.sql')} finally {$ketNoi.Dispose()}
