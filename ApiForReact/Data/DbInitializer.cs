using ApiForReact.Data.Dto;
using ApiForReact.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiForReact.Data
{
    public static class DbInitializerExtention
    {
        public static void InitializeAsync(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
                Initialize(context, app);
            }
        }

        public static void Initialize(AppDbContext context, IApplicationBuilder app)
        {
            context.Database.EnsureCreated();
            
            if (context.Users.Any())
                return;   

            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var textGeneratorService = serviceScope.ServiceProvider.GetRequiredService<ITextGeneratorService>();
                var users = GenerateUsers(200, textGeneratorService);

                foreach (var user in users)
                    context.Users.Add(user);

                context.SaveChanges();
            }
        }

        public static List<User> GenerateUsers(int count, ITextGeneratorService textGeneratorService)
        {
            List<User> Users = new List<User>();
            Random random = new Random();

            for (int i = 0; i < count; i++)
            {
                Location location = new Location
                {
                    City = textGeneratorService.GenerateText(random.Next(4, 10)),
                    Country = textGeneratorService.GenerateText(random.Next(4, 10))
                };

                User user = new User
                {
                    Id = i == 0 ? Guid.Parse("f6703ebc-be8c-4552-b11a-1e6f5a34bcf5") : Guid.NewGuid(),
                    Name = i == 0 ? "vlad" : textGeneratorService.GenerateText(random.Next(5, 10), 2),
                    PhotoUrl = "",
                    Status = textGeneratorService.GenerateText(random.Next(5, 10), 3),
                    Location = location,
                    Info = textGeneratorService.GenerateText(random.Next(5, 10), 5),
                    ResumeText = textGeneratorService.GenerateText(random.Next(5, 10), 15),
                    UserContacts = new UserContacts
                    {
                        Id = Guid.NewGuid(),
                        Facebook = "https://facebook.com/" + textGeneratorService.GenerateText(random.Next(5, 10)),
                        GitHub = "https://github.com/" + textGeneratorService.GenerateText(random.Next(5, 10)),
                        Instagram = "https://instagram.com/" + textGeneratorService.GenerateText(random.Next(5, 10)),
                        Twitter = "https://twitter.com/" + textGeneratorService.GenerateText(random.Next(5, 10)),
                        Vk = "https://vk.com/" + textGeneratorService.GenerateText(random.Next(5, 10)),
                        Youtube = "https://youtube.com/" + textGeneratorService.GenerateText(random.Next(5, 10))
                    },
                    IsLookingForAJob = false,
                    Email = textGeneratorService.GenerateText(5) + "@" + textGeneratorService.GenerateText(6) + ".com",
                    Password = "1111"
                };
                Users.Add(user);
            }

            return Users;
        }
    }
}
