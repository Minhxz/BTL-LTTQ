# Tiếp nhận và sửa chữa xe

Module thuộc project `QLyCuaHangSuaXe`, namespace `QLyCuaHangSuaXe.DanhMuc.TiepNhan`.

## Mở form

Nhấn F5 trong project chung rồi bấm **Tiếp nhận và sửa chữa xe**. Màn hình chính giữ một cửa sổ tiếp nhận; bấm lại nút sẽ kích hoạt cửa sổ có sẵn.

Trong Solution Explorer, mở **DanhMuc → TiepNhan**, chuột phải **frmTiepNhan.cs → View Designer** hoặc nhấn **Shift + F7** để sửa bằng Toolbox.

## Chạy thử

1. Nhập người tiếp nhận, khách hàng, điện thoại, biển số, dòng xe, tình trạng xe và thợ phụ trách. Hẹn trả phải từ ngày nhận trở đi.
2. Nhập tên phụ tùng, đơn vị, số lượng và giá rồi bấm **Thêm**. Nhập dịch vụ tương tự. Cùng tên, đơn vị và giá thì cộng số lượng; khác giá tạo dòng riêng.
3. Chọn dòng rồi bấm **Xóa dòng** để xóa; tiền tự cập nhật.
4. Bấm **Lưu phiếu**, mã phiếu sẽ xuất hiện trong ô **Phiếu đã lưu**.
5. Bấm **Phiếu mới**, sau đó chọn phiếu cũ và bấm **Mở phiếu** để xem lại trên cùng form. Sửa và lưu sẽ cập nhật theo mã.
6. Điền nội dung sửa chữa, thêm ít nhất một phụ tùng hoặc dịch vụ, rồi bấm **Chuyển thanh toán**. Phiếu được tự lưu với trạng thái **Chờ thanh toán**, thông tin nằm trong `PhieuChuyenThanhToan`.

Chưa ghép form thu ngân nên nút chuyển chỉ thông báo đã chuẩn bị phiếu, giữ cửa sổ tiếp nhận mở. Dữ liệu đang lưu trong bộ nhớ và mất khi đóng cửa sổ tiếp nhận. Chạy lại bắt đầu từ `TN-0001`.

## Các file chính

| File | Vai trò |
|---|---|
| `frmTiepNhan.cs` | Sự kiện nút bấm, kiểm tra, thêm/xóa, tính tiền, lưu/mở và chuẩn bị thanh toán |
| `frmTiepNhan.Designer.cs`, `frmTiepNhan.resx` | Giao diện Toolbox và tài nguyên của form |
| `PhieuSuaChua.cs` | Gom thông tin một phiếu để lưu và truyền giữa các form |

Dùng `DataTable` cho hai bảng chi tiết, `List<PhieuSuaChua>` cho các phiếu đã lưu. Xem [giải thích từng phần](../../../../docs/tiepnhan/HUONG_DAN_GIAI_THICH_CODE.md) và [tên control](../../../../docs/tiepnhan/HUONG_DAN_TOOLBOX.md).

## Ghép form thu ngân sau này

Khi đã có `FrmThuNgan` nhận `PhieuSuaChua`, có thể đổi nút mở trên màn hình chính sang đoạn minh họa sau. Lấy phiếu trước khi giải phóng form tiếp nhận:

```csharp
using (DanhMuc.TiepNhan.frmTiepNhan form = new DanhMuc.TiepNhan.frmTiepNhan())
{
    if (form.ShowDialog(this) == DialogResult.OK)
    {
        DanhMuc.TiepNhan.PhieuSuaChua phieu = form.PhieuChuyenThanhToan;
        using (FrmThuNgan thuNgan = new FrmThuNgan(phieu))
        {
            thuNgan.ShowDialog(this);
        }
    }
}
```

Đây là ví dụ để ghép bài; `FrmThuNgan` chưa tồn tại trong project. Khi có database, nhóm sẽ dùng chung nguồn dữ liệu thay cho danh sách trong bộ nhớ.