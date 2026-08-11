using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Android.Graphics; // add this
using Microsoft.Maui.Platform;

namespace Kaseb
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Window?.SetStatusBarColor(Colors.Transparent.ToPlatform());
            Window?.SetNavigationBarColor(Colors.Transparent.ToPlatform());

            if (Build.VERSION.SdkInt >= BuildVersionCodes.M)
            {
                Window!.DecorView!.SystemUiVisibility =
                    (StatusBarVisibility)SystemUiFlags.LightStatusBar;
            }
        }
    }
}
