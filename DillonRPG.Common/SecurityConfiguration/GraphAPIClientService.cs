
using System.Diagnostics;

namespace DillonRPG.Common.SecurityConfiguration;

public class GraphApiClientService
{
    private readonly GraphServiceClient _graphServiceClient;
    private IMemoryCache _memoryCache;
    private MemoryCacheEntryOptions _memoryCacheEntryOptions;
    private ILogger<GraphApiClientService> _logger;

    public GraphApiClientService(GraphApi graphApi, IMemoryCache memoryCache,
    MemoryCacheEntryOptions memoryCacheEntryOptions, ILogger<GraphApiClientService> logger)
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
        _memoryCache = memoryCache;
        _memoryCacheEntryOptions = memoryCacheEntryOptions;
        _logger = logger;
    }

    public async Task<IDirectoryObjectGetMemberGroupsCollectionPage> GetGraphApiUserMemberGroups(string userId)
    {
        string cacheKey = $"GraphApiUser:{userId}";
        var securityEnabledOnly = true;

        if (_memoryCache.TryGetValue(cacheKey, out IDirectoryObjectGetMemberGroupsCollectionPage groups))
        {
            _logger.LogInformation("MemberGroups for user found in cache.");
            if (groups is not null)
            {
                return groups;
            }
        }

        var result = await _graphServiceClient.Users[userId]
            .GetMemberGroups(securityEnabledOnly)
            .Request().PostAsync()
            .ConfigureAwait(false);

        var stopwatch = new Stopwatch();
        stopwatch.Start();
        _memoryCache.Set(cacheKey, result, _memoryCacheEntryOptions);
        stopwatch.Stop();
        _logger.LogInformation("Writing to catch took {stopwatch.ElapsedMilliseconds} ms", stopwatch.ElapsedMilliseconds);
        return result;
    }
}
