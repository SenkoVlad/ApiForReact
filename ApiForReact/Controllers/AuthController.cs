using ApiForReact.Models;
using ApiForReact.Models.Results;
using ApiForReact.Repositories.Intarfaces;
using Microsoft.AspNetCore.Authentication;
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
        private IAuthRepository _authService;
        public AuthController(IAuthRepository authService) =>
            _authService = authService;

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
                        Login = claimsIdentity.FindFirst(ClaimTypes.Name).Value,
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
            if (!_authService.Login(loginModel.Login, loginModel.Password))
            {
                BaseResult<LoginResult> result = new BaseResult<LoginResult>
                {
                    Message = "Your login data is incorrect",
                    Result = null,
                    ResultCode = 1
                };
                return Ok(result);
            }

            var claimsIdentity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, loginModel.Login),
                new Claim(ClaimTypes.Email, "vlad@senko.com"),
                new Claim(ClaimTypes.Role, "user"),
                new Claim(ClaimTypes.NameIdentifier, "19645127-3EEA-4444-9F0F-124BFE75E775"),
            }, "Cookies");
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            await Request.HttpContext.SignInAsync("Cookies", claimsPrincipal);

            BaseResult<LoginResult> resultOk = new BaseResult<LoginResult>
            {
                Message = "You are authorized",
                Result = new LoginResult
                {
                    Email = "vlad@senko.com",
                    Login = loginModel.Login,
                    UserId = Guid.Parse("19645127-3EEA-4444-9F0F-124BFE75E775")
                },
                ResultCode = 0
            };
            return Ok(resultOk);
        }
    }

    public class LoginModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
