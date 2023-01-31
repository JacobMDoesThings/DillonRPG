
namespace DillonRPG.Service.Client;

public class DillonRPGServiceClient : IDillonRPGServiceClient
{
    readonly HttpClient _client;
    ITokenAcquisition _tokenAcquisition;
    DillonRPGService _service;

    public DillonRPGServiceClient(HttpClient client,
       ITokenAcquisition tokenAcquistion,
       DillonRPGService service)
    {
        _client = client;
        _tokenAcquisition = tokenAcquistion;
        _service = service;
        
        _client.BaseAddress = _service.BaseUrl;
    }

    public IAbilitiesServiceClient AbilitiesServiceClient => new AbilitiesServiceClient(_client, _tokenAcquisition, _service);

    public IClassesServiceClient ClassesServiceClient => new ClassesServiceClient(_client, _tokenAcquisition, _service);

    public IFamiliesServiceClient FamiliesServiceClient => new FamiliesServiceClient(_client, _tokenAcquisition, _service);
 
    public ITribeServiceClient TribesServiceClient => new TribeServiceClient(_client, _tokenAcquisition, _service);
}
