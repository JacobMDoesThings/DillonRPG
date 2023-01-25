namespace DillonRPG.Service.Client;

public class ClassesServiceClient : ServiceClient, IClassesServiceClient
{
    public ClassesServiceClient(ServiceClientCaller client,
    ITokenAcquisition tokenAcquistion,
    DillonRPGService service,
    ILogger<ServiceClient> logger)
    : base(client, tokenAcquistion, service, logger)
    {
    }

    public async Task<ApiResponse<IEnumerable<Class>>> GetClasses()
    {
        return await _client.GetClient<IClassesServiceClient>(() =>
        {
            return _tokenAcquistion.GetAccessTokenForUserAsync(_service.Scope!).Result;
        }).GetClasses();
    }
}
