using Cards;
using DataBase;
using Microsoft.Extensions.Hosting;

namespace CardGame;

public class ColosseumExperimentWorker : BackgroundService
{
    private readonly ISandbox _colosseumSandbox;

    private static void ClearCurrentConsoleLine()
    {
        var currentLineCursor = Console.CursorTop;
        Console.SetCursorPosition(0, Console.CursorTop);
        Console.Write(new string(' ', Console.WindowWidth));
        Console.SetCursorPosition(0, currentLineCursor);
    }

    public ColosseumExperimentWorker(ISandbox colosseumSandbox)
    {
        _colosseumSandbox = colosseumSandbox;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var successCount = 0;
        Console.WriteLine("Start");
        Console.Write(0 + "%");

        for (var i = 1; i <= 1_000_000; i++)
        {
            _colosseumSandbox.Run();
            if (_colosseumSandbox.CardsColorsMatched)
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

public class ColosseumExperimentWorkerDB : BackgroundService
{
    private readonly ISandbox _colosseumSandbox;

    private static void ClearCurrentConsoleLine()
    {
        var currentLineCursor = Console.CursorTop;
        Console.SetCursorPosition(0, Console.CursorTop);
        Console.Write(new string(' ', Console.WindowWidth));
        Console.SetCursorPosition(0, currentLineCursor);
    }

    public ColosseumExperimentWorkerDB(ISandbox colosseumSandbox)
    {
        _colosseumSandbox = colosseumSandbox;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var db = new ColosseumContext();
        var experimentalConditions = db.experimental_conditions.ToList();

        var successCount = 0;
        Console.WriteLine("Start");
        Console.Write(0 + "%");

        var i = 0;
        foreach (var experimentalCondition in experimentalConditions)
        {
            var cardDeck = new CardDeck(experimentalCondition.cards_order);
            Console.WriteLine(cardDeck.ToString());
            _colosseumSandbox.Run(cardDeck);
            if (_colosseumSandbox.CardsColorsMatched)
            {
                successCount++;
            }

            ClearCurrentConsoleLine();
            Console.Write(100 * (float) successCount / i + "%");
            i++;
        }

        Console.WriteLine();
        Console.WriteLine("Finnish");
        return Task.CompletedTask;
    }
}