using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.Data.StateManagement
{
    public interface IStateManager
    {
        void SetAuthenticationToken(string email, bool persistent);
        string GetAuthenticationToken();
        void RemoveAuthenticationToken();
        void SetUserState(UserProfile profile);
        UserProfile GetUserState();
        string GetUserPreference(string key);
        void SetUserPreference(string key, string value);
        void ClearAllState();
    }
}