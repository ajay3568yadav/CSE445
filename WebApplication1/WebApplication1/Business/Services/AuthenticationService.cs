using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Data.Interfaces;
using WebApplication1.Data.StateManagement;

namespace WebApplication1.Business.Services
{
    public class CustomAuthenticationService  // Changed from AuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IStateManager _stateManager;

        public CustomAuthenticationService(IUserRepository userRepository, IStateManager stateManager)
        {
            _userRepository = userRepository;
            _stateManager = stateManager;
        }

        public string GetCurrentUserToken()
        {
            return _stateManager.GetAuthenticationToken();
        }

        public bool AuthenticateUser(string email, string password, bool rememberMe)
        {
            if (_userRepository.ValidateUser(email, password))
            {
                _stateManager.SetAuthenticationToken(email, rememberMe);
                return true;
            }
            return false;
        }

        public void LogoutUser()
        {
            _stateManager.ClearAllState();
        }
    }
}