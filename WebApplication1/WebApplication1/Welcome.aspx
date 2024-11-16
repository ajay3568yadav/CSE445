<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Welcome.aspx.cs" Inherits="WebApplication1.Welcome" %>
<%@ Import Namespace="WebApplication1.Data.StateManagement" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Welcome</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <style type="text/css">
        :root {
            --primary-color: #4a90e2;
            --error-color: #dc3545;
            --success-color: #28a745;
            --danger-color: #dc3545;
            --border-color: #ddd;
            --text-color: #333;
            --bg-color: #f8f9fa;
            --white: #ffffff;
            --gray-100: #f8f9fa;
            --gray-200: #e9ecef;
            --gray-300: #dee2e6;
            --gray-600: #6c757d;
        }

        * {
            margin: 0;
            padding: 0;
            box-sizing: border-box;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        }

        .bg-dark {
            background-color: #1a1a1a;
            color: var(--white);
        }

        .bg-light {
            background-color: var(--bg-color);
            color: var(--text-color);
        }

        body {
            min-height: 100vh;
            display: flex;
            justify-content: center;
            align-items: center;
            padding: 20px;
        }

        .welcome-container {
            background: var(--white);
            width: 100%;
            max-width: 600px;
            padding: 2rem;
            border-radius: 10px;
            box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
        }

        .bg-dark .welcome-container {
            background: #2d2d2d;
            box-shadow: 0 4px 6px rgba(0, 0, 0, 0.3);
        }

        h2 {
            text-align: center;
            margin-bottom: 1.5rem;
            color: var(--primary-color);
            font-size: 2rem;
        }

        .user-info {
            background: var(--gray-100);
            border-radius: 8px;
            padding: 1.5rem;
            margin-bottom: 1.5rem;
        }

        .bg-dark .user-info {
            background: #3d3d3d;
        }

        .user-info p {
            margin-bottom: 1rem;
            font-size: 1.1rem;
            display: flex;
            justify-content: space-between;
            align-items: center;
        }

        .user-info p:last-child {
            margin-bottom: 0;
        }

        .user-info label {
            font-weight: 600;
            color: var(--gray-600);
        }

        .bg-dark .user-info label {
            color: var(--gray-300);
        }

        .theme-selector {
            margin-bottom: 1.5rem;
            padding: 1rem;
            background: var(--gray-100);
            border-radius: 8px;
            display: flex;
            align-items: center;
            gap: 1rem;
        }

        .bg-dark .theme-selector {
            background: #3d3d3d;
        }

        .theme-selector select {
            padding: 0.5rem;
            border: 2px solid var(--border-color);
            border-radius: 5px;
            font-size: 1rem;
            background-color: var(--white);
            cursor: pointer;
        }

        .bg-dark .theme-selector select {
            background: #4d4d4d;
            border-color: #5d5d5d;
            color: var(--white);
        }

        .btn {
            display: inline-block;
            padding: 0.8rem 1.5rem;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            font-size: 1rem;
            font-weight: 500;
            text-align: center;
            transition: all 0.3s ease;
            width: 100%;
        }

        .btn-danger {
            background-color: var(--danger-color);
            color: var(--white);
        }

        .btn-danger:hover {
            background-color: #c82333;
        }

        .profile-section {
            display: grid;
            grid-template-columns: 1fr;
            gap: 1rem;
        }

        .info-card {
            background: var(--white);
            padding: 1rem;
            border-radius: 8px;
            border: 1px solid var(--gray-200);
        }

        .bg-dark .info-card {
            background: #3d3d3d;
            border-color: #4d4d4d;
        }

        @media (max-width: 640px) {
            .welcome-container {
                padding: 1.5rem;
            }

            h2 {
                font-size: 1.5rem;
            }

            .user-info p {
                flex-direction: column;
                align-items: flex-start;
                gap: 0.5rem;
            }
        }
    </style>
</head>
<body class='<%=new CookieStateManager().GetUserPreference("theme") == "dark" ? "bg-dark" : "bg-light" %>'>
    <form id="form1" runat="server">
        <div class="welcome-container">
            <div class="profile-card">
                <div class="profile-header">
                    <div class="profile-avatar">
                        <%# GetUserInitials() %>
                    </div>
                    <div class="profile-info">
                        <h3>Welcome Back!</h3>
                        <asp:Label ID="lblUserEmail" runat="server" CssClass="email-display"></asp:Label>
                    </div>
                </div>
                
                <div class="user-info">
                    <p>
                        <strong>Last Login:</strong>
                        <asp:Label ID="lblLastLogin" runat="server"></asp:Label>
                    </p>
                </div>
            </div>

            <div class="profile-card settings-section">
                <h3>Preferences</h3>
                <div class="theme-selector">
                    <label for="ddlTheme">Theme:</label>
                    <asp:DropDownList ID="ddlTheme" runat="server" AutoPostBack="true" 
                        CssClass="form-control" OnSelectedIndexChanged="ddlTheme_SelectedIndexChanged">
                        <asp:ListItem Text="Light Theme" Value="light" />
                        <asp:ListItem Text="Dark Theme" Value="dark" />
                    </asp:DropDownList>
                </div>
            </div>

            <div class="btn-group">
                <asp:Button ID="btnLogout" runat="server" Text="Sign Out" 
                    CssClass="btn btn-danger" OnClick="btnLogout_Click" />
            </div>
        </div>
    </form>
</body>
</html>