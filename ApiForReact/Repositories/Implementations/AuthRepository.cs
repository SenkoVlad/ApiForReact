using ApiForReact.Repositories.Intarfaces;

namespace ApiForReact.Repositories.Implementations
{
    public class AuthRepository : IAuthRepository
    {
        public bool Login(string login, string password)
        {
            if (login == "vlad" && password == "1234")
                return true;
            return false;
        }
    }
}
