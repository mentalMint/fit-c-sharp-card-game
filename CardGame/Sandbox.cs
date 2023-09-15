using Cards;
using Strategy;

namespace CardGame;

public class Sandbox
{
    private readonly CardDeck _cardDeck;
    private readonly Player _ilon;
    private readonly Player _mark;

    public bool CardsColorsMatched { get; private set; } = false;

    public Sandbox(CardDeck cardDeck, Player ilon, Player mark)
    {
        _cardDeck = cardDeck;
        this._ilon = ilon;
        this._mark = mark;
    }

    public void Run()
    {
        _cardDeck.Shuffle();
        _cardDeck.SplitMidPoint(out var ilonsCardDeck, out var marksCardDeck);
        _ilon.CardDeck = ilonsCardDeck;
        _mark.CardDeck = marksCardDeck;
        var ilonsNumber = _ilon.GetCardNumber();
        var marksNumber = _mark.GetCardNumber();
        if (ilonsCardDeck == null || marksCardDeck == null)
        {
            return;
        }

        if (marksCardDeck.Cards[ilonsNumber].Equals(ilonsCardDeck.Cards[marksNumber]))
        {
            CardsColorsMatched = true;
        }
    }
}