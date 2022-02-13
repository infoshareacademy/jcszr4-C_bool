using System;
using C_bool.BLL.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace C_bool.BLL.DAL.Context
{
    public class ApplicationDbContext : IdentityDbContext<User, UserRole, int>
    {
        public DbSet<Place> Places { get; set; }
        public override DbSet<User> Users { get; set; }
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
            //Seeding roles to AspNetRoles table
                modelBuilder.Entity<UserRole>().HasData(new UserRole
            {
                Id = 1, 
                Name = "Admin", 
                NormalizedName = "ADMIN"
            });
            modelBuilder.Entity<UserRole>().HasData(new UserRole
            {
                Id = 2,
                Name = "Moderator",
                NormalizedName = "MODERATOR"
            });
            modelBuilder.Entity<UserRole>().HasData(new UserRole
            {
                Id = 3,
                Name = "User",
                NormalizedName = "USER"
            });

            //a hasher to hash the password before seeding the user to the db
            var hasher = new PasswordHasher<User>();

            //Seeding the User to AspNetUsers table
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    UserName = "SuperAdmin",
                    NormalizedUserName = "SUPERADMIN",
                    Email = "super@admin.com",
                    NormalizedEmail = "SUPER@ADMIN.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Pa$$w0rd"),
                    SecurityStamp = Guid.NewGuid().ToString("D")
                }
            );

            //Seeding the relation between our user and role to AspNetUserRoles table
            modelBuilder.Entity<IdentityUserRole<int>>().HasData(
                new IdentityUserRole<int>
                {
                    RoleId = 1,
                    UserId = 1
                }
            );

            //modelBuilder.Entity<Place>().HasMany(g => g.Tasks);
            //modelBuilder.Entity<GameTask>().HasOne(g => g.Place);

        }
    }
}
