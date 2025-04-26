using System;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using DGVPrinterHelper;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Pharmacy_Management_System.PharmacistUC
{
    public partial class UC_P_SellMedicine : UserControl
    {
        // Get SQL Connection function.cs
        function fn = new function();
        String query;
        DataSet ds;

        public UC_P_SellMedicine()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        // Display the Medicines In Database
        private void UC_P_SellMedicine_Load(object sender, EventArgs e)
        {
            ListBoxMedicines.Items.Clear();

            query = "SELECT MedicineName FROM Medicine WHERE ExpireDate >= getdate() AND Quantity >'0'";
            ds = fn.getData(query);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                ListBoxMedicines.Items.Add(ds.Tables[0].Rows[i][0].ToString());
            }
        }

        // Load the Informations (Refresh)
        private void btnSync_Click(object sender, EventArgs e)
        {
            UC_P_SellMedicine_Load(this, null);
        }

        // Search the Medicines In Database and Check the Expire Date, Quantity
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            ListBoxMedicines.Items.Clear();
            query = "SELECT MedicineName FROM Medicine WHERE MedicineName like '" + txtSearch.Text + "%' AND ExpireDate >= getdate() AND Quantity >'0'";
            ds = fn.getData(query);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                ListBoxMedicines.Items.Add(ds.Tables[0].Rows[i][0].ToString());
            }
        }

        // Select the Medicines in Listbox and Show All Medicines Details in Text Boxes
        private void ListBoxMedicines_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtNoOfUnits.Clear();

            String name = ListBoxMedicines.GetItemText(ListBoxMedicines.SelectedItem);

            txtMedicineName.Text = name;
            query = "SELECT MedicineType, TypicalDosage, MedicineID, ExpireDate, PricePerUnit FROM Medicine WHERE MedicineName = '" + name + "'";
            ds = fn.getData(query);

            txtMedicineType.Text = ds.Tables[0].Rows[0][0].ToString();
            txtTypicalDosage.Text = ds.Tables[0].Rows[0][1].ToString();
            txtMedicineID.Text = ds.Tables[0].Rows[0][2].ToString();
            txtExpireDate.Text = ds.Tables[0].Rows[0][3].ToString();
            txtPricePerUnit.Text = ds.Tables[0].Rows[0][4].ToString();
        }

        // Type the No of Units And Automatically Calcluate the Total Price 
        private void txtNoOfUnits_TextChanged(object sender, EventArgs e)
        {
            if (txtNoOfUnits.Text != "")
            {
                Int64 PricePerUnit = Int64.Parse(txtPricePerUnit.Text);
                Int64 NoOfUnits = Int64.Parse(txtNoOfUnits.Text);
                Int64 TotalAmount = PricePerUnit * NoOfUnits;
                txtTotalPrice.Text = TotalAmount.ToString();
            }
            else
            {
                txtTotalPrice.Clear();
            }
        }

        // Add the Medicines into DataGrid and Update Database
        protected int n, TotalAmount = 0;
        protected Int64 Quantity, NewQuantity;


        private void btnAddtoCart_Click(object sender, EventArgs e)
        {
            if (txtMedicineID.Text != "")
            {
                query = "SELECT Quantity FROM Medicine WHERE MedicineID = '" + txtMedicineID.Text + "'";
                ds = fn.getData(query);

                Quantity = Int64.Parse(ds.Tables[0].Rows[0][0].ToString()); // 100
                NewQuantity = Quantity - Int64.Parse(txtNoOfUnits.Text);  // 100-50

                if (NewQuantity >= 0)
                {
                    n = DataGridView4.Rows.Add();
                    DataGridView4.Rows[n].Cells[0].Value = txtMedicineID.Text;
                    DataGridView4.Rows[n].Cells[1].Value = txtMedicineName.Text;
                    DataGridView4.Rows[n].Cells[2].Value = txtMedicineType.Text;
                    DataGridView4.Rows[n].Cells[3].Value = txtTypicalDosage.Text;
                    DataGridView4.Rows[n].Cells[4].Value = txtExpireDate.Text;
                    DataGridView4.Rows[n].Cells[5].Value = txtPricePerUnit.Text;
                    DataGridView4.Rows[n].Cells[6].Value = txtNoOfUnits.Text;
                    DataGridView4.Rows[n].Cells[7].Value = txtTotalPrice.Text;

                    TotalAmount = TotalAmount + int.Parse(txtTotalPrice.Text);
                    TotalLabel.Text = "Rs. " + TotalAmount.ToString(); // Display Toatal Amount In Medicines

                    query = "UPDATE Medicine SET Quantity = '" + NewQuantity + "' WHERE MedicineID = '" + txtMedicineID.Text + "'";
                    fn.setData(query, "Medicine Added.");
                }
                else
                {
                    MessageBox.Show("Medicine is Out of Stock.\n Only " + Quantity + " Left", "Warning !!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                clearAll();
                UC_P_SellMedicine_Load(this, null);
            }
            else
            {
                MessageBox.Show("Select Medicine First.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Select the Medicines in Datagrid
        int ValueAmount;
        String ValueID;
        protected Int64 NoOfUnits;

        private void DataGridView4_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                ValueAmount = int.Parse(DataGridView4.Rows[e.RowIndex].Cells[7].Value.ToString());
                ValueID = DataGridView4.Rows[e.RowIndex].Cells[0].Value.ToString();
                NoOfUnits = int.Parse(DataGridView4.Rows[e.RowIndex].Cells[6].Value.ToString());
            }
            catch (Exception)
            {

            }
        }

        // Remove the Datagrid Medicines and Update Database
        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (ValueID != null)
            {
                try
                {
                    DataGridView4.Rows.RemoveAt(this.DataGridView4.SelectedRows[0].Index);
                }
                catch
                {

                }
                finally
                {
                    query = "SELECT Quantity FROM Medicine WHERE MedicineID = '" + ValueID + "'";
                    ds = fn.getData(query);
                    Quantity = Int64.Parse(ds.Tables[0].Rows[0][0].ToString());
                    NewQuantity = Quantity + NoOfUnits;

                    query = "UPDATE Medicine SET Quantity = '" + NewQuantity + "' WHERE MedicineID = '" + ValueID + "'";
                    fn.setData(query, "Medicine Removed From Cart.");
                    TotalAmount = TotalAmount - ValueAmount;
                    TotalLabel.Text = "Rs. " + TotalAmount.ToString();
                }

                UC_P_SellMedicine_Load(this, null);
            }
        }

        // Print the Purchase Medicines Details in PDF File
        private void btnPurchaseAndPrint_Click(object sender, EventArgs e)
        {
            DGVPrinter print = new DGVPrinter();
            print.Title = "MEDI CORE Pharamacy Medicine Bill";
            print.SubTitle = String.Format("Date:- {0} \n Contact Number - 0112896789", DateTime.Now.Date);
            print.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
            print.PageNumbers = true;
            print.PageNumberInHeader = false;
            print.PorportionalColumns = true;
            print.HeaderCellAlignment = StringAlignment.Near;
            print.Footer = "Total Payable Amount : " + TotalLabel.Text;
            print.FooterSpacing = 15;
            print.PrintDataGridView(DataGridView4);

            TotalAmount = 0;
            TotalLabel.Text = "Rs. 00";
            DataGridView4.DataSource = 0;

        }

        // After Print PDF Clear All Select Medicine Details
        private void clearAll()
        {
            txtMedicineID.Clear();
            txtMedicineName.Clear();
            txtMedicineType.Clear();
            txtTypicalDosage.Clear();
            txtExpireDate.ResetText();
            txtPricePerUnit.Clear();
            txtNoOfUnits.Clear();
        }
    }

}

