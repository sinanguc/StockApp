using System;

namespace Assessment.Dto.Stock.Log
{
    public class LogResponseDto
    {
        public string Id { get; set; }
        public string LogType { get; set; }
        public string Data { get; set; }
        public DateTime RecordTime { get; set; }
    }
}
