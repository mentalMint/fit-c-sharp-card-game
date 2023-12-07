using Cards;
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

        for (var i = 1; i <= 100; i++)
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
