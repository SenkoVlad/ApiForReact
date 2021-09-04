using ApiForReact.Models;
using ApiForReact.Models.Results;
using System;
using System.Threading.Tasks;

namespace ApiForReact.Services.Intarfaces
{
    public interface IDialogService
    {
        public Task<BaseResult<string>> StartDialog(Guid userOwnerId, Guid userCompanionId);
        public Task<BaseResult<DialogsResult>> GetDialogs(Guid userId, int page, int count);
    }
}
