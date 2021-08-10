using ApiForReact.Models;
using ApiForReact.Repositories.Intarfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ApiForReact.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        IUsersRepository _usersService;
        IUsersProfileRepository _usersProfileService;
        public UsersController(IUsersRepository usersService, IUsersProfileRepository usersProfileService)
        {
            _usersService = usersService;
            _usersProfileService = usersProfileService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers(int page = 1, int count = 10)
        {
            var userId = Guid.Empty;

            if (HttpContext.User.Identity.IsAuthenticated)
                userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            BaseResult<UsersResult> result = new BaseResult<UsersResult>
            {
                Result =  await _usersService.GetUsers(page, count, userId),
                Message = "Success",
                ResultCode = 200
            };
            return Ok(result);
        }
        [HttpGet("profile/{userId}")]
        public IActionResult GetUserProfile(Guid userId)
        {
            BaseResult<UserProfile> result = new BaseResult<UserProfile>
            {
                Result = _usersProfileService.GetUserProfile(userId),
                Message = "Success",
                ResultCode = 200
            };
            return Ok(result);
        }

        //[HttpPost("follow/{userId}")]
        //public IActionResult FollowUser(Guid userId)
        //{

        //}
    }
}
