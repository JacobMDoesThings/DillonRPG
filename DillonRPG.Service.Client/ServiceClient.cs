
namespace DillonRPG.Service.Client;

public abstract class ServiceClient
{
    private protected ServiceClientCaller _client;
    private protected ITokenAcquisition _tokenAcquistion;
    private protected ILogger<ServiceClient> _logger;
    private protected DillonRPGService _service;
    public ServiceClient(ServiceClientCaller client, 
        ITokenAcquisition tokenAcquistion, 
        DillonRPGService service, 
        ILogger<ServiceClient> logger)
    {
        if (client is null)
        {
            throw new ArgumentNullException(nameof(client));
        }
        if (tokenAcquistion is null)
        {
            throw new ArgumentNullException(nameof(tokenAcquistion));
        }

        _client = client;
        _tokenAcquistion = tokenAcquistion;
        _service = service;
        _logger = logger;
    }
}
