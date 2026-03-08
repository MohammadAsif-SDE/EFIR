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
            CheckChiefAccess();
            LoadData();
        }
    }

    void CheckChiefAccess()
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
            
            bool isChief = result != null && Convert.ToBoolean(result);
            btnRegisterPolice.Visible = isChief;
            btnManageStations.Visible = isChief;
            btnAssignCases.Visible = isChief;
        }
    }

    void LoadData()
    {
        string cs = ConfigurationManager.ConnectionStrings["efir_dbConnectionString"].ConnectionString;

        using (SqlConnection con = new SqlConnection(cs))
        {
            EnsureFirColumns(con);

            SqlDataAdapter da = new SqlDataAdapter(
                "SELECT fir_id, fir_number, complaint_name, mobile, incident_date, incident_place, description, status, police_notes FROM FIR ORDER BY fir_id DESC",
                con);
            DataTable dt = new DataTable();
            da.Fill(dt);
              
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
    }

    private void EnsureFirColumns(SqlConnection con)
    {
        string query = @"
            IF COL_LENGTH('FIR', 'police_notes') IS NULL
                ALTER TABLE FIR ADD police_notes NVARCHAR(500) NULL;

            IF COL_LENGTH('FIR', 'fir_number') IS NULL
                ALTER TABLE FIR ADD fir_number NVARCHAR(50) NULL;";

        SqlCommand cmd = new SqlCommand(query, con);
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
    }

    protected void GridView1_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
    {
        lblMsg.Text = string.Empty;
        GridView1.EditIndex = e.NewEditIndex;
        LoadData();

        DropDownList ddlStatus = GridView1.Rows[e.NewEditIndex].FindControl("ddlStatus") as DropDownList;
        Label lblStatus = GridView1.Rows[e.NewEditIndex].FindControl("lblStatus") as Label;

        if (ddlStatus != null && lblStatus != null)
        {
            ListItem selected = ddlStatus.Items.FindByValue(lblStatus.Text.Trim());
            if (selected != null)
            {
                ddlStatus.ClearSelection();
                selected.Selected = true;
            }
        }
    }

    protected void GridView1_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
    {
        lblMsg.Text = string.Empty;
        GridView1.EditIndex = -1;
        LoadData();
    }

    protected void GridView1_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
    {
        string cs = ConfigurationManager.ConnectionStrings["efir_dbConnectionString"].ConnectionString;

        int id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);
        GridViewRow row = GridView1.Rows[e.RowIndex];
        DropDownList ddlStatus = row.FindControl("ddlStatus") as DropDownList;
        TextBox txtPoliceNotes = row.FindControl("txtPoliceNotes") as TextBox;

        string status = ddlStatus != null ? ddlStatus.SelectedValue : "Pending";
        string policeNotes = txtPoliceNotes != null ? txtPoliceNotes.Text.Trim() : string.Empty;

        if (status.Equals("Approved", StringComparison.OrdinalIgnoreCase) && string.IsNullOrWhiteSpace(policeNotes))
        {
            lblMsg.ForeColor = System.Drawing.Color.Red;
            lblMsg.Text = "Police notes are required when approving a complaint.";
            return;
        }

        using (SqlConnection con = new SqlConnection(cs))
        {
            EnsureFirColumns(con);

            string firNumber = null;

            if (status.Equals("Approved", StringComparison.OrdinalIgnoreCase))
            {
                string firNoQuery = "SELECT fir_number FROM FIR WHERE fir_id=@id";
                SqlCommand firNoCmd = new SqlCommand(firNoQuery, con);
                firNoCmd.Parameters.AddWithValue("@id", id);
                con.Open();
                object existingFirNumber = firNoCmd.ExecuteScalar();
                con.Close();

                string currentFirNumber = existingFirNumber == null || existingFirNumber == DBNull.Value
                    ? string.Empty
                    : existingFirNumber.ToString();

                if (string.IsNullOrWhiteSpace(currentFirNumber))
                {
                    firNumber = "FIR-" + DateTime.Now.Year + "-" + id.ToString("D6");
                }
                else
                {
                    firNumber = currentFirNumber;
                }
            }

            string query = "UPDATE FIR SET status=@s, police_notes=@n, fir_number=CASE WHEN @f IS NULL THEN fir_number ELSE @f END WHERE fir_id=@id";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@s", status);
            cmd.Parameters.AddWithValue("@n", policeNotes);
            cmd.Parameters.AddWithValue("@f", (object)firNumber ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@id", id);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            lblMsg.ForeColor = System.Drawing.Color.Green;
            if (status.Equals("Approved", StringComparison.OrdinalIgnoreCase) && !string.IsNullOrWhiteSpace(firNumber))
            {
                lblMsg.Text = "Complaint approved successfully. Generated FIR Number: " + firNumber;
            }
            else
            {
                lblMsg.Text = "Complaint updated successfully.";
            }
        }

        GridView1.EditIndex = -1;
        LoadData();
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string cs = ConfigurationManager.ConnectionStrings["efir_dbConnectionString"].ConnectionString;
        int id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);

        using (SqlConnection con = new SqlConnection(cs))
        {
            string query = "DELETE FROM FIR WHERE fir_id=@id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", id);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        lblMsg.ForeColor = System.Drawing.Color.Green;
        lblMsg.Text = "Complaint deleted successfully.";
        GridView1.EditIndex = -1;
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