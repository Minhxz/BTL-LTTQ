param(
    [switch]$XuatLai,
    [ValidatePattern('^QuanLySuaXe(?:_[A-Za-z0-9_]+)?$')]
    [string]$DatabaseNguon='QuanLySuaXe',
    [ValidatePattern('^QuanLySuaXe_(?:Codex|KiemThu)[A-Za-z0-9_]*$')]
    [string]$DatabaseThu='QuanLySuaXe_CodexInstall',
    [ValidatePattern('^[A-Za-z0-9_]+$')]
    [string]$TenDatabaseTrongTep='QuanLySuaXe',
    [string]$TepSql='database/QuanLySuaXe_DayDu_20261029.sql',
    [string]$TepDoiChieu='database/QuanLySuaXe_DoiChieu_20261029.json'
)
$ErrorActionPreference='Stop'
. (Join-Path $PSScriptRoot 'SqlChung.ps1')
$goc=Split-Path $PSScriptRoot -Parent
if($DatabaseThu -eq $DatabaseNguon){throw 'Database cài thử phải khác database nguồn.'}
function DuongDanDayDu([string]$tep){
    if([IO.Path]::IsPathRooted($tep)){return [IO.Path]::GetFullPath($tep)}
    return [IO.Path]::GetFullPath((Join-Path $goc $tep))
}
$tepSqlDayDu=DuongDanDayDu $TepSql
if($XuatLai){& (Join-Path $PSScriptRoot 'XuatSqlDayDu.ps1') -Database $DatabaseNguon -File $tepSqlDayDu -ExportName $TenDatabaseTrongTep}
$noiDung=[IO.File]::ReadAllText($tepSqlDayDu)
if([regex]::Matches($noiDung,'(?m)^INSERT dbo\.').Count -eq 0){throw 'Bản SQL chưa có dữ liệu INSERT.'}
if($noiDung -match '(?i)\bDROP\s+DATABASE\b' -or $noiDung -match '(?i)\bALTER\s+DATABASE\b'){
    throw 'Bản cài từ đầu không được chứa lệnh xóa hoặc sửa database đang có.'
}
if(-not $noiDung.Contains("CREATE DATABASE [$TenDatabaseTrongTep]") -or -not $noiDung.Contains("USE [$TenDatabaseTrongTep];")){
    throw 'Tên database trong file SQL không khớp tham số đã chọn.'
}
$noiDung=$noiDung.Replace("DB_ID(N'$TenDatabaseTrongTep')","DB_ID(N'$DatabaseThu')").Replace("CREATE DATABASE [$TenDatabaseTrongTep]","CREATE DATABASE [$DatabaseThu]").Replace("USE [$TenDatabaseTrongTep];","USE [$DatabaseThu];")
$thuMucThu=Join-Path $PSScriptRoot 'output'
[void](New-Item -ItemType Directory -Path $thuMucThu -Force)
$tepThu=Join-Path $thuMucThu ('cai-moi-thu-'+$DatabaseThu+'.sql')
[IO.File]::WriteAllText($tepThu,$noiDung,(New-Object Text.UTF8Encoding($true)))
$chu=MoSql master
try{
    ChaySql $chu "IF DB_ID(N'$DatabaseThu') IS NOT NULL BEGIN ALTER DATABASE [$DatabaseThu] SET SINGLE_USER WITH ROLLBACK IMMEDIATE; DROP DATABASE [$DatabaseThu]; END;"
    ChayTepSql $chu $tepThu
}finally{$chu.Dispose()}
$nguon=MoSql $DatabaseNguon
$banSao=MoSql $DatabaseThu
function JsonSql($kn,[string]$sql){return (@((DocSql $kn $sql).Rows|ForEach-Object{$_.Item(0)}) -join '')}
try{
    $cauTruc="SELECT t.name AS Bang,c.name AS Cot,y.name AS Kieu,c.max_length,c.precision,c.scale,c.is_nullable,c.is_identity,c.is_computed,c.collation_name,co.definition AS CongThuc,co.is_persisted,d.definition AS MacDinh FROM sys.tables t JOIN sys.columns c ON c.object_id=t.object_id JOIN sys.types y ON y.user_type_id=c.user_type_id LEFT JOIN sys.computed_columns co ON co.object_id=c.object_id AND co.column_id=c.column_id LEFT JOIN sys.default_constraints d ON d.object_id=c.default_object_id ORDER BY t.name,c.column_id FOR JSON PATH,INCLUDE_NULL_VALUES"
    if((JsonSql $nguon $cauTruc) -cne (JsonSql $banSao $cauTruc)){throw 'Cấu trúc bản cài mới không khớp database nguồn.'}
    $cacDoiChieu=@{
        'KhoaNgoai'='SELECT f.name,OBJECT_NAME(f.parent_object_id) AS Bang,OBJECT_NAME(f.referenced_object_id) AS BangCha,COL_NAME(k.parent_object_id,k.parent_column_id) AS Cot,COL_NAME(k.referenced_object_id,k.referenced_column_id) AS CotCha,k.constraint_column_id,f.delete_referential_action_desc,f.update_referential_action_desc,f.is_disabled,f.is_not_trusted FROM sys.foreign_keys f JOIN sys.foreign_key_columns k ON k.constraint_object_id=f.object_id ORDER BY f.name,k.constraint_column_id FOR JSON PATH,INCLUDE_NULL_VALUES';
        'DieuKien'='SELECT OBJECT_NAME(parent_object_id) AS Bang,name,definition,is_disabled,is_not_trusted FROM sys.check_constraints ORDER BY OBJECT_NAME(parent_object_id),name FOR JSON PATH,INCLUDE_NULL_VALUES';
        'ChiMuc'='SELECT OBJECT_NAME(i.object_id) AS Bang,i.name,i.type_desc,i.is_unique,i.is_primary_key,i.is_unique_constraint,i.has_filter,i.filter_definition,c.name AS Cot,k.key_ordinal,k.is_descending_key,k.is_included_column FROM sys.indexes i JOIN sys.index_columns k ON k.object_id=i.object_id AND k.index_id=i.index_id JOIN sys.columns c ON c.object_id=k.object_id AND c.column_id=k.column_id JOIN sys.tables t ON t.object_id=i.object_id WHERE i.index_id>0 AND i.is_hypothetical=0 ORDER BY OBJECT_NAME(i.object_id),i.name,k.index_column_id FOR JSON PATH,INCLUDE_NULL_VALUES'
    }
    foreach($ten in $cacDoiChieu.Keys){if((JsonSql $nguon $cacDoiChieu[$ten]) -cne (JsonSql $banSao $cacDoiChieu[$ten])){throw ('Bản cài mới khác '+$ten)}}
    if((DocSql $banSao 'SELECT COUNT(*) AS So FROM sys.tables t JOIN sys.columns c ON c.object_id=t.object_id WHERE c.is_nullable=1').Rows[0].So -ne 0){throw 'Bản cài mới còn cột cho phép NULL.'}
    $tables=DocSql $nguon 'SELECT object_id,name FROM sys.tables WHERE is_ms_shipped=0 ORDER BY name'
    if($tables.Rows.Count -notin @(18,20)){throw 'Số bảng nguồn phải là 18 bảng cũ hoặc 20 bảng sau tích hợp kho.'}
    $ketQua=@()
    foreach($t in $tables.Rows){
        $cot=DocSql $nguon "SELECT c.name FROM sys.indexes i JOIN sys.index_columns k ON k.object_id=i.object_id AND k.index_id=i.index_id JOIN sys.columns c ON c.object_id=k.object_id AND c.column_id=k.column_id WHERE i.object_id=$($t.object_id) AND i.is_primary_key=1 ORDER BY k.key_ordinal"
        $sapXep=@($cot.Rows|ForEach-Object{'['+$_.name.Replace(']',']]')+']'}) -join ','
        if(-not $sapXep){throw ('Bảng chưa có khóa chính: '+$t.name)}
        $sql='SELECT * FROM dbo.['+$t.name.Replace(']',']]')+'] ORDER BY '+$sapXep+' FOR JSON PATH,INCLUDE_NULL_VALUES'
        $json=JsonSql $nguon $sql
        if($json -cne (JsonSql $banSao $sql)){throw ('Dữ liệu không khớp: '+$t.name)}
        $sha=[Security.Cryptography.SHA256]::Create()
        try{$bam=[BitConverter]::ToString($sha.ComputeHash([Text.Encoding]::UTF8.GetBytes($json))).Replace('-','')}finally{$sha.Dispose()}
        $sl=(DocSql $nguon ('SELECT COUNT_BIG(*) AS So FROM dbo.['+$t.name.Replace(']',']]')+']')).Rows[0].So
        $ketQua+=@{Bang=$t.name;SoDong=$sl;SHA256=$bam}
        Write-Output ('PASS dữ liệu từng giá trị '+$t.name+'; số dòng='+$sl)
    }
    if($tables.Rows.Count -eq 20){
        if((DocSql $banSao "SELECT COUNT(*) AS So FROM sys.computed_columns WHERE object_id=OBJECT_ID('dbo.DieuChinhTonKho') AND name='GiaTriChenhLech' AND is_persisted=1 AND is_nullable=0").Rows[0].So -ne 1){throw 'Bản cài mới mất computed PERSISTED NOT NULL.'}
    }
    $ketQua|ConvertTo-Json -Depth 3|Set-Content -Encoding UTF8 -LiteralPath (DuongDanDayDu $TepDoiChieu)
    Write-Output ('PASS bản SQL đầy đủ: '+$tables.Rows.Count+' bảng; cột/computed/khóa/index/check và mọi giá trị dữ liệu khớp; không có cột nullable.')
}finally{$nguon.Dispose();$banSao.Dispose()}
