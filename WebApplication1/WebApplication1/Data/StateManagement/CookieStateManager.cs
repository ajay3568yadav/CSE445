using System;
using System.Web;
using WebApplication1.Models;
using WebApplication1.Data.StateManagement;

namespace WebApplication1.Data.StateManagement
{
    public class CookieStateManager : IStateManager
    {
        private const string AUTH_COOKIE = "AuthCookie";
        private const string THEME_COOKIE = "UserTheme";
        private const string USER_PROFILE_KEY = "UserProfile";
        private const int COOKIE_EXPIRY_DAYS = 30;

        public void SetAuthenticationToken(string email, bool persistent)
        {
            HttpCookie authCookie = new HttpCookie(AUTH_COOKIE, email);

            if (persistent)
            {
                authCookie.Expires = DateTime.Now.AddDays(COOKIE_EXPIRY_DAYS);
            }

            authCookie.HttpOnly = true;
            if (HttpContext.Current.Request.IsSecureConnection)
            {
                authCookie.Secure = true;
            }

            HttpContext.Current.Response.Cookies.Add(authCookie);
        }

        public string GetAuthenticationToken()
        {
            HttpCookie authCookie = HttpContext.Current.Request.Cookies[AUTH_COOKIE];
            return authCookie?.Value;
        }

        public void RemoveAuthenticationToken()
        {
            if (HttpContext.Current.Request.Cookies[AUTH_COOKIE] != null)
            {
                HttpCookie authCookie = new HttpCookie(AUTH_COOKIE);
                authCookie.Expires = DateTime.Now.AddDays(-1);
                HttpContext.Current.Response.Cookies.Add(authCookie);
            }
        }

        public void SetUserState(UserProfile profile)
        {
            HttpContext.Current.Session[USER_PROFILE_KEY] = profile;
            SetUserPreference("theme", profile.PreferredTheme);
        }

        public UserProfile GetUserState()
        {
            return HttpContext.Current.Session[USER_PROFILE_KEY] as UserProfile;
        }

        public string GetUserPreference(string key)
        {
            switch (key.ToLower())
            {
                case "theme":
                    HttpCookie themeCookie = HttpContext.Current.Request.Cookies[THEME_COOKIE];
                    return themeCookie?.Value ?? "light";
                default:
                    return null;
            }
        }

        public void SetUserPreference(string key, string value)
        {
            switch (key.ToLower())
            {
                case "theme":
                    HttpCookie themeCookie = new HttpCookie(THEME_COOKIE, value);
                    themeCookie.Expires = DateTime.Now.AddDays(COOKIE_EXPIRY_DAYS);
                    HttpContext.Current.Response.Cookies.Add(themeCookie);
                    break;
            }
        }

        public void ClearAllState()
        {
            // Clear session
            HttpContext.Current.Session.Clear();

            // Clear auth cookie
            RemoveAuthenticationToken();

            // Clear theme cookie
            HttpCookie themeCookie = new HttpCookie(THEME_COOKIE);
            themeCookie.Expires = DateTime.Now.AddDays(-1);
            HttpContext.Current.Response.Cookies.Add(themeCookie);


        }
    }
}