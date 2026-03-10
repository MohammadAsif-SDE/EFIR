<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AssignCases.aspx.cs" Inherits="AssignCases" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Assign Cases to Police</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h2>Assign Cases to Police</h2>
        <asp:Button ID="btnBack" runat="server" Text="Back to Dashboard" OnClick="btnBack_Click" />
        <br /><br />

        <asp:Label ID="lblMsg" runat="server" ForeColor="Green" />
        <br /><br />

        FIR ID:
        <asp:DropDownList ID="ddlFirId" runat="server" />
        <br /><br />

        Police:
        <asp:DropDownList ID="ddlPolice" runat="server" />
        <br /><br />

        <asp:Button ID="btnAssign" runat="server" Text="Assign" OnClick="btnAssign_Click" />
        <br /><br />

        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="fir_id">
            <Columns>
                <asp:BoundField DataField="fir_id" HeaderText="FIR ID" ReadOnly="True" />
                <asp:BoundField DataField="fir_number" HeaderText="FIR Number" ReadOnly="True" />
                <asp:BoundField DataField="complaint_name" HeaderText="Complainant" ReadOnly="True" />
                <asp:BoundField DataField="incident_place" HeaderText="Place" ReadOnly="True" />
                <asp:BoundField DataField="description" HeaderText="Description" ReadOnly="True" />
                <asp:BoundField DataField="assigned_to_name" HeaderText="Assigned To" ReadOnly="True" />
                <asp:BoundField DataField="investigation_status" HeaderText="Investigation Status" ReadOnly="True" />
            </Columns>
        </asp:GridView>
    </div>
    </form>
</body>
</html>
