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
                context.Database.Migrate();

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
                    Id = Guid.NewGuid(),
                    Name = textGeneratorService.GenerateText(random.Next(5, 10), 2),
                    PhotoUrl = "",
                    Status = textGeneratorService.GenerateText(random.Next(5, 10), 3),
                    Location = location,
                };
                Users.Add(user);
            }

            return Users;
        }
    }
}
