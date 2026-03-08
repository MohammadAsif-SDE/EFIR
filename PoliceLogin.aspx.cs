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
            string query = "SELECT is_chief FROM Police WHERE username=@u AND password=@p";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@u", txtUser.Text.Trim());
            cmd.Parameters.AddWithValue("@p", txtPass.Text);

            con.Open();
            object result = cmd.ExecuteScalar();
            con.Close();

            if (result != null)
            {
                Session["Police"] = txtUser.Text.Trim();
                bool isChief = Convert.ToBoolean(result);
                
                if (isChief)
                    Response.Redirect("PoliceDashboard.aspx");
                else
                    Response.Redirect("PoliceHome.aspx");
            }
            else
            {
                lblMsg.Text = "Invalid Login!";
            }
        }
    }
}