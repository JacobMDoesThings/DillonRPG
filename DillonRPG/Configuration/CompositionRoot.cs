
namespace DillonRPG.Service.Configuration;

internal static class CompositionRoot
{
    internal static IServiceCollection ConfigureDependencies(this IServiceCollection services)
    {
        var settings = BindSettings();

        if (settings.CosmosRepositoryOptions is null)
        {
            throw new ArgumentNullException($"{nameof(CosmosRepositoryOptions)} is required");
        }
        services.ConfigureCaching();
        services.AddSingleton(settings.GraphApi!);
        services.AddScoped<GraphApiClientService>();
        services.AddTransient<IClaimsTransformation, GraphApiClaimsTransformation>();
        services.AddCosmosClient(settings.CosmosRepositoryOptions);
        services.AddQueryRepository<DillonRPGContext>();
        services.AddGenericRepository<DillonRPGContext>();
       

        return services;

    }



    internal static IServiceCollection ConfigureSecurity(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMicrosoftIdentityWebApiAuthentication(configuration, "AzureAdB2C")
        //.AddMicrosoftIdentityWebApi(configuration, "AzureAdB2C")
        .EnableTokenAcquisitionToCallDownstreamApi()
        .AddMicrosoftGraph(configuration.GetSection("GraphApi"))
        .AddDistributedTokenCaches();

        services.AddSingleton<IAuthorizationHandler, IsInGroupHandlerUsingAzureGroups>();

        services.AddAuthorization(options =>
        {
            options.AddPolicy(DillonGodMode.SecurityGroupPolicyName!, policy =>
            {
                policy.Requirements.Add(new MemberOfSecurityGroupRequirement(DillonGodMode.SecurityGroupName!, DillonGodMode.SecurityGroupId!));
            });

            options.AddPolicy(Test.SecurityGroupPolicyName!, policy =>
            {
                policy.Requirements.Add(new MemberOfSecurityGroupRequirement(Test.SecurityGroupName!, Test.SecurityGroupId!));
            });
        });

        return services;
    }

    internal static IServiceCollection ConfigureCaching(this IServiceCollection services)
    {
       services.AddSingleton(x => new MemoryCacheEntryOptions()
              .SetAbsoluteExpiration(TimeSpan.FromHours(2))
              .SetPriority(CacheItemPriority.Normal));
       return services.AddSingleton<IMemoryCache, MemoryCache>();      
    }

    private static AppSettings BindSettings()
    {
        IConfigurationRoot configuration = new ConfigurationBuilder().BuildConfiguration();

        AppSettings settings = new();
        settings.CosmosRepositoryOptions = configuration.GetSection("CosmosRepositoryOptions").Get<CosmosRepositoryOptions>();
        settings.BlobStorage = configuration.GetSection("BlobStorage").Get<BlobStorage>();
        settings.GraphApi = configuration.GetRequiredSection("GraphApi").Get<GraphApi>();
        return settings;
    }

    private static IConfigurationRoot BuildConfiguration(this IConfigurationBuilder configurationBuilder)
    {
        return configurationBuilder
            .AddJsonFile("appsettings.json", false, false)
            .AddUserSecrets<Program>(optional: true)
            .Build();
    }
}
