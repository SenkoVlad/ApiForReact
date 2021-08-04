using ApiForReact.Models;
using ApiForReact.Services.Intarfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiForReact.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        IUsersService _usersService;
        public UsersController(IUsersService usersService)
        {
            this._usersService = usersService;
        }

        [HttpGet]
        public IActionResult GetUsers(int page, int count)
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
    }
}
