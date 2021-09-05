using System;

namespace ApiForReact.Models
{
    public class Dialog
    {
        public Guid Id { get; set; }
        public Guid  UserOwnerId { get; set; }
        public Guid UserCompanionId { get; set; }
        public string Name { get; set; }
        public static Dialog Map(Data.Dto.Dialog dialogDto)
        {
            return new Dialog
            {
                Id = dialogDto.Id,
                UserCompanionId = dialogDto.UserCompanionId,
                UserOwnerId = dialogDto.UserOwnerId
            };
        }
    }
}
