using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class PoliceDashboard : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Police"] == null)
            Response.Redirect("PoliceLogin.aspx");

        if (!IsPostBack)
        {
            LoadFirIds();
            LoadData();
        }
    }

    void LoadData()
    {
        string cs = ConfigurationManager.ConnectionStrings["efir_dbConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(cs))
        {
            SqlDataAdapter da = new SqlDataAdapter(
                "SELECT fir_id, complaint_name, mobile, incident_date, incident_place, description, status, police_notes, fir_number, assigned_to, investigation_status FROM FIR ORDER BY fir_id DESC",
                con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
    }

    void LoadFirIds()
    {
        string cs = ConfigurationManager.ConnectionStrings["efir_dbConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(cs))
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT fir_id FROM FIR ORDER BY fir_id DESC", con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            ddlFirId.DataSource = dt;
            ddlFirId.DataTextField = "fir_id";
            ddlFirId.DataValueField = "fir_id";
            ddlFirId.DataBind();
            ddlFirId.Items.Insert(0, new ListItem("Select FIR ID", ""));
        }
    }

    protected void btnUpdateStatus_Click(object sender, EventArgs e)
    {
        int id;
        if (!int.TryParse(ddlFirId.SelectedValue, out id))
        {
            lblMsg.ForeColor = System.Drawing.Color.Red;
            lblMsg.Text = "Enter a valid FIR ID.";
            return;
        }

        string cs = ConfigurationManager.ConnectionStrings["efir_dbConnectionString"].ConnectionString;
        string status = ddlStatusUpdate.SelectedValue;
        string firNumberInput = txtFirNumberUpdate.Text.Trim();
        string policeNotes = txtPoliceNotesUpdate.Text.Trim();

        using (SqlConnection con = new SqlConnection(cs))
        {

            string query = "UPDATE FIR SET status=@s, police_notes=@n, fir_number=CASE WHEN @f IS NULL THEN fir_number ELSE @f END WHERE fir_id=@id";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@s", status);
            cmd.Parameters.AddWithValue("@n", policeNotes);
            cmd.Parameters.AddWithValue("@f", firNumberInput);
            cmd.Parameters.AddWithValue("@id", id);

            con.Open();
            int rows = cmd.ExecuteNonQuery();
            con.Close();


            lblMsg.Text = "Complaint updated successfully.";
        }

        LoadData();
    }

    protected void btnDeleteFIR_Click(object sender, EventArgs e)
    {
        int id = Convert.ToInt32(ddlFirId.SelectedValue);

        string cs = ConfigurationManager.ConnectionStrings["efir_dbConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(cs))
        {
            string query = "DELETE FROM FIR WHERE fir_id=@id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", id);

            con.Open();
            int rows = cmd.ExecuteNonQuery();
            con.Close();
        }

        lblMsg.ForeColor = System.Drawing.Color.Green;
        lblMsg.Text = "FIR deleted successfully.";
        LoadFirIds();
        LoadData();
    }

    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Response.Redirect("PoliceLogin.aspx");
    }

    protected void btnRegisterPolice_Click(object sender, EventArgs e)
    {
        Response.Redirect("PoliceRegistration.aspx");
    }

    protected void btnManageStations_Click(object sender, EventArgs e)
    {
        Response.Redirect("PoliceStations.aspx");
    }

    protected void btnAssignCases_Click(object sender, EventArgs e)
    {
        Response.Redirect("AssignCases.aspx");
    }
}