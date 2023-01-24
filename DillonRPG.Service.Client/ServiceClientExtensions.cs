
namespace DillonRPG.Service.Client;

public static class ServiceClientExtensions
{
    public static void LogFailure(this ApiException apiEx, ILogger logger, string caller = "")
    {
        int statusCode = (int)apiEx.StatusCode;
        const string message = "Error while while calling {0} ResponseStatus: {1} Content: {2}";
        switch (statusCode)
        {
            case int n when (statusCode >= 300 && statusCode < 400):
                logger.LogInformation(message, caller, apiEx.Message, apiEx.Content);
                break;
            default:
                logger.LogError(message, caller, apiEx.Message, apiEx.Content);
                break;
        }
    }
}
