using Assessment.Common.Infrastructure.Mongo;
using Assessment.Dto.Stock.Log;
using AutoMapper;

namespace Assessment.Stock.Business.DependencyResolvers.AutoMapper
{
    public class LogProfile : Profile
    {
        public LogProfile()
        {
            CreateMap<LogResponseDto, ResponseRequestLog>().ReverseMap();
        }
    }
}
