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

        public async Task<BaseResult<string>> FollowUser(Guid srcUserId, Guid destUserId)
        {
            var srcUser = await _appDbContext.Users.FirstOrDefaultAsync(user => user.Id == srcUserId);
            var destUser = await _appDbContext.Users.FirstOrDefaultAsync(user => user.Id == destUserId);

            if (srcUser == null || destUser == null)
                return new BaseResult<string>
                {
                    Message = "User doesn't exists",
                    Result = "Error",
                    ResultCode = -1
                };
            
            var result = _appDbContext.UsersUsers.Add(new Data.Dto.UserUser
            {
                Id = Guid.NewGuid(),
                SubscriberUserId = srcUserId,
                SubscriptionUserId = destUserId 
            });
            var saveResult = await _appDbContext.SaveChangesAsync();

            if (saveResult == 1)
                return new BaseResult<string>
                {
                    Message = "Ok",
                    Result = "Ok",
                    ResultCode = 0
                };

            return new BaseResult<string>
            {
                Message = "Smth wrong",
                Result = "Smth wrong",
                ResultCode = -1
            };
        }

        public async Task<BaseResult<string>> UnFollowUser(Guid srcUserId, Guid destUserId)
        {
            var srcUser = await _appDbContext.Users.FirstOrDefaultAsync(user => user.Id == srcUserId);
            var destUser = await _appDbContext.Users.FirstOrDefaultAsync(user => user.Id == destUserId);

            if (srcUser == null || destUser == null)
                return new BaseResult<string>
                {
                    Message = "User doesn't exists",
                    Result = "Error",
                    ResultCode = -1
                };

            var usersUsersRow = await _appDbContext.UsersUsers.FirstOrDefaultAsync(item => item.SubscriberUserId == srcUserId && item.SubscriptionUserId == destUserId);

            if(usersUsersRow == null)
                return new BaseResult<string>
                {
                    Message = "Smth wrong",
                    Result = "Smth wrong",
                    ResultCode = -1
                };

            var result = _appDbContext.UsersUsers.Remove(usersUsersRow);
            var saveResult = await _appDbContext.SaveChangesAsync();

            if (saveResult == 1)
                return new BaseResult<string>
                {
                    Message = "Ok",
                    Result = "Ok",
                    ResultCode = 0
                };

            return new BaseResult<string>
            {
                Message = "Smth wrong",
                Result = "Smth wrong",
                ResultCode = -1
            };
        }

        public async Task<UsersResult> GetUsers(int page, int count, Guid userId)
        {
            var user3 = (from user1 in _appDbContext.Users
                        join user2 in _appDbContext.UsersUsers on user1.Id equals user2.SubscriptionUserId 
                        into User2
                        from u in User2.DefaultIfEmpty()
                        select  new User
                        {
                            Id = user1.Id,
                            Name = user1.Name,
                            PhotoUrl = user1.PhotoUrl,
                            Status = user1.Status,
                            Followed = u.SubscriberUserId == userId ? 1 : 0,
                            Location = new Location
                            {
                                City = user1.Location.City,
                                Country = user1.Location.Country
                            }
                        }).Skip((page - 1) * count).Take(count);

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
 