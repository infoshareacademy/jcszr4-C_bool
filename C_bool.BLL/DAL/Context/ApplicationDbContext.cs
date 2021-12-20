using C_bool.BLL.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace C_bool.BLL.DAL.Context
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Place> Places { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<GameTask> GameTasks { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
}
