using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiForReact.Data.Dto
{
    public class UserContacts
    {
        public Guid Id { get; set; }
        public string Facebook { get; set; }
        public string Vk { get; set; }
        public string Instagram { get; set; }
        public string Youtube { get; set; }
        public string GitHub { get; set; }
        public string Twitter { get; set; }
    }
}
