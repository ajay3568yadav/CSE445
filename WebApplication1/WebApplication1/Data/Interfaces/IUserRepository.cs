using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.Data.Interfaces
{
    public interface IUserRepository
    {
        bool CreateUser(User user);
        User GetUserByEmail(string email);
        bool ValidateUser(string email, string password);
    }
}