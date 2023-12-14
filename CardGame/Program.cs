﻿using Cards;
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

    private static void Main()
    {
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
    
    static async Task SendPostRequest()
    {
        // Replace the URL with the actual endpoint you want to send the POST request to
        string apiUrl = "https://example.com/api/endpoint";

        // Data to be sent in the request body (you can replace this with your own data)
        string postData = "{\"key\": \"value\"}";

        // Create an instance of HttpClient
        using (HttpClient httpClient = new HttpClient())
        {
            // Create the content to be sent in the request body
            HttpContent content = new StringContent(postData, System.Text.Encoding.UTF8, "application/json");

            try
            {
                // Send the POST request and get the response
                HttpResponseMessage response = await httpClient.PostAsync(apiUrl, content);

                // Check if the request was successful (status code 2xx)
                if (response.IsSuccessStatusCode)
                {
                    // Read and output the response content
                    string responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Response: " + responseContent);
                }
                else
                {
                    Console.WriteLine("Error: " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
        }
    }
}