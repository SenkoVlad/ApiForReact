using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiForReact.Data.Dto
{
    public class Dialog
    {
        public Guid Id { get; set; }
        
        public Guid OwnerUserId { get; set; }
        public Guid CompanionUserId { get; set; }

        [ForeignKey("CompanionUserId")]
        public virtual User CompanionUser { get; set; }
        [ForeignKey("OwnerUserId")]
        public virtual User OwnerUser { get; set; }

        public ICollection<Message> Messages { get; set; }
    }
}
