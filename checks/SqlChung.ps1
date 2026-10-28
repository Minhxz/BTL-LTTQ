$ErrorActionPreference='Stop'
Add-Type -AssemblyName System.Data
function MoSql([string]$database='QuanLySuaXe') {
    $boTao=New-Object Data.SqlClient.SqlConnectionStringBuilder "Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=$database;Integrated Security=True;Connect Timeout=3;TrustServerCertificate=True"
    $kn=New-Object Data.SqlClient.SqlConnection $boTao.ConnectionString
    try {$kn.Open();return $kn} catch {$kn.Dispose()}
    $tep=(Join-Path ([Environment]::GetFolderPath('UserProfile')) 'AppData/Local/Microsoft/Microsoft SQL Server Local DB/Instances/MSSQLLocalDB/error.log')
    $luong=[IO.File]::Open($tep,'Open','Read','ReadWrite');$doc=New-Object IO.StreamReader($luong)
    try {$noiDung=$doc.ReadToEnd()} finally {$doc.Dispose()}
    $cacOng=[regex]::Matches($noiDung,'\\\\\.\\pipe\\LOCALDB#[A-Fa-f0-9]+\\tsql\\query')
    if(!$cacOng.Count){throw 'Không tìm thấy LocalDB đang chạy.'}
    $boTao['Data Source']='np:\\.\pipe\'+([regex]::Match($cacOng[-1].Value,'LOCALDB#[A-Fa-f0-9]+').Value)+'\tsql\query'
    $kn=New-Object Data.SqlClient.SqlConnection $boTao.ConnectionString;$kn.Open();return $kn
}
function DocSql($kn,[string]$sql,$gd=$null) {
    $lenh=$kn.CreateCommand();$lenh.CommandTimeout=120;$lenh.CommandText=$sql;if($gd){$lenh.Transaction=$gd}
    $nap=New-Object Data.SqlClient.SqlDataAdapter $lenh;$bang=New-Object Data.DataTable
    try {[void]$nap.Fill($bang);return ,$bang} finally {$nap.Dispose();$lenh.Dispose()}
}
function ChaySql($kn,[string]$sql,$gd=$null) {
    $lenh=$kn.CreateCommand();$lenh.CommandTimeout=120;$lenh.CommandText=$sql;if($gd){$lenh.Transaction=$gd}
    try {[void]$lenh.ExecuteNonQuery()}finally{$lenh.Dispose()}
}
function ChayTepSql($kn,[string]$tep) {
    foreach($doan in [regex]::Split([IO.File]::ReadAllText($tep),'(?im)^\s*GO\s*\r?$')) {
        if($doan.Trim()){ChaySql $kn $doan}
    }
}
