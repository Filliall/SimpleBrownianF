using CommunityToolkit.Mvvm.ComponentModel;
using SimpleBrownianF.Services;

namespace SimpleBrownianF.ViewModels;

public partial class RootViewModel : ObservableObject, INavigationService
{
    [ObservableProperty]
    private ObservableObject? _currentViewModel;

    public RootViewModel()
    {
        // A tela inicial é a MainView
        CurrentViewModel = new MainViewModel(this);
    }

    // Lógica real de navegação que troca a tela
    public void NavigateToMainView()
    {
        CurrentViewModel = new MainViewModel(this);
    }

    public void NavigateToAboutView()
    {
        CurrentViewModel = new AboutViewModel(this);
    }
}
