
namespace DillonRPG.Service.Client;

public interface ITribeServiceClient : IServiceClient
{
    [Get("/Tribes")]
    public Task<ApiResponse<IEnumerable<Tribe>>> GetTribes();

    [Post("/Tribes")]
    public Task<ApiResponse<Tribe>> PostTribe(Tribe tribe);

    [Delete("/Tribes")]
    public Task<ApiResponse<Tribe>> DeleteTribe(Tribe tribe);

    [Patch("/Tribes")]
    public Task<ApiResponse<Tribe>> PatchTribe(Tribe tribe);
}
