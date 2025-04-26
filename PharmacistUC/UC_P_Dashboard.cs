using System;
using System.Data;
using System.Windows.Forms;

namespace Pharmacy_Management_System.PharmacistUC
{
    public partial class UC_P_Dashboard: UserControl
    {
        // Get SQL Connection function.cs
        function fn = new function();
        string query;
        DataSet ds;
        Int64 count;

        public UC_P_Dashboard()
        {
            InitializeComponent();
        }

        // Load the Bar Graph in Pharmacist Dashboard
        private void UC_P_Dashboard_Load(object sender, EventArgs e)
        {
            loadChart();
        }

        // Check the Expire date And Get the Information Valid and Expired Medicines
        public void loadChart()
        {
            query = "SELECT COUNT (MedicineName) FROM Medicine WHERE ExpireDate >= getdate()";
            ds = fn.getData(query);
            count = Int64.Parse(ds.Tables[0].Rows[0][0].ToString());
            this.chart1.Series["Valid Medicines"].Points.AddXY("Medicine Validity Chart", count);

            
            query = "SELECT COUNT (MedicineName) FROM Medicine WHERE ExpireDate <= getdate()";
            ds = fn.getData(query);
            count = Int64.Parse(ds.Tables[0].Rows[0][0].ToString());
            this.chart1.Series["Expired Medicines"].Points.AddXY("Medicine Validity Chart", count);
        }

        // Reload the Barcart 
        private void btnReload_Click(object sender, EventArgs e)
        {
            chart1.Series["Valid Medicines"].Points.Clear();
            chart1.Series["Expired Medicines"].Points.Clear();
            loadChart();
        }
    }
}
