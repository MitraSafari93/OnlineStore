using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Domain;

namespace OnlineStore.Infrustructure
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(current => current.Title)
                .HasMaxLength(40);

            builder.HasIndex(current => current.Title)
                .IsUnique(); 
        }
    }
}
