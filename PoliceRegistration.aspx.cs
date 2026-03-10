using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PoliceRegistration : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Police"] == null)
            Response.Redirect("PoliceLogin.aspx");

        if (!IsPostBack)
        {
            LoadStations();
        }
    }

    void LoadStations()
    {
        string cs = ConfigurationManager.ConnectionStrings["efir_dbConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(cs))
        {
            SqlDataAdapter da = new SqlDataAdapter(
                "SELECT station_id, station_name FROM PoliceStations ORDER BY station_name", con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            ddlStation.DataSource = dt;
            ddlStation.DataTextField = "station_name";
            ddlStation.DataValueField = "station_id";
            ddlStation.DataBind();
            ddlStation.Items.Insert(0, new ListItem("Select Station", ""));
        }
    }

    protected void btnRegister_Click(object sender, EventArgs e)
    {

        string cs = ConfigurationManager.ConnectionStrings["efir_dbConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(cs))
        {
            string query = "INSERT INTO Police (username, password, full_name, badge_number, station_id) " +
                           "VALUES (@u, @p, @n, @b, @s)";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@u", txtUsername.Text.Trim());
            cmd.Parameters.AddWithValue("@p", txtPassword.Text);
            cmd.Parameters.AddWithValue("@n", txtFullName.Text.Trim());
            cmd.Parameters.AddWithValue("@b", txtBadgeNumber.Text.Trim());
            cmd.Parameters.AddWithValue("@s", Convert.ToInt32(ddlStation.SelectedValue));
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        lblMsg.ForeColor = System.Drawing.Color.Green;
        lblMsg.Text = "Police registered successfully.";
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("PoliceDashboard.aspx");
    }
}
