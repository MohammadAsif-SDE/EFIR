using System;
using System.Configuration;
using System.Data.SqlClient;

public partial class FIRStatus : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        SearchAndBind(txtReference.Text);
    }

    private void SearchAndBind(string firIdInput)
    {
        lblMsg.ForeColor = System.Drawing.Color.Red;
        pnlResult.Visible = false;

        int firId;
        if (!int.TryParse(firIdInput.Trim(), out firId))
        {
            lblMsg.Text = "Enter a valid FIR ID.";
            return;
        }

        string cs = ConfigurationManager.ConnectionStrings["efir_dbConnectionString"].ConnectionString;

        using (SqlConnection con = new SqlConnection(cs))
        {
            string query = @"SELECT f.fir_id, f.complaint_name, f.mobile, f.incident_date, f.incident_place,
                                    f.status,
                                    COALESCE(f.investigation_status, 'Pending') AS investigation_status,
                             COALESCE(NULLIF(CAST(f.assigned_to AS NVARCHAR(100)), ''), 'Not Assigned') AS assigned_to_name,
                                    f.police_notes
                             FROM FIR f
                             WHERE f.fir_id = @id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", firId);

            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                lblReference.Text = reader["fir_id"].ToString();
                lblName.Text = reader["complaint_name"].ToString();
                lblMobile.Text = reader["mobile"].ToString();
                lblDate.Text = Convert.ToDateTime(reader["incident_date"]).ToString("yyyy-MM-dd");
                lblPlace.Text = reader["incident_place"].ToString();
                lblStatus.Text = reader["status"].ToString();
                lblInvestStatus.Text = reader["investigation_status"].ToString();
                lblAssignedTo.Text = reader["assigned_to_name"].ToString();
                lblPoliceNotes.Text = reader["police_notes"] == DBNull.Value || string.IsNullOrWhiteSpace(reader["police_notes"].ToString())
                    ? "-"
                    : reader["police_notes"].ToString();

                pnlResult.Visible = true;
                lblMsg.ForeColor = System.Drawing.Color.Green;
                lblMsg.Text = "FIR status found.";
            }
            else
            {
                lblMsg.Text = "No FIR found for this ID.";
            }

            reader.Close();
            con.Close();
        }
    }
}
