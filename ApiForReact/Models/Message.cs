using System;

namespace ApiForReact.Models
{
    public class Message
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public int Status { get; set; }
        public Guid UserIdOwner { get; set; }

        public static Message Map(Data.Dto.Message messageDto)
        {
            return new Message
            {
                Id = messageDto.Id,
                Text = messageDto.Text,
                Status = messageDto.Status,
                Date = messageDto.Date,
                UserIdOwner = messageDto.UserIdOwner
            };
        }
    }
}
