param(
    [string]$Database='QuanLySuaXe',
    [string]$File='database/QuanLySuaXe_DayDu.sql',
    [string]$ExportName='QuanLySuaXe',
    [System.Data.SqlClient.SqlConnection]$KetNoiHienCo,
    [System.Data.SqlClient.SqlTransaction]$GiaoDichHienCo
)
$ErrorActionPreference='Stop'
. (Join-Path $PSScriptRoot 'SqlChung.ps1')
if($ExportName -notmatch '^[A-Za-z0-9_]+$'){throw 'Invalid export name'}
$tuMoKetNoi=$null -eq $KetNoiHienCo
$kn=if($tuMoKetNoi){MoSql $Database}else{$KetNoiHienCo}
if($kn.State -ne [Data.ConnectionState]::Open -or $kn.Database -cne $Database){throw 'Kết nối xuất SQL chưa mở hoặc không đúng database.'}
if($GiaoDichHienCo -and $GiaoDichHienCo.Connection -ne $kn){throw 'Giao dịch xuất SQL không thuộc kết nối đã chọn.'}
function DocNguon([string]$sql){return ,(DocSql $kn $sql $GiaoDichHienCo)}
$script=New-Object Text.StringBuilder
function E([string]$s){[void]$script.AppendLine($s)}
function Batch([string]$s){E $s;E 'GO'}
function Q([string]$s){return '['+$s.Replace(']',']]')+']'}
function Literal($value){
    if($value -is [DBNull]){return 'NULL'}
    if($value -is [bool]){if($value){return '1'}else{return '0'}}
    if($value -is [byte[]]){return '0x'+[BitConverter]::ToString($value).Replace('-','')}
    if($value -is [DateTime]){return "N'"+$value.ToString('yyyy-MM-ddTHH:mm:ss.fffffff',[Globalization.CultureInfo]::InvariantCulture)+"'"}
    if($value -is [TimeSpan]){return "N'"+$value.ToString('c')+"'"}
    if($value -is [string] -or $value -is [Guid]){return "N'"+$value.ToString().Replace("'","''")+"'"}
    return [Convert]::ToString($value,[Globalization.CultureInfo]::InvariantCulture)
}
try {
    $collation=(DocNguon 'SELECT CONVERT(nvarchar(128),DATABASEPROPERTYEX(DB_NAME(),''Collation'')) AS CollationName').Rows[0].CollationName
    if($collation -notmatch '^[A-Za-z0-9_]+$'){throw 'Không nhận dạng được collation database nguồn.'}
    E "USE [master];";E 'GO';E "IF DB_ID(N'$ExportName') IS NOT NULL BEGIN PRINT N'Database đã tồn tại. Hãy cài vào một database mới, không ghi đè dữ liệu hiện có.'; SET NOEXEC ON; END;";E 'GO';Batch "CREATE DATABASE [$ExportName] COLLATE $collation;";Batch "USE [$ExportName];";Batch 'SET XACT_ABORT ON; SET NOCOUNT ON; SET ANSI_NULLS ON; SET QUOTED_IDENTIFIER ON; SET ANSI_PADDING ON; SET ANSI_WARNINGS ON; SET ARITHABORT ON; SET CONCAT_NULL_YIELDS_NULL ON; SET NUMERIC_ROUNDABORT OFF;'
    $tables=DocNguon "SELECT object_id,name FROM sys.tables WHERE is_ms_shipped=0 AND schema_id=SCHEMA_ID('dbo') ORDER BY name"
    $colSql='SELECT c.*,y.name AS TypeName,co.definition AS ComputedDefinition,co.is_persisted AS IsPersisted,dc.name AS DefaultName,dc.definition AS DefaultDefinition,ic.seed_value,ic.increment_value,ic.last_value FROM sys.columns c JOIN sys.types y ON y.user_type_id=c.user_type_id LEFT JOIN sys.computed_columns co ON co.object_id=c.object_id AND co.column_id=c.column_id LEFT JOIN sys.default_constraints dc ON dc.object_id=c.default_object_id LEFT JOIN sys.identity_columns ic ON ic.object_id=c.object_id AND ic.column_id=c.column_id WHERE c.object_id='
    foreach($table in $tables.Rows){
        $columns=DocNguon ($colSql+$table.object_id+' ORDER BY c.column_id');$defs=@()
        foreach($c in $columns.Rows){
            $line=Q $c.name
            if($c.is_computed){$line+=' AS '+$c.ComputedDefinition; if($c.IsPersisted){$line+=' PERSISTED';if(!$c.is_nullable){$line+=' NOT NULL'}}; $defs+=$line;continue}
            $type=$c.TypeName
            if($type -in @('nvarchar','nchar','varchar','char','varbinary','binary')){$size=if($c.max_length -eq -1){'MAX'}elseif($type -in @('nvarchar','nchar')){($c.max_length/2).ToString()}else{$c.max_length.ToString()};$type+='('+$size+')'}
            elseif($type -in @('decimal','numeric')){$type+='('+$c.precision+','+$c.scale+')'}
            elseif($type -in @('datetime2','datetimeoffset','time')){$type+='('+$c.scale+')'}
            $line+=' '+$type
            if($c.collation_name -isnot [DBNull]){$line+=' COLLATE '+$c.collation_name}
            if($c.is_identity){$line+=' IDENTITY('+ $c.seed_value+','+$c.increment_value+')'}
            $line+=if($c.is_nullable){' NULL'}else{' NOT NULL'}
            if($c.DefaultDefinition -isnot [DBNull]){$line+=' CONSTRAINT '+(Q $c.DefaultName)+' DEFAULT '+$c.DefaultDefinition}
            $defs+=$line
        }
        Batch ('CREATE TABLE dbo.'+(Q $table.name)+" (`r`n    "+($defs -join ",`r`n    ")+"`r`n);")
        $indexes=DocNguon ('SELECT * FROM sys.indexes WHERE object_id='+$table.object_id+' AND index_id>0 AND is_hypothetical=0 ORDER BY index_id')
        foreach($idx in $indexes.Rows){
            $ic=DocNguon ("SELECT c.name,i.key_ordinal,i.is_descending_key,i.is_included_column FROM sys.index_columns i JOIN sys.columns c ON c.object_id=i.object_id AND c.column_id=i.column_id WHERE i.object_id=$($table.object_id) AND i.index_id=$($idx.index_id) ORDER BY i.is_included_column,i.key_ordinal,i.index_column_id")
            $keys=@($ic.Rows|Where-Object{!$_.is_included_column -and $_.key_ordinal -gt 0}|ForEach-Object{(Q $_.name)+$(if($_.is_descending_key){' DESC'}else{' ASC'})}) -join ','
            $kind=if($idx.type -eq 1){'CLUSTERED'}else{'NONCLUSTERED'}
            if($idx.is_primary_key -or $idx.is_unique_constraint){$constraint=if($idx.is_primary_key){'PRIMARY KEY'}else{'UNIQUE'};Batch ('ALTER TABLE dbo.'+(Q $table.name)+' ADD CONSTRAINT '+(Q $idx.name)+' '+$constraint+' '+$kind+' ('+$keys+');')}
            else{$s='CREATE '+$(if($idx.is_unique){'UNIQUE '}else{''})+$kind+' INDEX '+(Q $idx.name)+' ON dbo.'+(Q $table.name)+' ('+$keys+')';$include=@($ic.Rows|Where-Object{$_.is_included_column}|ForEach-Object{Q $_.name});if($include.Count){$s+=' INCLUDE ('+($include -join ',')+')'};if($idx.has_filter){$s+=' WHERE '+$idx.filter_definition};Batch ($s+';')}
        }
        $checks=DocNguon ('SELECT name,definition FROM sys.check_constraints WHERE parent_object_id='+$table.object_id)
        foreach($check in $checks.Rows){Batch ('ALTER TABLE dbo.'+(Q $table.name)+' WITH CHECK ADD CONSTRAINT '+(Q $check.name)+' CHECK '+$check.definition+';')}
    }
    foreach($table in $tables.Rows){
        $columns=DocNguon ($colSql+$table.object_id+' AND c.is_computed=0 ORDER BY c.column_id')
        $names=@($columns.Rows|ForEach-Object{Q $_.name});$identity=@($columns.Rows|Where-Object{$_.is_identity})
        $rows=DocNguon ('SELECT '+($names -join ',')+' FROM dbo.'+(Q $table.name))
        if($identity.Count -and $rows.Rows.Count){Batch ('SET IDENTITY_INSERT dbo.'+(Q $table.name)+' ON;')}
        foreach($row in $rows.Rows){$values=@();foreach($c in $columns.Rows){$values+=Literal $row[$c.name]};Batch ('INSERT dbo.'+(Q $table.name)+' ('+($names -join ',')+') VALUES ('+($values -join ',')+');')}
        if($identity.Count -and $rows.Rows.Count){Batch ('SET IDENTITY_INSERT dbo.'+(Q $table.name)+' OFF;')}
        if($identity.Count -and $identity[0].last_value -isnot [DBNull]){Batch ("DBCC CHECKIDENT ('dbo."+$table.name+"', RESEED, "+$identity[0].last_value+") WITH NO_INFOMSGS;")}
    }
    $fks=DocNguon 'SELECT f.name,OBJECT_NAME(f.parent_object_id) AS ParentTable,OBJECT_NAME(f.referenced_object_id) AS RefTable,f.object_id,f.delete_referential_action_desc,f.update_referential_action_desc FROM sys.foreign_keys f ORDER BY f.name'
    foreach($fk in $fks.Rows){
        $cols=DocNguon ('SELECT COL_NAME(parent_object_id,parent_column_id) AS P,COL_NAME(referenced_object_id,referenced_column_id) AS R FROM sys.foreign_key_columns WHERE constraint_object_id='+$fk.object_id+' ORDER BY constraint_column_id')
        $p=@($cols.Rows|ForEach-Object{Q $_.P}) -join ',';$r=@($cols.Rows|ForEach-Object{Q $_.R}) -join ','
        Batch ('ALTER TABLE dbo.'+(Q $fk.ParentTable)+' WITH CHECK ADD CONSTRAINT '+(Q $fk.name)+' FOREIGN KEY ('+$p+') REFERENCES dbo.'+(Q $fk.RefTable)+' ('+$r+') ON DELETE '+$fk.delete_referential_action_desc.Replace('_',' ')+' ON UPDATE '+$fk.update_referential_action_desc.Replace('_',' ')+';')
    }
    $modules=DocNguon "SELECT m.definition,m.uses_ansi_nulls,m.uses_quoted_identifier FROM sys.sql_modules m JOIN sys.objects o ON o.object_id=m.object_id WHERE o.is_ms_shipped=0 ORDER BY CASE o.type WHEN 'FN' THEN 0 WHEN 'IF' THEN 0 WHEN 'V' THEN 1 WHEN 'P' THEN 2 ELSE 3 END,o.name"
    foreach($module in $modules.Rows){Batch ('SET ANSI_NULLS '+$(if($module.uses_ansi_nulls){'ON'}else{'OFF'})+'; SET QUOTED_IDENTIFIER '+$(if($module.uses_quoted_identifier){'ON'}else{'OFF'})+';');Batch $module.definition}
    $target=if([IO.Path]::IsPathRooted($File)){[IO.Path]::GetFullPath($File)}else{[IO.Path]::GetFullPath((Join-Path (Split-Path $PSScriptRoot -Parent) $File))}
    [IO.File]::WriteAllText($target,$script.ToString(),(New-Object Text.UTF8Encoding($true)))
    Write-Output ('EXPORTED '+$tables.Rows.Count+' tables: '+$target)
} finally {if($tuMoKetNoi){$kn.Dispose()}}
