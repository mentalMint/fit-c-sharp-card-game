using System.Numerics;

namespace CardGame.model.strategies;

public interface IStrategy
{
    void SetCards(Vector<Card> cards);

    int GetCardNumber();
}

public abstract class Strategy : IStrategy
{
    private Vector<Card> _cards;

    protected Strategy(Vector<Card> cards)
    {
        _cards = cards;
    }

    protected Strategy()
    {
    }

    public void SetCards(Vector<Card> cards)
    {
        _cards = cards;
    }

    public abstract int GetCardNumber();
}

public class FirstCardStrategy : Strategy
{
    public override int GetCardNumber()
    {
        return 1;
    }
}