SET NOCOUNT ON;
SET XACT_ABORT ON;
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
SET ANSI_PADDING ON;
SET ANSI_WARNINGS ON;
SET ARITHABORT ON;
SET CONCAT_NULL_YIELDS_NULL ON;
SET NUMERIC_ROUNDABORT OFF;

BEGIN TRY
    BEGIN TRANSACTION;

    IF DB_NAME() IN (N'master', N'model', N'msdb', N'tempdb')
        THROW 51120, N'Chọn database QuanLySuaXe hoặc bản sao kiểm thử trước khi cập nhật kho.', 1;

    DECLARE @BangCu TABLE(Ten SYSNAME PRIMARY KEY);
    INSERT @BangCu VALUES
        (N'CaLam'), (N'ChiTietCaLam'), (N'ChiTietDichVu'), (N'ChiTietPhanCong'),
        (N'ChiTietPhieuNhap'), (N'ChiTietPhieuTiepNhan'), (N'ChucVu'), (N'ChucVuDieuChinh'),
        (N'DichVu'), (N'HoaDon'), (N'KhachHang'), (N'KhoanDieuChinh'), (N'NhaCungCap'),
        (N'NhanVien'), (N'PhieuNhap'), (N'PhieuTiepNhan'), (N'PhuTung'), (N'Xe');
    IF EXISTS(SELECT 1 FROM @BangCu b WHERE OBJECT_ID(N'dbo.' + b.Ten, N'U') IS NULL)
        THROW 51121, N'Database chưa có đủ 18 bảng của cấu trúc đang dùng. Không nhập SQL kho gốc đè lên dữ liệu.', 1;
    IF EXISTS(SELECT 1 FROM sys.tables t JOIN sys.columns c ON c.object_id=t.object_id
              JOIN @BangCu b ON b.Ten=t.name WHERE t.schema_id=SCHEMA_ID(N'dbo') AND c.is_nullable=1)
        THROW 51122, N'Cấu trúc cũ còn cột cho phép NULL. Dừng cập nhật để kiểm tra đúng bản database.', 1;
    IF NOT EXISTS(SELECT 1 FROM sys.columns WHERE object_id=OBJECT_ID(N'dbo.NhanVien')
                  AND name=N'TrangThai' AND TYPE_NAME(user_type_id)=N'nvarchar' AND max_length=40)
       OR NOT EXISTS(SELECT 1 FROM sys.columns WHERE object_id=OBJECT_ID(N'dbo.PhuTung')
                     AND name=N'TrangThai' AND TYPE_NAME(user_type_id)=N'nvarchar' AND max_length=40)
       OR NOT EXISTS(SELECT 1 FROM sys.columns WHERE object_id=OBJECT_ID(N'dbo.DichVu')
                     AND name=N'TrangThai' AND TYPE_NAME(user_type_id)=N'nvarchar' AND max_length=40)
       OR NOT EXISTS(SELECT 1 FROM sys.columns WHERE object_id=OBJECT_ID(N'dbo.NhanVien')
                     AND name=N'MatKhau' AND TYPE_NAME(user_type_id)=N'varchar' AND max_length=64)
        THROW 51123, N'Không đúng cấu trúc tài khoản hoặc trạng thái chữ hiện tại. Không chuyển trạng thái sang BIT/INT.', 1;

    IF OBJECT_ID(N'dbo.DieuChinhTonKho') IS NOT NULL AND OBJECT_ID(N'dbo.DieuChinhTonKho', N'U') IS NULL
        THROW 51124, N'Tên DieuChinhTonKho đang được một đối tượng khác sử dụng.', 1;
    IF OBJECT_ID(N'dbo.DieuChinhGiaBan') IS NOT NULL AND OBJECT_ID(N'dbo.DieuChinhGiaBan', N'U') IS NULL
        THROW 51125, N'Tên DieuChinhGiaBan đang được một đối tượng khác sử dụng.', 1;
    IF EXISTS(SELECT 1 FROM sys.columns WHERE object_id IN(OBJECT_ID(N'dbo.PhuTung'),OBJECT_ID(N'dbo.NhanVien'))
              AND name IN(N'MaPhuTung',N'MaNhanVien') AND collation_name<>N'Vietnamese_CI_AS')
        THROW 51131, N'Collation mã phụ tùng/nhân viên khác bản SQL hiện tại. Dừng để đối chiếu trước khi tạo khóa ngoại.', 1;

    IF OBJECT_ID(N'dbo.DieuChinhTonKho', N'U') IS NULL
    BEGIN
        CREATE TABLE dbo.DieuChinhTonKho (
            MaDieuChinh VARCHAR(10) COLLATE Vietnamese_CI_AS NOT NULL,
            MaPhuTung VARCHAR(10) COLLATE Vietnamese_CI_AS NOT NULL,
            MaNhanVien VARCHAR(10) COLLATE Vietnamese_CI_AS NOT NULL,
            NgayDieuChinh DATE NOT NULL,
            SoLuongCu INT NOT NULL,
            SoLuongMoi INT NOT NULL,
            DonGiaNhap DECIMAL(18,0) NOT NULL,
            GiaTriChenhLech AS ((SoLuongMoi - SoLuongCu) * DonGiaNhap) PERSISTED NOT NULL,
            LyDo NVARCHAR(255) COLLATE Vietnamese_CI_AS NOT NULL,
            CONSTRAINT PK_DieuChinhTonKho PRIMARY KEY (MaDieuChinh)
        );
    END;
    IF OBJECT_ID(N'dbo.DieuChinhGiaBan', N'U') IS NULL
    BEGIN
        CREATE TABLE dbo.DieuChinhGiaBan (
            MaDieuChinhGia VARCHAR(10) COLLATE Vietnamese_CI_AS NOT NULL,
            MaPhuTung VARCHAR(10) COLLATE Vietnamese_CI_AS NOT NULL,
            MaNhanVien VARCHAR(10) COLLATE Vietnamese_CI_AS NOT NULL,
            NgayDieuChinh DATE NOT NULL,
            GiaBanCu DECIMAL(18,0) NOT NULL,
            GiaBanMoi DECIMAL(18,0) NOT NULL,
            LyDo NVARCHAR(255) COLLATE Vietnamese_CI_AS NOT NULL,
            CONSTRAINT PK_DieuChinhGiaBan PRIMARY KEY (MaDieuChinhGia)
        );
    END;

    DECLARE @Cot TABLE(Bang SYSNAME, Cot SYSNAME, Kieu SYSNAME, DoDai SMALLINT, DoChinhXac TINYINT, TiLe TINYINT, TinhToan BIT, PRIMARY KEY(Bang,Cot));
    INSERT @Cot VALUES
        (N'DieuChinhTonKho',N'MaDieuChinh',N'varchar',10,0,0,0),
        (N'DieuChinhTonKho',N'MaPhuTung',N'varchar',10,0,0,0),
        (N'DieuChinhTonKho',N'MaNhanVien',N'varchar',10,0,0,0),
        (N'DieuChinhTonKho',N'NgayDieuChinh',N'date',3,10,0,0),
        (N'DieuChinhTonKho',N'SoLuongCu',N'int',4,10,0,0),
        (N'DieuChinhTonKho',N'SoLuongMoi',N'int',4,10,0,0),
        (N'DieuChinhTonKho',N'DonGiaNhap',N'decimal',9,18,0,0),
        (N'DieuChinhTonKho',N'GiaTriChenhLech',N'decimal',17,29,0,1),
        (N'DieuChinhTonKho',N'LyDo',N'nvarchar',510,0,0,0),
        (N'DieuChinhGiaBan',N'MaDieuChinhGia',N'varchar',10,0,0,0),
        (N'DieuChinhGiaBan',N'MaPhuTung',N'varchar',10,0,0,0),
        (N'DieuChinhGiaBan',N'MaNhanVien',N'varchar',10,0,0,0),
        (N'DieuChinhGiaBan',N'NgayDieuChinh',N'date',3,10,0,0),
        (N'DieuChinhGiaBan',N'GiaBanCu',N'decimal',9,18,0,0),
        (N'DieuChinhGiaBan',N'GiaBanMoi',N'decimal',9,18,0,0),
        (N'DieuChinhGiaBan',N'LyDo',N'nvarchar',510,0,0,0);
    IF EXISTS(SELECT 1 FROM @Cot e LEFT JOIN sys.columns c ON c.object_id=OBJECT_ID(N'dbo.'+e.Bang) AND c.name=e.Cot
              WHERE c.column_id IS NULL OR TYPE_NAME(c.user_type_id)<>e.Kieu OR c.max_length<>e.DoDai
                 OR c.precision<>e.DoChinhXac OR c.scale<>e.TiLe OR c.is_nullable<>0 OR c.is_computed<>e.TinhToan)
       OR EXISTS(SELECT 1 FROM sys.tables t JOIN sys.columns c ON c.object_id=t.object_id
                  WHERE t.schema_id=SCHEMA_ID(N'dbo') AND t.name IN(N'DieuChinhTonKho',N'DieuChinhGiaBan')
                    AND NOT EXISTS(SELECT 1 FROM @Cot e WHERE e.Bang=t.name AND e.Cot=c.name))
        THROW 51126, N'Hai bảng lịch sử đã có nhưng cấu trúc không khớp. Dừng để giữ dữ liệu, không tự xóa hoặc tạo lại.', 1;
    IF NOT EXISTS(SELECT 1 FROM sys.computed_columns WHERE object_id=OBJECT_ID(N'dbo.DieuChinhTonKho')
                  AND name=N'GiaTriChenhLech' AND is_persisted=1 AND is_nullable=0
                  AND REPLACE(REPLACE(REPLACE(definition,N'[',N''),N']',N''),N' ',N'')=N'((SoLuongMoi-SoLuongCu)*DonGiaNhap)')
        THROW 51127, N'GiaTriChenhLech phải được tính theo (SoLuongMoi - SoLuongCu) * DonGiaNhap, lưu PERSISTED NOT NULL.', 1;

    DECLARE @KhoaChinh TABLE(Bang SYSNAME, Cot SYSNAME);
    INSERT @KhoaChinh VALUES(N'DieuChinhTonKho',N'MaDieuChinh'),(N'DieuChinhGiaBan',N'MaDieuChinhGia');
    IF EXISTS(SELECT 1 FROM @KhoaChinh k WHERE NOT EXISTS(
        SELECT 1 FROM sys.indexes i JOIN sys.index_columns ic ON ic.object_id=i.object_id AND ic.index_id=i.index_id
        JOIN sys.columns c ON c.object_id=ic.object_id AND c.column_id=ic.column_id
        WHERE i.object_id=OBJECT_ID(N'dbo.'+k.Bang) AND i.is_primary_key=1 AND ic.key_ordinal=1 AND c.name=k.Cot
          AND (SELECT COUNT(*) FROM sys.index_columns p WHERE p.object_id=i.object_id AND p.index_id=i.index_id AND p.key_ordinal>0)=1))
        THROW 51128, N'Khóa chính bảng lịch sử không đúng mã điều chỉnh.', 1;

    DECLARE @GiaCu NVARCHAR(MAX)=(SELECT definition FROM sys.check_constraints WHERE parent_object_id=OBJECT_ID(N'dbo.PhuTung') AND name=N'CK_PhuTung_Gia');
    SET @GiaCu=LOWER(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(COALESCE(@GiaCu,N''),N'[',N''),N']',N''),N'(',N''),N')',N''),N' ',N''));
    IF @GiaCu NOT IN(N'',N'dongianhap>0anddongiaban>=dongianhap',N'dongianhap>0anddongiaban>0')
        THROW 51129, N'Điều kiện giá phụ tùng khác cấu trúc dự kiến. Dừng để đối chiếu, không tự gỡ điều kiện lạ.', 1;
    IF @GiaCu=N'dongianhap>0anddongiaban>=dongianhap'
        ALTER TABLE dbo.PhuTung DROP CONSTRAINT CK_PhuTung_Gia;
    IF NOT EXISTS(SELECT 1 FROM sys.check_constraints WHERE parent_object_id=OBJECT_ID(N'dbo.PhuTung') AND name=N'CK_PhuTung_Gia')
        ALTER TABLE dbo.PhuTung WITH CHECK ADD CONSTRAINT CK_PhuTung_Gia CHECK(DonGiaNhap>0 AND DonGiaBan>0);
    ALTER TABLE dbo.PhuTung WITH CHECK CHECK CONSTRAINT CK_PhuTung_Gia;

    IF OBJECT_ID(N'dbo.FK_DCTK_PhuTung',N'F') IS NULL
        ALTER TABLE dbo.DieuChinhTonKho WITH CHECK ADD CONSTRAINT FK_DCTK_PhuTung FOREIGN KEY(MaPhuTung) REFERENCES dbo.PhuTung(MaPhuTung);
    IF OBJECT_ID(N'dbo.FK_DCTK_NhanVien',N'F') IS NULL
        ALTER TABLE dbo.DieuChinhTonKho WITH CHECK ADD CONSTRAINT FK_DCTK_NhanVien FOREIGN KEY(MaNhanVien) REFERENCES dbo.NhanVien(MaNhanVien);
    IF OBJECT_ID(N'dbo.FK_DCGB_PhuTung',N'F') IS NULL
        ALTER TABLE dbo.DieuChinhGiaBan WITH CHECK ADD CONSTRAINT FK_DCGB_PhuTung FOREIGN KEY(MaPhuTung) REFERENCES dbo.PhuTung(MaPhuTung);
    IF OBJECT_ID(N'dbo.FK_DCGB_NhanVien',N'F') IS NULL
        ALTER TABLE dbo.DieuChinhGiaBan WITH CHECK ADD CONSTRAINT FK_DCGB_NhanVien FOREIGN KEY(MaNhanVien) REFERENCES dbo.NhanVien(MaNhanVien);

    DECLARE @DieuKien TABLE(Bang SYSNAME, Ten SYSNAME, NoiDung NVARCHAR(MAX));
    INSERT @DieuKien VALUES
        (N'DieuChinhTonKho',N'CK_DCTK_SoLuong',N'SoLuongCu>=0 AND SoLuongMoi>=0 AND SoLuongMoi<>SoLuongCu'),
        (N'DieuChinhTonKho',N'CK_DCTK_DonGiaNhap',N'DonGiaNhap>0'),
        (N'DieuChinhTonKho',N'CK_DCTK_LyDo',N'LEN(LTRIM(RTRIM(LyDo)))>0'),
        (N'DieuChinhTonKho',N'CK_DCTK_Ma',N'LEN(MaDieuChinh) BETWEEN 5 AND 10 AND LEFT(MaDieuChinh,2)=''DC'' AND SUBSTRING(MaDieuChinh,3,8) NOT LIKE ''%[^0-9]%'''),
        (N'DieuChinhGiaBan',N'CK_DCGB_Gia',N'GiaBanCu>0 AND GiaBanMoi>0 AND GiaBanMoi<>GiaBanCu'),
        (N'DieuChinhGiaBan',N'CK_DCGB_LyDo',N'LEN(LTRIM(RTRIM(LyDo)))>0'),
        (N'DieuChinhGiaBan',N'CK_DCGB_Ma',N'LEN(MaDieuChinhGia) BETWEEN 5 AND 10 AND LEFT(MaDieuChinhGia,2)=''DG'' AND SUBSTRING(MaDieuChinhGia,3,8) NOT LIKE ''%[^0-9]%''');
    DECLARE @Bang SYSNAME,@Ten SYSNAME,@NoiDung NVARCHAR(MAX),@Lenh NVARCHAR(MAX);
    DECLARE DieuKien CURSOR LOCAL FAST_FORWARD FOR SELECT Bang,Ten,NoiDung FROM @DieuKien;
    OPEN DieuKien;
    FETCH NEXT FROM DieuKien INTO @Bang,@Ten,@NoiDung;
    WHILE @@FETCH_STATUS=0
    BEGIN
        IF NOT EXISTS(SELECT 1 FROM sys.check_constraints WHERE parent_object_id=OBJECT_ID(N'dbo.'+@Bang) AND name=@Ten)
        BEGIN
            SET @Lenh=N'ALTER TABLE dbo.'+QUOTENAME(@Bang)+N' WITH CHECK ADD CONSTRAINT '+QUOTENAME(@Ten)+N' CHECK('+@NoiDung+N');';
            EXEC sys.sp_executesql @Lenh;
        END;
        SET @Lenh=N'ALTER TABLE dbo.'+QUOTENAME(@Bang)+N' WITH CHECK CHECK CONSTRAINT '+QUOTENAME(@Ten)+N';';
        EXEC sys.sp_executesql @Lenh;
        FETCH NEXT FROM DieuKien INTO @Bang,@Ten,@NoiDung;
    END;
    CLOSE DieuKien;
    DEALLOCATE DieuKien;

    ALTER TABLE dbo.DieuChinhTonKho WITH CHECK CHECK CONSTRAINT FK_DCTK_PhuTung,FK_DCTK_NhanVien;
    ALTER TABLE dbo.DieuChinhGiaBan WITH CHECK CHECK CONSTRAINT FK_DCGB_PhuTung,FK_DCGB_NhanVien;
    IF (SELECT COUNT(*) FROM sys.foreign_keys f JOIN sys.foreign_key_columns k ON k.constraint_object_id=f.object_id
        WHERE f.name IN(N'FK_DCTK_PhuTung',N'FK_DCGB_PhuTung',N'FK_DCTK_NhanVien',N'FK_DCGB_NhanVien')
          AND f.parent_object_id IN(OBJECT_ID(N'dbo.DieuChinhTonKho'),OBJECT_ID(N'dbo.DieuChinhGiaBan'))
          AND f.referenced_object_id=OBJECT_ID(N'dbo.'+CASE WHEN f.name IN(N'FK_DCTK_PhuTung',N'FK_DCGB_PhuTung') THEN N'PhuTung' ELSE N'NhanVien' END)
          AND COL_NAME(k.parent_object_id,k.parent_column_id)=CASE WHEN f.name IN(N'FK_DCTK_PhuTung',N'FK_DCGB_PhuTung') THEN N'MaPhuTung' ELSE N'MaNhanVien' END
          AND COL_NAME(k.referenced_object_id,k.referenced_column_id)=CASE WHEN f.name IN(N'FK_DCTK_PhuTung',N'FK_DCGB_PhuTung') THEN N'MaPhuTung' ELSE N'MaNhanVien' END)<>4
        THROW 51130, N'Khóa ngoại lịch sử kho không đúng phụ tùng hoặc người điều chỉnh.', 1;

    IF NOT EXISTS(SELECT 1 FROM sys.indexes WHERE object_id=OBJECT_ID(N'dbo.DieuChinhTonKho') AND name=N'IX_DCTK_PhuTung_Ngay')
        CREATE INDEX IX_DCTK_PhuTung_Ngay ON dbo.DieuChinhTonKho(MaPhuTung,NgayDieuChinh DESC,MaDieuChinh DESC) INCLUDE(MaNhanVien,SoLuongCu,SoLuongMoi,DonGiaNhap);
    IF NOT EXISTS(SELECT 1 FROM sys.indexes WHERE object_id=OBJECT_ID(N'dbo.DieuChinhTonKho') AND name=N'IX_DCTK_NhanVien_Ngay')
        CREATE INDEX IX_DCTK_NhanVien_Ngay ON dbo.DieuChinhTonKho(MaNhanVien,NgayDieuChinh DESC);
    IF NOT EXISTS(SELECT 1 FROM sys.indexes WHERE object_id=OBJECT_ID(N'dbo.DieuChinhGiaBan') AND name=N'IX_DCGB_PhuTung_Ngay')
        CREATE INDEX IX_DCGB_PhuTung_Ngay ON dbo.DieuChinhGiaBan(MaPhuTung,NgayDieuChinh DESC,MaDieuChinhGia DESC) INCLUDE(MaNhanVien,GiaBanCu,GiaBanMoi);
    IF NOT EXISTS(SELECT 1 FROM sys.indexes WHERE object_id=OBJECT_ID(N'dbo.DieuChinhGiaBan') AND name=N'IX_DCGB_NhanVien_Ngay')
        CREATE INDEX IX_DCGB_NhanVien_Ngay ON dbo.DieuChinhGiaBan(MaNhanVien,NgayDieuChinh DESC);

    COMMIT TRANSACTION;
    PRINT N'Đã cập nhật kho: giữ dữ liệu cũ, thêm lịch sử tồn kho và lịch sử giá bán. Không đổi tài khoản hoặc trạng thái chữ.';
END TRY
BEGIN CATCH
    IF XACT_STATE()<>0 ROLLBACK TRANSACTION;
    THROW;
END CATCH;
