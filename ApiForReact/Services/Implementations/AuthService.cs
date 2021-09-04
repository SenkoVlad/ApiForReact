using ApiForReact.Models;
using ApiForReact.Models.Results;
using ApiForReact.Repositories.Intarfaces;
using ApiForReact.Services.Intarfaces;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ApiForReact.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private IUserRepository _userRepository;
        public AuthService(IUserRepository userRepository) =>
            _userRepository = userRepository;

        public async Task<BaseResult<User>> Login(string login, string password)
        {
            var user = await _userRepository.GetUserByLoginPassword(login, password);

            if (user == null)
            {
                return new BaseResult<User>
                {
                    Message = "Your login data is incorrect",
                    Result = null,
                    ResultCode = 1
                };
            }

            return new BaseResult<User>
            {
                Message = "You are authorized",
                Result = user,
                ResultCode = 0
            };
        }
    }
}
