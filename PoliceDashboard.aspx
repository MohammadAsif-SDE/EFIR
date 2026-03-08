<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PoliceDashboard.aspx.cs" Inherits="PoliceDashboard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h2>Police Dashboard</h2>


        <asp:Button ID="btnLogout" runat="server" OnClick="btnLogout_Click" Text="Logout" />


        <br />
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="fir_id" 
            OnRowEditing="GridView1_RowEditing" 
            OnRowUpdating="GridView1_RowUpdating" 
            OnRowCancelingEdit="GridView1_RowCancelingEdit">
            <Columns>
                <asp:BoundField DataField="fir_id" HeaderText="fir_id" InsertVisible="False" ReadOnly="True" SortExpression="fir_id" />
                <asp:BoundField DataField="complaint_name" HeaderText="complaint_name" SortExpression="complaint_name" />
                <asp:BoundField DataField="mobile" HeaderText="mobile" SortExpression="mobile" />
                <asp:BoundField DataField="incident_date" HeaderText="incident_date" SortExpression="incident_date" />
                <asp:BoundField DataField="incident_place" HeaderText="incident_place" SortExpression="incident_place" />
                <asp:BoundField DataField="description" HeaderText="description" SortExpression="description" />
                <asp:BoundField DataField="status" HeaderText="status" SortExpression="status" />
            </Columns>
        </asp:GridView>

    </div>
    </form>
</body>
</html>
