<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FIRStatus.aspx.cs" Inherits="FIRStatus" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>FIR Status Search</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            margin: 0;
            padding: 20px;
            min-height: 100vh;
        }
        .container {
            max-width: 700px;
            margin: 0 auto;
            background: white;
            padding: 30px;
            border-radius: 10px;
            box-shadow: 0 4px 6px rgba(0,0,0,0.1);
        }
        h2 {
            color: #667eea;
            text-align: center;
            margin-bottom: 30px;
        }
        .search-box {
            display: flex;
            gap: 10px;
            margin-bottom: 20px;
        }
        input[type="text"] {
            flex: 1;
            padding: 10px;
            border: 2px solid #ddd;
            border-radius: 5px;
            font-size: 14px;
        }
        input[type="text"]:focus {
            border-color: #667eea;
            outline: none;
        }
        .btn {
            background: #667eea;
            color: white;
            padding: 10px 25px;
            border: none;
            border-radius: 5px;
            font-size: 14px;
            cursor: pointer;
        }
        .btn:hover {
            background: #5568d3;
        }
        .message {
            text-align: center;
            margin: 15px 0;
            font-weight: bold;
        }
        .result-panel {
            background: #f8f9fa;
            padding: 20px;
            border-radius: 8px;
            border-left: 4px solid #667eea;
        }
        .result-row {
            margin-bottom: 15px;
            padding-bottom: 10px;
            border-bottom: 1px solid #ddd;
        }
        .result-row:last-child {
            border-bottom: none;
        }
        .result-label {
            font-weight: bold;
            color: #555;
            display: inline-block;
            min-width: 150px;
        }
        .result-value {
            color: #333;
        }
        .link {
            display: block;
            text-align: center;
            margin-top: 20px;
            color: #667eea;
            text-decoration: none;
            font-weight: bold;
        }
        .link:hover {
            text-decoration: underline;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="container">
        <h2>Search FIR Status</h2>

        <div class="search-box">
            <asp:TextBox ID="txtReference" runat="server" placeholder="Example: FIR-000001" />
            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn" OnClick="btnSearch_Click" />
        </div>

        <asp:Label ID="lblMsg" runat="server" ForeColor="Green" CssClass="message" />

        <asp:Panel ID="pnlResult" runat="server" Visible="false" CssClass="result-panel">
            <div class="result-row">
                <span class="result-label">Reference Number:</span>
                <span class="result-value"><asp:Label ID="lblReference" runat="server" /></span>
            </div>

            <div class="result-row">
                <span class="result-label">Complaint Name:</span>
                <span class="result-value"><asp:Label ID="lblName" runat="server" /></span>
            </div>

            <div class="result-row">
                <span class="result-label">Mobile:</span>
                <span class="result-value"><asp:Label ID="lblMobile" runat="server" /></span>
            </div>

            <div class="result-row">
                <span class="result-label">Incident Date:</span>
                <span class="result-value"><asp:Label ID="lblDate" runat="server" /></span>
            </div>

            <div class="result-row">
                <span class="result-label">Incident Place:</span>
                <span class="result-value"><asp:Label ID="lblPlace" runat="server" /></span>
            </div>

            <div class="result-row">
                <span class="result-label">Status:</span>
                <span class="result-value"><asp:Label ID="lblStatus" runat="server" /></span>
            </div>

            <div class="result-row">
                <span class="result-label">Police Notes:</span>
                <span class="result-value"><asp:Label ID="lblPoliceNotes" runat="server" /></span>
            </div>
        </asp:Panel>

        <asp:HyperLink ID="lnkRegister" runat="server" NavigateUrl="RegisterFIR.aspx" CssClass="link">Register New FIR</asp:HyperLink>
    </div>
    </form>
</body>
</html>
