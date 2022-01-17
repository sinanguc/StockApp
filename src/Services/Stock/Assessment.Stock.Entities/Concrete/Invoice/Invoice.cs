using Assessment.Stock.Entities.Concrete.Orders;
using Assessment.Stock.Entities.Concrete.Products;
using System;
using System.Collections.Generic;

namespace Assessment.Stock.Entities.Concrete.Invoices
{
    public class Invoice : BaseEntity, IEntity
    {
        public int ProductId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerSurname { get; set; }
        public decimal Amount { get; set; }
        public DateTime InvoiceDate { get; set; }

        public virtual Product Product { get; set; }

        public virtual ICollection<Order> Order { get; set; }
    }
}
