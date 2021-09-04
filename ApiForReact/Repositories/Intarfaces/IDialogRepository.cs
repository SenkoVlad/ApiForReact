using ApiForReact.Models;
using ApiForReact.Models.Results;
using System;
using System.Threading.Tasks;

namespace ApiForReact.Repositories.Intarfaces
{
    public interface IDialogRepository
    {
        public Task<DialogsResult> GetUserDialogs(Guid userId, int page, int count);
        public Task<int> StartDialog(Guid userOwnerId, Guid userCompanionId);

    }
}
