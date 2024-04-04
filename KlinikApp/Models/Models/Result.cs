namespace Shared.Models
{
    public class Result
    {
        public object MyResult { get; set; }

        public int ResultCode { get; set; }

        public string ResultMessage { get; set; }

        public bool IsSuccess { get; set; }

        public static Result Ok()
        {
            return new Result { IsSuccess = true, ResultCode = 200 };
        }

        public static Result Ok(object result)
        {
            return new Result { MyResult = result, IsSuccess = true, ResultCode = 200 };
        }

        public static Result Ok(object result, string message)
        {
            return new Result { MyResult = result, IsSuccess = true, ResultMessage = message, ResultCode = 200 };
        }

        public static Result Ok(string message, int resultCode)
        {
            return new Result { IsSuccess = true, ResultMessage = message, ResultCode = resultCode };
        }

        public static Result Fail(int resultCode)
        {
            return new Result { IsSuccess = false, ResultCode = resultCode };
        }

        public static Result Fail(string message, int resultCode)
        {
            return new Result { IsSuccess = false, ResultMessage = message, ResultCode = resultCode };
        }
    }
}
