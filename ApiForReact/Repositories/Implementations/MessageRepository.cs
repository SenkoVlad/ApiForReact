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

        public async Task<MessagesResult> GetMessages(Guid dialogId, Guid userId, int page, int count)
        {
            //var messages = await _appDbContext.Dialogs.AsNoTracking()
            //                                          .Where(dialog => dialog.Id == dialogId && 
            //                                                          (dialog.CompanionUserId == userId || dialog.OwnerUserId == userId))
            //                                          .Select(dialog => dialog.Messages.Skip((page - 1) * count).Take(count))
            //                                          .FirstOrDefaultAsync();

            var messages = await _appDbContext.Messages.AsNoTracking()
                                                       .Where(message => message.DialogId == dialogId &&
                                                                         (message.UserIdCompanion == userId || message.UserIdOwner == userId))
                                                       .ToListAsync();

            var totalCount = await _appDbContext.Dialogs.AsNoTracking()
                                                        .Where(dialog => dialog.Id == dialogId &&
                                                              (dialog.CompanionUserId == userId || dialog.OwnerUserId == userId))
                                                        .Select(dialog => dialog.Messages.Count)
                                                        .FirstOrDefaultAsync();
            return new MessagesResult
            {
                Items = messages.Select(Message.Map),
                totalCount = totalCount,
                dialogId = dialogId
            };
        }

        public async Task<Message> SendMessage(string text, Guid dialogId, Guid userOwnerId)
        {
            var dialog = await _appDbContext.Dialogs.Select(item => new {id = item.Id, 
                                                                         userCompanionId = item.CompanionUserId,
                                                                         userOwnerId = item.OwnerUserId})
                                                    .FirstOrDefaultAsync(dialog => dialog.id == dialogId);

            if (dialog == null || (userOwnerId != dialog.userCompanionId && userOwnerId != dialog.userOwnerId))
                return null;

            var CompanionUserId = userOwnerId == dialog.userCompanionId ? dialog.userOwnerId : dialog.userCompanionId;

            Guid id = Guid.NewGuid();
            var newMessage = new Data.Dto.Message
            {
                Id = id,
                Date = DateTime.Now,
                Status = 0,
                Text = text,
                UserIdCompanion = CompanionUserId,
                UserIdOwner = userOwnerId,
                DialogId = dialog.id
            };
            
            await _appDbContext.Messages.AddAsync(newMessage);
            await _appDbContext.SaveChangesAsync();

            return Message.Map(newMessage);
        }
    }
}
