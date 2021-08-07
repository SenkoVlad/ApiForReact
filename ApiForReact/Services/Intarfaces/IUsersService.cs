using ApiForReact.Models;
using System.Collections.Generic;

namespace ApiForReact.Services.Intarfaces
{
    public interface IUsersService
    {
        public IEnumerable<User> GetUsers(int page, int count);
        public int GetTotalCount();
    }
}
