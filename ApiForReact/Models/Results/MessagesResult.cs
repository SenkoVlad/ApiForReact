using System;
using System.Collections;
using System.Collections.Generic;

namespace ApiForReact.Models.Results
{
    public class MessagesResult
    {
        public IEnumerable<Message> Items { get; set; }
        public int totalCount { get; set; }
        public Guid dialogId { get; set; }
    }
}