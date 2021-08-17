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
                ResultCode = - 1
            };
        }

        public async Task<UsersResult> GetUsers(int page, int count, Guid userId)
        {
            var result = (from users1 in _appDbContext.Users
                    join  usersusers1 in
                    (
                    from usersusers2 in _appDbContext.UsersUsers
                    where usersusers2.SubscriberUserId == userId
                    select new { usersusers2.SubscriberUserId, usersusers2.SubscriptionUserId }
                    ) on users1.Id equals usersusers1.SubscriptionUserId into u_left
                    from usersusers3 in u_left.DefaultIfEmpty()
                    select new { users1, usersusers3 }).Skip((page - 1) * count).Take(count);

            var users = await result.Select(user => new User 
            {
                Followed = user.usersusers3.SubscriberUserId == userId ? 1 : 0,
                Id = user.users1.Id,
                Location = new Location 
                {
                    City = user.users1.Location.City,
                    Country = user.users1.Location.Country
                },
                Name = user.users1.Name,
                PhotoUrl = user.users1.PhotoUrl,
                Status = user.users1.Status
            }).ToListAsync();

            var totalCount = await _appDbContext.Users.CountAsync();

            return new UsersResult
            {
                Items = users,
                TotalCount = totalCount
            };
        }

        public async Task<BaseResult<User>> GetUser(Guid id)
        {
            var userDto = await _appDbContext.Users.Include(user => user.Contacts)
                                                   .Include(user => user.Location)
                                                   .FirstOrDefaultAsync(user => user.Id == id);
            BaseResult<User> user = new BaseResult<User>();
            if(userDto != null)
            {
                user.Message = "Success";
                user.Result = User.Mapper.Map(userDto);
                user.ResultCode = 0;
            }
            else
            {
                user.Message = "User isn't found";
                user.Result = null;
                user.ResultCode = -1;
            }
            return user;
        }
    }
}
 