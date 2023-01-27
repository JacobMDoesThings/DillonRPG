
namespace DillonRPG.Service.Client;

public interface IAbilitiesServiceClient : IServiceClient
{
    [Get("/Abilities")]
    public Task<ApiResponse<IEnumerable<Ability>>> GetAbilities();

    [Post("/Abilities")]
    public Task<ApiResponse<Ability>> PostAbility(Ability ability);


    [Post("/Abilities")]
    public Task<ApiResponse<Ability>> DeleteAbility(Ability ability);
}