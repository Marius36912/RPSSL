// =======================================================
// READ ME: RPSSL
// Aflevering for uge 5 industriel Programmering
//
// Regler for RPSSL:
// Rock slår Scissors (knuser) og Lizard (knuser).
// Paper slår Rock (dækker) og Spock (modbeviser).
// Scissors slår Paper (klipper) og Lizard (halshugger).
// Lizard slår Spock (forgifter) og Paper (æder).
// Spock slår Scissors (smadrer) og Rock (fordamper).
// Hvis begge vælger samme symbol = Tie.
// Første spiller til 5 point vinder spillet.
//
//Programmet:
// Programmet kører spillet rock, paper, scissors, lizard, spock i C#
// Brugeren vælger et symbol via knapper i en GUI.
// Agenten vælger et tilfældigt symbol random
// Programmet afgør udfaldet vha. enum, switch og en difference regel (p2 - p1) brugt fra pensum.
// Første spiller til WinningScore (5) vinder spillet, så vi har en slutning på spillet og en afgørelse.
//
// Opbygning:
// GUI bygget med avalonia XAML og C# code behind
// Separation af logik C sharp og præsentation med XAML
// Jeg har brugt event driven programmering, hvor hver knap i gui kalder en event handler
// Brug af enum Shape og enum outcome til at give faste navne på valg og resultater.
// Brug af Random.Shared.Next til at vælge agentens træk.
// Programmet viser valg, årsagstekst og score.
//
// Brug af AI
// Denne aflevering er udviklet med ChatGPT (OpenAI, 2025)
// Chatgpt genererede et skelet for GUI og RPSSL-logikken, jeg anvendte derudover activities i uge 5 for strukturen af opgaven.
// Jeg har så tilpasset namespaces, rettede fejl (fx Icon vs ShapeIcon), og omsat koden og funktioner til at følge opgavens formål.
// Derudover har jeg også oversat til mit eget sprog, og sikret at jeg bruger pensum ved at skrive noter og kommentar til koden ind i programmet.
// Den endelige aflevering, tilpasninger og forståelse er mit eget.
//
// Med afleveringen har jeg opnået forståelse af enum, switch, random, events og OOP.
//
// Flowchart vedlagt i github
// =======================================================
