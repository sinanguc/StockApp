using Assessment.Common.Security;
using Assessment.Dto;
using Assessment.Dto.Stock.Log;
using Assessment.Enum.Stock.Messages;
using Assessment.Stock.Business.Abstract.Log;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Assessment.Stock.WebApi.Controllers
{
    [AssessmentAuthorize]
    public class LogController : BaseController
    {
        private readonly ILogService _logService;
        public LogController(ILogService logService)
        {
            _logService = logService;
        }

        [HttpGet("GetLogs")]
        public async Task<ActionResult<GenericResult>> GetLogs([FromQuery] LogRequestDto logRequestDto)
        {
            result.Response = await _logService.GetRequestResponseAsync(logRequestDto);
            result.Message = StockMessages.BasariylaListelendi;
            return Ok(result);
        }
    }
}
