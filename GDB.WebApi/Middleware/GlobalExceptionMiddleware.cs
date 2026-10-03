using System.Net;
using System.Text.Json;
using GDB.Core.Domain.Exceptions;

namespace GDB.WebApi.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(
            RequestDelegate next,
            ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (InvalidAmountException ex)
            {
                await WriteErrorResponse(
                    context,
                    HttpStatusCode.BadRequest,
                    ex.Message);
            }
            catch (InvalidPinException ex)
            {
                await WriteErrorResponse(
                    context,
                    HttpStatusCode.BadRequest,
                    ex.Message);
            }
            catch (InactiveAccountException ex)
            {
                await WriteErrorResponse(
                    context,
                    HttpStatusCode.Conflict,
                    ex.Message);
            }
            catch (MinimumBalanceViolationException ex)
            {
                await WriteErrorResponse(
                    context,
                    HttpStatusCode.UnprocessableEntity,
                    ex.Message);
            }
            catch (InsufficientBalanceException ex)
            {
                await WriteErrorResponse(
                    context,
                    HttpStatusCode.UnprocessableEntity,
                    ex.Message);
            }
            catch (AccountException ex)
            {
                await WriteErrorResponse(
                    context,
                    HttpStatusCode.BadRequest,
                    ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                await WriteErrorResponse(
                    context,
                    HttpStatusCode.Conflict,
                    ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Unhandled exception occurred.");

                await WriteErrorResponse(
                    context,
                    HttpStatusCode.InternalServerError,
                    "An unexpected error occurred.");
            }
        }

        private static async Task WriteErrorResponse(
            HttpContext context,
            HttpStatusCode statusCode,
            string message)
        {
            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";

            var response = new
            {
                status = (int)statusCode,
                message
            };

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(response));
        }
    }
}

