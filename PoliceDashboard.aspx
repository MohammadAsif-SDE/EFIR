<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PoliceDashboard.aspx.cs" Inherits="PoliceDashboard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Police Dashboard</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h2>Police Dashboard</h2>
        
    <asp:Button ID="btnRegisterPolice" runat="server" Text="Register Police" OnClick="btnRegisterPolice_Click" />
    <asp:Button ID="btnManageStations" runat="server" Text="Manage Stations" OnClick="btnManageStations_Click" />
    <asp:Button ID="btnAssignCases" runat="server" Text="Assign Cases" OnClick="btnAssignCases_Click" />
    <asp:Button ID="btnLogout" runat="server" OnClick="btnLogout_Click" Text="Logout" />


        <br />
        <asp:Label ID="lblMsg" runat="server" ForeColor="Red" />

        <hr />
        <h3>Update FIR Status</h3>
        FIR ID:
        <asp:DropDownList ID="ddlFirId" runat="server" />
        <br /><br />

        Status:
        <asp:DropDownList ID="ddlStatusUpdate" runat="server">
            <asp:ListItem Text="Pending" Value="Pending" />
            <asp:ListItem Text="Approved" Value="Approved" />
            <asp:ListItem Text="Rejected" Value="Rejected" />
        </asp:DropDownList>
        <br /><br />

        FIR Number:
        <asp:TextBox ID="txtFirNumberUpdate" runat="server" Width="220px" />
        <br /><br />

        Police Notes:
        <asp:TextBox ID="txtPoliceNotesUpdate" runat="server" Width="320px" />
        <br /><br />

        <asp:Button ID="btnUpdateStatus" runat="server" Text="Update" OnClick="btnUpdateStatus_Click" />
        <asp:Button ID="btnDeleteFIR" runat="server" Text="Delete FIR" OnClick="btnDeleteFIR_Click"
            OnClientClick="return confirm('Are you sure you want to delete this FIR?');" />
        <br />
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="fir_id">
            <Columns>
                <asp:BoundField DataField="fir_id" HeaderText="fir_id" InsertVisible="False" ReadOnly="True" SortExpression="fir_id" />
                <asp:BoundField DataField="complaint_name" HeaderText="complaint_name" SortExpression="complaint_name" />
                <asp:BoundField DataField="mobile" HeaderText="mobile" SortExpression="mobile" />
                <asp:BoundField DataField="incident_date" HeaderText="incident_date" SortExpression="incident_date" />
                <asp:BoundField DataField="incident_place" HeaderText="incident_place" SortExpression="incident_place" />
                <asp:BoundField DataField="description" HeaderText="description" SortExpression="description" />
                <asp:BoundField DataField="status" HeaderText="status" SortExpression="status" />
                <asp:BoundField DataField="police_notes" HeaderText="police_notes" SortExpression="police_notes" />
                <asp:BoundField DataField="fir_number" HeaderText="fir_number" SortExpression="fir_number" />
                <asp:BoundField DataField="assigned_to" HeaderText="assigned_to" SortExpression="assigned_to" />
                <asp:BoundField DataField="investigation_status" HeaderText="investigation_status" SortExpression="investigation_status" />
            </Columns>
        </asp:GridView>
        <br />

    </div>
    </form>
</body>
</html>
