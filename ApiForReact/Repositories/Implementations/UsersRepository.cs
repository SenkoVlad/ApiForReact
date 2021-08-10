using ApiForReact.Data;
using ApiForReact.Models;
using ApiForReact.Repositories.Intarfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using static ApiForReact.Models.User;

namespace ApiForReact.Repositories.Implementations
{
    public class UsersRepository : IUsersRepository
    {
        private AppDbContext _appDbContext;
        public UsersRepository(AppDbContext appDbContext) =>
            _appDbContext = appDbContext;

        public async Task<UsersResult> GetUsers(int page, int count, Guid userId)
        {
            var usersDto = await _appDbContext.Users.OrderBy(item => item.Id)
                                                       .Include(item => item.Location)
                                                       .Skip((page - 1) * count)
                                                       .Take(count)
                                                       .ToListAsync();
            var users = usersDto.Select(Mapper.Map);

            var totalCount = await _appDbContext.Users.CountAsync();

            return new UsersResult
            {
                Items = users,
                TotalCount = totalCount
            };
        }
    }
}
 