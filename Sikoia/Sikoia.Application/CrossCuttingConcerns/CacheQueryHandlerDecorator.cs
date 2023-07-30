using Microsoft.Extensions.Caching.Memory;
using Sikoia.Application.Queries;

namespace Sikoia.Application.CrossCuttingConcerns
{
    public sealed class CacheQueryHandlerDecorator<TQuery, TResult> : IAsyncQueryHandler<TQuery, TResult>
        where TQuery : IQuery<TResult>
        where TResult : class
    {
        private readonly IAsyncQueryHandler<TQuery, TResult> decorated;
        private readonly IMemoryCache memoryCache; // Very much just for demonstration purposes

        public CacheQueryHandlerDecorator(IAsyncQueryHandler<TQuery, TResult> decorated, IMemoryCache memoryCache)
        {
            this.decorated = decorated;
            this.memoryCache = memoryCache;
        }

        public async Task<TResult> HandleAsync(TQuery query)
        {
            if (memoryCache.TryGetValue(query, out var cacheHit))
            {
                if (cacheHit is TResult cachedResult)
                {
                    return cachedResult;
                }
            }

            var result = await decorated.HandleAsync(query);

            memoryCache.Set(query, result);

            return result;
        }
    }
}