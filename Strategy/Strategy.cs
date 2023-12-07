using Cards;
namespace Strategy;


public interface IStrategy
{
    int GetCardNumber();
}

public abstract class Strategy : IStrategy
{
    private ICardDeck? _cardDeck;

    public ICardDeck? CardDeck
    {
        get => _cardDeck;
        set => _cardDeck = value ?? throw new ArgumentNullException(nameof(value));
    }
    
    protected Strategy(CardDeck? cardDeck)
    {
        _cardDeck = cardDeck;
    }

    protected Strategy()
    {
    }

    public abstract int GetCardNumber();
}

public class FirstCardStrategy : Strategy
{
    public FirstCardStrategy(CardDeck? cardDeck) : base(cardDeck)
    {
    }

    public FirstCardStrategy()
    {
    }

    public override int GetCardNumber()
    {
        return 0;
    }
}