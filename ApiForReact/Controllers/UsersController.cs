using ApiForReact.Controllers.RequestModels;
using ApiForReact.Models;
using ApiForReact.Repositories.Intarfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        IUserRepository _usersRepository;
        public UsersController(IUserRepository usersRepository) =>
            _usersRepository = usersRepository;

        [HttpGet]
        public async Task<IActionResult> GetUsers(int page = 1, int count = 10)
        {
            var userId = Guid.Empty;

            if (HttpContext.User.Identity.IsAuthenticated)
                userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var result = await _usersRepository.GetUsers(page, count, userId);
            return Ok(result);
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] User user)
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var result = await _usersRepository.UpdateUser(userId, user);

            return Ok(result);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUser(Guid userId)
        {
            var user = await _usersRepository.GetUser(userId);
            return Ok(user);
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

        [Authorize]
        [HttpPut("updatestatus")]
        public async Task<IActionResult> UpdateUserStatus([FromBody] UpdateUserStatusRequest status)
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var result = await _usersRepository.UpdateUserStatus(status.status, userId);

            return Ok(result);
        }

        [Authorize]
        [HttpPut("savephoto")]
        public async Task<IActionResult> SavePhoto(IFormFile image)
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var result = await _usersRepository.SavePhoto(image, userId);

            return Ok(result);
        }
    }
}
