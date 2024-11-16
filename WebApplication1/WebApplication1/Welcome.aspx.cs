using System;
using WebApplication1.Business.Services;
using WebApplication1.Data;
using WebApplication1.Data.StateManagement;
using WebApplication1.Models;

namespace WebApplication1
{
    public partial class Welcome : System.Web.UI.Page
    {
        private readonly AuthenticationService _authService;
        private readonly UserProfileService _profileService;

        public Welcome()
        {
            var stateManager = new CookieStateManager();
            var userRepository = new FileUserRepository();
            var profileRepository = new UserProfileRepository();

            _authService = new AuthenticationService(userRepository, stateManager);
            _profileService = new UserProfileService(profileRepository, stateManager);
        }

        protected string GetUserInitials()
        {
            string email = lblUserEmail.Text;
            if (string.IsNullOrEmpty(email)) return "?";
            return email.Substring(0, 1).ToUpper();
        }

        protected void btnUpdateProfile_Click(object sender, EventArgs e)
        {
            // Implement profile update functionality
            // For now, you can redirect to a new page or show a message
            Response.Redirect("UpdateProfile.aspx");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var userEmail = _authService.GetCurrentUserToken();

            if (string.IsNullOrEmpty(userEmail))
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                var userProfile = _profileService.GetUserProfile(userEmail);

                if (userProfile != null)
                {
                    lblUserEmail.Text = userProfile.Email;
                    lblLastLogin.Text = userProfile.LastLoginDate.ToString();
                    ddlTheme.SelectedValue = userProfile.PreferredTheme;
                }
                else
                {
                    // Handle case where profile doesn't exist
                    lblUserEmail.Text = userEmail;
                    lblLastLogin.Text = DateTime.Now.ToString();
                    ddlTheme.SelectedValue = "light";
                }
            }
        }

        protected void ddlTheme_SelectedIndexChanged(object sender, EventArgs e)
        {
            var userEmail = _authService.GetCurrentUserToken();
            if (!string.IsNullOrEmpty(userEmail))
            {
                _profileService.SaveUserPreferences(userEmail, ddlTheme.SelectedValue);
                Response.Redirect(Request.RawUrl);
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            _authService.LogoutUser();
            Response.Redirect("Login.aspx");
        }
    }
}