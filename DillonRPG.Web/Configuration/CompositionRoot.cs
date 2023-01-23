

namespace DillonRPG.Web.Configuration;

internal static class CompositionRoot
{
    /// <summary>
    /// Configures base services.
    /// </summary>
    /// <param name="services">The ServiceCollection.</param>
    /// <param name="configuration">The appsettings configuration.</param>
    /// <returns>Configured security services.</returns>
    internal static IServiceCollection ConfigureServices(this IServiceCollection services, ConfigurationManager configuration)
    {
        var settings = configuration.BindSettings();

        services.ConfigureCaching();
        services.AddSingleton(settings.GraphApi!);
        services.AddScoped<GraphApiClientService>();
        services.AddTransient<IClaimsTransformation, GraphApiClaimsTransformation>();
        services.AddSingleton(service => new DillonRPGService()
        {
            Scope = settings.DillonRPGService!.Scope,
            BaseUrl = settings.DillonRPGService.BaseUrl
        });

        services.AddHttpClient<ServiceClientCaller>(c => c.BaseAddress = settings.DillonRPGService!.BaseUrl);

        //services.AddScoped<HttpContextAccessor>();
        //services.AddRefitClient<IImagesClient>();
        //services.AddScoped(x => settings.SMDemoApi!);
        //services.AddScoped<ServiceClientCaller>();
        //services.AddScoped<ImagesClient>();
        return services;
    }

    /// <summary>
    /// Requires ConfigureServices and configures security for the application.
    /// </summary>
    /// <param name="services">The ServiceCollection.</param>
    /// <param name="configuration">The appsettings configuration.</param>
    /// <returns>Configured security services.</returns>
    internal static IServiceCollection ConfigureSecurity(this IServiceCollection services, ConfigurationManager configuration)
    {
        var settings = configuration.BindSettings();

        // This is required to be instantiated before the OpenIdConnectOptions starts getting configured.
        // By default, the claims mapping will map claim names in the old format to accommodate older SAML applications.
        // 'http://schemas.microsoft.com/ws/2008/06/identity/claims/role' instead of 'roles'
        // This flag ensures that the ClaimsIdentity claims collection will be built from the claims in the token.
        JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

        // Configuration to sign-in users with Azure AD B2C.
        services.AddMicrosoftIdentityWebAppAuthentication(configuration, nameof(settings.AzureAdB2C))
        .EnableTokenAcquisitionToCallDownstreamApi()
        .AddDownstreamWebApi(nameof(settings.DillonRPGService), configuration.GetSection(nameof(settings.DillonRPGService)))
        .AddMicrosoftGraph(configuration.GetSection(nameof(settings.GraphApi)))
        .AddDistributedTokenCaches();

        services.AddControllersWithViews(options =>
        {
            var policy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
            options.Filters.Add(new AuthorizeFilter(policy));
        }).AddMicrosoftIdentityUI();

        services.AddRazorPages();
        services.AddServerSideBlazor();

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

        services.AddSingleton(settings.SecurityGroups!);
        return services;
    }

    internal static IServiceCollection ConfigureCaching(this IServiceCollection services)
    {
        services.AddSingleton(x => new MemoryCacheEntryOptions()
               .SetAbsoluteExpiration(TimeSpan.FromHours(2))
               .SetPriority(CacheItemPriority.Normal));
        return services.AddSingleton<IMemoryCache, MemoryCache>();
    }

    /// <summary>
    /// Configure Apis, requires ConfigureSecurity.
    /// </summary>
    /// <param name="services">The Service Collection</param>
    /// <returns>Configured ApiServices</returns>
    internal static IServiceCollection ConfigureApis(this IServiceCollection services)
    {
        return services.AddScoped<IWeatherServiceClient, WeatherServiceClient>();
    }

    internal static AppSettings BindSettings(this IConfigurationRoot configurationRoot)
    {
        var check = configurationRoot.GetSection("SecurityGroups").Get<SecurityGroups>();

        AppSettings settings = new()
        {
            // AzureAdB2C = configuration.GetSection("AzureAdB2C").Get<AzureAdB2C>(),
            GraphApi = configurationRoot.GetRequiredSection("GraphApi").Get<GraphApi>(),
            DillonRPGService = configurationRoot.GetSection("DillonRPGService").Get<DillonRPGService>(),
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
