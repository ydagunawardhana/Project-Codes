using System;
using System.Data;
using System.Windows.Forms;

namespace Pharmacy_Management_System.CustomerUC
{
    public partial class UC_C_ViewMedicines: UserControl
    {
        // Get SQL Connection function.cs
        function fn = new function();
        String query;

        public UC_C_ViewMedicines()
        {
            InitializeComponent();
        }

        // Display All Medicines in Database
        private void UC_C_ViewMedicines_Load(object sender, EventArgs e)
        {
            query = "SELECT * FROM Medicine WHERE ExpireDate >= getdate()"; //Valid Medicines
            DataSet ds = fn.getData(query);
            DataGridView2.DataSource = ds.Tables[0];
        }

        // Search the Medicines in Database
        private void txtMedicineName_TextChanged(object sender, EventArgs e)
        {
            query = "SELECT * FROM Medicine WHERE MedicineName like '" + txtMedicineName.Text + "%' AND ExpireDate >= getdate()"; //a%
            DataSet ds = fn.getData(query);
            DataGridView2.DataSource = ds.Tables[0];
        }

        // Select the Medicines in DataGrid
        String MedicineID;

        private void DataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                MedicineID = DataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString();
            }
            catch { }
        }

        // Load the Information in Customer View Medicines Tab
        private void btnSync_Click(object sender, EventArgs e)
        {
            UC_C_ViewMedicines_Load(this, null);
        }
    }
}
