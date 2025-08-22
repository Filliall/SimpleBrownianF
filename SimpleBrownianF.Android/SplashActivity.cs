using Android.App;
using Android.Content.PM;
using Android.Content; // Necessário para Intent
using Android.OS; // Necessário para Handler e Looper


namespace SimpleBrownianF.Android;

[Activity(
    Theme = "@style/MyTheme.Splash",
    MainLauncher = true,
    NoHistory = true,
    ScreenOrientation = ScreenOrientation.Landscape)] // Mantém a orientação da MainActivity
public class SplashActivity : Activity // Herda diretamente de Android.App.Activity
{
    protected override void OnCreate(Bundle savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

        // Inicia a MainActivity após um pequeno atraso (opcional, mas comum)
        new Handler(Looper.MainLooper).PostDelayed(() =>
        {
            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
            // Aplica a animação de transição: fade_in para a nova Activity, fade_out para a atual
            OverridePendingTransition(Resource.Animation.fade_in, Resource.Animation.fade_out);
            Finish(); // Finaliza a SplashActivity para que ela não fique na pilha de volta
        }, 500); // Atraso de 500 milissegundos
    }
}