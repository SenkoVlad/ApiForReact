using ApiForReact.Models;
using ApiForReact.Models.Results;
using ApiForReact.Repositories.Intarfaces;
using ApiForReact.Services.Intarfaces;
using System;
using System.Threading.Tasks;

namespace ApiForReact.Services.Implementations
{
    public class MessageService : IMessageService
    {
        private IMessageRepository _messageRepository;
        public MessageService(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }
        public Task<BaseResult<string>> DeleteMessage(Guid userId, Guid messageId)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResult<MessagesResult>> GetMessages(Guid dialogId, int page, int count)
        {
            var result = await _messageRepository.GetMessages(dialogId, page, count);

            return new BaseResult<MessagesResult>
            {
                Message = "Success",
                Result = result,
                ResultCode = 0
            };
        }

        public async Task<BaseResult<string>> SendMessage(string message, Guid dialogId, Guid userOwnerId)
        {
            var result = await _messageRepository.SendMessage(message, dialogId, userOwnerId);

            if(result != "dialog doesn't exist")
            {
                return new BaseResult<string>
                {
                    Message = "success",
                    Result = result,
                    ResultCode = 0
                };
            }
            return new BaseResult<string>
            {
                Message = result,
                Result = null,
                ResultCode = -1
            };
        }
    }
}
