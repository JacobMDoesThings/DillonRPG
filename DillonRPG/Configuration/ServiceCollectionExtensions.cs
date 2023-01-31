namespace DillonRPG.Service.Configuration;
public static class ServiceCollectionExtensions
{

    /// <summary>
    /// Configure the CosmosClient.
    /// </summary>
    /// <param name="service">Service Collection</param>
    /// <param name="options">RepositoryOptions.</param>
    /// <returns><see cref="IServiceCollection"/>with CosmosClient configuration.</returns>
    public static IServiceCollection AddCosmosClient(this IServiceCollection service, CosmosRepositoryOptions options)
    {
        if (string.IsNullOrEmpty(options.EndPoint) ||
            string.IsNullOrEmpty(options.AccountKey) ||
            string.IsNullOrEmpty(options.DatabaseName))
        {
            throw new InvalidOperationException($"{nameof(options.EndPoint)}, {nameof(options.AccountKey)}, " +
                $"{nameof(options.DatabaseName)} must be all not null and not empty.");
        }
        CosmosSerializationOptions cosmosSerializer = new();
        options.AllowBulkExecution = true;
        options.MaxRetryAttemptsOnRateLimitedRequests = 100;
        options.MaxRetryWaitTimeOnRateLimitedRequests = TimeSpan.FromSeconds(120);
        
        service.AddDbContext<DillonRPGContext>(o =>
        o.UseCosmos(options.EndPoint, options.AccountKey, options.DatabaseName));

        return service;

    }
}
