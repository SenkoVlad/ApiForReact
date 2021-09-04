using System;

namespace ApiForReact.Models
{
    public class Message
    {
        public Guid Id { get; set; }
        public User UserOwner { get; set; }
        public User UserCompanion { get; set; }
        public string Text { get; set; }
    }
}
