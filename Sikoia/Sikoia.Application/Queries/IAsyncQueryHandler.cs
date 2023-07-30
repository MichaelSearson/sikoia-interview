namespace Sikoia.Application.Queries
{
    public interface IAsyncQueryHandler<TQuery, TResult>
        where TQuery : IQuery<TResult>
        where TResult : class
    {
        Task<TResult> HandleAsync(TQuery query);
    }
}