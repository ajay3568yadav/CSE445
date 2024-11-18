using System;
using WebApplication1.Business.Services;
using WebApplication1.Data;
using WebApplication1.Data.StateManagement;
using WebApplication1.Models;

namespace WebApplication1
{
    public partial class Welcome : System.Web.UI.Page
    {
        private readonly UserProfileService _profileService;

        public Welcome()
        {
            var stateManager = new CookieStateManager();
            var userRepository = new FileUserRepository();
            var profileRepository = new UserProfileRepository();

            
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

        }

        protected void ddlTheme_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {

        }
    }
}