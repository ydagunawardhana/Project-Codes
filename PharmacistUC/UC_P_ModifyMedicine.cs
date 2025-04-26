using System;
using System.Data;
using System.Windows.Forms;

namespace Pharmacy_Management_System.PharmacistUC
{
    public partial class UC_P_ModifyMedicine: UserControl
    {
        // Get SQL Connection function.cs
        function fn = new function();
        String query;

        public UC_P_ModifyMedicine()
        {
            InitializeComponent();
        }

        // Search the Medicines ID and Show all Information in Search Medicine
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtMedicineID.Text != "")
            {
                query = "SELECT * FROM Medicine WHERE MedicineID =  '" + txtMedicineID.Text + "'";
                DataSet ds = fn.getData(query);
                if (ds.Tables[0].Rows.Count != 0)
                {
                    txtMedicineName.Text = ds.Tables[0].Rows[0][2].ToString();
                    txtManufacturingDate.Text = ds.Tables[0].Rows[0][3].ToString();
                    txtMedicineNumber.Text = ds.Tables[0].Rows[0][4].ToString();
                    txtMedicineType.Text = ds.Tables[0].Rows [0][5].ToString();
                    txtTypicalDosage.Text = ds.Tables[0].Rows[0][6].ToString();
                    txtExpireDate.Text = ds.Tables[0].Rows[0][7].ToString();
                    txtAvailableQuantity.Text = ds.Tables[0].Rows[0][8].ToString();
                    txtPricePerUnit.Text = ds.Tables[0].Rows[0][9].ToString();
                }
                else
                {
                    MessageBox.Show("No Medicine With ID : " + txtMedicineID.Text + " Exist.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                clearAll();
            }
        }

        // Clear All Informations 
        private void clearAll()
        {
            txtMedicineID.Clear();
            txtMedicineName.Clear();
            txtManufacturingDate.ResetText();
            txtMedicineNumber.Clear();
            txtMedicineType.SelectedIndex = -1;
            txtTypicalDosage.Clear();
            txtExpireDate.ResetText();
            txtAvailableQuantity.Clear();
            txtPricePerUnit.Clear();

            if(txtAddQuantity.Text != "0")
            {
                txtAddQuantity.Text = "0";
            }
            else
            {
                txtAddQuantity.Text = "0";
            }
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        // Clear type Informations
        private void btnReset_Click(object sender, EventArgs e)
        {
            clearAll();
        }

        // Upadte Medicines Details and Add New Quantity to Stock (Update Database) 
        Int64 totalQuantity;

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            String MedicineName = txtMedicineName.Text;
            DateTime ManufacturingDate = DateTime.Parse(txtManufacturingDate.Text);
            String MedicineNumber = txtMedicineNumber.Text;
            String MedicineType = txtMedicineType.Text;
            String TypicalDosage = txtTypicalDosage.Text;
            DateTime ExpireDate = DateTime.Parse(txtExpireDate.Text);
            Int64 Quantity = Int64.Parse(txtAvailableQuantity.Text);
            Int64 AddQuantity = Int64.Parse(txtAddQuantity.Text);
            Int64 PricePerUnit = Int64.Parse(txtPricePerUnit.Text);

            totalQuantity = Quantity + AddQuantity;

            query = "UPDATE Medicine SET MedicineName = '" + MedicineName + "',ManufacturingDate = '" + ManufacturingDate + "',MedicineNumber = '" + MedicineNumber + "',MedicineType = '" + MedicineType + "',TypicalDosage = '" + TypicalDosage + "',ExpireDate = '" + ExpireDate + "',Quantity = " + totalQuantity + ",PricePerUnit = " + PricePerUnit + " WHERE MedicineID = '" + txtMedicineID.Text + "'";
            fn.setData(query, "Medicine Details Update Successful.");
        }
    }
}
