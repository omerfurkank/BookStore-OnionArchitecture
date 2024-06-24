using Infrastructure.Serilog;
using MediatR;
using Microsoft.AspNetCore.Http;
using Serilog.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Pipelines.Logging;
public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>, ILoggableRequest
{
    private readonly LoggerServiceBase _logger;
    private readonly IHttpContextAccessor httpContextAccessor;

    public LoggingBehavior(LoggerServiceBase loggerServiceBase, IHttpContextAccessor httpContextAccessor)
    {
        _logger = loggerServiceBase;
        this.httpContextAccessor = httpContextAccessor;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        LogContext.PushProperty("method_name", $"{httpContextAccessor.HttpContext?.Request.RouteValues["controller"]}/ {httpContextAccessor.HttpContext?.Request.RouteValues["action"]}");
        LogContext.PushProperty("user_name", httpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value);
        LogContext.PushProperty("ip_adress", httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.MapToIPv4().ToString());

        _logger.Info($"Handling {typeof(TRequest).Name}");
        var response = await next();
        _logger.Info($"Handled {typeof(TResponse).Name}");

        return await next();
    }
}
