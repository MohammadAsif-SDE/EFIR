using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

public partial class RegisterFIR : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string cs = ConfigurationManager.ConnectionStrings["efir_dbConnectionString"].ConnectionString;

        using (SqlConnection con = new SqlConnection(cs))
        {
            string query = "INSERT INTO Fir (complaint_name, mobile, incident_date, Incident_place, description, status) " +
                           "OUTPUT INSERTED.fir_id " +
                           "VALUES (@name, @mobile, @date, @place, @desc, 'Pending');";

            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@name", txtName.Text);
            cmd.Parameters.AddWithValue("@mobile", txtMobile.Text);
            cmd.Parameters.AddWithValue("@date", txtDate.Text);
            cmd.Parameters.AddWithValue("@place", txtPlace.Text);
            cmd.Parameters.AddWithValue("@desc", txtDescription.Text);

            con.Open();
            int firId = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();

            lblMsg.Text = "Complaint registered successfully. Your FIR ID is " + firId + ".";
            lnkCheckStatus.Visible = true;
        }
    }
}