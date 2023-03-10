using Android.App;
using Android.Content;

namespace DillonRPG.Maui.Platforms.Android;

[Activity(Exported = true)]
[IntentFilter(new[] { Intent.ActionView },
    Categories = new[] { Intent.CategoryBrowsable, Intent.CategoryDefault },
    DataHost = "auth",
    DataScheme = "msal{client-id}")]
public class MsalActivity : BrowserTabActivity
{
}

