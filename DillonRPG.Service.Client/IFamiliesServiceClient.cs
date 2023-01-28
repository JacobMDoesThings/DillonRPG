
namespace DillonRPG.Service.Client;

public interface IFamiliesServiceClient : IServiceClient
{
    [Get("/Families")]
    public Task<ApiResponse<IEnumerable<Family>>> GetFamilies();

    [Post("/Families")]
    public Task<ApiResponse<Family>> PostFamily(Family family);

    [Delete("/Families")]
    public Task<ApiResponse<Family>> DeleteFamily(string id);
}
