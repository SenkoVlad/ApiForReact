using ApiForReact.Models;
using ApiForReact.Services.Intarfaces;
using System;

namespace ApiForReact.Services.Implementations
{
    public class UsersProfileService : IUsersProfileService
    {
        private ITextGeneratorService _textGeneratorService;
        public UsersProfileService(ITextGeneratorService nameGeneratorService) =>
            _textGeneratorService = nameGeneratorService;

        public UserProfile GetUserProfile(Guid userId)
        {
            var random = new Random();
            UserProfile userProfile = new UserProfile
            {
                UserId = userId,
                Contacts = new UserContacts
                {
                    Facebook = "https://facebook.com/" + _textGeneratorService.GenerateText(random.Next(5, 10)),
                    GitHub = "https://github.com/" + _textGeneratorService.GenerateText(random.Next(5, 10)),
                    Instagram = "https://instagram.com/" + _textGeneratorService.GenerateText(random.Next(5, 10)),
                    Twitter = "https://twitter.com/" + _textGeneratorService.GenerateText(random.Next(5, 10)),
                    Vk = "https://vk.com/" + _textGeneratorService.GenerateText(random.Next(5, 10)),
                    Youtube = "https://youtube.com/" + _textGeneratorService.GenerateText(random.Next(5, 10))
                },
                FullName = _textGeneratorService.GenerateText(random.Next(5, 10)),
                Info = _textGeneratorService.GenerateText(random.Next(5, 10), 7),
                IsLookingForAJob = Convert.ToBoolean(random.Next(0, 2)),
                PhotoUrl = "",
                ResumeText = _textGeneratorService.GenerateText(random.Next(5, 10), 10)
            };
            return userProfile;
        }
    }
}
