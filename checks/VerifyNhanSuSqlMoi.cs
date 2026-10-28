using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using QLyCuaHangSuaXe.LopDungChung;
using QLyCuaHangSuaXe.DanhMuc;
using QLyCuaHangSuaXe.DanhMuc.DangNhap;

public static class KiemTraNhanSu
{
    static readonly XuLyDuLieu dl=new XuLyDuLieu();
    static readonly BindingFlags co=BindingFlags.Instance|BindingFlags.NonPublic;
    static int so;
    static T Lay<T>(object o,string ten)
    {
        Type kieu=o.GetType();
        while(kieu!=null){var truong=kieu.GetField(ten,co);if(truong!=null)return (T)truong.GetValue(o);kieu=kieu.BaseType;}
        throw new Exception("Không tìm thấy "+ten);
    }
    static void Dung(bool ok,string ten){if(!ok)throw new Exception(ten);so++;Console.WriteLine("PASS nhân sự: "+ten);}
    static void Chan(Action a,string ten){try{a();}catch(InvalidOperationException){Dung(true,ten);return;}throw new Exception("Không chặn: "+ten);}
    static object GiaTri(string sql){return dl.DocBang(sql).Rows[0][0];}
    static void Hien(Form f){f.StartPosition=FormStartPosition.Manual;f.Location=new Point(-4000,-4000);f.Show();Application.DoEvents();}
    static string DienThoaiMoi()
    {
        for(long i=900000001;i<900000099;i++){string p="0"+i; if(Convert.ToInt32(GiaTri("SELECT COUNT(*) FROM dbo.NhanVien WHERE SoDienThoai='"+p+"'"))==0)return p;}
        throw new Exception("Hết số điện thoại kiểm thử");
    }
    public static int Chay()
    {
        string catalog=new System.Data.SqlClient.SqlConnectionStringBuilder(XuLyDuLieu.ChuoiKetNoi).InitialCatalog;
        Dung(catalog.IndexOf("KiemThu",StringComparison.OrdinalIgnoreCase)>=0||catalog=="QuanLySuaXe_CodexChecks"||catalog=="QuanLySuaXe_CodexUpgrade","Chỉ ghi trên bản sao kiểm thử");
        var vaiTro=dl.DocBang("SELECT TenChucVu FROM dbo.ChucVu ORDER BY MaChucVu");
        Dung(vaiTro.Rows.Count==6&&PhanQuyenNguoiDung.VaiTro.Length==6,"SQL và ứng dụng có đúng sáu chức vụ mới");
        foreach(DataRow dong in vaiTro.Rows)Dung(PhanQuyenNguoiDung.CoVaiTro(dong["TenChucVu"].ToString()),"Có bộ quyền cho "+dong["TenChucVu"]);
        var taiKhoan=dl.DocBang("SELECT n.MaNhanVien,n.HoTen,n.TenDangNhap,c.TenChucVu FROM dbo.NhanVien n JOIN dbo.ChucVu c ON c.MaChucVu=n.MaChucVu ORDER BY n.MaNhanVien");
        Dung(taiKhoan.Rows.Count==5&&string.Join(",",taiKhoan.AsEnumerable().Select(d=>d["HoTen"].ToString()))=="Minh,An,Huy,Mai,Cường","Dữ liệu có đúng năm nhân viên theo yêu cầu");
        foreach(DataRow dong in taiKhoan.Rows)Dung(PhienDangNhap.DangNhap(dong["TenDangNhap"].ToString(),"123")&&PhienDangNhap.VaiTro==dong["TenChucVu"].ToString(),"Đăng nhập "+dong["HoTen"]+" đúng chức vụ");
        Dung(!PhienDangNhap.DangNhap("minh","sai")&&string.IsNullOrEmpty(PhienDangNhap.MaNhanVien),"Sai mật khẩu không giữ phiên trước");
        dl.CapNhatDuLieu("UPDATE dbo.NhanVien SET TrangThai=N'Nghỉ việc' WHERE TenDangNhap='cuong'");
        try {Dung(!PhienDangNhap.DangNhap("cuong","123"),"Nhân viên nghỉ việc không đăng nhập");}
        finally {dl.CapNhatDuLieu("UPDATE dbo.NhanVien SET TrangThai=N'Đang làm' WHERE TenDangNhap='cuong'");}
        Dung(PhienDangNhap.DangNhap("minh","123"),"Khôi phục phiên quản lý");
        string chu=PhienDangNhap.MaNhanVien;
        Chan(()=>QuanLyNhanSu.NghiViec(chu),"Không tự cho quản lý đang đăng nhập nghỉ việc");
        Chan(()=>QuanLyNhanSu.LuuThongTinTaiKhoan(chu,"minh","Thợ phụ"),"Lưu tài khoản vẫn chặn tự đổi quyền đang đăng nhập");
        Chan(()=>PhienDangNhap.DoiMatKhau("sai","moi"),"Đổi mật khẩu phải đúng mật khẩu hiện tại");
        PhienDangNhap.DoiMatKhau("123","456");
        Dung(GiaTri("SELECT MatKhau FROM dbo.NhanVien WHERE MaNhanVien='"+chu+"'").ToString()==BaoMat.BamMatKhau("456")&&BaoMat.BamMatKhau("456").Length==64,"Mật khẩu mới đúng SHA-256 64 ký tự");
        Dung(PhienDangNhap.DangNhap("minh","456"),"Đăng nhập được bằng mật khẩu vừa đổi");
        PhienDangNhap.DoiMatKhau("456","123");
        using(var f=new frmNhanVien())
        {
            Hien(f);
            Dung(f.GetType().GetField("cboTrangThai",co)==null&&Lay<DataGridView>(f,"dgvNhanVien").Columns.Contains("TrangThai"),"Nhân viên chỉ hiển thị trạng thái trong bảng, không có ô sửa trạng thái");
            var tab=Lay<TabControl>(f,"tabNhanSu");
            Dung(tab.TabPages.Contains(Lay<TabPage>(f,"tabNhanVien"))&&tab.TabPages.Contains(Lay<TabPage>(f,"tabBangCong")),"Quản lý mở được hồ sơ và bảng công trong cùng trang nhân sự");
            Lay<Button>(f,"btnBangCong").PerformClick();
            Dung(tab.SelectedTab==Lay<TabPage>(f,"tabBangCong")&&Lay<Panel>(f,"pnlBangCong").Controls.OfType<frmBangCong>().Any(),"Nút bảng công mở ngay thẻ tích hợp cùng form nhân viên");
        }
        string nv,tenTaiKhoan,dienThoai=DienThoaiMoi();
        using(var f=new frmQuanLyTaiKhoan())
        {
            Hien(f);Lay<Button>(f,"btnThemMoi").PerformClick();nv=Lay<TextBox>(f,"txtMaTaiKhoan").Text;tenTaiKhoan="kiemthu_"+nv.ToLowerInvariant();
            Dung(System.Text.RegularExpressions.Regex.IsMatch(nv,"^NV[0-9]{3,}$"),"Tạo mới tài khoản hiện sẵn mã nhân viên");
            Dung(Lay<ComboBox>(f,"cboChucVu").Text=="Thợ phụ"&&f.GetType().GetField("chkTaiKhoanHoatDong",co)==null&&f.GetType().GetField("lblchkTaiKhoanHoatDong",co)==null,"Tạo tài khoản mặc định Thợ phụ, không có nút hoặc ô đổi trạng thái");
            Lay<TextBox>(f,"txtTenNhanVien").Text="Nhân viên kiểm thử";Lay<TextBox>(f,"txtTenDangNhap").Text=tenTaiKhoan;Lay<TextBox>(f,"txtDienThoai").Text=dienThoai;Lay<NumericUpDown>(f,"numLuongThang").Value=8000000;Lay<ComboBox>(f,"cboChucVu").SelectedItem="Thợ phụ";Lay<Button>(f,"btnLuuTaiKhoan").PerformClick();
            Dung(Convert.ToInt32(GiaTri("SELECT COUNT(*) FROM dbo.NhanVien WHERE MaNhanVien='"+nv+"' AND HoTen=N'Nhân viên kiểm thử' AND Luong=8000000 AND TrangThai=N'Đang làm'"))==1,"Bấm Tạo tài khoản lưu đủ nhân viên, điện thoại, lương và trạng thái Đang làm");
            Dung(f.GetType().GetField("txtMatKhauMoi",co)==null&&f.GetType().GetField("lblMatKhauMacDinh",co)==null,"Form tạo tài khoản không có ô hay dòng mật khẩu thừa");
        }
        int soNv=Convert.ToInt32(GiaTri("SELECT COUNT(*) FROM dbo.NhanVien"));
        Chan(()=>QuanLyNhanSu.TaoTaiKhoanMoi("","Trùng tên",tenTaiKhoan,"123","Thợ phụ",true,null,DienThoaiMoi(),7000000),"Tên đăng nhập trùng bị chặn");
        Dung(Convert.ToInt32(GiaTri("SELECT COUNT(*) FROM dbo.NhanVien"))==soNv,"Lỗi tạo tài khoản không sinh dòng nhân viên dở");
        Dung(PhienDangNhap.DangNhap(tenTaiKhoan,"123"),"Tài khoản mới dùng được mật khẩu mặc định 123");
        Dung(!PhanQuyenNguoiDung.CoQuyen(PhienDangNhap.VaiTro,"NHAN_VIEN")&&PhanQuyenNguoiDung.CoQuyen(PhienDangNhap.VaiTro,"PHAN_CONG"),"Thợ chỉ có quyền đúng chức vụ");
        PhienDangNhap.DangNhap("minh","123");
        string tenMoi=tenTaiKhoan+"_moi";QuanLyNhanSu.LuuTaiKhoan(nv,tenMoi,"","Thợ phụ",false);
        Dung(!PhienDangNhap.DangNhap(tenMoi,"123"),"Nghỉ việc khóa đăng nhập");PhienDangNhap.DangNhap("minh","123");
        QuanLyNhanSu.LuuNhanVien(new ThongTinNhanVien{Ma=nv,HoTen="Nhân viên kiểm thử",DienThoai=dienThoai,ChucVu="Thợ phụ",LuongGio=8000000,TrangThai="Đang làm"});
        Dung(GiaTri("SELECT TrangThai FROM dbo.NhanVien WHERE MaNhanVien='"+nv+"'").ToString()=="Nghỉ việc","Sửa hồ sơ bằng trạng thái cũ không kích hoạt lại nhân viên nghỉ việc");
        QuanLyNhanSu.LuuThongTinTaiKhoan(nv,tenMoi,"Thợ phụ");
        Dung(GiaTri("SELECT TrangThai FROM dbo.NhanVien WHERE MaNhanVien='"+nv+"'").ToString()=="Nghỉ việc","Lưu thông tin tài khoản giữ nguyên trạng thái Nghỉ việc trong giao dịch");
        Chan(()=>QuanLyNhanSu.LuuThongTinTaiKhoan(nv,"minh","Thợ phụ"),"Lưu thông tin tài khoản vẫn chặn tên đăng nhập trùng");
        Dung(GiaTri("SELECT TenDangNhap FROM dbo.NhanVien WHERE MaNhanVien='"+nv+"'").ToString()==tenMoi,"Lỗi sửa tài khoản không thay tên hoặc trạng thái nhân viên");
        using(var f=new frmQuanLyTaiKhoan())
        {
            Hien(f);Lay<TextBox>(f,"txtTimKiem").Text=tenMoi;Lay<Button>(f,"btnTimKiem").PerformClick();
            Dung(Lay<DataGridView>(f,"dgvTaiKhoan").Rows.Count==1&&!Lay<Button>(f,"btnNghiViec").Enabled,"Tài khoản nghỉ việc còn trong bảng và không bị cho nghỉ lần nữa");
            Lay<Button>(f,"btnLuuTaiKhoan").PerformClick();
            Dung(GiaTri("SELECT TrangThai FROM dbo.NhanVien WHERE MaNhanVien='"+nv+"'").ToString()=="Nghỉ việc","Nút lưu tài khoản không vô tình kích hoạt nhân viên đã nghỉ");
        }
        QuanLyNhanSu.LuuTaiKhoan(nv,tenMoi,"","Thợ phụ",true);
        Dung(PhienDangNhap.DangNhap(tenMoi,"123")&&!PhienDangNhap.DangNhap(tenTaiKhoan,"123"),"Đổi tên đăng nhập và kích hoạt lại đúng trạng thái");PhienDangNhap.DangNhap("minh","123");
        string xe=XuLyDuLieu.TaoMaMoi("XE"),ptn=XuLyDuLieu.TaoMaMoi("PTN"),kh=GiaTri("SELECT TOP(1) MaKhachHang FROM dbo.KhachHang ORDER BY MaKhachHang").ToString();
        dl.CapNhatDuLieu("INSERT dbo.Xe(MaXe,MaKhachHang,BienXe,LoaiXe,HangSanXuat,MauSac) VALUES(@xe,@kh,'KT-NS',N'Xe số',N'Honda',N'Đen');INSERT dbo.PhieuTiepNhan(MaPhieuTiepNhan,MaXe,MaNhanVien,NgayLap,NgayHen,TrangThai) VALUES(@p,@xe,@nv,CAST(GETDATE() AS DATE),CAST(GETDATE() AS DATE),N'Chờ sửa');INSERT dbo.ChiTietPhanCong(MaNhanVien,MaXe,ThoiGianBatDau,ThoiGianHoanThanh) VALUES(@tho,@xe,GETDATE(),DATEADD(HOUR,1,GETDATE()));",XuLyDuLieu.ThamSo("@xe",xe),XuLyDuLieu.ThamSo("@kh",kh),XuLyDuLieu.ThamSo("@p",ptn),XuLyDuLieu.ThamSo("@nv",chu),XuLyDuLieu.ThamSo("@tho",nv));
        Chan(()=>QuanLyNhanSu.NghiViec(nv),"Thợ còn xe chưa hoàn thành không được nghỉ việc");
        dl.CapNhatDuLieu("DELETE dbo.ChiTietPhanCong WHERE MaXe=@xe;DELETE dbo.PhieuTiepNhan WHERE MaPhieuTiepNhan=@p;DELETE dbo.Xe WHERE MaXe=@xe",XuLyDuLieu.ThamSo("@xe",xe),XuLyDuLieu.ThamSo("@p",ptn));
        var ca=dl.DocBang("SELECT MaCaLam,ThoiGianCaLam,ThoiGianBatDau,ThoiGianKetThuc FROM dbo.CaLam ORDER BY ThoiGianBatDau");Dung(ca.Rows.Count==3,"Bảng công dùng đủ 3 ca từ SQL mới");
        DateTime ngay=new DateTime(2035,1,2);
        foreach(DataRow dong in ca.Rows)ChamCong.LuuCong(nv,ngay,dong["MaCaLam"].ToString(),Convert.ToDecimal(dong["ThoiGianCaLam"]));
        string caDau=ca.Rows[0]["MaCaLam"].ToString();ChamCong.LuuCong(nv,ngay,caDau,4.25m,nv,ngay,caDau);
        Dung(Convert.ToDecimal(GiaTri("SELECT TongThoiGianLamThucTe FROM dbo.ChiTietCaLam WHERE MaNhanVien='"+nv+"' AND NgayLam='20350102' AND MaCaLam='"+caDau+"'"))==4.25m,"Sửa giờ thực tế giữ chính xác phần lẻ");
        Chan(()=>ChamCong.LuuCong(nv,ngay,caDau,4.5m),"Chặn chấm trùng nhân viên, ngày và ca");
        string caHai=ca.Rows[1]["MaCaLam"].ToString();ChamCong.LuuCong(nv,ngay,caHai,0,nv,ngay,caHai);
        Dung(Convert.ToDecimal(GiaTri("SELECT TongThoiGianLamThucTe FROM dbo.ChiTietCaLam WHERE MaNhanVien='"+nv+"' AND NgayLam='20350102' AND MaCaLam='"+caHai+"'"))==0,"Ca vắng lưu 0 giờ, không NULL");
        Chan(()=>ChamCong.LuuCong(nv,ngay,caHai,-1,nv,ngay,caHai),"Chặn giờ thực làm âm");
        Chan(()=>ChamCong.LuuCong(nv,ngay,caHai,24,nv,ngay,caHai),"Chặn tổng giờ công ngày vượt 24 giờ");
        Dung(ChamCong.TinhTienCong(4.25m,8000000,208)==163462&&ChamCong.TinhTienCong(0,8000000,208)==0,"Công theo giờ = lương tháng chia 208, không cộng lại hệ số hay phụ cấp");
        using(var f=new frmBangCong())
        {
            Hien(f);Lay<DateTimePicker>(f,"dtpTuNgay").Value=ngay;Lay<DateTimePicker>(f,"dtpDenNgay").Value=ngay;Lay<ComboBox>(f,"cboLocNhanVien").SelectedValue=nv;
            for(int i=0;i<3;i++){Lay<ComboBox>(f,"cboBangCong").SelectedIndex=i;f.TaiDuLieu();Dung(Lay<DataGridView>(f,"dgvDanhSach").ColumnCount>0,"Bảng công tải mục "+i);}
        }
        using(var f=new frmThongKe())
        {
            Hien(f);Lay<DateTimePicker>(f,"dtpTuNgay").Value=new DateTime(2026,8,1);Lay<DateTimePicker>(f,"dtpDenNgay").Value=new DateTime(2026,10,27);
            for(int i=0;i<7;i++){Lay<ComboBox>(f,"cboBaoCao").SelectedIndex=i;f.TaiDuLieu();Dung(Lay<DataGridView>(f,"dgvDanhSach").ColumnCount>0,"Thống kê tải mục "+i);}
        }
        Dung(PhienDangNhap.DangNhap(tenMoi,"123"),"Đăng nhập thợ để kiểm tra dữ liệu cá nhân");
        using(var f=new frmNhanVien(true))
        {
            Hien(f);
            var tab=Lay<TabControl>(f,"tabNhanSu");
            var nhanVien=Lay<TabPage>(f,"tabNhanVien");
            var bangCong=Lay<TabPage>(f,"tabBangCong");
            Dung(!tab.TabPages.Contains(nhanVien)&&tab.SelectedTab==bangCong&&Lay<Panel>(f,"pnlBangCong").Controls.OfType<frmBangCong>().Count()==1,"Thợ chỉ mở thẻ bảng công cá nhân trong trang nhân sự tích hợp");
            var trangCong=Lay<Panel>(f,"pnlBangCong").Controls.OfType<frmBangCong>().Single();
            Lay<DateTimePicker>(trangCong,"dtpTuNgay").Value=ngay;Lay<DateTimePicker>(trangCong,"dtpDenNgay").Value=ngay;trangCong.TaiDuLieu();
            var bang=Lay<DataGridView>(trangCong,"dgvDanhSach").DataSource as DataTable;
            Dung(bang!=null&&bang.Rows.Count==3&&bang.AsEnumerable().All(d=>d["MaNhanVien"].ToString()==nv),"Bảng công nhúng chỉ tải các ca của tài khoản đang đăng nhập");
        }
        using(var f=new frmBangCong())
        {
            Hien(f);Lay<DateTimePicker>(f,"dtpTuNgay").Value=ngay;Lay<DateTimePicker>(f,"dtpDenNgay").Value=ngay;f.TaiDuLieu();
            var bang=Lay<DataGridView>(f,"dgvDanhSach").DataSource as DataTable;Dung(bang!=null&&bang.Rows.Count==3&&bang.AsEnumerable().All(d=>d["MaNhanVien"].ToString()==nv),"Thợ chỉ xem công của mình");
        }
        PhienDangNhap.DangNhap("minh","123");QuanLyNhanSu.NghiViec(nv);
        Dung(!PhienDangNhap.DangNhap(tenMoi,"123"),"Nút nghỉ việc backend khóa nhân viên đã xong việc");PhienDangNhap.DangNhap("minh","123");
        return so;
    }
}

