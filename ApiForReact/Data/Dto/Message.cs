using System;

namespace ApiForReact.Data.Dto
{
    public class Message
    {
        public Guid Id { get; set; }
        public User UserOwner { get; set; }
        public User UserCompanion { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public int Status { get; set; }
    }
}
