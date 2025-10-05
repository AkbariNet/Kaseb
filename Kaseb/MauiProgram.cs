using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using UraniumUI;

namespace Kaseb
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseUraniumUI()
                .UseUraniumUIMaterial()
                // Initialize the .NET MAUI Community Toolkit by adding the below line of code
                .UseMauiCommunityToolkit()
                // After initializing the .NET MAUI Community Toolkit, optionally add additional fonts
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("7Awesome-Free-Solid-900.otf", "7Awesome");
                    fonts.AddFont("7Awesome-Free-Regular-400.otf", "R7Awesome");
                    fonts.AddFont("7Awesome-Brand-Regular-400.otf", "BR7Awesome");
                    fonts.AddFont("7Awesome-Brand-Regular-400.otf", "BR7Awesome");
                    fonts.AddFont("Vazirmatn-Thin.ttf", "VazirmatnThin");
                    fonts.AddFont("Vazirmatn-ExtraLight.ttf", "VazirmatnExtraLight");
                    fonts.AddFont("Vazirmatn-Light.ttf", "VazirmatnLight");
                    fonts.AddFont("Vazirmatn-Regular.ttf", "VazirmatnRegular");
                    fonts.AddFont("Vazirmatn-Medium.ttf", "VazirmatnMedium");
                    fonts.AddFont("Vazirmatn-SemiBold.ttf", "VazirmatnSemiBold");
                    fonts.AddFont("Vazirmatn-Bold.ttf", "VazirmatnBold");
                    fonts.AddFont("Vazirmatn-ExtraBold.ttf", "VazirmatnExtraBold");
                    fonts.AddFont("Vazirmatn-Black.ttf", "VazirmatnBlack");
                    fonts.AddFont("Vazirmatn.ttf", "Vazirmatn");
                })
                .UseMauiMaps();

            // Continue initializing your .NET MAUI App here


            #if DEBUG
                        builder.Logging.AddDebug();
            #endif
            

            return builder.Build();
        }
    }
}
