using JwtTokensApi.Exceptions;
using JwtTokensApi.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace JwtTokensApi.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (CustomException ex)
            {
                await HandleExceptionAsync(context, ex);
            }
            catch (HttpStatusCodeException ex)
            {
                await HandleExceptionAsync(context, ex);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            string result = null;
            context.Response.ContentType = "application/json";

            if (exception is CustomException)
            {
                CustomException customException = exception as CustomException;

                result = new ErrorDetails()
                {
                    Message = customException.Message,
                    StatusCode = (int)customException.StatusCode
                }.ToString();

                context.Response.StatusCode = (int)customException.StatusCode;
            }
            else if (exception is HttpStatusCodeException)
            {
                HttpStatusCodeException httpStatusCodeException = exception as HttpStatusCodeException;

                result = new ErrorDetails()
                {
                    Message = httpStatusCodeException.Message,
                    StatusCode = (int)httpStatusCodeException.StatusCode
                }.ToString();

                context.Response.StatusCode = (int)httpStatusCodeException.StatusCode;
            }
            else
            {
                result = new ErrorDetails()
                {
                    Message = exception.Message,
                    StatusCode = (int)HttpStatusCode.InternalServerError
                }.ToString();

                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            return context.Response.WriteAsync(result);
        }
    }
}
