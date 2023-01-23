using DillonRPG.Common.Configuration;

namespace DillonRPG.Common.SecurityConfiguration;

public static class GraphConstants
{
    public static readonly Uri GraphGroupsUri = new("https://graph.microsoft.com/v1.0/groups");
    public static readonly string ReadGraphScope = "https://graph.microsoft.com/.default";
}
