using Assessment.Dto.Stock.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.Stock.Business.Abstract.Log
{
    public interface ILogService
    {
        Task<List<LogResponseDto>> GetRequestResponseAsync(LogRequestDto logRequestDto);
    }
}
