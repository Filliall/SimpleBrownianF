using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using SimpleBrownianF.ViewModels;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using SimpleBrownianF.Views;

namespace SimpleBrownianF;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
        
        LiveCharts.Configure(config =>
            config
                .AddSkiaSharp()
                .AddDefaultMappers()
                .AddDarkTheme());
    }

     public override void OnFrameworkInitializationCompleted()
    {
        var rootViewModel = new RootViewModel();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = rootViewModel
            };
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleView)
        {
            singleView.MainView = new RootView
            {
                DataContext = rootViewModel
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}