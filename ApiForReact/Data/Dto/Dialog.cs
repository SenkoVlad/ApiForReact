using System;
using System.Collections.Generic;

namespace ApiForReact.Data.Dto
{
    public class Dialog
    {
        public Guid Id { get; set; }
        public Guid UserOwnerId { get; set; }
        public Guid UserCompanionId { get; set; }
        public ICollection<Message> Messages { get; set; }
    }
}
