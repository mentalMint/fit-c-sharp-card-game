using Cards;
using Microsoft.Extensions.Hosting;

namespace CardGame;

public class CollisiumExperimentWorker : BackgroundService
{
    private readonly ISandbox _collisiumSandbox;

    private static void ClearCurrentConsoleLine()
    {
        var currentLineCursor = Console.CursorTop;
        Console.SetCursorPosition(0, Console.CursorTop);
        Console.Write(new string(' ', Console.WindowWidth));
        Console.SetCursorPosition(0, currentLineCursor);
    }

    public CollisiumExperimentWorker(ISandbox collisiumSandbox, ICardDeck cardDeck, IEnumerable<Player> players)
    {
        _collisiumSandbox = collisiumSandbox;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var successCount = 0;
        const int cardsCount = 36;
        Console.WriteLine("Start");
        Console.Write(0 + "%");

        for (var i = 1; i <= 1_000_000; i++)
        {
            _collisiumSandbox.Run();
            if (_collisiumSandbox.CardsColorsMatched)
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
        return Task.CompletedTask;
    }
}
