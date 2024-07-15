using Infrastructure.RedisCache;
using MediatR;
using SendGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Pipelines.Caching;
public class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> 
    where TRequest : IRequest<TResponse>, ICacheableRequest
{
    private readonly ICacheService _cacheService;

    public CachingBehavior(ICacheService cacheService)
    {
        _cacheService = cacheService;
    }
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
            var cacheKey = request.CacheKey;
            var cacheTime = request.CacheTime;

            var cachedData = await _cacheService.GetAsync<TResponse>(cacheKey);
            if (cachedData is not null)
                return cachedData;

            var response = await next();
            if (response is not null)
                await _cacheService.SetAsync(cacheKey, response, DateTime.UtcNow.AddMinutes(cacheTime));

            return response;
    }
}
