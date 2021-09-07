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

        public async Task<BaseResult<MessagesResult>> GetMessages(Guid dialogId, Guid userId, int page, int count)
        {
            var result = await _messageRepository.GetMessages(dialogId, userId, page, count);

            return new BaseResult<MessagesResult>
            {
                Message = "Success",
                Result = result,
                ResultCode = 0
            };
        }

        public async Task<BaseResult<Message>> SendMessage(string messageText, Guid dialogId, Guid userOwnerId)
        {
            if(string.IsNullOrWhiteSpace(messageText))
            {
                return new BaseResult<Message>
                {
                    Message = "message is empty",
                    Result = null,
                    ResultCode = -1
                };
            }
            
            var message = await _messageRepository.SendMessage(messageText, dialogId, userOwnerId);

            if(message != null)
            {
                return new BaseResult<Message>
                {
                    Message = "success",
                    Result = message,
                    ResultCode = 0
                };
            }
            return new BaseResult<Message>
            {
                Message = "bad request",
                Result = message,
                ResultCode = -1
            };
        }
    }
}
