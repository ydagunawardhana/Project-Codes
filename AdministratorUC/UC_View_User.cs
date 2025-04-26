using System;
using System.Data;
using System.Windows.Forms;

namespace Pharmacy_Management_System.AdministratorUC
{
    public partial class UC_View_User : UserControl
    {
        // Get SQL Connection function.cs
        function fn = new function();
        String query;
        String currentUser = "";


        public UC_View_User()
        {
            InitializeComponent();
        }

        // Display Username
        public string ID
        {
            set { currentUser = value; }
        }

        // Dispaly All Users in Database (DataGrid)
        private void UC_View_User_Load(object sender, EventArgs e)
        {
            query = "SELECT * FROM Users WHERE UserRole = 'Administrator'  OR UserRole = 'Pharmacist'";
            DataSet ds= fn.getData(query);
            DataGridView1.DataSource = ds.Tables[0];
        }

        // Type Username and Search 
        private void txtUserName_TextChanged(object sender, EventArgs e)
        {
            query = "SELECT * FROM Users WHERE Username like '" + txtUserName.Text + "%'"; //a%
            DataSet ds = fn.getData(query);
            DataGridView1.DataSource = ds.Tables[0];
        }

        // Click and Select the Data in Grid
        String userName;

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                userName = DataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
            }
            catch { }

        }

        // Delete the Records in Grid Data 
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you Sure?", "Delete Confirmation !", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {

                if (currentUser != userName)
                {
                    query = "DELETE FROM Users WHERE Username='"+userName+"'";
                    fn.setData(query, "User Record Deleted.");
                    UC_View_User_Load(this, null);
                }
                else
                {
                    MessageBox.Show("You are Trying to Delete \n Your Own Profile.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        // Load the information in View User Tab
        private void btnSync_Click(object sender, EventArgs e)
        {
            UC_View_User_Load(this, null);
        }
    }
}
