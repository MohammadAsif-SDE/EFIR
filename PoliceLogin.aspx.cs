using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;

public partial class PoliceLogin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        string cs = ConfigurationManager.ConnectionStrings["efir_dbConnectionString"].ConnectionString;

        using (SqlConnection con = new SqlConnection(cs))
        {
            string query = "SELECT COUNT(*) FROM Police WHERE username=@u AND password=@p";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@u", txtUser.Text.Trim());
            cmd.Parameters.AddWithValue("@p", txtPass.Text);

            con.Open();
            int count = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();

            if (count > 0)
            {
                Session["Police"] = txtUser.Text.Trim();
                Response.Redirect("PoliceDashboard.aspx");
            }
            else
            {
                lblMsg.Text = "Invalid Login!";
            }
        }
    }
}