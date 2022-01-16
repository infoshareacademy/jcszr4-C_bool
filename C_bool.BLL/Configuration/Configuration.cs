using C_bool.BLL.DAL.Entities;
using C_bool.BLL.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace C_bool.BLL.Configuration
{
    public class PlaceConfiguration : IEntityTypeConfiguration<Place>
    {
        public void Configure(EntityTypeBuilder<Place> builder)
        {

            builder.Property(e => e.Types).HasConversion(new TypeValueConverter());

        }

    }

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {

            builder.Property(e => e.UserBadges).HasConversion(new BadgeValueConverter());

        }

    }
}
