using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace WebApplication1.Models
{
    public class UserProfile
    {
        public string Email { get; set; }
        public DateTime LastLoginDate { get; set; }
        public string PreferredTheme { get; set; }
        public bool RememberMe { get; set; }
    }
}