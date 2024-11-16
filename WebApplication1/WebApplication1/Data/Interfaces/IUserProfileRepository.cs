using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.Data.Interfaces
{
    public interface IUserProfileRepository
    {
        void SaveUserProfile(UserProfile profile);
        UserProfile GetUserProfile(string email);
        void UpdateUserPreferences(string email, string theme);
    }
}