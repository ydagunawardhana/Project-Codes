using System;
using System.Data;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Pharmacy_Management_System.AdministratorUC
{
    public partial class UC_Dashborad : UserControl
    {
        // Get SQL Connection function.cs
        function fn = new function();
        String query;
        DataSet ds;

        public UC_Dashborad()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        // Count Admins, Pharmacists, Customers are Avaliable in System Database
        private void UC_Dashborad_Load(object sender, EventArgs e)
        {
            query = "SELECT COUNT(UserRole) FROM Users WHERE UserRole = 'Administrator'";
            ds = fn.getData(query);
            setLabel(ds, AdminLabel);

            query = "SELECT COUNT(UserRole) FROM Users WHERE UserRole = 'Pharmacist'";
            ds = fn.getData(query);
            setLabel(ds, PharmacistLabel);

            query = "SELECT COUNT(UserRole) FROM Customers WHERE UserRole = 'Customer'";
            ds = fn.getData(query);
            setLabel(ds, CustomerLabel);
        }

        // Display the labels How many Admins, Pharmacists, Customers are Avaliable in System
        private void setLabel(DataSet ds,Label lbl)
        {
            if (ds.Tables[0].Rows.Count != 0)
            {
                lbl.Text = ds.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                lbl.Text = "0";
            }
        }

        // Drag the Form 
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HTCAPTION = 0x2;
        [DllImport("User32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("User32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private void OnMouseDown(object sender, MouseEventArgs e)
        {

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void AdminLabel_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }

        // load the Dashboard
        private void btnSync_Click(object sender, EventArgs e)
        {
            UC_Dashborad_Load(this, null);
        }
    }
}
