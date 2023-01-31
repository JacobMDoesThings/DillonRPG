
namespace DillonRPG.Service.Client;

public interface IDillonRPGServiceClient
{
    IAbilitiesServiceClient AbilitiesServiceClient { get; }
    IClassesServiceClient ClassesServiceClient { get; }
    IFamiliesServiceClient FamiliesServiceClient { get; }
    ITribeServiceClient TribesServiceClient { get; }
}
