using System;
using System.Collections.Generic;

namespace ApiForReact.Data.Dto
{
    public class Dialog
    {
        public Guid Id { get; set; }
        public User UserOwner { get; set; }
        public User UserCompanion { get; set; }
        public ICollection<Message> Messages { get; set; }
    }
}
