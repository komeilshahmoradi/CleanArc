using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrasructure.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Products>
    {
        public void Configure(EntityTypeBuilder<Products> builder)
        {
            builder.Property(x => x.Price)
                .HasPrecision(18, 0);
        }
    }
}
