using System;
using System.ComponentModel.Design;
using System.Windows.Forms;
using QLyCuaHangSuaXe;
using QLyCuaHangSuaXe.DanhMuc.DangNhap;
using QLyCuaHangSuaXe.DanhMuc.DichVu;
using QLyCuaHangSuaXe.DanhMuc.TiepNhan;

class KiemTraDesigner
{
    [STAThread]
    static int Main()
    {
        try
        {
            Type[] cacForm =
            {
                typeof(frmChinh),
                typeof(frmDangNhap),
                typeof(frmTiepNhan),
                typeof(frmKiemTraTiepNhan),
                typeof(frmDanhMucDichVu),
                typeof(frmQuanLyTaiKhoan),
                typeof(frmDoiMatKhau),
                typeof(frmPhanQuyen),
                typeof(btl.frmKho),
                typeof(btl.frmPhieuNhap),
                typeof(btl.frmNhapPhieuNhap),
                typeof(btl.frmThemNCC)
            };
            foreach (Type kieuForm in cacForm)
                {
                    var beMat = new DesignSurface();
                    beMat.BeginLoad(kieuForm);
                    if (!beMat.IsLoaded || beMat.LoadErrors.Count > 0)
                        throw new Exception(kieuForm.Name + ": " + string.Join("; ", System.Linq.Enumerable.Cast<object>(beMat.LoadErrors)));
                    var khung = beMat.View as Control;
                    if (khung == null)
                        throw new Exception(kieuForm.Name + ": Không tạo được vùng thiết kế.");
                    Console.WriteLine("PASS: Designer " + kieuForm.Name);
                }

            Environment.Exit(0);
            return 0;
        }
        catch (Exception loi)
        {
            Console.Error.WriteLine(loi);
            return 1;
        }
    }
}
