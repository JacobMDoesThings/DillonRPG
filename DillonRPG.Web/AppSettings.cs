
namespace DillonRPG.Web;

internal class AppSettings
{
    public AzureAdB2C? AzureAdB2C { get; set; }
    public DillonRPGService? DillonRPGService { get; set; } 
    public GraphApi? GraphApi { get; set; }
    public SecurityGroups? SecurityGroups { get; set; }
}
