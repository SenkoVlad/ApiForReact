using ApiForReact.Models;
using ApiForReact.Models.Results;
using ApiForReact.Repositories.Intarfaces;
using ApiForReact.Services.Intarfaces;
using System;
using System.Threading.Tasks;

namespace ApiForReact.Services.Implementations
{
    public class DialogService : IDialogService
    {
        private IDialogRepository _dialogRepository;
        public DialogService(IDialogRepository dialogRepository)
        {
            _dialogRepository = dialogRepository;
        }

        public async Task<BaseResult<DialogsResult>> GetDialogs(Guid userId, int page, int count)
        {
            var result = await _dialogRepository.GetUserDialogs(userId, page, count);

            return new BaseResult<DialogsResult>
            {
                Message = "Success",
                Result = result,
                ResultCode = 0
            };
        }

        public async Task<BaseResult<string>> StartDialog(Guid userOwnerId, Guid userCompanionId)
        {
            var result = await _dialogRepository.StartDialog(userOwnerId, userCompanionId);

            if(result == 1)
            {
                return new BaseResult<string>
                {
                    Message = "user isn't found",
                    Result = null,
                    ResultCode = 1
                };
            }

            return new BaseResult<string>
            {
                Message = "success",
                Result = userCompanionId.ToString(),
                ResultCode = 0
            };
        }
    }
}
