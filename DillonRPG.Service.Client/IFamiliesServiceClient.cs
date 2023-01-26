
namespace DillonRPG.Service.Client;

public interface IFamiliesServiceClient : IServiceClient
{
    [Get("/Families")]
    public Task<ApiResponse<IEnumerable<Family>>> GetFamilies();

    [Post("/Families")]
    public Task<ApiResponse<Family>> PostFamily(Family family);
}
