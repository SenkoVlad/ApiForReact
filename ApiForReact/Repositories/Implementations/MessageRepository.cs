using ApiForReact.Data;
using ApiForReact.Models;
using ApiForReact.Models.Results;
using ApiForReact.Repositories.Intarfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ApiForReact.Repositories.Implementations
{
    public class MessageRepository : IMessageRepository
    {
        private AppDbContext _appDbContext;
        public MessageRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public Task<int> DeleteMessage(Guid userId, Guid messageId)
        {
            throw new NotImplementedException();
        }

        public async Task<MessagesResult> GetMessages(Guid dialogId, int page, int count)
        {
            var messages = await _appDbContext.Dialogs.AsNoTracking()
                                                      .Where(dialog => dialog.Id == dialogId)
                                                      .Select(dialog => dialog.Messages.Skip((page - 1) * count).Take(count))
                                                      .FirstOrDefaultAsync();
            
            var totalCount = await _appDbContext.Dialogs.AsNoTracking()
                                                        .Where(dialog => dialog.Id == dialogId)
                                                        .Select(dialog => dialog.Messages.Count)
                                                        .FirstOrDefaultAsync();
            return new MessagesResult
            {
                Items = messages.Select(Message.Map),
                totalCount = totalCount
            };
        }

        public async Task<string> SendMessage(string text, Guid dialogId)
        {
            var dialog = await _appDbContext.Dialogs.Select(item => new {id = item.Id, 
                                                                         userCompanionId = item.UserCompanionId,
                                                                         userOwnerId = item.UserOwnerId})
                                                    .FirstOrDefaultAsync(dialog => dialog.id == dialogId);

            if (dialog == null || dialog.userCompanionId == Guid.Empty || dialog.userOwnerId == Guid.Empty)
                return "dialog doesn't exist";

            Guid id = Guid.NewGuid();
            var newMessage = new Data.Dto.Message
            {
                Id = id,
                Date = DateTime.Now,
                Status = 0,
                Text = text,
                UserCompanionId = dialog.userCompanionId,
                UserOwnerId = dialog.userOwnerId,
                DialogId = dialog.id
            };
            
            await _appDbContext.Messages.AddAsync(newMessage);
            await _appDbContext.SaveChangesAsync();

            return id.ToString();
        }
    }
}
