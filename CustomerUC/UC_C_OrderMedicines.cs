using System;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using DGVPrinterHelper;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Pharmacy_Management_System.CustomerUC
{
    public partial class UC_C_OrderMedicines: UserControl
    {
        // Get SQL Connection function.cs
        function fn = new function();
        String query;
        DataSet ds;

        public UC_C_OrderMedicines()
        {
            InitializeComponent();
        }

        // Load the  Medicines in Database (Check the Expire date and Quantity)
        private void UC_C_OrderMedicines_Load(object sender, EventArgs e)
        {
            ListBoxMedicines.Items.Clear();

            query = "SELECT MedicineName FROM Medicine WHERE ExpireDate >= getdate() AND Quantity >'0'";
            ds = fn.getData(query);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                ListBoxMedicines.Items.Add(ds.Tables[0].Rows[i][0].ToString());
            }
        }

        // Load the Inforamtion in Customer Order Medicines Tab
        private void btnSync_Click(object sender, EventArgs e)
        {
            UC_C_OrderMedicines_Load(this, null);
        }

        // Type the Medicine Name in search box and check Expire date and Quantity
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

        // Select Medicine in ListBox and Show all Medicine Information in Test Boxes
        private void ListBoxMedicines_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtNoOfUnits.Clear();

            String name = ListBoxMedicines.GetItemText(ListBoxMedicines.SelectedItem);

            txtMedicineName.Text = name;
            query = "SELECT MedicineID, MedicineType, TypicalDosage, ExpireDate, PricePerUnit FROM Medicine WHERE MedicineName = '" + name + "'";
            ds = fn.getData(query);

            txtMedicineID.Text = ds.Tables[0].Rows[0][0].ToString();
            txtMedicineType.Text = ds.Tables[0].Rows[0][1].ToString();
            txtTypicalDosage.Text = ds.Tables[0].Rows[0][2].ToString();
            txtExpireDate.Text = ds.Tables[0].Rows[0][3].ToString();
            txtPricePerUnit.Text = ds.Tables[0].Rows[0][4].ToString();
        }

        // Type the No of Units Medicines and Automatically calculate the Total Price
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

        // Select the Medicines and click Add to Cart btn (Show Medicines in Data grid with Informations)
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
                    n = DataGridView5.Rows.Add();
                    DataGridView5.Rows[n].Cells[0].Value = txtMedicineID.Text;
                    DataGridView5.Rows[n].Cells[1].Value = txtMedicineName.Text;
                    DataGridView5.Rows[n].Cells[2].Value = txtMedicineType.Text;
                    DataGridView5.Rows[n].Cells[3].Value = txtTypicalDosage.Text;
                    DataGridView5.Rows[n].Cells[4].Value = txtExpireDate.Text;
                    DataGridView5.Rows[n].Cells[5].Value = txtPricePerUnit.Text;
                    DataGridView5.Rows[n].Cells[6].Value = txtNoOfUnits.Text;
                    DataGridView5.Rows[n].Cells[7].Value = txtTotalPrice.Text;

                    TotalAmount = TotalAmount + int.Parse(txtTotalPrice.Text);
                    TotalLabel.Text = "Rs. " + TotalAmount.ToString();

                    /*query = "UPDATE Medicine SET Quantity = '" + NewQuantity + "' WHERE MedicineID = '" + txtMedicineID.Text + "'";*/
                    fn.setData(query, "Medicine Added.");
                }
                else
                {
                    MessageBox.Show("Medicine is Out of Stock.\n Only " + Quantity + " Left", "Warning !!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                clearAll();
                UC_C_OrderMedicines_Load(this, null);
            }
            else
            {
                MessageBox.Show("Select Medicine First.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Select the Datagrid Informations and Add Column Amount, Noof Units
        int ValueAmount;
        String ValueID;
        protected Int64 NoOfUnits;


        private void DataGridView5_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                ValueAmount = int.Parse(DataGridView5.Rows[e.RowIndex].Cells[7].Value.ToString());
                ValueID = DataGridView5.Rows[e.RowIndex].Cells[0].Value.ToString();
                NoOfUnits = int.Parse(DataGridView5.Rows[e.RowIndex].Cells[6].Value.ToString());
            }
            catch (Exception)
            {

            }
        }

        // Remove the Add Cart information in Data Grid (Update Database)
        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (ValueID != null)
            {
                try
                {
                    DataGridView5.Rows.RemoveAt(this.DataGridView5.SelectedRows[0].Index);
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

                UC_C_OrderMedicines_Load(this, null);
            }
        }

        // Print the Order Medicines in PDF File
        private void btnOrderAndPrint_Click(object sender, EventArgs e)
        {
            DGVPrinter print = new DGVPrinter();
            print.Title = "MEDI CORE Pharamacy Order Medicines Bill";
            print.SubTitle = String.Format("Date:- {0} \n Contact Number - 0112896789", DateTime.Now.Date);
            print.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
            print.PageNumbers = true;
            print.PageNumberInHeader = false;
            print.PorportionalColumns = true;
            print.HeaderCellAlignment = StringAlignment.Near;
            print.Footer = "Total Payable Amount : " + TotalLabel.Text;
            print.FooterSpacing = 15;
            print.PrintDataGridView(DataGridView5);

            TotalAmount = 0;
            TotalLabel.Text = "Rs. 00";
            DataGridView5.DataSource = 0;
        }

        // After Print the Pdf Clear All Text Boxes
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

        // Send the Order Medicines Via SMS Notification to Pharamacist
        private void btnSendSMS_Click(object sender, EventArgs e)
        {
            string pharmacistPhone = "Recived Number";
            string messageBody = "New Order Received! Please Check the Pharmacy System.";

            SendSMS(pharmacistPhone, messageBody);
        }

        // Used Twilio API Verification Number
        private void SendSMS(string to, string message)
        {
            string accoundSid = "Twilio Account SID";
            string authToken = "Twilio Account AuthToken";

            TwilioClient.Init(accoundSid, authToken);
            var messageResult = MessageResource.Create(
                body: message,
                from: new Twilio.Types.PhoneNumber("Verify Number"),
                to: new Twilio.Types.PhoneNumber("Twilio Account Number")
                );
            MessageBox.Show("SMS Sent Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }







    }
}
