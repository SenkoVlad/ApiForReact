using ApiForReact.Data.Dto;
using Microsoft.EntityFrameworkCore;

namespace ApiForReact.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Dialog> Dialogs { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : base(dbContextOptions) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasMany(u => u.SubscriberUser)
                                       .WithMany(u => u.SubscriptionUser);
        }
    }
}
