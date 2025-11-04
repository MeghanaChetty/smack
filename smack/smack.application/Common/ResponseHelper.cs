using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace smack.application.Common
{
    public class ResponseHelper
    {
        public ApiResponse SuccessReponse<T>(T data,string message)
        {
            return new ApiResponse
            {
                Success = true,
                Message = message,
                TData = data,
                TimeStamp = DateTime.UtcNow
            };
        }

        public ApiErrorResponse ErrorResponse(string message, List<string> errors = null)
        {
            return new ApiErrorResponse
            {
                Success = false,
                Message = message,
                Errors = errors ?? new List<string>(),
                TimeStamp = DateTime.UtcNow
            };
        }
    }
}
