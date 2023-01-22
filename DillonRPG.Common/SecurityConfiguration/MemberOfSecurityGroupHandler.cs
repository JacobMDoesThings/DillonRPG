
namespace DillonRPG.Common.SecurityConfiguration;

public class MemberOfSecurityGroupHandler : AuthorizationHandler<MemberOfSecurityGroupRequirement>
{
    private ITokenAcquisition _tokenAcquisition;
    private HttpClient _httpClient;

    public MemberOfSecurityGroupHandler(ITokenAcquisition tokenAcquisition)
    {
        _tokenAcquisition = tokenAcquisition ?? throw new ArgumentNullException(nameof(tokenAcquisition));

        _httpClient = new()
        {
            BaseAddress = GraphGroupsUri
        };
    }

    public async Task GetGroups()
    {
        string[] scopes = new string[] { "user.read" };
        string accessToken = await _tokenAcquisition.GetAccessTokenForUserAsync(scopes);
        _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue($"Bearer", $"{accessToken}");
    }

    protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context, MemberOfSecurityGroupRequirement requirement)
    {
        await GetGroups();
        var groupClaim = context.User.Claims
             .FirstOrDefault(claim => claim.Type == "groups" &&
                 claim.Value.Equals(requirement.GroupId, StringComparison.InvariantCultureIgnoreCase));

        if (groupClaim != null)
            context.Succeed(requirement);
    }
}
