using ApiForReact.Data;
using ApiForReact.Models;
using ApiForReact.Repositories.Intarfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ApiForReact.Repositories.Implementations
{
    public class UsersRepository : IUsersRepository
    {
        private AppDbContext _appDbContext;
        public UsersRepository(AppDbContext appDbContext) =>
            _appDbContext = appDbContext;

        public async Task<UsersResult> GetUsers(int page, int count, Guid userId)
        {
            var user3 = from user1 in _appDbContext.Users
                        join user2 in _appDbContext.UsersUsers on user1.Id equals user2.SubscriptionUser.Id 
                        into User2
                        from u in User2.DefaultIfEmpty()
                        select  new User
                        {
                            Id = user1.Id,
                            Name = user1.Name,
                            PhotoUrl = user1.PhotoUrl,
                            Status = user1.Status,
                            Followed =  u.SubscriberUser != null && u.SubscriberUser.Id == userId ? 1 : 0,
                            Location = new Location
                            {
                                City = user1.Location.City,
                                Country = user1.Location.Country
                            }
                        };

            var users = await user3.ToListAsync();

            var totalCount = await _appDbContext.Users.CountAsync();

            return new UsersResult
            {
                Items = users,
                TotalCount = totalCount
            };
        }
    }
}
 