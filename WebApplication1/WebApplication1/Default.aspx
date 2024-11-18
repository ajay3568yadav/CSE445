
<%@ Page Title="Home Page" Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication1.Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div>
        <!-- Introduction to the Application -->
        <p>Welcome to our web application! This platform provides features for managing member information, staff access control, and testing a discount web service. The Members page allows users to view and manage member details. The Staff page includes tools for managing staff operations securely. Additionally, our integrated web service can be used to test discount calculations for various scenarios.</p>

        <h1>Highlights</h1>

        <table class="table table-hover table-striped">
            <thead>
                <tr>
                    <th>Feature name</th>
                    <th>Description</th>
                    <th>Action</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td>Members page</td>
                    <td>Displays members info</td>
                    <td>
                        <asp:LinkButton ID="lbTryIt_members" runat="server" OnClick="lbTryIt_members_Click">Try It!</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td>Staff page</td>
                    <td>Displays staff info and Access Control operations</td>
                    <td>
                        <asp:LinkButton ID="lbTryIt_staff" runat="server" OnClick="lbTryIt_staff_Click">Try It!</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td>Web Service page</td>
                    <td>Testing a web service called Discount web service</td>
                    <td>
                        <asp:LinkButton ID="lbTryIt_ws" runat="server" OnClick="lbTryIt_ws_Click">Try It!</asp:LinkButton>
                    </td>
                </tr>
                <%-- Testing Instructions for TA/Grader --%>
                <p>To test the application, use the following credentials:</p>
                <ul>
                    <li><strong>Username:</strong> TA</li>
                    <li><strong>Password:</strong> 1234</li>
                </ul>
                <p>Instructions for testing:</p>
                <ol>
                    <li>Access the Staff page using the "Try It!" button and log in with the provided credentials.</li>
                    <li>Sign up as a member on the Members page to test the sign-up functionality.</li>
                    <li>Test the Discount Web Service by entering the necessary inputs and verifying the output.</li>
                </ol>
                <p>Make sure to check the login redirection functionality by attempting to access protected pages without logging in first.</p>

            </tbody>
        </table>
    </div>

</asp:Content>

