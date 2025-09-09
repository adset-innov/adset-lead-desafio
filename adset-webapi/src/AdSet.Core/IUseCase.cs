namespace AdSet.Core
{
    public interface IUseCase
    {
        Task Execute();
    }
    public interface IUseCaseWithRequest<TRequest>
    {
        Task Execute (TRequest request);
    }

    public interface IUseCaseWithResponse<TResponse>
    {
        Task<TResponse> Execute ();
    }

    public interface IUseCase<TRequest, TResponse>
    {
        Task<TResponse> Execute (TRequest request, TResponse response);
    }
}
