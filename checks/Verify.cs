using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Reflection;
using System.Windows.Forms;
using QLyCuaHangSuaXe.DanhMuc.TiepNhan;
using Form1 = QLyCuaHangSuaXe.DanhMuc.TiepNhan.frmTiepNhan;
using LoginForm = QLyCuaHangSuaXe.DanhMuc.DangNhap.frmDangNhap;

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
            CheckLinkedFormsWorkflow();
            CheckTemporaryLoginWorkflow();
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
        int count = 0;
        foreach (Form form in shell.OwnedForms)
            if (form is Form1 && !form.IsDisposed) count++;
        return count;
    }

    private static void CheckLinkedFormsWorkflow()
    {
        using (QLyCuaHangSuaXe.Form1 shell = new QLyCuaHangSuaXe.Form1())
        {
            PrepareOffscreen(shell);
            shell.Show();
            Form services = CheckOwnedNavigation(shell, "btnMoDanhMucDichVu",
                "formDanhMucDichVu", "DanhMuc.frmDanhMucDichVu");
            FindControl<Button>(services, "btnDong").PerformClick();
            Application.DoEvents();
            Assert(services.IsDisposed && !shell.IsDisposed && shell.Visible,
                "The service Close button returns to the shared main form.");

            CheckAccountFormsWorkflow(shell);

            Form warehouse = CheckOwnedNavigation(shell, "btnMoKho", "formKho", "btl.frmKho");
            Form receipts = CheckOwnedNavigation(warehouse, "btnPhieuNhap",
                "formPhieuNhap", "btl.frmPhieuNhap");
            Form receiptEntry = CheckOwnedNavigation(receipts, "btnThemPhieuNhap",
                "formNhapPhieuNhap", "btl.frmNhapPhieuNhap");
            CheckSupplierDialog(receiptEntry);
            FindControl<Button>(receipts, "btnThoat").PerformClick();
            Application.DoEvents();
            Assert(receipts.IsDisposed && receiptEntry.IsDisposed && warehouse.Visible,
                "Closing receipt management also closes its owned entry form and leaves the warehouse open.");
            FindControl<Button>(warehouse, "btnPhieuNhap").PerformClick();
            Application.DoEvents();
            Form reopenedReceipts = CachedForm(warehouse, "formPhieuNhap");
            Assert(reopenedReceipts != null && !Object.ReferenceEquals(receipts, reopenedReceipts)
                && reopenedReceipts.Visible && Object.ReferenceEquals(reopenedReceipts.Owner, warehouse),
                "The warehouse creates fresh receipt management after its previous instance closes.");
            PrepareOffscreen(reopenedReceipts);
            shell.Close();
            Application.DoEvents();
            Assert(warehouse.IsDisposed && reopenedReceipts.IsDisposed,
                "Closing the shared main form closes the owned warehouse and receipt windows.");
        }
    }

    private static void CheckAccountFormsWorkflow(QLyCuaHangSuaXe.Form1 shell)
    {
        Form login = CheckOwnedNavigation(shell, "btnMoDangNhap",
            "formDangNhap", "QLyCuaHangSuaXe.DanhMuc.DangNhap.frmDangNhap");
        TextBox password = FindControl<TextBox>(login, "txtMatKhau");
        CheckBox showPassword = FindControl<CheckBox>(login, "chkHienMatKhau");
        Assert(password.UseSystemPasswordChar,
            "The linked login form initially masks its password.");
        showPassword.Checked = true;
        Assert(!password.UseSystemPasswordChar,
            "The actual show-password checkbox reveals the login password.");
        showPassword.Checked = false;
        Assert(password.UseSystemPasswordChar,
            "Clearing the show-password checkbox masks the login password again.");

        FindControl<TextBox>(login, "txtTenDangNhap").Text = "minh";
        password.Text = "123";
        FindControl<Button>(login, "btnDangNhap").PerformClick();
        Application.DoEvents();
        Assert(!login.IsDisposed && login.Visible && ((LoginForm)login).TenDangNhap == "minh",
            "Account navigation begins from a successful login that remains open.");

        Form changePassword = CheckOwnedNavigation(login, "btnMoDoiMatKhau",
            "formDoiMatKhau", "QLyCuaHangSuaXe.DanhMuc.DangNhap.frmDoiMatKhau");
        TextBox currentPassword = FindControl<TextBox>(changePassword, "txtMatKhauHienTai");
        TextBox newPassword = FindControl<TextBox>(changePassword, "txtMatKhauMoi");
        TextBox confirmPassword = FindControl<TextBox>(changePassword, "txtXacNhanMatKhauMoi");
        CheckBox showChangedPasswords = FindControl<CheckBox>(changePassword, "chkHienMatKhau");
        Assert(currentPassword.UseSystemPasswordChar && newPassword.UseSystemPasswordChar
            && confirmPassword.UseSystemPasswordChar,
            "Password change initially masks all three password fields.");
        showChangedPasswords.Checked = true;
        Assert(!currentPassword.UseSystemPasswordChar && !newPassword.UseSystemPasswordChar
            && !confirmPassword.UseSystemPasswordChar,
            "The password-change checkbox reveals all three password fields together.");
        showChangedPasswords.Checked = false;
        Assert(currentPassword.UseSystemPasswordChar && newPassword.UseSystemPasswordChar
            && confirmPassword.UseSystemPasswordChar,
            "The password-change checkbox masks all three password fields again.");
        Form accounts = CheckOwnedNavigation(login, "btnMoQuanLyTaiKhoan",
            "formQuanLyTaiKhoan", "QLyCuaHangSuaXe.DanhMuc.DangNhap.frmQuanLyTaiKhoan");

        FindControl<Button>(changePassword, "btnDong").PerformClick();
        FindControl<Button>(accounts, "btnDong").PerformClick();
        Application.DoEvents();
        Assert(changePassword.IsDisposed && accounts.IsDisposed && login.Visible && shell.Visible,
            "Each account child Close button returns to login while leaving the shared main form open.");
        Form reopenedChangePassword = ReopenOwnedForm(login, "btnMoDoiMatKhau", "formDoiMatKhau", changePassword);
        Form reopenedAccounts = ReopenOwnedForm(login, "btnMoQuanLyTaiKhoan", "formQuanLyTaiKhoan", accounts);
        FindControl<Button>(login, "btnThoat").PerformClick();
        Application.DoEvents();
        Assert(login.IsDisposed && reopenedChangePassword.IsDisposed && reopenedAccounts.IsDisposed && shell.Visible,
            "Login Exit closes its owned password and account windows and returns to the shared main form.");
        Form reopenedLogin = ReopenOwnedForm(shell, "btnMoDangNhap", "formDangNhap", login);
        Assert(string.IsNullOrEmpty(((LoginForm)reopenedLogin).TenDangNhap)
            && !FindControl<Button>(reopenedLogin, "btnMoDoiMatKhau").Enabled
            && !FindControl<Button>(reopenedLogin, "btnMoQuanLyTaiKhoan").Enabled,
            "Reopening login starts a fresh unauthenticated form with account navigation disabled.");
        FindControl<Button>(reopenedLogin, "btnThoat").PerformClick();
        Application.DoEvents();
        Assert(reopenedLogin.IsDisposed && shell.Visible,
            "The reopened login Exit button closes only its own window hierarchy.");
    }

    private static Form ReopenOwnedForm(Form owner, string launcherName, string fieldName, Form previous)
    {
        FindControl<Button>(owner, launcherName).PerformClick();
        Application.DoEvents();
        Form reopened = CachedForm(owner, fieldName);
        Assert(reopened != null && !Object.ReferenceEquals(previous, reopened) && !reopened.IsDisposed
            && reopened.Visible && Object.ReferenceEquals(reopened.Owner, owner)
            && CountOwnedForms(owner, previous.GetType()) == 1,
            launcherName + " creates one fresh owned form after its previous instance closes.");
        return reopened;
    }

    private static void CheckTemporaryLoginWorkflow()
    {
        using (QLyCuaHangSuaXe.Form1 shell = new QLyCuaHangSuaXe.Form1())
        {
            PrepareOffscreen(shell);
            shell.Show();
            Button launcher = FindControl<Button>(shell, "btnMoDangNhap");
            launcher.Click += delegate
            {
                Form current = CachedForm(shell, "formDangNhap");
                if (current != null && !current.IsDisposed) PrepareOffscreen(current);
            };
            launcher.PerformClick();
            Application.DoEvents();
            LoginForm login = (LoginForm)CachedForm(shell, "formDangNhap");
            Assert(login != null && login.Visible && Object.ReferenceEquals(login.Owner, shell),
                "The real main-form launcher creates the login form used for authentication.");
            TextBox username = FindControl<TextBox>(login, "txtTenDangNhap");
            TextBox password = FindControl<TextBox>(login, "txtMatKhau");
            Button submit = FindControl<Button>(login, "btnDangNhap");
            CheckBox showPassword = FindControl<CheckBox>(login, "chkHienMatKhau");
            Button changePasswordLauncher = FindControl<Button>(login, "btnMoDoiMatKhau");
            Button accountsLauncher = FindControl<Button>(login, "btnMoQuanLyTaiKhoan");
            Label loginMessage = FindControl<Label>(login, "lblMoTa");
            Label mainStatus = FindControl<Label>(shell, "lblHuongDan");
            string initialStatus = mainStatus.Text;
            int successCount = 0;
            object successSender = null;
            string successUsername = null;
            login.DangNhapThanhCong += delegate(object sender, EventArgs e)
            {
                successCount++;
                successSender = sender;
                successUsername = login.TenDangNhap;
            };
            Assert(!changePasswordLauncher.Enabled && !accountsLauncher.Enabled,
                "Password change and account management are disabled before login.");
            changePasswordLauncher.PerformClick();
            accountsLauncher.PerformClick();
            Application.DoEvents();
            Assert(CachedForm(login, "formDoiMatKhau") == null && CachedForm(login, "formQuanLyTaiKhoan") == null,
                "Disabled account launchers cannot open their forms before login.");

            string missingInput = "Vui lòng nhập tên đăng nhập và mật khẩu.";
            string wrongCredentials = "Tên đăng nhập hoặc mật khẩu không đúng.";
            string[,] rejectedInputs = {
                { "", "", missingInput },
                { "minh", "", missingInput },
                { "", "123", missingInput },
                { "Minh", "123", wrongCredentials },
                { "minh", "123 ", wrongCredentials },
                { "minh", "wrong", wrongCredentials }
            };
            for (int index = 0; index < rejectedInputs.GetLength(0); index++)
            {
                username.Text = rejectedInputs[index, 0];
                password.Text = rejectedInputs[index, 1];
                submit.PerformClick();
                Application.DoEvents();
                Assert(!login.IsDisposed && login.Visible && successCount == 0
                    && string.IsNullOrEmpty(login.TenDangNhap) && mainStatus.Text == initialStatus
                    && !changePasswordLauncher.Enabled && !accountsLauncher.Enabled
                    && loginMessage.Text == rejectedInputs[index, 2],
                    "Rejected login case " + (index + 1) + " stays open, reports its error and keeps account navigation disabled.");
            }

            username.Text = " minh ";
            password.Text = "123";
            submit.PerformClick();
            Application.DoEvents();
            Assert(successCount == 1 && Object.ReferenceEquals(successSender, login) && successUsername == "minh",
                "The valid temporary credentials raise one success event with the normalized username.");
            Assert(!login.IsDisposed && login.Visible && password.Text.Length == 0
                && changePasswordLauncher.Enabled && accountsLauncher.Enabled
                && loginMessage.Text == "Đăng nhập thành công.",
                "Successful login clears the password, stays open and enables both account launchers.");
            Assert(!username.Enabled && !password.Enabled && !showPassword.Enabled && !submit.Enabled,
                "Successful login locks credential input and the login button for the current form session.");
            Assert(!shell.IsDisposed && shell.Visible
                && mainStatus.Text == "Đã đăng nhập: minh (tài khoản tạm)",
                "The real main form handles login success and displays the signed-in temporary account.");
            submit.PerformClick();
            launcher.PerformClick();
            Application.DoEvents();
            Assert(successCount == 1 && Object.ReferenceEquals(login, CachedForm(shell, "formDangNhap"))
                && changePasswordLauncher.Enabled && accountsLauncher.Enabled,
                "Repeated login and main launcher clicks keep the authenticated form and raise no second success event.");

            changePasswordLauncher.Click += delegate
            {
                Form current = CachedForm(login, "formDoiMatKhau");
                if (current != null && !current.IsDisposed) PrepareOffscreen(current);
            };
            accountsLauncher.Click += delegate
            {
                Form current = CachedForm(login, "formQuanLyTaiKhoan");
                if (current != null && !current.IsDisposed) PrepareOffscreen(current);
            };
            changePasswordLauncher.PerformClick();
            accountsLauncher.PerformClick();
            Application.DoEvents();
            Form changePassword = CachedForm(login, "formDoiMatKhau");
            Form accounts = CachedForm(login, "formQuanLyTaiKhoan");
            Assert(changePassword != null && changePassword.Visible && Object.ReferenceEquals(changePassword.Owner, login)
                && accounts != null && accounts.Visible && Object.ReferenceEquals(accounts.Owner, login),
                "Both enabled account launchers open their real owned forms after login.");
            FindControl<Button>(login, "btnThoat").PerformClick();
            Application.DoEvents();
            Assert(login.IsDisposed && changePassword.IsDisposed && accounts.IsDisposed && shell.Visible,
                "Exit closes the authenticated login and its child forms while leaving the main form open.");
            shell.Close();
        }
    }

    private static Form CheckOwnedNavigation(Form owner, string launcherName, string fieldName, string typeName)
    {
        Type formType = typeof(Form1).Assembly.GetType(typeName, true);
        Form child = (Form)Activator.CreateInstance(formType);
        PrepareOffscreen(child);
        RemoveLoadHandlers(child);
        FieldInfo field = owner.GetType().GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic);
        if (field == null) throw new MissingFieldException(owner.GetType().FullName, fieldName);
        field.SetValue(owner, child);
        Button launcher = FindControl<Button>(owner, launcherName);
        launcher.Click += delegate
        {
            Form current = CachedForm(owner, fieldName);
            if (current != null && !current.IsDisposed) PrepareOffscreen(current);
        };
        Assert(launcher.Enabled, launcherName + " is enabled on its parent form.");
        launcher.PerformClick();
        Application.DoEvents();
        Assert(child.Visible && Object.ReferenceEquals(child.Owner, owner) && CountOwnedForms(owner, formType) == 1,
            launcherName + " opens the expected form with the correct owner.");
        launcher.PerformClick();
        Application.DoEvents();
        Assert(Object.ReferenceEquals(child, CachedForm(owner, fieldName)) && CountOwnedForms(owner, formType) == 1,
            launcherName + " reuses its open form without duplicates.");
        child.WindowState = FormWindowState.Minimized;
        launcher.PerformClick();
        Application.DoEvents();
        Assert(child.WindowState == FormWindowState.Normal && child.Visible,
            launcherName + " restores its minimized form.");
        child.Hide();
        launcher.PerformClick();
        Application.DoEvents();
        Assert(child.Visible && Object.ReferenceEquals(child, CachedForm(owner, fieldName))
            && CountOwnedForms(owner, formType) == 1,
            launcherName + " shows its hidden form again without replacing it.");
        return child;
    }

    private static void CheckSupplierDialog(Form receiptEntry)
    {
        using (Timer driver = new Timer())
        {
            Form supplier = null;
            Exception driverError = null;
            driver.Interval = 75;
            driver.Tick += delegate
            {
                foreach (Form openForm in Application.OpenForms)
                    if (openForm.GetType().FullName == "btl.frmThemNCC") supplier = openForm;
                if (supplier == null) return;
                driver.Stop();
                try
                {
                    PrepareOffscreen(supplier);
                    Assert(supplier.Modal && Object.ReferenceEquals(supplier.Owner, receiptEntry),
                        "The supplier button opens the actual supplier dialog owned by receipt entry.");
                }
                catch (Exception error) { driverError = error; }
                finally { supplier.Close(); }
            };
            driver.Start();
            FindControl<Button>(receiptEntry, "btnThemNCC").PerformClick();
            driver.Stop();
            if (driverError != null) throw new InvalidOperationException("Supplier navigation failed.", driverError);
            Assert(supplier != null && supplier.IsDisposed && receiptEntry.Visible && receiptEntry.Enabled,
                "Closing the supplier dialog disposes it and returns control to receipt entry.");
        }
    }

    private static void RemoveLoadHandlers(Form form)
    {
        PropertyInfo eventsProperty = typeof(Component).GetProperty("Events", BindingFlags.Instance | BindingFlags.NonPublic);
        FieldInfo loadKeyField = typeof(Form).GetField("EVENT_LOAD", BindingFlags.Static | BindingFlags.NonPublic);
        if (eventsProperty == null || loadKeyField == null)
            throw new InvalidOperationException("The .NET Framework form Load event could not be isolated for navigation verification.");
        EventHandlerList events = (EventHandlerList)eventsProperty.GetValue(form, null);
        object loadKey = loadKeyField.GetValue(null);
        Delegate handlers = events[loadKey];
        if (handlers != null) events.RemoveHandler(loadKey, handlers);
    }

    private static Form CachedForm(Form owner, string fieldName)
    {
        FieldInfo field = owner.GetType().GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic);
        if (field == null) throw new MissingFieldException(owner.GetType().FullName, fieldName);
        return (Form)field.GetValue(owner);
    }

    private static int CountOwnedForms(Form owner, Type formType)
    {
        int count = 0;
        foreach (Form form in owner.OwnedForms)
            if (form.GetType() == formType && !form.IsDisposed) count++;
        return count;
    }

    private static T FindControl<T>(Form form, string name) where T : Control
    {
        Control[] controls = form.Controls.Find(name, true);
        if (controls.Length != 1 || !(controls[0] is T))
            throw new InvalidOperationException("Expected one " + typeof(T).Name + " named " + name + " on " + form.GetType().FullName + ".");
        return (T)controls[0];
    }

    private static void PrepareOffscreen(Form form)
    {
        form.ShowInTaskbar = false;
        form.StartPosition = FormStartPosition.Manual;
        form.Location = new System.Drawing.Point(-10000, -10000);
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







