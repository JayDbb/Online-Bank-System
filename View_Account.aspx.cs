using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace Online_Bank_System
{
    public partial class View_Account : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Call method to load account data
                LoadAccountData();
            }
        } 

        private void LoadAccountData()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            // Define the SQL query to fetch account data (assuming user ID is stored in session or context)
            string userId = "ID"; // Retrieve user ID from session or context
            string query = "SELECT Balance, Type FROM Accounts WHERE ID = @UserId";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Add parameter for UserId to prevent SQL injection
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    try
                    {
                        conn.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            // Bind the data to the GridView
                            GridView1.DataSource = reader;
                            GridView1.DataBind();
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle errors (optional)
                        Response.Write("Error: " + ex.Message);
                    }
                }
            }
        }
    }
}
