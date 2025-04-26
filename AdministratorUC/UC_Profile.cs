using Google.Protobuf.WellKnownTypes;
using System;
using System.Data;
using System.Windows.Forms;
using static Guna.UI2.Native.WinApi;

namespace Pharmacy_Management_System.AdministratorUC
{
    public partial class UC_Profile : UserControl
    {
        // Get SQL Connection function.cs
        function fn = new function();
        String query;


        public UC_Profile()
        {
            InitializeComponent();
        }

        // Display Login Username in Profile 
        public string ID
        {
            set { UsernameLabel.Text = value; }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {

        }


        // Load the User Information in User Profile (Form Click)
        private void UC_Profile_Enter(object sender, EventArgs e)
        {
            query = "SELECT * FROM Users WHERE Username ='" + UsernameLabel.Text + "'";
            DataSet ds = fn.getData(query);
            txtUserRole.Text = ds.Tables[0].Rows[0][1].ToString();
            txtProfileName.Text = ds.Tables[0].Rows[0][2].ToString();
            txtDob.Text = ds.Tables[0].Rows[0][3].ToString();
            txtMobile.Text = ds.Tables[0].Rows[0][4].ToString();
            txtEmail.Text = ds.Tables[0].Rows[0][5].ToString();
            txtPassword.Text = ds.Tables[0].Rows[0][7].ToString();
        }

        // btn Reset Function
        private void btnReset_Click(object sender, EventArgs e)
        {
            UC_Profile_Enter(this, null);
        }

        // Update the User Profile (Query)
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            String role = txtUserRole.Text;
            String name = txtProfileName.Text;
            String dob = txtDob.Text;
            String mobile = txtMobile.Text;
            String email = txtEmail.Text;
            String username = UsernameLabel.Text;
            String password = txtPassword.Text;

            query = "UPDATE Users SET UserRole = '" + role + "',name = '" + name + "',dateofbirth = '" + dob + "',mobile = " + mobile + ",email = '" + email + "',password = '" + password + "' WHERE Username = '" + username + "' ";
            fn.setData(query, "Profile Update Successful.");
        }

        private void UC_Profile_Load(object sender, EventArgs e)
        {

        }
    }
}
