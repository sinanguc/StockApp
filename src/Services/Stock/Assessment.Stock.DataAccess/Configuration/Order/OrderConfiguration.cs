using Assessment.Stock.Entities.Concrete.Invoices;
using Assessment.Stock.Entities.Concrete.Orders;
using Assessment.Stock.Entities.Concrete.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assessment.StockMountAPI.DataAccess.Configuration.OrderConfiguration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("order", "public");
            builder.HasKey(d => d.Id);
            builder.Property(d => d.Id).HasColumnName("id").IsRequired().ValueGeneratedOnAdd();
            builder.Property(d => d.StoreId).HasColumnName("store_id").IsRequired();
            builder.Property(d => d.ProductId).HasColumnName("product_id").IsRequired();
            builder.Property(d => d.OrderProductCount).HasColumnName("order_product_count").IsRequired();
            builder.Property(d => d.Amount).HasColumnName("amount").IsRequired();
            builder.Property(d => d.InvoiceId).HasColumnName("invoice_id");
            builder.Property(d => d.InvoiceStatus).HasColumnName("invoice_status").IsRequired();
            builder.Property(d => d.OrderStatus).HasColumnName("order_status").HasMaxLength(20).IsRequired();
            builder.Property(d => d.OrderTime).HasColumnName("order_time");

            builder.HasOne<Product>(d => d.Product).WithMany(d => d.Order).HasForeignKey(d => d.ProductId);
            builder.HasOne<Invoice>(d => d.Invoice).WithMany(d => d.Order).HasForeignKey(d => d.InvoiceId).OnDelete(DeleteBehavior.SetNull);

            builder.HasIndex(d => new { d.Id }).IsUnique();
            builder.HasIndex(d => new { d.StoreId, d.ProductId, d.InvoiceId }).IsUnique(false);
        }
    }
}
