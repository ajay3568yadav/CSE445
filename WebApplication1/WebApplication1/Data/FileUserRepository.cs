using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using WebApplication1.Data.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class FileUserRepository : IUserRepository
    {
        private static string FilePath
        {
            get
            {
                string path = HttpContext.Current.Server.MapPath("~/App_Data/users.txt");
                Directory.CreateDirectory(Path.GetDirectoryName(path));
                return path;
            }
        }

        public bool CreateUser(User user)
        {
            var users = ReadUsers();

            if (users.Any(u => u.Email.ToLower() == user.Email.ToLower()))
            {
                return false;
            }

            users.Add(user);
            SaveUsers(users);
            return true;
        }

        public User GetUserByEmail(string email)
        {
            return ReadUsers().FirstOrDefault(u => u.Email.ToLower() == email.ToLower());
        }

        public bool ValidateUser(string email, string password)
        {
            var users = ReadUsers();
            return users.Any(u => u.Email.ToLower() == email.ToLower() &&
                                u.Password == password);
        }

        private List<User> ReadUsers()
        {
            List<User> users = new List<User>();
            if (File.Exists(FilePath))
            {
                var lines = File.ReadAllLines(FilePath);
                foreach (var line in lines)
                {
                    var parts = line.Split(',');
                    if (parts.Length == 2)
                    {
                        users.Add(new User { Email = parts[0], Password = parts[1] });
                    }
                }
            }
            return users;
        }

        private void SaveUsers(List<User> users)
        {
            var lines = users.Select(u => $"{u.Email},{u.Password}");
            File.WriteAllLines(FilePath, lines);
        }
    }
}