using Cards;
using Microsoft.Extensions.Hosting;

namespace CardGame;

public interface ISandbox
{
    void Run();
    bool CardsColorsMatched { get; }
}

public class CollisiumSandbox : ISandbox
{
    private readonly ICardDeck _cardDeck;
    private readonly IEnumerable<Player> _players;
    private IPlayer _ilon;
    private IPlayer _mark;
    public bool CardsColorsMatched { get; private set; } = false;

    public CollisiumSandbox(ICardDeck cardDeck, IEnumerable<Player> players)
    {
        _cardDeck = cardDeck;
        _players = players;
        _ilon = _players.ElementAt(0);
        _mark = _players.ElementAt(1);
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
            throw new NullReferenceException();
        }

        if (marksCardDeck.Cards[ilonsNumber].Equals(ilonsCardDeck.Cards[marksNumber]))
        {
            CardsColorsMatched = true;
            return;
        }

        CardsColorsMatched = false;
    }
}