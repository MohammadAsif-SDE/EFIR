<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PoliceLogin.aspx.cs" Inherits="PoliceLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h2>Police Login</h2>
    
        UserName:
        <asp:TextBox ID="txtUser" runat="server"></asp:TextBox>
        <br />
        Password:
        <asp:TextBox ID="txtPass" runat="server" TextMode="Password"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="btnLogin" runat="server" OnClick="btnLogin_Click" Text="Login" />
        <br />
        <br />
        <br />
        <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
    
    </div>
    </form>
</body>
</html>
