
namespace DillonRPG.Service.Client;

public class ServiceClientCaller
{
    private readonly ILogger<ServiceClientCaller> _logger;
    private readonly HttpClient _client;

    public ServiceClientCaller(HttpClient client, ILogger<ServiceClientCaller> logger)
    {
        _logger = logger;
        _client = client; 
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
    public IServiceClient GetClient<IServiceClient>(Func<string> getToken)
    {
        try
        {
            string token = getToken();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }
        return RestService.For<IServiceClient>(_client);
    }

}
