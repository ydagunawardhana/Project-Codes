using System;
using System.Data;
using System.Windows.Forms;

namespace Pharmacy_Management_System.CustomerUC
{
    public partial class UC_C_Profile: UserControl
    {
        // Get SQL Connection function.cs
        function fn = new function();
        String query;

        public UC_C_Profile()
        {
            InitializeComponent();
        }

        // Display the Customer Username in Profile Label
        public string ID
        {
            set { CustomerNameLabel.Text = value; }
        }

        private void UC_C_Profile_Load(object sender, EventArgs e)
        {

        }

        // Load the Customer Infromation in Profile Tab (Form Click)
        private void UC_C_Profile_Enter(object sender, EventArgs e)
        {
            string query = "SELECT * FROM Customers WHERE Username = '" + CustomerNameLabel.Text + "'";
            DataSet ds = fn.getData(query);
            txtUserRole.Text = ds.Tables[0].Rows[0][1].ToString();
            txtProfileName.Text = ds.Tables[0].Rows[0][2].ToString();
            txtDob.Text = ds.Tables[0].Rows[0][3].ToString();
            txtMobile.Text = ds.Tables[0].Rows[0][4].ToString();
            txtEmail.Text = ds.Tables[0].Rows[0][5].ToString();
            txtPassword.Text = ds.Tables[0].Rows[0][7].ToString();
        }

        // Reset the Entered Data 
        private void btnReset_Click(object sender, EventArgs e)
        {
            UC_C_Profile_Enter(this, null);
        }

        // Update the Customer Profile Informations (Update Database)
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            String role = txtUserRole.Text;
            String name = txtProfileName.Text;
            String dob = txtDob.Text;
            String mobile = txtMobile.Text;
            String email = txtEmail.Text;
            String username = CustomerNameLabel.Text;
            String password = txtPassword.Text;

            query = "UPDATE Customers SET UserRole = '" + role + "',Name = '" + name + "',DateofBirth = '" + dob + "',Mobile = " + mobile + ",Email = '" + email + "',Password = '" + password + "' WHERE Username = '" + username + "' ";
            fn.setData(query, "Profile Update Successful.");
        }
    }
}
