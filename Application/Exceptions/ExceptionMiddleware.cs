using Application.Exceptions.CustomExceptions;
using FluentValidation;
using Infrastructure.Serilog;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SendGrid.Helpers.Errors.Model;
using Serilog.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace Application.Exceptions;
public class ExceptionMiddleware : IMiddleware
{
    private readonly LoggerServiceBase _logger;

    public ExceptionMiddleware(LoggerServiceBase logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext, RequestDelegate next)
    {
        try
        {
            await next(httpContext);
        }
        catch (Exception ex)
        {
            LogContext.PushProperty("method_name", $"{httpContext.Request.RouteValues["controller"]}/ {httpContext.Request.RouteValues["action"]}");
            LogContext.PushProperty("user_name", httpContext?.User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value);
            LogContext.PushProperty("ip_adress", httpContext?.Connection?.RemoteIpAddress?.MapToIPv4().ToString());
            _logger.Error(ex.Message);

            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        int statusCode = GetStatusCode(exception);
        httpContext.Response.StatusCode = statusCode;
        httpContext.Response.ContentType = "application/json";

        List<string> errors = new()
            {
                $"Hata Mesajı : {exception.Message}"
            };

        return httpContext.Response.WriteAsync(new ExceptionModel
        {
            Errors = errors,
            StatusCode = statusCode
        }.ToString());

    }

    private static int GetStatusCode(Exception exception) =>
        exception switch
        {
            BadRequestException => StatusCodes.Status400BadRequest,
            NotFoundException => StatusCodes.Status400BadRequest,
            ValidationException => StatusCodes.Status422UnprocessableEntity,
            AuthorizationException => StatusCodes.Status401Unauthorized,
            _ => StatusCodes.Status500InternalServerError
        };
}

