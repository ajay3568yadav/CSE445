using System;
using System.Web;
using WebApplication1.Business.Services;
using WebApplication1.Data;
using WebApplication1.Data.StateManagement;
using WebApplication1.Models;

namespace WebApplication1
{
    public partial class Login : System.Web.UI.Page
    {
        private readonly CustomAuthenticationService _authService;
        private readonly UserProfileService _profileService;

        public Login()
        {
            var stateManager = new CookieStateManager();
            var userRepository = new FileUserRepository();
            var profileRepository = new UserProfileRepository();

            _authService = new CustomAuthenticationService(userRepository, stateManager);
            _profileService = new UserProfileService(profileRepository, stateManager);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if user is already authenticated
            var authToken = _authService.GetCurrentUserToken();
            if (!string.IsNullOrEmpty(authToken))
            {
                RedirectToWelcome(authToken);
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text;
            bool rememberMe = chkRememberMe.Checked;

            try
            {
                if (_authService.AuthenticateUser(email, password, rememberMe))
                {
                    // Create and save user profile
                    var profile = new UserProfile
                    {
                        Email = email,
                        LastLoginDate = DateTime.Now,
                        PreferredTheme = "light",
                        RememberMe = rememberMe
                    };

                    _profileService.SaveUserProfile(profile);
                    RedirectToWelcome(email);
                }
                else
                {
                    lblMessage.Text = "Invalid email or password!";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "An error occurred. Please try again later.";
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        private void RedirectToWelcome(string email)
        {
            Response.Redirect($"WebForm1.aspx?email={HttpUtility.UrlEncode(email)}");

        }

        protected void lnkSignup_Click(object sender, EventArgs e)
        {
            Response.Redirect("Signup.aspx");
        }
    }
}