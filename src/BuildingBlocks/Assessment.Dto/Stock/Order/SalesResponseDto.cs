using Assessment.Dto.Stock.Invoice;
using Assessment.Dto.Stock.Product;
using System;

namespace Assessment.Dto.Stock.Order
{
    public class SalesResponseDto
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public ProductResponseDto Product { get; set; }
        public int OrderProductCount { get; set; }
        public decimal Amount { get; set; }
        public InvoiceResponseDto Invoice { get; set; }
        public int InvoiceStatus { get; set; }
        public string OrderStatus { get; set; }
        public DateTime OrderTime { get; set; }
    }
}
