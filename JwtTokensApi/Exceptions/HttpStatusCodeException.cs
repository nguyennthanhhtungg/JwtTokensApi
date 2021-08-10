using Newtonsoft.Json.Linq;
using System;
using System.Net;

namespace JwtTokensApi.Exceptions
{
    public class HttpStatusCodeException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }

        public HttpStatusCodeException(HttpStatusCode statusCode) : base()
        {
            this.StatusCode = statusCode;
        }

        public HttpStatusCodeException(HttpStatusCode statusCode, string message) : base(message)
        {
            this.StatusCode = statusCode;
        }

        public HttpStatusCodeException(HttpStatusCode statusCode, Exception inner)
            : this(statusCode, inner.ToString()) { }

        public HttpStatusCodeException(HttpStatusCode statusCode, JObject errorObject)
            : this(statusCode, errorObject.ToString())
        {
        }
    }
}
