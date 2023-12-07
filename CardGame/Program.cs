using Cards;
using DataBase;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Strategy;

namespace CardGame;

internal static class Program
{
    private static IHostBuilder CreateHostBuilder()
    {
        return Host.CreateDefaultBuilder()
            .ConfigureServices((hostContext, services) =>
            {
                services.AddHostedService<ColosseumExperimentWorker>();
                services.AddScoped<ISandbox, ColosseumSandbox>();
                services.AddScoped<ICardDeck>(s => new CardDeck(36));
                services.AddScoped<Player>(_ => new Player("Elon", new FirstCardStrategy()));
                services.AddScoped<Player>(_ => new Player("Mark", new FirstCardStrategy()));
            });
    }
    
    private static IHostBuilder CreateHostBuilderDb()
    {
        return Host.CreateDefaultBuilder()
            .ConfigureServices((hostContext, services) =>
            {
                services.AddHostedService<ColosseumExperimentWorkerDB>();
                services.AddScoped<ISandbox, ColosseumSandboxNotShuffle>();
                services.AddScoped<Player>(_ => new Player("Elon", new FirstCardStrategy()));
                services.AddScoped<Player>(_ => new Player("Mark", new FirstCardStrategy()));
            });
    }

    private static void Main()
    {
        using var db = new ColosseumContext();
        foreach (var cardDeck in CardDeckGenerator.Generate(100))
        {
            db.Add(new ExperimentalCondition { cards_order = cardDeck.ToString() });
            // Console.WriteLine(cardDeck.ToString());
        }
        db.SaveChanges();

        
//
// // Create
//         Console.WriteLine("Inserting a new experimental condition");
//         db.Add(new ExperimentalCondition { cards_order = "RBRB" });
//         db.SaveChanges();
//
// // Read
//         Console.WriteLine("Querying for an experimental condition");
//         var experimentalCondition = db.experimental_conditions
//             .OrderBy(b => b.cards_order)
//             .First();
//
// // Update
//         Console.WriteLine("Updating the experimental condition");
//         experimentalCondition.cards_order = "BBRR";
//         db.SaveChanges();
//
// // Delete
//         Console.WriteLine("Delete the experimental condition");
//         db.Remove(experimentalCondition);
//         db.SaveChanges();

        var host = CreateHostBuilderDb().Build();
        host.Run();
    }
}