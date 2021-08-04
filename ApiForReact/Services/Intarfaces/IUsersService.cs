using ApiForReact.Models;
using System.Collections.Generic;

namespace ApiForReact.Services.Intarfaces
{
    public interface IUsersService
    {
        public IEnumerable<User> GetUsers(int? page = 1, int? count = 10);
        public int GetTotalCount();
    }
}
