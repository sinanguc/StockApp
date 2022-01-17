using Assessment.Dto.Stock.Order;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Assessment.Stock.Business.Abstract.OrderService
{
    public interface IOrderService
    {
        List<SalesResponseDto> GetSales(SalesRequestDto salesRequestDto);
        Task<List<SalesResponseDto>> GetSalesAsync(SalesRequestDto salesRequestDto);
    }
}
