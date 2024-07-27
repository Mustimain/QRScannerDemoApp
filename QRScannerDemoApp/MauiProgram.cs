using Microsoft.Extensions.Logging;
using Prism.Mvvm;
using Prism;
using QRScannerDemoApp.ViewModels;
using QRScannerDemoApp.Views;
using ZXing.Net.Maui.Controls;

namespace QRScannerDemoApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            }).UsePrism(prism =>
            {

                prism.RegisterTypes(container =>
                {
                    container.RegisterForNavigation<NavigationPage>();
                    container.RegisterForNavigation<MainPage, MainPageViewModel>();
                    container.RegisterForNavigation<ScannerPage, ScannerPageViewModel>();


                }).OnAppStart(async navigationService =>
                {
                    await navigationService.NavigateAsync(nameof(ScannerPage));
                });


            }).UseBarcodeReader();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}

