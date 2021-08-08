using ApiForReact.Services.Intarfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace ApiForReact.Services.Implementations
{
    public class AuthService : IAuthService
    {
        public bool Login(string login, string password)
        {
            if (login == "vlad" && password == "1234")
                return true;
            return false;
        }
    }
}
