
namespace DillonRPG.Service.Client;

public interface IClassesServiceClient : IServiceClient
{
    [Get("/Classes")]
    public Task<ApiResponse<IEnumerable<Class>>> GetClasses();
}
