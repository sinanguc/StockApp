
using MongoDB.Driver;

namespace Assessment.Common.Infrastructure.Mongo
{
    public interface IMongoContext
    {
        IMongoCollection<ResponseRequestLog> ResponseRequestLogs { get; }
    }
}
