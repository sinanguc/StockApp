using Assessment.Stock.Entities.Concrete.Invoices;
using Assessment.Stock.Entities.Concrete.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assessment.Stock.DataAccess.Configuration.InvoiceConfiguration
{
    public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.ToTable("invoice", "public");
            builder.HasKey(d => d.Id);
            builder.Property(d => d.Id).HasColumnName("id").IsRequired().ValueGeneratedOnAdd();
            builder.Property(d => d.ProductId).HasColumnName("product_id").IsRequired();
            builder.Property(d => d.CustomerName).HasColumnName("customer_name").HasMaxLength(150).IsRequired();
            builder.Property(d => d.CustomerSurname).HasColumnName("customer_surname").HasMaxLength(150).IsRequired();
            builder.Property(d => d.Amount).HasColumnName("amount").IsRequired();
            builder.Property(d => d.InvoiceDate).HasColumnName("invoice_date").IsRequired();

            builder.HasOne<Product>(d => d.Product).WithMany(d => d.Invoice).HasForeignKey(d => d.ProductId);

            builder.HasIndex(d => new { d.Id }).IsUnique();
            builder.HasIndex(d => new { d.ProductId }).IsUnique(false);
        }
    }
}
