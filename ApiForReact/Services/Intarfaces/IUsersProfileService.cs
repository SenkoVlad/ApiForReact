using ApiForReact.Models;
using System;

namespace ApiForReact.Services.Intarfaces
{
    public interface IUsersProfileService
    {
        public UserProfile GetUserProfile(Guid userId);
    }
}

