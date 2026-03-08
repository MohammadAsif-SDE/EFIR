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
                "SELECT f.fir_id, f.fir_number, f.complaint_name, f.incident_place, f.description, " +
                "ISNULL(p.full_name, 'Not Assigned') as assigned_to_name, " +
                "ISNULL(f.investigation_status, 'Pending') as investigation_status " +
                "FROM FIR f LEFT JOIN Police p ON f.assigned_to = p.PoliceId " +
                "WHERE f.status = 'Approved' " +
                "ORDER BY f.fir_id DESC", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
    }

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        lblMsg.Text = string.Empty;
        GridView1.EditIndex = e.NewEditIndex;
        LoadData();
        LoadPoliceDropdown(e.NewEditIndex);
    }

    void LoadPoliceDropdown(int rowIndex)
    {
        DropDownList ddlPolice = GridView1.Rows[rowIndex].FindControl("ddlPolice") as DropDownList;
        if (ddlPolice != null)
        {
            string cs = ConfigurationManager.ConnectionStrings["efir_dbConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlDataAdapter da = new SqlDataAdapter(
                    "SELECT PoliceId, full_name + ' (' + username + ')' as display_name FROM Police ORDER BY full_name", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                
                ddlPolice.DataSource = dt;
                ddlPolice.DataTextField = "display_name";
                ddlPolice.DataValueField = "PoliceId";
                ddlPolice.DataBind();
                ddlPolice.Items.Insert(0, new ListItem("-- Select Police --", "0"));
            }
        }
    }

    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        lblMsg.Text = string.Empty;
        GridView1.EditIndex = -1;
        LoadData();
    }

    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int firId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);
        GridViewRow row = GridView1.Rows[e.RowIndex];
        DropDownList ddlPolice = row.FindControl("ddlPolice") as DropDownList;

        if (ddlPolice == null || ddlPolice.SelectedValue == "0")
        {
            lblMsg.ForeColor = System.Drawing.Color.Red;
            lblMsg.Text = "Please select a police officer.";
            return;
        }

        int policeId = Convert.ToInt32(ddlPolice.SelectedValue);

        string cs = ConfigurationManager.ConnectionStrings["efir_dbConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(cs))
        {
            string query = "UPDATE FIR SET assigned_to=@p, investigation_status='Assigned' WHERE fir_id=@f";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@p", policeId);
            cmd.Parameters.AddWithValue("@f", firId);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        lblMsg.ForeColor = System.Drawing.Color.Green;
        lblMsg.Text = "Case assigned successfully.";
        GridView1.EditIndex = -1;
        LoadData();
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("PoliceDashboard.aspx");
    }
}
