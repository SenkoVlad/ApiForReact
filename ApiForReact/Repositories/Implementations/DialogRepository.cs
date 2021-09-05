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
    public class DialogRepository : IDialogRepository
    {
        private AppDbContext _appDbContext;
        public DialogRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<DialogsResult> GetUserDialogs(Guid userId, int page, int count)
        {
            var dialogs = await _appDbContext.Dialogs.AsNoTracking()
                                                     .Where(dialog => dialog.UserOwnerId == userId || dialog.UserCompanionId == userId)
                                                     .Join(_appDbContext.Users,
                                                            dialog => dialog.UserCompanionId == userId ? dialog.UserOwnerId : dialog.UserCompanionId,
                                                            user => user.Id,
                                                            (dialog, user) => new Dialog
                                                            {
                                                                Id = dialog.Id,
                                                                Name = user.Name,
                                                                UserCompanionId = dialog.UserCompanionId,
                                                                UserOwnerId = dialog.UserOwnerId
                                                            })
                                                     .Skip((page - 1) * count)
                                                     .Take(count)
                                                     .ToListAsync();

            var totalCount = await _appDbContext.Dialogs.AsNoTracking()
                                                        .Where(dialog => dialog.UserOwnerId == userId || dialog.UserCompanionId == userId)
                                                        .CountAsync();
            return new DialogsResult
            {
                Items = dialogs,
                TotalCount = totalCount
            };
        }
        public async Task<int> StartDialog(Guid userOwnerId, Guid userCompanionId)
        {
            var dialog = await _appDbContext.Dialogs.AsNoTracking()
                                                    .Select(dialog => new {id = dialog.Id, 
                                                                           userOwnerId = dialog.UserOwnerId,
                                                                           userCompanionId = dialog.UserCompanionId})
                                                    .FirstOrDefaultAsync(dialog => (dialog.userOwnerId == userOwnerId && dialog.userCompanionId == userCompanionId)
                                                                                   || 
                                                                                   (dialog.userCompanionId == userOwnerId && dialog.userOwnerId == userCompanionId));

            if (dialog != null)
                return 0;

            await _appDbContext.Dialogs.AddAsync(new Data.Dto.Dialog
            {
                Id = Guid.NewGuid(),
                UserCompanionId = userCompanionId,
                UserOwnerId = userOwnerId
            });
            await _appDbContext.SaveChangesAsync();
            return 0;
        } 
    }
}
