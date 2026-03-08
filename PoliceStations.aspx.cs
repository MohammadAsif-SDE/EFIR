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

        if (!CheckIsChief())
            Response.Redirect("PoliceHome.aspx");

        if (!IsPostBack)
        {
            LoadData();
            LoadDropdowns();
        }
    }

    bool CheckIsChief()
    {
        string cs = ConfigurationManager.ConnectionStrings["efir_dbConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(cs))
        {
            string query = "SELECT is_chief FROM Police WHERE username=@u";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@u", Session["Police"].ToString());
            con.Open();
            object result = cmd.ExecuteScalar();
            con.Close();
            return result != null && Convert.ToBoolean(result);
        }
    }

    void LoadData()
    {
        string cs = ConfigurationManager.ConnectionStrings["efir_dbConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(cs))
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM PoliceStations ORDER BY station_id DESC", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
    }

    void LoadDropdowns()
    {
        string cs = ConfigurationManager.ConnectionStrings["efir_dbConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(cs))
        {
            // Load Police
            SqlDataAdapter daPolice = new SqlDataAdapter(
                "SELECT PoliceId, username + ' - ' + full_name as display_name FROM Police ORDER BY username", con);
            DataTable dtPolice = new DataTable();
            daPolice.Fill(dtPolice);
            ddlPolice.DataSource = dtPolice;
            ddlPolice.DataTextField = "display_name";
            ddlPolice.DataValueField = "PoliceId";
            ddlPolice.DataBind();

            // Load Stations
            SqlDataAdapter daStation = new SqlDataAdapter("SELECT station_id, station_name FROM PoliceStations", con);
            DataTable dtStation = new DataTable();
            daStation.Fill(dtStation);
            ddlStation.DataSource = dtStation;
            ddlStation.DataTextField = "station_name";
            ddlStation.DataValueField = "station_id";
            ddlStation.DataBind();
        }
    }

    protected void btnCreate_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtStationName.Text))
        {
            lblMsg.ForeColor = System.Drawing.Color.Red;
            lblMsg.Text = "Station name is required.";
            return;
        }

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

        lblMsg.ForeColor = System.Drawing.Color.Green;
        lblMsg.Text = "Station created successfully.";
        txtStationName.Text = "";
        txtAddress.Text = "";
        LoadData();
        LoadDropdowns();
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);
        string cs = ConfigurationManager.ConnectionStrings["efir_dbConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(cs))
        {
            string query = "DELETE FROM PoliceStations WHERE station_id=@id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        lblMsg.ForeColor = System.Drawing.Color.Green;
        lblMsg.Text = "Station deleted successfully.";
        LoadData();
        LoadDropdowns();
    }

    protected void btnAssign_Click(object sender, EventArgs e)
    {
        if (ddlPolice.Items.Count == 0 || ddlStation.Items.Count == 0)
        {
            lblAssignMsg.ForeColor = System.Drawing.Color.Red;
            lblAssignMsg.Text = "No police or stations available.";
            return;
        }

        string cs = ConfigurationManager.ConnectionStrings["efir_dbConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(cs))
        {
            string query = "UPDATE Police SET station_id=@s WHERE PoliceId=@p";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@s", ddlStation.SelectedValue);
            cmd.Parameters.AddWithValue("@p", ddlPolice.SelectedValue);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        lblAssignMsg.ForeColor = System.Drawing.Color.Green;
        lblAssignMsg.Text = "Police assigned to station successfully.";
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("PoliceDashboard.aspx");
    }
}
