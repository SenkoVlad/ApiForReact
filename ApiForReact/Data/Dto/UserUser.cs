using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiForReact.Data.Dto
{
    public class UserUser
    {
        public Guid Id { get; set; }
        public User SubscriberUser { get; set; }
        public User SubscriptionUser { get; set; }
    }
}
