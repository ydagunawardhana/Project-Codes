using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Pharmacy_Management_System.AdministratorUC
{
    public partial class UC_Add_User : UserControl
    {
        // Get SQL Connection function.cs
        function fn = new function();
        string query;

        public UC_Add_User()
        {
            InitializeComponent();
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
        }

        // btn Sign up Function in Administrator Add User
        private void btnSignUp_Click(object sender, EventArgs e)
        {
            string UserRole = txtUserRole.Text;
            string name = txtName.Text;
            string DateOfBirth = txtDateofBirth.Text;
            Int64 Mobile;
            bool isValidMobile = Int64.TryParse(txtMobileNumber.Text, out Mobile);
            string Email = txtEmailAddress.Text;
            string Username = txtUsername_UC.Text;
            string Password = txtPassword_UC.Text;

            if (!isValidMobile)
            {
                MessageBox.Show("Fill the Fields!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (fn.UserExists(Username))
            {
                MessageBox.Show("Username already exists!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Data Insert into Users Table in system DataBase (Query)
            try
            {
                string query = "INSERT INTO Users (UserRole, Name, DateOfBirth, Mobile, Email, Username, Password) " +
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

                fn.setData(query, "Sign Up Successful.", parameters);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Show Password Box Function in Admin Add user
        private void CheckbxShowPassword2_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckbxShowPassword2.Checked == true)
            {
                txtPassword_UC.UseSystemPasswordChar = false;
            }
            else
            {
                txtPassword_UC.UseSystemPasswordChar = true;
            }
        }

        // btn Reset Function
        private void btnReset_UC_Click(object sender, EventArgs e)
        {
            clearAll();
        }

        // Clear All Text (Click Reset btn)
        public void clearAll()
        {
            txtUsername_UC.Clear();
            txtPassword_UC.Clear();
            txtDateofBirth.ResetText();
            txtMobileNumber.Clear();
            txtEmailAddress.Clear();
            txtName.Clear();
            txtUserRole.SelectedIndex = -1;
        }

        // Type text Username and Check the Username Exist in Database (Display in Picture Box Correct or Not)
        private void txtUsername_UC_TextChanged(object sender, EventArgs e)
        {
            query = "SELECT * FROM Users WHERE Username= '"+txtUsername_UC.Text+"'";
            DataSet ds = fn.getData(query);

            if (ds.Tables[0].Rows.Count == 0)
            {
                pictureBox1.ImageLocation = @"D:\Pharmacy Management System (GROUP - 57)\Pharmacy Management System Images\Icons\no.png";
                // Picture Location
            }
            else
            {
                pictureBox1.ImageLocation = @"D:\Pharmacy Management System (GROUP - 57)\Pharmacy Management System Images\Icons\yes.png";
                // Picture Location
            }
        }

        private void UC_Add_User_Load(object sender, EventArgs e)
        {

        }
    }
}
