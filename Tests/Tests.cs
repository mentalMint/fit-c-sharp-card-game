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

    private int GetNumberOfCardsOfColor(ICardDeck cardDeck, Color color)
    {
        var cards = cardDeck.Cards;
        int cardsOfColorNumber = 0;
        foreach (var card in cards)
        {
            if (card.color == color)
            {
                cardsOfColorNumber++;
            }
        }

        return cardsOfColorNumber;
    }

    [Test]
    public void CreateCardDeck_36_Contains18RedAnd18Black()
    {
        var cardDeck = new CardDeck(36);
        Assert.That(GetNumberOfCardsOfColor(cardDeck, Color.Red), Is.EqualTo(18));
        Assert.That(GetNumberOfCardsOfColor(cardDeck, Color.Black), Is.EqualTo(18));
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
    public void FirstCardStrategyReturnsBlack()
    {
        var cards = CreateCardsWithFirstBlack(36);
        var mockCardDeck = Mock.Of<CardDeck>();
        var strategy = new FirstCardStrategy(mockCardDeck);
        int cardNumber = strategy.GetCardNumber();
        Assert.That(cards[cardNumber].color, Is.EqualTo(Color.Black));
    }
}

public class CollisiumExperimentTest
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void FirstCardStrategyReturnsBlack()
    {
        var elon = new Player("Elon", new FirstCardStrategy());
        var mark = new Player("Mark", new FirstCardStrategy());
        var cardDeck = new CardDeck(36);
        cardDeck.SplitMidPoint(out var elonsCardDeck, out var marksCardDeck);
        elon.CardDeck = elonsCardDeck;
        mark.CardDeck = marksCardDeck;
        var elonsNumber = elon.GetCardNumber();
        var marksNumber = mark.GetCardNumber();
        if (elonsCardDeck == null || marksCardDeck == null)
        {
            throw new NullReferenceException();
        }

        bool cardsColorsMatched = marksCardDeck.Cards[elonsNumber].Equals(elonsCardDeck.Cards[marksNumber]);
        
        Assert.That(cardsColorsMatched, Is.EqualTo(true));
    }
}