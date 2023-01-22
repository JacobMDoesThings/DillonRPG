namespace DillonRPG.Maui.Auth;

public interface IAccountManager
{
    Task Login(bool forceSilentLogin = false);
    Task Logout();
}
