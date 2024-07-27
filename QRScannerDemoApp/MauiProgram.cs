using Microsoft.Extensions.Logging;
using Prism.Mvvm;
using Prism;
using QRScannerDemoApp.ViewModels;
using QRScannerDemoApp.Views;
using ZXing.Net.Maui.Controls;
using Microsoft.Extensions.DependencyInjection;
using Plugin.Maui.Audio;
using SkiaSharp.Views.Maui.Controls.Hosting;

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
                    container.RegisterForNavigation<ScannerPage, ScannerPageViewModel>();


                }).OnAppStart(async navigationService =>
                {
                    await navigationService.NavigateAsync(nameof(ScannerPage));
                });


            }).UseBarcodeReader().UseSkiaSharp();

        builder.Services.AddSingleton(AudioManager.Current);
#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}

