using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Net;
namespace Desafio.CopaGame.Filters
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        private readonly IWebHostEnvironment _env;

        public CustomExceptionFilter(IWebHostEnvironment env)
        {
            _env = env;
        }

        public void OnException(ExceptionContext context)
        {
            HttpResponse response = context.HttpContext.Response;
            Exception exception = context.Exception;
            response.StatusCode = (int)HttpStatusCode.InternalServerError;
            response.ContentType = "application/json";

            if (_env.IsProduction())
            {
                context.Result = new JsonResult(new
                {
                    message = "Error!"
                });
                return;
            }

            var result = new
            {
                message = exception.Message,
                innerException = exception.InnerException?.Message,
                stackTrace = exception.StackTrace,
            };

            context.Result = new JsonResult(result);
        }

    }
}
