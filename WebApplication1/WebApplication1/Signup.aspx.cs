using System;
using System.Web;
using WebApplication1.Business.Services;
using WebApplication1.Data;
using WebApplication1.Data.StateManagement;
using WebApplication1.Models;

namespace WebApplication1
{
    public partial class Signup : System.Web.UI.Page
    {
        private readonly CustomAuthenticationService _authService;
        private readonly UserProfileService _profileService;
        private readonly FileUserRepository _userRepository;

        public Signup()
        {
            var stateManager = new CookieStateManager();
            _userRepository = new FileUserRepository();
            var profileRepository = new UserProfileRepository();

            _authService = new CustomAuthenticationService(_userRepository, stateManager);
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

        protected void btnSignup_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text;

            try
            {
                var newUser = new User { Email = email, Password = password };

                if (_userRepository.CreateUser(newUser))
                {
                    // Create and save user profile
                    var profile = new UserProfile
                    {
                        Email = email,
                        LastLoginDate = DateTime.Now,
                        PreferredTheme = "light",
                        RememberMe = false
                    };

                    _profileService.SaveUserProfile(profile);

                    // Authenticate the new user
                    _authService.AuthenticateUser(email, password, false);

                    RedirectToWelcome(email);
                }
                else
                {
                    lblMessage.Text = "Email already exists!";
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

        protected void lnkLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }
    }
}