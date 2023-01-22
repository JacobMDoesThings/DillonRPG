namespace DillonRPG.Common.SecurityConfiguration;
public class MemberOfSecurityGroupRequirement : IAuthorizationRequirement
{
    public readonly string GroupId;
    public readonly string GroupName;

    public MemberOfSecurityGroupRequirement(string groupName, string groupId)
    {
        GroupName = groupName;
        GroupId = groupId;
    }
}


