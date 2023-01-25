namespace DillonRPG.Service.Client;

public class AbilitiesServiceClient : ServiceClient, IAbilitiesServiceClient
{
    public AbilitiesServiceClient(ServiceClientCaller client, 
        ITokenAcquisition tokenAcquistion, 
        DillonRPGService service, 
        ILogger<ServiceClient> logger) 
        : base(client, tokenAcquistion, service, logger)
    {
    }

    public async Task<ApiResponse<IEnumerable<Ability>>> GetAbilities()
    {
        return await _client.GetClient<IAbilitiesServiceClient>(() =>
        {
            return _tokenAcquistion.GetAccessTokenForUserAsync(_service.Scope!).Result;
        }).GetAbilities();
    }
}
