namespace DillonRPG.Common.SecurityConfiguration;

public static class SecurityConstants
{

    public static  SecurityGroup DillonGodMode = new()
    {
        SecurityGroupName = "DillionAppGodMode",
        SecurityGroupId = "5f47452f-da51-41ff-9dc2-b3439a75c037",
        SecurityGroupPolicyName = "GodModePolicy"
    };

    public static readonly SecurityGroup Test = new()
    {
        SecurityGroupName = "TestSG",
        SecurityGroupId = "25648d5e-0991-4555-ad27-4a7c42479aba",
        SecurityGroupPolicyName = "TestPolicy"
    };

    public static readonly Uri GraphGroupsUri = new("https://graph.microsoft.com/v1.0/groups");
    public static readonly string ReadGraphScope = "https://graph.microsoft.com/.default";
}
