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
        CosmosSerializationOptions cosmosSerializer = new CosmosSerializationOptions();
        options.AllowBulkExecution = true;
        options.MaxRetryAttemptsOnRateLimitedRequests = 100;
        options.MaxRetryWaitTimeOnRateLimitedRequests = TimeSpan.FromSeconds(120);
        
        service.AddDbContext<DillonRPGContext>(o =>
        o.UseCosmos(options.EndPoint, options.AccountKey, options.DatabaseName));

        return service;

    }
}
