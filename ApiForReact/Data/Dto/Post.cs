using System;

namespace ApiForReact.Data.Dto
{
    public class Post
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public int LikesCount { get; set; }
        public User User { get; set; }
    }
}
