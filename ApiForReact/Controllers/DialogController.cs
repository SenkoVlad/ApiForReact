using ApiForReact.Services.Intarfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ApiForReact.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DialogController : ControllerBase
    {
        private IDialogService _dialogService;
        public DialogController(IDialogService dialogService)
        {
            _dialogService = dialogService;
        }

        [Authorize]
        [HttpPost("start/{userId}")]
        public async Task<IActionResult> StartDialog(Guid userId)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var userOwnerId = Guid.Parse(claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value);

            var result = await _dialogService.StartDialog(userOwnerId, userId);

            return result.Result != null
                ? Ok(result)
                : NotFound(result);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetDialogs(int page = 1, int count = 5)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var userId = Guid.Parse(claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value);

            var result = await _dialogService.GetDialogs(userId, page, count);
            return Ok(result);
        }
    }
}
