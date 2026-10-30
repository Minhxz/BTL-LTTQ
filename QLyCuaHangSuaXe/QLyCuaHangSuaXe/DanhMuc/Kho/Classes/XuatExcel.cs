using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace btl.Classes
{
	// Các sheet sẽ ghi ra 1 file Excel (thay cho XLWorkbook của ClosedXML, project chung chạy .NET Framework không có ClosedXML)
	public class SoExcel
	{
		public List<DataTable> Sheet = new List<DataTable>();
		public List<string> TenSheet = new List<string>();
	}

	// Xuất DataTable / DataGridView ra file Excel (.xlsx), file được ghi bằng XuatExcel chung của project
	public static class XuatExcel
	{
		// Hộp chọn chỗ lưu, tên gợi ý có ngày giờ để mỗi lần xuất ra 1 file riêng; bấm Hủy thì trả về ""
		public static string ChonFileLuu(string tenFile)
		{
			using (SaveFileDialog dialog = new SaveFileDialog())
			{
				dialog.Filter = "Excel (*.xlsx)|*.xlsx";
				dialog.FileName = tenFile + "_" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm") + ".xlsx";

				return dialog.ShowDialog() == DialogResult.OK ? dialog.FileName : "";
			}
		}

		// Ghi 1 bảng ra 1 sheet: tiêu đề cột theo tieuDe, dữ liệu lấy theo cot; trả về bảng của sheet để thêm dòng tổng nếu cần
		public static DataTable ThemSheet(SoExcel wb, string tenSheet, DataTable data, string[] cot, string[] tieuDe)
		{
			DataTable ws = new DataTable(tenSheet);

			for (int j = 0; j < tieuDe.Length; j++)
			{
				ws.Columns.Add(tieuDe[j], typeof(object));
			}

			for (int i = 0; i < data.Rows.Count; i++)
			{
				DataRow dong = ws.NewRow();
				for (int j = 0; j < cot.Length; j++)
				{
					object giaTri = data.Rows[i][cot[j]];

					// Ngày chỉ ghi dd/MM/yyyy, không kèm giờ
					dong[j] = giaTri is DateTime ? ((DateTime)giaTri).ToString("dd/MM/yyyy") : giaTri;
				}
				ws.Rows.Add(dong);
			}

			wb.Sheet.Add(ws);
			wb.TenSheet.Add(tenSheet);

			return ws;
		}

		// Cột và tiêu đề lấy theo lưới đang hiện, dữ liệu theo ô tìm
		public static DataTable ThemSheet(SoExcel wb, string tenSheet, DataGridView dgv)
		{
			List<DataGridViewColumn> cot = dgv.Columns.Cast<DataGridViewColumn>().Where(c => c.Visible).OrderBy(c => c.DisplayIndex).ToList();

			return ThemSheet(wb, tenSheet, (DataTable)dgv.DataSource, cot.Select(c => c.DataPropertyName).ToArray(), cot.Select(c => c.HeaderText).ToArray());
		}

		public static void Luu(SoExcel wb, string duongDan)
		{
			try
			{
				QLyCuaHangSuaXe.LopDungChung.XuatExcel.GhiNhieuBang(wb.Sheet.ToArray(), duongDan, wb.TenSheet.ToArray());

				MessageBox.Show("Xuất file thành công!", "Thông báo");
			}
			catch (IOException)
			{
				MessageBox.Show("File đang được mở, hãy đóng file rồi thử lại!", "Thông báo");
			}
			catch (Exception ex)
			{
				MessageBox.Show("Lỗi khi xuất file: " + ex.Message, "Thông báo");
			}
		}
	}
}
