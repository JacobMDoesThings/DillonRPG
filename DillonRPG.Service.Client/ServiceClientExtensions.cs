
namespace DillonRPG.Service.Client;

public static class ServiceClientExtensions
{
    public static void LogFailure(this ApiException apiEx, ILogger logger, string caller = "")
    {
        int statusCode = (int)apiEx.StatusCode;
        const string message = "Error while while calling {0} ResponseStatus: {1} Content: {2}";
        switch (statusCode)
        {
            case int when (statusCode >= 300 && statusCode < 400):
                logger.LogInformation(message, caller, apiEx.Message, apiEx.Content);
                break;
            default:
                logger.LogError(message, caller, apiEx.Message, apiEx.Content);
                break;
        }
    }

    public static IServiceCollection AddDillonRPGServiceClient(
    this IServiceCollection services,
    ServiceLifetime lifetime = ServiceLifetime.Scoped)
    {
        switch (lifetime)
        {
            case ServiceLifetime.Singleton:
                services.AddSingleton<IDillonRPGServiceClient, DillonRPGServiceClient>();
                break;

            case ServiceLifetime.Transient:
                services.AddTransient<IDillonRPGServiceClient, DillonRPGServiceClient>();
                break;

            case ServiceLifetime.Scoped:
            default:
                services.AddScoped<IDillonRPGServiceClient, DillonRPGServiceClient>();
                break;
        }

        return services;
    }
}
