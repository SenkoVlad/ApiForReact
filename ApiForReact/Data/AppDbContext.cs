using ApiForReact.Data.Dto;
using Microsoft.EntityFrameworkCore;
using System;

namespace ApiForReact.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Dialog> Dialogs { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserContacts> UserContacts { get; set; }

        public DbSet<UserUser> UsersUsers { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : base(dbContextOptions) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserUser>().HasIndex(u => new { u.SubscriberUserId, u.SubscriptionUserId })
                                           .IsUnique(true);

        }
    }
}
