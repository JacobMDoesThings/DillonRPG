
namespace DillonRPG.Service.Client;

public interface IClassesServiceClient : IServiceClient
{

    [Get("/Classes")]
    public Task<ApiResponse<IEnumerable<Class>>> GetClasses();

    [Post("/Classes")]
    public Task<ApiResponse<Class>> PostClass(Class classEntity);
}
