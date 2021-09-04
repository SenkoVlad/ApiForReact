using System;

namespace ApiForReact.Models
{
    public class User
    {
        public string Name { get; set; }
        public Guid Id { get; set; }
        public string PhotoUrl { get; set; }
        public string Status { get; set; }
        public int Followed { get; set; }
        public Location Location { get; set; }
        public string Email { get; set; }
        public UserContacts UserContacts { get; set; }
        public string Info { get; set; }
        public bool IsLookingForAJob { get; set; }
        public string ResumeText { get; set; }

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
                    },
                    IsLookingForAJob = user.IsLookingForAJob,
                    Info = user.Info,
                    ResumeText = user.ResumeText,
                    UserContacts = new UserContacts
                    {
                        Facebook = user.UserContacts.Facebook,
                        GitHub = user.UserContacts.GitHub,
                        Instagram = user.UserContacts.Instagram,
                        Twitter = user.UserContacts.Twitter,
                        Vk = user.UserContacts.Vk,
                        Youtube = user.UserContacts.Youtube
                    },
                    Email = user.Email
                };
            }
        }
    }
}
