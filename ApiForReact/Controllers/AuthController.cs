using ApiForReact.Models;
using ApiForReact.Models.Results;
using ApiForReact.Repositories.Intarfaces;
using ApiForReact.Services.Intarfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ApiForReact.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private IUserRepository _usersRepository;
        private IAuthService _authService;
        public AuthController( IUserRepository usersRepository, IAuthService authService)
        {
            _usersRepository = usersRepository;
            _authService = authService;
        }

        [HttpGet("status")]
        public IActionResult GetAuth() 
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;

                BaseResult<LoginResult> resultOk = new BaseResult<LoginResult>
                {
                    Message = "You are authorized",
                    Result = new LoginResult
                    {
                        Email = claimsIdentity.FindFirst(ClaimTypes.Email).Value,
                        Name = claimsIdentity.FindFirst(ClaimTypes.Name).Value,
                        UserId = Guid.Parse(claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value)
                    },
                    ResultCode = 0
                };
                return Ok(resultOk);
            }
            BaseResult<LoginResult> result = new BaseResult<LoginResult>
            {
                Message = "You are not authorized!",
                Result = null,
                ResultCode = 1
            };
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginModel loginModel)
        {
            var loginResult = await _authService.Login(loginModel.Login, loginModel.Password);

            if (loginResult.Result == null)
                return BadRequest(loginResult);

            var claimsIdentity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, loginResult.Result.Name),
                new Claim(ClaimTypes.Email, loginResult.Result.Email),
                new Claim(ClaimTypes.Role, "user"),
                new Claim(ClaimTypes.NameIdentifier, loginResult.Result.Id.ToString()),
            }, "Cookies");

            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            await Request.HttpContext.SignInAsync("Cookies", claimsPrincipal);

            return Ok(loginResult);
        }

        [Authorize]
        [HttpDelete("logout")] 
        public async Task<IActionResult> Logout()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var userId = Guid.Parse(claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value);
            BaseResult<string> result = new BaseResult<string>();

            if ((await _usersRepository.GetUser(userId)).ResultCode == 0)
            {
                await Request.HttpContext.SignOutAsync();
                result.Message = "Success";
                result.Result = "Ok";
                result.ResultCode = 0;
                return Ok(result);
            }

            result.Message = "Bad result";
            result.Result = "Something is happened";
            result.ResultCode = -1;

            return Ok(result);
        }
    }

    public class LoginModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
