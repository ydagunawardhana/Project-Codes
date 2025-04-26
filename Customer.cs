using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Pharmacy_Management_System
{
    public partial class Customer : Form
    {
        // Get SQL Connection function.cs
        function fn = new function();
        String query;
        String user = "";

        public Customer()
        {
            InitializeComponent();
        }

        // Display the Customer Username Label
        public string ID
        {
            get { return user.ToString(); }
        }

        // Load infromation  Username
        public Customer(String username)
        {
            InitializeComponent();
            UserProfileLabel.Text = username;
            user = username;
            uC_C_Profile1.ID = ID;
        }

        private void Customer_Load(object sender, EventArgs e)
        {
        }

        //Click the Panel Btn Load the Customer Panel Sections and View Medicines
        private void Customer_Load_1(object sender, EventArgs e)
        {
            uC_C_ViewMedicines2.Visible = false;
            uC_C_OrderMedicines1.Visible = false;
            uC_C_Profile1.Visible = false;
            uC_C_OrderHistory1.Visible = false;
            btnViewMedicines.PerformClick();
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
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }

        // btn logout function
        private void btnLogOut_Click(object sender, EventArgs e)
        {
            SignIn signIn = new SignIn();
            signIn.Show();
            this.Hide();
        }

        // btn View Medicines function
        private void btnViewMedicines_Click(object sender, EventArgs e)
        {
            uC_C_ViewMedicines2.Visible = true;
            uC_C_ViewMedicines2.BringToFront();
        }

        private void uC_C_ViewMedicines1(object sender, EventArgs e)
        {

        }

        // btn Customer Profile Function
        private void btnProfile_Click(object sender, EventArgs e)
        {
            uC_C_Profile1.Visible = true;
            uC_C_Profile1.BringToFront();
        }

        // btn Order Medicines Function
        private void btnOrderMedicines_Click(object sender, EventArgs e)
        {
            uC_C_OrderMedicines1.Visible = true;
            uC_C_OrderMedicines1.BringToFront();
        }

        // btn Order History Function
        private void btnOrderHistory_Click(object sender, EventArgs e)
        {
            uC_C_OrderHistory1.Visible = true;
            uC_C_OrderHistory1.BringToFront();
        }
    }
}
