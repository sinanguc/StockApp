using Assessment.Common.Security;
using Assessment.Dto;
using Assessment.Dto.Stock.Order;
using Assessment.Enum.Stock.Messages;
using Assessment.Stock.Business.Abstract.OrderService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assessment.Stock.WebApi.Controllers
{
    [AssessmentAuthorize]
    public class IntegrationController : BaseController
    {
        private readonly IOrderService _orderService;

        public IntegrationController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("GetSales")]
        public async Task<ActionResult<GenericResult>> GetSales(SalesRequestDto salesRequestDto)
        {
            result.Message = StockMessages.BasariylaListelendi;
            result.Response = await _orderService.GetSalesAsync(salesRequestDto);
            return Ok(result);
        }
    }
}
