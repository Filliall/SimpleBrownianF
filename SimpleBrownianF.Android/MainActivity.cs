﻿using Android.App;
using Android.Content.PM;
using Avalonia;
using Avalonia.Android;

namespace SimpleBrownianF.Android;

[Activity(
    Label = "Brownian Simple App",
    Theme = "@style/MyTheme.NoActionBar",
    Icon = "@mipmap/ic_launcher",
    RoundIcon = "@mipmap/ic_launcher_round",
    MainLauncher = false,
    ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize | ConfigChanges.UiMode)]
public class MainActivity : AvaloniaMainActivity<App>
{
    protected override AppBuilder CustomizeAppBuilder(AppBuilder builder)
    {
        RequestedOrientation = ScreenOrientation.Landscape;
        return base.CustomizeAppBuilder(builder)
            .WithInterFont();
    }
}