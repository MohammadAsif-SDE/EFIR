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

    </div>
    </form>
</body>
</html>
