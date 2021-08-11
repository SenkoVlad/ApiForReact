﻿using System;
using System.Collections.Generic;

namespace ApiForReact.Data.Dto
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string PhotoUrl { get; set; }
        public int Status { get; set; }
        public Location Location { get; set; }
        public ICollection<Post> Posts { get; set; }

        public ICollection<User> SubscriberUser { get; set; }
        public ICollection<User> SubscriptionUser { get; set; }
    }
}