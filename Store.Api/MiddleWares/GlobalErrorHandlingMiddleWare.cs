using Azure;
using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Shared.ErrorModels;
using System.Net;

namespace Store.Api.MiddleWares
{
    public class GlobalErrorHandlingMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalErrorHandlingMiddleWare> _logger;

        public GlobalErrorHandlingMiddleWare(RequestDelegate next , ILogger<GlobalErrorHandlingMiddleWare> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try{

                await _next(httpContext);

                if(httpContext.Response.StatusCode == (int)HttpStatusCode.NotFound)
                {
                   await HandelNotFoundEndPointAsync(httpContext);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something Went Wrong {ex}");

                await HandelExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandelExceptionAsync(HttpContext httpContext , Exception ex)
        {
            //httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            httpContext.Response.ContentType = "application/json";
            var response = new ErrorDetails()
            {
                ErrorMessage = ex.Message,

            };

            httpContext.Response.StatusCode = ex switch
            {
                NotFoundException => (int)HttpStatusCode.NotFound,
                ValidationException validationException => HandelValidationException(validationException, response),
                _ => (int)HttpStatusCode.InternalServerError
            };
            response.StatusCode = httpContext.Response.StatusCode;
            await httpContext.Response.WriteAsync(response.ToString());
        }

        private async Task HandelNotFoundEndPointAsync(HttpContext httpContext)
        {
            httpContext.Response.ContentType = "application/json";
            var response = new ErrorDetails()
            {
                ErrorMessage = $"The End point {httpContext.Request.Path} Not found",
                StatusCode = (int)HttpStatusCode.NotFound,
            }.ToString()   ;

            await httpContext.Response.WriteAsync(response);
        }
        private int HandelValidationException(ValidationException ex , ErrorDetails errorDetails)
        {
            errorDetails.Errors = ex.Errors;
            return (int)HttpStatusCode.BadRequest;
        }
    }
}  

