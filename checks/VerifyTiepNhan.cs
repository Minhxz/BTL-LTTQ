using System;
using System.Data;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Reception = QLyCuaHangSuaXe.DanhMuc.TiepNhan.frmTiepNhan;

// UI regression checks only. They do not create or connect to a database.
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
        if (result == 0) Console.WriteLine("PASS: " + passed + " intake checks; no database was accessed or created.");
        return result;
    }

    private static void CheckWorkflow()
    {
        string customer = "O'Neil [x] 100%_ literal";
        string firstCode;
        string secondCode;
        int initialCount;
        DateTime savedDate;
        using (Reception form = NewForm())
        {
            DataGridView grid = Find<DataGridView>(form, "dgvPhieu");
            initialCount = grid.Rows.Count;
            Assert(initialCount > 0 && grid.ReadOnly && !grid.AllowUserToAddRows && grid.Columns.Count == 7,
                "Saved receipts use a real read-only DataGridView with seven columns and sample rows.");
            Assert(Find<TextBox>(form, "txtMaPhieu").ReadOnly && Find<TextBox>(form, "txtNgayNhan").ReadOnly,
                "Receipt code and time cannot be entered manually.");
            CheckLayout(form);
            SavePreviewImage();

            Click(form, "btnThemMoi");
            string codeBeforeSaving = Find<TextBox>(form, "txtMaPhieu").Text;
            Assert(codeBeforeSaving == "TN-0005" && Find<TextBox>(form, "txtMaPhieu").ReadOnly,
                "Add immediately displays the next automatic code before any information is saved.");
            SaveImage(form, "TiepNhan-PhieuMoi.png");
            Assert(Find<TextBox>(form, "txtBienSoXe").Text == "" && !Find<TextBox>(form, "txtBienSoXe").ReadOnly
                && Find<Button>(form, "btnLuu").Enabled && !grid.Enabled,
                "Add opens empty editable fields while protecting the saved list.");
            SaveWithoutSuccess(form);
            Assert(grid.Rows.Count == initialCount && Find<Button>(form, "btnLuu").Enabled
                && Find<Label>(form, "lblThongBao").Text.Length > 0,
                "Saving without a plate shows validation and leaves the list unchanged.");
            Fill(form, "59-demo-verify-01", customer);
            Find<TextBox>(form, "txtDienThoai").Text = "090abc1234";
            SaveWithoutSuccess(form);
            Assert(grid.Rows.Count == initialCount && Find<Button>(form, "btnLuu").Enabled,
                "A phone number containing letters cannot be saved.");
            Find<TextBox>(form, "txtDienThoai").Text = "123";
            SaveWithoutSuccess(form);
            Assert(grid.Rows.Count == initialCount && Find<Button>(form, "btnLuu").Enabled,
                "An incomplete phone number cannot be saved.");
            Find<TextBox>(form, "txtDienThoai").Text = "0901234567";
            Find<ComboBox>(form, "cboThoPhuTrach").SelectedIndex = 0;
            Find<ComboBox>(form, "cboTrangThai").SelectedItem = "Đang sửa";
            SaveWithoutSuccess(form);
            Assert(grid.Rows.Count == initialCount && Find<Button>(form, "btnLuu").Enabled,
                "A receipt in repair requires an assigned mechanic.");
            Assert(Find<TextBox>(form, "txtMaPhieu").Text == codeBeforeSaving,
                "Failed validation keeps the automatic code shown when Add was clicked.");
            Find<ComboBox>(form, "cboThoPhuTrach").SelectedIndex = 1;
            SaveSuccessfully(form, "Lưu phiếu thành công.");
            DataRow first = CurrentRecord(form);
            firstCode = first["MaPhieu"].ToString();
            savedDate = (DateTime)first["NgayNhan"];
            Assert(grid.Rows.Count == initialCount + 1 && !Find<Button>(form, "btnLuu").Enabled
                && first["BienSoXe"].ToString() == "59-DEMO-VERIFY-01" && first["KhachHang"].ToString() == customer
                && first["TrangThai"].ToString() == "Đang sửa" && first["TenTho"].ToString() != "Chưa phân công"
                && first["TinhTrangXe"].ToString() == "Condition entered only by this test",
                "Save adds all entered details and returns to viewing the saved receipt.");

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
            SelectReceipt(form, firstCode);
            Assert(Find<TextBox>(form, "txtKhachHang").Text == customer,
                "Selecting a grid row loads the correct receipt fields.");
            string prematureSuccess = Confirm(DialogResult.OK, delegate { Click(form, "btnSua"); });
            Assert(prematureSuccess == null && Find<Button>(form, "btnLuu").Enabled,
                "Edit opens the selected receipt without claiming success before Save.");
            Find<TextBox>(form, "txtKhachHang").Text = customer + " edited";
            Find<ComboBox>(form, "cboTrangThai").SelectedItem = "Hoàn thành";
            SaveSuccessfully(form, "Sửa phiếu thành công.");
            DataRow changed = CurrentRecord(form);
            Assert(grid.Rows.Count == initialCount + 2 && changed["MaPhieu"].ToString() == firstCode
                && (DateTime)changed["NgayNhan"] == savedDate && changed["KhachHang"].ToString() == customer + " edited"
                && changed["TrangThai"].ToString() == "Hoàn thành",
                "Edit updates one receipt while preserving its code and intake time.");

            bool found = true;
            foreach (string term in new string[] { "%", "_", "[x]", "o'neil", "59-demo-verify-01", firstCode })
            {
                Search(form, term);
                found &= grid.Rows.Count == 1 && CurrentRecord(form)["MaPhieu"].ToString() == firstCode;
            }
            Assert(found, "Search handles names, plates, codes, case and literal punctuation.");
            Search(form, "No receipt matches this text");
            Assert(grid.Rows.Count == 0 && !Find<Button>(form, "btnSua").Enabled && !Find<Button>(form, "btnXoa").Enabled,
                "No search result disables Edit and Delete.");
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
    }

    private static void SaveSuccessfully(Reception form, string expectedMessage)
    {
        string message = Confirm(DialogResult.OK, delegate { Click(form, "btnLuu"); });
        Assert(message != null && message.Contains(expectedMessage),
            "Successful Save displays the appropriate confirmation: " + expectedMessage);
    }

    private static void SaveWithoutSuccess(Reception form)
    {
        string message = Confirm(DialogResult.OK, delegate { Click(form, "btnLuu"); });
        Assert(message == null && Find<Button>(form, "btnThoat").Enabled,
            "Invalid input produces no success popup and still permits Exit.");
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
            new System.Drawing.Size(1080, 760), new System.Drawing.Size(960, 720), new System.Drawing.Size(1360, 860) })
        {
            form.ClientSize = size;
            Application.DoEvents();
            string[] names = { "txtTimKiem", "btnTimKiem", "txtMaPhieu", "txtNgayNhan", "txtBienSoXe", "cboLoaiXe",
                "txtKhachHang", "txtDienThoai", "cboThoPhuTrach", "cboTrangThai", "rtbTinhTrangXe", "lblThongBao",
                "btnThemMoi", "btnSua", "btnXoa", "btnLuu", "btnThoat", "dgvPhieu" };
            System.Drawing.Rectangle[] bounds = new System.Drawing.Rectangle[names.Length];
            bool fits = form.ClientSize == size;
            for (int i = 0; i < names.Length; i++)
            {
                Control control = Find<Control>(form, names[i]);
                bounds[i] = new System.Drawing.Rectangle(form.PointToClient(control.Parent.PointToScreen(control.Location)), control.Size);
                fits &= control.Visible && control.Width > 0 && control.Height > 0 && form.ClientRectangle.Contains(bounds[i]);
                for (int previous = 0; previous < i; previous++) fits &= !bounds[i].IntersectsWith(bounds[previous]);
            }
            fits &= Find<DataGridView>(form, "dgvPhieu").ClientSize.Height >= 140;
            Assert(fits, "Fields, action buttons and saved list fit without overlaps at " + size.Width + "x" + size.Height + ".");
            if (size.Width == 960) SaveImage(form, "TiepNhan-ThuNho.png");
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
            Click(form, "btnSua");
            SaveImage(form, "TiepNhan-HienTai.png");
            Click(form, "btnThoat");
        }
    }
    private static void SaveImage(Reception form, string fileName)
    {
        string directory = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\docs\tiepnhan"));
        Directory.CreateDirectory(directory);
        string path = Path.Combine(directory, fileName);
        using (System.Drawing.Bitmap image = new System.Drawing.Bitmap(form.Width, form.Height))
        {
            form.DrawToBitmap(image, new System.Drawing.Rectangle(0, 0, form.Width, form.Height));
            using (System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(image))
                RenderRichText(Find<RichTextBox>(form, "rtbTinhTrangXe"), form, graphics);
            image.Save(path, System.Drawing.Imaging.ImageFormat.Png);
        }
        Console.WriteLine("PREVIEW " + path);
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
