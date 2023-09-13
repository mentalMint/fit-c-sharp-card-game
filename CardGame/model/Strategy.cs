namespace CardGame.model;

public interface IStrategy
{
    int GetCardNumber();
}

public abstract class Strategy : IStrategy
{
    private CardDeck? _cards;

    public CardDeck? Cards
    {
        get => _cards;
        set => _cards = value ?? throw new ArgumentNullException(nameof(value));
    }

    protected Strategy(CardDeck? cards)
    {
        _cards = cards;
    }

    protected Strategy()
    {
    }

    public abstract int GetCardNumber();
}

public class FirstCardStrategy : Strategy

{
    public FirstCardStrategy(CardDeck? cards) : base(cards)
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