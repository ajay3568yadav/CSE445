using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using WebApplication1.Data.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class UserProfileRepository : IUserProfileRepository
    {
        private static string FilePath
        {
            get
            {
                string path = HttpContext.Current.Server.MapPath("~/App_Data/userprofiles.txt");
                Directory.CreateDirectory(Path.GetDirectoryName(path));
                return path;
            }
        }

        public void SaveUserProfile(UserProfile profile)
        {
            var profiles = ReadProfiles();
            var existingProfile = profiles.FirstOrDefault(p => p.Email == profile.Email);

            if (existingProfile != null)
            {
                profiles.Remove(existingProfile);
            }

            profiles.Add(profile);
            SaveProfiles(profiles);
        }

        public UserProfile GetUserProfile(string email)
        {
            return ReadProfiles().FirstOrDefault(p => p.Email.ToLower() == email.ToLower());
        }

        public void UpdateUserPreferences(string email, string theme)
        {
            var profiles = ReadProfiles();
            var profile = profiles.FirstOrDefault(p => p.Email.ToLower() == email.ToLower());

            if (profile != null)
            {
                profile.PreferredTheme = theme;
                SaveProfiles(profiles);
            }
        }

        private List<UserProfile> ReadProfiles()
        {
            List<UserProfile> profiles = new List<UserProfile>();
            if (File.Exists(FilePath))
            {
                var lines = File.ReadAllLines(FilePath);
                foreach (var line in lines)
                {
                    var parts = line.Split(',');
                    if (parts.Length == 4)
                    {
                        profiles.Add(new UserProfile
                        {
                            Email = parts[0],
                            LastLoginDate = DateTime.Parse(parts[1]),
                            PreferredTheme = parts[2],
                            RememberMe = bool.Parse(parts[3])
                        });
                    }
                }
            }
            return profiles;
        }

        private void SaveProfiles(List<UserProfile> profiles)
        {
            var lines = profiles.Select(p =>
                $"{p.Email},{p.LastLoginDate},{p.PreferredTheme},{p.RememberMe}");
            File.WriteAllLines(FilePath, lines);
        }
    }
}