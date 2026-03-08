<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RegisterFIR.aspx.cs" Inherits="RegisterFIR" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h2>Online eFIR Complaint Registration</h2>

        Name:
        <asp:TextBox ID="txtName" runat="server" /><br /><br />

        Mobile:
        <asp:TextBox ID="txtMobile" runat="server" /><br /><br />

        Incident Date:
        <asp:TextBox ID="txtDate" runat="server" TextMode="Date" /><br /><br />

        Incident Place:
        <asp:TextBox ID="txtPlace" runat="server" /><br /><br />

        Description:
        <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" /><br /><br />

        <asp:Button ID="btnSubmit" runat="server" Text="Submit FIR"
            OnClick="btnSubmit_Click" />

        <br /><br />
        <asp:Label ID="lblMsg" runat="server" ForeColor="Green" />
    </div>
    </form>
</body>
</html>
