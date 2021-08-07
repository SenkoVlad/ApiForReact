using ApiForReact.Models;
using ApiForReact.Services.Intarfaces;
using System;
using System.Collections.Generic;

namespace ApiForReact.Services.Implementations
{
    public class UsersService : IUsersService
    {
        private ITextGeneratorService _textGeneratorService;
        private IPageGeneratorService _pageGeneratorService;
        private static int _totalPages;
        public UsersService(ITextGeneratorService nameGeneratorService, IPageGeneratorService pageGeneratorService)
        {
            _textGeneratorService = nameGeneratorService;
            _pageGeneratorService = pageGeneratorService;
            _totalPages = _pageGeneratorService.GetTotalPage();
        }

        public int GetTotalCount() => _totalPages;

        public IEnumerable<User> GetUsers(int page, int count)
        {
            List<User> Users = new List<User>();
            Random random = new Random();

            if (page * count > _totalPages + count)
                count = 0;
            else if (page * count > _totalPages)
                count = _totalPages % count;


            for (int i = 0; i < count; i++)
            {
                Location location = new Location
                {
                    City = _textGeneratorService.GenerateText(random.Next(4, 10)),
                    Country = _textGeneratorService.GenerateText(random.Next(4, 10))
                };

                User user = new User
                {
                    Id = Guid.NewGuid(),
                    Followed = random.Next(0, 2),
                    Name = _textGeneratorService.GenerateText(random.Next(5, 10), 2),
                    PhotoUrl = "",
                    Status = random.Next(0, 2),
                    Location = location
                };
                Users.Add(user);
            }

            return Users;
        }
    }
}
 