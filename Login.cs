using System;
using System.Data;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Pharmacy_Management_System
{
    public partial class SignIn : Form
    {
        // Get SQL Connection function.cs
        function fn = new function();
        String query;
        DataSet ds;

        public SignIn()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        // btn Exit Function
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // btn Reset Function
        private void btnReset_Click(object sender, EventArgs e)
        {
            txtUsername.Clear();
            txtPassword.Clear();
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkbxShowPassword_CheckedChanged(object sender, EventArgs e)
        {

        }

        // btn Minimize Function
        private void btnMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        // Drag Application
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HTCAPTION = 0x2;
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

        // // Show Password Box Function in login Password
        private void CheckbxShowPassword1_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckbxShowPassword1.Checked == true)
            {
                txtPassword.UseSystemPasswordChar = false;
            }
            else
            {
                txtPassword.UseSystemPasswordChar = true;
            }
        }

        private void SignIn_Load(object sender, EventArgs e)
        {
        }

        private void btnSignIn_Click(object sender, EventArgs e)
        {

        }

        // Sign Btn Function
        private void btnSignIn_Click_1(object sender, EventArgs e)
        {
            query = "SELECT * FROM Customers WHERE Username ='" + txtUsername.Text + "' and Password = '" + txtPassword.Text + "'";
            ds = fn.getData(query);
            if (ds.Tables[0].Rows.Count != 0) // Select data from Customer Table
            {
                String role = ds.Tables[0].Rows[0][1].ToString();
                if (role == "Customer")
                {
                    MessageBox.Show("Sign In Sucessfully!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Customer customer = new Customer(txtUsername.Text);
                    customer.Show();
                    this.Hide();
                }
            }
            else
            {
                query = "SELECT * FROM Users WHERE Username ='" + txtUsername.Text + "' and Password = '" + txtPassword.Text + "'";
                ds = fn.getData(query);
                if (ds.Tables[0].Rows.Count != 0) // Select Data from Users Table
                {
                    String role = ds.Tables[0].Rows[0][1].ToString();
                    if (role == "Administrator")
                    {
                        MessageBox.Show("Sign In Sucessfully!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        Administrator administrator2 = new Administrator(txtUsername.Text);
                        administrator2.Show();
                        this.Hide();
                    }
                    else if (role == "Pharmacist")
                    {
                        MessageBox.Show("Sign In Sucessfully!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        Pharmacist pharmacist = new Pharmacist();
                        pharmacist.Show();
                        this.Hide();
                    }
                }
                else
                {
                    MessageBox.Show("Wrong Username or Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                /*if (txtUsername.Text == "yashan" && txtPassword.Text == "1234")
                {
                    MessageBox.Show("Sign In Sucessfully!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Administrator am = new Administrator();
                    am.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Wrong Username or Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }*/
            }
        }
        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        // Create Account btn Function
        private void btnCreateAccount_Click(object sender, EventArgs e)
        {
            Registration registration = new Registration();
            registration.Show();
            this.Hide();
        }
    }
}
