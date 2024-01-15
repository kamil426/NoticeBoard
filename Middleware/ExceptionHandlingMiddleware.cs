using Microsoft.AspNetCore.Mvc;
using NLog.Fluent;
using NoticeBoard.Controllers;
using System.Net;
using System.Security.Policy;

namespace NoticeBoard.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        public record ExceptionResponse(HttpStatusCode StatusCode, string Description);
        private readonly RequestDelegate _next;
        private static readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                HandleException(context, ex);
            }
        }

        private void HandleException(HttpContext context, Exception exception)
        {

            ExceptionResponse response = exception switch
            {
                ApplicationException _ => new ExceptionResponse(HttpStatusCode.BadRequest, "Application exception occurred."),
                KeyNotFoundException _ => new ExceptionResponse(HttpStatusCode.NotFound, "The request key not found."),
                UnauthorizedAccessException _ => new ExceptionResponse(HttpStatusCode.Unauthorized, "Unauthorized."),
                _ => new ExceptionResponse(HttpStatusCode.InternalServerError, "Internal server error. Please retry later.")
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)response.StatusCode;
            _logger.Error(exception, $"{exception.Message}\n{response}");
            context.Response.Redirect("/Home/Error");
        }
    }
}
