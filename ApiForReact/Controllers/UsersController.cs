using ApiForReact.Models;
using ApiForReact.Services.Intarfaces;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ApiForReact.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        IUsersService _usersService;
        IUsersProfileService _usersProfileService;
        public UsersController(IUsersService usersService, IUsersProfileService usersProfileService)
        {
            _usersService = usersService;
            _usersProfileService = usersProfileService;
        }

        [HttpGet]
        public IActionResult GetUsers(int page = 1, int count = 10)
        {
            BaseResult<UsersResult> result = new BaseResult<UsersResult>
            {
                Result = new UsersResult
                {
                    Items = _usersService.GetUsers(page, count),
                    TotalCount = _usersService.GetTotalCount()
                },
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
