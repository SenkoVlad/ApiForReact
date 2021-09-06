using ApiForReact.Models;
using ApiForReact.Models.Results;
using System;
using System.Threading.Tasks;

namespace ApiForReact.Repositories.Intarfaces
{
    public interface IMessageRepository
    {
        public Task<MessagesResult> GetMessages(Guid dialogId, Guid userId, int page, int count);
        public Task<Message> SendMessage(string text, Guid dialogId, Guid userOwnerId);
        public Task<int> DeleteMessage(Guid userId, Guid messageId);
    }
}
