<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PoliceRegistration.aspx.cs" Inherits="PoliceRegistration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Police Registration</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h2>Police Registration</h2>
        <asp:Button ID="btnBack" runat="server" Text="Back to Dashboard" OnClick="btnBack_Click" />
        <br /><br />

        Username:
        <asp:TextBox ID="txtUsername" runat="server" /><br /><br />

        Password:
        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" /><br /><br />

        Full Name:
        <asp:TextBox ID="txtFullName" runat="server" /><br /><br />

        Badge Number:
        <asp:TextBox ID="txtBadgeNumber" runat="server" /><br /><br />

        <asp:Button ID="btnRegister" runat="server" Text="Register Police" OnClick="btnRegister_Click" />
        <br /><br />
        <asp:Label ID="lblMsg" runat="server" ForeColor="Green" />

        <hr />
        <h3>Registered Police</h3>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="PoliceId"
            OnRowDeleting="GridView1_RowDeleting">
            <Columns>
                <asp:BoundField DataField="PoliceId" HeaderText="ID" ReadOnly="True" />
                <asp:BoundField DataField="username" HeaderText="Username" />
                <asp:BoundField DataField="full_name" HeaderText="Full Name" />
                <asp:BoundField DataField="badge_number" HeaderText="Badge Number" />
                <asp:BoundField DataField="station_name" HeaderText="Station" />
                <asp:CommandField ShowDeleteButton="True" />
            </Columns>
        </asp:GridView>
    </div>
    </form>
</body>
</html>
