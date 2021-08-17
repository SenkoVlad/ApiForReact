using ApiForReact.Models;
using ApiForReact.Repositories.Intarfaces;
using Microsoft.AspNetCore.Authorization;
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
        IUsersRepository _usersRepository;
        public UsersController(IUsersRepository usersRepository) =>
            _usersRepository = usersRepository;

        [HttpGet]
        public async Task<IActionResult> GetUsers(int page = 1, int count = 10)
        {
            var userId = Guid.Empty;

            if (HttpContext.User.Identity.IsAuthenticated)
                userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            BaseResult<UsersResult> result = new BaseResult<UsersResult>
            {
                Result =  await _usersRepository.GetUsers(page, count, userId),
                Message = "Success",
                ResultCode = 0
            };
            return Ok(result);
        }
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUser(Guid userId)
        {
            var result = await _usersRepository.GetUser(userId);
            return Ok(result);
        }

        [Authorize]
        [HttpPost("follow/{userId}")]
        public async Task<IActionResult> FollowUser(Guid userId)
        {
            var sourceUserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var result = await _usersRepository.FollowUser(sourceUserId, destUserId: userId);

            return Ok(result);
        }


        [Authorize]
        [HttpPost("unfollow/{userId}")]
        public async Task<IActionResult> UnFollowUser(Guid userId)
        {
            var sourceUserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var result = await _usersRepository.UnFollowUser(sourceUserId, destUserId: userId);

            return Ok(result);
        }
    }
}
