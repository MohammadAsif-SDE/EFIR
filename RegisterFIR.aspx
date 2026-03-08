<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RegisterFIR.aspx.cs" Inherits="RegisterFIR" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Register FIR</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            margin: 0;
            padding: 20px;
            min-height: 100vh;
        }
        .container {
            max-width: 600px;
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
        .form-group {
            margin-bottom: 20px;
        }
        label {
            display: block;
            font-weight: bold;
            margin-bottom: 5px;
            color: #333;
        }
        input[type="text"], input[type="date"], textarea {
            width: 100%;
            padding: 10px;
            border: 2px solid #ddd;
            border-radius: 5px;
            font-size: 14px;
            box-sizing: border-box;
        }
        input[type="text"]:focus, input[type="date"]:focus, textarea:focus {
            border-color: #667eea;
            outline: none;
        }
        textarea {
            resize: vertical;
            min-height: 100px;
        }
        .btn {
            background: #667eea;
            color: white;
            padding: 12px 30px;
            border: none;
            border-radius: 5px;
            font-size: 16px;
            cursor: pointer;
            width: 100%;
            margin-top: 10px;
        }
        .btn:hover {
            background: #5568d3;
        }
        .message {
            text-align: center;
            margin-top: 15px;
            font-weight: bold;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="container">
        <h2>Online eFIR Complaint Registration</h2>

        <div class="form-group">
            <label>Name:</label>
            <asp:TextBox ID="txtName" runat="server" />
        </div>

        <div class="form-group">
            <label>Mobile:</label>
            <asp:TextBox ID="txtMobile" runat="server" />
        </div>

        <div class="form-group">
            <label>Incident Date:</label>
            <asp:TextBox ID="txtDate" runat="server" TextMode="Date" />
        </div>

        <div class="form-group">
            <label>Incident Place:</label>
            <asp:TextBox ID="txtPlace" runat="server" />
        </div>

        <div class="form-group">
            <label>Description:</label>
            <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" />
        </div>

        <asp:Button ID="btnSubmit" runat="server" Text="Submit FIR" CssClass="btn"
            OnClick="btnSubmit_Click" />

        <asp:Label ID="lblMsg" runat="server" ForeColor="Green" CssClass="message" />
    </div>
    </form>
</body>
</html>
