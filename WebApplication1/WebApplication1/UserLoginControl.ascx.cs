using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebApplication1
{
    public partial class UserLoginControl : System.Web.UI.UserControl
    {
        private readonly string _captchaApiUrl = "https://venus.sod.asu.edu/WSRepository/Services/ImageVerifier/Service.svc/GetImage/";
        private bool _isAuthenticated = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Task.Run(()=>GenerateAndDisplayCaptcha()).GetAwaiter().GetResult();
            }
        }

        private string GenerateCaptchaText()
        {
            string captchaText = Path.GetRandomFileName().Replace(".", "").Substring(0, 5);
            Session["CaptchaText"] = captchaText;
            return captchaText;
        }

        private async Task GenerateAndDisplayCaptcha()
        {
            string captchaText = GenerateCaptchaText();

            using (HttpClient client = new HttpClient())
            {

                client.Timeout = TimeSpan.FromSeconds(30);  // Set a timeout limit (30 seconds)

                string requestUrl = $"{_captchaApiUrl}{captchaText}";
                try
                {
                    HttpResponseMessage response = await client.GetAsync(requestUrl).ConfigureAwait(false);

                    if (response.IsSuccessStatusCode)
                    {
                        byte[] imageBytes = await response.Content.ReadAsByteArrayAsync();
                        string base64Image = Convert.ToBase64String(imageBytes);
                        CaptchaImage.ImageUrl = "data:image/png;base64," + base64Image;
                    }
                    else
                    {
                        CaptchaMessage.Text = "Unable to load CAPTCHA. Please try again later.";
                    }
                }
                catch (TaskCanceledException)
                {
                    CaptchaMessage.Text = "Request timed out. Please try again later.";
                }
               
            }
        }

        protected void ValidateCaptchaButton_Click(object sender, EventArgs e)
        {
            if (CaptchaInput.Text == Session["CaptchaText"].ToString())
            {
                CaptchaMessage.Text = "CAPTCHA verified!";
                _isAuthenticated = true;
            }
            else
            {
                CaptchaMessage.Text = "Incorrect CAPTCHA, please try again.";
                Task.Run(() => GenerateAndDisplayCaptcha()).GetAwaiter().GetResult();
            }
        }

        public bool GetAuthenticated()
        {
            return _isAuthenticated;
        }

        public void ResetAuthenticated()
        {
            _isAuthenticated = false;
        }
    }
}