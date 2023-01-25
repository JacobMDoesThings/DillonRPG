namespace DillonRPG.Web.Data;

public class FamiliesServiceClient : ServiceClient, IFamiliesServiceClient
{

    public FamiliesServiceClient(ServiceClientCaller client,
    ITokenAcquisition tokenAcquistion,
    DillonRPGService service,
    ILogger<ServiceClient> logger)
    : base(client, tokenAcquistion, service, logger)
    {
    }
    public async Task<ApiResponse<IEnumerable<Family>>> GetFamilies()
    {
        return await _client.GetClient<IFamiliesServiceClient>(() =>
        {
            return _tokenAcquistion.GetAccessTokenForUserAsync(_service.Scope!).Result;
        }).GetFamilies();
    }
}
