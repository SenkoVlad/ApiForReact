using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiForReact.Data.Dto
{
    public class UserUser
    {
        public Guid Id { get; set; }
        public Guid SubscriberUserId { get; set; }
        public Guid  SubscriptionUserId { get; set; }

        [ForeignKey("SubscriberUserId")]
        public virtual User SubscriberUser { get; set; }
        [ForeignKey("SubscriptionUserId")]
        public virtual User SubscriptionUser { get; set; }
    }
}