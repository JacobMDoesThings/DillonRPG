
namespace DillonRPG.Service.Client;

public class FamiliesServiceClient : ServiceClient<IFamiliesServiceClient>, IFamiliesServiceClient
{

    public FamiliesServiceClient(HttpClient client,
    ITokenAcquisition tokenAcquistion,
    DillonRPGService service)
    : base(tokenAcquistion, service, client)
    {
    }
    
    public async Task<ApiResponse<IEnumerable<Family>>> GetFamilies()
    {
       return await _serviceClient!.GetFamilies();
    }

    public async Task<ApiResponse<Family>> PostFamily(Family family)
    {
        return await _serviceClient!.PostFamily(family);
    }
}
