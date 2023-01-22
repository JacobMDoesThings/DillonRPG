
namespace DillonRPG.Maui.Data.Services;
internal class WeatherServiceClient : IWeatherServiceClient, IServiceClient
{
    private ServiceClientCaller _client;
    private AuthStateProvider _authState;
    private ILogger<WeatherServiceClient> _logger;

    public WeatherServiceClient(ServiceClientCaller client, AuthStateProvider authState, ILogger<WeatherServiceClient> logger)
    {
        if (client is null)
        {
            throw new ArgumentNullException(nameof(client));
        }
        if (authState is null) 
        {
            throw new ArgumentNullException(nameof(authState));
        }

        _client = client;
        _authState = authState;
        _logger = logger;
    }

    public async Task<ApiResponse<IEnumerable<WeatherForecast>>> GetWeatherForecast()
    {
        return await _client.GetClient<IWeatherServiceClient>(()=> GetToken(_authState).Result).GetWeatherForecast();
    }
}
