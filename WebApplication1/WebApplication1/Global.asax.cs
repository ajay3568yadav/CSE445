using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace WebApplication1
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            // Initialize application-wide variables
            Application["OnlineUsers"] = 0;
            Application["TotalVisits"] = 0;
            Application["ApplicationStartTime"] = DateTime.Now;

            // Create necessary directories
            string appDataPath = Server.MapPath("~/App_Data");
            if (!Directory.Exists(appDataPath))
            {
                Directory.CreateDirectory(appDataPath);
            }

            // Log application start
            LogApplicationEvent("Application Started");
        }

        private void LogApplicationEvent(string message)
        {
            string logPath = Server.MapPath("~/App_Data/application_log.txt");
            string logEntry = $"[{DateTime.Now}] APPLICATION: {message}\n";
            WriteToLog(logPath, logEntry);
        }

        private void WriteToLog(string logPath, string logEntry)
        {
            try
            {
                File.AppendAllText(logPath, logEntry);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error writing to log: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Attempted to log: {logEntry}");
            }
        }


        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}