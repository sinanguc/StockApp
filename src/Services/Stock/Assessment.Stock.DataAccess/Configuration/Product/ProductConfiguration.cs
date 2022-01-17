using Assessment.Stock.Entities.Concrete.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assessment.Stock.DataAccess.Configuration.ProductConfiguration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("product", "public");
            builder.HasKey(d => d.Id);
            builder.Property(d => d.Id).HasColumnName("id").IsRequired().ValueGeneratedOnAdd();
            builder.Property(d => d.Name).HasColumnName("name").HasMaxLength(150).IsRequired();
            builder.Property(d => d.Stock).HasColumnName("stock").IsRequired();
            builder.Property(d => d.Price).HasColumnName("price").IsRequired();
            builder.Property(d => d.Deleted).HasColumnName("deleted").IsRequired();

            builder.HasIndex(d => d.Id).IsUnique();
        }
    }
}
