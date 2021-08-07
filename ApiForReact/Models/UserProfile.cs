using ApiForReact.Models;
using System;
using System.Collections.Generic;

namespace ApiForReact.Models
{
    public class UserProfile
    {
        public Guid UserId { get; set; }
        public string Info { get; set; }
        public UserContacts Contacts { get; set; }
        public bool IsLookingForAJob { get; set; }
        public string ResumeText { get; set; }
        public string FullName { get; set; }
        public string PhotoUrl { get; set; }
    }
}

