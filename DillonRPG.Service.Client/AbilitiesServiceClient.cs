namespace DillonRPG.Service.Client;

public class AbilitiesServiceClient : ServiceClient<IAbilitiesServiceClient>, IAbilitiesServiceClient
{
    public AbilitiesServiceClient(HttpClient client, 
        ITokenAcquisition tokenAcquistion, 
        DillonRPGService service)
        : base(tokenAcquistion, service, client)
    {

    }

    public async Task<ApiResponse<IEnumerable<Ability>>> GetAbilities()
    {
        return await _serviceClient!.GetAbilities();
    }

    public async Task<ApiResponse<Ability>> PostAbility(Ability ability)
    {
        return await _serviceClient!.PostAbility(ability);
    }

    public async Task<ApiResponse<Ability>> DeleteAbility(Ability ability)
    {
        return await _serviceClient!.DeleteAbility(ability);
    }
}
