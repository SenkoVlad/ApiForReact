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

            if (usersUsersRow == null)
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

        public async Task<BaseResult<User>> GetUser(Guid userId)
        {
            var userDto = await _appDbContext.Users.Include(item => item.Location)
                                                .Include(item => item.UserContacts)
                                                .FirstOrDefaultAsync(user => user.Id == userId);
            BaseResult<User> result = new BaseResult<User>();

            if (userDto != null)
            {
                var user = User.Mapper.Map(userDto);
                result.Result = user;
                result.Message = "Success";
                result.ResultCode = 0;
            }
            else
            {
                result.Result = null;
                result.Message = "Success";
                result.ResultCode = -1;
            }
            return result;
        }
        
        public async Task<BaseResult<UsersResult>> GetUsers(int page, int count, Guid userId)
        {
            var result = (from users1 in _appDbContext.Users
                    join  usersusers1 in
                    (
                    from usersusers2 in _appDbContext.UsersUsers
                    where usersusers2.SubscriberUserId == userId
                    select new { usersusers2.SubscriberUserId, usersusers2.SubscriptionUserId }
                    ) on users1.Id equals usersusers1.SubscriptionUserId into u_left
                    from usersusers3 in u_left.DefaultIfEmpty()
                    select new { users1.Id, users1.Location.City, 
                                 users1.Location.Country, 
                                 users1.Name, users1.PhotoUrl, 
                                 users1.Status, 
                                 usersusers3.SubscriberUserId }).Skip((page - 1) * count).Take(count);

            var users = await result.Select(user => new User
            {
                Followed = user.SubscriberUserId == userId ? 1 : 0,
                Id = user.Id,
                Location = new Location 
                {
                    City = user.City,
                    Country = user.Country
                },
                Name = user.Name,
                PhotoUrl = user.PhotoUrl,
                Status = user.Status
            }).ToListAsync();

            var totalCount = await _appDbContext.Users.CountAsync();

            BaseResult<UsersResult> baseResult = new BaseResult<UsersResult>
            {
                Result = new UsersResult
                {
                    Items = users,
                    TotalCount = totalCount
                },
                Message = "Success",
                ResultCode = 0
            };
            return baseResult;
        }

        public async Task<BaseResult<string>> UpdateUserStatus(string status, Guid userId)
        {
            var userDto = await _appDbContext.Users.FirstOrDefaultAsync(user => user.Id == userId);
            BaseResult<string> result = new BaseResult<string>();
            
            if (userDto != null)
            {
                userDto.Status = status;
                await _appDbContext.SaveChangesAsync();

                result.Message = "Success";
                result.Result = "";
                result.ResultCode = 0;

                return result;
            }

            result.Message = "user isn't exist";
            result.Result = "";
            result.ResultCode = -1;

            return result;
        }
    }
}
