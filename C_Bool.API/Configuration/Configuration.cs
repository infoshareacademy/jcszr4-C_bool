using C_Bool.API.DAL.Entities;
using C_Bool.API.Helpers.ValueConverters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace C_bool.API.Configuration
{
    public class PlaceConfiguration : IEntityTypeConfiguration<PlaceReport>
    {
        public void Configure(EntityTypeBuilder<PlaceReport> builder)
        {

            builder.Property(e => e.Types).HasConversion(new TypeValueConverter());

        }

    }
}
