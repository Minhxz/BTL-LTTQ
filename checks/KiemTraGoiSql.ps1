param([switch]$XuatLai)
$ErrorActionPreference='Stop'
. (Join-Path $PSScriptRoot 'SqlChung.ps1')
$goc=Split-Path $PSScriptRoot -Parent
$tepSql=Join-Path $goc 'database/QuanLySuaXe_DayDu_20261028.sql'
if($XuatLai){& (Join-Path $PSScriptRoot 'XuatSqlDayDu.ps1') -File 'database/QuanLySuaXe_DayDu_20261028.sql'}
$noiDung=[IO.File]::ReadAllText($tepSql)
if([regex]::Matches($noiDung,'(?m)^INSERT dbo\.').Count -eq 0){throw 'Bản SQL chưa có dữ liệu INSERT.'}
$noiDung=$noiDung.Replace("DB_ID(N'QuanLySuaXe')","DB_ID(N'QuanLySuaXe_CodexInstall')").Replace('CREATE DATABASE [QuanLySuaXe];','CREATE DATABASE [QuanLySuaXe_CodexInstall];').Replace('USE [QuanLySuaXe];','USE [QuanLySuaXe_CodexInstall];')
$tepThu=Join-Path $PSScriptRoot 'output/cai-moi-thu.sql'
[IO.File]::WriteAllText($tepThu,$noiDung,(New-Object Text.UTF8Encoding($true)))
$chu=MoSql master
try {
    ChaySql $chu "IF DB_ID('QuanLySuaXe_CodexInstall') IS NOT NULL BEGIN ALTER DATABASE [QuanLySuaXe_CodexInstall] SET SINGLE_USER WITH ROLLBACK IMMEDIATE; DROP DATABASE [QuanLySuaXe_CodexInstall]; END;"
    ChayTepSql $chu $tepThu
} finally {$chu.Dispose()}
$nguon=MoSql QuanLySuaXe;$banSao=MoSql QuanLySuaXe_CodexInstall
function JsonSql($kn,[string]$sql){return (@((DocSql $kn $sql).Rows|ForEach-Object{$_.Item(0)}) -join '')}
try {
    $cauTruc="SELECT t.name AS Bang,c.name AS Cot,y.name AS Kieu,c.max_length,c.precision,c.scale,c.is_nullable,c.is_identity,c.is_computed,c.collation_name,co.definition AS CongThuc,co.is_persisted,d.definition AS MacDinh FROM sys.tables t JOIN sys.columns c ON c.object_id=t.object_id JOIN sys.types y ON y.user_type_id=c.user_type_id LEFT JOIN sys.computed_columns co ON co.object_id=c.object_id AND co.column_id=c.column_id LEFT JOIN sys.default_constraints d ON d.object_id=c.default_object_id ORDER BY t.name,c.column_id FOR JSON PATH,INCLUDE_NULL_VALUES"
    if((JsonSql $nguon $cauTruc) -cne (JsonSql $banSao $cauTruc)){throw 'Cấu trúc bản cài mới không khớp database thật.'}
    $nullable=DocSql $banSao 'SELECT t.name,c.name FROM sys.tables t JOIN sys.columns c ON c.object_id=t.object_id WHERE c.is_nullable=1'
    if($nullable.Rows.Count){throw 'Bản cài mới còn cột cho phép NULL.'}
    $tables=DocSql $nguon 'SELECT object_id,name FROM sys.tables WHERE is_ms_shipped=0 ORDER BY name'
    $ketQua=@()
    foreach($t in $tables.Rows){
        $cot=DocSql $nguon "SELECT c.name FROM sys.indexes i JOIN sys.index_columns k ON k.object_id=i.object_id AND k.index_id=i.index_id JOIN sys.columns c ON c.object_id=k.object_id AND c.column_id=k.column_id WHERE i.object_id=$($t.object_id) AND i.is_primary_key=1 ORDER BY k.key_ordinal"
        $sapXep=@($cot.Rows|ForEach-Object{'['+$_.name+']'}) -join ','
        $sql='SELECT * FROM dbo.['+$t.name+'] ORDER BY '+$sapXep+' FOR JSON PATH,INCLUDE_NULL_VALUES'
        $json=JsonSql $nguon $sql
        if($json -cne (JsonSql $banSao $sql)){throw ('Dữ liệu không khớp: '+$t.name)}
        $sha=[Security.Cryptography.SHA256]::Create()
        try{$bam=[BitConverter]::ToString($sha.ComputeHash([Text.Encoding]::UTF8.GetBytes($json))).Replace('-','')}finally{$sha.Dispose()}
        $sl=(DocSql $nguon ('SELECT COUNT(*) AS So FROM dbo.['+$t.name+']')).Rows[0].So
        $ketQua+=@{Bang=$t.name;SoDong=$sl;SHA256=$bam}
        Write-Output ('PASS exact data '+$t.name+' rows='+$sl)
    }
    $ketQua|ConvertTo-Json -Depth 3|Set-Content -Encoding UTF8 (Join-Path $goc 'database/QuanLySuaXe_DoiChieu_20261028.json')
    Write-Output ('PASS complete SQL: '+$tables.Rows.Count+' tables, matching schema and every data value, no nullable columns.')
}finally{$nguon.Dispose();$banSao.Dispose()}
