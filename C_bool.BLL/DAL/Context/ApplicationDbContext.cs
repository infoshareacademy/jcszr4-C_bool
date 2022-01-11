using C_bool.BLL.DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace C_bool.BLL.DAL.Context
{
    public class ApplicationDbContext : IdentityDbContext<User, UserRole, int>
    {
        public DbSet<Place> Places { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<GameTask> GameTasks { get; set; }
        public DbSet<UserPlace> UsersPlaces { get; set; }
        public DbSet<UserGameTask> UsersGameTasks { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
            
            modelBuilder.Entity<UserPlace>().HasKey(table => new {
                table.UserId,
                table.PlaceId
            });

            modelBuilder.Entity<UserGameTask>().HasKey(table => new {
                table.UserId,
                table.GameTaskId
            });
        }
    }
}
