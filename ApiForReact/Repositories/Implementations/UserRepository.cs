using ApiForReact.Data;
using ApiForReact.Helpers;
using ApiForReact.Models;
using ApiForReact.Repositories.Intarfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ApiForReact.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private AppDbContext _appDbContext;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserRepository(AppDbContext appDbContext, IWebHostEnvironment hostingEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _appDbContext = appDbContext;
            _hostingEnvironment = hostingEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<BaseResult<string>> FollowUser(Guid srcUserId, Guid destUserId)
        {
            var srcUser = await _appDbContext.Users.FirstOrDefaultAsync(user => user.Id == srcUserId);
            var destUser = await _appDbContext.Users.FirstOrDefaultAsync(user => user.Id == destUserId);

            if (srcUser == null || destUser == null && srcUserId != destUserId)
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

            if (srcUser == null || destUser == null && srcUserId != destUserId)
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

        public async Task<User> GetUserByLoginPassword(string login, string password)
        {
            var userDto = await _appDbContext.Users.AsNoTracking()
                                                   .Include(user => user.Location)
                                                   .Include(user => user.UserContacts)
                                                   .FirstOrDefaultAsync(user => user.Name == login && user.Password == password);
            if (userDto != null)
                return User.Mapper.Map(userDto);
            return null;
        }
        public async Task<BaseResult<User>> GetUser(Guid userId)
        {
            var userDto = await _appDbContext.Users.Include(item => item.Location)
                                                .Include(item => item.UserContacts)
                                                .AsNoTracking()
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
                    where users1.Id != userId
                    select new { users1.Id, users1.Location.City, 
                                 users1.Location.Country, 
                                 users1.Name, users1.PhotoUrl, 
                                 users1.Status, 
                                 usersusers3.SubscriberUserId }).AsNoTracking()
                                                                .Skip((page - 1) * count)
                                                                .Take(count);

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
            }).AsNoTracking().ToListAsync();

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

            result.Message = "user doesn't exist";
            result.Result = "";
            result.ResultCode = -1;

            return result;
        }

        public async Task<BaseResult<string>> SavePhoto(IFormFile file, Guid userId)
        {
            var userDto = await _appDbContext.Users.FirstOrDefaultAsync(user => user.Id == userId);
            BaseResult<string> result = new BaseResult<string>();

            if (userDto != null)
            {
                var wwwroot = _hostingEnvironment.WebRootPath;

                var url = _httpContextAccessor.HttpContext.Request.Scheme + "://" + _httpContextAccessor.HttpContext.Request.Host;

                userDto.PhotoUrl = await ImageHelper.SaveImageAsync(file, userId, wwwroot, url);
                await _appDbContext.SaveChangesAsync();

                result.Message = "Success";
                result.Result = userDto.PhotoUrl;
                result.ResultCode = 0;

                return result;
            }

            result.Message = "uses doesn't exist";
            result.Result = "";
            result.ResultCode = -1;

            return result;
        }

        public async Task<BaseResult<User>> UpdateUser(Guid userId, User user)
        {
            var result = new BaseResult<User>();
            if(userId != user.Id)
            {
                result.Message = "you can't update another user";
                result.Result = null;
                result.ResultCode = -1;
                return result;
            }
            var validationError = Validator.ValidateUserContacts(user.UserContacts);
            if(!string.IsNullOrWhiteSpace(validationError))
            {
                result.Message = validationError;
                result.Result = null;
                result.ResultCode = -1;
                return result;
            }
            var userDto = await _appDbContext.Users.Include(item => item.Location)
                                                   .Include(item => item.UserContacts)
                                                   .FirstOrDefaultAsync(user => user.Id == userId);
            if(userDto == null)
            {
                result.Message = "user isn't found";
                result.Result = null;
                result.ResultCode = -1;
                return result;
            }
            Data.Dto.User.Map(user, userDto);
            await _appDbContext.SaveChangesAsync();

            var newUser = User.Mapper.Map(userDto);
            result.Message = "Success";
            result.Result = newUser;
            result.ResultCode = 0;
            return result;
        }
    }
}
