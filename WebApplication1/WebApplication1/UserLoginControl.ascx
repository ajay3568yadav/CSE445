<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserLoginControl.ascx.cs" Inherits="WebApplication1.UserLoginControl" %>
<asp:Image ID="CaptchaImage" runat="server" />
<asp:TextBox ID="CaptchaInput" runat="server" placeholder="Enter CAPTCHA" />
<asp:Button ID="ValidateCaptchaButton" runat="server" Text="Verify" OnClick="ValidateCaptchaButton_Click" />
<asp:Label ID="CaptchaMessage" runat="server" placeholder=""/>