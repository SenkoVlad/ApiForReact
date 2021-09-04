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
                                                     .Where(dialog => dialog.UserOwner.Id == userId || dialog.UserCompanion.Id == userId)
                                                     .Join(_appDbContext.Users,
                                                            dialog => dialog.UserCompanion.Id == userId ? dialog.UserOwner.Id : dialog.UserCompanion.Id,
                                                            user => user.Id,
                                                            (dialog, user) => new Dialog
                                                            {
                                                                Id = dialog.Id,
                                                                Name = user.Name,
                                                                UserCompanionId = dialog.UserCompanion.Id,
                                                                UserOwnerId = dialog.UserOwner.Id
                                                            })
                                                     .Skip((page - 1) * count)
                                                     .Take(count)
                                                     .ToListAsync();


            //var dialogs1 = await _appDbContext.Dialogs.AsNoTracking()
            //                                         .SelectMany(dialogs => _appDbContext.Users.Where(user => user.Id == dialogs.UserCompanion.Id ||
            //                                                                                                  user.Id == dialogs.UserOwner.Id)
            //                                                                                   .Select(item => new
            //                                                                                   {
            //                                                                                       Id = dialogs.Id,
            //                                                                                   }))
            //                                         .Skip((page - 1) * count)
            //                                         .Take(count)
            //                                         .ToListAsync();

            var totalCount = await _appDbContext.Dialogs.AsNoTracking()
                                                        .Where(dialog => dialog.UserOwner.Id == userId || dialog.UserCompanion.Id == userId)
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
                                                    .FirstOrDefaultAsync(dialog => dialog.UserOwner.Id == userOwnerId && dialog.UserCompanion.Id == userCompanionId);
            var userOwner = await _appDbContext.Users.FirstOrDefaultAsync(user => user.Id == userOwnerId);
            var userCompanion = await _appDbContext.Users.FirstOrDefaultAsync(user => user.Id == userCompanionId);

            if (dialog != null)
                return 0;

            if (userOwner == null || userCompanion == null)
                return 1;

            await _appDbContext.Dialogs.AddAsync(new Data.Dto.Dialog
            {
                Id = Guid.NewGuid(),
                UserCompanion = userCompanion,
                UserOwner = userOwner
            });
            await _appDbContext.SaveChangesAsync();
            return 0;
        } 
    }
}
