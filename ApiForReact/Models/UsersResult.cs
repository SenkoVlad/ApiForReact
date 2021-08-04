using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiForReact.Models
{
    public class UsersResult
    {
        public IEnumerable<User> Items { get; set; }
        public int TotalCount { get; set; }
    }
}
