using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using HalfDegreeApi.Models;
using HalfDegreeApi.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace HalfDegreeApi.ExceptionHandler
{
    public class LoggingExceptionsInFile
    {
        private ILogger<ErrorViewModel> _logger;
        private RequestDelegate _next; 

        public LoggingExceptionsInFile(RequestDelegate deleg, ILogger<ErrorViewModel> logger)
        {
            _logger = logger;
            _next = deleg;  
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Apply custom Exception middleware :{ex}");
                await HandlerFunction(context);
            }
        }

        private async Task HandlerFunction(HttpContext context)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";
            context.Response.Headers.ContentType = "application/json";
            var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
            if (contextFeature != null)
            {
                _logger.LogError($" something went wrong : {contextFeature.Error}");
                await context.Response.WriteAsync(new ErrorViewModel
                {
                    StatusCode = context.Response.StatusCode,
                    Message = "Internal server Error ",
                }.ToString());
            }
        }
    }
}
