namespace LabirentFethiye.Common.Responses
{
    public class ResponseDto<T> where T : class
    {
        public T Data { get; private set; } = default!;
        public int StatusCode { get; private set; }
        public List<ErrorDto> Errors { get; set; }
        public PageInfo PageInfo { get; set; }

        public static ResponseDto<T> Success(T data, int statusCode, PageInfo pageInfo) => new ResponseDto<T> { Data = data, StatusCode = statusCode, PageInfo = pageInfo };
        public static ResponseDto<T> Success(T data, int statusCode) => new ResponseDto<T> { Data = data, StatusCode = statusCode };
        public static ResponseDto<T> Fail(List<ErrorDto> errors, int statusCode) => new ResponseDto<T> { Errors = errors, StatusCode = statusCode };
    }
}
