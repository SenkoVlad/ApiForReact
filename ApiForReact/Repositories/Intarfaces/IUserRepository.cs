using ApiForReact.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace ApiForReact.Repositories.Intarfaces
{
    public interface IUserRepository
    {
        public Task<BaseResult<UsersResult>> GetUsers(int page, int count, Guid userId);
        public Task<BaseResult<string>> FollowUser(Guid srcUserId, Guid destUserId);
        public Task<BaseResult<string>> UnFollowUser(Guid srcUserId, Guid destUserId);
        public Task<BaseResult<User>> GetUser(Guid userId);
        public Task<BaseResult<string>> UpdateUserStatus(string status, Guid userId);
        public Task<BaseResult<string>> SavePhoto(IFormFile file, Guid userId);
        public Task<BaseResult<User>> UpdateUser(Guid userId, User user);
        public Task<User> GetUserByLoginPassword(string login, string password);
    }
}
