using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class TryItPageEmployeeService : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
        }

        // Employee Management Service
        protected void btnAddEmployee_Click(object sender, EventArgs e)
        {
            try
            {
                using (ServiceReference2.Service1Client client = new ServiceReference2.Service1Client())
                {
                    var employee = new ServiceReference2.Employee
                    {
                        Name = txtName.Text,
                        Position = txtPosition.Text,
                        Salary = decimal.Parse(txtSalary.Text)
                    };

                    var addedEmployee = client.AddEmployee(employee);
                    DisplayEmployeeResult($"Employee added successfully. ID: {addedEmployee.Id}");
                    ClearEmployeeForm();
                }
            }
            catch (Exception ex)
            {
                DisplayEmployeeError($"Error adding employee: {ex.Message}");
            }
        }

        protected void btnUpdateEmployee_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtId.Text))
            {
                DisplayEmployeeError("Please enter an Employee ID to update.");
                return;
            }

            try
            {
                using (ServiceReference2.Service1Client client = new ServiceReference2.Service1Client())
                {
                    var employee = new ServiceReference2.Employee
                    {
                        Id = int.Parse(txtId.Text),
                        Name = txtName.Text,
                        Position = txtPosition.Text,
                        Salary = decimal.Parse(txtSalary.Text)
                    };

                    var updatedEmployee = client.UpdateEmployee(employee);
                    DisplayEmployeeResult($"Employee updated successfully. ID: {updatedEmployee.Id}");
                }
            }
            catch (Exception ex)
            {
                DisplayEmployeeError($"Error updating employee: {ex.Message}");
            }
        }

        protected void btnGetEmployee_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtGetId.Text))
            {
                DisplayEmployeeError("Please enter an Employee ID to retrieve.");
                return;
            }

            try
            {
                using (ServiceReference2.Service1Client client = new ServiceReference2.Service1Client())
                {
                    int id = int.Parse(txtGetId.Text);
                    var employee = client.GetEmployee(id);
                    DisplayEmployeeResult("Employee retrieved successfully.", employee);
                }
            }
            catch (Exception ex)
            {
                DisplayEmployeeError($"Error retrieving employee: {ex.Message}");
            }
        }

        protected void btnDeleteEmployee_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtDeleteId.Text))
            {
                DisplayEmployeeError("Please enter an Employee ID to delete.");
                return;
            }

            try
            {
                using (ServiceReference2.Service1Client client = new ServiceReference2.Service1Client())
                {
                    int id = int.Parse(txtDeleteId.Text);
                    client.DeleteEmployee(id);
                    DisplayEmployeeResult($"Employee with ID {id} deleted successfully.");
                    txtDeleteId.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                DisplayEmployeeError($"Error deleting employee: {ex.Message}");
            }
        }

        protected void btnGetAllEmployees_Click(object sender, EventArgs e)
        {
            try
            {
                using (ServiceReference2.Service1Client client = new ServiceReference2.Service1Client())
                {
                    var employees = client.GetAllEmployees();
                    DisplayEmployeeResult("All employees retrieved successfully.", employees);
                }
            }
            catch (Exception ex)
            {
                DisplayEmployeeError($"Error retrieving employees: {ex.Message}");
            }
        }
        private void DisplayEmployeeResult(string message, object data = null)
        {
            employeeResult.Text = $"<h4>{message}</h4>";
            if (data != null)
            {
                // Option 1: Use full namespace
                employeeResult.Text += $"<pre>{JsonConvert.SerializeObject(data, Newtonsoft.Json.Formatting.Indented)}</pre>";

                // OR Option 2: Create JsonSerializerSettings
                var settings = new JsonSerializerSettings
                {
                    Formatting = Newtonsoft.Json.Formatting.Indented
                };
                employeeResult.Text += $"<pre>{JsonConvert.SerializeObject(data, settings)}</pre>";
            }
        }

        private void DisplayEmployeeError(string errorMessage)
        {
            employeeResult.Text = $"<h4 style='color: red;'>Error:</h4><p>{errorMessage}</p>";
        }

        private void ClearEmployeeForm()
        {
            txtId.Text = string.Empty;
            txtName.Text = string.Empty;
            txtPosition.Text = string.Empty;
            txtSalary.Text = string.Empty;
        }
    }
}