using System;

namespace ApiForReact.Models.Results
{
    public class LoginResult
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
    }
}
