using Assessment.Stock.Entities.Concrete.Invoices;
using Assessment.Stock.Entities.Concrete.Orders;
using System.Collections.Generic;

namespace Assessment.Stock.Entities.Concrete.Products
{
    public class Product : BaseEntity, IEntity, ISoftDeleteEntity
    {
        public string Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public bool Deleted { get; set; }

        #region Many to One
        public virtual ICollection<Order> Order { get; set; }
        public virtual ICollection<Invoice> Invoice { get; set; }
        #endregion
    }
}
