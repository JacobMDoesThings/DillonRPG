
namespace DillonRPG.Web.Data;

public class WeatherServiceClient : IWeatherServiceClient
{
    private ServiceClientCaller _client;
    private ITokenAcquisition _tokenAcquistion;
    private ILogger<WeatherServiceClient> _logger;
    private DillonRPGService _service;
    public WeatherServiceClient(ServiceClientCaller client, ITokenAcquisition tokenAcquistion, DillonRPGService service, ILogger<WeatherServiceClient> logger)
    {
        if (client is null)
        {
            throw new ArgumentNullException(nameof(client));
        }
        if (tokenAcquistion is null)
        {
            throw new ArgumentNullException(nameof(_tokenAcquistion));
        }

        _client = client;
        _tokenAcquistion = tokenAcquistion;
        _service = service;
        _logger = logger;
    }

    public async Task<ApiResponse<IEnumerable<Common.DTOs.WeatherForecast>>> GetWeatherForecast()
    {
        return await _client.GetClient<IWeatherServiceClient>(() => 
        {
            return _tokenAcquistion.GetAccessTokenForUserAsync(_service.Scope!).Result;
        }).GetWeatherForecast();
    }
}
