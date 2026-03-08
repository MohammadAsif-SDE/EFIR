<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FIRStatus.aspx.cs" Inherits="FIRStatus" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>FIR Status Search</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h2>Search FIR Status</h2>

        Reference Number:
        <asp:TextBox ID="txtReference" runat="server" placeholder="Example: FIR-000001" />
        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />

        <br /><br />
        <asp:Label ID="lblMsg" runat="server" ForeColor="Green" />

        <br /><br />
        <asp:Panel ID="pnlResult" runat="server" Visible="false">
            <strong>Reference Number:</strong>
            <asp:Label ID="lblReference" runat="server" />
            <br /><br />

            <strong>Complaint Name:</strong>
            <asp:Label ID="lblName" runat="server" />
            <br /><br />

            <strong>Mobile:</strong>
            <asp:Label ID="lblMobile" runat="server" />
            <br /><br />

            <strong>Incident Date:</strong>
            <asp:Label ID="lblDate" runat="server" />
            <br /><br />

            <strong>Incident Place:</strong>
            <asp:Label ID="lblPlace" runat="server" />
            <br /><br />

            <strong>Status:</strong>
            <asp:Label ID="lblStatus" runat="server" />
            <br /><br />

            <strong>Police Notes:</strong>
            <asp:Label ID="lblPoliceNotes" runat="server" />
        </asp:Panel>

        <br /><br />
        <asp:HyperLink ID="lnkRegister" runat="server" NavigateUrl="RegisterFIR.aspx">Register New FIR</asp:HyperLink>
    </div>
    </form>
</body>
</html>
