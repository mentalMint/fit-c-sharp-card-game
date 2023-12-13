using Cards;
using DataBase;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Tests;

public class DbTests
{
    private SqliteConnection _connection;
    private DbContextOptions<ColosseumContext> _contextOptions;

    [SetUp]
    public void Setup()
    {
        _connection = new SqliteConnection("Data source=:memory:");

        _contextOptions = new DbContextOptionsBuilder<ColosseumContext>()
            .UseSqlite(_connection)
            .Options;
    }

    private ColosseumContext CreateContext() => new(_contextOptions);

    [TearDown]
    public void Dispose()
    {
        _connection.Dispose();
    }

    [Test]
    public void ColosseumContext_AddExperimentalCondition_AddOneExperimentCondition()
    {
        var context = CreateContext();
        context.Database.EnsureCreated();

        context.Add(new ExperimentalCondition { CardsOrder = new CardDeck(36).ToString() });
        context.SaveChanges();

        var condition = context.Find<ExperimentalCondition>(1);
        Assert.That(condition, Is.Not.Null);
    }

    [Test]
    public void ColosseumContext_AddExperimentalCondition_GetSameExperimentCondition()
    {
        var context = CreateContext();
        context.Database.EnsureCreated();

        context.Add(new ExperimentalCondition { CardsOrder = new CardDeck(36).ToString() });
        context.SaveChanges();

        var cardDeck = new CardDeck(36);
        context.Add(new ExperimentalCondition { CardsOrder = cardDeck.ToString() });
        context.SaveChanges();

        var experimentalCondition = context.ExperimentalConditions.First();
        Assert.That(experimentalCondition.CardsOrder, Is.EqualTo(cardDeck.ToString()));
    }
}