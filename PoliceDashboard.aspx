<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PoliceDashboard.aspx.cs" Inherits="PoliceDashboard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Police Dashboard</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h2>Police Dashboard</h2>
        
    <asp:Button ID="btnRegisterPolice" runat="server" Text="Register Police" OnClick="btnRegisterPolice_Click" />
    <asp:Button ID="btnManageStations" runat="server" Text="Manage Stations" OnClick="btnManageStations_Click" />
    <asp:Button ID="btnAssignCases" runat="server" Text="Assign Cases" OnClick="btnAssignCases_Click" />
    <asp:Button ID="btnLogout" runat="server" OnClick="btnLogout_Click" Text="Logout" />


        <br />
        <asp:Label ID="lblMsg" runat="server" ForeColor="Red" />
        <br /><br />
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="fir_id" 
            OnRowEditing="GridView1_RowEditing" 
            OnRowUpdating="GridView1_RowUpdating" 
            OnRowCancelingEdit="GridView1_RowCancelingEdit"
            OnRowDeleting="GridView1_RowDeleting">
            <Columns>
                <asp:BoundField DataField="fir_id" HeaderText="fir_id" InsertVisible="False" ReadOnly="True" SortExpression="fir_id" />
                <asp:BoundField DataField="fir_number" HeaderText="fir_number" ReadOnly="True" SortExpression="fir_number" />
                <asp:BoundField DataField="complaint_name" HeaderText="complaint_name" SortExpression="complaint_name" />
                <asp:BoundField DataField="mobile" HeaderText="mobile" SortExpression="mobile" />
                <asp:BoundField DataField="incident_date" HeaderText="incident_date" SortExpression="incident_date" />
                <asp:BoundField DataField="incident_place" HeaderText="incident_place" SortExpression="incident_place" />
                <asp:BoundField DataField="description" HeaderText="description" SortExpression="description" />
                <asp:TemplateField HeaderText="status" SortExpression="status">
                    <ItemTemplate>
                        <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("status") %>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlStatus" runat="server">
                            <asp:ListItem Text="Pending" Value="Pending" />
                            <asp:ListItem Text="Approved" Value="Approved" />
                            <asp:ListItem Text="Rejected" Value="Rejected" />
                        </asp:DropDownList>
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="police_notes" SortExpression="police_notes">
                    <ItemTemplate>
                        <asp:Label ID="lblPoliceNotes" runat="server" Text='<%# Eval("police_notes") %>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtPoliceNotes" runat="server" Text='<%# Bind("police_notes") %>' Width="220px" />
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:CommandField ShowEditButton="True" />
                <asp:CommandField ShowDeleteButton="True" />
            </Columns>
        </asp:GridView>

    </div>
    </form>
</body>
</html>
