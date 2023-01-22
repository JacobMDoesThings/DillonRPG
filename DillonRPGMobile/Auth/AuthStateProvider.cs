
namespace DillonRPG.Maui.Auth;

internal class AuthStateProvider : AuthenticationStateProvider, IAccountManager
{
    internal AuthenticationToken Token { get; private set; } = new();

    internal IPublicClientApplication IdentityClient { get; private set; }

    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var identity = new ClaimsIdentity();
        try
        {
            if (!string.IsNullOrEmpty(Token.Token))
            {
                var claims = new[] { new Claim(ClaimTypes.Name, Token.DisplayName) };
                identity = new ClaimsIdentity(claims, "Server authentication");
            }
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine("Request failed:" + ex.ToString());
        }

        return Task.FromResult(new AuthenticationState(new ClaimsPrincipal(identity)));
    }

    public async Task Login(bool forceSilentLogin = false)
    {
        Token = await GetAuthenticationToken(forceSilentLogin);
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public async Task Logout()
    {
        Token = new();
        var account = (await IdentityClient.GetAccountsAsync()).FirstOrDefault();
        if (account is not null)
        {
            await IdentityClient.RemoveAsync(account);
        }

        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    internal async Task<AuthenticationToken> GetAuthenticationToken(bool forceSilentLogin = false)
    {
        if (IdentityClient == null)
        {
#if ANDROID
            IdentityClient = PublicClientApplicationBuilder
                .Create(MauiConstants.ApplicationId)
                .WithB2CAuthority(MauiConstants.B2CAuthority)
                .WithRedirectUri(MauiConstants.AndroidIOSRedirectURI)
                .WithParentActivityOrWindow(() => Platform.CurrentActivity)
                .Build();
#elif IOS
    _identityClient = PublicClientApplicationBuilder
        .Create(Constants.ApplicationId)
        .WithAuthority(AzureCloudInstance.AzurePublic, "common")
        .WithIosKeychainSecurityGroup("com.microsoft.adalcache")
        .WithRedirectUri(Constants.AndroidIOSRedirectURI)
        .Build();
#else
            IdentityClient = PublicClientApplicationBuilder
                .Create(MauiConstants.ApplicationId)
                .WithAuthority(AzureCloudInstance.AzurePublic, "common")
                .WithRedirectUri(MauiConstants.CommonRedirectURI)
                .Build();
#endif
        }
        var accounts = await IdentityClient.GetAccountsAsync();
        AuthenticationResult result = null;
        bool tryInteractiveLogin = false;

        try
        {
            result = await IdentityClient
                .AcquireTokenSilent(MauiConstants.Scopes, accounts.FirstOrDefault())
                .ExecuteAsync();
        }
        catch (MsalUiRequiredException)
        {
            if (!forceSilentLogin)
            {
                tryInteractiveLogin = true;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"MSAL Silent Error: {ex.Message}");
        }

        if (tryInteractiveLogin)
        {
            try
            {
                result = await IdentityClient
                    .AcquireTokenInteractive(MauiConstants.Scopes)
                    .ExecuteAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"MSAL Interactive Error: {ex.Message}");
            }
        }

        return new AuthenticationToken
        {
            DisplayName = result?.Account?.Username ?? "",
            ExpiresOn = result?.ExpiresOn ?? DateTimeOffset.MinValue,
            Token = result?.AccessToken ?? "",
            UserId = result?.Account?.Username ?? ""
        };
    }


}
