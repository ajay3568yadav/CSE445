using WebApplication1.Data.Interfaces;
using WebApplication1.Data.StateManagement;
using WebApplication1.Models;

namespace WebApplication1.Business.Services
{
    public class UserProfileService
    {
        private readonly IUserProfileRepository _profileRepository;
        private readonly IStateManager _stateManager;

        public UserProfileService(IUserProfileRepository profileRepository, IStateManager stateManager)
        {
            _profileRepository = profileRepository;
            _stateManager = stateManager;
        }

        public void SaveUserProfile(UserProfile profile)
        {
            _profileRepository.SaveUserProfile(profile);
            _stateManager.SetUserState(profile);
        }

        public UserProfile GetUserProfile(string email)
        {
            var profile = _stateManager.GetUserState();
            if (profile == null)
            {
                profile = _profileRepository.GetUserProfile(email);
                if (profile != null)
                {
                    _stateManager.SetUserState(profile);
                }
            }
            return profile;
        }

        public void SaveUserPreferences(string email, string theme)
        {
            _stateManager.SetUserPreference("theme", theme);
            _profileRepository.UpdateUserPreferences(email, theme);
        }
    }
}