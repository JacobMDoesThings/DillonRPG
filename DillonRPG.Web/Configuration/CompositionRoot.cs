
namespace DillonRPG.Web.Configuration;

internal static class CompositionRoot
{
    /// <summary>
    /// Configures base services.
    /// </summary>
    /// <param name="services">The ServiceCollection.</param>
    /// <param name="configuration">The appsettings configuration.</param>
    /// <returns>Configured security services.</returns>
    public static IServiceCollection ConfigureServices(this IServiceCollection services, ConfigurationManager configuration)
    {
        AppSettings settings = BindSettings();

        services.AddScoped(o => new GraphApiClientService(settings.GraphApi!));
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
    public static IServiceCollection ConfigureSecurity(this IServiceCollection services, ConfigurationManager configuration)
    {

        // This is required to be instantiated before the OpenIdConnectOptions starts getting configured.
        // By default, the claims mapping will map claim names in the old format to accommodate older SAML applications.
        // 'http://schemas.microsoft.com/ws/2008/06/identity/claims/role' instead of 'roles'
        // This flag ensures that the ClaimsIdentity claims collection will be built from the claims in the token.
        JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

        // Configuration to sign-in users with Azure AD B2C.
        services.AddMicrosoftIdentityWebAppAuthentication(configuration, "AzureAdB2C")
        .EnableTokenAcquisitionToCallDownstreamApi()
        .AddDownstreamWebApi("DillonRPGService", configuration.GetSection("DillonRPGService"))
        .AddMicrosoftGraph(configuration.GetSection("GraphApi"))
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

    /// <summary>
    /// Configure Apis, requires ConfigureSecurity.
    /// </summary>
    /// <param name="services">The Service Collection</param>
    /// <returns>Configured ApiServices</returns>
    public static IServiceCollection ConfigureApis(this IServiceCollection services)
    {
        return services.AddScoped<IWeatherServiceClient, WeatherServiceClient>();
    }

    private static AppSettings BindSettings()
    {
        IConfigurationRoot configuration = new ConfigurationBuilder().BuildConfiguration();
        AppSettings settings = new()
        {
            // AzureAdB2C = configuration.GetSection("AzureAdB2C").Get<AzureAdB2C>(),
            GraphApi = configuration.GetRequiredSection("GraphApi").Get<GraphApi>(),
            DillonRPGService = configuration.GetSection("DillonRPGService").Get<DillonRPGService>()
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
