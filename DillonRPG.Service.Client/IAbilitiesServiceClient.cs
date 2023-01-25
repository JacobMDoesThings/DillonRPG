
namespace DillonRPG.Service.Client;

public interface IAbilitiesServiceClient : IServiceClient
{
    [Get("/Abilities")]
    public Task<ApiResponse<IEnumerable<Ability>>> GetAbilities();
}
