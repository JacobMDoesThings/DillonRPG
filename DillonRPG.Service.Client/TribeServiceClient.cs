
namespace DillonRPG.Service.Client;

public class TribeServiceClient : ServiceClient<ITribeServiceClient>, ITribeServiceClient
{
    public TribeServiceClient(HttpClient client,
    ITokenAcquisition tokenAcquistion,
    DillonRPGService service)
    : base(tokenAcquistion, service, client)
    {
    }

    public async Task<ApiResponse<IEnumerable<Tribe>>> GetTribes()
    {
        return await _serviceClient!.GetTribes().ConfigureAwait(false);
    }

    public async Task<ApiResponse<Tribe>> PostTribe(Tribe tribe)
    {
        return await _serviceClient!.PostTribe(tribe).ConfigureAwait(false);
    }
}
