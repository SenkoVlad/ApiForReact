using ApiForReact.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiForReact.Repositories.Intarfaces
{
    public interface IMessageRepository
    {
        public Task<int> SendMessage(Guid fromUserId, Guid toUserId);
        public Task<IEnumerator<Message>> GetMessages(Guid userId, int page, int count);
        public Task<int> DeleteMessage(Guid userId, Guid messageId);
    }
}
