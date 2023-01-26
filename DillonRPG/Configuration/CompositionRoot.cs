
namespace DillonRPG.Service.Configuration;

internal static class CompositionRoot
{
    internal static IServiceCollection ConfigureDependencies(this IServiceCollection services, ConfigurationManager configuration)
    {
        var settings = configuration.BindSettings();

        if (settings.CosmosRepositoryOptions is null)
        {
            throw new ArgumentNullException($"{nameof(CosmosRepositoryOptions)} is required");
        }
        services.ConfigureCaching();
        services.AddSingleton(settings.GraphApi!);
        services.AddScoped<GraphApiClientService>();
        services.AddTransient<IClaimsTransformation, GraphApiClaimsTransformation>();
        services.AddCosmosClient(settings.CosmosRepositoryOptions);
        services.AddDbContext<DillonRPGContext>();
        return services;
    }



    internal static IServiceCollection ConfigureSecurity(this IServiceCollection services, ConfigurationManager configuration)
    {
        var settings = configuration.BindSettings();
        services.AddMicrosoftIdentityWebApiAuthentication(configuration, "AzureAdB2C")
        .EnableTokenAcquisitionToCallDownstreamApi()
        .AddMicrosoftGraph(configuration.GetSection(nameof(settings.GraphApi)))
        .AddDistributedTokenCaches();

        services.AddSingleton<IAuthorizationHandler, IsInGroupHandlerUsingAzureGroups>();
        services.AddAuthorization(options =>
        {
            options.AddPolicy(settings.SecurityGroups!.DillonGodMode!.SecurityGroupPolicyName!, policy =>
            {
                policy.Requirements.Add(new MemberOfSecurityGroupRequirement(settings.SecurityGroups!.DillonGodMode!.SecurityGroupName!,
                    settings.SecurityGroups!.DillonGodMode!.SecurityGroupId!));
            });

            options.AddPolicy(settings.SecurityGroups!.Test!.SecurityGroupPolicyName!, policy =>
            {
                policy.Requirements.Add(new MemberOfSecurityGroupRequirement(settings.SecurityGroups!.Test.SecurityGroupName!,
                    settings.SecurityGroups!.Test.SecurityGroupId!));
            });

            options.DefaultPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
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

    internal static AppSettings BindSettings(this IConfigurationRoot configurationRoot)
    {
        AppSettings settings = new()
        {
            CosmosRepositoryOptions = configurationRoot.GetSection("CosmosRepositoryOptions").Get<CosmosRepositoryOptions>(),
            BlobStorage = configurationRoot.GetSection("BlobStorage").Get<BlobStorage>(),
            GraphApi = configurationRoot.GetRequiredSection("GraphApi").Get<GraphApi>(),
            SecurityGroups = configurationRoot.GetSection("SecurityGroups").Get<SecurityGroups>()
        };

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
