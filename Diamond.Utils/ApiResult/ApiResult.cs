namespace Diamond.Utils.ApiResult
{
    public class ApiResult
    {
        public bool IsSuccess { get; set; } 
        public string Error { get; set; }
    }

    public class ApiResult<T>
    {
        public bool IsSuccess { get; set; }
        public string Error { get; set; }
        public T Data { get; set; }
    }
}
