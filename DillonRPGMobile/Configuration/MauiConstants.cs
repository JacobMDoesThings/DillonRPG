namespace DillonRPG.Maui.Configuration;

public static class MauiConstants
{
    /// <summary>
    /// The base URI for the Datasync service.
    /// </summary>
    public static string ServiceUri = "https://localhost:7036";

    /// <summary>
    /// The application (client) ID for the native app within Azure Active Directory.
    /// </summary>
    public static string ApplicationId = "b7536cda-d04c-46d4-8259-2c280616d15b";

    /// <summary>
    /// The B2C instance.
    /// </summary>
    public static string Instance = "https://jacobmdoesthings.b2clogin.com";

    /// <summary>
    /// The domain.
    /// </summary>
    public static string Domain = "jacobmdoesthings.onmicrosoft.com";

    /// <summary>
    /// The policy Id.
    /// </summary>
    public static string SignUpSignInPolicyId = "b2c_1_jmdoesthingsdirectory";

    /// <summary>
    /// The Azure Tenant Id.
    /// </summary>
    public static string TenantId = "3007dc4a-2870-423e-8ce7-ad5e951446f6";

    /// <summary>
    /// The list of scopes to request.
    /// </summary>
    public static string[] Scopes = new[]
    {
          "https://jacobmdoesthings.onmicrosoft.com/700fdbe6-447c-4669-bce1-561b79d987af/access_as_user"
    };

    /// <summary>
    /// The B2C Authority. 
    /// </summary>
    public static string B2CAuthority = $"{Instance}/tfp/{TenantId}/{SignUpSignInPolicyId}";

    /// <summary>
    /// The redirect URI for Android and IOS.
    /// </summary>
    public static string AndroidIOSRedirectURI => $"msal{ApplicationId}://auth";

    /// <summary>
    /// Common RedirectURI.
    /// </summary>
    public static string CommonRedirectURI = "https://login.microsoftonline.com/common/oauth2/nativeclient";

    
}