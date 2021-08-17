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
        [ForeignKey("User")]
        public Guid SubscriberUserId { get; set; }
        [ForeignKey("User")]
        public Guid  SubscriptionUserId { get; set; }
    }
}