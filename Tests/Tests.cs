using CardGame;
using Cards;
using Moq;
using Strategy;

namespace Tests;

public class CardDeckTests
{
    [SetUp]
    public void Setup()
    {
    }

    private int GetCountOfCardsOfColor(ICardDeck cardDeck, Color color)
    {
        var cards = cardDeck.Cards;
        return cards.Count(card => card.color == color);
    }

    [Test]
    public void CreateCardDeck_36_Contains18RedAnd18Black()
    {
        var cardDeck = new CardDeck(36);
        Assert.That(GetCountOfCardsOfColor(cardDeck, Color.Red), Is.EqualTo(18));
        Assert.That(GetCountOfCardsOfColor(cardDeck, Color.Black), Is.EqualTo(18));
    }
}

public class StrategyTests
{
    [SetUp]
    public void Setup()
    {
    }

    private Card[] CreateCardsWithFirstBlack(int cardsNumber)
    {
        var cards = new Card[cardsNumber];
        for (var i = 0; i < cards.Length / 2; i++)
        {
            cards[i] = new Card(Color.Black);
        }

        for (var i = cards.Length / 2; i < cards.Length; i++)
        {
            cards[i] = new Card(Color.Red);
        }

        return cards;
    }

    [Test]
    public void GetCardNumber_FirstCardStrategy_ReturnsBlack()
    {
        var cards = CreateCardsWithFirstBlack(36);
        var mockCardDeck = Mock.Of<CardDeck>();
        var strategy = new FirstCardStrategy(mockCardDeck);
        var cardNumber = strategy.GetCardNumber();
        Assert.That(cards[cardNumber].color, Is.EqualTo(Color.Black));
    }
}

public class ColosseumExperimentTest
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void PlayersGetCardNumber_ShuffledDecks_FirstCardsNumbers()
    {
        var elon = new Player("Elon", new FirstCardStrategy());
        var mark = new Player("Mark", new FirstCardStrategy());
        var cardDeck = new CardDeck(36);
        cardDeck.Shuffle();
        cardDeck.SplitMidPoint(out var elonsCardDeck, out var marksCardDeck);
        elon.CardDeck = elonsCardDeck;
        mark.CardDeck = marksCardDeck;
        var elonsNumber = elon.GetCardNumber();
        var marksNumber = mark.GetCardNumber();
        var elonsCard = elonsCardDeck.Cards[0];
        var marksCard = marksCardDeck.Cards[0];

        var cardsColorsMatched = elonsCardDeck.Cards[elonsNumber].color.Equals(marksCardDeck.Cards[marksNumber].color);

        Assert.That(cardsColorsMatched, Is.EqualTo(elonsCard.color == marksCard.color));
    }

    private Card[] CreateCards(int cardsNumber)
    {
        var cards = new Card[cardsNumber];
        for (var i = 0; i < cards.Length / 2; i++)
        {
            cards[i] = new Card(Color.Black);
        }

        for (var i = cards.Length / 2; i < cards.Length; i++)
        {
            cards[i] = new Card(Color.Red);
        }

        return cards;
    }

    [Test]
    public void DeckShuffles_Once()
    {
        var mockCardDeck = new Mock<ICardDeck>();
        var tuple = (new CardDeck(18), new CardDeck(18));
        mockCardDeck.Setup(cd => cd.SplitMidPoint()).Returns(tuple);
        var elon = new Player("Elon", new FirstCardStrategy());
        var mark = new Player("Mark", new FirstCardStrategy());
        var players = new[] { elon, mark };
        var closseumSandbox = new ColosseumSandbox(mockCardDeck.Object, players);
        closseumSandbox.Run();
        mockCardDeck.Verify(cd => cd.Shuffle(), Times.Once);
    }

    [Test]
    public void ColosseumExperiment_ShuffledDeckFirstCardStrategy_Success()
    {
        var mockCardDeck = new Mock<ICardDeck>();

        var tuple = (new CardDeck(18), new CardDeck(18));

        mockCardDeck.Setup(cd => cd.SplitMidPoint()).Returns(tuple);
        var colosseumSandbox = new ColosseumSandbox(mockCardDeck.Object,
            new[]
            {
                new Player("Elon", new FirstCardStrategy()),
                new Player("Mark", new FirstCardStrategy())
            });
        colosseumSandbox.Run();
        Assert.That(colosseumSandbox.CardsColorsMatched, Is.EqualTo(true));
    }
}