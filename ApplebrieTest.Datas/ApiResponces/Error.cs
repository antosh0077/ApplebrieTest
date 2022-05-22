using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ApplebrieTest.Datas.ApiResponces
{
    public class Error
    {
        public int? ErrorCode { get; set; }
        public HttpStatusCode? StatusCode { get; set; }
        public string? Message { get; set; }
        public Error(Error error, string supplementaryMessage = "")
        {
            Message = String.IsNullOrEmpty(supplementaryMessage) ? error.Message : $"{error.Message} - {supplementaryMessage}";
            ErrorCode = error.ErrorCode;
            StatusCode = error.StatusCode;
        }

        public Error(int code, string errorMessage, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            ErrorCode = code;
            Message = errorMessage;
            StatusCode = statusCode;
        }

        public Error(string errorMessage)
        {
            Message = errorMessage;
        }

        public Error WithMessage(string supplementaryMessage)
        {
            return new Error(this, supplementaryMessage);
        }
    }
}
