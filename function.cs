using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Pharmacy_Management_System
{
    class function
    {
        protected SqlConnection getConnection()
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Data Source=YASHAN-PC\\SQLEXPRESS;Initial Catalog=\"Pharmacy Management System DB\";Integrated Security=True;TrustServerCertificate=True;";
            return conn;
        }

        public DataSet getData(string query)
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

        public void setData(string query, string msg, SqlParameter[] parameters)
        {
            using (SqlConnection conn = getConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddRange(parameters);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }

            MessageBox.Show(msg, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public bool UserExists(string username)
        {
            using (SqlConnection conn = getConnection())
            {
                string query = "SELECT COUNT(*) FROM Users WHERE Username = @Username";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    conn.Open();
                    int count = (int)cmd.ExecuteScalar();
                    conn.Close();
                    return count > 0;
                }
            }
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
    }
}
