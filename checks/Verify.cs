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
            int receiptCount = FindControl<DataGridView>(first, "dgvPhieu").Rows.Count;
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
                && FindControl<DataGridView>(reopened, "dgvPhieu").Rows.Count == receiptCount,
                "The launcher creates a new reception window and retains the preview receipt list.");
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
                "formDanhMucDichVu", "QLyCuaHangSuaXe.DanhMuc.DichVu.frmDanhMucDichVu");
            FindControl<Button>(services, "btnDong").PerformClick();
            Application.DoEvents();
            Assert(services.IsDisposed && !shell.IsDisposed && shell.Visible,
                "The service Close button returns to the shared main form.");

            CheckAccountFormsWorkflow(shell);

            Assert(!FindControl<Button>(shell, "btnMoKho").Enabled,
                "The missing warehouse module has a disabled launcher.");
            shell.Close();
            Application.DoEvents();
            Assert(shell.IsDisposed, "The shared main form closes normally.");
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

    private static void Assert(bool condition, string description)
    {
        if (!condition) throw new InvalidOperationException(description);
        passed++;
        Console.WriteLine("PASS " + description);
    }
}
