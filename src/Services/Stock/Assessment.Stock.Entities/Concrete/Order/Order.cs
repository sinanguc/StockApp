using Assessment.Stock.Entities.Concrete.Invoices;
using Assessment.Stock.Entities.Concrete.Products;
using System;

namespace Assessment.Stock.Entities.Concrete.Orders
{
    public class Order : BaseEntity, IEntity
    {
        public int StoreId { get; set; }
        public int ProductId { get; set; }
        public int OrderProductCount { get; set; }
        public decimal Amount { get; set; }
        public int? InvoiceId { get; set; }
        public int InvoiceStatus { get; set; }
        public string OrderStatus { get; set; }
        public DateTime OrderTime { get; set; }

        public virtual Product Product { get; set; }
        public virtual Invoice Invoice { get; set; }
    }
}
