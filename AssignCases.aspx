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

        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="fir_id"
            OnRowEditing="GridView1_RowEditing"
            OnRowUpdating="GridView1_RowUpdating"
            OnRowCancelingEdit="GridView1_RowCancelingEdit">
            <Columns>
                <asp:BoundField DataField="fir_id" HeaderText="FIR ID" ReadOnly="True" />
                <asp:BoundField DataField="fir_number" HeaderText="FIR Number" ReadOnly="True" />
                <asp:BoundField DataField="complaint_name" HeaderText="Complainant" ReadOnly="True" />
                <asp:BoundField DataField="incident_place" HeaderText="Place" ReadOnly="True" />
                <asp:BoundField DataField="description" HeaderText="Description" ReadOnly="True" />
                
                <asp:TemplateField HeaderText="Assigned To">
                    <ItemTemplate>
                        <asp:Label ID="lblAssigned" runat="server" Text='<%# Eval("assigned_to_name") %>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlPolice" runat="server" />
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Investigation Status">
                    <ItemTemplate>
                        <asp:Label ID="lblInvestStatus" runat="server" Text='<%# Eval("investigation_status") %>' />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:CommandField ShowEditButton="True" />
            </Columns>
        </asp:GridView>
    </div>
    </form>
</body>
</html>
