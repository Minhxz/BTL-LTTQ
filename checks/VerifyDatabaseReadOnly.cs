using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using QLyCuaHangSuaXe.LopDungChung;
using QLyCuaHangSuaXe.DanhMuc;
using QLyCuaHangSuaXe.DanhMuc.DangNhap;
using QLyCuaHangSuaXe.DanhMuc.TiepNhan;
using QLyCuaHangSuaXe.DanhMuc.DichVu;

class KiemTraDuLieuChiDoc
{
    static readonly XuLyDuLieu dl=new XuLyDuLieu();
    static readonly BindingFlags co=BindingFlags.Instance|BindingFlags.NonPublic;
    static T Lay<T>(object o,string ten)
    {
        for(Type k=o.GetType();k!=null;k=k.BaseType){var f=k.GetField(ten,co);if(f!=null)return (T)f.GetValue(o);}
        throw new Exception("Thiếu trường "+ten);
    }
    static string BaoTen(string ten){return "["+ten.Replace("]","]]")+"]";}
    static Dictionary<string,string> DauVanTay()
    {
        var ketQua=new Dictionary<string,string>();
        foreach(DataRow bang in dl.DocBang("SELECT s.name AS LuocDo,t.name AS Bang,t.object_id FROM sys.tables t JOIN sys.schemas s ON s.schema_id=t.schema_id ORDER BY s.name,t.name").Rows)
        {
            string ten=BaoTen(bang["LuocDo"].ToString())+"."+BaoTen(bang["Bang"].ToString());
            var khoa=dl.DocBang("SELECT c.name FROM sys.indexes i JOIN sys.index_columns ic ON ic.object_id=i.object_id AND ic.index_id=i.index_id JOIN sys.columns c ON c.object_id=ic.object_id AND c.column_id=ic.column_id WHERE i.object_id=@id AND i.is_primary_key=1 ORDER BY ic.key_ordinal",XuLyDuLieu.ThamSo("@id",bang["object_id"]));
            string sapXep=string.Join(",",khoa.AsEnumerable().Select(d=>BaoTen(d[0].ToString())));
            var json=dl.DocBang("SELECT (SELECT * FROM "+ten+(sapXep.Length>0?" ORDER BY "+sapXep:"")+" FOR JSON PATH,INCLUDE_NULL_VALUES) AS DuLieu");
            string noiDung=string.Concat(json.AsEnumerable().Select(d=>d[0].ToString()));
            using(var bam=SHA256.Create())ketQua[ten]=Convert.ToBase64String(bam.ComputeHash(Encoding.UTF8.GetBytes(noiDung)));
        }
        return ketQua;
    }
    static void Chup(Form f,string hauTo="")
    {
        Directory.CreateDirectory("checks/output");
        using(var anh=new Bitmap(f.Width,f.Height)){f.DrawToBitmap(anh,new Rectangle(Point.Empty,anh.Size));anh.Save("checks/output/thuc-"+f.GetType().Name+hauTo+".png");}
    }
    static void Mo(Form f)
    {
        f.StartPosition=FormStartPosition.Manual;f.Location=new Point(-4000,-4000);if(!(f is frmDangNhap)&&!(f is frmThanhToanQR))f.Size=new Size(1280,900);
        if(f is frmKiemTraTiepNhan)((frmKiemTraTiepNhan)f).TaiDuLieu();else if(f is TrangThietKe)((TrangThietKe)f).TaiDuLieu();
        f.Show();Application.DoEvents();Chup(f);Console.WriteLine("PASS đọc SQL: "+f.GetType().Name);
    }
    [STAThread]static int Main()
    {
        try
        {
            var boTao=new System.Data.SqlClient.SqlConnectionStringBuilder(XuLyDuLieu.ChuoiKetNoi);
            if(boTao.InitialCatalog!="QuanLySuaXe")throw new Exception("Bài kiểm tra chỉ đọc phải trỏ database thật QuanLySuaXe.");
            if(!PhienDangNhap.DangNhap("minh","123"))throw new Exception("Không đăng nhập được minh bằng mật khẩu trong SQL mới.");
            Console.WriteLine("PASS: Đăng nhập thật bằng cấu trúc và dữ liệu SQL mới.");
            var truoc=DauVanTay();
            var hoaDonTruoc=dl.DocBang("SELECT COUNT(*) AS SoHoaDon,COALESCE(SUM(TienThanhToan),0) AS TongTien FROM dbo.HoaDon");
            if(Convert.ToInt32(dl.DocBang("SELECT COUNT(*) FROM sys.columns c JOIN sys.tables t ON t.object_id=c.object_id WHERE c.is_nullable=1").Rows[0][0])!=0)throw new Exception("Có cột cho phép NULL trong SQL.");
            Application.EnableVisualStyles();Application.SetCompatibleTextRenderingDefault(false);
            string ptn=dl.DocBang("SELECT TOP(1) MaPhieuTiepNhan FROM dbo.PhieuTiepNhan WHERE TrangThai=N'Hoàn thành' ORDER BY MaPhieuTiepNhan DESC").Rows[0][0].ToString();
            string dichVu=dl.DocBang("SELECT TOP(1) MaDichVu FROM dbo.DichVu ORDER BY MaDichVu").Rows[0][0].ToString();
            var chuaThu=dl.DocBang("SELECT TOP(1) p.MaPhieuTiepNhan FROM dbo.PhieuTiepNhan p WHERE p.TrangThai=N'Hoàn thành' AND NOT EXISTS(SELECT 1 FROM dbo.HoaDon h WHERE h.MaPhieuTiepNhan=p.MaPhieuTiepNhan) ORDER BY p.MaPhieuTiepNhan");
            Form[] cacForm={new frmDangNhap(),new frmTiepNhan(),new frmKiemTraTiepNhan(),new frmDanhMucDichVu(),new frmDinhMuc(dichVu),new btl.frmKho(),new btl.frmPhieuNhap(),new btl.frmNhapPhieuNhap(),new btl.frmThemNCC(),new frmHoaDon(),new frmChiTietHoaDon(ptn),new frmChiTietPhieu(ptn),new frmQuanLyTaiKhoan(),new frmNhanVien(),new frmDoiMatKhau(),new frmPhanQuyen(),new frmPhanCong(),new frmBangCong(),new frmThongKe(),chuaThu.Rows.Count>0?new frmThanhToanQR(chuaThu.Rows[0][0].ToString()):new frmThanhToanQR()};
            foreach(var f in cacForm)using(f)
            {
                Mo(f);
                if(f is frmHoaDon)
                    for(int i=0;i<3;i++){Lay<ComboBox>(f,"cboTrangThaiThanhToan").SelectedIndex=i;((frmHoaDon)f).TaiDuLieu();Chup(f,"-loc"+i);}
                if(f is frmThongKe)
                {
                    Lay<DateTimePicker>(f,"dtpTuNgay").Value=new DateTime(2026,8,1);Lay<DateTimePicker>(f,"dtpDenNgay").Value=new DateTime(2026,10,27);
                    for(int i=0;i<7;i++){Lay<ComboBox>(f,"cboBaoCao").SelectedIndex=i;((frmThongKe)f).TaiDuLieu();Chup(f,"-muc"+i);Console.WriteLine("PASS đọc SQL: Thống kê mục "+i);}
                }
                if(f is frmBangCong)
                {
                    Lay<DateTimePicker>(f,"dtpTuNgay").Value=new DateTime(2026,8,1);Lay<DateTimePicker>(f,"dtpDenNgay").Value=new DateTime(2026,10,27);
                    for(int i=0;i<3;i++){Lay<ComboBox>(f,"cboBangCong").SelectedIndex=i;((frmBangCong)f).TaiDuLieu();Chup(f,"-muc"+i);Console.WriteLine("PASS đọc SQL: Bảng công mục "+i);}
                }
                if(f is frmQuanLyTaiKhoan)
                {
                    Lay<TabControl>(f,"tabTaiKhoan").SelectedIndex=1;Application.DoEvents();Chup(f,"-doi-mat-khau");Console.WriteLine("PASS đọc SQL: Thẻ đổi mật khẩu nằm trong tài khoản.");
                }
            }
            var chiTiet=dl.DocBang("SELECT N'Dịch vụ' AS Loai,d.TenDichVu AS NoiDung,c.SoLuong,CAST(c.ThanhTien/c.SoLuong AS DECIMAL(18,0)) AS DonGia,c.ThanhTien FROM dbo.ChiTietPhieuTiepNhan c JOIN dbo.DichVu d ON d.MaDichVu=c.MaDichVu WHERE c.MaPhieuTiepNhan=@id",XuLyDuLieu.ThamSo("@id",ptn));
            var mau=new MauInPhieu(ptn,chiTiet,"Phiếu tiếp nhận: "+ptn);
            using(var xem=new frmXemBanInPhieu(mau)){Mo(xem);if(Lay<PictureBox>(xem,"picPhieu").Image==null)throw new Exception("Chưa có ảnh xem trước bản in.");}
            using(var chinh=new QLyCuaHangSuaXe.frmChinh(PhienDangNhap.TenDangNhap,PhienDangNhap.VaiTro))
            {
                chinh.WindowState=FormWindowState.Normal;Mo(chinh);
                var menu=Lay<FlowLayoutPanel>(chinh,"flpDieuHuong");
                var cacNut=menu.Controls.OfType<Button>().Where(n=>n.Visible).ToArray();
                var nhanVien=Lay<Button>(chinh,"btnMoNhanVien");
                if(!nhanVien.Visible||cacNut.Any(n=>n.Left!=nhanVien.Left||n.Width!=nhanVien.Width||n.Height!=nhanVien.Height)||cacNut.Any(n=>n.Text.Contains("Bảng công")))
                    throw new Exception("Sidebar chưa thẳng hàng hoặc còn nút bảng công riêng.");
                chinh.Scale(new SizeF(1.25F,1.25F));Application.DoEvents();
                if(cacNut.Any(n=>n.Left!=nhanVien.Left||n.Width!=nhanVien.Width||n.Height!=nhanVien.Height)||menu.HorizontalScroll.Visible)
                    throw new Exception("Sidebar bị lệch hoặc tràn ngang khi phóng giao diện 125%.");
                Console.WriteLine("PASS: Sidebar cùng hàng, cùng kích thước ở 100% và 125%; bảng công gộp trong nhân viên.");
            }
            var sau=DauVanTay();
            if(truoc.Count!=sau.Count||truoc.Any(d=>!sau.ContainsKey(d.Key)||sau[d.Key]!=d.Value))throw new Exception("Dữ liệu thật thay đổi sau khi kiểm tra chỉ đọc.");
            var hoaDonSau=dl.DocBang("SELECT COUNT(*) AS SoHoaDon,COALESCE(SUM(TienThanhToan),0) AS TongTien FROM dbo.HoaDon");
            if(!Equals(hoaDonTruoc.Rows[0][0],hoaDonSau.Rows[0][0])||!Equals(hoaDonTruoc.Rows[0][1],hoaDonSau.Rows[0][1]))throw new Exception("Số hóa đơn hoặc tiền thanh toán thay đổi.");
            Console.WriteLine("PASS: 22 form, 7 thống kê, 3 bảng công; không có cột NULL; SHA-256 toàn bộ "+sau.Count+" bảng giữ nguyên.");
            return 0;
        }
        catch(Exception loi){Console.Error.WriteLine(loi);return 1;}
    }
}
