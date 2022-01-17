using Assessment.Dto.Stock.Invoice;
using Assessment.Dto.Stock.Order;
using Assessment.Dto.Stock.Product;
using Assessment.Stock.Business.Abstract.OrderService;
using Assessment.Stock.DataAccess.Abstract;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assessment.Stock.Business.Concrete.OrderManager
{
    public class OrderManager : IOrderService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        
        public OrderManager(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public List<SalesResponseDto> GetSales(SalesRequestDto salesRequestDto)
        {
            var sales = _unitOfWork.OrderRepository.GetAll(d =>
                        d.StoreId == salesRequestDto.StoreId && d.OrderStatus == salesRequestDto.OrderStatus && d.InvoiceStatus == salesRequestDto.InvoiceStatus);
            return _mapper.Map<List<SalesResponseDto>>(sales);
        }

        public async Task<List<SalesResponseDto>> GetSalesAsync(SalesRequestDto salesRequestDto)
        {
            var sales = (from o in _unitOfWork.StockContext.Order
                        join p in _unitOfWork.StockContext.Product on o.ProductId equals p.Id
                        join oi in _unitOfWork.StockContext.Invoice on o.InvoiceId equals oi.Id into orderInvoice
                        from oi in orderInvoice.DefaultIfEmpty()
                        where o.StoreId == salesRequestDto.StoreId 
                        && o.OrderStatus == salesRequestDto.OrderStatus 
                        && o.InvoiceStatus == salesRequestDto.InvoiceStatus
                        select new SalesResponseDto
                        {
                            Amount = o.Amount,
                            Id = o.Id,
                            Invoice = oi != null ? _mapper.Map<InvoiceResponseDto>(oi) : null,
                            InvoiceStatus = o.InvoiceStatus,
                            OrderProductCount = o.OrderProductCount,
                            OrderStatus = o.OrderStatus,
                            OrderTime = o.OrderTime,
                            StoreId = o.StoreId,
                            Product = _mapper.Map<ProductResponseDto>(p)
                        });


            return await Task.FromResult(sales.ToList());
        }
    }
}
