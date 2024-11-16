<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Signup.aspx.cs" Inherits="WebApplication1.Signup" %>
<%@ Import Namespace="WebApplication1.Data.StateManagement" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sign Up</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <style type="text/css">
        :root {
            --primary-color: #4a90e2;
            --error-color: #dc3545;
            --success-color: #28a745;
            --border-color: #ddd;
            --text-color: #333;
            --bg-color: #f8f9fa;
            --white: #ffffff;
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

        .auth-container {
            background: var(--white);
            width: 100%;
            max-width: 400px;
            padding: 2rem;
            border-radius: 10px;
            box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
        }

        .bg-dark .auth-container {
            background: #2d2d2d;
            box-shadow: 0 4px 6px rgba(0, 0, 0, 0.3);
        }

        h2 {
            text-align: center;
            margin-bottom: 1.5rem;
            color: var(--primary-color);
            font-size: 1.8rem;
        }

        .form-group {
            margin-bottom: 1.2rem;
        }

        .form-group label {
            display: block;
            margin-bottom: 0.5rem;
            font-weight: 500;
        }

        .form-control {
            width: 100%;
            padding: 0.8rem;
            border: 2px solid var(--border-color);
            border-radius: 5px;
            font-size: 1rem;
            transition: border-color 0.3s ease;
        }

        .form-control:focus {
            outline: none;
            border-color: var(--primary-color);
        }

        .bg-dark .form-control {
            background: #3d3d3d;
            border-color: #4d4d4d;
            color: var(--white);
        }

        .btn {
            background-color: var(--success-color);  /* Changed to green for signup */
            color: var(--white);
            padding: 0.8rem 1.5rem;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            font-size: 1rem;
            font-weight: 500;
            transition: background-color 0.3s ease;
            width: 100%;
            margin-bottom: 1rem;
        }

        .btn:hover {
            background-color: #218838;  /* Darker green on hover */
        }

        .login-link {
            text-align: center;
            display: block;
            color: var(--primary-color);
            text-decoration: none;
            font-weight: 500;
        }

        .login-link:hover {
            text-decoration: underline;
        }

        .message {
            text-align: center;
            margin-top: 1rem;
            padding: 0.5rem;
            border-radius: 4px;
        }

        .validator {
            color: var(--error-color);
            font-size: 0.85rem;
            margin-top: 0.3rem;
            display: block;
        }

        .password-requirements {
            font-size: 0.85rem;
            color: #666;
            margin-top: 0.3rem;
        }

        .bg-dark .password-requirements {
            color: #aaa;
        }

        @media (max-width: 480px) {
            .auth-container {
                padding: 1.5rem;
            }

            h2 {
                font-size: 1.5rem;
            }

            .form-control {
                padding: 0.7rem;
            }
        }
    </style>
</head>
<body class='<%=new CookieStateManager().GetUserPreference("theme") == "dark" ? "bg-dark" : "bg-light" %>'>
    <form id="form1" runat="server">
        <div class="auth-container">
            <h2>Create Account</h2>
            <div class="form-group">
                <asp:Label ID="Label1" AssociatedControlID="txtEmail" runat="server" Text="Email Address"></asp:Label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" 
                    placeholder="Enter your email" TextMode="Email" autocomplete="email"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvEmail" runat="server" 
                    ControlToValidate="txtEmail" CssClass="validator"
                    Display="Dynamic"
                    ErrorMessage="Email is required"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revEmail" runat="server" 
                    ControlToValidate="txtEmail" CssClass="validator"
                    Display="Dynamic"
                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                    ErrorMessage="Please enter a valid email address"></asp:RegularExpressionValidator>
            </div>
            <div class="form-group">
                <asp:Label ID="Label2" AssociatedControlID="txtPassword" runat="server" Text="Create Password"></asp:Label>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" 
                    placeholder="Create a strong password" autocomplete="new-password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvPassword" runat="server" 
                    ControlToValidate="txtPassword" CssClass="validator"
                    Display="Dynamic"
                    ErrorMessage="Password is required"></asp:RequiredFieldValidator>
                <div class="password-requirements">
                    Password Requirements:
                    <ul class="validation-list">
                        <li>At least 8 characters long</li>
                        <li>Contains at least one uppercase letter</li>
                        <li>Contains at least one number</li>
                        <li>Contains at least one special character</li>
                    </ul>
                </div>
            </div>
            <div class="form-group">
                <asp:Label ID="Label3" AssociatedControlID="txtConfirmPassword" runat="server" Text="Confirm Password"></asp:Label>
                <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" CssClass="form-control" 
                    placeholder="Confirm your password" autocomplete="new-password"></asp:TextBox>
                <asp:CompareValidator ID="cvPassword" runat="server" 
                    ControlToValidate="txtConfirmPassword" 
                    ControlToCompare="txtPassword" 
                    CssClass="validator"
                    Display="Dynamic"
                    ErrorMessage="Passwords do not match"></asp:CompareValidator>
            </div>
            <div class="form-group">
                <asp:Button ID="btnSignup" runat="server" Text="Create Account" CssClass="btn" OnClick="btnSignup_Click" />
                <asp:LinkButton ID="lnkLogin" runat="server" Text="Already have an account? Sign In" 
                    CssClass="login-link" OnClick="lnkLogin_Click" CausesValidation="false" />
            </div>
            <asp:Panel ID="pnlMessage" runat="server" CssClass="message-panel" Visible="false">
                <asp:Label ID="lblMessage" runat="server" CssClass="message"></asp:Label>
            </asp:Panel>
        </div>
    </form>
</body>
</html>