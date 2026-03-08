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

        if (!CheckIsChief())
            Response.Redirect("PoliceHome.aspx");

        if (!IsPostBack)
            LoadData();
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
            SqlDataAdapter da = new SqlDataAdapter(
                "SELECT p.PoliceId, p.username, p.full_name, p.badge_number, ISNULL(s.station_name, '-') as station_name " +
                "FROM Police p LEFT JOIN PoliceStations s ON p.station_id = s.station_id " +
                "ORDER BY p.PoliceId DESC", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
    }

    protected void btnRegister_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
        {
            lblMsg.ForeColor = System.Drawing.Color.Red;
            lblMsg.Text = "Username and Password are required.";
            return;
        }

        string cs = ConfigurationManager.ConnectionStrings["efir_dbConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(cs))
        {
            con.Open();

            string checkQuery = "SELECT COUNT(*) FROM Police WHERE username=@u";
            SqlCommand checkCmd = new SqlCommand(checkQuery, con);
            checkCmd.Parameters.AddWithValue("@u", txtUsername.Text.Trim());
            int existingCount = Convert.ToInt32(checkCmd.ExecuteScalar());

            if (existingCount > 0)
            {
                lblMsg.ForeColor = System.Drawing.Color.Red;
                lblMsg.Text = "Username already exists.";
                con.Close();
                return;
            }

            int nextPoliceId;
            string nextIdQuery = "SELECT ISNULL(MAX(PoliceId), 0) + 1 FROM Police";
            SqlCommand nextIdCmd = new SqlCommand(nextIdQuery, con);
            nextPoliceId = Convert.ToInt32(nextIdCmd.ExecuteScalar());

            string query = "INSERT INTO Police (PoliceId, username, password, full_name, badge_number, is_chief) " +
                           "VALUES (@id, @u, @p, @n, @b, 0)";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", nextPoliceId);
            cmd.Parameters.AddWithValue("@u", txtUsername.Text.Trim());
            cmd.Parameters.AddWithValue("@p", txtPassword.Text);
            cmd.Parameters.AddWithValue("@n", txtFullName.Text.Trim());
            cmd.Parameters.AddWithValue("@b", txtBadgeNumber.Text.Trim());
            cmd.ExecuteNonQuery();
            con.Close();
        }

        lblMsg.ForeColor = System.Drawing.Color.Green;
        lblMsg.Text = "Police registered successfully.";
        ClearFields();
        LoadData();
    }

    void ClearFields()
    {
        txtUsername.Text = "";
        txtPassword.Text = "";
        txtFullName.Text = "";
        txtBadgeNumber.Text = "";
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);
        string cs = ConfigurationManager.ConnectionStrings["efir_dbConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(cs))
        {
            string query = "DELETE FROM Police WHERE PoliceId=@id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        lblMsg.ForeColor = System.Drawing.Color.Green;
        lblMsg.Text = "Police deleted successfully.";
        LoadData();
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("PoliceDashboard.aspx");
    }
}
