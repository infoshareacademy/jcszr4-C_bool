using System;
using C_Bool.API.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace C_bool.API.DAL.Context
{
    public class ApiDbContext : DbContext
    {
        public DbSet<Place> Places { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<GameTask> GameTasks { get; set; }
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
}
