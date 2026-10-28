param([switch]$BienDichLai,[switch]$ChiDon,[ValidateSet('Debug','Release')][string]$CauHinh='Debug')
$ErrorActionPreference='Stop'
$thuMucGoc=(Resolve-Path -LiteralPath $PSScriptRoot).ProviderPath
$cacProject=@('QLyCuaHangSuaXe/QLyCuaHangSuaXe')
$cacDich=@()
foreach($project in $cacProject){
    foreach($ten in @('bin','obj')){
        $dich=[IO.Path]::GetFullPath((Join-Path $thuMucGoc "$project/$ten"))
        if(-not $dich.StartsWith($thuMucGoc+[IO.Path]::DirectorySeparatorChar,[StringComparison]::OrdinalIgnoreCase)){throw 'Không dọn đường dẫn ngoài thư mục bài.'}
        if(Test-Path -LiteralPath $dich){
            $muc=Get-Item -LiteralPath $dich
            if($muc.Attributes -band [IO.FileAttributes]::ReparsePoint){throw "Không dọn thư mục liên kết: $dich"}
            $cacDich+=$muc.FullName
        }
    }
}
foreach($dich in $cacDich){
    $soFile=(Get-ChildItem -LiteralPath $dich -File -Recurse).Count
    Remove-Item -LiteralPath $dich -Recurse -Force
    Write-Output "Đã dọn $soFile file trong $dich"
}
if($BienDichLai -or -not $ChiDon){
    Write-Output 'Biên dịch lại để Designer nạp được các lớp giao diện dùng chung.'
    & (Join-Path $thuMucGoc 'checks/Run.ps1') -Configuration $CauHinh
    if($LASTEXITCODE -ne 0){throw 'Biên dịch hoặc kiểm tra chưa thành công.'}
}
