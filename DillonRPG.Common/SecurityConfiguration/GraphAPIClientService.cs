namespace DillonRPG.Common.SecurityConfiguration;

public class GraphApiClientService
{
    private readonly GraphServiceClient _graphServiceClient;

    public GraphApiClientService(GraphApi graphApi)
    {
        string[]? scopes = graphApi.Scopes?.Split(' ');
        var tenantId = graphApi.TenantId;

        // Values from app registration
        var clientId = graphApi.ClientId;
        var clientSecret = graphApi.ClientSecret;

        var options = new TokenCredentialOptions
        {
            AuthorityHost = AzureAuthorityHosts.AzurePublicCloud
        };

        // https://docs.microsoft.com/dotnet/api/azure.identity.clientsecretcredential
        var clientSecretCredential = new ClientSecretCredential(
            tenantId, clientId, clientSecret, options);

        _graphServiceClient = new GraphServiceClient(clientSecretCredential, scopes);
    }

    public async Task<IDirectoryObjectGetMemberGroupsCollectionPage> GetGraphApiUserMemberGroups(string userId)
    {
        var securityEnabledOnly = true;

        return await _graphServiceClient.Users[userId]
            .GetMemberGroups(securityEnabledOnly)
            .Request().PostAsync()
            .ConfigureAwait(false);
    }
}
