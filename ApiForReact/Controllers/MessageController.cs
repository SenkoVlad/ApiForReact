using ApiForReact.Models;
using ApiForReact.Services.Intarfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ApiForReact.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class MessageController : ControllerBase
    {
        private IMessageService _messageService;
        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }
        
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetMessage(Guid dialogId, int page = 1, int count = 10)
        {
            var result = await _messageService.GetMessages(dialogId, page, count);
            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] string message, Guid dialogId)
        {
            var result = await _messageService.SendMessage(message, dialogId);
            return Ok(result);
        }
    }
}
