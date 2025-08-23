using Avalonia;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SimpleBrownianF.Services;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace SimpleBrownianF.ViewModels;

public partial class AboutViewModel : ObservableObject
{
    private readonly INavigationService _navigationService;

    // URL do seu Buy Me a Coffee
    public string BuyMeACoffeeUrl { get; } = "https://www.buymeacoffee.com/fabiodutram"; // Substitua pelo seu link real!

    // Caminho para o QR code dentro dos Assets
    public string QrCodePath { get; } = "avares://SimpleBrownianF/Assets/bmc_qr.png";

    // Construtor sem parâmetros para o designer XAML e compilador AOT
    public AboutViewModel() : this(new DummyNavigationService())
    {
        // O compilador AOT precisa que isto exista.
    }

    public AboutViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    [RelayCommand]
    private void NavigateBack() => _navigationService.NavigateToMainView();

    [RelayCommand]
    private async Task OpenBuyMeACoffee(Visual? visual)
    {
        if (visual is null) return;

        try
        {
            var topLevel = TopLevel.GetTopLevel(visual);
            if (topLevel?.Launcher is { } launcher)
            {
                var uri = new Uri(BuyMeACoffeeUrl);
                await launcher.LaunchUriAsync(uri);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Erro ao abrir o link: {ex.Message}");
            // Opcional: Adicionar uma notificação ou caixa de diálogo para o usuário
        }
    }
}