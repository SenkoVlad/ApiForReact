namespace ApiForReact.Repositories.Intarfaces
{
    public interface IAuthRepository
    {
        public bool Login(string login, string password);
    }
}
