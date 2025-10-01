using Avalonia;

namespace RPSSL;

// Program.cs er altid startpunktet for et C#-program.
// Her starter vi vores GUI app (med Avalonia framework).
internal static class Program
{
    // Main er indgangen til programmet.
    // Herfra bliver Avalonia frameworket sat i gang.
    public static void Main(string[] args)
        // StartWithClassicDesktopLifetime betyder at appen kører som et normalt desktop vindue
        => BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);

    // Denne metode bygger konfigurationen for Avalonia-app'en.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()   // Binder til App-klassen (vores GUI-styring).
            .UsePlatformDetect()         // Gør at Avalonia selv finder ud af, hvilket OS vi kører på (Windows, Mac, Linux).
            .WithInterFont()             // Tilføjer en standard font (så tekst vises ens på tværs af platforme).
            .LogToTrace();               // Logger events (hjælper med at debugge, pensum: logning).
}