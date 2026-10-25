param([string]$Instance='CodexQuanLySuaXeTest')
$ErrorActionPreference='Stop'
if($Instance -ne 'CodexQuanLySuaXeTest'){throw 'This script only permits the dedicated test instance.'}
Add-Type -AssemblyName System.Data
$connection=New-Object System.Data.SqlClient.SqlConnection "Data Source=(localdb)\$Instance;Initial Catalog=master;Integrated Security=True;Connect Timeout=15;TrustServerCertificate=True"
try {
    $connection.Open()
    $root=Split-Path -Parent $PSScriptRoot
    $folder=Join-Path $PSScriptRoot 'output/sql'
    [void](New-Item -ItemType Directory -Path $folder -Force)
    $command=$connection.CreateCommand()
    $mdf=(Join-Path $folder 'QuanLySuaXe_CodexChecks.mdf').Replace("'","''")
    $ldf=(Join-Path $folder 'QuanLySuaXe_CodexChecks_log.ldf').Replace("'","''")
    $command.CommandText="IF DB_ID('QuanLySuaXe_CodexChecks') IS NULL CREATE DATABASE QuanLySuaXe_CodexChecks ON PRIMARY(NAME=Checks_Data,FILENAME=N'$mdf') LOG ON(NAME=Checks_Log,FILENAME=N'$ldf')"
    [void]$command.ExecuteNonQuery();$command.Dispose()
    foreach($name in @('QuanLySuaXe.sql','QuanLySuaXe_FakeData.sql','QuanLySuaXe_NghiepVu.sql','QuanLySuaXe_NghiepVu.sql')) {
        $source=Get-Content -Raw -LiteralPath (Join-Path $root "database/$name")
        $source=$source.Replace('QuanLySuaXe','QuanLySuaXe_CodexChecks')
        foreach($batch in [regex]::Split($source,'(?im)^\s*GO\s*\r?$')) {
            if([string]::IsNullOrWhiteSpace($batch)){continue}
            $command=$connection.CreateCommand();$command.CommandTimeout=60;$command.CommandText=$batch
            try{[void]$command.ExecuteNonQuery()}finally{$command.Dispose()}
        }
        Write-Output "PASS: $name"
    }
}finally{$connection.Dispose()}
