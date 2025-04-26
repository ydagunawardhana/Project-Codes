using System;
using System.Data;
using System.Windows.Forms;

namespace Pharmacy_Management_System.CustomerUC
{
    public partial class UC_C_OrderHistory : UserControl
    {
        // Get SQL Connection function.cs
        function fn = new function();
        String query;

        public UC_C_OrderHistory()
        {
            InitializeComponent();
        }

        // Load All Medicines In Database
        private void UC_C_OrderHistory_Load(object sender, EventArgs e)
        {
            query = "SELECT * FROM Orders";
            DataSet ds = fn.getData(query);
            DataGridView3.DataSource = ds.Tables[0];
        }

        // Search the Medicines in Medicine Name (Filter)
        private void txtMedicineName_TextChanged(object sender, EventArgs e)
        {
            query = "SELECT * FROM Orders WHERE MedicineName like '" + txtMedicineName.Text + "%'"; //a%
            DataSet ds = fn.getData(query);
            DataGridView3.DataSource = ds.Tables[0];
        }


        // Select the Medicine in Datagrid
        String MedicineID;

        private void DataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                MedicineID = DataGridView3.Rows[e.RowIndex].Cells[1].Value.ToString();
            }
            catch { }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you Sure?", "Delete Confirmation !", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                query = "DELETE FROM Orders WHERE MedicineID = '" + MedicineID + "'";
                fn.setData(query, "Medicine Order History Record Deleted.");
                UC_C_OrderHistory_Load(this, null);
            }
        }

        private void btnSync_Click(object sender, EventArgs e)
        {
            UC_C_OrderHistory_Load(this, null);
        }
    }
}
