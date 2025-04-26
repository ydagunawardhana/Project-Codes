using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using ZXing;


namespace Pharmacy_Management_System.PharmacistUC
{
    public partial class UC_P_AddMedicine : UserControl
    {
        // Get SQL Connection function.cs
        function fn = new function();
        string query;
        private object barcodeReader;

        public UC_P_AddMedicine()
        {
            InitializeComponent();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        // Add the Medicines Infromations to Database
        private void btnAdd_Click(object sender, EventArgs e)
        {
            string MedicineID = txtMedicineID.Text;
            string MedicineName = txtMedicineName.Text;
            string ManufacturingDate = txtManufacturingDate.Text;
            string MedicineNumber = txtMedicineNumber.Text;
            string MedicineType = txtMedicineType.Text;
            string TypicalDosage = txtTypicalDosage.Text;
            string ExpireDate = txtExpireDate.Text;
            Int64 Quantity;
            bool isValidQuantity = Int64.TryParse(txtQuantity.Text, out Quantity);
            Int64 PricePerUnit;
            bool isValidPricePerUnit = Int64.TryParse(txtPricePerUnit.Text, out PricePerUnit);

            if (!isValidQuantity)
            {
                MessageBox.Show("Enter All Data!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (fn.UserExists(MedicineNumber))
            {
                MessageBox.Show("Medicine already exists!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Data Insert into Medicine Table in system DataBase (Query)
            try
            {
                string query = "INSERT INTO Medicine (MedicineID, MedicineName, ManufacturingDate, MedicineNumber, MedicineType, TypicalDosage, ExpireDate, Quantity, PricePerUnit) " +
                               "VALUES (@MedicineID, @MedicineName, @ManufacturingDate, @MedicineNumber, @MedicineType, @TypicalDosage, @ExpireDate, @Quantity, @PricePerUnit)";

                SqlParameter[] parameters = new SqlParameter[]
                {
                        new SqlParameter("@MedicineID", MedicineID),
                        new SqlParameter("@MedicineName", MedicineName),
                        new SqlParameter("@ManufacturingDate", DateTime.Parse(ManufacturingDate)), // Ensure DateTime format
                        new SqlParameter("@MedicineNumber", MedicineNumber),
                        new SqlParameter("@MedicineType", MedicineType),
                        new SqlParameter("@TypicalDosage", TypicalDosage),
                        new SqlParameter("@ExpireDate", DateTime.Parse(ExpireDate)), // Ensure DateTime format
                        new SqlParameter("@Quantity", Quantity),
                        new SqlParameter("@PricePerUnit", PricePerUnit)
                };

                fn.setData(query, "Medicine Added Successful.", parameters);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
         // Clear All information
        private void btnReset_Click(object sender, EventArgs e)
        {
            clearAll();
        }

        public void clearAll()
        {
            txtMedicineID.Clear();
            txtMedicineName.Clear();
            txtManufacturingDate.ResetText();
            txtMedicineNumber.Clear();
            txtMedicineType.SelectedIndex = -1;
            txtTypicalDosage.Clear();
            txtExpireDate.ResetText();
            txtQuantity.Clear();
            txtPricePerUnit.Clear();
            txtBarCodeScanner.Clear();

            if (pictureBox1.Image != null) // Clear BarCode Scanner Upload Image
            {
                pictureBox1.Image.Dispose();
                pictureBox1.Image = null;
            }
        }

        // Choose the QR / BarCode Image in system
        private void BtnChoose_Click(object sender, EventArgs e)
        {
            // Open File Dialog to Select Image
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Display the image in PictureBox
                    pictureBox1.Image = Image.FromFile(openFileDialog.FileName);
                    BarcodeReader reader = new BarcodeReader();
                    var result = reader.Decode((Bitmap)pictureBox1.Image);

                        if (result != null)
                        {
                            txtBarCodeScanner.Text = result.ToString();
                        }
                }
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }
    }
}

