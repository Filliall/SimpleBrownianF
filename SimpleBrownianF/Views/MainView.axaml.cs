using Avalonia.Controls;
using Avalonia.Interactivity;
using System;
using System.Diagnostics;

namespace SimpleBrownianF.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();

        var buyMeACoffeeButton = this.FindControl<Button>("BuyMeACoffeeButton");
        if (buyMeACoffeeButton != null)
        {
            // Associa o evento de clique ao nosso método
            buyMeACoffeeButton.Click += OnBuyMeACoffeeClicked;
        }

    }

    private void OnBuyMeACoffeeClicked(object? sender, RoutedEventArgs e)
    {
        try
        {
            // !!! IMPORTANTE: Substitua "SEU_USUARIO" pelo seu nome de usuário real !!!
            const string url = "https://www.buymeacoffee.com/fabiodutram";

            // Este comando abre a URL no navegador padrão do sistema
            Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
        }
        catch (Exception ex)
        {
            // É uma boa prática registrar o erro caso algo dê errado
            Console.WriteLine($"Não foi possível abrir o link: {ex.Message}");
        }
    }
}