
namespace DillonRPG.Service.Client;

public interface IWeatherServiceClient : IServiceClient
{
    [Get("/WeatherForecast")]
    public Task<ApiResponse<IEnumerable<WeatherForecast>>> GetWeatherForecast();
}