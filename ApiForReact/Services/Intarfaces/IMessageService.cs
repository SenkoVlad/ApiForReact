using ApiForReact.Models;
using ApiForReact.Models.Results;
using System;
using System.Threading.Tasks;

namespace ApiForReact.Services.Intarfaces
{
    public interface IMessageService
    {
        public Task<BaseResult<Message>> SendMessage(string message, Guid dialogId, Guid userOwnerId);
        public Task<BaseResult<MessagesResult>> GetMessages(Guid dialogId, Guid userId, int page, int count);
        public Task<BaseResult<string>> DeleteMessage(Guid userId, Guid messageId);
    }
}
