
namespace DillonRPG.Maui.Data.Services;

internal class ServiceClientHelper
{
	internal static async Task<string> GetToken(AuthStateProvider authStateProvider)
	{
		if (authStateProvider.Token.ExpiresOn.CompareTo(DateTime.UtcNow) <= 0)
		{
			await authStateProvider.Login();
		}
		return authStateProvider.Token.Token;
	}
}
