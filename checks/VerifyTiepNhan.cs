using System;
using System.Data;
using System.Drawing.Printing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Reception = QLyCuaHangSuaXe.DanhMuc.TiepNhan.frmTiepNhan;

// UI regression checks only. They do not connect to a database or send a print job.
internal static class VerifyTiepNhan
{
    private static int passed;

    [STAThread]
    private static int Main()
    {
        int result = 0;
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        using (Form host = new Form())
        {
            PrepareOffscreen(host);
            host.Shown += delegate
            {
                host.BeginInvoke(new MethodInvoker(delegate
                {
                    try { CheckWorkflow(); }
                    catch (Exception error) { Console.Error.WriteLine("FAIL: " + error); result = 1; }
                    finally { host.Close(); }
                }));
            };
            Application.Run(host);
        }
        if (result == 0) Console.WriteLine("PASS: " + passed + " intake checks; no database access or physical printing.");
        return result;
    }

    private static void CheckWorkflow()
    {
        string customer = "O'Neil [x] 100%_ literal";
        string firstCode;
        string secondCode;
        int initialCount;
        DateTime savedDate;
        DateTime savedDue;
        using (Reception form = NewForm())
        {
            DataGridView grid = Find<DataGridView>(form, "dgvPhieu");
            initialCount = grid.Rows.Count;
            string[] columns = { "colMaPhieu", "colNgayNhan", "colBienSoXe", "colKhachHang", "colTenTho", "colHenTra", "colTrangThai", "colNhacHen" };
            bool correctColumns = grid.Columns.Count == columns.Length;
            for (int i = 0; correctColumns && i < columns.Length; i++) correctColumns &= grid.Columns[i].Name == columns[i];
            Assert(initialCount > 0 && grid.ReadOnly && !grid.AllowUserToAddRows && correctColumns,
                "Saved receipts use a read-only grid with eight intake and reminder columns.");
            DataTable original = OriginalTable(form);
            Assert(original.Columns["HenTra"].DataType == typeof(DateTime)
                && original.Columns["DongXe"].DataType == typeof(string) && original.Columns["SoKm"].DataType == typeof(int),
                "Temporary records store the due date, model and odometer in their expected types.");
            Assert(Find<TextBox>(form, "txtMaPhieu").ReadOnly && Find<TextBox>(form, "txtNgayNhan").ReadOnly,
                "Receipt code and time cannot be entered manually.");
            Assert(Find<ComboBox>(form, "cboLocTrangThai").Text == "Tất cả" && Find<Button>(form, "btnInPhieu").Enabled,
                "Opening the form shows all receipts and permits previewing the selected saved receipt.");
            CheckLayout(form);
            SavePreviewImage();

            Click(form, "btnThemMoi");
            string codeBeforeSaving = Find<TextBox>(form, "txtMaPhieu").Text;
            Assert(codeBeforeSaving == "TN-0005" && Find<TextBox>(form, "txtMaPhieu").ReadOnly,
                "Add immediately displays the next automatic code before any information is saved.");
            SaveImage(form, "TiepNhan-PhieuMoi.png");
            Assert(Find<TextBox>(form, "txtBienSoXe").Text == "" && !Find<TextBox>(form, "txtBienSoXe").ReadOnly
                && Find<Button>(form, "btnLuu").Enabled && !grid.Enabled
                && !Find<ComboBox>(form, "cboLocTrangThai").Enabled && !Find<Button>(form, "btnInPhieu").Enabled,
                "Add opens editable fields while protecting the list, filter and print action.");
            SaveInvalid(form, "Vui lòng nhập biển số xe.", "txtBienSoXe");
            Assert(grid.Rows.Count == initialCount && Find<Button>(form, "btnLuu").Enabled
                && Find<Label>(form, "lblThongBao").Text.Length > 0,
                "Saving without a plate shows validation and leaves the list unchanged.");
            Fill(form, "59-demo-verify-01", customer);
            Find<TextBox>(form, "txtDienThoai").Text = "090abc1234";
            SaveInvalid(form, "Số điện thoại chỉ được chứa chữ số.", "txtDienThoai");
            Assert(grid.Rows.Count == initialCount && Find<Button>(form, "btnLuu").Enabled,
                "A phone number containing letters cannot be saved.");
            Find<TextBox>(form, "txtDienThoai").Text = "123";
            SaveInvalid(form, "Số điện thoại phải có từ 8 đến 15 chữ số.", "txtDienThoai");
            Assert(grid.Rows.Count == initialCount && Find<Button>(form, "btnLuu").Enabled,
                "An incomplete phone number cannot be saved.");
            Find<TextBox>(form, "txtDienThoai").Text = "0888123456";
            Find<DateTimePicker>(form, "dtpHenTra").Value = ((DateTime)GetField(form, "ngayNhan")).AddMinutes(-5);
            SaveInvalid(form, "Hẹn trả xe không được trước ngày nhận xe.", "dtpHenTra");
            Assert(grid.Rows.Count == initialCount && Find<Button>(form, "btnLuu").Enabled,
                "An appointment before intake is rejected without changing saved records.");
            Find<DateTimePicker>(form, "dtpHenTra").Value = DateTime.Now.AddHours(3);
            Find<ComboBox>(form, "cboThoPhuTrach").SelectedIndex = 0;
            Find<ComboBox>(form, "cboTrangThai").SelectedItem = "Đang sửa";
            SaveInvalid(form, "Hãy phân công thợ trước khi chuyển sang Đang sửa hoặc Hoàn thành.", "cboThoPhuTrach");
            Assert(grid.Rows.Count == initialCount && Find<Button>(form, "btnLuu").Enabled,
                "A receipt in repair requires an assigned mechanic.");
            Assert(Find<TextBox>(form, "txtMaPhieu").Text == codeBeforeSaving,
                "Failed validation keeps the automatic code shown when Add was clicked.");
            Find<ComboBox>(form, "cboThoPhuTrach").SelectedIndex = 1;
            savedDue = Find<DateTimePicker>(form, "dtpHenTra").Value;
            SaveSuccessfully(form, "Lưu phiếu thành công.");
            DataRow first = CurrentRecord(form);
            firstCode = first["MaPhieu"].ToString();
            savedDate = (DateTime)first["NgayNhan"];
            Assert(grid.Rows.Count == initialCount + 1 && !Find<Button>(form, "btnLuu").Enabled
                && first["BienSoXe"].ToString() == "59-DEMO-VERIFY-01" && first["KhachHang"].ToString() == customer
                && first["TrangThai"].ToString() == "Đang sửa" && first["TenTho"].ToString() != "Chưa phân công"
                && first["TinhTrangXe"].ToString() == "Condition entered only by this test"
                && first["DongXe"].ToString() == "Honda Wave Alpha" && (int)first["SoKm"] == 12345
                && (DateTime)first["HenTra"] == savedDue && first["DienThoai"].ToString() == "0888123456",
                "Save adds all entered details, including model, odometer, phone and appointment.");

            Click(form, "btnThemMoi");
            Assert(firstCode == codeBeforeSaving && Find<TextBox>(form, "txtMaPhieu").Text == "TN-0006",
                "Save keeps the displayed code and the next Add advances to the next code.");
            Fill(form, "59-DEMO-VERIFY-02", "");
            Find<TextBox>(form, "txtDienThoai").Clear();
            Find<ComboBox>(form, "cboThoPhuTrach").SelectedIndex = 0;
            Find<ComboBox>(form, "cboTrangThai").SelectedItem = "Chờ khám";
            SaveSuccessfully(form, "Lưu phiếu thành công.");
            secondCode = CurrentRecord(form)["MaPhieu"].ToString();
            Assert(grid.Rows.Count == initialCount + 2 && secondCode != firstCode,
                "A waiting receipt may omit name, phone and mechanic and receives its own code.");
            Search(form, "59-demo-verify-");
            Filter(form, "Đang sửa");
            Assert(grid.Rows.Count == 1 && CurrentRecord(form)["MaPhieu"].ToString() == firstCode,
                "Changing the status filter immediately combines it with the plate keyword.");
            Filter(form, "Chờ khám");
            Assert(grid.Rows.Count == 1 && CurrentRecord(form)["MaPhieu"].ToString() == secondCode,
                "Changing only the filter refreshes the matching waiting receipt.");
            Filter(form, "Hoàn thành");
            Assert(grid.Rows.Count == 0 && !Find<Button>(form, "btnInPhieu").Enabled,
                "A status with no keyword match has no selected receipt to print.");
            CheckCount(form, 0);
            Filter(form, "Tất cả");
            Assert(grid.Rows.Count == 2, "All statuses still respect the current keyword.");
            Filter(form, "Đang sửa");
            SelectReceipt(form, firstCode);
            Assert(Find<TextBox>(form, "txtKhachHang").Text == customer
                && Find<TextBox>(form, "txtDienThoai").Text == "0888123456"
                && Find<TextBox>(form, "txtDongXe").Text == "Honda Wave Alpha"
                && Find<NumericUpDown>(form, "numSoKm").Value == 12345
                && Find<DateTimePicker>(form, "dtpHenTra").Value == savedDue
                && Find<ComboBox>(form, "cboLoaiXe").Text == "Xe số"
                && Find<RichTextBox>(form, "rtbTinhTrangXe").Text == "Condition entered only by this test",
                "Selection loads visible and hidden receipt details from the original record.");
            string prematureSuccess = Confirm(DialogResult.OK, delegate { Click(form, "btnSua"); });
            Assert(prematureSuccess == null && Find<Button>(form, "btnLuu").Enabled
                && !Find<Button>(form, "btnInPhieu").Enabled && Find<DateTimePicker>(form, "dtpHenTra").Enabled,
                "Edit opens appointment fields and disables printing until changes are saved.");
            Find<TextBox>(form, "txtKhachHang").Text = customer + " edited";
            Find<TextBox>(form, "txtDongXe").Text = "Honda Wave Alpha edited";
            Find<NumericUpDown>(form, "numSoKm").Value = 54321;
            Find<ComboBox>(form, "cboTrangThai").SelectedItem = "Hoàn thành";
            SaveSuccessfully(form, "Sửa phiếu thành công.");
            DataRow changed = CurrentRecord(form);
            Assert(grid.Rows.Count == initialCount + 2 && changed["MaPhieu"].ToString() == firstCode
                && (DateTime)changed["NgayNhan"] == savedDate && changed["KhachHang"].ToString() == customer + " edited"
                && changed["TrangThai"].ToString() == "Hoàn thành" && (DateTime)changed["HenTra"] == savedDue
                && changed["DongXe"].ToString() == "Honda Wave Alpha edited" && (int)changed["SoKm"] == 54321,
                "Edit updates the selected receipt while preserving its code, intake time and appointment.");
            Assert(Find<ComboBox>(form, "cboLocTrangThai").Text == "Tất cả"
                && Find<TextBox>(form, "txtTimKiem").Text == "" && Find<Button>(form, "btnInPhieu").Enabled,
                "Saving clears the previous filters and selects the saved receipt even after its status changes.");
            CheckCount(form, initialCount + 2);

            bool found = true;
            foreach (string term in new string[] { "%", "_", "[x]", "o'neil", "59-demo-verify-01", firstCode, "0888123456" })
            {
                Search(form, term);
                found &= grid.Rows.Count == 1 && CurrentRecord(form)["MaPhieu"].ToString() == firstCode;
            }
            Assert(found, "Search handles names, plates, codes, phones, case and literal punctuation.");
            Search(form, "No receipt matches this text");
            Assert(grid.Rows.Count == 0 && !Find<Button>(form, "btnSua").Enabled && !Find<Button>(form, "btnXoa").Enabled
                && !Find<Button>(form, "btnInPhieu").Enabled,
                "No search result disables Edit, Delete and Print.");
            CheckCount(form, 0);
            Search(form, "");
            Assert(grid.Rows.Count == initialCount + 2, "Clearing search restores the whole saved list.");
            SelectReceipt(form, secondCode);
            Confirm(DialogResult.No, delegate { Click(form, "btnXoa"); });
            Assert(grid.Rows.Count == initialCount + 2, "Declining deletion keeps the receipt.");
            Confirm(DialogResult.Yes, delegate { Click(form, "btnXoa"); });
            Assert(grid.Rows.Count == initialCount + 1 && !GridContains(form, secondCode),
                "Confirming deletion removes only the selected receipt.");
            Assert(form.Controls.Find("btnHuy", true).Length == 0
                && Find<Button>(form, "btnThoat").Enabled && Find<Button>(form, "btnThoat").Text == "Thoát",
                "Exit replaces Cancel and remains available while viewing receipts.");
        }
        using (Reception reopened = NewForm())
        {
            Assert(Find<DataGridView>(reopened, "dgvPhieu").Rows.Count == initialCount && !GridContains(reopened, firstCode),
                "Closing and reopening resets temporary data without claiming database persistence.");
        }
        using (QLyCuaHangSuaXe.Form1 shell = new QLyCuaHangSuaXe.Form1())
        using (Reception reception = new Reception())
        {
            PrepareOffscreen(shell);
            PrepareOffscreen(reception);
            typeof(QLyCuaHangSuaXe.Form1).GetField("formTiepNhan", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(shell, reception);
            shell.Show();
            Click(shell, "btnMoTiepNhan");
            Assert(reception.Visible && Object.ReferenceEquals(reception.Owner, shell),
                "The main form opens intake with the correct owner.");
            Click(shell, "btnMoTiepNhan");
            int count = 0;
            foreach (Form child in shell.OwnedForms) if (child is Reception && !child.IsDisposed) count++;
            Assert(count == 1, "Repeated launcher clicks reuse the same intake window.");
            Click(reception, "btnThoat");
            Assert(reception.IsDisposed && shell.Visible, "Exit closes intake and leaves the main form open.");
        }
        CheckExitWithoutSaving(false);
        CheckExitWithoutSaving(true);
        CheckDeleteFilteredLastRow();
        CheckReminders();
        CheckPrinting();
        CheckPrintPreview();
    }

    private static void SaveSuccessfully(Reception form, string expectedMessage)
    {
        string message = Confirm(DialogResult.OK, delegate { Click(form, "btnLuu"); });
        Assert(message != null && message.Contains(expectedMessage),
            "Successful Save displays the appropriate confirmation: " + expectedMessage);
    }

    private static void SaveInvalid(Reception form, string expectedMessage, string expectedFocus)
    {
        string message = Confirm(DialogResult.OK, delegate { Click(form, "btnLuu"); });
        Assert(message != null && message.Trim() == expectedMessage
            && Find<Control>(form, expectedFocus).Focused && Find<Button>(form, "btnThoat").Enabled,
            "Invalid input displays the error, focuses its field and still permits Exit: " + expectedMessage);
    }

    private static void CheckExitWithoutSaving(bool editExisting)
    {
        using (Reception form = NewForm())
        {
            DataTable original = (DataTable)typeof(Reception).GetField("bangPhieu", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(form);
            int count = original.Rows.Count;
            string originalName = original.Rows[0]["KhachHang"].ToString();
            Click(form, editExisting ? "btnSua" : "btnThemMoi");
            Find<TextBox>(form, "txtKhachHang").Text = "Unsaved value";
            bool enabled = Find<Button>(form, "btnThoat").Enabled;
            string message = Confirm(DialogResult.OK, delegate { Click(form, "btnThoat"); });
            Assert(enabled && form.IsDisposed && message == null && original.Rows.Count == count
                && original.Rows[0]["KhachHang"].ToString() == originalName,
                "Exit closes during " + (editExisting ? "Edit" : "Add") + " without saving unfinished input.");
        }
    }

    private static void CheckDeleteFilteredLastRow()
    {
        using (Reception form = NewForm())
        {
            DataTable original = OriginalTable(form);
            int count = original.Rows.Count;
            string code = original.Rows[0]["MaPhieu"].ToString();
            Filter(form, original.Rows[0]["TrangThai"].ToString());
            Search(form, code);
            Assert(Find<DataGridView>(form, "dgvPhieu").Rows.Count == 1,
                "A status and exact code filter isolate one saved receipt before deletion.");
            Confirm(DialogResult.Yes, delegate { Click(form, "btnXoa"); });
            DataGridView grid = Find<DataGridView>(form, "dgvPhieu");
            Assert(original.Rows.Count == count - 1 && original.Rows.Find(code) == null
                && grid.Rows.Count == 0 && grid.CurrentRow == null
                && Find<TextBox>(form, "txtMaPhieu").Text == "" && Find<TextBox>(form, "txtBienSoXe").Text == ""
                && !Find<Button>(form, "btnSua").Enabled && !Find<Button>(form, "btnXoa").Enabled
                && !Find<Button>(form, "btnInPhieu").Enabled,
                "Deleting the only filtered row safely clears selection, fields and saved-receipt actions.");
            CheckCount(form, 0);
            Filter(form, "Tất cả");
            Search(form, "");
            Assert(grid.Rows.Count == count - 1 && !GridContains(form, code) && Find<Button>(form, "btnInPhieu").Enabled,
                "Clearing filters after deletion restores the remaining receipts and their actions.");
        }
    }

    private static void CheckReminders()
    {
        using (Reception form = NewForm())
        {
            DataRow original = OriginalTable(form).Rows[0];
            string code = original["MaPhieu"].ToString();
            original["NgayNhan"] = DateTime.Now.AddHours(-4);
            original["HenTra"] = DateTime.Now.AddHours(1);
            original["TrangThai"] = "Đang sửa";
            original["TenTho"] = "Nguyễn Văn Hùng";
            Filter(form, "Chờ khám");
            Filter(form, "Tất cả");
            TickReminders(form);
            SelectReceipt(form, code);
            Assert(CurrentRecord(form)["NhacHen"].ToString() == "Đúng hẹn" && !HasWarning(form),
                "An unfinished receipt with a future appointment is on time without a warning.");

            original["HenTra"] = DateTime.Now.AddMinutes(-2);
            TickReminders(form);
            Assert(CurrentRecord(form)["NhacHen"].ToString() == "Quá hẹn" && HasWarning(form),
                "The reminder timer refreshes an elapsed appointment and warns for the selected receipt.");
            SaveImage(form, "TiepNhan-QuaHen.png");

            Click(form, "btnSua");
            string unsavedName = "Input kept while reminders tick";
            Find<TextBox>(form, "txtKhachHang").Text = unsavedName;
            TickReminders(form);
            Assert(Find<Button>(form, "btnLuu").Enabled && Find<TextBox>(form, "txtKhachHang").Text == unsavedName
                && !Find<Button>(form, "btnInPhieu").Enabled,
                "Timer refresh does not replace unfinished input or unlock Print during editing.");
            Find<DateTimePicker>(form, "dtpHenTra").Value = (DateTime)original["HenTra"];
            Find<ComboBox>(form, "cboTrangThai").SelectedItem = "Hoàn thành";
            SaveSuccessfully(form, "Sửa phiếu thành công.");
            TickReminders(form);
            Assert(CurrentRecord(form)["NhacHen"].ToString() == "Đã xong" && !HasWarning(form)
                && (DateTime)CurrentRecord(form)["HenTra"] < DateTime.Now,
                "Completing an overdue receipt clears its warning and keeps the completed reminder after a timer tick.");
            DataGridView grid = Find<DataGridView>(form, "dgvPhieu");
            DataGridViewCellFormattingEventArgs formatting = new DataGridViewCellFormattingEventArgs(
                grid.Columns["colNhacHen"].Index, grid.CurrentRow.Index, "Đã xong", typeof(string), new DataGridViewCellStyle());
            Invoke(form, "dgvPhieu_CellFormatting", grid, formatting);
            Assert(IsGreen(formatting.CellStyle.ForeColor) && IsGreen(formatting.CellStyle.SelectionForeColor),
                "Completed receipts have green text when displayed and selected.");
        }
    }

    private static void TickReminders(Reception form)
    {
        System.Windows.Forms.Timer timer = (System.Windows.Forms.Timer)GetField(form, "timerNhacHen");
        Assert(timer.Enabled && timer.Interval == 30000, "Appointment reminders run every 30 seconds while the form is open.");
        typeof(System.Windows.Forms.Timer).GetMethod("OnTick", BindingFlags.Instance | BindingFlags.NonPublic)
            .Invoke(timer, new object[] { EventArgs.Empty });
        Application.DoEvents();
    }

    private static bool HasWarning(Reception form)
    {
        Label warning = Find<Label>(form, "lblCanhBao");
        return warning.Visible && warning.Text.Contains("Quá hẹn");
    }

    private static bool IsGreen(System.Drawing.Color color)
    {
        return !color.IsEmpty && color.G > color.R && color.G > color.B;
    }

    private static void CheckPrinting()
    {
        using (Reception form = NewForm())
        {
            DataRow original = OriginalTable(form).Rows[0];
            string code = original["MaPhieu"].ToString();
            SelectReceipt(form, code);
            Click(form, "btnSua");
            Find<TextBox>(form, "txtBienSoXe").Text = "UNSAVED-PRINT-INPUT";
            Find<TextBox>(form, "txtKhachHang").Text = "Unsaved customer must not appear in receipt";

            PrintEventArgs begin = new PrintEventArgs();
            Invoke(form, "inPhieu_BeginPrint", null, begin);
            string text = (string)GetField(form, "noiDungIn");
            Assert(!begin.Cancel && text.Contains(code) && text.Contains(original["BienSoXe"].ToString())
                && text.Contains(original["KhachHang"].ToString()) && text.Contains(original["DienThoai"].ToString())
                && text.Contains(original["DongXe"].ToString()) && !text.Contains("UNSAVED-PRINT-INPUT")
                && !text.Contains("Unsaved customer must not appear in receipt") && (int)GetField(form, "viTriIn") == 0,
                "Printing snapshots saved receipt details and ignores unfinished control values.");
            Assert(RenderPrintPages(form, "TiepNhan-InPhieu", 1) == 1,
                "A normal receipt renders to one bitmap page without a physical print job.");

            string previousCondition = original["TinhTrangXe"].ToString();
            StringBuilder longCondition = new StringBuilder();
            for (int line = 1; line <= 140; line++)
                longCondition.AppendLine("Kiểm tra tình trạng xe dòng " + line + ": phanh, lốp và động cơ cần được kiểm tra đầy đủ.");
            original["TinhTrangXe"] = longCondition.ToString();
            try
            {
                Invoke(form, "inPhieu_BeginPrint", null, new PrintEventArgs());
                Assert(RenderPrintPages(form, "TiepNhan-InPhieu-Dai", 12) > 1,
                    "Long receipt contents advance across pages and terminate instead of repeating the first page.");
            }
            finally { original["TinhTrangXe"] = previousCondition; }
        }

        using (Reception empty = NewForm())
        {
            Search(empty, "No receipt exists for print verification");
            PrintEventArgs begin = new PrintEventArgs();
            Invoke(empty, "inPhieu_BeginPrint", null, begin);
            Assert(begin.Cancel && !Find<Button>(empty, "btnInPhieu").Enabled,
                "Printing is cancelled when there is no saved selected receipt.");
        }
    }

    private static void CheckPrintPreview()
    {
        bool validPrinter;
        try { validPrinter = new PrinterSettings().IsValid; }
        catch (InvalidPrinterException) { validPrinter = false; }
        if (!validPrinter)
        {
            Console.WriteLine("SKIP preview dialog: no valid default printer; bitmap print rendering was checked independently.");
            return;
        }
        using (Reception form = NewForm())
        using (System.Windows.Forms.Timer closePreview = new System.Windows.Forms.Timer())
        {
            bool opened = false;
            closePreview.Interval = 100;
            closePreview.Tick += delegate
            {
                foreach (Form open in Application.OpenForms)
                {
                    PrintPreviewDialog preview = open as PrintPreviewDialog;
                    if (preview == null) continue;
                    opened = preview.Document != null && preview.Visible;
                    closePreview.Stop();
                    preview.BeginInvoke(new MethodInvoker(preview.Close));
                    break;
                }
            };
            closePreview.Start();
            Confirm(DialogResult.OK, delegate { Click(form, "btnInPhieu"); });
            closePreview.Stop();
            Assert(opened && !form.IsDisposed && Find<Button>(form, "btnInPhieu").Enabled,
                "The Print button opens and closes a receipt preview without clicking Print or closing intake.");
        }
    }

    private static int RenderPrintPages(Reception form, string filePrefix, int pageLimit)
    {
        string text = (string)GetField(form, "noiDungIn");
        int pageCount = 0;
        bool more;
        do
        {
            int previousPosition = (int)GetField(form, "viTriIn");
            using (System.Drawing.Bitmap image = new System.Drawing.Bitmap(827, 1169))
            using (System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(image))
            {
                graphics.Clear(System.Drawing.Color.White);
                PrintPageEventArgs page = new PrintPageEventArgs(graphics,
                    new System.Drawing.Rectangle(50, 50, 727, 1069),
                    new System.Drawing.Rectangle(0, 0, 827, 1169), new PageSettings());
                Invoke(form, "inPhieu_PrintPage", null, page);
                more = page.HasMorePages;
                pageCount++;
                int position = (int)GetField(form, "viTriIn");
                Assert(position > previousPosition && position <= text.Length,
                    "Rendered receipt page " + pageCount + " advances through the saved print snapshot.");
                int ink = 0;
                for (int y = 0; y < image.Height; y += 4)
                    for (int x = 0; x < image.Width; x += 4)
                        if (image.GetPixel(x, y).ToArgb() != System.Drawing.Color.White.ToArgb()) ink++;
                Assert(ink > 100, "Rendered receipt page " + pageCount + " contains visible text.");
                if (pageCount <= 2)
                {
                    string path = Path.Combine(PreviewDirectory(), filePrefix + "-" + pageCount + ".png");
                    image.Save(path, System.Drawing.Imaging.ImageFormat.Png);
                    Console.WriteLine("PREVIEW " + path);
                }
            }
            if (more && pageCount >= pageLimit) throw new InvalidOperationException("Receipt pagination exceeded " + pageLimit + " pages.");
        } while (more);
        Assert((int)GetField(form, "viTriIn") == text.Length, "The final print page consumes the entire saved receipt.");
        return pageCount;
    }

    private static object GetField(Reception form, string name)
    {
        FieldInfo field = typeof(Reception).GetField(name, BindingFlags.Instance | BindingFlags.NonPublic);
        if (field == null) throw new InvalidOperationException("Expected reception field " + name + ".");
        return field.GetValue(form);
    }

    private static object Invoke(Reception form, string name, params object[] arguments)
    {
        MethodInfo method = typeof(Reception).GetMethod(name, BindingFlags.Instance | BindingFlags.NonPublic);
        if (method == null) throw new InvalidOperationException("Expected reception method " + name + ".");
        return method.Invoke(form, arguments);
    }

    private static DataTable OriginalTable(Reception form) { return (DataTable)GetField(form, "bangPhieu"); }

    private static void CheckCount(Reception form, int expected)
    {
        string text = Find<Label>(form, "lblTongPhieu").Text;
        Assert(System.Text.RegularExpressions.Regex.IsMatch(text, @"\b" + expected + @"\b"),
            "The receipt counter displays the current result count " + expected + ".");
    }

    private static Reception NewForm()
    {
        Reception form = new Reception();
        PrepareOffscreen(form);
        form.Show();
        Application.DoEvents();
        return form;
    }
    private static void Fill(Reception form, string plate, string customer)
    {
        Find<TextBox>(form, "txtBienSoXe").Text = plate;
        Find<TextBox>(form, "txtKhachHang").Text = customer;
        Find<ComboBox>(form, "cboLoaiXe").SelectedItem = "Xe số";
        Find<TextBox>(form, "txtDongXe").Text = "Honda Wave Alpha";
        Find<NumericUpDown>(form, "numSoKm").Value = 12345;
        Find<DateTimePicker>(form, "dtpHenTra").Value = DateTime.Now.AddHours(3);
        Find<RichTextBox>(form, "rtbTinhTrangXe").Text = "Condition entered only by this test";
    }
    private static DataRow CurrentRecord(Reception form)
    {
        DataGridView grid = Find<DataGridView>(form, "dgvPhieu");
        if (grid.CurrentRow == null) throw new InvalidOperationException("Expected a selected receipt.");
        return ((DataRowView)grid.CurrentRow.DataBoundItem).Row;
    }
    private static bool GridContains(Reception form, string code)
    {
        foreach (DataGridViewRow row in Find<DataGridView>(form, "dgvPhieu").Rows)
            if (((DataRowView)row.DataBoundItem)["MaPhieu"].ToString() == code) return true;
        return false;
    }
    private static void SelectReceipt(Reception form, string code)
    {
        DataGridView grid = Find<DataGridView>(form, "dgvPhieu");
        foreach (DataGridViewRow row in grid.Rows)
        {
            if (((DataRowView)row.DataBoundItem)["MaPhieu"].ToString() != code) continue;
            grid.CurrentCell = row.Cells[0];
            row.Selected = true;
            Application.DoEvents();
            return;
        }
        throw new InvalidOperationException("Expected receipt " + code + " in the grid.");
    }
    private static void Search(Reception form, string text) { Find<TextBox>(form, "txtTimKiem").Text = text; Click(form, "btnTimKiem"); }
    private static void Filter(Reception form, string status) { Find<ComboBox>(form, "cboLocTrangThai").SelectedItem = status; Application.DoEvents(); }
    private static void Click(Form form, string name) { Find<Button>(form, name).PerformClick(); Application.DoEvents(); }
    private static void PrepareOffscreen(Form form) { form.ShowInTaskbar = false; form.StartPosition = FormStartPosition.Manual; form.Location = new System.Drawing.Point(-10000, -10000); }
    private static T Find<T>(Form form, string name) where T : Control
    {
        Control[] found = form.Controls.Find(name, true);
        if (found.Length != 1 || !(found[0] is T)) throw new InvalidOperationException("Expected one " + name + " control.");
        return (T)found[0];
    }

    private static void CheckLayout(Reception form)
    {
        System.Drawing.Size original = form.ClientSize;
        foreach (System.Drawing.Size size in new System.Drawing.Size[] {
            new System.Drawing.Size(1080, 800), new System.Drawing.Size(1280, 840), new System.Drawing.Size(1440, 900) })
        {
            form.ClientSize = size;
            Application.DoEvents();
            string[] names = { "txtTimKiem", "btnTimKiem", "cboLocTrangThai", "txtMaPhieu", "txtNgayNhan", "txtBienSoXe", "cboLoaiXe",
                "txtKhachHang", "txtDienThoai", "cboThoPhuTrach", "cboTrangThai", "txtDongXe", "numSoKm", "dtpHenTra",
                "rtbTinhTrangXe", "lblThongBao", "lblCanhBao", "lblTongPhieu",
                "btnThemMoi", "btnSua", "btnXoa", "btnLuu", "btnInPhieu", "btnThoat", "dgvPhieu" };
            System.Drawing.Rectangle[] bounds = new System.Drawing.Rectangle[names.Length];
            bool fits = form.ClientSize == size;
            for (int i = 0; i < names.Length; i++)
            {
                Control control = Find<Control>(form, names[i]);
                bounds[i] = new System.Drawing.Rectangle(form.PointToClient(control.Parent.PointToScreen(control.Location)), control.Size);
                fits &= (control.Visible || names[i] == "lblCanhBao")
                    && control.Width > 0 && control.Height > 0 && form.ClientRectangle.Contains(bounds[i]);
                for (int previous = 0; previous < i; previous++) fits &= !bounds[i].IntersectsWith(bounds[previous]);
            }
            fits &= Find<DataGridView>(form, "dgvPhieu").ClientSize.Height >= 140;
            Assert(fits, "Fields, action buttons and saved list fit without overlaps at " + size.Width + "x" + size.Height + ".");
            if (size.Width == 1080) SaveImage(form, "TiepNhan-ThuNho.png");
        }
        form.ClientSize = original;
        Application.DoEvents();
    }

    // This native rendering is confined to the check program: RichTextBox.DrawToBitmap omits its text.
    private static void SavePreviewImage()
    {
        using (Reception form = NewForm())
        {
            SelectReceipt(form, "TN-0002");
            SaveImage(form, "TiepNhan-HienTai.png");
            Click(form, "btnThoat");
        }
    }
    private static void SaveImage(Reception form, string fileName)
    {
        string path = Path.Combine(PreviewDirectory(), fileName);
        using (System.Drawing.Bitmap image = new System.Drawing.Bitmap(form.Width, form.Height))
        {
            form.DrawToBitmap(image, new System.Drawing.Rectangle(0, 0, form.Width, form.Height));
            using (System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(image))
                RenderRichText(Find<RichTextBox>(form, "rtbTinhTrangXe"), form, graphics);
            image.Save(path, System.Drawing.Imaging.ImageFormat.Png);
        }
        Console.WriteLine("PREVIEW " + path);
    }
    private static string PreviewDirectory()
    {
        string directory = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\docs\tiepnhan"));
        Directory.CreateDirectory(directory);
        return directory;
    }
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    private struct PrintRectangle { public int Left, Top, Right, Bottom; }
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    private struct PrintRange { public IntPtr Device, Target; public PrintRectangle Content, Page; public int FirstCharacter, LastCharacter; }
    [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
    private static extern IntPtr SendMessage(IntPtr window, int message, IntPtr parameter, ref PrintRange range);
    [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
    private static extern IntPtr SendMessage(IntPtr window, int message, IntPtr parameter, IntPtr data);
    private static void RenderRichText(RichTextBox text, Form form, System.Drawing.Graphics graphics)
    {
        System.Drawing.Point location = text.PointToScreen(System.Drawing.Point.Empty);
        float twipsX = 1440F / graphics.DpiX, twipsY = 1440F / graphics.DpiY;
        PrintRectangle content = new PrintRectangle {
            Left = (int)((location.X - form.Left + 2) * twipsX), Top = (int)((location.Y - form.Top + 2) * twipsY),
            Right = (int)((location.X - form.Left + text.ClientSize.Width - 2) * twipsX),
            Bottom = (int)((location.Y - form.Top + text.ClientSize.Height - 2) * twipsY) };
        IntPtr device = graphics.GetHdc();
        try
        {
            PrintRange range = new PrintRange { Device = device, Target = device, Content = content, Page = content, FirstCharacter = 0, LastCharacter = -1 };
            SendMessage(text.Handle, 0x0439, new IntPtr(1), ref range);
        }
        finally { SendMessage(text.Handle, 0x0439, IntPtr.Zero, IntPtr.Zero); graphics.ReleaseHdc(device); }
    }

    private delegate bool WindowCallback(IntPtr window, IntPtr state);
    [System.Runtime.InteropServices.DllImport("user32.dll")] private static extern bool EnumThreadWindows(uint threadId, WindowCallback callback, IntPtr state);
    [System.Runtime.InteropServices.DllImport("kernel32.dll")] private static extern uint GetCurrentThreadId();
    [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Unicode)] private static extern int GetClassName(IntPtr window, StringBuilder name, int capacity);
    [System.Runtime.InteropServices.DllImport("user32.dll")] private static extern IntPtr GetDlgItem(IntPtr window, int id);
    [System.Runtime.InteropServices.DllImport("user32.dll")] private static extern bool PostMessage(IntPtr window, uint message, IntPtr wParam, IntPtr lParam);
    [System.Runtime.InteropServices.DllImport("user32.dll")] private static extern bool EnumChildWindows(IntPtr window, WindowCallback callback, IntPtr state);
    [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Unicode)] private static extern int GetWindowText(IntPtr window, StringBuilder text, int capacity);
    private static string Confirm(DialogResult answer, Action action)
    {
        uint uiThread = GetCurrentThreadId();
        int id = answer == DialogResult.OK ? 1 : answer == DialogResult.Yes ? 6 : 7;
        string observedMessage = null;
        DateTime deadline = DateTime.UtcNow.AddSeconds(5);
        using (System.Threading.Timer timer = new System.Threading.Timer(delegate(object state)
        {
            EnumThreadWindows(uiThread, delegate(IntPtr window, IntPtr ignored)
            {
                StringBuilder name = new StringBuilder(64);
                GetClassName(window, name, name.Capacity);
                if (name.ToString() == "#32770")
                {
                    StringBuilder message = new StringBuilder();
                    EnumChildWindows(window, delegate(IntPtr child, IntPtr unused)
                    {
                        StringBuilder childClass = new StringBuilder(32);
                        GetClassName(child, childClass, childClass.Capacity);
                        if (childClass.ToString() == "Static")
                        {
                            StringBuilder text = new StringBuilder(1024);
                            GetWindowText(child, text, text.Capacity);
                            message.AppendLine(text.ToString());
                        }
                        return true;
                    }, IntPtr.Zero);
                    observedMessage = message.ToString();
                    IntPtr button = GetDlgItem(window, id);
                    if (button != IntPtr.Zero) PostMessage(window, 0x0111, new IntPtr(id), button);
                    else if (DateTime.UtcNow > deadline)
                    {
                        // Unblock an unexpected modal dialog so its failure can be reported.
                        foreach (int fallback in new int[] { 1, 2, 6, 7 })
                        {
                            button = GetDlgItem(window, fallback);
                            if (button == IntPtr.Zero) continue;
                            PostMessage(window, 0x0111, new IntPtr(fallback), button);
                            break;
                        }
                    }
                }
                return true;
            }, IntPtr.Zero);
        }, null, 20, 20)) action();
        return observedMessage;
    }
    private static void Assert(bool condition, string description)
    {
        if (!condition) throw new InvalidOperationException(description);
        passed++;
        Console.WriteLine("PASS " + description);
    }
}
