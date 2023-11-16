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
    private IPlayer _elon;
    private IPlayer _mark;
    public bool CardsColorsMatched { get; private set; } = false;

    public CollisiumSandbox(ICardDeck cardDeck, IEnumerable<Player> players)
    {
        _cardDeck = cardDeck;
        _players = players;
        _elon = _players.ElementAt(0);
        _mark = _players.ElementAt(1);
    }

    public void Run()
    {
        _cardDeck.Shuffle();
        _cardDeck.SplitMidPoint(out var elonsCardDeck, out var marksCardDeck);
        _elon.CardDeck = elonsCardDeck;
        _mark.CardDeck = marksCardDeck;
        var elonsNumber = _elon.GetCardNumber();
        var marksNumber = _mark.GetCardNumber();
        if (elonsCardDeck == null || marksCardDeck == null)
        {
            throw new NullReferenceException();
        }

        if (marksCardDeck.Cards[elonsNumber].Equals(elonsCardDeck.Cards[marksNumber]))
        {
            CardsColorsMatched = true;
            return;
        }

        CardsColorsMatched = false;
    }
}