namespace SimpleBrownianF.Services;

public interface INavigationService
{
    void NavigateToMainView();
    void NavigateToAboutView();
}

/// <summary>
/// Uma implementação "dummy" do serviço de navegação para satisfazer construtores sem parâmetros
/// exigidos por algumas ferramentas (como o designer XAML ou o compilador AOT).
/// </summary>
public class DummyNavigationService : INavigationService
{
    public void NavigateToAboutView() { /* Não faz nada */ }
    public void NavigateToMainView() { /* Não faz nada */ }
}
