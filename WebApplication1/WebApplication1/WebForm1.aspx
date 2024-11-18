<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebApplication1.WebForm1" %>
<%@ Import Namespace="WebApplication1.Data.StateManagement" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Combined Services Test Page</title>
    <style>
        body { font-family: Arial, sans-serif; line-height: 1.6; padding: 20px; }
        h1, h2 { color: #333; }
        #form1 { max-width: 800px; margin: 0 auto; }
        .service-section { margin-bottom: 40px; border: 1px solid #ddd; padding: 20px; border-radius: 5px; }
        .form-group { margin-bottom: 15px; }
        label { display: block; margin-bottom: 5px; }
        input[type="file"], input[type="text"], input[type="number"] { display: block; margin-bottom: 10px; width: 100%; padding: 5px; }
        .btn-submit { padding: 10px 15px; background-color: #007bff; color: white; border: none; cursor: pointer; }
        .btn-submit:hover { background-color: #0056b3; }
        .result { margin-top: 20px; padding: 10px; border: 1px solid #ddd; background-color: #f9f9f9; }
        .navigation { margin-bottom: 20px; padding: 10px; background-color: #f0f0f0; border-radius: 5px; }
        .navigation a { margin-right: 10px; text-decoration: none; color: #007bff; }
        .navigation a:hover { text-decoration: underline; }
    </style>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script>
        $(document).ready(function () {
            $('#btnWordCount').on('click', function (e) {
                e.preventDefault();
                var formData = new FormData();
                formData.append('file', $('#fileUpload')[0].files[0]);
                $.ajax({
                    url: '/api/wordcount',
                    type: 'POST',
                    data: formData,
                    processData: false,
                    contentType: false,
                    success: function (data) {
                        $('#wordCountResult').html('<h3>Word Count Results:</h3><pre>' + JSON.stringify(JSON.parse(data), null, 2) + '</pre>');
                    },
                    error: function (xhr, status, error) {
                        $('#wordCountResult').html('<h3>Error:</h3><p>' + xhr.responseText + '</p>');
                    }
                });
            });
        });
    </script>
</head>
<body class='<%=new CookieStateManager().GetUserPreference("theme") == "dark" ? "bg-dark" : "bg-light" %>'>
    <form id="form1" runat="server">
        <h1>Services Test Page</h1>

        <div class="service-section">
            <h2>Employee Management Service  (Elective WebService : WSDL)</h2>
            <p>This service allows you to manage employee records.</p>

            <h3>Add/Update Employee</h3>
            <div class="form-group">
                <label for="txtId">ID:</label>
                <asp:TextBox ID="txtId" runat="server" TextMode="Number"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="txtName">Name:</label>
                <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="txtPosition">Position:</label>
                <asp:TextBox ID="txtPosition" runat="server"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="txtSalary">Salary:</label>
                <asp:TextBox ID="txtSalary" runat="server" TextMode="Number" step="0.01"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:Button ID="btnAddEmployee" runat="server" Text="Add Employee" OnClick="btnAddEmployee_Click" CssClass="btn-submit" />
                <asp:Button ID="btnUpdateEmployee" runat="server" Text="Update Employee" OnClick="btnUpdateEmployee_Click" CssClass="btn-submit" />
            </div>

            <h3>Get Employee</h3>
            <div class="form-group">
                <label for="txtGetId">Employee ID:</label>
                <asp:TextBox ID="txtGetId" runat="server" TextMode="Number"></asp:TextBox>
                <asp:Button ID="btnGetEmployee" runat="server" Text="Get Employee" OnClick="btnGetEmployee_Click" CssClass="btn-submit" />
            </div>

            <h3>Delete Employee</h3>
            <div class="form-group">
                <label for="txtDeleteId">Employee ID:</label>
                <asp:TextBox ID="txtDeleteId" runat="server" TextMode="Number"></asp:TextBox>
                <asp:Button ID="btnDeleteEmployee" runat="server" Text="Delete Employee" OnClick="btnDeleteEmployee_Click" CssClass="btn-submit" />
            </div>

            <h3>Get All Employees</h3>
            <div class="form-group">
                <asp:Button ID="btnGetAllEmployees" runat="server" Text="Get All Employees" OnClick="btnGetAllEmployees_Click" CssClass="btn-submit" />
            </div>

            <asp:Literal ID="employeeResult" runat="server"></asp:Literal>
        </div>
                                <div class="btn-group">
                <asp:Button ID="btnLogout" runat="server" Text="Sign Out" 
                    CssClass="btn btn-danger" OnClick="btnLogout_Click" />
            </div>
    </form>

</body>
</html>
