using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Assessment.Common.Infrastructure.Mongo
{
    public class MongoContext : IMongoContext
    {
        public MongoContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("MongoConfiguration:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("MongoConfiguration:DatabaseName"));

            ResponseRequestLogs = database.GetCollection<ResponseRequestLog>(configuration.GetValue<string>("MongoConfiguration:CollectionName"));
        }

        public IMongoCollection<ResponseRequestLog> ResponseRequestLogs { get; }
    }
}
