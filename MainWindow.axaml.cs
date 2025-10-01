using System;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace RPSSL; // <- fixede error ved at at matche namespace i C#, fordi det skal matche XAML filen, ellers kan GUI ikke finde MainWindow-klassen

public partial class MainWindow : Window
{
    // ===== Typer =====
    // Enum bruges her til at give faste navne til spillets valg
    // Enum en måde at undgå magiske tal og gøre koden mere læsbar som angivet.
    enum Shape   { Rock = 0, Paper = 1, Scissors = 2, Lizard = 3, Spock = 4 }
    enum Outcome { Tie, PlayerWins, AgentWins }

    // ===== State =====
    // Vi holder styr på spillerens og agentens point i felter variabler i klassen.
    private int _playerScore = 0;
    private int _agentScore  = 0;
    private const int WinningScore = 5; // (const) er valgt fordi konstante værdier er gode når reglerne ikke ændres

    public MainWindow()
    {
        InitializeComponent(); // Starter GUI og binder XAML med C#
        UpdateUI("Pick a shape to start the game.", ""); // Viser starttekst i GUI
    }

    // ===== GUI events =====
    // Hver knap i GUI kalder et event funktion. Så her bruger jeg event-driven programmering.
    private void PickRock(object? s, RoutedEventArgs e)     => PlayRound(Shape.Rock);
    private void PickPaper(object? s, RoutedEventArgs e)    => PlayRound(Shape.Paper);
    private void PickScissors(object? s, RoutedEventArgs e) => PlayRound(Shape.Scissors);
    private void PickLizard(object? s, RoutedEventArgs e)   => PlayRound(Shape.Lizard);
    private void PickSpock(object? s, RoutedEventArgs e)    => PlayRound(Shape.Spock);

    // Reset knap som nulstiller spillet
    private void ResetGame(object? s, RoutedEventArgs e)
    {
        _playerScore = _agentScore = 0; // Nulstiller begge scores
        UpdateUI("Game reset. Pick a shape!", ""); // og her kommer så feedback til brugeren
    }

    // ===== En runde (c sharp) =====
    private void PlayRound(Shape player)
    {
        // Først hvis nogen allerede har vundet, stopper vi spillet her bruges en if.
        if (_playerScore >= WinningScore || _agentScore >= WinningScore)
        {
            UpdateUI("Game is over. Press Reset to play again.", "");
            return;
        }

        // Agenten vælger tilfældigt som angivet i opgaven
        var agent = RandomAgent();

        // Spillets logik afgøres i Resolve, vi får både resultat og en forklaring
        var (outcome, reason) = Resolve(player, agent);

        // Opdater point
        if (outcome == Outcome.PlayerWins) _playerScore++;
        else if (outcome == Outcome.AgentWins) _agentScore++;

        // Lav tekstlinje til output i GUI
        var roundLine =
            $"You: {ShapeIcon(player)}  vs  Agent: {ShapeIcon(agent)}  →  {OutcomeText(outcome)}";

        UpdateUI(roundLine, reason); // Kalder én funktion der styrer al GUI-output opdateres

        // Tjek for vinder, første til 5 point vinder
        if (_playerScore == WinningScore || _agentScore == WinningScore)
        {
            var winner = _playerScore == WinningScore ? "You win! 🏆" : "Agent wins 🤖";
            UpdateUI(roundLine + $"\n{winner}", reason);
        }
    }

    // ===== Resolution (switch på forskel p2 - p1) =====
    private (Outcome outcome, string reason) Resolve(Shape p1, Shape p2)
    {
        if (p1 == p2) return (Outcome.Tie, "Tie."); // Hvis begge vælger det samme = uafgjort

        // Udnytter at enum kan konverteres til int, og forskellen fortæller hvem der vinder
        int diff = (int)p2 - (int)p1;

        // Switchcase bruges her når vi vil matche et input mod mange muligheder
        switch (diff)
        {
            // p1 vinder
            case -4:
            case -2:
            case 1:
            case 3:
                return (Outcome.PlayerWins, ReasonFor(p1, p2));

            // p2 vinder
            case -3:
            case -1:
            case 2:
            case 4:
                return (Outcome.AgentWins, ReasonFor(p2, p1));

            default:
                return (Outcome.Tie, "Tie.");
        }
    }

    // ===== Årsagstekster =====
    // Her bruger vi pattern matching, med en switch expression til at forklare hvorfor et valg vinder over et andet. Her er det lavet manielt med årsagstekster.
    private static string ReasonFor(Shape a, Shape b) => (a, b) switch
    {
        (Shape.Rock,     Shape.Scissors) => "Rock crushes Scissors.",
        (Shape.Rock,     Shape.Lizard)   => "Rock crushes Lizard.",
        (Shape.Paper,    Shape.Rock)     => "Paper covers Rock.",
        (Shape.Paper,    Shape.Spock)    => "Paper disproves Spock.",
        (Shape.Scissors, Shape.Paper)    => "Scissors cuts Paper.",
        (Shape.Scissors, Shape.Lizard)   => "Scissors decapitates Lizard.",
        (Shape.Lizard,   Shape.Spock)    => "Lizard poisons Spock.",
        (Shape.Lizard,   Shape.Paper)    => "Lizard eats Paper.",
        (Shape.Spock,    Shape.Scissors) => "Spock smashes Scissors.",
        (Shape.Spock,    Shape.Rock)     => "Spock vaporizes Rock.",
        _ => ""
    };

    // ===== Agentens tilfældige valg =====
    // Random.Shared.Next(0,5) vælger et tilfældigt tal mellem 0-4 som så laver et cast til enum Shape
    private static Shape RandomAgent() => (Shape)Random.Shared.Next(0, 5);

    // ===== UI-opdatering samlet ét sted =====
    // Samler kode i funktioner så vi undgår gentagelser for lidt cleanup.
    private void UpdateUI(string roundLine, string reason)
    {
        RoundText.Text  = roundLine;
        ReasonText.Text = reason;
        ScoreText.Text  = $"Score = You😁: {_playerScore} | Agent🤖: {_agentScore} (to {WinningScore})";
    }

    // ===== Ikoner er omdøbt for ikke at skjule Window.Icon) =====
    // Her viser vi symboler med emoji ligesom en af activities for at gøre spillet mere visuelt
    private static string ShapeIcon(Shape s) => s switch
    {
        Shape.Rock     => "🪨 Rock",
        Shape.Paper    => "🗒️ Paper",
        Shape.Scissors => "✂️ Scissors",
        Shape.Lizard   => "🦎 Lizard",
        Shape.Spock    => "🖖 Spock",
        _ => s.ToString()
    };

    // Tekst til at vise udfald tie, player win og agent win
    private static string OutcomeText(Outcome o) => o switch
    {
        Outcome.Tie        => "Tie.",
        Outcome.PlayerWins => "You win!",
        Outcome.AgentWins  => "Agent wins.",
        _ => ""
    };
}
