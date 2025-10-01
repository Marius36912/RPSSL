using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;

namespace RPSSL;

// App klassen styrer hele programmet, med at starte GUI og binder XAML og C# kode sammen
public partial class App : Application
{
    public override void Initialize()
    {
        // Her Loades GUI definitionerne fra XAML filerne
        // Som angivet i opgaven har jeg seperareret med logik C# og præsentation i XAML
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        // Tjekker om programmet kører som et desktop program med et vindue
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // Her sætter vi startvinduet = MainWindow som er vores GUI
            desktop.MainWindow = new MainWindow();
        }

        // denne kommando kører en metode så Avalonia framework kan gøre det sidste færdigt
        base.OnFrameworkInitializationCompleted();
    }
}