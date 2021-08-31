using ApiForReact.Models;
using System;
using System.Collections.Generic;

namespace ApiForReact.Data.Dto
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string PhotoUrl { get; set; }
        public string Status { get; set; }
        public Location Location { get; set; }
        public ICollection<Post> Posts { get; set; }

        public UserContacts UserContacts { get; set; }
        public string Info { get; set; }
        public bool IsLookingForAJob { get; set; }
        public string ResumeText { get; set; }

        public static void Map(Models.User userModel, User userDto)
        {
            userDto.IsLookingForAJob = userModel.IsLookingForAJob;
            userDto.Location.City = userModel.Location.City;
            userDto.Location.Country = userModel.Location.Country;
            userDto.Name = userModel.Name;
            userDto.ResumeText = userModel.ResumeText;
            userDto.Status = userModel.Status;
            userDto.UserContacts.Facebook = userModel.UserContacts.Facebook;
            userDto.UserContacts.GitHub = userModel.UserContacts.GitHub;
            userDto.UserContacts.Instagram = userModel.UserContacts.Instagram;
            userDto.UserContacts.Youtube = userModel.UserContacts.Youtube;
            userDto.UserContacts.Twitter = userModel.UserContacts.Twitter;
            userDto.UserContacts.Vk = userModel.UserContacts.Vk;
        }
    }
}
