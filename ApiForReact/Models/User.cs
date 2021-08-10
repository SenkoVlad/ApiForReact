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

        public static class Mapper
        {
            public static User Map(Data.Dto.User user)
            {
                return new User
                {
                    Name = user.Name,
                    Id = user.Id,
                    PhotoUrl = user.PhotoUrl,
                    Status = user.Status,
                    Followed = 0,
                    Location = new Location
                    {
                        City = user.Location.City,
                        Country = user.Location.Country
                    }
                };
            }
        }
    }
}
