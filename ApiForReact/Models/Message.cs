using System;

namespace ApiForReact.Models
{
    public class Message
    {
        public Guid Id { get; set; }
        public Guid UserOwnerId { get; set; }
        public Guid UserCompanionId { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public int Status { get; set; }

        public static Message Map(Data.Dto.Message messageDto)
        {
            return new Message
            {
                Id = messageDto.Id,
                UserCompanionId = messageDto.UserIdCompanion,
                UserOwnerId = messageDto.UserIdOwner,
                Text = messageDto.Text,
                Status = messageDto.Status,
                Date = messageDto.Date
            };
        }
    }
}
