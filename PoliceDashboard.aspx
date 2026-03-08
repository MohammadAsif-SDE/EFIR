<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PoliceDashboard.aspx.cs" Inherits="PoliceDashboard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Police Dashboard</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background: #f5f7fa;
            margin: 0;
            padding: 0;
        }
        .header {
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            color: white;
            padding: 20px;
            box-shadow: 0 2px 4px rgba(0,0,0,0.1);
        }
        .header h2 {
            margin: 0;
            display: inline-block;
        }
        .btn-logout {
            float: right;
            background: white;
            color: #667eea;
            padding: 10px 20px;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            font-weight: bold;
        }
        .btn-logout:hover {
            background: #f0f0f0;
        }
        .container {
            max-width: 1200px;
            margin: 20px auto;
            padding: 20px;
            background: white;
            border-radius: 10px;
            box-shadow: 0 2px 4px rgba(0,0,0,0.1);
        }
        .message {
            text-align: center;
            margin: 15px 0;
            padding: 10px;
            border-radius: 5px;
            font-weight: bold;
        }
        table {
            width: 100%;
            border-collapse: collapse;
            margin-top: 20px;
        }
        table th {
            background: #667eea;
            color: white;
            padding: 12px;
            text-align: left;
            font-weight: bold;
        }
        table td {
            padding: 10px;
            border-bottom: 1px solid #ddd;
        }
        table tr:hover {
            background: #f8f9fa;
        }
        input[type="text"], select, textarea {
            width: 100%;
            padding: 8px;
            border: 2px solid #ddd;
            border-radius: 4px;
            box-sizing: border-box;
        }
        input[type="text"]:focus, select:focus, textarea:focus {
            border-color: #667eea;
            outline: none;
        }
        .grid-btn {
            background: #667eea;
            color: white;
            padding: 6px 12px;
            border: none;
            border-radius: 4px;
            cursor: pointer;
            margin: 2px;
        }
        .grid-btn:hover {
            background: #5568d3;
        }
        .delete-btn {
            background: #dc3545;
        }
        .delete-btn:hover {
            background: #c82333;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="header">
        <h2>Police Dashboard</h2>
        <asp:Button ID="btnLogout" runat="server" OnClick="btnLogout_Click" Text="Logout" CssClass="btn-logout" />
    </div>
    
    <div class="container">
        <asp:Label ID="lblMsg" runat="server" ForeColor="Red" CssClass="message" />
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
