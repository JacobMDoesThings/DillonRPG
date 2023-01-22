namespace DillonRPG.Web.Configuration;

public class AzureAdB2C
{
    public string? ClientId { get;set;}
    public string? ClientSecret { get;set;}
    public string? Instance { get; set; }
    public string? SignUpSignInPolicyId { get; set; }
    public string? SignedOutCallbackPath { get; set; } 
    public string? CallbackPath { get; set; }
}
