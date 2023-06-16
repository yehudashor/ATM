using Newtonsoft.Json;
using System.Net;
using TransactionService.Exceptions;

namespace TransactionService.AccountServiceClient;

public class ApiClient : IApiClient
{
    private readonly HttpClient _httpClient;

    private readonly Action<HttpStatusCode, string> _errorStatusCode;

    public ApiClient()
    {
        _httpClient = new HttpClient();

        // Set up the error status code handler
        _errorStatusCode = (statusCode, contentString) =>
        {
            if (statusCode is not HttpStatusCode.OK)
            {
                (statusCode switch
                {
                    HttpStatusCode.NotFound =>
                        (Action)(() => throw new NotFoundException(contentString)),

                    HttpStatusCode.BadRequest =>
                        () => throw new BadRequestException(contentString),

                    _ => throw new ApiException("An error occurred while processing the request.")
                })();
            }
        };
    }

    /// <summary>
    /// Sends a get request to the specified url and returns the deserialized response.
    /// </summary>
    /// <typeparam name="TResponse">The type of the response object.</typeparam>
    /// <param name="url">The url to send the get request to.</param>
    /// <returns>The deserialized response object.</returns>
    public async Task<TResponse> Get<TResponse>(string url)
    {
        var response = await _httpClient.GetAsync(url);
        var contentString = await response.Content.ReadAsStringAsync();

        _errorStatusCode(response.StatusCode, contentString);

        TResponse responseData = JsonConvert.DeserializeObject<TResponse>(contentString);

        return responseData;
    }

    /// <summary>
    /// Sends a put request to the specified url.
    /// </summary>
    /// <typeparam name="TRequest">The type of the request object.</typeparam>
    /// <param name="url">The url to send the put request to.</param>
    /// <param name="request">The request object to be serialized and sent.</param>
    public async Task Put<TRequest>(string url, TRequest request)
    {
        var response = await _httpClient.PutAsJsonAsync(url, request);
        var contentString = await response.Content.ReadAsStringAsync();
        _errorStatusCode(response.StatusCode, contentString);
    }
}

