namespace TransactionService.AccountServiceClient;

public interface IApiClient
{
    Task<TResponse> Get<TResponse>(string url);
    Task Put<TRequest>(string url, TRequest request);
}
