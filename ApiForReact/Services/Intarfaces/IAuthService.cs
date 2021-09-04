using ApiForReact.Models;
using ApiForReact.Models.Results;
using System.Threading.Tasks;

namespace ApiForReact.Services.Intarfaces
{
    public interface IAuthService
    {
        public Task<BaseResult<User>> Login(string login, string password);
    }
}
