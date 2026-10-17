# Hiểu và giải thích code form tiếp nhận

## Chức năng của bài

Form này tiếp nhận xe, ghi thông tin sửa chữa, tính tiền phụ tùng và tiền công, lưu phiếu để mở lại trong lần chạy hiện tại. Khi hoàn tất sửa chữa, form chuẩn bị phiếu cho bộ phận thu ngân. Việc thu tiền và lập hóa đơn thuộc form của thành viên khác.

Chương trình dùng các kiến thức C# và Windows Forms cơ bản: biến, `if`, vòng lặp `for`/`foreach`, hàm, lớp có thuộc tính, `List`, `DataTable` và sự kiện bấm nút.

## Chỉ cần đọc những file nào?

| File | Vai trò |
|---|---|
| `frmTiepNhan.cs` | Các hàm xử lý khi bấm nút; đọc ô nhập, thêm/xóa dòng, tính tiền, lưu và mở phiếu |
| `frmTiepNhan.Designer.cs` | Code Visual Studio sinh để tạo giao diện và đặt tên control; sửa bố cục bằng Designer |
| `frmTiepNhan.resx` | Tài nguyên đi cùng form |
| `PhieuSuaChua.cs` | Lớp gom thông tin của một phiếu để lưu và chuyển cho form khác |
| `Program.cs` | Điểm bắt đầu chương trình, mở màn hình chính `Form1` |

Không cần tạo thêm form khách hàng, form danh sách phiếu hay form nhận thanh toán thử.

## Dữ liệu trên form

`dtPhuTung` và `dtDichVu` là hai `DataTable`. Có thể hiểu mỗi bảng gồm các cột và các dòng, giống một bảng nhỏ đang nằm trong bộ nhớ.

- Một dòng phụ tùng có mã, tên, đơn vị tính, số lượng, đơn giá và thành tiền.
- Một dòng dịch vụ có mã, tên, số lượng, đơn giá và thành tiền.
- `DataGridView.DataSource` trỏ đến `DataTable`, nên bảng trên giao diện hiển thị các dòng dữ liệu đó.
- Tiền dùng kiểu `decimal`, số lượng và số km dùng số nguyên; không lấy chuỗi đã định dạng tiền trên màn hình để tính lại.

`danhSachPhieu` là một `List<PhieuSuaChua>`, nghĩa là danh sách chứa các phiếu đã lưu. Chưa có SQL Server nên danh sách chỉ tồn tại khi form còn mở.

## Thêm, xóa và tính tiền hoạt động thế nào?

Khi bấm **Thêm phụ tùng**, chương trình đọc tên, đơn vị, số lượng và giá từ các control. Sau khi kiểm tra ô nhập, chương trình duyệt các dòng đang có:

- Nếu trùng tên, đơn vị và đơn giá thì cộng số lượng.
- Nếu chưa có dòng phù hợp thì thêm dòng mới.
- Nếu tổng số lượng vượt 999 thì yêu cầu người dùng sửa lại.

Dịch vụ thực hiện tương tự, không có cột đơn vị tính.

Khi bấm **Xóa dòng**, chương trình lấy dòng đang được chọn ở bảng tương ứng và xóa khỏi dữ liệu. Các nút thêm và xóa đều gọi `TinhTongTien()` sau khi thay đổi bảng.

Công thức:

```text
Thành tiền từng dòng = Số lượng × Đơn giá
Tiền phụ tùng       = Cộng thành tiền các dòng phụ tùng
Tiền công           = Cộng thành tiền các dòng dịch vụ
Tổng cộng           = Tiền phụ tùng + Tiền công
```

## Lưu và mở phiếu hoạt động thế nào?

`LayThongTinPhieu()` đọc các ô nhập và hai bảng để tạo một đối tượng `PhieuSuaChua`. Đối tượng này gom thông tin của cả phiếu vào một chỗ.

`LuuPhieu(bool deThanhToan)` kiểm tra thông tin rồi tìm mã phiếu trong danh sách:

- Chưa có mã đó: thêm phiếu mới vào danh sách.
- Đã có mã đó: thay thông tin phiếu đang lưu bằng thông tin vừa sửa.

Tham số `deThanhToan` cho biết đang lưu bình thường hay chuẩn bị thanh toán. Khi chuyển thanh toán, chương trình kiểm tra thêm nội dung sửa chữa và các dòng cần tính tiền.

`CapNhatDanhSachPhieu(...)` đưa các phiếu đã lưu lên `ComboBox`. Hàm `ToString()` trong lớp `PhieuSuaChua` quyết định chữ hiển thị cho mỗi phiếu: mã phiếu, tên khách và biển số xe. Chọn mã rồi bấm **Mở phiếu**, chương trình tìm phiếu và gọi `HienThiPhieu(...)` để điền lại các ô và hai bảng.

`TaoPhieuMoi()` xóa thông tin đang nhập, tạo mã mới, đặt ngày nhận/hẹn trả và đưa tiền về 0. Hàm này không xóa những phiếu đã lưu trong danh sách.

## Những biến đánh dấu thay đổi có tác dụng gì?

`coThayDoi` cho biết phiếu đang nhập đã được sửa nhưng chưa lưu hay chưa. `dangNapDuLieu` giúp việc tự điền phiếu cũ lên giao diện không bị hiểu là người dùng đang sửa. Hai biến này chỉ có giá trị `true` hoặc `false`, dùng trong câu lệnh `if` để quyết định có hỏi lưu trước khi đổi phiếu hay không.

`daKhoiTao` giúp form chỉ chuẩn bị dữ liệu một lần khi mở. `dangChuyenThanhToan` giúp đóng hộp thoại để trả phiếu cho màn hình gọi mà không hỏi đóng mất dữ liệu lần nữa.

## Vì sao phải sao chép hai bảng?

Nếu phiếu đã lưu và phiếu đang sửa cùng dùng một bảng thì khi xóa dòng trên màn hình, dữ liệu đã lưu cũng đổi theo dù chưa bấm **Lưu phiếu**.

`DataTable.Copy()` tạo một bảng riêng có cùng dữ liệu. Hai hàm `LayThongTinPhieu()` và `HienThiPhieu(...)` dùng cách này cho hai bảng, để phiếu đã lưu và phiếu đang sửa không ảnh hưởng lẫn nhau ngoài ý muốn.

## Các hàm quan trọng để tự đọc trong code

| Hàm | Hiểu ngắn gọn |
|---|---|
| `frmTiepNhan_Load`, `TaoBangChiTiet` | Chuẩn bị các cột của hai bảng và tạo phiếu trống khi mở form |
| `btnThemPhuTung_Click`, `btnThemDichVu_Click` | Đọc ô nhập rồi thêm hoặc cộng số lượng vào bảng |
| `btnXoaPhuTung_Click`, `btnXoaDichVu_Click` | Xóa dòng đang chọn |
| `TinhTienBang`, `TinhTongTien` | Duyệt các dòng, cộng tiền rồi hiển thị tổng |
| `KiemTraThongTin` | Trả về chuỗi lỗi đầu tiên; trả chuỗi rỗng nếu hợp lệ |
| `LayThongTinPhieu` | Gom dữ liệu trên form thành một phiếu |
| `LuuPhieu` | Thêm mới hoặc cập nhật phiếu theo mã |
| `HienThiPhieu` | Đưa dữ liệu của phiếu trở lại giao diện |
| `CapNhatDanhSachPhieu` | Cập nhật các mã trong ô chọn phiếu đã lưu |
| `TaoPhieuMoi` | Đặt lại giao diện cho phiếu tiếp theo |
| `ChuanBiChuyenThanhToan` | Kiểm tra, lưu và chuẩn bị dữ liệu bàn giao |

Các hàm có đuôi `_Click` là hàm được gọi khi người dùng bấm nút tương ứng. Visual Studio gắn nút với hàm trong code Designer. Một nút có thể gọi thêm hàm riêng để không phải viết lại cùng một đoạn code nhiều lần.

## Ghép với form thu ngân sau này

`PhieuChuyenThanhToan` là thuộc tính công khai của form tiếp nhận. Nó giữ phiếu đã được chuẩn bị cho thanh toán. `private set` nghĩa là form khác đọc được thông tin này, còn việc gán phiếu do form tiếp nhận thực hiện.

Nếu màn hình chính mở tiếp nhận bằng `ShowDialog()`, bấm **Chuyển thanh toán** sẽ trả về `DialogResult.OK`. Màn hình chính lấy phiếu rồi mở form thu ngân. Trong project chung hiện tại, màn hình chính mở tiếp nhận bằng `Show(this)` nên cửa sổ tiếp nhận vẫn mở và thông báo đã chuẩn bị phiếu; form thu ngân sẽ ghép sau.

Ví dụ bên dưới chỉ dùng sau khi nhóm đã có `FrmThuNgan` nhận một `PhieuSuaChua`; chưa đưa vào dự án vì form thu ngân do bạn khác làm:

```csharp
using (frmTiepNhan formTiepNhan = new frmTiepNhan())
{
    if (formTiepNhan.ShowDialog() == DialogResult.OK)
    {
        PhieuSuaChua phieu = formTiepNhan.PhieuChuyenThanhToan;
        FrmThuNgan formThuNgan = new FrmThuNgan(phieu);
        formThuNgan.ShowDialog();
    }
}
```

Hai người cần thống nhất lớp `PhieuSuaChua` và các cột trong hai bảng. Khi có database, thay các thao tác thêm/tìm/cập nhật trong danh sách bằng câu lệnh SQL; vẫn giữ kiểm tra thông tin và công thức tính tiền.

## Cách tự kiểm tra mình đã hiểu

Hãy đặt điểm dừng ở `btnThemPhuTung_Click` và bấm **Thêm**, rồi nhấn F10 để xem từng bước. Quan sát số lượng và đơn giá trước khi tính thành tiền. Làm tương tự với `LuuPhieu` để thấy khác nhau giữa thêm mã mới và cập nhật mã cũ.

Bạn nên giải thích được bằng lời của mình: dữ liệu đang ở control hay trong bảng; nút vừa bấm gọi hàm nào; hàm thay đổi dữ liệu gì; vì sao phải kiểm tra và sao chép bảng. Không cần học thuộc nguyên văn tài liệu này.


