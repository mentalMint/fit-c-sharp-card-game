using Cards;
using Strategy;

namespace CardGame;

public class Sandbox
{
    private readonly CardDeck _cardDeck;
    private readonly Strategy.Strategy _ilonsStrategy;
    private readonly Strategy.Strategy _marksStrategy;

    public bool CardsColorsMatched { get; private set; } = false;

    public Sandbox(CardDeck cardDeck, Strategy.Strategy ilonsStrategy, Strategy.Strategy marksStrategy)
    {
        _cardDeck = cardDeck;
        _ilonsStrategy = ilonsStrategy ?? throw new ArgumentNullException(nameof(ilonsStrategy));
        _marksStrategy = marksStrategy ?? throw new ArgumentNullException(nameof(marksStrategy));
    }

    public void Run()
    {
        _cardDeck.Shuffle();
        _cardDeck.SplitMidPoint(out var ilonsCards, out var marksCards);
        _ilonsStrategy.Cards = ilonsCards;
        _marksStrategy.Cards = marksCards;
        var ilonsNumber = _ilonsStrategy.GetCardNumber();
        var marksNumber = _marksStrategy.GetCardNumber();
        if (ilonsCards == null || marksCards == null)
        {
            return;
        }

        if (marksCards.Cards[ilonsNumber].Equals(ilonsCards.Cards[marksNumber]))
        {
            CardsColorsMatched = true;
        }
    }
}