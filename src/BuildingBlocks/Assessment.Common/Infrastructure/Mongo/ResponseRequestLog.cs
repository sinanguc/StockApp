using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Assessment.Common.Infrastructure.Mongo
{
    public class ResponseRequestLog
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("LogType")]
        public string LogType { get; set; }

        [BsonElement("Data")]
        public string Data { get; set; }

        [BsonElement("RecordTime")]
        public BsonDateTime RecordTime { get; set; }
    }
}
