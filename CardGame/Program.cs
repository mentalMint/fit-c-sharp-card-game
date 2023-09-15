using CardGame.model;
using CardGameStrategy;

void ClearCurrentConsoleLine()
{
    var currentLineCursor = Console.CursorTop;
    Console.SetCursorPosition(0, Console.CursorTop);
    Console.Write(new string(' ', Console.WindowWidth));
    Console.SetCursorPosition(0, currentLineCursor);
}

var successCount = 0;
Console.WriteLine("Start");
Console.Write(0 + "%");
for (var i = 1; i <= 1_000_000; i++)
{
    const int cardsNumber = 36;
    var model = new Sandbox(CardDeck.NewCardDeck(cardsNumber), new FirstCardStrategy(), new FirstCardStrategy());
    // var consoleView = new ConsoleView(model);
    model.Run();
    if (model.CardsColorsMatched)
    {
        successCount++;
    }

    ClearCurrentConsoleLine();
    Console.Write(100 * (float)successCount / i + "%");
}

Console.WriteLine("Finnish");