using ApiForReact.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiForReact.Repositories.Intarfaces
{
    public interface IUsersRepository
    {
        public Task<UsersResult> GetUsers(int page, int count, Guid userId);
    }
}
