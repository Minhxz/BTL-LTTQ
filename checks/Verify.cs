using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Reflection;
using System.Windows.Forms;
using QLyCuaHangSuaXe.DanhMuc.TiepNhan;
using Form1 = QLyCuaHangSuaXe.DanhMuc.TiepNhan.frmTiepNhan;

// Separate verification tool. This file is not part of the student application.
internal static class Verify
{
    private static int passed;

    [STAThread]
    private static int Main()
    {
        try
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            CheckSharedShellWorkflow();
            CheckTicketTotal();
            CheckSingleFormWorkflow();
            CheckModalPaymentWorkflow();
            CheckModalCloseWithoutPayment();
            Console.WriteLine("PASS: " + passed + " checks; data exists only in this verification process.");
            return 0;
        }
        catch (Exception error)
        {
            Console.Error.WriteLine("FAIL: " + error);
            return 1;
        }
    }

    private static void CheckSharedShellWorkflow()
    {
        using (QLyCuaHangSuaXe.Form1 shell = new QLyCuaHangSuaXe.Form1())
        {
            shell.StartPosition = FormStartPosition.Manual;
            shell.Location = new System.Drawing.Point(-10000, -10000);
            shell.ShowInTaskbar = false;
            shell.Show();
            Control[] launchers = shell.Controls.Find("btnMoTiepNhan", true);
            Assert(launchers.Length == 1 && launchers[0] is Button && launchers[0].Enabled,
                "The shared main form has one enabled reception launcher button.");
            Button launcher = (Button)launchers[0];
            // Runs after the production Click handler, before the next message-loop turn.
            launcher.Click += delegate
            {
                Form1 reception = FindOpenReception(shell);
                if (reception != null)
                {
                    reception.ShowInTaskbar = false;
                    reception.StartPosition = FormStartPosition.Manual;
                    reception.Location = new System.Drawing.Point(-10000, -10000);
                }
            };
            launcher.PerformClick();
            Application.DoEvents();
            Form1 first = FindOpenReception(shell);
            Assert(first != null && first.Visible && CountOpenReceptions(shell) == 1,
                "The actual main-form button opens one modeless reception form.");
            launcher.PerformClick();
            Application.DoEvents();
            Assert(Object.ReferenceEquals(first, FindOpenReception(shell)) && CountOpenReceptions(shell) == 1,
                "Repeated launcher clicks reuse the existing reception instead of opening duplicates.");
            first.Close();
            Application.DoEvents();
            Assert(first.IsDisposed && CountOpenReceptions(shell) == 0,
                "Closing an empty modeless reception disposes the instance.");
            launcher.PerformClick();
            Application.DoEvents();
            Form1 reopened = FindOpenReception(shell);
            Assert(reopened != null && !Object.ReferenceEquals(first, reopened)
                && reopened.Visible && CountOpenReceptions(shell) == 1
                && Get<TextBox>(reopened, "txtKhachHang").Text == ""
                && Tickets(reopened).Count == 0,
                "The launcher creates a fresh empty reception after the previous one was closed.");
            reopened.Close();
            shell.Close();
        }
    }

    private static Form1 FindOpenReception(QLyCuaHangSuaXe.Form1 shell)
    {
        FieldInfo field = typeof(QLyCuaHangSuaXe.Form1).GetField("formTiepNhan", BindingFlags.Instance | BindingFlags.NonPublic);
        Form1 form = (Form1)field.GetValue(shell);
        return form != null && !form.IsDisposed ? form : null;
    }

    private static int CountOpenReceptions(QLyCuaHangSuaXe.Form1 shell)
    {
        // Changing ShowInTaskbar on a visible .NET Framework form recreates its handle.
        // OwnedForms remains reliable after that; Application.OpenForms may drop it.
        int count = 0;
        foreach (Form form in shell.OwnedForms)
            if (form is Form1 && !form.IsDisposed) count++;
        return count;
    }
    private static void CheckTicketTotal()
    {
        PhieuSuaChua ticket = new PhieuSuaChua();
        ticket.TienPhuTung = 325000m;
        ticket.TienCong = 60000m;
        Assert(ticket.TongCong == 385000m, "The ticket total is calculated from parts plus labor.");
    }
    private static void CheckSingleFormWorkflow()
    {
        int formCount = 0;
        foreach (Type type in typeof(Form1).Assembly.GetTypes())
            if (type.IsSubclassOf(typeof(Form)) && type.Namespace == typeof(Form1).Namespace) formCount++;
        Assert(formCount == 1, "The reception module contains exactly one form.");

        using (Form1 form = new Form1())
        {
            Click(form, "frmTiepNhan_Load");
            Application.DoEvents();
            Assert(Get<TextBox>(form, "txtKhachHang").Text == ""
                && Get<TextBox>(form, "txtDienThoai").Text == ""
                && Table(form, "dtPhuTung").Rows.Count == 0
                && Table(form, "dtDichVu").Rows.Count == 0
                && Money(form, "txtTongCong") == 0m,
                "A fresh form starts with empty input and zero totals.");
            Assert(Tickets(form).Count == 0 && Get<ComboBox>(form, "cboPhieuDaLuu").Items.Count == 1,
                "No sample or previously saved tickets are inserted at startup.");

            FillHeader(form);
            Assert((string)Call(form, "KiemTraThongTin", false) == "",
                "A valid reception ticket can be saved before adding billable details.");
            Assert((string)Call(form, "KiemTraThongTin", true) != "",
                "An empty detail table cannot be sent for payment.");
            Get<TextBox>(form, "txtKhachHang").Clear();
            Assert((string)Call(form, "KiemTraThongTin", false) != "",
                "Saving rejects a missing customer name.");
            Get<TextBox>(form, "txtKhachHang").Text = "Customer used only in verification";
            Get<TextBox>(form, "txtDienThoai").Text = "090abc1234";
            Assert((string)Call(form, "KiemTraThongTin", false) != "",
                "Saving rejects a phone number containing letters.");
            Get<TextBox>(form, "txtDienThoai").Text = "0901234567";
            Get<DateTimePicker>(form, "dtpHenTraXe").Value = Get<DateTimePicker>(form, "dtpNgayNhan").Value.AddMinutes(-1);
            Assert((string)Call(form, "KiemTraThongTin", false) != "",
                "Saving rejects a return time before receipt.");
            Get<DateTimePicker>(form, "dtpHenTraXe").Value = Get<DateTimePicker>(form, "dtpNgayNhan").Value.AddHours(2);
            AddPart(form, "Test oil", "Bottle", 2, 120000m);
            AddPart(form, "Test oil", "Bottle", 1, 120000m);
            Assert(Table(form, "dtPhuTung").Rows.Count == 1
                && Convert.ToInt32(Table(form, "dtPhuTung").Rows[0]["SoLuong"]) == 3,
                "Adding the same name, unit and price merges quantities.");
            Assert(Money(form, "txtTienPhuTung") == 360000m,
                "Merged quantity is included in the parts total.");
            AddPart(form, "Test oil", "Bottle", 1, 85000m);
            Assert(Table(form, "dtPhuTung").Rows.Count == 2
                && Money(form, "txtTienPhuTung") == 445000m,
                "The same part with a different price occupies a separate line.");
            AddService(form, "Test service", 1, 20000m);
            AddService(form, "Test service", 1, 20000m);
            Assert(Table(form, "dtDichVu").Rows.Count == 1
                && Convert.ToInt32(Table(form, "dtDichVu").Rows[0]["SoLuong"]) == 2,
                "Repeated services with the same price merge quantities.");
            Assert(Money(form, "txtTienCong") == 40000m && Money(form, "txtTongCong") == 485000m,
                "The displayed grand total equals parts plus services.");

            SelectRow(Get<DataGridView>(form, "dgvPhuTung"), 1);
            Click(form, "btnXoaPhuTung_Click");
            Assert(Table(form, "dtPhuTung").Rows.Count == 1 && Money(form, "txtTongCong") == 400000m,
                "Deleting a selected part updates its table and total.");

            Assert((bool)Call(form, "LuuPhieu", false), "The valid ticket passes save validation.");
            string code = Get<TextBox>(form, "txtMaPhieu").Text;
            Assert(Tickets(form).Count == 1 && Tickets(form)[0].MaPhieu == code
                && Tickets(form)[0].TongCong == 400000m,
                "Saving records the complete current ticket.");
            Assert(Get<ComboBox>(form, "cboPhieuDaLuu").Items.Count == 2,
                "The saved ticket becomes available in the same-form selector.");
            Assert(!Object.ReferenceEquals(Tickets(form)[0].PhuTung, Table(form, "dtPhuTung"))
                && !Object.ReferenceEquals(Tickets(form)[0].DichVu, Table(form, "dtDichVu")),
                "Saved detail tables do not share mutable rows with the editor.");

            Get<TextBox>(form, "txtKhachHang").Text = "Customer edited before resave";
            Assert((bool)Call(form, "LuuPhieu", false), "The valid ticket passes save validation.");
            Assert(Tickets(form).Count == 1 && Tickets(form)[0].KhachHang == "Customer edited before resave",
                "Saving an existing code updates the ticket without duplicating it.");
            Click(form, "btnPhieuMoi_Click");
            Assert(Tickets(form).Count == 1 && Get<TextBox>(form, "txtMaPhieu").Text != code
                && Get<TextBox>(form, "txtKhachHang").Text == ""
                && Table(form, "dtPhuTung").Rows.Count == 0
                && Table(form, "dtDichVu").Rows.Count == 0 && Money(form, "txtTongCong") == 0m,
                "New ticket clears the editor and allocates another code while retaining saved tickets.");

            Get<ComboBox>(form, "cboPhieuDaLuu").SelectedIndex = 1;
            Click(form, "btnMoPhieu_Click");
            Assert(Get<TextBox>(form, "txtMaPhieu").Text == code
                && Get<TextBox>(form, "txtKhachHang").Text == "Customer edited before resave"
                && Get<TextBox>(form, "txtDienThoai").Text == "0901234567"
                && Get<TextBox>(form, "txtBienSoXe").Text == "59-TEST"
                && Table(form, "dtPhuTung").Rows.Count == 1
                && Table(form, "dtDichVu").Rows.Count == 1
                && Money(form, "txtTongCong") == 400000m,
                "Open restores the saved customer, vehicle, detail rows and exact total in the same form.");

            Get<TextBox>(form, "txtNoiDungSuaChua").Clear();
            Assert((string)Call(form, "KiemTraThongTin", true) != "",
                "Payment requires a completed repair description.");
            Get<TextBox>(form, "txtNoiDungSuaChua").Text = "Test completed repair";
            bool ready = (bool)Call(form, "ChuanBiChuyenThanhToan");
            PhieuSuaChua payment = form.PhieuChuyenThanhToan;
            Assert(ready && payment != null && payment.MaPhieu == code
                && payment.KhachHang == "Customer edited before resave"
                && payment.TongCong == 400000m && payment.TrangThai == "Chờ thanh toán",
                "Payment preparation exposes a complete ticket with waiting-for-payment status.");
            Assert(Tickets(form).Count == 1 && Tickets(form)[0].TrangThai == "Chờ thanh toán",
                "Payment preparation automatically saves the ticket's updated status.");
            payment.KhachHang = "Changed only by receiver";
            payment.PhuTung.Rows[0]["SoLuong"] = 99;
            payment.DichVu.Clear();
            Assert(Tickets(form)[0].KhachHang == "Customer edited before resave"
                && Convert.ToInt32(Tickets(form)[0].PhuTung.Rows[0]["SoLuong"]) == 3
                && Table(form, "dtDichVu").Rows.Count == 1,
                "A receiving form cannot change saved/editor data through the transferred ticket.");

            SelectRow(Get<DataGridView>(form, "dgvDichVu"), 0);
            Click(form, "btnXoaDichVu_Click");
            Assert(Table(form, "dtDichVu").Rows.Count == 0 && Money(form, "txtTongCong") == 360000m,
                "Deleting a selected service removes its charge.");
            SelectRow(Get<DataGridView>(form, "dgvPhuTung"), 0);
            Click(form, "btnXoaPhuTung_Click");
            Assert(Table(form, "dtPhuTung").Rows.Count == 0 && Money(form, "txtTongCong") == 0m,
                "Deleting the final detail line resets the amount to zero.");
            Assert(Tickets(form)[0].TongCong == 400000m && Tickets(form)[0].PhuTung.Rows.Count == 1,
                "Unsaved editor changes leave the previous saved ticket intact.");
        }
    }

    private static void CheckModalPaymentWorkflow()
    {
        using (Form1 form = new Form1())
        using (Timer driver = new Timer())
        {
            form.ShowInTaskbar = false;
            form.StartPosition = FormStartPosition.Manual;
            form.Location = new System.Drawing.Point(-10000, -10000);
            Exception driverError = null;
            driver.Interval = 75;
            driver.Tick += delegate
            {
                driver.Stop();
                try
                {
                    FillHeader(form);
                    Get<TextBox>(form, "txtTenPhuTung").Text = "Modal test part";
                    Get<TextBox>(form, "txtDonViTinh").Text = "Bottle";
                    Get<NumericUpDown>(form, "nudSoLuongPhuTung").Value = 2;
                    Get<NumericUpDown>(form, "nudDonGiaPhuTung").Value = 120000m;
                    Get<Button>(form, "btnThemPhuTung").PerformClick();
                    Get<TextBox>(form, "txtTenDichVu").Text = "Modal test service";
                    Get<NumericUpDown>(form, "nudSoLuongDichVu").Value = 1;
                    Get<NumericUpDown>(form, "nudDonGiaDichVu").Value = 60000m;
                    Get<Button>(form, "btnThemDichVu").PerformClick();
                    Get<Button>(form, "btnChuyenThanhToan").PerformClick();
                }
                catch (Exception error)
                {
                    driverError = error;
                    typeof(Form1).GetField("dangChuyenThanhToan", BindingFlags.Instance | BindingFlags.NonPublic)
                        .SetValue(form, true);
                    form.DialogResult = DialogResult.Cancel;
                }
            };
            driver.Start();
            DialogResult result = form.ShowDialog();
            if (driverError != null) throw new InvalidOperationException("Modal button workflow failed.", driverError);
            PhieuSuaChua payment = form.PhieuChuyenThanhToan;
            Assert(result == DialogResult.OK,
                "The actual payment button closes a modal reception form with DialogResult.OK.");
            Assert(payment != null && payment.KhachHang == "Customer used only in verification"
                && payment.DienThoai == "0901234567" && payment.BienSoXe == "59-TEST"
                && payment.NguoiTiepNhan == "Test receptionist" && payment.ThoPhuTrach == "Test mechanic"
                && payment.NoiDungSuaChua == "Test completed repair" && payment.SoKm == 12345,
                "The caller receives the customer, vehicle and repair fields after ShowDialog returns.");
            Assert(payment.PhuTung.Rows.Count == 1 && payment.DichVu.Rows.Count == 1
                && Convert.ToInt32(payment.PhuTung.Rows[0]["SoLuong"]) == 2
                && payment.TienPhuTung == 240000m && payment.TienCong == 60000m
                && payment.TongCong == 300000m && payment.TrangThai == "Chờ thanh toán",
                "The modal handoff contains both detail tables, exact money and payment status.");
        }
    }

    private static void CheckModalCloseWithoutPayment()
    {
        using (Form1 form = new Form1())
        using (Timer driver = new Timer())
        {
            form.ShowInTaskbar = false;
            form.StartPosition = FormStartPosition.Manual;
            form.Location = new System.Drawing.Point(-10000, -10000);
            driver.Interval = 75;
            driver.Tick += delegate
            {
                driver.Stop();
                Get<Button>(form, "btnDong").PerformClick();
            };
            driver.Start();
            DialogResult result = form.ShowDialog();
            Assert(result == DialogResult.Cancel && form.PhieuChuyenThanhToan == null,
                "Closing an empty modal form returns Cancel and no payment ticket.");
        }
    }
    private static void FillHeader(Form1 form)
    {
        Get<TextBox>(form, "txtKhachHang").Text = "Customer used only in verification";
        Get<TextBox>(form, "txtDienThoai").Text = "0901234567";
        Get<TextBox>(form, "txtBienSoXe").Text = "59-TEST";
        Get<TextBox>(form, "txtDongXe").Text = "Test vehicle";
        Get<TextBox>(form, "txtNguoiTiepNhan").Text = "Test receptionist";
        Get<TextBox>(form, "txtThoPhuTrach").Text = "Test mechanic";
        Get<TextBox>(form, "txtTinhTrangXe").Text = "Test initial condition";
        Get<TextBox>(form, "txtNoiDungSuaChua").Text = "Test completed repair";
        Get<NumericUpDown>(form, "nudSoKm").Value = 12345;
        Get<DateTimePicker>(form, "dtpHenTraXe").Value = Get<DateTimePicker>(form, "dtpNgayNhan").Value.AddHours(2);
    }

    private static void AddPart(Form1 form, string name, string unit, int quantity, decimal price)
    {
        Get<TextBox>(form, "txtTenPhuTung").Text = name;
        Get<TextBox>(form, "txtDonViTinh").Text = unit;
        Get<NumericUpDown>(form, "nudSoLuongPhuTung").Value = quantity;
        Get<NumericUpDown>(form, "nudDonGiaPhuTung").Value = price;
        Click(form, "btnThemPhuTung_Click");
    }

    private static void AddService(Form1 form, string name, int quantity, decimal price)
    {
        Get<TextBox>(form, "txtTenDichVu").Text = name;
        Get<NumericUpDown>(form, "nudSoLuongDichVu").Value = quantity;
        Get<NumericUpDown>(form, "nudDonGiaDichVu").Value = price;
        Click(form, "btnThemDichVu_Click");
    }

    private static void SelectRow(DataGridView grid, int index)
    {
        if (grid.Rows.Count <= index) throw new InvalidOperationException("Expected a selectable detail row.");
        grid.ClearSelection();
        grid.CurrentCell = grid.Rows[index].Cells[0];
        grid.Rows[index].Selected = true;
    }

    private static DataTable Table(Form1 form, string name) { return Get<DataTable>(form, name); }
    private static List<PhieuSuaChua> Tickets(Form1 form) { return Get<List<PhieuSuaChua>>(form, "danhSachPhieu"); }

    private static decimal Money(Form1 form, string name)
    {
        string digits = "";
        foreach (char character in Get<TextBox>(form, name).Text)
            if (character >= '0' && character <= '9') digits += character;
        return digits.Length == 0 ? 0m : decimal.Parse(digits, CultureInfo.InvariantCulture);
    }

    private static T Get<T>(Form1 form, string fieldName)
    {
        FieldInfo field = typeof(Form1).GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
        if (field == null) throw new MissingFieldException(typeof(Form1).FullName, fieldName);
        return (T)field.GetValue(form);
    }

    private static void Click(Form1 form, string handler)
    {
        Call(form, handler, form, EventArgs.Empty);
        Application.DoEvents();
    }

    private static object Call(Form1 form, string methodName, params object[] arguments)
    {
        MethodInfo method = typeof(Form1).GetMethod(methodName, BindingFlags.Instance | BindingFlags.NonPublic);
        if (method == null) throw new MissingMethodException(typeof(Form1).FullName, methodName);
        try { return method.Invoke(form, arguments); }
        catch (TargetInvocationException error)
        {
            throw new InvalidOperationException(methodName + " failed.", error.InnerException ?? error);
        }
    }

    private static void Assert(bool condition, string description)
    {
        if (!condition) throw new InvalidOperationException(description);
        passed++;
        Console.WriteLine("PASS " + description);
    }
}







