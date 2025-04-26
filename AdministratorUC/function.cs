using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Pharmacy_Management_System.AdministratorUC
{
    internal class function
    {
        // This method establishes a connection to the SQL database
        protected SqlConnection getConnection()
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Data Source=YASHAN-PC\\SQLEXPRESS;Initial Catalog=\"Pharmacy Management System DB\";Integrated Security=True;TrustServerCertificate=True;";
            return conn;
        }

        internal DataSet getData(string query)
        {
            using (SqlConnection conn = getConnection())
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
        }

        internal void setData(string query, string v, System.Data.SqlClient.SqlParameter[] parameters)
        {
            using (SqlConnection conn = getConnection())  // Ensure connection is handled properly
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddRange(parameters);  // Add parameters to avoid SQL injection
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
            MessageBox.Show(v, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        internal void setData(string query, string v)
        {
            using (SqlConnection conn = getConnection()) // Ensure connection is handled properly
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }

            MessageBox.Show(v, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        internal bool UserExists(string username)
        {
            using (SqlConnection conn = getConnection())
            {
                string query = "SELECT COUNT(*) FROM Users WHERE Username = @Username";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    conn.Open();
                    int count = (int)cmd.ExecuteScalar();  // Get count of existing usernames
                    conn.Close();
                    return count > 0;  // If count > 0, username exists
                }
            }
        }

    }
}