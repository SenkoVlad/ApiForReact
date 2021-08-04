using System;

namespace ApiForReact.Models
{
    public class User
    {
        public string Name { get; set; }
        public Guid Id { get; set; }
        public string PhotoUrl { get; set; }
        public int Status { get; set; }
        public int Followed { get; set; }
        public Location Location { get; set; }
    }
}
