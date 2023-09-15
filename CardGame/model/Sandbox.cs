using CardGameStrategy;

namespace CardGame.model;

public class Sandbox : Observable
{
    private readonly CardDeck _cardDeck;
    private readonly Strategy _ilonsStrategy;
    private readonly Strategy _marksStrategy;

    public bool CardsColorsMatched { get; private set; } = false;

    public Sandbox(CardDeck cardDeck, Strategy ilonsStrategy, Strategy marksStrategy)
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
        _marksStrategy.Thing = "aaaa"; 
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

        foreach (var observer in Observers)
        {
            observer.OnNext(CardsColorsMatched);
        }
    }
}