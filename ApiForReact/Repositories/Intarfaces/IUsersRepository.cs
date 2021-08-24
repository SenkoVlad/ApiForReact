using ApiForReact.Models;
using System;
using System.Threading.Tasks;

namespace ApiForReact.Repositories.Intarfaces
{
    public interface IUsersRepository
    {
        public Task<BaseResult<UsersResult>> GetUsers(int page, int count, Guid userId);
        public Task<BaseResult<string>> FollowUser(Guid srcUserId, Guid destUserId);
        public Task<BaseResult<string>> UnFollowUser(Guid srcUserId, Guid destUserId);
        public Task<BaseResult<User>> GetUser(Guid userId);
        public Task<BaseResult<string>> UpdateUserStatus(string status, Guid userId);

    }
}
