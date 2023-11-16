using Cards;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Strategy;

namespace CardGame;

static class Program
{
    private static IHostBuilder CreateHostBuilder()
    {
        return Host.CreateDefaultBuilder()
            .ConfigureServices((hostContext, services) =>
            {
                services.AddHostedService<CollisiumExperimentWorker>();
                services.AddScoped<ISandbox, CollisiumSandbox>();
                services.AddScoped<ICardDeck>(s => new CardDeck(36));
                services.AddScoped<Player>(_ => new Player("Elon", new FirstCardStrategy()));
                services.AddScoped<Player>(_ => new Player("Mark", new FirstCardStrategy()));
            });
    }

    static void Main(string[] args)
    {
        var host = CreateHostBuilder().Build();
        host.Run();
    }
}