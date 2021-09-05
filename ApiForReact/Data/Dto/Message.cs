using System;

namespace ApiForReact.Data.Dto
{
    public class Message
    {
        public Guid Id { get; set; }
        public Guid UserOwnerId { get; set; }
        public Guid UserCompanionId { get; set; }
        public Guid DialogId { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public int Status { get; set; }
    }
}
