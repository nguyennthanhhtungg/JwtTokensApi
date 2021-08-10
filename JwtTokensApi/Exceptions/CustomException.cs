using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace JwtTokensApi.Exceptions
{
    public class CustomException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }

        public CustomException(HttpStatusCode statusCode) : base()
        {
            this.StatusCode = statusCode;
        }

        public CustomException(HttpStatusCode statusCode, string message) : base(message)
        {
            this.StatusCode = statusCode;
        }
    }
}
