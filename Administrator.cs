using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Pharmacy_Management_System
{
    public partial class Administrator : Form
    {
        // Display the Username in Label
        String user = "";

        public Administrator()
        {
            InitializeComponent();
        }

        // Connection To database Login Username 
        public string ID
        {
            get { return user.ToString(); }
        }

        // Load infromation View user, Username
        public Administrator(String username)
        {
            InitializeComponent();
            UserProfileLabel.Text = username;
            user = username;
            uC_View_User1.ID = ID;
            uC_Profile1.ID = ID;
        }

        // Click the Controls buttons (Load Admin Panel, Display Dashboard )
        private void Administrator_Load(object sender, EventArgs e)
        {
            uC_Dashborad1.Visible = false;
            uC_Add_User1.Visible = false;
            uC_View_User1.Visible = false;
            uC_Profile1.Visible = false;
            btnDashboard.PerformClick();
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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        // Click the btn Dashboard Show Dashboard Panel
        private void btnDashboard_Click(object sender, EventArgs e)
        {
            uC_Dashborad1.Visible = true;
            uC_Dashborad1.BringToFront();
        }

        // Click the btn AddUser Show AddUser Panel
        private void btnAddUser_Click(object sender, EventArgs e)
        {
            uC_Add_User1.Visible = true;
            uC_Add_User1.BringToFront();
        }

        private void uC_Add_User1_Load(object sender, EventArgs e)
        {

        }

        // Click the btn Logout 
        private void btnLogOut_Click(object sender, EventArgs e)
        {
            SignIn am = new SignIn();
            am.Show();
            this.Hide();
        }
        private void btnLogOut_Click_1(object sender, EventArgs e)
        {
            SignIn am = new SignIn();
            am.Show();
            this.Hide();
        }

        // Click the btn View User Show ViewUser Panel
        private void btnViewUser_Click(object sender, EventArgs e)
        {
            uC_View_User1.Visible = true;
            uC_View_User1.BringToFront();
        }

        // Click the btn Profile Show Profile Panel
        private void btnProfile_Click(object sender, EventArgs e)
        {
            uC_Profile1.Visible = true;
            uC_Profile1.BringToFront();
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void UserProfileLabel_Click(object sender, EventArgs e)
        {

        }
    }
}
