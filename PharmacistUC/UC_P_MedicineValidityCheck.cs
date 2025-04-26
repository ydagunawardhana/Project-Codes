using System;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace Pharmacy_Management_System.PharmacistUC
{
    public partial class UC_P_MedicineValidityCheck: UserControl
    {
        // Get SQL Connection function.cs
        function fn = new function();
        String query;

        public UC_P_MedicineValidityCheck()
        {
            InitializeComponent();
        }

        // Check the Expire Date and select the Medicines Valid, Expired And All Medicines in Database
        private void txtMedicineCheck_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(txtMedicineCheck.SelectedIndex == 0)
            {
                query = "SELECT * FROM Medicine WHERE ExpireDate >= getdate()"; // Valid Medicines
                DataSet ds = fn.getData(query);
                DataGridView3.DataSource = ds.Tables[0];
                SetLabel.Text = "Valid Medicines";
                SetLabel.ForeColor = Color.Black;
            }
           else if(txtMedicineCheck.SelectedIndex == 1)
            {
                query = "SELECT * FROM Medicine WHERE ExpireDate <= getdate()"; // Expire Medicines
                DataSet ds = fn.getData(query);
                DataGridView3.DataSource = ds.Tables[0];
                SetLabel.Text = "Expired Medicines";
                SetLabel.ForeColor = Color.Firebrick;
            }
            else if(txtMedicineCheck.SelectedIndex == 2)
            {
                query = "SELECT * FROM Medicine"; // All Medicines
                DataSet ds = fn.getData(query);
                DataGridView3.DataSource = ds.Tables[0];
                SetLabel.Text = "All Medicines";
                SetLabel.ForeColor = Color.Black;
            }
        }

        // Show the What type of Medicine SELECTED
        private void UC_P_MedicineValidityCheck_Load(object sender, EventArgs e)
        {
            SetLabel.Text = "";
        }
    }
}
