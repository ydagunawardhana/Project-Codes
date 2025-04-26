using System;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Pharmacy_Management_System
{
    public partial class Registration : Form
    {
        // Get SQL Connection function.cs
        function fn = new function();
        string query;

        public Registration()
        {
            InitializeComponent();
        }

        // Exit btn Function
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Minimize btn Function
        private void btnMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        // Drag Form
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HTCAPTION = 0x2;
        private object connect;

        [DllImport("User32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("User32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }

        // Reset btn Function
        private void btnReset_UC_Click(object sender, EventArgs e)
        {
            clearAll();
        }
        public void clearAll()
        {
            txtUsername.Clear();
            txtPassword.Clear();
            txtDateofBirth.ResetText();
            txtMobileNumber.Clear();
            txtEmailAddress.Clear();
            txtName.Clear();
            txtUserRole.SelectedIndex = -1;
        }

        // Back to Sign in btn Function
        private void btnBacktoSignIn_Click(object sender, EventArgs e)
        {
            SignIn am = new SignIn();
            am.Show();
            this.Hide();
        }

        // Show Password In Registration form
        private void CheckbxShowPassword3_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckbxShowPassword3.Checked == true)
            {
                txtPassword.UseSystemPasswordChar = false;
            }
            else
            {
                txtPassword.UseSystemPasswordChar = true;
            }
        }

        // Signup btn Function
        private void btnSignUp_Click(object sender, EventArgs e)
        {
            string UserRole = txtUserRole.Text;
            string name = txtName.Text;
            string DateOfBirth = txtDateofBirth.Text;
            Int64 Mobile;
            bool isValidMobile = Int64.TryParse(txtMobileNumber.Text, out Mobile);
            string Email = txtEmailAddress.Text;
            string Username = txtUsername.Text;
            string Password = txtPassword.Text;

            if (!isValidMobile)
            {
                MessageBox.Show("Fill the All Fields!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (fn.UserExists(Username))
            {
                MessageBox.Show("Username already exists!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // Data Insert into Customer Table in System Database (Insert Query)
            try
            {
                string query = "INSERT INTO Customers (UserRole, Name, DateOfBirth, Mobile, Email, Username, Password) " +
                               "VALUES (@UserRole, @Name, @DateOfBirth, @Mobile, @Email, @Username, @Password)";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@UserRole", UserRole),
                    new SqlParameter("@Name", name),
                    new SqlParameter("@DateOfBirth", DateTime.Parse(DateOfBirth)), // Ensure DateTime format
                    new SqlParameter("@Mobile", Mobile),
                    new SqlParameter("@Email", Email),
                    new SqlParameter("@Username", Username),
                    new SqlParameter("@Password", Password)
                };

                fn.setData(query, "Registration Successful.", parameters);

            }

            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // To Switch the SignIn Form
            SignIn am = new SignIn();
            am.Show();
            this.Hide();
        }

    }
}
