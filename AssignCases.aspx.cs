using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AssignCases : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Police"] == null)
            Response.Redirect("PoliceLogin.aspx");

        if (!IsPostBack)
        {
            LoadFirIds();
            LoadPoliceDropdown();
            LoadData();
        }
    }

    void LoadFirIds()
    {
        string cs = ConfigurationManager.ConnectionStrings["efir_dbConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(cs))
        {
            SqlDataAdapter da = new SqlDataAdapter(
                "SELECT fir_id FROM FIR WHERE status='Approved' ORDER BY fir_id DESC", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            ddlFirId.DataSource = dt;
            ddlFirId.DataTextField = "fir_id";
            ddlFirId.DataValueField = "fir_id";
            ddlFirId.DataBind();
            ddlFirId.Items.Insert(0, new ListItem("Select FIR ID", ""));
        }
    }

    void LoadData()
    {
        string cs = ConfigurationManager.ConnectionStrings["efir_dbConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(cs))
        {
            SqlDataAdapter da = new SqlDataAdapter(
                "SELECT f.fir_id, f.fir_number, f.complaint_name, f.incident_place, f.description, " +
                "ISNULL(NULLIF(CAST(f.assigned_to AS NVARCHAR(100)), ''), 'Not Assigned') as assigned_to_name, " +
                "ISNULL(f.investigation_status, 'Pending') as investigation_status " +
                "FROM FIR f " +
                "WHERE f.status = 'Approved' " +
                "ORDER BY f.fir_id DESC", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
    }

    void LoadPoliceDropdown()
    {
        string cs = ConfigurationManager.ConnectionStrings["efir_dbConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(cs))
        {
            SqlDataAdapter da = new SqlDataAdapter(
                "SELECT full_name + ' (' + username + ')' as display_name FROM Police ORDER BY full_name", con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            ddlPolice.DataSource = dt;
            ddlPolice.DataTextField = "display_name";
            ddlPolice.DataValueField = "display_name";
            ddlPolice.DataBind();
            ddlPolice.Items.Insert(0, new ListItem("Select Police", ""));
        }
    }

    protected void btnAssign_Click(object sender, EventArgs e)
    {
        int firId = Convert.ToInt32(ddlFirId.SelectedValue);
        string assignedTo = ddlPolice.SelectedValue;

        string cs = ConfigurationManager.ConnectionStrings["efir_dbConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(cs))
        {
            string query = "UPDATE FIR SET assigned_to=@p, investigation_status='Assigned' WHERE fir_id=@f";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@p", assignedTo);
            cmd.Parameters.AddWithValue("@f", firId);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        lblMsg.ForeColor = System.Drawing.Color.Green;
        lblMsg.Text = "Case assigned successfully.";
        LoadData();
        LoadFirIds();
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("PoliceDashboard.aspx");
    }
}
