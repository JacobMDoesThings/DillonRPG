
namespace DillonRPG.Service.Client;

public abstract class ServiceClient<T> : IDisposable
    where T : IServiceClient
{
    private protected HttpClient _client;
    private protected T? _serviceClient;
    private protected ITokenAcquisition _tokenAcquistion;
    private protected DillonRPGService _service;
    public ServiceClient(
        ITokenAcquisition tokenAcquistion,
        DillonRPGService service,
        HttpClient client)
    {
        if (client is null)
        {
            throw new ArgumentNullException(nameof(client));
        }
        if (tokenAcquistion is null)
        {
            throw new ArgumentNullException(nameof(tokenAcquistion));
        }

        _client = client;
        _tokenAcquistion = tokenAcquistion;
        _service = service;

        _serviceClient = RestService.For<T>(
          GetClientWithAuthHeader<T>(() =>
          {
              return _tokenAcquistion.GetAccessTokenForUserAsync(_service.Scope!).Result;
          }));
    }

    public void Dispose()
    {
        _client.Dispose();
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Sets the authentication header value from token gained from the parameter function "getToken".
    /// </summary>
    /// <typeparam name="IServiceClient">The ServiceClient.</typeparam>
    /// <param name="getToken">The function to obtain bearer token.</param>
    /// <returns>a RestService for <see cref="IServiceClient"/>.</returns>
    public HttpClient GetClientWithAuthHeader<IServiceClient>(Func<string> getToken)
    {
        string token = getToken();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        return _client;
    }
}
