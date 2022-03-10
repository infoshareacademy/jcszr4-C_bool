using System;
using C_Bool.API.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace C_bool.API.DAL.Context
{
    public class ReportDbContext : DbContext
    {
        public DbSet<Place> Places { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<GameTask> GameTasks { get; set; }
        public DbSet<UserGameTaskReport> UserGameTask { get; set; }
        public ReportDbContext(DbContextOptions<ReportDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
}
