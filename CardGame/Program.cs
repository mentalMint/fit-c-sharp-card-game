using Cards;
using DataBase;
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

    private static IHostBuilder CreateHostBuilderExperimentalConditions()
    {
        return Host.CreateDefaultBuilder()
            .ConfigureServices((hostContext, services) =>
            {
                services.AddHostedService<ColosseumExperimentConditionWorker>();
                services.AddScoped<ISandbox, ColosseumSandboxNotShuffle>();
                services.AddScoped<Player>(_ => new Player("Elon", new FirstCardStrategy()));
                services.AddScoped<Player>(_ => new Player("Mark", new FirstCardStrategy()));
                services.AddDbContext<ColosseumContext>();
            });
    }
    
    private static IHostBuilder CreateHostBuilderDb()
    {
        return Host.CreateDefaultBuilder()
            .ConfigureServices((hostContext, services) =>
            {
                services.AddHostedService<ColosseumExperimentWorkerDb>();
                services.AddScoped<ISandbox, ColosseumSandboxNotShuffle>();
                services.AddScoped<Player>(_ => new Player("Elon", new FirstCardStrategy()));
                services.AddScoped<Player>(_ => new Player("Mark", new FirstCardStrategy()));
                services.AddDbContext<ColosseumContext>();
            });
    }
    
    private static IHostBuilder CreateHostBuilderWeb()
    {
        return Host.CreateDefaultBuilder()
            .ConfigureServices((hostContext, services) =>
            {
                services.AddHostedService<ColosseumExperimentWorkerWeb>();
                services.AddScoped<ISandbox, ColosseumSandboxWeb>();
                services.AddScoped<IPlayer>(_ => new WebPlayer("Elon", new FirstCardStrategy()));
                services.AddScoped<IPlayer>(_ => new WebPlayer("Mark", new FirstCardStrategy()));
                services.AddDbContext<ColosseumContext>();
            });
    }

    private static void Main()
    {
        var host = CreateHostBuilderWeb().Build();
        host.Run();
    }
    
}