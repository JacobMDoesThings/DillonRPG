
using System.Security.Claims;

namespace DillonRPG.Common.SecurityConfiguration;

public class GraphApiClaimsTransformation : IClaimsTransformation
{
    private GraphApiClientService _graphApiClientService;
    private ILogger<GraphApiClaimsTransformation> _logger;

    public GraphApiClaimsTransformation(GraphApiClientService graphApiClientService, ILogger<GraphApiClaimsTransformation> logger)
    {

        _graphApiClientService = graphApiClientService;
        _logger = logger;
    }

    public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        ClaimsIdentity claimsIdentity = new ClaimsIdentity();

        var groupClaimType = "group";
        if (!principal.HasClaim(claim => claim.Type == groupClaimType))
        {
            var id = principal.Claims.Where(x => x.Type.Equals("sub", StringComparison.OrdinalIgnoreCase)).FirstOrDefault();

            id ??= principal.Claims.Where(x => x.Type.Equals(ClaimTypes.NameIdentifier, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();

            if (id is null)
            {
                return principal;
            }

            claimsIdentity.Label = id.Value;

            var groupIds = await _graphApiClientService.GetGraphApiUserMemberGroups(id.Value);

            foreach (var groupId in groupIds.ToList())
            {
                var claim = new Claim(groupClaimType, groupId);
                claimsIdentity.AddClaim(claim);
                _logger.LogInformation("Added claim {claim} to identity {claimsIdentity.Label}", claim, claimsIdentity.Label);
            }

            principal.AddIdentity(claimsIdentity);
        }
       
        return principal;
    }
}
