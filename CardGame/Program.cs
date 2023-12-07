using Cards;
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

    private static void Main(string[] args)
    {
        var host = CreateHostBuilder().Build();
        host.Run();
    }
}