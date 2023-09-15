using CardGame;
using Cards;
using Strategy;

void ClearCurrentConsoleLine()
{
    var currentLineCursor = Console.CursorTop;
    Console.SetCursorPosition(0, Console.CursorTop);
    Console.Write(new string(' ', Console.WindowWidth));
    Console.SetCursorPosition(0, currentLineCursor);
}

var successCount = 0;
const int cardsCount = 36;
var cardDeck = CardDeck.NewCardDeck(cardsCount);
Console.WriteLine("Start");
Console.Write(0 + "%");
for (var i = 1; i <= 1_000_000; i++)
{
    var model = new Sandbox(cardDeck,
        new Player(new FirstCardStrategy()), 
        new Player(new FirstCardStrategy()));
    model.Run();
    if (model.CardsColorsMatched)
    {
        successCount++;
    }

    if (i % 10000 == 0)
    {
        ClearCurrentConsoleLine();
        Console.Write(100 * (float)successCount / i + "%");
    }
}

Console.WriteLine();
Console.WriteLine("Finnish");