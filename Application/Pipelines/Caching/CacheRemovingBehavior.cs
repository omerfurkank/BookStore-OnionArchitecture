using Infrastructure.RedisCache;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Pipelines.Caching;
public class CacheRemovingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>, ICacheRemovableRequest
{
    private readonly ICacheService _cacheService;

    public CacheRemovingBehavior(ICacheService cacheService, ILogger<CacheRemovingBehavior<TRequest, TResponse>> logger)
    {
        _cacheService = cacheService;
    }
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var response = await next();

        if (request.RemoveAllCache)
        {
            await _cacheService.RemoveAllAsync();
        }
        else if (!string.IsNullOrEmpty(request.CacheKey))
        {
            await _cacheService.RemoveAsync(request.CacheKey);
        }

        return response;
    }
}
