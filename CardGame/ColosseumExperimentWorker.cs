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

public class ColosseumExperimentConditionWorker : BackgroundService
{
    private readonly ISandbox _colosseumSandbox;
    private readonly ColosseumContext _colosseumContext;

    private static void ClearCurrentConsoleLine()
    {
        var currentLineCursor = Console.CursorTop;
        Console.SetCursorPosition(0, Console.CursorTop);
        Console.Write(new string(' ', Console.WindowWidth));
        Console.SetCursorPosition(0, currentLineCursor);
    }

    public ColosseumExperimentConditionWorker(ISandbox colosseumSandbox, ColosseumContext colosseumContext)
    {
        _colosseumSandbox = colosseumSandbox;
        _colosseumContext = colosseumContext;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _colosseumContext.Database.EnsureCreated();
        foreach (var cardDeck in CardDeckGenerator.Generate(100))
        {
            _colosseumContext.Add(new ExperimentalCondition { CardsOrder = cardDeck.ToString() });
        }

        _colosseumContext.SaveChanges();
        return Task.CompletedTask;
    }
}

public class ColosseumExperimentWorkerDb : BackgroundService
{
    private readonly ISandbox _colosseumSandbox;
    private readonly ColosseumContext _colosseumContext;

    private static void ClearCurrentConsoleLine()
    {
        var currentLineCursor = Console.CursorTop;
        Console.SetCursorPosition(0, Console.CursorTop);
        Console.Write(new string(' ', Console.WindowWidth));
        Console.SetCursorPosition(0, currentLineCursor);
    }

    public ColosseumExperimentWorkerDb(ISandbox colosseumSandbox, ColosseumContext colosseumContext)
    {
        _colosseumSandbox = colosseumSandbox;
        _colosseumContext = colosseumContext;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _colosseumContext.Database.EnsureCreated();
        Console.WriteLine("Querying for an experimental condition");
        var experimentalConditions = _colosseumContext.ExperimentalConditions
            .OrderBy(b => b.CardsOrder);
        var successCount = 0;
        var i = 0;
        foreach (var experimentalCondition in experimentalConditions)
        {
            i++;    
            var cardDeck = new CardDeck(experimentalCondition.CardsOrder);
            _colosseumSandbox.Run(cardDeck);
            if (_colosseumSandbox.CardsColorsMatched)
            {
                successCount++;
            }
        }

        Console.Write(100 * (float)successCount / i + "%");
        Console.WriteLine();
        Console.WriteLine("Finnish");
        return Task.CompletedTask;
    }
}

public class ColosseumExperimentWorkerWeb : BackgroundService
{
    private readonly ISandbox _colosseumSandbox;
    private readonly ColosseumContext _colosseumContext;

    private static void ClearCurrentConsoleLine()
    {
        var currentLineCursor = Console.CursorTop;
        Console.SetCursorPosition(0, Console.CursorTop);
        Console.Write(new string(' ', Console.WindowWidth));
        Console.SetCursorPosition(0, currentLineCursor);
    }

    public ColosseumExperimentWorkerWeb(ISandbox colosseumSandbox, ColosseumContext colosseumContext)
    {
        _colosseumSandbox = colosseumSandbox;
        _colosseumContext = colosseumContext;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _colosseumContext.Database.EnsureCreated();
        Console.WriteLine("Querying for an experimental condition");
        var experimentalConditions = _colosseumContext.ExperimentalConditions
            .OrderBy(b => b.CardsOrder);
        var successCount = 0;
        var i = 0;
        foreach (var experimentalCondition in experimentalConditions)
        {
            i++;    
            var cardDeck = new CardDeck(experimentalCondition.CardsOrder);
            _colosseumSandbox.Run(cardDeck);
            if (_colosseumSandbox.CardsColorsMatched)
            {
                successCount++;
            }
            Console.WriteLine(100 * (float) successCount / i + "%");
        }

        Console.WriteLine(100 * (float)successCount / i + "%");
        Console.WriteLine();
        Console.WriteLine("Finnish");
        return Task.CompletedTask;
    }
}