using Assessment.Common.Infrastructure.Mongo;
using Assessment.Dto.Stock.Invoice;
using Assessment.Dto.Stock.Log;
using Assessment.Dto.Stock.Order;
using Assessment.Dto.Stock.Product;
using Assessment.Stock.Entities.Concrete.Invoices;
using Assessment.Stock.Entities.Concrete.Orders;
using Assessment.Stock.Entities.Concrete.Products;
using AutoMapper;

namespace Assessment.Stock.Business.DependencyResolvers.AutoMapper
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Product, ProductResponseDto>().ReverseMap();
            CreateMap<Invoice, InvoiceResponseDto>().ReverseMap();
            CreateMap<Order, SalesResponseDto>().ReverseMap();
        }
    }
}
