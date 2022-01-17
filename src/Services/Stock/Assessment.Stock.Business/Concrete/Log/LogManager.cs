using Assessment.Common.Infrastructure.Mongo;
using Assessment.Dto.Stock.Log;
using Assessment.Stock.Business.Abstract.Log;
using AutoMapper;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Assessment.Stock.Business.Concrete.Log
{
    public class LogManager : ILogService
    {
        private readonly IMongoContext _mongoContext;
        private readonly IMapper _mapper;
        public LogManager(IMongoContext mongoContext, IMapper mapper)
        {
            _mongoContext = mongoContext;
            _mapper = mapper;
        }
        public async Task<List<LogResponseDto>> GetRequestResponseAsync(LogRequestDto logRequestDto)
        {
            var result = await _mongoContext.ResponseRequestLogs.Find(d => d.LogType == logRequestDto.LogType).SortByDescending(d => d.RecordTime).Limit(20).ToListAsync();
            return _mapper.Map<List<LogResponseDto>>(result);
        }
    }
}
