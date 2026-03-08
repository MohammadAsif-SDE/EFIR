<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PoliceHome.aspx.cs" Inherits="PoliceHome" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Police Home</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h2>My Cases - <asp:Label ID="lblWelcome" runat="server" /></h2>
        <asp:Button ID="btnLogout" runat="server" Text="Logout" OnClick="btnLogout_Click" />
        <br /><br />

        <asp:Label ID="lblMsg" runat="server" ForeColor="Green" />
        <br /><br />

        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="fir_id"
            OnRowCommand="GridView1_RowCommand">
            <Columns>
                <asp:BoundField DataField="fir_number" HeaderText="FIR Number" ReadOnly="True" />
                <asp:BoundField DataField="complaint_name" HeaderText="Complainant" ReadOnly="True" />
                <asp:BoundField DataField="mobile" HeaderText="Mobile" ReadOnly="True" />
                <asp:BoundField DataField="incident_date" HeaderText="Date" ReadOnly="True" DataFormatString="{0:yyyy-MM-dd}" />
                <asp:BoundField DataField="incident_place" HeaderText="Place" ReadOnly="True" />
                <asp:BoundField DataField="description" HeaderText="Description" ReadOnly="True" />
                <asp:BoundField DataField="investigation_status" HeaderText="Status" ReadOnly="True" />
                
                <asp:TemplateField HeaderText="Action">
                    <ItemTemplate>
                        <asp:Button ID="btnStart" runat="server" Text="Start Investigation" 
                            CommandName="StartInvestigation" CommandArgument='<%# Eval("fir_id") %>'
                            Visible='<%# Eval("investigation_status").ToString() == "Assigned" %>' />
                        <asp:Button ID="btnSolve" runat="server" Text="Mark Solved" 
                            CommandName="MarkSolved" CommandArgument='<%# Eval("fir_id") %>'
                            Visible='<%# Eval("investigation_status").ToString() == "Investigation Started" %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    </form>
</body>
</html>
