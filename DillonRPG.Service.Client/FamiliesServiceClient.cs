
namespace DillonRPG.Service.Client;

public class FamiliesServiceClient : ServiceClient<IFamiliesServiceClient>, IFamiliesServiceClient
{

    public FamiliesServiceClient(HttpClient client,
    ITokenAcquisition tokenAcquistion,
    DillonRPGService service)
    : base(tokenAcquistion, service, client)
    {
    }

    public async Task<ApiResponse<Family>> DeleteFamily(string id)
    {
        return await _serviceClient!.DeleteFamily(id).ConfigureAwait(false);
    }

    public async Task<ApiResponse<IEnumerable<Family>>> GetFamilies()
    {
       return await _serviceClient!.GetFamilies().ConfigureAwait(false);
    }

    public async Task<ApiResponse<Family>> PostFamily(Family family)
    {
        return await _serviceClient!.PostFamily(family).ConfigureAwait(false);
    }

    public async Task<ApiResponse<Family>> PatchFamily(Family family)
    {
        return await _serviceClient!.PatchFamily(family).ConfigureAwait(false);
    }

}
