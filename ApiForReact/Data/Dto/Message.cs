using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiForReact.Data.Dto
{
    public class Message
    {
        public Guid Id { get; set; }
        public User UserOwner { get; set; }
        public User UserCompanion { get; set; }
        public string Text { get; set; }
    }
}
