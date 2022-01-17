namespace Assessment.Dto
{
    public class GenericResult
    {
        public bool Result 
        { 
            get { return Response != null; } 
        }
        public string Message { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorCode { get; set; }
        public object Response { get; set; }
    }
}
