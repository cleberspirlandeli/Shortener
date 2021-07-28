using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Shortener.Common.Extensions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            // TODO: Inserted Error in database and return your IdError
            var message = "Ops, an unexpected error occurred. Try again later! Error ${Error.Id} - " + ex.Message;

            var result = JsonConvert.SerializeObject(new { success = false, errors = new List<string> { message } });

            //httpContext.Response.ContentType = "application/problem+json";
            httpContext.Response.ContentType = "application/json";
            await httpContext.Response.WriteAsync(result);
        }

    }
}
