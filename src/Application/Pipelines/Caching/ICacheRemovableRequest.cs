namespace Application.Pipelines.Caching;

public interface ICacheRemovableRequest
{
    string CacheKey { get; }
    bool RemoveAllCache { get; }
}
