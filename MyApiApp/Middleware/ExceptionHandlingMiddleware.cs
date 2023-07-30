using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace MyApiApp.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // Call the next middleware in the pipeline
                await _next(context);
            }
            catch (Exception ex)
            {
                // Log the exception (optional)
                // logger.LogError(ex, "An unhandled exception occurred.");

                // Handle the exception and return an appropriate response to the client
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            // Set the response status code based on the exception type
            int statusCode = GetStatusCode(exception);

            // Set the content type of the response
            context.Response.ContentType = "application/json";

            // Return the error response to the client
            await context.Response.WriteAsync($"{{ \"error\": \"{exception.Message}\" }}", System.Text.Encoding.UTF8);
        }

        private static int GetStatusCode(Exception exception)
        {
            // Add custom logic here to determine the status code based on the exception type
            // For example, you can check the exception type and return appropriate status codes
            // You may want to handle specific exceptions differently than others

            // In the default implementation, return 500 for any unhandled exception
            return (int)HttpStatusCode.InternalServerError;
        }
    }

}
