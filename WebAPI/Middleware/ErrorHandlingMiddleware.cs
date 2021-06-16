using Application.Exceptions;
using Application.Wrapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace WebAPI.Middleware
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<ErrorHandlingMiddleware> _logger;
        public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger)
        {
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch(NotFoundException notFoundException)
            {
                _logger.LogError(notFoundException, notFoundException.Message);
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                await context.Response.WriteAsJsonAsync(new ServiceResponse(notFoundException.Message, false));
            }
            catch(BadRequestException badRequestException)
            {
                _logger.LogError(badRequestException, badRequestException.Message);
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await context.Response.WriteAsJsonAsync(new ServiceResponse(badRequestException.Message, false));
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsJsonAsync(new ServiceResponse($"Internal server error: {ex.Message}",false));
            }
        }
    }
}
