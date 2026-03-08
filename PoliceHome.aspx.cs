using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PoliceHome : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Police"] == null)
            Response.Redirect("PoliceLogin.aspx");

        if (!IsPostBack)
        {
            lblWelcome.Text = Session["Police"].ToString();
            LoadMyCases();
        }
    }

    void LoadMyCases()
    {
        string cs = ConfigurationManager.ConnectionStrings["efir_dbConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(cs))
        {
            // Get current police ID
            string getPoliceIdQuery = "SELECT PoliceId FROM Police WHERE username=@u";
            SqlCommand cmdId = new SqlCommand(getPoliceIdQuery, con);
            cmdId.Parameters.AddWithValue("@u", Session["Police"].ToString());
            con.Open();
            object policeIdObj = cmdId.ExecuteScalar();
            con.Close();

            if (policeIdObj == null)
            {
                lblMsg.ForeColor = System.Drawing.Color.Red;
                lblMsg.Text = "Police record not found.";
                return;
            }

            int policeId = Convert.ToInt32(policeIdObj);

            // Load assigned cases
            SqlDataAdapter da = new SqlDataAdapter(
                "SELECT fir_id, fir_number, complaint_name, mobile, incident_date, incident_place, description, " +
                "ISNULL(investigation_status, 'Assigned') as investigation_status " +
                "FROM FIR WHERE assigned_to=@p AND status='Approved' " +
                "ORDER BY fir_id DESC", con);
            da.SelectCommand.Parameters.AddWithValue("@p", policeId);
            
            DataTable dt = new DataTable();
            da.Fill(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();

            if (dt.Rows.Count == 0)
            {
                lblMsg.ForeColor = System.Drawing.Color.Blue;
                lblMsg.Text = "No cases assigned to you yet.";
            }
        }
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int firId = Convert.ToInt32(e.CommandArgument);
        string cs = ConfigurationManager.ConnectionStrings["efir_dbConnectionString"].ConnectionString;

        if (e.CommandName == "StartInvestigation")
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "UPDATE FIR SET investigation_status='Investigation Started' WHERE fir_id=@f";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@f", firId);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            lblMsg.ForeColor = System.Drawing.Color.Green;
            lblMsg.Text = "Investigation started successfully.";
            LoadMyCases();
        }
        else if (e.CommandName == "MarkSolved")
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "UPDATE FIR SET investigation_status='Case Solved' WHERE fir_id=@f";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@f", firId);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            lblMsg.ForeColor = System.Drawing.Color.Green;
            lblMsg.Text = "Case marked as solved successfully.";
            LoadMyCases();
        }
    }

    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Response.Redirect("PoliceLogin.aspx");
    }
}
