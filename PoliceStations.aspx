<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PoliceStations.aspx.cs" Inherits="PoliceStations" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Police Stations Management</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h2>Police Stations Management</h2>
        <asp:Button ID="btnBack" runat="server" Text="Back to Dashboard" OnClick="btnBack_Click" />
        <br /><br />

        <h3>Create Station</h3>
        Station Name:
        <asp:TextBox ID="txtStationName" runat="server" /><br /><br />

        Address:
        <asp:TextBox ID="txtAddress" runat="server" /><br /><br />

        <asp:Button ID="btnCreate" runat="server" Text="Create Station" OnClick="btnCreate_Click" />
        <br /><br />
        <asp:Label ID="lblMsg" runat="server" ForeColor="Green" />

        <hr />
        <h3>Police Stations</h3>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="station_id"
            OnRowDeleting="GridView1_RowDeleting">
            <Columns>
                <asp:BoundField DataField="station_id" HeaderText="ID" ReadOnly="True" />
                <asp:BoundField DataField="station_name" HeaderText="Station Name" />
                <asp:BoundField DataField="address" HeaderText="Address" />
                <asp:CommandField ShowDeleteButton="True" />
            </Columns>
        </asp:GridView>

        <hr />
        <h3>Assign Police to Station</h3>
        Select Police:
        <asp:DropDownList ID="ddlPolice" runat="server" />
        <br /><br />

        Select Station:
        <asp:DropDownList ID="ddlStation" runat="server" />
        <br /><br />

        <asp:Button ID="btnAssign" runat="server" Text="Assign to Station" OnClick="btnAssign_Click" />
        <br /><br />
        <asp:Label ID="lblAssignMsg" runat="server" ForeColor="Green" />
    </div>
    </form>
</body>
</html>
