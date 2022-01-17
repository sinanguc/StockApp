using Assessment.Stock.DataAccess.Configuration.InvoiceConfiguration;
using Assessment.Stock.DataAccess.Configuration.ProductConfiguration;
using Assessment.Stock.DataAccess.Configuration.UserConfiguration;
using Assessment.Stock.Entities.Concrete.Invoices;
using Assessment.Stock.Entities.Concrete.Orders;
using Assessment.Stock.Entities.Concrete.Products;
using Assessment.Stock.Entities.Concrete.Users;
using Assessment.StockMountAPI.DataAccess.Configuration.OrderConfiguration;
using Microsoft.EntityFrameworkCore;
using System;

namespace Assessment.Stock.DataAccess.Concrete.EntityFramework.Contexts
{
    public class StockContext : DbContext
    {
        public StockContext(DbContextOptions<StockContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new InvoiceConfiguration());

            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 1,
                Name = "Stock",
                Surname = "Stock",
                Avatar = "https://pngset.com/images/image-library-stock-boy-svg-kid-child-avatar-icon-clothing-snowman-indoors-face-transparent-png-442997.png",
                Email = "info@stock.com",
                Username = "info@stock.com",
                Password = "123456",
                Deleted = false
            });

            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 1,
                Name = "IPhone 15",
                Price = 15000,
                Stock = 15,
                Deleted = false
            });

            modelBuilder.Entity<Order>().HasData(new Order
            {
                Id = 1,
                StoreId = 37814,
                InvoiceStatus = 0,
                OrderStatus = "Completed",
                Amount = 30000,
                OrderProductCount = 2,
                OrderTime = DateTime.Now,                
                ProductId = 1
            });

        }

        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<Invoice> Invoice { get; set; }

    }
}
