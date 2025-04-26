using System;
using System.Data;
using System.Windows.Forms;

namespace Pharmacy_Management_System.PharmacistUC
{
    public partial class UC_P_ViewMedicine: UserControl
    {
        // Get SQL Connection function.cs
        function fn = new function();
        String query;


        public UC_P_ViewMedicine()
        {
            InitializeComponent();
        }

        // Load All Medicines In Database
        private void UC_P_ViewMedicine_Load(object sender, EventArgs e)
        {
            query = "SELECT * FROM Medicine";
            DataSet ds = fn.getData(query);
            DataGridView2.DataSource = ds.Tables[0];
        }

        // Search the Medicines in Medicine Name (Filter)
        private void txtMedicineName_TextChanged(object sender, EventArgs e)
        {
            query = "SELECT * FROM Medicine WHERE MedicineName like '" + txtMedicineName.Text + "%'"; //a%
            DataSet ds = fn.getData(query);
            DataGridView2.DataSource = ds.Tables[0];
        }

        // Select the Medicine in Datagrid
        String MedicineID;

        private void DataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                MedicineID = DataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString();
            }
            catch { }
        }

        //Delete the Medicines in Database (Delete Query)
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you Sure?", "Delete Confirmation !", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                query = "DELETE FROM Medicine WHERE MedicineID = '" + MedicineID + "'";
                fn.setData(query, "Medicine Record Deleted.");
                UC_P_ViewMedicine_Load(this, null);
            }
        }

        // Load Infromation in Pharamcist View Medicines Tab
        private void btnSync_Click(object sender, EventArgs e)
        {
            UC_P_ViewMedicine_Load(this, null);
        }

        private void DataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
