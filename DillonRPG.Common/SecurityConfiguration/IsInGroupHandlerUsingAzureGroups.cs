
namespace DillonRPG.Common.SecurityConfiguration;

public class IsInGroupHandlerUsingAzureGroups : AuthorizationHandler<MemberOfSecurityGroupRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MemberOfSecurityGroupRequirement requirement)
    {
        if (context is null)
            throw new ArgumentNullException(nameof(context));
        if (requirement is null)
            throw new ArgumentNullException(nameof(requirement));

        var claimIdentityprovider = context.User.Claims.FirstOrDefault(t => t.Type == "group"
            && t.Value == requirement.GroupId);

        if (claimIdentityprovider is not null)
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}
