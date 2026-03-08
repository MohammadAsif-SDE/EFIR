using System;
using System.Configuration;
using System.Data.SqlClient;

public partial class FIRStatus : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string reference = Request.QueryString["ref"];
            if (!string.IsNullOrWhiteSpace(reference))
            {
                txtReference.Text = reference.Trim();
                SearchAndBind(reference, true);
            }
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        SearchAndBind(txtReference.Text, false);
    }

    private void SearchAndBind(string referenceInput, bool fromRegistrationRedirect)
    {
        lblMsg.ForeColor = System.Drawing.Color.Red;
        pnlResult.Visible = false;

        if (string.IsNullOrWhiteSpace(referenceInput))
        {
            lblMsg.Text = "Please enter a reference number.";
            return;
        }

        int firId;
        string normalizedReference = referenceInput.Trim().ToUpperInvariant();

        if (normalizedReference.StartsWith("FIR-"))
        {
            normalizedReference = normalizedReference.Substring(4);
        }

        if (!int.TryParse(normalizedReference, out firId))
        {
            lblMsg.Text = "Invalid reference number format.";
            return;
        }

        string cs = ConfigurationManager.ConnectionStrings["efir_dbConnectionString"].ConnectionString;

        using (SqlConnection con = new SqlConnection(cs))
        {
            string query = "SELECT fir_id, complaint_name, mobile, incident_date, incident_place, description, status, police_notes FROM FIR WHERE fir_id=@id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", firId);

            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                string formattedReference = "FIR-" + Convert.ToInt32(reader["fir_id"]).ToString("D6");
                lblReference.Text = formattedReference;
                lblName.Text = reader["complaint_name"].ToString();
                lblMobile.Text = reader["mobile"].ToString();
                lblDate.Text = Convert.ToDateTime(reader["incident_date"]).ToString("yyyy-MM-dd");
                lblPlace.Text = reader["incident_place"].ToString();
                lblDescription.Text = reader["description"].ToString();
                lblStatus.Text = reader["status"].ToString();
                lblPoliceNotes.Text = reader["police_notes"] == DBNull.Value || string.IsNullOrWhiteSpace(reader["police_notes"].ToString())
                    ? "-"
                    : reader["police_notes"].ToString();

                pnlResult.Visible = true;
                lblMsg.ForeColor = System.Drawing.Color.Green;

                if (fromRegistrationRedirect)
                {
                    lblMsg.Text = "FIR registered successfully. Your reference number is " + formattedReference + ".";
                }
                else
                {
                    lblMsg.Text = "FIR status found.";
                }
            }
            else
            {
                lblMsg.Text = "No FIR found for this reference number.";
            }

            reader.Close();
            con.Close();
        }
    }
}
