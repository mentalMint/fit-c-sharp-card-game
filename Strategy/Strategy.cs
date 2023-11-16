using Cards;
namespace Strategy;


public interface IStrategy
{
    int GetCardNumber();
}

public abstract class Strategy : IStrategy
{
    private ICardDeck? _cardDeckDeck;

    public ICardDeck? CardDeck
    {
        get => _cardDeckDeck;
        set => _cardDeckDeck = value ?? throw new ArgumentNullException(nameof(value));
    }
    
    protected Strategy(CardDeck? cardDeckDeck)
    {
        _cardDeckDeck = cardDeckDeck;
    }

    protected Strategy()
    {
    }

    public abstract int GetCardNumber();
}

public class FirstCardStrategy : Strategy

{
    public FirstCardStrategy(CardDeck? cardDeckDeck) : base(cardDeckDeck)
    {
    }

    public FirstCardStrategy()
    {
    }

    public override int GetCardNumber()
    {
        return 1;
    }
}