using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PoliceStations : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Police"] == null)
            Response.Redirect("PoliceLogin.aspx");
    }


    protected void btnCreate_Click(object sender, EventArgs e)
    {

        string cs = ConfigurationManager.ConnectionStrings["efir_dbConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(cs))
        {
            string query = "INSERT INTO PoliceStations (station_name, address) VALUES (@n, @a)";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@n", txtStationName.Text.Trim());
            cmd.Parameters.AddWithValue("@a", txtAddress.Text.Trim());
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        lblMsg.Text = "Station created successfully.";
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("PoliceDashboard.aspx");
    }
}
