using Cards;

namespace CardGame;

public class Player
{
    private readonly Strategy.Strategy _strategy;

    private CardDeck _cardDeck;
    public CardDeck CardDeck
    {
        get => _cardDeck;
        set
        {
            _cardDeck = value;
            _strategy.CardDeck = value;
        }
    }

    public Player(Strategy.Strategy strategy)
    {
        _strategy = strategy ?? throw new ArgumentNullException(nameof(strategy));
    }

    public int GetCardNumber()
    {
        return _strategy.GetCardNumber();
    }
}