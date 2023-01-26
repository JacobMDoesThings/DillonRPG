using Microsoft.Extensions.Configuration.EnvironmentVariables;

namespace DillonRPG.Service.Client;

public class FamiliesServiceClient : ServiceClient<IFamiliesServiceClient>, IFamiliesServiceClient
{

    public FamiliesServiceClient(HttpClient client,
    ITokenAcquisition tokenAcquistion,
    DillonRPGService service)
    : base(tokenAcquistion, service, client)
    {
        _serviceClient = RestService.For<IFamiliesServiceClient>(
         GetClientWithAuthHeader<IFamiliesServiceClient>(() =>
         {
             return _tokenAcquistion.GetAccessTokenForUserAsync(_service.Scope!).Result;
         }));
    }
    public async Task<ApiResponse<IEnumerable<Family>>> GetFamilies()
    {
       return await _serviceClient!.GetFamilies();
    }
}
