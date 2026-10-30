using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using GiaoDienNhom = QLyCuaHangSuaXe.LopDungChung.GiaoDien;

namespace btl.Classes
{
	// Giao diện các form kho: dùng chung kiểu của nhóm (nền, khung bo góc, nút, bảng),
	// riêng tiêu đề và độ rộng cột của bảng giữ như thiết kế của form kho
	public static class GiaoDien
	{
		public static readonly Color MauChinh = GiaoDienNhom.Xanh;

		static readonly Font FontThuong = new Font("Segoe UI", 9F);
		static readonly Font FontO = new Font("Segoe UI", 10F);
		static readonly Font FontDam = new Font("Segoe UI", 9.5F, FontStyle.Bold);
		static readonly Font FontTieuDe = new Font("Segoe UI", 18F, FontStyle.Bold);

		// Gọi sau InitializeComponent(); nutChinh là các nút hành động chính (nền xanh, chữ trắng)
		public static void ApDung(Form form, params Button[] nutChinh)
		{
			form.StartPosition = FormStartPosition.CenterScreen;

			// Chụp lại tiêu đề, độ rộng cột trước khi kiểu bảng của nhóm đổi chúng
			List<DataGridView> cacBang = new List<DataGridView>();
			DatFont(form.Controls, cacBang);
			Dictionary<DataGridView, CotThietKe[]> thietKe = new Dictionary<DataGridView, CotThietKe[]>();
			foreach (DataGridView dgv in cacBang)
			{
				thietKe[dgv] = ChupCot(dgv);
			}

			GiaoDienNhom.ApDungMauTiepNhan(form);

			foreach (Button nut in nutChinh)
			{
				nut.BackColor = MauChinh;
				nut.ForeColor = Color.White;
				nut.FlatAppearance.BorderColor = MauChinh;
				nut.Invalidate();
			}

			foreach (DataGridView dgv in cacBang)
			{
				KieuBang(dgv, thietKe[dgv]);
			}
		}

		// Tiêu đề trang như các form của nhóm: chữ in hoa xanh, căn trái, không có dòng mô tả
		public static void TieuDe(Label lblTieuDe, Label lblMoTa)
		{
			lblTieuDe.Font = FontTieuDe;
			lblTieuDe.ForeColor = MauChinh;
			lblTieuDe.TextAlign = ContentAlignment.MiddleLeft;
			lblTieuDe.Padding = new Padding(8, 0, 0, 0);

			lblMoTa.Visible = false;
		}

		// Nhãn cần nhấn mạnh (tổng tiền...): chữ đậm màu xanh
		public static void NhanManh(Label lbl)
		{
			lbl.Font = FontDam;
			lbl.ForeColor = MauChinh;
		}

		// Font chữ cho nhãn, ô nhập; gom các bảng để định dạng sau
		static void DatFont(Control.ControlCollection controls, List<DataGridView> cacBang)
		{
			foreach (Control c in controls)
			{
				if (c is DataGridView dgv)
				{
					cacBang.Add(dgv);
				}
				else if (c is TabControl tab)
				{
					// Tab cỡ như tab của nhóm (form nhân viên)
					tab.Font = FontO;
					tab.SizeMode = TabSizeMode.Fixed;
					tab.ItemSize = new Size(210, 40);
				}
				else if (c is GroupBox)
				{
					// Tiêu đề khung được kiểu chung vẽ đậm theo font của khung
					c.Font = FontO;
				}
				else if (c is TabPage trang)
				{
					// Tắt nền trắng mặc định để thấy màu nền chung
					trang.UseVisualStyleBackColor = false;
				}
				else if (c is DateTimePicker dtp)
				{
					dtp.Font = FontThuong;
					dtp.Format = DateTimePickerFormat.Custom;
					dtp.CustomFormat = "dd/MM/yyyy";
				}
				else if (c is Label || c is TextBoxBase || c is ComboBox || c is NumericUpDown || c is CheckBox || c is RadioButton)
				{
					c.Font = FontThuong;
				}

				// Chỉ đi vào các khung chứa, không đi vào bên trong NumericUpDown, DateTimePicker...
				if (c is GroupBox || c is Panel || c is TabControl)
				{
					DatFont(c.Controls, cacBang);
				}
			}
		}

		class CotThietKe
		{
			public string TieuDe;
			public int Rong;
			public DataGridViewAutoSizeColumnMode CoGian;
			public float TiLe;
		}

		static CotThietKe[] ChupCot(DataGridView dgv)
		{
			CotThietKe[] cot = new CotThietKe[dgv.Columns.Count];
			for (int i = 0; i < cot.Length; i++)
			{
				DataGridViewColumn c = dgv.Columns[i];
				cot[i] = new CotThietKe { TieuDe = c.HeaderText, Rong = c.Width, CoGian = c.AutoSizeMode, TiLe = c.FillWeight };
			}
			return cot;
		}

		static void KieuBang(DataGridView dgv, CotThietKe[] cot)
		{
			dgv.DefaultCellStyle.Font = FontO;
			dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

			// Bảng ít cột thì giãn hết bề ngang như bảng của nhóm (giữ tỉ lệ độ rộng thiết kế);
			// bảng nhiều cột hoặc đã tự chia cột thì giữ độ rộng thiết kế, có thanh cuộn ngang
			bool tuChiaCot = Array.Exists(cot, c => c.CoGian != DataGridViewAutoSizeColumnMode.NotSet);
			bool gianDay = !tuChiaCot && cot.Length <= 7;

			Action datLaiCot = () =>
			{
				dgv.AutoSizeColumnsMode = gianDay ? DataGridViewAutoSizeColumnsMode.Fill : DataGridViewAutoSizeColumnsMode.None;
				for (int i = 0; i < cot.Length && i < dgv.Columns.Count; i++)
				{
					DataGridViewColumn c = dgv.Columns[i];
					c.HeaderText = cot[i].TieuDe;
					c.MinimumWidth = Math.Min(60, cot[i].Rong);
					c.FillWeight = gianDay ? cot[i].Rong : cot[i].TiLe;
					if (!gianDay && c.InheritedAutoSizeMode != DataGridViewAutoSizeColumnMode.Fill)
					{
						c.Width = cot[i].Rong;
					}
				}
			};

			datLaiCot();
			// Kiểu bảng của nhóm đặt lại tên/độ rộng cột mỗi lần nạp dữ liệu, nên đặt lại theo thiết kế sau đó
			dgv.DataBindingComplete += (sender, e) => datLaiCot();

			// Lưới thêm dòng bằng tay (không nạp DataTable): ngày hiện dd/MM/yyyy, tiền hiện 1,200,000
			dgv.CellFormatting += (sender, e) =>
			{
				if (e.Value is DateTime ngay)
				{
					e.Value = ngay.ToString("dd/MM/yyyy");
					e.FormattingApplied = true;
				}
				else if (e.Value is decimal tien)
				{
					e.Value = tien.ToString("N0");
					e.FormattingApplied = true;
				}
			};
		}
	}
}
