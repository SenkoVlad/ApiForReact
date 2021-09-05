using ApiForReact.Models;
using ApiForReact.Models.Results;
using System;
using System.Threading.Tasks;

namespace ApiForReact.Repositories.Intarfaces
{
    public interface IMessageRepository
    {
        public Task<MessagesResult> GetMessages(Guid dialogId, int page, int count);
        public Task<string> SendMessage(string text, Guid dialogId);
        public Task<int> DeleteMessage(Guid userId, Guid messageId);
    }
}
