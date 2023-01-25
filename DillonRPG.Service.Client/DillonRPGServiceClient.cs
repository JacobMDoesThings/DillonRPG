
namespace DillonRPG.Service.Client;

public class DillonRPGServiceClient : IDillonRPGServiceClient
{
    ServiceClientCaller _client;
    ITokenAcquisition _tokenAcquisition;
    DillonRPGService _service;
    ILogger<ServiceClient> _logger;

    public DillonRPGServiceClient(ServiceClientCaller client,
       ITokenAcquisition tokenAcquistion,
       DillonRPGService service,
       ILogger<ServiceClient> logger)
    {
        _client = client;
        _tokenAcquisition = tokenAcquistion;
        _service = service;
        _logger = logger;
    }

    public IAbilitiesServiceClient AbilitiesServiceClient => new AbilitiesServiceClient(_client, _tokenAcquisition, _service, _logger);

    public IClassesServiceClient ClassesServiceClient => new ClassesServiceClient(_client, _tokenAcquisition, _service, _logger);

    public IFamiliesServiceClient FamiliesServiceClient => new FamiliesServiceClient(_client, _tokenAcquisition, _service, _logger);
}
