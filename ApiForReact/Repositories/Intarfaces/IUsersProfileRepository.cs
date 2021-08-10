using ApiForReact.Models;
using System;

namespace ApiForReact.Repositories.Intarfaces
{
    public interface IUsersProfileRepository
    {
        public UserProfile GetUserProfile(Guid userId);
    }
}

